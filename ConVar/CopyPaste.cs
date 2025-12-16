using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Facepunch;
using Network;
using ProtoBuf;
using Rust;
using Rust.Ai.Gen2;
using UnityEngine;

namespace ConVar;

[Factory("copypaste")]
public class CopyPaste : ConsoleSystem
{
	private class EntityWrapper
	{
		public BaseEntity Entity;

		public ProtoBuf.Entity Protobuf;

		public Vector3 Position;

		public Quaternion Rotation;

		public bool HasParent;
	}

	public class PasteOptions
	{
		public const string Argument_NPCs = "--npcs";

		public const string Argument_Resources = "--resources";

		public const string Argument_Vehicles = "--vehicles";

		public const string Argument_Deployables = "--deployables";

		public const string Argument_FoundationsOnly = "--foundations-only";

		public const string Argument_BuildingBlocksOnly = "--building-only";

		public const string Argument_SnapToTerrain = "--autosnap-terrain";

		public const string Argument_SnapToZeroHeight = "--autosnap-zero";

		public const string Argument_PastePlayers = "--players";

		public bool Resources;

		public bool NPCs;

		public bool Vehicles;

		public bool Deployables;

		public bool FoundationsOnly;

		public bool BuildingBlocksOnly;

		public bool SnapToTerrain;

		public bool SnapToZero;

		public bool Players;

		public Vector3 Origin;

		public Quaternion PlayerRotation;

		public Vector3 HeightOffset;

		public PasteOptions(Arg arg)
		{
			Resources = arg.HasArg("--resources", remove: true);
			NPCs = arg.HasArg("--npcs", remove: true);
			Vehicles = arg.HasArg("--vehicles", remove: true);
			Deployables = arg.HasArg("--deployables", remove: true);
			FoundationsOnly = arg.HasArg("--foundations-only", remove: true);
			BuildingBlocksOnly = arg.HasArg("--building-only", remove: true);
			SnapToTerrain = arg.HasArg("--autosnap-terrain", remove: true);
			SnapToZero = arg.HasArg("--autosnap-zero", remove: true);
			Players = arg.HasArg("--players", remove: true);
		}

		public PasteOptions(PasteRequest request)
		{
			Resources = request.resources;
			NPCs = request.npcs;
			Vehicles = request.vehicles;
			Deployables = request.deployables;
			FoundationsOnly = request.foundationsOnly;
			BuildingBlocksOnly = request.buildingBlocksOnly;
			SnapToTerrain = request.snapToTerrain;
			SnapToZero = request.snapToZero;
			Players = request.players;
			Origin = request.origin;
			PlayerRotation = Quaternion.Euler(request.playerRotation);
			HeightOffset = request.heightOffset;
		}

		public PasteOptions()
		{
		}
	}

	private const string ClipboardFileName = "clipboard";

	private const string OverwriteFlag = "--overwrite";

	private static CopyPasteHistoryManager playerHistory = new CopyPasteHistoryManager();

	private static void PrintPasteNames(StringBuilder builder, string directory)
	{
		if (!Directory.Exists(directory))
		{
			builder.AppendLine("No pastes found");
			return;
		}
		string[] files = Directory.GetFiles(directory, "*.data");
		builder.AppendLine($"Found {files.Length} pastes");
		foreach (string item in files.OrderBy((string x) => x))
		{
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(item);
			builder.AppendLine(fileNameWithoutExtension);
		}
	}

