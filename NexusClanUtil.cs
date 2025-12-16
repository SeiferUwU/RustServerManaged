using System;
using System.Collections.Generic;
using Facepunch.Nexus;
using Facepunch.Nexus.Models;
using UnityEngine;

public static class NexusClanUtil
{
	public const string MotdVariable = "motd";

	public const string MotdAuthorVariable = "motd_author";

	public const string LogoVariable = "logo";

	public const string ColorVariable = "color";

	public const string CanSetLogoVariable = "can_set_logo";

	public const string CanSetMotdVariable = "can_set_motd";

	public const string CanSetPlayerNotesVariable = "can_set_player_notes";

	public const string PlayerNoteVariable = "notes";

	public static readonly List<VariableUpdate> DefaultLeaderVariables = new List<VariableUpdate>
	{
		new VariableUpdate("can_set_logo", bool.TrueString),
		new VariableUpdate("can_set_motd", bool.TrueString),
		new VariableUpdate("can_set_player_notes", bool.TrueString)
	};

	private static readonly Memoized<string, ulong> SteamIdToPlayerId = new Memoized<string, ulong>((ulong steamId) => steamId.ToString("G"));

	public static string GetPlayerId(ulong steamId)
	{
		return SteamIdToPlayerId.Get(steamId);
	}

	public static string GetPlayerId(ulong? steamId)
	{
		if (!steamId.HasValue)
		{
			return null;
		}
		return SteamIdToPlayerId.Get(steamId.Value);
	}

	public static ulong GetSteamId(string playerId)
	{
		return ulong.Parse(playerId);
	}

	public static ulong? TryGetSteamId(string playerId)
	{
		if (!ulong.TryParse(playerId, out var result))
		{
			return null;
		}
		return result;
	}

	public static void GetMotd(this NexusClan clan, out string motd, out long motdTimestamp, out ulong motdAuthor)
	{
		if (!clan.TryGetVariable("motd", out var variable) || !clan.TryGetVariable("motd_author", out var variable2) || variable.Type != VariableType.String || variable2.Type != VariableType.String)
		{
			motd = null;
			motdTimestamp = 0L;
			motdAuthor = 0uL;
		}
		else
		{
			motd = variable.GetAsString();
			motdTimestamp = variable.LastUpdated * 1000;
			motdAuthor = GetSteamId(variable2.GetAsString());
		}
	}

	public static void GetBanner(this NexusClan clan, out byte[] logo, out Color32 color)
	{
		logo = ((clan.TryGetVariable("logo", out var variable) && variable.Type == VariableType.Binary) ? variable.GetAsBinary() : null);
		color = ((clan.TryGetVariable("color", out var variable2) && variable2.Type == VariableType.String) ? ColorEx.FromInt32(int.Parse(variable2.GetAsString())) : ((Color32)Color.white));
	}

	public static ClanRole ToClanRole(this NexusClanRole role)
	{
		bool flag = role.Rank == 1;
		Variable variable;
		Variable variable2;
		Variable variable3;
		return new ClanRole
		{
			RoleId = role.RoleId,
			Rank = role.Rank,
			Name = role.Name,
			CanInvite = (flag || role.CanInvite),
			CanKick = (flag || role.CanKick),
			CanPromote = (flag || role.CanPromote),
			CanDemote = (flag || role.CanDemote),
			CanSetLogo = (flag || (role.TryGetVariable("can_set_logo", out variable) && ParseFlag(variable))),
			CanSetMotd = (flag || (role.TryGetVariable("can_set_motd", out variable2) && ParseFlag(variable2))),
			CanSetPlayerNotes = (flag || (role.TryGetVariable("can_set_player_notes", out variable3) && ParseFlag(variable3))),
			CanAccessLogs = (flag || role.CanAccessLogs),
			CanAccessScoreEvents = (flag || role.CanAccessScoreEvents)
		};
	}

	public static ClanMember ToClanMember(this NexusClanMember member)
	{
		member.TryGetVariable("notes", out var variable);
		return new ClanMember
		{
			SteamId = GetSteamId(member.PlayerId),
			RoleId = member.RoleId,
			Joined = member.Joined * 1000,
			LastSeen = member.LastSeen * 1000,
			Notes = variable?.GetAsString(),
			NotesTimestamp = (variable?.LastUpdated ?? 0)
		};
	}

	public static ClanInvite ToClanInvite(this Facepunch.Nexus.Models.ClanInvite invite)
	{
		return new ClanInvite
		{
			SteamId = GetSteamId(invite.PlayerId),
			Recruiter = GetSteamId(invite.RecruiterPlayerId),
			Timestamp = invite.Created * 1000
		};
	}

	public static ClanResult ToClanResult(this NexusClanResultCode result)
	{
		return result switch
		{
			NexusClanResultCode.Fail => ClanResult.Fail, 
			NexusClanResultCode.Success => ClanResult.Success, 
			NexusClanResultCode.NoClan => ClanResult.NoClan, 
			NexusClanResultCode.NotFound => ClanResult.NotFound, 
			NexusClanResultCode.NoPermission => ClanResult.NoPermission, 
			NexusClanResultCode.DuplicateName => ClanResult.DuplicateName, 
			NexusClanResultCode.RoleNotEmpty => ClanResult.RoleNotEmpty, 
			NexusClanResultCode.CannotSwapLeader => ClanResult.CannotSwapLeader, 
			NexusClanResultCode.CannotDeleteLeader => ClanResult.CannotDeleteLeader, 
			NexusClanResultCode.CannotKickLeader => ClanResult.CannotKickLeader, 
			NexusClanResultCode.CannotDemoteLeader => ClanResult.CannotDemoteLeader, 
			NexusClanResultCode.AlreadyInAClan => ClanResult.AlreadyInAClan, 
			_ => throw new NotSupportedException($"Cannot map NexusClanResultCode {result} to ClanResult"), 
		};
	}

	public static ClanRoleParameters ToRoleParameters(this ClanRole role)
	{
		return new ClanRoleParameters
		{
			Name = role.Name,
			CanInvite = role.CanInvite,
			CanKick = role.CanKick,
			CanPromote = role.CanPromote,
			CanDemote = role.CanDemote,
			CanAccessLogs = role.CanAccessLogs,
			CanAccessScoreEvents = role.CanAccessScoreEvents,
			Variables = new List<VariableUpdate>(3)
			{
				FlagVariable("can_set_logo", role.CanSetLogo),
				FlagVariable("can_set_motd", role.CanSetMotd),
				FlagVariable("can_set_player_notes", role.CanSetPlayerNotes)
			}
		};
	}

	public static VariableUpdate FlagVariable(string key, bool value)
	{
		return new VariableUpdate(key, value ? bool.TrueString : bool.FalseString);
	}

	private static bool ParseFlag(Variable variable, bool defaultValue = false)
	{
		if ((object)variable == null || variable.Type != VariableType.String || !bool.TryParse(variable.GetAsString(), out var result))
		{
			return false;
		}
		return result;
	}
}
