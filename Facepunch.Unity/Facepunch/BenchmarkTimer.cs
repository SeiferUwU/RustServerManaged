using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Facepunch;

public static class BenchmarkTimer
{
	public struct Scope : IDisposable
	{
		private Stopwatch timer;

		public Scope(Stopwatch timer)
		{
			this.timer = timer;
			this.timer?.Start();
		}

		void IDisposable.Dispose()
		{
			timer?.Stop();
		}
	}

	public static bool Enabled = false;

	public static Dictionary<string, Stopwatch> All = new Dictionary<string, Stopwatch>();

	public static Stopwatch Get(string name)
	{
		if (!Enabled)
		{
			return null;
		}
		if (All.TryGetValue(name, out var value))
		{
			return value;
		}
		value = new Stopwatch();
		All.Add(name, value);
		return value;
	}

	public static Scope Measure(string name)
	{
		if (!Enabled)
		{
			return new Scope(null);
		}
		return new Scope(Get(name));
	}
}