	private static void CopyEntities(BasePlayer player, List<BaseEntity> entities, string name, Vector3 originPos, Quaternion originRot)
	{
		OrderEntitiesForSave(entities);
		using CopyPasteEntityInfo copyPasteEntityInfo = Facepunch.Pool.Get<CopyPasteEntityInfo>();
		copyPasteEntityInfo.entities = Facepunch.Pool.Get<List<ProtoBuf.Entity>>();
		Transform transform = new GameObject("Align").transform;
		transform.position = originPos;
		transform.rotation = originRot;
		foreach (BaseEntity entity in entities)
		{
			if (!entity.isClient && entity.enableSaving)
			{
				BaseEntity baseEntity = entity.parentEntity.Get(serverside: true);
				if (baseEntity != null && (!entities.Contains(baseEntity) || !baseEntity.enableSaving))
				{
					Debug.LogWarning("Skipping " + entity.ShortPrefabName + " as it is parented to an entity not included in the copy (it would become orphaned)");
				}
				else
				{
					SaveEntity(entity, copyPasteEntityInfo, baseEntity, transform);
				}
			}
		}
		UnityEngine.Object.Destroy(transform.gameObject);
		CopyPasteEntity.ServerInstance?.ClientRPC(RpcTarget.Player("RecievePaste", player), name, copyPasteEntityInfo);
	}

	private static List<EntityWrapper> PrepareEntityProtos(CopyPasteEntityInfo toLoad, PasteOptions options, bool assignNewUids)
	{
		toLoad = toLoad.Copy();
		Transform transform = new GameObject("Align").transform;
		transform.position = options.Origin;
		transform.rotation = options.PlayerRotation;
		List<EntityWrapper> list = new List<EntityWrapper>();
		Dictionary<ulong, ulong> remapping = new Dictionary<ulong, ulong>();
		Dictionary<uint, uint> dictionary = new Dictionary<uint, uint>();
		if (assignNewUids)
		{
			remapping = new Dictionary<ulong, ulong>();
		}
		foreach (ProtoBuf.Entity entity in toLoad.entities)
		{
			if (assignNewUids)
			{
				entity.InspectUids(UpdateWithNewUid);
			}
			EntityWrapper item = new EntityWrapper
			{
				Protobuf = entity,
				HasParent = (entity.parent != null && entity.parent.uid != default(NetworkableId))
			};
			list.Add(item);
			if (entity.decayEntity != null)
			{
				if (!dictionary.TryGetValue(entity.decayEntity.buildingID, out var value))
				{
					value = BuildingManager.server.NewBuildingID();
					dictionary.Add(entity.decayEntity.buildingID, value);
				}
				entity.decayEntity.buildingID = value;
			}
		}
		foreach (EntityWrapper item2 in list)
		{
			item2.Position = item2.Protobuf.baseEntity.pos;
			item2.Rotation = Quaternion.Euler(item2.Protobuf.baseEntity.rot);
			if (!item2.HasParent)
			{
				item2.Protobuf.baseEntity.pos = transform.TransformPoint(item2.Protobuf.baseEntity.pos);
				item2.Protobuf.baseEntity.rot = (transform.rotation * Quaternion.Euler(item2.Protobuf.baseEntity.rot)).eulerAngles;
			}
		}
		if (UnityEngine.Application.isPlaying)
		{
			UnityEngine.Object.Destroy(transform.gameObject);
		}
		else
		{
			UnityEngine.Object.DestroyImmediate(transform.gameObject);
		}
		return list;
		void UpdateWithNewUid(UidType type, ref ulong prevUid)
		{
			if (type == UidType.Clear)
			{
				prevUid = 0uL;
			}
			else if (prevUid != 0L && remapping != null)
			{
				if (!remapping.TryGetValue(prevUid, out var value2))
				{
					value2 = Network.Net.sv.TakeUID();
					remapping.Add(prevUid, value2);
				}
				prevUid = value2;
			}
		}
	}

