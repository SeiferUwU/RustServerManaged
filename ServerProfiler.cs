using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using UnityEngine;

[SuppressUnmanagedCodeSecurity]
public static class ServerProfiler
{
	[StructLayout(LayoutKind.Sequential, Pack = 1, Size = 9)]
	public struct Mark
	{
		public enum Type : byte
		{
			Sync,
			Enter,
			Exit,
			Exception,
			Alloc,
			GCBegin,
			GCEnd,
			AllocWithStack
		}

		public long Timestamp;

		public Type Event;
	}

	public struct Alloc
	{
		public unsafe Native.MonoClass* Class;

		public unsafe Native.MonoMethod* LastMethod;

		public uint AlignedSize;

		public uint FlatArraySize;
	}

	public struct Profile
	{
		public unsafe byte* Data;

		private uint WriteInd;

		public uint Capacity;

		public uint WriteEnd;

		public int ThreadId;

		public long Timestamp;
	}

	[StructLayout(LayoutKind.Sequential, Size = 64)]
	public struct MemoryReading
	{
		public long Timestamp;

		public ulong WorkingSet;

		public ulong VirtualSet;
	}

	public struct MemoryState
	{
		public unsafe MemoryReading* Readings;

		public uint Created;

		public uint Capacity;
	}

	public enum NotifyMetric : byte
	{
		TotalAllocCount,
		TotalMem,
		MainAllocCount,
		MainMem,
		WorkerAllocCount,
		WorkerMem,
		Count
	}

