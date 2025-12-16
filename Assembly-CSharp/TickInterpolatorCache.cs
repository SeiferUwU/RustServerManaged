using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TickInterpolatorCache
{
	public struct PlayerInfo
	{
		public int Count;

		public float Length;
	}

	public struct ReadOnlyState
	{
		public readonly NativeArray<TickInterpolator.Segment>.ReadOnly Segments;

		public readonly NativeArray<PlayerInfo>.ReadOnly Infos;

		public readonly int BufferSize;

		public ReadOnlyState(NativeArray<TickInterpolator.Segment>.ReadOnly playerSegments, NativeArray<PlayerInfo>.ReadOnly playerInfos, int bufferSize)
		{
			Segments = playerSegments;
			Infos = playerInfos;
			BufferSize = bufferSize;
		}
	}

	public struct PlayerTickIterator
	{
		private readonly ReadOnlyState state;

		private readonly int playerIndex;

		private Vector3 currPoint;

		private int segmentIndex;

		public Vector3 CurrentPoint => currPoint;

		public Vector3 StartPoint => GetStartPoint(state, playerIndex);

		public Vector3 EndPoint => GetEndPoint(state, playerIndex);

		public float Length => state.Infos[playerIndex].Length;

		public PlayerTickIterator(ReadOnlyState state, int playerIndex)
		{
			this.state = state;
			this.playerIndex = playerIndex;
			segmentIndex = 0;
			currPoint = GetStartPoint(state, playerIndex);
		}

		public bool MoveNext(float distance)
		{
			float num = 0f;
			int num2 = playerIndex * state.BufferSize + 1;
			while (num < distance && HasNext())
			{
				TickInterpolator.Segment segment = state.Segments[num2 + segmentIndex];
				currPoint = segment.point;
				num += segment.length;
				segmentIndex++;
			}
			return num > 0f;
		}

		public void Reset()
		{
			segmentIndex = 0;
			currPoint = StartPoint;
		}

		public bool HasNext()
		{
			return segmentIndex < state.Infos[playerIndex].Count;
		}
	}

	private NativeArray<TickInterpolator.Segment> playerSegments;

	private NativeArray<PlayerInfo> playerInfos;

	private int bufferSize = 9;

	public ReadOnlyState ReadOnly => new ReadOnlyState(playerSegments.AsReadOnly(), playerInfos.AsReadOnly(), bufferSize);

	public TickInterpolatorCache(int capacity = 32)
	{
		playerSegments = new NativeArray<TickInterpolator.Segment>(bufferSize * capacity, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		playerInfos = new NativeArray<PlayerInfo>(capacity, Allocator.Persistent);
	}

	public void Dispose()
	{
		NativeArrayEx.SafeDispose(ref playerSegments);
		NativeArrayEx.SafeDispose(ref playerInfos);
	}

	public void ReplacePlayer(int index)
	{
		playerInfos[index] = default(PlayerInfo);
	}

	public unsafe void AddTick(BasePlayer player, Vector3 point)
	{
		int stableIndex = player.StableIndex;
		int num = player.StableIndex * bufferSize;
		ref PlayerInfo reference = ref UnsafeUtility.ArrayElementAsRef<PlayerInfo>(playerInfos.GetUnsafePtr(), stableIndex);
		int num2 = ++reference.Count;
		Vector3 point2 = playerSegments[num + num2 - 1].point;
		TickInterpolator.Segment value = new TickInterpolator.Segment(point2, point);
		reference.Length += value.length;
		if (num2 >= bufferSize)
		{
			GrowSegments(playerInfos.Length);
		}
		playerSegments[num + num2] = value;
	}

	public unsafe void Reset(BasePlayer player, Vector3 point)
	{
		int stableIndex = player.StableIndex;
		ref PlayerInfo reference = ref UnsafeUtility.ArrayElementAsRef<PlayerInfo>(playerInfos.GetUnsafePtr(), stableIndex);
		reference.Count = 0;
		reference.Length = 0f;
		int index = player.StableIndex * bufferSize;
		playerSegments[index] = new TickInterpolator.Segment(point);
	}

	public void Expand(int newCap)
	{
		int length = playerInfos.Length;
		if (newCap > length)
		{
			NativeArrayEx.Expand(ref playerInfos, newCap);
			GrowSegments(length);
		}
	}

	public static Vector3 GetStartPoint(ReadOnlyState state, int playerIndex)
	{
		return state.Segments[playerIndex * state.BufferSize].point;
	}

	public static Vector3 GetEndPoint(ReadOnlyState state, int playerIndex)
	{
		PlayerInfo info = state.Infos[playerIndex];
		return GetEndPoint(state, playerIndex, info);
	}

	public static Vector3 GetEndPoint(ReadOnlyState state, int playerIndex, PlayerInfo info)
	{
		return state.Segments[playerIndex * state.BufferSize + info.Count].point;
	}

	public void TransformEntries(int playerIndex, in Matrix4x4 matrix)
	{
		PlayerInfo info = playerInfos[playerIndex];
		TransformEntries(playerIndex, info, in matrix);
	}

	public unsafe void TransformEntries(int playerIndex, PlayerInfo info, in Matrix4x4 matrix)
	{
		NativeArray<TickInterpolator.Segment> subArray = playerSegments.GetSubArray(playerIndex * bufferSize, info.Count + 1);
		void* unsafePtr = subArray.GetUnsafePtr();
		for (int i = 0; i < subArray.Length; i++)
		{
			ref TickInterpolator.Segment reference = ref UnsafeUtility.ArrayElementAsRef<TickInterpolator.Segment>(unsafePtr, i);
			reference.point = matrix.MultiplyPoint3x4(reference.point);
		}
	}

	public static PlayerTickIterator GetPlayerTickIterator(ReadOnlyState state, int playerIndex)
	{
		return new PlayerTickIterator(state, playerIndex);
	}

	private void GrowSegments(int oldPlayerCap)
	{
		int length = playerInfos.Length;
		int num = bufferSize;
		if (length == oldPlayerCap)
		{
			bufferSize += 4;
		}
		NativeArray<TickInterpolator.Segment> nativeArray = new NativeArray<TickInterpolator.Segment>(length * bufferSize, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
		for (int i = 0; i < oldPlayerCap; i++)
		{
			int count = playerInfos[i].Count;
			if (count > 0)
			{
				NativeArray<TickInterpolator.Segment> subArray = playerSegments.GetSubArray(i * num, count + 1);
				NativeArray<TickInterpolator.Segment> subArray2 = nativeArray.GetSubArray(i * bufferSize, count + 1);
				subArray.CopyTo(subArray2);
			}
			else
			{
				nativeArray[i * bufferSize] = playerSegments[i * num];
			}
		}
		playerSegments.Dispose();
		playerSegments = nativeArray;
	}
}
