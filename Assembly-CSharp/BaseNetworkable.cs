#define UNITY_ASSERTIONS
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ConVar;
using Facepunch;
using Facepunch.Rust.Profiling;
using Network;
using Network.Visibility;
using Oxide.Core;
using ProtoBuf;
using Rust;
using Rust.Registry;
using UnityEngine;
using UnityEngine.Assertions;

public abstract class BaseNetworkable : BaseMonoBehaviour, IEntity, NetworkHandler, IPrefabPostProcess
{
	public struct SaveInfo
	{
		public ProtoBuf.Entity msg;

		public bool forDisk;

		public bool forTransfer;

		public Connection forConnection;

		internal bool SendingTo(Connection ownerConnection)
		{
			if (ownerConnection == null)
			{
				return false;
			}
			if (forConnection == null)
			{
				return false;
			}
			return forConnection == ownerConnection;
		}
	}

	public struct LoadInfo
	{
		public ProtoBuf.Entity msg;

		public bool fromDisk;

		public bool fromCopy;

		public bool fromTransfer;
	}

	public class EntityRealmServer : EntityRealm
	{
		protected override Manager visibilityManager
		{
			get
			{
				if (Network.Net.sv == null)
				{
					return null;
				}
				return Network.Net.sv.visibility;
			}
		}
	}

	public abstract class EntityRealm : IEnumerable<BaseNetworkable>, IEnumerable
	{
		public HiddenValue<ListDictionary<NetworkableId, BaseNetworkable>> entityList = new HiddenValue<ListDictionary<NetworkableId, BaseNetworkable>>(new ListDictionary<NetworkableId, BaseNetworkable>());

		public int Count => entityList.Get().Count;

		protected abstract Manager visibilityManager { get; }

		public bool Contains(NetworkableId uid)
		{
			return entityList.Get().Contains(uid);
		}

		public BaseNetworkable Find(NetworkableId uid)
		{
			BaseNetworkable val = null;
			if (!entityList.Get().TryGetValue(uid, out val))
			{
				return null;
			}
			return val;
		}

		public bool TryGetEntity(NetworkableId uid, out BaseEntity entity)
		{
			entity = Find(uid) as BaseEntity;
			return entity != null;
		}

		public void RegisterID(BaseNetworkable ent)
		{
			if (ent.net != null)
			{
				ListDictionary<NetworkableId, BaseNetworkable> listDictionary = entityList.Get();
				if (listDictionary.Contains(ent.net.ID))
				{
					listDictionary[ent.net.ID] = ent;
				}
				else
				{
					listDictionary.Add(ent.net.ID, ent);
				}
			}
		}

		public void UnregisterID(BaseNetworkable ent)
		{
			if (ent.net != null)
			{
				entityList.Get().Remove(ent.net.ID);
			}
		}

		public Group FindGroup(uint uid)
		{
			return visibilityManager?.Get(uid);
		}

		public Group TryFindGroup(uint uid)
		{
			return visibilityManager?.TryGet(uid);
		}

		public void FindInGroup(uint uid, List<BaseNetworkable> list)
		{
			Group obj = TryFindGroup(uid);
			if (obj == null)
			{
				return;
			}
			int count = obj.networkables.Values.Count;
			Networkable[] buffer = obj.networkables.Values.Buffer;
			for (int i = 0; i < count; i++)
			{
				Networkable networkable = buffer[i];
				BaseNetworkable baseNetworkable = Find(networkable.ID);
				if (!(baseNetworkable == null) && baseNetworkable.net != null && baseNetworkable.net.group != null)
				{
					if (baseNetworkable.net.group.ID != uid)
					{
						Debug.LogWarning("Group ID mismatch: " + baseNetworkable.ToString());
					}
					else
					{
						list.Add(baseNetworkable);
					}
				}
			}
		}

		public BufferList<BaseNetworkable>.Enumerator GetEnumerator()
		{
			return entityList.Get().Values.GetEnumerator();
		}

		IEnumerator<BaseNetworkable> IEnumerable<BaseNetworkable>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Clear()
		{
			entityList.Get().Clear();
		}
	}

	public enum DestroyMode : byte
	{
		None,
		Gib
	}

	[ReadOnly]
	[Header("BaseNetworkable")]
	public uint prefabID;

	[Tooltip("If enabled the entity will send to everyone on the server - regardless of position")]
	public bool globalBroadcast;

	[Tooltip("Global broadcast a cut down version of the entity to show buildings across the map")]
	public bool globalBuildingBlock;

	[NonSerialized]
	public Networkable net;

	[NonSerialized]
	private BaseEntity _prefab;

