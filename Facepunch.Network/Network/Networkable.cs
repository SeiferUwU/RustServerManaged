#define UNITY_ASSERTIONS
using System.Collections.Generic;
using Facepunch;
using Facepunch.Extend;
using Network.Visibility;
using Oxide.Core;
using UnityEngine;

namespace Network;

public class Networkable : Pool.IPooled
{
	public NetworkableId ID;

	public Group group;

	public Group secondaryGroup;

	public Subscriber subscriber;

	public NetworkHandler handler;

	public bool updateSubscriptions;

	public Server sv;

	internal Client cl;

	public Connection connection { get; private set; }

	public ISubscriberStrategy SubStrategy { get; set; }

	public void Destroy()
	{
		CloseSubscriber();
		if (ID.IsValid)
		{
			SwitchGroup(null);
			if (sv != null)
			{
				sv.ReturnUID(ID.Value);
			}
		}
	}

	public void EnterPool()
	{
		ID = default(NetworkableId);
		connection = null;
		group = null;
		secondaryGroup = null;
		sv = null;
		cl = null;
		handler = null;
		updateSubscriptions = false;
		SubStrategy = null;
	}

	public void LeavePool()
	{
	}

	public void StartSubscriber()
	{
		if (subscriber != null)
		{
			Debug.Log("BecomeSubscriber called twice!");
			return;
		}
		subscriber = sv.visibility.CreateSubscriber(connection);
		OnSubscriptionChange();
	}

	public void OnConnected(Connection c)
	{
		connection = c;
	}

	public void OnDisconnected()
	{
		connection = null;
		CloseSubscriber();
	}

	public void CloseSubscriber()
	{
		if (subscriber != null)
		{
			sv.visibility.DestroySubscriber(ref subscriber);
		}
	}

	public bool UpdateGroups(Vector3 position)
	{
		Debug.Assert(sv != null, "SV IS NULL");
		Debug.Assert(sv.visibility != null, "sv.visibility IS NULL");
		Group newGroup = sv.visibility.GetGroup(position);
		return SwitchGroup(newGroup);
	}

	public bool SwitchGroup(Group newGroup)
	{
		if (newGroup == group)
		{
			return false;
		}
		using (TimeWarning.New("SwitchGroup"))
		{
			if (group != null)
			{
				using (TimeWarning.New("group.Leave"))
				{
					group.Leave(this);
				}
			}
			Group oldGroup = group;
			group = newGroup;
			if (group != null)
			{
				using (TimeWarning.New("group.Join"))
				{
					group.Join(this);
				}
			}
			using (TimeWarning.New("OnSubscriptionChange"))
			{
				OnSubscriptionChange();
			}
			if (handler != null)
			{
				using (TimeWarning.New("OnNetworkGroupChange"))
				{
					handler.OnNetworkGroupChange();
				}
			}
			using (TimeWarning.New("OnGroupTransition"))
			{
				OnGroupTransition(oldGroup);
			}
		}
		return true;
	}

	public void OnGroupTransition(Group oldGroup)
	{
		if (oldGroup == null)
		{
			if (group != null && handler != null)
			{
				handler.OnNetworkSubscribersEnter(group.subscribers);
			}
			return;
		}
		if (group == null)
		{
			if (oldGroup != null && handler != null)
			{
				handler.OnNetworkSubscribersLeave(oldGroup.subscribers);
			}
			return;
		}
		List<Connection> obj = Pool.Get<List<Connection>>();
		List<Connection> obj2 = Pool.Get<List<Connection>>();
		oldGroup.subscribers.Compare(group.subscribers, obj, obj2, null);
		if (handler != null)
		{
			handler.OnNetworkSubscribersEnter(obj);
		}
		if (handler != null)
		{
			handler.OnNetworkSubscribersLeave(obj2);
		}
		Pool.FreeUnmanaged(ref obj);
		Pool.FreeUnmanaged(ref obj2);
	}

	public void OnSubscriptionChange()
	{
		if (subscriber == null)
		{
			return;
		}
		if (group != null && !subscriber.IsSubscribed(group))
		{
			subscriber.Subscribe(group);
			if (handler != null)
			{
				handler.OnNetworkGroupEnter(group);
			}
		}
		updateSubscriptions = true;
		UpdateHighPrioritySubscriptions();
	}

	public bool SwitchSecondaryGroup(Group newGroup)
	{
		if (newGroup == secondaryGroup)
		{
			return false;
		}
		using (TimeWarning.New("SwitchSecondaryGroup"))
		{
			secondaryGroup = newGroup;
			using (TimeWarning.New("OnSubscriptionChange"))
			{
				OnSubscriptionChange();
			}
		}
		return true;
	}

