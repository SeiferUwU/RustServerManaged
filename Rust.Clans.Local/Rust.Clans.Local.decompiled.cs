using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Facepunch;
using Facepunch.Extend;
using Facepunch.Sqlite;
using Oxide.Core;
using UnityEngine;

[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyVersion("0.0.0.0")]
internal static class DBError
{
	public const int UniqueConstraintViolation = 2067;

	public const int ForeignKeyConstraintViolation = 787;

	public const int TriggerConstraintViolation = 1811;
}
public class LocalClanDatabase : Facepunch.Sqlite.Database
{
	private const int Version = 1;

	private void CreateClansTable()
	{
		Execute("\r\n            CREATE TABLE IF NOT EXISTS clans\r\n            (\r\n                clan_id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,\r\n                name TEXT NOT NULL,\r\n                name_normalized TEXT NOT NULL,\r\n                created INTEGER NOT NULL,\r\n                deleted INTEGER NULL,\r\n                creator INTEGER NOT NULL,\r\n                motd TEXT,\r\n                motd_timestamp INTEGER,\r\n                motd_author INTEGER,\r\n                logo BLOB,\r\n                logo_timestamp INTEGER,\r\n                color INTEGER,\r\n                score INTEGER NOT NULL DEFAULT 0\r\n            );\r\n        ");
		Execute("CREATE UNIQUE INDEX IF NOT EXISTS clans_name_normalized ON clans (name_normalized) WHERE deleted IS NULL;");
		Execute("CREATE INDEX IF NOT EXISTS clans_score ON clans (score DESC) WHERE deleted IS NULL;");
	}