	private static void ApplyAutoSnap(List<BaseEntity> entities, PasteOptions options)
	{
		float num = float.MaxValue;
		float num2 = float.MinValue;
		foreach (BaseEntity entity in entities)
		{
			Vector3 position = entity.transform.position;
			if (!(entity.parentEntity.Get(serverside: true) == null))
			{
				continue;
			}
			string shortPrefabName = entity.ShortPrefabName;
			if (shortPrefabName == "foundation" || shortPrefabName == "foundation.triangle")
			{
				float num3 = ((!UnityEngine.Application.isPlaying) ? 0f : TerrainMeta.HeightMap.GetHeight(position));
				RaycastHit hitInfo;
				if (options.SnapToZero)
				{
					num3 = 0f;
				}
				else if (UnityEngine.Physics.Raycast(new Vector3(position.x, num3, position.z) + new Vector3(0f, 100f, 0f), Vector3.down, out hitInfo, 100f, 8454144))
				{
					num3 = hitInfo.point.y;
				}
				if (position.y > num3)
				{
					num = Mathf.Min(num, position.y - num3);
				}
				if (num3 > position.y)
				{
					num2 = Mathf.Max(num2, num3 - position.y);
				}
			}
		}
		if ((!options.SnapToTerrain && !options.SnapToZero) || (num == float.MaxValue && num2 == float.MinValue))
		{
			num2 = 0f;
			num = 0f;
		}
		Vector3 vector = new Vector3(0f, (num < num2 || num2 == float.MinValue) ? (num * -1f) : num2, 0f);
		vector += options.HeightOffset;
		if (!(vector != Vector3.zero))
		{
			return;
		}
		foreach (BaseEntity entity2 in entities)
		{
			if (entity2.parentEntity.Get(serverside: true) == null)
			{
				entity2.transform.position += vector;
			}
			if (!(entity2 is IOEntity iOEntity))
			{
				continue;
			}
			if (iOEntity.inputs != null)
			{
				IOEntity.IOSlot[] inputs = iOEntity.inputs;
				for (int i = 0; i < inputs.Length; i++)
				{
					inputs[i].originPosition += vector;
				}
			}
			if (iOEntity.outputs != null)
			{
				IOEntity.IOSlot[] inputs = iOEntity.outputs;
				for (int i = 0; i < inputs.Length; i++)
				{
					inputs[i].originPosition += vector;
				}
			}
		}
	}

