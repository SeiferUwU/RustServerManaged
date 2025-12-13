using System;
using System.Diagnostics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

[assembly: AssemblyProduct("LZ4pn")]
[assembly: TargetFramework(".NETPortable,Version=v4.0,Profile=Profile92", FrameworkDisplayName = ".NET Portable Subset")]
[assembly: AssemblyTitle("LZ4pn")]
[assembly: AssemblyDescription("LZ4 compression codec (Portable/Unsafe)")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Milosz Krajewski")]
[assembly: NeutralResourcesLanguage("en")]
[assembly: AssemblyCopyright("Copyright (c) 2015, Milosz Krajewski")]
[assembly: AssemblyTrademark("")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyFileVersion("1.0.10.93")]
[assembly: CompilationRelaxations(8)]
[assembly: AssemblyVersion("1.0.10.93")]
namespace LZ4pn;

public static class LZ4Codec
{
	private class LZ4HC_Data_Structure
	{
		public unsafe byte* src_base;

		public unsafe byte* nextToUpdate;

		public int[] hashTable;

		public ushort[] chainTable;
	}

	private const int MEMORY_USAGE = 14;

	private const int NOTCOMPRESSIBLE_DETECTIONLEVEL = 6;

	private const int MINMATCH = 4;

	private const int SKIPSTRENGTH = 6;

	private const int COPYLENGTH = 8;

	private const int LASTLITERALS = 5;

	private const int MFLIMIT = 12;

	private const int MINLENGTH = 13;

	private const int MAXD_LOG = 16;

	private const int MAXD = 65536;

	private const int MAXD_MASK = 65535;

	private const int MAX_DISTANCE = 65535;

	private const int ML_BITS = 4;

	private const int ML_MASK = 15;

	private const int RUN_BITS = 4;

	private const int RUN_MASK = 15;

	private const int STEPSIZE_64 = 8;

	private const int STEPSIZE_32 = 4;

	private const int LZ4_64KLIMIT = 65547;

	private const int HASH_LOG = 12;

	private const int HASH_TABLESIZE = 4096;

	private const int HASH_ADJUST = 20;

	private const int HASH64K_LOG = 13;

	private const int HASH64K_TABLESIZE = 8192;

	private const int HASH64K_ADJUST = 19;

	private const int HASHHC_LOG = 15;

	private const int HASHHC_TABLESIZE = 32768;

	private const int HASHHC_ADJUST = 17;

	private const int MAX_NB_ATTEMPTS = 256;

	private const int OPTIMAL_ML = 18;

	private static readonly int[] DECODER_TABLE_32 = new int[8] { 0, 3, 2, 3, 0, 0, 0, 0 };

	private static readonly int[] DECODER_TABLE_64 = new int[8] { 0, 0, 0, -1, 0, 1, 2, 3 };

	private static readonly int[] DEBRUIJN_TABLE_32 = new int[32]
	{
		0, 0, 3, 0, 3, 1, 3, 0, 3, 2,
		2, 1, 3, 2, 0, 1, 3, 3, 1, 2,
		2, 2, 2, 0, 3, 1, 2, 0, 1, 0,
		1, 1
	};

	private static readonly int[] DEBRUIJN_TABLE_64 = new int[64]
	{
		0, 0, 0, 0, 0, 1, 1, 2, 0, 3,
		1, 3, 1, 4, 2, 7, 0, 2, 3, 6,
		1, 5, 3, 5, 1, 3, 4, 4, 2, 5,
		6, 7, 7, 0, 1, 2, 3, 3, 4, 6,
		2, 6, 5, 5, 3, 4, 5, 6, 7, 1,
		2, 4, 6, 4, 4, 5, 7, 2, 6, 5,
		7, 6, 7, 7
	};

	public static int MaximumOutputLength(int inputLength)
	{
		return inputLength + inputLength / 255 + 16;
	}

	internal static void CheckArguments(byte[] input, int inputOffset, ref int inputLength, byte[] output, int outputOffset, ref int outputLength)
	{
		if (inputLength < 0)
		{
			inputLength = input.Length - inputOffset;
		}
		if (inputLength == 0)
		{
			outputLength = 0;
			return;
		}
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (inputOffset < 0 || inputOffset + inputLength > input.Length)
		{
			throw new ArgumentException("inputOffset and inputLength are invalid for given input");
		}
		if (outputLength < 0)
		{
			outputLength = output.Length - outputOffset;
		}
		if (output == null)
		{
			throw new ArgumentNullException("output");
		}
		if (outputOffset >= 0 && outputOffset + outputLength <= output.Length)
		{
			return;
		}
		throw new ArgumentException("outputOffset and outputLength are invalid for given output");
	}

	private unsafe static void BlockCopy(byte* src, byte* dst, int len)
	{
		while (len >= 8)
		{
			*(long*)dst = *(long*)src;
			dst += 8;
			src += 8;
			len -= 8;
		}
		if (len >= 4)
		{
			*(int*)dst = *(int*)src;
			dst += 4;
			src += 4;
			len -= 4;
		}
		if (len >= 2)
		{
			*(short*)dst = *(short*)src;
			dst += 2;
			src += 2;
			len -= 2;
		}
		if (len >= 1)
		{
			*dst = *src;
		}
	}

	private unsafe static void BlockFill(byte* dst, int len, byte val)
	{
		if (len >= 8)
		{
			ulong num = val;
			num |= num << 8;
			num |= num << 16;
			num |= num << 32;
			do
			{
				*(ulong*)dst = num;
				dst += 8;
				len -= 8;
			}
			while (len >= 8);
		}
		while (len-- > 0)
		{
			*(dst++) = val;
		}
	}

	public unsafe static int Encode32(byte* input, byte* output, int inputLength, int outputLength)
	{
		if (inputLength < 65547)
		{
			ushort[] array = new ushort[8192];
			fixed (ushort* hash_table = &array[0])
			{
				return LZ4_compress64kCtx_32(hash_table, input, output, inputLength, outputLength);
			}
		}
		byte*[] array2 = new byte*[4096];
		fixed (byte** hash_table2 = &array2[0])
		{
			return LZ4_compressCtx_32(hash_table2, input, output, inputLength, outputLength);
		}
	}

	public unsafe static int Encode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
	{
		CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
		if (outputLength == 0)
		{
			return 0;
		}
		fixed (byte* input2 = &input[inputOffset])
		{
			fixed (byte* output2 = &output[outputOffset])
			{
				return Encode32(input2, output2, inputLength, outputLength);
			}
		}
	}

	public static byte[] Encode32(byte[] input, int inputOffset, int inputLength)
	{
		if (inputLength < 0)
		{
			inputLength = input.Length - inputOffset;
		}
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (inputOffset < 0 || inputOffset + inputLength > input.Length)
		{
			throw new ArgumentException("inputOffset and inputLength are invalid for given input");
		}
		byte[] array = new byte[MaximumOutputLength(inputLength)];
		int num = Encode32(input, inputOffset, inputLength, array, 0, array.Length);
		if (num != array.Length)
		{
			if (num < 0)
			{
				throw new InvalidOperationException("Compression has been corrupted");
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}
		return array;
	}

	public unsafe static int Decode32(byte* input, int inputLength, byte* output, int outputLength, bool knownOutputLength)
	{
		if (knownOutputLength)
		{
			int num = LZ4_uncompress_32(input, output, outputLength);
			if (num != inputLength)
			{
				throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
			}
			return outputLength;
		}
		int num2 = LZ4_uncompress_unknownOutputSize_32(input, output, inputLength, outputLength);
		if (num2 < 0)
		{
			throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
		}
		return num2;
	}

	public unsafe static int Decode32(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
	{
		CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
		if (outputLength == 0)
		{
			return 0;
		}
		fixed (byte* input2 = &input[inputOffset])
		{
			fixed (byte* output2 = &output[outputOffset])
			{
				return Decode32(input2, inputLength, output2, outputLength, knownOutputLength);
			}
		}
	}

	public static byte[] Decode32(byte[] input, int inputOffset, int inputLength, int outputLength)
	{
		if (inputLength < 0)
		{
			inputLength = input.Length - inputOffset;
		}
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (inputOffset < 0 || inputOffset + inputLength > input.Length)
		{
			throw new ArgumentException("inputOffset and inputLength are invalid for given input");
		}
		byte[] array = new byte[outputLength];
		int num = Decode32(input, inputOffset, inputLength, array, 0, outputLength, knownOutputLength: true);
		if (num != outputLength)
		{
			throw new ArgumentException("outputLength is not valid");
		}
		return array;
	}

	public unsafe static int Encode64(byte* input, byte* output, int inputLength, int outputLength)
	{
		if (inputLength < 65547)
		{
			ushort[] array = new ushort[8192];
			fixed (ushort* hash_table = &array[0])
			{
				return LZ4_compress64kCtx_64(hash_table, input, output, inputLength, outputLength);
			}
		}
		uint[] array2 = new uint[4096];
		fixed (uint* hash_table2 = &array2[0])
		{
			return LZ4_compressCtx_64(hash_table2, input, output, inputLength, outputLength);
		}
	}

	public unsafe static int Encode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
	{
		CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
		if (outputLength == 0)
		{
			return 0;
		}
		fixed (byte* input2 = &input[inputOffset])
		{
			fixed (byte* output2 = &output[outputOffset])
			{
				return Encode64(input2, output2, inputLength, outputLength);
			}
		}
	}

	public static byte[] Encode64(byte[] input, int inputOffset, int inputLength)
	{
		if (inputLength < 0)
		{
			inputLength = input.Length - inputOffset;
		}
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (inputOffset < 0 || inputOffset + inputLength > input.Length)
		{
			throw new ArgumentException("inputOffset and inputLength are invalid for given input");
		}
		byte[] array = new byte[MaximumOutputLength(inputLength)];
		int num = Encode64(input, inputOffset, inputLength, array, 0, array.Length);
		if (num != array.Length)
		{
			if (num < 0)
			{
				throw new InvalidOperationException("Compression has been corrupted");
			}
			byte[] array2 = new byte[num];
			Buffer.BlockCopy(array, 0, array2, 0, num);
			return array2;
		}
		return array;
	}

	public unsafe static int Decode64(byte* input, int inputLength, byte* output, int outputLength, bool knownOutputLength)
	{
		if (knownOutputLength)
		{
			int num = LZ4_uncompress_64(input, output, outputLength);
			if (num != inputLength)
			{
				throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
			}
			return outputLength;
		}
		int num2 = LZ4_uncompress_unknownOutputSize_64(input, output, inputLength, outputLength);
		if (num2 < 0)
		{
			throw new ArgumentException("LZ4 block is corrupted, or invalid length has been given.");
		}
		return num2;
	}

	public unsafe static int Decode64(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
	{
		CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
		if (outputLength == 0)
		{
			return 0;
		}
		fixed (byte* input2 = &input[inputOffset])
		{
			fixed (byte* output2 = &output[outputOffset])
			{
				return Decode64(input2, inputLength, output2, outputLength, knownOutputLength);
			}
		}
	}

	public static byte[] Decode64(byte[] input, int inputOffset, int inputLength, int outputLength)
	{
		if (inputLength < 0)
		{
			inputLength = input.Length - inputOffset;
		}
		if (input == null)
		{
			throw new ArgumentNullException("input");
		}
		if (inputOffset < 0 || inputOffset + inputLength > input.Length)
		{
			throw new ArgumentException("inputOffset and inputLength are invalid for given input");
		}
		byte[] array = new byte[outputLength];
		int num = Decode64(input, inputOffset, inputLength, array, 0, outputLength, knownOutputLength: true);
		if (num != outputLength)
		{
			throw new ArgumentException("outputLength is not valid");
		}
		return array;
	}

	private unsafe static LZ4HC_Data_Structure LZ4HC_Create(byte* src)
	{
		LZ4HC_Data_Structure lZ4HC_Data_Structure = new LZ4HC_Data_Structure();
		lZ4HC_Data_Structure.hashTable = new int[32768];
		lZ4HC_Data_Structure.chainTable = new ushort[65536];
		LZ4HC_Data_Structure lZ4HC_Data_Structure2 = lZ4HC_Data_Structure;
		fixed (ushort* dst = &lZ4HC_Data_Structure2.chainTable[0])
		{
			BlockFill((byte*)dst, 131072, byte.MaxValue);
		}
		lZ4HC_Data_Structure2.src_base = src;
		lZ4HC_Data_Structure2.nextToUpdate = src + 1;
		return lZ4HC_Data_Structure2;
	}

	private unsafe static int LZ4_compressHC_32(byte* input, byte* output, int inputLength, int outputLength)
	{
		return LZ4_compressHCCtx_32(LZ4HC_Create(input), input, output, inputLength, outputLength);
	}

	public unsafe static int Encode32HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
	{
		if (inputLength == 0)
		{
			return 0;
		}
		CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
		fixed (byte* input2 = &input[inputOffset])
		{
			fixed (byte* output2 = &output[outputOffset])
			{
				int num = LZ4_compressHC_32(input2, output2, inputLength, outputLength);
				return (num <= 0) ? (-1) : num;
			}
		}
	}

	public static byte[] Encode32HC(byte[] input, int inputOffset, int inputLength)
	{
		if (inputLength == 0)
		{
			return new byte[0];
		}
		int num = MaximumOutputLength(inputLength);
		byte[] array = new byte[num];
		int num2 = Encode32HC(input, inputOffset, inputLength, array, 0, num);
		if (num2 < 0)
		{
			throw new ArgumentException("Provided data seems to be corrupted.");
		}
		if (num2 != num)
		{
			byte[] array2 = new byte[num2];
			Buffer.BlockCopy(array, 0, array2, 0, num2);
			array = array2;
		}
		return array;
	}

	private unsafe static int LZ4_compressHC_64(byte* input, byte* output, int inputLength, int outputLength)
	{
		return LZ4_compressHCCtx_64(LZ4HC_Create(input), input, output, inputLength, outputLength);
	}

	public unsafe static int Encode64HC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
	{
		if (inputLength == 0)
		{
			return 0;
		}
		CheckArguments(input, inputOffset, ref inputLength, output, outputOffset, ref outputLength);
		fixed (byte* input2 = &input[inputOffset])
		{
			fixed (byte* output2 = &output[outputOffset])
			{
				int num = LZ4_compressHC_64(input2, output2, inputLength, outputLength);
				return (num <= 0) ? (-1) : num;
			}
		}
	}

	public static byte[] Encode64HC(byte[] input, int inputOffset, int inputLength)
	{
		if (inputLength == 0)
		{
			return new byte[0];
		}
		int num = MaximumOutputLength(inputLength);
		byte[] array = new byte[num];
		int num2 = Encode64HC(input, inputOffset, inputLength, array, 0, num);
		if (num2 < 0)
		{
			throw new ArgumentException("Provided data seems to be corrupted.");
		}
		if (num2 != num)
		{
			byte[] array2 = new byte[num2];
			Buffer.BlockCopy(array, 0, array2, 0, num2);
			array = array2;
		}
		return array;
	}

	private unsafe static int LZ4_compressCtx_32(byte** hash_table, byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		fixed (int* ptr = &DEBRUIJN_TABLE_32[0])
		{
			byte* ptr2 = src;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2 + src_len;
			byte* ptr5 = ptr4 - 12;
			byte* ptr6 = dst;
			byte* ptr7 = ptr6 + dst_maxlen;
			byte* ptr8 = ptr4 - 5;
			byte* ptr9 = ptr8 - 1;
			byte* ptr10 = ptr8 - 3;
			byte* ptr11 = ptr7 - 6;
			byte* ptr12 = ptr7 - 8;
			if (src_len >= 13)
			{
				hash_table[(uint)((int)(*(uint*)ptr2) * -1640531535 >>> 20)] = ptr2;
				ptr2++;
				uint num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 20;
				while (true)
				{
					int num2 = 67;
					byte* ptr13 = ptr2;
					byte* ptr14;
					while (true)
					{
						uint num3 = num;
						int num4 = num2++ >> 6;
						ptr2 = ptr13;
						ptr13 = ptr2 + num4;
						if (ptr13 > ptr5)
						{
							break;
						}
						num = (uint)((int)(*(uint*)ptr13) * -1640531535) >> 20;
						ptr14 = hash_table[num3];
						hash_table[num3] = ptr2;
						if (ptr14 < ptr2 - 65535 || *(uint*)ptr14 != *(uint*)ptr2)
						{
							continue;
						}
						goto IL_00f5;
					}
					break;
					IL_01d8:
					byte* ptr15;
					int num6;
					while (true)
					{
						*(ushort*)ptr6 = (ushort)(ptr2 - ptr14);
						ptr6 += 2;
						ptr2 += 4;
						ptr14 += 4;
						ptr3 = ptr2;
						while (true)
						{
							if (ptr2 < ptr10)
							{
								int num5 = *(int*)ptr14 ^ *(int*)ptr2;
								if (num5 == 0)
								{
									ptr2 += 4;
									ptr14 += 4;
									continue;
								}
								ptr2 += ptr[(uint)((num5 & -num5) * 125613361 >>> 27)];
								break;
							}
							if (ptr2 < ptr9 && *(ushort*)ptr14 == *(ushort*)ptr2)
							{
								ptr2 += 2;
								ptr14 += 2;
							}
							if (ptr2 < ptr8 && *ptr14 == *ptr2)
							{
								ptr2++;
							}
							break;
						}
						num6 = (int)(ptr2 - ptr3);
						if (ptr6 + (num6 >> 8) > ptr11)
						{
							return 0;
						}
						if (num6 >= 15)
						{
							byte* intPtr = ptr15;
							*intPtr += 15;
							for (num6 -= 15; num6 > 509; num6 -= 510)
							{
								*(ptr6++) = byte.MaxValue;
								*(ptr6++) = byte.MaxValue;
							}
							if (num6 > 254)
							{
								num6 -= 255;
								*(ptr6++) = byte.MaxValue;
							}
							*(ptr6++) = (byte)num6;
						}
						else
						{
							byte* intPtr2 = ptr15;
							*intPtr2 += (byte)num6;
						}
						if (ptr2 > ptr5)
						{
							break;
						}
						hash_table[(uint)((int)(*(uint*)(ptr2 - 2)) * -1640531535 >>> 20)] = ptr2 - 2;
						uint num3 = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 20;
						ptr14 = hash_table[num3];
						hash_table[num3] = ptr2;
						if (ptr14 > ptr2 - 65536 && *(uint*)ptr14 == *(uint*)ptr2)
						{
							ptr15 = ptr6++;
							*ptr15 = 0;
							continue;
						}
						goto IL_0378;
					}
					ptr3 = ptr2;
					break;
					IL_00f5:
					while (ptr2 > ptr3 && ptr14 > src && ptr2[-1] == ptr14[-1])
					{
						ptr2--;
						ptr14--;
					}
					num6 = (int)(ptr2 - ptr3);
					ptr15 = ptr6++;
					if (ptr6 + num6 + (num6 >> 8) > ptr12)
					{
						return 0;
					}
					if (num6 >= 15)
					{
						int num7 = num6 - 15;
						*ptr15 = 240;
						if (num7 > 254)
						{
							do
							{
								*(ptr6++) = byte.MaxValue;
								num7 -= 255;
							}
							while (num7 > 254);
							*(ptr6++) = (byte)num7;
							BlockCopy(ptr3, ptr6, num6);
							ptr6 += num6;
							goto IL_01d8;
						}
						*(ptr6++) = (byte)num7;
					}
					else
					{
						*ptr15 = (byte)(num6 << 4);
					}
					byte* ptr16 = ptr6 + num6;
					do
					{
						*(int*)ptr6 = *(int*)ptr3;
						ptr6 += 4;
						ptr3 += 4;
						*(int*)ptr6 = *(int*)ptr3;
						ptr6 += 4;
						ptr3 += 4;
					}
					while (ptr6 < ptr16);
					ptr6 = ptr16;
					goto IL_01d8;
					IL_0378:
					ptr3 = ptr2++;
					num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 20;
				}
			}
			int num8 = (int)(ptr4 - ptr3);
			if (ptr6 + num8 + 1 + (num8 + 255 - 15) / 255 > ptr7)
			{
				return 0;
			}
			if (num8 >= 15)
			{
				*(ptr6++) = 240;
				for (num8 -= 15; num8 > 254; num8 -= 255)
				{
					*(ptr6++) = byte.MaxValue;
				}
				*(ptr6++) = (byte)num8;
			}
			else
			{
				*(ptr6++) = (byte)(num8 << 4);
			}
			BlockCopy(ptr3, ptr6, (int)(ptr4 - ptr3));
			ptr6 += ptr4 - ptr3;
			return (int)(ptr6 - dst);
		}
	}

	private unsafe static int LZ4_compress64kCtx_32(ushort* hash_table, byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		fixed (int* ptr = &DEBRUIJN_TABLE_32[0])
		{
			byte* ptr2 = src;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2;
			byte* ptr5 = ptr2 + src_len;
			byte* ptr6 = ptr5 - 12;
			byte* ptr7 = dst;
			byte* ptr8 = ptr7 + dst_maxlen;
			byte* ptr9 = ptr5 - 5;
			byte* ptr10 = ptr9 - 1;
			byte* ptr11 = ptr9 - 3;
			byte* ptr12 = ptr8 - 6;
			byte* ptr13 = ptr8 - 8;
			if (src_len >= 13)
			{
				ptr2++;
				uint num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 19;
				while (true)
				{
					int num2 = 67;
					byte* ptr14 = ptr2;
					byte* ptr15;
					while (true)
					{
						uint num3 = num;
						int num4 = num2++ >> 6;
						ptr2 = ptr14;
						ptr14 = ptr2 + num4;
						if (ptr14 > ptr6)
						{
							break;
						}
						num = (uint)((int)(*(uint*)ptr14) * -1640531535) >> 19;
						ptr15 = ptr4 + (int)hash_table[num3];
						hash_table[num3] = (ushort)(ptr2 - ptr4);
						if (*(uint*)ptr15 != *(uint*)ptr2)
						{
							continue;
						}
						goto IL_00cf;
					}
					break;
					IL_01b2:
					byte* ptr16;
					while (true)
					{
						*(ushort*)ptr7 = (ushort)(ptr2 - ptr15);
						ptr7 += 2;
						ptr2 += 4;
						ptr15 += 4;
						ptr3 = ptr2;
						while (true)
						{
							if (ptr2 < ptr11)
							{
								int num5 = *(int*)ptr15 ^ *(int*)ptr2;
								if (num5 == 0)
								{
									ptr2 += 4;
									ptr15 += 4;
									continue;
								}
								ptr2 += ptr[(uint)((num5 & -num5) * 125613361 >>> 27)];
								break;
							}
							if (ptr2 < ptr10 && *(ushort*)ptr15 == *(ushort*)ptr2)
							{
								ptr2 += 2;
								ptr15 += 2;
							}
							if (ptr2 < ptr9 && *ptr15 == *ptr2)
							{
								ptr2++;
							}
							break;
						}
						int num6 = (int)(ptr2 - ptr3);
						if (ptr7 + (num6 >> 8) > ptr12)
						{
							return 0;
						}
						if (num6 >= 15)
						{
							byte* intPtr = ptr16;
							*intPtr += 15;
							for (num6 -= 15; num6 > 509; num6 -= 510)
							{
								*(ptr7++) = byte.MaxValue;
								*(ptr7++) = byte.MaxValue;
							}
							if (num6 > 254)
							{
								num6 -= 255;
								*(ptr7++) = byte.MaxValue;
							}
							*(ptr7++) = (byte)num6;
						}
						else
						{
							byte* intPtr2 = ptr16;
							*intPtr2 += (byte)num6;
						}
						if (ptr2 > ptr6)
						{
							break;
						}
						hash_table[(uint)((int)(*(uint*)(ptr2 - 2)) * -1640531535 >>> 19)] = (ushort)(ptr2 - 2 - ptr4);
						uint num3 = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 19;
						ptr15 = ptr4 + (int)hash_table[num3];
						hash_table[num3] = (ushort)(ptr2 - ptr4);
						if (*(uint*)ptr15 == *(uint*)ptr2)
						{
							ptr16 = ptr7++;
							*ptr16 = 0;
							continue;
						}
						goto IL_033f;
					}
					ptr3 = ptr2;
					break;
					IL_00cf:
					while (ptr2 > ptr3 && ptr15 > src && ptr2[-1] == ptr15[-1])
					{
						ptr2--;
						ptr15--;
					}
					int num7 = (int)(ptr2 - ptr3);
					ptr16 = ptr7++;
					if (ptr7 + num7 + (num7 >> 8) > ptr13)
					{
						return 0;
					}
					if (num7 >= 15)
					{
						int num6 = num7 - 15;
						*ptr16 = 240;
						if (num6 > 254)
						{
							do
							{
								*(ptr7++) = byte.MaxValue;
								num6 -= 255;
							}
							while (num6 > 254);
							*(ptr7++) = (byte)num6;
							BlockCopy(ptr3, ptr7, num7);
							ptr7 += num7;
							goto IL_01b2;
						}
						*(ptr7++) = (byte)num6;
					}
					else
					{
						*ptr16 = (byte)(num7 << 4);
					}
					byte* ptr17 = ptr7 + num7;
					do
					{
						*(int*)ptr7 = *(int*)ptr3;
						ptr7 += 4;
						ptr3 += 4;
						*(int*)ptr7 = *(int*)ptr3;
						ptr7 += 4;
						ptr3 += 4;
					}
					while (ptr7 < ptr17);
					ptr7 = ptr17;
					goto IL_01b2;
					IL_033f:
					ptr3 = ptr2++;
					num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 19;
				}
			}
			int num8 = (int)(ptr5 - ptr3);
			if (ptr7 + num8 + 1 + (num8 - 15 + 255) / 255 > ptr8)
			{
				return 0;
			}
			if (num8 >= 15)
			{
				*(ptr7++) = 240;
				for (num8 -= 15; num8 > 254; num8 -= 255)
				{
					*(ptr7++) = byte.MaxValue;
				}
				*(ptr7++) = (byte)num8;
			}
			else
			{
				*(ptr7++) = (byte)(num8 << 4);
			}
			BlockCopy(ptr3, ptr7, (int)(ptr5 - ptr3));
			ptr7 += ptr5 - ptr3;
			return (int)(ptr7 - dst);
		}
	}

	private unsafe static int LZ4_uncompress_32(byte* src, byte* dst, int dst_len)
	{
		fixed (int* ptr = &DECODER_TABLE_32[0])
		{
			byte* ptr2 = src;
			byte* ptr3 = dst;
			byte* ptr4 = ptr3 + dst_len;
			byte* ptr5 = ptr4 - 5;
			byte* ptr6 = ptr4 - 8;
			byte* ptr7 = ptr4 - 8;
			while (true)
			{
				uint num = *(ptr2++);
				int num2;
				if ((num2 = (int)(num >> 4)) == 15)
				{
					int num3;
					while ((num3 = *(ptr2++)) == 255)
					{
						num2 += 255;
					}
					num2 += num3;
				}
				byte* ptr8 = ptr3 + num2;
				if (ptr8 > ptr6)
				{
					if (ptr8 != ptr4)
					{
						break;
					}
					BlockCopy(ptr2, ptr3, num2);
					ptr2 += num2;
					return (int)(ptr2 - src);
				}
				do
				{
					*(int*)ptr3 = *(int*)ptr2;
					ptr3 += 4;
					ptr2 += 4;
					*(int*)ptr3 = *(int*)ptr2;
					ptr3 += 4;
					ptr2 += 4;
				}
				while (ptr3 < ptr8);
				ptr2 -= ptr3 - ptr8;
				ptr3 = ptr8;
				byte* ptr9 = ptr8 - (int)(*(ushort*)ptr2);
				ptr2 += 2;
				if (ptr9 < dst)
				{
					break;
				}
				if ((num2 = (int)(num & 0xF)) == 15)
				{
					while (*ptr2 == byte.MaxValue)
					{
						ptr2++;
						num2 += 255;
					}
					num2 += *(ptr2++);
				}
				if (ptr3 - ptr9 < 4)
				{
					*ptr3 = *ptr9;
					ptr3[1] = ptr9[1];
					ptr3[2] = ptr9[2];
					ptr3[3] = ptr9[3];
					ptr3 += 4;
					ptr9 += 4;
					ptr9 -= ptr[ptr3 - ptr9];
					*(int*)ptr3 = *(int*)ptr9;
					ptr3 = ptr3;
					ptr9 = ptr9;
				}
				else
				{
					*(int*)ptr3 = *(int*)ptr9;
					ptr3 += 4;
					ptr9 += 4;
				}
				ptr8 = ptr3 + num2;
				if (ptr8 > ptr7)
				{
					if (ptr8 > ptr5)
					{
						break;
					}
					do
					{
						*(int*)ptr3 = *(int*)ptr9;
						ptr3 += 4;
						ptr9 += 4;
						*(int*)ptr3 = *(int*)ptr9;
						ptr3 += 4;
						ptr9 += 4;
					}
					while (ptr3 < ptr6);
					while (ptr3 < ptr8)
					{
						*(ptr3++) = *(ptr9++);
					}
					ptr3 = ptr8;
				}
				else
				{
					do
					{
						*(int*)ptr3 = *(int*)ptr9;
						ptr3 += 4;
						ptr9 += 4;
						*(int*)ptr3 = *(int*)ptr9;
						ptr3 += 4;
						ptr9 += 4;
					}
					while (ptr3 < ptr8);
					ptr3 = ptr8;
				}
			}
			return (int)(-(ptr2 - src));
		}
	}

	private unsafe static int LZ4_uncompress_unknownOutputSize_32(byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		fixed (int* ptr = &DECODER_TABLE_32[0])
		{
			byte* ptr2 = src;
			byte* ptr3 = ptr2 + src_len;
			byte* ptr4 = dst;
			byte* ptr5 = ptr4 + dst_maxlen;
			byte* ptr6 = ptr3 - 8;
			byte* ptr7 = ptr3 - 6;
			byte* ptr8 = ptr5 - 8;
			byte* ptr9 = ptr5 - 8;
			byte* ptr10 = ptr5 - 5;
			byte* ptr11 = ptr5 - 12;
			if (ptr2 != ptr3)
			{
				while (true)
				{
					uint num = *(ptr2++);
					int num2;
					if ((num2 = (int)(num >> 4)) == 15)
					{
						int num3 = 255;
						while (ptr2 < ptr3 && num3 == 255)
						{
							num3 = *(ptr2++);
							num2 += num3;
						}
					}
					byte* ptr12 = ptr4 + num2;
					if (ptr12 > ptr11 || ptr2 + num2 > ptr6)
					{
						if (ptr12 > ptr5 || ptr2 + num2 != ptr3)
						{
							break;
						}
						BlockCopy(ptr2, ptr4, num2);
						ptr4 += num2;
						return (int)(ptr4 - dst);
					}
					do
					{
						*(int*)ptr4 = *(int*)ptr2;
						ptr4 += 4;
						ptr2 += 4;
						*(int*)ptr4 = *(int*)ptr2;
						ptr4 += 4;
						ptr2 += 4;
					}
					while (ptr4 < ptr12);
					ptr2 -= ptr4 - ptr12;
					ptr4 = ptr12;
					byte* ptr13 = ptr12 - (int)(*(ushort*)ptr2);
					ptr2 += 2;
					if (ptr13 < dst)
					{
						break;
					}
					if ((num2 = (int)(num & 0xF)) == 15)
					{
						while (ptr2 < ptr7)
						{
							int num4 = *(ptr2++);
							num2 += num4;
							if (num4 != 255)
							{
								break;
							}
						}
					}
					if (ptr4 - ptr13 < 4)
					{
						*ptr4 = *ptr13;
						ptr4[1] = ptr13[1];
						ptr4[2] = ptr13[2];
						ptr4[3] = ptr13[3];
						ptr4 += 4;
						ptr13 += 4;
						ptr13 -= ptr[ptr4 - ptr13];
						*(int*)ptr4 = *(int*)ptr13;
						ptr4 = ptr4;
						ptr13 = ptr13;
					}
					else
					{
						*(int*)ptr4 = *(int*)ptr13;
						ptr4 += 4;
						ptr13 += 4;
					}
					ptr12 = ptr4 + num2;
					if (ptr12 > ptr9)
					{
						if (ptr12 > ptr10)
						{
							break;
						}
						do
						{
							*(int*)ptr4 = *(int*)ptr13;
							ptr4 += 4;
							ptr13 += 4;
							*(int*)ptr4 = *(int*)ptr13;
							ptr4 += 4;
							ptr13 += 4;
						}
						while (ptr4 < ptr8);
						while (ptr4 < ptr12)
						{
							*(ptr4++) = *(ptr13++);
						}
						ptr4 = ptr12;
					}
					else
					{
						do
						{
							*(int*)ptr4 = *(int*)ptr13;
							ptr4 += 4;
							ptr13 += 4;
							*(int*)ptr4 = *(int*)ptr13;
							ptr4 += 4;
							ptr13 += 4;
						}
						while (ptr4 < ptr12);
						ptr4 = ptr12;
					}
				}
			}
			return (int)(-(ptr2 - src));
		}
	}

	private unsafe static void LZ4HC_Insert_32(LZ4HC_Data_Structure hc4, byte* src_p)
	{
		fixed (ushort* chainTable = hc4.chainTable)
		{
			fixed (int* hashTable = hc4.hashTable)
			{
				byte* src_base = hc4.src_base;
				while (hc4.nextToUpdate < src_p)
				{
					byte* nextToUpdate = hc4.nextToUpdate;
					int num = (int)(nextToUpdate - (hashTable[(uint)((int)(*(uint*)nextToUpdate) * -1640531535 >>> 17)] + src_base));
					if (num > 65535)
					{
						num = 65535;
					}
					chainTable[(int)nextToUpdate & 0xFFFF] = (ushort)num;
					hashTable[(uint)((int)(*(uint*)nextToUpdate) * -1640531535 >>> 17)] = (int)(nextToUpdate - src_base);
					hc4.nextToUpdate++;
				}
			}
		}
	}

	private unsafe static int LZ4HC_CommonLength_32(byte* p1, byte* p2, byte* src_LASTLITERALS)
	{
		fixed (int* dEBRUIJN_TABLE_ = DEBRUIJN_TABLE_32)
		{
			byte* ptr = p1;
			while (ptr < src_LASTLITERALS - 3)
			{
				int num = *(int*)p2 ^ *(int*)ptr;
				if (num == 0)
				{
					ptr += 4;
					p2 += 4;
					continue;
				}
				ptr += dEBRUIJN_TABLE_[(uint)((num & -num) * 125613361 >>> 27)];
				return (int)(ptr - p1);
			}
			if (ptr < src_LASTLITERALS - 1 && *(ushort*)p2 == *(ushort*)ptr)
			{
				ptr += 2;
				p2 += 2;
			}
			if (ptr < src_LASTLITERALS && *p2 == *ptr)
			{
				ptr++;
			}
			return (int)(ptr - p1);
		}
	}

	private unsafe static int LZ4HC_InsertAndFindBestMatch_32(LZ4HC_Data_Structure hc4, byte* src_p, byte* src_LASTLITERALS, ref byte* matchpos)
	{
		fixed (ushort* chainTable = hc4.chainTable)
		{
			fixed (int* hashTable = hc4.hashTable)
			{
				byte* src_base = hc4.src_base;
				int num = 256;
				int num2 = 0;
				int num3 = 0;
				ushort num4 = 0;
				LZ4HC_Insert_32(hc4, src_p);
				byte* ptr = hashTable[(uint)((int)(*(uint*)src_p) * -1640531535 >>> 17)] + src_base;
				if (ptr >= src_p - 4)
				{
					if (*(uint*)ptr == *(uint*)src_p)
					{
						num4 = (ushort)(src_p - ptr);
						num2 = (num3 = LZ4HC_CommonLength_32(src_p + 4, ptr + 4, src_LASTLITERALS) + 4);
						matchpos = ptr;
					}
					ptr -= (int)chainTable[(int)ptr & 0xFFFF];
				}
				while (ptr >= src_p - 65535 && num != 0)
				{
					num--;
					if (ptr[num3] == src_p[num3] && *(uint*)ptr == *(uint*)src_p)
					{
						int num5 = LZ4HC_CommonLength_32(src_p + 4, ptr + 4, src_LASTLITERALS) + 4;
						if (num5 > num3)
						{
							num3 = num5;
							matchpos = ptr;
						}
					}
					ptr -= (int)chainTable[(int)ptr & 0xFFFF];
				}
				if (num2 != 0)
				{
					byte* ptr2 = src_p;
					byte* ptr3;
					for (ptr3 = src_p + num2 - 3; ptr2 < ptr3 - (int)num4; ptr2++)
					{
						chainTable[(int)ptr2 & 0xFFFF] = num4;
					}
					do
					{
						chainTable[(int)ptr2 & 0xFFFF] = num4;
						hashTable[(uint)((int)(*(uint*)ptr2) * -1640531535 >>> 17)] = (int)(ptr2 - src_base);
						ptr2++;
					}
					while (ptr2 < ptr3);
					hc4.nextToUpdate = ptr3;
				}
				return num3;
			}
		}
	}

	private unsafe static int LZ4HC_InsertAndGetWiderMatch_32(LZ4HC_Data_Structure hc4, byte* src_p, byte* startLimit, byte* src_LASTLITERALS, int longest, ref byte* matchpos, ref byte* startpos)
	{
		fixed (ushort* chainTable = hc4.chainTable)
		{
			fixed (int* hashTable = hc4.hashTable)
			{
				fixed (int* dEBRUIJN_TABLE_ = DEBRUIJN_TABLE_32)
				{
					byte* src_base = hc4.src_base;
					int num = 256;
					int num2 = (int)(src_p - startLimit);
					LZ4HC_Insert_32(hc4, src_p);
					byte* ptr = hashTable[(uint)((int)(*(uint*)src_p) * -1640531535 >>> 17)] + src_base;
					while (ptr >= src_p - 65535 && num != 0)
					{
						num--;
						if (startLimit[longest] == (ptr - num2)[longest] && *(uint*)ptr == *(uint*)src_p)
						{
							byte* ptr2 = ptr + 4;
							byte* ptr3 = src_p + 4;
							byte* ptr4 = src_p;
							while (true)
							{
								if (ptr3 < src_LASTLITERALS - 3)
								{
									int num3 = *(int*)ptr2 ^ *(int*)ptr3;
									if (num3 == 0)
									{
										ptr3 += 4;
										ptr2 += 4;
										continue;
									}
									ptr3 += dEBRUIJN_TABLE_[(uint)((num3 & -num3) * 125613361 >>> 27)];
									break;
								}
								if (ptr3 < src_LASTLITERALS - 1 && *(ushort*)ptr2 == *(ushort*)ptr3)
								{
									ptr3 += 2;
									ptr2 += 2;
								}
								if (ptr3 < src_LASTLITERALS && *ptr2 == *ptr3)
								{
									ptr3++;
								}
								break;
							}
							ptr2 = ptr;
							while (ptr4 > startLimit && ptr2 > hc4.src_base && ptr4[-1] == ptr2[-1])
							{
								ptr4--;
								ptr2--;
							}
							if (ptr3 - ptr4 > longest)
							{
								longest = (int)(ptr3 - ptr4);
								matchpos = ptr2;
								startpos = ptr4;
							}
						}
						ptr -= (int)chainTable[(int)ptr & 0xFFFF];
					}
					return longest;
				}
			}
		}
	}

	private unsafe static int LZ4_encodeSequence_32(ref byte* src_p, ref byte* dst_p, ref byte* src_anchor, int matchLength, byte* xxx_ref, byte* dst_end)
	{
		int num = (int)(src_p - src_anchor);
		byte* ptr = dst_p++;
		if (dst_p + num + 8 + (num >> 8) > dst_end)
		{
			return 1;
		}
		int num2;
		if (num >= 15)
		{
			*ptr = 240;
			for (num2 = num - 15; num2 > 254; num2 -= 255)
			{
				*(dst_p++) = byte.MaxValue;
			}
			*(dst_p++) = (byte)num2;
		}
		else
		{
			*ptr = (byte)(num << 4);
		}
		byte* ptr2 = dst_p + num;
		do
		{
			*(int*)dst_p = *(int*)src_anchor;
			dst_p += 4;
			src_anchor += 4;
			*(int*)dst_p = *(int*)src_anchor;
			dst_p += 4;
			src_anchor += 4;
		}
		while (dst_p < ptr2);
		dst_p = ptr2;
		*(ushort*)dst_p = (ushort)(src_p - xxx_ref);
		dst_p += 2;
		num2 = matchLength - 4;
		if (dst_p + 6 + (num >> 8) > dst_end)
		{
			return 1;
		}
		if (num2 >= 15)
		{
			*ptr += 15;
			for (num2 -= 15; num2 > 509; num2 -= 510)
			{
				*(dst_p++) = byte.MaxValue;
				*(dst_p++) = byte.MaxValue;
			}
			if (num2 > 254)
			{
				num2 -= 255;
				*(dst_p++) = byte.MaxValue;
			}
			*(dst_p++) = (byte)num2;
		}
		else
		{
			*ptr += (byte)num2;
		}
		src_p += matchLength;
		src_anchor = src_p;
		return 0;
	}

	private unsafe static int LZ4_compressHCCtx_32(LZ4HC_Data_Structure ctx, byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		byte* ptr = src;
		byte* src_anchor = ptr;
		byte* ptr2 = ptr + src_len;
		byte* ptr3 = ptr2 - 12;
		byte* src_LASTLITERALS = ptr2 - 5;
		byte* dst_p = dst;
		byte* dst_end = dst_p + dst_maxlen;
		byte* matchpos = null;
		byte* startpos = null;
		byte* matchpos2 = null;
		byte* startpos2 = null;
		byte* matchpos3 = null;
		ptr++;
		while (ptr < ptr3)
		{
			int num = LZ4HC_InsertAndFindBestMatch_32(ctx, ptr, src_LASTLITERALS, ref matchpos);
			if (num == 0)
			{
				ptr++;
				continue;
			}
			byte* ptr4 = ptr;
			byte* ptr5 = matchpos;
			int num2 = num;
			while (true)
			{
				int num3 = ((ptr + num < ptr3) ? LZ4HC_InsertAndGetWiderMatch_32(ctx, ptr + num - 2, ptr + 1, src_LASTLITERALS, num, ref matchpos2, ref startpos) : num);
				if (num3 == num)
				{
					if (LZ4_encodeSequence_32(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) == 0)
					{
						break;
					}
					return 0;
				}
				if (ptr4 < ptr && startpos < ptr + num2)
				{
					ptr = ptr4;
					matchpos = ptr5;
					num = num2;
				}
				if (startpos - ptr < 3)
				{
					num = num3;
					ptr = startpos;
					matchpos = matchpos2;
					continue;
				}
				int num6;
				while (true)
				{
					if (startpos - ptr < 18)
					{
						int num4 = num;
						if (num4 > 18)
						{
							num4 = 18;
						}
						if (ptr + num4 > startpos + num3 - 4)
						{
							num4 = (int)(startpos - ptr) + num3 - 4;
						}
						int num5 = num4 - (int)(startpos - ptr);
						if (num5 > 0)
						{
							startpos += num5;
							matchpos2 += num5;
							num3 -= num5;
						}
					}
					num6 = ((startpos + num3 < ptr3) ? LZ4HC_InsertAndGetWiderMatch_32(ctx, startpos + num3 - 3, startpos, src_LASTLITERALS, num3, ref matchpos3, ref startpos2) : num3);
					if (num6 == num3)
					{
						break;
					}
					if (startpos2 < ptr + num + 3)
					{
						if (startpos2 < ptr + num)
						{
							startpos = startpos2;
							matchpos2 = matchpos3;
							num3 = num6;
							continue;
						}
						goto IL_01b8;
					}
					if (startpos < ptr + num)
					{
						if (startpos - ptr < 15)
						{
							if (num > 18)
							{
								num = 18;
							}
							if (ptr + num > startpos + num3 - 4)
							{
								num = (int)(startpos - ptr) + num3 - 4;
							}
							int num7 = num - (int)(startpos - ptr);
							if (num7 > 0)
							{
								startpos += num7;
								matchpos2 += num7;
								num3 -= num7;
							}
						}
						else
						{
							num = (int)(startpos - ptr);
						}
					}
					if (LZ4_encodeSequence_32(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) != 0)
					{
						return 0;
					}
					ptr = startpos;
					matchpos = matchpos2;
					num = num3;
					startpos = startpos2;
					matchpos2 = matchpos3;
					num3 = num6;
				}
				if (startpos < ptr + num)
				{
					num = (int)(startpos - ptr);
				}
				if (LZ4_encodeSequence_32(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) != 0)
				{
					return 0;
				}
				ptr = startpos;
				if (LZ4_encodeSequence_32(ref ptr, ref dst_p, ref src_anchor, num3, matchpos2, dst_end) == 0)
				{
					break;
				}
				return 0;
				IL_01b8:
				if (startpos < ptr + num)
				{
					int num8 = (int)(ptr + num - startpos);
					startpos += num8;
					matchpos2 += num8;
					num3 -= num8;
					if (num3 < 4)
					{
						startpos = startpos2;
						matchpos2 = matchpos3;
						num3 = num6;
					}
				}
				if (LZ4_encodeSequence_32(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) != 0)
				{
					return 0;
				}
				ptr = startpos2;
				matchpos = matchpos3;
				num = num6;
				ptr4 = startpos;
				ptr5 = matchpos2;
				num2 = num3;
			}
		}
		int num9 = (int)(ptr2 - src_anchor);
		if (dst_p - dst + num9 + 1 + (num9 + 255 - 15) / 255 > (uint)dst_maxlen)
		{
			return 0;
		}
		if (num9 >= 15)
		{
			*(dst_p++) = 240;
			for (num9 -= 15; num9 > 254; num9 -= 255)
			{
				*(dst_p++) = byte.MaxValue;
			}
			*(dst_p++) = (byte)num9;
		}
		else
		{
			*(dst_p++) = (byte)(num9 << 4);
		}
		BlockCopy(src_anchor, dst_p, (int)(ptr2 - src_anchor));
		dst_p += ptr2 - src_anchor;
		return (int)(dst_p - dst);
	}

	private unsafe static int LZ4_compressCtx_64(uint* hash_table, byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		fixed (int* ptr = &DEBRUIJN_TABLE_64[0])
		{
			byte* ptr2 = src;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2;
			byte* ptr5 = ptr2 + src_len;
			byte* ptr6 = ptr5 - 12;
			byte* ptr7 = dst;
			byte* ptr8 = ptr7 + dst_maxlen;
			byte* ptr9 = ptr5 - 5;
			byte* ptr10 = ptr9 - 1;
			byte* ptr11 = ptr9 - 3;
			byte* ptr12 = ptr9 - 7;
			byte* ptr13 = ptr8 - 6;
			byte* ptr14 = ptr8 - 8;
			if (src_len >= 13)
			{
				hash_table[(uint)((int)(*(uint*)ptr2) * -1640531535 >>> 20)] = (uint)(ptr2 - ptr3);
				ptr2++;
				uint num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 20;
				while (true)
				{
					int num2 = 67;
					byte* ptr15 = ptr2;
					byte* ptr16;
					while (true)
					{
						uint num3 = num;
						int num4 = num2++ >> 6;
						ptr2 = ptr15;
						ptr15 = ptr2 + num4;
						if (ptr15 > ptr6)
						{
							break;
						}
						num = (uint)((int)(*(uint*)ptr15) * -1640531535) >> 20;
						ptr16 = ptr3 + (int)hash_table[num3];
						hash_table[num3] = (uint)(ptr2 - ptr3);
						if (ptr16 < ptr2 - 65535 || *(uint*)ptr16 != *(uint*)ptr2)
						{
							continue;
						}
						goto IL_00f8;
					}
					break;
					IL_01d0:
					byte* ptr17;
					int num6;
					while (true)
					{
						*(ushort*)ptr7 = (ushort)(ptr2 - ptr16);
						ptr7 += 2;
						ptr2 += 4;
						ptr16 += 4;
						ptr4 = ptr2;
						while (true)
						{
							if (ptr2 < ptr12)
							{
								long num5 = *(long*)ptr16 ^ *(long*)ptr2;
								if (num5 == 0)
								{
									ptr2 += 8;
									ptr16 += 8;
									continue;
								}
								ptr2 += ptr[(num5 & -num5) * 151050438428048703L >>> 58];
								break;
							}
							if (ptr2 < ptr11 && *(uint*)ptr16 == *(uint*)ptr2)
							{
								ptr2 += 4;
								ptr16 += 4;
							}
							if (ptr2 < ptr10 && *(ushort*)ptr16 == *(ushort*)ptr2)
							{
								ptr2 += 2;
								ptr16 += 2;
							}
							if (ptr2 < ptr9 && *ptr16 == *ptr2)
							{
								ptr2++;
							}
							break;
						}
						num6 = (int)(ptr2 - ptr4);
						if (ptr7 + (num6 >> 8) > ptr13)
						{
							return 0;
						}
						if (num6 >= 15)
						{
							byte* intPtr = ptr17;
							*intPtr += 15;
							for (num6 -= 15; num6 > 509; num6 -= 510)
							{
								*(ptr7++) = byte.MaxValue;
								*(ptr7++) = byte.MaxValue;
							}
							if (num6 > 254)
							{
								num6 -= 255;
								*(ptr7++) = byte.MaxValue;
							}
							*(ptr7++) = (byte)num6;
						}
						else
						{
							byte* intPtr2 = ptr17;
							*intPtr2 += (byte)num6;
						}
						if (ptr2 > ptr6)
						{
							break;
						}
						hash_table[(uint)((int)(*(uint*)(ptr2 - 2)) * -1640531535 >>> 20)] = (uint)(ptr2 - 2 - ptr3);
						uint num3 = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 20;
						ptr16 = ptr3 + (int)hash_table[num3];
						hash_table[num3] = (uint)(ptr2 - ptr3);
						if (ptr16 > ptr2 - 65536 && *(uint*)ptr16 == *(uint*)ptr2)
						{
							ptr17 = ptr7++;
							*ptr17 = 0;
							continue;
						}
						goto IL_038b;
					}
					ptr4 = ptr2;
					break;
					IL_00f8:
					while (ptr2 > ptr4 && ptr16 > src && ptr2[-1] == ptr16[-1])
					{
						ptr2--;
						ptr16--;
					}
					num6 = (int)(ptr2 - ptr4);
					ptr17 = ptr7++;
					if (ptr7 + num6 + (num6 >> 8) > ptr14)
					{
						return 0;
					}
					if (num6 >= 15)
					{
						int num7 = num6 - 15;
						*ptr17 = 240;
						if (num7 > 254)
						{
							do
							{
								*(ptr7++) = byte.MaxValue;
								num7 -= 255;
							}
							while (num7 > 254);
							*(ptr7++) = (byte)num7;
							BlockCopy(ptr4, ptr7, num6);
							ptr7 += num6;
							goto IL_01d0;
						}
						*(ptr7++) = (byte)num7;
					}
					else
					{
						*ptr17 = (byte)(num6 << 4);
					}
					byte* ptr18 = ptr7 + num6;
					do
					{
						*(long*)ptr7 = *(long*)ptr4;
						ptr7 += 8;
						ptr4 += 8;
					}
					while (ptr7 < ptr18);
					ptr7 = ptr18;
					goto IL_01d0;
					IL_038b:
					ptr4 = ptr2++;
					num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 20;
				}
			}
			int num8 = (int)(ptr5 - ptr4);
			if (ptr7 + num8 + 1 + (num8 + 255 - 15) / 255 > ptr8)
			{
				return 0;
			}
			if (num8 >= 15)
			{
				*(ptr7++) = 240;
				for (num8 -= 15; num8 > 254; num8 -= 255)
				{
					*(ptr7++) = byte.MaxValue;
				}
				*(ptr7++) = (byte)num8;
			}
			else
			{
				*(ptr7++) = (byte)(num8 << 4);
			}
			BlockCopy(ptr4, ptr7, (int)(ptr5 - ptr4));
			ptr7 += ptr5 - ptr4;
			return (int)(ptr7 - dst);
		}
	}

	private unsafe static int LZ4_compress64kCtx_64(ushort* hash_table, byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		fixed (int* ptr = &DEBRUIJN_TABLE_64[0])
		{
			byte* ptr2 = src;
			byte* ptr3 = ptr2;
			byte* ptr4 = ptr2;
			byte* ptr5 = ptr2 + src_len;
			byte* ptr6 = ptr5 - 12;
			byte* ptr7 = dst;
			byte* ptr8 = ptr7 + dst_maxlen;
			byte* ptr9 = ptr5 - 5;
			byte* ptr10 = ptr9 - 1;
			byte* ptr11 = ptr9 - 3;
			byte* ptr12 = ptr9 - 7;
			byte* ptr13 = ptr8 - 6;
			byte* ptr14 = ptr8 - 8;
			if (src_len >= 13)
			{
				ptr2++;
				uint num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 19;
				while (true)
				{
					int num2 = 67;
					byte* ptr15 = ptr2;
					byte* ptr16;
					while (true)
					{
						uint num3 = num;
						int num4 = num2++ >> 6;
						ptr2 = ptr15;
						ptr15 = ptr2 + num4;
						if (ptr15 > ptr6)
						{
							break;
						}
						num = (uint)((int)(*(uint*)ptr15) * -1640531535) >> 19;
						ptr16 = ptr4 + (int)hash_table[num3];
						hash_table[num3] = (ushort)(ptr2 - ptr4);
						if (*(uint*)ptr16 != *(uint*)ptr2)
						{
							continue;
						}
						goto IL_00d6;
					}
					break;
					IL_01a8:
					byte* ptr17;
					while (true)
					{
						*(ushort*)ptr7 = (ushort)(ptr2 - ptr16);
						ptr7 += 2;
						ptr2 += 4;
						ptr16 += 4;
						ptr3 = ptr2;
						while (true)
						{
							if (ptr2 < ptr12)
							{
								long num5 = *(long*)ptr16 ^ *(long*)ptr2;
								if (num5 == 0)
								{
									ptr2 += 8;
									ptr16 += 8;
									continue;
								}
								ptr2 += ptr[(num5 & -num5) * 151050438428048703L >>> 58];
								break;
							}
							if (ptr2 < ptr11 && *(uint*)ptr16 == *(uint*)ptr2)
							{
								ptr2 += 4;
								ptr16 += 4;
							}
							if (ptr2 < ptr10 && *(ushort*)ptr16 == *(ushort*)ptr2)
							{
								ptr2 += 2;
								ptr16 += 2;
							}
							if (ptr2 < ptr9 && *ptr16 == *ptr2)
							{
								ptr2++;
							}
							break;
						}
						int num6 = (int)(ptr2 - ptr3);
						if (ptr7 + (num6 >> 8) > ptr13)
						{
							return 0;
						}
						if (num6 >= 15)
						{
							byte* intPtr = ptr17;
							*intPtr += 15;
							for (num6 -= 15; num6 > 509; num6 -= 510)
							{
								*(ptr7++) = byte.MaxValue;
								*(ptr7++) = byte.MaxValue;
							}
							if (num6 > 254)
							{
								num6 -= 255;
								*(ptr7++) = byte.MaxValue;
							}
							*(ptr7++) = (byte)num6;
						}
						else
						{
							byte* intPtr2 = ptr17;
							*intPtr2 += (byte)num6;
						}
						if (ptr2 > ptr6)
						{
							break;
						}
						hash_table[(uint)((int)(*(uint*)(ptr2 - 2)) * -1640531535 >>> 19)] = (ushort)(ptr2 - 2 - ptr4);
						uint num3 = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 19;
						ptr16 = ptr4 + (int)hash_table[num3];
						hash_table[num3] = (ushort)(ptr2 - ptr4);
						if (*(uint*)ptr16 == *(uint*)ptr2)
						{
							ptr17 = ptr7++;
							*ptr17 = 0;
							continue;
						}
						goto IL_0354;
					}
					ptr3 = ptr2;
					break;
					IL_00d6:
					while (ptr2 > ptr3 && ptr16 > src && ptr2[-1] == ptr16[-1])
					{
						ptr2--;
						ptr16--;
					}
					int num7 = (int)(ptr2 - ptr3);
					ptr17 = ptr7++;
					if (ptr7 + num7 + (num7 >> 8) > ptr14)
					{
						return 0;
					}
					if (num7 >= 15)
					{
						int num6 = num7 - 15;
						*ptr17 = 240;
						if (num6 > 254)
						{
							do
							{
								*(ptr7++) = byte.MaxValue;
								num6 -= 255;
							}
							while (num6 > 254);
							*(ptr7++) = (byte)num6;
							BlockCopy(ptr3, ptr7, num7);
							ptr7 += num7;
							goto IL_01a8;
						}
						*(ptr7++) = (byte)num6;
					}
					else
					{
						*ptr17 = (byte)(num7 << 4);
					}
					byte* ptr18 = ptr7 + num7;
					do
					{
						*(long*)ptr7 = *(long*)ptr3;
						ptr7 += 8;
						ptr3 += 8;
					}
					while (ptr7 < ptr18);
					ptr7 = ptr18;
					goto IL_01a8;
					IL_0354:
					ptr3 = ptr2++;
					num = (uint)((int)(*(uint*)ptr2) * -1640531535) >> 19;
				}
			}
			int num8 = (int)(ptr5 - ptr3);
			if (ptr7 + num8 + 1 + (num8 - 15 + 255) / 255 > ptr8)
			{
				return 0;
			}
			if (num8 >= 15)
			{
				*(ptr7++) = 240;
				for (num8 -= 15; num8 > 254; num8 -= 255)
				{
					*(ptr7++) = byte.MaxValue;
				}
				*(ptr7++) = (byte)num8;
			}
			else
			{
				*(ptr7++) = (byte)(num8 << 4);
			}
			BlockCopy(ptr3, ptr7, (int)(ptr5 - ptr3));
			ptr7 += ptr5 - ptr3;
			return (int)(ptr7 - dst);
		}
	}

	private unsafe static int LZ4_uncompress_64(byte* src, byte* dst, int dst_len)
	{
		fixed (int* ptr = &DECODER_TABLE_32[0])
		{
			fixed (int* ptr2 = &DECODER_TABLE_64[0])
			{
				byte* ptr3 = src;
				byte* ptr4 = dst;
				byte* ptr5 = ptr4 + dst_len;
				byte* ptr6 = ptr5 - 5;
				byte* ptr7 = ptr5 - 8;
				byte* ptr8 = ptr5 - 8 - 4;
				while (true)
				{
					byte b = *(ptr3++);
					int num;
					if ((num = b >> 4) == 15)
					{
						int num2;
						while ((num2 = *(ptr3++)) == 255)
						{
							num += 255;
						}
						num += num2;
					}
					byte* ptr9 = ptr4 + num;
					if (ptr9 > ptr7)
					{
						if (ptr9 != ptr5)
						{
							break;
						}
						BlockCopy(ptr3, ptr4, num);
						ptr3 += num;
						return (int)(ptr3 - src);
					}
					do
					{
						*(long*)ptr4 = *(long*)ptr3;
						ptr4 += 8;
						ptr3 += 8;
					}
					while (ptr4 < ptr9);
					ptr3 -= ptr4 - ptr9;
					ptr4 = ptr9;
					byte* ptr10 = ptr9 - (int)(*(ushort*)ptr3);
					ptr3 += 2;
					if (ptr10 < dst)
					{
						break;
					}
					if ((num = b & 0xF) == 15)
					{
						while (*ptr3 == byte.MaxValue)
						{
							ptr3++;
							num += 255;
						}
						num += *(ptr3++);
					}
					if (ptr4 - ptr10 < 8)
					{
						int num3 = ptr2[ptr4 - ptr10];
						*ptr4 = *ptr10;
						ptr4[1] = ptr10[1];
						ptr4[2] = ptr10[2];
						ptr4[3] = ptr10[3];
						ptr4 += 4;
						ptr10 += 4;
						ptr10 -= ptr[ptr4 - ptr10];
						*(int*)ptr4 = *(int*)ptr10;
						ptr4 += 4;
						ptr10 -= num3;
					}
					else
					{
						*(long*)ptr4 = *(long*)ptr10;
						ptr4 += 8;
						ptr10 += 8;
					}
					ptr9 = ptr4 + num - 4;
					if (ptr9 > ptr8)
					{
						if (ptr9 > ptr6)
						{
							break;
						}
						while (ptr4 < ptr7)
						{
							*(long*)ptr4 = *(long*)ptr10;
							ptr4 += 8;
							ptr10 += 8;
						}
						while (ptr4 < ptr9)
						{
							*(ptr4++) = *(ptr10++);
						}
						ptr4 = ptr9;
					}
					else
					{
						do
						{
							*(long*)ptr4 = *(long*)ptr10;
							ptr4 += 8;
							ptr10 += 8;
						}
						while (ptr4 < ptr9);
						ptr4 = ptr9;
					}
				}
				return (int)(-(ptr3 - src));
			}
		}
	}

	private unsafe static int LZ4_uncompress_unknownOutputSize_64(byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		fixed (int* ptr = &DECODER_TABLE_32[0])
		{
			fixed (int* ptr2 = &DECODER_TABLE_64[0])
			{
				byte* ptr3 = src;
				byte* ptr4 = ptr3 + src_len;
				byte* ptr5 = dst;
				byte* ptr6 = ptr5 + dst_maxlen;
				byte* ptr7 = ptr4 - 8;
				byte* ptr8 = ptr4 - 6;
				byte* ptr9 = ptr6 - 8;
				byte* ptr10 = ptr6 - 12;
				byte* ptr11 = ptr6 - 5;
				byte* ptr12 = ptr6 - 12;
				if (ptr3 != ptr4)
				{
					while (true)
					{
						byte b = *(ptr3++);
						int num;
						if ((num = b >> 4) == 15)
						{
							int num2 = 255;
							while (ptr3 < ptr4 && num2 == 255)
							{
								num2 = *(ptr3++);
								num += num2;
							}
						}
						byte* ptr13 = ptr5 + num;
						if (ptr13 > ptr12 || ptr3 + num > ptr7)
						{
							if (ptr13 > ptr6 || ptr3 + num != ptr4)
							{
								break;
							}
							BlockCopy(ptr3, ptr5, num);
							ptr5 += num;
							return (int)(ptr5 - dst);
						}
						do
						{
							*(long*)ptr5 = *(long*)ptr3;
							ptr5 += 8;
							ptr3 += 8;
						}
						while (ptr5 < ptr13);
						ptr3 -= ptr5 - ptr13;
						ptr5 = ptr13;
						byte* ptr14 = ptr13 - (int)(*(ushort*)ptr3);
						ptr3 += 2;
						if (ptr14 < dst)
						{
							break;
						}
						if ((num = b & 0xF) == 15)
						{
							while (ptr3 < ptr8)
							{
								int num3 = *(ptr3++);
								num += num3;
								if (num3 != 255)
								{
									break;
								}
							}
						}
						if (ptr5 - ptr14 < 8)
						{
							int num4 = ptr2[ptr5 - ptr14];
							*ptr5 = *ptr14;
							ptr5[1] = ptr14[1];
							ptr5[2] = ptr14[2];
							ptr5[3] = ptr14[3];
							ptr5 += 4;
							ptr14 += 4;
							ptr14 -= ptr[ptr5 - ptr14];
							*(int*)ptr5 = *(int*)ptr14;
							ptr5 += 4;
							ptr14 -= num4;
						}
						else
						{
							*(long*)ptr5 = *(long*)ptr14;
							ptr5 += 8;
							ptr14 += 8;
						}
						ptr13 = ptr5 + num - 4;
						if (ptr13 > ptr10)
						{
							if (ptr13 > ptr11)
							{
								break;
							}
							while (ptr5 < ptr9)
							{
								*(long*)ptr5 = *(long*)ptr14;
								ptr5 += 8;
								ptr14 += 8;
							}
							while (ptr5 < ptr13)
							{
								*(ptr5++) = *(ptr14++);
							}
							ptr5 = ptr13;
						}
						else
						{
							do
							{
								*(long*)ptr5 = *(long*)ptr14;
								ptr5 += 8;
								ptr14 += 8;
							}
							while (ptr5 < ptr13);
							ptr5 = ptr13;
						}
					}
				}
				return (int)(-(ptr3 - src));
			}
		}
	}

	private unsafe static void LZ4HC_Insert_64(LZ4HC_Data_Structure hc4, byte* src_p)
	{
		fixed (ushort* chainTable = hc4.chainTable)
		{
			fixed (int* hashTable = hc4.hashTable)
			{
				byte* src_base = hc4.src_base;
				while (hc4.nextToUpdate < src_p)
				{
					byte* nextToUpdate = hc4.nextToUpdate;
					int num = (int)(nextToUpdate - (hashTable[(uint)((int)(*(uint*)nextToUpdate) * -1640531535 >>> 17)] + src_base));
					if (num > 65535)
					{
						num = 65535;
					}
					chainTable[(int)nextToUpdate & 0xFFFF] = (ushort)num;
					hashTable[(uint)((int)(*(uint*)nextToUpdate) * -1640531535 >>> 17)] = (int)(nextToUpdate - src_base);
					hc4.nextToUpdate++;
				}
			}
		}
	}

	private unsafe static int LZ4HC_CommonLength_64(byte* p1, byte* p2, byte* src_LASTLITERALS)
	{
		fixed (int* dEBRUIJN_TABLE_ = DEBRUIJN_TABLE_64)
		{
			byte* ptr = p1;
			while (ptr < src_LASTLITERALS - 7)
			{
				long num = *(long*)p2 ^ *(long*)ptr;
				if (num == 0)
				{
					ptr += 8;
					p2 += 8;
					continue;
				}
				ptr += dEBRUIJN_TABLE_[(num & -num) * 151050438428048703L >>> 58];
				return (int)(ptr - p1);
			}
			if (ptr < src_LASTLITERALS - 3 && *(uint*)p2 == *(uint*)ptr)
			{
				ptr += 4;
				p2 += 4;
			}
			if (ptr < src_LASTLITERALS - 1 && *(ushort*)p2 == *(ushort*)ptr)
			{
				ptr += 2;
				p2 += 2;
			}
			if (ptr < src_LASTLITERALS && *p2 == *ptr)
			{
				ptr++;
			}
			return (int)(ptr - p1);
		}
	}

	private unsafe static int LZ4HC_InsertAndFindBestMatch_64(LZ4HC_Data_Structure hc4, byte* src_p, byte* src_LASTLITERALS, ref byte* matchpos)
	{
		fixed (ushort* chainTable = hc4.chainTable)
		{
			fixed (int* hashTable = hc4.hashTable)
			{
				byte* src_base = hc4.src_base;
				int num = 256;
				int num2 = 0;
				int num3 = 0;
				ushort num4 = 0;
				LZ4HC_Insert_64(hc4, src_p);
				byte* ptr = hashTable[(uint)((int)(*(uint*)src_p) * -1640531535 >>> 17)] + src_base;
				if (ptr >= src_p - 4)
				{
					if (*(uint*)ptr == *(uint*)src_p)
					{
						num4 = (ushort)(src_p - ptr);
						num2 = (num3 = LZ4HC_CommonLength_64(src_p + 4, ptr + 4, src_LASTLITERALS) + 4);
						matchpos = ptr;
					}
					ptr -= (int)chainTable[(int)ptr & 0xFFFF];
				}
				while (ptr >= src_p - 65535 && num != 0)
				{
					num--;
					if (ptr[num3] == src_p[num3] && *(uint*)ptr == *(uint*)src_p)
					{
						int num5 = LZ4HC_CommonLength_64(src_p + 4, ptr + 4, src_LASTLITERALS) + 4;
						if (num5 > num3)
						{
							num3 = num5;
							matchpos = ptr;
						}
					}
					ptr -= (int)chainTable[(int)ptr & 0xFFFF];
				}
				if (num2 != 0)
				{
					byte* ptr2 = src_p;
					byte* ptr3;
					for (ptr3 = src_p + num2 - 3; ptr2 < ptr3 - (int)num4; ptr2++)
					{
						chainTable[(int)ptr2 & 0xFFFF] = num4;
					}
					do
					{
						chainTable[(int)ptr2 & 0xFFFF] = num4;
						hashTable[(uint)((int)(*(uint*)ptr2) * -1640531535 >>> 17)] = (int)(ptr2 - src_base);
						ptr2++;
					}
					while (ptr2 < ptr3);
					hc4.nextToUpdate = ptr3;
				}
				return num3;
			}
		}
	}

	private unsafe static int LZ4HC_InsertAndGetWiderMatch_64(LZ4HC_Data_Structure hc4, byte* src_p, byte* startLimit, byte* src_LASTLITERALS, int longest, ref byte* matchpos, ref byte* startpos)
	{
		fixed (ushort* chainTable = hc4.chainTable)
		{
			fixed (int* hashTable = hc4.hashTable)
			{
				fixed (int* dEBRUIJN_TABLE_ = DEBRUIJN_TABLE_64)
				{
					byte* src_base = hc4.src_base;
					int num = 256;
					int num2 = (int)(src_p - startLimit);
					LZ4HC_Insert_64(hc4, src_p);
					byte* ptr = hashTable[(uint)((int)(*(uint*)src_p) * -1640531535 >>> 17)] + src_base;
					while (ptr >= src_p - 65535 && num != 0)
					{
						num--;
						if (startLimit[longest] == (ptr - num2)[longest] && *(uint*)ptr == *(uint*)src_p)
						{
							byte* ptr2 = ptr + 4;
							byte* ptr3 = src_p + 4;
							byte* ptr4 = src_p;
							while (true)
							{
								if (ptr3 < src_LASTLITERALS - 7)
								{
									long num3 = *(long*)ptr2 ^ *(long*)ptr3;
									if (num3 == 0)
									{
										ptr3 += 8;
										ptr2 += 8;
										continue;
									}
									ptr3 += dEBRUIJN_TABLE_[(num3 & -num3) * 151050438428048703L >>> 58];
									break;
								}
								if (ptr3 < src_LASTLITERALS - 3 && *(uint*)ptr2 == *(uint*)ptr3)
								{
									ptr3 += 4;
									ptr2 += 4;
								}
								if (ptr3 < src_LASTLITERALS - 1 && *(ushort*)ptr2 == *(ushort*)ptr3)
								{
									ptr3 += 2;
									ptr2 += 2;
								}
								if (ptr3 < src_LASTLITERALS && *ptr2 == *ptr3)
								{
									ptr3++;
								}
								break;
							}
							ptr2 = ptr;
							while (ptr4 > startLimit && ptr2 > hc4.src_base && ptr4[-1] == ptr2[-1])
							{
								ptr4--;
								ptr2--;
							}
							if (ptr3 - ptr4 > longest)
							{
								longest = (int)(ptr3 - ptr4);
								matchpos = ptr2;
								startpos = ptr4;
							}
						}
						ptr -= (int)chainTable[(int)ptr & 0xFFFF];
					}
					return longest;
				}
			}
		}
	}

	private unsafe static int LZ4_encodeSequence_64(ref byte* src_p, ref byte* dst_p, ref byte* src_anchor, int matchLength, byte* src_ref, byte* dst_end)
	{
		byte* ptr = dst_p++;
		int num = (int)(src_p - src_anchor);
		if (dst_p + num + 8 + (num >> 8) > dst_end)
		{
			return 1;
		}
		int num2;
		if (num >= 15)
		{
			*ptr = 240;
			for (num2 = num - 15; num2 > 254; num2 -= 255)
			{
				*(dst_p++) = byte.MaxValue;
			}
			*(dst_p++) = (byte)num2;
		}
		else
		{
			*ptr = (byte)(num << 4);
		}
		byte* ptr2 = dst_p + num;
		do
		{
			*(long*)dst_p = *(long*)src_anchor;
			dst_p += 8;
			src_anchor += 8;
		}
		while (dst_p < ptr2);
		dst_p = ptr2;
		*(ushort*)dst_p = (ushort)(src_p - src_ref);
		dst_p += 2;
		num2 = matchLength - 4;
		if (dst_p + 6 + (num >> 8) > dst_end)
		{
			return 1;
		}
		if (num2 >= 15)
		{
			*ptr += 15;
			for (num2 -= 15; num2 > 509; num2 -= 510)
			{
				*(dst_p++) = byte.MaxValue;
				*(dst_p++) = byte.MaxValue;
			}
			if (num2 > 254)
			{
				num2 -= 255;
				*(dst_p++) = byte.MaxValue;
			}
			*(dst_p++) = (byte)num2;
		}
		else
		{
			*ptr += (byte)num2;
		}
		src_p += matchLength;
		src_anchor = src_p;
		return 0;
	}

	private unsafe static int LZ4_compressHCCtx_64(LZ4HC_Data_Structure ctx, byte* src, byte* dst, int src_len, int dst_maxlen)
	{
		byte* ptr = src;
		byte* src_anchor = ptr;
		byte* ptr2 = ptr + src_len;
		byte* ptr3 = ptr2 - 12;
		byte* src_LASTLITERALS = ptr2 - 5;
		byte* dst_p = dst;
		byte* dst_end = dst_p + dst_maxlen;
		byte* matchpos = null;
		byte* startpos = null;
		byte* matchpos2 = null;
		byte* startpos2 = null;
		byte* matchpos3 = null;
		ptr++;
		while (ptr < ptr3)
		{
			int num = LZ4HC_InsertAndFindBestMatch_64(ctx, ptr, src_LASTLITERALS, ref matchpos);
			if (num == 0)
			{
				ptr++;
				continue;
			}
			byte* ptr4 = ptr;
			byte* ptr5 = matchpos;
			int num2 = num;
			while (true)
			{
				int num3 = ((ptr + num < ptr3) ? LZ4HC_InsertAndGetWiderMatch_64(ctx, ptr + num - 2, ptr + 1, src_LASTLITERALS, num, ref matchpos2, ref startpos) : num);
				if (num3 == num)
				{
					if (LZ4_encodeSequence_64(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) == 0)
					{
						break;
					}
					return 0;
				}
				if (ptr4 < ptr && startpos < ptr + num2)
				{
					ptr = ptr4;
					matchpos = ptr5;
					num = num2;
				}
				if (startpos - ptr < 3)
				{
					num = num3;
					ptr = startpos;
					matchpos = matchpos2;
					continue;
				}
				int num6;
				while (true)
				{
					if (startpos - ptr < 18)
					{
						int num4 = num;
						if (num4 > 18)
						{
							num4 = 18;
						}
						if (ptr + num4 > startpos + num3 - 4)
						{
							num4 = (int)(startpos - ptr) + num3 - 4;
						}
						int num5 = num4 - (int)(startpos - ptr);
						if (num5 > 0)
						{
							startpos += num5;
							matchpos2 += num5;
							num3 -= num5;
						}
					}
					num6 = ((startpos + num3 < ptr3) ? LZ4HC_InsertAndGetWiderMatch_64(ctx, startpos + num3 - 3, startpos, src_LASTLITERALS, num3, ref matchpos3, ref startpos2) : num3);
					if (num6 == num3)
					{
						break;
					}
					if (startpos2 < ptr + num + 3)
					{
						if (startpos2 < ptr + num)
						{
							startpos = startpos2;
							matchpos2 = matchpos3;
							num3 = num6;
							continue;
						}
						goto IL_01b8;
					}
					if (startpos < ptr + num)
					{
						if (startpos - ptr < 15)
						{
							if (num > 18)
							{
								num = 18;
							}
							if (ptr + num > startpos + num3 - 4)
							{
								num = (int)(startpos - ptr) + num3 - 4;
							}
							int num7 = num - (int)(startpos - ptr);
							if (num7 > 0)
							{
								startpos += num7;
								matchpos2 += num7;
								num3 -= num7;
							}
						}
						else
						{
							num = (int)(startpos - ptr);
						}
					}
					if (LZ4_encodeSequence_64(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) != 0)
					{
						return 0;
					}
					ptr = startpos;
					matchpos = matchpos2;
					num = num3;
					startpos = startpos2;
					matchpos2 = matchpos3;
					num3 = num6;
				}
				if (startpos < ptr + num)
				{
					num = (int)(startpos - ptr);
				}
				if (LZ4_encodeSequence_64(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) != 0)
				{
					return 0;
				}
				ptr = startpos;
				if (LZ4_encodeSequence_64(ref ptr, ref dst_p, ref src_anchor, num3, matchpos2, dst_end) == 0)
				{
					break;
				}
				return 0;
				IL_01b8:
				if (startpos < ptr + num)
				{
					int num8 = (int)(ptr + num - startpos);
					startpos += num8;
					matchpos2 += num8;
					num3 -= num8;
					if (num3 < 4)
					{
						startpos = startpos2;
						matchpos2 = matchpos3;
						num3 = num6;
					}
				}
				if (LZ4_encodeSequence_64(ref ptr, ref dst_p, ref src_anchor, num, matchpos, dst_end) != 0)
				{
					return 0;
				}
				ptr = startpos2;
				matchpos = matchpos3;
				num = num6;
				ptr4 = startpos;
				ptr5 = matchpos2;
				num2 = num3;
			}
		}
		int num9 = (int)(ptr2 - src_anchor);
		if (dst_p - dst + num9 + 1 + (num9 + 255 - 15) / 255 > (uint)dst_maxlen)
		{
			return 0;
		}
		if (num9 >= 15)
		{
			*(dst_p++) = 240;
			for (num9 -= 15; num9 > 254; num9 -= 255)
			{
				*(dst_p++) = byte.MaxValue;
			}
			*(dst_p++) = (byte)num9;
		}
		else
		{
			*(dst_p++) = (byte)(num9 << 4);
		}
		BlockCopy(src_anchor, dst_p, (int)(ptr2 - src_anchor));
		dst_p += ptr2 - src_anchor;
		return (int)(dst_p - dst);
	}
}
