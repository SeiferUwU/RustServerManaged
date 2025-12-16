using System;

public class PlayerCache : StableObjectCache<BasePlayer>
{
	public ValidView ValidPlayers => new ValidView(this);

	public ReadOnlySpan<BasePlayer> Players => base.Objects;

	public ReadOnlySpan<BaseEntity> AsEntities
	{
		get
		{
			BaseEntity[] objects = base.Objects;
			return objects;
		}
	}

	public PlayerCache(int initialCapacity)
		: base(initialCapacity)
	{
	}
}