	public bool UpdateSubscriptions(int removeLimit, int addLimit)
	{
		if (!updateSubscriptions)
		{
			return false;
		}
		if (subscriber == null)
		{
			return false;
		}
		using (TimeWarning.New("UpdateSubscriptions"))
		{
			updateSubscriptions = false;
			List<Group> obj = Pool.Get<List<Group>>();
			List<Group> obj2 = Pool.Get<List<Group>>();
			ListHashSet<Group> obj3 = Pool.Get<ListHashSet<Group>>();
			SubStrategy.GatherSubscriptions(this, obj3);
			ListHashSet<Group>.Compare(subscriber.subscribed, obj3, obj, obj2, null);
			if (Interface.CallHook("OnNetworkSubscriptionsUpdate", this, obj, obj2) != null)
			{
				/*Error: Could not find block for branch target IL_0140*/;
			}
			for (int i = 0; i < obj2.Count; i++)
			{
				Group obj4 = obj2[i];
				if (removeLimit > 0)
				{
					subscriber.Unsubscribe(obj4);
					if (handler != null)
					{
						handler.OnNetworkGroupLeave(obj4);
					}
					removeLimit -= obj4.networkables.Count;
				}
				else
				{
					updateSubscriptions = true;
				}
			}
			for (int j = 0; j < obj.Count; j++)
			{
				Group obj5 = obj[j];
				if (addLimit > 0)
				{
					subscriber.Subscribe(obj5);
					if (handler != null)
					{
						handler.OnNetworkGroupEnter(obj5);
					}
					addLimit -= obj5.networkables.Count;
				}
				else
				{
					updateSubscriptions = true;
				}
			}
			Pool.FreeUnmanaged(ref obj);
			Pool.FreeUnmanaged(ref obj2);
			Pool.FreeUnmanaged(ref obj3);
		}
		return true;
	}

	public bool UpdateHighPrioritySubscriptions()
	{
		if (subscriber == null)
		{
			return false;
		}
		using (TimeWarning.New("UpdateHighPrioritySubscriptions"))
		{
			List<Group> obj = Pool.Get<List<Group>>();
			ListHashSet<Group> obj2 = Pool.Get<ListHashSet<Group>>();
			SubStrategy.GatherHighPrioSubscriptions(this, obj2);
			ListHashSet<Group>.Compare(subscriber.subscribed, obj2, obj, null, null);
			if (Interface.CallHook("OnNetworkSubscriptionsUpdate", this, obj, null) != null)
			{
				/*Error: Could not find block for branch target IL_00a2*/;
			}
			for (int i = 0; i < obj.Count; i++)
			{
				Group obj3 = obj[i];
				subscriber.Subscribe(obj3);
				if (handler != null)
				{
					handler.OnNetworkGroupEnter(obj3);
				}
			}
			Pool.FreeUnmanaged(ref obj);
			Pool.FreeUnmanaged(ref obj2);
		}
		return true;
	}

	public void InvalidateSubscriptions(int maxNewCells)
	{
		if (subscriber == null)
		{
			return;
		}
		bool flag = false;
		using (TimeWarning.New("InvalidateSubscriptions"))
		{
			List<Group> obj = Pool.Get<List<Group>>();
			List<Group> obj2 = Pool.Get<List<Group>>();
			ListHashSet<Group> obj3 = Pool.Get<ListHashSet<Group>>();
			SubStrategy.GatherHighPrioSubscriptions(this, obj3);
			using (TimeWarning.New("Compare"))
			{
				ListHashSet<Group>.Compare(subscriber.subscribed, obj3, obj, obj2, null);
			}
			using (TimeWarning.New("Unsubscribe"))
			{
				for (int i = 0; i < obj2.Count; i++)
				{
					Group obj4 = obj2[i];
					subscriber.Unsubscribe(obj4);
					if (handler != null)
					{
						handler.OnNetworkGroupLeave(obj4);
					}
				}
			}
			using (TimeWarning.New("Subscribe"))
			{
				int num = Mathf.Min(obj.Count, maxNewCells);
				for (int j = 0; j < num; j++)
				{
					Group obj5 = obj[j];
					subscriber.Subscribe(obj5);
					if (handler != null)
					{
						handler.OnNetworkGroupEnter(obj5);
					}
				}
				flag = num == obj.Count;
			}
			Pool.FreeUnmanaged(ref obj);
			Pool.FreeUnmanaged(ref obj2);
			Pool.FreeUnmanaged(ref obj3);
		}
		updateSubscriptions = !flag;
	}
}
