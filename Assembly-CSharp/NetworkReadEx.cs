using Network;

public static class NetworkReadEx
{
	public static BasePlayer Player(this NetRead read)
	{
		return BasePlayer.FindByID(read.UInt64());
	}

	public static BaseEntity Entity(this NetRead read)
	{
		NetworkableId uid = read.EntityID();
		return BaseNetworkable.serverEntities.Find(uid) as BaseEntity;
	}
}
