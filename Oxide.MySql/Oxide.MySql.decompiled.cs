using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using MySql.Data.MySqlClient;
using Oxide.Core.Database;
using Oxide.Core.Extensions;
using Oxide.Core.Libraries;
using Oxide.Core.MySql.Libraries;
using Oxide.Core.Plugins;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
[assembly: AssemblyCompany("Oxide Team and Contributors")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("(c) 2013-2025 Oxide Team and Contributors")]
[assembly: AssemblyDescription("MySQL database support for the Oxide modding framework")]
[assembly: AssemblyFileVersion("2.0.3778.0")]
[assembly: AssemblyInformationalVersion("2.0.3778+bb3b9f7b08995bd40ce223294dcf370ea37e72b1")]
[assembly: AssemblyProduct("Oxide.MySql")]
[assembly: AssemblyTitle("Oxide.MySql")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/OxideMod/Oxide.MySQL")]
[assembly: AssemblyVersion("2.0.3778.0")]
namespace Oxide.Core.MySql
{
	public class MySqlExtension : Extension
	{
		internal static Assembly Assembly = Assembly.GetExecutingAssembly();

		internal static AssemblyName AssemblyName = Assembly.GetName();

		internal static VersionNumber AssemblyVersion = new VersionNumber(AssemblyName.Version.Major, AssemblyName.Version.Minor, AssemblyName.Version.Build);

		internal static string AssemblyAuthors = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly, typeof(AssemblyCompanyAttribute), inherit: false)).Company;

		public override bool IsCoreExtension => true;

		public override string Name => "MySql";

		public override string Author => AssemblyAuthors;

		public override VersionNumber Version => AssemblyVersion;

		public MySqlExtension(ExtensionManager manager)
			: base(manager)
		{
		}

		public override void Load()
		{
			base.Manager.RegisterLibrary("MySql", new Oxide.Core.MySql.Libraries.MySql());
		}

		public override void LoadPluginWatchers(string pluginDirectory)
		{
		}

		public override void OnModLoad()
		{
		}
	}
}
namespace Oxide.Core.MySql.Libraries
{
	public class MySql : Library, IDatabaseProvider
	{
		public class MySqlQuery
		{
			private MySqlCommand _cmd;

			private MySqlConnection _connection;

			private IAsyncResult _result;

			public Action<List<Dictionary<string, object>>> Callback { get; internal set; }

			public Action<int> CallbackNonQuery { get; internal set; }

			public Sql Sql { get; internal set; }

			public Connection Connection { get; internal set; }

			public bool NonQuery { get; internal set; }

			private void Cleanup()
			{
				if (_cmd != null)
				{
					_cmd.Dispose();
					_cmd = null;
				}
				_connection = null;
			}

			public bool Handle()
			{
				List<Dictionary<string, object>> list = null;
				int nonQueryResult = 0;
				long lastInsertRowId = 0L;
				try
				{
					if (Connection == null)
					{
						throw new Exception("Connection is null");
					}
					_connection = (MySqlConnection)Connection.Con;
					if (_connection.State == ConnectionState.Closed)
					{
						_connection.Open();
					}
					_cmd = _connection.CreateCommand();
					_cmd.CommandTimeout = 120;
					_cmd.CommandText = Sql.SQL;
					Sql.AddParams(_cmd, Sql.Arguments, "@");
					_result = (NonQuery ? _cmd.BeginExecuteNonQuery() : _cmd.BeginExecuteReader());
					_result.AsyncWaitHandle.WaitOne();
					if (NonQuery)
					{
						nonQueryResult = _cmd.EndExecuteNonQuery(_result);
					}
					else
					{
						using MySqlDataReader mySqlDataReader = _cmd.EndExecuteReader(_result);
						list = new List<Dictionary<string, object>>();
						while (mySqlDataReader.Read() && (!Connection.ConnectionPersistent || (Connection.Con.State != ConnectionState.Closed && Connection.Con.State != ConnectionState.Broken)))
						{
							Dictionary<string, object> dictionary = new Dictionary<string, object>();
							for (int i = 0; i < mySqlDataReader.FieldCount; i++)
							{
								dictionary.Add(mySqlDataReader.GetName(i), mySqlDataReader.GetValue(i));
							}
							list.Add(dictionary);
						}
					}
					lastInsertRowId = _cmd.LastInsertedId;
					Cleanup();
				}
				catch (Exception ex)
				{
					string text = "MySql handle raised an exception";
					if (Connection?.Plugin != null)
					{
						text += $" in '{Connection.Plugin.Name} v{Connection.Plugin.Version}' plugin";
					}
					Interface.Oxide.LogException(text, ex);
					Cleanup();
				}
				Interface.Oxide.NextTick(delegate
				{
					Connection?.Plugin?.TrackStart();
					try
					{
						if (Connection != null)
						{
							Connection.LastInsertRowId = lastInsertRowId;
						}
						if (!NonQuery)
						{
							Callback(list);
						}
						else
						{
							CallbackNonQuery?.Invoke(nonQueryResult);
						}
					}
					catch (Exception ex2)
					{
						string text2 = "MySql command callback raised an exception";
						if (Connection?.Plugin != null)
						{
							text2 += $" in '{Connection.Plugin.Name} v{Connection.Plugin.Version}' plugin";
						}
						Interface.Oxide.LogException(text2, ex2);
					}
					Connection?.Plugin?.TrackEnd();
				});
				return true;
			}
		}