	public static List<BaseEntity> PasteEntitiesInternal(CopyPasteEntityInfo toLoad, PasteOptions options)
	{
		List<EntityWrapper> list = PrepareEntityProtos(toLoad, options, assignNewUids: true);
		List<BaseEntity> list2 = new List<BaseEntity>();
		foreach (EntityWrapper item in list)
		{
			if (CanPrefabBePasted(item.Protobuf.baseNetworkable.prefabID, options))
			{
				item.Entity = GameManager.server.CreateEntity(StringPool.Get(item.Protobuf.baseNetworkable.prefabID), item.Protobuf.baseEntity.pos, Quaternion.Euler(item.Protobuf.baseEntity.rot));
				if (item.Protobuf.basePlayer != null && item.Protobuf.basePlayer.userid > 10000000)
				{
					ulong userid = 10000000uL + (ulong)UnityEngine.Random.Range(1, int.MaxValue);
					item.Protobuf.basePlayer.userid = userid;
				}
				item.Entity.InitLoad(item.Protobuf.baseNetworkable.uid);
				item.Entity.PreServerLoad();
				list2.Add(item.Entity);
			}
		}
		list.RemoveAll((EntityWrapper x) => x.Entity == null);
		for (int num = 0; num < list.Count; num++)
		{
			EntityWrapper entityWrapper = list[num];
			BaseNetworkable.LoadInfo info = new BaseNetworkable.LoadInfo
			{
				fromDisk = true,
				fromCopy = true,
				msg = entityWrapper.Protobuf
			};
			try
			{
				entityWrapper.Entity.Spawn();
				bool flag = false;
				if (!flag && entityWrapper.Protobuf.parent != null && entityWrapper.Protobuf.parent.uid != default(NetworkableId))
				{
					BaseEntity baseEntity = BaseNetworkable.serverEntities.Find(entityWrapper.Protobuf.parent.uid) as BaseEntity;
					if (baseEntity == null || baseEntity.net == null)
					{
						flag = true;
					}
				}
				if (flag)
				{
					entityWrapper.Entity.Kill();
					list.RemoveAt(num);
					num--;
				}
				else
				{
					entityWrapper.Entity.Load(info);
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
				entityWrapper.Entity.Kill();
			}
		}
		ApplyAutoSnap(list2, options);
		foreach (EntityWrapper item2 in list)
		{
			item2.Entity.PostServerLoad();
			item2.Entity.UpdateNetworkGroup();
		}
		foreach (EntityWrapper item3 in list)
		{
			item3.Entity.RefreshEntityLinks();
		}
		foreach (EntityWrapper item4 in list)
		{
			if (item4.Entity is BuildingBlock buildingBlock)
			{
				buildingBlock.UpdateSkin(force: true);
			}
		}
		return (from x in list
			select x.Entity into x
			where x != null
			select x).ToList();
	}

	public static List<BaseEntity> PasteEntitiesEditMode(CopyPasteEntityInfo toLoad, PasteOptions options)
	{
		List<EntityWrapper> list = PrepareEntityProtos(toLoad, options, assignNewUids: false);
		List<BaseEntity> list2 = new List<BaseEntity>();
		foreach (EntityWrapper item in list)
		{
			if (CanPrefabBePasted(item.Protobuf.baseNetworkable.prefabID, options))
			{
				string strPrefab = StringPool.Get(item.Protobuf.baseNetworkable.prefabID);
				item.Entity = GameManager.server.CreateEntity(strPrefab, item.Protobuf.baseEntity.pos, Quaternion.Euler(item.Protobuf.baseEntity.rot));
				if (item.Entity != null)
				{
					list2.Add(item.Entity);
				}
			}
		}
		list.RemoveAll((EntityWrapper x) => x.Entity == null);
		ApplyAutoSnap(list2, options);
		foreach (BaseEntity item2 in list2)
		{
			item2.RefreshEntityLinks();
		}
		return list2;
	}

	public static CopyPasteEntityInfo LoadFileFromBundles(string fullPath)
	{
		CopyPasteDataAsset copyPasteDataAsset = FileSystem.Load<CopyPasteDataAsset>(fullPath);
		if (copyPasteDataAsset == null)
		{
			Debug.LogWarning("Missing file: " + fullPath);
			return null;
		}
		return CopyPasteEntityInfo.Deserialize(copyPasteDataAsset.GetData());
	}

	private static void SaveEntity(BaseEntity baseEntity, CopyPasteEntityInfo toSave, BaseEntity parent, Transform alignObject)
	{
		BaseNetworkable.SaveInfo info = new BaseNetworkable.SaveInfo
		{
			forDisk = true,
			msg = Facepunch.Pool.Get<ProtoBuf.Entity>()
		};
		baseEntity.Save(info);
		if (parent == null)
		{
			info.msg.baseEntity.pos = alignObject.InverseTransformPoint(info.msg.baseEntity.pos);
			_ = alignObject.rotation * baseEntity.transform.rotation;
			info.msg.baseEntity.rot = (Quaternion.Inverse(alignObject.transform.rotation) * baseEntity.transform.rotation).eulerAngles;
		}
		toSave.entities.Add(info.msg);
	}

	private static void GetEntitiesLookingAt(Vector3 originPoint, Vector3 direction, List<BaseEntity> entityList)
	{
		entityList.Clear();
		BuildingBlock buildingBlock = GamePhysics.TraceRealmEntity(GamePhysics.Realm.Server, new Ray(originPoint, direction), 0f, 100f, 2097408) as BuildingBlock;
		if (!(buildingBlock == null))
		{
			ListHashSet<DecayEntity> listHashSet = buildingBlock.GetBuilding()?.decayEntities;
			if (listHashSet != null)
			{
				entityList.AddRange(listHashSet);
			}
		}
	}

	private static void GetEntitiesInRadius(Vector3 originPoint, float radius, List<BaseEntity> entityList)
	{
		if (radius <= 0f)
		{
			return;
		}
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		global::Vis.Entities(originPoint, radius, obj);
		foreach (BaseEntity item in obj)
		{
			if (!item.isClient)
			{
				entityList.Add(item);
			}
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	private static void GetEntitiesInBounds(Bounds bounds, List<BaseEntity> entityList)
	{
		OBB bounds2 = new OBB(bounds);
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		global::Vis.Entities(bounds2, obj);
		foreach (BaseEntity item in obj)
		{
			if (!item.isClient)
			{
				entityList.Add(item);
			}
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	private static bool CanPrefabBePasted(uint prefabId, PasteOptions options)
	{
		GameObject gameObject = GameManager.server.FindPrefab(prefabId);
		if (gameObject == null)
		{
			return false;
		}
		BaseEntity component = gameObject.GetComponent<BaseEntity>();
		if (component == null)
		{
			return false;
		}
		if (options.FoundationsOnly && component.ShortPrefabName != "foundation" && component.ShortPrefabName != "foundation.triangle")
		{
			return false;
		}
		if (options.BuildingBlocksOnly && !(component is BuildingBlock))
		{
			return false;
		}
		if (component is DecayEntity && !(component is BuildingBlock) && !options.Deployables)
		{
			return false;
		}
		if (component is BasePlayer { IsNpc: false } && !options.Players)
		{
			return false;
		}
		if (component is PointEntity || component is RelationshipManager)
		{
			return false;
		}
		if ((component is ResourceEntity || component is BushEntity) && !options.Resources)
		{
			return false;
		}
		if ((component is BaseNpc || component is RidableHorse) && !options.NPCs)
		{
			return false;
		}
		if (component is BaseVehicle && !(component is RidableHorse) && !options.Vehicles)
		{
			return false;
		}
		return true;
	}

	private static void OrderEntitiesForSave(List<BaseEntity> entities)
	{
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		obj.AddRange(entities);
		entities.Clear();
		HashSet<BaseEntity> hash = new HashSet<BaseEntity>();
		foreach (BaseEntity item in obj.OrderBy((BaseEntity x) => x.net.ID.Value))
		{
			AddRecursive(item);
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		void AddRecursive(BaseEntity current)
		{
			if (hash.Add(current))
			{
				entities.Add(current);
				if (current.children != null)
				{
					foreach (BaseEntity child in current.children)
					{
						AddRecursive(child);
					}
				}
			}
		}
	}

	[ServerVar(Name = "copybox_sv")]
	public static void copybox_sv(Arg args)
	{
		if (!args.HasArgs(3))
		{
			args.ReplyWith("Missing args: copybox_sv <name> <center> <size> <rotation>");
			return;
		}
		string name = args.GetString(0);
		Vector3 vector = args.GetVector3(1);
		Vector3 vector2 = args.GetVector3(2);
		Quaternion originRot = Quaternion.Euler(args.GetVector3(3));
		Bounds bounds = new Bounds(vector, vector2);
		List<BaseEntity> obj = Facepunch.Pool.GetList<BaseEntity>();
		GetEntitiesInBounds(bounds, obj);
		CopyEntities(ArgEx.Player(args), obj, name, vector, originRot);
		Facepunch.Pool.FreeList(ref obj);
	}

	public static List<BaseEntity> PasteEntities(CopyPasteEntityInfo data, PasteOptions options, ulong steamId)
	{
		List<BaseEntity> list;
		try
		{
			Rust.Application.isLoadingSave = true;
			Rust.Application.isLoading = true;
			list = PasteEntitiesInternal(data, options);
		}
		catch (Exception exception)
		{
			Debug.LogException(exception);
			return new List<BaseEntity>();
		}
		finally
		{
			Rust.Application.isLoadingSave = false;
			Rust.Application.isLoading = false;
		}
		foreach (BaseEntity item in list)
		{
			if (!(item == null) && item is StabilityEntity stabilityEntity)
			{
				stabilityEntity.UpdateStability();
			}
		}
		playerHistory.AddToHistory(steamId, list);
		return list;
	}

	[ServerVar]
	public static void undopaste_sv(Arg args)
	{
		ulong steamId = ArgEx.Player(args)?.userID ?? ((EncryptedValue<ulong>)0uL);
		PasteResult pasteResult = playerHistory.Undo(steamId);
		if (pasteResult == null)
		{
			args.ReplyWith("History empty");
			return;
		}
		foreach (BaseEntity entity in pasteResult.Entities)
		{
			entity.Kill();
		}
	}

	[ServerVar]
	public static void copyradius_sv(Arg args)
	{
		string name = args.GetString(0);
		Vector3 vector = args.GetVector3(1);
		float num = args.GetFloat(2);
		Quaternion originRot = Quaternion.Euler(args.GetVector3(3));
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		if (num <= 0f)
		{
			args.ReplyWith("Invalid radius: must be greater than zero");
			return;
		}
		GetEntitiesInRadius(vector, num, obj);
		CopyEntities(ArgEx.Player(args), obj, name, vector, originRot);
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void copybuilding_sv(Arg args)
	{
		string name = args.GetString(0);
		Vector3 vector = args.GetVector3(1);
		Vector3 vector2 = args.GetVector3(2);
		Quaternion originRot = Quaternion.Euler(args.GetVector3(3));
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		GetEntitiesLookingAt(vector, vector2, obj);
		CopyEntities(ArgEx.Player(args), obj, name, vector, originRot);
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	[ServerVar]
	public static void printselection_sv(Arg args)
	{
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		Vector3 vector = args.GetVector3(0);
		Vector3 vector2 = args.GetVector3(1);
		args.GetVector3(2);
		GetEntitiesInBounds(new Bounds(vector, vector2), obj);
		StringBuilder stringBuilder = new StringBuilder();
		if (obj.Count == 0)
		{
			stringBuilder.AppendLine("Empty");
		}
		else
		{
			foreach (BaseEntity item in obj)
			{
				if (!item.isClient)
				{
					stringBuilder.AppendLine(item.name);
				}
			}
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		args.ReplyWith(stringBuilder.ToString());
	}

	private static string GetLegacyServerDirectory()
	{
		return Server.GetServerFolder("copypaste");
	}

	private static string GetLegacyServerPath(string name)
	{
		return GetLegacyServerDirectory() + "/" + name + ".data";
	}

	[ServerVar]
	public static void download_paste(Arg arg)
	{
		if (!arg.HasArgs())
		{
			arg.ReplyWith("Missing args: download_paste <name>");
			return;
		}
		string text = arg.GetString(0);
		string legacyServerPath = GetLegacyServerPath(arg.GetString(0));
		if (!File.Exists(legacyServerPath))
		{
			arg.ReplyWith("Paste '" + text + "' not found");
			return;
		}
		using CopyPasteEntityInfo arg2 = CopyPasteEntityInfo.Deserialize(File.ReadAllBytes(legacyServerPath));
		CopyPasteEntity.ServerInstance.ClientRPC(RpcTarget.Player("RecievePaste", arg.Connection), text, arg2);
	}

	[ServerVar]
	public static void list_pastes_sv(Arg arg)
	{
		StringBuilder stringBuilder = new StringBuilder();
		PrintPasteNames(stringBuilder, GetLegacyServerDirectory());
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar]
	public static void killbox_sv(Arg args)
	{
		Vector3 vector = args.GetVector3(0);
		Vector3 vector2 = args.GetVector3(1);
		PasteOptions options = new PasteOptions(args);
		Bounds bounds = new Bounds(vector, vector2);
		List<BaseEntity> obj = Facepunch.Pool.Get<List<BaseEntity>>();
		GetEntitiesInBounds(bounds, obj);
		foreach (BaseEntity item in obj)
		{
			if (!item.isClient && CanPrefabBePasted(item.prefabID, options) && (!(item is BasePlayer entity) || entity.IsNpcPlayer()))
			{
				item.Kill();
			}
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
	}

	private static Quaternion GetPlayerRotation(BasePlayer ply)
	{
		Vector3 forward = ply.eyes.BodyForward();
		forward.y = 0f;
		return Quaternion.LookRotation(forward, Vector3.up);
	}
}
