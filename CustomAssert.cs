#define UNITY_ASSERTIONS
using UnityEngine.Assertions;

public static class CustomAssert
{
	public static void That(bool condition, string message = null)
	{
		Assert.IsTrue(condition, message);
	}

	public static void IsOn(BaseEntity entity, string message = null)
	{
		Assert.IsTrue(entity.IsOn(), message ?? (entity.name + " didn't have the On flag"));
	}

	public static void IsOff(BaseEntity entity, string message = null)
	{
		Assert.IsFalse(entity.IsOn(), message ?? (entity.name + " had the On flag"));
	}

	public static void IsBusy(BaseEntity entity, string message = null)
	{
		Assert.IsTrue(entity.IsBusy(), message ?? (entity.name + " didn't have the Busy flag"));
	}

	public static void HasPower(IOEntity entity, string message = null)
	{
		Assert.IsTrue(entity.HasFlag(BaseEntity.Flags.Reserved8), message ?? (entity.name + " didn't have the HasPower flag"));
	}

	public static void HasNoPower(IOEntity entity, string message = null)
	{
		Assert.IsFalse(entity.HasFlag(BaseEntity.Flags.Reserved8), message ?? (entity.name + " did have the HasPower flag"));
	}

	public static void IsGreater(float actual, float expected, string message = null)
	{
		Assert.IsTrue(actual > expected, message ?? $"Expected {actual} to be greater than {expected}.");
	}

	public static void IsGreaterOrEqual(float actual, float expected, string message = null)
	{
		Assert.IsTrue(actual >= expected, message ?? $"Expected {actual} to be at least {expected}, but got {actual}.");
	}

	public static void IsLess(float actual, float expected, string message = null)
	{
		Assert.IsTrue(actual < expected, message ?? $"Expected {actual} to be less than {expected}.");
	}

	public static void IsLessOrEqual(float actual, float expected, string message = null)
	{
		Assert.IsTrue(actual <= expected, message ?? $"Expected {actual} to be at most {expected}, but got {actual}.");
	}
}