	public long? CreateClan(string name, ulong creatorSteamId)
	{
		string value = name.ToLowerInvariant().Normalize(NormalizationForm.FormKC);
		IntPtr stmHandle = Prepare("INSERT INTO clans (name, name_normalized, created, creator) VALUES (?, ?, ?, ?) RETURNING clan_id");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, name);
		Facepunch.Sqlite.Database.Bind(stmHandle, 2, value);
		Facepunch.Sqlite.Database.Bind(stmHandle, 3, ClanUtility.Timestamp());
		Facepunch.Sqlite.Database.Bind(stmHandle, 4, creatorSteamId);
		long num = ExecuteAndReadQueryResult<long>(stmHandle);
		if (num <= 0)
		{
			return null;
		}
		return num;
	}

	public ClanData? ReadClan(long clanId)
	{
		IntPtr stmHandle = Prepare("SELECT name, created, creator, motd, motd_timestamp, motd_author, logo, logo_timestamp, color, score FROM clans WHERE clan_id = ? AND deleted IS NULL");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		return ExecuteAndReadQueryResult(stmHandle, (IntPtr stm) => new ClanData
		{
			Name = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 0),
			Created = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 1),
			Creator = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 2),
			Motd = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 3),
			MotdTimestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 4),
			MotdAuthor = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 5),
			Logo = Facepunch.Sqlite.Database.GetColumnValue<byte[]>(stm, 6),
			LogoTimestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 7),
			Color = ColorEx.FromInt32(Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 8)),
			Score = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 9)
		});
	}

	public bool UpdateClanMotd(long clanId, string newMotd, ulong authorSteamId)
	{
		Execute("UPDATE clans SET motd = ?, motd_timestamp = ?, motd_author = ? WHERE clan_id = ? AND deleted IS NULL", newMotd, ClanUtility.Timestamp(), authorSteamId, clanId);
		return base.AffectedRows > 0;
	}

	public bool UpdateClanLogo(long clanId, byte[] newLogo)
	{
		Execute("UPDATE clans SET logo = ?, logo_timestamp = ? WHERE clan_id = ? AND deleted IS NULL", newLogo, ClanUtility.Timestamp(), clanId);
		return base.AffectedRows > 0;
	}

	public bool UpdateClanColor(long clanId, Color32 newColor)
	{
		Execute("UPDATE clans SET color = ? WHERE clan_id = ? AND deleted IS NULL", newColor.ToInt32(), clanId);
		return base.AffectedRows > 0;
	}

	public bool DeleteClan(long clanId)
	{
		BeginTransaction();
		Execute("UPDATE clans SET deleted = ? WHERE clan_id = ? AND deleted IS NULL", ClanUtility.Timestamp(), clanId);
		if (base.AffectedRows == 0)
		{
			Rollback();
			return false;
		}
		Execute("DELETE FROM members WHERE clan_id = ?", clanId);
		Execute("DELETE FROM invites WHERE clan_id = ?", clanId);
		Commit();
		return true;
	}

	public long? FindClanByMember(ulong memberSteamId)
	{
		long num = Query<long, ulong>("SELECT clan_id FROM members WHERE user_id = ?", memberSteamId);
		if (num <= 0)
		{
			return null;
		}
		return num;
	}

	public List<ClanLeaderboardEntry> ListTopClans(int limit)
	{
		IntPtr stmHandle = Prepare("SELECT clan_id, name, score FROM clans ORDER BY score DESC LIMIT ?");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, Mathf.Clamp(limit, 10, 100));
		List<ClanLeaderboardEntry> list = Pool.Get<List<ClanLeaderboardEntry>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanLeaderboardEntry
		{
			ClanId = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 0),
			Name = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 1),
			Score = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 2)
		});
		return list;
	}

	public void Open(string rootFolder)
	{
		Open(Path.Combine(rootFolder, $"clans.{1}.db"), fastMode: true);
		Execute("PRAGMA foreign_keys = ON");
		CreateClansTable();
		CreateRolesTable();
		CreateMembersTable();
		CreateInvitesTable();
		CreateLogsTable();
		CreateScoreEventsTable();
	}

	private void CreateInvitesTable()
	{
		Execute("\r\n            CREATE TABLE IF NOT EXISTS invites\r\n            (\r\n                clan_id INTEGER NOT NULL,\r\n                user_id INTEGER NOT NULL,\r\n                recruiter INTEGER NOT NULL,\r\n                timestamp INTEGER NOT NULL,\r\n                PRIMARY KEY (clan_id, user_id),\r\n                FOREIGN KEY (clan_id) REFERENCES clans (clan_id) ON DELETE CASCADE\r\n            ) WITHOUT ROWID;\r\n        ");
		Execute("\r\n            CREATE INDEX IF NOT EXISTS invites_player ON invites (user_id);\r\n        ");
	}

	public bool CreateInvite(long clanId, ulong steamId, ulong recruiterSteamId)
	{
		Execute("\r\n            INSERT OR IGNORE INTO invites (clan_id, user_id, recruiter, timestamp)\r\n            SELECT ?1, ?2, ?3, ?4\r\n            FROM (SELECT 1)\r\n            WHERE NOT EXISTS (SELECT * FROM members m WHERE m.user_id = ?2);\r\n        ", clanId, steamId, recruiterSteamId, ClanUtility.Timestamp());
		return base.AffectedRows > 0;
	}

	public bool AcceptInvite(long clanId, ulong steamId)
	{
		BeginTransaction();
		try
		{
			if (DeleteInvite(clanId, steamId) && CreateMember(clanId, steamId))
			{
				Commit();
				Interface.CallHook("OnClanMemberAdded", clanId, steamId);
				return true;
			}
			Rollback();
			return false;
		}
		catch
		{
			Rollback();
			throw;
		}
	}

	public List<ClanInvite> ListInvites(long clanId)
	{
		IntPtr stmHandle = Prepare("SELECT user_id, recruiter, timestamp FROM invites WHERE clan_id = ? ORDER BY timestamp ASC");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		List<ClanInvite> list = Pool.Get<List<ClanInvite>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanInvite
		{
			SteamId = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 0),
			Recruiter = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 1),
			Timestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 2)
		});
		return list;
	}

	public List<ClanInvitation> ListInvitationsForPlayer(ulong steamId)
	{
		IntPtr stmHandle = Prepare("SELECT clan_id, recruiter, timestamp FROM invites WHERE user_id = ? ORDER BY timestamp ASC");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, steamId);
		List<ClanInvitation> list = Pool.Get<List<ClanInvitation>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanInvitation
		{
			ClanId = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 0),
			Recruiter = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 1),
			Timestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 2)
		});
		return list;
	}

	public bool DeleteInvite(long clanId, ulong steamId)
	{
		Execute("DELETE FROM invites WHERE clan_id = ? AND user_id = ?", clanId, steamId);
		return base.AffectedRows > 0;
	}

	public void DeleteAllInvites(ulong steamId)
	{
		Execute("DELETE FROM invites WHERE user_id = ?", steamId);
	}

	private void CreateLogsTable()
	{
		Execute("\r\n            CREATE TABLE IF NOT EXISTS logs\r\n            (\r\n                clan_id INTEGER NOT NULL,\r\n                timestamp INTEGER NOT NULL,\r\n                event TEXT NOT NULL,\r\n                arg1 TEXT,\r\n                arg2 TEXT,\r\n                arg3 TEXT,\r\n                arg4 TEXT,\r\n                FOREIGN KEY (clan_id) REFERENCES clans (clan_id) ON DELETE CASCADE\r\n            );\r\n        ");
		Execute("\r\n            CREATE INDEX IF NOT EXISTS logs_ordered ON logs (clan_id, timestamp DESC);\r\n        ");
	}

	public void AppendLog(long clanId, string eventKey)
	{
		Execute("INSERT INTO logs (clan_id, timestamp, event) VALUES (?, ?, ?)", clanId, ClanUtility.Timestamp(), eventKey);
	}

	public void AppendLog<T1>(long clanId, string eventKey, T1 arg1)
	{
		Execute("INSERT INTO logs (clan_id, timestamp, event, arg1) VALUES (?, ?, ?, ?)", clanId, ClanUtility.Timestamp(), eventKey, arg1);
	}

	public void AppendLog<T1, T2>(long clanId, string eventKey, T1 arg1, T2 arg2)
	{
		Execute("INSERT INTO logs (clan_id, timestamp, event, arg1, arg2) VALUES (?, ?, ?, ?, ?)", clanId, ClanUtility.Timestamp(), eventKey, arg1, arg2);
	}

	public void AppendLog<T1, T2, T3>(long clanId, string eventKey, T1 arg1, T2 arg2, T3 arg3)
	{
		Execute("INSERT INTO logs (clan_id, timestamp, event, arg1, arg2, arg3) VALUES (?, ?, ?, ?, ?, ?)", clanId, ClanUtility.Timestamp(), eventKey, arg1, arg2, arg3);
	}

	public void AppendLog<T1, T2, T3, T4>(long clanId, string eventKey, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
	{
		Execute("INSERT INTO logs (clan_id, timestamp, event, arg1, arg2, arg3, arg4) VALUES (?, ?, ?, ?, ?, ?, ?)", clanId, ClanUtility.Timestamp(), eventKey, arg1, arg2, arg3, arg4);
	}

	public List<ClanLogEntry> ReadLogs(long clanId, int limit)
	{
		IntPtr stmHandle = Prepare("SELECT timestamp, event, arg1, arg2, arg3, arg4 FROM logs WHERE clan_id = ? ORDER BY timestamp DESC LIMIT ?");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		Facepunch.Sqlite.Database.Bind(stmHandle, 2, Mathf.Clamp(limit, 10, 1000));
		List<ClanLogEntry> list = Pool.Get<List<ClanLogEntry>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanLogEntry
		{
			Timestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 0),
			EventKey = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 1),
			Arg1 = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 2),
			Arg2 = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 3),
			Arg3 = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 4),
			Arg4 = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 5)
		});
		return list;
	}

	private void CreateMembersTable()
	{
		Execute("\r\n            CREATE TABLE IF NOT EXISTS members\r\n            (\r\n                clan_id INTEGER NOT NULL,\r\n                user_id INTEGER NOT NULL,\r\n                role_id INTEGER NOT NULL,\r\n                joined INTEGER NOT NULL,\r\n                seen INTEGER NOT NULL,\r\n                notes TEXT,\r\n                notes_timestamp INTEGER,\r\n                PRIMARY KEY (clan_id, user_id),\r\n                UNIQUE (user_id),\r\n                FOREIGN KEY (clan_id) REFERENCES clans (clan_id) ON DELETE CASCADE,\r\n                FOREIGN KEY (clan_id, role_id) REFERENCES roles (clan_id, role_id) ON UPDATE CASCADE ON DELETE RESTRICT\r\n            ) WITHOUT ROWID;\r\n        ");
	}

	public bool CreateMember(long clanId, ulong steamId)
	{
		Execute("\r\n            INSERT OR IGNORE INTO members (clan_id, user_id, role_id, joined, seen)\r\n            SELECT ?1, ?2, MAX(r.role_id), ?3, ?3\r\n            FROM (SELECT role_id FROM roles WHERE clan_id = ?1 ORDER BY rank DESC LIMIT 1) r\r\n        ", clanId, steamId, ClanUtility.Timestamp());
		return base.AffectedRows > 0;
	}

	public bool CreateMember(long clanId, ulong steamId, int roleId)
	{
		Execute("INSERT INTO members (clan_id, user_id, role_id, joined, seen) VALUES (?1, ?2, ?3, ?4, ?4)", clanId, steamId, roleId, ClanUtility.Timestamp());
		return base.AffectedRows > 0;
	}

	public List<ClanMember> ListMembers(long clanId)
	{
		IntPtr stmHandle = Prepare("\r\n            SELECT m.user_id, m.role_id, m.joined, m.seen, m.notes, m.notes_timestamp\r\n            FROM members m\r\n            LEFT JOIN roles r ON r.clan_id = ?1 AND r.role_id = m.role_id\r\n            WHERE m.clan_id = ?1\r\n            ORDER BY r.rank ASC, joined ASC\r\n        ");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		List<ClanMember> list = Pool.Get<List<ClanMember>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanMember
		{
			SteamId = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 0),
			RoleId = Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 1),
			Joined = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 2),
			LastSeen = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 3),
			Notes = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 4),
			NotesTimestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 5)
		});
		return list;
	}

	public bool UpdateMemberLastSeen(long clanId, ulong steamId)
	{
		Execute("UPDATE members SET seen = ? WHERE clan_id = ? AND user_id = ?", ClanUtility.Timestamp(), clanId, steamId);
		return base.AffectedRows > 0;
	}

	public bool UpdateMemberRole(long clanId, ulong steamId, int newRoleId)
	{
		Execute("UPDATE members SET role_id = ? WHERE clan_id = ? AND user_id = ?", newRoleId, clanId, steamId);
		return base.AffectedRows > 0;
	}

	public bool UpdateMemberNotes(long clanId, ulong steamId, string newNotes)
	{
		Execute("UPDATE members SET notes = ?, notes_timestamp = ? WHERE clan_id = ? AND user_id = ?", newNotes, ClanUtility.Timestamp(), clanId, steamId);
		return base.AffectedRows > 0;
	}

	public bool DeleteMember(long clanId, ulong steamId)
	{
		Execute("DELETE FROM members WHERE clan_id = ? AND user_id = ?", clanId, steamId);
		return base.AffectedRows > 0;
	}

	private void CreateRolesTable()
	{
		Execute("\r\n            CREATE TABLE IF NOT EXISTS roles\r\n            (\r\n                clan_id INTEGER NOT NULL,\r\n                role_id INTEGER NOT NULL,\r\n                rank INTEGER NOT NULL,\r\n                name TEXT NOT NULL,\r\n                can_set_motd BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_set_logo BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_invite BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_kick BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_promote BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_demote BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_set_player_notes BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_access_logs BOOLEAN NOT NULL DEFAULT FALSE,\r\n                can_access_score_events BOOLEAN NOT NULL DEFAULT FALSE,\r\n                PRIMARY KEY (clan_id, role_id),\r\n                FOREIGN KEY (clan_id) REFERENCES clans (clan_id) ON DELETE CASCADE,\r\n                UNIQUE (clan_id, name)\r\n            ) WITHOUT ROWID;\r\n        ");
	}

	public int? CreateRole(long clanId, ClanRole role)
	{
		IntPtr stmHandle = Prepare("\r\n            WITH next AS (\r\n\t            SELECT\r\n                    COALESCE(MAX(role_id), 0) + 1 AS role_id,\r\n                    COALESCE(MAX(rank), 0) + 1 AS rank\r\n                FROM roles r\r\n                WHERE r.clan_id = ?1\r\n            )\r\n            INSERT INTO roles (clan_id, role_id, rank, name, can_set_motd, can_set_logo, can_invite, can_kick, can_promote, can_demote, can_set_player_notes, can_access_logs, can_access_score_events)\r\n            SELECT ?1, next.role_id, next.rank, ?2, ?3, ?4, ?5, ?6, ?7, ?8, ?9, ?10, ?11\r\n            FROM next\r\n            WHERE 1\r\n            RETURNING role_id\r\n        ");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		Facepunch.Sqlite.Database.Bind(stmHandle, 2, role.Name);
		Facepunch.Sqlite.Database.Bind(stmHandle, 3, role.CanSetMotd);
		Facepunch.Sqlite.Database.Bind(stmHandle, 4, role.CanSetLogo);
		Facepunch.Sqlite.Database.Bind(stmHandle, 5, role.CanInvite);
		Facepunch.Sqlite.Database.Bind(stmHandle, 6, role.CanKick);
		Facepunch.Sqlite.Database.Bind(stmHandle, 7, role.CanPromote);
		Facepunch.Sqlite.Database.Bind(stmHandle, 8, role.CanDemote);
		Facepunch.Sqlite.Database.Bind(stmHandle, 9, role.CanSetPlayerNotes);
		Facepunch.Sqlite.Database.Bind(stmHandle, 10, role.CanAccessLogs);
		Facepunch.Sqlite.Database.Bind(stmHandle, 11, role.CanAccessScoreEvents);
		int num = ExecuteAndReadQueryResult<int>(stmHandle);
		if (num <= 0)
		{
			return null;
		}
		return num;
	}

	public List<ClanRole> ListRoles(long clanId)
	{
		IntPtr stmHandle = Prepare("\r\n            SELECT role_id, rank, name, can_set_motd, can_set_logo, can_invite, can_kick, can_promote, can_demote, can_set_player_notes, can_access_logs, can_access_score_events\r\n            FROM roles\r\n            WHERE clan_id = ?\r\n            ORDER BY rank ASC, role_id ASC\r\n        ");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		List<ClanRole> list = Pool.Get<List<ClanRole>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanRole
		{
			RoleId = Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 0),
			Rank = Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 1),
			Name = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 2),
			CanSetMotd = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 3),
			CanSetLogo = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 4),
			CanInvite = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 5),
			CanKick = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 6),
			CanPromote = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 7),
			CanDemote = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 8),
			CanSetPlayerNotes = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 9),
			CanAccessLogs = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 10),
			CanAccessScoreEvents = Facepunch.Sqlite.Database.GetColumnValue<bool>(stm, 11)
		});
		return list;
	}

	public bool UpdateRole(long clanId, ClanRole role)
	{
		IntPtr stmHandle = Prepare("\r\n            UPDATE roles\r\n            SET name = ?3, can_set_motd = ?4, can_set_logo = ?5, can_invite = ?6, can_kick = ?7, can_promote = ?8, can_demote = ?9, can_set_player_notes = ?10, can_access_logs = ?11, can_access_score_events = ?12\r\n            WHERE clan_id = ?1 AND role_id = ?2\r\n        ");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		Facepunch.Sqlite.Database.Bind(stmHandle, 2, role.RoleId);
		Facepunch.Sqlite.Database.Bind(stmHandle, 3, role.Name);
		Facepunch.Sqlite.Database.Bind(stmHandle, 4, role.CanSetMotd);
		Facepunch.Sqlite.Database.Bind(stmHandle, 5, role.CanSetLogo);
		Facepunch.Sqlite.Database.Bind(stmHandle, 6, role.CanInvite);
		Facepunch.Sqlite.Database.Bind(stmHandle, 7, role.CanKick);
		Facepunch.Sqlite.Database.Bind(stmHandle, 8, role.CanPromote);
		Facepunch.Sqlite.Database.Bind(stmHandle, 9, role.CanDemote);
		Facepunch.Sqlite.Database.Bind(stmHandle, 10, role.CanSetPlayerNotes);
		Facepunch.Sqlite.Database.Bind(stmHandle, 11, role.CanAccessLogs);
		Facepunch.Sqlite.Database.Bind(stmHandle, 12, role.CanAccessScoreEvents);
		ExecuteQuery(stmHandle);
		return base.AffectedRows > 0;
	}

	public bool UpdateRoleName(long clanId, int roleId, string newRoleName)
	{
		Execute("UPDATE OR IGNORE roles SET name = ?3 WHERE clan_id = ?1 AND role_id = ?2", clanId, roleId, newRoleName);
		return base.AffectedRows > 0;
	}

	public bool SwapRoleRanks(long clanId, int roleIdA, int roleIdB)
	{
		BeginTransaction();
		try
		{
			int num = Query<int, long, int>("SELECT rank FROM roles WHERE clan_id = ?1 AND role_id = ?2", clanId, roleIdA);
			int num2 = Query<int, long, int>("SELECT rank FROM roles WHERE clan_id = ?1 AND role_id = ?2", clanId, roleIdB);
			if (num <= 0 || num2 <= 0)
			{
				Rollback();
				return false;
			}
			Execute("UPDATE OR IGNORE roles SET rank = ?3 WHERE clan_id = ?1 AND role_id = ?2", clanId, roleIdA, num2);
			if (base.AffectedRows != 1)
			{
				Rollback();
				return false;
			}
			Execute("UPDATE OR IGNORE roles SET rank = ?3 WHERE clan_id = ?1 AND role_id = ?2", clanId, roleIdB, num);
			if (base.AffectedRows != 1)
			{
				Rollback();
				return false;
			}
			Commit();
			return true;
		}
		catch
		{
			Rollback();
			throw;
		}
	}

	public bool DeleteRole(long clanId, int roleId)
	{
		Execute("DELETE FROM roles WHERE clan_id = ? AND role_id = ?", clanId, roleId);
		return base.AffectedRows > 0;
	}

	private void CreateScoreEventsTable()
	{
		Execute("\r\n            CREATE TABLE IF NOT EXISTS score_events\r\n            (\r\n                clan_id INTEGER NOT NULL,\r\n                timestamp INTEGER NOT NULL,\r\n                type INTEGER NOT NULL,\r\n                score INTEGER NOT NULL,\r\n                multiplier INTEGER NOT NULL,\r\n                user_id INTEGER,\r\n                other_user_id INTEGER,\r\n                other_clan_id INTEGER,\r\n                arg1 TEXT,\r\n                arg2 TEXT,\r\n                FOREIGN KEY (clan_id) REFERENCES clans (clan_id) ON DELETE CASCADE\r\n            );\r\n        ");
		Execute("\r\n            CREATE INDEX IF NOT EXISTS score_events_ordered ON logs (clan_id, timestamp DESC);\r\n        ");
	}

	public void AppendScoreEvent(long clanId, ClanScoreEvent e)
	{
		long arg = ((e.Timestamp > 0) ? e.Timestamp : ClanUtility.Timestamp());
		BeginTransaction();
		Execute("INSERT INTO score_events (clan_id, timestamp, type, score, multiplier, user_id, other_user_id, other_clan_id, arg1, arg2) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)", clanId, arg, (int)e.Type, e.Score, e.Multiplier, e.SteamId, e.OtherSteamId, e.OtherClanId, e.Arg1, e.Arg2);
		Execute("UPDATE clans SET score = score + ? WHERE clan_id = ?", e.Score * e.Multiplier, clanId);
		Commit();
	}

	public List<ClanScoreEvent> ReadScoreEvents(long clanId, int limit)
	{
		IntPtr stmHandle = Prepare("SELECT timestamp, type, score, multiplier, user_id, other_user_id, other_clan_id, arg1, arg2 FROM score_events WHERE clan_id = ? ORDER BY timestamp DESC LIMIT ?");
		Facepunch.Sqlite.Database.Bind(stmHandle, 1, clanId);
		Facepunch.Sqlite.Database.Bind(stmHandle, 2, Mathf.Clamp(limit, 10, 1000));
		List<ClanScoreEvent> list = Pool.Get<List<ClanScoreEvent>>();
		ExecuteAndReadQueryResults(stmHandle, list, (IntPtr stm) => new ClanScoreEvent
		{
			Timestamp = Facepunch.Sqlite.Database.GetColumnValue<long>(stm, 0),
			Type = (ClanScoreEventType)Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 1),
			Score = Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 2),
			Multiplier = Facepunch.Sqlite.Database.GetColumnValue<int>(stm, 3),
			SteamId = Facepunch.Sqlite.Database.GetColumnValue<ulong>(stm, 4),
			OtherSteamId = Facepunch.Sqlite.Database.GetColumnValue<ulong?>(stm, 5),
			OtherClanId = Facepunch.Sqlite.Database.GetColumnValue<long?>(stm, 6),
			Arg1 = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 7),
			Arg2 = Facepunch.Sqlite.Database.GetColumnValue<string>(stm, 8)
		});
		return list;
	}
}
public class LocalClan : IClan
{
	[StructLayout(LayoutKind.Auto)]
	[CompilerGenerated]
	private struct <Disband>d__72 : IAsyncStateMachine
	{
		public int <>1__state;

