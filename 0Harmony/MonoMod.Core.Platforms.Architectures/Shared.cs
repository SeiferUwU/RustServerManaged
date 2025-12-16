using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using MonoMod.Utils;

namespace MonoMod.Core.Platforms.Architectures;

internal static class Shared
{
	public unsafe static ReadOnlyMemory<IAllocatedMemory> CreateVtableStubs(ISystem system, IntPtr vtableBase, int vtableSize, ReadOnlySpan<byte> stubData, int indexOffs, bool premulOffset)
	{
		int maxSize = system.MemoryAllocator.MaxSize;
		int num = stubData.Length * vtableSize;
		int num2 = num / maxSize;
		int num3 = maxSize / stubData.Length;
		int num4 = num3 * stubData.Length;
		int num5 = num % num4;
		IAllocatedMemory[] array = new IAllocatedMemory[num2 + ((num5 != 0) ? 1 : 0)];
		byte[] array2 = ArrayPool<byte>.Shared.Rent(num4);
		Span<byte> backup = MemoryExtensions.AsSpan(array2);
		Span<byte> span = backup.Slice(0, num4);
		for (int i = 0; i < num3; i++)
		{
			stubData.CopyTo(span.Slice(i * stubData.Length));
		}
		ref IntPtr vtblBase = ref System.Runtime.CompilerServices.Unsafe.AsRef<IntPtr>((void*)vtableBase);
		AllocationRequest allocationRequest = new AllocationRequest(num4);
		allocationRequest.Alignment = IntPtr.Size;
		allocationRequest.Executable = true;
		AllocationRequest allocationRequest2 = allocationRequest;
		for (int j = 0; j < num2; j++)
		{
			Helpers.Assert(system.MemoryAllocator.TryAllocate(allocationRequest2, out IAllocatedMemory allocated), null, "system.MemoryAllocator.TryAllocate(allocReq, out var alloc)");
			array[j] = allocated;
			FillBufferIndicies(stubData.Length, indexOffs, num3, j, span, premulOffset);
			FillVtbl(stubData.Length, num3 * j, ref vtblBase, num3, allocated.BaseAddress);
			IntPtr baseAddress = allocated.BaseAddress;
			ReadOnlySpan<byte> data = span;
			backup = default(Span<byte>);
			system.PatchData(PatchTargetKind.Executable, baseAddress, data, backup);
		}
		if (num5 > 0)
		{
			allocationRequest2 = allocationRequest2 with
			{
				Size = num5
			};
			Helpers.Assert(system.MemoryAllocator.TryAllocate(allocationRequest2, out IAllocatedMemory allocated2), null, "system.MemoryAllocator.TryAllocate(allocReq, out var alloc)");
			array[^1] = allocated2;
			FillBufferIndicies(stubData.Length, indexOffs, num3, num2, span, premulOffset);
			FillVtbl(stubData.Length, num3 * num2, ref vtblBase, num5 / stubData.Length, allocated2.BaseAddress);
			IntPtr baseAddress2 = allocated2.BaseAddress;
			ReadOnlySpan<byte> data2 = span.Slice(0, num5);
			backup = default(Span<byte>);
			system.PatchData(PatchTargetKind.Executable, baseAddress2, data2, backup);
		}
		ArrayPool<byte>.Shared.Return(array2);
		return array;
		static void FillBufferIndicies(int stubSize, int num6, int numPerAlloc, int num8, Span<byte> mainAllocBuf, bool premul)
		{
			for (int k = 0; k < numPerAlloc; k++)
			{
				ref byte destination = ref mainAllocBuf[k * stubSize + num6];
				uint num7 = (uint)(numPerAlloc * num8 + k);
				if (premul)
				{
					num7 *= (uint)IntPtr.Size;
				}
				System.Runtime.CompilerServices.Unsafe.WriteUnaligned(ref destination, num7);
			}
		}
		static void FillVtbl(int stubSize, int baseIndex, ref IntPtr source, int numEntries, nint baseAddr)
		{
			for (int k = 0; k < numEntries; k++)
			{
				System.Runtime.CompilerServices.Unsafe.Add(ref source, baseIndex + k) = baseAddr + stubSize * k;
			}
		}
	}
}
