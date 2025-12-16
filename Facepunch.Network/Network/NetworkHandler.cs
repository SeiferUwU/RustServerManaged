using System.Collections.Generic;
using Network.Visibility;

namespace Network;

public interface NetworkHandler
{
	void OnNetworkSubscribersEnter(List<Connection> connections);

	void OnNetworkSubscribersLeave(List<Connection> connections);

	void OnNetworkGroupChange();

	void OnNetworkGroupLeave(Group group);

	void OnNetworkGroupEnter(Group group);
}