		public AsyncValueTaskMethodBuilder<ClanResult> <>t__builder;

		public LocalClan <>4__this;

		public ulong bySteamId;

		private void MoveNext()
		{
			int num = <>1__state;
			LocalClan localClan = <>4__this;
			ClanResult result;
			try
			{
				if (!localClan.TryGetRank(bySteamId, out var rank) || rank != 1)
				{
					result = ClanResult.NoPermission;
				}
				else if (localClan._backend.Database.DeleteClan(localClan.ClanId))
				{
					localClan._backend.ClanDisbanded(localClan.ClanId);
					List<ClanMember>.Enumerator enumerator = localClan._members.GetEnumerator();
					try
					{
						while (enumerator.MoveNext())
						{
							ClanMember current = enumerator.Current;
							localClan._backend.MembershipChanged(current.SteamId, null);
						}
					}
					finally
					{
						if (num < 0)
						{
							((IDisposable)enumerator/*cast due to .constrained prefix*/).Dispose();
						}
					}
					result = ClanResult.Success;
				}
				else
				{
					result = ClanResult.Fail;
				}
			}
			catch (Exception exception)
			{
				<>1__state = -2;
				<>t__builder.SetException(exception);
				return;
			}
			Interface.CallHook("OnClanDisbanded", localClan, bySteamId);
			<>1__state = -2;
			<>t__builder.SetResult(result);
		}

