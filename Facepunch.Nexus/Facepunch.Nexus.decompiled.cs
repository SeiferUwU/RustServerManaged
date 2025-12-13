using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Facepunch.Nexus.Connector;
using Facepunch.Nexus.Logging;
using Facepunch.Nexus.Models;
using Facepunch.Nexus.Time;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Facepunch.Nexus.Test")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = ".NET Standard 2.0")]
[assembly: AssemblyCompany("Facepunch.Nexus")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("1.0.0+5f94d7877b03eb531349cfb05301555375672d44")]
[assembly: AssemblyProduct("Facepunch.Nexus")]
[assembly: AssemblyTitle("Facepunch.Nexus")]
[assembly: AssemblyVersion("1.0.0.0")]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class IsReadOnlyAttribute : Attribute
	{
	}
}
namespace Facepunch.Nexus
{
	internal readonly struct ApiResult
	{
		public HttpStatusCode StatusCode { get; }

		public bool IsSuccess
		{
			get
			{
				if (StatusCode >= HttpStatusCode.OK)
				{
					return StatusCode <= (HttpStatusCode)299;
				}
				return false;
			}
		}

		public ApiResult(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
		}

		public void EnsureSuccessful()
		{
			if (!IsSuccess)
			{
				throw new HttpRequestException($"API call was not successful: {StatusCode}");
			}
		}
	}
	internal readonly struct ApiResult<T>
	{
		private readonly T _response;

		private readonly bool _hasResponse;

		public HttpStatusCode StatusCode { get; }

		public bool IsSuccess
		{
			get
			{
				if (StatusCode >= HttpStatusCode.OK)
				{
					return StatusCode <= (HttpStatusCode)299;
				}
				return false;
			}
		}

		public T Response
		{
			get
			{
				EnsureSuccessfulWithResponse();
				return _response;
			}
		}

		public ApiResult(HttpStatusCode statusCode, T response)
		{
			StatusCode = statusCode;
			_response = response;
			_hasResponse = true;
		}

		public ApiResult(HttpStatusCode statusCode)
		{
			StatusCode = statusCode;
			_response = default(T);
			_hasResponse = false;
		}

		public void EnsureSuccessful()
		{
			if (!IsSuccess)
			{
				throw new HttpRequestException($"API call was not successful: {StatusCode}");
			}
		}

		public void EnsureSuccessfulWithResponse()
		{
			EnsureSuccessful();
			if (!_hasResponse)
			{
				throw new HttpRequestException($"API call did not return a response: {StatusCode}");
			}
		}

		public bool TryGetResponse(out T response)
		{
			if (IsSuccess && _hasResponse)
			{
				response = _response;
				return true;
			}
			response = default(T);
			return false;
		}
	}
	public abstract class BaseNexusClient : NexusAnonymousClient, IDisposable
	{
		protected readonly IClockProvider Clock;

		protected readonly CancellationToken CancellationToken;

		private readonly CancellationTokenSource _cts;

		private readonly double _updateInterval;

		private bool _disposed;

		private bool _started;

		protected virtual bool ShouldUpdateVariables => _started;

		public event NexusVariableChangedHandler OnVariableChanged;

		public event NexusErrorHandler OnError;

		protected BaseNexusClient(INexusConnector connector, IClockProvider clock, double updateInterval)
			: base(connector, clock)
		{
			_cts = new CancellationTokenSource();
			_updateInterval = updateInterval;
			Clock = clock ?? throw new ArgumentNullException("clock");
			CancellationToken = _cts.Token;
		}

		~BaseNexusClient()
		{
			Dispose(disposing: false);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				throw new ObjectDisposedException(GetType().Name);
			}
			_disposed = true;
			_cts.Cancel();
		}

		public async Task Start()
		{
			if (_started)
			{
				throw new InvalidOperationException("The " + GetType().Name + " was already started");
			}
			await Initialize(CancellationToken);
			UpdateLoop(CancellationToken);
			_started = true;
		}

		protected abstract ValueTask Initialize(CancellationToken ct);

		protected abstract ValueTask Update(CancellationToken ct);

