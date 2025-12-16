using UnityEngine;

namespace Network.Visibility;

public interface Provider
{
	void OnGroupAdded(Group group);

	bool IsInside(Group group, Vector3 vPos);

	Group GetGroup(Vector3 vPos);

	void GetVisibleFromFar(Group group, ListHashSet<Group> groups);

	void GetVisibleFromNear(Group group, ListHashSet<Group> groups);
}