		private readonly Queue<MySqlQuery> _queue = new Queue<MySqlQuery>();

		private readonly object _syncroot = new object();

		private readonly AutoResetEvent _workevent = new AutoResetEvent(initialState: false);

		private readonly HashSet<Connection> _runningConnections = new HashSet<Connection>();

		private bool _running = true;

		private readonly Dictionary<string, Dictionary<string, Connection>> _connections = new Dictionary<string, Dictionary<string, Connection>>();

		private readonly Thread _worker;

		private readonly Dictionary<Plugin, Event.Callback<Plugin, PluginManager>> _pluginRemovedFromManager;

		public override bool IsGlobal => false;

		public MySql()
		{
			_pluginRemovedFromManager = new Dictionary<Plugin, Event.Callback<Plugin, PluginManager>>();
			_worker = new Thread(Worker);
			_worker.Start();
		}

		private void Worker()
		{
			while (_running || _queue.Count > 0)
			{
				MySqlQuery mySqlQuery = null;
				lock (_syncroot)
				{
					if (_queue.Count > 0)
					{
						mySqlQuery = _queue.Dequeue();
					}
					else
					{
						foreach (Connection runningConnection in _runningConnections)
						{
							if (runningConnection != null && !runningConnection.ConnectionPersistent)
							{
								CloseDb(runningConnection);
							}
						}
						_runningConnections.Clear();
					}
				}
				if (mySqlQuery != null)
				{
					mySqlQuery.Handle();
					if (mySqlQuery.Connection != null)
					{
						_runningConnections.Add(mySqlQuery.Connection);
					}
				}
				else if (_running)
				{
					_workevent.WaitOne();
				}
			}
		}

		[LibraryFunction("OpenDb")]
		public Connection OpenDb(string host, int port, string database, string user, string password, Plugin plugin, bool persistent = false)
		{
			return OpenDb($"Server={host};Port={port};Database={database};User={user};Password={password};Pooling=false;default command timeout=120;Allow Zero Datetime=true;", plugin, persistent);
		}

		public Connection OpenDb(string conStr, Plugin plugin, bool persistent = false)
		{
			if (!_connections.TryGetValue(plugin?.Name ?? "null", out var value))
			{
				value = (_connections[plugin?.Name ?? "null"] = new Dictionary<string, Connection>());
			}
			if (value.TryGetValue(conStr, out var value2))
			{
				Interface.Oxide.LogWarning("There is already an open connection, using existing connection instead...");
			}
			else
			{
				Connection obj = new Connection(conStr, persistent)
				{
					Plugin = plugin,
					Con = new MySqlConnection(conStr)
				};
				value2 = (value[conStr] = obj);
			}
			if (plugin != null && !_pluginRemovedFromManager.ContainsKey(plugin))
			{
				_pluginRemovedFromManager[plugin] = plugin.OnRemovedFromManager.Add(OnRemovedFromManager);
			}
			return value2;
		}

		private void OnRemovedFromManager(Plugin sender, PluginManager manager)
		{
			if (_connections.TryGetValue(sender.Name, out var value))
			{
				foreach (KeyValuePair<string, Connection> item in value)
				{
					if (item.Value.Plugin == sender)
					{
						DbConnection con = item.Value.Con;
						if (con == null || con.State != ConnectionState.Closed)
						{
							Interface.Oxide.LogWarning("Unclosed MySQL connection from plugin '{0}', closing...", sender.Name);
						}
						item.Value.Con?.Close();
						item.Value.Plugin = null;
					}
				}
				_connections.Remove(sender.Name);
			}
			if (_pluginRemovedFromManager.TryGetValue(sender, out var value2))
			{
				value2.Remove();
				_pluginRemovedFromManager.Remove(sender);
			}
		}