	public static class Native
	{
		[StructLayout(LayoutKind.Explicit)]
		public struct MonoImage
		{
			[FieldOffset(48)]
			public unsafe byte* AssemblyName;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct MonoClass
		{
			private const int ImageOffset = 64;

			[FieldOffset(64)]
			public unsafe MonoImage* Image;

			[FieldOffset(72)]
			public unsafe byte* Name;

			[FieldOffset(80)]
			public unsafe byte* Namespace;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct MonoMethod
		{
			[FieldOffset(8)]
			public unsafe MonoClass* Class;

			[FieldOffset(24)]
			public unsafe byte* Name;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct MonoVTable
		{
			[FieldOffset(0)]
			public unsafe MonoClass* Class;
		}

		[StructLayout(LayoutKind.Explicit)]
		public struct MonoObject
		{
			[FieldOffset(0)]
			public unsafe MonoVTable* VTable;
		}

		public enum StorageType : byte
		{
			FrameLimited,
			FixedBuffer
		}

		[DllImport("ServerProfiler.Core")]
		public static extern void Install();

		[DllImport("ServerProfiler.Core")]
		public static extern void SetStorageType(byte aStorageType);

		[DllImport("ServerProfiler.Core")]
		public static extern bool SetFramesToRecord(byte aFrameCount);

		[DllImport("ServerProfiler.Core")]
		public static extern bool SetFixedBufferCap(uint aMainThreadCap, uint aWorkerThreadCap);

		[DllImport("ServerProfiler.Core")]
		public static extern void TakeSnapshot();

		[DllImport("ServerProfiler.Core")]
		public static extern void StartContinuousProfiling(byte aMaxStackDepth);

		[DllImport("ServerProfiler.Core")]
		public static extern void StopContinuousProfiling();

		[DllImport("ServerProfiler.Core")]
		public static extern void ResumeContinuousProfiling();

		[DllImport("ServerProfiler.Core")]
		public static extern void SetContinuousProfilerNotifySettings(NotifyMetric aSetting, uint aValue);

		[DllImport("ServerProfiler.Core")]
		public static extern bool OnFrameEnd();

		[DllImport("ServerProfiler.Core")]
		public unsafe static extern void GetData(out Profile** profiles, out byte count);

		[DllImport("ServerProfiler.Core")]
		public unsafe static extern void GetMemoryUsage(out MemoryState* state);

		[DllImport("ServerProfiler.Core")]
		public static extern bool ReleaseResources();
	}

	public const byte MaxFrames = 10;

	private static bool canBeActivated;

	private static Action<IList<Profile>, MemoryState> onDoneCallback;

	private static bool isContinuous;

	private static int mainThreadId;

	public static bool IsRunning => onDoneCallback != null;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	public static void Init()
	{
		if (!Environment.CommandLine.Contains("-enableProfiler"))
		{
			Debug.Log("Profiler Disabled!");
			return;
		}
		mainThreadId = Environment.CurrentManagedThreadId;
		Native.Install();
		canBeActivated = true;
		Debug.Log("Profiler Initialized!");
		PostUpdateHook.EndOfFrame = (Action)Delegate.Combine(PostUpdateHook.EndOfFrame, new Action(OnFrameEnd));
	}

	public static void RecordNextFrames(int frames, Action<IList<Profile>, MemoryState> onDone)
	{
		if (onDone != null && IsEnabled())
		{
			onDoneCallback = onDone;
			Native.SetStorageType(0);
			Native.SetFramesToRecord((byte)Math.Clamp(frames, 1, 10));
			Native.TakeSnapshot();
			isContinuous = false;
		}
	}

	public static void RecordIntoBuffer(uint mainThreadCap, uint workerThreadCap, Action<IList<Profile>, MemoryState> onDone)
	{
		if (onDone != null && IsEnabled())
		{
			onDoneCallback = onDone;
			Native.SetStorageType(1);
			Native.SetFixedBufferCap(mainThreadCap, workerThreadCap);
			Native.TakeSnapshot();
			isContinuous = false;
		}
	}

	public static void StartContinuousRecording(byte maxStackDepth, Action<IList<Profile>, MemoryState> onDone)
	{
		if (onDone != null && IsEnabled())
		{
			onDoneCallback = onDone;
			Native.SetStorageType(1);
			Native.SetFixedBufferCap(33554432u, 8388608u);
			Native.StartContinuousProfiling(maxStackDepth);
			isContinuous = true;
		}
	}

	public static void StopContinuousRecording()
	{
		Native.StopContinuousProfiling();
		onDoneCallback = null;
	}

	public static void ResumeContinuousRecording()
	{
		Native.ResumeContinuousProfiling();
	}

	public static void ReleaseResources()
	{
		Native.ReleaseResources();
	}

	public static bool IsEnabled()
	{
		return canBeActivated;
	}

	private unsafe static void OnFrameEnd()
	{
		if (!Native.OnFrameEnd())
		{
			return;
		}
		List<Profile> list = null;
		Native.GetData(out var profiles, out var count);
		list = new List<Profile>(count);
		for (byte b = 0; b < count; b++)
		{
			if (profiles[(int)b]->WriteEnd != 0)
			{
				list.Add(*profiles[(int)b]);
			}
		}
		Native.GetMemoryUsage(out var state);
		MemoryState arg = *state;
		onDoneCallback(list, arg);
		if (!isContinuous)
		{
			onDoneCallback = null;
		}
	}

	public static TimeSpan TimestampToTimespan(long stamp)
	{
		return TimeSpan.FromMilliseconds((double)stamp / 1000000.0);
	}

	public static long TimestampToMicros(long stamp)
	{
		return stamp / 1000;
	}

	public unsafe static void AppendNameTo(Native.MonoMethod* method, StringBuilder builder)
	{
		for (byte* ptr = method->Class->Image->AssemblyName; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
		builder.Append('!');
		for (byte* ptr = method->Class->Name; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
		builder.Append("::");
		for (byte* ptr = method->Name; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
	}

	public unsafe static void AppendNameTo(Native.MonoMethod* method, ProfileExporter.JSON.StringStream builder)
	{
		for (byte* ptr = method->Class->Image->AssemblyName; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
		builder.Append('!');
		for (byte* ptr = method->Class->Name; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
		builder.Append("::");
		for (byte* ptr = method->Name; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
	}

	public unsafe static void SerializeNameTo(Native.MonoMethod* method, MemoryStream stream)
	{
		long position = stream.Position;
		ushort num = 0;
		stream.WriteByte(0);
		stream.WriteByte(0);
		byte* ptr = method->Class->Image->AssemblyName;
		while (*ptr != 0)
		{
			stream.WriteByte(*ptr);
			ptr++;
			num++;
		}
		stream.WriteByte(33);
		num++;
		ptr = method->Class->Name;
		while (*ptr != 0)
		{
			stream.WriteByte(*ptr);
			ptr++;
			num++;
		}
		stream.WriteByte(58);
		stream.WriteByte(58);
		num += 2;
		ptr = method->Name;
		while (*ptr != 0)
		{
			stream.WriteByte(*ptr);
			ptr++;
			num++;
		}
		byte[] buffer = stream.GetBuffer();
		buffer[position] = (byte)(num >> 8);
		buffer[position + 1] = (byte)num;
	}

	public unsafe static void AppendNameTo(Alloc alloc, StringBuilder builder)
	{
		for (byte* ptr = alloc.Class->Image->AssemblyName; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
		builder.Append('!');
		for (byte* ptr = alloc.Class->Name; *ptr != 0; ptr++)
		{
			char c = (char)(*ptr);
			builder.Append(c);
			if (alloc.FlatArraySize != 0 && c == '[')
			{
				builder.Append(alloc.FlatArraySize);
			}
		}
	}

	public unsafe static void AppendNameTo(Alloc alloc, ProfileExporter.JSON.StringStream builder)
	{
		for (byte* ptr = alloc.Class->Image->AssemblyName; *ptr != 0; ptr++)
		{
			builder.Append((char)(*ptr));
		}
		builder.Append('!');
		for (byte* ptr = alloc.Class->Name; *ptr != 0; ptr++)
		{
			char c = (char)(*ptr);
			builder.Append(c);
			if (alloc.FlatArraySize != 0 && c == '[')
			{
				builder.Append(alloc.FlatArraySize);
			}
		}
	}

	public unsafe static void SerializeNameTo(Alloc alloc, MemoryStream stream)
	{
		long position = stream.Position;
		ushort num = 0;
		stream.WriteByte(0);
		stream.WriteByte(0);
		byte* ptr = alloc.Class->Image->AssemblyName;
		while (*ptr != 0)
		{
			stream.WriteByte(*ptr);
			ptr++;
			num++;
		}
		stream.WriteByte(33);
		num++;
		ptr = alloc.Class->Name;
		while (*ptr != 0)
		{
			stream.WriteByte(*ptr);
			ptr++;
			num++;
		}
		byte[] buffer = stream.GetBuffer();
		buffer[position] = (byte)(num >> 8);
		buffer[position + 1] = (byte)num;
	}

	public static int GetMainThreadId()
	{
		return mainThreadId;
	}
}
