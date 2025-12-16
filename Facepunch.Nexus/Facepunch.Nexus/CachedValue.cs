using System;
using System.Collections.Generic;
using Facepunch.Nexus.Time;

namespace Facepunch.Nexus;

internal abstract class CachedValue
{
	internal const double CacheExpirySeconds = 30.0;

	protected readonly IClockProvider Clock;

	protected CachedValue(IClockProvider clock)
	{
		Clock = clock ?? throw new ArgumentNullException("clock");
	}
}
internal class CachedValue<TValue> : CachedValue
{
	private double _expiryTime;

	private bool _hasValue;

	private TValue _value;

	public CachedValue(IClockProvider clock)
		: base(clock)
	{
	}

	public bool TryGetValue(out TValue value)
	{
		if (!_hasValue || Clock.Timestamp >= _expiryTime)
		{
			value = default(TValue);
			return false;
		}
		value = _value;
		return true;
	}

	public ref readonly TValue Update(in TValue value)
	{
		if (value != null)
		{
			_hasValue = true;
			_value = value;
			_expiryTime = Clock.Timestamp + 30.0;
		}
		else
		{
			Invalidate();
		}
		return ref value;
	}

	public void Invalidate()
	{
		_hasValue = false;
		_value = default(TValue);
		_expiryTime = 0.0;
	}
}
internal class CachedValue<TKey, TValue> : CachedValue where TKey : IEquatable<TKey>
{
	private readonly Dictionary<TKey, (TValue Value, double Expiry)> _values = new Dictionary<TKey, (TValue, double)>();

	public CachedValue(IClockProvider clock)
		: base(clock)
	{
	}

	public bool TryGetValue(in TKey key, out TValue value)
	{
		if (!_values.TryGetValue(key, out (TValue, double) value2) || Clock.Timestamp >= value2.Item2)
		{
			value = default(TValue);
			return false;
		}
		(value, _) = value2;
		return true;
	}

	public ref readonly TValue Update(in TKey key, in TValue value)
	{
		if (value != null)
		{
			_values[key] = (value, Clock.Timestamp + 30.0);
		}
		else
		{
			Invalidate(in key);
		}
		return ref value;
	}

	public void Invalidate(in TKey key)
	{
		_values.Remove(key);
	}
}