		[LibraryFunction("CloseDb")]
		public void CloseDb(Connection db)
		{
			if (db == null)
			{
				return;
			}
			if (_connections.TryGetValue(db.Plugin?.Name ?? "null", out var value))
			{
				value.Remove(db.ConnectionString);
				if (value.Count == 0)
				{
					_connections.Remove(db.Plugin?.Name ?? "null");
					if (db.Plugin != null && _pluginRemovedFromManager.TryGetValue(db.Plugin, out var value2))
					{
						value2.Remove();
						_pluginRemovedFromManager.Remove(db.Plugin);
					}
				}
			}
			db.Con?.Close();
			db.Plugin = null;
		}

		[LibraryFunction("NewSql")]
		public Sql NewSql()
		{
			return Sql.Builder;
		}

		[LibraryFunction("Query")]
		public void Query(Sql sql, Connection db, Action<List<Dictionary<string, object>>> callback)
		{
			MySqlQuery item = new MySqlQuery
			{
				Sql = sql,
				Connection = db,
				Callback = callback
			};
			lock (_syncroot)
			{
				_queue.Enqueue(item);
			}
			_workevent.Set();
		}

		[LibraryFunction("ExecuteNonQuery")]
		public void ExecuteNonQuery(Sql sql, Connection db, Action<int> callback = null)
		{
			MySqlQuery item = new MySqlQuery
			{
				Sql = sql,
				Connection = db,
				CallbackNonQuery = callback,
				NonQuery = true
			};
			lock (_syncroot)
			{
				_queue.Enqueue(item);
			}
			_workevent.Set();
		}

		[LibraryFunction("Insert")]
		public void Insert(Sql sql, Connection db, Action<int> callback = null)
		{
			ExecuteNonQuery(sql, db, callback);
		}

		[LibraryFunction("Update")]
		public void Update(Sql sql, Connection db, Action<int> callback = null)
		{
			ExecuteNonQuery(sql, db, callback);
		}

		[LibraryFunction("Delete")]
		public void Delete(Sql sql, Connection db, Action<int> callback = null)
		{
			ExecuteNonQuery(sql, db, callback);
		}

		public override void Shutdown()
		{
			_running = false;
			_workevent.Set();
			_worker.Join();
		}
	}
}
namespace Oxide.Ext.MySql
{
	public sealed class Connection
	{
		internal string ConnectionString { get; set; }

		internal bool ConnectionPersistent { get; set; }

		internal MySqlConnection Con { get; set; }

		internal Plugin Plugin { get; set; }

		public long LastInsertRowId { get; internal set; }

		public Connection(string connection, bool persistent)
		{
			ConnectionString = connection;
			ConnectionPersistent = persistent;
		}
	}
	public class Sql
	{
		public class SqlJoinClause
		{
			private readonly Sql _sql;

			public SqlJoinClause(Sql sql)
			{
				_sql = sql;
			}

			public Sql On(string onClause, params object[] args)
			{
				return _sql.Append("ON " + onClause, args);
			}
		}