		private async Task UpdateLoop(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested)
			{
				await Clock.Delay(_updateInterval);
				try
				{
					await Update(ct);
				}
				catch (Exception ex) when (!(ex is OperationCanceledException))
				{
					DispatchError(ex);
				}
			}
		}

		internal void UpdateVariables(VariableContainer container, Dictionary<string, VariableData> updatedData)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (updatedData == null || updatedData.Count == 0)
			{
				return;
			}
			NexusVariableChangedHandler nexusVariableChangedHandler = this.OnVariableChanged;
			HashSet<string> hashSet = ((ShouldUpdateVariables && nexusVariableChangedHandler != null) ? new HashSet<string>() : null);
			container.UpdateFromModel(updatedData, hashSet);
			if (nexusVariableChangedHandler == null || hashSet == null)
			{
				return;
			}
			foreach (string item in hashSet)
			{
				try
				{
					nexusVariableChangedHandler(this, item);
				}
				catch (Exception exception)
				{
					DispatchError(exception);
				}
			}
		}

		internal void DispatchVariableChanged(string key)
		{
			try
			{
				this.OnVariableChanged?.Invoke(this, key);
			}
			catch (Exception exception)
			{
				DispatchError(exception);
			}
		}

		internal void DispatchError(Exception exception)
		{
			try
			{
				this.OnError?.Invoke(this, exception);
			}
			catch
			{
			}
		}
	}
	public abstract class BaseNexusPlayerClient : BaseNexusClient
	{
		private protected readonly VariableContainer NexusVariables;

		private protected readonly VariableContainer ZoneVariables;

		private protected readonly VariableContainer PlayerVariables;

		public int NexusId { get; protected set; }

		public string ZoneKey { get; protected set; }

		public NexusDetails Nexus { get; protected set; }

		public NexusZoneDetails Zone => FindZone(Nexus, ZoneKey);

		public PlayerDetails Player { get; protected set; }

		protected override bool ShouldUpdateVariables
		{
			get
			{
				if (base.ShouldUpdateVariables)
				{
					return NexusId > 0;
				}
				return false;
			}
		}

		internal BaseNexusPlayerClient(INexusConnector connector, IClockProvider clock, double updateInterval)
			: base(connector, clock, updateInterval)
		{
			NexusVariables = new VariableContainer();
			ZoneVariables = new VariableContainer();
			PlayerVariables = new VariableContainer();
		}

		protected internal abstract ValueTask<PlayerDetails> GetPlayerDetails(int nexusId);

		public async ValueTask<PlayerDetails> SelectNexus(int nexusId)
		{
			if (nexusId <= 0)
			{
				throw new ArgumentOutOfRangeException("nexusId");
			}
			if (NexusId > 0)
			{
				throw new InvalidOperationException("A nexus has already been selected");
			}
			Reset();
			try
			{
				NexusDetails nexus = await GetNexusDetails(nexusId).ConfigureAwait(continueOnCapturedContext: false);
				PlayerDetails playerDetails = await GetPlayerDetails(nexusId).ConfigureAwait(continueOnCapturedContext: false);
				NexusZoneDetails nexusZoneDetails = FindZone(nexus, playerDetails.ConnectZoneKey);
				UpdateVariables(NexusVariables, nexus.Variables);
				UpdateVariables(ZoneVariables, nexusZoneDetails?.Variables);
				UpdateVariables(PlayerVariables, playerDetails.Variables);
				NexusId = nexusId;
				ZoneKey = nexusZoneDetails?.Key;
				Nexus = nexus;
				Player = playerDetails;
				return playerDetails;
			}
			catch
			{
				Reset();
				throw;
			}
		}

		protected override ValueTask Initialize(CancellationToken ct)
		{
			if (NexusId <= 0)
			{
				throw new InvalidOperationException("Must select a nexus before starting the client");
			}
			return default(ValueTask);
		}

		protected virtual void Reset()
		{
			NexusId = 0;
			ZoneKey = null;
			Nexus = null;
			Player = null;
			NexusVariables.Clear();
			ZoneVariables.Clear();
			PlayerVariables.Clear();
		}

		public bool TryGetNexusVariable(string key, out Variable variable)
		{
			return NexusVariables.TryGet(key, out variable);
		}

		public bool TryGetZoneVariable(string key, out Variable variable)
		{
			return ZoneVariables.TryGet(key, out variable);
		}

		public bool TryGetPlayerVariable(string key, out Variable variable)
		{
			return PlayerVariables.TryGet(key, out variable);
		}

		protected override void OnNexusDetailsUpdated(int nexusId, NexusDetails details)
		{
			base.OnNexusDetailsUpdated(nexusId, details);
			if (nexusId == NexusId)
			{
				NexusZoneDetails nexusZoneDetails = FindZone(details, ZoneKey);
				UpdateVariables(NexusVariables, details.Variables);
				UpdateVariables(ZoneVariables, nexusZoneDetails?.Variables);
			}
		}

		private static NexusZoneDetails FindZone(NexusDetails nexus, string zoneKey)
		{
			if (nexus == null || string.IsNullOrEmpty(zoneKey))
			{
				return null;
			}
			foreach (NexusZoneDetails zone in nexus.Zones)
			{
				if (string.Equals(zone.Key, zoneKey, StringComparison.InvariantCultureIgnoreCase))
				{
					return zone;
				}
			}
			return null;
		}
	}
	internal abstract class CachedValue
	{
		internal const double CacheExpirySeconds = 30.0;

		protected readonly IClockProvider Clock;

		protected CachedValue(IClockProvider clock)
		{
			Clock = clock ?? throw new ArgumentNullException("clock");
		}
	}
	internal class CachedValue<TValue> : CachedValue
	{
		private double _expiryTime;

		private bool _hasValue;

		private TValue _value;

		public CachedValue(IClockProvider clock)
			: base(clock)
		{
		}

		public bool TryGetValue(out TValue value)
		{
			if (!_hasValue || Clock.Timestamp >= _expiryTime)
			{
				value = default(TValue);
				return false;
			}
			value = _value;
			return true;
		}

		public ref readonly TValue Update(in TValue value)
		{
			if (value != null)
			{
				_hasValue = true;
				_value = value;
				_expiryTime = Clock.Timestamp + 30.0;
			}
			else
			{
				Invalidate();
			}
			return ref value;
		}

		public void Invalidate()
		{
			_hasValue = false;
			_value = default(TValue);
			_expiryTime = 0.0;
		}
	}
	internal class CachedValue<TKey, TValue> : CachedValue where TKey : IEquatable<TKey>
	{
		private readonly Dictionary<TKey, (TValue Value, double Expiry)> _values = new Dictionary<TKey, (TValue, double)>();

		public CachedValue(IClockProvider clock)
			: base(clock)
		{
		}

		public bool TryGetValue(in TKey key, out TValue value)
		{
			if (!_values.TryGetValue(key, out (TValue, double) value2) || Clock.Timestamp >= value2.Item2)
			{
				value = default(TValue);
				return false;
			}
			(value, _) = value2;
			return true;
		}

		public ref readonly TValue Update(in TKey key, in TValue value)
		{
			if (value != null)
			{
				_values[key] = (value, Clock.Timestamp + 30.0);
			}
			else
			{
				Invalidate(in key);
			}
			return ref value;
		}

		public void Invalidate(in TKey key)
		{
			_values.Remove(key);
		}
	}
	public enum NexusClanResultCode
	{
		Fail,
		Success,
		NoClan,
		NotFound,
		NoPermission,
		DuplicateName,
		RoleNotEmpty,
		CannotSwapLeader,
		CannotDeleteLeader,
		CannotKickLeader,
		CannotDemoteLeader,
		AlreadyInAClan,
		ClanIsFull
	}
	public readonly struct NexusClanResult<T>
	{
		private readonly T _response;

		private readonly bool _hasResponse;

		public NexusClanResultCode ResultCode { get; }

		public bool IsSuccess => ResultCode == NexusClanResultCode.Success;

		internal NexusClanResult(NexusClanResultCode resultCode)
		{
			if (resultCode == NexusClanResultCode.Success)
			{
				throw new ArgumentOutOfRangeException("resultCode", "Cannot build a successful NexusClanResult<T> without a response.");
			}
			ResultCode = resultCode;
			_response = default(T);
			_hasResponse = false;
		}

		internal NexusClanResult(T response)
		{
			ResultCode = NexusClanResultCode.Success;
			_response = response;
			_hasResponse = true;
		}

		public bool TryGetResponse(out T response)
		{
			if (IsSuccess && _hasResponse)
			{
				response = _response;
				return true;
			}
			response = default(T);
			return false;
		}
	}
	public delegate void NexusInitializedHandler(BaseNexusClient sender);
	public delegate void NexusVariableChangedHandler(BaseNexusClient sender, string variableKey);
	public delegate void NexusErrorHandler(BaseNexusClient sender, Exception exception);
	public interface INexusClanEventListener
	{
		void OnDisbanded(in ClanDisbandedEvent args);

		void OnInvitation(in ClanInvitedEvent args);

		void OnJoined(in ClanJoinedEvent args);

		void OnKicked(in ClanKickedEvent args);

		void OnChanged(in ClanChangedEvent args);

		void OnUnload(in long clanId);
	}
	public class NexusAnonymousClient
	{
		private readonly INexusConnector _connector;

		private readonly CachedValue<List<Facepunch.Nexus.Models.Nexus>> _cachedNexusList;

		private readonly CachedValue<int, NexusDetails> _cachedNexusDetails;

		public NexusAnonymousClient(INexusLogger logger, IClockProvider clock, string baseUrl)
			: this(new NexusConnector(logger, baseUrl), clock)
		{
		}

		internal NexusAnonymousClient(INexusConnector connector, IClockProvider clock)
		{
			_connector = connector ?? throw new ArgumentNullException("connector");
			_cachedNexusList = new CachedValue<List<Facepunch.Nexus.Models.Nexus>>(clock);
			_cachedNexusDetails = new CachedValue<int, NexusDetails>(clock);
		}

		public async ValueTask<IEnumerable<Facepunch.Nexus.Models.Nexus>> GetNexusList(string publicKey, NexusRealm realm)
		{
			if (!_cachedNexusList.TryGetValue(out var value))
			{
				NexusListing nexusListing = await _connector.ListNexuses(publicKey, realm).ConfigureAwait(continueOnCapturedContext: false);
				value = _cachedNexusList.Update(nexusListing.Nexuses);
			}
			return value;
		}

		public async ValueTask<NexusDetails> GetNexusDetails(int nexusId)
		{
			if (nexusId <= 0)
			{
				throw new ArgumentOutOfRangeException("nexusId");
			}
			if (!_cachedNexusDetails.TryGetValue(in nexusId, out var value))
			{
				NexusDetails value2 = await _connector.GetNexus(nexusId).ConfigureAwait(continueOnCapturedContext: false);
				value = _cachedNexusDetails.Update(in nexusId, in value2);
				OnNexusDetailsUpdated(nexusId, value);
			}
			return value;
		}

		protected virtual void OnNexusDetailsUpdated(int nexusId, NexusDetails details)
		{
		}
	}
	public class NexusClan
	{
		private const double RefreshInterval = 300.0;

		private readonly NexusZoneClient _client;

		private readonly INexusZoneConnector _zoneConnector;

		private readonly IClockProvider _clockProvider;

		private readonly INexusLogger _logger;

		private readonly VariableContainer _variables;

		private double _lastRefreshed;

		public long ClanId { get; }

		public string Name { get; private set; }

		public long Created { get; private set; }

		public string Creator { get; private set; }

		public long Score { get; private set; }

		public List<NexusClanRole> Roles { get; }

		public List<NexusClanMember> Members { get; }

		public int MaxMemberCount { get; private set; }

		public List<ClanInvite> Invites { get; }

		public IEnumerable<KeyValuePair<string, Variable>> Variables => _variables;

		internal NexusClan(NexusZoneClient client, INexusZoneConnector zoneConnector, IClockProvider clockProvider, INexusLogger logger, long clanId)
		{
			ClanId = clanId;
			Roles = new List<NexusClanRole>();
			Members = new List<NexusClanMember>();
			Invites = new List<ClanInvite>();
			_client = client ?? throw new ArgumentNullException("client");
			_zoneConnector = zoneConnector ?? throw new ArgumentNullException("zoneConnector");
			_clockProvider = clockProvider ?? throw new ArgumentNullException("clockProvider");
			_logger = logger ?? throw new ArgumentNullException("logger");
			_variables = new VariableContainer();
		}

		internal bool UpdateFromModel(in ClanDetails details)
		{
			if (details.ClanId != ClanId)
			{
				throw new InvalidOperationException("Cannot update NexusClan with details from a different clan ID!");
			}
			lock (this)
			{
				bool changed = false;
				Name = Util.Update<string>(Name, details.Name, ref changed);
				Created = Util.Update<long>(Created, details.Created, ref changed);
				Creator = Util.Update<string>(Creator, details.Creator, ref changed);
				Score = Util.Update<long>(Score, details.Score, ref changed);
				MaxMemberCount = Util.Update<int>(MaxMemberCount, details.MaxMemberCount, ref changed);
				changed |= _variables.UpdateFromModel(details.Variables);
				changed |= Roles.Resize(details.Roles.Count);
				for (int i = 0; i < Roles.Count; i++)
				{
					NexusClanRole nexusClanRole = Roles[i];
					if (nexusClanRole != null)
					{
						changed |= nexusClanRole.UpdateFromModel(details.Roles[i]);
						continue;
					}
					Roles[i] = new NexusClanRole(details.Roles[i]);
					changed = true;
				}
				changed |= Members.Resize(details.Members.Count);
				for (int j = 0; j < Members.Count; j++)
				{
					NexusClanMember nexusClanMember = Members[j];
					if (nexusClanMember != null)
					{
						changed |= nexusClanMember.UpdateFromModel(details.Members[j]);
						continue;
					}
					Members[j] = new NexusClanMember(details.Members[j]);
					changed = true;
				}
				changed |= Invites.Resize(details.Invites.Count);
				for (int k = 0; k < Invites.Count; k++)
				{
					bool changed2 = false;
					ClanInvite value = Invites[k];
					ClanInvite clanInvite = details.Invites[k];
					value.PlayerId = Util.Update<string>(value.PlayerId, clanInvite.PlayerId, ref changed2);
					value.RecruiterPlayerId = Util.Update<string>(value.RecruiterPlayerId, clanInvite.RecruiterPlayerId, ref changed2);
					value.Created = Util.Update<long>(value.Created, clanInvite.Created, ref changed2);
					if (changed2)
					{
						Invites[k] = value;
						changed = true;
					}
				}
				_lastRefreshed = _clockProvider.Timestamp;
				return changed;
			}
		}

		internal void RefreshIfNeeded()
		{
			if (_clockProvider.Timestamp - _lastRefreshed > 300.0)
			{
				RefreshInBackground();
			}
		}

		internal async void RefreshInBackground()
		{
			try
			{
				await Refresh();
			}
			catch (Exception exception)
			{
				_logger.LogError($"Failed to refresh clan ID {ClanId}", exception);
			}
		}

		public async Task Refresh()
		{
			_lastRefreshed = _clockProvider.Timestamp;
			ApiResult<ClanDetails> apiResult = await _zoneConnector.GetClan(ClanId);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				_logger.LogWarn($"Clan ID {ClanId} was not found during a refresh, removing from cache");
				_client.RemoveClan(ClanId);
			}
			else
			{
				apiResult.EnsureSuccessfulWithResponse();
				ClanDetails details = apiResult.Response;
				_client.UpsertClan(in details);
			}
		}

		public bool TryGetVariable(string key, out Variable variable)
		{
			return _variables.TryGet(key, out variable);
		}

		public async Task<NexusClanResultCode> UpdateVariables(ClanVariablesUpdate update)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.UpdateClanVariables(ClanId, update).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> UpdatePlayerVariables(string playerId, ClanVariablesUpdate update)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.UpdatePlayerClanVariables(ClanId, playerId, update).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResult<List<ClanLogEntry>>> GetLogs(string byPlayerId, int limit = 100)
		{
			ApiResult<List<ClanLogEntry>> apiResult = await _zoneConnector.GetClanLogs(ClanId, byPlayerId, limit).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return new NexusClanResult<List<ClanLogEntry>>(NexusClanResultCode.NotFound);
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return new NexusClanResult<List<ClanLogEntry>>(NexusClanResultCode.NoPermission);
			}
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<List<ClanLogEntry>>(NexusClanResultCode.Fail);
			}
			apiResult.EnsureSuccessfulWithResponse();
			return new NexusClanResult<List<ClanLogEntry>>(apiResult.Response);
		}

		public async Task<NexusClanResultCode> AddLog(string eventKey, string arg1 = null, string arg2 = null, string arg3 = null, string arg4 = null)
		{
			ApiResult apiResult = await _zoneConnector.AddClanLog(ClanId, eventKey, arg1, arg2, arg3, arg4).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			return apiResult.IsSuccess ? NexusClanResultCode.Success : NexusClanResultCode.Fail;
		}

		public async Task<NexusClanResult<List<ClanScoreEventEntry>>> GetScoreEvents(string byPlayerId, int limit = 100)
		{
			ApiResult<List<ClanScoreEventEntry>> apiResult = await _zoneConnector.GetClanScoreEvents(ClanId, byPlayerId, limit).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return new NexusClanResult<List<ClanScoreEventEntry>>(NexusClanResultCode.NotFound);
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return new NexusClanResult<List<ClanScoreEventEntry>>(NexusClanResultCode.NoPermission);
			}
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<List<ClanScoreEventEntry>>(NexusClanResultCode.Fail);
			}
			apiResult.EnsureSuccessfulWithResponse();
			return new NexusClanResult<List<ClanScoreEventEntry>>(apiResult.Response);
		}

		public void AddScoreEvent(NewClanScoreEventEntry entry)
		{
			NexusZoneClient client = _client;
			NewClanScoreEventBatchEntry entry2 = new NewClanScoreEventBatchEntry
			{
				ClanId = ClanId,
				Type = entry.Type,
				Score = entry.Score,
				Multiplier = entry.Multiplier,
				PlayerId = entry.PlayerId,
				OtherClanId = entry.OtherClanId,
				OtherPlayerId = entry.OtherPlayerId,
				Arg1 = entry.Arg1,
				Arg2 = entry.Arg2
			};
			client.EnqueueClanScoreEvent(in entry2);
		}

		public async Task<NexusClanResultCode> UpdateLastSeen(string playerId)
		{
			ApiResult apiResult = await _zoneConnector.UpdateLastSeen(ClanId, playerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			return apiResult.IsSuccess ? NexusClanResultCode.Success : NexusClanResultCode.Fail;
		}

		public async Task<NexusClanResultCode> Invite(string playerId, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.CreateInvite(ClanId, playerId, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return NexusClanResultCode.AlreadyInAClan;
			}
			if (apiResult.StatusCode == HttpStatusCode.ExpectationFailed)
			{
				return NexusClanResultCode.ClanIsFull;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> CancelInvite(string playerId, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.CancelInvite(ClanId, playerId, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> AcceptInvite(string playerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.AcceptInvite(ClanId, playerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return NexusClanResultCode.AlreadyInAClan;
			}
			if (apiResult.StatusCode == HttpStatusCode.ExpectationFailed)
			{
				return NexusClanResultCode.ClanIsFull;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> Kick(string playerId, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.Kick(ClanId, playerId, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return NexusClanResultCode.CannotKickLeader;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> SetPlayerRole(string playerId, int newRoleId, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.SetPlayerRole(ClanId, playerId, newRoleId, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.ExpectationFailed)
			{
				return NexusClanResultCode.CannotDemoteLeader;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> CreateRole(ClanRoleParameters parameters, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.CreateRole(ClanId, parameters, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return NexusClanResultCode.DuplicateName;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> UpdateRole(int roleId, ClanRoleParameters parameters, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.UpdateRole(ClanId, roleId, parameters, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return NexusClanResultCode.DuplicateName;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> DeleteRole(int roleId, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.DeleteRole(ClanId, roleId, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return NexusClanResultCode.CannotDeleteLeader;
			}
			if (apiResult.StatusCode == HttpStatusCode.ExpectationFailed)
			{
				return NexusClanResultCode.RoleNotEmpty;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> SwapRoleRanks(int roleIdA, int roleIdB, string byPlayerId)
		{
			ApiResult<ClanDetails> apiResult = await _zoneConnector.SwapRoleRanks(ClanId, roleIdA, roleIdB, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (apiResult.StatusCode == HttpStatusCode.ExpectationFailed)
			{
				return NexusClanResultCode.CannotSwapLeader;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			if (apiResult.TryGetResponse(out var response))
			{
				_client.UpsertClan(in response);
			}
			return NexusClanResultCode.Success;
		}

		public async Task<NexusClanResultCode> Disband(string byPlayerId)
		{
			ApiResult apiResult = await _zoneConnector.DisbandClan(ClanId, byPlayerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return NexusClanResultCode.NotFound;
			}
			if (apiResult.StatusCode == HttpStatusCode.Forbidden)
			{
				return NexusClanResultCode.NoPermission;
			}
			if (!apiResult.IsSuccess)
			{
				return NexusClanResultCode.Fail;
			}
			apiResult.EnsureSuccessful();
			return NexusClanResultCode.Success;
		}
	}
	public class NexusClanMember
	{
		private readonly VariableContainer _variables;

		public string PlayerId { get; set; }

		public int RoleId { get; set; }

		public long Joined { get; set; }

		public long LastSeen { get; set; }

		public IEnumerable<KeyValuePair<string, Variable>> Variables => _variables;

		public NexusClanMember(ClanMember details)
		{
			_variables = new VariableContainer();
			UpdateFromModel(details);
		}

		public bool TryGetVariable(string key, out Variable variable)
		{
			return _variables.TryGet(key, out variable);
		}

		internal bool UpdateFromModel(ClanMember details)
		{
			bool changed = false;
			PlayerId = Util.Update<string>(PlayerId, details.PlayerId, ref changed);
			RoleId = Util.Update<int>(RoleId, details.RoleId, ref changed);
			Joined = Util.Update<long>(Joined, details.Joined, ref changed);
			LastSeen = Util.Update<long>(LastSeen, details.LastSeen, ref changed);
			return changed | _variables.UpdateFromModel(details.Variables);
		}
	}
	public class NexusClanRole
	{
		private readonly VariableContainer _variables;

		public int RoleId { get; set; }

		public int Rank { get; set; }

		public string Name { get; set; }

		public bool CanInvite { get; set; }

		public bool CanKick { get; set; }

		public bool CanPromote { get; set; }

		public bool CanDemote { get; set; }

		public bool CanAccessLogs { get; set; }

		public bool CanAccessScoreEvents { get; set; }

		public IEnumerable<KeyValuePair<string, Variable>> Variables => _variables;

		public NexusClanRole(ClanRole details)
		{
			_variables = new VariableContainer();
			UpdateFromModel(details);
		}

		public bool TryGetVariable(string key, out Variable variable)
		{
			return _variables.TryGet(key, out variable);
		}

		internal bool UpdateFromModel(ClanRole details)
		{
			bool changed = false;
			RoleId = Util.Update<int>(RoleId, details.RoleId, ref changed);
			Rank = Util.Update<int>(Rank, details.Rank, ref changed);
			Name = Util.Update<string>(Name, details.Name, ref changed);
			CanInvite = Util.Update<bool>(CanInvite, details.CanInvite, ref changed);
			CanKick = Util.Update<bool>(CanKick, details.CanKick, ref changed);
			CanPromote = Util.Update<bool>(CanPromote, details.CanPromote, ref changed);
			CanDemote = Util.Update<bool>(CanDemote, details.CanDemote, ref changed);
			CanAccessLogs = Util.Update<bool>(CanAccessLogs, details.CanAccessLogs, ref changed);
			CanAccessScoreEvents = Util.Update<bool>(CanAccessScoreEvents, details.CanAccessScoreEvents, ref changed);
			return changed | _variables.UpdateFromModel(details.Variables);
		}
	}
	public readonly struct NexusLoginResult
	{
		private readonly ZonePlayerLogin _login;

		public string PlayerId => _login.PlayerId;

		public long LastSeen => _login.LastSeen;

		public Dictionary<string, VariableData> Variables => _login.Variables;

		public string AssignedZoneKey => _login.AssignedZoneKey;

		public bool IsRedirect
		{
			get
			{
				if (_login.RedirectIpAddress == null && !_login.RedirectGamePort.HasValue)
				{
					return _login.RedirectQueryPort.HasValue;
				}
				return true;
			}
		}

		public string RedirectIpAddress
		{
			get
			{
				if (!IsRedirect)
				{
					throw new InvalidOperationException("Result is not a redirect");
				}
				return _login.RedirectIpAddress ?? throw new InvalidOperationException("Redirect IP address was not set");
			}
		}

		public int RedirectGamePort
		{
			get
			{
				if (!IsRedirect)
				{
					throw new InvalidOperationException("Result is not a redirect");
				}
				return _login.RedirectGamePort ?? throw new InvalidOperationException("Redirect game port was not set");
			}
		}

		public int RedirectQueryPort
		{
			get
			{
				if (!IsRedirect)
				{
					throw new InvalidOperationException("Result is not a redirect");
				}
				return _login.RedirectQueryPort ?? throw new InvalidOperationException("Redirect query port was not set");
			}
		}

		internal NexusLoginResult(ZonePlayerLogin login)
		{
			_login = login ?? throw new ArgumentNullException("login");
		}
	}
	public readonly struct NexusMessage
	{
		private readonly string _stringData;

		private readonly byte[] _binaryData;

		internal string MessageId { get; }

		public Uuid Id { get; }

		public string ContentType { get; }

		public bool IsString => _stringData != null;

		public bool IsBinary => _binaryData != null;

		public string AsString => _stringData ?? throw new InvalidOperationException("Message is not text");

		public byte[] AsBinary => _binaryData ?? throw new InvalidOperationException("Message is not binary");

		internal NexusMessage(string messageId, Guid id, string contentType, string data)
		{
			MessageId = messageId ?? throw new ArgumentNullException("messageId");
			Id = id;
			ContentType = contentType ?? throw new ArgumentNullException("contentType");
			_stringData = data ?? throw new ArgumentNullException("data");
			_binaryData = null;
		}

		internal NexusMessage(string messageId, Guid id, string contentType, byte[] data)
		{
			MessageId = messageId ?? throw new ArgumentNullException("messageId");
			Id = id;
			ContentType = contentType ?? throw new ArgumentNullException("contentType");
			_stringData = null;
			_binaryData = data ?? throw new ArgumentNullException("data");
		}
	}
	public class NexusPlayer
	{
		private const double RefreshInterval = 120.0;

		private readonly INexusLogger _logger;

		private readonly INexusZoneConnector _zoneConnector;

		private readonly IClockProvider _clockProvider;

		private readonly VariableContainer _variables;

		private double _lastRefreshed;

		public string PlayerId { get; }

		public string AssignedZoneKey { get; private set; }

		public IEnumerable<KeyValuePair<string, Variable>> Variables => _variables;

		internal NexusPlayer(INexusLogger logger, INexusZoneConnector zoneConnector, IClockProvider clockProvider, string playerId)
		{
			_logger = logger ?? throw new ArgumentNullException("logger");
			_zoneConnector = zoneConnector ?? throw new ArgumentNullException("zoneConnector");
			_clockProvider = clockProvider ?? throw new ArgumentNullException("clockProvider");
			_variables = new VariableContainer();
			_lastRefreshed = _clockProvider.Timestamp;
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			PlayerId = playerId;
		}

		public bool TryGetVariable(string key, out Variable variable)
		{
			return _variables.TryGet(key, out variable);
		}

		public async Task SetVariable(string key, string value, bool isTransient = true, bool isSecret = true)
		{
			if (!_variables.TryGet(key, out var variable) || variable != value)
			{
				await _zoneConnector.SetPlayerVariable(PlayerId, key, value, isTransient, isSecret).ConfigureAwait(continueOnCapturedContext: false);
				_variables.Set(key, value);
			}
		}

		public async Task SetVariable(string key, byte[] value, bool isTransient = true, bool isSecret = true)
		{
			if (!_variables.TryGet(key, out var variable) || variable != value)
			{
				await _zoneConnector.SetPlayerVariable(PlayerId, key, value, isTransient, isSecret).ConfigureAwait(continueOnCapturedContext: false);
				_variables.Set(key, value);
			}
		}

		internal void RefreshIfNeeded()
		{
			double timestamp = _clockProvider.Timestamp;
			if (timestamp - _lastRefreshed > 120.0)
			{
				_lastRefreshed = timestamp;
				RefreshInBackground();
			}
		}

		internal async void RefreshInBackground()
		{
			try
			{
				await Refresh();
			}
			catch (Exception exception)
			{
				_logger.LogError("Failed to refresh player " + PlayerId, exception);
			}
		}

		internal async Task Refresh()
		{
			ZonePlayerDetails zonePlayerDetails = await _zoneConnector.GetPlayerDetails(PlayerId);
			UpdateZone(zonePlayerDetails.AssignedZoneKey);
			UpdateVariables(zonePlayerDetails.Variables);
		}

		internal void UpdateZone(string assignedZoneKey)
		{
			AssignedZoneKey = assignedZoneKey;
		}

		internal void UpdateVariables(Dictionary<string, VariableData> variables)
		{
			_variables.UpdateFromModel(variables);
		}
	}
	public sealed class NexusPlayerClient : BaseNexusPlayerClient
	{
		public delegate ValueTask<string> AuthHandler();

		private readonly INexusPlayerConnector _connector;

		private readonly AuthHandler _authHandler;

		private readonly CachedValue<int, PlayerDetails> _cachedPlayerInfo;

		public NexusPlayerClient(INexusLogger logger, string baseUrl, AuthHandler authHandler)
			: this(new NexusPlayerConnector(logger, baseUrl), DefaultClockProvider.Instance, 300.0, authHandler)
		{
		}

		internal NexusPlayerClient(INexusPlayerConnector connector, IClockProvider clock, double updateInterval, AuthHandler authHandler)
			: base(connector, clock, updateInterval)
		{
			_connector = connector ?? throw new ArgumentNullException("connector");
			_authHandler = authHandler ?? throw new ArgumentNullException("authHandler");
			_cachedPlayerInfo = new CachedValue<int, PlayerDetails>(clock);
		}

		protected override async ValueTask Update(CancellationToken ct)
		{
			if (base.NexusId > 0)
			{
				base.Nexus = await GetNexusDetails(base.NexusId);
				base.Player = await GetPlayerDetails(base.NexusId);
			}
		}

		protected internal override async ValueTask<PlayerDetails> GetPlayerDetails(int nexusId)
		{
			if (nexusId <= 0)
			{
				throw new ArgumentOutOfRangeException("nexusId");
			}
			if (!_cachedPlayerInfo.TryGetValue(in nexusId, out var value))
			{
				string playerAuthToken = await _authHandler();
				PlayerDetails value2 = await _connector.GetPlayerDetails(nexusId, playerAuthToken);
				value = _cachedPlayerInfo.Update(in nexusId, in value2);
				if (nexusId == base.NexusId)
				{
					UpdateVariables(PlayerVariables, value.Variables);
				}
			}
			return value;
		}
	}
	public sealed class NexusZoneClient : BaseNexusClient
	{
		private readonly INexusLogger _logger;

		private readonly INexusZoneConnector _connector;

		private readonly INexusSocketConnector _socket;

		private readonly IClockProvider _clock;

		private readonly VariableContainer _nexusVariables;

		private readonly VariableContainer _zoneVariables;

		private readonly CachedValue<ZoneDetails> _cachedZoneInfo;

		private readonly CachedValue<NexusDetails> _cachedNexusInfo;

		private readonly Dictionary<string, NexusPlayer> _players;

		private readonly Dictionary<long, NexusClan> _clans;

		private readonly Queue<NewClanScoreEventBatchEntry> _scoreEventQueue;

		private readonly List<NewClanScoreEventBatchEntry> _scoreEventBatch;

		private bool _submittingScoreEventsBatch;

		public INexusClanEventListener ClanEventListener { get; set; }

		public ZoneDetails Zone { get; private set; }

		public NexusDetails Nexus { get; private set; }

		public async ValueTask<NexusClanResult<NexusClan>> GetClan(long clanId)
		{
			AssertInitialized();
			lock (_clans)
			{
				if (_clans.TryGetValue(clanId, out var value))
				{
					return new NexusClanResult<NexusClan>(value);
				}
			}
			ApiResult<ClanDetails> apiResult = await _connector.GetClan(clanId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.NotFound);
			}
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.Fail);
			}
			apiResult.EnsureSuccessfulWithResponse();
			NexusClan response = UpsertClan(apiResult.Response);
			return new NexusClanResult<NexusClan>(response);
		}

		public async ValueTask<NexusClanResult<NexusClan>> GetClanByMember(string playerId)
		{
			AssertInitialized();
			ApiResult<ClanDetails> apiResult = await _connector.GetClanByMember(playerId).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.NotFound)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.NoClan);
			}
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.Fail);
			}
			apiResult.EnsureSuccessfulWithResponse();
			NexusClan response = UpsertClan(apiResult.Response);
			return new NexusClanResult<NexusClan>(response);
		}

		public bool TryGetClan(long clanId, out NexusClan clan)
		{
			AssertInitialized();
			lock (_clans)
			{
				return _clans.TryGetValue(clanId, out clan);
			}
		}

		public async ValueTask<NexusClanResult<NexusClan>> CreateClan(ClanCreateParameters parameters)
		{
			AssertInitialized();
			ApiResult<ClanDetails> apiResult = await _connector.CreateClan(parameters).ConfigureAwait(continueOnCapturedContext: false);
			if (apiResult.StatusCode == HttpStatusCode.Conflict)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.DuplicateName);
			}
			if (apiResult.StatusCode == HttpStatusCode.ExpectationFailed)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.AlreadyInAClan);
			}
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<NexusClan>(NexusClanResultCode.Fail);
			}
			apiResult.EnsureSuccessfulWithResponse();
			NexusClan response = UpsertClan(apiResult.Response);
			return new NexusClanResult<NexusClan>(response);
		}

		public async ValueTask<NexusClanResult<List<ClanInvitation>>> ListClanInvitations(string playerId)
		{
			AssertInitialized();
			ApiResult<List<ClanInvitation>> apiResult = await _connector.ListInvitations(playerId).ConfigureAwait(continueOnCapturedContext: false);
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<List<ClanInvitation>>(NexusClanResultCode.Fail);
			}
			return new NexusClanResult<List<ClanInvitation>>(apiResult.Response);
		}

		public async ValueTask<NexusClanResult<List<ClanLeaderboardEntry>>> GetClanLeaderboard(int limit = 100)
		{
			AssertInitialized();
			ApiResult<List<ClanLeaderboardEntry>> apiResult = await _connector.GetClanLeaderboard(limit).ConfigureAwait(continueOnCapturedContext: false);
			if (!apiResult.IsSuccess)
			{
				return new NexusClanResult<List<ClanLeaderboardEntry>>(NexusClanResultCode.Fail);
			}
			return new NexusClanResult<List<ClanLeaderboardEntry>>(apiResult.Response);
		}

		internal void EnqueueClanScoreEvent(in NewClanScoreEventBatchEntry entry)
		{
			lock (_scoreEventQueue)
			{
				_scoreEventQueue.Enqueue(entry);
			}
		}

		private void UpdateClanScoreEvents()
		{
			if (_submittingScoreEventsBatch)
			{
				return;
			}
			lock (_scoreEventQueue)
			{
				if (_scoreEventQueue.Count == 0)
				{
					return;
				}
				_scoreEventBatch.Clear();
				while (_scoreEventQueue.Count > 0)
				{
					_scoreEventBatch.Add(_scoreEventQueue.Dequeue());
				}
			}
			SubmitClanScoreEventBatch();
		}

		private async void SubmitClanScoreEventBatch()
		{
			_submittingScoreEventsBatch = true;
			try
			{
				await _connector.AddClanScoreEventBatch(_scoreEventBatch);
				_scoreEventBatch.Clear();
			}
			catch (Exception exception)
			{
				DispatchError(exception);
			}
			finally
			{
				_submittingScoreEventsBatch = false;
			}
		}

		private void HandleClanEvent(in NexusMessage message)
		{
			try
			{
				if (!message.IsString)
				{
					_logger.LogError("Received clan event type " + message.ContentType + " but the payload is not a string");
				}
				else if (message.ContentType.EndsWith("-disband"))
				{
					ClanDisbandedEvent args = JsonConvert.DeserializeObject<ClanDisbandedEvent>(message.AsString);
					RemoveClan(args.ClanId);
					ClanEventListener?.OnDisbanded(in args);
				}
				else if (message.ContentType.EndsWith("-invite"))
				{
					ClanInvitedEvent args2 = JsonConvert.DeserializeObject<ClanInvitedEvent>(message.AsString);
					RefreshClan(args2.ClanId);
					ClanEventListener?.OnInvitation(in args2);
				}
				else if (message.ContentType.EndsWith("-join"))
				{
					ClanJoinedEvent args3 = JsonConvert.DeserializeObject<ClanJoinedEvent>(message.AsString);
					RefreshClan(args3.ClanId);
					ClanEventListener?.OnJoined(in args3);
				}
				else if (message.ContentType.EndsWith("-kick"))
				{
					ClanKickedEvent args4 = JsonConvert.DeserializeObject<ClanKickedEvent>(message.AsString);
					RefreshClan(args4.ClanId);
					ClanEventListener?.OnKicked(in args4);
				}
				else if (message.ContentType.EndsWith("-change"))
				{
					RefreshClan(JsonConvert.DeserializeObject<ClanChangedEvent>(message.AsString).ClanId);
				}
				else
				{
					_logger.LogError("Cannot handle clan event: " + message.ContentType);
				}
			}
			catch (Exception exception)
			{
				_logger.LogError("Failed to handle clan event (type=" + message.ContentType + "): " + message.AsString, exception);
			}
			AcknowledgeMessage(in message);
		}

		private void RefreshClan(long clanId)
		{
			lock (_clans)
			{
				if (_clans.TryGetValue(clanId, out var value))
				{
					value.RefreshInBackground();
				}
			}
		}

		internal NexusClan UpsertClan(in ClanDetails details)
		{
			lock (_clans)
			{
				if (_clans.TryGetValue(details.ClanId, out var value))
				{
					UpdateClan(value, in details);
				}
				else
				{
					value = new NexusClan(this, _connector, _clock, _logger, details.ClanId);
					UpdateClan(value, in details);
					_clans.Add(details.ClanId, value);
				}
				return value;
			}
		}

		private void UpdateClan(NexusClan clan, in ClanDetails details)
		{
			if (clan.UpdateFromModel(in details))
			{
				ClanChangedEvent args = new ClanChangedEvent
				{
					ClanId = clan.ClanId
				};
				ClanEventListener?.OnChanged(in args);
			}
		}

		internal void RemoveClan(long clanId)
		{
			lock (_clans)
			{
				if (!_clans.Remove(clanId))
				{
					return;
				}
			}
			try
			{
				ClanEventListener?.OnUnload(in clanId);
			}
			catch (Exception exception)
			{
				_logger.LogError("Clan event listener for OnUnload threw an exception", exception);
			}
		}

		public NexusZoneClient(INexusLogger logger, string baseUrl, string secretKey, int lockDuration = 30)
			: this(logger, new NexusZoneConnector(logger, baseUrl, secretKey), new NexusSocketConnector(logger, baseUrl, secretKey, lockDuration), DefaultClockProvider.Instance, 30.0)
		{
		}

		internal NexusZoneClient(INexusLogger logger, INexusZoneConnector connector, INexusSocketConnector socket, IClockProvider clock, double updateInterval)
			: base(connector, clock, updateInterval)
		{
			_logger = logger ?? throw new ArgumentNullException("logger");
			_connector = connector ?? throw new ArgumentNullException("connector");
			_socket = socket ?? throw new ArgumentNullException("socket");
			_clock = clock ?? throw new ArgumentNullException("clock");
			_nexusVariables = new VariableContainer();
			_zoneVariables = new VariableContainer();
			_cachedNexusInfo = new CachedValue<NexusDetails>(clock);
			_cachedZoneInfo = new CachedValue<ZoneDetails>(clock);
			_players = new Dictionary<string, NexusPlayer>();
			_clans = new Dictionary<long, NexusClan>();
			_scoreEventQueue = new Queue<NewClanScoreEventBatchEntry>();
			_scoreEventBatch = new List<NewClanScoreEventBatchEntry>();
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			_socket.Dispose();
			lock (_players)
			{
				_players.Clear();
			}
			lock (_clans)
			{
				_clans.Clear();
			}
		}

		protected override async ValueTask Initialize(CancellationToken ct)
		{
			Zone = await GetZoneDetails();
			Nexus = await GetNexusDetails(Zone.NexusId);
		}

		protected override async ValueTask Update(CancellationToken ct)
		{
			Zone = await GetZoneDetails();
			Nexus = await GetNexusDetails(Zone.NexusId);
			if (_socket.IsStarted && !_socket.IsConnected)
			{
				DispatchError(new Exception("Socket is not connected for zone " + Zone.Key));
			}
			lock (_players)
			{
				foreach (NexusPlayer value in _players.Values)
				{
					value.RefreshIfNeeded();
				}
			}
			lock (_clans)
			{
				foreach (NexusClan value2 in _clans.Values)
				{
					value2.RefreshIfNeeded();
				}
			}
			UpdateClanScoreEvents();
		}

		public void StartListening()
		{
			_socket.Start();
		}

		public bool TryGetNexusVariable(string key, out Variable variable)
		{
			return _nexusVariables.TryGet(key, out variable);
		}

		public bool TryGetZoneVariable(string key, out Variable variable)
		{
			return _zoneVariables.TryGet(key, out variable);
		}

		public async Task SetNexusVariable(string key, string value, bool isTransient = true, bool isSecret = true)
		{
			AssertInitialized();
			if (!_nexusVariables.TryGet(key, out var variable) || variable != value)
			{
				await _connector.SetNexusVariable(key, value, isTransient, isSecret).ConfigureAwait(continueOnCapturedContext: false);
				_nexusVariables.Set(key, value);
				DispatchVariableChanged(key);
			}
		}

		public async Task SetNexusVariable(string key, byte[] value, bool isTransient = true, bool isSecret = true)
		{
			AssertInitialized();
			if (!_nexusVariables.TryGet(key, out var variable) || variable != value)
			{
				await _connector.SetNexusVariable(key, value, isTransient, isSecret).ConfigureAwait(continueOnCapturedContext: false);
				_nexusVariables.Set(key, value);
				DispatchVariableChanged(key);
			}
		}

		public async Task SetZoneVariable(string key, string value, bool isTransient = true, bool isSecret = true)
		{
			AssertInitialized();
			if (!_zoneVariables.TryGet(key, out var variable) || variable != value)
			{
				await _connector.SetZoneVariable(key, value, isTransient, isSecret).ConfigureAwait(continueOnCapturedContext: false);
				_zoneVariables.Set(key, value);
				DispatchVariableChanged(key);
			}
		}

		public async Task SetZoneVariable(string key, byte[] value, bool isTransient = true, bool isSecret = true)
		{
			AssertInitialized();
			if (!_zoneVariables.TryGet(key, out var variable) || variable != value)
			{
				await _connector.SetZoneVariable(key, value, isTransient, isSecret).ConfigureAwait(continueOnCapturedContext: false);
				_zoneVariables.Set(key, value);
				DispatchVariableChanged(key);
			}
		}

		public Task<List<string>> FindPlayersWithVariable(string key, string value)
		{
			return _connector.FindPlayersWithVariable(key, value);
		}

		public async Task<NexusLoginResult> PlayerLogin(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			AssertInitialized();
			NexusLoginResult result = new NexusLoginResult(await _connector.PlayerLogin(playerId));
			if (!result.IsRedirect && result.AssignedZoneKey != null && result.AssignedZoneKey != Zone.Key)
			{
				throw new InvalidOperationException("Player is assigned to a different zone");
			}
			NexusPlayer value;
			lock (_players)
			{
				if (!_players.TryGetValue(playerId, out value))
				{
					value = new NexusPlayer(_logger, _connector, _clock, playerId);
					_players.Add(playerId, value);
				}
			}
			value.UpdateZone(result.AssignedZoneKey);
			value.UpdateVariables(result.Variables);
			return result;
		}

		public void PlayerLogout(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			AssertInitialized();
			lock (_players)
			{
				_players.Remove(playerId);
			}
		}

		public async ValueTask<NexusPlayer> GetPlayer(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			AssertInitialized();
			lock (_players)
			{
				if (_players.TryGetValue(playerId, out var value))
				{
					return value;
				}
			}
			NexusPlayer newPlayer = new NexusPlayer(_logger, _connector, _clock, playerId);
			await newPlayer.Refresh();
			lock (_players)
			{
				if (_players.TryGetValue(playerId, out var value2))
				{
					return value2;
				}
				_players.Add(playerId, newPlayer);
			}
			return newPlayer;
		}

		public bool TryGetPlayer(string playerId, out NexusPlayer player)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			AssertInitialized();
			lock (_players)
			{
				return _players.TryGetValue(playerId, out player);
			}
		}

		public async Task Assign(string playerId, string toZone)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(toZone))
			{
				throw new ArgumentNullException("toZone");
			}
			AssertInitialized();
			await _connector.Assign(playerId, toZone);
			if (TryGetPlayer(playerId, out var player))
			{
				player.UpdateZone(toZone);
			}
		}

		public Task RegisterTransfers(string toZone, IEnumerable<string> playerIds)
		{
			if (string.IsNullOrWhiteSpace(toZone))
			{
				throw new ArgumentNullException("toZone");
			}
			if (playerIds == null)
			{
				throw new ArgumentNullException("playerIds");
			}
			AssertInitialized();
			return _connector.RegisterTransfers(toZone, playerIds);
		}

		public Task CompleteTransfers(IEnumerable<string> playerIds)
		{
			if (playerIds == null)
			{
				throw new ArgumentNullException("playerIds");
			}
			AssertInitialized();
			return _connector.CompleteTransfers(playerIds);
		}

		public bool TryReceiveMessage(out NexusMessage message)
		{
			AssertInitialized();
			while (true)
			{
				if (!_socket.TryReceive(out message))
				{
					message = default(NexusMessage);
					return false;
				}
				if (!message.ContentType.StartsWith("application/json+clan-"))
				{
					break;
				}
				HandleClanEvent(in message);
			}
			return true;
		}

		public void AcknowledgeMessage(in NexusMessage message)
		{
			AssertInitialized();
			_socket.Acknowledge(message.MessageId);
		}

		public Task SendMessage(string toZone, Uuid id, string message, int? ttl = null)
		{
			if (id == Uuid.Empty)
			{
				throw new ArgumentNullException("id");
			}
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException("message");
			}
			AssertInitialized();
			return _connector.SendMessage(toZone, id, message, ttl);
		}

		public Task SendMessage(string toZone, Uuid id, Memory<byte> message, int? ttl = null)
		{
			if (id == Uuid.Empty)
			{
				throw new ArgumentNullException("id");
			}
			if (message.IsEmpty)
			{
				throw new ArgumentNullException("message");
			}
			AssertInitialized();
			return _connector.SendMessage(toZone, id, message, ttl);
		}

		public Task<ZoneMapCheckResult> CheckUploadedMap()
		{
			AssertInitialized();
			return _connector.CheckUploadedMap();
		}

		public Task UploadMap(string key, byte[] pngMapImage)
		{
			AssertInitialized();
			return _connector.UploadMap(key, pngMapImage);
		}

		internal async ValueTask<ZoneDetails> GetZoneDetails()
		{
			if (!_cachedZoneInfo.TryGetValue(out var value))
			{
				ZoneDetails value2 = await _connector.GetZoneDetails();
				value = _cachedZoneInfo.Update(in value2);
				UpdateVariables(_nexusVariables, value.NexusVariables);
				UpdateVariables(_zoneVariables, value.Variables);
			}
			return value;
		}

		internal new async ValueTask<NexusDetails> GetNexusDetails(int nexusId)
		{
			if (!_cachedNexusInfo.TryGetValue(out var value))
			{
				NexusDetails value2 = await _connector.GetNexus(nexusId);
				value = _cachedNexusInfo.Update(in value2);
			}
			return value;
		}

		private void AssertInitialized()
		{
			if (Zone == null)
			{
				throw new InvalidOperationException("Zone is not initialized");
			}
		}
	}
	internal static class Util
	{
		public static T Update<T>(in T currentValue, in T newValue, ref bool changed) where T : IEquatable<T>
		{
			if (EqualityComparer<T>.Default.Equals(currentValue, newValue))
			{
				return currentValue;
			}
			changed = true;
			return newValue;
		}

		public static bool Resize<T>(this List<T> list, int newCount)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (newCount < 0)
			{
				throw new ArgumentOutOfRangeException("newCount");
			}
			if (list.Count == newCount)
			{
				return false;
			}
			if (list.Count > newCount)
			{
				while (list.Count > newCount)
				{
					list.RemoveAt(list.Count - 1);
				}
			}
			else
			{
				while (list.Count < newCount)
				{
					list.Add(default(T));
				}
			}
			return true;
		}
	}
	public struct Uuid : IEquatable<Uuid>
	{
		public static readonly Uuid Empty;

		private static readonly object _syncRoot;

		private static readonly int _nodeId;

		private static int _sequence;

		private static ulong _previousTimestamp;

		public int NodeId { get; set; }

		public int Sequence { get; set; }

		public ulong Timestamp { get; set; }

		public Uuid(int nodeId, int sequence, ulong timestamp)
		{
			NodeId = nodeId;
			Sequence = sequence;
			Timestamp = timestamp;
		}

		public override string ToString()
		{
			return $"{NodeId:X8}{Sequence:X8}{Timestamp:X16}";
		}

		public static implicit operator Uuid(Guid guid)
		{
			return Unsafe.As<Guid, Uuid>(ref guid);
		}

		public static implicit operator Guid(Uuid uuid)
		{
			return Unsafe.As<Uuid, Guid>(ref uuid);
		}

		public bool Equals(Uuid other)
		{
			if (NodeId == other.NodeId && Sequence == other.Sequence)
			{
				return Timestamp == other.Timestamp;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj is Uuid other)
			{
				return Equals(other);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (((NodeId * 397) ^ Sequence) * 397) ^ Timestamp.GetHashCode();
		}

		public static bool operator ==(Uuid left, Uuid right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Uuid left, Uuid right)
		{
			return !left.Equals(right);
		}

		static Uuid()
		{
			if (Marshal.SizeOf<Uuid>() != Marshal.SizeOf<Guid>())
			{
				throw new Exception("sizeof(Uuid) != sizeof(Guid)");
			}
			_syncRoot = new object();
			_nodeId = Environment.MachineName.GetHashCode();
			_sequence = Environment.TickCount;
		}

		public static Uuid Generate()
		{
			lock (_syncRoot)
			{
				ulong ticks = (ulong)DateTime.UtcNow.Ticks;
				if (ticks <= _previousTimestamp)
				{
					_sequence++;
				}
				_previousTimestamp = ticks;
				return new Uuid(_nodeId, _sequence, ticks);
			}
		}
	}
	public class Variable
	{
		private string _rawValue;

		private byte[] _binaryValue;

		public VariableType Type { get; private set; }

		public long LastUpdated { get; private set; }

		internal bool Set(string value, long lastUpdated = 0L)
		{
			if (lastUpdated <= 0)
			{
				lastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			}
			if (value == null)
			{
				value = "";
			}
			int num;
			if (Type == VariableType.String && LastUpdated == lastUpdated)
			{
				num = ((GetAsString() != value) ? 1 : 0);
				if (num == 0)
				{
					goto IL_005e;
				}
			}
			else
			{
				num = 1;
			}
			Type = VariableType.String;
			LastUpdated = lastUpdated;
			_rawValue = value;
			_binaryValue = null;
			goto IL_005e;
			IL_005e:
			return (byte)num != 0;
		}

		internal bool Set(byte[] value, long lastUpdated = 0L)
		{
			if (lastUpdated <= 0)
			{
				lastUpdated = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
			}
			int num;
			if (Type == VariableType.Binary && LastUpdated == lastUpdated && (GetAsBinary() == null || GetAsBinary().Length == 0) == (value == null || value.Length == 0))
			{
				if (value != null)
				{
					num = ((!GetAsBinary().SequenceEqual(value)) ? 1 : 0);
					if (num != 0)
					{
						goto IL_0062;
					}
				}
				else
				{
					num = 0;
				}
				goto IL_0091;
			}
			num = 1;
			goto IL_0062;
			IL_0091:
			return (byte)num != 0;
			IL_0062:
			Type = VariableType.Binary;
			LastUpdated = lastUpdated;
			_rawValue = "*none*";
			_binaryValue = ((value == null) ? Array.Empty<byte>() : value.ToArray());
			goto IL_0091;
		}

		internal bool UpdateFromModel(VariableData data)
		{
			bool result = data.Type != Type || data.LastUpdated != LastUpdated || data.Value != _rawValue;
			Type = data.Type;
			LastUpdated = data.LastUpdated;
			_rawValue = data.Value;
			_binaryValue = null;
			return result;
		}

		public string GetAsString()
		{
			if (Type != VariableType.String)
			{
				throw new InvalidOperationException("Cannot get string value of binary variable");
			}
			if (_rawValue == null)
			{
				throw new InvalidOperationException("Variable has no value to return");
			}
			return _rawValue;
		}

		public byte[] GetAsBinary()
		{
			if (Type != VariableType.Binary)
			{
				throw new InvalidOperationException("Cannot get binary value of string variable");
			}
			if (_rawValue == null)
			{
				throw new InvalidOperationException("Variable has no value to return");
			}
			if (_binaryValue == null)
			{
				_binaryValue = Convert.FromBase64String(_rawValue);
			}
			return _binaryValue;
		}

		public static bool operator ==(Variable variable, string value)
		{
			if (variable.Type == VariableType.String)
			{
				return variable.GetAsString() == (value ?? "");
			}
			return false;
		}

		public static bool operator !=(Variable variable, string value)
		{
			return !(variable == value);
		}

		public static bool operator ==(Variable variable, byte[] value)
		{
			if (variable.Type == VariableType.Binary)
			{
				return variable.GetAsBinary().SequenceEqual(value ?? Array.Empty<byte>());
			}
			return false;
		}

		public static bool operator !=(Variable variable, byte[] value)
		{
			return !(variable == value);
		}
	}
	internal class VariableContainer : IEnumerable<KeyValuePair<string, Variable>>, IEnumerable
	{
		private readonly Dictionary<string, Variable> _variables;

		public int Count => _variables.Count;

		public VariableContainer()
		{
			_variables = new Dictionary<string, Variable>(StringComparer.InvariantCultureIgnoreCase);
		}

		public bool UpdateFromModel(Dictionary<string, VariableData> data, HashSet<string> changedKeys = null)
		{
			if (data == null || data.Count == 0)
			{
				return false;
			}
			bool result = false;
			foreach (KeyValuePair<string, VariableData> datum in data)
			{
				if (UpdateFromModel(datum.Key, datum.Value))
				{
					changedKeys?.Add(datum.Key);
					result = true;
				}
			}
			return result;
		}

		public bool UpdateFromModel(string key, VariableData data)
		{
			if (_variables.TryGetValue(key, out var value))
			{
				return value.UpdateFromModel(data);
			}
			value = new Variable();
			value.UpdateFromModel(data);
			_variables.Add(key, value);
			return true;
		}

		public bool TryGet(string key, out Variable variable)
		{
			return _variables.TryGetValue(key, out variable);
		}

		public bool Set(string key, string value)
		{
			if (!_variables.TryGetValue(key, out var value2))
			{
				value2 = new Variable();
				_variables.Add(key, value2);
			}
			return value2.Set(value, 0L);
		}

		public bool Set(string key, byte[] value)
		{
			if (!_variables.TryGetValue(key, out var value2))
			{
				value2 = new Variable();
				_variables.Add(key, value2);
			}
			return value2.Set(value, 0L);
		}

		public void Remove(string key)
		{
			_variables.Remove(key);
		}

		public void Clear()
		{
			_variables.Clear();
		}

		public Dictionary<string, Variable>.Enumerator GetEnumerator()
		{
			return _variables.GetEnumerator();
		}

		IEnumerator<KeyValuePair<string, Variable>> IEnumerable<KeyValuePair<string, Variable>>.GetEnumerator()
		{
			return _variables.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
namespace Facepunch.Nexus.Time
{
	public class DefaultClockProvider : IClockProvider
	{
		public static DefaultClockProvider Instance { get; } = new DefaultClockProvider();

		public double Timestamp => (double)Stopwatch.GetTimestamp() / (double)Stopwatch.Frequency;

		public Task Delay(double seconds)
		{
			return Task.Delay(TimeSpan.FromSeconds(seconds));
		}
	}
	public interface IClockProvider
	{
		double Timestamp { get; }

		Task Delay(double seconds);
	}
}
namespace Facepunch.Nexus.Models
{
	public struct ClanCreateParameters
	{
		public string ClanName { get; set; }

		public string ClanNameNormalized { get; set; }

		public string LeaderPlayerId { get; set; }

		public string LeaderRoleName { get; set; }

		public string MemberRoleName { get; set; }

		public List<VariableUpdate> ClanVariables { get; set; }

		public List<VariableUpdate> LeaderRoleVariables { get; set; }

		public List<VariableUpdate> MemberRoleVariables { get; set; }
	}
	public struct ClanRole
	{
		public int RoleId { get; set; }

		public int Rank { get; set; }

		public string Name { get; set; }

		public bool CanInvite { get; set; }

		public bool CanKick { get; set; }

		public bool CanPromote { get; set; }

		public bool CanDemote { get; set; }

		public bool CanAccessLogs { get; set; }

		public bool CanAccessScoreEvents { get; set; }

		public VariableDictionary Variables { get; set; }
	}
	public struct ClanMember
	{
		public string PlayerId { get; set; }

		public int RoleId { get; set; }

		public long Joined { get; set; }

		public long LastSeen { get; set; }

		public VariableDictionary Variables { get; set; }
	}
	public struct ClanInvite
	{
		public string PlayerId { get; set; }

		public string RecruiterPlayerId { get; set; }

		public long Created { get; set; }
	}
	public struct ClanDetails
	{
		public long ClanId { get; set; }

		public string Name { get; set; }

		public long Created { get; set; }

		public string Creator { get; set; }

		public long Score { get; set; }

		public List<ClanRole> Roles { get; set; }

		public List<ClanMember> Members { get; set; }

		public int MaxMemberCount { get; set; }

		public List<ClanInvite> Invites { get; set; }

		public VariableDictionary Variables { get; set; }
	}
	public struct ClanDisbandedEvent
	{
		public long ClanId { get; set; }

		public List<string> Members { get; set; }
	}
	public struct ClanInvitedEvent
	{
		public long ClanId { get; set; }

		public string PlayerId { get; set; }
	}
	public struct ClanJoinedEvent
	{
		public long ClanId { get; set; }

		public string PlayerId { get; set; }
	}
	public struct ClanKickedEvent
	{
		public long ClanId { get; set; }

		public string PlayerId { get; set; }
	}
	public struct ClanChangedEvent
	{
		public long ClanId { get; set; }
	}
	public struct ClanInvitation
	{
		public long ClanId { get; set; }

		public string RecruiterPlayerId { get; set; }

		public long Timestamp { get; set; }
	}
	public struct ClanLeaderboardEntry
	{
		public long ClanId { get; set; }

		public string Name { get; set; }

		public long Score { get; set; }
	}
	public struct ClanLogEntry
	{
		public long Timestamp { get; set; }

		public string EventKey { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }

		public string Arg3 { get; set; }

		public string Arg4 { get; set; }
	}
	public struct ClanRoleParameters
	{
		public string Name { get; set; }

		public bool CanInvite { get; set; }

		public bool CanKick { get; set; }

		public bool CanPromote { get; set; }

		public bool CanDemote { get; set; }

		public bool CanAccessLogs { get; set; }

		public bool CanAccessScoreEvents { get; set; }

		public List<VariableUpdate> Variables { get; set; }
	}
	public struct ClanScoreEventEntry
	{
		public long Timestamp { get; set; }

		public int Type { get; set; }

		public int Score { get; set; }

		public int Multiplier { get; set; }

		public string PlayerId { get; set; }

		public string OtherPlayerId { get; set; }

		public long? OtherClanId { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }
	}
	public struct NewClanScoreEventEntry
	{
		public int Type { get; set; }

		public int Score { get; set; }

		public int Multiplier { get; set; }

		public string PlayerId { get; set; }

		public string OtherPlayerId { get; set; }

		public long? OtherClanId { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }
	}
	public struct NewClanScoreEventBatchEntry
	{
		public long ClanId { get; set; }

		public int Type { get; set; }

		public int Score { get; set; }

		public int Multiplier { get; set; }

		public string PlayerId { get; set; }

		public string OtherPlayerId { get; set; }

		public long? OtherClanId { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }
	}
	public struct ClanVariablesUpdate
	{
		public List<VariableUpdate> Variables { get; set; }

		public string EventKey { get; set; }

		public string Arg1 { get; set; }

		public string Arg2 { get; set; }

		public string Arg3 { get; set; }

		public string Arg4 { get; set; }
	}
	internal struct CompleteTransfersRequest
	{
		public IEnumerable<string> PlayerIds { get; set; }
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct CompleteTransfersResponse
	{
	}
	internal struct IncomingMessage
	{
		[JsonProperty("i")]
		public string MessageId { get; set; }

		[JsonIgnore]
		public Guid Id => Guid.Parse(MessageId);

		[JsonProperty("t")]
		public string ContentType { get; set; }

		[JsonProperty("p")]
		public byte[] Data { get; set; }
	}
	public class NexusDetails
	{
		public string Name { get; set; }

		public long LastReset { get; set; }

		public int MaxPlayers { get; set; }

		public int OnlinePlayers { get; set; }

		public int QueuedPlayers { get; set; }

		public int Build { get; set; }

		public int Protocol { get; set; }

		public List<NexusZoneDetails> Zones { get; set; }

		public VariableDictionary Variables { get; set; }
	}
	public class NexusZoneDetails
	{
		public int Id { get; set; }

		public string Key { get; set; }

		public string Name { get; set; }

		public double PositionX { get; set; }

		public double PositionY { get; set; }

		public string IpAddress { get; set; }

		public int GamePort { get; set; }

		public int QueryPort { get; set; }

		public int MaxPlayers { get; set; }

		public int OnlinePlayers { get; set; }

		public int QueuedPlayers { get; set; }

		public int Build { get; set; }

		public int Protocol { get; set; }

		public VariableDictionary Variables { get; set; }
	}
	public struct NexusListing
	{
		public List<Nexus> Nexuses { get; set; }
	}
	public class Nexus
	{
		public int NexusId { get; set; }

		public string Name { get; set; }

		public long LastReset { get; set; }

		public int ZoneCount { get; set; }

		public int MaxPlayers { get; set; }

		public int OnlinePlayers { get; set; }

		public int QueuedPlayers { get; set; }

		public int Build { get; set; }

		public int Protocol { get; set; }

		public string Tags { get; set; }
	}
	public enum NexusRealm : byte
	{
		Development,
		Staging,
		Production,
		Count
	}
	public class PlayerDetails
	{
		public string PlayerId { get; set; }

		public long LastSeen { get; set; }

		public string AssignedZoneKey { get; set; }

		public VariableDictionary Variables { get; set; }

		public string ConnectZoneKey { get; set; }

		public string ConnectIpAddress { get; set; }

		public int? ConnectGamePort { get; set; }

		public int? ConnectQueryPort { get; set; }
	}
	internal struct RegisterTransfersRequest
	{
		public IEnumerable<string> PlayerIds { get; set; }

		public string ToZoneKey { get; set; }
	}
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	internal struct RegisterTransfersResponse
	{
	}
	public enum VariableType : byte
	{
		Binary,
		String
	}
	public struct VariableData
	{
		public string Value { get; set; }

		public VariableType Type { get; set; }

		public long LastUpdated { get; set; }
	}
	public class VariableDictionary : Dictionary<string, VariableData>
	{
		public VariableDictionary()
			: base((IEqualityComparer<string>)StringComparer.InvariantCultureIgnoreCase)
		{
		}
	}
	public readonly struct VariableUpdate
	{
		private readonly Memory<byte> _binaryValue;

		private readonly string _stringValue;

		public string Key { get; }

		public VariableType Type { get; }

		public bool? Transient { get; }

		public bool? Secret { get; }

		public string Value
		{
			get
			{
				if (Type == VariableType.String)
				{
					return _stringValue;
				}
				byte[] array = ArrayPool<byte>.Shared.Rent(_binaryValue.Length);
				try
				{
					_binaryValue.CopyTo(array);
					return Convert.ToBase64String(array, 0, _binaryValue.Length);
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(array);
				}
			}
		}

		public VariableUpdate(string key, Memory<byte> value, bool? transient = null, bool? secret = null)
		{
			Key = key ?? throw new ArgumentNullException("key");
			Type = VariableType.Binary;
			_binaryValue = value;
			_stringValue = null;
			Transient = transient;
			Secret = secret;
		}

		public VariableUpdate(string key, string value, bool? transient = null, bool? secret = null)
		{
			Key = key ?? throw new ArgumentNullException("key");
			Type = VariableType.String;
			_binaryValue = default(Memory<byte>);
			_stringValue = value;
			Transient = transient;
			Secret = secret;
		}
	}
	public class ZoneDetails
	{
		public int NexusId { get; set; }

		public int ZoneId { get; set; }

		public string Key { get; set; }

		public string Name { get; set; }

		public double PositionX { get; set; }

		public double PositionY { get; set; }

		public VariableDictionary Variables { get; set; }

		public string NexusName { get; set; }

		public VariableDictionary NexusVariables { get; set; }
	}
	public class ZoneMapCheckResult
	{
		public string Key { get; set; }

		public DateTimeOffset LastUpdated { get; set; }
	}
	public class ZonePlayerDetails
	{
		public string PlayerId { get; set; }

		public long LastSeen { get; set; }

		public Dictionary<string, VariableData> Variables { get; set; }

		public string AssignedZoneKey { get; set; }
	}
	internal class ZonePlayerLogin : ZonePlayerDetails
	{
		public string RedirectIpAddress { get; set; }

		public int? RedirectGamePort { get; set; }

		public int? RedirectQueryPort { get; set; }
	}
}
namespace Facepunch.Nexus.Logging
{
	public sealed class ConsoleLogger : INexusLogger
	{
		public static ConsoleLogger Instance { get; } = new ConsoleLogger();

		private ConsoleLogger()
		{
		}

		public void Log(NexusLogLevel level, string message, Exception exception = null)
		{
			Console.WriteLine($"[{level}] {message}");
			if (exception != null)
			{
				Console.WriteLine(exception);
			}
		}
	}
	public interface INexusLogger
	{
		void Log(NexusLogLevel level, string message, Exception exception = null);
	}
	public static class NexusLoggerExtensions
	{
		public static void LogInfo(this INexusLogger logger, string message)
		{
			logger?.Log(NexusLogLevel.Info, message);
		}

		public static void LogWarn(this INexusLogger logger, string message, Exception exception = null)
		{
			logger?.Log(NexusLogLevel.Warn, message, exception);
		}

		public static void LogError(this INexusLogger logger, string message, Exception exception = null)
		{
			logger?.Log(NexusLogLevel.Error, message, exception);
		}
	}
	public enum NexusLogLevel
	{
		Info,
		Warn,
		Error
	}
	public sealed class NullLogger : INexusLogger
	{
		public static NullLogger Instance { get; } = new NullLogger();

		private NullLogger()
		{
		}

		public void Log(NexusLogLevel level, string message, Exception exception = null)
		{
		}
	}
}
namespace Facepunch.Nexus.Connector
{
	public interface INexusConnector
	{
		Task<NexusListing> ListNexuses(string publicKey, NexusRealm realm);

		Task<NexusDetails> GetNexus(int nexusId);
	}
	internal class NexusConnector : INexusConnector
	{
		private struct Request
		{
			public HttpMethod Method { get; set; }

			public string Url { get; set; }

			public HttpContent Content { get; set; }
		}

		private const int MaxRetryCount = 5;

		private const double BaseRetryDelay = 3.0;

		protected readonly INexusLogger Logger;

		protected readonly string BaseUrl;

		protected readonly HttpClient HttpClient;

		private static readonly Task<int> CompletedDummyTask = Task.FromResult(0);

		public NexusConnector(INexusLogger logger, string baseUrl)
		{
			Logger = logger ?? NullLogger.Instance;
			BaseUrl = baseUrl?.TrimEnd(new char[1] { '/' }) ?? throw new ArgumentNullException("baseUrl");
			HttpClient = new HttpClient();
		}

		public async Task<NexusListing> ListNexuses(string publicKey, NexusRealm realm)
		{
			if (string.IsNullOrWhiteSpace(publicKey))
			{
				throw new ArgumentNullException("publicKey");
			}
			return (await GetRequest<NexusListing>($"{BaseUrl}?publicKey={WebUtility.UrlEncode(publicKey)}&realm={(int)realm}")).Response;
		}

		public async Task<NexusDetails> GetNexus(int nexusId)
		{
			return (await GetRequest<NexusDetails>($"{BaseUrl}/{nexusId}")).Response;
		}

		protected Task<ApiResult<TResponse>> GetRequest<TResponse>(string url, string authToken = null)
		{
			Request request = new Request
			{
				Method = HttpMethod.Get,
				Url = url
			};
			return SendRequest<TResponse>(in request, authToken);
		}

		protected Task<ApiResult<TResponse>> PostRequest<TResponse>(string url, string authToken = null)
		{
			Request request = new Request
			{
				Method = HttpMethod.Post,
				Url = url
			};
			return SendRequest<TResponse>(in request, authToken);
		}

		protected Task<ApiResult<TResponse>> PostRequest<TRequest, TResponse>(string url, TRequest requestBody, string authToken = null)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
			Request request = new Request
			{
				Method = HttpMethod.Post,
				Url = url,
				Content = content
			};
			return SendRequest<TResponse>(in request, authToken);
		}

		protected Task<ApiResult<TResponse>> DeleteRequest<TResponse>(string url, string authToken = null)
		{
			Request request = new Request
			{
				Method = HttpMethod.Delete,
				Url = url
			};
			return SendRequest<TResponse>(in request, authToken);
		}

		protected Task<ApiResult> PostRequestWithoutResponse<TRequest>(string url, TRequest requestBody, string authToken = null)
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
			return SendRequestWithoutResponse(new Request
			{
				Method = HttpMethod.Post,
				Url = url,
				Content = content
			}, authToken);
		}

		protected Task<ApiResult> PostRequestWithoutResponse(string url, string authToken = null)
		{
			return SendRequestWithoutResponse(new Request
			{
				Method = HttpMethod.Post,
				Url = url
			}, authToken);
		}

		protected Task<ApiResult> PostRequestRawWithoutResponse(string url, string payload, string payloadMimeType, string authToken = null)
		{
			return SendRequestWithoutResponse(new Request
			{
				Method = HttpMethod.Post,
				Url = url,
				Content = new StringContent(payload, Encoding.UTF8, payloadMimeType)
			}, authToken);
		}

		protected Task<ApiResult> PostRequestRawWithoutResponse(string url, ArraySegment<byte> payload, string payloadMimeType, string authToken = null)
		{
			ByteArrayContent byteArrayContent = new ByteArrayContent(payload.Array, payload.Offset, payload.Count);
			byteArrayContent.Headers.ContentType = MediaTypeHeaderValue.Parse(payloadMimeType);
			return SendRequestWithoutResponse(new Request
			{
				Method = HttpMethod.Post,
				Url = url,
				Content = byteArrayContent
			}, authToken);
		}

		protected Task<ApiResult> DeleteRequestWithoutResponse(string url, string authToken = null)
		{
			return SendRequestWithoutResponse(new Request
			{
				Method = HttpMethod.Delete,
				Url = url
			}, authToken);
		}

		private Task<ApiResult<TResponse>> SendRequest<TResponse>(in Request request, string authToken = null)
		{
			return SendRequestImpl(request, authToken, async (HttpResponseMessage response) => JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync()));
		}

		private async Task<ApiResult> SendRequestWithoutResponse(Request request, string authToken = null)
		{
			return new ApiResult((await SendRequestImpl(request, authToken, (HttpResponseMessage _) => CompletedDummyTask)).StatusCode);
		}

		private async Task<ApiResult<TResponse>> SendRequestImpl<TResponse>(Request request, string authToken, Func<HttpResponseMessage, Task<TResponse>> responseReader)
		{
			AuthenticationHeaderValue auth = ((authToken != null) ? new AuthenticationHeaderValue("Bearer", authToken) : null);
			int retryCount = 0;
			HttpResponseMessage response;
			int num;
			while (true)
			{
				HttpRequestMessage httpRequestMessage = new HttpRequestMessage(request.Method, request.Url);
				httpRequestMessage.Headers.Authorization = auth;
				httpRequestMessage.Content = request.Content;
				HttpRequestMessage request2 = httpRequestMessage;
				response = null;
				try
				{
					response = await HttpClient.SendAsync(request2).ConfigureAwait(continueOnCapturedContext: false);
				}
				catch (HttpRequestException exception)
				{
					Logger.LogWarn("Failed to send HTTP request to Nexus", exception);
				}
				num = (int)(response?.StatusCode ?? ((HttpStatusCode)0));
				if (response != null && (num < 500 || num > 599))
				{
					break;
				}
				if (retryCount > 5)
				{
					response?.EnsureSuccessStatusCode();
					throw new Exception($"Expected EnsureSuccessStatusCode to throw for status {num}");
				}
				await Task.Delay(TimeSpan.FromSeconds(Math.Pow(3.0, retryCount))).ConfigureAwait(continueOnCapturedContext: false);
				retryCount++;
			}
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				response.EnsureSuccessStatusCode();
				throw new Exception($"Expected EnsureSuccessStatusCode to throw for status {num}");
			}
			if (response.StatusCode == HttpStatusCode.OK)
			{
				TResponse response2 = await responseReader(response).ConfigureAwait(continueOnCapturedContext: false);
				return new ApiResult<TResponse>(response.StatusCode, response2);
			}
			return new ApiResult<TResponse>(response.StatusCode);
		}
	}
	internal interface INexusPlayerConnector : INexusConnector
	{
		Task<PlayerDetails> GetPlayerDetails(int nexusId, string playerAuthToken);
	}
	internal class NexusPlayerConnector : NexusConnector, INexusPlayerConnector, INexusConnector
	{
		private readonly string _playerInfoEndpoint;

		public NexusPlayerConnector(INexusLogger logger, string baseUrl)
			: base(logger, baseUrl)
		{
			_playerInfoEndpoint = BaseUrl + "/player/info?nexusId=";
		}

		public async Task<PlayerDetails> GetPlayerDetails(int nexusId, string playerAuthToken)
		{
			return (await GetRequest<PlayerDetails>(_playerInfoEndpoint + nexusId, playerAuthToken)).Response;
		}
	}
	internal interface INexusSocketConnector : IDisposable
	{
		bool IsStarted { get; }

		bool IsConnected { get; }

		void Start();

		bool TryReceive(out NexusMessage message);

		void Acknowledge(string messageId);
	}
	internal class NexusSocketConnector : INexusSocketConnector, IDisposable
	{
		private const int MaxMessageIdLength = 64;

		private readonly INexusLogger _logger;

		private readonly CancellationTokenSource _cts;

		private readonly Uri _socketEndpoint;

		private readonly string _secretKey;

		private readonly Queue<NexusMessage> _incoming;

		private readonly Channel<string> _outgoing;

		private bool _started;

		private ClientWebSocket _socket;

		public bool IsStarted => _started;

		public bool IsConnected
		{
			get
			{
				if (_started && _socket != null)
				{
					return _socket.State == WebSocketState.Open;
				}
				return false;
			}
		}

		public NexusSocketConnector(INexusLogger logger, string baseUrl, string secretKey, int lockDuration)
		{
			_logger = logger ?? NullLogger.Instance;
			baseUrl = baseUrl?.TrimEnd(new char[1] { '/' }) ?? throw new ArgumentNullException("baseUrl");
			if (baseUrl.StartsWith("http://"))
			{
				baseUrl = baseUrl.Replace("http://", "ws://");
			}
			if (baseUrl.StartsWith("https://"))
			{
				baseUrl = baseUrl.Replace("https://", "wss://");
			}
			_cts = new CancellationTokenSource();
			_socketEndpoint = new Uri($"{baseUrl}/zone/socket?lockDuration={lockDuration}");
			_secretKey = secretKey ?? throw new ArgumentNullException("secretKey");
			_incoming = new Queue<NexusMessage>();
			_outgoing = Channel.CreateBounded<string>(new BoundedChannelOptions(10)
			{
				SingleReader = true,
				SingleWriter = false
			});
		}

		public void Dispose()
		{
			_outgoing.Writer.Complete();
			_cts.Cancel();
			_cts.Dispose();
			_socket?.Dispose();
			_socket = null;
		}

		public void Start()
		{
			if (_started)
			{
				throw new InvalidOperationException("The socket connector was already started.");
			}
			Task.Run(() => ReconnectLoop(_cts.Token));
			_started = true;
		}

		public bool TryReceive(out NexusMessage message)
		{
			lock (_incoming)
			{
				if (_incoming.Count == 0)
				{
					message = default(NexusMessage);
					return false;
				}
				message = _incoming.Dequeue();
				return true;
			}
		}

		public void Acknowledge(string messageId)
		{
			if (string.IsNullOrWhiteSpace(messageId))
			{
				throw new ArgumentNullException("messageId");
			}
			if (Encoding.UTF8.GetByteCount(messageId) >= 64)
			{
				throw new ArgumentException(string.Format("{0} cannot exceed {1} bytes", "messageId", 64));
			}
			if (!_outgoing.Writer.TryWrite(messageId))
			{
				throw new InvalidOperationException("Failed to enqueue message acknowledgement");
			}
		}

		private async Task ReconnectLoop(CancellationToken ct)
		{
			_ = 2;
			try
			{
				while (!ct.IsCancellationRequested)
				{
					try
					{
						_logger.LogInfo("Connecting to nexus socket...");
						_socket = await Connect(ct);
						_logger.LogInfo("Connected to nexus socket!");
						using (CancellationTokenSource connCts = CancellationTokenSource.CreateLinkedTokenSource(ct))
						{
							await Task.WhenAny(HandleIncoming(connCts.Token), HandleOutgoing(connCts.Token));
							connCts.Cancel();
						}
						_logger.LogInfo("Nexus socket handlers terminated");
					}
					catch (Exception exception) when (!ct.IsCancellationRequested)
					{
						_logger.LogError("Lost connection to Nexus zone socket", exception);
						_socket?.Dispose();
						_socket = null;
						await Task.Delay(5000, ct);
					}
				}
			}
			catch (OperationCanceledException)
			{
			}
			finally
			{
				_logger.LogInfo("Nexus socket reconnect loop is exiting");
			}
		}

		private async Task HandleIncoming(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested && IsConnected)
			{
				byte[] buffer = ArrayPool<byte>.Shared.Rent(1048576);
				try
				{
					int offset = 0;
					WebSocketReceiveResult webSocketReceiveResult;
					do
					{
						ArraySegment<byte> buffer2 = new ArraySegment<byte>(buffer, offset, buffer.Length - offset);
						webSocketReceiveResult = await _socket.ReceiveAsync(buffer2, ct);
						if (webSocketReceiveResult.MessageType == WebSocketMessageType.Close)
						{
							await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", ct);
							return;
						}
						if (webSocketReceiveResult.MessageType != WebSocketMessageType.Text)
						{
							throw new InvalidOperationException($"Unexpected message type {webSocketReceiveResult.MessageType}");
						}
						if (webSocketReceiveResult.Count == 0)
						{
							throw new InvalidOperationException("Received no data");
						}
						offset += webSocketReceiveResult.Count;
						if (offset >= buffer.Length)
						{
							if (buffer.Length >= 67108864)
							{
								throw new InvalidOperationException("Message too large");
							}
							byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length * 2);
							Buffer.BlockCopy(buffer, 0, array, 0, buffer.Length);
							ArrayPool<byte>.Shared.Return(buffer);
							buffer = array;
						}
					}
					while (!webSocketReceiveResult.EndOfMessage);
					NexusMessage item = ReadMessage(buffer, offset);
					lock (_incoming)
					{
						_incoming.Enqueue(item);
					}
				}
				catch (Exception exception) when (!ct.IsCancellationRequested)
				{
					_logger.LogError("Error handling incoming message", exception);
					throw;
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(buffer);
				}
			}
			_logger.LogInfo("Nexus socket incoming handler terminating");
			static NexusMessage ReadMessage(byte[] bytes, int length)
			{
				IncomingMessage incomingMessage = JsonConvert.DeserializeObject<IncomingMessage>(Encoding.UTF8.GetString(bytes, 0, length));
				if (incomingMessage.ContentType.StartsWith("text/") || incomingMessage.ContentType.StartsWith("application/json"))
				{
					string data = Encoding.UTF8.GetString(incomingMessage.Data);
					return new NexusMessage(incomingMessage.MessageId, incomingMessage.Id, incomingMessage.ContentType, data);
				}
				return new NexusMessage(incomingMessage.MessageId, incomingMessage.Id, incomingMessage.ContentType, incomingMessage.Data);
			}
		}

		private async Task HandleOutgoing(CancellationToken ct)
		{
			while (!ct.IsCancellationRequested && IsConnected)
			{
				byte[] buffer = ArrayPool<byte>.Shared.Rent(64);
				try
				{
					string text = await _outgoing.Reader.ReadAsync(ct);
					int bytes = Encoding.UTF8.GetBytes(text, 0, text.Length, buffer, 0);
					ArraySegment<byte> buffer2 = new ArraySegment<byte>(buffer, 0, bytes);
					await _socket.SendAsync(buffer2, WebSocketMessageType.Text, endOfMessage: true, ct);
				}
				catch (Exception exception) when (!ct.IsCancellationRequested)
				{
					_logger.LogError("Error handling outgoing message", exception);
					throw;
				}
				finally
				{
					ArrayPool<byte>.Shared.Return(buffer);
				}
			}
			_logger.LogInfo("Nexus socket incoming handler terminating");
		}

		private async Task<ClientWebSocket> Connect(CancellationToken ct)
		{
			ClientWebSocket socket = new ClientWebSocket();
			socket.Options.KeepAliveInterval = TimeSpan.FromSeconds(30.0);
			socket.Options.SetRequestHeader("Authorization", "Bearer " + _secretKey);
			await socket.ConnectAsync(_socketEndpoint, ct);
			return socket;
		}
	}
	internal interface INexusZoneConnector : INexusConnector
	{
		Task<ZoneDetails> GetZoneDetails();

		Task<ZonePlayerDetails> GetPlayerDetails(string playerId);

		Task<ZonePlayerLogin> PlayerLogin(string playerId);

		Task Assign(string playerId, string toZone);

		Task RegisterTransfers(string toZone, IEnumerable<string> playerIds);

		Task CompleteTransfers(IEnumerable<string> playerIds);

		Task SendMessage(string toZone, Guid id, string message, int? ttl, string contentType = "text/plain");

		Task SendMessage(string toZone, Guid id, Memory<byte> message, int? ttl, string contentType = "application/octet-stream");

		Task SetNexusVariable(string key, string value, bool transient, bool secret);

		Task SetNexusVariable(string key, Memory<byte> value, bool transient, bool secret);

		Task SetZoneVariable(string key, string value, bool transient, bool secret);

		Task SetZoneVariable(string key, Memory<byte> value, bool transient, bool secret);

		Task SetPlayerVariable(string playerId, string key, string value, bool transient, bool secret);

		Task SetPlayerVariable(string playerId, string key, Memory<byte> value, bool transient, bool secret);

		Task<List<string>> FindPlayersWithVariable(string key, string value);

		Task<ZoneMapCheckResult> CheckUploadedMap();

		Task UploadMap(string key, byte[] pngMapImage);

		Task<ApiResult<ClanDetails>> CreateClan(ClanCreateParameters parameters);

		Task<ApiResult<ClanDetails>> GetClan(long clanId);

		Task<ApiResult<ClanDetails>> GetClanByMember(string playerId);

		Task<ApiResult> DisbandClan(long clanId, string byPlayerId);

		Task<ApiResult<ClanDetails>> UpdateClanVariables(long clanId, ClanVariablesUpdate update);

		Task<ApiResult<List<ClanLeaderboardEntry>>> GetClanLeaderboard(int limit = 100);

		Task<ApiResult<ClanDetails>> CreateRole(long clanId, ClanRoleParameters parameters, string byPlayerId);

		Task<ApiResult<ClanDetails>> UpdateRole(long clanId, int roleId, ClanRoleParameters parameters, string byPlayerId);

		Task<ApiResult<ClanDetails>> DeleteRole(long clanId, int roleId, string byPlayerId);

		Task<ApiResult<ClanDetails>> SwapRoleRanks(long clanId, int roleIdA, int roleIdB, string byPlayerId);

		Task<ApiResult<ClanDetails>> CreateInvite(long clanId, string playerId, string byPlayerId);

		Task<ApiResult<ClanDetails>> AcceptInvite(long clanId, string playerId);

		Task<ApiResult<ClanDetails>> CancelInvite(long clanId, string playerId, string byPlayerId);

		Task<ApiResult<ClanDetails>> Kick(long clanId, string playerId, string byPlayerId);

		Task<ApiResult<List<ClanInvitation>>> ListInvitations(string playerId);

		Task<ApiResult> UpdateLastSeen(long clanId, string playerId);

		Task<ApiResult<ClanDetails>> UpdatePlayerClanVariables(long clanId, string playerId, ClanVariablesUpdate update);

		Task<ApiResult<ClanDetails>> SetPlayerRole(long clanId, string playerId, int roleId, string byPlayerId);

		Task<ApiResult<List<ClanLogEntry>>> GetClanLogs(long clanId, string byPlayerId, int limit = 100);

		Task<ApiResult> AddClanLog(long clanId, string eventKey, string arg1 = null, string arg2 = null, string arg3 = null, string arg4 = null);

		Task<ApiResult<List<ClanScoreEventEntry>>> GetClanScoreEvents(long clanId, string byPlayerId, int limit = 100);

		Task<ApiResult> AddClanScoreEvent(long clanId, NewClanScoreEventEntry entry);

		Task<ApiResult> AddClanScoreEventBatch(List<NewClanScoreEventBatchEntry> entries);
	}
	internal class NexusZoneConnector : NexusConnector, INexusZoneConnector, INexusConnector
	{
		private struct NewClanLogEntry
		{
			public string EventKey { get; set; }

			public string Arg1 { get; set; }

			public string Arg2 { get; set; }

			public string Arg3 { get; set; }

			public string Arg4 { get; set; }
		}

		private readonly string _getZoneInfoEndpoint;

		private readonly string _getPlayerInfoEndpoint;

		private readonly string _playerLoginEndpoint;

		private readonly string _assignEndpoint;

		private readonly string _registerTransferEndpoint;

		private readonly string _completeTransferEndpoint;

		private readonly string _messageEndpoint;

		private readonly string _setNexusVariableEndpoint;

		private readonly string _setZoneVariableEndpoint;

		private readonly string _setPlayerVariableEndpoint;

		private readonly string _findPlayersWithVariableEndpoint;

		private readonly string _mapEndpoint;

		private readonly string _clanEndpoint;

		public NexusZoneConnector(INexusLogger logger, string baseUrl, string secretKey)
			: base(logger, baseUrl)
		{
			if (string.IsNullOrWhiteSpace(secretKey))
			{
				throw new ArgumentNullException("secretKey");
			}
			_getZoneInfoEndpoint = BaseUrl + "/zone/info";
			_getPlayerInfoEndpoint = BaseUrl + "/zone/player/info?playerId=";
			_playerLoginEndpoint = BaseUrl + "/zone/login?playerId=";
			_assignEndpoint = BaseUrl + "/zone/assign";
			_registerTransferEndpoint = BaseUrl + "/zone/transfer/register";
			_completeTransferEndpoint = BaseUrl + "/zone/transfer/complete";
			_messageEndpoint = BaseUrl + "/zone/message";
			_setNexusVariableEndpoint = BaseUrl + "/zone/variables/nexus";
			_setZoneVariableEndpoint = BaseUrl + "/zone/variables/zone";
			_setPlayerVariableEndpoint = BaseUrl + "/zone/variables/player";
			_findPlayersWithVariableEndpoint = BaseUrl + "/zone/query";
			_mapEndpoint = BaseUrl + "/zone/map";
			_clanEndpoint = BaseUrl + "/zone/clan";
			HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", secretKey);
		}

		public async Task<ZoneDetails> GetZoneDetails()
		{
			return (await GetRequest<ZoneDetails>(_getZoneInfoEndpoint)).Response;
		}

		public async Task<ZonePlayerDetails> GetPlayerDetails(string playerId)
		{
			return (await GetRequest<ZonePlayerDetails>(_getPlayerInfoEndpoint + playerId)).Response;
		}

		public async Task<ZonePlayerLogin> PlayerLogin(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			return (await PostRequest<ZonePlayerLogin>(_playerLoginEndpoint + playerId)).Response;
		}

		public async Task Assign(string playerId, string toZone)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			(await PostRequestWithoutResponse(_assignEndpoint + "?playerId=" + playerId + "&toZone=" + toZone).ConfigureAwait(continueOnCapturedContext: false)).EnsureSuccessful();
		}

		public async Task RegisterTransfers(string toZone, IEnumerable<string> playerIds)
		{
			if (string.IsNullOrWhiteSpace(toZone))
			{
				throw new ArgumentNullException("toZone");
			}
			if (playerIds == null)
			{
				throw new ArgumentNullException("playerIds");
			}
			(await PostRequest<RegisterTransfersRequest, RegisterTransfersResponse>(_registerTransferEndpoint, new RegisterTransfersRequest
			{
				PlayerIds = playerIds,
				ToZoneKey = toZone
			})).EnsureSuccessfulWithResponse();
		}

		public async Task CompleteTransfers(IEnumerable<string> playerIds)
		{
			if (playerIds == null)
			{
				throw new ArgumentNullException("playerIds");
			}
			(await PostRequest<CompleteTransfersRequest, CompleteTransfersResponse>(_completeTransferEndpoint, new CompleteTransfersRequest
			{
				PlayerIds = playerIds
			})).EnsureSuccessfulWithResponse();
		}

		public async Task SendMessage(string toZone, Guid id, string message, int? ttl, string contentType = "text/plain")
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentNullException("id");
			}
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new ArgumentNullException("message");
			}
			(await PostRequestRawWithoutResponse($"{_messageEndpoint}?toZone={toZone}&id={id}&ttl={ttl}", message, contentType).ConfigureAwait(continueOnCapturedContext: false)).EnsureSuccessful();
		}

		public async Task SendMessage(string toZone, Guid id, Memory<byte> message, int? ttl, string contentType = "application/octet-stream")
		{
			if (id == Guid.Empty)
			{
				throw new ArgumentNullException("id");
			}
			if (message.IsEmpty)
			{
				throw new ArgumentNullException("message");
			}
			byte[] copyBuffer = ArrayPool<byte>.Shared.Rent(message.Length);
			try
			{
				ArraySegment<byte> arraySegment = new ArraySegment<byte>(copyBuffer, 0, message.Length);
				message.CopyTo(arraySegment);
				(await PostRequestRawWithoutResponse($"{_messageEndpoint}?toZone={toZone}&id={id}&ttl={ttl}", arraySegment, contentType).ConfigureAwait(continueOnCapturedContext: false)).EnsureSuccessful();
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(copyBuffer);
			}
		}

		public Task SetNexusVariable(string key, string value, bool transient, bool secret)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			return SetVariableImpl(_setNexusVariableEndpoint, key, value, transient, secret);
		}

		public Task SetNexusVariable(string key, Memory<byte> value, bool transient, bool secret)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			return SetVariableImpl(_setNexusVariableEndpoint, key, value, transient, secret);
		}

		public Task SetZoneVariable(string key, string value, bool transient, bool secret)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			return SetVariableImpl(_setZoneVariableEndpoint, key, value, transient, secret);
		}

		public Task SetZoneVariable(string key, Memory<byte> value, bool transient, bool secret)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			return SetVariableImpl(_setZoneVariableEndpoint, key, value, transient, secret);
		}

		public Task SetPlayerVariable(string playerId, string key, string value, bool transient, bool secret)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			string endpoint = _setPlayerVariableEndpoint + "?playerId=" + playerId;
			return SetVariableImpl(endpoint, key, value, transient, secret);
		}

		public Task SetPlayerVariable(string playerId, string key, Memory<byte> value, bool transient, bool secret)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			string endpoint = _setPlayerVariableEndpoint + "?playerId=" + playerId;
			return SetVariableImpl(endpoint, key, value, transient, secret);
		}

		private async Task SetVariableImpl(string endpoint, string key, string value, bool transient, bool secret)
		{
			VariableUpdate[] updateArray = ArrayPool<VariableUpdate>.Shared.Rent(1);
			try
			{
				updateArray[0] = new VariableUpdate(key, value, transient, secret);
				ArraySegment<VariableUpdate> requestBody = new ArraySegment<VariableUpdate>(updateArray, 0, 1);
				(await PostRequestWithoutResponse(endpoint, requestBody).ConfigureAwait(continueOnCapturedContext: false)).EnsureSuccessful();
			}
			finally
			{
				ArrayPool<VariableUpdate>.Shared.Return(updateArray);
			}
		}

		private async Task SetVariableImpl(string endpoint, string key, Memory<byte> value, bool transient, bool secret)
		{
			VariableUpdate[] updateArray = ArrayPool<VariableUpdate>.Shared.Rent(1);
			try
			{
				updateArray[0] = new VariableUpdate(key, value, transient, secret);
				ArraySegment<VariableUpdate> requestBody = new ArraySegment<VariableUpdate>(updateArray, 0, 1);
				(await PostRequestWithoutResponse(endpoint, requestBody).ConfigureAwait(continueOnCapturedContext: false)).EnsureSuccessful();
			}
			finally
			{
				ArrayPool<VariableUpdate>.Shared.Return(updateArray);
			}
		}

		public async Task<List<string>> FindPlayersWithVariable(string key, string value)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			if (!key.StartsWith("id."))
			{
				throw new ArgumentException("Key must begin with 'id.'", "key");
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentNullException("value");
			}
			return (await GetRequest<List<string>>(_findPlayersWithVariableEndpoint + "?key=" + key + "&value=" + value)).Response;
		}

		public async Task<ZoneMapCheckResult> CheckUploadedMap()
		{
			return (await GetRequest<ZoneMapCheckResult>(_mapEndpoint)).Response;
		}

		public async Task UploadMap(string key, byte[] pngMapImage)
		{
			if (string.IsNullOrWhiteSpace(key))
			{
				throw new ArgumentNullException("key");
			}
			if (pngMapImage == null)
			{
				throw new ArgumentNullException("pngMapImage");
			}
			if (pngMapImage.Length == 0)
			{
				throw new ArgumentException("Image cannot be empty.", "pngMapImage");
			}
			await PostRequestRawWithoutResponse(_mapEndpoint + "?key=" + WebUtility.UrlEncode(key), new ArraySegment<byte>(pngMapImage, 0, pngMapImage.Length), "image/png");
		}

		public Task<ApiResult<ClanDetails>> CreateClan(ClanCreateParameters parameters)
		{
			if (string.IsNullOrWhiteSpace(parameters.ClanName))
			{
				throw new ArgumentException("Clan name must be set.", "parameters");
			}
			if (string.IsNullOrWhiteSpace(parameters.ClanNameNormalized))
			{
				throw new ArgumentException("Normalized clan name must be set.", "parameters");
			}
			if (string.IsNullOrWhiteSpace(parameters.LeaderPlayerId))
			{
				throw new ArgumentException("Leader player ID must be set.", "parameters");
			}
			if (string.IsNullOrWhiteSpace(parameters.LeaderRoleName))
			{
				throw new ArgumentException("Leader role name must be set.", "parameters");
			}
			if (string.IsNullOrWhiteSpace(parameters.MemberRoleName))
			{
				throw new ArgumentException("Member role name must be set.", "parameters");
			}
			return PostRequest<ClanCreateParameters, ClanDetails>(_clanEndpoint, parameters);
		}

		public Task<ApiResult<ClanDetails>> GetClan(long clanId)
		{
			return GetRequest<ClanDetails>($"{_clanEndpoint}/{clanId}");
		}

		public Task<ApiResult<ClanDetails>> GetClanByMember(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			return GetRequest<ClanDetails>(_clanEndpoint + "?playerId=" + playerId);
		}

		public Task<ApiResult> DisbandClan(long clanId, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return DeleteRequestWithoutResponse($"{_clanEndpoint}/{clanId}?byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<ClanDetails>> UpdateClanVariables(long clanId, ClanVariablesUpdate update)
		{
			return PostRequest<ClanVariablesUpdate, ClanDetails>($"{_clanEndpoint}/{clanId}/variables", update);
		}

		public Task<ApiResult<List<ClanLeaderboardEntry>>> GetClanLeaderboard(int limit = 100)
		{
			return GetRequest<List<ClanLeaderboardEntry>>($"{_clanEndpoint}/leaderboard?limit={limit}");
		}

		public Task<ApiResult<ClanDetails>> CreateRole(long clanId, ClanRoleParameters parameters, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(parameters.Name))
			{
				throw new ArgumentException("Role name must be set.", "parameters");
			}
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return PostRequest<ClanRoleParameters, ClanDetails>($"{_clanEndpoint}/{clanId}/roles?byPlayerId={byPlayerId}", parameters);
		}

		public Task<ApiResult<ClanDetails>> UpdateRole(long clanId, int roleId, ClanRoleParameters parameters, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return PostRequest<ClanRoleParameters, ClanDetails>($"{_clanEndpoint}/{clanId}/roles/{roleId}?byPlayerId={byPlayerId}", parameters);
		}

		public Task<ApiResult<ClanDetails>> DeleteRole(long clanId, int roleId, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return DeleteRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/roles/{roleId}?byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<ClanDetails>> SwapRoleRanks(long clanId, int roleIdA, int roleIdB, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return PostRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/roles/swap?roleIdA={roleIdA}&roleIdB={roleIdB}&byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<ClanDetails>> CreateInvite(long clanId, string playerId, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return PostRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/invite?playerId={playerId}&byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<ClanDetails>> AcceptInvite(long clanId, string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			return PostRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/invite/accept?playerId={playerId}");
		}

		public Task<ApiResult<ClanDetails>> CancelInvite(long clanId, string playerId, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return PostRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/invite/cancel?playerId={playerId}&byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<ClanDetails>> Kick(long clanId, string playerId, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return DeleteRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/player/{playerId}?byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<List<ClanInvitation>>> ListInvitations(string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			return GetRequest<List<ClanInvitation>>(_clanEndpoint + "/invitations?playerId=" + playerId);
		}

		public Task<ApiResult> UpdateLastSeen(long clanId, string playerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			return PostRequestWithoutResponse($"{_clanEndpoint}/{clanId}/player/{playerId}/seen");
		}

		public Task<ApiResult<ClanDetails>> UpdatePlayerClanVariables(long clanId, string playerId, ClanVariablesUpdate update)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			return PostRequest<ClanVariablesUpdate, ClanDetails>($"{_clanEndpoint}/{clanId}/player/{playerId}/variables", update);
		}

		public Task<ApiResult<ClanDetails>> SetPlayerRole(long clanId, string playerId, int roleId, string byPlayerId)
		{
			if (string.IsNullOrWhiteSpace(playerId))
			{
				throw new ArgumentNullException("playerId");
			}
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return PostRequest<ClanDetails>($"{_clanEndpoint}/{clanId}/player/{playerId}/role?roleId={roleId}&byPlayerId={byPlayerId}");
		}

		public Task<ApiResult<List<ClanLogEntry>>> GetClanLogs(long clanId, string byPlayerId, int limit = 100)
		{
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return GetRequest<List<ClanLogEntry>>($"{_clanEndpoint}/{clanId}/logs?byPlayerId={byPlayerId}&limit={limit}");
		}

		public Task<ApiResult> AddClanLog(long clanId, string eventKey, string arg1 = null, string arg2 = null, string arg3 = null, string arg4 = null)
		{
			if (string.IsNullOrWhiteSpace(eventKey))
			{
				throw new ArgumentNullException("eventKey");
			}
			return PostRequestWithoutResponse($"{_clanEndpoint}/{clanId}/logs", new NewClanLogEntry
			{
				EventKey = eventKey,
				Arg1 = arg1,
				Arg2 = arg2,
				Arg3 = arg3,
				Arg4 = arg4
			});
		}

		public Task<ApiResult<List<ClanScoreEventEntry>>> GetClanScoreEvents(long clanId, string byPlayerId, int limit = 100)
		{
			if (string.IsNullOrWhiteSpace(byPlayerId))
			{
				throw new ArgumentNullException("byPlayerId");
			}
			return GetRequest<List<ClanScoreEventEntry>>($"{_clanEndpoint}/{clanId}/scoreEvents?byPlayerId={byPlayerId}&limit={limit}");
		}

		public Task<ApiResult> AddClanScoreEvent(long clanId, NewClanScoreEventEntry entry)
		{
			if (entry.Score == 0)
			{
				throw new ArgumentException("Score cannot be zero.", "entry");
			}
			if (entry.Multiplier == 0)
			{
				throw new ArgumentException("Multiplier cannot be zero.", "entry");
			}
			return PostRequestWithoutResponse($"{_clanEndpoint}/{clanId}/scoreEvents", entry);
		}

		public Task<ApiResult> AddClanScoreEventBatch(List<NewClanScoreEventBatchEntry> entries)
		{
			if (entries == null)
			{
				throw new ArgumentNullException("entries");
			}
			if (entries.Count == 0)
			{
				return Task.FromResult(new ApiResult(HttpStatusCode.NoContent));
			}
			foreach (NewClanScoreEventBatchEntry entry in entries)
			{
				if (entry.Score == 0)
				{
					throw new ArgumentException("Score cannot be zero.", "entry");
				}
				if (entry.Multiplier == 0)
				{
					throw new ArgumentException("Multiplier cannot be zero.", "entry");
				}
			}
			return PostRequestWithoutResponse(_clanEndpoint + "/scoreEventsBatch", entries);
		}
	}
}