	private string _prefabName;

	private string _prefabNameWithoutExtension;

	public static EntityRealm serverEntities = new EntityRealmServer();

	private const bool isServersideEntity = true;

	public static List<Connection> connectionsInSphereList = new List<Connection>();

	public List<Component> postNetworkUpdateComponents = new List<Component>();

	public bool _limitedNetworking;

	[NonSerialized]
	public EntityRef parentEntity;

	[NonSerialized]
	public readonly List<BaseEntity> children = new List<BaseEntity>();

	[NonSerialized]
	public bool canTriggerParent = true;

	public int creationFrame;

	public bool isSpawned;

	public ListHashSet<BaseNetworkable> occludees;

	private int lastDemoIndex = -1;

	public MemoryStream _NetworkCache;

	public static Queue<MemoryStream> EntityMemoryStreamPool = new Queue<MemoryStream>();

	private MemoryStream _SaveCache;

	public bool IsDestroyed { get; private set; }

	public string PrefabName
	{
		get
		{
			if (_prefabName == null)
			{
				_prefabName = StringPool.Get(prefabID);
			}
			return _prefabName;
		}
	}

	public string ShortPrefabName
	{
		get
		{
			if (_prefabNameWithoutExtension == null)
			{
				_prefabNameWithoutExtension = Path.GetFileNameWithoutExtension(PrefabName);
			}
			return _prefabNameWithoutExtension;
		}
	}

	public bool isServer => true;

	public bool isClient => false;

	public bool limitNetworking
	{
		get
		{
			return _limitedNetworking;
		}
		set
		{
			if (value != _limitedNetworking)
			{
				_limitedNetworking = value;
				if (_limitedNetworking)
				{
					OnNetworkLimitStart();
				}
				else
				{
					OnNetworkLimitEnd();
				}
				UpdateNetworkGroup();
			}
		}
	}

	public GameManager gameManager
	{
		get
		{
			if (isServer)
			{
				return GameManager.server;
			}
			throw new NotImplementedException("Missing gameManager path");
		}
	}

	public PrefabAttribute.Library prefabAttribute
	{
		get
		{
			if (isServer)
			{
				return PrefabAttribute.server;
			}
			throw new NotImplementedException("Missing prefabAttribute path");
		}
	}

	public static Group GlobalNetworkGroup => Network.Net.sv.visibility.Get(0u);

	public static Group LimboNetworkGroup => Network.Net.sv.visibility.Get(1u);

	public bool HasNetworkCache => _NetworkCache != null;

	public virtual Vector3 GetNetworkPosition()
	{
		return base.transform.localPosition;
	}

	public virtual Quaternion GetNetworkRotation()
	{
		return base.transform.localRotation;
	}