		private static readonly Regex Filter = new Regex("LOAD\\s*DATA|INTO\\s*(OUTFILE|DUMPFILE)|LOAD_FILE", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		private static readonly Regex RxParams = new Regex("(?<!@)@\\w+", RegexOptions.Compiled);

		private readonly object[] _args;

		private readonly string _sql;

		private object[] _argsFinal;

		private Sql _rhs;

		private string _sqlFinal;

		public static Sql Builder => new Sql();

		public string SQL
		{
			get
			{
				Build();
				return _sqlFinal;
			}
		}

		public object[] Arguments
		{
			get
			{
				Build();
				return _argsFinal;
			}
		}

		public Sql()
		{
		}

		public Sql(string sql, params object[] args)
		{
			_sql = sql;
			_args = args;
		}

		private void Build()
		{
			if (_sqlFinal == null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				List<object> list = new List<object>();
				Build(stringBuilder, list, null);
				string text = stringBuilder.ToString();
				if (Filter.IsMatch(text))
				{
					throw new Exception("Commands LOAD DATA, LOAD_FILE, OUTFILE, DUMPFILE not allowed.");
				}
				_sqlFinal = text;
				_argsFinal = list.ToArray();
			}
		}

		public Sql Append(Sql sql)
		{
			if (_rhs != null)
			{
				_rhs.Append(sql);
			}
			else
			{
				_rhs = sql;
			}
			return this;
		}

		public Sql Append(string sql, params object[] args)
		{
			return Append(new Sql(sql, args));
		}

		private static bool Is(Sql sql, string sqltype)
		{
			if (sql != null && sql._sql != null)
			{
				return sql._sql.StartsWith(sqltype, StringComparison.InvariantCultureIgnoreCase);
			}
			return false;
		}

		private void Build(StringBuilder sb, List<object> args, Sql lhs)
		{
			if (!string.IsNullOrEmpty(_sql))
			{
				if (sb.Length > 0)
				{
					sb.Append("\n");
				}
				string text = ProcessParams(_sql, _args, args);
				if (Is(lhs, "WHERE ") && Is(this, "WHERE "))
				{
					text = "AND " + text.Substring(6);
				}
				if (Is(lhs, "ORDER BY ") && Is(this, "ORDER BY "))
				{
					text = ", " + text.Substring(9);
				}
				sb.Append(text);
			}
			_rhs?.Build(sb, args, this);
		}

		public Sql Where(string sql, params object[] args)
		{
			return Append(new Sql("WHERE (" + sql + ")", args));
		}

		public Sql OrderBy(params object[] columns)
		{
			return Append(new Sql("ORDER BY " + string.Join(", ", columns.Select((object x) => x.ToString()).ToArray())));
		}

		public Sql Select(params object[] columns)
		{
			return Append(new Sql("SELECT " + string.Join(", ", columns.Select((object x) => x.ToString()).ToArray())));
		}

		public Sql From(params object[] tables)
		{
			return Append(new Sql("FROM " + string.Join(", ", tables.Select((object x) => x.ToString()).ToArray())));
		}

		public Sql GroupBy(params object[] columns)
		{
			return Append(new Sql("GROUP BY " + string.Join(", ", columns.Select((object x) => x.ToString()).ToArray())));
		}

		private SqlJoinClause Join(string joinType, string table)
		{
			return new SqlJoinClause(Append(new Sql(joinType + table)));
		}

		public SqlJoinClause InnerJoin(string table)
		{
			return Join("INNER JOIN ", table);
		}

		public SqlJoinClause LeftJoin(string table)
		{
			return Join("LEFT JOIN ", table);
		}

		public static string ProcessParams(string sql, object[] argsSrc, List<object> argsDest)
		{
			return RxParams.Replace(sql, delegate(Match m)
			{
				string text = m.Value.Substring(1);
				object obj;
				if (int.TryParse(text, out var result))
				{
					if (result < 0 || result >= argsSrc.Length)
					{
						throw new ArgumentOutOfRangeException($"Parameter '@{result}' specified but only {argsSrc.Length} parameters supplied (in `{sql}`)");
					}
					obj = argsSrc[result];
				}
				else
				{
					bool flag = false;
					obj = null;
					object[] array = argsSrc;
					foreach (object obj2 in array)
					{
						PropertyInfo property = obj2.GetType().GetProperty(text);
						if (!(property == null))
						{
							obj = property.GetValue(obj2, null);
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new ArgumentException("Parameter '@" + text + "' specified but none of the passed arguments have a property with this name (in '" + sql + "')");
					}
				}
				if (obj is IEnumerable && !(obj is string) && !(obj is byte[]))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object item in obj as IEnumerable)
					{
						stringBuilder.Append(((stringBuilder.Length == 0) ? "@" : ",@") + argsDest.Count);
						argsDest.Add(item);
					}
					return stringBuilder.ToString();
				}
				argsDest.Add(obj);
				return "@" + (argsDest.Count - 1);
			});
		}

		public static void AddParams(IDbCommand cmd, object[] items, string parameterPrefix)
		{
			foreach (object item in items)
			{
				AddParam(cmd, item, "@");
			}
		}

		public static void AddParam(IDbCommand cmd, object item, string parameterPrefix)
		{
			if (item is IDbDataParameter dbDataParameter)
			{
				dbDataParameter.ParameterName = $"{parameterPrefix}{cmd.Parameters.Count}";
				cmd.Parameters.Add(dbDataParameter);
				return;
			}
			IDbDataParameter dbDataParameter2 = cmd.CreateParameter();
			dbDataParameter2.ParameterName = $"{parameterPrefix}{cmd.Parameters.Count}";
			if (item == null)
			{
				dbDataParameter2.Value = DBNull.Value;
			}
			else
			{
				Type type = item.GetType();
				if (type.IsEnum)
				{
					dbDataParameter2.Value = (int)item;
				}
				else if (type == typeof(Guid))
				{
					dbDataParameter2.Value = item.ToString();
					dbDataParameter2.DbType = DbType.String;
					dbDataParameter2.Size = 40;
				}
				else if (type == typeof(string))
				{
					dbDataParameter2.Size = Math.Max(((string)item).Length + 1, 4000);
					dbDataParameter2.Value = item;
				}
				else if (type == typeof(bool))
				{
					dbDataParameter2.Value = (((bool)item) ? 1 : 0);
				}
				else
				{
					dbDataParameter2.Value = item;
				}
			}
			cmd.Parameters.Add(dbDataParameter2);
		}
	}
}
