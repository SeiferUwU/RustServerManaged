#define UNITY_ASSERTIONS
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConVar;
using Development.Attributes;
using Facepunch;
using Facepunch.Rust;
using Network;
using Oxide.Core;
using ProtoBuf;
using Rust;
using Rust.Safety;
using UnityEngine;
using UnityEngine.Assertions;

[ResetStaticFields]
public class SleepingBag : DecayEntity
{
	public enum BagAssignMode
	{
		Allowed = 0,
		TeamAndFriendlyContacts = 1,
		None = 2,
		LAST = 2
	}

	public enum BagResultType
	{
		Ok,
		TooManyBags,
		BagBlocked,
		TargetIsPlayingTutorial
	}

	public struct CanAssignBedResult
	{
		public BagResultType Result;

		public int Count;

		public int Max;
	}

	public enum SleepingBagResetReason
	{
		Respawned,
		Placed,
		Death
	}

	[ReplicatedVar]
	public static bool UseTeamLabels = true;

	[NonSerialized]
	public ulong deployerUserID;

	public GameObject renameDialog;

	public GameObject assignDialog;

	public float secondsBetweenReuses = 300f;

	public bool perPlayerRespawnCooldown;

	private Dictionary<ulong, float> playerCooldowns = new Dictionary<ulong, float>();

	public string niceName = "Unnamed Bag";

	public Vector3 spawnOffset = Vector3.zero;

	public RespawnInformation.SpawnOptions.RespawnType RespawnType = RespawnInformation.SpawnOptions.RespawnType.SleepingBag;

	public bool isStatic;

	public bool canBePublic;

	public bool canReassignToFriends = true;

	public const Flags IsPublicFlag = Flags.Reserved3;

	public const Flags DestroyAfterUseFlag = Flags.Reserved14;

	public static Translate.Phrase bagLimitPhrase = new Translate.Phrase("bag_limit_update", "You are now at {0}/{1} bags");

	public static Translate.Phrase bagLimitReachedPhrase = new Translate.Phrase("bag_limit_reached", "You have reached your bag limit!");

	public static Translate.Phrase teammateBagPhrase = new Translate.Phrase("teammate_bag", "{0}'s bag");

	public Translate.Phrase assignOtherBagPhrase = new Translate.Phrase("assigned_other_bag_limit", "You have assigned {0} a bag, they are now at {0}/{1} bags");

	public Translate.Phrase assignedBagPhrase = new Translate.Phrase("assigned_bag_limit", "You have been assigned a bag, you are now at {0}/{1} bags");

	public Translate.Phrase cannotAssignBedNoPlayerPhrase = new Translate.Phrase("cannot_assign_bag_limit_noplayer", "You cannot assign a bag to this player, they have reached their bag limit!");

	public Translate.Phrase cannotAssignBedPhrase = new Translate.Phrase("cannot_assign_bag_limit", "You cannot assign {0} a bag, they have reached their bag limit!");

	public Translate.Phrase cannotMakeBedPhrase = new Translate.Phrase("cannot_make_bed_limit", "You cannot take ownership of the bed, you are at your bag limit");

	public Translate.Phrase bedAssigningBlocked = new Translate.Phrase("bag_assign_blocked", "That player has blocked bag assignment");

	public static Translate.Phrase tutorialPhrase = new Translate.Phrase("bag_assign_tutorial", "Cannot assign bags to players mid-tutorial");

	public float unlockTime;

	public static List<SleepingBag> sleepingBags = new List<SleepingBag>();

	private bool notifyPlayerOnServerInit;

	private static Dictionary<ulong, List<SleepingBag>> bagsPerPlayer = new Dictionary<ulong, List<SleepingBag>>();

	public virtual float unlockSeconds
	{
		get
		{
			if (unlockTime < UnityEngine.Time.realtimeSinceStartup)
			{
				return 0f;
			}
			return unlockTime - UnityEngine.Time.realtimeSinceStartup;
		}
	}

	public bool IsTutorialBag
	{
		get
		{
			if (net != null && net.group != null)
			{
				return net.group.restricted;
			}
			return false;
		}
	}