	public string InvokeString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		List<InvokeAction> obj = Facepunch.Pool.Get<List<InvokeAction>>();
		InvokeHandler.FindInvokes(this, obj);
		foreach (InvokeAction item in obj)
		{
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(item.action.Method.Name);
		}
		Facepunch.Pool.FreeUnmanaged(ref obj);
		return stringBuilder.ToString();
	}

	public BaseEntity LookupPrefab()
	{
		if (_prefab == null)
		{
			_prefab = GameObjectEx.ToBaseEntity(gameManager.FindPrefab(PrefabName));
		}
		return _prefab;
	}

	public T LookupPrefab<T>() where T : BaseEntity
	{
		return LookupPrefab() as T;
	}

	public bool EqualNetID(BaseNetworkable other)
	{
		if (!other.IsRealNull() && other.net != null && net != null)
		{
			return other.net.ID == net.ID;
		}
		return false;
	}

	public bool EqualNetID(NetworkableId otherID)
	{
		if (net != null)
		{
			return otherID == net.ID;
		}
		return false;
	}

	public virtual void ResetState()
	{
		if (children.Count > 0)
		{
			children.Clear();
		}
		if (this is ILootableEntity lootableEntity)
		{
			lootableEntity.LastLootedBy = 0uL;
		}
	}

	public virtual void InitShared()
	{
	}

	public virtual void PreInitShared()
	{
	}

	public virtual void PostInitShared()
	{
	}

	public virtual void DestroyShared()
	{
	}

	public virtual void OnNetworkGroupEnter(Group group)
	{
		Interface.CallHook("OnNetworkGroupEntered", this, group);
	}

	public virtual void OnNetworkGroupLeave(Group group)
	{
		Interface.CallHook("OnNetworkGroupLeft", this, group);
	}

	public void OnNetworkGroupChange()
	{
		if (children != null && net.group != null)
		{
			foreach (BaseEntity child in children)
			{
				if (child.IsRealNull())
				{
					Debug.LogError("Child is null when switching groups", this);
				}
				else if (child.net != null)
				{
					if (child.ShouldInheritNetworkGroup())
					{
						child.net.SwitchGroup(net.group);
					}
					else if (isServer)
					{
						child.UpdateNetworkGroup();
					}
				}
			}
		}
		if (ServerOcclusion.OcclusionEnabled && SupportsServerOcclusion())
		{
			ListHashSet<BaseNetworkable> value;
			if (net.group == null)
			{
				occludees = null;
			}
			else if (ServerOcclusion.Occludees.TryGetValue(net.group, out value))
			{
				occludees = value;
			}
			else
			{
				occludees = null;
			}
		}
	}

	public void OnNetworkSubscribersEnter(List<Connection> connections)
	{
		if (!Network.Net.sv.IsConnected())
		{
			return;
		}
		foreach (Connection connection in connections)
		{
			BasePlayer basePlayer = connection.player as BasePlayer;
			if (!(basePlayer == null))
			{
				basePlayer.QueueUpdate(BasePlayer.NetworkQueue.Update, this as BaseEntity);
			}
		}
	}

	public void OnNetworkSubscribersLeave(List<Connection> connections)
	{
		if (Network.Net.sv.IsConnected())
		{
			LogEntry(RustLog.EntryType.Network, 2, "LeaveVisibility");
			NetWrite netWrite = Network.Net.sv.StartWrite();
			netWrite.PacketID(Message.Type.EntityDestroy);
			netWrite.EntityID(net.ID);
			netWrite.UInt8(0);
			netWrite.Send(new SendInfo(connections));
		}
	}

	public void EntityDestroy()
	{
		if ((bool)base.gameObject)
		{
			ResetState();
			gameManager.Retire(base.gameObject);
		}
	}

	private void DoEntityDestroy()
	{
		if (IsDestroyed)
		{
			return;
		}
		IsDestroyed = true;
		if (Rust.Application.isQuitting)
		{
			return;
		}
		DestroyShared();
		if (isServer)
		{
			DoServerDestroy();
		}
		using (TimeWarning.New("Registry.Entity.Unregister"))
		{
			Rust.Registry.Entity.Unregister(base.gameObject);
		}
	}

	private void SpawnShared()
	{
		IsDestroyed = false;
		using (TimeWarning.New("Registry.Entity.Register"))
		{
			Rust.Registry.Entity.Register(base.gameObject, this);
		}
	}

	public virtual void Save(SaveInfo info)
	{
		if (prefabID == 0)
		{
			Debug.LogError("PrefabID is 0! " + TransformEx.GetRecursiveName(base.transform), base.gameObject);
		}
		info.msg.baseNetworkable = Facepunch.Pool.Get<ProtoBuf.BaseNetworkable>();
		info.msg.baseNetworkable.uid = net.ID;
		info.msg.baseNetworkable.prefabID = prefabID;
		if (net.group != null)
		{
			info.msg.baseNetworkable.group = net.group.ID;
		}
		if (!info.forDisk)
		{
			info.msg.createdThisFrame = creationFrame == UnityEngine.Time.frameCount;
		}
	}

	public virtual void PostSave(SaveInfo info)
	{
	}

	public void InitLoad(NetworkableId entityID)
	{
		net = Network.Net.sv.CreateNetworkable(entityID);
		serverEntities.RegisterID(this);
	}

	public virtual void PreServerLoad()
	{
	}

	public virtual void Load(LoadInfo info)
	{
		if (info.msg.baseNetworkable != null)
		{
			LoadInfo loadInfo = info;
			Interface.CallHook("OnEntityLoaded", this, info);
			ProtoBuf.BaseNetworkable baseNetworkable = loadInfo.msg.baseNetworkable;
			if (prefabID != baseNetworkable.prefabID)
			{
				Debug.LogError("Prefab IDs don't match! " + prefabID + "/" + baseNetworkable.prefabID + " -> " + base.gameObject, base.gameObject);
			}
		}
	}

	public virtual void PostServerLoad()
	{
		base.gameObject.SendOnSendNetworkUpdate(this as BaseEntity);
	}

	public T ToServer<T>() where T : BaseNetworkable
	{
		if (isServer)
		{
			return this as T;
		}
		return null;
	}

	public virtual bool OnRpcMessage(BasePlayer player, uint rpc, Message msg)
	{
		return false;
	}

	protected virtual bool OnSyncVar(byte syncVar, NetRead reader, bool fromAutoSave = false)
	{
		return false;
	}

	protected virtual bool WriteSyncVar(byte id, NetWrite writer)
	{
		return false;
	}

	protected virtual bool AutoSaveSyncVars(SaveInfo save)
	{
		return false;
	}

	protected virtual bool AutoLoadSyncVars(LoadInfo load)
	{
		return false;
	}

	protected virtual void ResetSyncVars()
	{
	}

	protected virtual bool ShouldInvalidateCache(byte id)
	{
		return false;
	}

	protected virtual bool IsSyncVarEqual<T>(T oldValue, T newValue)
	{
		return EqualityComparer<T>.Default.Equals(oldValue, newValue);
	}

	public static List<Connection> GetConnectionsWithin(Vector3 position, float distance, bool addSecondaryConnections = false, bool useRcEntityPosition = true, bool includeInvisPlayers = false)
	{
		connectionsInSphereList.Clear();
		using PooledList<BasePlayer> pooledList = Facepunch.Pool.Get<PooledList<BasePlayer>>();
		BaseEntity.Query.Server.GetPlayersInSphere(position, distance, pooledList);
		float num = distance * distance;
		foreach (BasePlayer item in pooledList)
		{
			if (item == null || item.isClient || item.Connection == null)
			{
				continue;
			}
			if (addSecondaryConnections)
			{
				if (useRcEntityPosition)
				{
					if (item.RcEntityPosition.HasValue)
					{
						AddSecondaryConnectionsWithin(item.RcEntityPosition.Value, num, item);
					}
				}
				else
				{
					AddSecondaryConnectionsWithin(position, num, item);
				}
			}
			connectionsInSphereList.Add(item.Connection);
			if (item.IsBeingSpectated)
			{
				ReadOnlySpan<BasePlayer> spectators = item.GetSpectators();
				for (int i = 0; i < spectators.Length; i++)
				{
					BasePlayer basePlayer = spectators[i];
					connectionsInSphereList.Add(basePlayer.Connection);
				}
			}
		}
		if (includeInvisPlayers)
		{
			foreach (BasePlayer invisPlayer in BasePlayer.invisPlayers)
			{
				if ((invisPlayer.transform.position - position).sqrMagnitude <= num)
				{
					connectionsInSphereList.Add(invisPlayer.Connection);
				}
			}
		}
		return connectionsInSphereList;
	}

	private static void AddSecondaryConnectionsWithin(Vector3 position, float sqrDistance, BasePlayer player)
	{
		if (position == Vector3.zero || player.net.secondaryGroup == null)
		{
			return;
		}
		foreach (Connection subscriber in player.net.secondaryGroup.subscribers)
		{
			BasePlayer basePlayer = subscriber.player as BasePlayer;
			if (!(basePlayer == null) && !(basePlayer.SqrDistance(position) > sqrDistance))
			{
				connectionsInSphereList.Add(player.Connection);
			}
		}
	}

	public static void GetCloseConnections(Vector3 position, float distance, List<Connection> foundConnections)
	{
		if (Network.Net.sv == null || Network.Net.sv.visibility == null)
		{
			return;
		}
		float num = distance * distance;
		Group obj = Network.Net.sv.visibility.GetGroup(position);
		if (obj == null)
		{
			return;
		}
		List<Connection> subscribers = obj.subscribers;
		for (int i = 0; i < subscribers.Count; i++)
		{
			Connection connection = subscribers[i];
			if (connection.active)
			{
				BasePlayer basePlayer = connection.player as BasePlayer;
				if (!(basePlayer == null) && !(basePlayer.SqrDistance(position) > num))
				{
					foundConnections.Add(basePlayer.Connection);
				}
			}
		}
	}

	public static void GetCloseConnections(Vector3 position, float distance, List<BasePlayer> players)
	{
		if (Network.Net.sv == null || Network.Net.sv.visibility == null)
		{
			return;
		}
		float num = distance * distance;
		Group obj = Network.Net.sv.visibility.GetGroup(position);
		if (obj == null)
		{
			return;
		}
		List<Connection> subscribers = obj.subscribers;
		for (int i = 0; i < subscribers.Count; i++)
		{
			Connection connection = subscribers[i];
			if (connection.active)
			{
				BasePlayer basePlayer = connection.player as BasePlayer;
				if (!(basePlayer == null) && !(basePlayer.SqrDistance(position) > num))
				{
					players.Add(basePlayer);
				}
			}
		}
	}

	public static bool HasCloseConnections(Vector3 position, float distance)
	{
		if (Network.Net.sv == null)
		{
			return false;
		}
		if (Network.Net.sv.visibility == null)
		{
			return false;
		}
		float num = distance * distance;
		Group obj = Network.Net.sv.visibility.GetGroup(position);
		if (obj == null)
		{
			return false;
		}
		List<Connection> subscribers = obj.subscribers;
		for (int i = 0; i < subscribers.Count; i++)
		{
			Connection connection = subscribers[i];
			if (connection.active)
			{
				BasePlayer basePlayer = connection.player as BasePlayer;
				if (!(basePlayer == null) && !(basePlayer.SqrDistance(position) > num))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool HasConnections(Vector3 position)
	{
		if (Network.Net.sv == null)
		{
			return false;
		}
		if (Network.Net.sv.visibility == null)
		{
			return false;
		}
		Group obj = Network.Net.sv.visibility.GetGroup(position);
		if (obj == null)
		{
			return false;
		}
		List<Connection> subscribers = obj.subscribers;
		for (int i = 0; i < subscribers.Count; i++)
		{
			Connection connection = subscribers[i];
			if (connection.active && !(connection.player as BasePlayer == null))
			{
				return true;
			}
		}
		return false;
	}

	public void BroadcastOnPostNetworkUpdate(BaseEntity entity)
	{
		foreach (Component postNetworkUpdateComponent in postNetworkUpdateComponents)
		{
			(postNetworkUpdateComponent as IOnPostNetworkUpdate)?.OnPostNetworkUpdate(entity);
		}
		foreach (BaseEntity child in children)
		{
			child.BroadcastOnPostNetworkUpdate(entity);
		}
	}

	public virtual void PostProcess(IPrefabProcessor preProcess, GameObject rootObj, string name, bool serverside, bool clientside, bool bundling)
	{
		if (!serverside)
		{
			postNetworkUpdateComponents = GetComponentsInChildren<IOnPostNetworkUpdate>(includeInactive: true).Cast<Component>().ToList();
		}
	}

	private void OnNetworkLimitStart()
	{
		LogEntry(RustLog.EntryType.Network, 2, "OnNetworkLimitStart");
		List<Connection> subscribers = GetSubscribers();
		if (subscribers == null)
		{
			return;
		}
		subscribers = subscribers.ToList();
		subscribers.RemoveAll((Connection x) => ShouldNetworkTo(x.player as BasePlayer));
		OnNetworkSubscribersLeave(subscribers);
		if (children == null)
		{
			return;
		}
		foreach (BaseEntity child in children)
		{
			child.OnNetworkLimitStart();
		}
	}

	private void OnNetworkLimitEnd()
	{
		LogEntry(RustLog.EntryType.Network, 2, "OnNetworkLimitEnd");
		List<Connection> subscribers = GetSubscribers();
		if (subscribers == null)
		{
			return;
		}
		OnNetworkSubscribersEnter(subscribers);
		if (children == null)
		{
			return;
		}
		foreach (BaseEntity child in children)
		{
			child.OnNetworkLimitEnd();
		}
	}

	public BaseEntity GetParentEntity()
	{
		return parentEntity.Get(isServer);
	}

	public bool HasParent()
	{
		return parentEntity.IsValid(isServer);
	}

	public void AddChild(BaseEntity child)
	{
		if (!children.Contains(child))
		{
			children.Add(child);
			OnChildAdded(child);
		}
	}

	protected virtual void OnChildAdded(BaseEntity child)
	{
	}

	public void RemoveChild(BaseEntity child)
	{
		children.Remove(child);
		OnChildRemoved(child);
	}

	protected virtual void OnChildRemoved(BaseEntity child)
	{
	}

	public virtual float GetNetworkTime()
	{
		return UnityEngine.Time.time;
	}

	public virtual void Spawn()
	{
		EntityProfiler.spawned++;
		if (EntityProfiler.mode >= 2)
		{
			EntityProfiler.OnSpawned(this);
		}
		SpawnShared();
		if (net == null)
		{
			net = Network.Net.sv.CreateNetworkable();
		}
		creationFrame = UnityEngine.Time.frameCount;
		PreInitShared();
		InitShared();
		ServerInit();
		PostInitShared();
		UpdateNetworkGroup();
		ServerInitPostNetworkGroupAssign();
		isSpawned = true;
		Interface.CallHook("OnEntitySpawned", this);
		SendNetworkUpdateImmediate();
		Invoke(SendGlobalNetworkUpdate, 0f);
		if (Rust.Application.isLoading && !Rust.Application.isLoadingSave)
		{
			base.gameObject.SendOnSendNetworkUpdate(this as BaseEntity);
		}
	}

	private void SendGlobalNetworkUpdate()
	{
		GlobalNetworkHandler.server?.TrySendNetworkUpdate(this);
	}

	public bool IsFullySpawned()
	{
		return isSpawned;
	}

	public virtual void ServerInit()
	{
		serverEntities.RegisterID(this);
		if (net != null)
		{
			net.handler = this;
		}
		lastDemoIndex = -1;
	}

	public virtual void ServerInitPostNetworkGroupAssign()
	{
	}

	public List<Connection> GetSubscribers()
	{
		if (net == null)
		{
			return null;
		}
		if (net.group == null)
		{
			return null;
		}
		return net.group.subscribers;
	}

	protected ListHashSet<BaseNetworkable> GetOccludees()
	{
		return occludees;
	}

	public void KillMessage()
	{
		Kill();
	}

	public virtual void AdminKill()
	{
		Kill(DestroyMode.Gib);
	}

	public virtual void OnKilled()
	{
	}

	public void Kill(DestroyMode mode = DestroyMode.None)
	{
		if (IsDestroyed)
		{
			Debug.LogWarning("Calling kill - but already IsDestroyed!? " + this);
		}
		else if (Interface.CallHook("OnEntityKill", this) == null)
		{
			EntityProfiler.killed++;
			if (EntityProfiler.mode >= 2)
			{
				EntityProfiler.OnKilled(this);
			}
			OnParentDestroyingEx.BroadcastOnParentDestroying(base.gameObject);
			OnKilled();
			DoEntityDestroy();
			TerminateOnClient(mode);
			TerminateOnServer();
			EntityDestroy();
		}
	}

	public void KillAsMapEntity()
	{
		if (IsFullySpawned())
		{
			Kill();
			return;
		}
		IsDestroyed = true;
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public void TerminateOnClient(DestroyMode mode)
	{
		if (net != null && net.group != null && Network.Net.sv.IsConnected())
		{
			LogEntry(RustLog.EntryType.Network, 2, "Term {0}", mode);
			NetWrite netWrite = Network.Net.sv.StartWrite();
			netWrite.PacketID(Message.Type.EntityDestroy);
			netWrite.EntityID(net.ID);
			netWrite.UInt8((byte)mode);
			netWrite.Send(new SendInfo(net.group.subscribers));
			GlobalNetworkHandler.server?.OnEntityKilled(this);
		}
	}

	public void TerminateOnServer()
	{
		if (net != null)
		{
			InvalidateNetworkCache();
			serverEntities.UnregisterID(this);
			Network.Net.sv.DestroyNetworkable(ref net);
			StopAllCoroutines();
			base.gameObject.SetActive(value: false);
		}
	}

	internal virtual void DoServerDestroy()
	{
		isSpawned = false;
	}

	public virtual bool ShouldNetworkTo(BasePlayer player)
	{
		object obj = Interface.CallHook("CanNetworkTo", this, player);
		if (obj is bool)
		{
			return (bool)obj;
		}
		if (net.group == null)
		{
			return true;
		}
		return player.net.subscriber.IsSubscribed(net.group);
	}

	public void SendNetworkGroupChange()
	{
		if (isSpawned && Network.Net.sv.IsConnected())
		{
			if (net.group == null)
			{
				Debug.LogWarning(ToString() + " changed its network group to null");
				return;
			}
			NetWrite netWrite = Network.Net.sv.StartWrite();
			netWrite.PacketID(Message.Type.GroupChange);
			netWrite.EntityID(net.ID);
			netWrite.GroupID(net.group.ID);
			netWrite.Send(new SendInfo(net.group.subscribers));
		}
	}

	public void SendAsSnapshot(Connection connection, bool ordered = true)
	{
		if (Interface.CallHook("OnEntitySnapshot", this, connection) == null)
		{
			NetWrite netWrite = Network.Net.sv.StartWrite();
			uint val = (ordered ? (++connection.validate.entityUpdates) : uint.MaxValue);
			SaveInfo saveInfo = new SaveInfo
			{
				forConnection = connection,
				forDisk = false
			};
			netWrite.PacketID(Message.Type.Entities);
			netWrite.UInt32(val);
			ToStreamForNetwork(netWrite, saveInfo);
			netWrite.Send(new SendInfo(connection));
			if (DemoConVars.ServerDemosEnabled)
			{
				lastDemoIndex = Network.Net.sv.serverDemos.DemoCount;
			}
		}
	}

	public void SendAsSnapshot(Connection connection, NetWrite write, bool ordered = true)
	{
		uint val = (ordered ? (++connection.validate.entityUpdates) : uint.MaxValue);
		if (Interface.CallHook("OnEntitySnapshot", this, connection) == null)
		{
			SaveInfo saveInfo = new SaveInfo
			{
				forConnection = connection,
				forDisk = false
			};
			write.PacketID(Message.Type.Entities);
			write.UInt32(val);
			ToStreamForNetwork(write, saveInfo);
			write.Send(new SendInfo(connection));
			if (DemoConVars.ServerDemosEnabled)
			{
				lastDemoIndex = Network.Net.sv.serverDemos.DemoCount;
			}
		}
	}

	public void SendAsSnapshotWithChildren(BasePlayer player, bool ordered = true)
	{
		Connection connection = player.net.connection;
		SendAsSnapshot(connection, ordered);
		SendChildren(children, player, ordered);
		static void SendChildren(List<BaseEntity> children, BasePlayer basePlayer, bool ordered2)
		{
			Connection connection2 = basePlayer.net.connection;
			foreach (BaseEntity child in children)
			{
				if (child.ShouldNetworkTo(basePlayer))
				{
					child.SendAsSnapshot(connection2, ordered2);
					SendChildren(child.children, basePlayer, ordered2);
				}
			}
		}
	}

	public void SendDemoTransientEntity()
	{
		if (!DemoConVars.ServerDemosEnabled)
		{
			return;
		}
		BaseEntity baseEntity = this as BaseEntity;
		if (!baseEntity.enableSaving && lastDemoIndex != Network.Net.sv.serverDemos.DemoCount)
		{
			lastDemoIndex = Network.Net.sv.serverDemos.DemoCount;
			NetWrite netWrite = Network.Net.sv.StartWrite();
			netWrite.PacketID(Message.Type.DemoTransientEntities);
			SaveInfo saveInfo = new SaveInfo
			{
				forDisk = true
			};
			bool flag = true;
			try
			{
				ToStream(netWrite, saveInfo);
			}
			catch (Exception exception)
			{
				Debug.LogError($"ServerDemo: Failed to take a snapshot of transient ${baseEntity.ShortPrefabName}[${net.ID}]", baseEntity);
				Debug.LogException(exception, this);
				flag = false;
			}
			if (flag)
			{
				Network.Net.sv.EnqueueToDemoThread(new DemoQueueItem(netWrite)
				{
					IgnoreNoConnections = true
				});
			}
			netWrite.RemoveReference();
		}
	}

	public void SendNetworkUpdate(BasePlayer.NetworkQueue queue = BasePlayer.NetworkQueue.Update)
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || IsDestroyed || net == null || !isSpawned)
		{
			return;
		}
		using (TimeWarning.New("SendNetworkUpdate"))
		{
			LogEntry(RustLog.EntryType.Network, 3, "SendNetworkUpdate");
			InvalidateNetworkCache();
			List<Connection> subscribers = GetSubscribers();
			if (subscribers != null && subscribers.Count > 0)
			{
				for (int i = 0; i < subscribers.Count; i++)
				{
					BasePlayer basePlayer = subscribers[i].player as BasePlayer;
					if (!(basePlayer == null) && ShouldNetworkTo(basePlayer))
					{
						basePlayer.QueueUpdate(queue, this);
					}
				}
			}
		}
		base.gameObject.SendOnSendNetworkUpdate(this as BaseEntity);
	}

	public void SendNetworkUpdateImmediate()
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || IsDestroyed || net == null || !isSpawned)
		{
			return;
		}
		using (TimeWarning.New("SendNetworkUpdateImmediate"))
		{
			LogEntry(RustLog.EntryType.Network, 3, "SendNetworkUpdateImmediate");
			InvalidateNetworkCache();
			List<Connection> subscribers = GetSubscribers();
			if (subscribers != null && subscribers.Count > 0)
			{
				for (int i = 0; i < subscribers.Count; i++)
				{
					Connection connection = subscribers[i];
					BasePlayer basePlayer = connection.player as BasePlayer;
					if (!(basePlayer == null) && ShouldNetworkTo(basePlayer))
					{
						SendAsSnapshot(connection);
					}
				}
			}
		}
		base.gameObject.SendOnSendNetworkUpdate(this as BaseEntity);
	}

	public void SendNetworkUpdate_Position()
	{
		if (Rust.Application.isLoading || Rust.Application.isLoadingSave || IsDestroyed || net == null || !isSpawned)
		{
			return;
		}
		using (TimeWarning.New("SendNetworkUpdate_Position"))
		{
			LogEntry(RustLog.EntryType.Network, 3, "SendNetworkUpdate_Position");
			List<Connection> obj = GetSubscribers();
			if (obj == null || obj.Count <= 0)
			{
				return;
			}
			if (ServerOcclusion.OcclusionEnabled && SupportsServerOcclusion())
			{
				List<Connection> list = Facepunch.Pool.Get<List<Connection>>();
				foreach (Connection item in obj)
				{
					BasePlayer basePlayer = item.player as BasePlayer;
					if (!(basePlayer == null) && ShouldNetworkTo(basePlayer))
					{
						list.Add(item);
					}
				}
				obj = list;
			}
			if (obj.Count > 0)
			{
				SendDemoTransientEntity();
				NetWrite netWrite = Network.Net.sv.StartWrite();
				netWrite.PacketID(Message.Type.EntityPosition);
				netWrite.EntityID(net.ID);
				netWrite.Vector3(GetNetworkPosition());
				netWrite.Vector3(GetNetworkRotation().eulerAngles);
				netWrite.Float(GetNetworkTime());
				NetworkableId uid = parentEntity.uid;
				if (uid.IsValid)
				{
					netWrite.EntityID(uid);
				}
				SendInfo sendInfo = new SendInfo(obj);
				sendInfo.method = SendMethod.ReliableUnordered;
				sendInfo.priority = Priority.Immediate;
				SendInfo info = sendInfo;
				netWrite.Send(info);
			}
			if (ServerOcclusion.OcclusionEnabled && SupportsServerOcclusion())
			{
				Facepunch.Pool.FreeUnmanaged(ref obj);
			}
		}
	}

	public void ToStream(Stream stream, SaveInfo saveInfo)
	{
		using (saveInfo.msg = Facepunch.Pool.Get<ProtoBuf.Entity>())
		{
			Save(saveInfo);
			if (saveInfo.msg.baseEntity == null)
			{
				Debug.LogError(this?.ToString() + ": ToStream - no BaseEntity!?");
			}
			if (saveInfo.msg.baseNetworkable == null)
			{
				Debug.LogError(this?.ToString() + ": ToStream - no baseNetworkable!?");
			}
			Interface.CallHook("IOnEntitySaved", this, saveInfo);
			saveInfo.msg.WriteToStream(stream);
			PostSave(saveInfo);
		}
	}

	public virtual bool CanUseNetworkCache(Connection connection)
	{
		return ConVar.Server.netcache;
	}

	public void ToStreamForNetwork(Stream stream, SaveInfo saveInfo)
	{
		if (!CanUseNetworkCache(saveInfo.forConnection))
		{
			ToStream(stream, saveInfo);
			return;
		}
		if (_NetworkCache == null)
		{
			_NetworkCache = ((EntityMemoryStreamPool.Count > 0) ? (_NetworkCache = EntityMemoryStreamPool.Dequeue()) : new MemoryStream(8));
			ToStream(_NetworkCache, saveInfo);
			ConVar.Server.netcachesize += (int)_NetworkCache.Length;
		}
		_NetworkCache.WriteTo(stream);
	}

	public void InvalidateNetworkCache()
	{
		using (TimeWarning.New("InvalidateNetworkCache"))
		{
			if (_SaveCache != null)
			{
				ConVar.Server.savecachesize -= (int)_SaveCache.Length;
				_SaveCache.SetLength(0L);
				_SaveCache.Position = 0L;
				EntityMemoryStreamPool.Enqueue(_SaveCache);
				_SaveCache = null;
			}
			if (_NetworkCache != null)
			{
				ConVar.Server.netcachesize -= (int)_NetworkCache.Length;
				_NetworkCache.SetLength(0L);
				_NetworkCache.Position = 0L;
				EntityMemoryStreamPool.Enqueue(_NetworkCache);
				_NetworkCache = null;
			}
			LogEntry(RustLog.EntryType.Network, 3, "InvalidateNetworkCache");
		}
	}

	public MemoryStream GetSaveCache()
	{
		if (_SaveCache == null)
		{
			if (EntityMemoryStreamPool.Count > 0)
			{
				_SaveCache = EntityMemoryStreamPool.Dequeue();
			}
			else
			{
				_SaveCache = new MemoryStream(8);
			}
			SaveInfo saveInfo = new SaveInfo
			{
				forDisk = true
			};
			ToStream(_SaveCache, saveInfo);
			ConVar.Server.savecachesize += (int)_SaveCache.Length;
		}
		return _SaveCache;
	}

	public virtual void UpdateNetworkGroup()
	{
		Assert.IsTrue(isServer, "UpdateNetworkGroup called on clientside entity!");
		if (net == null)
		{
			return;
		}
		using (TimeWarning.New("UpdateGroups"))
		{
			if (net.UpdateGroups(base.transform.position))
			{
				SendNetworkGroupChange();
			}
		}
	}

	public virtual bool SupportsServerOcclusion()
	{
		return false;
	}
}