		void IAsyncStateMachine.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			this.MoveNext();
		}

		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
			<>t__builder.SetStateMachine(stateMachine);
		}

		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
			this.SetStateMachine(stateMachine);
		}
	}

	private const int MaxChatScrollback = 20;

	private readonly LocalClanBackend _backend;

	private readonly List<ClanRole> _roles;

	private readonly List<ClanMember> _members;

	private readonly List<ClanInvite> _invites;

	private readonly List<ClanChatEntry> _chatHistory;

	private RealTimeSince _sinceLastRefresh;

	public long ClanId { get; }

	public string Name { get; private set; }

	public long Created { get; private set; }

	public ulong Creator { get; private set; }

	public string Motd { get; private set; }

	public long MotdTimestamp { get; private set; }

	public ulong MotdAuthor { get; private set; }

	public byte[] Logo { get; private set; }

	public Color32 Color { get; private set; }

	public long Score { get; private set; }

	public IReadOnlyList<ClanRole> Roles => _roles;

	public IReadOnlyList<ClanMember> Members => _members;

	public int MaxMemberCount => _backend.MaxMemberCount;

	public IReadOnlyList<ClanInvite> Invites => _invites;

	public LocalClan(LocalClanBackend backend, long clanId)
	{
		_backend = backend ?? throw new ArgumentNullException("backend");
		ClanId = clanId;
		_roles = new List<ClanRole>();
		_members = new List<ClanMember>();
		_invites = new List<ClanInvite>();
		_chatHistory = new List<ClanChatEntry>(20);
		_sinceLastRefresh = 0f;
	}

	public bool Refresh(ClanDataSource sources = ClanDataSource.All)
	{
		if (sources.HasFlag(ClanDataSource.Basic) || sources.HasFlag(ClanDataSource.Motd) || sources.HasFlag(ClanDataSource.Logo))
		{
			ClanData? clanData = _backend.Database.ReadClan(ClanId);
			if (!clanData.HasValue)
			{
				return false;
			}
			ClanData value = clanData.Value;
			Name = value.Name;
			Created = value.Created;
			Creator = value.Creator;
			Motd = value.Motd;
			MotdTimestamp = value.MotdTimestamp;
			MotdAuthor = value.MotdAuthor;
			Logo = value.Logo;
			Color = value.Color;
			Score = value.Score;
		}
		if (sources.HasFlag(ClanDataSource.Roles))
		{
			List<ClanRole> obj = _backend.Database.ListRoles(ClanId);
			if (obj.Count == 0)
			{
				Pool.FreeUnmanaged(ref obj);
				return false;
			}
			_roles.Clear();
			_roles.AddRange(obj);
			Pool.FreeUnmanaged(ref obj);
		}
		if (sources.HasFlag(ClanDataSource.Members))
		{
			List<ClanMember> obj2 = _backend.Database.ListMembers(ClanId);
			if (obj2.Count == 0)
			{
				Pool.FreeUnmanaged(ref obj2);
				return false;
			}
			_members.Clear();
			_members.AddRange(obj2);
			Pool.FreeUnmanaged(ref obj2);
		}
		if (sources.HasFlag(ClanDataSource.Invites))
		{
			List<ClanInvite> obj3 = _backend.Database.ListInvites(ClanId);
			_invites.Clear();
			_invites.AddRange(obj3);
			Pool.FreeUnmanaged(ref obj3);
		}
		return true;
	}

	public async ValueTask RefreshIfStale()
	{
		if ((float)_sinceLastRefresh > 30f)
		{
			_sinceLastRefresh = 0f;
			Refresh();
		}
	}

	public async ValueTask<ClanValueResult<ClanLogs>> GetLogs(int limit, ulong bySteamId)
	{
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanAccessLogs))
		{
			return ClanResult.NoPermission;
		}
		List<ClanLogEntry> entries = _backend.Database.ReadLogs(ClanId, limit);
		return new ClanValueResult<ClanLogs>(new ClanLogs
		{
			ClanId = ClanId,
			Entries = entries
		});
	}

	public async ValueTask<ClanResult> UpdateLastSeen(ulong steamId)
	{
		return _backend.Database.UpdateMemberLastSeen(ClanId, steamId) ? ClanResult.Success : ClanResult.NotFound;
	}

	public async ValueTask<ClanResult> SetMotd(string newMotd, ulong bySteamId)
	{
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanSetMotd))
		{
			return ClanResult.NoPermission;
		}
		if (newMotd == Motd)
		{
			return ClanResult.Success;
		}
		if (_backend.Database.UpdateClanMotd(ClanId, newMotd, bySteamId))
		{
			_backend.Database.AppendLog(ClanId, "set_motd", bySteamId, newMotd);
			Changed(ClanDataSource.Motd);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> SetLogo(byte[] newLogo, ulong bySteamId)
	{
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanSetLogo))
		{
			return ClanResult.NoPermission;
		}
		if (Logo != null && Logo.Length == newLogo.Length && Logo.SequenceEqual(newLogo))
		{
			return ClanResult.Success;
		}
		if (_backend.Database.UpdateClanLogo(ClanId, newLogo))
		{
			_backend.Database.AppendLog(ClanId, "set_logo", bySteamId);
			Changed(ClanDataSource.Logo);
			Interface.CallHook("OnClanLogoChanged", this, newLogo, bySteamId);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> SetColor(Color32 newColor, ulong bySteamId)
	{
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanSetLogo))
		{
			return ClanResult.NoPermission;
		}
		if (Color.ToInt32() == newColor.ToInt32())
		{
			return ClanResult.Success;
		}
		if (_backend.Database.UpdateClanColor(ClanId, newColor))
		{
			_backend.Database.AppendLog(ClanId, "set_color", bySteamId, newColor.ToHex());
			Changed(ClanDataSource.Basic);
			Interface.CallHook("OnClanColorChanged", this, newColor, bySteamId);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> Invite(ulong steamId, ulong bySteamId)
	{
		if (_backend.MaxMemberCount > 0 && _members.Count >= _backend.MaxMemberCount)
		{
			return ClanResult.ClanIsFull;
		}
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanInvite))
		{
			return ClanResult.NoPermission;
		}
		if (_backend.Database.CreateInvite(ClanId, steamId, bySteamId))
		{
			_backend.Database.AppendLog(ClanId, "invite", bySteamId, steamId);
			Changed(ClanDataSource.Invites);
			_backend.InvitationCreated(steamId, ClanId);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> CancelInvite(ulong steamId, ulong bySteamId)
	{
		if (steamId != bySteamId && !CheckRole(bySteamId, (ClanRole r) => r.CanInvite))
		{
			return ClanResult.NoPermission;
		}
		if (_backend.Database.DeleteInvite(ClanId, steamId))
		{
			if (steamId == bySteamId)
			{
				_backend.Database.AppendLog(ClanId, "decline_invite", bySteamId);
			}
			else
			{
				_backend.Database.AppendLog(ClanId, "cancel_invite", bySteamId, steamId);
			}
			Changed(ClanDataSource.Invites);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> AcceptInvite(ulong steamId)
	{
		if (_backend.MaxMemberCount > 0 && _members.Count >= _backend.MaxMemberCount)
		{
			return ClanResult.ClanIsFull;
		}
		try
		{
			if (_backend.Database.AcceptInvite(ClanId, steamId))
			{
				_backend.Database.AppendLog(ClanId, "accept_invite", steamId);
				Changed(ClanDataSource.Members | ClanDataSource.Invites);
				_backend.MembershipChanged(steamId, ClanId);
				return ClanResult.Success;
			}
		}
		catch (SqliteException ex) when (ex.Result == 2067)
		{
			return ClanResult.AlreadyInAClan;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> Kick(ulong steamId, ulong bySteamId)
	{
		if (!TryGetRank(steamId, out var rank))
		{
			return ClanResult.NotFound;
		}
		bool flag = steamId == bySteamId;
		if (!flag)
		{
			if (!CheckRole(bySteamId, (ClanRole r) => r.CanKick))
			{
				return ClanResult.NoPermission;
			}
			if (!TryGetRank(bySteamId, out var rank2))
			{
				return ClanResult.NotFound;
			}
			if (rank <= rank2 && rank2 != 1)
			{
				return ClanResult.NoPermission;
			}
		}
		else
		{
			if (_members.Count == 1)
			{
				return await Disband(bySteamId);
			}
			if (rank == 1 && OtherLeaderCount(steamId) == 0)
			{
				return ClanResult.CannotKickLeader;
			}
		}
		if (_backend.Database.DeleteMember(ClanId, steamId))
		{
			if (flag)
			{
				Interface.CallHook("OnClanMemberLeft", this, steamId);
				_backend.Database.AppendLog(ClanId, "leave", steamId);
			}
			else
			{
				Interface.CallHook("OnClanMemberKicked", this, steamId, bySteamId);
				_backend.Database.AppendLog(ClanId, "kick", bySteamId, steamId);
			}
			Changed(ClanDataSource.Members);
			_backend.MembershipChanged(steamId, null);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> SetPlayerRole(ulong steamId, int newRoleId, ulong bySteamId)
	{
		ClanMember? clanMember = _members.TryFindWith((ClanMember m) => m.SteamId, steamId);
		if (!clanMember.HasValue)
		{
			return ClanResult.NotFound;
		}
		ClanRole? clanRole = _roles.TryFindWith((ClanRole r) => r.RoleId, clanMember.Value.RoleId);
		if (!clanRole.HasValue)
		{
			return ClanResult.Fail;
		}
		ClanRole? clanRole2 = _roles.TryFindWith((ClanRole r) => r.RoleId, newRoleId);
		if (!clanRole2.HasValue)
		{
			return ClanResult.NotFound;
		}
		if (!TryGetRank(bySteamId, out var rank))
		{
			return ClanResult.NotFound;
		}
		if (clanRole.Value.Rank <= rank && rank != 1)
		{
			return ClanResult.NoPermission;
		}
		if (clanRole2.Value.Rank <= rank && rank != 1)
		{
			return ClanResult.NoPermission;
		}
		if (!((clanRole2.Value.Rank < clanRole.Value.Rank) ? CheckRole(bySteamId, (ClanRole r) => r.CanPromote) : CheckRole(bySteamId, (ClanRole r) => r.CanDemote)))
		{
			return ClanResult.NoPermission;
		}
		if (clanMember.Value.RoleId == newRoleId)
		{
			return ClanResult.Success;
		}
		if (rank == 1 && steamId == bySteamId && OtherLeaderCount(steamId) == 0)
		{
			return ClanResult.CannotDemoteLeader;
		}
		if (_backend.Database.UpdateMemberRole(ClanId, steamId, newRoleId))
		{
			_backend.Database.AppendLog(ClanId, "change_role", bySteamId, steamId, clanRole.Value.Name, clanRole2.Value.Name);
			Changed(ClanDataSource.Members);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> SetPlayerNotes(ulong steamId, string newNotes, ulong bySteamId)
	{
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanSetPlayerNotes))
		{
			return ClanResult.NoPermission;
		}
		ClanMember? clanMember = _members.TryFindWith((ClanMember m) => m.SteamId, steamId);
		if (!clanMember.HasValue)
		{
			return ClanResult.NotFound;
		}
		if (clanMember.Value.Notes == newNotes)
		{
			return ClanResult.Success;
		}
		if (_backend.Database.UpdateMemberNotes(ClanId, steamId, newNotes))
		{
			_backend.Database.AppendLog(ClanId, "set_notes", bySteamId, steamId, newNotes);
			Changed(ClanDataSource.Members);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> CreateRole(ClanRole role, ulong bySteamId)
	{
		if (string.IsNullOrWhiteSpace(role.Name))
		{
			return ClanResult.InvalidText;
		}
		if (!TryGetRank(bySteamId, out var rank) || rank != 1)
		{
			return ClanResult.NoPermission;
		}
		try
		{
			if (_backend.Database.CreateRole(ClanId, role).HasValue)
			{
				_backend.Database.AppendLog(ClanId, "create_role", bySteamId, role.Name);
				Changed(ClanDataSource.Roles);
				return ClanResult.Success;
			}
		}
		catch (SqliteException ex) when (ex.Result == 2067)
		{
			return ClanResult.DuplicateName;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> UpdateRole(ClanRole role, ulong bySteamId)
	{
		if (string.IsNullOrWhiteSpace(role.Name))
		{
			return ClanResult.InvalidText;
		}
		if (!TryGetRank(bySteamId, out var rank) || rank != 1)
		{
			return ClanResult.NoPermission;
		}
		ClanRole? clanRole = _roles.TryFindWith((ClanRole r) => r.RoleId, role.RoleId);
		if (!clanRole.HasValue)
		{
			return ClanResult.NotFound;
		}
		try
		{
			if ((clanRole.Value.Rank == 1) ? _backend.Database.UpdateRoleName(ClanId, role.RoleId, role.Name) : _backend.Database.UpdateRole(ClanId, role))
			{
				if (role.Name != clanRole.Value.Name)
				{
					_backend.Database.AppendLog(ClanId, "update_role_renamed", bySteamId, clanRole.Value.Name, role.Name);
				}
				else
				{
					_backend.Database.AppendLog(ClanId, "update_role", bySteamId, role.Name);
				}
				Changed(ClanDataSource.Roles);
				return ClanResult.Success;
			}
		}
		catch (SqliteException ex) when (ex.Result == 2067)
		{
			return ClanResult.DuplicateName;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> SwapRoleRanks(int roleIdA, int roleIdB, ulong bySteamId)
	{
		if (!TryGetRank(bySteamId, out var rank) || rank != 1)
		{
			return ClanResult.NoPermission;
		}
		ClanRole? clanRole = _roles.TryFindWith((ClanRole r) => r.RoleId, roleIdA);
		if (!clanRole.HasValue)
		{
			return ClanResult.NotFound;
		}
		ClanRole? clanRole2 = _roles.TryFindWith((ClanRole r) => r.RoleId, roleIdB);
		if (!clanRole2.HasValue)
		{
			return ClanResult.NotFound;
		}
		if (clanRole.Value.Rank == 1 || clanRole2.Value.Rank == 1)
		{
			return ClanResult.CannotSwapLeader;
		}
		if (_backend.Database.SwapRoleRanks(ClanId, roleIdA, roleIdB))
		{
			_backend.Database.AppendLog(ClanId, "swap_roles", bySteamId, clanRole.Value.Name, clanRole2.Value.Name);
			Changed(ClanDataSource.Roles);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	public async ValueTask<ClanResult> DeleteRole(int roleId, ulong bySteamId)
	{
		if (!TryGetRank(bySteamId, out var rank) || rank != 1)
		{
			return ClanResult.NoPermission;
		}
		ClanRole? clanRole = _roles.TryFindWith((ClanRole r) => r.RoleId, roleId);
		if (!clanRole.HasValue)
		{
			return ClanResult.NotFound;
		}
		if (clanRole.Value.Rank == 1)
		{
			return ClanResult.CannotDeleteLeader;
		}
		bool flag;
		try
		{
			flag = _backend.Database.DeleteRole(ClanId, roleId);
		}
		catch (SqliteException ex) when (ex.Result == 787 || ex.Result == 1811)
		{
			return ClanResult.RoleNotEmpty;
		}
		if (flag)
		{
			_backend.Database.AppendLog(ClanId, "delete_role", bySteamId, clanRole.Value.Name);
			Changed(ClanDataSource.Roles);
			return ClanResult.Success;
		}
		return ClanResult.Fail;
	}

	[AsyncStateMachine(typeof(<Disband>d__72))]
	public ValueTask<ClanResult> Disband(ulong bySteamId)
	{
		<Disband>d__72 stateMachine = default(<Disband>d__72);
		stateMachine.<>t__builder = AsyncValueTaskMethodBuilder<ClanResult>.Create();
		stateMachine.<>4__this = this;
		stateMachine.bySteamId = bySteamId;
		stateMachine.<>1__state = -1;
		stateMachine.<>t__builder.Start(ref stateMachine);
		return stateMachine.<>t__builder.Task;
	}

	public async ValueTask<ClanValueResult<ClanScoreEvents>> GetScoreEvents(int limit, ulong bySteamId)
	{
		if (!CheckRole(bySteamId, (ClanRole r) => r.CanAccessScoreEvents))
		{
			return ClanResult.NoPermission;
		}
		List<ClanScoreEvent> scoreEvents = _backend.Database.ReadScoreEvents(ClanId, limit);
		return new ClanValueResult<ClanScoreEvents>(new ClanScoreEvents
		{
			ClanId = ClanId,
			ScoreEvents = scoreEvents
		});
	}

	public async ValueTask<ClanResult> AddScoreEvent(ClanScoreEvent scoreEvent)
	{
		if (scoreEvent.Score == 0)
		{
			throw new ArgumentException("Score cannot be zero.", "scoreEvent");
		}
		if (scoreEvent.Multiplier == 0)
		{
			throw new ArgumentException("Multiplier cannot be zero.", "scoreEvent");
		}
		_backend.Database.AppendScoreEvent(ClanId, scoreEvent);
		Changed(ClanDataSource.Score);
		return ClanResult.Success;
	}

	public async ValueTask<ClanValueResult<ClanChatScrollback>> GetChatScrollback()
	{
		return new ClanValueResult<ClanChatScrollback>(new ClanChatScrollback
		{
			ClanId = ClanId,
			Entries = _chatHistory.ToList()
		});
	}

	public async ValueTask<ClanResult> SendChatMessage(string name, string message, ulong bySteamId)
	{
		if (!TryGetRank(bySteamId, out var _))
		{
			return ClanResult.Fail;
		}
		if (!ClanValidator.ValidateChatMessage(message, out var validated))
		{
			return ClanResult.InvalidText;
		}
		ClanChatEntry clanChatEntry = new ClanChatEntry
		{
			SteamId = bySteamId,
			Name = name,
			Message = validated,
			Time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
		};
		if (_chatHistory.Count >= 20)
		{
			_chatHistory.RemoveAt(0);
		}
		_chatHistory.Add(clanChatEntry);
		_backend.ClanChatMessage(ClanId, clanChatEntry);
		return ClanResult.Success;
	}

	private int OtherLeaderCount(ulong excludeSteamId)
	{
		int num = 0;
		foreach (ClanMember member in _members)
		{
			if (member.SteamId != excludeSteamId && TryGetRank(member.SteamId, out var rank) && rank == 1)
			{
				num++;
			}
		}
		return num;
	}

	private bool CheckRole(ulong steamId, Func<ClanRole, bool> roleTest)
	{
		ClanMember? clanMember = _members.TryFindWith((ClanMember m) => m.SteamId, steamId);
		if (!clanMember.HasValue)
		{
			return false;
		}
		ClanRole? clanRole = _roles.TryFindWith((ClanRole r) => r.RoleId, clanMember.Value.RoleId);
		if (!clanRole.HasValue)
		{
			return false;
		}
		if (clanRole.Value.Rank != 1)
		{
			return roleTest(clanRole.Value);
		}
		return true;
	}

	private bool TryGetRank(ulong steamId, out int rank)
	{
		ClanMember? clanMember = _members.TryFindWith((ClanMember m) => m.SteamId, steamId);
		if (!clanMember.HasValue)
		{
			rank = int.MaxValue;
			return false;
		}
		ClanRole? clanRole = _roles.TryFindWith((ClanRole r) => r.RoleId, clanMember.Value.RoleId);
		if (!clanRole.HasValue)
		{
			rank = int.MaxValue;
			return false;
		}
		rank = clanRole.Value.Rank;
		return true;
	}

	private void Changed(ClanDataSource dataSources)
	{
		_backend.ClanChanged(ClanId, dataSources);
		Refresh(dataSources);
	}
}
public class LocalClanBackend : IClanBackend, IDisposable, IClanChangeSink
{
	private readonly string _rootFolder;

	public readonly int MaxMemberCount;

	private readonly Dictionary<long, LocalClan> _clans;

	private IClanChangeSink _changeSink;

	public readonly LocalClanDatabase Database;

	public LocalClanBackend(string rootFolder, int maxMemberCount)
	{
		_rootFolder = rootFolder;
		MaxMemberCount = Math.Max(maxMemberCount, 0);
		_clans = new Dictionary<long, LocalClan>();
		Database = new LocalClanDatabase();
	}

	public async ValueTask Initialize(IClanChangeSink changeSink)
	{
		_changeSink = changeSink ?? throw new ArgumentNullException("changeSink");
		Database.Open(_rootFolder);
	}

	public void Dispose()
	{
		Database.Close();
	}

	public async ValueTask<ClanValueResult<IClan>> Get(long clanId)
	{
		if (clanId <= 0)
		{
			return ClanResult.NotFound;
		}
		LocalClan clan;
		return TryGetClan(clanId, out clan) ? new ClanValueResult<IClan>(clan) : ((ClanValueResult<IClan>)ClanResult.NotFound);
	}

	public bool TryGet(long clanId, out IClan clan)
	{
		if (_clans.TryGetValue(clanId, out var value))
		{
			clan = value;
			return true;
		}
		clan = null;
		return false;
	}

	public async ValueTask<ClanValueResult<IClan>> GetByMember(ulong steamId)
	{
		long? num = Database.FindClanByMember(steamId);
		if (!num.HasValue)
		{
			return ClanResult.NoClan;
		}
		LocalClan clan;
		return TryGetClan(num.Value, out clan) ? new ClanValueResult<IClan>(clan) : ((ClanValueResult<IClan>)ClanResult.NotFound);
	}

	public async ValueTask<ClanValueResult<IClan>> Create(ulong leaderSteamId, string name)
	{
		Database.BeginTransaction();
		long clanId;
		try
		{
			try
			{
				clanId = Database.CreateClan(name, leaderSteamId) ?? throw new Exception("Failed to create clan");
			}
			catch (SqliteException ex) when (ex.Result == 2067)
			{
				Database.Rollback();
				return ClanResult.DuplicateName;
			}
			int num = Database.CreateRole(clanId, new ClanRole
			{
				Name = "Leader",
				CanSetMotd = true,
				CanSetLogo = true,
				CanInvite = true,
				CanKick = true,
				CanPromote = true,
				CanDemote = true,
				CanSetPlayerNotes = true
			}) ?? throw new Exception("Failed to create leader role");
			if (num != 1)
			{
				throw new Exception("Owner role does not have rank 1!");
			}
			_ = Database.CreateRole(clanId, new ClanRole
			{
				Name = "Member",
				CanSetMotd = false,
				CanSetLogo = false,
				CanInvite = false,
				CanKick = false,
				CanPromote = false,
				CanDemote = false,
				CanSetPlayerNotes = false
			}) ?? throw new Exception("Failed to create member role");
			try
			{
				if (!Database.CreateMember(clanId, leaderSteamId, num))
				{
					throw new Exception("Failed to add leader to new clan");
				}
			}
			catch (SqliteException ex2) when (ex2.Result == 2067)
			{
				Database.Rollback();
				return ClanResult.AlreadyInAClan;
			}
			Database.AppendLog(clanId, "founded", leaderSteamId);
			Database.Commit();
		}
		catch
		{
			Database.Rollback();
			throw;
		}
		if (!TryGetClan(clanId, out var clan))
		{
			throw new Exception("Couldn't find the clan we just created?");
		}
		MembershipChanged(leaderSteamId, clan.ClanId);
		ClanValueResult<IClan> result = (ClanValueResult<IClan>)(IClan)clan;
		Interface.CallHook("OnClanCreated", clan, leaderSteamId);
		return result;
	}

	public async ValueTask<ClanValueResult<List<ClanInvitation>>> ListInvitations(ulong steamId)
	{
		return Database.ListInvitationsForPlayer(steamId);
	}

	public async ValueTask<ClanValueResult<List<ClanLeaderboardEntry>>> GetLeaderboard(int limit)
	{
		return Database.ListTopClans(limit);
	}

	private bool TryGetClan(long clanId, out LocalClan clan)
	{
		if (_clans.TryGetValue(clanId, out var value))
		{
			clan = value;
			return value != null;
		}
		LocalClan localClan = new LocalClan(this, clanId);
		if (!localClan.Refresh())
		{
			_clans.Add(clanId, null);
			clan = null;
			return false;
		}
		_clans.Add(clanId, localClan);
		clan = localClan;
		return true;
	}

	public void ClanChanged(long clanId, ClanDataSource dataSources)
	{
		_changeSink.ClanChanged(clanId, dataSources);
	}

	public void ClanDisbanded(long clanId)
	{
		_clans.Remove(clanId);
		_changeSink.ClanDisbanded(clanId);
	}

	public void InvitationCreated(ulong steamId, long clanId)
	{
		_changeSink.InvitationCreated(steamId, clanId);
	}

	public void MembershipChanged(ulong steamId, long? clanId)
	{
		_changeSink.MembershipChanged(steamId, clanId);
	}

	public void ClanChatMessage(long clanId, ClanChatEntry entry)
	{
		_changeSink.ClanChatMessage(clanId, entry);
	}
}
public struct ClanData
{
	public string Name;

	public long Created;

	public ulong Creator;

	public string Motd;

	public long MotdTimestamp;

	public ulong MotdAuthor;

	public byte[] Logo;

	public long LogoTimestamp;

	public Color32 Color;

	public long Score;
}
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
[EditorBrowsable(EditorBrowsableState.Never)]
[CompilerGenerated]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[761]
			{
				0, 0, 0, 1, 0, 0, 0, 52, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 67,
				108, 97, 110, 115, 46, 76, 111, 99, 97, 108,
				92, 68, 97, 116, 97, 98, 97, 115, 101, 92,
				68, 66, 69, 114, 114, 111, 114, 46, 99, 115,
				0, 0, 0, 1, 0, 0, 0, 68, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 82, 117, 115, 116, 46, 67,
				108, 97, 110, 115, 46, 76, 111, 99, 97, 108,
				92, 68, 97, 116, 97, 98, 97, 115, 101, 92,
				76, 111, 99, 97, 108, 67, 108, 97, 110, 68,
				97, 116, 97, 98, 97, 115, 101, 46, 67, 108,
				97, 110, 115, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 62, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				82, 117, 115, 116, 46, 67, 108, 97, 110, 115,
				46, 76, 111, 99, 97, 108, 92, 68, 97, 116,
				97, 98, 97, 115, 101, 92, 76, 111, 99, 97,
				108, 67, 108, 97, 110, 68, 97, 116, 97, 98,
				97, 115, 101, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 70, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				82, 117, 115, 116, 46, 67, 108, 97, 110, 115,
				46, 76, 111, 99, 97, 108, 92, 68, 97, 116,
				97, 98, 97, 115, 101, 92, 76, 111, 99, 97,
				108, 67, 108, 97, 110, 68, 97, 116, 97, 98,
				97, 115, 101, 46, 73, 110, 118, 105, 116, 101,
				115, 46, 99, 115, 0, 0, 0, 1, 0, 0,
				0, 67, 92, 65, 115, 115, 101, 116, 115, 92,
				80, 108, 117, 103, 105, 110, 115, 92, 82, 117,
				115, 116, 46, 67, 108, 97, 110, 115, 46, 76,
				111, 99, 97, 108, 92, 68, 97, 116, 97, 98,
				97, 115, 101, 92, 76, 111, 99, 97, 108, 67,
				108, 97, 110, 68, 97, 116, 97, 98, 97, 115,
				101, 46, 76, 111, 103, 115, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 70, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 67, 108,
				97, 110, 115, 46, 76, 111, 99, 97, 108, 92,
				68, 97, 116, 97, 98, 97, 115, 101, 92, 76,
				111, 99, 97, 108, 67, 108, 97, 110, 68, 97,
				116, 97, 98, 97, 115, 101, 46, 77, 101, 109,
				98, 101, 114, 115, 46, 99, 115, 0, 0, 0,
				1, 0, 0, 0, 68, 92, 65, 115, 115, 101,
				116, 115, 92, 80, 108, 117, 103, 105, 110, 115,
				92, 82, 117, 115, 116, 46, 67, 108, 97, 110,
				115, 46, 76, 111, 99, 97, 108, 92, 68, 97,
				116, 97, 98, 97, 115, 101, 92, 76, 111, 99,
				97, 108, 67, 108, 97, 110, 68, 97, 116, 97,
				98, 97, 115, 101, 46, 82, 111, 108, 101, 115,
				46, 99, 115, 0, 0, 0, 1, 0, 0, 0,
				68, 92, 65, 115, 115, 101, 116, 115, 92, 80,
				108, 117, 103, 105, 110, 115, 92, 82, 117, 115,
				116, 46, 67, 108, 97, 110, 115, 46, 76, 111,
				99, 97, 108, 92, 68, 97, 116, 97, 98, 97,
				115, 101, 92, 76, 111, 99, 97, 108, 67, 108,
				97, 110, 68, 97, 116, 97, 98, 97, 115, 101,
				46, 83, 99, 111, 114, 101, 46, 99, 115, 0,
				0, 0, 1, 0, 0, 0, 45, 92, 65, 115,
				115, 101, 116, 115, 92, 80, 108, 117, 103, 105,
				110, 115, 92, 82, 117, 115, 116, 46, 67, 108,
				97, 110, 115, 46, 76, 111, 99, 97, 108, 92,
				76, 111, 99, 97, 108, 67, 108, 97, 110, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 52,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 67, 108, 97, 110, 115, 46, 76, 111, 99,
				97, 108, 92, 76, 111, 99, 97, 108, 67, 108,
				97, 110, 66, 97, 99, 107, 101, 110, 100, 46,
				99, 115, 0, 0, 0, 1, 0, 0, 0, 51,
				92, 65, 115, 115, 101, 116, 115, 92, 80, 108,
				117, 103, 105, 110, 115, 92, 82, 117, 115, 116,
				46, 67, 108, 97, 110, 115, 46, 76, 111, 99,
				97, 108, 92, 77, 111, 100, 101, 108, 115, 92,
				67, 108, 97, 110, 68, 97, 116, 97, 46, 99,
				115
			},
			TypesData = new byte[225]
			{
				0, 0, 0, 0, 8, 124, 68, 66, 69, 114,
				114, 111, 114, 1, 0, 0, 0, 18, 124, 76,
				111, 99, 97, 108, 67, 108, 97, 110, 68, 97,
				116, 97, 98, 97, 115, 101, 1, 0, 0, 0,
				18, 124, 76, 111, 99, 97, 108, 67, 108, 97,
				110, 68, 97, 116, 97, 98, 97, 115, 101, 1,
				0, 0, 0, 18, 124, 76, 111, 99, 97, 108,
				67, 108, 97, 110, 68, 97, 116, 97, 98, 97,
				115, 101, 1, 0, 0, 0, 18, 124, 76, 111,
				99, 97, 108, 67, 108, 97, 110, 68, 97, 116,
				97, 98, 97, 115, 101, 1, 0, 0, 0, 18,
				124, 76, 111, 99, 97, 108, 67, 108, 97, 110,
				68, 97, 116, 97, 98, 97, 115, 101, 1, 0,
				0, 0, 18, 124, 76, 111, 99, 97, 108, 67,
				108, 97, 110, 68, 97, 116, 97, 98, 97, 115,
				101, 1, 0, 0, 0, 18, 124, 76, 111, 99,
				97, 108, 67, 108, 97, 110, 68, 97, 116, 97,
				98, 97, 115, 101, 0, 0, 0, 0, 10, 124,
				76, 111, 99, 97, 108, 67, 108, 97, 110, 0,
				0, 0, 0, 17, 124, 76, 111, 99, 97, 108,
				67, 108, 97, 110, 66, 97, 99, 107, 101, 110,
				100, 0, 0, 0, 0, 9, 124, 67, 108, 97,
				110, 68, 97, 116, 97
			},
			TotalFiles = 11,
			TotalTypes = 11,
			IsEditorOnly = false
		};
	}
}
