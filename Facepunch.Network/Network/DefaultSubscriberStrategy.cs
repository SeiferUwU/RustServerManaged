using Facepunch;
using Network.Visibility;

namespace Network;

public class DefaultSubscriberStrategy : ISubscriberStrategy
{
	public void GatherHighPrioSubscriptions(Networkable net, ListHashSet<Group> visible)
	{
		net.sv.visibility.GetVisibleFromNear(net.group, visible);
		AddSecondaryGroup(net, visible);
	}

	public void GatherSubscriptions(Networkable net, ListHashSet<Group> visible)
	{
		net.sv.visibility.GetVisibleFromFar(net.group, visible);
		AddSecondaryGroup(net, visible);
	}

	private void AddSecondaryGroup(Networkable net, ListHashSet<Group> groupsVisible)
	{
		Group secondaryGroup = net.secondaryGroup;
		if (secondaryGroup != null)
		{
			ListHashSet<Group> obj = Pool.Get<ListHashSet<Group>>();
			net.sv.visibility.GetVisibleFromNear(secondaryGroup, obj);
			for (int i = 0; i < obj.Count; i++)
			{
				groupsVisible.TryAdd(obj[i]);
			}
			Pool.FreeUnmanaged(ref obj);
		}
	}
}