	public override bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		using (TimeWarning.New("SleepingBag.OnRpcMessage"))
		{
			if (rpc == 3057055788u && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - AssignToFriend ");
				}
				using (TimeWarning.New("AssignToFriend"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(3057055788u, "AssignToFriend", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg2 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							AssignToFriend(msg2);
						}
					}
					catch (Exception exception)
					{
						Debug.LogException(exception);
						player.Kick("RPC Error in AssignToFriend");
					}
				}
				return true;
			}
			if (rpc == 1335950295 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - Rename ");
				}
				using (TimeWarning.New("Rename"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(1335950295u, "Rename", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg3 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							Rename(msg3);
						}
					}
					catch (Exception exception2)
					{
						Debug.LogException(exception2);
						player.Kick("RPC Error in Rename");
					}
				}
				return true;
			}
			if (rpc == 42669546 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - RPC_MakeBed ");
				}
				using (TimeWarning.New("RPC_MakeBed"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(42669546u, "RPC_MakeBed", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg4 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							RPC_MakeBed(msg4);
						}
					}
					catch (Exception exception3)
					{
						Debug.LogException(exception3);
						player.Kick("RPC Error in RPC_MakeBed");
					}
				}
				return true;
			}
			if (rpc == 393812086 && player != null)
			{
				Assert.IsTrue(player.isServer, "SV_RPC Message is using a clientside player!");
				if (ConVar.Global.developer > 2)
				{
					Debug.Log("SV_RPCMessage: " + player?.ToString() + " - RPC_MakePublic ");
				}
				using (TimeWarning.New("RPC_MakePublic"))
				{
					using (TimeWarning.New("Conditions"))
					{
						if (!RPC_Server.IsVisible.Test(393812086u, "RPC_MakePublic", this, player, 3f))
						{
							return true;
						}
					}
					try
					{
						using (TimeWarning.New("Call"))
						{
							RPCMessage msg5 = new RPCMessage
							{
								connection = msg.connection,
								player = player,
								read = msg.read
							};
							RPC_MakePublic(msg5);
						}
					}
					catch (Exception exception4)
					{
						Debug.LogException(exception4);
						player.Kick("RPC Error in RPC_MakePublic");
					}
				}
				return true;
			}
		}
		return base.OnRpcMessage(player, rpc, msg);
	}

	public bool IsPublic()
	{
		return HasFlag(Flags.Reserved3);
	}

	private float EvaluatedSecondsBetweenReuses()
	{
		float num = 0f;
		switch (RespawnType)
		{
		case RespawnInformation.SpawnOptions.RespawnType.SleepingBag:
		case RespawnInformation.SpawnOptions.RespawnType.BeachTowel:
			num = ConVar.Server.respawnTimeAdditionBag;
			break;
		case RespawnInformation.SpawnOptions.RespawnType.Bed:
		case RespawnInformation.SpawnOptions.RespawnType.Camper:
			num = ConVar.Server.respawnTimeAdditionBed;
			break;
		}
		return secondsBetweenReuses + num;
	}

	public virtual float GetUnlockSeconds(ulong playerID)
	{
		if (playerCooldowns.TryGetValue(playerID, out var value) && value > unlockTime)
		{
			return Mathf.Max(0f, value - UnityEngine.Time.realtimeSinceStartup);
		}
		return unlockSeconds;
	}

	public virtual bool ValidForPlayer(ulong playerID, bool ignoreTimers)
	{
		object obj = Interface.CallHook("OnSleepingBagValidCheck", this, playerID, ignoreTimers);
		if (obj is bool)
		{
			return (bool)obj;
		}
		if (deployerUserID == playerID)
		{
			if (!ignoreTimers)
			{
				return unlockTime < UnityEngine.Time.realtimeSinceStartup;
			}
			return true;
		}
		return false;
	}

	public static CanAssignBedResult? CanAssignBed(BasePlayer player, SleepingBag newBag, ulong targetPlayer, int countOffset = 1, int maxOffset = 0, SleepingBag ignore = null)
	{
		int num = ConVar.Server.max_sleeping_bags + maxOffset;
		if (player.IsInTutorial)
		{
			return null;
		}
		if (num < 0)
		{
			return null;
		}
		int num2 = countOffset;
		BasePlayer basePlayer = BasePlayer.FindByID(targetPlayer);
		BagAssignMode bagAssignMode = (BagAssignMode)Mathf.Clamp((basePlayer != null) ? basePlayer.GetInfoInt("client.bagassignmode", 0) : 0, 0, 2);
		int max = num;
		if (player != basePlayer)
		{
			switch (bagAssignMode)
			{
			case BagAssignMode.Allowed:
				if (ConVar.Server.fogofwar || !ConVar.Server.mapenabled)
				{
					num--;
				}
				break;
			case BagAssignMode.None:
				return new CanAssignBedResult
				{
					Result = BagResultType.BagBlocked
				};
			case BagAssignMode.TeamAndFriendlyContacts:
			{
				if (!(basePlayer != null))
				{
					break;
				}
				bool flag = false;
				if (basePlayer.Team != null && basePlayer.Team.members.Contains(player.userID))
				{
					flag = true;
				}
				else
				{
					RelationshipManager.PlayerRelationshipInfo relations = RelationshipManager.ServerInstance.GetRelationships(targetPlayer).GetRelations(player.userID);
					if (relations != null && relations.type == RelationshipManager.RelationshipType.Friend)
					{
						flag = true;
					}
					if (!flag && ClanManager.ServerInstance != null && basePlayer.clanId != 0L && basePlayer.clanId == player.clanId)
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return new CanAssignBedResult
					{
						Result = BagResultType.BagBlocked
					};
				}
				break;
			}
			}
			if (basePlayer != player && basePlayer != null && basePlayer.IsInTutorial)
			{
				return new CanAssignBedResult
				{
					Result = BagResultType.TargetIsPlayingTutorial
				};
			}
		}
		foreach (SleepingBag sleepingBag in sleepingBags)
		{
			if (sleepingBag != ignore && sleepingBag.deployerUserID == targetPlayer)
			{
				num2++;
				if (num2 > num)
				{
					return new CanAssignBedResult
					{
						Count = num2,
						Max = max,
						Result = BagResultType.TooManyBags
					};
				}
			}
		}
		return new CanAssignBedResult
		{
			Count = num2,
			Max = max,
			Result = BagResultType.Ok
		};
	}

	public static Planner.CanBuildResult? CanBuildBed(BasePlayer player, Construction construction)
	{
		if (GameManager.server.FindPrefab(construction.prefabID)?.GetComponent<BaseEntity>() is SleepingBag)
		{
			CanAssignBedResult? canAssignBedResult = CanAssignBed(player, null, player.userID);
			if (canAssignBedResult.HasValue)
			{
				if (canAssignBedResult.Value.Result == BagResultType.Ok)
				{
					return new Planner.CanBuildResult
					{
						Result = true,
						Phrase = bagLimitPhrase,
						Arguments = new string[2]
						{
							canAssignBedResult.Value.Count.ToString(),
							canAssignBedResult.Value.Max.ToString()
						}
					};
				}
				return new Planner.CanBuildResult
				{
					Result = false,
					Phrase = bagLimitReachedPhrase
				};
			}
		}
		return null;
	}

	public static PooledList<SleepingBag> FindForPlayer(ulong playerID, bool ignoreTimers = true)
	{
		PooledList<SleepingBag> pooledList = Facepunch.Pool.Get<PooledList<SleepingBag>>();
		if (bagsPerPlayer.TryGetValue(playerID, out var value))
		{
			if (!ignoreTimers)
			{
				foreach (SleepingBag item in value)
				{
					if (item.ValidForPlayer(playerID, ignoreTimers))
					{
						pooledList.Add(item);
					}
				}
			}
			else
			{
				foreach (SleepingBag item2 in value)
				{
					pooledList.Add(item2);
				}
			}
		}
		if (!ignoreTimers)
		{
			foreach (StaticRespawnArea staticRespawnArea in StaticRespawnArea.staticRespawnAreas)
			{
				if (staticRespawnArea.ValidForPlayer(playerID, ignoreTimers))
				{
					pooledList.Add(staticRespawnArea);
				}
			}
		}
		else
		{
			foreach (StaticRespawnArea staticRespawnArea2 in StaticRespawnArea.staticRespawnAreas)
			{
				pooledList.Add(staticRespawnArea2);
			}
		}
		return pooledList;
	}

	public static void FindForPlayer(ulong playerID, bool ignoreTimers, List<SleepingBag> result)
	{
		foreach (SleepingBag sleepingBag in sleepingBags)
		{
			if (sleepingBag.ValidForPlayer(playerID, ignoreTimers))
			{
				result.Add(sleepingBag);
			}
		}
	}

	public static void UpdateMyBags(ulong id)
	{
		if (!bagsPerPlayer.TryGetValue(id, out var value))
		{
			return;
		}
		foreach (SleepingBag item in value)
		{
			if (!(item == null))
			{
				item.SendNetworkUpdate();
			}
		}
	}

	public static void UpdateTeamsBags(List<ulong> ids)
	{
		foreach (ulong id in ids)
		{
			UpdateMyBags(id);
		}
	}

	public static bool SpawnPlayer(BasePlayer player, NetworkableId sleepingBag)
	{
		using PooledList<SleepingBag> pooledList = FindForPlayer(player.userID);
		SleepingBag sleepingBag2 = null;
		foreach (SleepingBag item in pooledList)
		{
			if (item.ValidForPlayer(player.userID, ignoreTimers: false) && item.net.ID == sleepingBag && item.unlockTime < UnityEngine.Time.realtimeSinceStartup)
			{
				sleepingBag2 = item;
				break;
			}
		}
		if (sleepingBag2 == null)
		{
			return false;
		}
		object obj = Interface.CallHook("OnPlayerRespawn", player, sleepingBag2);
		if (obj is SleepingBag)
		{
			sleepingBag2 = (SleepingBag)obj;
		}
		if (sleepingBag2 is StaticRespawnArea staticRespawnArea && !staticRespawnArea.IsAuthed(player.userID))
		{
			return false;
		}
		if (sleepingBag2.GetRespawnState(player.userID) != RespawnInformation.SpawnOptions.RespawnState.OK)
		{
			return false;
		}
		sleepingBag2.GetSpawnPos(out var pos, out var rot);
		player.RespawnAt(pos, rot, sleepingBag2);
		sleepingBag2.PostPlayerSpawn(player);
		foreach (SleepingBag item2 in pooledList)
		{
			SetBagTimer(item2, pos, SleepingBagResetReason.Respawned, player);
		}
		if (sleepingBag2.HasFlag(Flags.Reserved14))
		{
			sleepingBag2.Kill();
		}
		return true;
	}

	public static void AddBagForPlayer(SleepingBag bag, ulong user, bool networkUpdate = true)
	{
		if (user == 0L)
		{
			return;
		}
		if (!bagsPerPlayer.TryGetValue(user, out var value))
		{
			value = new List<SleepingBag>();
			bagsPerPlayer[user] = value;
		}
		if (!value.Contains(bag))
		{
			value.Add(bag);
			if (networkUpdate)
			{
				RelationshipManager.FindByID(user)?.SendNetworkUpdate();
			}
		}
	}

	public static void RemoveBagForPlayer(SleepingBag bag, ulong user)
	{
		if (user != 0L && bagsPerPlayer.TryGetValue(user, out var value))
		{
			if (value.Remove(bag))
			{
				RelationshipManager.FindByID(user)?.SendNetworkUpdate();
			}
			if (value.Count == 0)
			{
				bagsPerPlayer.Remove(user);
			}
		}
	}

	public static void OnBagChangedOwnership(SleepingBag bag, ulong oldUser)
	{
		if (bag.deployerUserID != oldUser)
		{
			RemoveBagForPlayer(bag, oldUser);
			AddBagForPlayer(bag, bag.deployerUserID);
		}
	}

	public static void ClearTutorialBagsForPlayer(ulong userId)
	{
		if (userId == 0L || !bagsPerPlayer.TryGetValue(userId, out var _))
		{
			return;
		}
		List<SleepingBag> obj = Facepunch.Pool.Get<List<SleepingBag>>();
		foreach (SleepingBag item in bagsPerPlayer[userId])
		{
			if (item.net != null && item.net.group != null && item.net.group.restricted)
			{
				obj.Add(item);
			}
		}
		foreach (SleepingBag item2 in obj)
		{
			item2.deployerUserID = 0uL;
			RemoveBagForPlayer(item2, userId);
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	public static int GetSleepingBagCount(ulong userId)
	{
		if (userId == 0L)
		{
			return 0;
		}
		if (!bagsPerPlayer.TryGetValue(userId, out var value))
		{
			return 0;
		}
		return value.Count;
	}

	public static bool TrySpawnPlayer(BasePlayer player, NetworkableId sleepingBag, out string errorMessage)
	{
		if (!player.IsDead())
		{
			errorMessage = "Couldn't spawn - player is not dead!";
			return false;
		}
		if (player.CanRespawn())
		{
			if (SpawnPlayer(player, sleepingBag))
			{
				player.MarkRespawn();
				errorMessage = null;
				return true;
			}
			errorMessage = "Couldn't spawn in sleeping bag!";
			return false;
		}
		errorMessage = "You can't respawn again so quickly, wait a while";
		return false;
	}

	public virtual void SetUnlockTime(float newTime)
	{
		unlockTime = newTime;
	}

	public void SetUnlockTimeForPlayer(ulong player, float time)
	{
		playerCooldowns.TryGetValue(player, out var value);
		playerCooldowns[player] = Mathf.Max(value, time);
	}

	public void ResetUnlockTimeForPlayer(ulong player)
	{
		playerCooldowns.Remove(player);
	}

	public static bool DestroyBag(ulong userID, NetworkableId sleepingBag)
	{
		using PooledList<SleepingBag> pooledList = FindForPlayer(userID);
		SleepingBag sleepingBag2 = null;
		foreach (SleepingBag item in pooledList)
		{
			if (item.net.ID == sleepingBag)
			{
				sleepingBag2 = item;
				break;
			}
		}
		if (sleepingBag2 == null)
		{
			return false;
		}
		if (Interface.CallHook("OnSleepingBagDestroy", sleepingBag2, userID) != null)
		{
			return false;
		}
		RemoveBagForPlayer(sleepingBag2, sleepingBag2.deployerUserID);
		sleepingBag2.deployerUserID = 0uL;
		if (sleepingBag2.HasFlag(Flags.Reserved14))
		{
			sleepingBag2.Kill();
		}
		else
		{
			sleepingBag2.SendNetworkUpdate();
		}
		BasePlayer basePlayer = BasePlayer.FindByID(userID);
		if (basePlayer != null)
		{
			basePlayer.SendRespawnOptions();
			Interface.CallHook("OnSleepingBagDestroyed", sleepingBag2, userID);
			Facepunch.Rust.Analytics.Azure.OnBagUnclaimed(basePlayer, sleepingBag2);
		}
		return true;
	}

	public static void ResetTimersForPlayer(BasePlayer player)
	{
		using PooledList<SleepingBag> pooledList = FindForPlayer(player.userID);
		foreach (SleepingBag item in pooledList)
		{
			item.unlockTime = 0f;
			item.ResetUnlockTimeForPlayer(player.userID);
		}
	}

	public virtual void GetSpawnPos(out Vector3 pos, out Quaternion rot)
	{
		pos = base.transform.position + spawnOffset;
		rot = Quaternion.Euler(0f, base.transform.rotation.eulerAngles.y, 0f);
	}

	public void SetPublic(bool isPublic)
	{
		SetFlag(Flags.Reserved3, isPublic);
	}

	public void SetDeployedBy(BasePlayer player)
	{
		if (!(player == null) && !base.isClient)
		{
			deployerUserID = player.userID;
			SetBagTimer(this, base.transform.position, SleepingBagResetReason.Placed, player);
			SendNetworkUpdate();
			notifyPlayerOnServerInit = true;
		}
	}

	public static void OnPlayerDeath(BasePlayer player)
	{
		using PooledList<SleepingBag> pooledList = FindForPlayer(player.userID);
		foreach (SleepingBag item in pooledList)
		{
			SetBagTimer(item, player.transform.position, SleepingBagResetReason.Death, player);
		}
	}

	public static void SetBagTimer(SleepingBag bag, Vector3 position, SleepingBagResetReason reason, BasePlayer forPlayer)
	{
		BaseGameMode activeGameMode = BaseGameMode.GetActiveGameMode(serverside: true);
		float? num = null;
		if (activeGameMode != null)
		{
			num = activeGameMode.EvaluateSleepingBagReset(bag, position, reason);
		}
		if (num.HasValue)
		{
			bag.SetUnlockTime(UnityEngine.Time.realtimeSinceStartup + num.Value);
			return;
		}
		if (reason == SleepingBagResetReason.Respawned && Vector3.Distance(position, bag.transform.position) <= ConVar.Server.respawnresetrange)
		{
			if (bag.perPlayerRespawnCooldown)
			{
				bag.SetUnlockTimeForPlayer(forPlayer.userID, UnityEngine.Time.realtimeSinceStartup + bag.EvaluatedSecondsBetweenReuses());
			}
			else
			{
				bag.SetUnlockTime(UnityEngine.Time.realtimeSinceStartup + bag.EvaluatedSecondsBetweenReuses());
			}
			bag.SendNetworkUpdate();
		}
		if (reason != SleepingBagResetReason.Placed)
		{
			return;
		}
		float realtimeSinceStartup = UnityEngine.Time.realtimeSinceStartup;
		SleepingBag[] array = sleepingBags.Where((SleepingBag x) => x.deployerUserID != 0L && x.deployerUserID == bag.deployerUserID && x.unlockTime > UnityEngine.Time.realtimeSinceStartup).ToArray();
		foreach (SleepingBag sleepingBag in array)
		{
			if (bag.unlockTime > realtimeSinceStartup && Vector3.Distance(sleepingBag.transform.position, position) <= ConVar.Server.respawnresetrange)
			{
				realtimeSinceStartup = bag.unlockTime;
			}
		}
		float num3 = Mathf.Max(realtimeSinceStartup, UnityEngine.Time.realtimeSinceStartup + bag.EvaluatedSecondsBetweenReuses());
		if (forPlayer != null && forPlayer.IsInTutorial)
		{
			num3 = 0f;
		}
		bag.SetUnlockTime(num3);
		bag.SendNetworkUpdate();
	}

	public override void ServerInit()
	{
		base.ServerInit();
		if (!sleepingBags.Contains(this))
		{
			sleepingBags.Add(this);
			if (deployerUserID != 0L)
			{
				AddBagForPlayer(this, deployerUserID, !Rust.Application.isLoadingSave);
			}
		}
		if (notifyPlayerOnServerInit)
		{
			notifyPlayerOnServerInit = false;
			NotifyPlayer(deployerUserID);
		}
	}

	public override void OnPlaced(BasePlayer player)
	{
		SetDeployedBy(player);
	}

	public override void OnParentChanging(BaseEntity oldParent, BaseEntity newParent)
	{
		base.OnParentChanging(oldParent, newParent);
		Invoke(DelayedPlayerNotify, 0.1f);
	}

	private void DelayedPlayerNotify()
	{
		NotifyPlayer(deployerUserID);
	}

	public override void PostServerLoad()
	{
		base.PostServerLoad();
		AddBagForPlayer(this, deployerUserID, !Rust.Application.isLoadingSave);
	}

	private void NotifyPlayer(ulong id)
	{
		BasePlayer basePlayer = BasePlayer.FindByID(id);
		if (basePlayer != null && basePlayer.IsConnected)
		{
			basePlayer.SendRespawnOptions();
		}
	}

	public override void DoServerDestroy()
	{
		base.DoServerDestroy();
		sleepingBags.RemoveAll((SleepingBag x) => x == this);
		RemoveBagForPlayer(this, deployerUserID);
		NotifyPlayer(deployerUserID);
	}

	public override void Save(SaveInfo info)
	{
		base.Save(info);
		using (TimeWarning.New("SleepingBag.Save"))
		{
			info.msg.sleepingBag = Facepunch.Pool.Get<ProtoBuf.SleepingBag>();
			info.msg.sleepingBag.name = niceName;
			if (info.forDisk)
			{
				info.msg.sleepingBag.deployerID = deployerUserID;
			}
			else
			{
				info.msg.sleepingBag.clientAssigned = deployerUserID == info.forConnection.userid;
				info.msg.sleepingBag.isAssigned = deployerUserID != 0;
			}
			if (!UseTeamLabels)
			{
				return;
			}
			if (RelationshipManager.ServerInstance != null && info.forConnection != null)
			{
				try
				{
					RelationshipManager.PlayerTeam playerTeam = RelationshipManager.ServerInstance.FindPlayersTeam(deployerUserID);
					BasePlayer basePlayer = BasePlayer.FindAwakeOrSleepingByID(deployerUserID);
					if (playerTeam != null || (basePlayer != null && info.forConnection == basePlayer.Connection))
					{
						if (info.forConnection.userid != 0L && basePlayer != null)
						{
							string displayName = basePlayer.displayName;
							if (deployerUserID == info.forConnection.userid)
							{
								info.msg.sleepingBag.teamMemberName = displayName;
							}
							else
							{
								List<ulong> members = playerTeam.members;
								if (members.Count > 0 && members.Contains(info.forConnection.userid))
								{
									info.msg.sleepingBag.teamMemberName = displayName;
								}
								else
								{
									info.msg.sleepingBag.teamMemberName = "";
								}
							}
						}
					}
					else
					{
						info.msg.sleepingBag.teamMemberName = "";
					}
					return;
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					return;
				}
			}
			info.msg.sleepingBag.teamMemberName = "";
		}
	}

	public override bool CanUseNetworkCache(Connection connection)
	{
		return false;
	}

	public override bool ShouldUseCastNoClipChecks()
	{
		return true;
	}

	[RPC_Server.IsVisible(3f)]
	[RPC_Server]
	public void Rename(RPCMessage msg)
	{
		if (!msg.player.CanInteract())
		{
			return;
		}
		string text = msg.read.String();
		if (Interface.CallHook("CanRenameBed", msg.player, this, text) == null)
		{
			text = WordFilter.Filter(text);
			if (string.IsNullOrEmpty(text))
			{
				text = "Unnamed Sleeping Bag";
			}
			if (text.Length > 24)
			{
				text = text.Substring(0, 22) + "..";
			}
			niceName = text;
			SendNetworkUpdate();
			NotifyPlayer(deployerUserID);
		}
	}

	[RPC_Server]
	[RPC_Server.IsVisible(3f)]
	public void AssignToFriend(RPCMessage msg)
	{
		if (!msg.player.CanInteract() || deployerUserID != (ulong)msg.player.userID || !canReassignToFriends)
		{
			return;
		}
		ulong num = msg.read.UInt64();
		if (num == 0L || Interface.CallHook("CanAssignBed", msg.player, this, num) != null)
		{
			return;
		}
		if (ConVar.Server.max_sleeping_bags > 0)
		{
			CanAssignBedResult? canAssignBedResult = CanAssignBed(msg.player, this, num);
			if (canAssignBedResult.HasValue)
			{
				BasePlayer basePlayer = RelationshipManager.FindByID(num);
				if (canAssignBedResult.Value.Result == BagResultType.TooManyBags)
				{
					if (basePlayer == null)
					{
						msg.player.ShowToast(GameTip.Styles.Error, cannotAssignBedNoPlayerPhrase, false);
					}
					else
					{
						string playerNameStreamSafe = NameHelper.GetPlayerNameStreamSafe(msg.player, basePlayer);
						msg.player.ShowToast(GameTip.Styles.Error, cannotAssignBedPhrase, false, playerNameStreamSafe);
					}
				}
				else if (canAssignBedResult.Value.Result == BagResultType.BagBlocked)
				{
					msg.player.ShowToast(GameTip.Styles.Error, bedAssigningBlocked, false);
				}
				else if (canAssignBedResult.Value.Result == BagResultType.TargetIsPlayingTutorial)
				{
					msg.player.ShowToast(GameTip.Styles.Error, tutorialPhrase, false);
				}
				else
				{
					basePlayer?.ShowToast(GameTip.Styles.Blue_Long, assignedBagPhrase, false, canAssignBedResult.Value.Count.ToString(), canAssignBedResult.Value.Max.ToString());
					msg.player.ShowToast(GameTip.Styles.Blue_Long, bagLimitPhrase, false, (GetSleepingBagCount(msg.player.userID) - 1).ToString(), canAssignBedResult.Value.Max.ToString());
					SendNetworkUpdate();
				}
				if (canAssignBedResult.Value.Result != BagResultType.Ok)
				{
					return;
				}
			}
		}
		ulong num2 = deployerUserID;
		deployerUserID = num;
		OnBagChangedOwnership(this, num2);
		NotifyPlayer(num2);
		NotifyPlayer(deployerUserID);
		Facepunch.Rust.Analytics.Azure.OnSleepingBagAssigned(msg.player, this, num);
		SendNetworkUpdate();
	}

	[ServerVar]
	public static void AssignToPlayer(ConsoleSystem.Arg arg)
	{
		BasePlayer basePlayer = ArgEx.Player(arg);
		NetworkableId entityID = ArgEx.GetEntityID(arg, 0);
		SleepingBag obj = BaseNetworkable.serverEntities.Find(entityID) as SleepingBag;
		ulong num = obj.deployerUserID;
		obj.deployerUserID = basePlayer.userID.Get();
		OnBagChangedOwnership(obj, num);
		obj.NotifyPlayer(num);
		obj.NotifyPlayer(obj.deployerUserID);
		obj.SendNetworkUpdate();
	}

	[ServerVar]
	public static void ClearFromPlayer(ConsoleSystem.Arg arg)
	{
		NetworkableId entityID = ArgEx.GetEntityID(arg, 0);
		SleepingBag obj = BaseNetworkable.serverEntities.Find(entityID) as SleepingBag;
		ulong num = obj.deployerUserID;
		obj.deployerUserID = 0uL;
		RemoveBagForPlayer(obj, num);
		obj.NotifyPlayer(num);
		obj.SendNetworkUpdate();
	}

	[RPC_Server]
	[RPC_Server.IsVisible(3f)]
	public virtual void RPC_MakePublic(RPCMessage msg)
	{
		if (!canBePublic || !msg.player.CanInteract() || (deployerUserID != (ulong)msg.player.userID && !msg.player.CanBuild()))
		{
			return;
		}
		bool flag = msg.read.Bit();
		if (flag == IsPublic() || Interface.CallHook("CanSetBedPublic", msg.player, this) != null)
		{
			return;
		}
		SetPublic(flag);
		if (!IsPublic())
		{
			if (ConVar.Server.max_sleeping_bags > 0)
			{
				CanAssignBedResult? canAssignBedResult = CanAssignBed(msg.player, this, msg.player.userID, 1, 0, this);
				if (canAssignBedResult.HasValue)
				{
					if (canAssignBedResult.Value.Result == BagResultType.Ok)
					{
						msg.player.ShowToast(GameTip.Styles.Blue_Long, bagLimitPhrase, false, canAssignBedResult.Value.Count.ToString(), canAssignBedResult.Value.Max.ToString());
					}
					else
					{
						msg.player.ShowToast(GameTip.Styles.Blue_Long, cannotMakeBedPhrase, false, canAssignBedResult.Value.Count.ToString(), canAssignBedResult.Value.Max.ToString());
					}
					if (canAssignBedResult.Value.Result != BagResultType.Ok)
					{
						return;
					}
				}
			}
			ulong num = deployerUserID;
			deployerUserID = msg.player.userID;
			OnBagChangedOwnership(this, num);
			NotifyPlayer(num);
			NotifyPlayer(deployerUserID);
			Facepunch.Rust.Analytics.Azure.OnSleepingBagAssigned(msg.player, this, deployerUserID = msg.player.userID);
		}
		else
		{
			Facepunch.Rust.Analytics.Azure.OnSleepingBagAssigned(msg.player, this, 0uL);
		}
		SendNetworkUpdate();
	}

	[RPC_Server]
	[RPC_Server.IsVisible(3f)]
	public void RPC_MakeBed(RPCMessage msg)
	{
		if (!canBePublic || !IsPublic() || !msg.player.CanInteract())
		{
			return;
		}
		if (ConVar.Server.max_sleeping_bags > 0)
		{
			CanAssignBedResult? canAssignBedResult = CanAssignBed(msg.player, this, msg.player.userID, 1, 0, this);
			if (canAssignBedResult.HasValue)
			{
				if (canAssignBedResult.Value.Result != BagResultType.Ok)
				{
					msg.player.ShowToast(GameTip.Styles.Red_Normal, cannotMakeBedPhrase, false);
				}
				else
				{
					msg.player.ShowToast(GameTip.Styles.Blue_Long, bagLimitPhrase, false, canAssignBedResult.Value.Count.ToString(), canAssignBedResult.Value.Max.ToString());
				}
				if (canAssignBedResult.Value.Result != BagResultType.Ok)
				{
					return;
				}
			}
		}
		ulong num = deployerUserID;
		deployerUserID = msg.player.userID;
		OnBagChangedOwnership(this, num);
		NotifyPlayer(num);
		NotifyPlayer(deployerUserID);
		Interface.CallHook("OnBedMade", this, msg.player);
		SendNetworkUpdate();
	}

	protected virtual void PostPlayerSpawn(BasePlayer p)
	{
		p.SendRespawnOptions();
	}

	public virtual RespawnInformation.SpawnOptions.RespawnState GetRespawnState(ulong userID)
	{
		if (WaterLevel.Test(base.transform.position, waves: true, volumes: false))
		{
			return RespawnInformation.SpawnOptions.RespawnState.Underwater;
		}
		if (TriggerNoRespawnZone.InAnyNoRespawnZone(base.transform.position))
		{
			return RespawnInformation.SpawnOptions.RespawnState.InNoRespawnZone;
		}
		return RespawnInformation.SpawnOptions.RespawnState.OK;
	}

	public virtual bool IsMobile()
	{
		BaseEntity baseEntity = GetParentEntity();
		if (baseEntity != null && baseEntity is BaseVehicle)
		{
			return true;
		}
		return RespawnType == RespawnInformation.SpawnOptions.RespawnType.Camper;
	}

	public override string Admin_Who()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine(base.Admin_Who());
		stringBuilder.AppendLine($"Assigned bag ID: {deployerUserID}");
		stringBuilder.AppendLine("Assigned player name: " + Admin.GetPlayerName(deployerUserID));
		stringBuilder.AppendLine("Bag Name:" + niceName);
		return stringBuilder.ToString();
	}

	public override void OnDeployableCorpseSpawned(BaseEntity corpse)
	{
		base.OnDeployableCorpseSpawned(corpse);
		if (corpse is SleepingBag sleepingBag)
		{
			sleepingBag.deployerUserID = deployerUserID;
			sleepingBag.niceName = niceName;
			sleepingBag.SetFlag(Flags.Reserved14, b: true);
			AddBagForPlayer(sleepingBag, deployerUserID);
		}
	}

	public override bool ShouldDropDeployableCorpse(HitInfo info)
	{
		if (!base.ShouldDropDeployableCorpse(info))
		{
			return false;
		}
		if (HasFlag(Flags.Reserved14))
		{
			return false;
		}
		return true;
	}

	public override void Load(LoadInfo info)
	{
		base.Load(info);
		if (info.msg.sleepingBag != null)
		{
			niceName = info.msg.sleepingBag.name;
			if (base.isServer)
			{
				deployerUserID = info.msg.sleepingBag.deployerID;
			}
		}
	}

	protected override bool ShouldDisplayPickupOption(BasePlayer player)
	{
		if (base.ShouldDisplayPickupOption(player) && Check.IsAuthorisedToBuild(player))
		{
			return true;
		}
		if (base.ShouldDisplayPickupOption(player))
		{
			return (ulong)player.userID == deployerUserID;
		}
		return false;
	}
}
