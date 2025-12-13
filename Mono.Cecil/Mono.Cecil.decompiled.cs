#define DEBUG
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Metadata;
using Mono.Cecil.PE;
using Mono.Collections.Generic;
using Mono.Security.Cryptography;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: AssemblyProduct("Mono.Cecil")]
[assembly: AssemblyCopyright("Copyright © 2008 - 2018 Jb Evain")]
[assembly: ComVisible(false)]
[assembly: AssemblyFileVersion("0.11.5.0")]
[assembly: AssemblyInformationalVersion("0.11.5.0")]
[assembly: AssemblyTitle("Mono.Cecil")]
[assembly: Guid("fd225bb4-fa53-44b2-a6db-85f5e48dcb54")]
[assembly: InternalsVisibleTo("Mono.Cecil.Tests, PublicKey=00240000048000009400000006020000002400005253413100040000010001002b5c9f7f04346c324a3176f8d3ee823bbf2d60efdbc35f86fd9e65ea3e6cd11bcdcba3a353e55133c8ac5c4caaba581b2c6dfff2cc2d0edc43959ddb86b973300a479a82419ef489c3225f1fe429a708507bd515835160e10bc743d20ca33ab9570cfd68d479fcf0bc797a763bec5d1000f0159ef619e709d915975e87beebaf")]
[assembly: InternalsVisibleTo("Mono.Cecil.Pdb, PublicKey=00240000048000009400000006020000002400005253413100040000010001002b5c9f7f04346c324a3176f8d3ee823bbf2d60efdbc35f86fd9e65ea3e6cd11bcdcba3a353e55133c8ac5c4caaba581b2c6dfff2cc2d0edc43959ddb86b973300a479a82419ef489c3225f1fe429a708507bd515835160e10bc743d20ca33ab9570cfd68d479fcf0bc797a763bec5d1000f0159ef619e709d915975e87beebaf")]
[assembly: InternalsVisibleTo("Mono.Cecil.Mdb, PublicKey=00240000048000009400000006020000002400005253413100040000010001002b5c9f7f04346c324a3176f8d3ee823bbf2d60efdbc35f86fd9e65ea3e6cd11bcdcba3a353e55133c8ac5c4caaba581b2c6dfff2cc2d0edc43959ddb86b973300a479a82419ef489c3225f1fe429a708507bd515835160e10bc743d20ca33ab9570cfd68d479fcf0bc797a763bec5d1000f0159ef619e709d915975e87beebaf")]
[assembly: InternalsVisibleTo("Mono.Cecil.Rocks, PublicKey=00240000048000009400000006020000002400005253413100040000010001002b5c9f7f04346c324a3176f8d3ee823bbf2d60efdbc35f86fd9e65ea3e6cd11bcdcba3a353e55133c8ac5c4caaba581b2c6dfff2cc2d0edc43959ddb86b973300a479a82419ef489c3225f1fe429a708507bd515835160e10bc743d20ca33ab9570cfd68d479fcf0bc797a763bec5d1000f0159ef619e709d915975e87beebaf")]
[assembly: TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName = ".NET Framework 4")]
[assembly: AssemblyVersion("0.11.5.0")]
internal static class Consts
{
	public const string AssemblyName = "Mono.Cecil";

	public const string PublicKey = "00240000048000009400000006020000002400005253413100040000010001002b5c9f7f04346c324a3176f8d3ee823bbf2d60efdbc35f86fd9e65ea3e6cd11bcdcba3a353e55133c8ac5c4caaba581b2c6dfff2cc2d0edc43959ddb86b973300a479a82419ef489c3225f1fe429a708507bd515835160e10bc743d20ca33ab9570cfd68d479fcf0bc797a763bec5d1000f0159ef619e709d915975e87beebaf";
}
namespace Mono
{
	internal static class Disposable
	{
		public static Disposable<T> Owned<T>(T value) where T : class, IDisposable
		{
			return new Disposable<T>(value, owned: true);
		}

		public static Disposable<T> NotOwned<T>(T value) where T : class, IDisposable
		{
			return new Disposable<T>(value, owned: false);
		}
	}
	internal struct Disposable<T> : IDisposable where T : class, IDisposable
	{
		internal readonly T value;

		private readonly bool owned;

		public Disposable(T value, bool owned)
		{
			this.value = value;
			this.owned = owned;
		}

		public void Dispose()
		{
			if (value != null && owned)
			{
				value.Dispose();
			}
		}
	}
	internal static class Empty<T>
	{
		public static readonly T[] Array = new T[0];
	}
	internal class ArgumentNullOrEmptyException : ArgumentException
	{
		public ArgumentNullOrEmptyException(string paramName)
			: base("Argument null or empty", paramName)
		{
		}
	}
	internal class MergeSort<T>
	{
		private readonly T[] elements;

		private readonly T[] buffer;

		private readonly IComparer<T> comparer;

		private MergeSort(T[] elements, IComparer<T> comparer)
		{
			this.elements = elements;
			buffer = new T[elements.Length];
			Array.Copy(this.elements, buffer, elements.Length);
			this.comparer = comparer;
		}

		public static void Sort(T[] source, IComparer<T> comparer)
		{
			Sort(source, 0, source.Length, comparer);
		}

		public static void Sort(T[] source, int start, int length, IComparer<T> comparer)
		{
			new MergeSort<T>(source, comparer).Sort(start, length);
		}

		private void Sort(int start, int length)
		{
			TopDownSplitMerge(buffer, elements, start, length);
		}

		private void TopDownSplitMerge(T[] a, T[] b, int start, int end)
		{
			if (end - start >= 2)
			{
				int num = (end + start) / 2;
				TopDownSplitMerge(b, a, start, num);
				TopDownSplitMerge(b, a, num, end);
				TopDownMerge(a, b, start, num, end);
			}
		}

		private void TopDownMerge(T[] a, T[] b, int start, int middle, int end)
		{
			int num = start;
			int num2 = middle;
			for (int i = start; i < end; i++)
			{
				if (num < middle && (num2 >= end || comparer.Compare(a[num], a[num2]) <= 0))
				{
					b[i] = a[num++];
				}
				else
				{
					b[i] = a[num2++];
				}
			}
		}
	}
}
namespace Mono.Security.Cryptography
{
	internal static class CryptoConvert
	{
		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return (bytes[offset + 3] << 24) | (bytes[offset + 2] << 16) | (bytes[offset + 1] << 8) | bytes[offset];
		}

		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)((bytes[offset + 3] << 24) | (bytes[offset + 2] << 16) | (bytes[offset + 1] << 8) | bytes[offset]);
		}

		private static byte[] GetBytesLE(int val)
		{
			return new byte[4]
			{
				(byte)(val & 0xFF),
				(byte)((val >> 8) & 0xFF),
				(byte)((val >> 16) & 0xFF),
				(byte)((val >> 24) & 0xFF)
			};
		}

		private static byte[] Trim(byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					byte[] array2 = new byte[array.Length - i];
					Buffer.BlockCopy(array, i, array2, 0, array2.Length);
					return array2;
				}
			}
			return null;
		}

		private static RSA FromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			RSAParameters parameters = default(RSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || ToUInt32LE(blob, offset + 8) != 843141970)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = ToInt32LE(blob, offset + 12);
				byte[] array = new byte[4];
				Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
				Array.Reverse((Array)array);
				parameters.Exponent = Trim(array);
				int num2 = offset + 20;
				int num3 = num >> 3;
				parameters.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, parameters.Modulus, 0, num3);
				Array.Reverse((Array)parameters.Modulus);
				num2 += num3;
				int num4 = num3 >> 1;
				parameters.P = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.P, 0, num4);
				Array.Reverse((Array)parameters.P);
				num2 += num4;
				parameters.Q = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.Q, 0, num4);
				Array.Reverse((Array)parameters.Q);
				num2 += num4;
				parameters.DP = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.DP, 0, num4);
				Array.Reverse((Array)parameters.DP);
				num2 += num4;
				parameters.DQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.DQ, 0, num4);
				Array.Reverse((Array)parameters.DQ);
				num2 += num4;
				parameters.InverseQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, parameters.InverseQ, 0, num4);
				Array.Reverse((Array)parameters.InverseQ);
				num2 += num4;
				parameters.D = new byte[num3];
				if (num2 + num3 + offset <= blob.Length)
				{
					Buffer.BlockCopy(blob, num2, parameters.D, 0, num3);
					Array.Reverse((Array)parameters.D);
				}
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
			RSA rSA = null;
			try
			{
				rSA = RSA.Create();
				rSA.ImportParameters(parameters);
			}
			catch (CryptographicException)
			{
				bool flag = false;
				try
				{
					CspParameters cspParameters = new CspParameters();
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
					rSA = new RSACryptoServiceProvider(cspParameters);
					rSA.ImportParameters(parameters);
				}
				catch
				{
					flag = true;
				}
				if (flag)
				{
					throw;
				}
			}
			return rSA;
		}

		private static RSA FromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || ToUInt32LE(blob, offset + 8) != 826364754)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = ToInt32LE(blob, offset + 12);
				RSAParameters parameters = new RSAParameters
				{
					Exponent = new byte[3]
				};
				parameters.Exponent[0] = blob[offset + 18];
				parameters.Exponent[1] = blob[offset + 17];
				parameters.Exponent[2] = blob[offset + 16];
				int srcOffset = offset + 20;
				int num2 = num >> 3;
				parameters.Modulus = new byte[num2];
				Buffer.BlockCopy(blob, srcOffset, parameters.Modulus, 0, num2);
				Array.Reverse((Array)parameters.Modulus);
				RSA rSA = null;
				try
				{
					rSA = RSA.Create();
					rSA.ImportParameters(parameters);
				}
				catch (CryptographicException)
				{
					CspParameters cspParameters = new CspParameters();
					cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
					rSA = new RSACryptoServiceProvider(cspParameters);
					rSA.ImportParameters(parameters);
				}
				return rSA;
			}
			catch (Exception inner)
			{
				throw new CryptographicException("Invalid blob.", inner);
			}
		}

		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return FromCapiKeyBlob(blob, 0);
		}

		public static RSA FromCapiKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			switch (blob[offset])
			{
			case 0:
				if (blob[offset + 12] == 6)
				{
					return FromCapiPublicKeyBlob(blob, offset + 12);
				}
				break;
			case 6:
				return FromCapiPublicKeyBlob(blob, offset);
			case 7:
				return FromCapiPrivateKeyBlob(blob, offset);
			}
			throw new CryptographicException("Unknown blob format.");
		}

		public static byte[] ToCapiPublicKeyBlob(RSA rsa)
		{
			RSAParameters rSAParameters = rsa.ExportParameters(includePrivateParameters: false);
			int num = rSAParameters.Modulus.Length;
			byte[] array = new byte[20 + num];
			array[0] = 6;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 49;
			byte[] bytesLE = GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int num3 = rSAParameters.Exponent.Length;
			while (num3 > 0)
			{
				array[num2++] = rSAParameters.Exponent[--num3];
			}
			num2 = 20;
			byte[] modulus = rSAParameters.Modulus;
			int num4 = modulus.Length;
			Array.Reverse((Array)modulus, 0, num4);
			Buffer.BlockCopy(modulus, 0, array, num2, num4);
			num2 += num4;
			return array;
		}
	}
}
namespace Mono.Collections.Generic
{
	public class Collection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			private Collection<T> collection;

			private T current;

			private int next;

			private readonly int version;

			public T Current => current;

			object IEnumerator.Current
			{
				get
				{
					CheckState();
					if (next <= 0)
					{
						throw new InvalidOperationException();
					}
					return current;
				}
			}

			internal Enumerator(Collection<T> collection)
			{
				this = default(Enumerator);
				this.collection = collection;
				version = collection.version;
			}

			public bool MoveNext()
			{
				CheckState();
				if (next < 0)
				{
					return false;
				}
				if (next < collection.size)
				{
					current = collection.items[next++];
					return true;
				}
				next = -1;
				return false;
			}

			public void Reset()
			{
				CheckState();
				next = 0;
			}

			private void CheckState()
			{
				if (collection == null)
				{
					throw new ObjectDisposedException(GetType().FullName);
				}
				if (version != collection.version)
				{
					throw new InvalidOperationException();
				}
			}

			public void Dispose()
			{
				collection = null;
			}
		}

		internal T[] items;

		internal int size;

		private int version;

		public int Count => size;

		public T this[int index]
		{
			get
			{
				if (index >= size)
				{
					throw new ArgumentOutOfRangeException();
				}
				return items[index];
			}
			set
			{
				CheckIndex(index);
				if (index == size)
				{
					throw new ArgumentOutOfRangeException();
				}
				OnSet(value, index);
				items[index] = value;
			}
		}

		public int Capacity
		{
			get
			{
				return items.Length;
			}
			set
			{
				if (value < 0 || value < size)
				{
					throw new ArgumentOutOfRangeException();
				}
				Resize(value);
			}
		}

		bool ICollection<T>.IsReadOnly => false;

		bool IList.IsFixedSize => false;

		bool IList.IsReadOnly => false;

		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				CheckIndex(index);
				try
				{
					this[index] = (T)value;
					return;
				}
				catch (InvalidCastException)
				{
				}
				catch (NullReferenceException)
				{
				}
				throw new ArgumentException();
			}
		}

		int ICollection.Count => Count;

		bool ICollection.IsSynchronized => false;

		object ICollection.SyncRoot => this;

		public Collection()
		{
			items = Empty<T>.Array;
		}

		public Collection(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			items = ((capacity == 0) ? Empty<T>.Array : new T[capacity]);
		}

		public Collection(ICollection<T> items)
		{
			if (items == null)
			{
				throw new ArgumentNullException("items");
			}
			this.items = new T[items.Count];
			items.CopyTo(this.items, 0);
			size = this.items.Length;
		}

		public void Add(T item)
		{
			if (size == items.Length)
			{
				Grow(1);
			}
			OnAdd(item, size);
			items[size++] = item;
			version++;
		}

		public bool Contains(T item)
		{
			return IndexOf(item) != -1;
		}

		public int IndexOf(T item)
		{
			return Array.IndexOf(items, item, 0, size);
		}

		public void Insert(int index, T item)
		{
			CheckIndex(index);
			if (size == items.Length)
			{
				Grow(1);
			}
			OnInsert(item, index);
			Shift(index, 1);
			items[index] = item;
			version++;
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= size)
			{
				throw new ArgumentOutOfRangeException();
			}
			T item = items[index];
			OnRemove(item, index);
			Shift(index, -1);
			version++;
		}

		public bool Remove(T item)
		{
			int num = IndexOf(item);
			if (num == -1)
			{
				return false;
			}
			OnRemove(item, num);
			Shift(num, -1);
			version++;
			return true;
		}

		public void Clear()
		{
			OnClear();
			Array.Clear(items, 0, size);
			size = 0;
			version++;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			Array.Copy(items, 0, array, arrayIndex, size);
		}

		public T[] ToArray()
		{
			T[] array = new T[size];
			Array.Copy(items, 0, array, 0, size);
			return array;
		}

		private void CheckIndex(int index)
		{
			if (index < 0 || index > size)
			{
				throw new ArgumentOutOfRangeException();
			}
		}

		private void Shift(int start, int delta)
		{
			if (delta < 0)
			{
				start -= delta;
			}
			if (start < size)
			{
				Array.Copy(items, start, items, start + delta, size - start);
			}
			size += delta;
			if (delta < 0)
			{
				Array.Clear(items, size, -delta);
			}
		}

		protected virtual void OnAdd(T item, int index)
		{
		}

		protected virtual void OnInsert(T item, int index)
		{
		}

		protected virtual void OnSet(T item, int index)
		{
		}

		protected virtual void OnRemove(T item, int index)
		{
		}

		protected virtual void OnClear()
		{
		}

		internal virtual void Grow(int desired)
		{
			int num = size + desired;
			if (num > items.Length)
			{
				num = System.Math.Max(System.Math.Max(items.Length * 2, 4), num);
				Resize(num);
			}
		}

		protected void Resize(int new_size)
		{
			if (new_size != size)
			{
				if (new_size < size)
				{
					throw new ArgumentOutOfRangeException();
				}
				items = items.Resize(new_size);
			}
		}

		int IList.Add(object value)
		{
			try
			{
				Add((T)value);
				return size - 1;
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			throw new ArgumentException();
		}

		void IList.Clear()
		{
			Clear();
		}

		bool IList.Contains(object value)
		{
			return ((IList)this).IndexOf(value) > -1;
		}

		int IList.IndexOf(object value)
		{
			try
			{
				return IndexOf((T)value);
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			return -1;
		}

		void IList.Insert(int index, object value)
		{
			CheckIndex(index);
			try
			{
				Insert(index, (T)value);
				return;
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
			throw new ArgumentException();
		}

		void IList.Remove(object value)
		{
			try
			{
				Remove((T)value);
			}
			catch (InvalidCastException)
			{
			}
			catch (NullReferenceException)
			{
			}
		}

		void IList.RemoveAt(int index)
		{
			RemoveAt(index);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			Array.Copy(items, 0, array, index, size);
		}

		public Enumerator GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Enumerator(this);
		}

		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return new Enumerator(this);
		}
	}
	public sealed class ReadOnlyCollection<T> : Collection<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
	{
		private static ReadOnlyCollection<T> empty;

		public static ReadOnlyCollection<T> Empty
		{
			get
			{
				if (empty != null)
				{
					return empty;
				}
				Interlocked.CompareExchange(ref empty, new ReadOnlyCollection<T>(), null);
				return empty;
			}
		}

		bool ICollection<T>.IsReadOnly => true;

		bool IList.IsFixedSize => true;

		bool IList.IsReadOnly => true;

		private ReadOnlyCollection()
		{
		}

		public ReadOnlyCollection(T[] array)
		{
			if (array == null)
			{
				throw new ArgumentNullException();
			}
			Initialize(array, array.Length);
		}

		public ReadOnlyCollection(Collection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException();
			}
			Initialize(collection.items, collection.size);
		}

		private void Initialize(T[] items, int size)
		{
			base.items = new T[size];
			Array.Copy(items, 0, base.items, 0, size);
			base.size = size;
		}

		internal override void Grow(int desired)
		{
			throw new InvalidOperationException();
		}

		protected override void OnAdd(T item, int index)
		{
			throw new InvalidOperationException();
		}

		protected override void OnClear()
		{
			throw new InvalidOperationException();
		}

		protected override void OnInsert(T item, int index)
		{
			throw new InvalidOperationException();
		}

		protected override void OnRemove(T item, int index)
		{
			throw new InvalidOperationException();
		}

		protected override void OnSet(T item, int index)
		{
			throw new InvalidOperationException();
		}
	}
}
namespace Mono.Cecil
{
	internal static class Mixin
	{
		public enum Argument
		{
			name,
			fileName,
			fullName,
			stream,
			type,
			method,
			field,
			parameters,
			module,
			modifierType,
			eventType,
			fieldType,
			declaringType,
			returnType,
			propertyType,
			interfaceType,
			constraintType
		}

		public static Version ZeroVersion = new Version(0, 0, 0, 0);

		public const int NotResolvedMarker = -2;

		public const int NoDataMarker = -1;

		internal static object NoValue = new object();

		internal static object NotResolved = new object();

		public const string mscorlib = "mscorlib";

		public const string system_runtime = "System.Runtime";

		public const string system_private_corelib = "System.Private.CoreLib";

		public const string netstandard = "netstandard";

		public const int TableCount = 58;

		public const int CodedIndexCount = 14;

		public static bool IsNullOrEmpty<T>(this T[] self)
		{
			return self == null || self.Length == 0;
		}

		public static bool IsNullOrEmpty<T>(this Collection<T> self)
		{
			return self == null || self.size == 0;
		}

		public static T[] Resize<T>(this T[] self, int length)
		{
			Array.Resize(ref self, length);
			return self;
		}

		public static T[] Add<T>(this T[] self, T item)
		{
			if (self == null)
			{
				self = new T[1] { item };
				return self;
			}
			self = self.Resize(self.Length + 1);
			self[^1] = item;
			return self;
		}

		public static Version CheckVersion(Version version)
		{
			if (version == null)
			{
				return ZeroVersion;
			}
			if (version.Build == -1)
			{
				return new Version(version.Major, version.Minor, 0, 0);
			}
			if (version.Revision == -1)
			{
				return new Version(version.Major, version.Minor, version.Build, 0);
			}
			return version;
		}

		public static bool TryGetUniqueDocument(this MethodDebugInformation info, out Document document)
		{
			document = info.SequencePoints[0].Document;
			for (int i = 1; i < info.SequencePoints.Count; i++)
			{
				SequencePoint sequencePoint = info.SequencePoints[i];
				if (sequencePoint.Document != document)
				{
					return false;
				}
			}
			return true;
		}

		public static void ResolveConstant(this IConstantProvider self, ref object constant, ModuleDefinition module)
		{
			if (module == null)
			{
				constant = NoValue;
				return;
			}
			lock (module.SyncRoot)
			{
				if (constant != NotResolved)
				{
					return;
				}
				if (module.HasImage())
				{
					constant = module.Read(self, (IConstantProvider provider, MetadataReader reader) => reader.ReadConstant(provider));
				}
				else
				{
					constant = NoValue;
				}
			}
		}

		public static bool GetHasCustomAttributes(this ICustomAttributeProvider self, ModuleDefinition module)
		{
			return module.HasImage() && module.Read(self, (ICustomAttributeProvider provider, MetadataReader reader) => reader.HasCustomAttributes(provider));
		}

		public static Collection<CustomAttribute> GetCustomAttributes(this ICustomAttributeProvider self, ref Collection<CustomAttribute> variable, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read(ref variable, self, (ICustomAttributeProvider provider, MetadataReader reader) => reader.ReadCustomAttributes(provider));
			}
			Interlocked.CompareExchange(ref variable, new Collection<CustomAttribute>(), null);
			return variable;
		}

		public static bool ContainsGenericParameter(this IGenericInstance self)
		{
			Collection<TypeReference> genericArguments = self.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				if (genericArguments[i].ContainsGenericParameter)
				{
					return true;
				}
			}
			return false;
		}

		public static void GenericInstanceFullName(this IGenericInstance self, StringBuilder builder)
		{
			builder.Append("<");
			Collection<TypeReference> genericArguments = self.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				if (i > 0)
				{
					builder.Append(",");
				}
				builder.Append(genericArguments[i].FullName);
			}
			builder.Append(">");
		}

		public static bool GetHasGenericParameters(this IGenericParameterProvider self, ModuleDefinition module)
		{
			return module.HasImage() && module.Read(self, (IGenericParameterProvider provider, MetadataReader reader) => reader.HasGenericParameters(provider));
		}

		public static Collection<GenericParameter> GetGenericParameters(this IGenericParameterProvider self, ref Collection<GenericParameter> collection, ModuleDefinition module)
		{
			if (module.HasImage())
			{
				return module.Read(ref collection, self, (IGenericParameterProvider provider, MetadataReader reader) => reader.ReadGenericParameters(provider));
			}
			Interlocked.CompareExchange(ref collection, new GenericParameterCollection(self), null);
			return collection;
		}

		public static bool GetHasMarshalInfo(this IMarshalInfoProvider self, ModuleDefinition module)
		{
			return module.HasImage() && module.Read(self, (IMarshalInfoProvider provider, MetadataReader reader) => reader.HasMarshalInfo(provider));
		}

		public static MarshalInfo GetMarshalInfo(this IMarshalInfoProvider self, ref MarshalInfo variable, ModuleDefinition module)
		{
			return module.HasImage() ? module.Read(ref variable, self, (IMarshalInfoProvider provider, MetadataReader reader) => reader.ReadMarshalInfo(provider)) : null;
		}

		public static bool GetAttributes(this uint self, uint attributes)
		{
			return (self & attributes) != 0;
		}

		public static uint SetAttributes(this uint self, uint attributes, bool value)
		{
			if (value)
			{
				return self | attributes;
			}
			return self & ~attributes;
		}

		public static bool GetMaskedAttributes(this uint self, uint mask, uint attributes)
		{
			return (self & mask) == attributes;
		}

		public static uint SetMaskedAttributes(this uint self, uint mask, uint attributes, bool value)
		{
			if (value)
			{
				self &= ~mask;
				return self | attributes;
			}
			return self & ~(mask & attributes);
		}

		public static bool GetAttributes(this ushort self, ushort attributes)
		{
			return (self & attributes) != 0;
		}

		public static ushort SetAttributes(this ushort self, ushort attributes, bool value)
		{
			if (value)
			{
				return (ushort)(self | attributes);
			}
			return (ushort)(self & ~attributes);
		}

		public static bool GetMaskedAttributes(this ushort self, ushort mask, uint attributes)
		{
			return (self & mask) == attributes;
		}

		public static ushort SetMaskedAttributes(this ushort self, ushort mask, uint attributes, bool value)
		{
			if (value)
			{
				self = (ushort)(self & ~mask);
				return (ushort)(self | attributes);
			}
			return (ushort)(self & ~(mask & attributes));
		}

		public static bool HasImplicitThis(this IMethodSignature self)
		{
			return self.HasThis && !self.ExplicitThis;
		}

		public static void MethodSignatureFullName(this IMethodSignature self, StringBuilder builder)
		{
			builder.Append("(");
			if (self.HasParameters)
			{
				Collection<ParameterDefinition> parameters = self.Parameters;
				for (int i = 0; i < parameters.Count; i++)
				{
					ParameterDefinition parameterDefinition = parameters[i];
					if (i > 0)
					{
						builder.Append(",");
					}
					if (parameterDefinition.ParameterType.IsSentinel)
					{
						builder.Append("...,");
					}
					builder.Append(parameterDefinition.ParameterType.FullName);
				}
			}
			builder.Append(")");
		}

		public static void CheckModule(ModuleDefinition module)
		{
			if (module == null)
			{
				throw new ArgumentNullException(Argument.module.ToString());
			}
		}

		public static bool TryGetAssemblyNameReference(this ModuleDefinition module, AssemblyNameReference name_reference, out AssemblyNameReference assembly_reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (Equals(name_reference, assemblyNameReference))
				{
					assembly_reference = assemblyNameReference;
					return true;
				}
			}
			assembly_reference = null;
			return false;
		}

		private static bool Equals(byte[] a, byte[] b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i])
				{
					return false;
				}
			}
			return true;
		}

		private static bool Equals<T>(T a, T b) where T : class, IEquatable<T>
		{
			if (a == b)
			{
				return true;
			}
			return a?.Equals(b) ?? false;
		}

		private static bool Equals(AssemblyNameReference a, AssemblyNameReference b)
		{
			if (a == b)
			{
				return true;
			}
			if (a.Name != b.Name)
			{
				return false;
			}
			if (!Equals(a.Version, b.Version))
			{
				return false;
			}
			if (a.Culture != b.Culture)
			{
				return false;
			}
			if (!Equals(a.PublicKeyToken, b.PublicKeyToken))
			{
				return false;
			}
			return true;
		}

		public static ParameterDefinition GetParameter(this Mono.Cecil.Cil.MethodBody self, int index)
		{
			MethodDefinition method = self.method;
			if (method.HasThis)
			{
				if (index == 0)
				{
					return self.ThisParameter;
				}
				index--;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			if (index < 0 || index >= parameters.size)
			{
				return null;
			}
			return parameters[index];
		}

		public static VariableDefinition GetVariable(this Mono.Cecil.Cil.MethodBody self, int index)
		{
			Collection<VariableDefinition> variables = self.Variables;
			if (index < 0 || index >= variables.size)
			{
				return null;
			}
			return variables[index];
		}

		public static bool GetSemantics(this MethodDefinition self, MethodSemanticsAttributes semantics)
		{
			return (self.SemanticsAttributes & semantics) != 0;
		}

		public static void SetSemantics(this MethodDefinition self, MethodSemanticsAttributes semantics, bool value)
		{
			if (value)
			{
				self.SemanticsAttributes |= semantics;
			}
			else
			{
				self.SemanticsAttributes &= (MethodSemanticsAttributes)(ushort)(~(int)semantics);
			}
		}

		public static bool IsVarArg(this IMethodSignature self)
		{
			return self.CallingConvention == MethodCallingConvention.VarArg;
		}

		public static int GetSentinelPosition(this IMethodSignature self)
		{
			if (!self.HasParameters)
			{
				return -1;
			}
			Collection<ParameterDefinition> parameters = self.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				if (parameters[i].ParameterType.IsSentinel)
				{
					return i;
				}
			}
			return -1;
		}

		public static void CheckName(object name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(Argument.name.ToString());
			}
		}

		public static void CheckName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullOrEmptyException(Argument.name.ToString());
			}
		}

		public static void CheckFileName(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullOrEmptyException(Argument.fileName.ToString());
			}
		}

		public static void CheckFullName(string fullName)
		{
			if (string.IsNullOrEmpty(fullName))
			{
				throw new ArgumentNullOrEmptyException(Argument.fullName.ToString());
			}
		}

		public static void CheckStream(object stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException(Argument.stream.ToString());
			}
		}

		public static void CheckWriteSeek(Stream stream)
		{
			if (!stream.CanWrite || !stream.CanSeek)
			{
				throw new ArgumentException("Stream must be writable and seekable.");
			}
		}

		public static void CheckReadSeek(Stream stream)
		{
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException("Stream must be readable and seekable.");
			}
		}

		public static void CheckType(object type)
		{
			if (type == null)
			{
				throw new ArgumentNullException(Argument.type.ToString());
			}
		}

		public static void CheckType(object type, Argument argument)
		{
			if (type == null)
			{
				throw new ArgumentNullException(argument.ToString());
			}
		}

		public static void CheckField(object field)
		{
			if (field == null)
			{
				throw new ArgumentNullException(Argument.field.ToString());
			}
		}

		public static void CheckMethod(object method)
		{
			if (method == null)
			{
				throw new ArgumentNullException(Argument.method.ToString());
			}
		}

		public static void CheckParameters(object parameters)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException(Argument.parameters.ToString());
			}
		}

		public static uint GetTimestamp()
		{
			return (uint)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}

		public static bool HasImage(this ModuleDefinition self)
		{
			return self?.HasImage ?? false;
		}

		public static string GetFileName(this Stream self)
		{
			if (!(self is FileStream fileStream))
			{
				return string.Empty;
			}
			return Path.GetFullPath(fileStream.Name);
		}

		public static TargetRuntime ParseRuntime(this string self)
		{
			if (string.IsNullOrEmpty(self))
			{
				return TargetRuntime.Net_4_0;
			}
			return self[1] switch
			{
				'1' => (self[3] != '0') ? TargetRuntime.Net_1_1 : TargetRuntime.Net_1_0, 
				'2' => TargetRuntime.Net_2_0, 
				_ => TargetRuntime.Net_4_0, 
			};
		}

		public static string RuntimeVersionString(this TargetRuntime runtime)
		{
			return runtime switch
			{
				TargetRuntime.Net_1_0 => "v1.0.3705", 
				TargetRuntime.Net_1_1 => "v1.1.4322", 
				TargetRuntime.Net_2_0 => "v2.0.50727", 
				_ => "v4.0.30319", 
			};
		}

		public static bool IsWindowsMetadata(this ModuleDefinition module)
		{
			return module.MetadataKind != MetadataKind.Ecma335;
		}

		public static byte[] ReadAll(this Stream self)
		{
			MemoryStream memoryStream = new MemoryStream((int)self.Length);
			byte[] array = new byte[1024];
			int count;
			while ((count = self.Read(array, 0, array.Length)) != 0)
			{
				memoryStream.Write(array, 0, count);
			}
			return memoryStream.ToArray();
		}

		public static void Read(object o)
		{
		}

		public static bool GetHasSecurityDeclarations(this ISecurityDeclarationProvider self, ModuleDefinition module)
		{
			return module.HasImage() && module.Read(self, (ISecurityDeclarationProvider provider, MetadataReader reader) => reader.HasSecurityDeclarations(provider));
		}

		public static Collection<SecurityDeclaration> GetSecurityDeclarations(this ISecurityDeclarationProvider self, ref Collection<SecurityDeclaration> variable, ModuleDefinition module)
		{
			if (module.HasImage)
			{
				return module.Read(ref variable, self, (ISecurityDeclarationProvider provider, MetadataReader reader) => reader.ReadSecurityDeclarations(provider));
			}
			Interlocked.CompareExchange(ref variable, new Collection<SecurityDeclaration>(), null);
			return variable;
		}

		public static TypeReference GetEnumUnderlyingType(this TypeDefinition self)
		{
			Collection<FieldDefinition> fields = self.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (!fieldDefinition.IsStatic)
				{
					return fieldDefinition.FieldType;
				}
			}
			throw new ArgumentException();
		}

		public static TypeDefinition GetNestedType(this TypeDefinition self, string fullname)
		{
			if (!self.HasNestedTypes)
			{
				return null;
			}
			Collection<TypeDefinition> nestedTypes = self.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				TypeDefinition typeDefinition = nestedTypes[i];
				if (typeDefinition.TypeFullName() == fullname)
				{
					return typeDefinition;
				}
			}
			return null;
		}

		public static bool IsPrimitive(this ElementType self)
		{
			ElementType elementType = self;
			ElementType elementType2 = elementType;
			if (elementType2 - 2 <= ElementType.U8 || elementType2 - 24 <= ElementType.Void)
			{
				return true;
			}
			return false;
		}

		public static string TypeFullName(this TypeReference self)
		{
			return string.IsNullOrEmpty(self.Namespace) ? self.Name : (self.Namespace + "." + self.Name);
		}

		public static bool IsTypeOf(this TypeReference self, string @namespace, string name)
		{
			return self.Name == name && self.Namespace == @namespace;
		}

		public static bool IsTypeSpecification(this TypeReference type)
		{
			switch (type.etype)
			{
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.GenericInst:
			case ElementType.FnPtr:
			case ElementType.SzArray:
			case ElementType.MVar:
			case ElementType.CModReqD:
			case ElementType.CModOpt:
			case ElementType.Sentinel:
			case ElementType.Pinned:
				return true;
			default:
				return false;
			}
		}

		public static TypeDefinition CheckedResolve(this TypeReference self)
		{
			TypeDefinition typeDefinition = self.Resolve();
			if (typeDefinition == null)
			{
				throw new ResolutionException(self);
			}
			return typeDefinition;
		}

		public static bool TryGetCoreLibraryReference(this ModuleDefinition module, out AssemblyNameReference reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				reference = assemblyReferences[i];
				if (IsCoreLibrary(reference))
				{
					return true;
				}
			}
			reference = null;
			return false;
		}

		public static bool IsCoreLibrary(this ModuleDefinition module)
		{
			if (module.Assembly == null)
			{
				return false;
			}
			if (!IsCoreLibrary(module.Assembly.Name))
			{
				return false;
			}
			if (module.HasImage && module.Read(module, (ModuleDefinition m, MetadataReader reader) => reader.image.GetTableLength(Table.AssemblyRef) > 0))
			{
				return false;
			}
			return true;
		}

		public static void KnownValueType(this TypeReference type)
		{
			if (!type.IsDefinition)
			{
				type.IsValueType = true;
			}
		}

		private static bool IsCoreLibrary(AssemblyNameReference reference)
		{
			string name = reference.Name;
			int result;
			switch (name)
			{
			default:
				result = ((name == "netstandard") ? 1 : 0);
				break;
			case "mscorlib":
			case "System.Runtime":
			case "System.Private.CoreLib":
				result = 1;
				break;
			}
			return (byte)result != 0;
		}

		public static ImageDebugHeaderEntry GetCodeViewEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.CodeView);
		}

		public static ImageDebugHeaderEntry GetDeterministicEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.Deterministic);
		}

		public static ImageDebugHeader AddDeterministicEntry(this ImageDebugHeader header)
		{
			ImageDebugHeaderEntry imageDebugHeaderEntry = new ImageDebugHeaderEntry(new ImageDebugDirectory
			{
				Type = ImageDebugType.Deterministic
			}, Empty<byte>.Array);
			if (header == null)
			{
				return new ImageDebugHeader(imageDebugHeaderEntry);
			}
			ImageDebugHeaderEntry[] array = new ImageDebugHeaderEntry[header.Entries.Length + 1];
			Array.Copy(header.Entries, array, header.Entries.Length);
			array[^1] = imageDebugHeaderEntry;
			return new ImageDebugHeader(array);
		}

		public static ImageDebugHeaderEntry GetEmbeddedPortablePdbEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.EmbeddedPortablePdb);
		}

		public static ImageDebugHeaderEntry GetPdbChecksumEntry(this ImageDebugHeader header)
		{
			return header.GetEntry(ImageDebugType.PdbChecksum);
		}

		private static ImageDebugHeaderEntry GetEntry(this ImageDebugHeader header, ImageDebugType type)
		{
			if (!header.HasEntries)
			{
				return null;
			}
			for (int i = 0; i < header.Entries.Length; i++)
			{
				ImageDebugHeaderEntry imageDebugHeaderEntry = header.Entries[i];
				if (imageDebugHeaderEntry.Directory.Type == type)
				{
					return imageDebugHeaderEntry;
				}
			}
			return null;
		}

		public static string GetPdbFileName(string assemblyFileName)
		{
			return Path.ChangeExtension(assemblyFileName, ".pdb");
		}

		public static string GetMdbFileName(string assemblyFileName)
		{
			return assemblyFileName + ".mdb";
		}

		public static bool IsPortablePdb(string fileName)
		{
			using FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
			return IsPortablePdb(stream);
		}

		public static bool IsPortablePdb(Stream stream)
		{
			if (stream.Length < 4)
			{
				return false;
			}
			long position = stream.Position;
			try
			{
				BinaryReader binaryReader = new BinaryReader(stream);
				return binaryReader.ReadUInt32() == 1112167234;
			}
			finally
			{
				stream.Position = position;
			}
		}

		public static uint ReadCompressedUInt32(this byte[] data, ref int position)
		{
			uint result;
			if ((data[position] & 0x80) == 0)
			{
				result = data[position];
				position++;
			}
			else if ((data[position] & 0x40) == 0)
			{
				result = (uint)((data[position] & -129) << 8);
				result |= data[position + 1];
				position += 2;
			}
			else
			{
				result = (uint)((data[position] & -193) << 24);
				result |= (uint)(data[position + 1] << 16);
				result |= (uint)(data[position + 2] << 8);
				result |= data[position + 3];
				position += 4;
			}
			return result;
		}

		public static MetadataToken GetMetadataToken(this CodedIndex self, uint data)
		{
			uint rid;
			TokenType type;
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				rid = data >> 2;
				switch (data & 3)
				{
				case 0u:
					break;
				case 1u:
					goto IL_0075;
				case 2u:
					goto IL_0080;
				default:
					goto end_IL_0006;
				}
				type = TokenType.TypeDef;
				goto IL_0644;
			case CodedIndex.HasConstant:
				rid = data >> 2;
				switch (data & 3)
				{
				case 0u:
					break;
				case 1u:
					goto IL_00bd;
				case 2u:
					goto IL_00c8;
				default:
					goto end_IL_0006;
				}
				type = TokenType.Field;
				goto IL_0644;
			case CodedIndex.HasCustomAttribute:
				rid = data >> 5;
				switch (data & 0x1F)
				{
				case 0u:
					break;
				case 1u:
					goto IL_0155;
				case 2u:
					goto IL_0160;
				case 3u:
					goto IL_016b;
				case 4u:
					goto IL_0176;
				case 5u:
					goto IL_0181;
				case 6u:
					goto IL_018c;
				case 7u:
					goto IL_0197;
				case 8u:
					goto IL_019e;
				case 9u:
					goto IL_01a9;
				case 10u:
					goto IL_01b4;
				case 11u:
					goto IL_01bf;
				case 12u:
					goto IL_01ca;
				case 13u:
					goto IL_01d5;
				case 14u:
					goto IL_01e0;
				case 15u:
					goto IL_01eb;
				case 16u:
					goto IL_01f6;
				case 17u:
					goto IL_0201;
				case 18u:
					goto IL_020c;
				case 19u:
					goto IL_0217;
				case 20u:
					goto IL_0222;
				case 21u:
					goto IL_022d;
				default:
					goto end_IL_0006;
				}
				type = TokenType.Method;
				goto IL_0644;
			case CodedIndex.HasFieldMarshal:
			{
				rid = data >> 1;
				uint num11 = data & 1;
				uint num12 = num11;
				if (num12 != 0)
				{
					if (num12 != 1)
					{
						break;
					}
					type = TokenType.Param;
				}
				else
				{
					type = TokenType.Field;
				}
				goto IL_0644;
			}
			case CodedIndex.HasDeclSecurity:
				rid = data >> 2;
				switch (data & 3)
				{
				case 0u:
					break;
				case 1u:
					goto IL_029f;
				case 2u:
					goto IL_02aa;
				default:
					goto end_IL_0006;
				}
				type = TokenType.TypeDef;
				goto IL_0644;
			case CodedIndex.MemberRefParent:
				rid = data >> 3;
				switch (data & 7)
				{
				case 0u:
					break;
				case 1u:
					goto IL_02ef;
				case 2u:
					goto IL_02fa;
				case 3u:
					goto IL_0305;
				case 4u:
					goto IL_0310;
				default:
					goto end_IL_0006;
				}
				type = TokenType.TypeDef;
				goto IL_0644;
			case CodedIndex.HasSemantics:
			{
				rid = data >> 1;
				uint num9 = data & 1;
				uint num10 = num9;
				if (num10 != 0)
				{
					if (num10 != 1)
					{
						break;
					}
					type = TokenType.Property;
				}
				else
				{
					type = TokenType.Event;
				}
				goto IL_0644;
			}
			case CodedIndex.MethodDefOrRef:
			{
				rid = data >> 1;
				uint num7 = data & 1;
				uint num8 = num7;
				if (num8 != 0)
				{
					if (num8 != 1)
					{
						break;
					}
					type = TokenType.MemberRef;
				}
				else
				{
					type = TokenType.Method;
				}
				goto IL_0644;
			}
			case CodedIndex.MemberForwarded:
			{
				rid = data >> 1;
				uint num5 = data & 1;
				uint num6 = num5;
				if (num6 != 0)
				{
					if (num6 != 1)
					{
						break;
					}
					type = TokenType.Method;
				}
				else
				{
					type = TokenType.Field;
				}
				goto IL_0644;
			}
			case CodedIndex.Implementation:
				rid = data >> 2;
				switch (data & 3)
				{
				case 0u:
					break;
				case 1u:
					goto IL_03ec;
				case 2u:
					goto IL_03f7;
				default:
					goto end_IL_0006;
				}
				type = TokenType.File;
				goto IL_0644;
			case CodedIndex.CustomAttributeType:
			{
				rid = data >> 3;
				uint num3 = data & 7;
				uint num4 = num3;
				if (num4 != 2)
				{
					if (num4 != 3)
					{
						break;
					}
					type = TokenType.MemberRef;
				}
				else
				{
					type = TokenType.Method;
				}
				goto IL_0644;
			}
			case CodedIndex.ResolutionScope:
				rid = data >> 2;
				switch (data & 3)
				{
				case 0u:
					break;
				case 1u:
					goto IL_046a;
				case 2u:
					goto IL_0475;
				case 3u:
					goto IL_0480;
				default:
					goto end_IL_0006;
				}
				type = TokenType.Module;
				goto IL_0644;
			case CodedIndex.TypeOrMethodDef:
			{
				rid = data >> 1;
				uint num = data & 1;
				uint num2 = num;
				if (num2 != 0)
				{
					if (num2 != 1)
					{
						break;
					}
					type = TokenType.Method;
				}
				else
				{
					type = TokenType.TypeDef;
				}
				goto IL_0644;
			}
			case CodedIndex.HasCustomDebugInformation:
				{
					rid = data >> 5;
					switch (data & 0x1F)
					{
					case 0u:
						break;
					case 1u:
						goto IL_0556;
					case 2u:
						goto IL_0561;
					case 3u:
						goto IL_056c;
					case 4u:
						goto IL_0577;
					case 5u:
						goto IL_0582;
					case 6u:
						goto IL_058d;
					case 7u:
						goto IL_0598;
					case 8u:
						goto IL_059f;
					case 9u:
						goto IL_05aa;
					case 10u:
						goto IL_05b5;
					case 11u:
						goto IL_05c0;
					case 12u:
						goto IL_05c8;
					case 13u:
						goto IL_05d0;
					case 14u:
						goto IL_05d8;
					case 15u:
						goto IL_05e0;
					case 16u:
						goto IL_05e8;
					case 17u:
						goto IL_05f0;
					case 18u:
						goto IL_05f8;
					case 19u:
						goto IL_0600;
					case 20u:
						goto IL_0608;
					case 21u:
						goto IL_0610;
					case 22u:
						goto IL_0618;
					case 23u:
						goto IL_0620;
					case 24u:
						goto IL_0628;
					case 25u:
						goto IL_0630;
					case 26u:
						goto IL_0638;
					default:
						goto end_IL_0006;
					}
					type = TokenType.Method;
					goto IL_0644;
				}
				IL_0638:
				type = TokenType.ImportScope;
				goto IL_0644;
				IL_0630:
				type = TokenType.LocalConstant;
				goto IL_0644;
				IL_0628:
				type = TokenType.LocalVariable;
				goto IL_0644;
				IL_0620:
				type = TokenType.LocalScope;
				goto IL_0644;
				IL_0618:
				type = TokenType.Document;
				goto IL_0644;
				IL_0610:
				type = TokenType.MethodSpec;
				goto IL_0644;
				IL_0608:
				type = TokenType.GenericParamConstraint;
				goto IL_0644;
				IL_0600:
				type = TokenType.GenericParam;
				goto IL_0644;
				IL_05f8:
				type = TokenType.ManifestResource;
				goto IL_0644;
				IL_05f0:
				type = TokenType.ExportedType;
				goto IL_0644;
				IL_05e8:
				type = TokenType.File;
				goto IL_0644;
				IL_05e0:
				type = TokenType.AssemblyRef;
				goto IL_0644;
				IL_05d8:
				type = TokenType.Assembly;
				goto IL_0644;
				IL_05d0:
				type = TokenType.TypeSpec;
				goto IL_0644;
				IL_05c8:
				type = TokenType.ModuleRef;
				goto IL_0644;
				IL_05c0:
				type = TokenType.Signature;
				goto IL_0644;
				IL_05b5:
				type = TokenType.Event;
				goto IL_0644;
				IL_05aa:
				type = TokenType.Property;
				goto IL_0644;
				IL_059f:
				type = TokenType.Permission;
				goto IL_0644;
				IL_0598:
				type = TokenType.Module;
				goto IL_0644;
				IL_058d:
				type = TokenType.MemberRef;
				goto IL_0644;
				IL_0582:
				type = TokenType.InterfaceImpl;
				goto IL_0644;
				IL_0577:
				type = TokenType.Param;
				goto IL_0644;
				IL_056c:
				type = TokenType.TypeDef;
				goto IL_0644;
				IL_0561:
				type = TokenType.TypeRef;
				goto IL_0644;
				IL_0556:
				type = TokenType.Field;
				goto IL_0644;
				IL_01f6:
				type = TokenType.File;
				goto IL_0644;
				IL_01eb:
				type = TokenType.AssemblyRef;
				goto IL_0644;
				IL_01d5:
				type = TokenType.TypeSpec;
				goto IL_0644;
				IL_01e0:
				type = TokenType.Assembly;
				goto IL_0644;
				IL_0480:
				type = TokenType.TypeRef;
				goto IL_0644;
				IL_0475:
				type = TokenType.AssemblyRef;
				goto IL_0644;
				IL_046a:
				type = TokenType.ModuleRef;
				goto IL_0644;
				IL_01ca:
				type = TokenType.ModuleRef;
				goto IL_0644;
				IL_01bf:
				type = TokenType.Signature;
				goto IL_0644;
				IL_01a9:
				type = TokenType.Property;
				goto IL_0644;
				IL_01b4:
				type = TokenType.Event;
				goto IL_0644;
				IL_03f7:
				type = TokenType.ExportedType;
				goto IL_0644;
				IL_03ec:
				type = TokenType.AssemblyRef;
				goto IL_0644;
				IL_019e:
				type = TokenType.Permission;
				goto IL_0644;
				IL_0197:
				type = TokenType.Module;
				goto IL_0644;
				IL_0181:
				type = TokenType.InterfaceImpl;
				goto IL_0644;
				IL_018c:
				type = TokenType.MemberRef;
				goto IL_0644;
				IL_0176:
				type = TokenType.Param;
				goto IL_0644;
				IL_0160:
				type = TokenType.TypeRef;
				goto IL_0644;
				IL_016b:
				type = TokenType.TypeDef;
				goto IL_0644;
				IL_0155:
				type = TokenType.Field;
				goto IL_0644;
				IL_0075:
				type = TokenType.TypeRef;
				goto IL_0644;
				IL_0310:
				type = TokenType.TypeSpec;
				goto IL_0644;
				IL_0305:
				type = TokenType.Method;
				goto IL_0644;
				IL_02fa:
				type = TokenType.ModuleRef;
				goto IL_0644;
				IL_02ef:
				type = TokenType.TypeRef;
				goto IL_0644;
				IL_00c8:
				type = TokenType.Property;
				goto IL_0644;
				IL_02aa:
				type = TokenType.Assembly;
				goto IL_0644;
				IL_029f:
				type = TokenType.Method;
				goto IL_0644;
				IL_00bd:
				type = TokenType.Param;
				goto IL_0644;
				IL_0644:
				return new MetadataToken(type, rid);
				IL_0080:
				type = TokenType.TypeSpec;
				goto IL_0644;
				IL_022d:
				type = TokenType.MethodSpec;
				goto IL_0644;
				IL_0222:
				type = TokenType.GenericParamConstraint;
				goto IL_0644;
				IL_0217:
				type = TokenType.GenericParam;
				goto IL_0644;
				IL_020c:
				type = TokenType.ManifestResource;
				goto IL_0644;
				IL_0201:
				type = TokenType.ExportedType;
				goto IL_0644;
				end_IL_0006:
				break;
			}
			return MetadataToken.Zero;
		}

		public static uint CompressMetadataToken(this CodedIndex self, MetadataToken token)
		{
			uint result = 0u;
			if (token.RID == 0)
			{
				return result;
			}
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				result = token.RID << 2;
				switch (token.TokenType)
				{
				case TokenType.TypeDef:
					return result | 0;
				case TokenType.TypeRef:
					return result | 1;
				case TokenType.TypeSpec:
					return result | 2;
				}
				break;
			case CodedIndex.HasConstant:
				result = token.RID << 2;
				switch (token.TokenType)
				{
				case TokenType.Field:
					return result | 0;
				case TokenType.Param:
					return result | 1;
				case TokenType.Property:
					return result | 2;
				}
				break;
			case CodedIndex.HasCustomAttribute:
				result = token.RID << 5;
				switch (token.TokenType)
				{
				case TokenType.Method:
					return result | 0;
				case TokenType.Field:
					return result | 1;
				case TokenType.TypeRef:
					return result | 2;
				case TokenType.TypeDef:
					return result | 3;
				case TokenType.Param:
					return result | 4;
				case TokenType.InterfaceImpl:
					return result | 5;
				case TokenType.MemberRef:
					return result | 6;
				case TokenType.Module:
					return result | 7;
				case TokenType.Permission:
					return result | 8;
				case TokenType.Property:
					return result | 9;
				case TokenType.Event:
					return result | 0xA;
				case TokenType.Signature:
					return result | 0xB;
				case TokenType.ModuleRef:
					return result | 0xC;
				case TokenType.TypeSpec:
					return result | 0xD;
				case TokenType.Assembly:
					return result | 0xE;
				case TokenType.AssemblyRef:
					return result | 0xF;
				case TokenType.File:
					return result | 0x10;
				case TokenType.ExportedType:
					return result | 0x11;
				case TokenType.ManifestResource:
					return result | 0x12;
				case TokenType.GenericParam:
					return result | 0x13;
				case TokenType.GenericParamConstraint:
					return result | 0x14;
				case TokenType.MethodSpec:
					return result | 0x15;
				}
				break;
			case CodedIndex.HasFieldMarshal:
				result = token.RID << 1;
				switch (token.TokenType)
				{
				case TokenType.Field:
					return result | 0;
				case TokenType.Param:
					return result | 1;
				}
				break;
			case CodedIndex.HasDeclSecurity:
				result = token.RID << 2;
				switch (token.TokenType)
				{
				case TokenType.TypeDef:
					return result | 0;
				case TokenType.Method:
					return result | 1;
				case TokenType.Assembly:
					return result | 2;
				}
				break;
			case CodedIndex.MemberRefParent:
				result = token.RID << 3;
				switch (token.TokenType)
				{
				case TokenType.TypeDef:
					return result | 0;
				case TokenType.TypeRef:
					return result | 1;
				case TokenType.ModuleRef:
					return result | 2;
				case TokenType.Method:
					return result | 3;
				case TokenType.TypeSpec:
					return result | 4;
				}
				break;
			case CodedIndex.HasSemantics:
				result = token.RID << 1;
				switch (token.TokenType)
				{
				case TokenType.Event:
					return result | 0;
				case TokenType.Property:
					return result | 1;
				}
				break;
			case CodedIndex.MethodDefOrRef:
				result = token.RID << 1;
				switch (token.TokenType)
				{
				case TokenType.Method:
					return result | 0;
				case TokenType.MemberRef:
					return result | 1;
				}
				break;
			case CodedIndex.MemberForwarded:
				result = token.RID << 1;
				switch (token.TokenType)
				{
				case TokenType.Field:
					return result | 0;
				case TokenType.Method:
					return result | 1;
				}
				break;
			case CodedIndex.Implementation:
				result = token.RID << 2;
				switch (token.TokenType)
				{
				case TokenType.File:
					return result | 0;
				case TokenType.AssemblyRef:
					return result | 1;
				case TokenType.ExportedType:
					return result | 2;
				}
				break;
			case CodedIndex.CustomAttributeType:
				result = token.RID << 3;
				switch (token.TokenType)
				{
				case TokenType.Method:
					return result | 2;
				case TokenType.MemberRef:
					return result | 3;
				}
				break;
			case CodedIndex.ResolutionScope:
				result = token.RID << 2;
				switch (token.TokenType)
				{
				case TokenType.Module:
					return result | 0;
				case TokenType.ModuleRef:
					return result | 1;
				case TokenType.AssemblyRef:
					return result | 2;
				case TokenType.TypeRef:
					return result | 3;
				}
				break;
			case CodedIndex.TypeOrMethodDef:
				result = token.RID << 1;
				switch (token.TokenType)
				{
				case TokenType.TypeDef:
					return result | 0;
				case TokenType.Method:
					return result | 1;
				}
				break;
			case CodedIndex.HasCustomDebugInformation:
				result = token.RID << 5;
				switch (token.TokenType)
				{
				case TokenType.Method:
					return result | 0;
				case TokenType.Field:
					return result | 1;
				case TokenType.TypeRef:
					return result | 2;
				case TokenType.TypeDef:
					return result | 3;
				case TokenType.Param:
					return result | 4;
				case TokenType.InterfaceImpl:
					return result | 5;
				case TokenType.MemberRef:
					return result | 6;
				case TokenType.Module:
					return result | 7;
				case TokenType.Permission:
					return result | 8;
				case TokenType.Property:
					return result | 9;
				case TokenType.Event:
					return result | 0xA;
				case TokenType.Signature:
					return result | 0xB;
				case TokenType.ModuleRef:
					return result | 0xC;
				case TokenType.TypeSpec:
					return result | 0xD;
				case TokenType.Assembly:
					return result | 0xE;
				case TokenType.AssemblyRef:
					return result | 0xF;
				case TokenType.File:
					return result | 0x10;
				case TokenType.ExportedType:
					return result | 0x11;
				case TokenType.ManifestResource:
					return result | 0x12;
				case TokenType.GenericParam:
					return result | 0x13;
				case TokenType.GenericParamConstraint:
					return result | 0x14;
				case TokenType.MethodSpec:
					return result | 0x15;
				case TokenType.Document:
					return result | 0x16;
				case TokenType.LocalScope:
					return result | 0x17;
				case TokenType.LocalVariable:
					return result | 0x18;
				case TokenType.LocalConstant:
					return result | 0x19;
				case TokenType.ImportScope:
					return result | 0x1A;
				}
				break;
			}
			throw new ArgumentException();
		}

		public static int GetSize(this CodedIndex self, Func<Table, int> counter)
		{
			int num;
			Table[] array;
			switch (self)
			{
			case CodedIndex.TypeDefOrRef:
				num = 2;
				array = new Table[3]
				{
					Table.TypeDef,
					Table.TypeRef,
					Table.TypeSpec
				};
				break;
			case CodedIndex.HasConstant:
				num = 2;
				array = new Table[3]
				{
					Table.Field,
					Table.Param,
					Table.Property
				};
				break;
			case CodedIndex.HasCustomAttribute:
				num = 5;
				array = new Table[22]
				{
					Table.Method,
					Table.Field,
					Table.TypeRef,
					Table.TypeDef,
					Table.Param,
					Table.InterfaceImpl,
					Table.MemberRef,
					Table.Module,
					Table.DeclSecurity,
					Table.Property,
					Table.Event,
					Table.StandAloneSig,
					Table.ModuleRef,
					Table.TypeSpec,
					Table.Assembly,
					Table.AssemblyRef,
					Table.File,
					Table.ExportedType,
					Table.ManifestResource,
					Table.GenericParam,
					Table.GenericParamConstraint,
					Table.MethodSpec
				};
				break;
			case CodedIndex.HasFieldMarshal:
				num = 1;
				array = new Table[2]
				{
					Table.Field,
					Table.Param
				};
				break;
			case CodedIndex.HasDeclSecurity:
				num = 2;
				array = new Table[3]
				{
					Table.TypeDef,
					Table.Method,
					Table.Assembly
				};
				break;
			case CodedIndex.MemberRefParent:
				num = 3;
				array = new Table[5]
				{
					Table.TypeDef,
					Table.TypeRef,
					Table.ModuleRef,
					Table.Method,
					Table.TypeSpec
				};
				break;
			case CodedIndex.HasSemantics:
				num = 1;
				array = new Table[2]
				{
					Table.Event,
					Table.Property
				};
				break;
			case CodedIndex.MethodDefOrRef:
				num = 1;
				array = new Table[2]
				{
					Table.Method,
					Table.MemberRef
				};
				break;
			case CodedIndex.MemberForwarded:
				num = 1;
				array = new Table[2]
				{
					Table.Field,
					Table.Method
				};
				break;
			case CodedIndex.Implementation:
				num = 2;
				array = new Table[3]
				{
					Table.File,
					Table.AssemblyRef,
					Table.ExportedType
				};
				break;
			case CodedIndex.CustomAttributeType:
				num = 3;
				array = new Table[2]
				{
					Table.Method,
					Table.MemberRef
				};
				break;
			case CodedIndex.ResolutionScope:
				num = 2;
				array = new Table[4]
				{
					Table.Module,
					Table.ModuleRef,
					Table.AssemblyRef,
					Table.TypeRef
				};
				break;
			case CodedIndex.TypeOrMethodDef:
				num = 1;
				array = new Table[2]
				{
					Table.TypeDef,
					Table.Method
				};
				break;
			case CodedIndex.HasCustomDebugInformation:
				num = 5;
				array = new Table[27]
				{
					Table.Method,
					Table.Field,
					Table.TypeRef,
					Table.TypeDef,
					Table.Param,
					Table.InterfaceImpl,
					Table.MemberRef,
					Table.Module,
					Table.DeclSecurity,
					Table.Property,
					Table.Event,
					Table.StandAloneSig,
					Table.ModuleRef,
					Table.TypeSpec,
					Table.Assembly,
					Table.AssemblyRef,
					Table.File,
					Table.ExportedType,
					Table.ManifestResource,
					Table.GenericParam,
					Table.GenericParamConstraint,
					Table.MethodSpec,
					Table.Document,
					Table.LocalScope,
					Table.LocalVariable,
					Table.LocalConstant,
					Table.ImportScope
				};
				break;
			default:
				throw new ArgumentException();
			}
			int num2 = 0;
			for (int i = 0; i < array.Length; i++)
			{
				num2 = System.Math.Max(counter(array[i]), num2);
			}
			return (num2 < 1 << 16 - num) ? 2 : 4;
		}

		public static RSA CreateRSA(this WriterParameters writer_parameters)
		{
			if (writer_parameters.StrongNameKeyBlob != null)
			{
				return Mono.Security.Cryptography.CryptoConvert.FromCapiKeyBlob(writer_parameters.StrongNameKeyBlob);
			}
			string key_container;
			byte[] key;
			if (writer_parameters.StrongNameKeyContainer != null)
			{
				key_container = writer_parameters.StrongNameKeyContainer;
			}
			else if (!TryGetKeyContainer(writer_parameters.StrongNameKeyPair, out key, out key_container))
			{
				return Mono.Security.Cryptography.CryptoConvert.FromCapiKeyBlob(key);
			}
			CspParameters parameters = new CspParameters
			{
				Flags = CspProviderFlags.UseMachineKeyStore,
				KeyContainerName = key_container,
				KeyNumber = 2
			};
			return new RSACryptoServiceProvider(parameters);
		}

		private static bool TryGetKeyContainer(ISerializable key_pair, out byte[] key, out string key_container)
		{
			SerializationInfo serializationInfo = new SerializationInfo(typeof(StrongNameKeyPair), new FormatterConverter());
			key_pair.GetObjectData(serializationInfo, default(StreamingContext));
			key = (byte[])serializationInfo.GetValue("_keyPairArray", typeof(byte[]));
			key_container = serializationInfo.GetString("_keyPairContainer");
			return key_container != null;
		}
	}
	public struct ArrayDimension
	{
		private int? lower_bound;

		private int? upper_bound;

		public int? LowerBound
		{
			get
			{
				return lower_bound;
			}
			set
			{
				lower_bound = value;
			}
		}

		public int? UpperBound
		{
			get
			{
				return upper_bound;
			}
			set
			{
				upper_bound = value;
			}
		}

		public bool IsSized => lower_bound.HasValue || upper_bound.HasValue;

		public ArrayDimension(int? lowerBound, int? upperBound)
		{
			lower_bound = lowerBound;
			upper_bound = upperBound;
		}

		public override string ToString()
		{
			string result;
			if (IsSized)
			{
				int? num = lower_bound;
				string text = num.ToString();
				num = upper_bound;
				result = text + "..." + num;
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
	public sealed class ArrayType : TypeSpecification
	{
		private Collection<ArrayDimension> dimensions;

		public Collection<ArrayDimension> Dimensions
		{
			get
			{
				if (dimensions != null)
				{
					return dimensions;
				}
				Collection<ArrayDimension> collection = new Collection<ArrayDimension>();
				collection.Add(default(ArrayDimension));
				Interlocked.CompareExchange(ref dimensions, collection, null);
				return dimensions;
			}
		}

		public int Rank => (dimensions == null) ? 1 : dimensions.Count;

		public bool IsVector
		{
			get
			{
				if (dimensions == null)
				{
					return true;
				}
				if (dimensions.Count > 1)
				{
					return false;
				}
				return !dimensions[0].IsSized;
			}
		}

		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override string Name => base.Name + Suffix;

		public override string FullName => base.FullName + Suffix;

		private string Suffix
		{
			get
			{
				if (IsVector)
				{
					return "[]";
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("[");
				for (int i = 0; i < dimensions.Count; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(dimensions[i].ToString());
				}
				stringBuilder.Append("]");
				return stringBuilder.ToString();
			}
		}

		public override bool IsArray => true;

		public ArrayType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			etype = Mono.Cecil.Metadata.ElementType.Array;
		}

		public ArrayType(TypeReference type, int rank)
			: this(type)
		{
			Mixin.CheckType(type);
			if (rank != 1)
			{
				dimensions = new Collection<ArrayDimension>(rank);
				for (int i = 0; i < rank; i++)
				{
					dimensions.Add(default(ArrayDimension));
				}
				etype = Mono.Cecil.Metadata.ElementType.Array;
			}
		}
	}
	public sealed class AssemblyDefinition : ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider, IDisposable
	{
		private AssemblyNameDefinition name;

		internal ModuleDefinition main_module;

		private Collection<ModuleDefinition> modules;

		private Collection<CustomAttribute> custom_attributes;

		private Collection<SecurityDeclaration> security_declarations;

		public AssemblyNameDefinition Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public string FullName => (name != null) ? name.FullName : string.Empty;

		public MetadataToken MetadataToken
		{
			get
			{
				return new MetadataToken(TokenType.Assembly, 1);
			}
			set
			{
			}
		}

		public Collection<ModuleDefinition> Modules
		{
			get
			{
				if (modules != null)
				{
					return modules;
				}
				if (main_module.HasImage)
				{
					return main_module.Read(ref modules, this, (AssemblyDefinition _, MetadataReader reader) => reader.ReadModules());
				}
				Interlocked.CompareExchange(ref modules, new Collection<ModuleDefinition>(1) { main_module }, null);
				return modules;
			}
		}

		public ModuleDefinition MainModule => main_module;

		public MethodDefinition EntryPoint
		{
			get
			{
				return main_module.EntryPoint;
			}
			set
			{
				main_module.EntryPoint = value;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(main_module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, main_module);

		public bool HasSecurityDeclarations
		{
			get
			{
				if (security_declarations != null)
				{
					return security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(main_module);
			}
		}

		public Collection<SecurityDeclaration> SecurityDeclarations => security_declarations ?? this.GetSecurityDeclarations(ref security_declarations, main_module);

		internal AssemblyDefinition()
		{
		}

		public void Dispose()
		{
			if (modules == null)
			{
				main_module.Dispose();
				return;
			}
			Collection<ModuleDefinition> collection = Modules;
			for (int i = 0; i < collection.Count; i++)
			{
				collection[i].Dispose();
			}
		}

		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleKind kind)
		{
			return CreateAssembly(assemblyName, moduleName, new ModuleParameters
			{
				Kind = kind
			});
		}

		public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleParameters parameters)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (moduleName == null)
			{
				throw new ArgumentNullException("moduleName");
			}
			Mixin.CheckParameters(parameters);
			if (parameters.Kind == ModuleKind.NetModule)
			{
				throw new ArgumentException("kind");
			}
			AssemblyDefinition assembly = ModuleDefinition.CreateModule(moduleName, parameters).Assembly;
			assembly.Name = assemblyName;
			return assembly;
		}

		public static AssemblyDefinition ReadAssembly(string fileName)
		{
			return ReadAssembly(ModuleDefinition.ReadModule(fileName));
		}

		public static AssemblyDefinition ReadAssembly(string fileName, ReaderParameters parameters)
		{
			return ReadAssembly(ModuleDefinition.ReadModule(fileName, parameters));
		}

		public static AssemblyDefinition ReadAssembly(Stream stream)
		{
			return ReadAssembly(ModuleDefinition.ReadModule(stream));
		}

		public static AssemblyDefinition ReadAssembly(Stream stream, ReaderParameters parameters)
		{
			return ReadAssembly(ModuleDefinition.ReadModule(stream, parameters));
		}

		private static AssemblyDefinition ReadAssembly(ModuleDefinition module)
		{
			AssemblyDefinition assembly = module.Assembly;
			if (assembly == null)
			{
				throw new ArgumentException();
			}
			return assembly;
		}

		public void Write(string fileName)
		{
			Write(fileName, new WriterParameters());
		}

		public void Write(string fileName, WriterParameters parameters)
		{
			main_module.Write(fileName, parameters);
		}

		public void Write()
		{
			main_module.Write();
		}

		public void Write(WriterParameters parameters)
		{
			main_module.Write(parameters);
		}

		public void Write(Stream stream)
		{
			Write(stream, new WriterParameters());
		}

		public void Write(Stream stream, WriterParameters parameters)
		{
			main_module.Write(stream, parameters);
		}

		public override string ToString()
		{
			return FullName;
		}
	}
	[Flags]
	public enum AssemblyAttributes : uint
	{
		PublicKey = 1u,
		SideBySideCompatible = 0u,
		Retargetable = 0x100u,
		WindowsRuntime = 0x200u,
		DisableJITCompileOptimizer = 0x4000u,
		EnableJITCompileTracking = 0x8000u
	}
	public enum AssemblyHashAlgorithm : uint
	{
		None = 0u,
		MD5 = 32771u,
		SHA1 = 32772u,
		SHA256 = 32780u,
		SHA384 = 32781u,
		SHA512 = 32782u,
		Reserved = 32771u
	}
	public sealed class AssemblyLinkedResource : Resource
	{
		private AssemblyNameReference reference;

		public AssemblyNameReference Assembly
		{
			get
			{
				return reference;
			}
			set
			{
				reference = value;
			}
		}

		public override ResourceType ResourceType => ResourceType.AssemblyLinked;

		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags)
			: base(name, flags)
		{
		}

		public AssemblyLinkedResource(string name, ManifestResourceAttributes flags, AssemblyNameReference reference)
			: base(name, flags)
		{
			this.reference = reference;
		}
	}
	public sealed class AssemblyNameDefinition : AssemblyNameReference
	{
		public override byte[] Hash => Empty<byte>.Array;

		internal AssemblyNameDefinition()
		{
			token = new MetadataToken(TokenType.Assembly, 1);
		}

		public AssemblyNameDefinition(string name, Version version)
			: base(name, version)
		{
			token = new MetadataToken(TokenType.Assembly, 1);
		}
	}
	public class AssemblyNameReference : IMetadataScope, IMetadataTokenProvider
	{
		private string name;

		private string culture;

		private Version version;

		private uint attributes;

		private byte[] public_key;

		private byte[] public_key_token;

		private AssemblyHashAlgorithm hash_algorithm;

		private byte[] hash;

		internal MetadataToken token;

		private string full_name;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
				full_name = null;
			}
		}

		public string Culture
		{
			get
			{
				return culture;
			}
			set
			{
				culture = value;
				full_name = null;
			}
		}

		public Version Version
		{
			get
			{
				return version;
			}
			set
			{
				version = Mixin.CheckVersion(value);
				full_name = null;
			}
		}

		public AssemblyAttributes Attributes
		{
			get
			{
				return (AssemblyAttributes)attributes;
			}
			set
			{
				attributes = (uint)value;
			}
		}

		public bool HasPublicKey
		{
			get
			{
				return attributes.GetAttributes(1u);
			}
			set
			{
				attributes = attributes.SetAttributes(1u, value);
			}
		}

		public bool IsSideBySideCompatible
		{
			get
			{
				return attributes.GetAttributes(0u);
			}
			set
			{
				attributes = attributes.SetAttributes(0u, value);
			}
		}

		public bool IsRetargetable
		{
			get
			{
				return attributes.GetAttributes(256u);
			}
			set
			{
				attributes = attributes.SetAttributes(256u, value);
			}
		}

		public bool IsWindowsRuntime
		{
			get
			{
				return attributes.GetAttributes(512u);
			}
			set
			{
				attributes = attributes.SetAttributes(512u, value);
			}
		}

		public byte[] PublicKey
		{
			get
			{
				return public_key ?? Empty<byte>.Array;
			}
			set
			{
				public_key = value;
				HasPublicKey = !public_key.IsNullOrEmpty();
				public_key_token = null;
				full_name = null;
			}
		}

		public byte[] PublicKeyToken
		{
			get
			{
				if (public_key_token == null && !public_key.IsNullOrEmpty())
				{
					byte[] array = HashPublicKey();
					byte[] array2 = new byte[8];
					Array.Copy(array, array.Length - 8, array2, 0, 8);
					Array.Reverse((Array)array2, 0, 8);
					Interlocked.CompareExchange(ref public_key_token, array2, null);
				}
				return public_key_token ?? Empty<byte>.Array;
			}
			set
			{
				public_key_token = value;
				full_name = null;
			}
		}

		public virtual MetadataScopeType MetadataScopeType => MetadataScopeType.AssemblyNameReference;

		public string FullName
		{
			get
			{
				if (full_name != null)
				{
					return full_name;
				}
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(name);
				stringBuilder.Append(", ");
				stringBuilder.Append("Version=");
				stringBuilder.Append(version.ToString(4));
				stringBuilder.Append(", ");
				stringBuilder.Append("Culture=");
				stringBuilder.Append(string.IsNullOrEmpty(culture) ? "neutral" : culture);
				stringBuilder.Append(", ");
				stringBuilder.Append("PublicKeyToken=");
				byte[] publicKeyToken = PublicKeyToken;
				if (!publicKeyToken.IsNullOrEmpty() && publicKeyToken.Length != 0)
				{
					for (int i = 0; i < publicKeyToken.Length; i++)
					{
						stringBuilder.Append(publicKeyToken[i].ToString("x2"));
					}
				}
				else
				{
					stringBuilder.Append("null");
				}
				if (IsRetargetable)
				{
					stringBuilder.Append(", ");
					stringBuilder.Append("Retargetable=Yes");
				}
				Interlocked.CompareExchange(ref full_name, stringBuilder.ToString(), null);
				return full_name;
			}
		}

		public AssemblyHashAlgorithm HashAlgorithm
		{
			get
			{
				return hash_algorithm;
			}
			set
			{
				hash_algorithm = value;
			}
		}

		public virtual byte[] Hash
		{
			get
			{
				return hash;
			}
			set
			{
				hash = value;
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		private byte[] HashPublicKey()
		{
			AssemblyHashAlgorithm assemblyHashAlgorithm = hash_algorithm;
			AssemblyHashAlgorithm assemblyHashAlgorithm2 = assemblyHashAlgorithm;
			HashAlgorithm hashAlgorithm = ((assemblyHashAlgorithm2 != AssemblyHashAlgorithm.MD5) ? ((HashAlgorithm)SHA1.Create()) : ((HashAlgorithm)MD5.Create()));
			using (hashAlgorithm)
			{
				return hashAlgorithm.ComputeHash(public_key);
			}
		}

		public static AssemblyNameReference Parse(string fullName)
		{
			if (fullName == null)
			{
				throw new ArgumentNullException("fullName");
			}
			if (fullName.Length == 0)
			{
				throw new ArgumentException("Name can not be empty");
			}
			AssemblyNameReference assemblyNameReference = new AssemblyNameReference();
			string[] array = fullName.Split(new char[1] { ',' });
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				if (i == 0)
				{
					assemblyNameReference.Name = text;
					continue;
				}
				string[] array2 = text.Split(new char[1] { '=' });
				if (array2.Length != 2)
				{
					throw new ArgumentException("Malformed name");
				}
				switch (array2[0].ToLowerInvariant())
				{
				case "version":
					assemblyNameReference.Version = new Version(array2[1]);
					break;
				case "culture":
					assemblyNameReference.Culture = ((array2[1] == "neutral") ? "" : array2[1]);
					break;
				case "publickeytoken":
				{
					string text2 = array2[1];
					if (!(text2 == "null"))
					{
						assemblyNameReference.PublicKeyToken = new byte[text2.Length / 2];
						for (int j = 0; j < assemblyNameReference.PublicKeyToken.Length; j++)
						{
							assemblyNameReference.PublicKeyToken[j] = byte.Parse(text2.Substring(j * 2, 2), NumberStyles.HexNumber);
						}
					}
					break;
				}
				}
			}
			return assemblyNameReference;
		}

		internal AssemblyNameReference()
		{
			version = Mixin.ZeroVersion;
			token = new MetadataToken(TokenType.AssemblyRef);
		}

		public AssemblyNameReference(string name, Version version)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.version = Mixin.CheckVersion(version);
			hash_algorithm = AssemblyHashAlgorithm.None;
			token = new MetadataToken(TokenType.AssemblyRef);
		}

		public override string ToString()
		{
			return FullName;
		}
	}
	internal abstract class ModuleReader
	{
		protected readonly ModuleDefinition module;

		protected ModuleReader(Image image, ReadingMode mode)
		{
			module = new ModuleDefinition(image);
			module.ReadingMode = mode;
		}

		protected abstract void ReadModule();

		public abstract void ReadSymbols(ModuleDefinition module);

		protected void ReadModuleManifest(MetadataReader reader)
		{
			reader.Populate(module);
			ReadAssembly(reader);
		}

		private void ReadAssembly(MetadataReader reader)
		{
			AssemblyNameDefinition assemblyNameDefinition = reader.ReadAssemblyNameDefinition();
			if (assemblyNameDefinition == null)
			{
				module.kind = ModuleKind.NetModule;
				return;
			}
			AssemblyDefinition assemblyDefinition = new AssemblyDefinition();
			assemblyDefinition.Name = assemblyNameDefinition;
			module.assembly = assemblyDefinition;
			assemblyDefinition.main_module = module;
		}

		public static ModuleDefinition CreateModule(Image image, ReaderParameters parameters)
		{
			ModuleReader moduleReader = CreateModuleReader(image, parameters.ReadingMode);
			ModuleDefinition moduleDefinition = moduleReader.module;
			if (parameters.assembly_resolver != null)
			{
				moduleDefinition.assembly_resolver = Disposable.NotOwned(parameters.assembly_resolver);
			}
			if (parameters.metadata_resolver != null)
			{
				moduleDefinition.metadata_resolver = parameters.metadata_resolver;
			}
			if (parameters.metadata_importer_provider != null)
			{
				moduleDefinition.metadata_importer = parameters.metadata_importer_provider.GetMetadataImporter(moduleDefinition);
			}
			if (parameters.reflection_importer_provider != null)
			{
				moduleDefinition.reflection_importer = parameters.reflection_importer_provider.GetReflectionImporter(moduleDefinition);
			}
			GetMetadataKind(moduleDefinition, parameters);
			moduleReader.ReadModule();
			ReadSymbols(moduleDefinition, parameters);
			moduleReader.ReadSymbols(moduleDefinition);
			if (parameters.ReadingMode == ReadingMode.Immediate)
			{
				moduleDefinition.MetadataSystem.Clear();
			}
			return moduleDefinition;
		}

		private static void ReadSymbols(ModuleDefinition module, ReaderParameters parameters)
		{
			ISymbolReaderProvider symbolReaderProvider = parameters.SymbolReaderProvider;
			if (symbolReaderProvider == null && parameters.ReadSymbols)
			{
				symbolReaderProvider = new DefaultSymbolReaderProvider();
			}
			if (symbolReaderProvider != null)
			{
				module.SymbolReaderProvider = symbolReaderProvider;
				ISymbolReader symbolReader = ((parameters.SymbolStream != null) ? symbolReaderProvider.GetSymbolReader(module, parameters.SymbolStream) : symbolReaderProvider.GetSymbolReader(module, module.FileName));
				if (symbolReader != null)
				{
					try
					{
						module.ReadSymbols(symbolReader, parameters.ThrowIfSymbolsAreNotMatching);
					}
					catch (Exception)
					{
						symbolReader.Dispose();
						throw;
					}
				}
			}
			if (module.Image.HasDebugTables())
			{
				module.ReadSymbols(new PortablePdbReader(module.Image, module));
			}
		}

		private static void GetMetadataKind(ModuleDefinition module, ReaderParameters parameters)
		{
			if (!parameters.ApplyWindowsRuntimeProjections)
			{
				module.MetadataKind = MetadataKind.Ecma335;
				return;
			}
			string runtimeVersion = module.RuntimeVersion;
			if (!runtimeVersion.Contains("WindowsRuntime"))
			{
				module.MetadataKind = MetadataKind.Ecma335;
			}
			else if (runtimeVersion.Contains("CLR"))
			{
				module.MetadataKind = MetadataKind.ManagedWindowsMetadata;
			}
			else
			{
				module.MetadataKind = MetadataKind.WindowsMetadata;
			}
		}

		private static ModuleReader CreateModuleReader(Image image, ReadingMode mode)
		{
			return mode switch
			{
				ReadingMode.Immediate => new ImmediateModuleReader(image), 
				ReadingMode.Deferred => new DeferredModuleReader(image), 
				_ => throw new ArgumentException(), 
			};
		}
	}
	internal sealed class ImmediateModuleReader : ModuleReader
	{
		private bool resolve_attributes;

		public ImmediateModuleReader(Image image)
			: base(image, ReadingMode.Immediate)
		{
		}

		protected override void ReadModule()
		{
			module.Read(module, delegate(ModuleDefinition module, MetadataReader reader)
			{
				ReadModuleManifest(reader);
				ReadModule(module, resolve_attributes: true);
			});
		}

		public void ReadModule(ModuleDefinition module, bool resolve_attributes)
		{
			this.resolve_attributes = resolve_attributes;
			if (module.HasAssemblyReferences)
			{
				Mixin.Read(module.AssemblyReferences);
			}
			if (module.HasResources)
			{
				Mixin.Read(module.Resources);
			}
			if (module.HasModuleReferences)
			{
				Mixin.Read(module.ModuleReferences);
			}
			if (module.HasTypes)
			{
				ReadTypes(module.Types);
			}
			if (module.HasExportedTypes)
			{
				Mixin.Read(module.ExportedTypes);
			}
			ReadCustomAttributes(module);
			AssemblyDefinition assembly = module.Assembly;
			if (module.kind != ModuleKind.NetModule && assembly != null)
			{
				ReadCustomAttributes(assembly);
				ReadSecurityDeclarations(assembly);
			}
		}

		private void ReadTypes(Collection<TypeDefinition> types)
		{
			for (int i = 0; i < types.Count; i++)
			{
				ReadType(types[i]);
			}
		}

		private void ReadType(TypeDefinition type)
		{
			ReadGenericParameters(type);
			if (type.HasInterfaces)
			{
				ReadInterfaces(type);
			}
			if (type.HasNestedTypes)
			{
				ReadTypes(type.NestedTypes);
			}
			if (type.HasLayoutInfo)
			{
				Mixin.Read(type.ClassSize);
			}
			if (type.HasFields)
			{
				ReadFields(type);
			}
			if (type.HasMethods)
			{
				ReadMethods(type);
			}
			if (type.HasProperties)
			{
				ReadProperties(type);
			}
			if (type.HasEvents)
			{
				ReadEvents(type);
			}
			ReadSecurityDeclarations(type);
			ReadCustomAttributes(type);
		}

		private void ReadInterfaces(TypeDefinition type)
		{
			Collection<InterfaceImplementation> interfaces = type.Interfaces;
			for (int i = 0; i < interfaces.Count; i++)
			{
				ReadCustomAttributes(interfaces[i]);
			}
		}

		private void ReadGenericParameters(IGenericParameterProvider provider)
		{
			if (!provider.HasGenericParameters)
			{
				return;
			}
			Collection<GenericParameter> genericParameters = provider.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				GenericParameter genericParameter = genericParameters[i];
				if (genericParameter.HasConstraints)
				{
					ReadGenericParameterConstraints(genericParameter);
				}
				ReadCustomAttributes(genericParameter);
			}
		}

		private void ReadGenericParameterConstraints(GenericParameter parameter)
		{
			Collection<GenericParameterConstraint> constraints = parameter.Constraints;
			for (int i = 0; i < constraints.Count; i++)
			{
				ReadCustomAttributes(constraints[i]);
			}
		}

		private void ReadSecurityDeclarations(ISecurityDeclarationProvider provider)
		{
			if (!provider.HasSecurityDeclarations)
			{
				return;
			}
			Collection<SecurityDeclaration> securityDeclarations = provider.SecurityDeclarations;
			if (resolve_attributes)
			{
				for (int i = 0; i < securityDeclarations.Count; i++)
				{
					SecurityDeclaration securityDeclaration = securityDeclarations[i];
					Mixin.Read(securityDeclaration.SecurityAttributes);
				}
			}
		}

		private void ReadCustomAttributes(ICustomAttributeProvider provider)
		{
			if (!provider.HasCustomAttributes)
			{
				return;
			}
			Collection<CustomAttribute> customAttributes = provider.CustomAttributes;
			if (resolve_attributes)
			{
				for (int i = 0; i < customAttributes.Count; i++)
				{
					CustomAttribute customAttribute = customAttributes[i];
					Mixin.Read(customAttribute.ConstructorArguments);
				}
			}
		}

		private void ReadFields(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (fieldDefinition.HasConstant)
				{
					Mixin.Read(fieldDefinition.Constant);
				}
				if (fieldDefinition.HasLayoutInfo)
				{
					Mixin.Read(fieldDefinition.Offset);
				}
				if (fieldDefinition.RVA > 0)
				{
					Mixin.Read(fieldDefinition.InitialValue);
				}
				if (fieldDefinition.HasMarshalInfo)
				{
					Mixin.Read(fieldDefinition.MarshalInfo);
				}
				ReadCustomAttributes(fieldDefinition);
			}
		}

		private void ReadMethods(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				ReadGenericParameters(methodDefinition);
				if (methodDefinition.HasParameters)
				{
					ReadParameters(methodDefinition);
				}
				if (methodDefinition.HasOverrides)
				{
					Mixin.Read(methodDefinition.Overrides);
				}
				if (methodDefinition.IsPInvokeImpl)
				{
					Mixin.Read(methodDefinition.PInvokeInfo);
				}
				ReadSecurityDeclarations(methodDefinition);
				ReadCustomAttributes(methodDefinition);
				MethodReturnType methodReturnType = methodDefinition.MethodReturnType;
				if (methodReturnType.HasConstant)
				{
					Mixin.Read(methodReturnType.Constant);
				}
				if (methodReturnType.HasMarshalInfo)
				{
					Mixin.Read(methodReturnType.MarshalInfo);
				}
				ReadCustomAttributes(methodReturnType);
			}
		}

		private void ReadParameters(MethodDefinition method)
		{
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterDefinition parameterDefinition = parameters[i];
				if (parameterDefinition.HasConstant)
				{
					Mixin.Read(parameterDefinition.Constant);
				}
				if (parameterDefinition.HasMarshalInfo)
				{
					Mixin.Read(parameterDefinition.MarshalInfo);
				}
				ReadCustomAttributes(parameterDefinition);
			}
		}

		private void ReadProperties(TypeDefinition type)
		{
			Collection<PropertyDefinition> properties = type.Properties;
			for (int i = 0; i < properties.Count; i++)
			{
				PropertyDefinition propertyDefinition = properties[i];
				Mixin.Read(propertyDefinition.GetMethod);
				if (propertyDefinition.HasConstant)
				{
					Mixin.Read(propertyDefinition.Constant);
				}
				ReadCustomAttributes(propertyDefinition);
			}
		}

		private void ReadEvents(TypeDefinition type)
		{
			Collection<EventDefinition> events = type.Events;
			for (int i = 0; i < events.Count; i++)
			{
				EventDefinition eventDefinition = events[i];
				Mixin.Read(eventDefinition.AddMethod);
				ReadCustomAttributes(eventDefinition);
			}
		}

		public override void ReadSymbols(ModuleDefinition module)
		{
			if (module.symbol_reader != null)
			{
				ReadTypesSymbols(module.Types, module.symbol_reader);
			}
		}

		private void ReadTypesSymbols(Collection<TypeDefinition> types, ISymbolReader symbol_reader)
		{
			for (int i = 0; i < types.Count; i++)
			{
				TypeDefinition typeDefinition = types[i];
				if (typeDefinition.HasNestedTypes)
				{
					ReadTypesSymbols(typeDefinition.NestedTypes, symbol_reader);
				}
				if (typeDefinition.HasMethods)
				{
					ReadMethodsSymbols(typeDefinition, symbol_reader);
				}
			}
		}

		private void ReadMethodsSymbols(TypeDefinition type, ISymbolReader symbol_reader)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (methodDefinition.HasBody && methodDefinition.token.RID != 0 && methodDefinition.debug_info == null)
				{
					methodDefinition.debug_info = symbol_reader.Read(methodDefinition);
				}
			}
		}
	}
	internal sealed class DeferredModuleReader : ModuleReader
	{
		public DeferredModuleReader(Image image)
			: base(image, ReadingMode.Deferred)
		{
		}

		protected override void ReadModule()
		{
			module.Read(module, delegate(ModuleDefinition _, MetadataReader reader)
			{
				ReadModuleManifest(reader);
			});
		}

		public override void ReadSymbols(ModuleDefinition module)
		{
		}
	}
	internal sealed class MetadataReader : ByteBuffer
	{
		internal readonly Image image;

		internal readonly ModuleDefinition module;

		internal readonly MetadataSystem metadata;

		internal CodeReader code;

		internal IGenericContext context;

		private readonly MetadataReader metadata_reader;

		public MetadataReader(ModuleDefinition module)
			: base(module.Image.TableHeap.data)
		{
			image = module.Image;
			this.module = module;
			metadata = module.MetadataSystem;
			code = new CodeReader(this);
		}

		public MetadataReader(Image image, ModuleDefinition module, MetadataReader metadata_reader)
			: base(image.TableHeap.data)
		{
			this.image = image;
			this.module = module;
			metadata = module.MetadataSystem;
			this.metadata_reader = metadata_reader;
		}

		private int GetCodedIndexSize(CodedIndex index)
		{
			return image.GetCodedIndexSize(index);
		}

		private uint ReadByIndexSize(int size)
		{
			if (size == 4)
			{
				return ReadUInt32();
			}
			return ReadUInt16();
		}

		private byte[] ReadBlob()
		{
			BlobHeap blobHeap = image.BlobHeap;
			if (blobHeap == null)
			{
				position += 2;
				return Empty<byte>.Array;
			}
			return blobHeap.Read(ReadBlobIndex());
		}

		private byte[] ReadBlob(uint signature)
		{
			BlobHeap blobHeap = image.BlobHeap;
			if (blobHeap == null)
			{
				return Empty<byte>.Array;
			}
			return blobHeap.Read(signature);
		}

		private uint ReadBlobIndex()
		{
			return ReadByIndexSize(image.BlobHeap?.IndexSize ?? 2);
		}

		private void GetBlobView(uint signature, out byte[] blob, out int index, out int count)
		{
			BlobHeap blobHeap = image.BlobHeap;
			if (blobHeap == null)
			{
				blob = null;
				index = (count = 0);
			}
			else
			{
				blobHeap.GetView(signature, out blob, out index, out count);
			}
		}

		private string ReadString()
		{
			return image.StringHeap.Read(ReadByIndexSize(image.StringHeap.IndexSize));
		}

		private uint ReadStringIndex()
		{
			return ReadByIndexSize(image.StringHeap.IndexSize);
		}

		private Guid ReadGuid()
		{
			return image.GuidHeap.Read(ReadByIndexSize(image.GuidHeap.IndexSize));
		}

		private uint ReadTableIndex(Table table)
		{
			return ReadByIndexSize(image.GetTableIndexSize(table));
		}

		private MetadataToken ReadMetadataToken(CodedIndex index)
		{
			return index.GetMetadataToken(ReadByIndexSize(GetCodedIndexSize(index)));
		}

		private int MoveTo(Table table)
		{
			TableInformation tableInformation = image.TableHeap[table];
			if (tableInformation.Length != 0)
			{
				position = (int)tableInformation.Offset;
			}
			return (int)tableInformation.Length;
		}

		private bool MoveTo(Table table, uint row)
		{
			TableInformation tableInformation = image.TableHeap[table];
			uint num = tableInformation.Length;
			if (num == 0 || row > num)
			{
				return false;
			}
			position = (int)(tableInformation.Offset + tableInformation.RowSize * (row - 1));
			return true;
		}

		public AssemblyNameDefinition ReadAssemblyNameDefinition()
		{
			if (MoveTo(Table.Assembly) == 0)
			{
				return null;
			}
			AssemblyNameDefinition assemblyNameDefinition = new AssemblyNameDefinition();
			assemblyNameDefinition.HashAlgorithm = (AssemblyHashAlgorithm)ReadUInt32();
			PopulateVersionAndFlags(assemblyNameDefinition);
			assemblyNameDefinition.PublicKey = ReadBlob();
			PopulateNameAndCulture(assemblyNameDefinition);
			return assemblyNameDefinition;
		}

		public ModuleDefinition Populate(ModuleDefinition module)
		{
			if (MoveTo(Table.Module) == 0)
			{
				return module;
			}
			Advance(2);
			module.Name = ReadString();
			module.Mvid = ReadGuid();
			return module;
		}

		private void InitializeAssemblyReferences()
		{
			if (metadata.AssemblyReferences != null)
			{
				return;
			}
			int num = MoveTo(Table.AssemblyRef);
			AssemblyNameReference[] array = (metadata.AssemblyReferences = new AssemblyNameReference[num]);
			for (uint num2 = 0u; num2 < num; num2++)
			{
				AssemblyNameReference assemblyNameReference = new AssemblyNameReference();
				assemblyNameReference.token = new MetadataToken(TokenType.AssemblyRef, num2 + 1);
				PopulateVersionAndFlags(assemblyNameReference);
				byte[] array2 = ReadBlob();
				if (assemblyNameReference.HasPublicKey)
				{
					assemblyNameReference.PublicKey = array2;
				}
				else
				{
					assemblyNameReference.PublicKeyToken = array2;
				}
				PopulateNameAndCulture(assemblyNameReference);
				assemblyNameReference.Hash = ReadBlob();
				array[num2] = assemblyNameReference;
			}
		}

		public Collection<AssemblyNameReference> ReadAssemblyReferences()
		{
			InitializeAssemblyReferences();
			Collection<AssemblyNameReference> collection = new Collection<AssemblyNameReference>(metadata.AssemblyReferences);
			if (module.IsWindowsMetadata())
			{
				module.Projections.AddVirtualReferences(collection);
			}
			return collection;
		}

		public MethodDefinition ReadEntryPoint()
		{
			if (module.Image.EntryPointToken == 0)
			{
				return null;
			}
			return GetMethodDefinition(new MetadataToken(module.Image.EntryPointToken).RID);
		}

		public Collection<ModuleDefinition> ReadModules()
		{
			Collection<ModuleDefinition> collection = new Collection<ModuleDefinition>(1);
			collection.Add(module);
			int num = MoveTo(Table.File);
			for (uint num2 = 1u; num2 <= num; num2++)
			{
				FileAttributes fileAttributes = (FileAttributes)ReadUInt32();
				string name = ReadString();
				ReadBlobIndex();
				if (fileAttributes == FileAttributes.ContainsMetaData)
				{
					ReaderParameters parameters = new ReaderParameters
					{
						ReadingMode = module.ReadingMode,
						SymbolReaderProvider = module.SymbolReaderProvider,
						AssemblyResolver = module.AssemblyResolver
					};
					ModuleDefinition moduleDefinition = ModuleDefinition.ReadModule(GetModuleFileName(name), parameters);
					moduleDefinition.assembly = module.assembly;
					collection.Add(moduleDefinition);
				}
			}
			return collection;
		}

		private string GetModuleFileName(string name)
		{
			if (module.FileName == null)
			{
				throw new NotSupportedException();
			}
			string directoryName = Path.GetDirectoryName(module.FileName);
			return Path.Combine(directoryName, name);
		}

		private void InitializeModuleReferences()
		{
			if (metadata.ModuleReferences == null)
			{
				int num = MoveTo(Table.ModuleRef);
				ModuleReference[] array = (metadata.ModuleReferences = new ModuleReference[num]);
				for (uint num2 = 0u; num2 < num; num2++)
				{
					ModuleReference moduleReference = new ModuleReference(ReadString());
					moduleReference.token = new MetadataToken(TokenType.ModuleRef, num2 + 1);
					array[num2] = moduleReference;
				}
			}
		}

		public Collection<ModuleReference> ReadModuleReferences()
		{
			InitializeModuleReferences();
			return new Collection<ModuleReference>(metadata.ModuleReferences);
		}

		public bool HasFileResource()
		{
			int num = MoveTo(Table.File);
			if (num == 0)
			{
				return false;
			}
			for (uint num2 = 1u; num2 <= num; num2++)
			{
				if (ReadFileRecord(num2).Col1 == FileAttributes.ContainsNoMetaData)
				{
					return true;
				}
			}
			return false;
		}

		public Collection<Resource> ReadResources()
		{
			int num = MoveTo(Table.ManifestResource);
			Collection<Resource> collection = new Collection<Resource>(num);
			for (int i = 1; i <= num; i++)
			{
				uint offset = ReadUInt32();
				ManifestResourceAttributes manifestResourceAttributes = (ManifestResourceAttributes)ReadUInt32();
				string name = ReadString();
				MetadataToken scope = ReadMetadataToken(CodedIndex.Implementation);
				Resource item;
				if (scope.RID == 0)
				{
					item = new EmbeddedResource(name, manifestResourceAttributes, offset, this);
				}
				else if (scope.TokenType == TokenType.AssemblyRef)
				{
					item = new AssemblyLinkedResource(name, manifestResourceAttributes)
					{
						Assembly = (AssemblyNameReference)GetTypeReferenceScope(scope)
					};
				}
				else
				{
					if (scope.TokenType != TokenType.File)
					{
						continue;
					}
					Row<FileAttributes, string, uint> row = ReadFileRecord(scope.RID);
					item = new LinkedResource(name, manifestResourceAttributes)
					{
						File = row.Col2,
						hash = ReadBlob(row.Col3)
					};
				}
				collection.Add(item);
			}
			return collection;
		}

		private Row<FileAttributes, string, uint> ReadFileRecord(uint rid)
		{
			int num = position;
			if (!MoveTo(Table.File, rid))
			{
				throw new ArgumentException();
			}
			Row<FileAttributes, string, uint> result = new Row<FileAttributes, string, uint>((FileAttributes)ReadUInt32(), ReadString(), ReadBlobIndex());
			position = num;
			return result;
		}

		public byte[] GetManagedResource(uint offset)
		{
			return image.GetReaderAt(image.Resources.VirtualAddress, offset, delegate(uint o, BinaryStreamReader reader)
			{
				reader.Advance((int)o);
				return reader.ReadBytes(reader.ReadInt32());
			}) ?? Empty<byte>.Array;
		}

		private void PopulateVersionAndFlags(AssemblyNameReference name)
		{
			name.Version = new Version(ReadUInt16(), ReadUInt16(), ReadUInt16(), ReadUInt16());
			name.Attributes = (AssemblyAttributes)ReadUInt32();
		}

		private void PopulateNameAndCulture(AssemblyNameReference name)
		{
			name.Name = ReadString();
			name.Culture = ReadString();
		}

		public TypeDefinitionCollection ReadTypes()
		{
			InitializeTypeDefinitions();
			TypeDefinition[] types = metadata.Types;
			int capacity = types.Length - metadata.NestedTypes.Count;
			TypeDefinitionCollection typeDefinitionCollection = new TypeDefinitionCollection(module, capacity);
			foreach (TypeDefinition typeDefinition in types)
			{
				if (!IsNested(typeDefinition.Attributes))
				{
					typeDefinitionCollection.Add(typeDefinition);
				}
			}
			if (image.HasTable(Table.MethodPtr) || image.HasTable(Table.FieldPtr))
			{
				CompleteTypes();
			}
			return typeDefinitionCollection;
		}

		private void CompleteTypes()
		{
			TypeDefinition[] types = metadata.Types;
			foreach (TypeDefinition typeDefinition in types)
			{
				Mixin.Read(typeDefinition.Fields);
				Mixin.Read(typeDefinition.Methods);
			}
		}

		private void InitializeTypeDefinitions()
		{
			if (metadata.Types != null)
			{
				return;
			}
			InitializeNestedTypes();
			InitializeFields();
			InitializeMethods();
			int num = MoveTo(Table.TypeDef);
			TypeDefinition[] array = (metadata.Types = new TypeDefinition[num]);
			for (uint num2 = 0u; num2 < num; num2++)
			{
				if (array[num2] == null)
				{
					array[num2] = ReadType(num2 + 1);
				}
			}
			if (module.IsWindowsMetadata())
			{
				for (uint num3 = 0u; num3 < num; num3++)
				{
					WindowsRuntimeProjections.Project(array[num3]);
				}
			}
		}

		private static bool IsNested(TypeAttributes attributes)
		{
			TypeAttributes typeAttributes = attributes & TypeAttributes.VisibilityMask;
			TypeAttributes typeAttributes2 = typeAttributes;
			if (typeAttributes2 - 2 <= TypeAttributes.NestedAssembly)
			{
				return true;
			}
			return false;
		}

		public bool HasNestedTypes(TypeDefinition type)
		{
			InitializeNestedTypes();
			if (!metadata.TryGetNestedTypeMapping(type, out var mapping))
			{
				return false;
			}
			return mapping.Count > 0;
		}

		public Collection<TypeDefinition> ReadNestedTypes(TypeDefinition type)
		{
			InitializeNestedTypes();
			if (!metadata.TryGetNestedTypeMapping(type, out var mapping))
			{
				return new MemberDefinitionCollection<TypeDefinition>(type);
			}
			MemberDefinitionCollection<TypeDefinition> memberDefinitionCollection = new MemberDefinitionCollection<TypeDefinition>(type, mapping.Count);
			for (int i = 0; i < mapping.Count; i++)
			{
				TypeDefinition typeDefinition = GetTypeDefinition(mapping[i]);
				if (typeDefinition != null)
				{
					memberDefinitionCollection.Add(typeDefinition);
				}
			}
			return memberDefinitionCollection;
		}

		private void InitializeNestedTypes()
		{
			if (metadata.NestedTypes != null)
			{
				return;
			}
			int num = MoveTo(Table.NestedClass);
			metadata.NestedTypes = new Dictionary<uint, Collection<uint>>(num);
			metadata.ReverseNestedTypes = new Dictionary<uint, uint>(num);
			if (num != 0)
			{
				for (int i = 1; i <= num; i++)
				{
					uint nested = ReadTableIndex(Table.TypeDef);
					uint declaring = ReadTableIndex(Table.TypeDef);
					AddNestedMapping(declaring, nested);
				}
			}
		}

		private void AddNestedMapping(uint declaring, uint nested)
		{
			metadata.SetNestedTypeMapping(declaring, AddMapping(metadata.NestedTypes, declaring, nested));
			metadata.SetReverseNestedTypeMapping(nested, declaring);
		}

		private static Collection<TValue> AddMapping<TKey, TValue>(Dictionary<TKey, Collection<TValue>> cache, TKey key, TValue value)
		{
			if (!cache.TryGetValue(key, out var value2))
			{
				value2 = new Collection<TValue>();
			}
			value2.Add(value);
			return value2;
		}

		private TypeDefinition ReadType(uint rid)
		{
			if (!MoveTo(Table.TypeDef, rid))
			{
				return null;
			}
			TypeAttributes attributes = (TypeAttributes)ReadUInt32();
			string name = ReadString();
			string text = ReadString();
			TypeDefinition typeDefinition = new TypeDefinition(text, name, attributes);
			typeDefinition.token = new MetadataToken(TokenType.TypeDef, rid);
			typeDefinition.scope = module;
			typeDefinition.module = module;
			metadata.AddTypeDefinition(typeDefinition);
			context = typeDefinition;
			typeDefinition.BaseType = GetTypeDefOrRef(ReadMetadataToken(CodedIndex.TypeDefOrRef));
			typeDefinition.fields_range = ReadListRange(rid, Table.TypeDef, Table.Field);
			typeDefinition.methods_range = ReadListRange(rid, Table.TypeDef, Table.Method);
			if (IsNested(attributes))
			{
				typeDefinition.DeclaringType = GetNestedTypeDeclaringType(typeDefinition);
			}
			return typeDefinition;
		}

		private TypeDefinition GetNestedTypeDeclaringType(TypeDefinition type)
		{
			if (!metadata.TryGetReverseNestedTypeMapping(type, out var declaring))
			{
				return null;
			}
			return GetTypeDefinition(declaring);
		}

		private Range ReadListRange(uint current_index, Table current, Table target)
		{
			Range result = default(Range);
			uint num = ReadTableIndex(target);
			if (num == 0)
			{
				return result;
			}
			TableInformation tableInformation = image.TableHeap[current];
			uint num2;
			if (current_index == tableInformation.Length)
			{
				num2 = image.TableHeap[target].Length + 1;
			}
			else
			{
				int num3 = position;
				position += (int)(tableInformation.RowSize - image.GetTableIndexSize(target));
				num2 = ReadTableIndex(target);
				position = num3;
			}
			result.Start = num;
			result.Length = num2 - num;
			return result;
		}

		public Row<short, int> ReadTypeLayout(TypeDefinition type)
		{
			InitializeTypeLayouts();
			uint rID = type.token.RID;
			if (!metadata.ClassLayouts.TryGetValue(rID, out var value))
			{
				return new Row<short, int>(-1, -1);
			}
			type.PackingSize = (short)value.Col1;
			type.ClassSize = (int)value.Col2;
			metadata.ClassLayouts.Remove(rID);
			return new Row<short, int>((short)value.Col1, (int)value.Col2);
		}

		private void InitializeTypeLayouts()
		{
			if (metadata.ClassLayouts == null)
			{
				int num = MoveTo(Table.ClassLayout);
				Dictionary<uint, Row<ushort, uint>> dictionary = (metadata.ClassLayouts = new Dictionary<uint, Row<ushort, uint>>(num));
				for (uint num2 = 0u; num2 < num; num2++)
				{
					ushort col = ReadUInt16();
					uint col2 = ReadUInt32();
					uint key = ReadTableIndex(Table.TypeDef);
					dictionary.Add(key, new Row<ushort, uint>(col, col2));
				}
			}
		}

		public TypeReference GetTypeDefOrRef(MetadataToken token)
		{
			return (TypeReference)LookupToken(token);
		}

		public TypeDefinition GetTypeDefinition(uint rid)
		{
			InitializeTypeDefinitions();
			TypeDefinition typeDefinition = metadata.GetTypeDefinition(rid);
			if (typeDefinition != null)
			{
				return typeDefinition;
			}
			typeDefinition = ReadTypeDefinition(rid);
			if (module.IsWindowsMetadata())
			{
				WindowsRuntimeProjections.Project(typeDefinition);
			}
			return typeDefinition;
		}

		private TypeDefinition ReadTypeDefinition(uint rid)
		{
			if (!MoveTo(Table.TypeDef, rid))
			{
				return null;
			}
			return ReadType(rid);
		}

		private void InitializeTypeReferences()
		{
			if (metadata.TypeReferences == null)
			{
				metadata.TypeReferences = new TypeReference[image.GetTableLength(Table.TypeRef)];
			}
		}

		public TypeReference GetTypeReference(string scope, string full_name)
		{
			InitializeTypeReferences();
			int num = metadata.TypeReferences.Length;
			for (uint num2 = 1u; num2 <= num; num2++)
			{
				TypeReference typeReference = GetTypeReference(num2);
				if (!(typeReference.FullName != full_name))
				{
					if (string.IsNullOrEmpty(scope))
					{
						return typeReference;
					}
					if (typeReference.Scope.Name == scope)
					{
						return typeReference;
					}
				}
			}
			return null;
		}

		private TypeReference GetTypeReference(uint rid)
		{
			InitializeTypeReferences();
			TypeReference typeReference = metadata.GetTypeReference(rid);
			if (typeReference != null)
			{
				return typeReference;
			}
			return ReadTypeReference(rid);
		}

		private TypeReference ReadTypeReference(uint rid)
		{
			if (!MoveTo(Table.TypeRef, rid))
			{
				return null;
			}
			TypeReference typeReference = null;
			MetadataToken metadataToken = ReadMetadataToken(CodedIndex.ResolutionScope);
			string name = ReadString();
			string text = ReadString();
			TypeReference typeReference2 = new TypeReference(text, name, module, null);
			typeReference2.token = new MetadataToken(TokenType.TypeRef, rid);
			metadata.AddTypeReference(typeReference2);
			IMetadataScope scope;
			if (metadataToken.TokenType == TokenType.TypeRef)
			{
				if (metadataToken.RID != rid)
				{
					typeReference = GetTypeDefOrRef(metadataToken);
					IMetadataScope metadataScope2;
					if (typeReference == null)
					{
						IMetadataScope metadataScope = module;
						metadataScope2 = metadataScope;
					}
					else
					{
						metadataScope2 = typeReference.Scope;
					}
					scope = metadataScope2;
				}
				else
				{
					scope = module;
				}
			}
			else
			{
				scope = GetTypeReferenceScope(metadataToken);
			}
			typeReference2.scope = scope;
			typeReference2.DeclaringType = typeReference;
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference2);
			if (typeReference2.Module.IsWindowsMetadata())
			{
				WindowsRuntimeProjections.Project(typeReference2);
			}
			return typeReference2;
		}

		private IMetadataScope GetTypeReferenceScope(MetadataToken scope)
		{
			if (scope.TokenType == TokenType.Module)
			{
				return module;
			}
			IMetadataScope[] array;
			switch (scope.TokenType)
			{
			case TokenType.AssemblyRef:
			{
				InitializeAssemblyReferences();
				IMetadataScope[] moduleReferences = metadata.AssemblyReferences;
				array = moduleReferences;
				break;
			}
			case TokenType.ModuleRef:
			{
				InitializeModuleReferences();
				IMetadataScope[] moduleReferences = metadata.ModuleReferences;
				array = moduleReferences;
				break;
			}
			default:
				throw new NotSupportedException();
			}
			uint num = scope.RID - 1;
			if (num < 0 || num >= array.Length)
			{
				return null;
			}
			return array[num];
		}

		public IEnumerable<TypeReference> GetTypeReferences()
		{
			InitializeTypeReferences();
			int tableLength = image.GetTableLength(Table.TypeRef);
			TypeReference[] array = new TypeReference[tableLength];
			for (uint num = 1u; num <= tableLength; num++)
			{
				array[num - 1] = GetTypeReference(num);
			}
			return array;
		}

		private TypeReference GetTypeSpecification(uint rid)
		{
			if (!MoveTo(Table.TypeSpec, rid))
			{
				return null;
			}
			SignatureReader signatureReader = ReadSignature(ReadBlobIndex());
			TypeReference typeReference = signatureReader.ReadTypeSignature();
			if (typeReference.token.RID == 0)
			{
				typeReference.token = new MetadataToken(TokenType.TypeSpec, rid);
			}
			return typeReference;
		}

		private SignatureReader ReadSignature(uint signature)
		{
			return new SignatureReader(signature, this);
		}

		public bool HasInterfaces(TypeDefinition type)
		{
			InitializeInterfaces();
			Collection<Row<uint, MetadataToken>> mapping;
			return metadata.TryGetInterfaceMapping(type, out mapping);
		}

		public InterfaceImplementationCollection ReadInterfaces(TypeDefinition type)
		{
			InitializeInterfaces();
			if (!metadata.TryGetInterfaceMapping(type, out var mapping))
			{
				return new InterfaceImplementationCollection(type);
			}
			InterfaceImplementationCollection interfaceImplementationCollection = new InterfaceImplementationCollection(type, mapping.Count);
			context = type;
			for (int i = 0; i < mapping.Count; i++)
			{
				interfaceImplementationCollection.Add(new InterfaceImplementation(GetTypeDefOrRef(mapping[i].Col2), new MetadataToken(TokenType.InterfaceImpl, mapping[i].Col1)));
			}
			return interfaceImplementationCollection;
		}

		private void InitializeInterfaces()
		{
			if (metadata.Interfaces == null)
			{
				int num = MoveTo(Table.InterfaceImpl);
				metadata.Interfaces = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(num);
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					uint type = ReadTableIndex(Table.TypeDef);
					MetadataToken col = ReadMetadataToken(CodedIndex.TypeDefOrRef);
					AddInterfaceMapping(type, new Row<uint, MetadataToken>(num2, col));
				}
			}
		}

		private void AddInterfaceMapping(uint type, Row<uint, MetadataToken> @interface)
		{
			metadata.SetInterfaceMapping(type, AddMapping(metadata.Interfaces, type, @interface));
		}

		public Collection<FieldDefinition> ReadFields(TypeDefinition type)
		{
			Range fields_range = type.fields_range;
			if (fields_range.Length == 0)
			{
				return new MemberDefinitionCollection<FieldDefinition>(type);
			}
			MemberDefinitionCollection<FieldDefinition> memberDefinitionCollection = new MemberDefinitionCollection<FieldDefinition>(type, (int)fields_range.Length);
			context = type;
			if (!MoveTo(Table.FieldPtr, fields_range.Start))
			{
				if (!MoveTo(Table.Field, fields_range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0u; num < fields_range.Length; num++)
				{
					ReadField(fields_range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				ReadPointers(Table.FieldPtr, Table.Field, fields_range, memberDefinitionCollection, ReadField);
			}
			return memberDefinitionCollection;
		}

		private void ReadField(uint field_rid, Collection<FieldDefinition> fields)
		{
			FieldAttributes attributes = (FieldAttributes)ReadUInt16();
			string name = ReadString();
			uint signature = ReadBlobIndex();
			FieldDefinition fieldDefinition = new FieldDefinition(name, attributes, ReadFieldType(signature));
			fieldDefinition.token = new MetadataToken(TokenType.Field, field_rid);
			metadata.AddFieldDefinition(fieldDefinition);
			if (!IsDeleted(fieldDefinition))
			{
				fields.Add(fieldDefinition);
				if (module.IsWindowsMetadata())
				{
					WindowsRuntimeProjections.Project(fieldDefinition);
				}
			}
		}

		private void InitializeFields()
		{
			if (metadata.Fields == null)
			{
				metadata.Fields = new FieldDefinition[image.GetTableLength(Table.Field)];
			}
		}

		private TypeReference ReadFieldType(uint signature)
		{
			SignatureReader signatureReader = ReadSignature(signature);
			if (signatureReader.ReadByte() != 6)
			{
				throw new NotSupportedException();
			}
			return signatureReader.ReadTypeSignature();
		}

		public int ReadFieldRVA(FieldDefinition field)
		{
			InitializeFieldRVAs();
			uint rID = field.token.RID;
			if (!metadata.FieldRVAs.TryGetValue(rID, out var value))
			{
				return 0;
			}
			int fieldTypeSize = GetFieldTypeSize(field.FieldType);
			if (fieldTypeSize == 0 || value == 0)
			{
				return 0;
			}
			metadata.FieldRVAs.Remove(rID);
			field.InitialValue = GetFieldInitializeValue(fieldTypeSize, value);
			return (int)value;
		}

		private byte[] GetFieldInitializeValue(int size, uint rva)
		{
			return image.GetReaderAt(rva, size, (int s, BinaryStreamReader reader) => reader.ReadBytes(s)) ?? Empty<byte>.Array;
		}

		private static int GetFieldTypeSize(TypeReference type)
		{
			int result = 0;
			switch (type.etype)
			{
			case ElementType.Boolean:
			case ElementType.I1:
			case ElementType.U1:
				result = 1;
				break;
			case ElementType.Char:
			case ElementType.I2:
			case ElementType.U2:
				result = 2;
				break;
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.R4:
				result = 4;
				break;
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R8:
				result = 8;
				break;
			case ElementType.Ptr:
			case ElementType.FnPtr:
				result = IntPtr.Size;
				break;
			case ElementType.CModReqD:
			case ElementType.CModOpt:
				return GetFieldTypeSize(((IModifierType)type).ElementType);
			default:
			{
				TypeDefinition typeDefinition = type.Resolve();
				if (typeDefinition != null && typeDefinition.HasLayoutInfo)
				{
					result = typeDefinition.ClassSize;
				}
				break;
			}
			}
			return result;
		}

		private void InitializeFieldRVAs()
		{
			if (metadata.FieldRVAs == null)
			{
				int num = MoveTo(Table.FieldRVA);
				Dictionary<uint, uint> dictionary = (metadata.FieldRVAs = new Dictionary<uint, uint>(num));
				for (int i = 0; i < num; i++)
				{
					uint value = ReadUInt32();
					uint key = ReadTableIndex(Table.Field);
					dictionary.Add(key, value);
				}
			}
		}

		public int ReadFieldLayout(FieldDefinition field)
		{
			InitializeFieldLayouts();
			uint rID = field.token.RID;
			if (!metadata.FieldLayouts.TryGetValue(rID, out var value))
			{
				return -1;
			}
			metadata.FieldLayouts.Remove(rID);
			return (int)value;
		}

		private void InitializeFieldLayouts()
		{
			if (metadata.FieldLayouts == null)
			{
				int num = MoveTo(Table.FieldLayout);
				Dictionary<uint, uint> dictionary = (metadata.FieldLayouts = new Dictionary<uint, uint>(num));
				for (int i = 0; i < num; i++)
				{
					uint value = ReadUInt32();
					uint key = ReadTableIndex(Table.Field);
					dictionary.Add(key, value);
				}
			}
		}

		public bool HasEvents(TypeDefinition type)
		{
			InitializeEvents();
			if (!metadata.TryGetEventsRange(type, out var range))
			{
				return false;
			}
			return range.Length != 0;
		}

		public Collection<EventDefinition> ReadEvents(TypeDefinition type)
		{
			InitializeEvents();
			if (!metadata.TryGetEventsRange(type, out var range))
			{
				return new MemberDefinitionCollection<EventDefinition>(type);
			}
			MemberDefinitionCollection<EventDefinition> memberDefinitionCollection = new MemberDefinitionCollection<EventDefinition>(type, (int)range.Length);
			if (range.Length == 0)
			{
				return memberDefinitionCollection;
			}
			context = type;
			if (!MoveTo(Table.EventPtr, range.Start))
			{
				if (!MoveTo(Table.Event, range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0u; num < range.Length; num++)
				{
					ReadEvent(range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				ReadPointers(Table.EventPtr, Table.Event, range, memberDefinitionCollection, ReadEvent);
			}
			return memberDefinitionCollection;
		}

		private void ReadEvent(uint event_rid, Collection<EventDefinition> events)
		{
			EventAttributes attributes = (EventAttributes)ReadUInt16();
			string name = ReadString();
			TypeReference typeDefOrRef = GetTypeDefOrRef(ReadMetadataToken(CodedIndex.TypeDefOrRef));
			EventDefinition eventDefinition = new EventDefinition(name, attributes, typeDefOrRef);
			eventDefinition.token = new MetadataToken(TokenType.Event, event_rid);
			if (!IsDeleted(eventDefinition))
			{
				events.Add(eventDefinition);
			}
		}

		private void InitializeEvents()
		{
			if (metadata.Events == null)
			{
				int num = MoveTo(Table.EventMap);
				metadata.Events = new Dictionary<uint, Range>(num);
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					uint type_rid = ReadTableIndex(Table.TypeDef);
					Range range = ReadListRange(num2, Table.EventMap, Table.Event);
					metadata.AddEventsRange(type_rid, range);
				}
			}
		}

		public bool HasProperties(TypeDefinition type)
		{
			InitializeProperties();
			if (!metadata.TryGetPropertiesRange(type, out var range))
			{
				return false;
			}
			return range.Length != 0;
		}

		public Collection<PropertyDefinition> ReadProperties(TypeDefinition type)
		{
			InitializeProperties();
			if (!metadata.TryGetPropertiesRange(type, out var range))
			{
				return new MemberDefinitionCollection<PropertyDefinition>(type);
			}
			MemberDefinitionCollection<PropertyDefinition> memberDefinitionCollection = new MemberDefinitionCollection<PropertyDefinition>(type, (int)range.Length);
			if (range.Length == 0)
			{
				return memberDefinitionCollection;
			}
			context = type;
			if (!MoveTo(Table.PropertyPtr, range.Start))
			{
				if (!MoveTo(Table.Property, range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0u; num < range.Length; num++)
				{
					ReadProperty(range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				ReadPointers(Table.PropertyPtr, Table.Property, range, memberDefinitionCollection, ReadProperty);
			}
			return memberDefinitionCollection;
		}

		private void ReadProperty(uint property_rid, Collection<PropertyDefinition> properties)
		{
			PropertyAttributes attributes = (PropertyAttributes)ReadUInt16();
			string name = ReadString();
			uint signature = ReadBlobIndex();
			SignatureReader signatureReader = ReadSignature(signature);
			byte b = signatureReader.ReadByte();
			if ((b & 8) == 0)
			{
				throw new NotSupportedException();
			}
			bool hasThis = (b & 0x20) != 0;
			signatureReader.ReadCompressedUInt32();
			PropertyDefinition propertyDefinition = new PropertyDefinition(name, attributes, signatureReader.ReadTypeSignature());
			propertyDefinition.HasThis = hasThis;
			propertyDefinition.token = new MetadataToken(TokenType.Property, property_rid);
			if (!IsDeleted(propertyDefinition))
			{
				properties.Add(propertyDefinition);
			}
		}

		private void InitializeProperties()
		{
			if (metadata.Properties == null)
			{
				int num = MoveTo(Table.PropertyMap);
				metadata.Properties = new Dictionary<uint, Range>(num);
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					uint type_rid = ReadTableIndex(Table.TypeDef);
					Range range = ReadListRange(num2, Table.PropertyMap, Table.Property);
					metadata.AddPropertiesRange(type_rid, range);
				}
			}
		}

		private MethodSemanticsAttributes ReadMethodSemantics(MethodDefinition method)
		{
			InitializeMethodSemantics();
			if (!metadata.Semantics.TryGetValue(method.token.RID, out var value))
			{
				return MethodSemanticsAttributes.None;
			}
			TypeDefinition declaringType = method.DeclaringType;
			switch (value.Col1)
			{
			case MethodSemanticsAttributes.AddOn:
				GetEvent(declaringType, value.Col2).add_method = method;
				break;
			case MethodSemanticsAttributes.Fire:
				GetEvent(declaringType, value.Col2).invoke_method = method;
				break;
			case MethodSemanticsAttributes.RemoveOn:
				GetEvent(declaringType, value.Col2).remove_method = method;
				break;
			case MethodSemanticsAttributes.Getter:
				GetProperty(declaringType, value.Col2).get_method = method;
				break;
			case MethodSemanticsAttributes.Setter:
				GetProperty(declaringType, value.Col2).set_method = method;
				break;
			case MethodSemanticsAttributes.Other:
				switch (value.Col2.TokenType)
				{
				case TokenType.Event:
				{
					EventDefinition eventDefinition = GetEvent(declaringType, value.Col2);
					if (eventDefinition.other_methods == null)
					{
						eventDefinition.other_methods = new Collection<MethodDefinition>();
					}
					eventDefinition.other_methods.Add(method);
					break;
				}
				case TokenType.Property:
				{
					PropertyDefinition property = GetProperty(declaringType, value.Col2);
					if (property.other_methods == null)
					{
						property.other_methods = new Collection<MethodDefinition>();
					}
					property.other_methods.Add(method);
					break;
				}
				default:
					throw new NotSupportedException();
				}
				break;
			default:
				throw new NotSupportedException();
			}
			metadata.Semantics.Remove(method.token.RID);
			return value.Col1;
		}

		private static EventDefinition GetEvent(TypeDefinition type, MetadataToken token)
		{
			if (token.TokenType != TokenType.Event)
			{
				throw new ArgumentException();
			}
			return GetMember(type.Events, token);
		}

		private static PropertyDefinition GetProperty(TypeDefinition type, MetadataToken token)
		{
			if (token.TokenType != TokenType.Property)
			{
				throw new ArgumentException();
			}
			return GetMember(type.Properties, token);
		}

		private static TMember GetMember<TMember>(Collection<TMember> members, MetadataToken token) where TMember : IMemberDefinition
		{
			for (int i = 0; i < members.Count; i++)
			{
				TMember result = members[i];
				if (result.MetadataToken == token)
				{
					return result;
				}
			}
			throw new ArgumentException();
		}

		private void InitializeMethodSemantics()
		{
			if (metadata.Semantics == null)
			{
				int num = MoveTo(Table.MethodSemantics);
				Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> dictionary = (metadata.Semantics = new Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>>(0));
				for (uint num2 = 0u; num2 < num; num2++)
				{
					MethodSemanticsAttributes col = (MethodSemanticsAttributes)ReadUInt16();
					uint key = ReadTableIndex(Table.Method);
					MetadataToken col2 = ReadMetadataToken(CodedIndex.HasSemantics);
					dictionary[key] = new Row<MethodSemanticsAttributes, MetadataToken>(col, col2);
				}
			}
		}

		public void ReadMethods(PropertyDefinition property)
		{
			ReadAllSemantics(property.DeclaringType);
		}

		public void ReadMethods(EventDefinition @event)
		{
			ReadAllSemantics(@event.DeclaringType);
		}

		public void ReadAllSemantics(MethodDefinition method)
		{
			ReadAllSemantics(method.DeclaringType);
		}

		private void ReadAllSemantics(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (!methodDefinition.sem_attrs_ready)
				{
					methodDefinition.sem_attrs = ReadMethodSemantics(methodDefinition);
					methodDefinition.sem_attrs_ready = true;
				}
			}
		}

		public Collection<MethodDefinition> ReadMethods(TypeDefinition type)
		{
			Range methods_range = type.methods_range;
			if (methods_range.Length == 0)
			{
				return new MemberDefinitionCollection<MethodDefinition>(type);
			}
			MemberDefinitionCollection<MethodDefinition> memberDefinitionCollection = new MemberDefinitionCollection<MethodDefinition>(type, (int)methods_range.Length);
			if (!MoveTo(Table.MethodPtr, methods_range.Start))
			{
				if (!MoveTo(Table.Method, methods_range.Start))
				{
					return memberDefinitionCollection;
				}
				for (uint num = 0u; num < methods_range.Length; num++)
				{
					ReadMethod(methods_range.Start + num, memberDefinitionCollection);
				}
			}
			else
			{
				ReadPointers(Table.MethodPtr, Table.Method, methods_range, memberDefinitionCollection, ReadMethod);
			}
			return memberDefinitionCollection;
		}

		private void ReadPointers<TMember>(Table ptr, Table table, Range range, Collection<TMember> members, Action<uint, Collection<TMember>> reader) where TMember : IMemberDefinition
		{
			for (uint num = 0u; num < range.Length; num++)
			{
				MoveTo(ptr, range.Start + num);
				uint num2 = ReadTableIndex(table);
				MoveTo(table, num2);
				reader(num2, members);
			}
		}

		private static bool IsDeleted(IMemberDefinition member)
		{
			return member.IsSpecialName && member.Name == "_Deleted";
		}

		private void InitializeMethods()
		{
			if (metadata.Methods == null)
			{
				metadata.Methods = new MethodDefinition[image.GetTableLength(Table.Method)];
			}
		}

		private void ReadMethod(uint method_rid, Collection<MethodDefinition> methods)
		{
			MethodDefinition methodDefinition = new MethodDefinition();
			methodDefinition.rva = ReadUInt32();
			methodDefinition.ImplAttributes = (MethodImplAttributes)ReadUInt16();
			methodDefinition.Attributes = (MethodAttributes)ReadUInt16();
			methodDefinition.Name = ReadString();
			methodDefinition.token = new MetadataToken(TokenType.Method, method_rid);
			if (!IsDeleted(methodDefinition))
			{
				methods.Add(methodDefinition);
				uint signature = ReadBlobIndex();
				Range param_range = ReadListRange(method_rid, Table.Method, Table.Param);
				context = methodDefinition;
				ReadMethodSignature(signature, methodDefinition);
				metadata.AddMethodDefinition(methodDefinition);
				if (param_range.Length != 0)
				{
					int num = position;
					ReadParameters(methodDefinition, param_range);
					position = num;
				}
				if (module.IsWindowsMetadata())
				{
					WindowsRuntimeProjections.Project(methodDefinition);
				}
			}
		}

		private void ReadParameters(MethodDefinition method, Range param_range)
		{
			if (!MoveTo(Table.ParamPtr, param_range.Start))
			{
				if (MoveTo(Table.Param, param_range.Start))
				{
					for (uint num = 0u; num < param_range.Length; num++)
					{
						ReadParameter(param_range.Start + num, method);
					}
				}
			}
			else
			{
				ReadParameterPointers(method, param_range);
			}
		}

		private void ReadParameterPointers(MethodDefinition method, Range range)
		{
			for (uint num = 0u; num < range.Length; num++)
			{
				MoveTo(Table.ParamPtr, range.Start + num);
				uint num2 = ReadTableIndex(Table.Param);
				MoveTo(Table.Param, num2);
				ReadParameter(num2, method);
			}
		}

		private void ReadParameter(uint param_rid, MethodDefinition method)
		{
			ParameterAttributes attributes = (ParameterAttributes)ReadUInt16();
			ushort num = ReadUInt16();
			string name = ReadString();
			ParameterDefinition parameterDefinition = ((num == 0) ? method.MethodReturnType.Parameter : method.Parameters[num - 1]);
			parameterDefinition.token = new MetadataToken(TokenType.Param, param_rid);
			parameterDefinition.Name = name;
			parameterDefinition.Attributes = attributes;
		}

		private void ReadMethodSignature(uint signature, IMethodSignature method)
		{
			SignatureReader signatureReader = ReadSignature(signature);
			signatureReader.ReadMethodSignature(method);
		}

		public PInvokeInfo ReadPInvokeInfo(MethodDefinition method)
		{
			InitializePInvokes();
			uint rID = method.token.RID;
			if (!metadata.PInvokes.TryGetValue(rID, out var value))
			{
				return null;
			}
			metadata.PInvokes.Remove(rID);
			return new PInvokeInfo(value.Col1, image.StringHeap.Read(value.Col2), module.ModuleReferences[(int)(value.Col3 - 1)]);
		}

		private void InitializePInvokes()
		{
			if (metadata.PInvokes != null)
			{
				return;
			}
			int num = MoveTo(Table.ImplMap);
			Dictionary<uint, Row<PInvokeAttributes, uint, uint>> dictionary = (metadata.PInvokes = new Dictionary<uint, Row<PInvokeAttributes, uint, uint>>(num));
			for (int i = 1; i <= num; i++)
			{
				PInvokeAttributes col = (PInvokeAttributes)ReadUInt16();
				MetadataToken metadataToken = ReadMetadataToken(CodedIndex.MemberForwarded);
				uint col2 = ReadStringIndex();
				uint col3 = ReadTableIndex(Table.File);
				if (metadataToken.TokenType == TokenType.Method)
				{
					dictionary.Add(metadataToken.RID, new Row<PInvokeAttributes, uint, uint>(col, col2, col3));
				}
			}
		}

		public bool HasGenericParameters(IGenericParameterProvider provider)
		{
			InitializeGenericParameters();
			if (!metadata.TryGetGenericParameterRanges(provider, out var ranges))
			{
				return false;
			}
			return RangesSize(ranges) > 0;
		}

		public Collection<GenericParameter> ReadGenericParameters(IGenericParameterProvider provider)
		{
			InitializeGenericParameters();
			if (!metadata.TryGetGenericParameterRanges(provider, out var ranges))
			{
				return new GenericParameterCollection(provider);
			}
			GenericParameterCollection genericParameterCollection = new GenericParameterCollection(provider, RangesSize(ranges));
			for (int i = 0; i < ranges.Length; i++)
			{
				ReadGenericParametersRange(ranges[i], provider, genericParameterCollection);
			}
			return genericParameterCollection;
		}

		private void ReadGenericParametersRange(Range range, IGenericParameterProvider provider, GenericParameterCollection generic_parameters)
		{
			if (MoveTo(Table.GenericParam, range.Start))
			{
				for (uint num = 0u; num < range.Length; num++)
				{
					ReadUInt16();
					GenericParameterAttributes attributes = (GenericParameterAttributes)ReadUInt16();
					ReadMetadataToken(CodedIndex.TypeOrMethodDef);
					string name = ReadString();
					GenericParameter genericParameter = new GenericParameter(name, provider);
					genericParameter.token = new MetadataToken(TokenType.GenericParam, range.Start + num);
					genericParameter.Attributes = attributes;
					generic_parameters.Add(genericParameter);
				}
			}
		}

		private void InitializeGenericParameters()
		{
			if (metadata.GenericParameters == null)
			{
				metadata.GenericParameters = InitializeRanges(Table.GenericParam, delegate
				{
					Advance(4);
					MetadataToken result = ReadMetadataToken(CodedIndex.TypeOrMethodDef);
					ReadStringIndex();
					return result;
				});
			}
		}

		private Dictionary<MetadataToken, Range[]> InitializeRanges(Table table, Func<MetadataToken> get_next)
		{
			int num = MoveTo(table);
			Dictionary<MetadataToken, Range[]> dictionary = new Dictionary<MetadataToken, Range[]>(num);
			if (num == 0)
			{
				return dictionary;
			}
			MetadataToken metadataToken = MetadataToken.Zero;
			Range range = new Range(1u, 0u);
			for (uint num2 = 1u; num2 <= num; num2++)
			{
				MetadataToken metadataToken2 = get_next();
				if (num2 == 1)
				{
					metadataToken = metadataToken2;
					range.Length++;
				}
				else if (metadataToken2 != metadataToken)
				{
					AddRange(dictionary, metadataToken, range);
					range = new Range(num2, 1u);
					metadataToken = metadataToken2;
				}
				else
				{
					range.Length++;
				}
			}
			AddRange(dictionary, metadataToken, range);
			return dictionary;
		}

		private static void AddRange(Dictionary<MetadataToken, Range[]> ranges, MetadataToken owner, Range range)
		{
			if (owner.RID != 0)
			{
				if (!ranges.TryGetValue(owner, out var value))
				{
					ranges.Add(owner, new Range[1] { range });
				}
				else
				{
					ranges[owner] = value.Add(range);
				}
			}
		}

		public bool HasGenericConstraints(GenericParameter generic_parameter)
		{
			InitializeGenericConstraints();
			if (!metadata.TryGetGenericConstraintMapping(generic_parameter, out var mapping))
			{
				return false;
			}
			return mapping.Count > 0;
		}

		public GenericParameterConstraintCollection ReadGenericConstraints(GenericParameter generic_parameter)
		{
			InitializeGenericConstraints();
			if (!metadata.TryGetGenericConstraintMapping(generic_parameter, out var mapping))
			{
				return new GenericParameterConstraintCollection(generic_parameter);
			}
			GenericParameterConstraintCollection genericParameterConstraintCollection = new GenericParameterConstraintCollection(generic_parameter, mapping.Count);
			context = (IGenericContext)generic_parameter.Owner;
			for (int i = 0; i < mapping.Count; i++)
			{
				genericParameterConstraintCollection.Add(new GenericParameterConstraint(GetTypeDefOrRef(mapping[i].Col2), new MetadataToken(TokenType.GenericParamConstraint, mapping[i].Col1)));
			}
			return genericParameterConstraintCollection;
		}

		private void InitializeGenericConstraints()
		{
			if (metadata.GenericConstraints == null)
			{
				int num = MoveTo(Table.GenericParamConstraint);
				metadata.GenericConstraints = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(num);
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					AddGenericConstraintMapping(ReadTableIndex(Table.GenericParam), new Row<uint, MetadataToken>(num2, ReadMetadataToken(CodedIndex.TypeDefOrRef)));
				}
			}
		}

		private void AddGenericConstraintMapping(uint generic_parameter, Row<uint, MetadataToken> constraint)
		{
			metadata.SetGenericConstraintMapping(generic_parameter, AddMapping(metadata.GenericConstraints, generic_parameter, constraint));
		}

		public bool HasOverrides(MethodDefinition method)
		{
			InitializeOverrides();
			if (!metadata.TryGetOverrideMapping(method, out var mapping))
			{
				return false;
			}
			return mapping.Count > 0;
		}

		public Collection<MethodReference> ReadOverrides(MethodDefinition method)
		{
			InitializeOverrides();
			if (!metadata.TryGetOverrideMapping(method, out var mapping))
			{
				return new Collection<MethodReference>();
			}
			Collection<MethodReference> collection = new Collection<MethodReference>(mapping.Count);
			context = method;
			for (int i = 0; i < mapping.Count; i++)
			{
				collection.Add((MethodReference)LookupToken(mapping[i]));
			}
			return collection;
		}

		private void InitializeOverrides()
		{
			if (metadata.Overrides != null)
			{
				return;
			}
			int num = MoveTo(Table.MethodImpl);
			metadata.Overrides = new Dictionary<uint, Collection<MetadataToken>>(num);
			for (int i = 1; i <= num; i++)
			{
				ReadTableIndex(Table.TypeDef);
				MetadataToken metadataToken = ReadMetadataToken(CodedIndex.MethodDefOrRef);
				if (metadataToken.TokenType != TokenType.Method)
				{
					throw new NotSupportedException();
				}
				MetadataToken metadataToken2 = ReadMetadataToken(CodedIndex.MethodDefOrRef);
				AddOverrideMapping(metadataToken.RID, metadataToken2);
			}
		}

		private void AddOverrideMapping(uint method_rid, MetadataToken @override)
		{
			metadata.SetOverrideMapping(method_rid, AddMapping(metadata.Overrides, method_rid, @override));
		}

		public Mono.Cecil.Cil.MethodBody ReadMethodBody(MethodDefinition method)
		{
			return code.ReadMethodBody(method);
		}

		public int ReadCodeSize(MethodDefinition method)
		{
			return code.ReadCodeSize(method);
		}

		public CallSite ReadCallSite(MetadataToken token)
		{
			if (!MoveTo(Table.StandAloneSig, token.RID))
			{
				return null;
			}
			uint signature = ReadBlobIndex();
			CallSite callSite = new CallSite();
			ReadMethodSignature(signature, callSite);
			callSite.MetadataToken = token;
			return callSite;
		}

		public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token, MethodDefinition method = null)
		{
			if (!MoveTo(Table.StandAloneSig, local_var_token.RID))
			{
				return null;
			}
			SignatureReader signatureReader = ReadSignature(ReadBlobIndex());
			if (signatureReader.ReadByte() != 7)
			{
				throw new NotSupportedException();
			}
			uint num = signatureReader.ReadCompressedUInt32();
			if (num == 0)
			{
				return null;
			}
			VariableDefinitionCollection variableDefinitionCollection = new VariableDefinitionCollection(method, (int)num);
			for (int i = 0; i < num; i++)
			{
				variableDefinitionCollection.Add(new VariableDefinition(signatureReader.ReadTypeSignature()));
			}
			return variableDefinitionCollection;
		}

		public IMetadataTokenProvider LookupToken(MetadataToken token)
		{
			uint rID = token.RID;
			if (rID == 0)
			{
				return null;
			}
			if (metadata_reader != null)
			{
				return metadata_reader.LookupToken(token);
			}
			int num = position;
			IGenericContext genericContext = context;
			IMetadataTokenProvider result;
			switch (token.TokenType)
			{
			case TokenType.TypeDef:
				result = GetTypeDefinition(rID);
				break;
			case TokenType.TypeRef:
				result = GetTypeReference(rID);
				break;
			case TokenType.TypeSpec:
				result = GetTypeSpecification(rID);
				break;
			case TokenType.Field:
				result = GetFieldDefinition(rID);
				break;
			case TokenType.Method:
				result = GetMethodDefinition(rID);
				break;
			case TokenType.MemberRef:
				result = GetMemberReference(rID);
				break;
			case TokenType.MethodSpec:
				result = GetMethodSpecification(rID);
				break;
			default:
				return null;
			}
			position = num;
			context = genericContext;
			return result;
		}

		public FieldDefinition GetFieldDefinition(uint rid)
		{
			InitializeTypeDefinitions();
			FieldDefinition fieldDefinition = metadata.GetFieldDefinition(rid);
			if (fieldDefinition != null)
			{
				return fieldDefinition;
			}
			return LookupField(rid);
		}

		private FieldDefinition LookupField(uint rid)
		{
			TypeDefinition fieldDeclaringType = metadata.GetFieldDeclaringType(rid);
			if (fieldDeclaringType == null)
			{
				return null;
			}
			Mixin.Read(fieldDeclaringType.Fields);
			return metadata.GetFieldDefinition(rid);
		}

		public MethodDefinition GetMethodDefinition(uint rid)
		{
			InitializeTypeDefinitions();
			MethodDefinition methodDefinition = metadata.GetMethodDefinition(rid);
			if (methodDefinition != null)
			{
				return methodDefinition;
			}
			return LookupMethod(rid);
		}

		private MethodDefinition LookupMethod(uint rid)
		{
			TypeDefinition methodDeclaringType = metadata.GetMethodDeclaringType(rid);
			if (methodDeclaringType == null)
			{
				return null;
			}
			Mixin.Read(methodDeclaringType.Methods);
			return metadata.GetMethodDefinition(rid);
		}

		private MethodSpecification GetMethodSpecification(uint rid)
		{
			if (!MoveTo(Table.MethodSpec, rid))
			{
				return null;
			}
			MethodReference method = (MethodReference)LookupToken(ReadMetadataToken(CodedIndex.MethodDefOrRef));
			uint signature = ReadBlobIndex();
			MethodSpecification methodSpecification = ReadMethodSpecSignature(signature, method);
			methodSpecification.token = new MetadataToken(TokenType.MethodSpec, rid);
			return methodSpecification;
		}

		private MethodSpecification ReadMethodSpecSignature(uint signature, MethodReference method)
		{
			SignatureReader signatureReader = ReadSignature(signature);
			byte b = signatureReader.ReadByte();
			if (b != 10)
			{
				throw new NotSupportedException();
			}
			uint arity = signatureReader.ReadCompressedUInt32();
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(method, (int)arity);
			signatureReader.ReadGenericInstanceSignature(method, genericInstanceMethod, arity);
			return genericInstanceMethod;
		}

		private MemberReference GetMemberReference(uint rid)
		{
			InitializeMemberReferences();
			MemberReference memberReference = metadata.GetMemberReference(rid);
			if (memberReference != null)
			{
				return memberReference;
			}
			memberReference = ReadMemberReference(rid);
			if (memberReference != null && !memberReference.ContainsGenericParameter)
			{
				metadata.AddMemberReference(memberReference);
			}
			return memberReference;
		}

		private MemberReference ReadMemberReference(uint rid)
		{
			if (!MoveTo(Table.MemberRef, rid))
			{
				return null;
			}
			MetadataToken metadataToken = ReadMetadataToken(CodedIndex.MemberRefParent);
			string name = ReadString();
			uint signature = ReadBlobIndex();
			MemberReference memberReference;
			switch (metadataToken.TokenType)
			{
			case TokenType.TypeRef:
			case TokenType.TypeDef:
			case TokenType.TypeSpec:
				memberReference = ReadTypeMemberReference(metadataToken, name, signature);
				break;
			case TokenType.Method:
				memberReference = ReadMethodMemberReference(metadataToken, name, signature);
				break;
			default:
				throw new NotSupportedException();
			}
			memberReference.token = new MetadataToken(TokenType.MemberRef, rid);
			return memberReference;
		}

		private MemberReference ReadTypeMemberReference(MetadataToken type, string name, uint signature)
		{
			TypeReference typeDefOrRef = GetTypeDefOrRef(type);
			if (!typeDefOrRef.IsArray)
			{
				context = typeDefOrRef;
			}
			MemberReference memberReference = ReadMemberReferenceSignature(signature, typeDefOrRef);
			memberReference.Name = name;
			return memberReference;
		}

		private MemberReference ReadMemberReferenceSignature(uint signature, TypeReference declaring_type)
		{
			SignatureReader signatureReader = ReadSignature(signature);
			if (signatureReader.buffer[signatureReader.position] == 6)
			{
				signatureReader.position++;
				FieldReference fieldReference = new FieldReference();
				fieldReference.DeclaringType = declaring_type;
				fieldReference.FieldType = signatureReader.ReadTypeSignature();
				return fieldReference;
			}
			MethodReference methodReference = new MethodReference();
			methodReference.DeclaringType = declaring_type;
			signatureReader.ReadMethodSignature(methodReference);
			return methodReference;
		}

		private MemberReference ReadMethodMemberReference(MetadataToken token, string name, uint signature)
		{
			MemberReference memberReference = ReadMemberReferenceSignature(signature, ((MethodDefinition)(context = GetMethodDefinition(token.RID))).DeclaringType);
			memberReference.Name = name;
			return memberReference;
		}

		private void InitializeMemberReferences()
		{
			if (metadata.MemberReferences == null)
			{
				metadata.MemberReferences = new MemberReference[image.GetTableLength(Table.MemberRef)];
			}
		}

		public IEnumerable<MemberReference> GetMemberReferences()
		{
			InitializeMemberReferences();
			int tableLength = image.GetTableLength(Table.MemberRef);
			TypeSystem typeSystem = module.TypeSystem;
			MethodDefinition methodDefinition = new MethodDefinition(string.Empty, MethodAttributes.Static, typeSystem.Void);
			methodDefinition.DeclaringType = new TypeDefinition(string.Empty, string.Empty, TypeAttributes.Public);
			MemberReference[] array = new MemberReference[tableLength];
			for (uint num = 1u; num <= tableLength; num++)
			{
				context = methodDefinition;
				array[num - 1] = GetMemberReference(num);
			}
			return array;
		}

		private void InitializeConstants()
		{
			if (metadata.Constants == null)
			{
				int num = MoveTo(Table.Constant);
				Dictionary<MetadataToken, Row<ElementType, uint>> dictionary = (metadata.Constants = new Dictionary<MetadataToken, Row<ElementType, uint>>(num));
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					ElementType col = (ElementType)ReadUInt16();
					MetadataToken key = ReadMetadataToken(CodedIndex.HasConstant);
					uint col2 = ReadBlobIndex();
					dictionary.Add(key, new Row<ElementType, uint>(col, col2));
				}
			}
		}

		public TypeReference ReadConstantSignature(MetadataToken token)
		{
			if (token.TokenType != TokenType.Signature)
			{
				throw new NotSupportedException();
			}
			if (token.RID == 0)
			{
				return null;
			}
			if (!MoveTo(Table.StandAloneSig, token.RID))
			{
				return null;
			}
			return ReadFieldType(ReadBlobIndex());
		}

		public object ReadConstant(IConstantProvider owner)
		{
			InitializeConstants();
			if (!metadata.Constants.TryGetValue(owner.MetadataToken, out var value))
			{
				return Mixin.NoValue;
			}
			metadata.Constants.Remove(owner.MetadataToken);
			return ReadConstantValue(value.Col1, value.Col2);
		}

		private object ReadConstantValue(ElementType etype, uint signature)
		{
			switch (etype)
			{
			case ElementType.Class:
			case ElementType.Object:
				return null;
			case ElementType.String:
				return ReadConstantString(signature);
			default:
				return ReadConstantPrimitive(etype, signature);
			}
		}

		private string ReadConstantString(uint signature)
		{
			GetBlobView(signature, out var blob, out var index, out var count);
			if (count == 0)
			{
				return string.Empty;
			}
			if ((count & 1) == 1)
			{
				count--;
			}
			return Encoding.Unicode.GetString(blob, index, count);
		}

		private object ReadConstantPrimitive(ElementType type, uint signature)
		{
			SignatureReader signatureReader = ReadSignature(signature);
			return signatureReader.ReadConstantSignature(type);
		}

		internal void InitializeCustomAttributes()
		{
			if (metadata.CustomAttributes == null)
			{
				metadata.CustomAttributes = InitializeRanges(Table.CustomAttribute, delegate
				{
					MetadataToken result = ReadMetadataToken(CodedIndex.HasCustomAttribute);
					ReadMetadataToken(CodedIndex.CustomAttributeType);
					ReadBlobIndex();
					return result;
				});
			}
		}

		public bool HasCustomAttributes(ICustomAttributeProvider owner)
		{
			InitializeCustomAttributes();
			if (!metadata.TryGetCustomAttributeRanges(owner, out var ranges))
			{
				return false;
			}
			return RangesSize(ranges) > 0;
		}

		public Collection<CustomAttribute> ReadCustomAttributes(ICustomAttributeProvider owner)
		{
			InitializeCustomAttributes();
			if (!metadata.TryGetCustomAttributeRanges(owner, out var ranges))
			{
				return new Collection<CustomAttribute>();
			}
			Collection<CustomAttribute> collection = new Collection<CustomAttribute>(RangesSize(ranges));
			for (int i = 0; i < ranges.Length; i++)
			{
				ReadCustomAttributeRange(ranges[i], collection);
			}
			if (module.IsWindowsMetadata())
			{
				foreach (CustomAttribute item in collection)
				{
					WindowsRuntimeProjections.Project(owner, collection, item);
				}
			}
			return collection;
		}

		private void ReadCustomAttributeRange(Range range, Collection<CustomAttribute> custom_attributes)
		{
			if (MoveTo(Table.CustomAttribute, range.Start))
			{
				for (int i = 0; i < range.Length; i++)
				{
					ReadMetadataToken(CodedIndex.HasCustomAttribute);
					MethodReference constructor = (MethodReference)LookupToken(ReadMetadataToken(CodedIndex.CustomAttributeType));
					uint signature = ReadBlobIndex();
					custom_attributes.Add(new CustomAttribute(signature, constructor));
				}
			}
		}

		private static int RangesSize(Range[] ranges)
		{
			uint num = 0u;
			for (int i = 0; i < ranges.Length; i++)
			{
				num += ranges[i].Length;
			}
			return (int)num;
		}

		public IEnumerable<CustomAttribute> GetCustomAttributes()
		{
			InitializeTypeDefinitions();
			uint capacity = image.TableHeap[Table.CustomAttribute].Length;
			Collection<CustomAttribute> collection = new Collection<CustomAttribute>((int)capacity);
			ReadCustomAttributeRange(new Range(1u, capacity), collection);
			return collection;
		}

		public byte[] ReadCustomAttributeBlob(uint signature)
		{
			return ReadBlob(signature);
		}

		public void ReadCustomAttributeSignature(CustomAttribute attribute)
		{
			SignatureReader signatureReader = ReadSignature(attribute.signature);
			if (!signatureReader.CanReadMore())
			{
				return;
			}
			if (signatureReader.ReadUInt16() != 1)
			{
				throw new InvalidOperationException();
			}
			MethodReference constructor = attribute.Constructor;
			if (constructor.HasParameters)
			{
				signatureReader.ReadCustomAttributeConstructorArguments(attribute, constructor.Parameters);
			}
			if (signatureReader.CanReadMore())
			{
				ushort num = signatureReader.ReadUInt16();
				if (num != 0)
				{
					signatureReader.ReadCustomAttributeNamedArguments(num, ref attribute.fields, ref attribute.properties);
				}
			}
		}

		private void InitializeMarshalInfos()
		{
			if (metadata.FieldMarshals != null)
			{
				return;
			}
			int num = MoveTo(Table.FieldMarshal);
			Dictionary<MetadataToken, uint> dictionary = (metadata.FieldMarshals = new Dictionary<MetadataToken, uint>(num));
			for (int i = 0; i < num; i++)
			{
				MetadataToken key = ReadMetadataToken(CodedIndex.HasFieldMarshal);
				uint value = ReadBlobIndex();
				if (key.RID != 0)
				{
					dictionary.Add(key, value);
				}
			}
		}

		public bool HasMarshalInfo(IMarshalInfoProvider owner)
		{
			InitializeMarshalInfos();
			return metadata.FieldMarshals.ContainsKey(owner.MetadataToken);
		}

		public MarshalInfo ReadMarshalInfo(IMarshalInfoProvider owner)
		{
			InitializeMarshalInfos();
			if (!metadata.FieldMarshals.TryGetValue(owner.MetadataToken, out var value))
			{
				return null;
			}
			SignatureReader signatureReader = ReadSignature(value);
			metadata.FieldMarshals.Remove(owner.MetadataToken);
			return signatureReader.ReadMarshalInfo();
		}

		private void InitializeSecurityDeclarations()
		{
			if (metadata.SecurityDeclarations == null)
			{
				metadata.SecurityDeclarations = InitializeRanges(Table.DeclSecurity, delegate
				{
					ReadUInt16();
					MetadataToken result = ReadMetadataToken(CodedIndex.HasDeclSecurity);
					ReadBlobIndex();
					return result;
				});
			}
		}

		public bool HasSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			InitializeSecurityDeclarations();
			if (!metadata.TryGetSecurityDeclarationRanges(owner, out var ranges))
			{
				return false;
			}
			return RangesSize(ranges) > 0;
		}

		public Collection<SecurityDeclaration> ReadSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			InitializeSecurityDeclarations();
			if (!metadata.TryGetSecurityDeclarationRanges(owner, out var ranges))
			{
				return new Collection<SecurityDeclaration>();
			}
			Collection<SecurityDeclaration> collection = new Collection<SecurityDeclaration>(RangesSize(ranges));
			for (int i = 0; i < ranges.Length; i++)
			{
				ReadSecurityDeclarationRange(ranges[i], collection);
			}
			return collection;
		}

		private void ReadSecurityDeclarationRange(Range range, Collection<SecurityDeclaration> security_declarations)
		{
			if (MoveTo(Table.DeclSecurity, range.Start))
			{
				for (int i = 0; i < range.Length; i++)
				{
					SecurityAction action = (SecurityAction)ReadUInt16();
					ReadMetadataToken(CodedIndex.HasDeclSecurity);
					uint signature = ReadBlobIndex();
					security_declarations.Add(new SecurityDeclaration(action, signature, module));
				}
			}
		}

		public byte[] ReadSecurityDeclarationBlob(uint signature)
		{
			return ReadBlob(signature);
		}

		public void ReadSecurityDeclarationSignature(SecurityDeclaration declaration)
		{
			uint signature = declaration.signature;
			SignatureReader signatureReader = ReadSignature(signature);
			if (signatureReader.buffer[signatureReader.position] != 46)
			{
				ReadXmlSecurityDeclaration(signature, declaration);
				return;
			}
			signatureReader.position++;
			uint num = signatureReader.ReadCompressedUInt32();
			Collection<SecurityAttribute> collection = new Collection<SecurityAttribute>((int)num);
			for (int i = 0; i < num; i++)
			{
				collection.Add(signatureReader.ReadSecurityAttribute());
			}
			declaration.security_attributes = collection;
		}

		private void ReadXmlSecurityDeclaration(uint signature, SecurityDeclaration declaration)
		{
			Collection<SecurityAttribute> collection = new Collection<SecurityAttribute>(1);
			SecurityAttribute securityAttribute = new SecurityAttribute(module.TypeSystem.LookupType("System.Security.Permissions", "PermissionSetAttribute"));
			securityAttribute.properties = new Collection<CustomAttributeNamedArgument>(1);
			securityAttribute.properties.Add(new CustomAttributeNamedArgument("XML", new CustomAttributeArgument(module.TypeSystem.String, ReadUnicodeStringBlob(signature))));
			collection.Add(securityAttribute);
			declaration.security_attributes = collection;
		}

		public Collection<ExportedType> ReadExportedTypes()
		{
			int num = MoveTo(Table.ExportedType);
			if (num == 0)
			{
				return new Collection<ExportedType>();
			}
			Collection<ExportedType> collection = new Collection<ExportedType>(num);
			for (int i = 1; i <= num; i++)
			{
				TypeAttributes attributes = (TypeAttributes)ReadUInt32();
				uint identifier = ReadUInt32();
				string name = ReadString();
				string text = ReadString();
				MetadataToken token = ReadMetadataToken(CodedIndex.Implementation);
				ExportedType declaringType = null;
				IMetadataScope scope = null;
				switch (token.TokenType)
				{
				case TokenType.AssemblyRef:
				case TokenType.File:
					scope = GetExportedTypeScope(token);
					break;
				case TokenType.ExportedType:
					declaringType = collection[(int)(token.RID - 1)];
					break;
				}
				ExportedType exportedType = new ExportedType(text, name, module, scope)
				{
					Attributes = attributes,
					Identifier = (int)identifier,
					DeclaringType = declaringType
				};
				exportedType.token = new MetadataToken(TokenType.ExportedType, i);
				collection.Add(exportedType);
			}
			return collection;
		}

		private IMetadataScope GetExportedTypeScope(MetadataToken token)
		{
			int num = position;
			IMetadataScope result;
			switch (token.TokenType)
			{
			case TokenType.AssemblyRef:
				InitializeAssemblyReferences();
				result = metadata.GetAssemblyNameReference(token.RID);
				break;
			case TokenType.File:
				InitializeModuleReferences();
				result = GetModuleReferenceFromFile(token);
				break;
			default:
				throw new NotSupportedException();
			}
			position = num;
			return result;
		}

		private ModuleReference GetModuleReferenceFromFile(MetadataToken token)
		{
			if (!MoveTo(Table.File, token.RID))
			{
				return null;
			}
			ReadUInt32();
			string text = ReadString();
			Collection<ModuleReference> moduleReferences = module.ModuleReferences;
			ModuleReference moduleReference;
			for (int i = 0; i < moduleReferences.Count; i++)
			{
				moduleReference = moduleReferences[i];
				if (moduleReference.Name == text)
				{
					return moduleReference;
				}
			}
			moduleReference = new ModuleReference(text);
			moduleReferences.Add(moduleReference);
			return moduleReference;
		}

		private void InitializeDocuments()
		{
			if (metadata.Documents == null)
			{
				int num = MoveTo(Table.Document);
				Document[] array = (metadata.Documents = new Document[num]);
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					uint signature = ReadBlobIndex();
					Guid hashAlgorithmGuid = ReadGuid();
					byte[] hash = ReadBlob();
					Guid languageGuid = ReadGuid();
					SignatureReader signatureReader = ReadSignature(signature);
					string url = signatureReader.ReadDocumentName();
					array[num2 - 1] = new Document(url)
					{
						HashAlgorithmGuid = hashAlgorithmGuid,
						Hash = hash,
						LanguageGuid = languageGuid,
						token = new MetadataToken(TokenType.Document, num2)
					};
				}
			}
		}

		public Collection<SequencePoint> ReadSequencePoints(MethodDefinition method)
		{
			InitializeDocuments();
			if (!MoveTo(Table.MethodDebugInformation, method.MetadataToken.RID))
			{
				return new Collection<SequencePoint>(0);
			}
			uint rid = ReadTableIndex(Table.Document);
			uint num = ReadBlobIndex();
			if (num == 0)
			{
				return new Collection<SequencePoint>(0);
			}
			Document document = GetDocument(rid);
			SignatureReader signatureReader = ReadSignature(num);
			return signatureReader.ReadSequencePoints(document);
		}

		public Document GetDocument(uint rid)
		{
			Document document = metadata.GetDocument(rid);
			if (document == null)
			{
				return null;
			}
			document.custom_infos = GetCustomDebugInformation(document);
			return document;
		}

		private void InitializeLocalScopes()
		{
			if (metadata.LocalScopes == null)
			{
				InitializeMethods();
				int num = MoveTo(Table.LocalScope);
				metadata.LocalScopes = new Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>>();
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					uint num3 = ReadTableIndex(Table.Method);
					uint col = ReadTableIndex(Table.ImportScope);
					Range col2 = ReadListRange(num2, Table.LocalScope, Table.LocalVariable);
					Range col3 = ReadListRange(num2, Table.LocalScope, Table.LocalConstant);
					uint col4 = ReadUInt32();
					uint col5 = ReadUInt32();
					metadata.SetLocalScopes(num3, AddMapping(metadata.LocalScopes, num3, new Row<uint, Range, Range, uint, uint, uint>(col, col2, col3, col4, col5, num2)));
				}
			}
		}

		public ScopeDebugInformation ReadScope(MethodDefinition method)
		{
			InitializeLocalScopes();
			InitializeImportScopes();
			if (!metadata.TryGetLocalScopes(method, out var scopes))
			{
				return null;
			}
			ScopeDebugInformation scopeDebugInformation = null;
			for (int i = 0; i < scopes.Count; i++)
			{
				ScopeDebugInformation scopeDebugInformation2 = ReadLocalScope(scopes[i]);
				if (i == 0)
				{
					scopeDebugInformation = scopeDebugInformation2;
				}
				else if (!AddScope(scopeDebugInformation.scopes, scopeDebugInformation2))
				{
					scopeDebugInformation.Scopes.Add(scopeDebugInformation2);
				}
			}
			return scopeDebugInformation;
		}

		private static bool AddScope(Collection<ScopeDebugInformation> scopes, ScopeDebugInformation scope)
		{
			if (scopes.IsNullOrEmpty())
			{
				return false;
			}
			foreach (ScopeDebugInformation scope2 in scopes)
			{
				if (scope2.HasScopes && AddScope(scope2.Scopes, scope))
				{
					return true;
				}
				if (scope.Start.Offset >= scope2.Start.Offset && scope.End.Offset <= scope2.End.Offset)
				{
					scope2.Scopes.Add(scope);
					return true;
				}
			}
			return false;
		}

		private ScopeDebugInformation ReadLocalScope(Row<uint, Range, Range, uint, uint, uint> record)
		{
			ScopeDebugInformation scopeDebugInformation = new ScopeDebugInformation
			{
				start = new InstructionOffset((int)record.Col4),
				end = new InstructionOffset((int)(record.Col4 + record.Col5)),
				token = new MetadataToken(TokenType.LocalScope, record.Col6)
			};
			if (record.Col1 != 0)
			{
				scopeDebugInformation.import = metadata.GetImportScope(record.Col1);
			}
			if (record.Col2.Length != 0)
			{
				scopeDebugInformation.variables = new Collection<VariableDebugInformation>((int)record.Col2.Length);
				for (uint num = 0u; num < record.Col2.Length; num++)
				{
					VariableDebugInformation variableDebugInformation = ReadLocalVariable(record.Col2.Start + num);
					if (variableDebugInformation != null)
					{
						scopeDebugInformation.variables.Add(variableDebugInformation);
					}
				}
			}
			if (record.Col3.Length != 0)
			{
				scopeDebugInformation.constants = new Collection<ConstantDebugInformation>((int)record.Col3.Length);
				for (uint num2 = 0u; num2 < record.Col3.Length; num2++)
				{
					ConstantDebugInformation constantDebugInformation = ReadLocalConstant(record.Col3.Start + num2);
					if (constantDebugInformation != null)
					{
						scopeDebugInformation.constants.Add(constantDebugInformation);
					}
				}
			}
			return scopeDebugInformation;
		}

		private VariableDebugInformation ReadLocalVariable(uint rid)
		{
			if (!MoveTo(Table.LocalVariable, rid))
			{
				return null;
			}
			VariableAttributes attributes = (VariableAttributes)ReadUInt16();
			ushort index = ReadUInt16();
			string name = ReadString();
			VariableDebugInformation variableDebugInformation = new VariableDebugInformation(index, name)
			{
				Attributes = attributes,
				token = new MetadataToken(TokenType.LocalVariable, rid)
			};
			variableDebugInformation.custom_infos = GetCustomDebugInformation(variableDebugInformation);
			return variableDebugInformation;
		}

		private ConstantDebugInformation ReadLocalConstant(uint rid)
		{
			if (!MoveTo(Table.LocalConstant, rid))
			{
				return null;
			}
			string name = ReadString();
			SignatureReader signatureReader = ReadSignature(ReadBlobIndex());
			TypeReference typeReference = signatureReader.ReadTypeSignature();
			object value;
			if (typeReference.etype == ElementType.String)
			{
				if (!signatureReader.CanReadMore())
				{
					value = "";
				}
				else if (signatureReader.buffer[signatureReader.position] != byte.MaxValue)
				{
					byte[] array = signatureReader.ReadBytes((int)(signatureReader.sig_length - (signatureReader.position - signatureReader.start)));
					value = Encoding.Unicode.GetString(array, 0, array.Length);
				}
				else
				{
					value = null;
				}
			}
			else if (!typeReference.IsTypeOf("System", "Decimal"))
			{
				value = (typeReference.IsTypeOf("System", "DateTime") ? ((object)new DateTime(signatureReader.ReadInt64())) : ((typeReference.etype != ElementType.Object && typeReference.etype != ElementType.None && typeReference.etype != ElementType.Class && typeReference.etype != ElementType.Array && typeReference.etype != ElementType.GenericInst) ? signatureReader.ReadConstantSignature(typeReference.etype) : null));
			}
			else
			{
				byte b = signatureReader.ReadByte();
				value = new decimal(signatureReader.ReadInt32(), signatureReader.ReadInt32(), signatureReader.ReadInt32(), (b & 0x80) != 0, (byte)(b & 0x7F));
			}
			ConstantDebugInformation constantDebugInformation = new ConstantDebugInformation(name, typeReference, value)
			{
				token = new MetadataToken(TokenType.LocalConstant, rid)
			};
			constantDebugInformation.custom_infos = GetCustomDebugInformation(constantDebugInformation);
			return constantDebugInformation;
		}

		private void InitializeImportScopes()
		{
			if (metadata.ImportScopes != null)
			{
				return;
			}
			int num = MoveTo(Table.ImportScope);
			metadata.ImportScopes = new ImportDebugInformation[num];
			for (int i = 1; i <= num; i++)
			{
				ReadTableIndex(Table.ImportScope);
				ImportDebugInformation importDebugInformation = new ImportDebugInformation();
				importDebugInformation.token = new MetadataToken(TokenType.ImportScope, i);
				SignatureReader signatureReader = ReadSignature(ReadBlobIndex());
				while (signatureReader.CanReadMore())
				{
					importDebugInformation.Targets.Add(ReadImportTarget(signatureReader));
				}
				metadata.ImportScopes[i - 1] = importDebugInformation;
			}
			MoveTo(Table.ImportScope);
			for (int j = 0; j < num; j++)
			{
				uint num2 = ReadTableIndex(Table.ImportScope);
				ReadBlobIndex();
				if (num2 != 0)
				{
					metadata.ImportScopes[j].Parent = metadata.GetImportScope(num2);
				}
			}
		}

		public string ReadUTF8StringBlob(uint signature)
		{
			return ReadStringBlob(signature, Encoding.UTF8);
		}

		private string ReadUnicodeStringBlob(uint signature)
		{
			return ReadStringBlob(signature, Encoding.Unicode);
		}

		private string ReadStringBlob(uint signature, Encoding encoding)
		{
			GetBlobView(signature, out var blob, out var index, out var count);
			if (count == 0)
			{
				return string.Empty;
			}
			return encoding.GetString(blob, index, count);
		}

		private ImportTarget ReadImportTarget(SignatureReader signature)
		{
			AssemblyNameReference reference = null;
			string text = null;
			string alias = null;
			TypeReference type = null;
			ImportTargetKind importTargetKind = (ImportTargetKind)signature.ReadCompressedUInt32();
			switch (importTargetKind)
			{
			case ImportTargetKind.ImportNamespace:
				text = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.ImportNamespaceInAssembly:
				reference = metadata.GetAssemblyNameReference(signature.ReadCompressedUInt32());
				text = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.ImportType:
				type = signature.ReadTypeToken();
				break;
			case ImportTargetKind.ImportXmlNamespaceWithAlias:
				alias = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				text = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.ImportAlias:
				alias = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineAssemblyAlias:
				alias = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				reference = metadata.GetAssemblyNameReference(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineNamespaceAlias:
				alias = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				text = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineNamespaceInAssemblyAlias:
				alias = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				reference = metadata.GetAssemblyNameReference(signature.ReadCompressedUInt32());
				text = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				break;
			case ImportTargetKind.DefineTypeAlias:
				alias = ReadUTF8StringBlob(signature.ReadCompressedUInt32());
				type = signature.ReadTypeToken();
				break;
			}
			return new ImportTarget(importTargetKind)
			{
				alias = alias,
				type = type,
				@namespace = text,
				reference = reference
			};
		}

		private void InitializeStateMachineMethods()
		{
			if (metadata.StateMachineMethods == null)
			{
				int num = MoveTo(Table.StateMachineMethod);
				metadata.StateMachineMethods = new Dictionary<uint, uint>(num);
				for (int i = 0; i < num; i++)
				{
					metadata.StateMachineMethods.Add(ReadTableIndex(Table.Method), ReadTableIndex(Table.Method));
				}
			}
		}

		public MethodDefinition ReadStateMachineKickoffMethod(MethodDefinition method)
		{
			InitializeStateMachineMethods();
			if (!metadata.TryGetStateMachineKickOffMethod(method, out var rid))
			{
				return null;
			}
			return GetMethodDefinition(rid);
		}

		private void InitializeCustomDebugInformations()
		{
			if (metadata.CustomDebugInformations == null)
			{
				int num = MoveTo(Table.CustomDebugInformation);
				metadata.CustomDebugInformations = new Dictionary<MetadataToken, Row<Guid, uint, uint>[]>();
				for (uint num2 = 1u; num2 <= num; num2++)
				{
					MetadataToken key = ReadMetadataToken(CodedIndex.HasCustomDebugInformation);
					Row<Guid, uint, uint> item = new Row<Guid, uint, uint>(ReadGuid(), ReadBlobIndex(), num2);
					metadata.CustomDebugInformations.TryGetValue(key, out var value);
					metadata.CustomDebugInformations[key] = value.Add(item);
				}
			}
		}

		public Collection<CustomDebugInformation> GetCustomDebugInformation(ICustomDebugInformationProvider provider)
		{
			InitializeCustomDebugInformations();
			if (!metadata.CustomDebugInformations.TryGetValue(provider.MetadataToken, out var value))
			{
				return null;
			}
			Collection<CustomDebugInformation> collection = new Collection<CustomDebugInformation>(value.Length);
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i].Col1 == StateMachineScopeDebugInformation.KindIdentifier)
				{
					SignatureReader signatureReader = ReadSignature(value[i].Col2);
					Collection<StateMachineScope> collection2 = new Collection<StateMachineScope>();
					while (signatureReader.CanReadMore())
					{
						int num = signatureReader.ReadInt32();
						int end = num + signatureReader.ReadInt32();
						collection2.Add(new StateMachineScope(num, end));
					}
					StateMachineScopeDebugInformation stateMachineScopeDebugInformation = new StateMachineScopeDebugInformation();
					stateMachineScopeDebugInformation.scopes = collection2;
					collection.Add(stateMachineScopeDebugInformation);
				}
				else if (value[i].Col1 == AsyncMethodBodyDebugInformation.KindIdentifier)
				{
					SignatureReader signatureReader2 = ReadSignature(value[i].Col2);
					int catchHandler = signatureReader2.ReadInt32() - 1;
					Collection<InstructionOffset> collection3 = new Collection<InstructionOffset>();
					Collection<InstructionOffset> collection4 = new Collection<InstructionOffset>();
					Collection<MethodDefinition> collection5 = new Collection<MethodDefinition>();
					while (signatureReader2.CanReadMore())
					{
						collection3.Add(new InstructionOffset(signatureReader2.ReadInt32()));
						collection4.Add(new InstructionOffset(signatureReader2.ReadInt32()));
						collection5.Add(GetMethodDefinition(signatureReader2.ReadCompressedUInt32()));
					}
					AsyncMethodBodyDebugInformation asyncMethodBodyDebugInformation = new AsyncMethodBodyDebugInformation(catchHandler);
					asyncMethodBodyDebugInformation.yields = collection3;
					asyncMethodBodyDebugInformation.resumes = collection4;
					asyncMethodBodyDebugInformation.resume_methods = collection5;
					collection.Add(asyncMethodBodyDebugInformation);
				}
				else if (value[i].Col1 == EmbeddedSourceDebugInformation.KindIdentifier)
				{
					collection.Add(new EmbeddedSourceDebugInformation(value[i].Col2, this));
				}
				else if (value[i].Col1 == SourceLinkDebugInformation.KindIdentifier)
				{
					collection.Add(new SourceLinkDebugInformation(Encoding.UTF8.GetString(ReadBlob(value[i].Col2))));
				}
				else
				{
					collection.Add(new BinaryCustomDebugInformation(value[i].Col1, ReadBlob(value[i].Col2)));
				}
				collection[i].token = new MetadataToken(TokenType.CustomDebugInformation, value[i].Col3);
			}
			return collection;
		}

		public byte[] ReadRawEmbeddedSourceDebugInformation(uint index)
		{
			SignatureReader signatureReader = ReadSignature(index);
			return signatureReader.ReadBytes((int)signatureReader.sig_length);
		}

		public Row<byte[], bool> ReadEmbeddedSourceDebugInformation(uint index)
		{
			SignatureReader signatureReader = ReadSignature(index);
			int num = signatureReader.ReadInt32();
			uint num2 = signatureReader.sig_length - 4;
			if (num == 0)
			{
				return new Row<byte[], bool>(signatureReader.ReadBytes((int)num2), col2: false);
			}
			if (num > 0)
			{
				MemoryStream stream = new MemoryStream(signatureReader.ReadBytes((int)num2));
				byte[] col = new byte[num];
				MemoryStream destination = new MemoryStream(col);
				using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen: true))
				{
					deflateStream.CopyTo(destination);
				}
				return new Row<byte[], bool>(col, col2: true);
			}
			throw new NotSupportedException();
		}
	}
	internal sealed class SignatureReader : ByteBuffer
	{
		private readonly MetadataReader reader;

		internal readonly uint start;

		internal readonly uint sig_length;

		private TypeSystem TypeSystem => reader.module.TypeSystem;

		public SignatureReader(uint blob, MetadataReader reader)
			: base(reader.image.BlobHeap.data)
		{
			this.reader = reader;
			position = (int)blob;
			sig_length = ReadCompressedUInt32();
			start = (uint)position;
		}

		private MetadataToken ReadTypeTokenSignature()
		{
			return CodedIndex.TypeDefOrRef.GetMetadataToken(ReadCompressedUInt32());
		}

		private GenericParameter GetGenericParameter(GenericParameterType type, uint var)
		{
			IGenericContext context = reader.context;
			if (context == null)
			{
				return GetUnboundGenericParameter(type, (int)var);
			}
			IGenericParameterProvider genericParameterProvider = type switch
			{
				GenericParameterType.Type => context.Type, 
				GenericParameterType.Method => context.Method, 
				_ => throw new NotSupportedException(), 
			};
			if (!context.IsDefinition)
			{
				CheckGenericContext(genericParameterProvider, (int)var);
			}
			if ((int)var >= genericParameterProvider.GenericParameters.Count)
			{
				return GetUnboundGenericParameter(type, (int)var);
			}
			return genericParameterProvider.GenericParameters[(int)var];
		}

		private GenericParameter GetUnboundGenericParameter(GenericParameterType type, int index)
		{
			return new GenericParameter(index, type, reader.module);
		}

		private static void CheckGenericContext(IGenericParameterProvider owner, int index)
		{
			Collection<GenericParameter> genericParameters = owner.GenericParameters;
			for (int i = genericParameters.Count; i <= index; i++)
			{
				genericParameters.Add(new GenericParameter(owner));
			}
		}

		public void ReadGenericInstanceSignature(IGenericParameterProvider provider, IGenericInstance instance, uint arity)
		{
			if (!provider.IsDefinition)
			{
				CheckGenericContext(provider, (int)(arity - 1));
			}
			Collection<TypeReference> genericArguments = instance.GenericArguments;
			for (int i = 0; i < arity; i++)
			{
				genericArguments.Add(ReadTypeSignature());
			}
		}

		private ArrayType ReadArrayTypeSignature()
		{
			ArrayType arrayType = new ArrayType(ReadTypeSignature());
			uint num = ReadCompressedUInt32();
			uint[] array = new uint[ReadCompressedUInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = ReadCompressedUInt32();
			}
			int[] array2 = new int[ReadCompressedUInt32()];
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = ReadCompressedInt32();
			}
			arrayType.Dimensions.Clear();
			for (int k = 0; k < num; k++)
			{
				int? num2 = null;
				int? upperBound = null;
				if (k < array2.Length)
				{
					num2 = array2[k];
				}
				if (k < array.Length)
				{
					upperBound = num2 + (int)array[k] - 1;
				}
				arrayType.Dimensions.Add(new ArrayDimension(num2, upperBound));
			}
			return arrayType;
		}

		private TypeReference GetTypeDefOrRef(MetadataToken token)
		{
			return reader.GetTypeDefOrRef(token);
		}

		public TypeReference ReadTypeSignature()
		{
			return ReadTypeSignature((ElementType)ReadByte());
		}

		public TypeReference ReadTypeToken()
		{
			return GetTypeDefOrRef(ReadTypeTokenSignature());
		}

		private TypeReference ReadTypeSignature(ElementType etype)
		{
			switch (etype)
			{
			case ElementType.ValueType:
			{
				TypeReference typeDefOrRef2 = GetTypeDefOrRef(ReadTypeTokenSignature());
				typeDefOrRef2.KnownValueType();
				return typeDefOrRef2;
			}
			case ElementType.Class:
				return GetTypeDefOrRef(ReadTypeTokenSignature());
			case ElementType.Ptr:
				return new PointerType(ReadTypeSignature());
			case ElementType.FnPtr:
			{
				FunctionPointerType functionPointerType = new FunctionPointerType();
				ReadMethodSignature(functionPointerType);
				return functionPointerType;
			}
			case ElementType.ByRef:
				return new ByReferenceType(ReadTypeSignature());
			case ElementType.Pinned:
				return new PinnedType(ReadTypeSignature());
			case ElementType.SzArray:
				return new ArrayType(ReadTypeSignature());
			case ElementType.Array:
				return ReadArrayTypeSignature();
			case ElementType.CModOpt:
				return new OptionalModifierType(GetTypeDefOrRef(ReadTypeTokenSignature()), ReadTypeSignature());
			case ElementType.CModReqD:
				return new RequiredModifierType(GetTypeDefOrRef(ReadTypeTokenSignature()), ReadTypeSignature());
			case ElementType.Sentinel:
				return new SentinelType(ReadTypeSignature());
			case ElementType.Var:
				return GetGenericParameter(GenericParameterType.Type, ReadCompressedUInt32());
			case ElementType.MVar:
				return GetGenericParameter(GenericParameterType.Method, ReadCompressedUInt32());
			case ElementType.GenericInst:
			{
				bool flag = ReadByte() == 17;
				TypeReference typeDefOrRef = GetTypeDefOrRef(ReadTypeTokenSignature());
				uint arity = ReadCompressedUInt32();
				GenericInstanceType genericInstanceType = new GenericInstanceType(typeDefOrRef, (int)arity);
				ReadGenericInstanceSignature(typeDefOrRef, genericInstanceType, arity);
				if (flag)
				{
					genericInstanceType.KnownValueType();
					typeDefOrRef.GetElementType().KnownValueType();
				}
				return genericInstanceType;
			}
			case ElementType.Object:
				return TypeSystem.Object;
			case ElementType.Void:
				return TypeSystem.Void;
			case ElementType.TypedByRef:
				return TypeSystem.TypedReference;
			case ElementType.I:
				return TypeSystem.IntPtr;
			case ElementType.U:
				return TypeSystem.UIntPtr;
			default:
				return GetPrimitiveType(etype);
			}
		}

		public void ReadMethodSignature(IMethodSignature method)
		{
			byte b = ReadByte();
			if ((b & 0x20) != 0)
			{
				method.HasThis = true;
				b = (byte)(b & -33);
			}
			if ((b & 0x40) != 0)
			{
				method.ExplicitThis = true;
				b = (byte)(b & -65);
			}
			method.CallingConvention = (MethodCallingConvention)b;
			MethodReference methodReference = method as MethodReference;
			if (methodReference != null && !methodReference.DeclaringType.IsArray)
			{
				reader.context = methodReference;
			}
			if ((b & 0x10) != 0)
			{
				uint num = ReadCompressedUInt32();
				if (methodReference != null && !methodReference.IsDefinition)
				{
					CheckGenericContext(methodReference, (int)(num - 1));
				}
			}
			uint num2 = ReadCompressedUInt32();
			method.MethodReturnType.ReturnType = ReadTypeSignature();
			if (num2 != 0)
			{
				Collection<ParameterDefinition> collection = ((!(method is MethodReference methodReference2)) ? method.Parameters : (methodReference2.parameters = new ParameterDefinitionCollection(method, (int)num2)));
				for (int i = 0; i < num2; i++)
				{
					collection.Add(new ParameterDefinition(ReadTypeSignature()));
				}
			}
		}

		public object ReadConstantSignature(ElementType type)
		{
			return ReadPrimitiveValue(type);
		}

		public void ReadCustomAttributeConstructorArguments(CustomAttribute attribute, Collection<ParameterDefinition> parameters)
		{
			int count = parameters.Count;
			if (count != 0)
			{
				attribute.arguments = new Collection<CustomAttributeArgument>(count);
				for (int i = 0; i < count; i++)
				{
					TypeReference type = GenericParameterResolver.ResolveParameterTypeIfNeeded(attribute.Constructor, parameters[i]);
					attribute.arguments.Add(ReadCustomAttributeFixedArgument(type));
				}
			}
		}

		private CustomAttributeArgument ReadCustomAttributeFixedArgument(TypeReference type)
		{
			if (type.IsArray)
			{
				return ReadCustomAttributeFixedArrayArgument((ArrayType)type);
			}
			return ReadCustomAttributeElement(type);
		}

		public void ReadCustomAttributeNamedArguments(ushort count, ref Collection<CustomAttributeNamedArgument> fields, ref Collection<CustomAttributeNamedArgument> properties)
		{
			for (int i = 0; i < count; i++)
			{
				if (!CanReadMore())
				{
					break;
				}
				ReadCustomAttributeNamedArgument(ref fields, ref properties);
			}
		}

		private void ReadCustomAttributeNamedArgument(ref Collection<CustomAttributeNamedArgument> fields, ref Collection<CustomAttributeNamedArgument> properties)
		{
			byte b = ReadByte();
			TypeReference type = ReadCustomAttributeFieldOrPropType();
			string name = ReadUTF8String();
			(b switch
			{
				83 => GetCustomAttributeNamedArgumentCollection(ref fields), 
				84 => GetCustomAttributeNamedArgumentCollection(ref properties), 
				_ => throw new NotSupportedException(), 
			}).Add(new CustomAttributeNamedArgument(name, ReadCustomAttributeFixedArgument(type)));
		}

		private static Collection<CustomAttributeNamedArgument> GetCustomAttributeNamedArgumentCollection(ref Collection<CustomAttributeNamedArgument> collection)
		{
			if (collection != null)
			{
				return collection;
			}
			return collection = new Collection<CustomAttributeNamedArgument>();
		}

		private CustomAttributeArgument ReadCustomAttributeFixedArrayArgument(ArrayType type)
		{
			uint num = ReadUInt32();
			switch (num)
			{
			case uint.MaxValue:
				return new CustomAttributeArgument(type, null);
			case 0u:
				return new CustomAttributeArgument(type, Empty<CustomAttributeArgument>.Array);
			default:
			{
				CustomAttributeArgument[] array = new CustomAttributeArgument[num];
				TypeReference elementType = type.ElementType;
				for (int i = 0; i < num; i++)
				{
					array[i] = ReadCustomAttributeElement(elementType);
				}
				return new CustomAttributeArgument(type, array);
			}
			}
		}

		private CustomAttributeArgument ReadCustomAttributeElement(TypeReference type)
		{
			if (type.IsArray)
			{
				return ReadCustomAttributeFixedArrayArgument((ArrayType)type);
			}
			return new CustomAttributeArgument(type, (type.etype == ElementType.Object) ? ((object)ReadCustomAttributeElement(ReadCustomAttributeFieldOrPropType())) : ReadCustomAttributeElementValue(type));
		}

		private object ReadCustomAttributeElementValue(TypeReference type)
		{
			ElementType etype = type.etype;
			if (etype == ElementType.GenericInst)
			{
				type = type.GetElementType();
				etype = type.etype;
			}
			switch (etype)
			{
			case ElementType.String:
				return ReadUTF8String();
			case ElementType.None:
				if (type.IsTypeOf("System", "Type"))
				{
					return ReadTypeReference();
				}
				return ReadCustomAttributeEnum(type);
			default:
				return ReadPrimitiveValue(etype);
			}
		}

		private object ReadPrimitiveValue(ElementType type)
		{
			return type switch
			{
				ElementType.Boolean => ReadByte() == 1, 
				ElementType.I1 => (sbyte)ReadByte(), 
				ElementType.U1 => ReadByte(), 
				ElementType.Char => (char)ReadUInt16(), 
				ElementType.I2 => ReadInt16(), 
				ElementType.U2 => ReadUInt16(), 
				ElementType.I4 => ReadInt32(), 
				ElementType.U4 => ReadUInt32(), 
				ElementType.I8 => ReadInt64(), 
				ElementType.U8 => ReadUInt64(), 
				ElementType.R4 => ReadSingle(), 
				ElementType.R8 => ReadDouble(), 
				_ => throw new NotImplementedException(type.ToString()), 
			};
		}

		private TypeReference GetPrimitiveType(ElementType etype)
		{
			return etype switch
			{
				ElementType.Boolean => TypeSystem.Boolean, 
				ElementType.Char => TypeSystem.Char, 
				ElementType.I1 => TypeSystem.SByte, 
				ElementType.U1 => TypeSystem.Byte, 
				ElementType.I2 => TypeSystem.Int16, 
				ElementType.U2 => TypeSystem.UInt16, 
				ElementType.I4 => TypeSystem.Int32, 
				ElementType.U4 => TypeSystem.UInt32, 
				ElementType.I8 => TypeSystem.Int64, 
				ElementType.U8 => TypeSystem.UInt64, 
				ElementType.R4 => TypeSystem.Single, 
				ElementType.R8 => TypeSystem.Double, 
				ElementType.String => TypeSystem.String, 
				_ => throw new NotImplementedException(etype.ToString()), 
			};
		}

		private TypeReference ReadCustomAttributeFieldOrPropType()
		{
			ElementType elementType = (ElementType)ReadByte();
			return elementType switch
			{
				ElementType.Boxed => TypeSystem.Object, 
				ElementType.SzArray => new ArrayType(ReadCustomAttributeFieldOrPropType()), 
				ElementType.Enum => ReadTypeReference(), 
				ElementType.Type => TypeSystem.LookupType("System", "Type"), 
				_ => GetPrimitiveType(elementType), 
			};
		}

		public TypeReference ReadTypeReference()
		{
			return TypeParser.ParseType(reader.module, ReadUTF8String());
		}

		private object ReadCustomAttributeEnum(TypeReference enum_type)
		{
			TypeDefinition typeDefinition = enum_type.CheckedResolve();
			if (!typeDefinition.IsEnum)
			{
				throw new ArgumentException();
			}
			return ReadCustomAttributeElementValue(typeDefinition.GetEnumUnderlyingType());
		}

		public SecurityAttribute ReadSecurityAttribute()
		{
			SecurityAttribute securityAttribute = new SecurityAttribute(ReadTypeReference());
			ReadCompressedUInt32();
			ReadCustomAttributeNamedArguments((ushort)ReadCompressedUInt32(), ref securityAttribute.fields, ref securityAttribute.properties);
			return securityAttribute;
		}

		public MarshalInfo ReadMarshalInfo()
		{
			NativeType nativeType = ReadNativeType();
			switch (nativeType)
			{
			case NativeType.Array:
			{
				ArrayMarshalInfo arrayMarshalInfo = new ArrayMarshalInfo();
				if (CanReadMore())
				{
					arrayMarshalInfo.element_type = ReadNativeType();
				}
				if (CanReadMore())
				{
					arrayMarshalInfo.size_parameter_index = (int)ReadCompressedUInt32();
				}
				if (CanReadMore())
				{
					arrayMarshalInfo.size = (int)ReadCompressedUInt32();
				}
				if (CanReadMore())
				{
					arrayMarshalInfo.size_parameter_multiplier = (int)ReadCompressedUInt32();
				}
				return arrayMarshalInfo;
			}
			case NativeType.SafeArray:
			{
				SafeArrayMarshalInfo safeArrayMarshalInfo = new SafeArrayMarshalInfo();
				if (CanReadMore())
				{
					safeArrayMarshalInfo.element_type = ReadVariantType();
				}
				return safeArrayMarshalInfo;
			}
			case NativeType.FixedArray:
			{
				FixedArrayMarshalInfo fixedArrayMarshalInfo = new FixedArrayMarshalInfo();
				if (CanReadMore())
				{
					fixedArrayMarshalInfo.size = (int)ReadCompressedUInt32();
				}
				if (CanReadMore())
				{
					fixedArrayMarshalInfo.element_type = ReadNativeType();
				}
				return fixedArrayMarshalInfo;
			}
			case NativeType.FixedSysString:
			{
				FixedSysStringMarshalInfo fixedSysStringMarshalInfo = new FixedSysStringMarshalInfo();
				if (CanReadMore())
				{
					fixedSysStringMarshalInfo.size = (int)ReadCompressedUInt32();
				}
				return fixedSysStringMarshalInfo;
			}
			case NativeType.CustomMarshaler:
			{
				CustomMarshalInfo customMarshalInfo = new CustomMarshalInfo();
				string text = ReadUTF8String();
				customMarshalInfo.guid = ((!string.IsNullOrEmpty(text)) ? new Guid(text) : Guid.Empty);
				customMarshalInfo.unmanaged_type = ReadUTF8String();
				customMarshalInfo.managed_type = ReadTypeReference();
				customMarshalInfo.cookie = ReadUTF8String();
				return customMarshalInfo;
			}
			default:
				return new MarshalInfo(nativeType);
			}
		}

		private NativeType ReadNativeType()
		{
			return (NativeType)ReadByte();
		}

		private VariantType ReadVariantType()
		{
			return (VariantType)ReadByte();
		}

		private string ReadUTF8String()
		{
			if (buffer[position] == byte.MaxValue)
			{
				position++;
				return null;
			}
			int num = (int)ReadCompressedUInt32();
			if (num == 0)
			{
				return string.Empty;
			}
			if (position + num > buffer.Length)
			{
				return string.Empty;
			}
			string result = Encoding.UTF8.GetString(buffer, position, num);
			position += num;
			return result;
		}

		public string ReadDocumentName()
		{
			char c = (char)buffer[position];
			position++;
			StringBuilder stringBuilder = new StringBuilder();
			int num = 0;
			while (CanReadMore())
			{
				if (num > 0 && c != 0)
				{
					stringBuilder.Append(c);
				}
				uint num2 = ReadCompressedUInt32();
				if (num2 != 0)
				{
					stringBuilder.Append(reader.ReadUTF8StringBlob(num2));
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		public Collection<SequencePoint> ReadSequencePoints(Document document)
		{
			ReadCompressedUInt32();
			if (document == null)
			{
				document = reader.GetDocument(ReadCompressedUInt32());
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag = true;
			long num4 = sig_length - (position - start);
			int capacity = (int)num4 / 5;
			Collection<SequencePoint> collection = new Collection<SequencePoint>(capacity);
			int num5 = 0;
			while (CanReadMore())
			{
				int num6 = (int)ReadCompressedUInt32();
				if (num5 > 0 && num6 == 0)
				{
					document = reader.GetDocument(ReadCompressedUInt32());
				}
				else
				{
					num += num6;
					int num7 = (int)ReadCompressedUInt32();
					int num8 = ((num7 == 0) ? ((int)ReadCompressedUInt32()) : ReadCompressedInt32());
					if (num7 == 0 && num8 == 0)
					{
						collection.Add(new SequencePoint(num, document)
						{
							StartLine = 16707566,
							EndLine = 16707566,
							StartColumn = 0,
							EndColumn = 0
						});
					}
					else
					{
						if (flag)
						{
							num2 = (int)ReadCompressedUInt32();
							num3 = (int)ReadCompressedUInt32();
						}
						else
						{
							num2 += ReadCompressedInt32();
							num3 += ReadCompressedInt32();
						}
						collection.Add(new SequencePoint(num, document)
						{
							StartLine = num2,
							StartColumn = num3,
							EndLine = num2 + num7,
							EndColumn = num3 + num8
						});
						flag = false;
					}
				}
				num5++;
			}
			return collection;
		}

		public bool CanReadMore()
		{
			return position - start < sig_length;
		}
	}
	internal static class ModuleWriter
	{
		public static void WriteModule(ModuleDefinition module, Disposable<Stream> stream, WriterParameters parameters)
		{
			using (stream)
			{
				Write(module, stream, parameters);
			}
		}

		private static void Write(ModuleDefinition module, Disposable<Stream> stream, WriterParameters parameters)
		{
			if ((module.Attributes & ModuleAttributes.ILOnly) == 0)
			{
				throw new NotSupportedException("Writing mixed-mode assemblies is not supported");
			}
			if (module.HasImage && module.ReadingMode == ReadingMode.Deferred)
			{
				ImmediateModuleReader immediateModuleReader = new ImmediateModuleReader(module.Image);
				immediateModuleReader.ReadModule(module, resolve_attributes: false);
				immediateModuleReader.ReadSymbols(module);
			}
			module.MetadataSystem.Clear();
			if (module.symbol_reader != null)
			{
				module.symbol_reader.Dispose();
			}
			AssemblyNameDefinition assemblyNameDefinition = ((module.assembly != null && module.kind != ModuleKind.NetModule) ? module.assembly.Name : null);
			string fileName = stream.value.GetFileName();
			uint timestamp = parameters.Timestamp ?? module.timestamp;
			ISymbolWriterProvider symbolWriterProvider = parameters.SymbolWriterProvider;
			if (symbolWriterProvider == null && parameters.WriteSymbols)
			{
				symbolWriterProvider = new DefaultSymbolWriterProvider();
			}
			if (parameters.HasStrongNameKey && assemblyNameDefinition != null)
			{
				assemblyNameDefinition.PublicKey = CryptoService.GetPublicKey(parameters);
				module.Attributes |= ModuleAttributes.StrongNameSigned;
			}
			if (parameters.DeterministicMvid)
			{
				module.Mvid = Guid.Empty;
			}
			MetadataBuilder metadataBuilder = new MetadataBuilder(module, fileName, timestamp, symbolWriterProvider);
			try
			{
				module.metadata_builder = metadataBuilder;
				using ISymbolWriter symbolWriter = GetSymbolWriter(module, fileName, symbolWriterProvider, parameters);
				metadataBuilder.SetSymbolWriter(symbolWriter);
				BuildMetadata(module, metadataBuilder);
				symbolWriter?.Write();
				ImageWriter imageWriter = ImageWriter.CreateWriter(module, metadataBuilder, stream);
				stream.value.SetLength(0L);
				imageWriter.WriteImage();
				if (parameters.DeterministicMvid)
				{
					ComputeDeterministicMvid(imageWriter, module);
				}
				if (parameters.HasStrongNameKey)
				{
					CryptoService.StrongName(stream.value, imageWriter, parameters);
				}
			}
			finally
			{
				module.metadata_builder = null;
			}
		}

		private static void BuildMetadata(ModuleDefinition module, MetadataBuilder metadata)
		{
			if (!module.HasImage)
			{
				metadata.BuildMetadata();
				return;
			}
			module.Read(metadata, delegate(MetadataBuilder builder, MetadataReader _)
			{
				builder.BuildMetadata();
				return builder;
			});
		}

		private static ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fq_name, ISymbolWriterProvider symbol_writer_provider, WriterParameters parameters)
		{
			if (symbol_writer_provider == null)
			{
				return null;
			}
			if (parameters.SymbolStream != null)
			{
				return symbol_writer_provider.GetSymbolWriter(module, parameters.SymbolStream);
			}
			return symbol_writer_provider.GetSymbolWriter(module, fq_name);
		}

		private static void ComputeDeterministicMvid(ImageWriter writer, ModuleDefinition module)
		{
			long position = writer.BaseStream.Position;
			writer.BaseStream.Seek(0L, SeekOrigin.Begin);
			Guid mvid = CryptoService.ComputeGuid(CryptoService.ComputeHash(writer.BaseStream));
			writer.MoveToRVA(TextSegment.GuidHeap);
			writer.WriteBytes(mvid.ToByteArray());
			writer.Flush();
			module.Mvid = mvid;
			writer.BaseStream.Seek(position, SeekOrigin.Begin);
		}
	}
	internal abstract class MetadataTable
	{
		public abstract int Length { get; }

		public bool IsLarge => Length > 65535;

		public abstract void Write(TableHeapBuffer buffer);

		public abstract void Sort();
	}
	internal abstract class OneRowTable<TRow> : MetadataTable where TRow : struct
	{
		internal TRow row;

		public sealed override int Length => 1;

		public sealed override void Sort()
		{
		}
	}
	internal abstract class MetadataTable<TRow> : MetadataTable where TRow : struct
	{
		internal TRow[] rows = new TRow[2];

		internal int length;

		public sealed override int Length => length;

		public int AddRow(TRow row)
		{
			if (rows.Length == length)
			{
				Grow();
			}
			rows[length++] = row;
			return length;
		}

		private void Grow()
		{
			TRow[] destinationArray = new TRow[rows.Length * 2];
			Array.Copy(rows, destinationArray, rows.Length);
			rows = destinationArray;
		}

		public override void Sort()
		{
		}
	}
	internal abstract class SortedTable<TRow> : MetadataTable<TRow>, IComparer<TRow> where TRow : struct
	{
		public sealed override void Sort()
		{
			MergeSort<TRow>.Sort(rows, 0, length, this);
		}

		protected static int Compare(uint x, uint y)
		{
			return (x != y) ? ((x > y) ? 1 : (-1)) : 0;
		}

		public abstract int Compare(TRow x, TRow y);
	}
	internal sealed class ModuleTable : OneRowTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			buffer.WriteUInt16(0);
			buffer.WriteString(row.Col1);
			buffer.WriteGuid(row.Col2);
			buffer.WriteUInt16(0);
			buffer.WriteUInt16(0);
		}
	}
	internal sealed class TypeRefTable : MetadataTable<Row<uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteCodedRID(rows[i].Col1, CodedIndex.ResolutionScope);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteString(rows[i].Col3);
			}
		}
	}
	internal sealed class TypeDefTable : MetadataTable<Row<TypeAttributes, uint, uint, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32((uint)rows[i].Col1);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteString(rows[i].Col3);
				buffer.WriteCodedRID(rows[i].Col4, CodedIndex.TypeDefOrRef);
				buffer.WriteRID(rows[i].Col5, Table.Field);
				buffer.WriteRID(rows[i].Col6, Table.Method);
			}
		}
	}
	internal sealed class FieldTable : MetadataTable<Row<FieldAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteBlob(rows[i].Col3);
			}
		}
	}
	internal sealed class MethodTable : MetadataTable<Row<uint, MethodImplAttributes, MethodAttributes, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32(rows[i].Col1);
				buffer.WriteUInt16((ushort)rows[i].Col2);
				buffer.WriteUInt16((ushort)rows[i].Col3);
				buffer.WriteString(rows[i].Col4);
				buffer.WriteBlob(rows[i].Col5);
				buffer.WriteRID(rows[i].Col6, Table.Param);
			}
		}
	}
	internal sealed class ParamTable : MetadataTable<Row<ParameterAttributes, ushort, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteUInt16(rows[i].Col2);
				buffer.WriteString(rows[i].Col3);
			}
		}
	}
	internal sealed class InterfaceImplTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.TypeDef);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.TypeDefOrRef);
			}
		}
	}
	internal sealed class MemberRefTable : MetadataTable<Row<uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteCodedRID(rows[i].Col1, CodedIndex.MemberRefParent);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteBlob(rows[i].Col3);
			}
		}
	}
	internal sealed class ConstantTable : SortedTable<Row<ElementType, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.HasConstant);
				buffer.WriteBlob(rows[i].Col3);
			}
		}

		public override int Compare(Row<ElementType, uint, uint> x, Row<ElementType, uint, uint> y)
		{
			return SortedTable<Row<ElementType, uint, uint>>.Compare(x.Col2, y.Col2);
		}
	}
	internal sealed class CustomAttributeTable : SortedTable<Row<uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteCodedRID(rows[i].Col1, CodedIndex.HasCustomAttribute);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.CustomAttributeType);
				buffer.WriteBlob(rows[i].Col3);
			}
		}

		public override int Compare(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return SortedTable<Row<uint, uint, uint>>.Compare(x.Col1, y.Col1);
		}
	}
	internal sealed class FieldMarshalTable : SortedTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteCodedRID(rows[i].Col1, CodedIndex.HasFieldMarshal);
				buffer.WriteBlob(rows[i].Col2);
			}
		}

		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return SortedTable<Row<uint, uint>>.Compare(x.Col1, y.Col1);
		}
	}
	internal sealed class DeclSecurityTable : SortedTable<Row<SecurityAction, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.HasDeclSecurity);
				buffer.WriteBlob(rows[i].Col3);
			}
		}

		public override int Compare(Row<SecurityAction, uint, uint> x, Row<SecurityAction, uint, uint> y)
		{
			return SortedTable<Row<SecurityAction, uint, uint>>.Compare(x.Col2, y.Col2);
		}
	}
	internal sealed class ClassLayoutTable : SortedTable<Row<ushort, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16(rows[i].Col1);
				buffer.WriteUInt32(rows[i].Col2);
				buffer.WriteRID(rows[i].Col3, Table.TypeDef);
			}
		}

		public override int Compare(Row<ushort, uint, uint> x, Row<ushort, uint, uint> y)
		{
			return SortedTable<Row<ushort, uint, uint>>.Compare(x.Col3, y.Col3);
		}
	}
	internal sealed class FieldLayoutTable : SortedTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32(rows[i].Col1);
				buffer.WriteRID(rows[i].Col2, Table.Field);
			}
		}

		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return SortedTable<Row<uint, uint>>.Compare(x.Col2, y.Col2);
		}
	}
	internal sealed class StandAloneSigTable : MetadataTable<uint>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteBlob(rows[i]);
			}
		}
	}
	internal sealed class EventMapTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(rows[i].Col2, Table.Event);
			}
		}
	}
	internal sealed class EventTable : MetadataTable<Row<EventAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteCodedRID(rows[i].Col3, CodedIndex.TypeDefOrRef);
			}
		}
	}
	internal sealed class PropertyMapTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(rows[i].Col2, Table.Property);
			}
		}
	}
	internal sealed class PropertyTable : MetadataTable<Row<PropertyAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteBlob(rows[i].Col3);
			}
		}
	}
	internal sealed class MethodSemanticsTable : SortedTable<Row<MethodSemanticsAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteRID(rows[i].Col2, Table.Method);
				buffer.WriteCodedRID(rows[i].Col3, CodedIndex.HasSemantics);
			}
		}

		public override int Compare(Row<MethodSemanticsAttributes, uint, uint> x, Row<MethodSemanticsAttributes, uint, uint> y)
		{
			return SortedTable<Row<MethodSemanticsAttributes, uint, uint>>.Compare(x.Col3, y.Col3);
		}
	}
	internal sealed class MethodImplTable : MetadataTable<Row<uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.TypeDef);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.MethodDefOrRef);
				buffer.WriteCodedRID(rows[i].Col3, CodedIndex.MethodDefOrRef);
			}
		}
	}
	internal sealed class ModuleRefTable : MetadataTable<uint>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteString(rows[i]);
			}
		}
	}
	internal sealed class TypeSpecTable : MetadataTable<uint>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteBlob(rows[i]);
			}
		}
	}
	internal sealed class ImplMapTable : SortedTable<Row<PInvokeAttributes, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.MemberForwarded);
				buffer.WriteString(rows[i].Col3);
				buffer.WriteRID(rows[i].Col4, Table.ModuleRef);
			}
		}

		public override int Compare(Row<PInvokeAttributes, uint, uint, uint> x, Row<PInvokeAttributes, uint, uint, uint> y)
		{
			return SortedTable<Row<PInvokeAttributes, uint, uint, uint>>.Compare(x.Col2, y.Col2);
		}
	}
	internal sealed class FieldRVATable : SortedTable<Row<uint, uint>>
	{
		internal int position;

		public override void Write(TableHeapBuffer buffer)
		{
			position = buffer.position;
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32(rows[i].Col1);
				buffer.WriteRID(rows[i].Col2, Table.Field);
			}
		}

		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return SortedTable<Row<uint, uint>>.Compare(x.Col2, y.Col2);
		}
	}
	internal sealed class AssemblyTable : OneRowTable<Row<AssemblyHashAlgorithm, ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			buffer.WriteUInt32((uint)row.Col1);
			buffer.WriteUInt16(row.Col2);
			buffer.WriteUInt16(row.Col3);
			buffer.WriteUInt16(row.Col4);
			buffer.WriteUInt16(row.Col5);
			buffer.WriteUInt32((uint)row.Col6);
			buffer.WriteBlob(row.Col7);
			buffer.WriteString(row.Col8);
			buffer.WriteString(row.Col9);
		}
	}
	internal sealed class AssemblyRefTable : MetadataTable<Row<ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16(rows[i].Col1);
				buffer.WriteUInt16(rows[i].Col2);
				buffer.WriteUInt16(rows[i].Col3);
				buffer.WriteUInt16(rows[i].Col4);
				buffer.WriteUInt32((uint)rows[i].Col5);
				buffer.WriteBlob(rows[i].Col6);
				buffer.WriteString(rows[i].Col7);
				buffer.WriteString(rows[i].Col8);
				buffer.WriteBlob(rows[i].Col9);
			}
		}
	}
	internal sealed class FileTable : MetadataTable<Row<FileAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32((uint)rows[i].Col1);
				buffer.WriteString(rows[i].Col2);
				buffer.WriteBlob(rows[i].Col3);
			}
		}
	}
	internal sealed class ExportedTypeTable : MetadataTable<Row<TypeAttributes, uint, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32((uint)rows[i].Col1);
				buffer.WriteUInt32(rows[i].Col2);
				buffer.WriteString(rows[i].Col3);
				buffer.WriteString(rows[i].Col4);
				buffer.WriteCodedRID(rows[i].Col5, CodedIndex.Implementation);
			}
		}
	}
	internal sealed class ManifestResourceTable : MetadataTable<Row<uint, ManifestResourceAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt32(rows[i].Col1);
				buffer.WriteUInt32((uint)rows[i].Col2);
				buffer.WriteString(rows[i].Col3);
				buffer.WriteCodedRID(rows[i].Col4, CodedIndex.Implementation);
			}
		}
	}
	internal sealed class NestedClassTable : SortedTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.TypeDef);
				buffer.WriteRID(rows[i].Col2, Table.TypeDef);
			}
		}

		public override int Compare(Row<uint, uint> x, Row<uint, uint> y)
		{
			return SortedTable<Row<uint, uint>>.Compare(x.Col1, y.Col1);
		}
	}
	internal sealed class GenericParamTable : MetadataTable<Row<ushort, GenericParameterAttributes, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16(rows[i].Col1);
				buffer.WriteUInt16((ushort)rows[i].Col2);
				buffer.WriteCodedRID(rows[i].Col3, CodedIndex.TypeOrMethodDef);
				buffer.WriteString(rows[i].Col4);
			}
		}
	}
	internal sealed class MethodSpecTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteCodedRID(rows[i].Col1, CodedIndex.MethodDefOrRef);
				buffer.WriteBlob(rows[i].Col2);
			}
		}
	}
	internal sealed class GenericParamConstraintTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.GenericParam);
				buffer.WriteCodedRID(rows[i].Col2, CodedIndex.TypeDefOrRef);
			}
		}
	}
	internal sealed class DocumentTable : MetadataTable<Row<uint, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteBlob(rows[i].Col1);
				buffer.WriteGuid(rows[i].Col2);
				buffer.WriteBlob(rows[i].Col3);
				buffer.WriteGuid(rows[i].Col4);
			}
		}
	}
	internal sealed class MethodDebugInformationTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.Document);
				buffer.WriteBlob(rows[i].Col2);
			}
		}
	}
	internal sealed class LocalScopeTable : MetadataTable<Row<uint, uint, uint, uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.Method);
				buffer.WriteRID(rows[i].Col2, Table.ImportScope);
				buffer.WriteRID(rows[i].Col3, Table.LocalVariable);
				buffer.WriteRID(rows[i].Col4, Table.LocalConstant);
				buffer.WriteUInt32(rows[i].Col5);
				buffer.WriteUInt32(rows[i].Col6);
			}
		}
	}
	internal sealed class LocalVariableTable : MetadataTable<Row<VariableAttributes, ushort, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteUInt16((ushort)rows[i].Col1);
				buffer.WriteUInt16(rows[i].Col2);
				buffer.WriteString(rows[i].Col3);
			}
		}
	}
	internal sealed class LocalConstantTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteString(rows[i].Col1);
				buffer.WriteBlob(rows[i].Col2);
			}
		}
	}
	internal sealed class ImportScopeTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.ImportScope);
				buffer.WriteBlob(rows[i].Col2);
			}
		}
	}
	internal sealed class StateMachineMethodTable : MetadataTable<Row<uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteRID(rows[i].Col1, Table.Method);
				buffer.WriteRID(rows[i].Col2, Table.Method);
			}
		}
	}
	internal sealed class CustomDebugInformationTable : SortedTable<Row<uint, uint, uint>>
	{
		public override void Write(TableHeapBuffer buffer)
		{
			for (int i = 0; i < length; i++)
			{
				buffer.WriteCodedRID(rows[i].Col1, CodedIndex.HasCustomDebugInformation);
				buffer.WriteGuid(rows[i].Col2);
				buffer.WriteBlob(rows[i].Col3);
			}
		}

		public override int Compare(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return SortedTable<Row<uint, uint, uint>>.Compare(x.Col1, y.Col1);
		}
	}
	internal sealed class MetadataBuilder
	{
		private sealed class GenericParameterComparer : IComparer<GenericParameter>
		{
			public int Compare(GenericParameter a, GenericParameter b)
			{
				uint num = MakeCodedRID(a.Owner, CodedIndex.TypeOrMethodDef);
				uint num2 = MakeCodedRID(b.Owner, CodedIndex.TypeOrMethodDef);
				if (num == num2)
				{
					int position = a.Position;
					int position2 = b.Position;
					return (position != position2) ? ((position > position2) ? 1 : (-1)) : 0;
				}
				return (num > num2) ? 1 : (-1);
			}
		}

		internal readonly ModuleDefinition module;

		internal readonly ISymbolWriterProvider symbol_writer_provider;

		internal ISymbolWriter symbol_writer;

		internal readonly TextMap text_map;

		internal readonly string fq_name;

		internal readonly uint timestamp;

		private readonly Dictionary<Row<uint, uint, uint>, MetadataToken> type_ref_map;

		private readonly Dictionary<uint, MetadataToken> type_spec_map;

		private readonly Dictionary<Row<uint, uint, uint>, MetadataToken> member_ref_map;

		private readonly Dictionary<Row<uint, uint>, MetadataToken> method_spec_map;

		private readonly Collection<GenericParameter> generic_parameters;

		internal readonly CodeWriter code;

		internal readonly DataBuffer data;

		internal readonly ResourceBuffer resources;

		internal readonly StringHeapBuffer string_heap;

		internal readonly GuidHeapBuffer guid_heap;

		internal readonly UserStringHeapBuffer user_string_heap;

		internal readonly BlobHeapBuffer blob_heap;

		internal readonly TableHeapBuffer table_heap;

		internal readonly PdbHeapBuffer pdb_heap;

		internal MetadataToken entry_point;

		internal uint type_rid = 1u;

		internal uint field_rid = 1u;

		internal uint method_rid = 1u;

		internal uint param_rid = 1u;

		internal uint property_rid = 1u;

		internal uint event_rid = 1u;

		internal uint local_variable_rid = 1u;

		internal uint local_constant_rid = 1u;

		private readonly TypeRefTable type_ref_table;

		private readonly TypeDefTable type_def_table;

		private readonly FieldTable field_table;

		private readonly MethodTable method_table;

		private readonly ParamTable param_table;

		private readonly InterfaceImplTable iface_impl_table;

		private readonly MemberRefTable member_ref_table;

		private readonly ConstantTable constant_table;

		private readonly CustomAttributeTable custom_attribute_table;

		private readonly DeclSecurityTable declsec_table;

		private readonly StandAloneSigTable standalone_sig_table;

		private readonly EventMapTable event_map_table;

		private readonly EventTable event_table;

		private readonly PropertyMapTable property_map_table;

		private readonly PropertyTable property_table;

		private readonly TypeSpecTable typespec_table;

		private readonly MethodSpecTable method_spec_table;

		internal MetadataBuilder metadata_builder;

		private readonly DocumentTable document_table;

		private readonly MethodDebugInformationTable method_debug_information_table;

		private readonly LocalScopeTable local_scope_table;

		private readonly LocalVariableTable local_variable_table;

		private readonly LocalConstantTable local_constant_table;

		private readonly ImportScopeTable import_scope_table;

		private readonly StateMachineMethodTable state_machine_method_table;

		private readonly CustomDebugInformationTable custom_debug_information_table;

		private readonly Dictionary<Row<uint, uint>, MetadataToken> import_scope_map;

		private readonly Dictionary<string, MetadataToken> document_map;

		public MetadataBuilder(ModuleDefinition module, string fq_name, uint timestamp, ISymbolWriterProvider symbol_writer_provider)
		{
			this.module = module;
			text_map = CreateTextMap();
			this.fq_name = fq_name;
			this.timestamp = timestamp;
			this.symbol_writer_provider = symbol_writer_provider;
			code = new CodeWriter(this);
			data = new DataBuffer();
			resources = new ResourceBuffer();
			string_heap = new StringHeapBuffer();
			guid_heap = new GuidHeapBuffer();
			user_string_heap = new UserStringHeapBuffer();
			blob_heap = new BlobHeapBuffer();
			table_heap = new TableHeapBuffer(module, this);
			type_ref_table = GetTable<TypeRefTable>(Table.TypeRef);
			type_def_table = GetTable<TypeDefTable>(Table.TypeDef);
			field_table = GetTable<FieldTable>(Table.Field);
			method_table = GetTable<MethodTable>(Table.Method);
			param_table = GetTable<ParamTable>(Table.Param);
			iface_impl_table = GetTable<InterfaceImplTable>(Table.InterfaceImpl);
			member_ref_table = GetTable<MemberRefTable>(Table.MemberRef);
			constant_table = GetTable<ConstantTable>(Table.Constant);
			custom_attribute_table = GetTable<CustomAttributeTable>(Table.CustomAttribute);
			declsec_table = GetTable<DeclSecurityTable>(Table.DeclSecurity);
			standalone_sig_table = GetTable<StandAloneSigTable>(Table.StandAloneSig);
			event_map_table = GetTable<EventMapTable>(Table.EventMap);
			event_table = GetTable<EventTable>(Table.Event);
			property_map_table = GetTable<PropertyMapTable>(Table.PropertyMap);
			property_table = GetTable<PropertyTable>(Table.Property);
			typespec_table = GetTable<TypeSpecTable>(Table.TypeSpec);
			method_spec_table = GetTable<MethodSpecTable>(Table.MethodSpec);
			RowEqualityComparer comparer = new RowEqualityComparer();
			type_ref_map = new Dictionary<Row<uint, uint, uint>, MetadataToken>(comparer);
			type_spec_map = new Dictionary<uint, MetadataToken>();
			member_ref_map = new Dictionary<Row<uint, uint, uint>, MetadataToken>(comparer);
			method_spec_map = new Dictionary<Row<uint, uint>, MetadataToken>(comparer);
			generic_parameters = new Collection<GenericParameter>();
			document_table = GetTable<DocumentTable>(Table.Document);
			method_debug_information_table = GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
			local_scope_table = GetTable<LocalScopeTable>(Table.LocalScope);
			local_variable_table = GetTable<LocalVariableTable>(Table.LocalVariable);
			local_constant_table = GetTable<LocalConstantTable>(Table.LocalConstant);
			import_scope_table = GetTable<ImportScopeTable>(Table.ImportScope);
			state_machine_method_table = GetTable<StateMachineMethodTable>(Table.StateMachineMethod);
			custom_debug_information_table = GetTable<CustomDebugInformationTable>(Table.CustomDebugInformation);
			document_map = new Dictionary<string, MetadataToken>(StringComparer.Ordinal);
			import_scope_map = new Dictionary<Row<uint, uint>, MetadataToken>(comparer);
		}

		public MetadataBuilder(ModuleDefinition module, PortablePdbWriterProvider writer_provider)
		{
			this.module = module;
			text_map = new TextMap();
			symbol_writer_provider = writer_provider;
			string_heap = new StringHeapBuffer();
			guid_heap = new GuidHeapBuffer();
			user_string_heap = new UserStringHeapBuffer();
			blob_heap = new BlobHeapBuffer();
			table_heap = new TableHeapBuffer(module, this);
			pdb_heap = new PdbHeapBuffer();
			document_table = GetTable<DocumentTable>(Table.Document);
			method_debug_information_table = GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
			local_scope_table = GetTable<LocalScopeTable>(Table.LocalScope);
			local_variable_table = GetTable<LocalVariableTable>(Table.LocalVariable);
			local_constant_table = GetTable<LocalConstantTable>(Table.LocalConstant);
			import_scope_table = GetTable<ImportScopeTable>(Table.ImportScope);
			state_machine_method_table = GetTable<StateMachineMethodTable>(Table.StateMachineMethod);
			custom_debug_information_table = GetTable<CustomDebugInformationTable>(Table.CustomDebugInformation);
			RowEqualityComparer comparer = new RowEqualityComparer();
			document_map = new Dictionary<string, MetadataToken>();
			import_scope_map = new Dictionary<Row<uint, uint>, MetadataToken>(comparer);
		}

		public void SetSymbolWriter(ISymbolWriter writer)
		{
			symbol_writer = writer;
			if (symbol_writer == null && module.HasImage && module.Image.HasDebugTables())
			{
				symbol_writer = new PortablePdbWriter(this, module);
			}
		}

		private TextMap CreateTextMap()
		{
			TextMap textMap = new TextMap();
			textMap.AddMap(TextSegment.ImportAddressTable, (module.Architecture == TargetArchitecture.I386) ? 8 : 0);
			textMap.AddMap(TextSegment.CLIHeader, 72, 8);
			bool flag = module.Architecture == TargetArchitecture.AMD64 || module.Architecture == TargetArchitecture.IA64 || module.Architecture == TargetArchitecture.ARM64;
			textMap.AddMap(TextSegment.Code, 0, (!flag) ? 4 : 16);
			return textMap;
		}

		private TTable GetTable<TTable>(Table table) where TTable : MetadataTable, new()
		{
			return table_heap.GetTable<TTable>(table);
		}

		private uint GetStringIndex(string @string)
		{
			if (string.IsNullOrEmpty(@string))
			{
				return 0u;
			}
			return string_heap.GetStringIndex(@string);
		}

		private uint GetGuidIndex(Guid guid)
		{
			return guid_heap.GetGuidIndex(guid);
		}

		private uint GetBlobIndex(ByteBuffer blob)
		{
			if (blob.length == 0)
			{
				return 0u;
			}
			return blob_heap.GetBlobIndex(blob);
		}

		private uint GetBlobIndex(byte[] blob)
		{
			if (blob.IsNullOrEmpty())
			{
				return 0u;
			}
			return GetBlobIndex(new ByteBuffer(blob));
		}

		public void BuildMetadata()
		{
			BuildModule();
			table_heap.string_offsets = string_heap.WriteStrings();
			table_heap.ComputeTableInformations();
			table_heap.WriteTableHeap();
		}

		private void BuildModule()
		{
			ModuleTable table = GetTable<ModuleTable>(Table.Module);
			table.row.Col1 = GetStringIndex(module.Name);
			table.row.Col2 = GetGuidIndex(module.Mvid);
			AssemblyDefinition assembly = module.Assembly;
			if (module.kind != ModuleKind.NetModule && assembly != null)
			{
				BuildAssembly();
			}
			if (module.HasAssemblyReferences)
			{
				AddAssemblyReferences();
			}
			if (module.HasModuleReferences)
			{
				AddModuleReferences();
			}
			if (module.HasResources)
			{
				AddResources();
			}
			if (module.HasExportedTypes)
			{
				AddExportedTypes();
			}
			BuildTypes();
			if (module.kind != ModuleKind.NetModule && assembly != null)
			{
				if (assembly.HasCustomAttributes)
				{
					AddCustomAttributes(assembly);
				}
				if (assembly.HasSecurityDeclarations)
				{
					AddSecurityDeclarations(assembly);
				}
			}
			if (module.HasCustomAttributes)
			{
				AddCustomAttributes(module);
			}
			if (module.EntryPoint != null)
			{
				entry_point = LookupToken(module.EntryPoint);
			}
		}

		private void BuildAssembly()
		{
			AssemblyDefinition assembly = module.Assembly;
			AssemblyNameDefinition name = assembly.Name;
			AssemblyTable table = GetTable<AssemblyTable>(Table.Assembly);
			table.row = new Row<AssemblyHashAlgorithm, ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint>(name.HashAlgorithm, (ushort)name.Version.Major, (ushort)name.Version.Minor, (ushort)name.Version.Build, (ushort)name.Version.Revision, name.Attributes, GetBlobIndex(name.PublicKey), GetStringIndex(name.Name), GetStringIndex(name.Culture));
			if (assembly.Modules.Count > 1)
			{
				BuildModules();
			}
		}

		private void BuildModules()
		{
			Collection<ModuleDefinition> modules = module.Assembly.Modules;
			FileTable table = GetTable<FileTable>(Table.File);
			for (int i = 0; i < modules.Count; i++)
			{
				ModuleDefinition moduleDefinition = modules[i];
				if (!moduleDefinition.IsMain)
				{
					WriterParameters parameters = new WriterParameters
					{
						SymbolWriterProvider = symbol_writer_provider
					};
					string moduleFileName = GetModuleFileName(moduleDefinition.Name);
					moduleDefinition.Write(moduleFileName, parameters);
					byte[] blob = CryptoService.ComputeHash(moduleFileName);
					table.AddRow(new Row<FileAttributes, uint, uint>(FileAttributes.ContainsMetaData, GetStringIndex(moduleDefinition.Name), GetBlobIndex(blob)));
				}
			}
		}

		private string GetModuleFileName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new NotSupportedException();
			}
			string directoryName = Path.GetDirectoryName(fq_name);
			return Path.Combine(directoryName, name);
		}

		private void AddAssemblyReferences()
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			AssemblyRefTable table = GetTable<AssemblyRefTable>(Table.AssemblyRef);
			if (module.IsWindowsMetadata())
			{
				module.Projections.RemoveVirtualReferences(assemblyReferences);
			}
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				byte[] blob = (assemblyNameReference.PublicKey.IsNullOrEmpty() ? assemblyNameReference.PublicKeyToken : assemblyNameReference.PublicKey);
				Version version = assemblyNameReference.Version;
				int rid = table.AddRow(new Row<ushort, ushort, ushort, ushort, AssemblyAttributes, uint, uint, uint, uint>((ushort)version.Major, (ushort)version.Minor, (ushort)version.Build, (ushort)version.Revision, assemblyNameReference.Attributes, GetBlobIndex(blob), GetStringIndex(assemblyNameReference.Name), GetStringIndex(assemblyNameReference.Culture), GetBlobIndex(assemblyNameReference.Hash)));
				assemblyNameReference.token = new MetadataToken(TokenType.AssemblyRef, rid);
			}
			if (module.IsWindowsMetadata())
			{
				module.Projections.AddVirtualReferences(assemblyReferences);
			}
		}

		private void AddModuleReferences()
		{
			Collection<ModuleReference> moduleReferences = module.ModuleReferences;
			ModuleRefTable table = GetTable<ModuleRefTable>(Table.ModuleRef);
			for (int i = 0; i < moduleReferences.Count; i++)
			{
				ModuleReference moduleReference = moduleReferences[i];
				moduleReference.token = new MetadataToken(TokenType.ModuleRef, table.AddRow(GetStringIndex(moduleReference.Name)));
			}
		}

		private void AddResources()
		{
			Collection<Resource> collection = module.Resources;
			ManifestResourceTable table = GetTable<ManifestResourceTable>(Table.ManifestResource);
			for (int i = 0; i < collection.Count; i++)
			{
				Resource resource = collection[i];
				Row<uint, ManifestResourceAttributes, uint, uint> row = new Row<uint, ManifestResourceAttributes, uint, uint>(0u, resource.Attributes, GetStringIndex(resource.Name), 0u);
				switch (resource.ResourceType)
				{
				case ResourceType.Embedded:
					row.Col1 = AddEmbeddedResource((EmbeddedResource)resource);
					break;
				case ResourceType.Linked:
					row.Col4 = CodedIndex.Implementation.CompressMetadataToken(new MetadataToken(TokenType.File, AddLinkedResource((LinkedResource)resource)));
					break;
				case ResourceType.AssemblyLinked:
					row.Col4 = CodedIndex.Implementation.CompressMetadataToken(((AssemblyLinkedResource)resource).Assembly.MetadataToken);
					break;
				default:
					throw new NotSupportedException();
				}
				table.AddRow(row);
			}
		}

		private uint AddLinkedResource(LinkedResource resource)
		{
			FileTable table = GetTable<FileTable>(Table.File);
			byte[] array = resource.Hash;
			if (array.IsNullOrEmpty())
			{
				array = CryptoService.ComputeHash(resource.File);
			}
			return (uint)table.AddRow(new Row<FileAttributes, uint, uint>(FileAttributes.ContainsNoMetaData, GetStringIndex(resource.File), GetBlobIndex(array)));
		}

		private uint AddEmbeddedResource(EmbeddedResource resource)
		{
			return resources.AddResource(resource.GetResourceData());
		}

		private void AddExportedTypes()
		{
			Collection<ExportedType> exportedTypes = module.ExportedTypes;
			ExportedTypeTable table = GetTable<ExportedTypeTable>(Table.ExportedType);
			for (int i = 0; i < exportedTypes.Count; i++)
			{
				ExportedType exportedType = exportedTypes[i];
				int rid = table.AddRow(new Row<TypeAttributes, uint, uint, uint, uint>(exportedType.Attributes, (uint)exportedType.Identifier, GetStringIndex(exportedType.Name), GetStringIndex(exportedType.Namespace), MakeCodedRID(GetExportedTypeScope(exportedType), CodedIndex.Implementation)));
				exportedType.token = new MetadataToken(TokenType.ExportedType, rid);
			}
		}

		private MetadataToken GetExportedTypeScope(ExportedType exported_type)
		{
			if (exported_type.DeclaringType != null)
			{
				return exported_type.DeclaringType.MetadataToken;
			}
			IMetadataScope scope = exported_type.Scope;
			switch (scope.MetadataToken.TokenType)
			{
			case TokenType.AssemblyRef:
				return scope.MetadataToken;
			case TokenType.ModuleRef:
			{
				FileTable table = GetTable<FileTable>(Table.File);
				for (int i = 0; i < table.length; i++)
				{
					if (table.rows[i].Col2 == GetStringIndex(scope.Name))
					{
						return new MetadataToken(TokenType.File, i + 1);
					}
				}
				break;
			}
			}
			throw new NotSupportedException();
		}

		private void BuildTypes()
		{
			if (module.HasTypes)
			{
				AttachTokens();
				AddTypes();
				AddGenericParameters();
			}
		}

		private void AttachTokens()
		{
			Collection<TypeDefinition> types = module.Types;
			for (int i = 0; i < types.Count; i++)
			{
				AttachTypeToken(types[i]);
			}
		}

		private void AttachTypeToken(TypeDefinition type)
		{
			TypeDefinitionProjection projection = WindowsRuntimeProjections.RemoveProjection(type);
			type.token = new MetadataToken(TokenType.TypeDef, type_rid++);
			type.fields_range.Start = field_rid;
			type.methods_range.Start = method_rid;
			if (type.HasFields)
			{
				AttachFieldsToken(type);
			}
			if (type.HasMethods)
			{
				AttachMethodsToken(type);
			}
			if (type.HasNestedTypes)
			{
				AttachNestedTypesToken(type);
			}
			WindowsRuntimeProjections.ApplyProjection(type, projection);
		}

		private void AttachNestedTypesToken(TypeDefinition type)
		{
			Collection<TypeDefinition> nestedTypes = type.NestedTypes;
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				AttachTypeToken(nestedTypes[i]);
			}
		}

		private void AttachFieldsToken(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			type.fields_range.Length = (uint)fields.Count;
			for (int i = 0; i < fields.Count; i++)
			{
				fields[i].token = new MetadataToken(TokenType.Field, field_rid++);
			}
		}

		private void AttachMethodsToken(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			type.methods_range.Length = (uint)methods.Count;
			for (int i = 0; i < methods.Count; i++)
			{
				methods[i].token = new MetadataToken(TokenType.Method, method_rid++);
			}
		}

		private MetadataToken GetTypeToken(TypeReference type)
		{
			if (type == null)
			{
				return MetadataToken.Zero;
			}
			if (type.IsDefinition)
			{
				return type.token;
			}
			if (type.IsTypeSpecification())
			{
				return GetTypeSpecToken(type);
			}
			return GetTypeRefToken(type);
		}

		private MetadataToken GetTypeSpecToken(TypeReference type)
		{
			uint blobIndex = GetBlobIndex(GetTypeSpecSignature(type));
			if (type_spec_map.TryGetValue(blobIndex, out var value))
			{
				return value;
			}
			return AddTypeSpecification(type, blobIndex);
		}

		private MetadataToken AddTypeSpecification(TypeReference type, uint row)
		{
			type.token = new MetadataToken(TokenType.TypeSpec, typespec_table.AddRow(row));
			MetadataToken token = type.token;
			type_spec_map.Add(row, token);
			return token;
		}

		private MetadataToken GetTypeRefToken(TypeReference type)
		{
			TypeReferenceProjection projection = WindowsRuntimeProjections.RemoveProjection(type);
			Row<uint, uint, uint> row = CreateTypeRefRow(type);
			if (!type_ref_map.TryGetValue(row, out var value))
			{
				value = AddTypeReference(type, row);
			}
			WindowsRuntimeProjections.ApplyProjection(type, projection);
			return value;
		}

		private Row<uint, uint, uint> CreateTypeRefRow(TypeReference type)
		{
			MetadataToken scopeToken = GetScopeToken(type);
			return new Row<uint, uint, uint>(MakeCodedRID(scopeToken, CodedIndex.ResolutionScope), GetStringIndex(type.Name), GetStringIndex(type.Namespace));
		}

		private MetadataToken GetScopeToken(TypeReference type)
		{
			if (type.IsNested)
			{
				return GetTypeRefToken(type.DeclaringType);
			}
			return type.Scope?.MetadataToken ?? MetadataToken.Zero;
		}

		private static uint MakeCodedRID(IMetadataTokenProvider provider, CodedIndex index)
		{
			return MakeCodedRID(provider.MetadataToken, index);
		}

		private static uint MakeCodedRID(MetadataToken token, CodedIndex index)
		{
			return index.CompressMetadataToken(token);
		}

		private MetadataToken AddTypeReference(TypeReference type, Row<uint, uint, uint> row)
		{
			type.token = new MetadataToken(TokenType.TypeRef, type_ref_table.AddRow(row));
			MetadataToken token = type.token;
			type_ref_map.Add(row, token);
			return token;
		}

		private void AddTypes()
		{
			Collection<TypeDefinition> types = module.Types;
			for (int i = 0; i < types.Count; i++)
			{
				AddType(types[i]);
			}
		}

		private void AddType(TypeDefinition type)
		{
			TypeDefinitionProjection projection = WindowsRuntimeProjections.RemoveProjection(type);
			type_def_table.AddRow(new Row<TypeAttributes, uint, uint, uint, uint, uint>(type.Attributes, GetStringIndex(type.Name), GetStringIndex(type.Namespace), MakeCodedRID(GetTypeToken(type.BaseType), CodedIndex.TypeDefOrRef), type.fields_range.Start, type.methods_range.Start));
			if (type.HasGenericParameters)
			{
				AddGenericParameters(type);
			}
			if (type.HasInterfaces)
			{
				AddInterfaces(type);
			}
			if (type.HasLayoutInfo)
			{
				AddLayoutInfo(type);
			}
			if (type.HasFields)
			{
				AddFields(type);
			}
			if (type.HasMethods)
			{
				AddMethods(type);
			}
			if (type.HasProperties)
			{
				AddProperties(type);
			}
			if (type.HasEvents)
			{
				AddEvents(type);
			}
			if (type.HasCustomAttributes)
			{
				AddCustomAttributes(type);
			}
			if (type.HasSecurityDeclarations)
			{
				AddSecurityDeclarations(type);
			}
			if (type.HasNestedTypes)
			{
				AddNestedTypes(type);
			}
			WindowsRuntimeProjections.ApplyProjection(type, projection);
		}

		private void AddGenericParameters(IGenericParameterProvider owner)
		{
			Collection<GenericParameter> genericParameters = owner.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				generic_parameters.Add(genericParameters[i]);
			}
		}

		private void AddGenericParameters()
		{
			GenericParameter[] items = generic_parameters.items;
			int size = generic_parameters.size;
			Array.Sort(items, 0, size, new GenericParameterComparer());
			GenericParamTable table = GetTable<GenericParamTable>(Table.GenericParam);
			GenericParamConstraintTable table2 = GetTable<GenericParamConstraintTable>(Table.GenericParamConstraint);
			for (int i = 0; i < size; i++)
			{
				GenericParameter genericParameter = items[i];
				int rid = table.AddRow(new Row<ushort, GenericParameterAttributes, uint, uint>((ushort)genericParameter.Position, genericParameter.Attributes, MakeCodedRID(genericParameter.Owner, CodedIndex.TypeOrMethodDef), GetStringIndex(genericParameter.Name)));
				genericParameter.token = new MetadataToken(TokenType.GenericParam, rid);
				if (genericParameter.HasConstraints)
				{
					AddConstraints(genericParameter, table2);
				}
				if (genericParameter.HasCustomAttributes)
				{
					AddCustomAttributes(genericParameter);
				}
			}
		}

		private void AddConstraints(GenericParameter generic_parameter, GenericParamConstraintTable table)
		{
			Collection<GenericParameterConstraint> constraints = generic_parameter.Constraints;
			uint rID = generic_parameter.token.RID;
			for (int i = 0; i < constraints.Count; i++)
			{
				GenericParameterConstraint genericParameterConstraint = constraints[i];
				int rid = table.AddRow(new Row<uint, uint>(rID, MakeCodedRID(GetTypeToken(genericParameterConstraint.ConstraintType), CodedIndex.TypeDefOrRef)));
				genericParameterConstraint.token = new MetadataToken(TokenType.GenericParamConstraint, rid);
				if (genericParameterConstraint.HasCustomAttributes)
				{
					AddCustomAttributes(genericParameterConstraint);
				}
			}
		}

		private void AddInterfaces(TypeDefinition type)
		{
			Collection<InterfaceImplementation> interfaces = type.Interfaces;
			uint rID = type.token.RID;
			for (int i = 0; i < interfaces.Count; i++)
			{
				InterfaceImplementation interfaceImplementation = interfaces[i];
				int rid = iface_impl_table.AddRow(new Row<uint, uint>(rID, MakeCodedRID(GetTypeToken(interfaceImplementation.InterfaceType), CodedIndex.TypeDefOrRef)));
				interfaceImplementation.token = new MetadataToken(TokenType.InterfaceImpl, rid);
				if (interfaceImplementation.HasCustomAttributes)
				{
					AddCustomAttributes(interfaceImplementation);
				}
			}
		}

		private void AddLayoutInfo(TypeDefinition type)
		{
			ClassLayoutTable table = GetTable<ClassLayoutTable>(Table.ClassLayout);
			table.AddRow(new Row<ushort, uint, uint>((ushort)type.PackingSize, (uint)type.ClassSize, type.token.RID));
		}

		private void AddNestedTypes(TypeDefinition type)
		{
			Collection<TypeDefinition> nestedTypes = type.NestedTypes;
			NestedClassTable table = GetTable<NestedClassTable>(Table.NestedClass);
			for (int i = 0; i < nestedTypes.Count; i++)
			{
				TypeDefinition typeDefinition = nestedTypes[i];
				AddType(typeDefinition);
				table.AddRow(new Row<uint, uint>(typeDefinition.token.RID, type.token.RID));
			}
		}

		private void AddFields(TypeDefinition type)
		{
			Collection<FieldDefinition> fields = type.Fields;
			for (int i = 0; i < fields.Count; i++)
			{
				AddField(fields[i]);
			}
		}

		private void AddField(FieldDefinition field)
		{
			FieldDefinitionProjection projection = WindowsRuntimeProjections.RemoveProjection(field);
			field_table.AddRow(new Row<FieldAttributes, uint, uint>(field.Attributes, GetStringIndex(field.Name), GetBlobIndex(GetFieldSignature(field))));
			if (!field.InitialValue.IsNullOrEmpty())
			{
				AddFieldRVA(field);
			}
			if (field.HasLayoutInfo)
			{
				AddFieldLayout(field);
			}
			if (field.HasCustomAttributes)
			{
				AddCustomAttributes(field);
			}
			if (field.HasConstant)
			{
				AddConstant(field, field.FieldType);
			}
			if (field.HasMarshalInfo)
			{
				AddMarshalInfo(field);
			}
			WindowsRuntimeProjections.ApplyProjection(field, projection);
		}

		private void AddFieldRVA(FieldDefinition field)
		{
			FieldRVATable table = GetTable<FieldRVATable>(Table.FieldRVA);
			int align = 1;
			if (field.FieldType.IsDefinition && !field.FieldType.IsGenericInstance)
			{
				TypeDefinition typeDefinition = field.FieldType.Resolve();
				if (typeDefinition.Module == module && typeDefinition.PackingSize > 1)
				{
					align = typeDefinition.PackingSize;
				}
			}
			table.AddRow(new Row<uint, uint>(data.AddData(field.InitialValue, align), field.token.RID));
		}

		private void AddFieldLayout(FieldDefinition field)
		{
			FieldLayoutTable table = GetTable<FieldLayoutTable>(Table.FieldLayout);
			table.AddRow(new Row<uint, uint>((uint)field.Offset, field.token.RID));
		}

		private void AddMethods(TypeDefinition type)
		{
			Collection<MethodDefinition> methods = type.Methods;
			for (int i = 0; i < methods.Count; i++)
			{
				AddMethod(methods[i]);
			}
		}

		private void AddMethod(MethodDefinition method)
		{
			MethodDefinitionProjection projection = WindowsRuntimeProjections.RemoveProjection(method);
			method_table.AddRow(new Row<uint, MethodImplAttributes, MethodAttributes, uint, uint, uint>(method.HasBody ? code.WriteMethodBody(method) : 0u, method.ImplAttributes, method.Attributes, GetStringIndex(method.Name), GetBlobIndex(GetMethodSignature(method)), param_rid));
			AddParameters(method);
			if (method.HasGenericParameters)
			{
				AddGenericParameters(method);
			}
			if (method.IsPInvokeImpl)
			{
				AddPInvokeInfo(method);
			}
			if (method.HasCustomAttributes)
			{
				AddCustomAttributes(method);
			}
			if (method.HasSecurityDeclarations)
			{
				AddSecurityDeclarations(method);
			}
			if (method.HasOverrides)
			{
				AddOverrides(method);
			}
			WindowsRuntimeProjections.ApplyProjection(method, projection);
		}

		private void AddParameters(MethodDefinition method)
		{
			ParameterDefinition parameter = method.MethodReturnType.parameter;
			if (parameter != null && RequiresParameterRow(parameter))
			{
				AddParameter(0, parameter, param_table);
			}
			if (!method.HasParameters)
			{
				return;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			for (int i = 0; i < parameters.Count; i++)
			{
				ParameterDefinition parameter2 = parameters[i];
				if (RequiresParameterRow(parameter2))
				{
					AddParameter((ushort)(i + 1), parameter2, param_table);
				}
			}
		}

		private void AddPInvokeInfo(MethodDefinition method)
		{
			PInvokeInfo pInvokeInfo = method.PInvokeInfo;
			if (pInvokeInfo != null)
			{
				ImplMapTable table = GetTable<ImplMapTable>(Table.ImplMap);
				table.AddRow(new Row<PInvokeAttributes, uint, uint, uint>(pInvokeInfo.Attributes, MakeCodedRID(method, CodedIndex.MemberForwarded), GetStringIndex(pInvokeInfo.EntryPoint), pInvokeInfo.Module.MetadataToken.RID));
			}
		}

		private void AddOverrides(MethodDefinition method)
		{
			Collection<MethodReference> overrides = method.Overrides;
			MethodImplTable table = GetTable<MethodImplTable>(Table.MethodImpl);
			for (int i = 0; i < overrides.Count; i++)
			{
				table.AddRow(new Row<uint, uint, uint>(method.DeclaringType.token.RID, MakeCodedRID(method, CodedIndex.MethodDefOrRef), MakeCodedRID(LookupToken(overrides[i]), CodedIndex.MethodDefOrRef)));
			}
		}

		private static bool RequiresParameterRow(ParameterDefinition parameter)
		{
			return !string.IsNullOrEmpty(parameter.Name) || parameter.Attributes != ParameterAttributes.None || parameter.HasMarshalInfo || parameter.HasConstant || parameter.HasCustomAttributes;
		}

		private void AddParameter(ushort sequence, ParameterDefinition parameter, ParamTable table)
		{
			table.AddRow(new Row<ParameterAttributes, ushort, uint>(parameter.Attributes, sequence, GetStringIndex(parameter.Name)));
			parameter.token = new MetadataToken(TokenType.Param, param_rid++);
			if (parameter.HasCustomAttributes)
			{
				AddCustomAttributes(parameter);
			}
			if (parameter.HasConstant)
			{
				AddConstant(parameter, parameter.ParameterType);
			}
			if (parameter.HasMarshalInfo)
			{
				AddMarshalInfo(parameter);
			}
		}

		private void AddMarshalInfo(IMarshalInfoProvider owner)
		{
			FieldMarshalTable table = GetTable<FieldMarshalTable>(Table.FieldMarshal);
			table.AddRow(new Row<uint, uint>(MakeCodedRID(owner, CodedIndex.HasFieldMarshal), GetBlobIndex(GetMarshalInfoSignature(owner))));
		}

		private void AddProperties(TypeDefinition type)
		{
			Collection<PropertyDefinition> properties = type.Properties;
			property_map_table.AddRow(new Row<uint, uint>(type.token.RID, property_rid));
			for (int i = 0; i < properties.Count; i++)
			{
				AddProperty(properties[i]);
			}
		}

		private void AddProperty(PropertyDefinition property)
		{
			property_table.AddRow(new Row<PropertyAttributes, uint, uint>(property.Attributes, GetStringIndex(property.Name), GetBlobIndex(GetPropertySignature(property))));
			property.token = new MetadataToken(TokenType.Property, property_rid++);
			MethodDefinition getMethod = property.GetMethod;
			if (getMethod != null)
			{
				AddSemantic(MethodSemanticsAttributes.Getter, property, getMethod);
			}
			getMethod = property.SetMethod;
			if (getMethod != null)
			{
				AddSemantic(MethodSemanticsAttributes.Setter, property, getMethod);
			}
			if (property.HasOtherMethods)
			{
				AddOtherSemantic(property, property.OtherMethods);
			}
			if (property.HasCustomAttributes)
			{
				AddCustomAttributes(property);
			}
			if (property.HasConstant)
			{
				AddConstant(property, property.PropertyType);
			}
		}

		private void AddOtherSemantic(IMetadataTokenProvider owner, Collection<MethodDefinition> others)
		{
			for (int i = 0; i < others.Count; i++)
			{
				AddSemantic(MethodSemanticsAttributes.Other, owner, others[i]);
			}
		}

		private void AddEvents(TypeDefinition type)
		{
			Collection<EventDefinition> events = type.Events;
			event_map_table.AddRow(new Row<uint, uint>(type.token.RID, event_rid));
			for (int i = 0; i < events.Count; i++)
			{
				AddEvent(events[i]);
			}
		}

		private void AddEvent(EventDefinition @event)
		{
			event_table.AddRow(new Row<EventAttributes, uint, uint>(@event.Attributes, GetStringIndex(@event.Name), MakeCodedRID(GetTypeToken(@event.EventType), CodedIndex.TypeDefOrRef)));
			@event.token = new MetadataToken(TokenType.Event, event_rid++);
			MethodDefinition addMethod = @event.AddMethod;
			if (addMethod != null)
			{
				AddSemantic(MethodSemanticsAttributes.AddOn, @event, addMethod);
			}
			addMethod = @event.InvokeMethod;
			if (addMethod != null)
			{
				AddSemantic(MethodSemanticsAttributes.Fire, @event, addMethod);
			}
			addMethod = @event.RemoveMethod;
			if (addMethod != null)
			{
				AddSemantic(MethodSemanticsAttributes.RemoveOn, @event, addMethod);
			}
			if (@event.HasOtherMethods)
			{
				AddOtherSemantic(@event, @event.OtherMethods);
			}
			if (@event.HasCustomAttributes)
			{
				AddCustomAttributes(@event);
			}
		}

		private void AddSemantic(MethodSemanticsAttributes semantics, IMetadataTokenProvider provider, MethodDefinition method)
		{
			method.SemanticsAttributes = semantics;
			MethodSemanticsTable table = GetTable<MethodSemanticsTable>(Table.MethodSemantics);
			table.AddRow(new Row<MethodSemanticsAttributes, uint, uint>(semantics, method.token.RID, MakeCodedRID(provider, CodedIndex.HasSemantics)));
		}

		private void AddConstant(IConstantProvider owner, TypeReference type)
		{
			object constant = owner.Constant;
			ElementType constantType = GetConstantType(type, constant);
			constant_table.AddRow(new Row<ElementType, uint, uint>(constantType, MakeCodedRID(owner.MetadataToken, CodedIndex.HasConstant), GetBlobIndex(GetConstantSignature(constantType, constant))));
		}

		private static ElementType GetConstantType(TypeReference constant_type, object constant)
		{
			if (constant == null)
			{
				return ElementType.Class;
			}
			ElementType etype = constant_type.etype;
			switch (etype)
			{
			case ElementType.None:
			{
				TypeDefinition typeDefinition = constant_type.CheckedResolve();
				if (typeDefinition.IsEnum)
				{
					return GetConstantType(typeDefinition.GetEnumUnderlyingType(), constant);
				}
				return ElementType.Class;
			}
			case ElementType.String:
				return ElementType.String;
			case ElementType.Object:
				return GetConstantType(constant.GetType());
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.SzArray:
			case ElementType.MVar:
				return ElementType.Class;
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)constant_type;
				if (genericInstanceType.ElementType.IsTypeOf("System", "Nullable`1"))
				{
					return GetConstantType(genericInstanceType.GenericArguments[0], constant);
				}
				return GetConstantType(((TypeSpecification)constant_type).ElementType, constant);
			}
			case ElementType.ByRef:
			case ElementType.CModReqD:
			case ElementType.CModOpt:
			case ElementType.Sentinel:
				return GetConstantType(((TypeSpecification)constant_type).ElementType, constant);
			case ElementType.Boolean:
			case ElementType.Char:
			case ElementType.I1:
			case ElementType.U1:
			case ElementType.I2:
			case ElementType.U2:
			case ElementType.I4:
			case ElementType.U4:
			case ElementType.I8:
			case ElementType.U8:
			case ElementType.R4:
			case ElementType.R8:
			case ElementType.I:
			case ElementType.U:
				return GetConstantType(constant.GetType());
			default:
				return etype;
			}
		}

		private static ElementType GetConstantType(Type type)
		{
			return Type.GetTypeCode(type) switch
			{
				TypeCode.Boolean => ElementType.Boolean, 
				TypeCode.Byte => ElementType.U1, 
				TypeCode.SByte => ElementType.I1, 
				TypeCode.Char => ElementType.Char, 
				TypeCode.Int16 => ElementType.I2, 
				TypeCode.UInt16 => ElementType.U2, 
				TypeCode.Int32 => ElementType.I4, 
				TypeCode.UInt32 => ElementType.U4, 
				TypeCode.Int64 => ElementType.I8, 
				TypeCode.UInt64 => ElementType.U8, 
				TypeCode.Single => ElementType.R4, 
				TypeCode.Double => ElementType.R8, 
				TypeCode.String => ElementType.String, 
				_ => throw new NotSupportedException(type.FullName), 
			};
		}

		private void AddCustomAttributes(ICustomAttributeProvider owner)
		{
			Collection<CustomAttribute> customAttributes = owner.CustomAttributes;
			for (int i = 0; i < customAttributes.Count; i++)
			{
				CustomAttribute customAttribute = customAttributes[i];
				CustomAttributeValueProjection projection = WindowsRuntimeProjections.RemoveProjection(customAttribute);
				custom_attribute_table.AddRow(new Row<uint, uint, uint>(MakeCodedRID(owner, CodedIndex.HasCustomAttribute), MakeCodedRID(LookupToken(customAttribute.Constructor), CodedIndex.CustomAttributeType), GetBlobIndex(GetCustomAttributeSignature(customAttribute))));
				WindowsRuntimeProjections.ApplyProjection(customAttribute, projection);
			}
		}

		private void AddSecurityDeclarations(ISecurityDeclarationProvider owner)
		{
			Collection<SecurityDeclaration> securityDeclarations = owner.SecurityDeclarations;
			for (int i = 0; i < securityDeclarations.Count; i++)
			{
				SecurityDeclaration securityDeclaration = securityDeclarations[i];
				declsec_table.AddRow(new Row<SecurityAction, uint, uint>(securityDeclaration.Action, MakeCodedRID(owner, CodedIndex.HasDeclSecurity), GetBlobIndex(GetSecurityDeclarationSignature(securityDeclaration))));
			}
		}

		private MetadataToken GetMemberRefToken(MemberReference member)
		{
			Row<uint, uint, uint> row = CreateMemberRefRow(member);
			if (!member_ref_map.TryGetValue(row, out var value))
			{
				return AddMemberReference(member, row);
			}
			return value;
		}

		private Row<uint, uint, uint> CreateMemberRefRow(MemberReference member)
		{
			return new Row<uint, uint, uint>(MakeCodedRID(GetTypeToken(member.DeclaringType), CodedIndex.MemberRefParent), GetStringIndex(member.Name), GetBlobIndex(GetMemberRefSignature(member)));
		}

		private MetadataToken AddMemberReference(MemberReference member, Row<uint, uint, uint> row)
		{
			member.token = new MetadataToken(TokenType.MemberRef, member_ref_table.AddRow(row));
			MetadataToken token = member.token;
			member_ref_map.Add(row, token);
			return token;
		}

		private MetadataToken GetMethodSpecToken(MethodSpecification method_spec)
		{
			Row<uint, uint> row = CreateMethodSpecRow(method_spec);
			if (method_spec_map.TryGetValue(row, out var value))
			{
				return value;
			}
			AddMethodSpecification(method_spec, row);
			return method_spec.token;
		}

		private void AddMethodSpecification(MethodSpecification method_spec, Row<uint, uint> row)
		{
			method_spec.token = new MetadataToken(TokenType.MethodSpec, method_spec_table.AddRow(row));
			method_spec_map.Add(row, method_spec.token);
		}

		private Row<uint, uint> CreateMethodSpecRow(MethodSpecification method_spec)
		{
			return new Row<uint, uint>(MakeCodedRID(LookupToken(method_spec.ElementMethod), CodedIndex.MethodDefOrRef), GetBlobIndex(GetMethodSpecSignature(method_spec)));
		}

		private SignatureWriter CreateSignatureWriter()
		{
			return new SignatureWriter(this);
		}

		private SignatureWriter GetMethodSpecSignature(MethodSpecification method_spec)
		{
			if (!method_spec.IsGenericInstance)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod instance = (GenericInstanceMethod)method_spec;
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteByte(10);
			signatureWriter.WriteGenericInstanceSignature(instance);
			return signatureWriter;
		}

		public uint AddStandAloneSignature(uint signature)
		{
			return (uint)standalone_sig_table.AddRow(signature);
		}

		public uint GetLocalVariableBlobIndex(Collection<VariableDefinition> variables)
		{
			return GetBlobIndex(GetVariablesSignature(variables));
		}

		public uint GetCallSiteBlobIndex(CallSite call_site)
		{
			return GetBlobIndex(GetMethodSignature(call_site));
		}

		public uint GetConstantTypeBlobIndex(TypeReference constant_type)
		{
			return GetBlobIndex(GetConstantTypeSignature(constant_type));
		}

		private SignatureWriter GetVariablesSignature(Collection<VariableDefinition> variables)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteByte(7);
			signatureWriter.WriteCompressedUInt32((uint)variables.Count);
			for (int i = 0; i < variables.Count; i++)
			{
				signatureWriter.WriteTypeSignature(variables[i].VariableType);
			}
			return signatureWriter;
		}

		private SignatureWriter GetConstantTypeSignature(TypeReference constant_type)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteByte(6);
			signatureWriter.WriteTypeSignature(constant_type);
			return signatureWriter;
		}

		private SignatureWriter GetFieldSignature(FieldReference field)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteByte(6);
			signatureWriter.WriteTypeSignature(field.FieldType);
			return signatureWriter;
		}

		private SignatureWriter GetMethodSignature(IMethodSignature method)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteMethodSignature(method);
			return signatureWriter;
		}

		private SignatureWriter GetMemberRefSignature(MemberReference member)
		{
			if (member is FieldReference field)
			{
				return GetFieldSignature(field);
			}
			if (member is MethodReference method)
			{
				return GetMethodSignature(method);
			}
			throw new NotSupportedException();
		}

		private SignatureWriter GetPropertySignature(PropertyDefinition property)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			byte b = 8;
			if (property.HasThis)
			{
				b |= 0x20;
			}
			uint num = 0u;
			Collection<ParameterDefinition> collection = null;
			if (property.HasParameters)
			{
				collection = property.Parameters;
				num = (uint)collection.Count;
			}
			signatureWriter.WriteByte(b);
			signatureWriter.WriteCompressedUInt32(num);
			signatureWriter.WriteTypeSignature(property.PropertyType);
			if (num == 0)
			{
				return signatureWriter;
			}
			for (int i = 0; i < num; i++)
			{
				signatureWriter.WriteTypeSignature(collection[i].ParameterType);
			}
			return signatureWriter;
		}

		private SignatureWriter GetTypeSpecSignature(TypeReference type)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteTypeSignature(type);
			return signatureWriter;
		}

		private SignatureWriter GetConstantSignature(ElementType type, object value)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			switch (type)
			{
			case ElementType.None:
			case ElementType.Class:
			case ElementType.Var:
			case ElementType.Array:
			case ElementType.GenericInst:
			case ElementType.Object:
			case ElementType.SzArray:
			case ElementType.MVar:
				signatureWriter.WriteInt32(0);
				break;
			case ElementType.String:
				signatureWriter.WriteConstantString((string)value);
				break;
			default:
				signatureWriter.WriteConstantPrimitive(value);
				break;
			}
			return signatureWriter;
		}

		private SignatureWriter GetCustomAttributeSignature(CustomAttribute attribute)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			if (!attribute.resolved)
			{
				signatureWriter.WriteBytes(attribute.GetBlob());
				return signatureWriter;
			}
			signatureWriter.WriteUInt16(1);
			signatureWriter.WriteCustomAttributeConstructorArguments(attribute);
			signatureWriter.WriteCustomAttributeNamedArguments(attribute);
			return signatureWriter;
		}

		private SignatureWriter GetSecurityDeclarationSignature(SecurityDeclaration declaration)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			if (!declaration.resolved)
			{
				signatureWriter.WriteBytes(declaration.GetBlob());
			}
			else if (module.Runtime < TargetRuntime.Net_2_0)
			{
				signatureWriter.WriteXmlSecurityDeclaration(declaration);
			}
			else
			{
				signatureWriter.WriteSecurityDeclaration(declaration);
			}
			return signatureWriter;
		}

		private SignatureWriter GetMarshalInfoSignature(IMarshalInfoProvider owner)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteMarshalInfo(owner.MarshalInfo);
			return signatureWriter;
		}

		private static Exception CreateForeignMemberException(MemberReference member)
		{
			return new ArgumentException($"Member '{member}' is declared in another module and needs to be imported");
		}

		public MetadataToken LookupToken(IMetadataTokenProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException();
			}
			if (metadata_builder != null)
			{
				return metadata_builder.LookupToken(provider);
			}
			MemberReference memberReference = provider as MemberReference;
			if (memberReference == null || memberReference.Module != module)
			{
				throw CreateForeignMemberException(memberReference);
			}
			MetadataToken metadataToken = provider.MetadataToken;
			switch (metadataToken.TokenType)
			{
			case TokenType.TypeDef:
			case TokenType.Field:
			case TokenType.Method:
			case TokenType.Event:
			case TokenType.Property:
				return metadataToken;
			case TokenType.TypeRef:
			case TokenType.TypeSpec:
			case TokenType.GenericParam:
				return GetTypeToken((TypeReference)provider);
			case TokenType.MethodSpec:
				return GetMethodSpecToken((MethodSpecification)provider);
			case TokenType.MemberRef:
				return GetMemberRefToken(memberReference);
			default:
				throw new NotSupportedException();
			}
		}

		public void AddMethodDebugInformation(MethodDebugInformation method_info)
		{
			if (method_info.HasSequencePoints)
			{
				AddSequencePoints(method_info);
			}
			if (method_info.Scope != null)
			{
				AddLocalScope(method_info, method_info.Scope);
			}
			if (method_info.StateMachineKickOffMethod != null)
			{
				AddStateMachineMethod(method_info);
			}
			AddCustomDebugInformations(method_info.Method);
		}

		private void AddStateMachineMethod(MethodDebugInformation method_info)
		{
			state_machine_method_table.AddRow(new Row<uint, uint>(method_info.Method.MetadataToken.RID, method_info.StateMachineKickOffMethod.MetadataToken.RID));
		}

		private void AddLocalScope(MethodDebugInformation method_info, ScopeDebugInformation scope)
		{
			int rid = local_scope_table.AddRow(new Row<uint, uint, uint, uint, uint, uint>(method_info.Method.MetadataToken.RID, (scope.import != null) ? AddImportScope(scope.import) : 0u, local_variable_rid, local_constant_rid, (uint)scope.Start.Offset, (uint)((scope.End.IsEndOfMethod ? method_info.code_size : scope.End.Offset) - scope.Start.Offset)));
			scope.token = new MetadataToken(TokenType.LocalScope, rid);
			AddCustomDebugInformations(scope);
			if (scope.HasVariables)
			{
				AddLocalVariables(scope);
			}
			if (scope.HasConstants)
			{
				AddLocalConstants(scope);
			}
			for (int i = 0; i < scope.Scopes.Count; i++)
			{
				AddLocalScope(method_info, scope.Scopes[i]);
			}
		}

		private void AddLocalVariables(ScopeDebugInformation scope)
		{
			for (int i = 0; i < scope.Variables.Count; i++)
			{
				VariableDebugInformation variableDebugInformation = scope.Variables[i];
				local_variable_table.AddRow(new Row<VariableAttributes, ushort, uint>(variableDebugInformation.Attributes, (ushort)variableDebugInformation.Index, GetStringIndex(variableDebugInformation.Name)));
				variableDebugInformation.token = new MetadataToken(TokenType.LocalVariable, local_variable_rid);
				local_variable_rid++;
				AddCustomDebugInformations(variableDebugInformation);
			}
		}

		private void AddLocalConstants(ScopeDebugInformation scope)
		{
			for (int i = 0; i < scope.Constants.Count; i++)
			{
				ConstantDebugInformation constantDebugInformation = scope.Constants[i];
				local_constant_table.AddRow(new Row<uint, uint>(GetStringIndex(constantDebugInformation.Name), GetBlobIndex(GetConstantSignature(constantDebugInformation))));
				constantDebugInformation.token = new MetadataToken(TokenType.LocalConstant, local_constant_rid);
				local_constant_rid++;
			}
		}

		private SignatureWriter GetConstantSignature(ConstantDebugInformation constant)
		{
			TypeReference constantType = constant.ConstantType;
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteTypeSignature(constantType);
			if (constantType.IsTypeOf("System", "Decimal"))
			{
				int[] bits = decimal.GetBits((decimal)constant.Value);
				uint value = (uint)bits[0];
				uint value2 = (uint)bits[1];
				uint value3 = (uint)bits[2];
				byte b = (byte)(bits[3] >> 16);
				bool flag = (bits[3] & 0x80000000u) != 0;
				signatureWriter.WriteByte((byte)(b | (flag ? 128 : 0)));
				signatureWriter.WriteUInt32(value);
				signatureWriter.WriteUInt32(value2);
				signatureWriter.WriteUInt32(value3);
				return signatureWriter;
			}
			if (constantType.IsTypeOf("System", "DateTime"))
			{
				signatureWriter.WriteInt64(((DateTime)constant.Value).Ticks);
				return signatureWriter;
			}
			signatureWriter.WriteBytes(GetConstantSignature(constantType.etype, constant.Value));
			return signatureWriter;
		}

		public void AddCustomDebugInformations(ICustomDebugInformationProvider provider)
		{
			if (!provider.HasCustomDebugInformations)
			{
				return;
			}
			Collection<CustomDebugInformation> customDebugInformations = provider.CustomDebugInformations;
			for (int i = 0; i < customDebugInformations.Count; i++)
			{
				CustomDebugInformation customDebugInformation = customDebugInformations[i];
				switch (customDebugInformation.Kind)
				{
				case CustomDebugInformationKind.Binary:
				{
					BinaryCustomDebugInformation binaryCustomDebugInformation = (BinaryCustomDebugInformation)customDebugInformation;
					AddCustomDebugInformation(provider, binaryCustomDebugInformation, GetBlobIndex(binaryCustomDebugInformation.Data));
					break;
				}
				case CustomDebugInformationKind.AsyncMethodBody:
					AddAsyncMethodBodyDebugInformation(provider, (AsyncMethodBodyDebugInformation)customDebugInformation);
					break;
				case CustomDebugInformationKind.StateMachineScope:
					AddStateMachineScopeDebugInformation(provider, (StateMachineScopeDebugInformation)customDebugInformation);
					break;
				case CustomDebugInformationKind.EmbeddedSource:
					AddEmbeddedSourceDebugInformation(provider, (EmbeddedSourceDebugInformation)customDebugInformation);
					break;
				case CustomDebugInformationKind.SourceLink:
					AddSourceLinkDebugInformation(provider, (SourceLinkDebugInformation)customDebugInformation);
					break;
				default:
					throw new NotImplementedException();
				}
			}
		}

		private void AddStateMachineScopeDebugInformation(ICustomDebugInformationProvider provider, StateMachineScopeDebugInformation state_machine_scope)
		{
			MethodDebugInformation debugInformation = ((MethodDefinition)provider).DebugInformation;
			SignatureWriter signatureWriter = CreateSignatureWriter();
			Collection<StateMachineScope> scopes = state_machine_scope.Scopes;
			for (int i = 0; i < scopes.Count; i++)
			{
				StateMachineScope stateMachineScope = scopes[i];
				signatureWriter.WriteUInt32((uint)stateMachineScope.Start.Offset);
				int num = (stateMachineScope.End.IsEndOfMethod ? debugInformation.code_size : stateMachineScope.End.Offset);
				signatureWriter.WriteUInt32((uint)(num - stateMachineScope.Start.Offset));
			}
			AddCustomDebugInformation(provider, state_machine_scope, signatureWriter);
		}

		private void AddAsyncMethodBodyDebugInformation(ICustomDebugInformationProvider provider, AsyncMethodBodyDebugInformation async_method)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteUInt32((uint)(async_method.catch_handler.Offset + 1));
			if (!async_method.yields.IsNullOrEmpty())
			{
				for (int i = 0; i < async_method.yields.Count; i++)
				{
					signatureWriter.WriteUInt32((uint)async_method.yields[i].Offset);
					signatureWriter.WriteUInt32((uint)async_method.resumes[i].Offset);
					signatureWriter.WriteCompressedUInt32(async_method.resume_methods[i].MetadataToken.RID);
				}
			}
			AddCustomDebugInformation(provider, async_method, signatureWriter);
		}

		private void AddEmbeddedSourceDebugInformation(ICustomDebugInformationProvider provider, EmbeddedSourceDebugInformation embedded_source)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			if (!embedded_source.resolved)
			{
				signatureWriter.WriteBytes(embedded_source.ReadRawEmbeddedSourceDebugInformation());
				AddCustomDebugInformation(provider, embedded_source, signatureWriter);
				return;
			}
			byte[] array = embedded_source.content ?? Empty<byte>.Array;
			if (embedded_source.compress)
			{
				signatureWriter.WriteInt32(array.Length);
				MemoryStream memoryStream = new MemoryStream(array);
				MemoryStream memoryStream2 = new MemoryStream();
				using (DeflateStream destination = new DeflateStream(memoryStream2, CompressionMode.Compress, leaveOpen: true))
				{
					memoryStream.CopyTo(destination);
				}
				signatureWriter.WriteBytes(memoryStream2.ToArray());
			}
			else
			{
				signatureWriter.WriteInt32(0);
				signatureWriter.WriteBytes(array);
			}
			AddCustomDebugInformation(provider, embedded_source, signatureWriter);
		}

		private void AddSourceLinkDebugInformation(ICustomDebugInformationProvider provider, SourceLinkDebugInformation source_link)
		{
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteBytes(Encoding.UTF8.GetBytes(source_link.content));
			AddCustomDebugInformation(provider, source_link, signatureWriter);
		}

		private void AddCustomDebugInformation(ICustomDebugInformationProvider provider, CustomDebugInformation custom_info, SignatureWriter signature)
		{
			AddCustomDebugInformation(provider, custom_info, GetBlobIndex(signature));
		}

		private void AddCustomDebugInformation(ICustomDebugInformationProvider provider, CustomDebugInformation custom_info, uint blob_index)
		{
			int rid = custom_debug_information_table.AddRow(new Row<uint, uint, uint>(MakeCodedRID(provider.MetadataToken, CodedIndex.HasCustomDebugInformation), GetGuidIndex(custom_info.Identifier), blob_index));
			custom_info.token = new MetadataToken(TokenType.CustomDebugInformation, rid);
		}

		private uint AddImportScope(ImportDebugInformation import)
		{
			uint col = 0u;
			if (import.Parent != null)
			{
				col = AddImportScope(import.Parent);
			}
			uint col2 = 0u;
			if (import.HasTargets)
			{
				SignatureWriter signatureWriter = CreateSignatureWriter();
				for (int i = 0; i < import.Targets.Count; i++)
				{
					AddImportTarget(import.Targets[i], signatureWriter);
				}
				col2 = GetBlobIndex(signatureWriter);
			}
			Row<uint, uint> row = new Row<uint, uint>(col, col2);
			if (import_scope_map.TryGetValue(row, out var value))
			{
				return value.RID;
			}
			value = new MetadataToken(TokenType.ImportScope, import_scope_table.AddRow(row));
			import_scope_map.Add(row, value);
			return value.RID;
		}

		private void AddImportTarget(ImportTarget target, SignatureWriter signature)
		{
			signature.WriteCompressedUInt32((uint)target.kind);
			switch (target.kind)
			{
			case ImportTargetKind.ImportNamespace:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.@namespace));
				break;
			case ImportTargetKind.ImportNamespaceInAssembly:
				signature.WriteCompressedUInt32(target.reference.MetadataToken.RID);
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.@namespace));
				break;
			case ImportTargetKind.ImportType:
				signature.WriteTypeToken(target.type);
				break;
			case ImportTargetKind.ImportXmlNamespaceWithAlias:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.@namespace));
				break;
			case ImportTargetKind.ImportAlias:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.alias));
				break;
			case ImportTargetKind.DefineAssemblyAlias:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(target.reference.MetadataToken.RID);
				break;
			case ImportTargetKind.DefineNamespaceAlias:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.@namespace));
				break;
			case ImportTargetKind.DefineNamespaceInAssemblyAlias:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.alias));
				signature.WriteCompressedUInt32(target.reference.MetadataToken.RID);
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.@namespace));
				break;
			case ImportTargetKind.DefineTypeAlias:
				signature.WriteCompressedUInt32(GetUTF8StringBlobIndex(target.alias));
				signature.WriteTypeToken(target.type);
				break;
			}
		}

		private uint GetUTF8StringBlobIndex(string s)
		{
			return GetBlobIndex(Encoding.UTF8.GetBytes(s));
		}

		public MetadataToken GetDocumentToken(Document document)
		{
			if (document_map.TryGetValue(document.Url, out var value))
			{
				return value;
			}
			value = (document.token = new MetadataToken(TokenType.Document, document_table.AddRow(new Row<uint, uint, uint, uint>(GetBlobIndex(GetDocumentNameSignature(document)), GetGuidIndex(document.HashAlgorithm.ToGuid()), GetBlobIndex(document.Hash), GetGuidIndex(document.Language.ToGuid())))));
			AddCustomDebugInformations(document);
			document_map.Add(document.Url, value);
			return value;
		}

		private SignatureWriter GetDocumentNameSignature(Document document)
		{
			string url = document.Url;
			SignatureWriter signatureWriter = CreateSignatureWriter();
			if (!TryGetDocumentNameSeparator(url, out var separator))
			{
				signatureWriter.WriteByte(0);
				signatureWriter.WriteCompressedUInt32(GetUTF8StringBlobIndex(url));
				return signatureWriter;
			}
			signatureWriter.WriteByte((byte)separator);
			string[] array = url.Split(new char[1] { separator });
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] == string.Empty)
				{
					signatureWriter.WriteCompressedUInt32(0u);
				}
				else
				{
					signatureWriter.WriteCompressedUInt32(GetUTF8StringBlobIndex(array[i]));
				}
			}
			return signatureWriter;
		}

		private static bool TryGetDocumentNameSeparator(string path, out char separator)
		{
			separator = '\0';
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < path.Length; i++)
			{
				if (path[i] == '/')
				{
					num++;
				}
				else if (path[i] == '\\')
				{
					num2++;
				}
			}
			if (num == 0 && num2 == 0)
			{
				return false;
			}
			if (num >= num2)
			{
				separator = '/';
				return true;
			}
			separator = '\\';
			return true;
		}

		private void AddSequencePoints(MethodDebugInformation info)
		{
			uint rID = info.Method.MetadataToken.RID;
			if (info.TryGetUniqueDocument(out var document))
			{
				method_debug_information_table.rows[rID - 1].Col1 = GetDocumentToken(document).RID;
			}
			SignatureWriter signatureWriter = CreateSignatureWriter();
			signatureWriter.WriteSequencePoints(info);
			method_debug_information_table.rows[rID - 1].Col2 = GetBlobIndex(signatureWriter);
		}
	}
	internal sealed class SignatureWriter : ByteBuffer
	{
		private readonly MetadataBuilder metadata;

		public SignatureWriter(MetadataBuilder metadata)
			: base(6)
		{
			this.metadata = metadata;
		}

		public void WriteElementType(ElementType element_type)
		{
			WriteByte((byte)element_type);
		}

		public void WriteUTF8String(string @string)
		{
			if (@string == null)
			{
				WriteByte(byte.MaxValue);
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(@string);
			WriteCompressedUInt32((uint)bytes.Length);
			WriteBytes(bytes);
		}

		public void WriteMethodSignature(IMethodSignature method)
		{
			byte b = (byte)method.CallingConvention;
			if (method.HasThis)
			{
				b |= 0x20;
			}
			if (method.ExplicitThis)
			{
				b |= 0x40;
			}
			int num = ((method is IGenericParameterProvider { HasGenericParameters: not false } genericParameterProvider) ? genericParameterProvider.GenericParameters.Count : 0);
			if (num > 0)
			{
				b |= 0x10;
			}
			int num2 = (method.HasParameters ? method.Parameters.Count : 0);
			WriteByte(b);
			if (num > 0)
			{
				WriteCompressedUInt32((uint)num);
			}
			WriteCompressedUInt32((uint)num2);
			WriteTypeSignature(method.ReturnType);
			if (num2 != 0)
			{
				Collection<ParameterDefinition> parameters = method.Parameters;
				for (int i = 0; i < num2; i++)
				{
					WriteTypeSignature(parameters[i].ParameterType);
				}
			}
		}

		private uint MakeTypeDefOrRefCodedRID(TypeReference type)
		{
			return CodedIndex.TypeDefOrRef.CompressMetadataToken(metadata.LookupToken(type));
		}

		public void WriteTypeToken(TypeReference type)
		{
			WriteCompressedUInt32(MakeTypeDefOrRefCodedRID(type));
		}

		public void WriteTypeSignature(TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException();
			}
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Var:
			case ElementType.MVar:
			{
				GenericParameter genericParameter = (GenericParameter)type;
				WriteElementType(etype);
				int num = genericParameter.Position;
				if (num == -1)
				{
					throw new NotSupportedException();
				}
				WriteCompressedUInt32((uint)num);
				break;
			}
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)type;
				WriteElementType(ElementType.GenericInst);
				WriteElementType(genericInstanceType.IsValueType ? ElementType.ValueType : ElementType.Class);
				WriteCompressedUInt32(MakeTypeDefOrRefCodedRID(genericInstanceType.ElementType));
				WriteGenericInstanceSignature(genericInstanceType);
				break;
			}
			case ElementType.Ptr:
			case ElementType.ByRef:
			case ElementType.Sentinel:
			case ElementType.Pinned:
			{
				TypeSpecification typeSpecification = (TypeSpecification)type;
				WriteElementType(etype);
				WriteTypeSignature(typeSpecification.ElementType);
				break;
			}
			case ElementType.FnPtr:
			{
				FunctionPointerType method = (FunctionPointerType)type;
				WriteElementType(ElementType.FnPtr);
				WriteMethodSignature(method);
				break;
			}
			case ElementType.CModReqD:
			case ElementType.CModOpt:
			{
				IModifierType type2 = (IModifierType)type;
				WriteModifierSignature(etype, type2);
				break;
			}
			case ElementType.Array:
			{
				ArrayType arrayType = (ArrayType)type;
				if (!arrayType.IsVector)
				{
					WriteArrayTypeSignature(arrayType);
					break;
				}
				WriteElementType(ElementType.SzArray);
				WriteTypeSignature(arrayType.ElementType);
				break;
			}
			case ElementType.None:
				WriteElementType(type.IsValueType ? ElementType.ValueType : ElementType.Class);
				WriteCompressedUInt32(MakeTypeDefOrRefCodedRID(type));
				break;
			default:
				if (!TryWriteElementType(type))
				{
					throw new NotSupportedException();
				}
				break;
			}
		}

		private void WriteArrayTypeSignature(ArrayType array)
		{
			WriteElementType(ElementType.Array);
			WriteTypeSignature(array.ElementType);
			Collection<ArrayDimension> dimensions = array.Dimensions;
			int count = dimensions.Count;
			WriteCompressedUInt32((uint)count);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < count; i++)
			{
				ArrayDimension arrayDimension = dimensions[i];
				if (arrayDimension.UpperBound.HasValue)
				{
					num++;
					num2++;
				}
				else if (arrayDimension.LowerBound.HasValue)
				{
					num2++;
				}
			}
			int[] array2 = new int[num];
			int[] array3 = new int[num2];
			for (int j = 0; j < num2; j++)
			{
				ArrayDimension arrayDimension2 = dimensions[j];
				array3[j] = arrayDimension2.LowerBound.GetValueOrDefault();
				if (arrayDimension2.UpperBound.HasValue)
				{
					array2[j] = arrayDimension2.UpperBound.Value - array3[j] + 1;
				}
			}
			WriteCompressedUInt32((uint)num);
			for (int k = 0; k < num; k++)
			{
				WriteCompressedUInt32((uint)array2[k]);
			}
			WriteCompressedUInt32((uint)num2);
			for (int l = 0; l < num2; l++)
			{
				WriteCompressedInt32(array3[l]);
			}
		}

		public void WriteGenericInstanceSignature(IGenericInstance instance)
		{
			Collection<TypeReference> genericArguments = instance.GenericArguments;
			int count = genericArguments.Count;
			WriteCompressedUInt32((uint)count);
			for (int i = 0; i < count; i++)
			{
				WriteTypeSignature(genericArguments[i]);
			}
		}

		private void WriteModifierSignature(ElementType element_type, IModifierType type)
		{
			WriteElementType(element_type);
			WriteCompressedUInt32(MakeTypeDefOrRefCodedRID(type.ModifierType));
			WriteTypeSignature(type.ElementType);
		}

		private bool TryWriteElementType(TypeReference type)
		{
			ElementType etype = type.etype;
			if (etype == ElementType.None)
			{
				return false;
			}
			WriteElementType(etype);
			return true;
		}

		public void WriteConstantString(string value)
		{
			if (value != null)
			{
				WriteBytes(Encoding.Unicode.GetBytes(value));
			}
			else
			{
				WriteByte(byte.MaxValue);
			}
		}

		public void WriteConstantPrimitive(object value)
		{
			WritePrimitiveValue(value);
		}

		public void WriteCustomAttributeConstructorArguments(CustomAttribute attribute)
		{
			if (attribute.HasConstructorArguments)
			{
				Collection<CustomAttributeArgument> constructorArguments = attribute.ConstructorArguments;
				Collection<ParameterDefinition> parameters = attribute.Constructor.Parameters;
				if (parameters.Count != constructorArguments.Count)
				{
					throw new InvalidOperationException();
				}
				for (int i = 0; i < constructorArguments.Count; i++)
				{
					TypeReference type = GenericParameterResolver.ResolveParameterTypeIfNeeded(attribute.Constructor, parameters[i]);
					WriteCustomAttributeFixedArgument(type, constructorArguments[i]);
				}
			}
		}

		private void WriteCustomAttributeFixedArgument(TypeReference type, CustomAttributeArgument argument)
		{
			if (type.IsArray)
			{
				WriteCustomAttributeFixedArrayArgument((ArrayType)type, argument);
			}
			else
			{
				WriteCustomAttributeElement(type, argument);
			}
		}

		private void WriteCustomAttributeFixedArrayArgument(ArrayType type, CustomAttributeArgument argument)
		{
			if (!(argument.Value is CustomAttributeArgument[] array))
			{
				WriteUInt32(uint.MaxValue);
				return;
			}
			WriteInt32(array.Length);
			if (array.Length != 0)
			{
				TypeReference elementType = type.ElementType;
				for (int i = 0; i < array.Length; i++)
				{
					WriteCustomAttributeElement(elementType, array[i]);
				}
			}
		}

		private void WriteCustomAttributeElement(TypeReference type, CustomAttributeArgument argument)
		{
			if (type.IsArray)
			{
				WriteCustomAttributeFixedArrayArgument((ArrayType)type, argument);
			}
			else if (type.etype == ElementType.Object)
			{
				argument = (CustomAttributeArgument)argument.Value;
				type = argument.Type;
				WriteCustomAttributeFieldOrPropType(type);
				WriteCustomAttributeElement(type, argument);
			}
			else
			{
				WriteCustomAttributeValue(type, argument.Value);
			}
		}

		private void WriteCustomAttributeValue(TypeReference type, object value)
		{
			switch (type.etype)
			{
			case ElementType.String:
			{
				string text = (string)value;
				if (text == null)
				{
					WriteByte(byte.MaxValue);
				}
				else
				{
					WriteUTF8String(text);
				}
				break;
			}
			case ElementType.None:
				if (type.IsTypeOf("System", "Type"))
				{
					WriteCustomAttributeTypeValue((TypeReference)value);
				}
				else
				{
					WriteCustomAttributeEnumValue(type, value);
				}
				break;
			case ElementType.GenericInst:
				WriteCustomAttributeEnumValue(type, value);
				break;
			default:
				WritePrimitiveValue(value);
				break;
			}
		}

		private void WriteCustomAttributeTypeValue(TypeReference value)
		{
			if (value is TypeDefinition typeDefinition)
			{
				TypeDefinition typeDefinition2 = typeDefinition;
				while (typeDefinition2.DeclaringType != null)
				{
					typeDefinition2 = typeDefinition2.DeclaringType;
				}
				if (WindowsRuntimeProjections.IsClrImplementationType(typeDefinition2))
				{
					WindowsRuntimeProjections.Project(typeDefinition2);
					WriteTypeReference(value);
					WindowsRuntimeProjections.RemoveProjection(typeDefinition2);
					return;
				}
			}
			WriteTypeReference(value);
		}

		private void WritePrimitiveValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException();
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Boolean:
				WriteByte((byte)(((bool)value) ? 1u : 0u));
				break;
			case TypeCode.Byte:
				WriteByte((byte)value);
				break;
			case TypeCode.SByte:
				WriteSByte((sbyte)value);
				break;
			case TypeCode.Int16:
				WriteInt16((short)value);
				break;
			case TypeCode.UInt16:
				WriteUInt16((ushort)value);
				break;
			case TypeCode.Char:
				WriteInt16((short)(char)value);
				break;
			case TypeCode.Int32:
				WriteInt32((int)value);
				break;
			case TypeCode.UInt32:
				WriteUInt32((uint)value);
				break;
			case TypeCode.Single:
				WriteSingle((float)value);
				break;
			case TypeCode.Int64:
				WriteInt64((long)value);
				break;
			case TypeCode.UInt64:
				WriteUInt64((ulong)value);
				break;
			case TypeCode.Double:
				WriteDouble((double)value);
				break;
			default:
				throw new NotSupportedException(value.GetType().FullName);
			}
		}

		private void WriteCustomAttributeEnumValue(TypeReference enum_type, object value)
		{
			TypeDefinition typeDefinition = enum_type.CheckedResolve();
			if (!typeDefinition.IsEnum)
			{
				throw new ArgumentException();
			}
			WriteCustomAttributeValue(typeDefinition.GetEnumUnderlyingType(), value);
		}

		private void WriteCustomAttributeFieldOrPropType(TypeReference type)
		{
			if (type.IsArray)
			{
				ArrayType arrayType = (ArrayType)type;
				WriteElementType(ElementType.SzArray);
				WriteCustomAttributeFieldOrPropType(arrayType.ElementType);
				return;
			}
			ElementType etype = type.etype;
			switch (etype)
			{
			case ElementType.Object:
				WriteElementType(ElementType.Boxed);
				break;
			case ElementType.None:
				if (type.IsTypeOf("System", "Type"))
				{
					WriteElementType(ElementType.Type);
					break;
				}
				WriteElementType(ElementType.Enum);
				WriteTypeReference(type);
				break;
			case ElementType.GenericInst:
				WriteElementType(ElementType.Enum);
				WriteTypeReference(type);
				break;
			default:
				WriteElementType(etype);
				break;
			}
		}

		public void WriteCustomAttributeNamedArguments(CustomAttribute attribute)
		{
			int namedArgumentCount = GetNamedArgumentCount(attribute);
			WriteUInt16((ushort)namedArgumentCount);
			if (namedArgumentCount != 0)
			{
				WriteICustomAttributeNamedArguments(attribute);
			}
		}

		private static int GetNamedArgumentCount(ICustomAttribute attribute)
		{
			int num = 0;
			if (attribute.HasFields)
			{
				num += attribute.Fields.Count;
			}
			if (attribute.HasProperties)
			{
				num += attribute.Properties.Count;
			}
			return num;
		}

		private void WriteICustomAttributeNamedArguments(ICustomAttribute attribute)
		{
			if (attribute.HasFields)
			{
				WriteCustomAttributeNamedArguments(83, attribute.Fields);
			}
			if (attribute.HasProperties)
			{
				WriteCustomAttributeNamedArguments(84, attribute.Properties);
			}
		}

		private void WriteCustomAttributeNamedArguments(byte kind, Collection<CustomAttributeNamedArgument> named_arguments)
		{
			for (int i = 0; i < named_arguments.Count; i++)
			{
				WriteCustomAttributeNamedArgument(kind, named_arguments[i]);
			}
		}

		private void WriteCustomAttributeNamedArgument(byte kind, CustomAttributeNamedArgument named_argument)
		{
			CustomAttributeArgument argument = named_argument.Argument;
			WriteByte(kind);
			WriteCustomAttributeFieldOrPropType(argument.Type);
			WriteUTF8String(named_argument.Name);
			WriteCustomAttributeFixedArgument(argument.Type, argument);
		}

		private void WriteSecurityAttribute(SecurityAttribute attribute)
		{
			WriteTypeReference(attribute.AttributeType);
			int namedArgumentCount = GetNamedArgumentCount(attribute);
			if (namedArgumentCount == 0)
			{
				WriteCompressedUInt32(1u);
				WriteCompressedUInt32(0u);
				return;
			}
			SignatureWriter signatureWriter = new SignatureWriter(metadata);
			signatureWriter.WriteCompressedUInt32((uint)namedArgumentCount);
			signatureWriter.WriteICustomAttributeNamedArguments(attribute);
			WriteCompressedUInt32((uint)signatureWriter.length);
			WriteBytes(signatureWriter);
		}

		public void WriteSecurityDeclaration(SecurityDeclaration declaration)
		{
			WriteByte(46);
			Collection<SecurityAttribute> security_attributes = declaration.security_attributes;
			if (security_attributes == null)
			{
				throw new NotSupportedException();
			}
			WriteCompressedUInt32((uint)security_attributes.Count);
			for (int i = 0; i < security_attributes.Count; i++)
			{
				WriteSecurityAttribute(security_attributes[i]);
			}
		}

		public void WriteXmlSecurityDeclaration(SecurityDeclaration declaration)
		{
			string xmlSecurityDeclaration = GetXmlSecurityDeclaration(declaration);
			if (xmlSecurityDeclaration == null)
			{
				throw new NotSupportedException();
			}
			WriteBytes(Encoding.Unicode.GetBytes(xmlSecurityDeclaration));
		}

		private static string GetXmlSecurityDeclaration(SecurityDeclaration declaration)
		{
			if (declaration.security_attributes == null || declaration.security_attributes.Count != 1)
			{
				return null;
			}
			SecurityAttribute securityAttribute = declaration.security_attributes[0];
			if (!securityAttribute.AttributeType.IsTypeOf("System.Security.Permissions", "PermissionSetAttribute"))
			{
				return null;
			}
			if (securityAttribute.properties == null || securityAttribute.properties.Count != 1)
			{
				return null;
			}
			CustomAttributeNamedArgument customAttributeNamedArgument = securityAttribute.properties[0];
			if (customAttributeNamedArgument.Name != "XML")
			{
				return null;
			}
			return (string)customAttributeNamedArgument.Argument.Value;
		}

		private void WriteTypeReference(TypeReference type)
		{
			WriteUTF8String(TypeParser.ToParseable(type, top_level: false));
		}

		public void WriteMarshalInfo(MarshalInfo marshal_info)
		{
			WriteNativeType(marshal_info.native);
			switch (marshal_info.native)
			{
			case NativeType.Array:
			{
				ArrayMarshalInfo arrayMarshalInfo = (ArrayMarshalInfo)marshal_info;
				if (arrayMarshalInfo.element_type != NativeType.None)
				{
					WriteNativeType(arrayMarshalInfo.element_type);
				}
				if (arrayMarshalInfo.size_parameter_index > -1)
				{
					WriteCompressedUInt32((uint)arrayMarshalInfo.size_parameter_index);
				}
				if (arrayMarshalInfo.size > -1)
				{
					WriteCompressedUInt32((uint)arrayMarshalInfo.size);
				}
				if (arrayMarshalInfo.size_parameter_multiplier > -1)
				{
					WriteCompressedUInt32((uint)arrayMarshalInfo.size_parameter_multiplier);
				}
				break;
			}
			case NativeType.SafeArray:
			{
				SafeArrayMarshalInfo safeArrayMarshalInfo = (SafeArrayMarshalInfo)marshal_info;
				if (safeArrayMarshalInfo.element_type != VariantType.None)
				{
					WriteVariantType(safeArrayMarshalInfo.element_type);
				}
				break;
			}
			case NativeType.FixedArray:
			{
				FixedArrayMarshalInfo fixedArrayMarshalInfo = (FixedArrayMarshalInfo)marshal_info;
				if (fixedArrayMarshalInfo.size > -1)
				{
					WriteCompressedUInt32((uint)fixedArrayMarshalInfo.size);
				}
				if (fixedArrayMarshalInfo.element_type != NativeType.None)
				{
					WriteNativeType(fixedArrayMarshalInfo.element_type);
				}
				break;
			}
			case NativeType.FixedSysString:
			{
				FixedSysStringMarshalInfo fixedSysStringMarshalInfo = (FixedSysStringMarshalInfo)marshal_info;
				if (fixedSysStringMarshalInfo.size > -1)
				{
					WriteCompressedUInt32((uint)fixedSysStringMarshalInfo.size);
				}
				break;
			}
			case NativeType.CustomMarshaler:
			{
				CustomMarshalInfo customMarshalInfo = (CustomMarshalInfo)marshal_info;
				WriteUTF8String((customMarshalInfo.guid != Guid.Empty) ? customMarshalInfo.guid.ToString() : string.Empty);
				WriteUTF8String(customMarshalInfo.unmanaged_type);
				WriteTypeReference(customMarshalInfo.managed_type);
				WriteUTF8String(customMarshalInfo.cookie);
				break;
			}
			}
		}

		private void WriteNativeType(NativeType native)
		{
			WriteByte((byte)native);
		}

		private void WriteVariantType(VariantType variant)
		{
			WriteByte((byte)variant);
		}

		public void WriteSequencePoints(MethodDebugInformation info)
		{
			int num = -1;
			int num2 = -1;
			WriteCompressedUInt32(info.local_var_token.RID);
			if (!info.TryGetUniqueDocument(out var document))
			{
				document = null;
			}
			for (int i = 0; i < info.SequencePoints.Count; i++)
			{
				SequencePoint sequencePoint = info.SequencePoints[i];
				Document document2 = sequencePoint.Document;
				if (document != document2)
				{
					MetadataToken documentToken = metadata.GetDocumentToken(document2);
					if (document != null)
					{
						WriteCompressedUInt32(0u);
					}
					WriteCompressedUInt32(documentToken.RID);
					document = document2;
				}
				if (i > 0)
				{
					WriteCompressedUInt32((uint)(sequencePoint.Offset - info.SequencePoints[i - 1].Offset));
				}
				else
				{
					WriteCompressedUInt32((uint)sequencePoint.Offset);
				}
				if (sequencePoint.IsHidden)
				{
					WriteInt16(0);
					continue;
				}
				int num3 = sequencePoint.EndLine - sequencePoint.StartLine;
				int value = sequencePoint.EndColumn - sequencePoint.StartColumn;
				WriteCompressedUInt32((uint)num3);
				if (num3 == 0)
				{
					WriteCompressedUInt32((uint)value);
				}
				else
				{
					WriteCompressedInt32(value);
				}
				if (num < 0)
				{
					WriteCompressedUInt32((uint)sequencePoint.StartLine);
					WriteCompressedUInt32((uint)sequencePoint.StartColumn);
				}
				else
				{
					WriteCompressedInt32(sequencePoint.StartLine - num);
					WriteCompressedInt32(sequencePoint.StartColumn - num2);
				}
				num = sequencePoint.StartLine;
				num2 = sequencePoint.StartColumn;
			}
		}
	}
	public delegate AssemblyDefinition AssemblyResolveEventHandler(object sender, AssemblyNameReference reference);
	public sealed class AssemblyResolveEventArgs : EventArgs
	{
		private readonly AssemblyNameReference reference;

		public AssemblyNameReference AssemblyReference => reference;

		public AssemblyResolveEventArgs(AssemblyNameReference reference)
		{
			this.reference = reference;
		}
	}
	[Serializable]
	public sealed class AssemblyResolutionException : FileNotFoundException
	{
		private readonly AssemblyNameReference reference;

		public AssemblyNameReference AssemblyReference => reference;

		public AssemblyResolutionException(AssemblyNameReference reference)
			: this(reference, null)
		{
		}

		public AssemblyResolutionException(AssemblyNameReference reference, Exception innerException)
			: base($"Failed to resolve assembly: '{reference}'", innerException)
		{
			this.reference = reference;
		}

		private AssemblyResolutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public abstract class BaseAssemblyResolver : IAssemblyResolver, IDisposable
	{
		private static readonly bool on_mono = Type.GetType("Mono.Runtime") != null;

		private readonly Collection<string> directories;

		private Collection<string> gac_paths;

		public event AssemblyResolveEventHandler ResolveFailure;

		public void AddSearchDirectory(string directory)
		{
			directories.Add(directory);
		}

		public void RemoveSearchDirectory(string directory)
		{
			directories.Remove(directory);
		}

		public string[] GetSearchDirectories()
		{
			string[] array = new string[directories.size];
			Array.Copy(directories.items, array, array.Length);
			return array;
		}

		protected BaseAssemblyResolver()
		{
			directories = new Collection<string>(2) { ".", "bin" };
		}

		private AssemblyDefinition GetAssembly(string file, ReaderParameters parameters)
		{
			if (parameters.AssemblyResolver == null)
			{
				parameters.AssemblyResolver = this;
			}
			return ModuleDefinition.ReadModule(file, parameters).Assembly;
		}

		public virtual AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			return Resolve(name, new ReaderParameters());
		}

		public virtual AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters)
		{
			Mixin.CheckName(name);
			Mixin.CheckParameters(parameters);
			AssemblyDefinition assemblyDefinition = SearchDirectory(name, directories, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			if (name.IsRetargetable)
			{
				name = new AssemblyNameReference(name.Name, Mixin.ZeroVersion)
				{
					PublicKeyToken = Empty<byte>.Array
				};
			}
			string directoryName = Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName);
			string[] array = ((!on_mono) ? new string[1] { directoryName } : new string[2]
			{
				directoryName,
				Path.Combine(directoryName, "Facades")
			});
			if (IsZero(name.Version))
			{
				assemblyDefinition = SearchDirectory(name, array, parameters);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			if (name.Name == "mscorlib")
			{
				assemblyDefinition = GetCorlib(name, parameters);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			assemblyDefinition = GetAssemblyInGac(name, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			assemblyDefinition = SearchDirectory(name, array, parameters);
			if (assemblyDefinition != null)
			{
				return assemblyDefinition;
			}
			if (this.ResolveFailure != null)
			{
				assemblyDefinition = this.ResolveFailure(this, name);
				if (assemblyDefinition != null)
				{
					return assemblyDefinition;
				}
			}
			throw new AssemblyResolutionException(name);
		}

		protected virtual AssemblyDefinition SearchDirectory(AssemblyNameReference name, IEnumerable<string> directories, ReaderParameters parameters)
		{
			string[] array = ((!name.IsWindowsRuntime) ? new string[2] { ".dll", ".exe" } : new string[2] { ".winmd", ".dll" });
			foreach (string directory in directories)
			{
				string[] array2 = array;
				foreach (string text in array2)
				{
					string text2 = Path.Combine(directory, name.Name + text);
					if (File.Exists(text2))
					{
						try
						{
							return GetAssembly(text2, parameters);
						}
						catch (BadImageFormatException)
						{
						}
					}
				}
			}
			return null;
		}

		private static bool IsZero(Version version)
		{
			return version.Major == 0 && version.Minor == 0 && version.Build == 0 && version.Revision == 0;
		}

		private AssemblyDefinition GetCorlib(AssemblyNameReference reference, ReaderParameters parameters)
		{
			Version version = reference.Version;
			AssemblyName name = typeof(object).Assembly.GetName();
			if (name.Version == version || IsZero(version))
			{
				return GetAssembly(typeof(object).Module.FullyQualifiedName, parameters);
			}
			string fullName = Directory.GetParent(Directory.GetParent(typeof(object).Module.FullyQualifiedName).FullName).FullName;
			if (!on_mono)
			{
				fullName = version.Major switch
				{
					1 => (version.MajorRevision != 3300) ? Path.Combine(fullName, "v1.1.4322") : Path.Combine(fullName, "v1.0.3705"), 
					2 => Path.Combine(fullName, "v2.0.50727"), 
					4 => Path.Combine(fullName, "v4.0.30319"), 
					_ => throw new NotSupportedException("Version not supported: " + version), 
				};
			}
			else if (version.Major == 1)
			{
				fullName = Path.Combine(fullName, "1.0");
			}
			else if (version.Major == 2)
			{
				fullName = ((version.MajorRevision != 5) ? Path.Combine(fullName, "2.0") : Path.Combine(fullName, "2.1"));
			}
			else
			{
				if (version.Major != 4)
				{
					throw new NotSupportedException("Version not supported: " + version);
				}
				fullName = Path.Combine(fullName, "4.0");
			}
			string text = Path.Combine(fullName, "mscorlib.dll");
			if (File.Exists(text))
			{
				return GetAssembly(text, parameters);
			}
			if (on_mono && Directory.Exists(fullName + "-api"))
			{
				text = Path.Combine(fullName + "-api", "mscorlib.dll");
				if (File.Exists(text))
				{
					return GetAssembly(text, parameters);
				}
			}
			return null;
		}

		private static Collection<string> GetGacPaths()
		{
			if (on_mono)
			{
				return GetDefaultMonoGacPaths();
			}
			Collection<string> collection = new Collection<string>(2);
			string environmentVariable = Environment.GetEnvironmentVariable("WINDIR");
			if (environmentVariable == null)
			{
				return collection;
			}
			collection.Add(Path.Combine(environmentVariable, "assembly"));
			collection.Add(Path.Combine(environmentVariable, Path.Combine("Microsoft.NET", "assembly")));
			return collection;
		}

		private static Collection<string> GetDefaultMonoGacPaths()
		{
			Collection<string> collection = new Collection<string>(1);
			string currentMonoGac = GetCurrentMonoGac();
			if (currentMonoGac != null)
			{
				collection.Add(currentMonoGac);
			}
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_GAC_PREFIX");
			if (string.IsNullOrEmpty(environmentVariable))
			{
				return collection;
			}
			string[] array = environmentVariable.Split(new char[1] { Path.PathSeparator });
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string text2 = Path.Combine(Path.Combine(Path.Combine(text, "lib"), "mono"), "gac");
					if (Directory.Exists(text2) && !collection.Contains(currentMonoGac))
					{
						collection.Add(text2);
					}
				}
			}
			return collection;
		}

		private static string GetCurrentMonoGac()
		{
			return Path.Combine(Directory.GetParent(Path.GetDirectoryName(typeof(object).Module.FullyQualifiedName)).FullName, "gac");
		}

		private AssemblyDefinition GetAssemblyInGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			if (reference.PublicKeyToken == null || reference.PublicKeyToken.Length == 0)
			{
				return null;
			}
			if (gac_paths == null)
			{
				gac_paths = GetGacPaths();
			}
			if (on_mono)
			{
				return GetAssemblyInMonoGac(reference, parameters);
			}
			return GetAssemblyInNetGac(reference, parameters);
		}

		private AssemblyDefinition GetAssemblyInMonoGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			for (int i = 0; i < gac_paths.Count; i++)
			{
				string gac = gac_paths[i];
				string assemblyFile = GetAssemblyFile(reference, string.Empty, gac);
				if (File.Exists(assemblyFile))
				{
					return GetAssembly(assemblyFile, parameters);
				}
			}
			return null;
		}

		private AssemblyDefinition GetAssemblyInNetGac(AssemblyNameReference reference, ReaderParameters parameters)
		{
			string[] array = new string[4] { "GAC_MSIL", "GAC_32", "GAC_64", "GAC" };
			string[] array2 = new string[2]
			{
				string.Empty,
				"v4.0_"
			};
			for (int i = 0; i < gac_paths.Count; i++)
			{
				for (int j = 0; j < array.Length; j++)
				{
					string text = Path.Combine(gac_paths[i], array[j]);
					string assemblyFile = GetAssemblyFile(reference, array2[i], text);
					if (Directory.Exists(text) && File.Exists(assemblyFile))
					{
						return GetAssembly(assemblyFile, parameters);
					}
				}
			}
			return null;
		}

		private static string GetAssemblyFile(AssemblyNameReference reference, string prefix, string gac)
		{
			StringBuilder stringBuilder = new StringBuilder().Append(prefix).Append(reference.Version).Append("__");
			for (int i = 0; i < reference.PublicKeyToken.Length; i++)
			{
				stringBuilder.Append(reference.PublicKeyToken[i].ToString("x2"));
			}
			return Path.Combine(Path.Combine(Path.Combine(gac, reference.Name), stringBuilder.ToString()), reference.Name + ".dll");
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
		}
	}
	public sealed class CallSite : IMethodSignature, IMetadataTokenProvider
	{
		private readonly MethodReference signature;

		public bool HasThis
		{
			get
			{
				return signature.HasThis;
			}
			set
			{
				signature.HasThis = value;
			}
		}

		public bool ExplicitThis
		{
			get
			{
				return signature.ExplicitThis;
			}
			set
			{
				signature.ExplicitThis = value;
			}
		}

		public MethodCallingConvention CallingConvention
		{
			get
			{
				return signature.CallingConvention;
			}
			set
			{
				signature.CallingConvention = value;
			}
		}

		public bool HasParameters => signature.HasParameters;

		public Collection<ParameterDefinition> Parameters => signature.Parameters;

		public TypeReference ReturnType
		{
			get
			{
				return signature.MethodReturnType.ReturnType;
			}
			set
			{
				signature.MethodReturnType.ReturnType = value;
			}
		}

		public MethodReturnType MethodReturnType => signature.MethodReturnType;

		public string Name
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public ModuleDefinition Module => ReturnType.Module;

		public IMetadataScope Scope => signature.ReturnType.Scope;

		public MetadataToken MetadataToken
		{
			get
			{
				return signature.token;
			}
			set
			{
				signature.token = value;
			}
		}

		public string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(ReturnType.FullName);
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		internal CallSite()
		{
			signature = new MethodReference();
			signature.token = new MetadataToken(TokenType.Signature, 0);
		}

		public CallSite(TypeReference returnType)
			: this()
		{
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			signature.ReturnType = returnType;
		}

		public override string ToString()
		{
			return FullName;
		}
	}
	public struct CustomAttributeArgument
	{
		private readonly TypeReference type;

		private readonly object value;

		public TypeReference Type => type;

		public object Value => value;

		public CustomAttributeArgument(TypeReference type, object value)
		{
			Mixin.CheckType(type);
			this.type = type;
			this.value = value;
		}
	}
	public struct CustomAttributeNamedArgument
	{
		private readonly string name;

		private readonly CustomAttributeArgument argument;

		public string Name => name;

		public CustomAttributeArgument Argument => argument;

		public CustomAttributeNamedArgument(string name, CustomAttributeArgument argument)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.argument = argument;
		}
	}
	public interface ICustomAttribute
	{
		TypeReference AttributeType { get; }

		bool HasFields { get; }

		bool HasProperties { get; }

		bool HasConstructorArguments { get; }

		Collection<CustomAttributeNamedArgument> Fields { get; }

		Collection<CustomAttributeNamedArgument> Properties { get; }

		Collection<CustomAttributeArgument> ConstructorArguments { get; }
	}
	[DebuggerDisplay("{AttributeType}")]
	public sealed class CustomAttribute : ICustomAttribute
	{
		internal CustomAttributeValueProjection projection;

		internal readonly uint signature;

		internal bool resolved;

		private MethodReference constructor;

		private byte[] blob;

		internal Collection<CustomAttributeArgument> arguments;

		internal Collection<CustomAttributeNamedArgument> fields;

		internal Collection<CustomAttributeNamedArgument> properties;

		public MethodReference Constructor
		{
			get
			{
				return constructor;
			}
			set
			{
				constructor = value;
			}
		}

		public TypeReference AttributeType => constructor.DeclaringType;

		public bool IsResolved => resolved;

		public bool HasConstructorArguments
		{
			get
			{
				Resolve();
				return !arguments.IsNullOrEmpty();
			}
		}

		public Collection<CustomAttributeArgument> ConstructorArguments
		{
			get
			{
				Resolve();
				if (arguments == null)
				{
					Interlocked.CompareExchange(ref arguments, new Collection<CustomAttributeArgument>(), null);
				}
				return arguments;
			}
		}

		public bool HasFields
		{
			get
			{
				Resolve();
				return !fields.IsNullOrEmpty();
			}
		}

		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				Resolve();
				if (fields == null)
				{
					Interlocked.CompareExchange(ref fields, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return fields;
			}
		}

		public bool HasProperties
		{
			get
			{
				Resolve();
				return !properties.IsNullOrEmpty();
			}
		}

		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				Resolve();
				if (properties == null)
				{
					Interlocked.CompareExchange(ref properties, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return properties;
			}
		}

		internal bool HasImage => constructor != null && constructor.HasImage;

		internal ModuleDefinition Module => constructor.Module;

		internal CustomAttribute(uint signature, MethodReference constructor)
		{
			this.signature = signature;
			this.constructor = constructor;
			resolved = false;
		}

		public CustomAttribute(MethodReference constructor)
		{
			this.constructor = constructor;
			resolved = true;
		}

		public CustomAttribute(MethodReference constructor, byte[] blob)
		{
			this.constructor = constructor;
			resolved = false;
			this.blob = blob;
		}

		public byte[] GetBlob()
		{
			if (blob != null)
			{
				return blob;
			}
			if (!HasImage)
			{
				throw new NotSupportedException();
			}
			return Module.Read(ref blob, this, (CustomAttribute attribute, MetadataReader reader) => reader.ReadCustomAttributeBlob(attribute.signature));
		}

		private void Resolve()
		{
			if (resolved || !HasImage)
			{
				return;
			}
			lock (Module.SyncRoot)
			{
				if (resolved)
				{
					return;
				}
				Module.Read(this, delegate(CustomAttribute attribute, MetadataReader reader)
				{
					try
					{
						reader.ReadCustomAttributeSignature(attribute);
						resolved = true;
					}
					catch (ResolutionException)
					{
						if (arguments != null)
						{
							arguments.Clear();
						}
						if (fields != null)
						{
							fields.Clear();
						}
						if (properties != null)
						{
							properties.Clear();
						}
						resolved = false;
					}
				});
			}
		}
	}
	public class DefaultAssemblyResolver : BaseAssemblyResolver
	{
		private readonly IDictionary<string, AssemblyDefinition> cache;

		public DefaultAssemblyResolver()
		{
			cache = new Dictionary<string, AssemblyDefinition>(StringComparer.Ordinal);
		}

		public override AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			Mixin.CheckName(name);
			if (cache.TryGetValue(name.FullName, out var value))
			{
				return value;
			}
			value = base.Resolve(name);
			cache[name.FullName] = value;
			return value;
		}

		protected void RegisterAssembly(AssemblyDefinition assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			string fullName = assembly.Name.FullName;
			if (!cache.ContainsKey(fullName))
			{
				cache[fullName] = assembly;
			}
		}

		protected override void Dispose(bool disposing)
		{
			foreach (AssemblyDefinition value in cache.Values)
			{
				value.Dispose();
			}
			cache.Clear();
			base.Dispose(disposing);
		}
	}
	public sealed class EmbeddedResource : Resource
	{
		private readonly MetadataReader reader;

		private uint? offset;

		private byte[] data;

		private Stream stream;

		public override ResourceType ResourceType => ResourceType.Embedded;

		public EmbeddedResource(string name, ManifestResourceAttributes attributes, byte[] data)
			: base(name, attributes)
		{
			this.data = data;
		}

		public EmbeddedResource(string name, ManifestResourceAttributes attributes, Stream stream)
			: base(name, attributes)
		{
			this.stream = stream;
		}

		internal EmbeddedResource(string name, ManifestResourceAttributes attributes, uint offset, MetadataReader reader)
			: base(name, attributes)
		{
			this.offset = offset;
			this.reader = reader;
		}

		public Stream GetResourceStream()
		{
			if (stream != null)
			{
				return stream;
			}
			if (data != null)
			{
				return new MemoryStream(data);
			}
			if (offset.HasValue)
			{
				return new MemoryStream(reader.GetManagedResource(offset.Value));
			}
			throw new InvalidOperationException();
		}

		public byte[] GetResourceData()
		{
			if (stream != null)
			{
				return ReadStream(stream);
			}
			if (data != null)
			{
				return data;
			}
			if (offset.HasValue)
			{
				return reader.GetManagedResource(offset.Value);
			}
			throw new InvalidOperationException();
		}

		private static byte[] ReadStream(Stream stream)
		{
			int num3;
			if (stream.CanSeek)
			{
				int num = (int)stream.Length;
				byte[] array = new byte[num];
				int num2 = 0;
				while ((num3 = stream.Read(array, num2, num - num2)) > 0)
				{
					num2 += num3;
				}
				return array;
			}
			byte[] array2 = new byte[8192];
			MemoryStream memoryStream = new MemoryStream();
			while ((num3 = stream.Read(array2, 0, array2.Length)) > 0)
			{
				memoryStream.Write(array2, 0, num3);
			}
			return memoryStream.ToArray();
		}
	}
	[Flags]
	public enum EventAttributes : ushort
	{
		None = 0,
		SpecialName = 0x200,
		RTSpecialName = 0x400
	}
	public sealed class EventDefinition : EventReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider
	{
		private ushort attributes;

		private Collection<CustomAttribute> custom_attributes;

		internal MethodDefinition add_method;

		internal MethodDefinition invoke_method;

		internal MethodDefinition remove_method;

		internal Collection<MethodDefinition> other_methods;

		public EventAttributes Attributes
		{
			get
			{
				return (EventAttributes)attributes;
			}
			set
			{
				attributes = (ushort)value;
			}
		}

		public MethodDefinition AddMethod
		{
			get
			{
				if (add_method != null)
				{
					return add_method;
				}
				InitializeMethods();
				return add_method;
			}
			set
			{
				add_method = value;
			}
		}

		public MethodDefinition InvokeMethod
		{
			get
			{
				if (invoke_method != null)
				{
					return invoke_method;
				}
				InitializeMethods();
				return invoke_method;
			}
			set
			{
				invoke_method = value;
			}
		}

		public MethodDefinition RemoveMethod
		{
			get
			{
				if (remove_method != null)
				{
					return remove_method;
				}
				InitializeMethods();
				return remove_method;
			}
			set
			{
				remove_method = value;
			}
		}

		public bool HasOtherMethods
		{
			get
			{
				if (other_methods != null)
				{
					return other_methods.Count > 0;
				}
				InitializeMethods();
				return !other_methods.IsNullOrEmpty();
			}
		}

		public Collection<MethodDefinition> OtherMethods
		{
			get
			{
				if (other_methods != null)
				{
					return other_methods;
				}
				InitializeMethods();
				if (other_methods == null)
				{
					Interlocked.CompareExchange(ref other_methods, new Collection<MethodDefinition>(), null);
				}
				return other_methods;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, Module);

		public bool IsSpecialName
		{
			get
			{
				return attributes.GetAttributes(512);
			}
			set
			{
				attributes = attributes.SetAttributes(512, value);
			}
		}

		public bool IsRuntimeSpecialName
		{
			get
			{
				return attributes.GetAttributes(1024);
			}
			set
			{
				attributes = attributes.SetAttributes(1024, value);
			}
		}

		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		public override bool IsDefinition => true;

		public EventDefinition(string name, EventAttributes attributes, TypeReference eventType)
			: base(name, eventType)
		{
			this.attributes = (ushort)attributes;
			token = new MetadataToken(TokenType.Event);
		}

		private void InitializeMethods()
		{
			ModuleDefinition module = Module;
			if (module == null)
			{
				return;
			}
			lock (module.SyncRoot)
			{
				if (add_method == null && invoke_method == null && remove_method == null && module.HasImage())
				{
					module.Read(this, delegate(EventDefinition @event, MetadataReader reader)
					{
						reader.ReadMethods(@event);
					});
				}
			}
		}

		public override EventDefinition Resolve()
		{
			return this;
		}
	}
	public abstract class EventReference : MemberReference
	{
		private TypeReference event_type;

		public TypeReference EventType
		{
			get
			{
				return event_type;
			}
			set
			{
				event_type = value;
			}
		}

		public override string FullName => event_type.FullName + " " + MemberFullName();

		protected EventReference(string name, TypeReference eventType)
			: base(name)
		{
			Mixin.CheckType(eventType, Mixin.Argument.eventType);
			event_type = eventType;
		}

		protected override IMemberDefinition ResolveDefinition()
		{
			return Resolve();
		}

		public new abstract EventDefinition Resolve();
	}
	public sealed class ExportedType : IMetadataTokenProvider
	{
		private string @namespace;

		private string name;

		private uint attributes;

		private IMetadataScope scope;

		private ModuleDefinition module;

		private int identifier;

		private ExportedType declaring_type;

		internal MetadataToken token;

		public string Namespace
		{
			get
			{
				return @namespace;
			}
			set
			{
				@namespace = value;
			}
		}

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)attributes;
			}
			set
			{
				attributes = (uint)value;
			}
		}

		public IMetadataScope Scope
		{
			get
			{
				if (declaring_type != null)
				{
					return declaring_type.Scope;
				}
				return scope;
			}
			set
			{
				if (declaring_type != null)
				{
					declaring_type.Scope = value;
				}
				else
				{
					scope = value;
				}
			}
		}

		public ExportedType DeclaringType
		{
			get
			{
				return declaring_type;
			}
			set
			{
				declaring_type = value;
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		public int Identifier
		{
			get
			{
				return identifier;
			}
			set
			{
				identifier = value;
			}
		}

		public bool IsNotPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 0u, value);
			}
		}

		public bool IsPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 1u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 1u, value);
			}
		}

		public bool IsNestedPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 2u, value);
			}
		}

		public bool IsNestedPrivate
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 3u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 3u, value);
			}
		}

		public bool IsNestedFamily
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 4u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 4u, value);
			}
		}

		public bool IsNestedAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 5u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 5u, value);
			}
		}

		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 6u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 6u, value);
			}
		}

		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 7u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 7u, value);
			}
		}

		public bool IsAutoLayout
		{
			get
			{
				return attributes.GetMaskedAttributes(24u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(24u, 0u, value);
			}
		}

		public bool IsSequentialLayout
		{
			get
			{
				return attributes.GetMaskedAttributes(24u, 8u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(24u, 8u, value);
			}
		}

		public bool IsExplicitLayout
		{
			get
			{
				return attributes.GetMaskedAttributes(24u, 16u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(24u, 16u, value);
			}
		}

		public bool IsClass
		{
			get
			{
				return attributes.GetMaskedAttributes(32u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(32u, 0u, value);
			}
		}

		public bool IsInterface
		{
			get
			{
				return attributes.GetMaskedAttributes(32u, 32u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(32u, 32u, value);
			}
		}

		public bool IsAbstract
		{
			get
			{
				return attributes.GetAttributes(128u);
			}
			set
			{
				attributes = attributes.SetAttributes(128u, value);
			}
		}

		public bool IsSealed
		{
			get
			{
				return attributes.GetAttributes(256u);
			}
			set
			{
				attributes = attributes.SetAttributes(256u, value);
			}
		}

		public bool IsSpecialName
		{
			get
			{
				return attributes.GetAttributes(1024u);
			}
			set
			{
				attributes = attributes.SetAttributes(1024u, value);
			}
		}

		public bool IsImport
		{
			get
			{
				return attributes.GetAttributes(4096u);
			}
			set
			{
				attributes = attributes.SetAttributes(4096u, value);
			}
		}

		public bool IsSerializable
		{
			get
			{
				return attributes.GetAttributes(8192u);
			}
			set
			{
				attributes = attributes.SetAttributes(8192u, value);
			}
		}

		public bool IsAnsiClass
		{
			get
			{
				return attributes.GetMaskedAttributes(196608u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(196608u, 0u, value);
			}
		}

		public bool IsUnicodeClass
		{
			get
			{
				return attributes.GetMaskedAttributes(196608u, 65536u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(196608u, 65536u, value);
			}
		}

		public bool IsAutoClass
		{
			get
			{
				return attributes.GetMaskedAttributes(196608u, 131072u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(196608u, 131072u, value);
			}
		}

		public bool IsBeforeFieldInit
		{
			get
			{
				return attributes.GetAttributes(1048576u);
			}
			set
			{
				attributes = attributes.SetAttributes(1048576u, value);
			}
		}

		public bool IsRuntimeSpecialName
		{
			get
			{
				return attributes.GetAttributes(2048u);
			}
			set
			{
				attributes = attributes.SetAttributes(2048u, value);
			}
		}

		public bool HasSecurity
		{
			get
			{
				return attributes.GetAttributes(262144u);
			}
			set
			{
				attributes = attributes.SetAttributes(262144u, value);
			}
		}

		public bool IsForwarder
		{
			get
			{
				return attributes.GetAttributes(2097152u);
			}
			set
			{
				attributes = attributes.SetAttributes(2097152u, value);
			}
		}

		public string FullName
		{
			get
			{
				string text = (string.IsNullOrEmpty(@namespace) ? name : (@namespace + "." + name));
				if (declaring_type != null)
				{
					return declaring_type.FullName + "/" + text;
				}
				return text;
			}
		}

		public ExportedType(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
		{
			this.@namespace = @namespace;
			this.name = name;
			this.scope = scope;
			this.module = module;
		}

		public override string ToString()
		{
			return FullName;
		}

		public TypeDefinition Resolve()
		{
			return module.Resolve(CreateReference());
		}

		internal TypeReference CreateReference()
		{
			return new TypeReference(@namespace, name, module, scope)
			{
				DeclaringType = ((declaring_type != null) ? declaring_type.CreateReference() : null)
			};
		}
	}
	[Flags]
	public enum FieldAttributes : ushort
	{
		FieldAccessMask = 7,
		CompilerControlled = 0,
		Private = 1,
		FamANDAssem = 2,
		Assembly = 3,
		Family = 4,
		FamORAssem = 5,
		Public = 6,
		Static = 0x10,
		InitOnly = 0x20,
		Literal = 0x40,
		NotSerialized = 0x80,
		SpecialName = 0x200,
		PInvokeImpl = 0x2000,
		RTSpecialName = 0x400,
		HasFieldMarshal = 0x1000,
		HasDefault = 0x8000,
		HasFieldRVA = 0x100
	}
	public sealed class FieldDefinition : FieldReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider, IMarshalInfoProvider
	{
		private ushort attributes;

		private Collection<CustomAttribute> custom_attributes;

		private int offset = -2;

		internal int rva = -2;

		private byte[] initial_value;

		private object constant = Mixin.NotResolved;

		private MarshalInfo marshal_info;

		public bool HasLayoutInfo
		{
			get
			{
				if (offset >= 0)
				{
					return true;
				}
				ResolveLayout();
				return offset >= 0;
			}
		}

		public int Offset
		{
			get
			{
				if (offset >= 0)
				{
					return offset;
				}
				ResolveLayout();
				return (offset >= 0) ? offset : (-1);
			}
			set
			{
				offset = value;
			}
		}

		internal FieldDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (FieldDefinitionProjection)projection;
			}
			set
			{
				projection = value;
			}
		}

		public int RVA
		{
			get
			{
				if (rva > 0)
				{
					return rva;
				}
				ResolveRVA();
				return (rva > 0) ? rva : 0;
			}
		}

		public byte[] InitialValue
		{
			get
			{
				if (initial_value != null)
				{
					return initial_value;
				}
				ResolveRVA();
				if (initial_value == null)
				{
					initial_value = Empty<byte>.Array;
				}
				return initial_value;
			}
			set
			{
				initial_value = value;
				HasFieldRVA = !initial_value.IsNullOrEmpty();
				rva = 0;
			}
		}

		public FieldAttributes Attributes
		{
			get
			{
				return (FieldAttributes)attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && (uint)value != attributes)
				{
					throw new InvalidOperationException();
				}
				attributes = (ushort)value;
			}
		}

		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref constant, Module);
				return constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					constant = Mixin.NoValue;
				}
			}
		}

		public object Constant
		{
			get
			{
				return HasConstant ? constant : null;
			}
			set
			{
				constant = value;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, Module);

		public bool HasMarshalInfo
		{
			get
			{
				if (marshal_info != null)
				{
					return true;
				}
				return this.GetHasMarshalInfo(Module);
			}
		}

		public MarshalInfo MarshalInfo
		{
			get
			{
				return marshal_info ?? this.GetMarshalInfo(ref marshal_info, Module);
			}
			set
			{
				marshal_info = value;
			}
		}

		public bool IsCompilerControlled
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 0u, value);
			}
		}

		public bool IsPrivate
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 1u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 1u, value);
			}
		}

		public bool IsFamilyAndAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 2u, value);
			}
		}

		public bool IsAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 3u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 3u, value);
			}
		}

		public bool IsFamily
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 4u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 4u, value);
			}
		}

		public bool IsFamilyOrAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 5u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 5u, value);
			}
		}

		public bool IsPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 6u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 6u, value);
			}
		}

		public bool IsStatic
		{
			get
			{
				return attributes.GetAttributes(16);
			}
			set
			{
				attributes = attributes.SetAttributes(16, value);
			}
		}

		public bool IsInitOnly
		{
			get
			{
				return attributes.GetAttributes(32);
			}
			set
			{
				attributes = attributes.SetAttributes(32, value);
			}
		}

		public bool IsLiteral
		{
			get
			{
				return attributes.GetAttributes(64);
			}
			set
			{
				attributes = attributes.SetAttributes(64, value);
			}
		}

		public bool IsNotSerialized
		{
			get
			{
				return attributes.GetAttributes(128);
			}
			set
			{
				attributes = attributes.SetAttributes(128, value);
			}
		}

		public bool IsSpecialName
		{
			get
			{
				return attributes.GetAttributes(512);
			}
			set
			{
				attributes = attributes.SetAttributes(512, value);
			}
		}

		public bool IsPInvokeImpl
		{
			get
			{
				return attributes.GetAttributes(8192);
			}
			set
			{
				attributes = attributes.SetAttributes(8192, value);
			}
		}

		public bool IsRuntimeSpecialName
		{
			get
			{
				return attributes.GetAttributes(1024);
			}
			set
			{
				attributes = attributes.SetAttributes(1024, value);
			}
		}

		public bool HasDefault
		{
			get
			{
				return attributes.GetAttributes(32768);
			}
			set
			{
				attributes = attributes.SetAttributes(32768, value);
			}
		}

		public bool HasFieldRVA
		{
			get
			{
				return attributes.GetAttributes(256);
			}
			set
			{
				attributes = attributes.SetAttributes(256, value);
			}
		}

		public override bool IsDefinition => true;

		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		private void ResolveLayout()
		{
			if (offset != -2)
			{
				return;
			}
			if (!base.HasImage)
			{
				offset = -1;
				return;
			}
			lock (Module.SyncRoot)
			{
				if (offset == -2)
				{
					offset = Module.Read(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldLayout(field));
				}
			}
		}

		private void ResolveRVA()
		{
			if (rva != -2 || !base.HasImage)
			{
				return;
			}
			lock (Module.SyncRoot)
			{
				if (rva == -2)
				{
					rva = Module.Read(this, (FieldDefinition field, MetadataReader reader) => reader.ReadFieldRVA(field));
				}
			}
		}

		public FieldDefinition(string name, FieldAttributes attributes, TypeReference fieldType)
			: base(name, fieldType)
		{
			this.attributes = (ushort)attributes;
		}

		public override FieldDefinition Resolve()
		{
			return this;
		}
	}
	public class FieldReference : MemberReference
	{
		private TypeReference field_type;

		public TypeReference FieldType
		{
			get
			{
				return field_type;
			}
			set
			{
				field_type = value;
			}
		}

		public override string FullName => field_type.FullName + " " + MemberFullName();

		public override bool ContainsGenericParameter => field_type.ContainsGenericParameter || base.ContainsGenericParameter;

		internal FieldReference()
		{
			token = new MetadataToken(TokenType.MemberRef);
		}

		public FieldReference(string name, TypeReference fieldType)
			: base(name)
		{
			Mixin.CheckType(fieldType, Mixin.Argument.fieldType);
			field_type = fieldType;
			token = new MetadataToken(TokenType.MemberRef);
		}

		public FieldReference(string name, TypeReference fieldType, TypeReference declaringType)
			: this(name, fieldType)
		{
			Mixin.CheckType(declaringType, Mixin.Argument.declaringType);
			DeclaringType = declaringType;
		}

		protected override IMemberDefinition ResolveDefinition()
		{
			return Resolve();
		}

		public new virtual FieldDefinition Resolve()
		{
			ModuleDefinition module = Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}
	}
	internal enum FileAttributes : uint
	{
		ContainsMetaData,
		ContainsNoMetaData
	}
	public sealed class FunctionPointerType : TypeSpecification, IMethodSignature, IMetadataTokenProvider
	{
		private readonly MethodReference function;

		public bool HasThis
		{
			get
			{
				return function.HasThis;
			}
			set
			{
				function.HasThis = value;
			}
		}

		public bool ExplicitThis
		{
			get
			{
				return function.ExplicitThis;
			}
			set
			{
				function.ExplicitThis = value;
			}
		}

		public MethodCallingConvention CallingConvention
		{
			get
			{
				return function.CallingConvention;
			}
			set
			{
				function.CallingConvention = value;
			}
		}

		public bool HasParameters => function.HasParameters;

		public Collection<ParameterDefinition> Parameters => function.Parameters;

		public TypeReference ReturnType
		{
			get
			{
				return function.MethodReturnType.ReturnType;
			}
			set
			{
				function.MethodReturnType.ReturnType = value;
			}
		}

		public MethodReturnType MethodReturnType => function.MethodReturnType;

		public override string Name
		{
			get
			{
				return function.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override ModuleDefinition Module => ReturnType.Module;

		public override IMetadataScope Scope
		{
			get
			{
				return function.ReturnType.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsFunctionPointer => true;

		public override bool ContainsGenericParameter => function.ContainsGenericParameter;

		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(function.Name);
				stringBuilder.Append(" ");
				stringBuilder.Append(function.ReturnType.FullName);
				stringBuilder.Append(" *");
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		public FunctionPointerType()
			: base(null)
		{
			function = new MethodReference();
			function.Name = "method";
			etype = Mono.Cecil.Metadata.ElementType.FnPtr;
		}

		public override TypeDefinition Resolve()
		{
			return null;
		}

		public override TypeReference GetElementType()
		{
			return this;
		}
	}
	public sealed class GenericInstanceMethod : MethodSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		private Collection<TypeReference> arguments;

		public bool HasGenericArguments => !arguments.IsNullOrEmpty();

		public Collection<TypeReference> GenericArguments
		{
			get
			{
				if (arguments == null)
				{
					Interlocked.CompareExchange(ref arguments, new Collection<TypeReference>(), null);
				}
				return arguments;
			}
		}

		public override bool IsGenericInstance => true;

		IGenericParameterProvider IGenericContext.Method => base.ElementMethod;

		IGenericParameterProvider IGenericContext.Type => base.ElementMethod.DeclaringType;

		public override bool ContainsGenericParameter => this.ContainsGenericParameter() || base.ContainsGenericParameter;

		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				MethodReference elementMethod = base.ElementMethod;
				stringBuilder.Append(elementMethod.ReturnType.FullName).Append(" ").Append(elementMethod.DeclaringType.FullName)
					.Append("::")
					.Append(elementMethod.Name);
				this.GenericInstanceFullName(stringBuilder);
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		public GenericInstanceMethod(MethodReference method)
			: base(method)
		{
		}

		internal GenericInstanceMethod(MethodReference method, int arity)
			: this(method)
		{
			arguments = new Collection<TypeReference>(arity);
		}
	}
	public sealed class GenericInstanceType : TypeSpecification, IGenericInstance, IMetadataTokenProvider, IGenericContext
	{
		private Collection<TypeReference> arguments;

		public bool HasGenericArguments => !arguments.IsNullOrEmpty();

		public Collection<TypeReference> GenericArguments
		{
			get
			{
				if (arguments == null)
				{
					Interlocked.CompareExchange(ref arguments, new Collection<TypeReference>(), null);
				}
				return arguments;
			}
		}

		public override TypeReference DeclaringType
		{
			get
			{
				return base.ElementType.DeclaringType;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.FullName);
				this.GenericInstanceFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		public override bool IsGenericInstance => true;

		public override bool ContainsGenericParameter => this.ContainsGenericParameter() || base.ContainsGenericParameter;

		IGenericParameterProvider IGenericContext.Type => base.ElementType;

		public GenericInstanceType(TypeReference type)
			: base(type)
		{
			base.IsValueType = type.IsValueType;
			etype = Mono.Cecil.Metadata.ElementType.GenericInst;
		}

		internal GenericInstanceType(TypeReference type, int arity)
			: this(type)
		{
			arguments = new Collection<TypeReference>(arity);
		}
	}
	public sealed class GenericParameter : TypeReference, ICustomAttributeProvider, IMetadataTokenProvider
	{
		internal int position;

		internal GenericParameterType type;

		internal IGenericParameterProvider owner;

		private ushort attributes;

		private GenericParameterConstraintCollection constraints;

		private Collection<CustomAttribute> custom_attributes;

		public GenericParameterAttributes Attributes
		{
			get
			{
				return (GenericParameterAttributes)attributes;
			}
			set
			{
				attributes = (ushort)value;
			}
		}

		public int Position => position;

		public GenericParameterType Type => type;

		public IGenericParameterProvider Owner => owner;

		public bool HasConstraints
		{
			get
			{
				if (constraints != null)
				{
					return constraints.Count > 0;
				}
				return base.HasImage && Module.Read(this, (GenericParameter generic_parameter, MetadataReader reader) => reader.HasGenericConstraints(generic_parameter));
			}
		}

		public Collection<GenericParameterConstraint> Constraints
		{
			get
			{
				if (constraints != null)
				{
					return constraints;
				}
				if (base.HasImage)
				{
					return Module.Read(ref constraints, this, (GenericParameter generic_parameter, MetadataReader reader) => reader.ReadGenericConstraints(generic_parameter));
				}
				Interlocked.CompareExchange(ref constraints, new GenericParameterConstraintCollection(this), null);
				return constraints;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, Module);

		public override IMetadataScope Scope
		{
			get
			{
				if (owner == null)
				{
					return null;
				}
				return (owner.GenericParameterType == GenericParameterType.Method) ? ((MethodReference)owner).DeclaringType.Scope : ((TypeReference)owner).Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override TypeReference DeclaringType
		{
			get
			{
				return owner as TypeReference;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public MethodReference DeclaringMethod => owner as MethodReference;

		public override ModuleDefinition Module => module ?? owner.Module;

		public override string Name
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Name))
				{
					return base.Name;
				}
				return base.Name = ((type == GenericParameterType.Method) ? "!!" : "!") + position;
			}
		}

		public override string Namespace
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override string FullName => Name;

		public override bool IsGenericParameter => true;

		public override bool ContainsGenericParameter => true;

		public override MetadataType MetadataType => (MetadataType)etype;

		public bool IsNonVariant
		{
			get
			{
				return attributes.GetMaskedAttributes(3, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(3, 0u, value);
			}
		}

		public bool IsCovariant
		{
			get
			{
				return attributes.GetMaskedAttributes(3, 1u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(3, 1u, value);
			}
		}

		public bool IsContravariant
		{
			get
			{
				return attributes.GetMaskedAttributes(3, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(3, 2u, value);
			}
		}

		public bool HasReferenceTypeConstraint
		{
			get
			{
				return attributes.GetAttributes(4);
			}
			set
			{
				attributes = attributes.SetAttributes(4, value);
			}
		}

		public bool HasNotNullableValueTypeConstraint
		{
			get
			{
				return attributes.GetAttributes(8);
			}
			set
			{
				attributes = attributes.SetAttributes(8, value);
			}
		}

		public bool HasDefaultConstructorConstraint
		{
			get
			{
				return attributes.GetAttributes(16);
			}
			set
			{
				attributes = attributes.SetAttributes(16, value);
			}
		}

		public bool AllowByRefLikeConstraint
		{
			get
			{
				return attributes.GetAttributes(32);
			}
			set
			{
				attributes = attributes.SetAttributes(32, value);
			}
		}

		public GenericParameter(IGenericParameterProvider owner)
			: this(string.Empty, owner)
		{
		}

		public GenericParameter(string name, IGenericParameterProvider owner)
			: base(string.Empty, name)
		{
			if (owner == null)
			{
				throw new ArgumentNullException();
			}
			position = -1;
			this.owner = owner;
			type = owner.GenericParameterType;
			etype = ConvertGenericParameterType(type);
			token = new MetadataToken(TokenType.GenericParam);
		}

		internal GenericParameter(int position, GenericParameterType type, ModuleDefinition module)
			: base(string.Empty, string.Empty)
		{
			Mixin.CheckModule(module);
			this.position = position;
			this.type = type;
			etype = ConvertGenericParameterType(type);
			base.module = module;
			token = new MetadataToken(TokenType.GenericParam);
		}

		private static ElementType ConvertGenericParameterType(GenericParameterType type)
		{
			return type switch
			{
				GenericParameterType.Type => ElementType.Var, 
				GenericParameterType.Method => ElementType.MVar, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
		}

		public override TypeDefinition Resolve()
		{
			return null;
		}
	}
	internal sealed class GenericParameterCollection : Collection<GenericParameter>
	{
		private readonly IGenericParameterProvider owner;

		internal GenericParameterCollection(IGenericParameterProvider owner)
		{
			this.owner = owner;
		}

		internal GenericParameterCollection(IGenericParameterProvider owner, int capacity)
			: base(capacity)
		{
			this.owner = owner;
		}

		protected override void OnAdd(GenericParameter item, int index)
		{
			UpdateGenericParameter(item, index);
		}

		protected override void OnInsert(GenericParameter item, int index)
		{
			UpdateGenericParameter(item, index);
			for (int i = index; i < size; i++)
			{
				items[i].position = i + 1;
			}
		}

		protected override void OnSet(GenericParameter item, int index)
		{
			UpdateGenericParameter(item, index);
		}

		private void UpdateGenericParameter(GenericParameter item, int index)
		{
			item.owner = owner;
			item.position = index;
			item.type = owner.GenericParameterType;
		}

		protected override void OnRemove(GenericParameter item, int index)
		{
			item.owner = null;
			item.position = -1;
			item.type = GenericParameterType.Type;
			for (int i = index + 1; i < size; i++)
			{
				items[i].position = i - 1;
			}
		}
	}
	public sealed class GenericParameterConstraint : ICustomAttributeProvider, IMetadataTokenProvider
	{
		internal GenericParameter generic_parameter;

		internal MetadataToken token;

		private TypeReference constraint_type;

		private Collection<CustomAttribute> custom_attributes;

		public TypeReference ConstraintType
		{
			get
			{
				return constraint_type;
			}
			set
			{
				constraint_type = value;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				if (generic_parameter == null)
				{
					return false;
				}
				return this.GetHasCustomAttributes(generic_parameter.Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				if (generic_parameter == null)
				{
					if (custom_attributes == null)
					{
						Interlocked.CompareExchange(ref custom_attributes, new Collection<CustomAttribute>(), null);
					}
					return custom_attributes;
				}
				return custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, generic_parameter.Module);
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		internal GenericParameterConstraint(TypeReference constraintType, MetadataToken token)
		{
			constraint_type = constraintType;
			this.token = token;
		}

		public GenericParameterConstraint(TypeReference constraintType)
		{
			Mixin.CheckType(constraintType, Mixin.Argument.constraintType);
			constraint_type = constraintType;
			token = new MetadataToken(TokenType.GenericParamConstraint);
		}
	}
	internal class GenericParameterConstraintCollection : Collection<GenericParameterConstraint>
	{
		private readonly GenericParameter generic_parameter;

		internal GenericParameterConstraintCollection(GenericParameter genericParameter)
		{
			generic_parameter = genericParameter;
		}

		internal GenericParameterConstraintCollection(GenericParameter genericParameter, int length)
			: base(length)
		{
			generic_parameter = genericParameter;
		}

		protected override void OnAdd(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = generic_parameter;
		}

		protected override void OnInsert(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = generic_parameter;
		}

		protected override void OnSet(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = generic_parameter;
		}

		protected override void OnRemove(GenericParameterConstraint item, int index)
		{
			item.generic_parameter = null;
		}
	}
	[Flags]
	public enum GenericParameterAttributes : ushort
	{
		VarianceMask = 3,
		NonVariant = 0,
		Covariant = 1,
		Contravariant = 2,
		SpecialConstraintMask = 0x1C,
		ReferenceTypeConstraint = 4,
		NotNullableValueTypeConstraint = 8,
		DefaultConstructorConstraint = 0x10,
		AllowByRefLikeConstraint = 0x20
	}
	internal sealed class GenericParameterResolver
	{
		internal static TypeReference ResolveReturnTypeIfNeeded(MethodReference methodReference)
		{
			if (methodReference.DeclaringType.IsArray && methodReference.Name == "Get")
			{
				return methodReference.ReturnType;
			}
			GenericInstanceMethod genericInstanceMethod = methodReference as GenericInstanceMethod;
			GenericInstanceType genericInstanceType = methodReference.DeclaringType as GenericInstanceType;
			if (genericInstanceMethod == null && genericInstanceType == null)
			{
				return methodReference.ReturnType;
			}
			return ResolveIfNeeded(genericInstanceMethod, genericInstanceType, methodReference.ReturnType);
		}

		internal static TypeReference ResolveFieldTypeIfNeeded(FieldReference fieldReference)
		{
			return ResolveIfNeeded(null, fieldReference.DeclaringType as GenericInstanceType, fieldReference.FieldType);
		}

		internal static TypeReference ResolveParameterTypeIfNeeded(MethodReference method, ParameterReference parameter)
		{
			GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;
			GenericInstanceType genericInstanceType = method.DeclaringType as GenericInstanceType;
			if (genericInstanceMethod == null && genericInstanceType == null)
			{
				return parameter.ParameterType;
			}
			return ResolveIfNeeded(genericInstanceMethod, genericInstanceType, parameter.ParameterType);
		}

		internal static TypeReference ResolveVariableTypeIfNeeded(MethodReference method, VariableReference variable)
		{
			GenericInstanceMethod genericInstanceMethod = method as GenericInstanceMethod;
			GenericInstanceType genericInstanceType = method.DeclaringType as GenericInstanceType;
			if (genericInstanceMethod == null && genericInstanceType == null)
			{
				return variable.VariableType;
			}
			return ResolveIfNeeded(genericInstanceMethod, genericInstanceType, variable.VariableType);
		}

		private static TypeReference ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance declaringGenericInstanceType, TypeReference parameterType)
		{
			if (parameterType is ByReferenceType byReferenceType)
			{
				return ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, byReferenceType);
			}
			if (parameterType is ArrayType arrayType)
			{
				return ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, arrayType);
			}
			if (parameterType is GenericInstanceType genericInstanceType)
			{
				return ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, genericInstanceType);
			}
			if (parameterType is GenericParameter genericParameterElement)
			{
				return ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, genericParameterElement);
			}
			if (parameterType is RequiredModifierType requiredModifierType && ContainsGenericParameters(requiredModifierType))
			{
				return ResolveIfNeeded(genericInstanceMethod, declaringGenericInstanceType, requiredModifierType.ElementType);
			}
			if (ContainsGenericParameters(parameterType))
			{
				throw new Exception("Unexpected generic parameter.");
			}
			return parameterType;
		}

		private static TypeReference ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, GenericParameter genericParameterElement)
		{
			return (genericParameterElement.MetadataType != MetadataType.MVar) ? genericInstanceType.GenericArguments[genericParameterElement.Position] : ((genericInstanceMethod != null) ? genericInstanceMethod.GenericArguments[genericParameterElement.Position] : genericParameterElement);
		}

		private static ArrayType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, ArrayType arrayType)
		{
			return new ArrayType(ResolveIfNeeded(genericInstanceMethod, genericInstanceType, arrayType.ElementType), arrayType.Rank);
		}

		private static ByReferenceType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, ByReferenceType byReferenceType)
		{
			return new ByReferenceType(ResolveIfNeeded(genericInstanceMethod, genericInstanceType, byReferenceType.ElementType));
		}

		private static GenericInstanceType ResolveIfNeeded(IGenericInstance genericInstanceMethod, IGenericInstance genericInstanceType, GenericInstanceType genericInstanceType1)
		{
			if (!ContainsGenericParameters(genericInstanceType1))
			{
				return genericInstanceType1;
			}
			GenericInstanceType genericInstanceType2 = new GenericInstanceType(genericInstanceType1.ElementType);
			foreach (TypeReference genericArgument in genericInstanceType1.GenericArguments)
			{
				if (!genericArgument.IsGenericParameter)
				{
					genericInstanceType2.GenericArguments.Add(ResolveIfNeeded(genericInstanceMethod, genericInstanceType, genericArgument));
					continue;
				}
				GenericParameter genericParameter = (GenericParameter)genericArgument;
				switch (genericParameter.Type)
				{
				case GenericParameterType.Type:
					if (genericInstanceType == null)
					{
						throw new NotSupportedException();
					}
					genericInstanceType2.GenericArguments.Add(genericInstanceType.GenericArguments[genericParameter.Position]);
					break;
				case GenericParameterType.Method:
					if (genericInstanceMethod == null)
					{
						genericInstanceType2.GenericArguments.Add(genericParameter);
					}
					else
					{
						genericInstanceType2.GenericArguments.Add(genericInstanceMethod.GenericArguments[genericParameter.Position]);
					}
					break;
				}
			}
			return genericInstanceType2;
		}

		private static bool ContainsGenericParameters(TypeReference typeReference)
		{
			if (typeReference is GenericParameter)
			{
				return true;
			}
			if (typeReference is ArrayType arrayType)
			{
				return ContainsGenericParameters(arrayType.ElementType);
			}
			if (typeReference is PointerType pointerType)
			{
				return ContainsGenericParameters(pointerType.ElementType);
			}
			if (typeReference is ByReferenceType byReferenceType)
			{
				return ContainsGenericParameters(byReferenceType.ElementType);
			}
			if (typeReference is SentinelType sentinelType)
			{
				return ContainsGenericParameters(sentinelType.ElementType);
			}
			if (typeReference is PinnedType pinnedType)
			{
				return ContainsGenericParameters(pinnedType.ElementType);
			}
			if (typeReference is RequiredModifierType requiredModifierType)
			{
				return ContainsGenericParameters(requiredModifierType.ElementType);
			}
			if (typeReference is GenericInstanceType genericInstanceType)
			{
				foreach (TypeReference genericArgument in genericInstanceType.GenericArguments)
				{
					if (ContainsGenericParameters(genericArgument))
					{
						return true;
					}
				}
				return false;
			}
			if (typeReference is TypeSpecification)
			{
				throw new NotSupportedException();
			}
			return false;
		}
	}
	public interface IConstantProvider : IMetadataTokenProvider
	{
		bool HasConstant { get; set; }

		object Constant { get; set; }
	}
	public interface ICustomAttributeProvider : IMetadataTokenProvider
	{
		Collection<CustomAttribute> CustomAttributes { get; }

		bool HasCustomAttributes { get; }
	}
	public interface IGenericInstance : IMetadataTokenProvider
	{
		bool HasGenericArguments { get; }

		Collection<TypeReference> GenericArguments { get; }
	}
	public interface IGenericParameterProvider : IMetadataTokenProvider
	{
		bool HasGenericParameters { get; }

		bool IsDefinition { get; }

		ModuleDefinition Module { get; }

		Collection<GenericParameter> GenericParameters { get; }

		GenericParameterType GenericParameterType { get; }
	}
	public enum GenericParameterType
	{
		Type,
		Method
	}
	internal interface IGenericContext
	{
		bool IsDefinition { get; }

		IGenericParameterProvider Type { get; }

		IGenericParameterProvider Method { get; }
	}
	public interface IMarshalInfoProvider : IMetadataTokenProvider
	{
		bool HasMarshalInfo { get; }

		MarshalInfo MarshalInfo { get; set; }
	}
	public interface IMemberDefinition : ICustomAttributeProvider, IMetadataTokenProvider
	{
		string Name { get; set; }

		string FullName { get; }

		bool IsSpecialName { get; set; }

		bool IsRuntimeSpecialName { get; set; }

		TypeDefinition DeclaringType { get; set; }
	}
	public enum MetadataScopeType
	{
		AssemblyNameReference,
		ModuleReference,
		ModuleDefinition
	}
	public interface IMetadataScope : IMetadataTokenProvider
	{
		MetadataScopeType MetadataScopeType { get; }

		string Name { get; set; }
	}
	public interface IMetadataTokenProvider
	{
		MetadataToken MetadataToken { get; set; }
	}
	public interface IMethodSignature : IMetadataTokenProvider
	{
		bool HasThis { get; set; }

		bool ExplicitThis { get; set; }

		MethodCallingConvention CallingConvention { get; set; }

		bool HasParameters { get; }

		Collection<ParameterDefinition> Parameters { get; }

		TypeReference ReturnType { get; set; }

		MethodReturnType MethodReturnType { get; }
	}
	public interface IMetadataImporterProvider
	{
		IMetadataImporter GetMetadataImporter(ModuleDefinition module);
	}
	public interface IMetadataImporter
	{
		AssemblyNameReference ImportReference(AssemblyNameReference reference);

		TypeReference ImportReference(TypeReference type, IGenericParameterProvider context);

		FieldReference ImportReference(FieldReference field, IGenericParameterProvider context);

		MethodReference ImportReference(MethodReference method, IGenericParameterProvider context);
	}
	public interface IReflectionImporterProvider
	{
		IReflectionImporter GetReflectionImporter(ModuleDefinition module);
	}
	public interface IReflectionImporter
	{
		AssemblyNameReference ImportReference(AssemblyName reference);

		TypeReference ImportReference(Type type, IGenericParameterProvider context);

		FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context);

		MethodReference ImportReference(MethodBase method, IGenericParameterProvider context);
	}
	internal struct ImportGenericContext
	{
		private Collection<IGenericParameterProvider> stack;

		public bool IsEmpty => stack == null;

		public ImportGenericContext(IGenericParameterProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			stack = null;
			Push(provider);
		}

		public void Push(IGenericParameterProvider provider)
		{
			if (stack == null)
			{
				stack = new Collection<IGenericParameterProvider>(1) { provider };
			}
			else
			{
				stack.Add(provider);
			}
		}

		public void Pop()
		{
			stack.RemoveAt(stack.Count - 1);
		}

		public TypeReference MethodParameter(string method, int position)
		{
			for (int num = stack.Count - 1; num >= 0; num--)
			{
				if (stack[num] is MethodReference methodReference && !(method != NormalizeMethodName(methodReference)))
				{
					return methodReference.GenericParameters[position];
				}
			}
			throw new InvalidOperationException();
		}

		public string NormalizeMethodName(MethodReference method)
		{
			return method.DeclaringType.GetElementType().FullName + "." + method.Name;
		}

		public TypeReference TypeParameter(string type, int position)
		{
			for (int num = stack.Count - 1; num >= 0; num--)
			{
				TypeReference typeReference = GenericTypeFor(stack[num]);
				if (!(typeReference.FullName != type))
				{
					return typeReference.GenericParameters[position];
				}
			}
			throw new InvalidOperationException();
		}

		private static TypeReference GenericTypeFor(IGenericParameterProvider context)
		{
			if (context is TypeReference typeReference)
			{
				return typeReference.GetElementType();
			}
			if (context is MethodReference methodReference)
			{
				return methodReference.DeclaringType.GetElementType();
			}
			throw new InvalidOperationException();
		}

		public static ImportGenericContext For(IGenericParameterProvider context)
		{
			return (context != null) ? new ImportGenericContext(context) : default(ImportGenericContext);
		}
	}
	public class DefaultReflectionImporter : IReflectionImporter
	{
		private enum ImportGenericKind
		{
			Definition,
			Open
		}

		protected readonly ModuleDefinition module;

		private static readonly Dictionary<Type, ElementType> type_etype_mapping = new Dictionary<Type, ElementType>(18)
		{
			{
				typeof(void),
				ElementType.Void
			},
			{
				typeof(bool),
				ElementType.Boolean
			},
			{
				typeof(char),
				ElementType.Char
			},
			{
				typeof(sbyte),
				ElementType.I1
			},
			{
				typeof(byte),
				ElementType.U1
			},
			{
				typeof(short),
				ElementType.I2
			},
			{
				typeof(ushort),
				ElementType.U2
			},
			{
				typeof(int),
				ElementType.I4
			},
			{
				typeof(uint),
				ElementType.U4
			},
			{
				typeof(long),
				ElementType.I8
			},
			{
				typeof(ulong),
				ElementType.U8
			},
			{
				typeof(float),
				ElementType.R4
			},
			{
				typeof(double),
				ElementType.R8
			},
			{
				typeof(string),
				ElementType.String
			},
			{
				typeof(TypedReference),
				ElementType.TypedByRef
			},
			{
				typeof(IntPtr),
				ElementType.I
			},
			{
				typeof(UIntPtr),
				ElementType.U
			},
			{
				typeof(object),
				ElementType.Object
			}
		};

		public DefaultReflectionImporter(ModuleDefinition module)
		{
			Mixin.CheckModule(module);
			this.module = module;
		}

		private TypeReference ImportType(Type type, ImportGenericContext context, Type[] required_modifiers, Type[] optional_modifiers)
		{
			TypeReference typeReference = ImportType(type, context);
			foreach (Type type2 in required_modifiers)
			{
				typeReference = new RequiredModifierType(ImportType(type2, context), typeReference);
			}
			foreach (Type type3 in optional_modifiers)
			{
				typeReference = new OptionalModifierType(ImportType(type3, context), typeReference);
			}
			return typeReference;
		}

		private TypeReference ImportType(Type type, ImportGenericContext context)
		{
			return ImportType(type, context, ImportGenericKind.Open);
		}

		private TypeReference ImportType(Type type, ImportGenericContext context, ImportGenericKind import_kind)
		{
			if (IsTypeSpecification(type) || ImportOpenGenericType(type, import_kind))
			{
				return ImportTypeSpecification(type, context);
			}
			TypeReference typeReference = new TypeReference(string.Empty, type.Name, module, ImportScope(type), type.IsValueType);
			typeReference.etype = ImportElementType(type);
			if (IsNestedType(type))
			{
				typeReference.DeclaringType = ImportType(type.DeclaringType, context, import_kind);
			}
			else
			{
				typeReference.Namespace = type.Namespace ?? string.Empty;
			}
			if (type.IsGenericType)
			{
				ImportGenericParameters(typeReference, type.GetGenericArguments());
			}
			return typeReference;
		}

		protected virtual IMetadataScope ImportScope(Type type)
		{
			return ImportScope(type.Assembly);
		}

		private static bool ImportOpenGenericType(Type type, ImportGenericKind import_kind)
		{
			return type.IsGenericType && type.IsGenericTypeDefinition && import_kind == ImportGenericKind.Open;
		}

		private static bool ImportOpenGenericMethod(MethodBase method, ImportGenericKind import_kind)
		{
			return method.IsGenericMethod && method.IsGenericMethodDefinition && import_kind == ImportGenericKind.Open;
		}

		private static bool IsNestedType(Type type)
		{
			return type.IsNested;
		}

		private TypeReference ImportTypeSpecification(Type type, ImportGenericContext context)
		{
			if (type.IsByRef)
			{
				return new ByReferenceType(ImportType(type.GetElementType(), context));
			}
			if (type.IsPointer)
			{
				return new PointerType(ImportType(type.GetElementType(), context));
			}
			if (type.IsArray)
			{
				return new ArrayType(ImportType(type.GetElementType(), context), type.GetArrayRank());
			}
			if (type.IsGenericType)
			{
				return ImportGenericInstance(type, context);
			}
			if (type.IsGenericParameter)
			{
				return ImportGenericParameter(type, context);
			}
			throw new NotSupportedException(type.FullName);
		}

		private static TypeReference ImportGenericParameter(Type type, ImportGenericContext context)
		{
			if (context.IsEmpty)
			{
				throw new InvalidOperationException();
			}
			if (type.DeclaringMethod != null)
			{
				return context.MethodParameter(NormalizeMethodName(type.DeclaringMethod), type.GenericParameterPosition);
			}
			if (type.DeclaringType != null)
			{
				return context.TypeParameter(NormalizeTypeFullName(type.DeclaringType), type.GenericParameterPosition);
			}
			throw new InvalidOperationException();
		}

		private static string NormalizeMethodName(MethodBase method)
		{
			return NormalizeTypeFullName(method.DeclaringType) + "." + method.Name;
		}

		private static string NormalizeTypeFullName(Type type)
		{
			if (IsNestedType(type))
			{
				return NormalizeTypeFullName(type.DeclaringType) + "/" + type.Name;
			}
			return type.FullName;
		}

		private TypeReference ImportGenericInstance(Type type, ImportGenericContext context)
		{
			TypeReference typeReference = ImportType(type.GetGenericTypeDefinition(), context, ImportGenericKind.Definition);
			Type[] genericArguments = type.GetGenericArguments();
			GenericInstanceType genericInstanceType = new GenericInstanceType(typeReference, genericArguments.Length);
			Collection<TypeReference> genericArguments2 = genericInstanceType.GenericArguments;
			context.Push(typeReference);
			try
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments2.Add(ImportType(genericArguments[i], context));
				}
				return genericInstanceType;
			}
			finally
			{
				context.Pop();
			}
		}

		private static bool IsTypeSpecification(Type type)
		{
			return type.HasElementType || IsGenericInstance(type) || type.IsGenericParameter;
		}

		private static bool IsGenericInstance(Type type)
		{
			return type.IsGenericType && !type.IsGenericTypeDefinition;
		}

		private static ElementType ImportElementType(Type type)
		{
			if (!type_etype_mapping.TryGetValue(type, out var value))
			{
				return ElementType.None;
			}
			return value;
		}

		protected AssemblyNameReference ImportScope(Assembly assembly)
		{
			return ImportReference(assembly.GetName());
		}

		public virtual AssemblyNameReference ImportReference(AssemblyName name)
		{
			Mixin.CheckName(name);
			if (TryGetAssemblyNameReference(name, out var assembly_reference))
			{
				return assembly_reference;
			}
			assembly_reference = new AssemblyNameReference(name.Name, name.Version)
			{
				PublicKeyToken = name.GetPublicKeyToken(),
				Culture = name.CultureInfo.Name,
				HashAlgorithm = (AssemblyHashAlgorithm)name.HashAlgorithm
			};
			module.AssemblyReferences.Add(assembly_reference);
			return assembly_reference;
		}

		private bool TryGetAssemblyNameReference(AssemblyName name, out AssemblyNameReference assembly_reference)
		{
			Collection<AssemblyNameReference> assemblyReferences = module.AssemblyReferences;
			for (int i = 0; i < assemblyReferences.Count; i++)
			{
				AssemblyNameReference assemblyNameReference = assemblyReferences[i];
				if (!(name.FullName != assemblyNameReference.FullName))
				{
					assembly_reference = assemblyNameReference;
					return true;
				}
			}
			assembly_reference = null;
			return false;
		}

		private FieldReference ImportField(FieldInfo field, ImportGenericContext context)
		{
			TypeReference typeReference = ImportType(field.DeclaringType, context);
			if (IsGenericInstance(field.DeclaringType))
			{
				field = ResolveFieldDefinition(field);
			}
			context.Push(typeReference);
			try
			{
				return new FieldReference
				{
					Name = field.Name,
					DeclaringType = typeReference,
					FieldType = ImportType(field.FieldType, context, field.GetRequiredCustomModifiers(), field.GetOptionalCustomModifiers())
				};
			}
			finally
			{
				context.Pop();
			}
		}

		private static FieldInfo ResolveFieldDefinition(FieldInfo field)
		{
			return field.Module.ResolveField(field.MetadataToken);
		}

		private static MethodBase ResolveMethodDefinition(MethodBase method)
		{
			return method.Module.ResolveMethod(method.MetadataToken);
		}

		private MethodReference ImportMethod(MethodBase method, ImportGenericContext context, ImportGenericKind import_kind)
		{
			if (IsMethodSpecification(method) || ImportOpenGenericMethod(method, import_kind))
			{
				return ImportMethodSpecification(method, context);
			}
			TypeReference declaringType = ImportType(method.DeclaringType, context);
			if (IsGenericInstance(method.DeclaringType))
			{
				method = ResolveMethodDefinition(method);
			}
			MethodReference methodReference = new MethodReference
			{
				Name = method.Name,
				HasThis = HasCallingConvention(method, CallingConventions.HasThis),
				ExplicitThis = HasCallingConvention(method, CallingConventions.ExplicitThis),
				DeclaringType = ImportType(method.DeclaringType, context, ImportGenericKind.Definition)
			};
			if (HasCallingConvention(method, CallingConventions.VarArgs))
			{
				methodReference.CallingConvention &= MethodCallingConvention.VarArg;
			}
			if (method.IsGenericMethod)
			{
				ImportGenericParameters(methodReference, method.GetGenericArguments());
			}
			context.Push(methodReference);
			try
			{
				MethodInfo methodInfo = method as MethodInfo;
				methodReference.ReturnType = ((methodInfo != null) ? ImportType(methodInfo.ReturnType, context, methodInfo.ReturnParameter.GetRequiredCustomModifiers(), methodInfo.ReturnParameter.GetOptionalCustomModifiers()) : ImportType(typeof(void), default(ImportGenericContext)));
				ParameterInfo[] parameters = method.GetParameters();
				Collection<ParameterDefinition> parameters2 = methodReference.Parameters;
				foreach (ParameterInfo parameterInfo in parameters)
				{
					parameters2.Add(new ParameterDefinition(ImportType(parameterInfo.ParameterType, context, parameterInfo.GetRequiredCustomModifiers(), parameterInfo.GetOptionalCustomModifiers())));
				}
				methodReference.DeclaringType = declaringType;
				return methodReference;
			}
			finally
			{
				context.Pop();
			}
		}

		private static void ImportGenericParameters(IGenericParameterProvider provider, Type[] arguments)
		{
			Collection<GenericParameter> genericParameters = provider.GenericParameters;
			for (int i = 0; i < arguments.Length; i++)
			{
				genericParameters.Add(new GenericParameter(arguments[i].Name, provider));
			}
		}

		private static bool IsMethodSpecification(MethodBase method)
		{
			return method.IsGenericMethod && !method.IsGenericMethodDefinition;
		}

		private MethodReference ImportMethodSpecification(MethodBase method, ImportGenericContext context)
		{
			MethodInfo methodInfo = method as MethodInfo;
			if (methodInfo == null)
			{
				throw new InvalidOperationException();
			}
			MethodReference methodReference = ImportMethod(methodInfo.GetGenericMethodDefinition(), context, ImportGenericKind.Definition);
			GenericInstanceMethod genericInstanceMethod = new GenericInstanceMethod(methodReference);
			Type[] genericArguments = method.GetGenericArguments();
			Collection<TypeReference> genericArguments2 = genericInstanceMethod.GenericArguments;
			context.Push(methodReference);
			try
			{
				for (int i = 0; i < genericArguments.Length; i++)
				{
					genericArguments2.Add(ImportType(genericArguments[i], context));
				}
				return genericInstanceMethod;
			}
			finally
			{
				context.Pop();
			}
		}

		private static bool HasCallingConvention(MethodBase method, CallingConventions conventions)
		{
			return (method.CallingConvention & conventions) != 0;
		}

		public virtual TypeReference ImportReference(Type type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			return ImportType(type, ImportGenericContext.For(context), (context != null) ? ImportGenericKind.Open : ImportGenericKind.Definition);
		}

		public virtual FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			return ImportField(field, ImportGenericContext.For(context));
		}

		public virtual MethodReference ImportReference(MethodBase method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			return ImportMethod(method, ImportGenericContext.For(context), (context != null) ? ImportGenericKind.Open : ImportGenericKind.Definition);
		}
	}
	public class DefaultMetadataImporter : IMetadataImporter
	{
		protected readonly ModuleDefinition module;

		public DefaultMetadataImporter(ModuleDefinition module)
		{
			Mixin.CheckModule(module);
			this.module = module;
		}

		private TypeReference ImportType(TypeReference type, ImportGenericContext context)
		{
			if (type.IsTypeSpecification())
			{
				return ImportTypeSpecification(type, context);
			}
			TypeReference typeReference = new TypeReference(type.Namespace, type.Name, module, ImportScope(type), type.IsValueType);
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference);
			if (type.IsNested)
			{
				typeReference.DeclaringType = ImportType(type.DeclaringType, context);
			}
			if (type.HasGenericParameters)
			{
				ImportGenericParameters(typeReference, type);
			}
			return typeReference;
		}

		protected virtual IMetadataScope ImportScope(TypeReference type)
		{
			return ImportScope(type.Scope);
		}

		protected IMetadataScope ImportScope(IMetadataScope scope)
		{
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
				return ImportReference((AssemblyNameReference)scope);
			case MetadataScopeType.ModuleDefinition:
				if (scope == module)
				{
					return scope;
				}
				return ImportReference(((ModuleDefinition)scope).Assembly.Name);
			case MetadataScopeType.ModuleReference:
				throw new NotImplementedException();
			default:
				throw new NotSupportedException();
			}
		}

		public virtual AssemblyNameReference ImportReference(AssemblyNameReference name)
		{
			Mixin.CheckName(name);
			if (module.TryGetAssemblyNameReference(name, out var assembly_reference))
			{
				return assembly_reference;
			}
			assembly_reference = new AssemblyNameReference(name.Name, name.Version)
			{
				Culture = name.Culture,
				HashAlgorithm = name.HashAlgorithm,
				IsRetargetable = name.IsRetargetable,
				IsWindowsRuntime = name.IsWindowsRuntime
			};
			byte[] array = ((!name.PublicKeyToken.IsNullOrEmpty()) ? new byte[name.PublicKeyToken.Length] : Empty<byte>.Array);
			if (array.Length != 0)
			{
				Buffer.BlockCopy(name.PublicKeyToken, 0, array, 0, array.Length);
			}
			assembly_reference.PublicKeyToken = array;
			module.AssemblyReferences.Add(assembly_reference);
			return assembly_reference;
		}

		private static void ImportGenericParameters(IGenericParameterProvider imported, IGenericParameterProvider original)
		{
			Collection<GenericParameter> genericParameters = original.GenericParameters;
			Collection<GenericParameter> genericParameters2 = imported.GenericParameters;
			for (int i = 0; i < genericParameters.Count; i++)
			{
				genericParameters2.Add(new GenericParameter(genericParameters[i].Name, imported));
			}
		}

		private TypeReference ImportTypeSpecification(TypeReference type, ImportGenericContext context)
		{
			switch (type.etype)
			{
			case ElementType.SzArray:
			{
				ArrayType arrayType = (ArrayType)type;
				return new ArrayType(ImportType(arrayType.ElementType, context));
			}
			case ElementType.Ptr:
			{
				PointerType pointerType = (PointerType)type;
				return new PointerType(ImportType(pointerType.ElementType, context));
			}
			case ElementType.ByRef:
			{
				ByReferenceType byReferenceType = (ByReferenceType)type;
				return new ByReferenceType(ImportType(byReferenceType.ElementType, context));
			}
			case ElementType.Pinned:
			{
				PinnedType pinnedType = (PinnedType)type;
				return new PinnedType(ImportType(pinnedType.ElementType, context));
			}
			case ElementType.Sentinel:
			{
				SentinelType sentinelType = (SentinelType)type;
				return new SentinelType(ImportType(sentinelType.ElementType, context));
			}
			case ElementType.FnPtr:
			{
				FunctionPointerType functionPointerType = (FunctionPointerType)type;
				FunctionPointerType functionPointerType2 = new FunctionPointerType
				{
					HasThis = functionPointerType.HasThis,
					ExplicitThis = functionPointerType.ExplicitThis,
					CallingConvention = functionPointerType.CallingConvention,
					ReturnType = ImportType(functionPointerType.ReturnType, context)
				};
				if (!functionPointerType.HasParameters)
				{
					return functionPointerType2;
				}
				for (int j = 0; j < functionPointerType.Parameters.Count; j++)
				{
					functionPointerType2.Parameters.Add(new ParameterDefinition(ImportType(functionPointerType.Parameters[j].ParameterType, context)));
				}
				return functionPointerType2;
			}
			case ElementType.CModOpt:
			{
				OptionalModifierType optionalModifierType = (OptionalModifierType)type;
				return new OptionalModifierType(ImportType(optionalModifierType.ModifierType, context), ImportType(optionalModifierType.ElementType, context));
			}
			case ElementType.CModReqD:
			{
				RequiredModifierType requiredModifierType = (RequiredModifierType)type;
				return new RequiredModifierType(ImportType(requiredModifierType.ModifierType, context), ImportType(requiredModifierType.ElementType, context));
			}
			case ElementType.Array:
			{
				ArrayType arrayType2 = (ArrayType)type;
				ArrayType arrayType3 = new ArrayType(ImportType(arrayType2.ElementType, context));
				if (arrayType2.IsVector)
				{
					return arrayType3;
				}
				Collection<ArrayDimension> dimensions = arrayType2.Dimensions;
				Collection<ArrayDimension> dimensions2 = arrayType3.Dimensions;
				dimensions2.Clear();
				for (int k = 0; k < dimensions.Count; k++)
				{
					ArrayDimension arrayDimension = dimensions[k];
					dimensions2.Add(new ArrayDimension(arrayDimension.LowerBound, arrayDimension.UpperBound));
				}
				return arrayType3;
			}
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)type;
				TypeReference type2 = ImportType(genericInstanceType.ElementType, context);
				Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
				GenericInstanceType genericInstanceType2 = new GenericInstanceType(type2, genericArguments.Count);
				Collection<TypeReference> genericArguments2 = genericInstanceType2.GenericArguments;
				for (int i = 0; i < genericArguments.Count; i++)
				{
					genericArguments2.Add(ImportType(genericArguments[i], context));
				}
				return genericInstanceType2;
			}
			case ElementType.Var:
			{
				GenericParameter genericParameter2 = (GenericParameter)type;
				if (genericParameter2.DeclaringType == null)
				{
					throw new InvalidOperationException();
				}
				return context.TypeParameter(genericParameter2.DeclaringType.FullName, genericParameter2.Position);
			}
			case ElementType.MVar:
			{
				GenericParameter genericParameter = (GenericParameter)type;
				if (genericParameter.DeclaringMethod == null)
				{
					throw new InvalidOperationException();
				}
				return context.MethodParameter(context.NormalizeMethodName(genericParameter.DeclaringMethod), genericParameter.Position);
			}
			default:
				throw new NotSupportedException(type.etype.ToString());
			}
		}

		private FieldReference ImportField(FieldReference field, ImportGenericContext context)
		{
			TypeReference typeReference = ImportType(field.DeclaringType, context);
			context.Push(typeReference);
			try
			{
				return new FieldReference
				{
					Name = field.Name,
					DeclaringType = typeReference,
					FieldType = ImportType(field.FieldType, context)
				};
			}
			finally
			{
				context.Pop();
			}
		}

		private MethodReference ImportMethod(MethodReference method, ImportGenericContext context)
		{
			if (method.IsGenericInstance)
			{
				return ImportMethodSpecification(method, context);
			}
			TypeReference declaringType = ImportType(method.DeclaringType, context);
			MethodReference methodReference = new MethodReference
			{
				Name = method.Name,
				HasThis = method.HasThis,
				ExplicitThis = method.ExplicitThis,
				DeclaringType = declaringType,
				CallingConvention = method.CallingConvention
			};
			if (method.HasGenericParameters)
			{
				ImportGenericParameters(methodReference, method);
			}
			context.Push(methodReference);
			try
			{
				methodReference.ReturnType = ImportType(method.ReturnType, context);
				if (!method.HasParameters)
				{
					return methodReference;
				}
				Collection<ParameterDefinition> parameters = method.Parameters;
				ParameterDefinitionCollection parameterDefinitionCollection = (methodReference.parameters = new ParameterDefinitionCollection(methodReference, parameters.Count));
				for (int i = 0; i < parameters.Count; i++)
				{
					parameterDefinitionCollection.Add(new ParameterDefinition(ImportType(parameters[i].ParameterType, context)));
				}
				return methodReference;
			}
			finally
			{
				context.Pop();
			}
		}

		private MethodSpecification ImportMethodSpecification(MethodReference method, ImportGenericContext context)
		{
			if (!method.IsGenericInstance)
			{
				throw new NotSupportedException();
			}
			GenericInstanceMethod genericInstanceMethod = (GenericInstanceMethod)method;
			MethodReference method2 = ImportMethod(genericInstanceMethod.ElementMethod, context);
			GenericInstanceMethod genericInstanceMethod2 = new GenericInstanceMethod(method2);
			Collection<TypeReference> genericArguments = genericInstanceMethod.GenericArguments;
			Collection<TypeReference> genericArguments2 = genericInstanceMethod2.GenericArguments;
			for (int i = 0; i < genericArguments.Count; i++)
			{
				genericArguments2.Add(ImportType(genericArguments[i], context));
			}
			return genericInstanceMethod2;
		}

		public virtual TypeReference ImportReference(TypeReference type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			return ImportType(type, ImportGenericContext.For(context));
		}

		public virtual FieldReference ImportReference(FieldReference field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			return ImportField(field, ImportGenericContext.For(context));
		}

		public virtual MethodReference ImportReference(MethodReference method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			return ImportMethod(method, ImportGenericContext.For(context));
		}
	}
	public sealed class LinkedResource : Resource
	{
		internal byte[] hash;

		private string file;

		public byte[] Hash => hash;

		public string File
		{
			get
			{
				return file;
			}
			set
			{
				file = value;
			}
		}

		public override ResourceType ResourceType => ResourceType.Linked;

		public LinkedResource(string name, ManifestResourceAttributes flags)
			: base(name, flags)
		{
		}

		public LinkedResource(string name, ManifestResourceAttributes flags, string file)
			: base(name, flags)
		{
			this.file = file;
		}
	}
	[Flags]
	public enum ManifestResourceAttributes : uint
	{
		VisibilityMask = 7u,
		Public = 1u,
		Private = 2u
	}
	public class MarshalInfo
	{
		internal NativeType native;

		public NativeType NativeType
		{
			get
			{
				return native;
			}
			set
			{
				native = value;
			}
		}

		public MarshalInfo(NativeType native)
		{
			this.native = native;
		}
	}
	public sealed class ArrayMarshalInfo : MarshalInfo
	{
		internal NativeType element_type;

		internal int size_parameter_index;

		internal int size;

		internal int size_parameter_multiplier;

		public NativeType ElementType
		{
			get
			{
				return element_type;
			}
			set
			{
				element_type = value;
			}
		}

		public int SizeParameterIndex
		{
			get
			{
				return size_parameter_index;
			}
			set
			{
				size_parameter_index = value;
			}
		}

		public int Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}

		public int SizeParameterMultiplier
		{
			get
			{
				return size_parameter_multiplier;
			}
			set
			{
				size_parameter_multiplier = value;
			}
		}

		public ArrayMarshalInfo()
			: base(NativeType.Array)
		{
			element_type = NativeType.None;
			size_parameter_index = -1;
			size = -1;
			size_parameter_multiplier = -1;
		}
	}
	public sealed class CustomMarshalInfo : MarshalInfo
	{
		internal Guid guid;

		internal string unmanaged_type;

		internal TypeReference managed_type;

		internal string cookie;

		public Guid Guid
		{
			get
			{
				return guid;
			}
			set
			{
				guid = value;
			}
		}

		public string UnmanagedType
		{
			get
			{
				return unmanaged_type;
			}
			set
			{
				unmanaged_type = value;
			}
		}

		public TypeReference ManagedType
		{
			get
			{
				return managed_type;
			}
			set
			{
				managed_type = value;
			}
		}

		public string Cookie
		{
			get
			{
				return cookie;
			}
			set
			{
				cookie = value;
			}
		}

		public CustomMarshalInfo()
			: base(NativeType.CustomMarshaler)
		{
		}
	}
	public sealed class SafeArrayMarshalInfo : MarshalInfo
	{
		internal VariantType element_type;

		public VariantType ElementType
		{
			get
			{
				return element_type;
			}
			set
			{
				element_type = value;
			}
		}

		public SafeArrayMarshalInfo()
			: base(NativeType.SafeArray)
		{
			element_type = VariantType.None;
		}
	}
	public sealed class FixedArrayMarshalInfo : MarshalInfo
	{
		internal NativeType element_type;

		internal int size;

		public NativeType ElementType
		{
			get
			{
				return element_type;
			}
			set
			{
				element_type = value;
			}
		}

		public int Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}

		public FixedArrayMarshalInfo()
			: base(NativeType.FixedArray)
		{
			element_type = NativeType.None;
		}
	}
	public sealed class FixedSysStringMarshalInfo : MarshalInfo
	{
		internal int size;

		public int Size
		{
			get
			{
				return size;
			}
			set
			{
				size = value;
			}
		}

		public FixedSysStringMarshalInfo()
			: base(NativeType.FixedSysString)
		{
			size = -1;
		}
	}
	internal sealed class MemberDefinitionCollection<T> : Collection<T> where T : IMemberDefinition
	{
		private TypeDefinition container;

		internal MemberDefinitionCollection(TypeDefinition container)
		{
			this.container = container;
		}

		internal MemberDefinitionCollection(TypeDefinition container, int capacity)
			: base(capacity)
		{
			this.container = container;
		}

		protected override void OnAdd(T item, int index)
		{
			Attach(item);
		}

		protected sealed override void OnSet(T item, int index)
		{
			Attach(item);
		}

		protected sealed override void OnInsert(T item, int index)
		{
			Attach(item);
		}

		protected sealed override void OnRemove(T item, int index)
		{
			Detach(item);
		}

		protected sealed override void OnClear()
		{
			using Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				T current = enumerator.Current;
				Detach(current);
			}
		}

		private void Attach(T element)
		{
			if (element.DeclaringType != container)
			{
				if (element.DeclaringType != null)
				{
					throw new ArgumentException("Member already attached");
				}
				TypeDefinition declaringType = container;
				element.DeclaringType = declaringType;
			}
		}

		private static void Detach(T element)
		{
			element.DeclaringType = null;
		}
	}
	public abstract class MemberReference : IMetadataTokenProvider
	{
		private string name;

		private TypeReference declaring_type;

		internal MetadataToken token;

		internal object projection;

		public virtual string Name
		{
			get
			{
				return name;
			}
			set
			{
				if (IsWindowsRuntimeProjection && value != name)
				{
					throw new InvalidOperationException();
				}
				name = value;
			}
		}

		public abstract string FullName { get; }

		public virtual TypeReference DeclaringType
		{
			get
			{
				return declaring_type;
			}
			set
			{
				declaring_type = value;
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		public bool IsWindowsRuntimeProjection => projection != null;

		internal bool HasImage => Module?.HasImage ?? false;

		public virtual ModuleDefinition Module => (declaring_type != null) ? declaring_type.Module : null;

		public virtual bool IsDefinition => false;

		public virtual bool ContainsGenericParameter => declaring_type != null && declaring_type.ContainsGenericParameter;

		internal MemberReference()
		{
		}

		internal MemberReference(string name)
		{
			this.name = name ?? string.Empty;
		}

		internal string MemberFullName()
		{
			if (declaring_type == null)
			{
				return name;
			}
			return declaring_type.FullName + "::" + name;
		}

		public IMemberDefinition Resolve()
		{
			return ResolveDefinition();
		}

		protected abstract IMemberDefinition ResolveDefinition();

		public override string ToString()
		{
			return FullName;
		}
	}
	public interface IAssemblyResolver : IDisposable
	{
		AssemblyDefinition Resolve(AssemblyNameReference name);

		AssemblyDefinition Resolve(AssemblyNameReference name, ReaderParameters parameters);
	}
	public interface IMetadataResolver
	{
		TypeDefinition Resolve(TypeReference type);

		FieldDefinition Resolve(FieldReference field);

		MethodDefinition Resolve(MethodReference method);
	}
	[Serializable]
	public sealed class ResolutionException : Exception
	{
		private readonly MemberReference member;

		public MemberReference Member => member;

		public IMetadataScope Scope
		{
			get
			{
				if (!(member is TypeReference { Scope: var scope }))
				{
					TypeReference declaringType = member.DeclaringType;
					if (declaringType != null)
					{
						return declaringType.Scope;
					}
					throw new NotSupportedException();
				}
				return scope;
			}
		}

		public ResolutionException(MemberReference member)
			: base("Failed to resolve " + member.FullName)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.member = member;
		}

		public ResolutionException(MemberReference member, Exception innerException)
			: base("Failed to resolve " + member.FullName, innerException)
		{
			if (member == null)
			{
				throw new ArgumentNullException("member");
			}
			this.member = member;
		}

		private ResolutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public class MetadataResolver : IMetadataResolver
	{
		private readonly IAssemblyResolver assembly_resolver;

		public IAssemblyResolver AssemblyResolver => assembly_resolver;

		public MetadataResolver(IAssemblyResolver assemblyResolver)
		{
			if (assemblyResolver == null)
			{
				throw new ArgumentNullException("assemblyResolver");
			}
			assembly_resolver = assemblyResolver;
		}

		public virtual TypeDefinition Resolve(TypeReference type)
		{
			Mixin.CheckType(type);
			type = type.GetElementType();
			IMetadataScope scope = type.Scope;
			if (scope == null)
			{
				return null;
			}
			switch (scope.MetadataScopeType)
			{
			case MetadataScopeType.AssemblyNameReference:
			{
				AssemblyDefinition assemblyDefinition = assembly_resolver.Resolve((AssemblyNameReference)scope);
				if (assemblyDefinition == null)
				{
					return null;
				}
				return GetType(assemblyDefinition.MainModule, type);
			}
			case MetadataScopeType.ModuleDefinition:
				return GetType((ModuleDefinition)scope, type);
			case MetadataScopeType.ModuleReference:
			{
				if (type.Module.Assembly == null)
				{
					return null;
				}
				Collection<ModuleDefinition> modules = type.Module.Assembly.Modules;
				ModuleReference moduleReference = (ModuleReference)scope;
				for (int i = 0; i < modules.Count; i++)
				{
					ModuleDefinition moduleDefinition = modules[i];
					if (moduleDefinition.Name == moduleReference.Name)
					{
						return GetType(moduleDefinition, type);
					}
				}
				break;
			}
			}
			throw new NotSupportedException();
		}

		private static TypeDefinition GetType(ModuleDefinition module, TypeReference reference)
		{
			TypeDefinition typeDefinition = GetTypeDefinition(module, reference);
			if (typeDefinition != null)
			{
				return typeDefinition;
			}
			if (!module.HasExportedTypes)
			{
				return null;
			}
			Collection<ExportedType> exportedTypes = module.ExportedTypes;
			for (int i = 0; i < exportedTypes.Count; i++)
			{
				ExportedType exportedType = exportedTypes[i];
				if (!(exportedType.Name != reference.Name) && !(exportedType.Namespace != reference.Namespace))
				{
					return exportedType.Resolve();
				}
			}
			return null;
		}

		private static TypeDefinition GetTypeDefinition(ModuleDefinition module, TypeReference type)
		{
			if (!type.IsNested)
			{
				return module.GetType(type.Namespace, type.Name);
			}
			return type.DeclaringType.Resolve()?.GetNestedType(type.TypeFullName());
		}

		public virtual FieldDefinition Resolve(FieldReference field)
		{
			Mixin.CheckField(field);
			TypeDefinition typeDefinition = Resolve(field.DeclaringType);
			if (typeDefinition == null)
			{
				return null;
			}
			if (!typeDefinition.HasFields)
			{
				return null;
			}
			return GetField(typeDefinition, field);
		}

		private FieldDefinition GetField(TypeDefinition type, FieldReference reference)
		{
			while (type != null)
			{
				FieldDefinition field = GetField(type.Fields, reference);
				if (field != null)
				{
					return field;
				}
				if (type.BaseType == null)
				{
					return null;
				}
				type = Resolve(type.BaseType);
			}
			return null;
		}

		private static FieldDefinition GetField(Collection<FieldDefinition> fields, FieldReference reference)
		{
			for (int i = 0; i < fields.Count; i++)
			{
				FieldDefinition fieldDefinition = fields[i];
				if (!(fieldDefinition.Name != reference.Name) && AreSame(fieldDefinition.FieldType, reference.FieldType))
				{
					return fieldDefinition;
				}
			}
			return null;
		}

		public virtual MethodDefinition Resolve(MethodReference method)
		{
			Mixin.CheckMethod(method);
			TypeDefinition typeDefinition = Resolve(method.DeclaringType);
			if (typeDefinition == null)
			{
				return null;
			}
			method = method.GetElementMethod();
			if (!typeDefinition.HasMethods)
			{
				return null;
			}
			if (method is MethodDefinition result)
			{
				return result;
			}
			return GetMethod(typeDefinition, method);
		}

		private MethodDefinition GetMethod(TypeDefinition type, MethodReference reference)
		{
			while (type != null)
			{
				MethodDefinition method = GetMethod(type.Methods, reference);
				if (method != null)
				{
					return method;
				}
				if (type.BaseType == null)
				{
					return null;
				}
				type = Resolve(type.BaseType);
			}
			return null;
		}

		public static MethodDefinition GetMethod(Collection<MethodDefinition> methods, MethodReference reference)
		{
			for (int i = 0; i < methods.Count; i++)
			{
				MethodDefinition methodDefinition = methods[i];
				if (methodDefinition.Name != reference.Name || methodDefinition.HasGenericParameters != reference.HasGenericParameters || (methodDefinition.HasGenericParameters && methodDefinition.GenericParameters.Count != reference.GenericParameters.Count) || !AreSame(methodDefinition.ReturnType, reference.ReturnType) || methodDefinition.HasThis != reference.HasThis || methodDefinition.IsVarArg() != reference.IsVarArg())
				{
					continue;
				}
				if (methodDefinition.IsVarArg() && IsVarArgCallTo(methodDefinition, reference))
				{
					return methodDefinition;
				}
				if (methodDefinition.HasParameters == reference.HasParameters)
				{
					if (!methodDefinition.HasParameters && !reference.HasParameters)
					{
						return methodDefinition;
					}
					if (AreSame(methodDefinition.Parameters, reference.Parameters))
					{
						return methodDefinition;
					}
				}
			}
			return null;
		}

		private static bool AreSame(Collection<ParameterDefinition> a, Collection<ParameterDefinition> b)
		{
			int count = a.Count;
			if (count != b.Count)
			{
				return false;
			}
			if (count == 0)
			{
				return true;
			}
			for (int i = 0; i < count; i++)
			{
				if (!AreSame(a[i].ParameterType, b[i].ParameterType))
				{
					return false;
				}
			}
			return true;
		}

		private static bool IsVarArgCallTo(MethodDefinition method, MethodReference reference)
		{
			if (method.Parameters.Count >= reference.Parameters.Count)
			{
				return false;
			}
			if (reference.GetSentinelPosition() != method.Parameters.Count)
			{
				return false;
			}
			for (int i = 0; i < method.Parameters.Count; i++)
			{
				if (!AreSame(method.Parameters[i].ParameterType, reference.Parameters[i].ParameterType))
				{
					return false;
				}
			}
			return true;
		}

		private static bool AreSame(TypeSpecification a, TypeSpecification b)
		{
			if (!AreSame(a.ElementType, b.ElementType))
			{
				return false;
			}
			if (a.IsGenericInstance)
			{
				return AreSame((GenericInstanceType)a, (GenericInstanceType)b);
			}
			if (a.IsRequiredModifier || a.IsOptionalModifier)
			{
				return AreSame((IModifierType)a, (IModifierType)b);
			}
			if (a.IsArray)
			{
				return AreSame((ArrayType)a, (ArrayType)b);
			}
			if (a.IsFunctionPointer)
			{
				return AreSame((FunctionPointerType)a, (FunctionPointerType)b);
			}
			return true;
		}

		private static bool AreSame(FunctionPointerType a, FunctionPointerType b)
		{
			if (a.HasThis != b.HasThis)
			{
				return false;
			}
			if (a.CallingConvention != b.CallingConvention)
			{
				return false;
			}
			if (!AreSame(a.ReturnType, b.ReturnType))
			{
				return false;
			}
			if (a.ContainsGenericParameter != b.ContainsGenericParameter)
			{
				return false;
			}
			if (a.HasParameters != b.HasParameters)
			{
				return false;
			}
			if (!a.HasParameters)
			{
				return true;
			}
			if (!AreSame(a.Parameters, b.Parameters))
			{
				return false;
			}
			return true;
		}

		private static bool AreSame(ArrayType a, ArrayType b)
		{
			if (a.Rank != b.Rank)
			{
				return false;
			}
			return true;
		}

		private static bool AreSame(IModifierType a, IModifierType b)
		{
			return AreSame(a.ModifierType, b.ModifierType);
		}

		private static bool AreSame(GenericInstanceType a, GenericInstanceType b)
		{
			if (a.GenericArguments.Count != b.GenericArguments.Count)
			{
				return false;
			}
			for (int i = 0; i < a.GenericArguments.Count; i++)
			{
				if (!AreSame(a.GenericArguments[i], b.GenericArguments[i]))
				{
					return false;
				}
			}
			return true;
		}

		private static bool AreSame(GenericParameter a, GenericParameter b)
		{
			return a.Position == b.Position;
		}

		private static bool AreSame(TypeReference a, TypeReference b)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			if (a.etype != b.etype)
			{
				return false;
			}
			if (a.IsGenericParameter)
			{
				return AreSame((GenericParameter)a, (GenericParameter)b);
			}
			if (a.IsTypeSpecification())
			{
				return AreSame((TypeSpecification)a, (TypeSpecification)b);
			}
			if (a.Name != b.Name || a.Namespace != b.Namespace)
			{
				return false;
			}
			return AreSame(a.DeclaringType, b.DeclaringType);
		}
	}
	internal struct Range
	{
		public uint Start;

		public uint Length;

		public Range(uint index, uint length)
		{
			Start = index;
			Length = length;
		}
	}
	internal sealed class MetadataSystem
	{
		internal AssemblyNameReference[] AssemblyReferences;

		internal ModuleReference[] ModuleReferences;

		internal TypeDefinition[] Types;

		internal TypeReference[] TypeReferences;

		internal FieldDefinition[] Fields;

		internal MethodDefinition[] Methods;

		internal MemberReference[] MemberReferences;

		internal Dictionary<uint, Collection<uint>> NestedTypes;

		internal Dictionary<uint, uint> ReverseNestedTypes;

		internal Dictionary<uint, Collection<Row<uint, MetadataToken>>> Interfaces;

		internal Dictionary<uint, Row<ushort, uint>> ClassLayouts;

		internal Dictionary<uint, uint> FieldLayouts;

		internal Dictionary<uint, uint> FieldRVAs;

		internal Dictionary<MetadataToken, uint> FieldMarshals;

		internal Dictionary<MetadataToken, Row<ElementType, uint>> Constants;

		internal Dictionary<uint, Collection<MetadataToken>> Overrides;

		internal Dictionary<MetadataToken, Range[]> CustomAttributes;

		internal Dictionary<MetadataToken, Range[]> SecurityDeclarations;

		internal Dictionary<uint, Range> Events;

		internal Dictionary<uint, Range> Properties;

		internal Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>> Semantics;

		internal Dictionary<uint, Row<PInvokeAttributes, uint, uint>> PInvokes;

		internal Dictionary<MetadataToken, Range[]> GenericParameters;

		internal Dictionary<uint, Collection<Row<uint, MetadataToken>>> GenericConstraints;

		internal Document[] Documents;

		internal Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>> LocalScopes;

		internal ImportDebugInformation[] ImportScopes;

		internal Dictionary<uint, uint> StateMachineMethods;

		internal Dictionary<MetadataToken, Row<Guid, uint, uint>[]> CustomDebugInformations;

		private static Dictionary<string, Row<ElementType, bool>> primitive_value_types;

		private static void InitializePrimitives()
		{
			Dictionary<string, Row<ElementType, bool>> value = new Dictionary<string, Row<ElementType, bool>>(18, StringComparer.Ordinal)
			{
				{
					"Void",
					new Row<ElementType, bool>(ElementType.Void, col2: false)
				},
				{
					"Boolean",
					new Row<ElementType, bool>(ElementType.Boolean, col2: true)
				},
				{
					"Char",
					new Row<ElementType, bool>(ElementType.Char, col2: true)
				},
				{
					"SByte",
					new Row<ElementType, bool>(ElementType.I1, col2: true)
				},
				{
					"Byte",
					new Row<ElementType, bool>(ElementType.U1, col2: true)
				},
				{
					"Int16",
					new Row<ElementType, bool>(ElementType.I2, col2: true)
				},
				{
					"UInt16",
					new Row<ElementType, bool>(ElementType.U2, col2: true)
				},
				{
					"Int32",
					new Row<ElementType, bool>(ElementType.I4, col2: true)
				},
				{
					"UInt32",
					new Row<ElementType, bool>(ElementType.U4, col2: true)
				},
				{
					"Int64",
					new Row<ElementType, bool>(ElementType.I8, col2: true)
				},
				{
					"UInt64",
					new Row<ElementType, bool>(ElementType.U8, col2: true)
				},
				{
					"Single",
					new Row<ElementType, bool>(ElementType.R4, col2: true)
				},
				{
					"Double",
					new Row<ElementType, bool>(ElementType.R8, col2: true)
				},
				{
					"String",
					new Row<ElementType, bool>(ElementType.String, col2: false)
				},
				{
					"TypedReference",
					new Row<ElementType, bool>(ElementType.TypedByRef, col2: false)
				},
				{
					"IntPtr",
					new Row<ElementType, bool>(ElementType.I, col2: true)
				},
				{
					"UIntPtr",
					new Row<ElementType, bool>(ElementType.U, col2: true)
				},
				{
					"Object",
					new Row<ElementType, bool>(ElementType.Object, col2: false)
				}
			};
			Interlocked.CompareExchange(ref primitive_value_types, value, null);
		}

		public static void TryProcessPrimitiveTypeReference(TypeReference type)
		{
			if (!(type.Namespace != "System"))
			{
				IMetadataScope scope = type.scope;
				if (scope != null && scope.MetadataScopeType == MetadataScopeType.AssemblyNameReference && TryGetPrimitiveData(type, out var primitive_data))
				{
					type.etype = primitive_data.Col1;
					type.IsValueType = primitive_data.Col2;
				}
			}
		}

		public static bool TryGetPrimitiveElementType(TypeDefinition type, out ElementType etype)
		{
			etype = ElementType.None;
			if (type.Namespace != "System")
			{
				return false;
			}
			if (TryGetPrimitiveData(type, out var primitive_data))
			{
				etype = primitive_data.Col1;
				return true;
			}
			return false;
		}

		private static bool TryGetPrimitiveData(TypeReference type, out Row<ElementType, bool> primitive_data)
		{
			if (primitive_value_types == null)
			{
				InitializePrimitives();
			}
			return primitive_value_types.TryGetValue(type.Name, out primitive_data);
		}

		public void Clear()
		{
			if (NestedTypes != null)
			{
				NestedTypes = new Dictionary<uint, Collection<uint>>(0);
			}
			if (ReverseNestedTypes != null)
			{
				ReverseNestedTypes = new Dictionary<uint, uint>(0);
			}
			if (Interfaces != null)
			{
				Interfaces = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(0);
			}
			if (ClassLayouts != null)
			{
				ClassLayouts = new Dictionary<uint, Row<ushort, uint>>(0);
			}
			if (FieldLayouts != null)
			{
				FieldLayouts = new Dictionary<uint, uint>(0);
			}
			if (FieldRVAs != null)
			{
				FieldRVAs = new Dictionary<uint, uint>(0);
			}
			if (FieldMarshals != null)
			{
				FieldMarshals = new Dictionary<MetadataToken, uint>(0);
			}
			if (Constants != null)
			{
				Constants = new Dictionary<MetadataToken, Row<ElementType, uint>>(0);
			}
			if (Overrides != null)
			{
				Overrides = new Dictionary<uint, Collection<MetadataToken>>(0);
			}
			if (CustomAttributes != null)
			{
				CustomAttributes = new Dictionary<MetadataToken, Range[]>(0);
			}
			if (SecurityDeclarations != null)
			{
				SecurityDeclarations = new Dictionary<MetadataToken, Range[]>(0);
			}
			if (Events != null)
			{
				Events = new Dictionary<uint, Range>(0);
			}
			if (Properties != null)
			{
				Properties = new Dictionary<uint, Range>(0);
			}
			if (Semantics != null)
			{
				Semantics = new Dictionary<uint, Row<MethodSemanticsAttributes, MetadataToken>>(0);
			}
			if (PInvokes != null)
			{
				PInvokes = new Dictionary<uint, Row<PInvokeAttributes, uint, uint>>(0);
			}
			if (GenericParameters != null)
			{
				GenericParameters = new Dictionary<MetadataToken, Range[]>(0);
			}
			if (GenericConstraints != null)
			{
				GenericConstraints = new Dictionary<uint, Collection<Row<uint, MetadataToken>>>(0);
			}
			Documents = Empty<Document>.Array;
			ImportScopes = Empty<ImportDebugInformation>.Array;
			if (LocalScopes != null)
			{
				LocalScopes = new Dictionary<uint, Collection<Row<uint, Range, Range, uint, uint, uint>>>(0);
			}
			if (StateMachineMethods != null)
			{
				StateMachineMethods = new Dictionary<uint, uint>(0);
			}
		}

		public AssemblyNameReference GetAssemblyNameReference(uint rid)
		{
			if (rid < 1 || rid > AssemblyReferences.Length)
			{
				return null;
			}
			return AssemblyReferences[rid - 1];
		}

		public TypeDefinition GetTypeDefinition(uint rid)
		{
			if (rid < 1 || rid > Types.Length)
			{
				return null;
			}
			return Types[rid - 1];
		}

		public void AddTypeDefinition(TypeDefinition type)
		{
			Types[type.token.RID - 1] = type;
		}

		public TypeReference GetTypeReference(uint rid)
		{
			if (rid < 1 || rid > TypeReferences.Length)
			{
				return null;
			}
			return TypeReferences[rid - 1];
		}

		public void AddTypeReference(TypeReference type)
		{
			TypeReferences[type.token.RID - 1] = type;
		}

		public FieldDefinition GetFieldDefinition(uint rid)
		{
			if (rid < 1 || rid > Fields.Length)
			{
				return null;
			}
			return Fields[rid - 1];
		}

		public void AddFieldDefinition(FieldDefinition field)
		{
			Fields[field.token.RID - 1] = field;
		}

		public MethodDefinition GetMethodDefinition(uint rid)
		{
			if (rid < 1 || rid > Methods.Length)
			{
				return null;
			}
			return Methods[rid - 1];
		}

		public void AddMethodDefinition(MethodDefinition method)
		{
			Methods[method.token.RID - 1] = method;
		}

		public MemberReference GetMemberReference(uint rid)
		{
			if (rid < 1 || rid > MemberReferences.Length)
			{
				return null;
			}
			return MemberReferences[rid - 1];
		}

		public void AddMemberReference(MemberReference member)
		{
			MemberReferences[member.token.RID - 1] = member;
		}

		public bool TryGetNestedTypeMapping(TypeDefinition type, out Collection<uint> mapping)
		{
			return NestedTypes.TryGetValue(type.token.RID, out mapping);
		}

		public void SetNestedTypeMapping(uint type_rid, Collection<uint> mapping)
		{
			NestedTypes[type_rid] = mapping;
		}

		public bool TryGetReverseNestedTypeMapping(TypeDefinition type, out uint declaring)
		{
			return ReverseNestedTypes.TryGetValue(type.token.RID, out declaring);
		}

		public void SetReverseNestedTypeMapping(uint nested, uint declaring)
		{
			ReverseNestedTypes[nested] = declaring;
		}

		public bool TryGetInterfaceMapping(TypeDefinition type, out Collection<Row<uint, MetadataToken>> mapping)
		{
			return Interfaces.TryGetValue(type.token.RID, out mapping);
		}

		public void SetInterfaceMapping(uint type_rid, Collection<Row<uint, MetadataToken>> mapping)
		{
			Interfaces[type_rid] = mapping;
		}

		public void AddPropertiesRange(uint type_rid, Range range)
		{
			Properties.Add(type_rid, range);
		}

		public bool TryGetPropertiesRange(TypeDefinition type, out Range range)
		{
			return Properties.TryGetValue(type.token.RID, out range);
		}

		public void AddEventsRange(uint type_rid, Range range)
		{
			Events.Add(type_rid, range);
		}

		public bool TryGetEventsRange(TypeDefinition type, out Range range)
		{
			return Events.TryGetValue(type.token.RID, out range);
		}

		public bool TryGetGenericParameterRanges(IGenericParameterProvider owner, out Range[] ranges)
		{
			return GenericParameters.TryGetValue(owner.MetadataToken, out ranges);
		}

		public bool TryGetCustomAttributeRanges(ICustomAttributeProvider owner, out Range[] ranges)
		{
			return CustomAttributes.TryGetValue(owner.MetadataToken, out ranges);
		}

		public bool TryGetSecurityDeclarationRanges(ISecurityDeclarationProvider owner, out Range[] ranges)
		{
			return SecurityDeclarations.TryGetValue(owner.MetadataToken, out ranges);
		}

		public bool TryGetGenericConstraintMapping(GenericParameter generic_parameter, out Collection<Row<uint, MetadataToken>> mapping)
		{
			return GenericConstraints.TryGetValue(generic_parameter.token.RID, out mapping);
		}

		public void SetGenericConstraintMapping(uint gp_rid, Collection<Row<uint, MetadataToken>> mapping)
		{
			GenericConstraints[gp_rid] = mapping;
		}

		public bool TryGetOverrideMapping(MethodDefinition method, out Collection<MetadataToken> mapping)
		{
			return Overrides.TryGetValue(method.token.RID, out mapping);
		}

		public void SetOverrideMapping(uint rid, Collection<MetadataToken> mapping)
		{
			Overrides[rid] = mapping;
		}

		public Document GetDocument(uint rid)
		{
			if (rid < 1 || rid > Documents.Length)
			{
				return null;
			}
			return Documents[rid - 1];
		}

		public bool TryGetLocalScopes(MethodDefinition method, out Collection<Row<uint, Range, Range, uint, uint, uint>> scopes)
		{
			return LocalScopes.TryGetValue(method.MetadataToken.RID, out scopes);
		}

		public void SetLocalScopes(uint method_rid, Collection<Row<uint, Range, Range, uint, uint, uint>> records)
		{
			LocalScopes[method_rid] = records;
		}

		public ImportDebugInformation GetImportScope(uint rid)
		{
			if (rid < 1 || rid > ImportScopes.Length)
			{
				return null;
			}
			return ImportScopes[rid - 1];
		}

		public bool TryGetStateMachineKickOffMethod(MethodDefinition method, out uint rid)
		{
			return StateMachineMethods.TryGetValue(method.MetadataToken.RID, out rid);
		}

		public TypeDefinition GetFieldDeclaringType(uint field_rid)
		{
			return BinaryRangeSearch(Types, field_rid, field: true);
		}

		public TypeDefinition GetMethodDeclaringType(uint method_rid)
		{
			return BinaryRangeSearch(Types, method_rid, field: false);
		}

		private static TypeDefinition BinaryRangeSearch(TypeDefinition[] types, uint rid, bool field)
		{
			int num = 0;
			int num2 = types.Length - 1;
			while (num <= num2)
			{
				int num3 = num + (num2 - num) / 2;
				TypeDefinition typeDefinition = types[num3];
				Range range = (field ? typeDefinition.fields_range : typeDefinition.methods_range);
				if (rid < range.Start)
				{
					num2 = num3 - 1;
					continue;
				}
				if (rid >= range.Start + range.Length)
				{
					num = num3 + 1;
					continue;
				}
				return typeDefinition;
			}
			return null;
		}
	}
	[Flags]
	public enum MethodAttributes : ushort
	{
		MemberAccessMask = 7,
		CompilerControlled = 0,
		Private = 1,
		FamANDAssem = 2,
		Assembly = 3,
		Family = 4,
		FamORAssem = 5,
		Public = 6,
		Static = 0x10,
		Final = 0x20,
		Virtual = 0x40,
		HideBySig = 0x80,
		VtableLayoutMask = 0x100,
		ReuseSlot = 0,
		NewSlot = 0x100,
		CheckAccessOnOverride = 0x200,
		Abstract = 0x400,
		SpecialName = 0x800,
		PInvokeImpl = 0x2000,
		UnmanagedExport = 8,
		RTSpecialName = 0x1000,
		HasSecurity = 0x4000,
		RequireSecObject = 0x8000
	}
	public enum MethodCallingConvention : byte
	{
		Default = 0,
		C = 1,
		StdCall = 2,
		ThisCall = 3,
		FastCall = 4,
		VarArg = 5,
		Unmanaged = 9,
		Generic = 16
	}
	public sealed class MethodDefinition : MethodReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider, ICustomDebugInformationProvider
	{
		private ushort attributes;

		private ushort impl_attributes;

		internal volatile bool sem_attrs_ready;

		internal MethodSemanticsAttributes sem_attrs;

		private Collection<CustomAttribute> custom_attributes;

		private Collection<SecurityDeclaration> security_declarations;

		internal uint rva;

		internal PInvokeInfo pinvoke;

		private Collection<MethodReference> overrides;

		internal Mono.Cecil.Cil.MethodBody body;

		internal MethodDebugInformation debug_info;

		internal Collection<CustomDebugInformation> custom_infos;

		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.Name)
				{
					throw new InvalidOperationException();
				}
				base.Name = value;
			}
		}

		public MethodAttributes Attributes
		{
			get
			{
				return (MethodAttributes)attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && (uint)value != attributes)
				{
					throw new InvalidOperationException();
				}
				attributes = (ushort)value;
			}
		}

		public MethodImplAttributes ImplAttributes
		{
			get
			{
				return (MethodImplAttributes)impl_attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && (uint)value != impl_attributes)
				{
					throw new InvalidOperationException();
				}
				impl_attributes = (ushort)value;
			}
		}

		public MethodSemanticsAttributes SemanticsAttributes
		{
			get
			{
				if (sem_attrs_ready)
				{
					return sem_attrs;
				}
				if (base.HasImage)
				{
					ReadSemantics();
					return sem_attrs;
				}
				sem_attrs = MethodSemanticsAttributes.None;
				sem_attrs_ready = true;
				return sem_attrs;
			}
			set
			{
				sem_attrs = value;
			}
		}

		internal MethodDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (MethodDefinitionProjection)projection;
			}
			set
			{
				projection = value;
			}
		}

		public bool HasSecurityDeclarations
		{
			get
			{
				if (security_declarations != null)
				{
					return security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(Module);
			}
		}

		public Collection<SecurityDeclaration> SecurityDeclarations => security_declarations ?? this.GetSecurityDeclarations(ref security_declarations, Module);

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, Module);

		public int RVA => (int)rva;

		public bool HasBody => (attributes & 0x400) == 0 && (attributes & 0x2000) == 0 && (impl_attributes & 0x1000) == 0 && (impl_attributes & 1) == 0 && (impl_attributes & 4) == 0 && (impl_attributes & 3) == 0;

		public Mono.Cecil.Cil.MethodBody Body
		{
			get
			{
				Mono.Cecil.Cil.MethodBody methodBody = body;
				if (methodBody != null)
				{
					return methodBody;
				}
				if (!HasBody)
				{
					return null;
				}
				if (base.HasImage && rva != 0)
				{
					return Module.Read(ref body, this, (MethodDefinition method, MetadataReader reader) => reader.ReadMethodBody(method));
				}
				Interlocked.CompareExchange(ref body, new Mono.Cecil.Cil.MethodBody(this), null);
				return body;
			}
			set
			{
				ModuleDefinition module = Module;
				if (module == null)
				{
					body = value;
					return;
				}
				lock (module.SyncRoot)
				{
					body = value;
					if (value == null)
					{
						debug_info = null;
					}
				}
			}
		}

		public MethodDebugInformation DebugInformation
		{
			get
			{
				Mixin.Read(Body);
				if (debug_info == null)
				{
					Interlocked.CompareExchange(ref debug_info, new MethodDebugInformation(this), null);
				}
				return debug_info;
			}
			set
			{
				debug_info = value;
			}
		}

		public bool HasPInvokeInfo
		{
			get
			{
				if (pinvoke != null)
				{
					return true;
				}
				return IsPInvokeImpl;
			}
		}

		public PInvokeInfo PInvokeInfo
		{
			get
			{
				if (pinvoke != null)
				{
					return pinvoke;
				}
				if (base.HasImage && IsPInvokeImpl)
				{
					return Module.Read(ref pinvoke, this, (MethodDefinition method, MetadataReader reader) => reader.ReadPInvokeInfo(method));
				}
				return null;
			}
			set
			{
				IsPInvokeImpl = true;
				pinvoke = value;
			}
		}

		public bool HasOverrides
		{
			get
			{
				if (overrides != null)
				{
					return overrides.Count > 0;
				}
				return base.HasImage && Module.Read(this, (MethodDefinition method, MetadataReader reader) => reader.HasOverrides(method));
			}
		}

		public Collection<MethodReference> Overrides
		{
			get
			{
				if (overrides != null)
				{
					return overrides;
				}
				if (base.HasImage)
				{
					return Module.Read(ref overrides, this, (MethodDefinition method, MetadataReader reader) => reader.ReadOverrides(method));
				}
				Interlocked.CompareExchange(ref overrides, new Collection<MethodReference>(), null);
				return overrides;
			}
		}

		public override bool HasGenericParameters
		{
			get
			{
				if (generic_parameters != null)
				{
					return generic_parameters.Count > 0;
				}
				return this.GetHasGenericParameters(Module);
			}
		}

		public override Collection<GenericParameter> GenericParameters => generic_parameters ?? this.GetGenericParameters(ref generic_parameters, Module);

		public bool HasCustomDebugInformations
		{
			get
			{
				Mixin.Read(Body);
				return !custom_infos.IsNullOrEmpty();
			}
		}

		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				Mixin.Read(Body);
				if (custom_infos == null)
				{
					Interlocked.CompareExchange(ref custom_infos, new Collection<CustomDebugInformation>(), null);
				}
				return custom_infos;
			}
		}

		public bool IsCompilerControlled
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 0u, value);
			}
		}

		public bool IsPrivate
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 1u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 1u, value);
			}
		}

		public bool IsFamilyAndAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 2u, value);
			}
		}

		public bool IsAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 3u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 3u, value);
			}
		}

		public bool IsFamily
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 4u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 4u, value);
			}
		}

		public bool IsFamilyOrAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 5u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 5u, value);
			}
		}

		public bool IsPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7, 6u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7, 6u, value);
			}
		}

		public bool IsStatic
		{
			get
			{
				return attributes.GetAttributes(16);
			}
			set
			{
				attributes = attributes.SetAttributes(16, value);
			}
		}

		public bool IsFinal
		{
			get
			{
				return attributes.GetAttributes(32);
			}
			set
			{
				attributes = attributes.SetAttributes(32, value);
			}
		}

		public bool IsVirtual
		{
			get
			{
				return attributes.GetAttributes(64);
			}
			set
			{
				attributes = attributes.SetAttributes(64, value);
			}
		}

		public bool IsHideBySig
		{
			get
			{
				return attributes.GetAttributes(128);
			}
			set
			{
				attributes = attributes.SetAttributes(128, value);
			}
		}

		public bool IsReuseSlot
		{
			get
			{
				return attributes.GetMaskedAttributes(256, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(256, 0u, value);
			}
		}

		public bool IsNewSlot
		{
			get
			{
				return attributes.GetMaskedAttributes(256, 256u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(256, 256u, value);
			}
		}

		public bool IsCheckAccessOnOverride
		{
			get
			{
				return attributes.GetAttributes(512);
			}
			set
			{
				attributes = attributes.SetAttributes(512, value);
			}
		}

		public bool IsAbstract
		{
			get
			{
				return attributes.GetAttributes(1024);
			}
			set
			{
				attributes = attributes.SetAttributes(1024, value);
			}
		}

		public bool IsSpecialName
		{
			get
			{
				return attributes.GetAttributes(2048);
			}
			set
			{
				attributes = attributes.SetAttributes(2048, value);
			}
		}

		public bool IsPInvokeImpl
		{
			get
			{
				return attributes.GetAttributes(8192);
			}
			set
			{
				attributes = attributes.SetAttributes(8192, value);
			}
		}

		public bool IsUnmanagedExport
		{
			get
			{
				return attributes.GetAttributes(8);
			}
			set
			{
				attributes = attributes.SetAttributes(8, value);
			}
		}

		public bool IsRuntimeSpecialName
		{
			get
			{
				return attributes.GetAttributes(4096);
			}
			set
			{
				attributes = attributes.SetAttributes(4096, value);
			}
		}

		public bool HasSecurity
		{
			get
			{
				return attributes.GetAttributes(16384);
			}
			set
			{
				attributes = attributes.SetAttributes(16384, value);
			}
		}

		public bool IsIL
		{
			get
			{
				return impl_attributes.GetMaskedAttributes(3, 0u);
			}
			set
			{
				impl_attributes = impl_attributes.SetMaskedAttributes(3, 0u, value);
			}
		}

		public bool IsNative
		{
			get
			{
				return impl_attributes.GetMaskedAttributes(3, 1u);
			}
			set
			{
				impl_attributes = impl_attributes.SetMaskedAttributes(3, 1u, value);
			}
		}

		public bool IsRuntime
		{
			get
			{
				return impl_attributes.GetMaskedAttributes(3, 3u);
			}
			set
			{
				impl_attributes = impl_attributes.SetMaskedAttributes(3, 3u, value);
			}
		}

		public bool IsUnmanaged
		{
			get
			{
				return impl_attributes.GetMaskedAttributes(4, 4u);
			}
			set
			{
				impl_attributes = impl_attributes.SetMaskedAttributes(4, 4u, value);
			}
		}

		public bool IsManaged
		{
			get
			{
				return impl_attributes.GetMaskedAttributes(4, 0u);
			}
			set
			{
				impl_attributes = impl_attributes.SetMaskedAttributes(4, 0u, value);
			}
		}

		public bool IsForwardRef
		{
			get
			{
				return impl_attributes.GetAttributes(16);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(16, value);
			}
		}

		public bool IsPreserveSig
		{
			get
			{
				return impl_attributes.GetAttributes(128);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(128, value);
			}
		}

		public bool IsInternalCall
		{
			get
			{
				return impl_attributes.GetAttributes(4096);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(4096, value);
			}
		}

		public bool IsSynchronized
		{
			get
			{
				return impl_attributes.GetAttributes(32);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(32, value);
			}
		}

		public bool NoInlining
		{
			get
			{
				return impl_attributes.GetAttributes(8);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(8, value);
			}
		}

		public bool NoOptimization
		{
			get
			{
				return impl_attributes.GetAttributes(64);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(64, value);
			}
		}

		public bool AggressiveInlining
		{
			get
			{
				return impl_attributes.GetAttributes(256);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(256, value);
			}
		}

		public bool AggressiveOptimization
		{
			get
			{
				return impl_attributes.GetAttributes(512);
			}
			set
			{
				impl_attributes = impl_attributes.SetAttributes(512, value);
			}
		}

		public bool IsSetter
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Setter);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Setter, value);
			}
		}

		public bool IsGetter
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Getter);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Getter, value);
			}
		}

		public bool IsOther
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Other);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Other, value);
			}
		}

		public bool IsAddOn
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.AddOn);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.AddOn, value);
			}
		}

		public bool IsRemoveOn
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.RemoveOn);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.RemoveOn, value);
			}
		}

		public bool IsFire
		{
			get
			{
				return this.GetSemantics(MethodSemanticsAttributes.Fire);
			}
			set
			{
				this.SetSemantics(MethodSemanticsAttributes.Fire, value);
			}
		}

		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		public bool IsConstructor => IsRuntimeSpecialName && IsSpecialName && (Name == ".cctor" || Name == ".ctor");

		public override bool IsDefinition => true;

		internal void ReadSemantics()
		{
			if (sem_attrs_ready)
			{
				return;
			}
			ModuleDefinition module = Module;
			if (module == null || !module.HasImage)
			{
				return;
			}
			lock (module.SyncRoot)
			{
				if (!sem_attrs_ready)
				{
					module.Read(this, delegate(MethodDefinition method, MetadataReader reader)
					{
						reader.ReadAllSemantics(method);
					});
				}
			}
		}

		internal MethodDefinition()
		{
			token = new MetadataToken(TokenType.Method);
		}

		public MethodDefinition(string name, MethodAttributes attributes, TypeReference returnType)
			: base(name, returnType)
		{
			this.attributes = (ushort)attributes;
			HasThis = !IsStatic;
			token = new MetadataToken(TokenType.Method);
		}

		public override MethodDefinition Resolve()
		{
			return this;
		}
	}
	[Flags]
	public enum MethodImplAttributes : ushort
	{
		CodeTypeMask = 3,
		IL = 0,
		Native = 1,
		OPTIL = 2,
		Runtime = 3,
		ManagedMask = 4,
		Unmanaged = 4,
		Managed = 0,
		ForwardRef = 0x10,
		PreserveSig = 0x80,
		InternalCall = 0x1000,
		Synchronized = 0x20,
		NoOptimization = 0x40,
		NoInlining = 8,
		AggressiveInlining = 0x100,
		AggressiveOptimization = 0x200
	}
	public class MethodReference : MemberReference, IMethodSignature, IMetadataTokenProvider, IGenericParameterProvider, IGenericContext
	{
		internal ParameterDefinitionCollection parameters;

		private MethodReturnType return_type;

		private bool has_this;

		private bool explicit_this;

		private MethodCallingConvention calling_convention;

		internal Collection<GenericParameter> generic_parameters;

		public virtual bool HasThis
		{
			get
			{
				return has_this;
			}
			set
			{
				has_this = value;
			}
		}

		public virtual bool ExplicitThis
		{
			get
			{
				return explicit_this;
			}
			set
			{
				explicit_this = value;
			}
		}

		public virtual MethodCallingConvention CallingConvention
		{
			get
			{
				return calling_convention;
			}
			set
			{
				calling_convention = value;
			}
		}

		public virtual bool HasParameters => !parameters.IsNullOrEmpty();

		public virtual Collection<ParameterDefinition> Parameters
		{
			get
			{
				if (parameters == null)
				{
					Interlocked.CompareExchange(ref parameters, new ParameterDefinitionCollection(this), null);
				}
				return parameters;
			}
		}

		IGenericParameterProvider IGenericContext.Type
		{
			get
			{
				TypeReference declaringType = DeclaringType;
				if (declaringType is GenericInstanceType genericInstanceType)
				{
					return genericInstanceType.ElementType;
				}
				return declaringType;
			}
		}

		IGenericParameterProvider IGenericContext.Method => this;

		GenericParameterType IGenericParameterProvider.GenericParameterType => GenericParameterType.Method;

		public virtual bool HasGenericParameters => !generic_parameters.IsNullOrEmpty();

		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (generic_parameters == null)
				{
					Interlocked.CompareExchange(ref generic_parameters, new GenericParameterCollection(this), null);
				}
				return generic_parameters;
			}
		}

		public TypeReference ReturnType
		{
			get
			{
				return MethodReturnType?.ReturnType;
			}
			set
			{
				MethodReturnType methodReturnType = MethodReturnType;
				if (methodReturnType != null)
				{
					methodReturnType.ReturnType = value;
				}
			}
		}

		public virtual MethodReturnType MethodReturnType
		{
			get
			{
				return return_type;
			}
			set
			{
				return_type = value;
			}
		}

		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(ReturnType.FullName).Append(" ").Append(MemberFullName());
				this.MethodSignatureFullName(stringBuilder);
				return stringBuilder.ToString();
			}
		}

		public virtual bool IsGenericInstance => false;

		public override bool ContainsGenericParameter
		{
			get
			{
				if (ReturnType.ContainsGenericParameter || base.ContainsGenericParameter)
				{
					return true;
				}
				if (!HasParameters)
				{
					return false;
				}
				Collection<ParameterDefinition> collection = Parameters;
				for (int i = 0; i < collection.Count; i++)
				{
					if (collection[i].ParameterType.ContainsGenericParameter)
					{
						return true;
					}
				}
				return false;
			}
		}

		internal MethodReference()
		{
			return_type = new MethodReturnType(this);
			token = new MetadataToken(TokenType.MemberRef);
		}

		public MethodReference(string name, TypeReference returnType)
			: base(name)
		{
			Mixin.CheckType(returnType, Mixin.Argument.returnType);
			return_type = new MethodReturnType(this);
			return_type.ReturnType = returnType;
			token = new MetadataToken(TokenType.MemberRef);
		}

		public MethodReference(string name, TypeReference returnType, TypeReference declaringType)
			: this(name, returnType)
		{
			Mixin.CheckType(declaringType, Mixin.Argument.declaringType);
			DeclaringType = declaringType;
		}

		public virtual MethodReference GetElementMethod()
		{
			return this;
		}

		protected override IMemberDefinition ResolveDefinition()
		{
			return Resolve();
		}

		public new virtual MethodDefinition Resolve()
		{
			ModuleDefinition module = Module;
			if (module == null)
			{
				throw new NotSupportedException();
			}
			return module.Resolve(this);
		}
	}
	internal sealed class MethodReferenceComparer : EqualityComparer<MethodReference>
	{
		[ThreadStatic]
		private static List<MethodReference> xComparisonStack;

		[ThreadStatic]
		private static List<MethodReference> yComparisonStack;

		public override bool Equals(MethodReference x, MethodReference y)
		{
			return AreEqual(x, y);
		}

		public override int GetHashCode(MethodReference obj)
		{
			return GetHashCodeFor(obj);
		}

		public static bool AreEqual(MethodReference x, MethodReference y)
		{
			if (x == y)
			{
				return true;
			}
			if (x.HasThis != y.HasThis)
			{
				return false;
			}
			if (x.HasParameters != y.HasParameters)
			{
				return false;
			}
			if (x.HasGenericParameters != y.HasGenericParameters)
			{
				return false;
			}
			if (x.Parameters.Count != y.Parameters.Count)
			{
				return false;
			}
			if (x.Name != y.Name)
			{
				return false;
			}
			if (!TypeReferenceEqualityComparer.AreEqual(x.DeclaringType, y.DeclaringType))
			{
				return false;
			}
			GenericInstanceMethod genericInstanceMethod = x as GenericInstanceMethod;
			GenericInstanceMethod genericInstanceMethod2 = y as GenericInstanceMethod;
			if (genericInstanceMethod != null || genericInstanceMethod2 != null)
			{
				if (genericInstanceMethod == null || genericInstanceMethod2 == null)
				{
					return false;
				}
				if (genericInstanceMethod.GenericArguments.Count != genericInstanceMethod2.GenericArguments.Count)
				{
					return false;
				}
				for (int i = 0; i < genericInstanceMethod.GenericArguments.Count; i++)
				{
					if (!TypeReferenceEqualityComparer.AreEqual(genericInstanceMethod.GenericArguments[i], genericInstanceMethod2.GenericArguments[i]))
					{
						return false;
					}
				}
			}
			MethodDefinition methodDefinition = x.Resolve();
			MethodDefinition methodDefinition2 = y.Resolve();
			if (methodDefinition != methodDefinition2)
			{
				return false;
			}
			if (methodDefinition == null)
			{
				if (xComparisonStack == null)
				{
					xComparisonStack = new List<MethodReference>();
				}
				if (yComparisonStack == null)
				{
					yComparisonStack = new List<MethodReference>();
				}
				for (int j = 0; j < xComparisonStack.Count; j++)
				{
					if (xComparisonStack[j] == x && yComparisonStack[j] == y)
					{
						return true;
					}
				}
				xComparisonStack.Add(x);
				try
				{
					yComparisonStack.Add(y);
					try
					{
						for (int k = 0; k < x.Parameters.Count; k++)
						{
							if (!TypeReferenceEqualityComparer.AreEqual(x.Parameters[k].ParameterType, y.Parameters[k].ParameterType))
							{
								return false;
							}
						}
					}
					finally
					{
						yComparisonStack.RemoveAt(yComparisonStack.Count - 1);
					}
				}
				finally
				{
					xComparisonStack.RemoveAt(xComparisonStack.Count - 1);
				}
			}
			return true;
		}

		public static bool AreSignaturesEqual(MethodReference x, MethodReference y, TypeComparisonMode comparisonMode = TypeComparisonMode.Exact)
		{
			if (x.HasThis != y.HasThis)
			{
				return false;
			}
			if (x.Parameters.Count != y.Parameters.Count)
			{
				return false;
			}
			if (x.GenericParameters.Count != y.GenericParameters.Count)
			{
				return false;
			}
			for (int i = 0; i < x.Parameters.Count; i++)
			{
				if (!TypeReferenceEqualityComparer.AreEqual(x.Parameters[i].ParameterType, y.Parameters[i].ParameterType, comparisonMode))
				{
					return false;
				}
			}
			if (!TypeReferenceEqualityComparer.AreEqual(x.ReturnType, y.ReturnType, comparisonMode))
			{
				return false;
			}
			return true;
		}

		public static int GetHashCodeFor(MethodReference obj)
		{
			if (obj is GenericInstanceMethod genericInstanceMethod)
			{
				int num = GetHashCodeFor(genericInstanceMethod.ElementMethod);
				for (int i = 0; i < genericInstanceMethod.GenericArguments.Count; i++)
				{
					num = num * 486187739 + TypeReferenceEqualityComparer.GetHashCodeFor(genericInstanceMethod.GenericArguments[i]);
				}
				return num;
			}
			return TypeReferenceEqualityComparer.GetHashCodeFor(obj.DeclaringType) * 486187739 + obj.Name.GetHashCode();
		}
	}
	public sealed class MethodReturnType : IConstantProvider, IMetadataTokenProvider, ICustomAttributeProvider, IMarshalInfoProvider
	{
		internal IMethodSignature method;

		internal ParameterDefinition parameter;

		private TypeReference return_type;

		public IMethodSignature Method => method;

		public TypeReference ReturnType
		{
			get
			{
				return return_type;
			}
			set
			{
				return_type = value;
			}
		}

		internal ParameterDefinition Parameter
		{
			get
			{
				if (parameter == null)
				{
					Interlocked.CompareExchange(ref parameter, new ParameterDefinition(return_type, method), null);
				}
				return parameter;
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return Parameter.MetadataToken;
			}
			set
			{
				Parameter.MetadataToken = value;
			}
		}

		public ParameterAttributes Attributes
		{
			get
			{
				return Parameter.Attributes;
			}
			set
			{
				Parameter.Attributes = value;
			}
		}

		public string Name
		{
			get
			{
				return Parameter.Name;
			}
			set
			{
				Parameter.Name = value;
			}
		}

		public bool HasCustomAttributes => parameter != null && parameter.HasCustomAttributes;

		public Collection<CustomAttribute> CustomAttributes => Parameter.CustomAttributes;

		public bool HasDefault
		{
			get
			{
				return parameter != null && parameter.HasDefault;
			}
			set
			{
				Parameter.HasDefault = value;
			}
		}

		public bool HasConstant
		{
			get
			{
				return parameter != null && parameter.HasConstant;
			}
			set
			{
				Parameter.HasConstant = value;
			}
		}

		public object Constant
		{
			get
			{
				return Parameter.Constant;
			}
			set
			{
				Parameter.Constant = value;
			}
		}

		public bool HasFieldMarshal
		{
			get
			{
				return parameter != null && parameter.HasFieldMarshal;
			}
			set
			{
				Parameter.HasFieldMarshal = value;
			}
		}

		public bool HasMarshalInfo => parameter != null && parameter.HasMarshalInfo;

		public MarshalInfo MarshalInfo
		{
			get
			{
				return Parameter.MarshalInfo;
			}
			set
			{
				Parameter.MarshalInfo = value;
			}
		}

		public MethodReturnType(IMethodSignature method)
		{
			this.method = method;
		}
	}
	[Flags]
	public enum MethodSemanticsAttributes : ushort
	{
		None = 0,
		Setter = 1,
		Getter = 2,
		Other = 4,
		AddOn = 8,
		RemoveOn = 0x10,
		Fire = 0x20
	}
	public abstract class MethodSpecification : MethodReference
	{
		private readonly MethodReference method;

		public MethodReference ElementMethod => method;

		public override string Name
		{
			get
			{
				return method.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override MethodCallingConvention CallingConvention
		{
			get
			{
				return method.CallingConvention;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool HasThis
		{
			get
			{
				return method.HasThis;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool ExplicitThis
		{
			get
			{
				return method.ExplicitThis;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override MethodReturnType MethodReturnType
		{
			get
			{
				return method.MethodReturnType;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override TypeReference DeclaringType
		{
			get
			{
				return method.DeclaringType;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override ModuleDefinition Module => method.Module;

		public override bool HasParameters => method.HasParameters;

		public override Collection<ParameterDefinition> Parameters => method.Parameters;

		public override bool ContainsGenericParameter => method.ContainsGenericParameter;

		internal MethodSpecification(MethodReference method)
		{
			Mixin.CheckMethod(method);
			this.method = method;
			token = new MetadataToken(TokenType.MethodSpec);
		}

		public sealed override MethodReference GetElementMethod()
		{
			return method.GetElementMethod();
		}
	}
	public interface IModifierType
	{
		TypeReference ModifierType { get; }

		TypeReference ElementType { get; }
	}
	public sealed class OptionalModifierType : TypeSpecification, IModifierType
	{
		private TypeReference modifier_type;

		public TypeReference ModifierType
		{
			get
			{
				return modifier_type;
			}
			set
			{
				modifier_type = value;
			}
		}

		public override string Name => base.Name + Suffix;

		public override string FullName => base.FullName + Suffix;

		private string Suffix => " modopt(" + modifier_type?.ToString() + ")";

		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsOptionalModifier => true;

		public override bool ContainsGenericParameter => modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;

		public OptionalModifierType(TypeReference modifierType, TypeReference type)
			: base(type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException(Mixin.Argument.modifierType.ToString());
			}
			Mixin.CheckType(type);
			modifier_type = modifierType;
			etype = Mono.Cecil.Metadata.ElementType.CModOpt;
		}
	}
	public sealed class RequiredModifierType : TypeSpecification, IModifierType
	{
		private TypeReference modifier_type;

		public TypeReference ModifierType
		{
			get
			{
				return modifier_type;
			}
			set
			{
				modifier_type = value;
			}
		}

		public override string Name => base.Name + Suffix;

		public override string FullName => base.FullName + Suffix;

		private string Suffix => " modreq(" + modifier_type?.ToString() + ")";

		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsRequiredModifier => true;

		public override bool ContainsGenericParameter => modifier_type.ContainsGenericParameter || base.ContainsGenericParameter;

		public RequiredModifierType(TypeReference modifierType, TypeReference type)
			: base(type)
		{
			if (modifierType == null)
			{
				throw new ArgumentNullException(Mixin.Argument.modifierType.ToString());
			}
			Mixin.CheckType(type);
			modifier_type = modifierType;
			etype = Mono.Cecil.Metadata.ElementType.CModReqD;
		}
	}
	public enum ReadingMode
	{
		Immediate = 1,
		Deferred
	}
	public sealed class ReaderParameters
	{
		private ReadingMode reading_mode;

		internal IAssemblyResolver assembly_resolver;

		internal IMetadataResolver metadata_resolver;

		internal IMetadataImporterProvider metadata_importer_provider;

		internal IReflectionImporterProvider reflection_importer_provider;

		private Stream symbol_stream;

		private ISymbolReaderProvider symbol_reader_provider;

		private bool read_symbols;

		private bool throw_symbols_mismatch;

		private bool projections;

		private bool in_memory;

		private bool read_write;

		public ReadingMode ReadingMode
		{
			get
			{
				return reading_mode;
			}
			set
			{
				reading_mode = value;
			}
		}

		public bool InMemory
		{
			get
			{
				return in_memory;
			}
			set
			{
				in_memory = value;
			}
		}

		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return assembly_resolver;
			}
			set
			{
				assembly_resolver = value;
			}
		}

		public IMetadataResolver MetadataResolver
		{
			get
			{
				return metadata_resolver;
			}
			set
			{
				metadata_resolver = value;
			}
		}

		public IMetadataImporterProvider MetadataImporterProvider
		{
			get
			{
				return metadata_importer_provider;
			}
			set
			{
				metadata_importer_provider = value;
			}
		}

		public IReflectionImporterProvider ReflectionImporterProvider
		{
			get
			{
				return reflection_importer_provider;
			}
			set
			{
				reflection_importer_provider = value;
			}
		}

		public Stream SymbolStream
		{
			get
			{
				return symbol_stream;
			}
			set
			{
				symbol_stream = value;
			}
		}

		public ISymbolReaderProvider SymbolReaderProvider
		{
			get
			{
				return symbol_reader_provider;
			}
			set
			{
				symbol_reader_provider = value;
			}
		}

		public bool ReadSymbols
		{
			get
			{
				return read_symbols;
			}
			set
			{
				read_symbols = value;
			}
		}

		public bool ThrowIfSymbolsAreNotMatching
		{
			get
			{
				return throw_symbols_mismatch;
			}
			set
			{
				throw_symbols_mismatch = value;
			}
		}

		public bool ReadWrite
		{
			get
			{
				return read_write;
			}
			set
			{
				read_write = value;
			}
		}

		public bool ApplyWindowsRuntimeProjections
		{
			get
			{
				return projections;
			}
			set
			{
				projections = value;
			}
		}

		public ReaderParameters()
			: this(ReadingMode.Deferred)
		{
		}

		public ReaderParameters(ReadingMode readingMode)
		{
			reading_mode = readingMode;
			throw_symbols_mismatch = true;
		}
	}
	public sealed class ModuleParameters
	{
		private ModuleKind kind;

		private TargetRuntime runtime;

		private uint? timestamp;

		private TargetArchitecture architecture;

		private IAssemblyResolver assembly_resolver;

		private IMetadataResolver metadata_resolver;

		private IMetadataImporterProvider metadata_importer_provider;

		private IReflectionImporterProvider reflection_importer_provider;

		public ModuleKind Kind
		{
			get
			{
				return kind;
			}
			set
			{
				kind = value;
			}
		}

		public TargetRuntime Runtime
		{
			get
			{
				return runtime;
			}
			set
			{
				runtime = value;
			}
		}

		public uint? Timestamp
		{
			get
			{
				return timestamp;
			}
			set
			{
				timestamp = value;
			}
		}

		public TargetArchitecture Architecture
		{
			get
			{
				return architecture;
			}
			set
			{
				architecture = value;
			}
		}

		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				return assembly_resolver;
			}
			set
			{
				assembly_resolver = value;
			}
		}

		public IMetadataResolver MetadataResolver
		{
			get
			{
				return metadata_resolver;
			}
			set
			{
				metadata_resolver = value;
			}
		}

		public IMetadataImporterProvider MetadataImporterProvider
		{
			get
			{
				return metadata_importer_provider;
			}
			set
			{
				metadata_importer_provider = value;
			}
		}

		public IReflectionImporterProvider ReflectionImporterProvider
		{
			get
			{
				return reflection_importer_provider;
			}
			set
			{
				reflection_importer_provider = value;
			}
		}

		public ModuleParameters()
		{
			kind = ModuleKind.Dll;
			Runtime = GetCurrentRuntime();
			architecture = TargetArchitecture.I386;
		}

		private static TargetRuntime GetCurrentRuntime()
		{
			return typeof(object).Assembly.ImageRuntimeVersion.ParseRuntime();
		}
	}
	public sealed class WriterParameters
	{
		private uint? timestamp;

		private Stream symbol_stream;

		private ISymbolWriterProvider symbol_writer_provider;

		private bool write_symbols;

		private byte[] key_blob;

		private string key_container;

		private StrongNameKeyPair key_pair;

		public uint? Timestamp
		{
			get
			{
				return timestamp;
			}
			set
			{
				timestamp = value;
			}
		}

		public Stream SymbolStream
		{
			get
			{
				return symbol_stream;
			}
			set
			{
				symbol_stream = value;
			}
		}

		public ISymbolWriterProvider SymbolWriterProvider
		{
			get
			{
				return symbol_writer_provider;
			}
			set
			{
				symbol_writer_provider = value;
			}
		}

		public bool WriteSymbols
		{
			get
			{
				return write_symbols;
			}
			set
			{
				write_symbols = value;
			}
		}

		public bool HasStrongNameKey => key_pair != null || key_blob != null || key_container != null;

		public byte[] StrongNameKeyBlob
		{
			get
			{
				return key_blob;
			}
			set
			{
				key_blob = value;
			}
		}

		public string StrongNameKeyContainer
		{
			get
			{
				return key_container;
			}
			set
			{
				key_container = value;
			}
		}

		public StrongNameKeyPair StrongNameKeyPair
		{
			get
			{
				return key_pair;
			}
			set
			{
				key_pair = value;
			}
		}

		public bool DeterministicMvid { get; set; }
	}
	public sealed class ModuleDefinition : ModuleReference, ICustomAttributeProvider, IMetadataTokenProvider, ICustomDebugInformationProvider, IDisposable
	{
		internal Image Image;

		internal MetadataSystem MetadataSystem;

		internal ReadingMode ReadingMode;

		internal ISymbolReaderProvider SymbolReaderProvider;

		internal ISymbolReader symbol_reader;

		internal Disposable<IAssemblyResolver> assembly_resolver;

		internal IMetadataResolver metadata_resolver;

		internal TypeSystem type_system;

		internal readonly MetadataReader reader;

		private readonly string file_name;

		internal string runtime_version;

		internal ModuleKind kind;

		private WindowsRuntimeProjections projections;

		private MetadataKind metadata_kind;

		private TargetRuntime runtime;

		private TargetArchitecture architecture;

		private ModuleAttributes attributes;

		private ModuleCharacteristics characteristics;

		private Guid mvid;

		internal ushort linker_version = 8;

		internal ushort subsystem_major = 4;

		internal ushort subsystem_minor = 0;

		internal uint timestamp;

		internal AssemblyDefinition assembly;

		private MethodDefinition entry_point;

		private bool entry_point_set;

		internal IReflectionImporter reflection_importer;

		internal IMetadataImporter metadata_importer;

		private Collection<CustomAttribute> custom_attributes;

		private Collection<AssemblyNameReference> references;

		private Collection<ModuleReference> modules;

		private Collection<Resource> resources;

		private Collection<ExportedType> exported_types;

		private TypeDefinitionCollection types;

		internal Collection<CustomDebugInformation> custom_infos;

		internal MetadataBuilder metadata_builder;

		private readonly object module_lock = new object();

		public bool IsMain => kind != ModuleKind.NetModule;

		public ModuleKind Kind
		{
			get
			{
				return kind;
			}
			set
			{
				kind = value;
			}
		}

		public MetadataKind MetadataKind
		{
			get
			{
				return metadata_kind;
			}
			set
			{
				metadata_kind = value;
			}
		}

		internal WindowsRuntimeProjections Projections
		{
			get
			{
				if (projections == null)
				{
					Interlocked.CompareExchange(ref projections, new WindowsRuntimeProjections(this), null);
				}
				return projections;
			}
		}

		public TargetRuntime Runtime
		{
			get
			{
				return runtime;
			}
			set
			{
				runtime = value;
				runtime_version = runtime.RuntimeVersionString();
			}
		}

		public string RuntimeVersion
		{
			get
			{
				return runtime_version;
			}
			set
			{
				runtime_version = value;
				runtime = runtime_version.ParseRuntime();
			}
		}

		public TargetArchitecture Architecture
		{
			get
			{
				return architecture;
			}
			set
			{
				architecture = value;
			}
		}

		public ModuleAttributes Attributes
		{
			get
			{
				return attributes;
			}
			set
			{
				attributes = value;
			}
		}

		public ModuleCharacteristics Characteristics
		{
			get
			{
				return characteristics;
			}
			set
			{
				characteristics = value;
			}
		}

		[Obsolete("Use FileName")]
		public string FullyQualifiedName => file_name;

		public string FileName => file_name;

		public Guid Mvid
		{
			get
			{
				return mvid;
			}
			set
			{
				mvid = value;
			}
		}

		internal bool HasImage => Image != null;

		public bool HasSymbols => symbol_reader != null;

		public ISymbolReader SymbolReader => symbol_reader;

		public override MetadataScopeType MetadataScopeType => MetadataScopeType.ModuleDefinition;

		public AssemblyDefinition Assembly => assembly;

		internal IReflectionImporter ReflectionImporter
		{
			get
			{
				if (reflection_importer == null)
				{
					Interlocked.CompareExchange(ref reflection_importer, new DefaultReflectionImporter(this), null);
				}
				return reflection_importer;
			}
		}

		internal IMetadataImporter MetadataImporter
		{
			get
			{
				if (metadata_importer == null)
				{
					Interlocked.CompareExchange(ref metadata_importer, new DefaultMetadataImporter(this), null);
				}
				return metadata_importer;
			}
		}

		public IAssemblyResolver AssemblyResolver
		{
			get
			{
				if (assembly_resolver.value == null)
				{
					lock (module_lock)
					{
						assembly_resolver = Disposable.Owned((IAssemblyResolver)new DefaultAssemblyResolver());
					}
				}
				return assembly_resolver.value;
			}
		}

		public IMetadataResolver MetadataResolver
		{
			get
			{
				if (metadata_resolver == null)
				{
					Interlocked.CompareExchange(ref metadata_resolver, new MetadataResolver(AssemblyResolver), null);
				}
				return metadata_resolver;
			}
		}

		public TypeSystem TypeSystem
		{
			get
			{
				if (type_system == null)
				{
					Interlocked.CompareExchange(ref type_system, TypeSystem.CreateTypeSystem(this), null);
				}
				return type_system;
			}
		}

		public bool HasAssemblyReferences
		{
			get
			{
				if (references != null)
				{
					return references.Count > 0;
				}
				return HasImage && Image.HasTable(Table.AssemblyRef);
			}
		}

		public Collection<AssemblyNameReference> AssemblyReferences
		{
			get
			{
				if (references != null)
				{
					return references;
				}
				if (HasImage)
				{
					return Read(ref references, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadAssemblyReferences());
				}
				Interlocked.CompareExchange(ref references, new Collection<AssemblyNameReference>(), null);
				return references;
			}
		}

		public bool HasModuleReferences
		{
			get
			{
				if (modules != null)
				{
					return modules.Count > 0;
				}
				return HasImage && Image.HasTable(Table.ModuleRef);
			}
		}

		public Collection<ModuleReference> ModuleReferences
		{
			get
			{
				if (modules != null)
				{
					return modules;
				}
				if (HasImage)
				{
					return Read(ref modules, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadModuleReferences());
				}
				Interlocked.CompareExchange(ref modules, new Collection<ModuleReference>(), null);
				return modules;
			}
		}

		public bool HasResources
		{
			get
			{
				if (resources != null)
				{
					return resources.Count > 0;
				}
				if (HasImage)
				{
					return Image.HasTable(Table.ManifestResource) || Read(this, (ModuleDefinition _, MetadataReader reader) => reader.HasFileResource());
				}
				return false;
			}
		}

		public Collection<Resource> Resources
		{
			get
			{
				if (resources != null)
				{
					return resources;
				}
				if (HasImage)
				{
					return Read(ref resources, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadResources());
				}
				Interlocked.CompareExchange(ref resources, new Collection<Resource>(), null);
				return resources;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(this);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, this);

		public bool HasTypes
		{
			get
			{
				if (types != null)
				{
					return types.Count > 0;
				}
				return HasImage && Image.HasTable(Table.TypeDef);
			}
		}

		public Collection<TypeDefinition> Types
		{
			get
			{
				if (types != null)
				{
					return types;
				}
				if (HasImage)
				{
					return Read(ref types, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadTypes());
				}
				Interlocked.CompareExchange(ref types, new TypeDefinitionCollection(this), null);
				return types;
			}
		}

		public bool HasExportedTypes
		{
			get
			{
				if (exported_types != null)
				{
					return exported_types.Count > 0;
				}
				return HasImage && Image.HasTable(Table.ExportedType);
			}
		}

		public Collection<ExportedType> ExportedTypes
		{
			get
			{
				if (exported_types != null)
				{
					return exported_types;
				}
				if (HasImage)
				{
					return Read(ref exported_types, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadExportedTypes());
				}
				Interlocked.CompareExchange(ref exported_types, new Collection<ExportedType>(), null);
				return exported_types;
			}
		}

		public MethodDefinition EntryPoint
		{
			get
			{
				if (entry_point_set)
				{
					return entry_point;
				}
				if (HasImage)
				{
					Read(ref entry_point, this, (ModuleDefinition _, MetadataReader reader) => reader.ReadEntryPoint());
				}
				else
				{
					entry_point = null;
				}
				entry_point_set = true;
				return entry_point;
			}
			set
			{
				entry_point = value;
				entry_point_set = true;
			}
		}

		public bool HasCustomDebugInformations => custom_infos != null && custom_infos.Count > 0;

		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				if (custom_infos == null)
				{
					Interlocked.CompareExchange(ref custom_infos, new Collection<CustomDebugInformation>(), null);
				}
				return custom_infos;
			}
		}

		internal object SyncRoot => module_lock;

		public bool HasDebugHeader => Image != null && Image.DebugHeader != null;

		internal ModuleDefinition()
		{
			MetadataSystem = new MetadataSystem();
			token = new MetadataToken(TokenType.Module, 1);
		}

		internal ModuleDefinition(Image image)
			: this()
		{
			Image = image;
			kind = image.Kind;
			RuntimeVersion = image.RuntimeVersion;
			architecture = image.Architecture;
			attributes = image.Attributes;
			characteristics = image.DllCharacteristics;
			linker_version = image.LinkerVersion;
			subsystem_major = image.SubSystemMajor;
			subsystem_minor = image.SubSystemMinor;
			file_name = image.FileName;
			timestamp = image.Timestamp;
			reader = new MetadataReader(this);
		}

		public void Dispose()
		{
			if (Image != null)
			{
				Image.Dispose();
			}
			if (symbol_reader != null)
			{
				symbol_reader.Dispose();
			}
			if (assembly_resolver.value != null)
			{
				assembly_resolver.Dispose();
			}
		}

		public bool HasTypeReference(string fullName)
		{
			return HasTypeReference(string.Empty, fullName);
		}

		public bool HasTypeReference(string scope, string fullName)
		{
			Mixin.CheckFullName(fullName);
			if (!HasImage)
			{
				return false;
			}
			return GetTypeReference(scope, fullName) != null;
		}

		public bool TryGetTypeReference(string fullName, out TypeReference type)
		{
			return TryGetTypeReference(string.Empty, fullName, out type);
		}

		public bool TryGetTypeReference(string scope, string fullName, out TypeReference type)
		{
			Mixin.CheckFullName(fullName);
			if (!HasImage)
			{
				type = null;
				return false;
			}
			return (type = GetTypeReference(scope, fullName)) != null;
		}

		private TypeReference GetTypeReference(string scope, string fullname)
		{
			return Read(new Row<string, string>(scope, fullname), (Row<string, string> row, MetadataReader reader) => reader.GetTypeReference(row.Col1, row.Col2));
		}

		public IEnumerable<TypeReference> GetTypeReferences()
		{
			if (!HasImage)
			{
				return Empty<TypeReference>.Array;
			}
			return Read(this, (ModuleDefinition _, MetadataReader reader) => reader.GetTypeReferences());
		}

		public IEnumerable<MemberReference> GetMemberReferences()
		{
			if (!HasImage)
			{
				return Empty<MemberReference>.Array;
			}
			return Read(this, (ModuleDefinition _, MetadataReader reader) => reader.GetMemberReferences());
		}

		public IEnumerable<CustomAttribute> GetCustomAttributes()
		{
			if (!HasImage)
			{
				return Empty<CustomAttribute>.Array;
			}
			return Read(this, (ModuleDefinition _, MetadataReader reader) => reader.GetCustomAttributes());
		}

		public TypeReference GetType(string fullName, bool runtimeName)
		{
			return runtimeName ? TypeParser.ParseType(this, fullName, typeDefinitionOnly: true) : GetType(fullName);
		}

		public TypeDefinition GetType(string fullName)
		{
			Mixin.CheckFullName(fullName);
			int num = fullName.IndexOf('/');
			if (num > 0)
			{
				return GetNestedType(fullName);
			}
			return ((TypeDefinitionCollection)Types).GetType(fullName);
		}

		public TypeDefinition GetType(string @namespace, string name)
		{
			Mixin.CheckName(name);
			return ((TypeDefinitionCollection)Types).GetType(@namespace ?? string.Empty, name);
		}

		public void RebuildNameCache()
		{
			((TypeDefinitionCollection)Types).RebuildNameCache();
		}

		public IEnumerable<TypeDefinition> GetTypes()
		{
			return GetTypes(Types);
		}

		private static IEnumerable<TypeDefinition> GetTypes(Collection<TypeDefinition> types)
		{
			for (int i = 0; i < types.Count; i++)
			{
				TypeDefinition type = types[i];
				yield return type;
				if (!type.HasNestedTypes)
				{
					continue;
				}
				foreach (TypeDefinition type2 in GetTypes(type.NestedTypes))
				{
					yield return type2;
				}
			}
		}

		private TypeDefinition GetNestedType(string fullname)
		{
			string[] array = fullname.Split(new char[1] { '/' });
			TypeDefinition typeDefinition = GetType(array[0]);
			if (typeDefinition == null)
			{
				return null;
			}
			for (int i = 1; i < array.Length; i++)
			{
				TypeDefinition nestedType = typeDefinition.GetNestedType(array[i]);
				if (nestedType == null)
				{
					return null;
				}
				typeDefinition = nestedType;
			}
			return typeDefinition;
		}

		internal FieldDefinition Resolve(FieldReference field)
		{
			return MetadataResolver.Resolve(field);
		}

		internal MethodDefinition Resolve(MethodReference method)
		{
			return MetadataResolver.Resolve(method);
		}

		internal TypeDefinition Resolve(TypeReference type)
		{
			return MetadataResolver.Resolve(type);
		}

		private static void CheckContext(IGenericParameterProvider context, ModuleDefinition module)
		{
			if (context == null || context.Module == module)
			{
				return;
			}
			throw new ArgumentException();
		}

		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(Type type)
		{
			return ImportReference(type, null);
		}

		public TypeReference ImportReference(Type type)
		{
			return ImportReference(type, null);
		}

		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(Type type, IGenericParameterProvider context)
		{
			return ImportReference(type, context);
		}

		public TypeReference ImportReference(Type type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			CheckContext(context, this);
			return ReflectionImporter.ImportReference(type, context);
		}

		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldInfo field)
		{
			return ImportReference(field, null);
		}

		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldInfo field, IGenericParameterProvider context)
		{
			return ImportReference(field, context);
		}

		public FieldReference ImportReference(FieldInfo field)
		{
			return ImportReference(field, null);
		}

		public FieldReference ImportReference(FieldInfo field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			CheckContext(context, this);
			return ReflectionImporter.ImportReference(field, context);
		}

		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodBase method)
		{
			return ImportReference(method, null);
		}

		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodBase method, IGenericParameterProvider context)
		{
			return ImportReference(method, context);
		}

		public MethodReference ImportReference(MethodBase method)
		{
			return ImportReference(method, null);
		}

		public MethodReference ImportReference(MethodBase method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			CheckContext(context, this);
			return ReflectionImporter.ImportReference(method, context);
		}

		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(TypeReference type)
		{
			return ImportReference(type, null);
		}

		[Obsolete("Use ImportReference", false)]
		public TypeReference Import(TypeReference type, IGenericParameterProvider context)
		{
			return ImportReference(type, context);
		}

		public TypeReference ImportReference(TypeReference type)
		{
			return ImportReference(type, null);
		}

		public TypeReference ImportReference(TypeReference type, IGenericParameterProvider context)
		{
			Mixin.CheckType(type);
			if (type.Module == this)
			{
				return type;
			}
			CheckContext(context, this);
			return MetadataImporter.ImportReference(type, context);
		}

		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldReference field)
		{
			return ImportReference(field, null);
		}

		[Obsolete("Use ImportReference", false)]
		public FieldReference Import(FieldReference field, IGenericParameterProvider context)
		{
			return ImportReference(field, context);
		}

		public FieldReference ImportReference(FieldReference field)
		{
			return ImportReference(field, null);
		}

		public FieldReference ImportReference(FieldReference field, IGenericParameterProvider context)
		{
			Mixin.CheckField(field);
			if (field.Module == this)
			{
				return field;
			}
			CheckContext(context, this);
			return MetadataImporter.ImportReference(field, context);
		}

		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodReference method)
		{
			return ImportReference(method, null);
		}

		[Obsolete("Use ImportReference", false)]
		public MethodReference Import(MethodReference method, IGenericParameterProvider context)
		{
			return ImportReference(method, context);
		}

		public MethodReference ImportReference(MethodReference method)
		{
			return ImportReference(method, null);
		}

		public MethodReference ImportReference(MethodReference method, IGenericParameterProvider context)
		{
			Mixin.CheckMethod(method);
			if (method.Module == this)
			{
				return method;
			}
			CheckContext(context, this);
			return MetadataImporter.ImportReference(method, context);
		}

		public IMetadataTokenProvider LookupToken(int token)
		{
			return LookupToken(new MetadataToken((uint)token));
		}

		public IMetadataTokenProvider LookupToken(MetadataToken token)
		{
			return Read(token, (MetadataToken t, MetadataReader reader) => reader.LookupToken(t));
		}

		public void ImmediateRead()
		{
			if (HasImage)
			{
				ReadingMode = ReadingMode.Immediate;
				ImmediateModuleReader immediateModuleReader = new ImmediateModuleReader(Image);
				immediateModuleReader.ReadModule(this, resolve_attributes: true);
			}
		}

		internal void Read<TItem>(TItem item, Action<TItem, MetadataReader> read)
		{
			lock (module_lock)
			{
				int position = reader.position;
				IGenericContext context = reader.context;
				read(item, reader);
				reader.position = position;
				reader.context = context;
			}
		}

		internal TRet Read<TItem, TRet>(TItem item, Func<TItem, MetadataReader, TRet> read)
		{
			lock (module_lock)
			{
				int position = reader.position;
				IGenericContext context = reader.context;
				TRet result = read(item, reader);
				reader.position = position;
				reader.context = context;
				return result;
			}
		}

		internal TRet Read<TItem, TRet>(ref TRet variable, TItem item, Func<TItem, MetadataReader, TRet> read) where TRet : class
		{
			lock (module_lock)
			{
				if (variable != null)
				{
					return variable;
				}
				int position = reader.position;
				IGenericContext context = reader.context;
				TRet val = read(item, reader);
				reader.position = position;
				reader.context = context;
				return variable = val;
			}
		}

		public ImageDebugHeader GetDebugHeader()
		{
			return Image.DebugHeader ?? new ImageDebugHeader();
		}

		public static ModuleDefinition CreateModule(string name, ModuleKind kind)
		{
			return CreateModule(name, new ModuleParameters
			{
				Kind = kind
			});
		}

		public static ModuleDefinition CreateModule(string name, ModuleParameters parameters)
		{
			Mixin.CheckName(name);
			Mixin.CheckParameters(parameters);
			ModuleDefinition moduleDefinition = new ModuleDefinition
			{
				Name = name,
				kind = parameters.Kind,
				timestamp = (parameters.Timestamp ?? Mixin.GetTimestamp()),
				Runtime = parameters.Runtime,
				architecture = parameters.Architecture,
				mvid = Guid.NewGuid(),
				Attributes = ModuleAttributes.ILOnly,
				Characteristics = (ModuleCharacteristics.DynamicBase | ModuleCharacteristics.NoSEH | ModuleCharacteristics.NXCompat | ModuleCharacteristics.TerminalServerAware)
			};
			if (parameters.AssemblyResolver != null)
			{
				moduleDefinition.assembly_resolver = Disposable.NotOwned(parameters.AssemblyResolver);
			}
			if (parameters.MetadataResolver != null)
			{
				moduleDefinition.metadata_resolver = parameters.MetadataResolver;
			}
			if (parameters.MetadataImporterProvider != null)
			{
				moduleDefinition.metadata_importer = parameters.MetadataImporterProvider.GetMetadataImporter(moduleDefinition);
			}
			if (parameters.ReflectionImporterProvider != null)
			{
				moduleDefinition.reflection_importer = parameters.ReflectionImporterProvider.GetReflectionImporter(moduleDefinition);
			}
			if (parameters.Kind != ModuleKind.NetModule)
			{
				AssemblyDefinition assemblyDefinition = (moduleDefinition.assembly = new AssemblyDefinition());
				moduleDefinition.assembly.Name = CreateAssemblyName(name);
				assemblyDefinition.main_module = moduleDefinition;
			}
			moduleDefinition.Types.Add(new TypeDefinition(string.Empty, "<Module>", TypeAttributes.NotPublic));
			return moduleDefinition;
		}

		private static AssemblyNameDefinition CreateAssemblyName(string name)
		{
			if (name.EndsWith(".dll") || name.EndsWith(".exe"))
			{
				name = name.Substring(0, name.Length - 4);
			}
			return new AssemblyNameDefinition(name, Mixin.ZeroVersion);
		}

		public void ReadSymbols()
		{
			if (string.IsNullOrEmpty(file_name))
			{
				throw new InvalidOperationException();
			}
			DefaultSymbolReaderProvider defaultSymbolReaderProvider = new DefaultSymbolReaderProvider(throwIfNoSymbol: true);
			ReadSymbols(defaultSymbolReaderProvider.GetSymbolReader(this, file_name), throwIfSymbolsAreNotMaching: true);
		}

		public void ReadSymbols(ISymbolReader reader)
		{
			ReadSymbols(reader, throwIfSymbolsAreNotMaching: true);
		}

		public void ReadSymbols(ISymbolReader reader, bool throwIfSymbolsAreNotMaching)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			symbol_reader = reader;
			if (!symbol_reader.ProcessDebugHeader(GetDebugHeader()))
			{
				symbol_reader = null;
				if (throwIfSymbolsAreNotMaching)
				{
					throw new SymbolsNotMatchingException("Symbols were found but are not matching the assembly");
				}
			}
			else if (HasImage && ReadingMode == ReadingMode.Immediate)
			{
				ImmediateModuleReader immediateModuleReader = new ImmediateModuleReader(Image);
				immediateModuleReader.ReadSymbols(this);
			}
		}

		public static ModuleDefinition ReadModule(string fileName)
		{
			return ReadModule(fileName, new ReaderParameters(ReadingMode.Deferred));
		}

		public static ModuleDefinition ReadModule(string fileName, ReaderParameters parameters)
		{
			Stream stream = GetFileStream(fileName, FileMode.Open, (!parameters.ReadWrite) ? FileAccess.Read : FileAccess.ReadWrite, FileShare.Read);
			if (parameters.InMemory)
			{
				MemoryStream memoryStream = new MemoryStream((int)(stream.CanSeek ? stream.Length : 0));
				using (stream)
				{
					stream.CopyTo(memoryStream);
				}
				memoryStream.Position = 0L;
				stream = memoryStream;
			}
			try
			{
				return ReadModule(Disposable.Owned(stream), fileName, parameters);
			}
			catch (Exception)
			{
				stream.Dispose();
				throw;
			}
		}

		private static Stream GetFileStream(string fileName, FileMode mode, FileAccess access, FileShare share)
		{
			Mixin.CheckFileName(fileName);
			return new FileStream(fileName, mode, access, share);
		}

		public static ModuleDefinition ReadModule(Stream stream)
		{
			return ReadModule(stream, new ReaderParameters(ReadingMode.Deferred));
		}

		public static ModuleDefinition ReadModule(Stream stream, ReaderParameters parameters)
		{
			Mixin.CheckStream(stream);
			Mixin.CheckReadSeek(stream);
			return ReadModule(Disposable.NotOwned(stream), stream.GetFileName(), parameters);
		}

		private static ModuleDefinition ReadModule(Disposable<Stream> stream, string fileName, ReaderParameters parameters)
		{
			Mixin.CheckParameters(parameters);
			return ModuleReader.CreateModule(ImageReader.ReadImage(stream, fileName), parameters);
		}

		public void Write(string fileName)
		{
			Write(fileName, new WriterParameters());
		}

		public void Write(string fileName, WriterParameters parameters)
		{
			Mixin.CheckParameters(parameters);
			Stream fileStream = GetFileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read);
			ModuleWriter.WriteModule(this, Disposable.Owned(fileStream), parameters);
		}

		public void Write()
		{
			Write(new WriterParameters());
		}

		public void Write(WriterParameters parameters)
		{
			if (!HasImage)
			{
				throw new InvalidOperationException();
			}
			Write(Image.Stream.value, parameters);
		}

		public void Write(Stream stream)
		{
			Write(stream, new WriterParameters());
		}

		public void Write(Stream stream, WriterParameters parameters)
		{
			Mixin.CheckStream(stream);
			Mixin.CheckWriteSeek(stream);
			Mixin.CheckParameters(parameters);
			ModuleWriter.WriteModule(this, Disposable.NotOwned(stream), parameters);
		}
	}
	public enum ModuleKind
	{
		Dll,
		Console,
		Windows,
		NetModule
	}
	public enum MetadataKind
	{
		Ecma335,
		WindowsMetadata,
		ManagedWindowsMetadata
	}
	public enum TargetArchitecture
	{
		I386 = 332,
		AMD64 = 34404,
		IA64 = 512,
		ARM = 448,
		ARMv7 = 452,
		ARM64 = 43620
	}
	[Flags]
	public enum ModuleAttributes
	{
		ILOnly = 1,
		Required32Bit = 2,
		ILLibrary = 4,
		StrongNameSigned = 8,
		Preferred32Bit = 0x20000
	}
	[Flags]
	public enum ModuleCharacteristics
	{
		HighEntropyVA = 0x20,
		DynamicBase = 0x40,
		NoSEH = 0x400,
		NXCompat = 0x100,
		AppContainer = 0x1000,
		TerminalServerAware = 0x8000
	}
	public class ModuleReference : IMetadataScope, IMetadataTokenProvider
	{
		private string name;

		internal MetadataToken token;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public virtual MetadataScopeType MetadataScopeType => MetadataScopeType.ModuleReference;

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		internal ModuleReference()
		{
			token = new MetadataToken(TokenType.ModuleRef);
		}

		public ModuleReference(string name)
			: this()
		{
			this.name = name;
		}

		public override string ToString()
		{
			return name;
		}
	}
	public enum NativeType
	{
		None = 102,
		Boolean = 2,
		I1 = 3,
		U1 = 4,
		I2 = 5,
		U2 = 6,
		I4 = 7,
		U4 = 8,
		I8 = 9,
		U8 = 10,
		R4 = 11,
		R8 = 12,
		LPStr = 20,
		Int = 31,
		UInt = 32,
		Func = 38,
		Array = 42,
		Currency = 15,
		BStr = 19,
		LPWStr = 21,
		LPTStr = 22,
		FixedSysString = 23,
		IUnknown = 25,
		IDispatch = 26,
		Struct = 27,
		IntF = 28,
		SafeArray = 29,
		FixedArray = 30,
		ByValStr = 34,
		ANSIBStr = 35,
		TBStr = 36,
		VariantBool = 37,
		ASAny = 40,
		LPStruct = 43,
		CustomMarshaler = 44,
		Error = 45,
		Max = 80
	}
	[Flags]
	public enum ParameterAttributes : ushort
	{
		None = 0,
		In = 1,
		Out = 2,
		Lcid = 4,
		Retval = 8,
		Optional = 0x10,
		HasDefault = 0x1000,
		HasFieldMarshal = 0x2000,
		Unused = 0xCFE0
	}
	public sealed class ParameterDefinition : ParameterReference, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider, IMarshalInfoProvider
	{
		private ushort attributes;

		internal IMethodSignature method;

		private object constant = Mixin.NotResolved;

		private Collection<CustomAttribute> custom_attributes;

		private MarshalInfo marshal_info;

		public ParameterAttributes Attributes
		{
			get
			{
				return (ParameterAttributes)attributes;
			}
			set
			{
				attributes = (ushort)value;
			}
		}

		public IMethodSignature Method => method;

		public int Sequence
		{
			get
			{
				if (method == null)
				{
					return -1;
				}
				return method.HasImplicitThis() ? (index + 1) : index;
			}
		}

		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref constant, parameter_type.Module);
				return constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					constant = Mixin.NoValue;
				}
			}
		}

		public object Constant
		{
			get
			{
				return HasConstant ? constant : null;
			}
			set
			{
				constant = value;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(parameter_type.Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, parameter_type.Module);

		public bool HasMarshalInfo
		{
			get
			{
				if (marshal_info != null)
				{
					return true;
				}
				return this.GetHasMarshalInfo(parameter_type.Module);
			}
		}

		public MarshalInfo MarshalInfo
		{
			get
			{
				return marshal_info ?? this.GetMarshalInfo(ref marshal_info, parameter_type.Module);
			}
			set
			{
				marshal_info = value;
			}
		}

		public bool IsIn
		{
			get
			{
				return attributes.GetAttributes(1);
			}
			set
			{
				attributes = attributes.SetAttributes(1, value);
			}
		}

		public bool IsOut
		{
			get
			{
				return attributes.GetAttributes(2);
			}
			set
			{
				attributes = attributes.SetAttributes(2, value);
			}
		}

		public bool IsLcid
		{
			get
			{
				return attributes.GetAttributes(4);
			}
			set
			{
				attributes = attributes.SetAttributes(4, value);
			}
		}

		public bool IsReturnValue
		{
			get
			{
				return attributes.GetAttributes(8);
			}
			set
			{
				attributes = attributes.SetAttributes(8, value);
			}
		}

		public bool IsOptional
		{
			get
			{
				return attributes.GetAttributes(16);
			}
			set
			{
				attributes = attributes.SetAttributes(16, value);
			}
		}

		public bool HasDefault
		{
			get
			{
				return attributes.GetAttributes(4096);
			}
			set
			{
				attributes = attributes.SetAttributes(4096, value);
			}
		}

		public bool HasFieldMarshal
		{
			get
			{
				return attributes.GetAttributes(8192);
			}
			set
			{
				attributes = attributes.SetAttributes(8192, value);
			}
		}

		internal ParameterDefinition(TypeReference parameterType, IMethodSignature method)
			: this(string.Empty, ParameterAttributes.None, parameterType)
		{
			this.method = method;
		}

		public ParameterDefinition(TypeReference parameterType)
			: this(string.Empty, ParameterAttributes.None, parameterType)
		{
		}

		public ParameterDefinition(string name, ParameterAttributes attributes, TypeReference parameterType)
			: base(name, parameterType)
		{
			this.attributes = (ushort)attributes;
			token = new MetadataToken(TokenType.Param);
		}

		public override ParameterDefinition Resolve()
		{
			return this;
		}
	}
	internal sealed class ParameterDefinitionCollection : Collection<ParameterDefinition>
	{
		private readonly IMethodSignature method;

		internal ParameterDefinitionCollection(IMethodSignature method)
		{
			this.method = method;
		}

		internal ParameterDefinitionCollection(IMethodSignature method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		protected override void OnAdd(ParameterDefinition item, int index)
		{
			item.method = method;
			item.index = index;
		}

		protected override void OnInsert(ParameterDefinition item, int index)
		{
			item.method = method;
			item.index = index;
			for (int i = index; i < size; i++)
			{
				items[i].index = i + 1;
			}
		}

		protected override void OnSet(ParameterDefinition item, int index)
		{
			item.method = method;
			item.index = index;
		}

		protected override void OnRemove(ParameterDefinition item, int index)
		{
			item.method = null;
			item.index = -1;
			for (int i = index + 1; i < size; i++)
			{
				items[i].index = i - 1;
			}
		}
	}
	public abstract class ParameterReference : IMetadataTokenProvider
	{
		private string name;

		internal int index = -1;

		protected TypeReference parameter_type;

		internal MetadataToken token;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public int Index => index;

		public TypeReference ParameterType
		{
			get
			{
				return parameter_type;
			}
			set
			{
				parameter_type = value;
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		internal ParameterReference(string name, TypeReference parameterType)
		{
			if (parameterType == null)
			{
				throw new ArgumentNullException("parameterType");
			}
			this.name = name ?? string.Empty;
			parameter_type = parameterType;
		}

		public override string ToString()
		{
			return name;
		}

		public abstract ParameterDefinition Resolve();
	}
	public sealed class PinnedType : TypeSpecification
	{
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsPinned => true;

		public PinnedType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			etype = Mono.Cecil.Metadata.ElementType.Pinned;
		}
	}
	[Flags]
	public enum PInvokeAttributes : ushort
	{
		NoMangle = 1,
		CharSetMask = 6,
		CharSetNotSpec = 0,
		CharSetAnsi = 2,
		CharSetUnicode = 4,
		CharSetAuto = 6,
		SupportsLastError = 0x40,
		CallConvMask = 0x700,
		CallConvWinapi = 0x100,
		CallConvCdecl = 0x200,
		CallConvStdCall = 0x300,
		CallConvThiscall = 0x400,
		CallConvFastcall = 0x500,
		BestFitMask = 0x30,
		BestFitEnabled = 0x10,
		BestFitDisabled = 0x20,
		ThrowOnUnmappableCharMask = 0x3000,
		ThrowOnUnmappableCharEnabled = 0x1000,
		ThrowOnUnmappableCharDisabled = 0x2000
	}
	public sealed class PInvokeInfo
	{
		private ushort attributes;

		private string entry_point;

		private ModuleReference module;

		public PInvokeAttributes Attributes
		{
			get
			{
				return (PInvokeAttributes)attributes;
			}
			set
			{
				attributes = (ushort)value;
			}
		}

		public string EntryPoint
		{
			get
			{
				return entry_point;
			}
			set
			{
				entry_point = value;
			}
		}

		public ModuleReference Module
		{
			get
			{
				return module;
			}
			set
			{
				module = value;
			}
		}

		public bool IsNoMangle
		{
			get
			{
				return attributes.GetAttributes(1);
			}
			set
			{
				attributes = attributes.SetAttributes(1, value);
			}
		}

		public bool IsCharSetNotSpec
		{
			get
			{
				return attributes.GetMaskedAttributes(6, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(6, 0u, value);
			}
		}

		public bool IsCharSetAnsi
		{
			get
			{
				return attributes.GetMaskedAttributes(6, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(6, 2u, value);
			}
		}

		public bool IsCharSetUnicode
		{
			get
			{
				return attributes.GetMaskedAttributes(6, 4u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(6, 4u, value);
			}
		}

		public bool IsCharSetAuto
		{
			get
			{
				return attributes.GetMaskedAttributes(6, 6u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(6, 6u, value);
			}
		}

		public bool SupportsLastError
		{
			get
			{
				return attributes.GetAttributes(64);
			}
			set
			{
				attributes = attributes.SetAttributes(64, value);
			}
		}

		public bool IsCallConvWinapi
		{
			get
			{
				return attributes.GetMaskedAttributes(1792, 256u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(1792, 256u, value);
			}
		}

		public bool IsCallConvCdecl
		{
			get
			{
				return attributes.GetMaskedAttributes(1792, 512u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(1792, 512u, value);
			}
		}

		public bool IsCallConvStdCall
		{
			get
			{
				return attributes.GetMaskedAttributes(1792, 768u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(1792, 768u, value);
			}
		}

		public bool IsCallConvThiscall
		{
			get
			{
				return attributes.GetMaskedAttributes(1792, 1024u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(1792, 1024u, value);
			}
		}

		public bool IsCallConvFastcall
		{
			get
			{
				return attributes.GetMaskedAttributes(1792, 1280u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(1792, 1280u, value);
			}
		}

		public bool IsBestFitEnabled
		{
			get
			{
				return attributes.GetMaskedAttributes(48, 16u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(48, 16u, value);
			}
		}

		public bool IsBestFitDisabled
		{
			get
			{
				return attributes.GetMaskedAttributes(48, 32u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(48, 32u, value);
			}
		}

		public bool IsThrowOnUnmappableCharEnabled
		{
			get
			{
				return attributes.GetMaskedAttributes(12288, 4096u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(12288, 4096u, value);
			}
		}

		public bool IsThrowOnUnmappableCharDisabled
		{
			get
			{
				return attributes.GetMaskedAttributes(12288, 8192u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(12288, 8192u, value);
			}
		}

		public PInvokeInfo(PInvokeAttributes attributes, string entryPoint, ModuleReference module)
		{
			this.attributes = (ushort)attributes;
			entry_point = entryPoint;
			this.module = module;
		}
	}
	public sealed class PointerType : TypeSpecification
	{
		public override string Name => base.Name + "*";

		public override string FullName => base.FullName + "*";

		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsPointer => true;

		public PointerType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			etype = Mono.Cecil.Metadata.ElementType.Ptr;
		}
	}
	[Flags]
	public enum PropertyAttributes : ushort
	{
		None = 0,
		SpecialName = 0x200,
		RTSpecialName = 0x400,
		HasDefault = 0x1000,
		Unused = 0xE9FF
	}
	public sealed class PropertyDefinition : PropertyReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, IConstantProvider
	{
		private bool? has_this;

		private ushort attributes;

		private Collection<CustomAttribute> custom_attributes;

		internal MethodDefinition get_method;

		internal MethodDefinition set_method;

		internal Collection<MethodDefinition> other_methods;

		private object constant = Mixin.NotResolved;

		public PropertyAttributes Attributes
		{
			get
			{
				return (PropertyAttributes)attributes;
			}
			set
			{
				attributes = (ushort)value;
			}
		}

		public bool HasThis
		{
			get
			{
				if (has_this.HasValue)
				{
					return has_this.Value;
				}
				if (GetMethod != null)
				{
					return get_method.HasThis;
				}
				if (SetMethod != null)
				{
					return set_method.HasThis;
				}
				return false;
			}
			set
			{
				has_this = value;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, Module);

		public MethodDefinition GetMethod
		{
			get
			{
				if (get_method != null)
				{
					return get_method;
				}
				InitializeMethods();
				return get_method;
			}
			set
			{
				get_method = value;
			}
		}

		public MethodDefinition SetMethod
		{
			get
			{
				if (set_method != null)
				{
					return set_method;
				}
				InitializeMethods();
				return set_method;
			}
			set
			{
				set_method = value;
			}
		}

		public bool HasOtherMethods
		{
			get
			{
				if (other_methods != null)
				{
					return other_methods.Count > 0;
				}
				InitializeMethods();
				return !other_methods.IsNullOrEmpty();
			}
		}

		public Collection<MethodDefinition> OtherMethods
		{
			get
			{
				if (other_methods != null)
				{
					return other_methods;
				}
				InitializeMethods();
				if (other_methods != null)
				{
					return other_methods;
				}
				Interlocked.CompareExchange(ref other_methods, new Collection<MethodDefinition>(), null);
				return other_methods;
			}
		}

		public bool HasParameters
		{
			get
			{
				InitializeMethods();
				if (get_method != null)
				{
					return get_method.HasParameters;
				}
				if (set_method != null)
				{
					return set_method.HasParameters && set_method.Parameters.Count > 1;
				}
				return false;
			}
		}

		public override Collection<ParameterDefinition> Parameters
		{
			get
			{
				InitializeMethods();
				if (get_method != null)
				{
					return MirrorParameters(get_method, 0);
				}
				if (set_method != null)
				{
					return MirrorParameters(set_method, 1);
				}
				return new Collection<ParameterDefinition>();
			}
		}

		public bool HasConstant
		{
			get
			{
				this.ResolveConstant(ref constant, Module);
				return constant != Mixin.NoValue;
			}
			set
			{
				if (!value)
				{
					constant = Mixin.NoValue;
				}
			}
		}

		public object Constant
		{
			get
			{
				return HasConstant ? constant : null;
			}
			set
			{
				constant = value;
			}
		}

		public bool IsSpecialName
		{
			get
			{
				return attributes.GetAttributes(512);
			}
			set
			{
				attributes = attributes.SetAttributes(512, value);
			}
		}

		public bool IsRuntimeSpecialName
		{
			get
			{
				return attributes.GetAttributes(1024);
			}
			set
			{
				attributes = attributes.SetAttributes(1024, value);
			}
		}

		public bool HasDefault
		{
			get
			{
				return attributes.GetAttributes(4096);
			}
			set
			{
				attributes = attributes.SetAttributes(4096, value);
			}
		}

		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		public override bool IsDefinition => true;

		public override string FullName
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(base.PropertyType.ToString());
				stringBuilder.Append(' ');
				stringBuilder.Append(MemberFullName());
				stringBuilder.Append('(');
				if (HasParameters)
				{
					Collection<ParameterDefinition> parameters = Parameters;
					for (int i = 0; i < parameters.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(parameters[i].ParameterType.FullName);
					}
				}
				stringBuilder.Append(')');
				return stringBuilder.ToString();
			}
		}

		private static Collection<ParameterDefinition> MirrorParameters(MethodDefinition method, int bound)
		{
			Collection<ParameterDefinition> collection = new Collection<ParameterDefinition>();
			if (!method.HasParameters)
			{
				return collection;
			}
			Collection<ParameterDefinition> parameters = method.Parameters;
			int num = parameters.Count - bound;
			for (int i = 0; i < num; i++)
			{
				collection.Add(parameters[i]);
			}
			return collection;
		}

		public PropertyDefinition(string name, PropertyAttributes attributes, TypeReference propertyType)
			: base(name, propertyType)
		{
			this.attributes = (ushort)attributes;
			token = new MetadataToken(TokenType.Property);
		}

		private void InitializeMethods()
		{
			ModuleDefinition module = Module;
			if (module == null)
			{
				return;
			}
			lock (module.SyncRoot)
			{
				if (get_method == null && set_method == null && module.HasImage())
				{
					module.Read(this, delegate(PropertyDefinition property, MetadataReader reader)
					{
						reader.ReadMethods(property);
					});
				}
			}
		}

		public override PropertyDefinition Resolve()
		{
			return this;
		}
	}
	public abstract class PropertyReference : MemberReference
	{
		private TypeReference property_type;

		public TypeReference PropertyType
		{
			get
			{
				return property_type;
			}
			set
			{
				property_type = value;
			}
		}

		public abstract Collection<ParameterDefinition> Parameters { get; }

		internal PropertyReference(string name, TypeReference propertyType)
			: base(name)
		{
			Mixin.CheckType(propertyType, Mixin.Argument.propertyType);
			property_type = propertyType;
		}

		protected override IMemberDefinition ResolveDefinition()
		{
			return Resolve();
		}

		public new abstract PropertyDefinition Resolve();
	}
	public sealed class ByReferenceType : TypeSpecification
	{
		public override string Name => base.Name + "&";

		public override string FullName => base.FullName + "&";

		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsByReference => true;

		public ByReferenceType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			etype = Mono.Cecil.Metadata.ElementType.ByRef;
		}
	}
	public enum ResourceType
	{
		Linked,
		Embedded,
		AssemblyLinked
	}
	public abstract class Resource
	{
		private string name;

		private uint attributes;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public ManifestResourceAttributes Attributes
		{
			get
			{
				return (ManifestResourceAttributes)attributes;
			}
			set
			{
				attributes = (uint)value;
			}
		}

		public abstract ResourceType ResourceType { get; }

		public bool IsPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 1u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 1u, value);
			}
		}

		public bool IsPrivate
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 2u, value);
			}
		}

		internal Resource(string name, ManifestResourceAttributes attributes)
		{
			this.name = name;
			this.attributes = (uint)attributes;
		}
	}
	public enum SecurityAction : ushort
	{
		Request = 1,
		Demand,
		Assert,
		Deny,
		PermitOnly,
		LinkDemand,
		InheritDemand,
		RequestMinimum,
		RequestOptional,
		RequestRefuse,
		PreJitGrant,
		PreJitDeny,
		NonCasDemand,
		NonCasLinkDemand,
		NonCasInheritance
	}
	public interface ISecurityDeclarationProvider : IMetadataTokenProvider
	{
		bool HasSecurityDeclarations { get; }

		Collection<SecurityDeclaration> SecurityDeclarations { get; }
	}
	[DebuggerDisplay("{AttributeType}")]
	public sealed class SecurityAttribute : ICustomAttribute
	{
		private TypeReference attribute_type;

		internal Collection<CustomAttributeNamedArgument> fields;

		internal Collection<CustomAttributeNamedArgument> properties;

		public TypeReference AttributeType
		{
			get
			{
				return attribute_type;
			}
			set
			{
				attribute_type = value;
			}
		}

		public bool HasFields => !fields.IsNullOrEmpty();

		public Collection<CustomAttributeNamedArgument> Fields
		{
			get
			{
				if (fields == null)
				{
					Interlocked.CompareExchange(ref fields, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return fields;
			}
		}

		public bool HasProperties => !properties.IsNullOrEmpty();

		public Collection<CustomAttributeNamedArgument> Properties
		{
			get
			{
				if (properties == null)
				{
					Interlocked.CompareExchange(ref properties, new Collection<CustomAttributeNamedArgument>(), null);
				}
				return properties;
			}
		}

		bool ICustomAttribute.HasConstructorArguments => false;

		Collection<CustomAttributeArgument> ICustomAttribute.ConstructorArguments
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		public SecurityAttribute(TypeReference attributeType)
		{
			attribute_type = attributeType;
		}
	}
	public sealed class SecurityDeclaration
	{
		internal readonly uint signature;

		private byte[] blob;

		private readonly ModuleDefinition module;

		internal bool resolved;

		private SecurityAction action;

		internal Collection<SecurityAttribute> security_attributes;

		public SecurityAction Action
		{
			get
			{
				return action;
			}
			set
			{
				action = value;
			}
		}

		public bool HasSecurityAttributes
		{
			get
			{
				Resolve();
				return !security_attributes.IsNullOrEmpty();
			}
		}

		public Collection<SecurityAttribute> SecurityAttributes
		{
			get
			{
				Resolve();
				if (security_attributes == null)
				{
					Interlocked.CompareExchange(ref security_attributes, new Collection<SecurityAttribute>(), null);
				}
				return security_attributes;
			}
		}

		internal bool HasImage => module != null && module.HasImage;

		internal SecurityDeclaration(SecurityAction action, uint signature, ModuleDefinition module)
		{
			this.action = action;
			this.signature = signature;
			this.module = module;
		}

		public SecurityDeclaration(SecurityAction action)
		{
			this.action = action;
			resolved = true;
		}

		public SecurityDeclaration(SecurityAction action, byte[] blob)
		{
			this.action = action;
			resolved = false;
			this.blob = blob;
		}

		public byte[] GetBlob()
		{
			if (blob != null)
			{
				return blob;
			}
			if (!HasImage || signature == 0)
			{
				throw new NotSupportedException();
			}
			return module.Read(ref blob, this, (SecurityDeclaration declaration, MetadataReader reader) => reader.ReadSecurityDeclarationBlob(declaration.signature));
		}

		private void Resolve()
		{
			if (resolved || !HasImage)
			{
				return;
			}
			lock (module.SyncRoot)
			{
				if (!resolved)
				{
					module.Read(this, delegate(SecurityDeclaration declaration, MetadataReader reader)
					{
						reader.ReadSecurityDeclarationSignature(declaration);
					});
					resolved = true;
				}
			}
		}
	}
	public sealed class SentinelType : TypeSpecification
	{
		public override bool IsValueType
		{
			get
			{
				return false;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override bool IsSentinel => true;

		public SentinelType(TypeReference type)
			: base(type)
		{
			Mixin.CheckType(type);
			etype = Mono.Cecil.Metadata.ElementType.Sentinel;
		}
	}
	public enum TargetRuntime
	{
		Net_1_0,
		Net_1_1,
		Net_2_0,
		Net_4_0
	}
	[Flags]
	internal enum TypeDefinitionTreatment
	{
		None = 0,
		KindMask = 0xF,
		NormalType = 1,
		NormalAttribute = 2,
		UnmangleWindowsRuntimeName = 3,
		PrefixWindowsRuntimeName = 4,
		RedirectToClrType = 5,
		RedirectToClrAttribute = 6,
		RedirectImplementedMethods = 7,
		Abstract = 0x10,
		Internal = 0x20
	}
	internal enum TypeReferenceTreatment
	{
		None,
		SystemDelegate,
		SystemAttribute,
		UseProjectionInfo
	}
	[Flags]
	internal enum MethodDefinitionTreatment
	{
		None = 0,
		Abstract = 2,
		Private = 4,
		Public = 8,
		Runtime = 0x10,
		InternalCall = 0x20
	}
	internal enum FieldDefinitionTreatment
	{
		None,
		Public
	}
	internal enum CustomAttributeValueTreatment
	{
		None,
		AllowSingle,
		AllowMultiple,
		VersionAttribute,
		DeprecatedAttribute
	}
	[Flags]
	public enum TypeAttributes : uint
	{
		VisibilityMask = 7u,
		NotPublic = 0u,
		Public = 1u,
		NestedPublic = 2u,
		NestedPrivate = 3u,
		NestedFamily = 4u,
		NestedAssembly = 5u,
		NestedFamANDAssem = 6u,
		NestedFamORAssem = 7u,
		LayoutMask = 0x18u,
		AutoLayout = 0u,
		SequentialLayout = 8u,
		ExplicitLayout = 0x10u,
		ClassSemanticMask = 0x20u,
		Class = 0u,
		Interface = 0x20u,
		Abstract = 0x80u,
		Sealed = 0x100u,
		SpecialName = 0x400u,
		Import = 0x1000u,
		Serializable = 0x2000u,
		WindowsRuntime = 0x4000u,
		StringFormatMask = 0x30000u,
		AnsiClass = 0u,
		UnicodeClass = 0x10000u,
		AutoClass = 0x20000u,
		BeforeFieldInit = 0x100000u,
		RTSpecialName = 0x800u,
		HasSecurity = 0x40000u,
		Forwarder = 0x200000u
	}
	internal enum TypeComparisonMode
	{
		Exact,
		SignatureOnly,
		SignatureOnlyLoose
	}
	public sealed class TypeDefinition : TypeReference, IMemberDefinition, ICustomAttributeProvider, IMetadataTokenProvider, ISecurityDeclarationProvider
	{
		private uint attributes;

		private TypeReference base_type;

		internal Range fields_range;

		internal Range methods_range;

		private short packing_size = -2;

		private int class_size = -2;

		private InterfaceImplementationCollection interfaces;

		private Collection<TypeDefinition> nested_types;

		private Collection<MethodDefinition> methods;

		private Collection<FieldDefinition> fields;

		private Collection<EventDefinition> events;

		private Collection<PropertyDefinition> properties;

		private Collection<CustomAttribute> custom_attributes;

		private Collection<SecurityDeclaration> security_declarations;

		public TypeAttributes Attributes
		{
			get
			{
				return (TypeAttributes)attributes;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && (ushort)value != attributes)
				{
					throw new InvalidOperationException();
				}
				attributes = (uint)value;
			}
		}

		public TypeReference BaseType
		{
			get
			{
				return base_type;
			}
			set
			{
				base_type = value;
			}
		}

		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.Name)
				{
					throw new InvalidOperationException();
				}
				base.Name = value;
			}
		}

		public bool HasLayoutInfo
		{
			get
			{
				if (packing_size >= 0 || class_size >= 0)
				{
					return true;
				}
				ResolveLayout();
				return packing_size >= 0 || class_size >= 0;
			}
		}

		public short PackingSize
		{
			get
			{
				if (packing_size >= 0)
				{
					return packing_size;
				}
				ResolveLayout();
				return (short)((packing_size >= 0) ? packing_size : (-1));
			}
			set
			{
				packing_size = value;
			}
		}

		public int ClassSize
		{
			get
			{
				if (class_size >= 0)
				{
					return class_size;
				}
				ResolveLayout();
				return (class_size >= 0) ? class_size : (-1);
			}
			set
			{
				class_size = value;
			}
		}

		public bool HasInterfaces
		{
			get
			{
				if (interfaces != null)
				{
					return interfaces.Count > 0;
				}
				return base.HasImage && Module.Read(this, (TypeDefinition type, MetadataReader reader) => reader.HasInterfaces(type));
			}
		}

		public Collection<InterfaceImplementation> Interfaces
		{
			get
			{
				if (interfaces != null)
				{
					return interfaces;
				}
				if (base.HasImage)
				{
					return Module.Read(ref interfaces, this, (TypeDefinition type, MetadataReader reader) => reader.ReadInterfaces(type));
				}
				Interlocked.CompareExchange(ref interfaces, new InterfaceImplementationCollection(this), null);
				return interfaces;
			}
		}

		public bool HasNestedTypes
		{
			get
			{
				if (nested_types != null)
				{
					return nested_types.Count > 0;
				}
				return base.HasImage && Module.Read(this, (TypeDefinition type, MetadataReader reader) => reader.HasNestedTypes(type));
			}
		}

		public Collection<TypeDefinition> NestedTypes
		{
			get
			{
				if (nested_types != null)
				{
					return nested_types;
				}
				if (base.HasImage)
				{
					return Module.Read(ref nested_types, this, (TypeDefinition type, MetadataReader reader) => reader.ReadNestedTypes(type));
				}
				Interlocked.CompareExchange(ref nested_types, new MemberDefinitionCollection<TypeDefinition>(this), null);
				return nested_types;
			}
		}

		public bool HasMethods
		{
			get
			{
				if (methods != null)
				{
					return methods.Count > 0;
				}
				return base.HasImage && methods_range.Length != 0;
			}
		}

		public Collection<MethodDefinition> Methods
		{
			get
			{
				if (methods != null)
				{
					return methods;
				}
				if (base.HasImage)
				{
					return Module.Read(ref methods, this, (TypeDefinition type, MetadataReader reader) => reader.ReadMethods(type));
				}
				Interlocked.CompareExchange(ref methods, new MemberDefinitionCollection<MethodDefinition>(this), null);
				return methods;
			}
		}

		public bool HasFields
		{
			get
			{
				if (fields != null)
				{
					return fields.Count > 0;
				}
				return base.HasImage && fields_range.Length != 0;
			}
		}

		public Collection<FieldDefinition> Fields
		{
			get
			{
				if (fields != null)
				{
					return fields;
				}
				if (base.HasImage)
				{
					return Module.Read(ref fields, this, (TypeDefinition type, MetadataReader reader) => reader.ReadFields(type));
				}
				Interlocked.CompareExchange(ref fields, new MemberDefinitionCollection<FieldDefinition>(this), null);
				return fields;
			}
		}

		public bool HasEvents
		{
			get
			{
				if (events != null)
				{
					return events.Count > 0;
				}
				return base.HasImage && Module.Read(this, (TypeDefinition type, MetadataReader reader) => reader.HasEvents(type));
			}
		}

		public Collection<EventDefinition> Events
		{
			get
			{
				if (events != null)
				{
					return events;
				}
				if (base.HasImage)
				{
					return Module.Read(ref events, this, (TypeDefinition type, MetadataReader reader) => reader.ReadEvents(type));
				}
				Interlocked.CompareExchange(ref events, new MemberDefinitionCollection<EventDefinition>(this), null);
				return events;
			}
		}

		public bool HasProperties
		{
			get
			{
				if (properties != null)
				{
					return properties.Count > 0;
				}
				return base.HasImage && Module.Read(this, (TypeDefinition type, MetadataReader reader) => reader.HasProperties(type));
			}
		}

		public Collection<PropertyDefinition> Properties
		{
			get
			{
				if (properties != null)
				{
					return properties;
				}
				if (base.HasImage)
				{
					return Module.Read(ref properties, this, (TypeDefinition type, MetadataReader reader) => reader.ReadProperties(type));
				}
				Interlocked.CompareExchange(ref properties, new MemberDefinitionCollection<PropertyDefinition>(this), null);
				return properties;
			}
		}

		public bool HasSecurityDeclarations
		{
			get
			{
				if (security_declarations != null)
				{
					return security_declarations.Count > 0;
				}
				return this.GetHasSecurityDeclarations(Module);
			}
		}

		public Collection<SecurityDeclaration> SecurityDeclarations => security_declarations ?? this.GetSecurityDeclarations(ref security_declarations, Module);

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				return this.GetHasCustomAttributes(Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, Module);

		public override bool HasGenericParameters
		{
			get
			{
				if (generic_parameters != null)
				{
					return generic_parameters.Count > 0;
				}
				return this.GetHasGenericParameters(Module);
			}
		}

		public override Collection<GenericParameter> GenericParameters => generic_parameters ?? this.GetGenericParameters(ref generic_parameters, Module);

		public bool IsNotPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 0u, value);
			}
		}

		public bool IsPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 1u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 1u, value);
			}
		}

		public bool IsNestedPublic
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 2u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 2u, value);
			}
		}

		public bool IsNestedPrivate
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 3u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 3u, value);
			}
		}

		public bool IsNestedFamily
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 4u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 4u, value);
			}
		}

		public bool IsNestedAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 5u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 5u, value);
			}
		}

		public bool IsNestedFamilyAndAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 6u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 6u, value);
			}
		}

		public bool IsNestedFamilyOrAssembly
		{
			get
			{
				return attributes.GetMaskedAttributes(7u, 7u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(7u, 7u, value);
			}
		}

		public bool IsAutoLayout
		{
			get
			{
				return attributes.GetMaskedAttributes(24u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(24u, 0u, value);
			}
		}

		public bool IsSequentialLayout
		{
			get
			{
				return attributes.GetMaskedAttributes(24u, 8u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(24u, 8u, value);
			}
		}

		public bool IsExplicitLayout
		{
			get
			{
				return attributes.GetMaskedAttributes(24u, 16u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(24u, 16u, value);
			}
		}

		public bool IsClass
		{
			get
			{
				return attributes.GetMaskedAttributes(32u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(32u, 0u, value);
			}
		}

		public bool IsInterface
		{
			get
			{
				return attributes.GetMaskedAttributes(32u, 32u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(32u, 32u, value);
			}
		}

		public bool IsAbstract
		{
			get
			{
				return attributes.GetAttributes(128u);
			}
			set
			{
				attributes = attributes.SetAttributes(128u, value);
			}
		}

		public bool IsSealed
		{
			get
			{
				return attributes.GetAttributes(256u);
			}
			set
			{
				attributes = attributes.SetAttributes(256u, value);
			}
		}

		public bool IsSpecialName
		{
			get
			{
				return attributes.GetAttributes(1024u);
			}
			set
			{
				attributes = attributes.SetAttributes(1024u, value);
			}
		}

		public bool IsImport
		{
			get
			{
				return attributes.GetAttributes(4096u);
			}
			set
			{
				attributes = attributes.SetAttributes(4096u, value);
			}
		}

		public bool IsSerializable
		{
			get
			{
				return attributes.GetAttributes(8192u);
			}
			set
			{
				attributes = attributes.SetAttributes(8192u, value);
			}
		}

		public bool IsWindowsRuntime
		{
			get
			{
				return attributes.GetAttributes(16384u);
			}
			set
			{
				attributes = attributes.SetAttributes(16384u, value);
			}
		}

		public bool IsAnsiClass
		{
			get
			{
				return attributes.GetMaskedAttributes(196608u, 0u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(196608u, 0u, value);
			}
		}

		public bool IsUnicodeClass
		{
			get
			{
				return attributes.GetMaskedAttributes(196608u, 65536u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(196608u, 65536u, value);
			}
		}

		public bool IsAutoClass
		{
			get
			{
				return attributes.GetMaskedAttributes(196608u, 131072u);
			}
			set
			{
				attributes = attributes.SetMaskedAttributes(196608u, 131072u, value);
			}
		}

		public bool IsBeforeFieldInit
		{
			get
			{
				return attributes.GetAttributes(1048576u);
			}
			set
			{
				attributes = attributes.SetAttributes(1048576u, value);
			}
		}

		public bool IsRuntimeSpecialName
		{
			get
			{
				return attributes.GetAttributes(2048u);
			}
			set
			{
				attributes = attributes.SetAttributes(2048u, value);
			}
		}

		public bool HasSecurity
		{
			get
			{
				return attributes.GetAttributes(262144u);
			}
			set
			{
				attributes = attributes.SetAttributes(262144u, value);
			}
		}

		public bool IsEnum => base_type != null && base_type.IsTypeOf("System", "Enum");

		public override bool IsValueType
		{
			get
			{
				if (base_type == null)
				{
					return false;
				}
				return base_type.IsTypeOf("System", "Enum") || (base_type.IsTypeOf("System", "ValueType") && !this.IsTypeOf("System", "Enum"));
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public override bool IsPrimitive
		{
			get
			{
				ElementType self;
				return MetadataSystem.TryGetPrimitiveElementType(this, out self) && self.IsPrimitive();
			}
		}

		public override MetadataType MetadataType
		{
			get
			{
				if (MetadataSystem.TryGetPrimitiveElementType(this, out var result))
				{
					return (MetadataType)result;
				}
				return base.MetadataType;
			}
		}

		public override bool IsDefinition => true;

		public new TypeDefinition DeclaringType
		{
			get
			{
				return (TypeDefinition)base.DeclaringType;
			}
			set
			{
				base.DeclaringType = value;
			}
		}

		internal new TypeDefinitionProjection WindowsRuntimeProjection
		{
			get
			{
				return (TypeDefinitionProjection)projection;
			}
			set
			{
				projection = value;
			}
		}

		private void ResolveLayout()
		{
			if (!base.HasImage)
			{
				packing_size = -1;
				class_size = -1;
				return;
			}
			lock (Module.SyncRoot)
			{
				if (packing_size == -2 && class_size == -2)
				{
					Row<short, int> row = Module.Read(this, (TypeDefinition type, MetadataReader reader) => reader.ReadTypeLayout(type));
					packing_size = row.Col1;
					class_size = row.Col2;
				}
			}
		}

		public TypeDefinition(string @namespace, string name, TypeAttributes attributes)
			: base(@namespace, name)
		{
			this.attributes = (uint)attributes;
			token = new MetadataToken(TokenType.TypeDef);
		}

		public TypeDefinition(string @namespace, string name, TypeAttributes attributes, TypeReference baseType)
			: this(@namespace, name, attributes)
		{
			BaseType = baseType;
		}

		protected override void ClearFullName()
		{
			base.ClearFullName();
			if (HasNestedTypes)
			{
				Collection<TypeDefinition> nestedTypes = NestedTypes;
				for (int i = 0; i < nestedTypes.Count; i++)
				{
					nestedTypes[i].ClearFullName();
				}
			}
		}

		public override TypeDefinition Resolve()
		{
			return this;
		}
	}
	public sealed class InterfaceImplementation : ICustomAttributeProvider, IMetadataTokenProvider
	{
		internal TypeDefinition type;

		internal MetadataToken token;

		private TypeReference interface_type;

		private Collection<CustomAttribute> custom_attributes;

		public TypeReference InterfaceType
		{
			get
			{
				return interface_type;
			}
			set
			{
				interface_type = value;
			}
		}

		public bool HasCustomAttributes
		{
			get
			{
				if (custom_attributes != null)
				{
					return custom_attributes.Count > 0;
				}
				if (type == null)
				{
					return false;
				}
				return this.GetHasCustomAttributes(type.Module);
			}
		}

		public Collection<CustomAttribute> CustomAttributes
		{
			get
			{
				if (type == null)
				{
					if (custom_attributes == null)
					{
						Interlocked.CompareExchange(ref custom_attributes, new Collection<CustomAttribute>(), null);
					}
					return custom_attributes;
				}
				return custom_attributes ?? this.GetCustomAttributes(ref custom_attributes, type.Module);
			}
		}

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		internal InterfaceImplementation(TypeReference interfaceType, MetadataToken token)
		{
			interface_type = interfaceType;
			this.token = token;
		}

		public InterfaceImplementation(TypeReference interfaceType)
		{
			Mixin.CheckType(interfaceType, Mixin.Argument.interfaceType);
			interface_type = interfaceType;
			token = new MetadataToken(TokenType.InterfaceImpl);
		}
	}
	internal class InterfaceImplementationCollection : Collection<InterfaceImplementation>
	{
		private readonly TypeDefinition type;

		internal InterfaceImplementationCollection(TypeDefinition type)
		{
			this.type = type;
		}

		internal InterfaceImplementationCollection(TypeDefinition type, int length)
			: base(length)
		{
			this.type = type;
		}

		protected override void OnAdd(InterfaceImplementation item, int index)
		{
			item.type = type;
		}

		protected override void OnInsert(InterfaceImplementation item, int index)
		{
			item.type = type;
		}

		protected override void OnSet(InterfaceImplementation item, int index)
		{
			item.type = type;
		}

		protected override void OnRemove(InterfaceImplementation item, int index)
		{
			item.type = null;
		}
	}
	internal sealed class TypeDefinitionCollection : Collection<TypeDefinition>
	{
		private readonly ModuleDefinition container;

		private readonly Dictionary<Row<string, string>, TypeDefinition> name_cache;

		internal TypeDefinitionCollection(ModuleDefinition container)
		{
			this.container = container;
			name_cache = new Dictionary<Row<string, string>, TypeDefinition>(new RowEqualityComparer());
		}

		internal TypeDefinitionCollection(ModuleDefinition container, int capacity)
			: base(capacity)
		{
			this.container = container;
			name_cache = new Dictionary<Row<string, string>, TypeDefinition>(capacity, new RowEqualityComparer());
		}

		protected override void OnAdd(TypeDefinition item, int index)
		{
			Attach(item);
		}

		protected override void OnSet(TypeDefinition item, int index)
		{
			Attach(item);
		}

		protected override void OnInsert(TypeDefinition item, int index)
		{
			Attach(item);
		}

		protected override void OnRemove(TypeDefinition item, int index)
		{
			Detach(item);
		}

		protected override void OnClear()
		{
			using Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				TypeDefinition current = enumerator.Current;
				Detach(current);
			}
		}

		private void Attach(TypeDefinition type)
		{
			if (type.Module != null && type.Module != container)
			{
				throw new ArgumentException("Type already attached");
			}
			type.module = container;
			type.scope = container;
			name_cache[new Row<string, string>(type.Namespace, type.Name)] = type;
		}

		private void Detach(TypeDefinition type)
		{
			type.module = null;
			type.scope = null;
			name_cache.Remove(new Row<string, string>(type.Namespace, type.Name));
		}

		public TypeDefinition GetType(string fullname)
		{
			TypeParser.SplitFullName(fullname, out var @namespace, out var name);
			return GetType(@namespace, name);
		}

		public TypeDefinition GetType(string @namespace, string name)
		{
			if (name_cache.TryGetValue(new Row<string, string>(@namespace, name), out var value))
			{
				return value;
			}
			return null;
		}

		public void RebuildNameCache()
		{
			name_cache.Clear();
			using Enumerator enumerator = GetEnumerator();
			while (enumerator.MoveNext())
			{
				TypeDefinition current = enumerator.Current;
				name_cache[new Row<string, string>(current.Namespace, current.Name)] = current;
			}
		}
	}
	internal class TypeParser
	{
		private class Type
		{
			public const int Ptr = -1;

			public const int ByRef = -2;

			public const int SzArray = -3;

			public string type_fullname;

			public string[] nested_names;

			public int arity;

			public int[] specs;

			public Type[] generic_arguments;

			public string assembly;
		}

		private readonly string fullname;

		private readonly int length;

		private int position;

		private TypeParser(string fullname)
		{
			this.fullname = fullname;
			length = fullname.Length;
		}

		private Type ParseType(bool fq_name)
		{
			Type type = new Type();
			type.type_fullname = ParsePart();
			type.nested_names = ParseNestedNames();
			if (TryGetArity(type))
			{
				type.generic_arguments = ParseGenericArguments(type.arity);
			}
			type.specs = ParseSpecs();
			if (fq_name)
			{
				type.assembly = ParseAssemblyName();
			}
			return type;
		}

		private static bool TryGetArity(Type type)
		{
			int arity = 0;
			TryAddArity(type.type_fullname, ref arity);
			string[] nested_names = type.nested_names;
			if (!nested_names.IsNullOrEmpty())
			{
				for (int i = 0; i < nested_names.Length; i++)
				{
					TryAddArity(nested_names[i], ref arity);
				}
			}
			type.arity = arity;
			return arity > 0;
		}

		private static bool TryGetArity(string name, out int arity)
		{
			arity = 0;
			int num = name.LastIndexOf('`');
			if (num == -1)
			{
				return false;
			}
			return ParseInt32(name.Substring(num + 1), out arity);
		}

		private static bool ParseInt32(string value, out int result)
		{
			return int.TryParse(value, out result);
		}

		private static void TryAddArity(string name, ref int arity)
		{
			if (TryGetArity(name, out var arity2))
			{
				arity += arity2;
			}
		}

		private string ParsePart()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (position < length && !IsDelimiter(fullname[position]))
			{
				if (fullname[position] == '\\')
				{
					position++;
				}
				stringBuilder.Append(fullname[position++]);
			}
			return stringBuilder.ToString();
		}

		private static bool IsDelimiter(char chr)
		{
			return "+,[]*&".IndexOf(chr) != -1;
		}

		private void TryParseWhiteSpace()
		{
			while (position < length && char.IsWhiteSpace(fullname[position]))
			{
				position++;
			}
		}

		private string[] ParseNestedNames()
		{
			string[] array = null;
			while (TryParse('+'))
			{
				Add(ref array, ParsePart());
			}
			return array;
		}

		private bool TryParse(char chr)
		{
			if (position < length && fullname[position] == chr)
			{
				position++;
				return true;
			}
			return false;
		}

		private static void Add<T>(ref T[] array, T item)
		{
			array = array.Add(item);
		}

		private int[] ParseSpecs()
		{
			int[] array = null;
			while (position < length)
			{
				switch (fullname[position])
				{
				case '*':
					position++;
					Add(ref array, -1);
					break;
				case '&':
					position++;
					Add(ref array, -2);
					break;
				case '[':
					position++;
					switch (fullname[position])
					{
					case ']':
						position++;
						Add(ref array, -3);
						break;
					case '*':
						position++;
						Add(ref array, 1);
						break;
					default:
					{
						int num = 1;
						while (TryParse(','))
						{
							num++;
						}
						Add(ref array, num);
						TryParse(']');
						break;
					}
					}
					break;
				default:
					return array;
				}
			}
			return array;
		}

		private Type[] ParseGenericArguments(int arity)
		{
			Type[] array = null;
			if (position == length || fullname[position] != '[')
			{
				return array;
			}
			TryParse('[');
			for (int i = 0; i < arity; i++)
			{
				bool flag = TryParse('[');
				Add(ref array, ParseType(flag));
				if (flag)
				{
					TryParse(']');
				}
				TryParse(',');
				TryParseWhiteSpace();
			}
			TryParse(']');
			return array;
		}

		private string ParseAssemblyName()
		{
			if (!TryParse(','))
			{
				return string.Empty;
			}
			TryParseWhiteSpace();
			int num = position;
			while (position < length)
			{
				char c = fullname[position];
				if (c == '[' || c == ']')
				{
					break;
				}
				position++;
			}
			return fullname.Substring(num, position - num);
		}

		public static TypeReference ParseType(ModuleDefinition module, string fullname, bool typeDefinitionOnly = false)
		{
			if (string.IsNullOrEmpty(fullname))
			{
				return null;
			}
			TypeParser typeParser = new TypeParser(fullname);
			return GetTypeReference(module, typeParser.ParseType(fq_name: true), typeDefinitionOnly);
		}

		private static TypeReference GetTypeReference(ModuleDefinition module, Type type_info, bool type_def_only)
		{
			if (!TryGetDefinition(module, type_info, out var type))
			{
				if (type_def_only)
				{
					return null;
				}
				type = CreateReference(type_info, module, GetMetadataScope(module, type_info));
			}
			return CreateSpecs(type, type_info);
		}

		private static TypeReference CreateSpecs(TypeReference type, Type type_info)
		{
			type = TryCreateGenericInstanceType(type, type_info);
			int[] specs = type_info.specs;
			if (specs.IsNullOrEmpty())
			{
				return type;
			}
			for (int i = 0; i < specs.Length; i++)
			{
				switch (specs[i])
				{
				case -1:
					type = new PointerType(type);
					continue;
				case -2:
					type = new ByReferenceType(type);
					continue;
				case -3:
					type = new ArrayType(type);
					continue;
				}
				ArrayType arrayType = new ArrayType(type);
				arrayType.Dimensions.Clear();
				for (int j = 0; j < specs[i]; j++)
				{
					arrayType.Dimensions.Add(default(ArrayDimension));
				}
				type = arrayType;
			}
			return type;
		}

		private static TypeReference TryCreateGenericInstanceType(TypeReference type, Type type_info)
		{
			Type[] generic_arguments = type_info.generic_arguments;
			if (generic_arguments.IsNullOrEmpty())
			{
				return type;
			}
			GenericInstanceType genericInstanceType = new GenericInstanceType(type, generic_arguments.Length);
			Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
			for (int i = 0; i < generic_arguments.Length; i++)
			{
				genericArguments.Add(GetTypeReference(type.Module, generic_arguments[i], type_def_only: false));
			}
			return genericInstanceType;
		}

		public static void SplitFullName(string fullname, out string @namespace, out string name)
		{
			int num = fullname.LastIndexOf('.');
			if (num == -1)
			{
				@namespace = string.Empty;
				name = fullname;
			}
			else
			{
				@namespace = fullname.Substring(0, num);
				name = fullname.Substring(num + 1);
			}
		}

		private static TypeReference CreateReference(Type type_info, ModuleDefinition module, IMetadataScope scope)
		{
			SplitFullName(type_info.type_fullname, out var @namespace, out var name);
			TypeReference typeReference = new TypeReference(@namespace, name, module, scope);
			MetadataSystem.TryProcessPrimitiveTypeReference(typeReference);
			AdjustGenericParameters(typeReference);
			string[] nested_names = type_info.nested_names;
			if (nested_names.IsNullOrEmpty())
			{
				return typeReference;
			}
			for (int i = 0; i < nested_names.Length; i++)
			{
				typeReference = new TypeReference(string.Empty, nested_names[i], module, null)
				{
					DeclaringType = typeReference
				};
				AdjustGenericParameters(typeReference);
			}
			return typeReference;
		}

		private static void AdjustGenericParameters(TypeReference type)
		{
			if (TryGetArity(type.Name, out var arity))
			{
				for (int i = 0; i < arity; i++)
				{
					type.GenericParameters.Add(new GenericParameter(type));
				}
			}
		}

		private static IMetadataScope GetMetadataScope(ModuleDefinition module, Type type_info)
		{
			if (string.IsNullOrEmpty(type_info.assembly))
			{
				return module.TypeSystem.CoreLibrary;
			}
			AssemblyNameReference assemblyNameReference = AssemblyNameReference.Parse(type_info.assembly);
			AssemblyNameReference assembly_reference;
			return module.TryGetAssemblyNameReference(assemblyNameReference, out assembly_reference) ? assembly_reference : assemblyNameReference;
		}

		private static bool TryGetDefinition(ModuleDefinition module, Type type_info, out TypeReference type)
		{
			type = null;
			if (!TryCurrentModule(module, type_info))
			{
				return false;
			}
			TypeDefinition typeDefinition = module.GetType(type_info.type_fullname);
			if (typeDefinition == null)
			{
				return false;
			}
			string[] nested_names = type_info.nested_names;
			if (!nested_names.IsNullOrEmpty())
			{
				for (int i = 0; i < nested_names.Length; i++)
				{
					TypeDefinition nestedType = typeDefinition.GetNestedType(nested_names[i]);
					if (nestedType == null)
					{
						return false;
					}
					typeDefinition = nestedType;
				}
			}
			type = typeDefinition;
			return true;
		}

		private static bool TryCurrentModule(ModuleDefinition module, Type type_info)
		{
			if (string.IsNullOrEmpty(type_info.assembly))
			{
				return true;
			}
			if (module.assembly != null && module.assembly.Name.FullName == type_info.assembly)
			{
				return true;
			}
			return false;
		}

		public static string ToParseable(TypeReference type, bool top_level = true)
		{
			if (type == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			AppendType(type, stringBuilder, fq_name: true, top_level);
			return stringBuilder.ToString();
		}

		private static void AppendNamePart(string part, StringBuilder name)
		{
			foreach (char c in part)
			{
				if (IsDelimiter(c))
				{
					name.Append('\\');
				}
				name.Append(c);
			}
		}

		private static void AppendType(TypeReference type, StringBuilder name, bool fq_name, bool top_level)
		{
			TypeReference elementType = type.GetElementType();
			TypeReference declaringType = elementType.DeclaringType;
			if (declaringType != null)
			{
				AppendType(declaringType, name, fq_name: false, top_level);
				name.Append('+');
			}
			string text = type.Namespace;
			if (!string.IsNullOrEmpty(text))
			{
				AppendNamePart(text, name);
				name.Append('.');
			}
			AppendNamePart(elementType.Name, name);
			if (fq_name)
			{
				if (type.IsTypeSpecification())
				{
					AppendTypeSpecification((TypeSpecification)type, name);
				}
				if (RequiresFullyQualifiedName(type, top_level))
				{
					name.Append(", ");
					name.Append(GetScopeFullName(type));
				}
			}
		}

		private static string GetScopeFullName(TypeReference type)
		{
			IMetadataScope scope = type.Scope;
			return scope.MetadataScopeType switch
			{
				MetadataScopeType.AssemblyNameReference => ((AssemblyNameReference)scope).FullName, 
				MetadataScopeType.ModuleDefinition => ((ModuleDefinition)scope).Assembly.Name.FullName, 
				_ => throw new ArgumentException(), 
			};
		}

		private static void AppendTypeSpecification(TypeSpecification type, StringBuilder name)
		{
			if (type.ElementType.IsTypeSpecification())
			{
				AppendTypeSpecification((TypeSpecification)type.ElementType, name);
			}
			switch (type.etype)
			{
			case ElementType.Ptr:
				name.Append('*');
				break;
			case ElementType.ByRef:
				name.Append('&');
				break;
			case ElementType.Array:
			case ElementType.SzArray:
			{
				ArrayType arrayType = (ArrayType)type;
				if (arrayType.IsVector)
				{
					name.Append("[]");
					break;
				}
				name.Append('[');
				for (int j = 1; j < arrayType.Rank; j++)
				{
					name.Append(',');
				}
				name.Append(']');
				break;
			}
			case ElementType.GenericInst:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)type;
				Collection<TypeReference> genericArguments = genericInstanceType.GenericArguments;
				name.Append('[');
				for (int i = 0; i < genericArguments.Count; i++)
				{
					if (i > 0)
					{
						name.Append(',');
					}
					TypeReference typeReference = genericArguments[i];
					bool flag = typeReference.Scope != typeReference.Module;
					if (flag)
					{
						name.Append('[');
					}
					AppendType(typeReference, name, fq_name: true, top_level: false);
					if (flag)
					{
						name.Append(']');
					}
				}
				name.Append(']');
				break;
			}
			}
		}

		private static bool RequiresFullyQualifiedName(TypeReference type, bool top_level)
		{
			if (type.Scope == type.Module)
			{
				return false;
			}
			if (type.Scope.Name == "mscorlib" && top_level)
			{
				return false;
			}
			return true;
		}
	}
	public enum MetadataType : byte
	{
		Void = 1,
		Boolean = 2,
		Char = 3,
		SByte = 4,
		Byte = 5,
		Int16 = 6,
		UInt16 = 7,
		Int32 = 8,
		UInt32 = 9,
		Int64 = 10,
		UInt64 = 11,
		Single = 12,
		Double = 13,
		String = 14,
		Pointer = 15,
		ByReference = 16,
		ValueType = 17,
		Class = 18,
		Var = 19,
		Array = 20,
		GenericInstance = 21,
		TypedByReference = 22,
		IntPtr = 24,
		UIntPtr = 25,
		FunctionPointer = 27,
		Object = 28,
		MVar = 30,
		RequiredModifier = 31,
		OptionalModifier = 32,
		Sentinel = 65,
		Pinned = 69
	}
	public class TypeReference : MemberReference, IGenericParameterProvider, IMetadataTokenProvider, IGenericContext
	{
		private string @namespace;

		private bool value_type;

		internal IMetadataScope scope;

		internal ModuleDefinition module;

		internal ElementType etype = ElementType.None;

		private string fullname;

		protected Collection<GenericParameter> generic_parameters;

		public override string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.Name)
				{
					throw new InvalidOperationException("Projected type reference name can't be changed.");
				}
				base.Name = value;
				ClearFullName();
			}
		}

		public virtual string Namespace
		{
			get
			{
				return @namespace;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != @namespace)
				{
					throw new InvalidOperationException("Projected type reference namespace can't be changed.");
				}
				@namespace = value;
				ClearFullName();
			}
		}

		public virtual bool IsValueType
		{
			get
			{
				return value_type;
			}
			set
			{
				value_type = value;
			}
		}

		public override ModuleDefinition Module
		{
			get
			{
				if (module != null)
				{
					return module;
				}
				return DeclaringType?.Module;
			}
		}

		internal TypeReferenceProjection WindowsRuntimeProjection
		{
			get
			{
				return (TypeReferenceProjection)projection;
			}
			set
			{
				projection = value;
			}
		}

		IGenericParameterProvider IGenericContext.Type => this;

		IGenericParameterProvider IGenericContext.Method => null;

		GenericParameterType IGenericParameterProvider.GenericParameterType => GenericParameterType.Type;

		public virtual bool HasGenericParameters => !generic_parameters.IsNullOrEmpty();

		public virtual Collection<GenericParameter> GenericParameters
		{
			get
			{
				if (generic_parameters == null)
				{
					Interlocked.CompareExchange(ref generic_parameters, new GenericParameterCollection(this), null);
				}
				return generic_parameters;
			}
		}

		public virtual IMetadataScope Scope
		{
			get
			{
				TypeReference declaringType = DeclaringType;
				if (declaringType != null)
				{
					return declaringType.Scope;
				}
				return scope;
			}
			set
			{
				TypeReference declaringType = DeclaringType;
				if (declaringType != null)
				{
					if (base.IsWindowsRuntimeProjection && value != declaringType.Scope)
					{
						throw new InvalidOperationException("Projected type scope can't be changed.");
					}
					declaringType.Scope = value;
				}
				else
				{
					if (base.IsWindowsRuntimeProjection && value != scope)
					{
						throw new InvalidOperationException("Projected type scope can't be changed.");
					}
					scope = value;
				}
			}
		}

		public bool IsNested => DeclaringType != null;

		public override TypeReference DeclaringType
		{
			get
			{
				return base.DeclaringType;
			}
			set
			{
				if (base.IsWindowsRuntimeProjection && value != base.DeclaringType)
				{
					throw new InvalidOperationException("Projected type declaring type can't be changed.");
				}
				base.DeclaringType = value;
				ClearFullName();
			}
		}

		public override string FullName
		{
			get
			{
				if (fullname != null)
				{
					return fullname;
				}
				string text = this.TypeFullName();
				if (IsNested)
				{
					text = DeclaringType.FullName + "/" + text;
				}
				Interlocked.CompareExchange(ref fullname, text, null);
				return fullname;
			}
		}

		public virtual bool IsByReference => false;

		public virtual bool IsPointer => false;

		public virtual bool IsSentinel => false;

		public virtual bool IsArray => false;

		public virtual bool IsGenericParameter => false;

		public virtual bool IsGenericInstance => false;

		public virtual bool IsRequiredModifier => false;

		public virtual bool IsOptionalModifier => false;

		public virtual bool IsPinned => false;

		public virtual bool IsFunctionPointer => false;

		public virtual bool IsPrimitive => etype.IsPrimitive();

		public virtual MetadataType MetadataType
		{
			get
			{
				if (etype == ElementType.None)
				{
					return IsValueType ? MetadataType.ValueType : MetadataType.Class;
				}
				return (MetadataType)etype;
			}
		}

		protected TypeReference(string @namespace, string name)
			: base(name)
		{
			this.@namespace = @namespace ?? string.Empty;
			token = new MetadataToken(TokenType.TypeRef, 0);
		}

		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope)
			: this(@namespace, name)
		{
			this.module = module;
			this.scope = scope;
		}

		public TypeReference(string @namespace, string name, ModuleDefinition module, IMetadataScope scope, bool valueType)
			: this(@namespace, name, module, scope)
		{
			value_type = valueType;
		}

		protected virtual void ClearFullName()
		{
			fullname = null;
		}

		public virtual TypeReference GetElementType()
		{
			return this;
		}

		protected override IMemberDefinition ResolveDefinition()
		{
			return Resolve();
		}

		public new virtual TypeDefinition Resolve()
		{
			ModuleDefinition moduleDefinition = Module;
			if (moduleDefinition == null)
			{
				throw new NotSupportedException();
			}
			return moduleDefinition.Resolve(this);
		}
	}
	internal sealed class TypeReferenceEqualityComparer : EqualityComparer<TypeReference>
	{
		public override bool Equals(TypeReference x, TypeReference y)
		{
			return AreEqual(x, y);
		}

		public override int GetHashCode(TypeReference obj)
		{
			return GetHashCodeFor(obj);
		}

		public static bool AreEqual(TypeReference a, TypeReference b, TypeComparisonMode comparisonMode = TypeComparisonMode.Exact)
		{
			if (a == b)
			{
				return true;
			}
			if (a == null || b == null)
			{
				return false;
			}
			MetadataType metadataType = a.MetadataType;
			MetadataType metadataType2 = b.MetadataType;
			if (metadataType == MetadataType.GenericInstance || metadataType2 == MetadataType.GenericInstance)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual((GenericInstanceType)a, (GenericInstanceType)b, comparisonMode);
			}
			if (metadataType == MetadataType.Array || metadataType2 == MetadataType.Array)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				ArrayType arrayType = (ArrayType)a;
				ArrayType arrayType2 = (ArrayType)b;
				if (arrayType.Rank != arrayType2.Rank)
				{
					return false;
				}
				return AreEqual(arrayType.ElementType, arrayType2.ElementType, comparisonMode);
			}
			if (metadataType == MetadataType.Var || metadataType2 == MetadataType.Var)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual((GenericParameter)a, (GenericParameter)b, comparisonMode);
			}
			if (metadataType == MetadataType.MVar || metadataType2 == MetadataType.MVar)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual((GenericParameter)a, (GenericParameter)b, comparisonMode);
			}
			if (metadataType == MetadataType.ByReference || metadataType2 == MetadataType.ByReference)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual(((ByReferenceType)a).ElementType, ((ByReferenceType)b).ElementType, comparisonMode);
			}
			if (metadataType == MetadataType.Pointer || metadataType2 == MetadataType.Pointer)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual(((PointerType)a).ElementType, ((PointerType)b).ElementType, comparisonMode);
			}
			if (metadataType == MetadataType.RequiredModifier || metadataType2 == MetadataType.RequiredModifier)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				RequiredModifierType requiredModifierType = (RequiredModifierType)a;
				RequiredModifierType requiredModifierType2 = (RequiredModifierType)b;
				return AreEqual(requiredModifierType.ModifierType, requiredModifierType2.ModifierType, comparisonMode) && AreEqual(requiredModifierType.ElementType, requiredModifierType2.ElementType, comparisonMode);
			}
			if (metadataType == MetadataType.OptionalModifier || metadataType2 == MetadataType.OptionalModifier)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				OptionalModifierType optionalModifierType = (OptionalModifierType)a;
				OptionalModifierType optionalModifierType2 = (OptionalModifierType)b;
				return AreEqual(optionalModifierType.ModifierType, optionalModifierType2.ModifierType, comparisonMode) && AreEqual(optionalModifierType.ElementType, optionalModifierType2.ElementType, comparisonMode);
			}
			if (metadataType == MetadataType.Pinned || metadataType2 == MetadataType.Pinned)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual(((PinnedType)a).ElementType, ((PinnedType)b).ElementType, comparisonMode);
			}
			if (metadataType == MetadataType.Sentinel || metadataType2 == MetadataType.Sentinel)
			{
				if (metadataType != metadataType2)
				{
					return false;
				}
				return AreEqual(((SentinelType)a).ElementType, ((SentinelType)b).ElementType, comparisonMode);
			}
			if (!a.Name.Equals(b.Name) || !a.Namespace.Equals(b.Namespace))
			{
				return false;
			}
			TypeDefinition typeDefinition = a.Resolve();
			TypeDefinition typeDefinition2 = b.Resolve();
			if (comparisonMode == TypeComparisonMode.SignatureOnlyLoose)
			{
				if (typeDefinition.Module.Name != typeDefinition2.Module.Name)
				{
					return false;
				}
				if (typeDefinition.Module.Assembly.Name.Name != typeDefinition2.Module.Assembly.Name.Name)
				{
					return false;
				}
				return typeDefinition.FullName == typeDefinition2.FullName;
			}
			return typeDefinition == typeDefinition2;
		}

		private static bool AreEqual(GenericParameter a, GenericParameter b, TypeComparisonMode comparisonMode = TypeComparisonMode.Exact)
		{
			if (a == b)
			{
				return true;
			}
			if (a.Position != b.Position)
			{
				return false;
			}
			if (a.Type != b.Type)
			{
				return false;
			}
			if (a.Owner is TypeReference a2 && AreEqual(a2, b.Owner as TypeReference, comparisonMode))
			{
				return true;
			}
			if (a.Owner is MethodReference x && comparisonMode != TypeComparisonMode.SignatureOnlyLoose && MethodReferenceComparer.AreEqual(x, b.Owner as MethodReference))
			{
				return true;
			}
			return comparisonMode == TypeComparisonMode.SignatureOnly || comparisonMode == TypeComparisonMode.SignatureOnlyLoose;
		}

		private static bool AreEqual(GenericInstanceType a, GenericInstanceType b, TypeComparisonMode comparisonMode = TypeComparisonMode.Exact)
		{
			if (a == b)
			{
				return true;
			}
			int count = a.GenericArguments.Count;
			if (count != b.GenericArguments.Count)
			{
				return false;
			}
			if (!AreEqual(a.ElementType, b.ElementType, comparisonMode))
			{
				return false;
			}
			for (int i = 0; i < count; i++)
			{
				if (!AreEqual(a.GenericArguments[i], b.GenericArguments[i], comparisonMode))
				{
					return false;
				}
			}
			return true;
		}

		public static int GetHashCodeFor(TypeReference obj)
		{
			MetadataType metadataType = obj.MetadataType;
			switch (metadataType)
			{
			case MetadataType.GenericInstance:
			{
				GenericInstanceType genericInstanceType = (GenericInstanceType)obj;
				int num6 = GetHashCodeFor(genericInstanceType.ElementType) * 486187739 + 31;
				for (int i = 0; i < genericInstanceType.GenericArguments.Count; i++)
				{
					num6 = num6 * 486187739 + GetHashCodeFor(genericInstanceType.GenericArguments[i]);
				}
				return num6;
			}
			case MetadataType.Array:
			{
				ArrayType arrayType = (ArrayType)obj;
				return GetHashCodeFor(arrayType.ElementType) * 486187739 + arrayType.Rank.GetHashCode();
			}
			default:
				if (metadataType != MetadataType.MVar)
				{
					switch (metadataType)
					{
					case MetadataType.ByReference:
					{
						ByReferenceType byReferenceType = (ByReferenceType)obj;
						return GetHashCodeFor(byReferenceType.ElementType) * 486187739 * 37;
					}
					case MetadataType.Pointer:
					{
						PointerType pointerType = (PointerType)obj;
						return GetHashCodeFor(pointerType.ElementType) * 486187739 * 41;
					}
					case MetadataType.RequiredModifier:
					{
						RequiredModifierType requiredModifierType = (RequiredModifierType)obj;
						int num2 = GetHashCodeFor(requiredModifierType.ElementType) * 43;
						return num2 * 486187739 + GetHashCodeFor(requiredModifierType.ModifierType);
					}
					case MetadataType.OptionalModifier:
					{
						OptionalModifierType optionalModifierType = (OptionalModifierType)obj;
						int num = GetHashCodeFor(optionalModifierType.ElementType) * 47;
						return num * 486187739 + GetHashCodeFor(optionalModifierType.ModifierType);
					}
					case MetadataType.Pinned:
					{
						PinnedType pinnedType = (PinnedType)obj;
						return GetHashCodeFor(pinnedType.ElementType) * 486187739 * 53;
					}
					case MetadataType.Sentinel:
					{
						SentinelType sentinelType = (SentinelType)obj;
						return GetHashCodeFor(sentinelType.ElementType) * 486187739 * 59;
					}
					case MetadataType.FunctionPointer:
						throw new NotImplementedException("We currently don't handle function pointer types.");
					default:
						return obj.Namespace.GetHashCode() * 486187739 + obj.FullName.GetHashCode();
					}
				}
				goto case MetadataType.Var;
			case MetadataType.Var:
			{
				GenericParameter genericParameter = (GenericParameter)obj;
				int num3 = genericParameter.Position.GetHashCode() * 486187739;
				int num4 = (int)metadataType;
				int num5 = num3 + num4.GetHashCode();
				if (genericParameter.Owner is TypeReference obj2)
				{
					return num5 * 486187739 + GetHashCodeFor(obj2);
				}
				if (genericParameter.Owner is MethodReference obj3)
				{
					return num5 * 486187739 + MethodReferenceComparer.GetHashCodeFor(obj3);
				}
				throw new InvalidOperationException("Generic parameter encountered with invalid owner");
			}
			}
		}
	}
	internal sealed class TypeResolver
	{
		private readonly IGenericInstance _typeDefinitionContext;

		private readonly IGenericInstance _methodDefinitionContext;

		public static TypeResolver For(TypeReference typeReference)
		{
			return typeReference.IsGenericInstance ? new TypeResolver((GenericInstanceType)typeReference) : new TypeResolver();
		}

		public static TypeResolver For(TypeReference typeReference, MethodReference methodReference)
		{
			return new TypeResolver(typeReference as GenericInstanceType, methodReference as GenericInstanceMethod);
		}

		public TypeResolver()
		{
		}

		public TypeResolver(GenericInstanceType typeDefinitionContext)
		{
			_typeDefinitionContext = typeDefinitionContext;
		}

		public TypeResolver(GenericInstanceMethod methodDefinitionContext)
		{
			_methodDefinitionContext = methodDefinitionContext;
		}

		public TypeResolver(GenericInstanceType typeDefinitionContext, GenericInstanceMethod methodDefinitionContext)
		{
			_typeDefinitionContext = typeDefinitionContext;
			_methodDefinitionContext = methodDefinitionContext;
		}

		public MethodReference Resolve(MethodReference method)
		{
			MethodReference result = method;
			if (IsDummy())
			{
				return result;
			}
			TypeReference declaringType = Resolve(method.DeclaringType);
			if (method is GenericInstanceMethod genericInstanceMethod)
			{
				result = new MethodReference(method.Name, method.ReturnType, declaringType);
				foreach (ParameterDefinition parameter in method.Parameters)
				{
					result.Parameters.Add(new ParameterDefinition(parameter.Name, parameter.Attributes, parameter.ParameterType));
				}
				foreach (GenericParameter genericParameter in genericInstanceMethod.ElementMethod.GenericParameters)
				{
					result.GenericParameters.Add(new GenericParameter(genericParameter.Name, result));
				}
				result.HasThis = method.HasThis;
				GenericInstanceMethod genericInstanceMethod2 = new GenericInstanceMethod(result);
				foreach (TypeReference genericArgument in genericInstanceMethod.GenericArguments)
				{
					genericInstanceMethod2.GenericArguments.Add(Resolve(genericArgument));
				}
				result = genericInstanceMethod2;
			}
			else
			{
				result = new MethodReference(method.Name, method.ReturnType, declaringType);
				foreach (GenericParameter genericParameter2 in method.GenericParameters)
				{
					result.GenericParameters.Add(new GenericParameter(genericParameter2.Name, result));
				}
				foreach (ParameterDefinition parameter2 in method.Parameters)
				{
					result.Parameters.Add(new ParameterDefinition(parameter2.Name, parameter2.Attributes, parameter2.ParameterType));
				}
				result.HasThis = method.HasThis;
			}
			return result;
		}

		public FieldReference Resolve(FieldReference field)
		{
			TypeReference typeReference = Resolve(field.DeclaringType);
			if (typeReference == field.DeclaringType)
			{
				return field;
			}
			return new FieldReference(field.Name, field.FieldType, typeReference);
		}

		public TypeReference ResolveReturnType(MethodReference method)
		{
			return Resolve(GenericParameterResolver.ResolveReturnTypeIfNeeded(method));
		}

		public TypeReference ResolveParameterType(MethodReference method, ParameterReference parameter)
		{
			return Resolve(GenericParameterResolver.ResolveParameterTypeIfNeeded(method, parameter));
		}

		public TypeReference ResolveVariableType(MethodReference method, VariableReference variable)
		{
			return Resolve(GenericParameterResolver.ResolveVariableTypeIfNeeded(method, variable));
		}

		public TypeReference ResolveFieldType(FieldReference field)
		{
			return Resolve(GenericParameterResolver.ResolveFieldTypeIfNeeded(field));
		}

		public TypeReference Resolve(TypeReference typeReference)
		{
			return Resolve(typeReference, includeTypeDefinitions: true);
		}

		public TypeReference Resolve(TypeReference typeReference, bool includeTypeDefinitions)
		{
			if (IsDummy())
			{
				return typeReference;
			}
			if (_typeDefinitionContext != null && _typeDefinitionContext.GenericArguments.Contains(typeReference))
			{
				return typeReference;
			}
			if (_methodDefinitionContext != null && _methodDefinitionContext.GenericArguments.Contains(typeReference))
			{
				return typeReference;
			}
			if (typeReference is GenericParameter genericParameter)
			{
				if (_typeDefinitionContext != null && _typeDefinitionContext.GenericArguments.Contains(genericParameter))
				{
					return genericParameter;
				}
				if (_methodDefinitionContext != null && _methodDefinitionContext.GenericArguments.Contains(genericParameter))
				{
					return genericParameter;
				}
				return ResolveGenericParameter(genericParameter);
			}
			if (typeReference is ArrayType arrayType)
			{
				return new ArrayType(Resolve(arrayType.ElementType), arrayType.Rank);
			}
			if (typeReference is PointerType pointerType)
			{
				return new PointerType(Resolve(pointerType.ElementType));
			}
			if (typeReference is ByReferenceType byReferenceType)
			{
				return new ByReferenceType(Resolve(byReferenceType.ElementType));
			}
			if (typeReference is PinnedType pinnedType)
			{
				return new PinnedType(Resolve(pinnedType.ElementType));
			}
			if (typeReference is GenericInstanceType genericInstanceType)
			{
				GenericInstanceType genericInstanceType2 = new GenericInstanceType(genericInstanceType.ElementType);
				foreach (TypeReference genericArgument in genericInstanceType.GenericArguments)
				{
					genericInstanceType2.GenericArguments.Add(Resolve(genericArgument));
				}
				return genericInstanceType2;
			}
			if (typeReference is RequiredModifierType requiredModifierType)
			{
				return Resolve(requiredModifierType.ElementType, includeTypeDefinitions);
			}
			if (includeTypeDefinitions && typeReference is TypeDefinition { HasGenericParameters: not false } typeDefinition)
			{
				GenericInstanceType genericInstanceType3 = new GenericInstanceType(typeDefinition);
				foreach (GenericParameter genericParameter2 in typeDefinition.GenericParameters)
				{
					genericInstanceType3.GenericArguments.Add(Resolve(genericParameter2));
				}
				return genericInstanceType3;
			}
			if (typeReference is TypeSpecification)
			{
				throw new NotSupportedException($"The type {typeReference.FullName} cannot be resolved correctly.");
			}
			return typeReference;
		}

		internal TypeResolver Nested(GenericInstanceMethod genericInstanceMethod)
		{
			return new TypeResolver(_typeDefinitionContext as GenericInstanceType, genericInstanceMethod);
		}

		private TypeReference ResolveGenericParameter(GenericParameter genericParameter)
		{
			if (genericParameter.Owner == null)
			{
				return HandleOwnerlessInvalidILCode(genericParameter);
			}
			if (!(genericParameter.Owner is MemberReference))
			{
				throw new NotSupportedException();
			}
			return (genericParameter.Type == GenericParameterType.Type) ? _typeDefinitionContext.GenericArguments[genericParameter.Position] : ((_methodDefinitionContext != null) ? _methodDefinitionContext.GenericArguments[genericParameter.Position] : genericParameter);
		}

		private TypeReference HandleOwnerlessInvalidILCode(GenericParameter genericParameter)
		{
			if (genericParameter.Type == GenericParameterType.Method && _typeDefinitionContext != null && genericParameter.Position < _typeDefinitionContext.GenericArguments.Count)
			{
				return _typeDefinitionContext.GenericArguments[genericParameter.Position];
			}
			return genericParameter.Module.TypeSystem.Object;
		}

		private bool IsDummy()
		{
			return _typeDefinitionContext == null && _methodDefinitionContext == null;
		}
	}
	public abstract class TypeSpecification : TypeReference
	{
		private readonly TypeReference element_type;

		public TypeReference ElementType => element_type;

		public override string Name
		{
			get
			{
				return element_type.Name;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override string Namespace
		{
			get
			{
				return element_type.Namespace;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override IMetadataScope Scope
		{
			get
			{
				return element_type.Scope;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		public override ModuleDefinition Module => element_type.Module;

		public override string FullName => element_type.FullName;

		public override bool ContainsGenericParameter => element_type.ContainsGenericParameter;

		public override MetadataType MetadataType => (MetadataType)etype;

		internal TypeSpecification(TypeReference type)
			: base(null, null)
		{
			element_type = type;
			token = new MetadataToken(TokenType.TypeSpec);
		}

		public override TypeReference GetElementType()
		{
			return element_type.GetElementType();
		}
	}
	public abstract class TypeSystem
	{
		private sealed class CoreTypeSystem : TypeSystem
		{
			public CoreTypeSystem(ModuleDefinition module)
				: base(module)
			{
			}

			internal override TypeReference LookupType(string @namespace, string name)
			{
				TypeReference typeReference = LookupTypeDefinition(@namespace, name) ?? LookupTypeForwarded(@namespace, name);
				if (typeReference != null)
				{
					return typeReference;
				}
				throw new NotSupportedException();
			}

			private TypeReference LookupTypeDefinition(string @namespace, string name)
			{
				MetadataSystem metadataSystem = module.MetadataSystem;
				if (metadataSystem.Types == null)
				{
					Initialize(module.Types);
				}
				return module.Read(new Row<string, string>(@namespace, name), delegate(Row<string, string> row, MetadataReader reader)
				{
					TypeDefinition[] types = reader.metadata.Types;
					for (int i = 0; i < types.Length; i++)
					{
						if (types[i] == null)
						{
							types[i] = reader.GetTypeDefinition((uint)(i + 1));
						}
						TypeDefinition typeDefinition = types[i];
						if (typeDefinition.Name == row.Col2 && typeDefinition.Namespace == row.Col1)
						{
							return typeDefinition;
						}
					}
					return (TypeDefinition)null;
				});
			}

			private TypeReference LookupTypeForwarded(string @namespace, string name)
			{
				if (!module.HasExportedTypes)
				{
					return null;
				}
				Collection<ExportedType> exportedTypes = module.ExportedTypes;
				for (int i = 0; i < exportedTypes.Count; i++)
				{
					ExportedType exportedType = exportedTypes[i];
					if (exportedType.Name == name && exportedType.Namespace == @namespace)
					{
						return exportedType.CreateReference();
					}
				}
				return null;
			}

			private static void Initialize(object obj)
			{
			}
		}

		private sealed class CommonTypeSystem : TypeSystem
		{
			private AssemblyNameReference core_library;

			public CommonTypeSystem(ModuleDefinition module)
				: base(module)
			{
			}

			internal override TypeReference LookupType(string @namespace, string name)
			{
				return CreateTypeReference(@namespace, name);
			}

			public AssemblyNameReference GetCoreLibraryReference()
			{
				if (core_library != null)
				{
					return core_library;
				}
				if (module.TryGetCoreLibraryReference(out core_library))
				{
					return core_library;
				}
				core_library = new AssemblyNameReference
				{
					Name = "mscorlib",
					Version = GetCorlibVersion(),
					PublicKeyToken = new byte[8] { 183, 122, 92, 86, 25, 52, 224, 137 }
				};
				module.AssemblyReferences.Add(core_library);
				return core_library;
			}

			private Version GetCorlibVersion()
			{
				switch (module.Runtime)
				{
				case TargetRuntime.Net_1_0:
				case TargetRuntime.Net_1_1:
					return new Version(1, 0, 0, 0);
				case TargetRuntime.Net_2_0:
					return new Version(2, 0, 0, 0);
				case TargetRuntime.Net_4_0:
					return new Version(4, 0, 0, 0);
				default:
					throw new NotSupportedException();
				}
			}

			private TypeReference CreateTypeReference(string @namespace, string name)
			{
				return new TypeReference(@namespace, name, module, GetCoreLibraryReference());
			}
		}

		private readonly ModuleDefinition module;

		private TypeReference type_object;

		private TypeReference type_void;

		private TypeReference type_bool;

		private TypeReference type_char;

		private TypeReference type_sbyte;

		private TypeReference type_byte;

		private TypeReference type_int16;

		private TypeReference type_uint16;

		private TypeReference type_int32;

		private TypeReference type_uint32;

		private TypeReference type_int64;

		private TypeReference type_uint64;

		private TypeReference type_single;

		private TypeReference type_double;

		private TypeReference type_intptr;

		private TypeReference type_uintptr;

		private TypeReference type_string;

		private TypeReference type_typedref;

		[Obsolete("Use CoreLibrary")]
		public IMetadataScope Corlib => CoreLibrary;

		public IMetadataScope CoreLibrary
		{
			get
			{
				if (!(this is CommonTypeSystem commonTypeSystem))
				{
					return module;
				}
				return commonTypeSystem.GetCoreLibraryReference();
			}
		}

		public TypeReference Object => type_object ?? LookupSystemType(ref type_object, "Object", ElementType.Object);

		public TypeReference Void => type_void ?? LookupSystemType(ref type_void, "Void", ElementType.Void);

		public TypeReference Boolean => type_bool ?? LookupSystemValueType(ref type_bool, "Boolean", ElementType.Boolean);

		public TypeReference Char => type_char ?? LookupSystemValueType(ref type_char, "Char", ElementType.Char);

		public TypeReference SByte => type_sbyte ?? LookupSystemValueType(ref type_sbyte, "SByte", ElementType.I1);

		public TypeReference Byte => type_byte ?? LookupSystemValueType(ref type_byte, "Byte", ElementType.U1);

		public TypeReference Int16 => type_int16 ?? LookupSystemValueType(ref type_int16, "Int16", ElementType.I2);

		public TypeReference UInt16 => type_uint16 ?? LookupSystemValueType(ref type_uint16, "UInt16", ElementType.U2);

		public TypeReference Int32 => type_int32 ?? LookupSystemValueType(ref type_int32, "Int32", ElementType.I4);

		public TypeReference UInt32 => type_uint32 ?? LookupSystemValueType(ref type_uint32, "UInt32", ElementType.U4);

		public TypeReference Int64 => type_int64 ?? LookupSystemValueType(ref type_int64, "Int64", ElementType.I8);

		public TypeReference UInt64 => type_uint64 ?? LookupSystemValueType(ref type_uint64, "UInt64", ElementType.U8);

		public TypeReference Single => type_single ?? LookupSystemValueType(ref type_single, "Single", ElementType.R4);

		public TypeReference Double => type_double ?? LookupSystemValueType(ref type_double, "Double", ElementType.R8);

		public TypeReference IntPtr => type_intptr ?? LookupSystemValueType(ref type_intptr, "IntPtr", ElementType.I);

		public TypeReference UIntPtr => type_uintptr ?? LookupSystemValueType(ref type_uintptr, "UIntPtr", ElementType.U);

		public TypeReference String => type_string ?? LookupSystemType(ref type_string, "String", ElementType.String);

		public TypeReference TypedReference => type_typedref ?? LookupSystemValueType(ref type_typedref, "TypedReference", ElementType.TypedByRef);

		private TypeSystem(ModuleDefinition module)
		{
			this.module = module;
		}

		internal static TypeSystem CreateTypeSystem(ModuleDefinition module)
		{
			if (module.IsCoreLibrary())
			{
				return new CoreTypeSystem(module);
			}
			return new CommonTypeSystem(module);
		}

		internal abstract TypeReference LookupType(string @namespace, string name);

		private TypeReference LookupSystemType(ref TypeReference reference, string name, ElementType element_type)
		{
			lock (module.SyncRoot)
			{
				if (reference != null)
				{
					return reference;
				}
				TypeReference typeReference = LookupType("System", name);
				typeReference.etype = element_type;
				return reference = typeReference;
			}
		}

		private TypeReference LookupSystemValueType(ref TypeReference typeRef, string name, ElementType element_type)
		{
			lock (module.SyncRoot)
			{
				if (typeRef != null)
				{
					return typeRef;
				}
				TypeReference typeReference = LookupType("System", name);
				typeReference.etype = element_type;
				typeReference.KnownValueType();
				return typeRef = typeReference;
			}
		}
	}
	public enum VariantType
	{
		None = 0,
		I2 = 2,
		I4 = 3,
		R4 = 4,
		R8 = 5,
		CY = 6,
		Date = 7,
		BStr = 8,
		Dispatch = 9,
		Error = 10,
		Bool = 11,
		Variant = 12,
		Unknown = 13,
		Decimal = 14,
		I1 = 16,
		UI1 = 17,
		UI2 = 18,
		UI4 = 19,
		I8 = 20,
		UI8 = 21,
		Int = 22,
		UInt = 23
	}
	internal sealed class TypeDefinitionProjection
	{
		public readonly TypeAttributes Attributes;

		public readonly string Name;

		public readonly TypeDefinitionTreatment Treatment;

		public readonly Collection<MethodDefinition> RedirectedMethods;

		public readonly Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> RedirectedInterfaces;

		public TypeDefinitionProjection(TypeDefinition type, TypeDefinitionTreatment treatment, Collection<MethodDefinition> redirectedMethods, Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> redirectedInterfaces)
		{
			Attributes = type.Attributes;
			Name = type.Name;
			Treatment = treatment;
			RedirectedMethods = redirectedMethods;
			RedirectedInterfaces = redirectedInterfaces;
		}
	}
	internal sealed class TypeReferenceProjection
	{
		public readonly string Name;

		public readonly string Namespace;

		public readonly IMetadataScope Scope;

		public readonly TypeReferenceTreatment Treatment;

		public TypeReferenceProjection(TypeReference type, TypeReferenceTreatment treatment)
		{
			Name = type.Name;
			Namespace = type.Namespace;
			Scope = type.Scope;
			Treatment = treatment;
		}
	}
	internal sealed class MethodDefinitionProjection
	{
		public readonly MethodAttributes Attributes;

		public readonly MethodImplAttributes ImplAttributes;

		public readonly string Name;

		public readonly MethodDefinitionTreatment Treatment;

		public MethodDefinitionProjection(MethodDefinition method, MethodDefinitionTreatment treatment)
		{
			Attributes = method.Attributes;
			ImplAttributes = method.ImplAttributes;
			Name = method.Name;
			Treatment = treatment;
		}
	}
	internal sealed class FieldDefinitionProjection
	{
		public readonly FieldAttributes Attributes;

		public readonly FieldDefinitionTreatment Treatment;

		public FieldDefinitionProjection(FieldDefinition field, FieldDefinitionTreatment treatment)
		{
			Attributes = field.Attributes;
			Treatment = treatment;
		}
	}
	internal sealed class CustomAttributeValueProjection
	{
		public readonly AttributeTargets Targets;

		public readonly CustomAttributeValueTreatment Treatment;

		public CustomAttributeValueProjection(AttributeTargets targets, CustomAttributeValueTreatment treatment)
		{
			Targets = targets;
			Treatment = treatment;
		}
	}
	internal sealed class WindowsRuntimeProjections
	{
		private struct ProjectionInfo
		{
			public readonly string WinRTNamespace;

			public readonly string ClrNamespace;

			public readonly string ClrName;

			public readonly string ClrAssembly;

			public readonly bool Attribute;

			public ProjectionInfo(string winrt_namespace, string clr_namespace, string clr_name, string clr_assembly, bool attribute = false)
			{
				WinRTNamespace = winrt_namespace;
				ClrNamespace = clr_namespace;
				ClrName = clr_name;
				ClrAssembly = clr_assembly;
				Attribute = attribute;
			}
		}

		private static readonly Version version = new Version(4, 0, 0, 0);

		private static readonly byte[] contract_pk_token = new byte[8] { 176, 63, 95, 127, 17, 213, 10, 58 };

		private static readonly byte[] contract_pk = new byte[160]
		{
			0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
			0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
			82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
			1, 0, 7, 209, 250, 87, 196, 174, 217, 240,
			163, 46, 132, 170, 15, 174, 253, 13, 233, 232,
			253, 106, 236, 143, 135, 251, 3, 118, 108, 131,
			76, 153, 146, 30, 178, 59, 231, 154, 217, 213,
			220, 193, 221, 154, 210, 54, 19, 33, 2, 144,
			11, 114, 60, 249, 128, 149, 127, 196, 225, 119,
			16, 143, 198, 7, 119, 79, 41, 232, 50, 14,
			146, 234, 5, 236, 228, 232, 33, 192, 165, 239,
			232, 241, 100, 92, 76, 12, 147, 193, 171, 153,
			40, 93, 98, 44, 170, 101, 44, 29, 250, 214,
			61, 116, 93, 111, 45, 229, 241, 126, 94, 175,
			15, 196, 150, 61, 38, 28, 138, 18, 67, 101,
			24, 32, 109, 192, 147, 52, 77, 90, 210, 147
		};

		private static Dictionary<string, ProjectionInfo> projections;

		private readonly ModuleDefinition module;

		private Version corlib_version = new Version(255, 255, 255, 255);

		private AssemblyNameReference[] virtual_references;

		private static Dictionary<string, ProjectionInfo> Projections
		{
			get
			{
				if (projections != null)
				{
					return projections;
				}
				Dictionary<string, ProjectionInfo> value = new Dictionary<string, ProjectionInfo>
				{
					{
						"AttributeTargets",
						new ProjectionInfo("Windows.Foundation.Metadata", "System", "AttributeTargets", "System.Runtime")
					},
					{
						"AttributeUsageAttribute",
						new ProjectionInfo("Windows.Foundation.Metadata", "System", "AttributeUsageAttribute", "System.Runtime", attribute: true)
					},
					{
						"Color",
						new ProjectionInfo("Windows.UI", "Windows.UI", "Color", "System.Runtime.WindowsRuntime")
					},
					{
						"CornerRadius",
						new ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "CornerRadius", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"DateTime",
						new ProjectionInfo("Windows.Foundation", "System", "DateTimeOffset", "System.Runtime")
					},
					{
						"Duration",
						new ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "Duration", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"DurationType",
						new ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "DurationType", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"EventHandler`1",
						new ProjectionInfo("Windows.Foundation", "System", "EventHandler`1", "System.Runtime")
					},
					{
						"EventRegistrationToken",
						new ProjectionInfo("Windows.Foundation", "System.Runtime.InteropServices.WindowsRuntime", "EventRegistrationToken", "System.Runtime.InteropServices.WindowsRuntime")
					},
					{
						"GeneratorPosition",
						new ProjectionInfo("Windows.UI.Xaml.Controls.Primitives", "Windows.UI.Xaml.Controls.Primitives", "GeneratorPosition", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"GridLength",
						new ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "GridLength", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"GridUnitType",
						new ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "GridUnitType", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"HResult",
						new ProjectionInfo("Windows.Foundation", "System", "Exception", "System.Runtime")
					},
					{
						"IBindableIterable",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections", "IEnumerable", "System.Runtime")
					},
					{
						"IBindableVector",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections", "IList", "System.Runtime")
					},
					{
						"IClosable",
						new ProjectionInfo("Windows.Foundation", "System", "IDisposable", "System.Runtime")
					},
					{
						"ICommand",
						new ProjectionInfo("Windows.UI.Xaml.Input", "System.Windows.Input", "ICommand", "System.ObjectModel")
					},
					{
						"IIterable`1",
						new ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IEnumerable`1", "System.Runtime")
					},
					{
						"IKeyValuePair`2",
						new ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "KeyValuePair`2", "System.Runtime")
					},
					{
						"IMapView`2",
						new ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IReadOnlyDictionary`2", "System.Runtime")
					},
					{
						"IMap`2",
						new ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IDictionary`2", "System.Runtime")
					},
					{
						"INotifyCollectionChanged",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "INotifyCollectionChanged", "System.ObjectModel")
					},
					{
						"INotifyPropertyChanged",
						new ProjectionInfo("Windows.UI.Xaml.Data", "System.ComponentModel", "INotifyPropertyChanged", "System.ObjectModel")
					},
					{
						"IReference`1",
						new ProjectionInfo("Windows.Foundation", "System", "Nullable`1", "System.Runtime")
					},
					{
						"IVectorView`1",
						new ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IReadOnlyList`1", "System.Runtime")
					},
					{
						"IVector`1",
						new ProjectionInfo("Windows.Foundation.Collections", "System.Collections.Generic", "IList`1", "System.Runtime")
					},
					{
						"KeyTime",
						new ProjectionInfo("Windows.UI.Xaml.Media.Animation", "Windows.UI.Xaml.Media.Animation", "KeyTime", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"Matrix",
						new ProjectionInfo("Windows.UI.Xaml.Media", "Windows.UI.Xaml.Media", "Matrix", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"Matrix3D",
						new ProjectionInfo("Windows.UI.Xaml.Media.Media3D", "Windows.UI.Xaml.Media.Media3D", "Matrix3D", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"Matrix3x2",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Matrix3x2", "System.Numerics.Vectors")
					},
					{
						"Matrix4x4",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Matrix4x4", "System.Numerics.Vectors")
					},
					{
						"NotifyCollectionChangedAction",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "NotifyCollectionChangedAction", "System.ObjectModel")
					},
					{
						"NotifyCollectionChangedEventArgs",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "NotifyCollectionChangedEventArgs", "System.ObjectModel")
					},
					{
						"NotifyCollectionChangedEventHandler",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System.Collections.Specialized", "NotifyCollectionChangedEventHandler", "System.ObjectModel")
					},
					{
						"Plane",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Plane", "System.Numerics.Vectors")
					},
					{
						"Point",
						new ProjectionInfo("Windows.Foundation", "Windows.Foundation", "Point", "System.Runtime.WindowsRuntime")
					},
					{
						"PropertyChangedEventArgs",
						new ProjectionInfo("Windows.UI.Xaml.Data", "System.ComponentModel", "PropertyChangedEventArgs", "System.ObjectModel")
					},
					{
						"PropertyChangedEventHandler",
						new ProjectionInfo("Windows.UI.Xaml.Data", "System.ComponentModel", "PropertyChangedEventHandler", "System.ObjectModel")
					},
					{
						"Quaternion",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Quaternion", "System.Numerics.Vectors")
					},
					{
						"Rect",
						new ProjectionInfo("Windows.Foundation", "Windows.Foundation", "Rect", "System.Runtime.WindowsRuntime")
					},
					{
						"RepeatBehavior",
						new ProjectionInfo("Windows.UI.Xaml.Media.Animation", "Windows.UI.Xaml.Media.Animation", "RepeatBehavior", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"RepeatBehaviorType",
						new ProjectionInfo("Windows.UI.Xaml.Media.Animation", "Windows.UI.Xaml.Media.Animation", "RepeatBehaviorType", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"Size",
						new ProjectionInfo("Windows.Foundation", "Windows.Foundation", "Size", "System.Runtime.WindowsRuntime")
					},
					{
						"Thickness",
						new ProjectionInfo("Windows.UI.Xaml", "Windows.UI.Xaml", "Thickness", "System.Runtime.WindowsRuntime.UI.Xaml")
					},
					{
						"TimeSpan",
						new ProjectionInfo("Windows.Foundation", "System", "TimeSpan", "System.Runtime")
					},
					{
						"TypeName",
						new ProjectionInfo("Windows.UI.Xaml.Interop", "System", "Type", "System.Runtime")
					},
					{
						"Uri",
						new ProjectionInfo("Windows.Foundation", "System", "Uri", "System.Runtime")
					},
					{
						"Vector2",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Vector2", "System.Numerics.Vectors")
					},
					{
						"Vector3",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Vector3", "System.Numerics.Vectors")
					},
					{
						"Vector4",
						new ProjectionInfo("Windows.Foundation.Numerics", "System.Numerics", "Vector4", "System.Numerics.Vectors")
					}
				};
				Interlocked.CompareExchange(ref projections, value, null);
				return projections;
			}
		}

		private AssemblyNameReference[] VirtualReferences
		{
			get
			{
				if (virtual_references == null)
				{
					Mixin.Read(module.AssemblyReferences);
				}
				return virtual_references;
			}
		}

		public WindowsRuntimeProjections(ModuleDefinition module)
		{
			this.module = module;
		}

		public static void Project(TypeDefinition type)
		{
			TypeDefinitionTreatment typeDefinitionTreatment = TypeDefinitionTreatment.None;
			MetadataKind metadataKind = type.Module.MetadataKind;
			Collection<MethodDefinition> redirectedMethods = null;
			Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> redirectedInterfaces = null;
			if (type.IsWindowsRuntime)
			{
				switch (metadataKind)
				{
				case MetadataKind.WindowsMetadata:
				{
					typeDefinitionTreatment = GetWellKnownTypeDefinitionTreatment(type);
					if (typeDefinitionTreatment != TypeDefinitionTreatment.None)
					{
						ApplyProjection(type, new TypeDefinitionProjection(type, typeDefinitionTreatment, redirectedMethods, redirectedInterfaces));
						return;
					}
					TypeReference baseType = type.BaseType;
					typeDefinitionTreatment = ((baseType == null || !IsAttribute(baseType)) ? GenerateRedirectionInformation(type, out redirectedMethods, out redirectedInterfaces) : TypeDefinitionTreatment.NormalAttribute);
					break;
				}
				case MetadataKind.ManagedWindowsMetadata:
					if (NeedsWindowsRuntimePrefix(type))
					{
						typeDefinitionTreatment = TypeDefinitionTreatment.PrefixWindowsRuntimeName;
					}
					break;
				}
				if ((typeDefinitionTreatment == TypeDefinitionTreatment.PrefixWindowsRuntimeName || typeDefinitionTreatment == TypeDefinitionTreatment.NormalType) && !type.IsInterface && HasAttribute(type.CustomAttributes, "Windows.UI.Xaml", "TreatAsAbstractComposableClassAttribute"))
				{
					typeDefinitionTreatment |= TypeDefinitionTreatment.Abstract;
				}
			}
			else if (metadataKind == MetadataKind.ManagedWindowsMetadata && IsClrImplementationType(type))
			{
				typeDefinitionTreatment = TypeDefinitionTreatment.UnmangleWindowsRuntimeName;
			}
			if (typeDefinitionTreatment != TypeDefinitionTreatment.None)
			{
				ApplyProjection(type, new TypeDefinitionProjection(type, typeDefinitionTreatment, redirectedMethods, redirectedInterfaces));
			}
		}

		private static TypeDefinitionTreatment GetWellKnownTypeDefinitionTreatment(TypeDefinition type)
		{
			if (!Projections.TryGetValue(type.Name, out var value))
			{
				return TypeDefinitionTreatment.None;
			}
			TypeDefinitionTreatment typeDefinitionTreatment = (value.Attribute ? TypeDefinitionTreatment.RedirectToClrAttribute : TypeDefinitionTreatment.RedirectToClrType);
			if (type.Namespace == value.ClrNamespace)
			{
				return typeDefinitionTreatment;
			}
			if (type.Namespace == value.WinRTNamespace)
			{
				return typeDefinitionTreatment | TypeDefinitionTreatment.Internal;
			}
			return TypeDefinitionTreatment.None;
		}

		private static TypeDefinitionTreatment GenerateRedirectionInformation(TypeDefinition type, out Collection<MethodDefinition> redirectedMethods, out Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>> redirectedInterfaces)
		{
			bool flag = false;
			redirectedMethods = null;
			redirectedInterfaces = null;
			foreach (InterfaceImplementation @interface in type.Interfaces)
			{
				if (IsRedirectedType(@interface.InterfaceType))
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				return TypeDefinitionTreatment.NormalType;
			}
			HashSet<TypeReference> hashSet = new HashSet<TypeReference>(new TypeReferenceEqualityComparer());
			redirectedMethods = new Collection<MethodDefinition>();
			redirectedInterfaces = new Collection<KeyValuePair<InterfaceImplementation, InterfaceImplementation>>();
			foreach (InterfaceImplementation interface2 in type.Interfaces)
			{
				TypeReference interfaceType = interface2.InterfaceType;
				if (IsRedirectedType(interfaceType))
				{
					hashSet.Add(interfaceType);
					CollectImplementedInterfaces(interfaceType, hashSet);
				}
			}
			foreach (InterfaceImplementation interface3 in type.Interfaces)
			{
				TypeReference interfaceType2 = interface3.InterfaceType;
				if (!IsRedirectedType(interface3.InterfaceType))
				{
					continue;
				}
				TypeReference elementType = interfaceType2.GetElementType();
				TypeReference typeReference = new TypeReference(elementType.Namespace, elementType.Name, elementType.Module, elementType.Scope)
				{
					DeclaringType = elementType.DeclaringType,
					projection = elementType.projection
				};
				RemoveProjection(typeReference);
				if (interfaceType2 is GenericInstanceType genericInstanceType)
				{
					GenericInstanceType genericInstanceType2 = new GenericInstanceType(typeReference);
					foreach (TypeReference genericArgument in genericInstanceType.GenericArguments)
					{
						genericInstanceType2.GenericArguments.Add(genericArgument);
					}
					typeReference = genericInstanceType2;
				}
				InterfaceImplementation value = new InterfaceImplementation(typeReference);
				redirectedInterfaces.Add(new KeyValuePair<InterfaceImplementation, InterfaceImplementation>(interface3, value));
			}
			if (!type.IsInterface)
			{
				foreach (TypeReference item in hashSet)
				{
					RedirectInterfaceMethods(item, redirectedMethods);
				}
			}
			return TypeDefinitionTreatment.RedirectImplementedMethods;
		}

		private static void CollectImplementedInterfaces(TypeReference type, HashSet<TypeReference> results)
		{
			TypeResolver typeResolver = TypeResolver.For(type);
			TypeDefinition typeDefinition = type.Resolve();
			foreach (InterfaceImplementation @interface in typeDefinition.Interfaces)
			{
				TypeReference typeReference = typeResolver.Resolve(@interface.InterfaceType);
				results.Add(typeReference);
				CollectImplementedInterfaces(typeReference, results);
			}
		}

		private static void RedirectInterfaceMethods(TypeReference interfaceType, Collection<MethodDefinition> redirectedMethods)
		{
			TypeResolver typeResolver = TypeResolver.For(interfaceType);
			TypeDefinition typeDefinition = interfaceType.Resolve();
			foreach (MethodDefinition method in typeDefinition.Methods)
			{
				MethodDefinition methodDefinition = new MethodDefinition(method.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.VtableLayoutMask, typeResolver.Resolve(method.ReturnType));
				methodDefinition.ImplAttributes = MethodImplAttributes.CodeTypeMask;
				foreach (ParameterDefinition parameter in method.Parameters)
				{
					methodDefinition.Parameters.Add(new ParameterDefinition(parameter.Name, parameter.Attributes, typeResolver.Resolve(parameter.ParameterType)));
				}
				methodDefinition.Overrides.Add(typeResolver.Resolve(method));
				redirectedMethods.Add(methodDefinition);
			}
		}

		private static bool IsRedirectedType(TypeReference type)
		{
			return type.GetElementType().projection is TypeReferenceProjection typeReferenceProjection && typeReferenceProjection.Treatment == TypeReferenceTreatment.UseProjectionInfo;
		}

		private static bool NeedsWindowsRuntimePrefix(TypeDefinition type)
		{
			if ((type.Attributes & (TypeAttributes.VisibilityMask | TypeAttributes.ClassSemanticMask)) != TypeAttributes.Public)
			{
				return false;
			}
			TypeReference baseType = type.BaseType;
			if (baseType == null || baseType.MetadataToken.TokenType != TokenType.TypeRef)
			{
				return false;
			}
			if (baseType.Namespace == "System")
			{
				switch (baseType.Name)
				{
				case "Attribute":
				case "MulticastDelegate":
				case "ValueType":
					return false;
				}
			}
			return true;
		}

		public static bool IsClrImplementationType(TypeDefinition type)
		{
			if ((type.Attributes & (TypeAttributes.VisibilityMask | TypeAttributes.SpecialName)) != TypeAttributes.SpecialName)
			{
				return false;
			}
			return type.Name.StartsWith("<CLR>");
		}

		public static void ApplyProjection(TypeDefinition type, TypeDefinitionProjection projection)
		{
			if (projection == null)
			{
				return;
			}
			TypeDefinitionTreatment treatment = projection.Treatment;
			switch (treatment & TypeDefinitionTreatment.KindMask)
			{
			case TypeDefinitionTreatment.NormalType:
				type.Attributes |= TypeAttributes.Import | TypeAttributes.WindowsRuntime;
				break;
			case TypeDefinitionTreatment.NormalAttribute:
				type.Attributes |= TypeAttributes.Sealed | TypeAttributes.WindowsRuntime;
				break;
			case TypeDefinitionTreatment.UnmangleWindowsRuntimeName:
				type.Attributes = (TypeAttributes)(((uint)type.Attributes & 0xFFFFFBFFu) | 1);
				type.Name = type.Name.Substring("<CLR>".Length);
				break;
			case TypeDefinitionTreatment.PrefixWindowsRuntimeName:
				type.Attributes = (TypeAttributes)(((uint)type.Attributes & 0xFFFFFFFEu) | 0x1000);
				type.Name = "<WinRT>" + type.Name;
				break;
			case TypeDefinitionTreatment.RedirectToClrType:
				type.Attributes = (TypeAttributes)(((uint)type.Attributes & 0xFFFFFFFEu) | 0x1000);
				break;
			case TypeDefinitionTreatment.RedirectToClrAttribute:
				type.Attributes &= ~TypeAttributes.Public;
				break;
			case TypeDefinitionTreatment.RedirectImplementedMethods:
				type.Attributes |= TypeAttributes.Import | TypeAttributes.WindowsRuntime;
				foreach (KeyValuePair<InterfaceImplementation, InterfaceImplementation> redirectedInterface in projection.RedirectedInterfaces)
				{
					type.Interfaces.Add(redirectedInterface.Value);
					foreach (CustomAttribute customAttribute in redirectedInterface.Key.CustomAttributes)
					{
						redirectedInterface.Value.CustomAttributes.Add(customAttribute);
					}
					redirectedInterface.Key.CustomAttributes.Clear();
					foreach (MethodDefinition method in type.Methods)
					{
						foreach (MethodReference @override in method.Overrides)
						{
							if (TypeReferenceEqualityComparer.AreEqual(@override.DeclaringType, redirectedInterface.Key.InterfaceType))
							{
								@override.DeclaringType = redirectedInterface.Value.InterfaceType;
							}
						}
					}
				}
				foreach (MethodDefinition redirectedMethod in projection.RedirectedMethods)
				{
					type.Methods.Add(redirectedMethod);
				}
				break;
			}
			if ((treatment & TypeDefinitionTreatment.Abstract) != TypeDefinitionTreatment.None)
			{
				type.Attributes |= TypeAttributes.Abstract;
			}
			if ((treatment & TypeDefinitionTreatment.Internal) != TypeDefinitionTreatment.None)
			{
				type.Attributes &= ~TypeAttributes.Public;
			}
			type.WindowsRuntimeProjection = projection;
		}

		public static TypeDefinitionProjection RemoveProjection(TypeDefinition type)
		{
			if (!type.IsWindowsRuntimeProjection)
			{
				return null;
			}
			TypeDefinitionProjection windowsRuntimeProjection = type.WindowsRuntimeProjection;
			type.WindowsRuntimeProjection = null;
			type.Attributes = windowsRuntimeProjection.Attributes;
			type.Name = windowsRuntimeProjection.Name;
			if (windowsRuntimeProjection.Treatment == TypeDefinitionTreatment.RedirectImplementedMethods)
			{
				foreach (MethodDefinition redirectedMethod in windowsRuntimeProjection.RedirectedMethods)
				{
					type.Methods.Remove(redirectedMethod);
				}
				foreach (KeyValuePair<InterfaceImplementation, InterfaceImplementation> redirectedInterface in windowsRuntimeProjection.RedirectedInterfaces)
				{
					foreach (MethodDefinition method in type.Methods)
					{
						foreach (MethodReference @override in method.Overrides)
						{
							if (TypeReferenceEqualityComparer.AreEqual(@override.DeclaringType, redirectedInterface.Value.InterfaceType))
							{
								@override.DeclaringType = redirectedInterface.Key.InterfaceType;
							}
						}
					}
					foreach (CustomAttribute customAttribute in redirectedInterface.Value.CustomAttributes)
					{
						redirectedInterface.Key.CustomAttributes.Add(customAttribute);
					}
					redirectedInterface.Value.CustomAttributes.Clear();
					type.Interfaces.Remove(redirectedInterface.Value);
				}
			}
			return windowsRuntimeProjection;
		}

		public static void Project(TypeReference type)
		{
			ProjectionInfo value;
			TypeReferenceTreatment typeReferenceTreatment = ((!Projections.TryGetValue(type.Name, out value) || !(value.WinRTNamespace == type.Namespace)) ? GetSpecialTypeReferenceTreatment(type) : TypeReferenceTreatment.UseProjectionInfo);
			if (typeReferenceTreatment != TypeReferenceTreatment.None)
			{
				ApplyProjection(type, new TypeReferenceProjection(type, typeReferenceTreatment));
			}
		}

		private static TypeReferenceTreatment GetSpecialTypeReferenceTreatment(TypeReference type)
		{
			if (type.Namespace == "System")
			{
				if (type.Name == "MulticastDelegate")
				{
					return TypeReferenceTreatment.SystemDelegate;
				}
				if (type.Name == "Attribute")
				{
					return TypeReferenceTreatment.SystemAttribute;
				}
			}
			return TypeReferenceTreatment.None;
		}

		private static bool IsAttribute(TypeReference type)
		{
			if (type.MetadataToken.TokenType != TokenType.TypeRef)
			{
				return false;
			}
			return type.Name == "Attribute" && type.Namespace == "System";
		}

		private static bool IsEnum(TypeReference type)
		{
			if (type.MetadataToken.TokenType != TokenType.TypeRef)
			{
				return false;
			}
			return type.Name == "Enum" && type.Namespace == "System";
		}

		public static void ApplyProjection(TypeReference type, TypeReferenceProjection projection)
		{
			if (projection != null)
			{
				switch (projection.Treatment)
				{
				case TypeReferenceTreatment.SystemDelegate:
				case TypeReferenceTreatment.SystemAttribute:
					type.Scope = type.Module.Projections.GetAssemblyReference("System.Runtime");
					break;
				case TypeReferenceTreatment.UseProjectionInfo:
				{
					ProjectionInfo projectionInfo = Projections[type.Name];
					type.Name = projectionInfo.ClrName;
					type.Namespace = projectionInfo.ClrNamespace;
					type.Scope = type.Module.Projections.GetAssemblyReference(projectionInfo.ClrAssembly);
					break;
				}
				}
				type.WindowsRuntimeProjection = projection;
			}
		}

		public static TypeReferenceProjection RemoveProjection(TypeReference type)
		{
			if (!type.IsWindowsRuntimeProjection)
			{
				return null;
			}
			TypeReferenceProjection windowsRuntimeProjection = type.WindowsRuntimeProjection;
			type.WindowsRuntimeProjection = null;
			type.Name = windowsRuntimeProjection.Name;
			type.Namespace = windowsRuntimeProjection.Namespace;
			type.Scope = windowsRuntimeProjection.Scope;
			return windowsRuntimeProjection;
		}

		public static void Project(MethodDefinition method)
		{
			MethodDefinitionTreatment methodDefinitionTreatment = MethodDefinitionTreatment.None;
			bool flag = false;
			TypeDefinition declaringType = method.DeclaringType;
			if (declaringType.IsWindowsRuntime)
			{
				if (IsClrImplementationType(declaringType))
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.None;
				}
				else if (declaringType.IsNested)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.None;
				}
				else if (declaringType.IsInterface)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.Runtime | MethodDefinitionTreatment.InternalCall;
				}
				else if (declaringType.Module.MetadataKind == MetadataKind.ManagedWindowsMetadata && !method.IsPublic)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.None;
				}
				else
				{
					flag = true;
					TypeReference baseType = declaringType.BaseType;
					if (baseType != null && baseType.MetadataToken.TokenType == TokenType.TypeRef)
					{
						switch (GetSpecialTypeReferenceTreatment(baseType))
						{
						case TypeReferenceTreatment.SystemDelegate:
							methodDefinitionTreatment = MethodDefinitionTreatment.Public | MethodDefinitionTreatment.Runtime;
							flag = false;
							break;
						case TypeReferenceTreatment.SystemAttribute:
							methodDefinitionTreatment = MethodDefinitionTreatment.Runtime | MethodDefinitionTreatment.InternalCall;
							flag = false;
							break;
						}
					}
				}
			}
			if (flag)
			{
				bool flag2 = false;
				bool flag3 = false;
				foreach (MethodReference @override in method.Overrides)
				{
					if (@override.MetadataToken.TokenType == TokenType.MemberRef && ImplementsRedirectedInterface(@override))
					{
						flag2 = true;
					}
					else
					{
						flag3 = true;
					}
				}
				if (flag2 && !flag3)
				{
					methodDefinitionTreatment = MethodDefinitionTreatment.Private | MethodDefinitionTreatment.Runtime | MethodDefinitionTreatment.InternalCall;
					flag = false;
				}
			}
			if (flag)
			{
				methodDefinitionTreatment |= GetMethodDefinitionTreatmentFromCustomAttributes(method);
			}
			if (methodDefinitionTreatment != MethodDefinitionTreatment.None)
			{
				ApplyProjection(method, new MethodDefinitionProjection(method, methodDefinitionTreatment));
			}
		}

		private static MethodDefinitionTreatment GetMethodDefinitionTreatmentFromCustomAttributes(MethodDefinition method)
		{
			MethodDefinitionTreatment methodDefinitionTreatment = MethodDefinitionTreatment.None;
			foreach (CustomAttribute customAttribute in method.CustomAttributes)
			{
				TypeReference attributeType = customAttribute.AttributeType;
				if (!(attributeType.Namespace != "Windows.UI.Xaml"))
				{
					if (attributeType.Name == "TreatAsPublicMethodAttribute")
					{
						methodDefinitionTreatment |= MethodDefinitionTreatment.Public;
					}
					else if (attributeType.Name == "TreatAsAbstractMethodAttribute")
					{
						methodDefinitionTreatment |= MethodDefinitionTreatment.Abstract;
					}
				}
			}
			return methodDefinitionTreatment;
		}

		public static void ApplyProjection(MethodDefinition method, MethodDefinitionProjection projection)
		{
			if (projection != null)
			{
				MethodDefinitionTreatment treatment = projection.Treatment;
				if ((treatment & MethodDefinitionTreatment.Abstract) != MethodDefinitionTreatment.None)
				{
					method.Attributes |= MethodAttributes.Abstract;
				}
				if ((treatment & MethodDefinitionTreatment.Private) != MethodDefinitionTreatment.None)
				{
					method.Attributes = (method.Attributes & ~MethodAttributes.MemberAccessMask) | MethodAttributes.Private;
				}
				if ((treatment & MethodDefinitionTreatment.Public) != MethodDefinitionTreatment.None)
				{
					method.Attributes = (method.Attributes & ~MethodAttributes.MemberAccessMask) | MethodAttributes.Public;
				}
				if ((treatment & MethodDefinitionTreatment.Runtime) != MethodDefinitionTreatment.None)
				{
					method.ImplAttributes |= MethodImplAttributes.CodeTypeMask;
				}
				if ((treatment & MethodDefinitionTreatment.InternalCall) != MethodDefinitionTreatment.None)
				{
					method.ImplAttributes |= MethodImplAttributes.InternalCall;
				}
				method.WindowsRuntimeProjection = projection;
			}
		}

		public static MethodDefinitionProjection RemoveProjection(MethodDefinition method)
		{
			if (!method.IsWindowsRuntimeProjection)
			{
				return null;
			}
			MethodDefinitionProjection windowsRuntimeProjection = method.WindowsRuntimeProjection;
			method.WindowsRuntimeProjection = null;
			method.Attributes = windowsRuntimeProjection.Attributes;
			method.ImplAttributes = windowsRuntimeProjection.ImplAttributes;
			method.Name = windowsRuntimeProjection.Name;
			return windowsRuntimeProjection;
		}

		public static void Project(FieldDefinition field)
		{
			FieldDefinitionTreatment fieldDefinitionTreatment = FieldDefinitionTreatment.None;
			TypeDefinition declaringType = field.DeclaringType;
			if (declaringType.Module.MetadataKind == MetadataKind.WindowsMetadata && field.IsRuntimeSpecialName && field.Name == "value__")
			{
				TypeReference baseType = declaringType.BaseType;
				if (baseType != null && IsEnum(baseType))
				{
					fieldDefinitionTreatment = FieldDefinitionTreatment.Public;
				}
			}
			if (fieldDefinitionTreatment != FieldDefinitionTreatment.None)
			{
				ApplyProjection(field, new FieldDefinitionProjection(field, fieldDefinitionTreatment));
			}
		}

		public static void ApplyProjection(FieldDefinition field, FieldDefinitionProjection projection)
		{
			if (projection != null)
			{
				if (projection.Treatment == FieldDefinitionTreatment.Public)
				{
					field.Attributes = (field.Attributes & ~FieldAttributes.FieldAccessMask) | FieldAttributes.Public;
				}
				field.WindowsRuntimeProjection = projection;
			}
		}

		public static FieldDefinitionProjection RemoveProjection(FieldDefinition field)
		{
			if (!field.IsWindowsRuntimeProjection)
			{
				return null;
			}
			FieldDefinitionProjection windowsRuntimeProjection = field.WindowsRuntimeProjection;
			field.WindowsRuntimeProjection = null;
			field.Attributes = windowsRuntimeProjection.Attributes;
			return windowsRuntimeProjection;
		}

		private static bool ImplementsRedirectedInterface(MemberReference member)
		{
			TypeReference declaringType = member.DeclaringType;
			TypeReference typeReference;
			switch (declaringType.MetadataToken.TokenType)
			{
			case TokenType.TypeRef:
				typeReference = declaringType;
				break;
			case TokenType.TypeSpec:
				if (!declaringType.IsGenericInstance)
				{
					return false;
				}
				typeReference = ((TypeSpecification)declaringType).ElementType;
				if (typeReference.MetadataType != MetadataType.Class || typeReference.MetadataToken.TokenType != TokenType.TypeRef)
				{
					return false;
				}
				break;
			default:
				return false;
			}
			TypeReferenceProjection projection = RemoveProjection(typeReference);
			bool result = false;
			if (Projections.TryGetValue(typeReference.Name, out var value) && typeReference.Namespace == value.WinRTNamespace)
			{
				result = true;
			}
			ApplyProjection(typeReference, projection);
			return result;
		}

		public void AddVirtualReferences(Collection<AssemblyNameReference> references)
		{
			AssemblyNameReference coreLibrary = GetCoreLibrary(references);
			corlib_version = coreLibrary.Version;
			coreLibrary.Version = version;
			if (virtual_references == null)
			{
				AssemblyNameReference[] assemblyReferences = GetAssemblyReferences(coreLibrary);
				Interlocked.CompareExchange(ref virtual_references, assemblyReferences, null);
			}
			AssemblyNameReference[] array = virtual_references;
			foreach (AssemblyNameReference item in array)
			{
				references.Add(item);
			}
		}

		public void RemoveVirtualReferences(Collection<AssemblyNameReference> references)
		{
			AssemblyNameReference coreLibrary = GetCoreLibrary(references);
			coreLibrary.Version = corlib_version;
			AssemblyNameReference[] virtualReferences = VirtualReferences;
			foreach (AssemblyNameReference item in virtualReferences)
			{
				references.Remove(item);
			}
		}

		private static AssemblyNameReference[] GetAssemblyReferences(AssemblyNameReference corlib)
		{
			AssemblyNameReference assemblyNameReference = new AssemblyNameReference("System.Runtime", version);
			AssemblyNameReference assemblyNameReference2 = new AssemblyNameReference("System.Runtime.InteropServices.WindowsRuntime", version);
			AssemblyNameReference assemblyNameReference3 = new AssemblyNameReference("System.ObjectModel", version);
			AssemblyNameReference assemblyNameReference4 = new AssemblyNameReference("System.Runtime.WindowsRuntime", version);
			AssemblyNameReference assemblyNameReference5 = new AssemblyNameReference("System.Runtime.WindowsRuntime.UI.Xaml", version);
			AssemblyNameReference assemblyNameReference6 = new AssemblyNameReference("System.Numerics.Vectors", version);
			if (corlib.HasPublicKey)
			{
				byte[] publicKey = (assemblyNameReference5.PublicKey = corlib.PublicKey);
				assemblyNameReference4.PublicKey = publicKey;
				byte[] array = (assemblyNameReference6.PublicKey = contract_pk);
				byte[] array3 = (assemblyNameReference3.PublicKey = array);
				publicKey = (assemblyNameReference2.PublicKey = array3);
				assemblyNameReference.PublicKey = publicKey;
			}
			else
			{
				byte[] publicKey = (assemblyNameReference5.PublicKeyToken = corlib.PublicKeyToken);
				assemblyNameReference4.PublicKeyToken = publicKey;
				byte[] array = (assemblyNameReference6.PublicKeyToken = contract_pk_token);
				byte[] array3 = (assemblyNameReference3.PublicKeyToken = array);
				publicKey = (assemblyNameReference2.PublicKeyToken = array3);
				assemblyNameReference.PublicKeyToken = publicKey;
			}
			return new AssemblyNameReference[6] { assemblyNameReference, assemblyNameReference2, assemblyNameReference3, assemblyNameReference4, assemblyNameReference5, assemblyNameReference6 };
		}

		private static AssemblyNameReference GetCoreLibrary(Collection<AssemblyNameReference> references)
		{
			foreach (AssemblyNameReference reference in references)
			{
				if (reference.Name == "mscorlib")
				{
					return reference;
				}
			}
			throw new BadImageFormatException("Missing mscorlib reference in AssemblyRef table.");
		}

		private AssemblyNameReference GetAssemblyReference(string name)
		{
			AssemblyNameReference[] virtualReferences = VirtualReferences;
			foreach (AssemblyNameReference assemblyNameReference in virtualReferences)
			{
				if (assemblyNameReference.Name == name)
				{
					return assemblyNameReference;
				}
			}
			throw new Exception();
		}

		public static void Project(ICustomAttributeProvider owner, Collection<CustomAttribute> owner_attributes, CustomAttribute attribute)
		{
			if (!IsWindowsAttributeUsageAttribute(owner, attribute))
			{
				return;
			}
			CustomAttributeValueTreatment customAttributeValueTreatment = CustomAttributeValueTreatment.None;
			TypeDefinition typeDefinition = (TypeDefinition)owner;
			if (typeDefinition.Namespace == "Windows.Foundation.Metadata")
			{
				if (typeDefinition.Name == "VersionAttribute")
				{
					customAttributeValueTreatment = CustomAttributeValueTreatment.VersionAttribute;
				}
				else if (typeDefinition.Name == "DeprecatedAttribute")
				{
					customAttributeValueTreatment = CustomAttributeValueTreatment.DeprecatedAttribute;
				}
			}
			if (customAttributeValueTreatment == CustomAttributeValueTreatment.None)
			{
				customAttributeValueTreatment = ((!HasAttribute(owner_attributes, "Windows.Foundation.Metadata", "AllowMultipleAttribute")) ? CustomAttributeValueTreatment.AllowSingle : CustomAttributeValueTreatment.AllowMultiple);
			}
			if (customAttributeValueTreatment != CustomAttributeValueTreatment.None)
			{
				AttributeTargets targets = (AttributeTargets)attribute.ConstructorArguments[0].Value;
				ApplyProjection(attribute, new CustomAttributeValueProjection(targets, customAttributeValueTreatment));
			}
		}

		private static bool IsWindowsAttributeUsageAttribute(ICustomAttributeProvider owner, CustomAttribute attribute)
		{
			if (owner.MetadataToken.TokenType != TokenType.TypeDef)
			{
				return false;
			}
			MethodReference constructor = attribute.Constructor;
			if (constructor.MetadataToken.TokenType != TokenType.MemberRef)
			{
				return false;
			}
			TypeReference declaringType = constructor.DeclaringType;
			if (declaringType.MetadataToken.TokenType != TokenType.TypeRef)
			{
				return false;
			}
			return declaringType.Name == "AttributeUsageAttribute" && declaringType.Namespace == "System";
		}

		private static bool HasAttribute(Collection<CustomAttribute> attributes, string @namespace, string name)
		{
			foreach (CustomAttribute attribute in attributes)
			{
				TypeReference attributeType = attribute.AttributeType;
				if (attributeType.Name == name && attributeType.Namespace == @namespace)
				{
					return true;
				}
			}
			return false;
		}

		public static void ApplyProjection(CustomAttribute attribute, CustomAttributeValueProjection projection)
		{
			if (projection != null)
			{
				bool flag;
				bool flag2;
				switch (projection.Treatment)
				{
				case CustomAttributeValueTreatment.AllowSingle:
					flag = false;
					flag2 = false;
					break;
				case CustomAttributeValueTreatment.AllowMultiple:
					flag = false;
					flag2 = true;
					break;
				case CustomAttributeValueTreatment.VersionAttribute:
				case CustomAttributeValueTreatment.DeprecatedAttribute:
					flag = true;
					flag2 = true;
					break;
				default:
					throw new ArgumentException();
				}
				AttributeTargets attributeTargets = (AttributeTargets)attribute.ConstructorArguments[0].Value;
				if (flag)
				{
					attributeTargets |= AttributeTargets.Constructor | AttributeTargets.Property;
				}
				attribute.ConstructorArguments[0] = new CustomAttributeArgument(attribute.ConstructorArguments[0].Type, attributeTargets);
				attribute.Properties.Add(new CustomAttributeNamedArgument("AllowMultiple", new CustomAttributeArgument(attribute.Module.TypeSystem.Boolean, flag2)));
				attribute.projection = projection;
			}
		}

		public static CustomAttributeValueProjection RemoveProjection(CustomAttribute attribute)
		{
			if (attribute.projection == null)
			{
				return null;
			}
			CustomAttributeValueProjection projection = attribute.projection;
			attribute.projection = null;
			attribute.ConstructorArguments[0] = new CustomAttributeArgument(attribute.ConstructorArguments[0].Type, projection.Targets);
			attribute.Properties.Clear();
			return projection;
		}
	}
	public struct MetadataToken : IEquatable<MetadataToken>
	{
		private readonly uint token;

		public static readonly MetadataToken Zero = new MetadataToken(0u);

		public uint RID => token & 0xFFFFFF;

		public TokenType TokenType => (TokenType)(token & 0xFF000000u);

		public MetadataToken(uint token)
		{
			this.token = token;
		}

		public MetadataToken(TokenType type)
			: this(type, 0)
		{
		}

		public MetadataToken(TokenType type, uint rid)
		{
			token = (uint)type | rid;
		}

		public MetadataToken(TokenType type, int rid)
		{
			token = (uint)type | (uint)rid;
		}

		public int ToInt32()
		{
			return (int)token;
		}

		public uint ToUInt32()
		{
			return token;
		}

		public override int GetHashCode()
		{
			return (int)token;
		}

		public bool Equals(MetadataToken other)
		{
			return other.token == token;
		}

		public override bool Equals(object obj)
		{
			if (obj is MetadataToken metadataToken)
			{
				return metadataToken.token == token;
			}
			return false;
		}

		public static bool operator ==(MetadataToken one, MetadataToken other)
		{
			return one.token == other.token;
		}

		public static bool operator !=(MetadataToken one, MetadataToken other)
		{
			return one.token != other.token;
		}

		public override string ToString()
		{
			return string.Format("[{0}:0x{1}]", TokenType, RID.ToString("x4"));
		}
	}
	public enum TokenType : uint
	{
		Module = 0u,
		TypeRef = 16777216u,
		TypeDef = 33554432u,
		Field = 67108864u,
		Method = 100663296u,
		Param = 134217728u,
		InterfaceImpl = 150994944u,
		MemberRef = 167772160u,
		CustomAttribute = 201326592u,
		Permission = 234881024u,
		Signature = 285212672u,
		Event = 335544320u,
		Property = 385875968u,
		ModuleRef = 436207616u,
		TypeSpec = 452984832u,
		Assembly = 536870912u,
		AssemblyRef = 587202560u,
		File = 637534208u,
		ExportedType = 654311424u,
		ManifestResource = 671088640u,
		GenericParam = 704643072u,
		MethodSpec = 721420288u,
		GenericParamConstraint = 738197504u,
		Document = 805306368u,
		MethodDebugInformation = 822083584u,
		LocalScope = 838860800u,
		LocalVariable = 855638016u,
		LocalConstant = 872415232u,
		ImportScope = 889192448u,
		StateMachineMethod = 905969664u,
		CustomDebugInformation = 922746880u,
		String = 1879048192u
	}
	internal static class CryptoService
	{
		private static SHA1 CreateSHA1()
		{
			return new SHA1CryptoServiceProvider();
		}

		public static byte[] GetPublicKey(WriterParameters parameters)
		{
			using RSA rsa = parameters.CreateRSA();
			byte[] array = Mono.Security.Cryptography.CryptoConvert.ToCapiPublicKeyBlob(rsa);
			byte[] array2 = new byte[12 + array.Length];
			Buffer.BlockCopy(array, 0, array2, 12, array.Length);
			array2[1] = 36;
			array2[4] = 4;
			array2[5] = 128;
			array2[8] = (byte)array.Length;
			array2[9] = (byte)(array.Length >> 8);
			array2[10] = (byte)(array.Length >> 16);
			array2[11] = (byte)(array.Length >> 24);
			return array2;
		}

		public static void StrongName(Stream stream, ImageWriter writer, WriterParameters parameters)
		{
			int strong_name_pointer;
			byte[] strong_name = CreateStrongName(parameters, HashStream(stream, writer, out strong_name_pointer));
			PatchStrongName(stream, strong_name_pointer, strong_name);
		}

		private static void PatchStrongName(Stream stream, int strong_name_pointer, byte[] strong_name)
		{
			stream.Seek(strong_name_pointer, SeekOrigin.Begin);
			stream.Write(strong_name, 0, strong_name.Length);
		}

		private static byte[] CreateStrongName(WriterParameters parameters, byte[] hash)
		{
			using RSA key = parameters.CreateRSA();
			RSAPKCS1SignatureFormatter rSAPKCS1SignatureFormatter = new RSAPKCS1SignatureFormatter(key);
			rSAPKCS1SignatureFormatter.SetHashAlgorithm("SHA1");
			byte[] array = rSAPKCS1SignatureFormatter.CreateSignature(hash);
			Array.Reverse((Array)array);
			return array;
		}

		private static byte[] HashStream(Stream stream, ImageWriter writer, out int strong_name_pointer)
		{
			Section text = writer.text;
			int headerSize = (int)writer.GetHeaderSize();
			int pointerToRawData = (int)text.PointerToRawData;
			DataDirectory strongNameSignatureDirectory = writer.GetStrongNameSignatureDirectory();
			if (strongNameSignatureDirectory.Size == 0)
			{
				throw new InvalidOperationException();
			}
			strong_name_pointer = (int)(pointerToRawData + (strongNameSignatureDirectory.VirtualAddress - text.VirtualAddress));
			int size = (int)strongNameSignatureDirectory.Size;
			SHA1 sHA = CreateSHA1();
			byte[] buffer = new byte[8192];
			using (CryptoStream dest_stream = new CryptoStream(Stream.Null, sHA, CryptoStreamMode.Write))
			{
				stream.Seek(0L, SeekOrigin.Begin);
				CopyStreamChunk(stream, dest_stream, buffer, headerSize);
				stream.Seek(pointerToRawData, SeekOrigin.Begin);
				CopyStreamChunk(stream, dest_stream, buffer, strong_name_pointer - pointerToRawData);
				stream.Seek(size, SeekOrigin.Current);
				CopyStreamChunk(stream, dest_stream, buffer, (int)(stream.Length - (strong_name_pointer + size)));
			}
			return sHA.Hash;
		}

		public static void CopyStreamChunk(Stream stream, Stream dest_stream, byte[] buffer, int length)
		{
			while (length > 0)
			{
				int num = stream.Read(buffer, 0, System.Math.Min(buffer.Length, length));
				dest_stream.Write(buffer, 0, num);
				length -= num;
			}
		}

		public static byte[] ComputeHash(string file)
		{
			if (!File.Exists(file))
			{
				return Empty<byte>.Array;
			}
			using FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
			return ComputeHash(stream);
		}

		public static byte[] ComputeHash(Stream stream)
		{
			SHA1 sHA = CreateSHA1();
			byte[] buffer = new byte[8192];
			using (CryptoStream dest_stream = new CryptoStream(Stream.Null, sHA, CryptoStreamMode.Write))
			{
				CopyStreamChunk(stream, dest_stream, buffer, (int)stream.Length);
			}
			return sHA.Hash;
		}

		public static byte[] ComputeHash(params ByteBuffer[] buffers)
		{
			SHA1 sHA = CreateSHA1();
			using (CryptoStream cryptoStream = new CryptoStream(Stream.Null, sHA, CryptoStreamMode.Write))
			{
				for (int i = 0; i < buffers.Length; i++)
				{
					cryptoStream.Write(buffers[i].buffer, 0, buffers[i].length);
				}
			}
			return sHA.Hash;
		}

		public static Guid ComputeGuid(byte[] hash)
		{
			byte[] array = new byte[16];
			Buffer.BlockCopy(hash, 0, array, 0, 16);
			array[7] = (byte)((array[7] & 0xF) | 0x40);
			array[8] = (byte)((array[8] & 0x3F) | 0x80);
			return new Guid(array);
		}
	}
}
namespace Mono.Cecil.PE
{
	internal class BinaryStreamReader : BinaryReader
	{
		public int Position
		{
			get
			{
				return (int)BaseStream.Position;
			}
			set
			{
				BaseStream.Position = value;
			}
		}

		public int Length => (int)BaseStream.Length;

		public BinaryStreamReader(Stream stream)
			: base(stream)
		{
		}

		public void Advance(int bytes)
		{
			BaseStream.Seek(bytes, SeekOrigin.Current);
		}

		public void MoveTo(uint position)
		{
			BaseStream.Seek(position, SeekOrigin.Begin);
		}

		public void Align(int align)
		{
			align--;
			int position = Position;
			Advance(((position + align) & ~align) - position);
		}

		public DataDirectory ReadDataDirectory()
		{
			return new DataDirectory(ReadUInt32(), ReadUInt32());
		}
	}
	internal class BinaryStreamWriter : BinaryWriter
	{
		public int Position
		{
			get
			{
				return (int)BaseStream.Position;
			}
			set
			{
				BaseStream.Position = value;
			}
		}

		public BinaryStreamWriter(Stream stream)
			: base(stream)
		{
		}

		public void WriteByte(byte value)
		{
			Write(value);
		}

		public void WriteUInt16(ushort value)
		{
			Write(value);
		}

		public void WriteInt16(short value)
		{
			Write(value);
		}

		public void WriteUInt32(uint value)
		{
			Write(value);
		}

		public void WriteInt32(int value)
		{
			Write(value);
		}

		public void WriteUInt64(ulong value)
		{
			Write(value);
		}

		public void WriteBytes(byte[] bytes)
		{
			Write(bytes);
		}

		public void WriteDataDirectory(DataDirectory directory)
		{
			Write(directory.VirtualAddress);
			Write(directory.Size);
		}

		public void WriteBuffer(ByteBuffer buffer)
		{
			Write(buffer.buffer, 0, buffer.length);
		}

		protected void Advance(int bytes)
		{
			BaseStream.Seek(bytes, SeekOrigin.Current);
		}

		public void Align(int align)
		{
			align--;
			int position = Position;
			int num = ((position + align) & ~align) - position;
			for (int i = 0; i < num; i++)
			{
				WriteByte(0);
			}
		}
	}
	internal class ByteBuffer
	{
		internal byte[] buffer;

		internal int length;

		internal int position;

		public ByteBuffer()
		{
			buffer = Empty<byte>.Array;
		}

		public ByteBuffer(int length)
		{
			buffer = new byte[length];
		}

		public ByteBuffer(byte[] buffer)
		{
			this.buffer = buffer ?? Empty<byte>.Array;
			length = this.buffer.Length;
		}

		public void Advance(int length)
		{
			position += length;
		}

		public byte ReadByte()
		{
			return buffer[position++];
		}

		public sbyte ReadSByte()
		{
			return (sbyte)ReadByte();
		}

		public byte[] ReadBytes(int length)
		{
			byte[] array = new byte[length];
			Buffer.BlockCopy(buffer, position, array, 0, length);
			position += length;
			return array;
		}

		public ushort ReadUInt16()
		{
			ushort result = (ushort)(buffer[position] | (buffer[position + 1] << 8));
			position += 2;
			return result;
		}

		public short ReadInt16()
		{
			return (short)ReadUInt16();
		}

		public uint ReadUInt32()
		{
			uint result = (uint)(buffer[position] | (buffer[position + 1] << 8) | (buffer[position + 2] << 16) | (buffer[position + 3] << 24));
			position += 4;
			return result;
		}

		public int ReadInt32()
		{
			return (int)ReadUInt32();
		}

		public ulong ReadUInt64()
		{
			uint num = ReadUInt32();
			uint num2 = ReadUInt32();
			return ((ulong)num2 << 32) | num;
		}

		public long ReadInt64()
		{
			return (long)ReadUInt64();
		}

		public uint ReadCompressedUInt32()
		{
			byte b = ReadByte();
			if ((b & 0x80) == 0)
			{
				return b;
			}
			if ((b & 0x40) == 0)
			{
				return (uint)(((b & -129) << 8) | ReadByte());
			}
			return (uint)(((b & -193) << 24) | (ReadByte() << 16) | (ReadByte() << 8) | ReadByte());
		}

		public int ReadCompressedInt32()
		{
			byte b = buffer[position];
			int num = (int)ReadCompressedUInt32();
			int num2 = num >> 1;
			if ((num & 1) == 0)
			{
				return num2;
			}
			switch (b & 0xC0)
			{
			case 0:
			case 64:
				return num2 - 64;
			case 128:
				return num2 - 8192;
			default:
				return num2 - 268435456;
			}
		}

		public float ReadSingle()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = ReadBytes(4);
				Array.Reverse((Array)array);
				return BitConverter.ToSingle(array, 0);
			}
			float result = BitConverter.ToSingle(buffer, position);
			position += 4;
			return result;
		}

		public double ReadDouble()
		{
			if (!BitConverter.IsLittleEndian)
			{
				byte[] array = ReadBytes(8);
				Array.Reverse((Array)array);
				return BitConverter.ToDouble(array, 0);
			}
			double result = BitConverter.ToDouble(buffer, position);
			position += 8;
			return result;
		}

		public void WriteByte(byte value)
		{
			if (position == buffer.Length)
			{
				Grow(1);
			}
			buffer[position++] = value;
			if (position > length)
			{
				length = position;
			}
		}

		public void WriteSByte(sbyte value)
		{
			WriteByte((byte)value);
		}

		public void WriteUInt16(ushort value)
		{
			if (position + 2 > buffer.Length)
			{
				Grow(2);
			}
			buffer[position++] = (byte)value;
			buffer[position++] = (byte)(value >> 8);
			if (position > length)
			{
				length = position;
			}
		}

		public void WriteInt16(short value)
		{
			WriteUInt16((ushort)value);
		}

		public void WriteUInt32(uint value)
		{
			if (position + 4 > buffer.Length)
			{
				Grow(4);
			}
			buffer[position++] = (byte)value;
			buffer[position++] = (byte)(value >> 8);
			buffer[position++] = (byte)(value >> 16);
			buffer[position++] = (byte)(value >> 24);
			if (position > length)
			{
				length = position;
			}
		}

		public void WriteInt32(int value)
		{
			WriteUInt32((uint)value);
		}

		public void WriteUInt64(ulong value)
		{
			if (position + 8 > buffer.Length)
			{
				Grow(8);
			}
			buffer[position++] = (byte)value;
			buffer[position++] = (byte)(value >> 8);
			buffer[position++] = (byte)(value >> 16);
			buffer[position++] = (byte)(value >> 24);
			buffer[position++] = (byte)(value >> 32);
			buffer[position++] = (byte)(value >> 40);
			buffer[position++] = (byte)(value >> 48);
			buffer[position++] = (byte)(value >> 56);
			if (position > length)
			{
				length = position;
			}
		}

		public void WriteInt64(long value)
		{
			WriteUInt64((ulong)value);
		}

		public void WriteCompressedUInt32(uint value)
		{
			if (value < 128)
			{
				WriteByte((byte)value);
				return;
			}
			if (value < 16384)
			{
				WriteByte((byte)(0x80 | (value >> 8)));
				WriteByte((byte)(value & 0xFF));
				return;
			}
			WriteByte((byte)((value >> 24) | 0xC0));
			WriteByte((byte)((value >> 16) & 0xFF));
			WriteByte((byte)((value >> 8) & 0xFF));
			WriteByte((byte)(value & 0xFF));
		}

		public void WriteCompressedInt32(int value)
		{
			int num = value >> 31;
			if ((value & -64) == (num & -64))
			{
				int num2 = ((value & 0x3F) << 1) | (num & 1);
				WriteByte((byte)num2);
				return;
			}
			if ((value & -8192) == (num & -8192))
			{
				int num3 = ((value & 0x1FFF) << 1) | (num & 1);
				ushort num4 = (ushort)(0x8000 | num3);
				WriteUInt16(BitConverter.IsLittleEndian ? ReverseEndianness(num4) : num4);
				return;
			}
			if ((value & -268435456) == (num & -268435456))
			{
				int num5 = ((value & 0xFFFFFFF) << 1) | (num & 1);
				uint num6 = (uint)(-1073741824 | num5);
				WriteUInt32(BitConverter.IsLittleEndian ? ReverseEndianness(num6) : num6);
				return;
			}
			throw new ArgumentOutOfRangeException("value", "valid range is -2^28 to 2^28 -1");
		}

		private static uint ReverseEndianness(uint value)
		{
			return RotateRight(value & 0xFF00FF, 8) + RotateLeft(value & 0xFF00FF00u, 8);
		}

		private static uint RotateRight(uint value, int offset)
		{
			return (value >> offset) | (value << 32 - offset);
		}

		private static uint RotateLeft(uint value, int offset)
		{
			return (value << offset) | (value >> 32 - offset);
		}

		private static ushort ReverseEndianness(ushort value)
		{
			return (ushort)((value >> 8) + (value << 8));
		}

		public void WriteBytes(byte[] bytes)
		{
			int num = bytes.Length;
			if (position + num > buffer.Length)
			{
				Grow(num);
			}
			Buffer.BlockCopy(bytes, 0, buffer, position, num);
			position += num;
			if (position > length)
			{
				length = position;
			}
		}

		public void WriteBytes(int length)
		{
			if (position + length > buffer.Length)
			{
				Grow(length);
			}
			position += length;
			if (position > this.length)
			{
				this.length = position;
			}
		}

		public void WriteBytes(ByteBuffer buffer)
		{
			if (position + buffer.length > this.buffer.Length)
			{
				Grow(buffer.length);
			}
			Buffer.BlockCopy(buffer.buffer, 0, this.buffer, position, buffer.length);
			position += buffer.length;
			if (position > length)
			{
				length = position;
			}
		}

		public void WriteSingle(float value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)bytes);
			}
			WriteBytes(bytes);
		}

		public void WriteDouble(double value)
		{
			byte[] bytes = BitConverter.GetBytes(value);
			if (!BitConverter.IsLittleEndian)
			{
				Array.Reverse((Array)bytes);
			}
			WriteBytes(bytes);
		}

		private void Grow(int desired)
		{
			byte[] array = buffer;
			int num = array.Length;
			byte[] dst = new byte[System.Math.Max(num + desired, num * 2)];
			Buffer.BlockCopy(array, 0, dst, 0, num);
			buffer = dst;
		}
	}
	internal sealed class ByteBufferEqualityComparer : IEqualityComparer<ByteBuffer>
	{
		public bool Equals(ByteBuffer x, ByteBuffer y)
		{
			if (x.length != y.length)
			{
				return false;
			}
			byte[] buffer = x.buffer;
			byte[] buffer2 = y.buffer;
			for (int i = 0; i < x.length; i++)
			{
				if (buffer[i] != buffer2[i])
				{
					return false;
				}
			}
			return true;
		}

		public int GetHashCode(ByteBuffer buffer)
		{
			int num = -2128831035;
			byte[] buffer2 = buffer.buffer;
			for (int i = 0; i < buffer.length; i++)
			{
				num = (num ^ buffer2[i]) * 16777619;
			}
			return num;
		}
	}
	internal struct DataDirectory
	{
		public readonly uint VirtualAddress;

		public readonly uint Size;

		public bool IsZero => VirtualAddress == 0 && Size == 0;

		public DataDirectory(uint rva, uint size)
		{
			VirtualAddress = rva;
			Size = size;
		}
	}
	internal sealed class Image : IDisposable
	{
		public Disposable<Stream> Stream;

		public string FileName;

		public ModuleKind Kind;

		public uint Characteristics;

		public string RuntimeVersion;

		public TargetArchitecture Architecture;

		public ModuleCharacteristics DllCharacteristics;

		public ushort LinkerVersion;

		public ushort SubSystemMajor;

		public ushort SubSystemMinor;

		public ImageDebugHeader DebugHeader;

		public Section[] Sections;

		public Section MetadataSection;

		public uint EntryPointToken;

		public uint Timestamp;

		public ModuleAttributes Attributes;

		public DataDirectory Win32Resources;

		public DataDirectory Debug;

		public DataDirectory Resources;

		public DataDirectory StrongName;

		public StringHeap StringHeap;

		public BlobHeap BlobHeap;

		public UserStringHeap UserStringHeap;

		public GuidHeap GuidHeap;

		public TableHeap TableHeap;

		public PdbHeap PdbHeap;

		private readonly int[] coded_index_sizes = new int[14];

		private readonly Func<Table, int> counter;

		public Image()
		{
			counter = GetTableLength;
		}

		public bool HasTable(Table table)
		{
			return GetTableLength(table) > 0;
		}

		public int GetTableLength(Table table)
		{
			return (int)TableHeap[table].Length;
		}

		public int GetTableIndexSize(Table table)
		{
			return (GetTableLength(table) < 65536) ? 2 : 4;
		}

		public int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return coded_index_sizes[(int)coded_index] = coded_index.GetSize(counter);
		}

		public uint ResolveVirtualAddress(uint rva)
		{
			Section sectionAtVirtualAddress = GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				throw new ArgumentOutOfRangeException();
			}
			return ResolveVirtualAddressInSection(rva, sectionAtVirtualAddress);
		}

		public uint ResolveVirtualAddressInSection(uint rva, Section section)
		{
			return rva + section.PointerToRawData - section.VirtualAddress;
		}

		public Section GetSection(string name)
		{
			Section[] sections = Sections;
			foreach (Section section in sections)
			{
				if (section.Name == name)
				{
					return section;
				}
			}
			return null;
		}

		public Section GetSectionAtVirtualAddress(uint rva)
		{
			Section[] sections = Sections;
			foreach (Section section in sections)
			{
				if (rva >= section.VirtualAddress && rva < section.VirtualAddress + section.SizeOfRawData)
				{
					return section;
				}
			}
			return null;
		}

		private BinaryStreamReader GetReaderAt(uint rva)
		{
			Section sectionAtVirtualAddress = GetSectionAtVirtualAddress(rva);
			if (sectionAtVirtualAddress == null)
			{
				return null;
			}
			BinaryStreamReader binaryStreamReader = new BinaryStreamReader(Stream.value);
			binaryStreamReader.MoveTo(ResolveVirtualAddressInSection(rva, sectionAtVirtualAddress));
			return binaryStreamReader;
		}

		public TRet GetReaderAt<TItem, TRet>(uint rva, TItem item, Func<TItem, BinaryStreamReader, TRet> read) where TRet : class
		{
			long position = Stream.value.Position;
			try
			{
				BinaryStreamReader readerAt = GetReaderAt(rva);
				if (readerAt == null)
				{
					return null;
				}
				return read(item, readerAt);
			}
			finally
			{
				Stream.value.Position = position;
			}
		}

		public bool HasDebugTables()
		{
			return HasTable(Table.Document) || HasTable(Table.MethodDebugInformation) || HasTable(Table.LocalScope) || HasTable(Table.LocalVariable) || HasTable(Table.LocalConstant) || HasTable(Table.StateMachineMethod) || HasTable(Table.CustomDebugInformation);
		}

		public void Dispose()
		{
			Stream.Dispose();
		}
	}
	internal sealed class ImageReader : BinaryStreamReader
	{
		private readonly Image image;

		private DataDirectory cli;

		private DataDirectory metadata;

		private uint table_heap_offset;

		private uint pdb_heap_offset;

		public ImageReader(Disposable<Stream> stream, string file_name)
			: base(stream.value)
		{
			image = new Image();
			image.Stream = stream;
			image.FileName = file_name;
		}

		private void MoveTo(DataDirectory directory)
		{
			BaseStream.Position = image.ResolveVirtualAddress(directory.VirtualAddress);
		}

		private void ReadImage()
		{
			if (BaseStream.Length < 128)
			{
				throw new BadImageFormatException();
			}
			if (ReadUInt16() != 23117)
			{
				throw new BadImageFormatException();
			}
			Advance(58);
			MoveTo(ReadUInt32());
			if (ReadUInt32() != 17744)
			{
				throw new BadImageFormatException();
			}
			image.Architecture = ReadArchitecture();
			ushort count = ReadUInt16();
			image.Timestamp = ReadUInt32();
			Advance(10);
			ushort characteristics = ReadUInt16();
			ReadOptionalHeaders(out var subsystem, out var dll_characteristics);
			ReadSections(count);
			ReadCLIHeader();
			ReadMetadata();
			ReadDebugHeader();
			image.Characteristics = characteristics;
			image.Kind = GetModuleKind(characteristics, subsystem);
			image.DllCharacteristics = (ModuleCharacteristics)dll_characteristics;
		}

		private TargetArchitecture ReadArchitecture()
		{
			return (TargetArchitecture)ReadUInt16();
		}

		private static ModuleKind GetModuleKind(ushort characteristics, ushort subsystem)
		{
			if ((characteristics & 0x2000) != 0)
			{
				return ModuleKind.Dll;
			}
			if (subsystem == 2 || subsystem == 9)
			{
				return ModuleKind.Windows;
			}
			return ModuleKind.Console;
		}

		private void ReadOptionalHeaders(out ushort subsystem, out ushort dll_characteristics)
		{
			bool flag = ReadUInt16() == 523;
			image.LinkerVersion = ReadUInt16();
			Advance(44);
			image.SubSystemMajor = ReadUInt16();
			image.SubSystemMinor = ReadUInt16();
			Advance(16);
			subsystem = ReadUInt16();
			dll_characteristics = ReadUInt16();
			Advance(flag ? 56 : 40);
			image.Win32Resources = ReadDataDirectory();
			Advance(24);
			image.Debug = ReadDataDirectory();
			Advance(56);
			cli = ReadDataDirectory();
			if (cli.IsZero)
			{
				throw new BadImageFormatException();
			}
			Advance(8);
		}

		private string ReadAlignedString(int length)
		{
			int num = 0;
			char[] array = new char[length];
			while (num < length)
			{
				byte b = ReadByte();
				if (b == 0)
				{
					break;
				}
				array[num++] = (char)b;
			}
			Advance(-1 + ((num + 4) & -4) - num);
			return new string(array, 0, num);
		}

		private string ReadZeroTerminatedString(int length)
		{
			int num = 0;
			char[] array = new char[length];
			byte[] array2 = ReadBytes(length);
			while (num < length)
			{
				byte b = array2[num];
				if (b == 0)
				{
					break;
				}
				array[num++] = (char)b;
			}
			return new string(array, 0, num);
		}

		private void ReadSections(ushort count)
		{
			Section[] array = new Section[count];
			for (int i = 0; i < count; i++)
			{
				Section section = new Section();
				section.Name = ReadZeroTerminatedString(8);
				Advance(4);
				section.VirtualAddress = ReadUInt32();
				section.SizeOfRawData = ReadUInt32();
				section.PointerToRawData = ReadUInt32();
				Advance(16);
				array[i] = section;
			}
			image.Sections = array;
		}

		private void ReadCLIHeader()
		{
			MoveTo(cli);
			Advance(8);
			metadata = ReadDataDirectory();
			image.Attributes = (ModuleAttributes)ReadUInt32();
			image.EntryPointToken = ReadUInt32();
			image.Resources = ReadDataDirectory();
			image.StrongName = ReadDataDirectory();
		}

		private void ReadMetadata()
		{
			MoveTo(metadata);
			if (ReadUInt32() != 1112167234)
			{
				throw new BadImageFormatException();
			}
			Advance(8);
			image.RuntimeVersion = ReadZeroTerminatedString(ReadInt32());
			Advance(2);
			ushort num = ReadUInt16();
			Section sectionAtVirtualAddress = image.GetSectionAtVirtualAddress(metadata.VirtualAddress);
			if (sectionAtVirtualAddress == null)
			{
				throw new BadImageFormatException();
			}
			image.MetadataSection = sectionAtVirtualAddress;
			for (int i = 0; i < num; i++)
			{
				ReadMetadataStream(sectionAtVirtualAddress);
			}
			if (image.PdbHeap != null)
			{
				ReadPdbHeap();
			}
			if (image.TableHeap != null)
			{
				ReadTableHeap();
			}
		}

		private void ReadDebugHeader()
		{
			if (image.Debug.IsZero)
			{
				image.DebugHeader = new ImageDebugHeader(Empty<ImageDebugHeaderEntry>.Array);
				return;
			}
			MoveTo(image.Debug);
			ImageDebugHeaderEntry[] array = new ImageDebugHeaderEntry[(int)image.Debug.Size / 28];
			for (int i = 0; i < array.Length; i++)
			{
				ImageDebugDirectory directory = new ImageDebugDirectory
				{
					Characteristics = ReadInt32(),
					TimeDateStamp = ReadInt32(),
					MajorVersion = ReadInt16(),
					MinorVersion = ReadInt16(),
					Type = (ImageDebugType)ReadInt32(),
					SizeOfData = ReadInt32(),
					AddressOfRawData = ReadInt32(),
					PointerToRawData = ReadInt32()
				};
				if (directory.PointerToRawData == 0 || directory.SizeOfData < 0)
				{
					array[i] = new ImageDebugHeaderEntry(directory, Empty<byte>.Array);
					continue;
				}
				int position = base.Position;
				try
				{
					MoveTo((uint)directory.PointerToRawData);
					byte[] data = ReadBytes(directory.SizeOfData);
					array[i] = new ImageDebugHeaderEntry(directory, data);
				}
				finally
				{
					base.Position = position;
				}
			}
			image.DebugHeader = new ImageDebugHeader(array);
		}

		private void ReadMetadataStream(Section section)
		{
			uint offset = metadata.VirtualAddress - section.VirtualAddress + ReadUInt32();
			uint size = ReadUInt32();
			byte[] data = ReadHeapData(offset, size);
			switch (ReadAlignedString(16))
			{
			case "#~":
			case "#-":
				image.TableHeap = new TableHeap(data);
				table_heap_offset = offset;
				break;
			case "#Strings":
				image.StringHeap = new StringHeap(data);
				break;
			case "#Blob":
				image.BlobHeap = new BlobHeap(data);
				break;
			case "#GUID":
				image.GuidHeap = new GuidHeap(data);
				break;
			case "#US":
				image.UserStringHeap = new UserStringHeap(data);
				break;
			case "#Pdb":
				image.PdbHeap = new PdbHeap(data);
				pdb_heap_offset = offset;
				break;
			}
		}

		private byte[] ReadHeapData(uint offset, uint size)
		{
			long position = BaseStream.Position;
			MoveTo(offset + image.MetadataSection.PointerToRawData);
			byte[] result = ReadBytes((int)size);
			BaseStream.Position = position;
			return result;
		}

		private void ReadTableHeap()
		{
			TableHeap tableHeap = image.TableHeap;
			MoveTo(table_heap_offset + image.MetadataSection.PointerToRawData);
			Advance(6);
			byte sizes = ReadByte();
			Advance(1);
			tableHeap.Valid = ReadInt64();
			tableHeap.Sorted = ReadInt64();
			if (image.PdbHeap != null)
			{
				for (int i = 0; i < 58; i++)
				{
					if (image.PdbHeap.HasTable((Table)i))
					{
						tableHeap.Tables[i].Length = image.PdbHeap.TypeSystemTableRows[i];
					}
				}
			}
			for (int j = 0; j < 58; j++)
			{
				if (tableHeap.HasTable((Table)j))
				{
					tableHeap.Tables[j].Length = ReadUInt32();
				}
			}
			SetIndexSize(image.StringHeap, sizes, 1);
			SetIndexSize(image.GuidHeap, sizes, 2);
			SetIndexSize(image.BlobHeap, sizes, 4);
			ComputeTableInformations();
		}

		private static void SetIndexSize(Heap heap, uint sizes, byte flag)
		{
			if (heap != null)
			{
				heap.IndexSize = (((sizes & flag) != 0) ? 4 : 2);
			}
		}

		private int GetTableIndexSize(Table table)
		{
			return image.GetTableIndexSize(table);
		}

		private int GetCodedIndexSize(CodedIndex index)
		{
			return image.GetCodedIndexSize(index);
		}

		private void ComputeTableInformations()
		{
			uint num = (uint)((int)BaseStream.Position - (int)table_heap_offset) - image.MetadataSection.PointerToRawData;
			int num2 = ((image.StringHeap != null) ? image.StringHeap.IndexSize : 2);
			int num3 = ((image.GuidHeap != null) ? image.GuidHeap.IndexSize : 2);
			int num4 = ((image.BlobHeap != null) ? image.BlobHeap.IndexSize : 2);
			TableHeap tableHeap = image.TableHeap;
			TableInformation[] tables = tableHeap.Tables;
			for (int i = 0; i < 58; i++)
			{
				Table table = (Table)i;
				if (tableHeap.HasTable(table))
				{
					int num5 = table switch
					{
						Table.Module => 2 + num2 + num3 * 3, 
						Table.TypeRef => GetCodedIndexSize(CodedIndex.ResolutionScope) + num2 * 2, 
						Table.TypeDef => 4 + num2 * 2 + GetCodedIndexSize(CodedIndex.TypeDefOrRef) + GetTableIndexSize(Table.Field) + GetTableIndexSize(Table.Method), 
						Table.FieldPtr => GetTableIndexSize(Table.Field), 
						Table.Field => 2 + num2 + num4, 
						Table.MethodPtr => GetTableIndexSize(Table.Method), 
						Table.Method => 8 + num2 + num4 + GetTableIndexSize(Table.Param), 
						Table.ParamPtr => GetTableIndexSize(Table.Param), 
						Table.Param => 4 + num2, 
						Table.InterfaceImpl => GetTableIndexSize(Table.TypeDef) + GetCodedIndexSize(CodedIndex.TypeDefOrRef), 
						Table.MemberRef => GetCodedIndexSize(CodedIndex.MemberRefParent) + num2 + num4, 
						Table.Constant => 2 + GetCodedIndexSize(CodedIndex.HasConstant) + num4, 
						Table.CustomAttribute => GetCodedIndexSize(CodedIndex.HasCustomAttribute) + GetCodedIndexSize(CodedIndex.CustomAttributeType) + num4, 
						Table.FieldMarshal => GetCodedIndexSize(CodedIndex.HasFieldMarshal) + num4, 
						Table.DeclSecurity => 2 + GetCodedIndexSize(CodedIndex.HasDeclSecurity) + num4, 
						Table.ClassLayout => 6 + GetTableIndexSize(Table.TypeDef), 
						Table.FieldLayout => 4 + GetTableIndexSize(Table.Field), 
						Table.StandAloneSig => num4, 
						Table.EventMap => GetTableIndexSize(Table.TypeDef) + GetTableIndexSize(Table.Event), 
						Table.EventPtr => GetTableIndexSize(Table.Event), 
						Table.Event => 2 + num2 + GetCodedIndexSize(CodedIndex.TypeDefOrRef), 
						Table.PropertyMap => GetTableIndexSize(Table.TypeDef) + GetTableIndexSize(Table.Property), 
						Table.PropertyPtr => GetTableIndexSize(Table.Property), 
						Table.Property => 2 + num2 + num4, 
						Table.MethodSemantics => 2 + GetTableIndexSize(Table.Method) + GetCodedIndexSize(CodedIndex.HasSemantics), 
						Table.MethodImpl => GetTableIndexSize(Table.TypeDef) + GetCodedIndexSize(CodedIndex.MethodDefOrRef) + GetCodedIndexSize(CodedIndex.MethodDefOrRef), 
						Table.ModuleRef => num2, 
						Table.TypeSpec => num4, 
						Table.ImplMap => 2 + GetCodedIndexSize(CodedIndex.MemberForwarded) + num2 + GetTableIndexSize(Table.ModuleRef), 
						Table.FieldRVA => 4 + GetTableIndexSize(Table.Field), 
						Table.EncLog => 8, 
						Table.EncMap => 4, 
						Table.Assembly => 16 + num4 + num2 * 2, 
						Table.AssemblyProcessor => 4, 
						Table.AssemblyOS => 12, 
						Table.AssemblyRef => 12 + num4 * 2 + num2 * 2, 
						Table.AssemblyRefProcessor => 4 + GetTableIndexSize(Table.AssemblyRef), 
						Table.AssemblyRefOS => 12 + GetTableIndexSize(Table.AssemblyRef), 
						Table.File => 4 + num2 + num4, 
						Table.ExportedType => 8 + num2 * 2 + GetCodedIndexSize(CodedIndex.Implementation), 
						Table.ManifestResource => 8 + num2 + GetCodedIndexSize(CodedIndex.Implementation), 
						Table.NestedClass => GetTableIndexSize(Table.TypeDef) + GetTableIndexSize(Table.TypeDef), 
						Table.GenericParam => 4 + GetCodedIndexSize(CodedIndex.TypeOrMethodDef) + num2, 
						Table.MethodSpec => GetCodedIndexSize(CodedIndex.MethodDefOrRef) + num4, 
						Table.GenericParamConstraint => GetTableIndexSize(Table.GenericParam) + GetCodedIndexSize(CodedIndex.TypeDefOrRef), 
						Table.Document => num4 + num3 + num4 + num3, 
						Table.MethodDebugInformation => GetTableIndexSize(Table.Document) + num4, 
						Table.LocalScope => GetTableIndexSize(Table.Method) + GetTableIndexSize(Table.ImportScope) + GetTableIndexSize(Table.LocalVariable) + GetTableIndexSize(Table.LocalConstant) + 8, 
						Table.LocalVariable => 4 + num2, 
						Table.LocalConstant => num2 + num4, 
						Table.ImportScope => GetTableIndexSize(Table.ImportScope) + num4, 
						Table.StateMachineMethod => GetTableIndexSize(Table.Method) + GetTableIndexSize(Table.Method), 
						Table.CustomDebugInformation => GetCodedIndexSize(CodedIndex.HasCustomDebugInformation) + num3 + num4, 
						_ => throw new NotSupportedException(), 
					};
					tables[i].RowSize = (uint)num5;
					tables[i].Offset = num;
					num += (uint)(num5 * (int)tables[i].Length);
				}
			}
		}

		private void ReadPdbHeap()
		{
			PdbHeap pdbHeap = image.PdbHeap;
			ByteBuffer byteBuffer = new ByteBuffer(pdbHeap.data);
			pdbHeap.Id = byteBuffer.ReadBytes(20);
			pdbHeap.EntryPoint = byteBuffer.ReadUInt32();
			pdbHeap.TypeSystemTables = byteBuffer.ReadInt64();
			pdbHeap.TypeSystemTableRows = new uint[58];
			for (int i = 0; i < 58; i++)
			{
				Table table = (Table)i;
				if (pdbHeap.HasTable(table))
				{
					pdbHeap.TypeSystemTableRows[i] = byteBuffer.ReadUInt32();
				}
			}
		}

		public static Image ReadImage(Disposable<Stream> stream, string file_name)
		{
			try
			{
				ImageReader imageReader = new ImageReader(stream, file_name);
				imageReader.ReadImage();
				return imageReader.image;
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(stream.value.GetFileName(), inner);
			}
		}

		public static Image ReadPortablePdb(Disposable<Stream> stream, string file_name, out uint pdb_heap_offset)
		{
			try
			{
				ImageReader imageReader = new ImageReader(stream, file_name);
				uint num = (uint)stream.value.Length;
				imageReader.image.Sections = new Section[1]
				{
					new Section
					{
						PointerToRawData = 0u,
						SizeOfRawData = num,
						VirtualAddress = 0u,
						VirtualSize = num
					}
				};
				imageReader.metadata = new DataDirectory(0u, num);
				imageReader.ReadMetadata();
				pdb_heap_offset = imageReader.pdb_heap_offset;
				return imageReader.image;
			}
			catch (EndOfStreamException inner)
			{
				throw new BadImageFormatException(stream.value.GetFileName(), inner);
			}
		}
	}
	internal sealed class ImageWriter : BinaryStreamWriter
	{
		private readonly ModuleDefinition module;

		private readonly MetadataBuilder metadata;

		private readonly TextMap text_map;

		internal readonly Disposable<Stream> stream;

		private readonly string runtime_version;

		private ImageDebugHeader debug_header;

		private ByteBuffer win32_resources;

		private const uint pe_header_size = 152u;

		private const uint section_header_size = 40u;

		private const uint file_alignment = 512u;

		private const uint section_alignment = 8192u;

		private const ulong image_base = 4194304uL;

		internal const uint text_rva = 8192u;

		private readonly bool pe64;

		private readonly bool has_reloc;

		internal Section text;

		internal Section rsrc;

		internal Section reloc;

		private ushort sections;

		internal long debug_header_entries_position;

		private ImageWriter(ModuleDefinition module, string runtime_version, MetadataBuilder metadata, Disposable<Stream> stream, bool metadataOnly = false)
			: base(stream.value)
		{
			this.module = module;
			this.runtime_version = runtime_version;
			text_map = metadata.text_map;
			this.stream = stream;
			this.metadata = metadata;
			if (!metadataOnly)
			{
				pe64 = module.Architecture == TargetArchitecture.AMD64 || module.Architecture == TargetArchitecture.IA64 || module.Architecture == TargetArchitecture.ARM64;
				has_reloc = module.Architecture == TargetArchitecture.I386;
				GetDebugHeader();
				GetWin32Resources();
				BuildTextMap();
				sections = (ushort)((!has_reloc) ? 1u : 2u);
			}
		}

		private void GetDebugHeader()
		{
			ISymbolWriter symbol_writer = metadata.symbol_writer;
			if (symbol_writer != null)
			{
				debug_header = symbol_writer.GetDebugHeader();
			}
			if (module.HasDebugHeader)
			{
				ImageDebugHeader debugHeader = module.GetDebugHeader();
				ImageDebugHeaderEntry deterministicEntry = debugHeader.GetDeterministicEntry();
				if (deterministicEntry != null)
				{
					debug_header = debug_header.AddDeterministicEntry();
				}
			}
		}

		private void GetWin32Resources()
		{
			if (!module.HasImage)
			{
				return;
			}
			DataDirectory win32Resources = module.Image.Win32Resources;
			uint size = win32Resources.Size;
			if (size != 0)
			{
				win32_resources = module.Image.GetReaderAt(win32Resources.VirtualAddress, size, (uint s, BinaryStreamReader reader) => new ByteBuffer(reader.ReadBytes((int)s)));
			}
		}

		public static ImageWriter CreateWriter(ModuleDefinition module, MetadataBuilder metadata, Disposable<Stream> stream)
		{
			ImageWriter imageWriter = new ImageWriter(module, module.runtime_version, metadata, stream);
			imageWriter.BuildSections();
			return imageWriter;
		}

		public static ImageWriter CreateDebugWriter(ModuleDefinition module, MetadataBuilder metadata, Disposable<Stream> stream)
		{
			ImageWriter imageWriter = new ImageWriter(module, "PDB v1.0", metadata, stream, metadataOnly: true);
			uint length = metadata.text_map.GetLength();
			imageWriter.text = new Section
			{
				SizeOfRawData = length,
				VirtualSize = length
			};
			return imageWriter;
		}

		private void BuildSections()
		{
			bool flag = win32_resources != null;
			if (flag)
			{
				sections++;
			}
			text = CreateSection(".text", text_map.GetLength(), null);
			Section previous = text;
			if (flag)
			{
				rsrc = CreateSection(".rsrc", (uint)win32_resources.length, previous);
				PatchWin32Resources(win32_resources);
				previous = rsrc;
			}
			if (has_reloc)
			{
				reloc = CreateSection(".reloc", 12u, previous);
			}
		}

		private Section CreateSection(string name, uint size, Section previous)
		{
			return new Section
			{
				Name = name,
				VirtualAddress = ((previous != null) ? (previous.VirtualAddress + Align(previous.VirtualSize, 8192u)) : 8192u),
				VirtualSize = size,
				PointerToRawData = ((previous != null) ? (previous.PointerToRawData + previous.SizeOfRawData) : Align(GetHeaderSize(), 512u)),
				SizeOfRawData = Align(size, 512u)
			};
		}

		private static uint Align(uint value, uint align)
		{
			align--;
			return (value + align) & ~align;
		}

		private void WriteDOSHeader()
		{
			Write(new byte[128]
			{
				77, 90, 144, 0, 3, 0, 0, 0, 4, 0,
				0, 0, 255, 255, 0, 0, 184, 0, 0, 0,
				0, 0, 0, 0, 64, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
				128, 0, 0, 0, 14, 31, 186, 14, 0, 180,
				9, 205, 33, 184, 1, 76, 205, 33, 84, 104,
				105, 115, 32, 112, 114, 111, 103, 114, 97, 109,
				32, 99, 97, 110, 110, 111, 116, 32, 98, 101,
				32, 114, 117, 110, 32, 105, 110, 32, 68, 79,
				83, 32, 109, 111, 100, 101, 46, 13, 13, 10,
				36, 0, 0, 0, 0, 0, 0, 0
			});
		}

		private ushort SizeOfOptionalHeader()
		{
			return (ushort)((!pe64) ? 224u : 240u);
		}

		private void WritePEFileHeader()
		{
			WriteUInt32(17744u);
			WriteUInt16((ushort)module.Architecture);
			WriteUInt16(sections);
			WriteUInt32(metadata.timestamp);
			WriteUInt32(0u);
			WriteUInt32(0u);
			WriteUInt16(SizeOfOptionalHeader());
			ushort num = (ushort)(2 | ((!pe64) ? 256 : 32));
			if (module.Kind == ModuleKind.Dll || module.Kind == ModuleKind.NetModule)
			{
				num |= 0x2000;
			}
			if (module.Image != null && (module.Image.Characteristics & 0x20) != 0)
			{
				num |= 0x20;
			}
			WriteUInt16(num);
		}

		private Section LastSection()
		{
			if (reloc != null)
			{
				return reloc;
			}
			if (rsrc != null)
			{
				return rsrc;
			}
			return text;
		}

		private void WriteOptionalHeaders()
		{
			WriteUInt16((ushort)((!pe64) ? 267u : 523u));
			WriteUInt16(module.linker_version);
			WriteUInt32(text.SizeOfRawData);
			WriteUInt32(((reloc != null) ? reloc.SizeOfRawData : 0) + ((rsrc != null) ? rsrc.SizeOfRawData : 0));
			WriteUInt32(0u);
			Range range = text_map.GetRange(TextSegment.StartupStub);
			WriteUInt32((range.Length != 0) ? range.Start : 0u);
			WriteUInt32(8192u);
			if (!pe64)
			{
				WriteUInt32(0u);
				WriteUInt32(4194304u);
			}
			else
			{
				WriteUInt64(4194304uL);
			}
			WriteUInt32(8192u);
			WriteUInt32(512u);
			WriteUInt16(4);
			WriteUInt16(0);
			WriteUInt16(0);
			WriteUInt16(0);
			WriteUInt16(module.subsystem_major);
			WriteUInt16(module.subsystem_minor);
			WriteUInt32(0u);
			Section section = LastSection();
			WriteUInt32(section.VirtualAddress + Align(section.VirtualSize, 8192u));
			WriteUInt32(text.PointerToRawData);
			WriteUInt32(0u);
			WriteUInt16(GetSubSystem());
			WriteUInt16((ushort)module.Characteristics);
			if (!pe64)
			{
				WriteUInt32(1048576u);
				WriteUInt32(4096u);
				WriteUInt32(1048576u);
				WriteUInt32(4096u);
			}
			else
			{
				WriteUInt64(4194304uL);
				WriteUInt64(16384uL);
				WriteUInt64(1048576uL);
				WriteUInt64(8192uL);
			}
			WriteUInt32(0u);
			WriteUInt32(16u);
			WriteZeroDataDirectory();
			WriteDataDirectory(text_map.GetDataDirectory(TextSegment.ImportDirectory));
			if (rsrc != null)
			{
				WriteUInt32(rsrc.VirtualAddress);
				WriteUInt32(rsrc.VirtualSize);
			}
			else
			{
				WriteZeroDataDirectory();
			}
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteUInt32((reloc != null) ? reloc.VirtualAddress : 0u);
			WriteUInt32((reloc != null) ? reloc.VirtualSize : 0u);
			if (text_map.GetLength(TextSegment.DebugDirectory) > 0)
			{
				WriteUInt32(text_map.GetRVA(TextSegment.DebugDirectory));
				WriteUInt32((uint)(debug_header.Entries.Length * 28));
			}
			else
			{
				WriteZeroDataDirectory();
			}
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteDataDirectory(text_map.GetDataDirectory(TextSegment.ImportAddressTable));
			WriteZeroDataDirectory();
			WriteDataDirectory(text_map.GetDataDirectory(TextSegment.CLIHeader));
			WriteZeroDataDirectory();
		}

		private void WriteZeroDataDirectory()
		{
			WriteUInt32(0u);
			WriteUInt32(0u);
		}

		private ushort GetSubSystem()
		{
			switch (module.Kind)
			{
			case ModuleKind.Dll:
			case ModuleKind.Console:
			case ModuleKind.NetModule:
				return 3;
			case ModuleKind.Windows:
				return 2;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private void WriteSectionHeaders()
		{
			WriteSection(text, 1610612768u);
			if (rsrc != null)
			{
				WriteSection(rsrc, 1073741888u);
			}
			if (reloc != null)
			{
				WriteSection(reloc, 1107296320u);
			}
		}

		private void WriteSection(Section section, uint characteristics)
		{
			byte[] array = new byte[8];
			string name = section.Name;
			for (int i = 0; i < name.Length; i++)
			{
				array[i] = (byte)name[i];
			}
			WriteBytes(array);
			WriteUInt32(section.VirtualSize);
			WriteUInt32(section.VirtualAddress);
			WriteUInt32(section.SizeOfRawData);
			WriteUInt32(section.PointerToRawData);
			WriteUInt32(0u);
			WriteUInt32(0u);
			WriteUInt16(0);
			WriteUInt16(0);
			WriteUInt32(characteristics);
		}

		private uint GetRVAFileOffset(Section section, uint rva)
		{
			return section.PointerToRawData + rva - section.VirtualAddress;
		}

		private void MoveTo(uint pointer)
		{
			BaseStream.Seek(pointer, SeekOrigin.Begin);
		}

		private void MoveToRVA(Section section, uint rva)
		{
			BaseStream.Seek(GetRVAFileOffset(section, rva), SeekOrigin.Begin);
		}

		internal void MoveToRVA(TextSegment segment)
		{
			MoveToRVA(text, text_map.GetRVA(segment));
		}

		private void WriteRVA(uint rva)
		{
			if (!pe64)
			{
				WriteUInt32(rva);
			}
			else
			{
				WriteUInt64(rva);
			}
		}

		private void PrepareSection(Section section)
		{
			MoveTo(section.PointerToRawData);
			if (section.SizeOfRawData <= 4096)
			{
				Write(new byte[section.SizeOfRawData]);
				MoveTo(section.PointerToRawData);
				return;
			}
			int i = 0;
			byte[] buffer = new byte[4096];
			int num;
			for (; i != section.SizeOfRawData; i += num)
			{
				num = System.Math.Min((int)section.SizeOfRawData - i, 4096);
				Write(buffer, 0, num);
			}
			MoveTo(section.PointerToRawData);
		}

		private void WriteText()
		{
			PrepareSection(text);
			if (has_reloc)
			{
				WriteRVA(text_map.GetRVA(TextSegment.ImportHintNameTable));
				WriteRVA(0u);
			}
			WriteUInt32(72u);
			WriteUInt16(2);
			WriteUInt16((ushort)((module.Runtime > TargetRuntime.Net_1_1) ? 5u : 0u));
			WriteUInt32(text_map.GetRVA(TextSegment.MetadataHeader));
			WriteUInt32(GetMetadataLength());
			WriteUInt32((uint)module.Attributes);
			WriteUInt32(metadata.entry_point.ToUInt32());
			WriteDataDirectory(text_map.GetDataDirectory(TextSegment.Resources));
			WriteDataDirectory(text_map.GetDataDirectory(TextSegment.StrongNameSignature));
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			WriteZeroDataDirectory();
			MoveToRVA(TextSegment.Code);
			WriteBuffer(metadata.code);
			MoveToRVA(TextSegment.Resources);
			WriteBuffer(metadata.resources);
			if (metadata.data.length > 0)
			{
				MoveToRVA(TextSegment.Data);
				WriteBuffer(metadata.data);
			}
			MoveToRVA(TextSegment.MetadataHeader);
			WriteMetadataHeader();
			WriteMetadata();
			if (text_map.GetLength(TextSegment.DebugDirectory) > 0)
			{
				MoveToRVA(TextSegment.DebugDirectory);
				WriteDebugDirectory();
			}
			if (has_reloc)
			{
				MoveToRVA(TextSegment.ImportDirectory);
				WriteImportDirectory();
				MoveToRVA(TextSegment.StartupStub);
				WriteStartupStub();
			}
		}

		private uint GetMetadataLength()
		{
			return text_map.GetRVA(TextSegment.DebugDirectory) - text_map.GetRVA(TextSegment.MetadataHeader);
		}

		public void WriteMetadataHeader()
		{
			WriteUInt32(1112167234u);
			WriteUInt16(1);
			WriteUInt16(1);
			WriteUInt32(0u);
			byte[] zeroTerminatedString = GetZeroTerminatedString(runtime_version);
			WriteUInt32((uint)zeroTerminatedString.Length);
			WriteBytes(zeroTerminatedString);
			WriteUInt16(0);
			WriteUInt16(GetStreamCount());
			uint offset = text_map.GetRVA(TextSegment.TableHeap) - text_map.GetRVA(TextSegment.MetadataHeader);
			WriteStreamHeader(ref offset, TextSegment.TableHeap, "#~");
			WriteStreamHeader(ref offset, TextSegment.StringHeap, "#Strings");
			WriteStreamHeader(ref offset, TextSegment.UserStringHeap, "#US");
			WriteStreamHeader(ref offset, TextSegment.GuidHeap, "#GUID");
			WriteStreamHeader(ref offset, TextSegment.BlobHeap, "#Blob");
			WriteStreamHeader(ref offset, TextSegment.PdbHeap, "#Pdb");
		}

		private ushort GetStreamCount()
		{
			return (ushort)((uint)(2 + ((!metadata.user_string_heap.IsEmpty) ? 1 : 0) + ((!metadata.guid_heap.IsEmpty) ? 1 : 0) + ((!metadata.blob_heap.IsEmpty) ? 1 : 0)) + ((metadata.pdb_heap != null) ? 1u : 0u));
		}

		private void WriteStreamHeader(ref uint offset, TextSegment heap, string name)
		{
			uint length = (uint)text_map.GetLength(heap);
			if (length != 0)
			{
				WriteUInt32(offset);
				WriteUInt32(length);
				WriteBytes(GetZeroTerminatedString(name));
				offset += length;
			}
		}

		private static int GetZeroTerminatedStringLength(string @string)
		{
			return (@string.Length + 1 + 3) & -4;
		}

		private static byte[] GetZeroTerminatedString(string @string)
		{
			return GetString(@string, GetZeroTerminatedStringLength(@string));
		}

		private static byte[] GetSimpleString(string @string)
		{
			return GetString(@string, @string.Length);
		}

		private static byte[] GetString(string @string, int length)
		{
			byte[] array = new byte[length];
			for (int i = 0; i < @string.Length; i++)
			{
				array[i] = (byte)@string[i];
			}
			return array;
		}

		public void WriteMetadata()
		{
			WriteHeap(TextSegment.TableHeap, metadata.table_heap);
			WriteHeap(TextSegment.StringHeap, metadata.string_heap);
			WriteHeap(TextSegment.UserStringHeap, metadata.user_string_heap);
			WriteHeap(TextSegment.GuidHeap, metadata.guid_heap);
			WriteHeap(TextSegment.BlobHeap, metadata.blob_heap);
			WriteHeap(TextSegment.PdbHeap, metadata.pdb_heap);
		}

		private void WriteHeap(TextSegment heap, HeapBuffer buffer)
		{
			if (buffer != null && !buffer.IsEmpty)
			{
				MoveToRVA(heap);
				WriteBuffer(buffer);
			}
		}

		private void WriteDebugDirectory()
		{
			int num = (int)BaseStream.Position + debug_header.Entries.Length * 28;
			for (int i = 0; i < debug_header.Entries.Length; i++)
			{
				ImageDebugHeaderEntry imageDebugHeaderEntry = debug_header.Entries[i];
				ImageDebugDirectory directory = imageDebugHeaderEntry.Directory;
				WriteInt32(directory.Characteristics);
				WriteInt32(directory.TimeDateStamp);
				WriteInt16(directory.MajorVersion);
				WriteInt16(directory.MinorVersion);
				WriteInt32((int)directory.Type);
				WriteInt32(directory.SizeOfData);
				WriteInt32(directory.AddressOfRawData);
				WriteInt32(num);
				num += imageDebugHeaderEntry.Data.Length;
			}
			debug_header_entries_position = BaseStream.Position;
			for (int j = 0; j < debug_header.Entries.Length; j++)
			{
				ImageDebugHeaderEntry imageDebugHeaderEntry2 = debug_header.Entries[j];
				WriteBytes(imageDebugHeaderEntry2.Data);
			}
		}

		private void WriteImportDirectory()
		{
			WriteUInt32(text_map.GetRVA(TextSegment.ImportDirectory) + 40);
			WriteUInt32(0u);
			WriteUInt32(0u);
			WriteUInt32(text_map.GetRVA(TextSegment.ImportHintNameTable) + 14);
			WriteUInt32(text_map.GetRVA(TextSegment.ImportAddressTable));
			Advance(20);
			WriteUInt32(text_map.GetRVA(TextSegment.ImportHintNameTable));
			MoveToRVA(TextSegment.ImportHintNameTable);
			WriteUInt16(0);
			WriteBytes(GetRuntimeMain());
			WriteByte(0);
			WriteBytes(GetSimpleString("mscoree.dll"));
			WriteUInt16(0);
		}

		private byte[] GetRuntimeMain()
		{
			return (module.Kind == ModuleKind.Dll || module.Kind == ModuleKind.NetModule) ? GetSimpleString("_CorDllMain") : GetSimpleString("_CorExeMain");
		}

		private void WriteStartupStub()
		{
			TargetArchitecture architecture = module.Architecture;
			TargetArchitecture targetArchitecture = architecture;
			if (targetArchitecture == TargetArchitecture.I386)
			{
				WriteUInt16(9727);
				WriteUInt32(4194304 + text_map.GetRVA(TextSegment.ImportAddressTable));
				return;
			}
			throw new NotSupportedException();
		}

		private void WriteRsrc()
		{
			PrepareSection(rsrc);
			WriteBuffer(win32_resources);
		}

		private void WriteReloc()
		{
			PrepareSection(reloc);
			uint rVA = text_map.GetRVA(TextSegment.StartupStub);
			rVA += (uint)((module.Architecture == TargetArchitecture.IA64) ? 32 : 2);
			uint num = rVA & 0xFFFFF000u;
			WriteUInt32(num);
			WriteUInt32(12u);
			TargetArchitecture architecture = module.Architecture;
			TargetArchitecture targetArchitecture = architecture;
			if (targetArchitecture == TargetArchitecture.I386)
			{
				WriteUInt32(12288 + rVA - num);
				return;
			}
			throw new NotSupportedException();
		}

		public void WriteImage()
		{
			WriteDOSHeader();
			WritePEFileHeader();
			WriteOptionalHeaders();
			WriteSectionHeaders();
			WriteText();
			if (rsrc != null)
			{
				WriteRsrc();
			}
			if (reloc != null)
			{
				WriteReloc();
			}
			Flush();
		}

		private void BuildTextMap()
		{
			TextMap textMap = text_map;
			textMap.AddMap(TextSegment.Code, metadata.code.length, (!pe64) ? 4 : 16);
			textMap.AddMap(TextSegment.Resources, metadata.resources.length, 8);
			textMap.AddMap(TextSegment.Data, metadata.data.length, metadata.data.BufferAlign);
			if (metadata.data.length > 0)
			{
				metadata.table_heap.FixupData(textMap.GetRVA(TextSegment.Data));
			}
			textMap.AddMap(TextSegment.StrongNameSignature, GetStrongNameLength(), 4);
			BuildMetadataTextMap();
			int length = 0;
			if (debug_header != null && debug_header.HasEntries)
			{
				int num = debug_header.Entries.Length * 28;
				int num2 = (int)textMap.GetNextRVA(TextSegment.BlobHeap) + num;
				int num3 = 0;
				for (int i = 0; i < debug_header.Entries.Length; i++)
				{
					ImageDebugHeaderEntry imageDebugHeaderEntry = debug_header.Entries[i];
					ImageDebugDirectory directory = imageDebugHeaderEntry.Directory;
					directory.AddressOfRawData = ((imageDebugHeaderEntry.Data.Length != 0) ? num2 : 0);
					imageDebugHeaderEntry.Directory = directory;
					num3 += imageDebugHeaderEntry.Data.Length;
					num2 += imageDebugHeaderEntry.Data.Length;
				}
				length = num + num3;
			}
			textMap.AddMap(TextSegment.DebugDirectory, length, 4);
			if (!has_reloc)
			{
				uint nextRVA = textMap.GetNextRVA(TextSegment.DebugDirectory);
				textMap.AddMap(TextSegment.ImportDirectory, new Range(nextRVA, 0u));
				textMap.AddMap(TextSegment.ImportHintNameTable, new Range(nextRVA, 0u));
				textMap.AddMap(TextSegment.StartupStub, new Range(nextRVA, 0u));
				return;
			}
			uint nextRVA2 = textMap.GetNextRVA(TextSegment.DebugDirectory);
			uint num4 = nextRVA2 + 48;
			num4 = (num4 + 15) & 0xFFFFFFF0u;
			uint num5 = num4 - nextRVA2 + 27;
			uint num6 = nextRVA2 + num5;
			num6 = ((module.Architecture == TargetArchitecture.IA64) ? ((num6 + 15) & 0xFFFFFFF0u) : (2 + ((num6 + 3) & 0xFFFFFFFCu)));
			textMap.AddMap(TextSegment.ImportDirectory, new Range(nextRVA2, num5));
			textMap.AddMap(TextSegment.ImportHintNameTable, new Range(num4, 0u));
			textMap.AddMap(TextSegment.StartupStub, new Range(num6, GetStartupStubLength()));
		}

		public void BuildMetadataTextMap()
		{
			TextMap textMap = text_map;
			textMap.AddMap(TextSegment.MetadataHeader, GetMetadataHeaderLength(module.RuntimeVersion));
			textMap.AddMap(TextSegment.TableHeap, metadata.table_heap.length, 4);
			textMap.AddMap(TextSegment.StringHeap, metadata.string_heap.length, 4);
			textMap.AddMap(TextSegment.UserStringHeap, (!metadata.user_string_heap.IsEmpty) ? metadata.user_string_heap.length : 0, 4);
			textMap.AddMap(TextSegment.GuidHeap, metadata.guid_heap.length, 4);
			textMap.AddMap(TextSegment.BlobHeap, (!metadata.blob_heap.IsEmpty) ? metadata.blob_heap.length : 0, 4);
			textMap.AddMap(TextSegment.PdbHeap, (metadata.pdb_heap != null) ? metadata.pdb_heap.length : 0, 4);
		}

		private uint GetStartupStubLength()
		{
			TargetArchitecture architecture = module.Architecture;
			TargetArchitecture targetArchitecture = architecture;
			if (targetArchitecture == TargetArchitecture.I386)
			{
				return 6u;
			}
			throw new NotSupportedException();
		}

		private int GetMetadataHeaderLength(string runtimeVersion)
		{
			return 20 + GetZeroTerminatedStringLength(runtimeVersion) + 12 + 20 + ((!metadata.user_string_heap.IsEmpty) ? 12 : 0) + 16 + ((!metadata.blob_heap.IsEmpty) ? 16 : 0) + ((metadata.pdb_heap != null) ? 16 : 0);
		}

		private int GetStrongNameLength()
		{
			if (module.kind == ModuleKind.NetModule || module.Assembly == null)
			{
				return 0;
			}
			byte[] publicKey = module.Assembly.Name.PublicKey;
			if (publicKey.IsNullOrEmpty())
			{
				return 0;
			}
			int num = publicKey.Length;
			if (num > 32)
			{
				return num - 32;
			}
			return 128;
		}

		public DataDirectory GetStrongNameSignatureDirectory()
		{
			return text_map.GetDataDirectory(TextSegment.StrongNameSignature);
		}

		public uint GetHeaderSize()
		{
			return (uint)(152 + SizeOfOptionalHeader() + sections * 40);
		}

		private void PatchWin32Resources(ByteBuffer resources)
		{
			PatchResourceDirectoryTable(resources);
		}

		private void PatchResourceDirectoryTable(ByteBuffer resources)
		{
			resources.Advance(12);
			int num = resources.ReadUInt16() + resources.ReadUInt16();
			for (int i = 0; i < num; i++)
			{
				PatchResourceDirectoryEntry(resources);
			}
		}

		private void PatchResourceDirectoryEntry(ByteBuffer resources)
		{
			resources.Advance(4);
			uint num = resources.ReadUInt32();
			int position = resources.position;
			resources.position = (int)(num & 0x7FFFFFFF);
			if ((num & 0x80000000u) != 0)
			{
				PatchResourceDirectoryTable(resources);
			}
			else
			{
				PatchResourceDataEntry(resources);
			}
			resources.position = position;
		}

		private void PatchResourceDataEntry(ByteBuffer resources)
		{
			uint num = resources.ReadUInt32();
			resources.position -= 4;
			resources.WriteUInt32(num - module.Image.Win32Resources.VirtualAddress + rsrc.VirtualAddress);
		}
	}
	internal sealed class Section
	{
		public string Name;

		public uint VirtualAddress;

		public uint VirtualSize;

		public uint SizeOfRawData;

		public uint PointerToRawData;
	}
	internal enum TextSegment
	{
		ImportAddressTable,
		CLIHeader,
		Code,
		Resources,
		Data,
		StrongNameSignature,
		MetadataHeader,
		TableHeap,
		StringHeap,
		UserStringHeap,
		GuidHeap,
		BlobHeap,
		PdbHeap,
		DebugDirectory,
		ImportDirectory,
		ImportHintNameTable,
		StartupStub
	}
	internal sealed class TextMap
	{
		private readonly Range[] map = new Range[17];

		public void AddMap(TextSegment segment, int length)
		{
			map[(int)segment] = new Range(GetStart(segment), (uint)length);
		}

		private uint AlignUp(uint value, uint align)
		{
			align--;
			return (value + align) & ~align;
		}

		public void AddMap(TextSegment segment, int length, int align)
		{
			int num = (int)segment;
			uint num2;
			if (num != 0)
			{
				num--;
				Range range = map[num];
				num2 = AlignUp(range.Start + range.Length, (uint)align);
				map[num].Length = num2 - range.Start;
			}
			else
			{
				num2 = 8192u;
				Debug.Assert(num2 == AlignUp(num2, (uint)align));
			}
			Debug.Assert(num2 == GetStart(segment));
			map[(int)segment] = new Range(num2, (uint)length);
		}

		public void AddMap(TextSegment segment, Range range)
		{
			map[(int)segment] = range;
		}

		public Range GetRange(TextSegment segment)
		{
			return map[(int)segment];
		}

		public DataDirectory GetDataDirectory(TextSegment segment)
		{
			Range range = map[(int)segment];
			return new DataDirectory((range.Length != 0) ? range.Start : 0u, range.Length);
		}

		public uint GetRVA(TextSegment segment)
		{
			return map[(int)segment].Start;
		}

		public uint GetNextRVA(TextSegment segment)
		{
			return map[(int)segment].Start + map[(int)segment].Length;
		}

		public int GetLength(TextSegment segment)
		{
			return (int)map[(int)segment].Length;
		}

		private uint GetStart(TextSegment segment)
		{
			return (segment == TextSegment.ImportAddressTable) ? 8192u : ComputeStart((int)segment);
		}

		private uint ComputeStart(int index)
		{
			index--;
			return map[index].Start + map[index].Length;
		}

		public uint GetLength()
		{
			Range range = map[16];
			return range.Start - 8192 + range.Length;
		}
	}
}
namespace Mono.Cecil.Metadata
{
	internal sealed class BlobHeap : Heap
	{
		public BlobHeap(byte[] data)
			: base(data)
		{
		}

		public byte[] Read(uint index)
		{
			if (index == 0 || index > data.Length - 1)
			{
				return Empty<byte>.Array;
			}
			int position = (int)index;
			int num = (int)data.ReadCompressedUInt32(ref position);
			if (num > data.Length - position)
			{
				return Empty<byte>.Array;
			}
			byte[] array = new byte[num];
			Buffer.BlockCopy(data, position, array, 0, num);
			return array;
		}

		public void GetView(uint signature, out byte[] buffer, out int index, out int length)
		{
			if (signature == 0 || signature > data.Length - 1)
			{
				buffer = null;
				index = (length = 0);
			}
			else
			{
				buffer = data;
				index = (int)signature;
				length = (int)buffer.ReadCompressedUInt32(ref index);
			}
		}
	}
	internal sealed class TableHeapBuffer : HeapBuffer
	{
		private readonly ModuleDefinition module;

		private readonly MetadataBuilder metadata;

		internal readonly TableInformation[] table_infos = new TableInformation[58];

		internal readonly MetadataTable[] tables = new MetadataTable[58];

		private bool large_string;

		private bool large_blob;

		private bool large_guid;

		private readonly int[] coded_index_sizes = new int[14];

		private readonly Func<Table, int> counter;

		internal uint[] string_offsets;

		public override bool IsEmpty => false;

		public TableHeapBuffer(ModuleDefinition module, MetadataBuilder metadata)
			: base(24)
		{
			this.module = module;
			this.metadata = metadata;
			counter = GetTableLength;
		}

		private int GetTableLength(Table table)
		{
			return (int)table_infos[(uint)table].Length;
		}

		public TTable GetTable<TTable>(Table table) where TTable : MetadataTable, new()
		{
			TTable val = (TTable)tables[(uint)table];
			if (val != null)
			{
				return val;
			}
			val = new TTable();
			tables[(uint)table] = val;
			return val;
		}

		public void WriteBySize(uint value, int size)
		{
			if (size == 4)
			{
				WriteUInt32(value);
			}
			else
			{
				WriteUInt16((ushort)value);
			}
		}

		public void WriteBySize(uint value, bool large)
		{
			if (large)
			{
				WriteUInt32(value);
			}
			else
			{
				WriteUInt16((ushort)value);
			}
		}

		public void WriteString(uint @string)
		{
			WriteBySize(string_offsets[@string], large_string);
		}

		public void WriteBlob(uint blob)
		{
			WriteBySize(blob, large_blob);
		}

		public void WriteGuid(uint guid)
		{
			WriteBySize(guid, large_guid);
		}

		public void WriteRID(uint rid, Table table)
		{
			WriteBySize(rid, table_infos[(uint)table].IsLarge);
		}

		private int GetCodedIndexSize(CodedIndex coded_index)
		{
			int num = coded_index_sizes[(int)coded_index];
			if (num != 0)
			{
				return num;
			}
			return coded_index_sizes[(int)coded_index] = coded_index.GetSize(counter);
		}

		public void WriteCodedRID(uint rid, CodedIndex coded_index)
		{
			WriteBySize(rid, GetCodedIndexSize(coded_index));
		}

		public void WriteTableHeap()
		{
			WriteUInt32(0u);
			WriteByte(GetTableHeapVersion());
			WriteByte(0);
			WriteByte(GetHeapSizes());
			WriteByte(10);
			WriteUInt64(GetValid());
			WriteUInt64(55193285546867200uL);
			WriteRowCount();
			WriteTables();
		}

		private void WriteRowCount()
		{
			for (int i = 0; i < tables.Length; i++)
			{
				MetadataTable metadataTable = tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					WriteUInt32((uint)metadataTable.Length);
				}
			}
		}

		private void WriteTables()
		{
			for (int i = 0; i < tables.Length; i++)
			{
				MetadataTable metadataTable = tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					metadataTable.Write(this);
				}
			}
		}

		private ulong GetValid()
		{
			ulong num = 0uL;
			for (int i = 0; i < tables.Length; i++)
			{
				MetadataTable metadataTable = tables[i];
				if (metadataTable != null && metadataTable.Length != 0)
				{
					metadataTable.Sort();
					num |= (ulong)(1L << i);
				}
			}
			return num;
		}

		public void ComputeTableInformations()
		{
			if (metadata.metadata_builder != null)
			{
				ComputeTableInformations(metadata.metadata_builder.table_heap);
			}
			ComputeTableInformations(metadata.table_heap);
		}

		private void ComputeTableInformations(TableHeapBuffer table_heap)
		{
			MetadataTable[] array = table_heap.tables;
			for (int i = 0; i < array.Length; i++)
			{
				MetadataTable metadataTable = array[i];
				if (metadataTable != null && metadataTable.Length > 0)
				{
					table_infos[i].Length = (uint)metadataTable.Length;
				}
			}
		}

		private byte GetHeapSizes()
		{
			byte b = 0;
			if (metadata.string_heap.IsLarge)
			{
				large_string = true;
				b |= 1;
			}
			if (metadata.guid_heap.IsLarge)
			{
				large_guid = true;
				b |= 2;
			}
			if (metadata.blob_heap.IsLarge)
			{
				large_blob = true;
				b |= 4;
			}
			return b;
		}

		private byte GetTableHeapVersion()
		{
			TargetRuntime runtime = module.Runtime;
			TargetRuntime targetRuntime = runtime;
			if ((uint)targetRuntime <= 1u)
			{
				return 1;
			}
			return 2;
		}

		public void FixupData(uint data_rva)
		{
			FieldRVATable table = GetTable<FieldRVATable>(Table.FieldRVA);
			if (table.length != 0)
			{
				int num = (GetTable<FieldTable>(Table.Field).IsLarge ? 4 : 2);
				int num2 = position;
				position = table.position;
				for (int i = 0; i < table.length; i++)
				{
					uint num3 = ReadUInt32();
					position -= 4;
					WriteUInt32(num3 + data_rva);
					position += num;
				}
				position = num2;
			}
		}
	}
	internal sealed class ResourceBuffer : ByteBuffer
	{
		public ResourceBuffer()
			: base(0)
		{
		}

		public uint AddResource(byte[] resource)
		{
			uint result = (uint)position;
			WriteInt32(resource.Length);
			WriteBytes(resource);
			return result;
		}
	}
	internal sealed class DataBuffer : ByteBuffer
	{
		private int buffer_align = 4;

		public int BufferAlign => buffer_align;

		public DataBuffer()
			: base(0)
		{
		}

		private void Align(int align)
		{
			align--;
			WriteBytes(((position + align) & ~align) - position);
		}

		public uint AddData(byte[] data, int align)
		{
			if (buffer_align < align)
			{
				buffer_align = align;
			}
			Align(align);
			uint result = (uint)position;
			WriteBytes(data);
			return result;
		}
	}
	internal abstract class HeapBuffer : ByteBuffer
	{
		public bool IsLarge => length > 65535;

		public abstract bool IsEmpty { get; }

		protected HeapBuffer(int length)
			: base(length)
		{
		}
	}
	internal sealed class GuidHeapBuffer : HeapBuffer
	{
		private readonly Dictionary<Guid, uint> guids = new Dictionary<Guid, uint>();

		public override bool IsEmpty => length == 0;

		public GuidHeapBuffer()
			: base(16)
		{
		}

		public uint GetGuidIndex(Guid guid)
		{
			if (guids.TryGetValue(guid, out var value))
			{
				return value;
			}
			value = (uint)(guids.Count + 1);
			WriteGuid(guid);
			guids.Add(guid, value);
			return value;
		}

		private void WriteGuid(Guid guid)
		{
			WriteBytes(guid.ToByteArray());
		}
	}
	internal class StringHeapBuffer : HeapBuffer
	{
		private class SuffixSort : IComparer<KeyValuePair<string, uint>>
		{
			public int Compare(KeyValuePair<string, uint> xPair, KeyValuePair<string, uint> yPair)
			{
				string key = xPair.Key;
				string key2 = yPair.Key;
				int num = key.Length - 1;
				int num2 = key2.Length - 1;
				while (num >= 0 && num2 >= 0)
				{
					if (key[num] < key2[num2])
					{
						return -1;
					}
					if (key[num] > key2[num2])
					{
						return 1;
					}
					num--;
					num2--;
				}
				return key2.Length.CompareTo(key.Length);
			}
		}

		protected Dictionary<string, uint> strings = new Dictionary<string, uint>(StringComparer.Ordinal);

		public sealed override bool IsEmpty => length <= 1;

		public StringHeapBuffer()
			: base(1)
		{
			WriteByte(0);
		}

		public virtual uint GetStringIndex(string @string)
		{
			if (strings.TryGetValue(@string, out var value))
			{
				return value;
			}
			value = (uint)(strings.Count + 1);
			strings.Add(@string, value);
			return value;
		}

		public uint[] WriteStrings()
		{
			List<KeyValuePair<string, uint>> list = SortStrings(strings);
			strings = null;
			uint[] array = new uint[list.Count + 1];
			array[0] = 0u;
			string text = string.Empty;
			foreach (KeyValuePair<string, uint> item in list)
			{
				string key = item.Key;
				uint value = item.Value;
				int num = position;
				if (text.EndsWith(key, StringComparison.Ordinal) && !IsLowSurrogateChar(item.Key[0]))
				{
					array[value] = (uint)(num - (Encoding.UTF8.GetByteCount(item.Key) + 1));
				}
				else
				{
					array[value] = (uint)num;
					WriteString(key);
				}
				text = item.Key;
			}
			return array;
		}

		private static List<KeyValuePair<string, uint>> SortStrings(Dictionary<string, uint> strings)
		{
			List<KeyValuePair<string, uint>> list = new List<KeyValuePair<string, uint>>(strings);
			list.Sort(new SuffixSort());
			return list;
		}

		private static bool IsLowSurrogateChar(int c)
		{
			return (uint)(c - 56320) <= 1023u;
		}

		protected virtual void WriteString(string @string)
		{
			WriteBytes(Encoding.UTF8.GetBytes(@string));
			WriteByte(0);
		}
	}
	internal sealed class BlobHeapBuffer : HeapBuffer
	{
		private readonly Dictionary<ByteBuffer, uint> blobs = new Dictionary<ByteBuffer, uint>(new ByteBufferEqualityComparer());

		public override bool IsEmpty => length <= 1;

		public BlobHeapBuffer()
			: base(1)
		{
			WriteByte(0);
		}

		public uint GetBlobIndex(ByteBuffer blob)
		{
			if (blobs.TryGetValue(blob, out var value))
			{
				return value;
			}
			value = (uint)position;
			WriteBlob(blob);
			blobs.Add(blob, value);
			return value;
		}

		private void WriteBlob(ByteBuffer blob)
		{
			WriteCompressedUInt32((uint)blob.length);
			WriteBytes(blob);
		}
	}
	internal sealed class UserStringHeapBuffer : StringHeapBuffer
	{
		public override uint GetStringIndex(string @string)
		{
			if (strings.TryGetValue(@string, out var value))
			{
				return value;
			}
			value = (uint)position;
			WriteString(@string);
			strings.Add(@string, value);
			return value;
		}

		protected override void WriteString(string @string)
		{
			WriteCompressedUInt32((uint)(@string.Length * 2 + 1));
			byte b = 0;
			foreach (char c in @string)
			{
				WriteUInt16(c);
				if (b != 1 && (c < ' ' || c > '~') && (c > '~' || (c >= '\u0001' && c <= '\b') || (c >= '\u000e' && c <= '\u001f') || c == '\'' || c == '-'))
				{
					b = 1;
				}
			}
			WriteByte(b);
		}
	}
	internal sealed class PdbHeapBuffer : HeapBuffer
	{
		public override bool IsEmpty => false;

		public PdbHeapBuffer()
			: base(0)
		{
		}
	}
	internal enum CodedIndex
	{
		TypeDefOrRef,
		HasConstant,
		HasCustomAttribute,
		HasFieldMarshal,
		HasDeclSecurity,
		MemberRefParent,
		HasSemantics,
		MethodDefOrRef,
		MemberForwarded,
		Implementation,
		CustomAttributeType,
		ResolutionScope,
		TypeOrMethodDef,
		HasCustomDebugInformation
	}
	internal enum ElementType : byte
	{
		None = 0,
		Void = 1,
		Boolean = 2,
		Char = 3,
		I1 = 4,
		U1 = 5,
		I2 = 6,
		U2 = 7,
		I4 = 8,
		U4 = 9,
		I8 = 10,
		U8 = 11,
		R4 = 12,
		R8 = 13,
		String = 14,
		Ptr = 15,
		ByRef = 16,
		ValueType = 17,
		Class = 18,
		Var = 19,
		Array = 20,
		GenericInst = 21,
		TypedByRef = 22,
		I = 24,
		U = 25,
		FnPtr = 27,
		Object = 28,
		SzArray = 29,
		MVar = 30,
		CModReqD = 31,
		CModOpt = 32,
		Internal = 33,
		Modifier = 64,
		Sentinel = 65,
		Pinned = 69,
		Type = 80,
		Boxed = 81,
		Enum = 85
	}
	internal sealed class GuidHeap : Heap
	{
		public GuidHeap(byte[] data)
			: base(data)
		{
		}

		public Guid Read(uint index)
		{
			if (index == 0 || index - 1 + 16 > data.Length)
			{
				return default(Guid);
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(data, (int)((index - 1) * 16), array, 0, 16);
			return new Guid(array);
		}
	}
	internal abstract class Heap
	{
		public int IndexSize;

		internal readonly byte[] data;

		protected Heap(byte[] data)
		{
			this.data = data;
		}
	}
	internal sealed class PdbHeap : Heap
	{
		public byte[] Id;

		public uint EntryPoint;

		public long TypeSystemTables;

		public uint[] TypeSystemTableRows;

		public PdbHeap(byte[] data)
			: base(data)
		{
		}

		public bool HasTable(Table table)
		{
			return (TypeSystemTables & (1L << (int)table)) != 0;
		}
	}
	internal struct Row<T1, T2>
	{
		internal T1 Col1;

		internal T2 Col2;

		public Row(T1 col1, T2 col2)
		{
			Col1 = col1;
			Col2 = col2;
		}
	}
	internal struct Row<T1, T2, T3>
	{
		internal T1 Col1;

		internal T2 Col2;

		internal T3 Col3;

		public Row(T1 col1, T2 col2, T3 col3)
		{
			Col1 = col1;
			Col2 = col2;
			Col3 = col3;
		}
	}
	internal struct Row<T1, T2, T3, T4>
	{
		internal T1 Col1;

		internal T2 Col2;

		internal T3 Col3;

		internal T4 Col4;

		public Row(T1 col1, T2 col2, T3 col3, T4 col4)
		{
			Col1 = col1;
			Col2 = col2;
			Col3 = col3;
			Col4 = col4;
		}
	}
	internal struct Row<T1, T2, T3, T4, T5>
	{
		internal T1 Col1;

		internal T2 Col2;

		internal T3 Col3;

		internal T4 Col4;

		internal T5 Col5;

		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5)
		{
			Col1 = col1;
			Col2 = col2;
			Col3 = col3;
			Col4 = col4;
			Col5 = col5;
		}
	}
	internal struct Row<T1, T2, T3, T4, T5, T6>
	{
		internal T1 Col1;

		internal T2 Col2;

		internal T3 Col3;

		internal T4 Col4;

		internal T5 Col5;

		internal T6 Col6;

		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6)
		{
			Col1 = col1;
			Col2 = col2;
			Col3 = col3;
			Col4 = col4;
			Col5 = col5;
			Col6 = col6;
		}
	}
	internal struct Row<T1, T2, T3, T4, T5, T6, T7, T8, T9>
	{
		internal T1 Col1;

		internal T2 Col2;

		internal T3 Col3;

		internal T4 Col4;

		internal T5 Col5;

		internal T6 Col6;

		internal T7 Col7;

		internal T8 Col8;

		internal T9 Col9;

		public Row(T1 col1, T2 col2, T3 col3, T4 col4, T5 col5, T6 col6, T7 col7, T8 col8, T9 col9)
		{
			Col1 = col1;
			Col2 = col2;
			Col3 = col3;
			Col4 = col4;
			Col5 = col5;
			Col6 = col6;
			Col7 = col7;
			Col8 = col8;
			Col9 = col9;
		}
	}
	internal sealed class RowEqualityComparer : IEqualityComparer<Row<string, string>>, IEqualityComparer<Row<uint, uint>>, IEqualityComparer<Row<uint, uint, uint>>
	{
		public bool Equals(Row<string, string> x, Row<string, string> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2;
		}

		public int GetHashCode(Row<string, string> obj)
		{
			string col = obj.Col1;
			string col2 = obj.Col2;
			return (col?.GetHashCode() ?? 0) ^ (col2?.GetHashCode() ?? 0);
		}

		public bool Equals(Row<uint, uint> x, Row<uint, uint> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2;
		}

		public int GetHashCode(Row<uint, uint> obj)
		{
			return (int)(obj.Col1 ^ obj.Col2);
		}

		public bool Equals(Row<uint, uint, uint> x, Row<uint, uint, uint> y)
		{
			return x.Col1 == y.Col1 && x.Col2 == y.Col2 && x.Col3 == y.Col3;
		}

		public int GetHashCode(Row<uint, uint, uint> obj)
		{
			return (int)(obj.Col1 ^ obj.Col2 ^ obj.Col3);
		}
	}
	internal class StringHeap : Heap
	{
		private readonly Dictionary<uint, string> strings = new Dictionary<uint, string>();

		public StringHeap(byte[] data)
			: base(data)
		{
		}

		public string Read(uint index)
		{
			if (index == 0)
			{
				return string.Empty;
			}
			if (strings.TryGetValue(index, out var value))
			{
				return value;
			}
			if (index > data.Length - 1)
			{
				return string.Empty;
			}
			value = ReadStringAt(index);
			if (value.Length != 0)
			{
				strings.Add(index, value);
			}
			return value;
		}

		protected virtual string ReadStringAt(uint index)
		{
			int num = 0;
			for (int i = (int)index; data[i] != 0; i++)
			{
				num++;
			}
			return Encoding.UTF8.GetString(data, (int)index, num);
		}
	}
	internal enum Table : byte
	{
		Module = 0,
		TypeRef = 1,
		TypeDef = 2,
		FieldPtr = 3,
		Field = 4,
		MethodPtr = 5,
		Method = 6,
		ParamPtr = 7,
		Param = 8,
		InterfaceImpl = 9,
		MemberRef = 10,
		Constant = 11,
		CustomAttribute = 12,
		FieldMarshal = 13,
		DeclSecurity = 14,
		ClassLayout = 15,
		FieldLayout = 16,
		StandAloneSig = 17,
		EventMap = 18,
		EventPtr = 19,
		Event = 20,
		PropertyMap = 21,
		PropertyPtr = 22,
		Property = 23,
		MethodSemantics = 24,
		MethodImpl = 25,
		ModuleRef = 26,
		TypeSpec = 27,
		ImplMap = 28,
		FieldRVA = 29,
		EncLog = 30,
		EncMap = 31,
		Assembly = 32,
		AssemblyProcessor = 33,
		AssemblyOS = 34,
		AssemblyRef = 35,
		AssemblyRefProcessor = 36,
		AssemblyRefOS = 37,
		File = 38,
		ExportedType = 39,
		ManifestResource = 40,
		NestedClass = 41,
		GenericParam = 42,
		MethodSpec = 43,
		GenericParamConstraint = 44,
		Document = 48,
		MethodDebugInformation = 49,
		LocalScope = 50,
		LocalVariable = 51,
		LocalConstant = 52,
		ImportScope = 53,
		StateMachineMethod = 54,
		CustomDebugInformation = 55
	}
	internal struct TableInformation
	{
		public uint Offset;

		public uint Length;

		public uint RowSize;

		public bool IsLarge => Length > 65535;
	}
	internal sealed class TableHeap : Heap
	{
		public long Valid;

		public long Sorted;

		public readonly TableInformation[] Tables = new TableInformation[58];

		public TableInformation this[Table table] => Tables[(uint)table];

		public TableHeap(byte[] data)
			: base(data)
		{
		}

		public bool HasTable(Table table)
		{
			return (Valid & (1L << (int)table)) != 0;
		}
	}
	internal sealed class UserStringHeap : StringHeap
	{
		public UserStringHeap(byte[] data)
			: base(data)
		{
		}

		protected override string ReadStringAt(uint index)
		{
			int position = (int)index;
			uint num = (uint)(data.ReadCompressedUInt32(ref position) & -2);
			if (num < 1)
			{
				return string.Empty;
			}
			char[] array = new char[num / 2];
			int i = position;
			int num2 = 0;
			for (; i < position + num; i += 2)
			{
				array[num2++] = (char)(data[i] | (data[i + 1] << 8));
			}
			return new string(array);
		}
	}
}
namespace Mono.Cecil.Cil
{
	public enum Code
	{
		Nop,
		Break,
		Ldarg_0,
		Ldarg_1,
		Ldarg_2,
		Ldarg_3,
		Ldloc_0,
		Ldloc_1,
		Ldloc_2,
		Ldloc_3,
		Stloc_0,
		Stloc_1,
		Stloc_2,
		Stloc_3,
		Ldarg_S,
		Ldarga_S,
		Starg_S,
		Ldloc_S,
		Ldloca_S,
		Stloc_S,
		Ldnull,
		Ldc_I4_M1,
		Ldc_I4_0,
		Ldc_I4_1,
		Ldc_I4_2,
		Ldc_I4_3,
		Ldc_I4_4,
		Ldc_I4_5,
		Ldc_I4_6,
		Ldc_I4_7,
		Ldc_I4_8,
		Ldc_I4_S,
		Ldc_I4,
		Ldc_I8,
		Ldc_R4,
		Ldc_R8,
		Dup,
		Pop,
		Jmp,
		Call,
		Calli,
		Ret,
		Br_S,
		Brfalse_S,
		Brtrue_S,
		Beq_S,
		Bge_S,
		Bgt_S,
		Ble_S,
		Blt_S,
		Bne_Un_S,
		Bge_Un_S,
		Bgt_Un_S,
		Ble_Un_S,
		Blt_Un_S,
		Br,
		Brfalse,
		Brtrue,
		Beq,
		Bge,
		Bgt,
		Ble,
		Blt,
		Bne_Un,
		Bge_Un,
		Bgt_Un,
		Ble_Un,
		Blt_Un,
		Switch,
		Ldind_I1,
		Ldind_U1,
		Ldind_I2,
		Ldind_U2,
		Ldind_I4,
		Ldind_U4,
		Ldind_I8,
		Ldind_I,
		Ldind_R4,
		Ldind_R8,
		Ldind_Ref,
		Stind_Ref,
		Stind_I1,
		Stind_I2,
		Stind_I4,
		Stind_I8,
		Stind_R4,
		Stind_R8,
		Add,
		Sub,
		Mul,
		Div,
		Div_Un,
		Rem,
		Rem_Un,
		And,
		Or,
		Xor,
		Shl,
		Shr,
		Shr_Un,
		Neg,
		Not,
		Conv_I1,
		Conv_I2,
		Conv_I4,
		Conv_I8,
		Conv_R4,
		Conv_R8,
		Conv_U4,
		Conv_U8,
		Callvirt,
		Cpobj,
		Ldobj,
		Ldstr,
		Newobj,
		Castclass,
		Isinst,
		Conv_R_Un,
		Unbox,
		Throw,
		Ldfld,
		Ldflda,
		Stfld,
		Ldsfld,
		Ldsflda,
		Stsfld,
		Stobj,
		Conv_Ovf_I1_Un,
		Conv_Ovf_I2_Un,
		Conv_Ovf_I4_Un,
		Conv_Ovf_I8_Un,
		Conv_Ovf_U1_Un,
		Conv_Ovf_U2_Un,
		Conv_Ovf_U4_Un,
		Conv_Ovf_U8_Un,
		Conv_Ovf_I_Un,
		Conv_Ovf_U_Un,
		Box,
		Newarr,
		Ldlen,
		Ldelema,
		Ldelem_I1,
		Ldelem_U1,
		Ldelem_I2,
		Ldelem_U2,
		Ldelem_I4,
		Ldelem_U4,
		Ldelem_I8,
		Ldelem_I,
		Ldelem_R4,
		Ldelem_R8,
		Ldelem_Ref,
		Stelem_I,
		Stelem_I1,
		Stelem_I2,
		Stelem_I4,
		Stelem_I8,
		Stelem_R4,
		Stelem_R8,
		Stelem_Ref,
		Ldelem_Any,
		Stelem_Any,
		Unbox_Any,
		Conv_Ovf_I1,
		Conv_Ovf_U1,
		Conv_Ovf_I2,
		Conv_Ovf_U2,
		Conv_Ovf_I4,
		Conv_Ovf_U4,
		Conv_Ovf_I8,
		Conv_Ovf_U8,
		Refanyval,
		Ckfinite,
		Mkrefany,
		Ldtoken,
		Conv_U2,
		Conv_U1,
		Conv_I,
		Conv_Ovf_I,
		Conv_Ovf_U,
		Add_Ovf,
		Add_Ovf_Un,
		Mul_Ovf,
		Mul_Ovf_Un,
		Sub_Ovf,
		Sub_Ovf_Un,
		Endfinally,
		Leave,
		Leave_S,
		Stind_I,
		Conv_U,
		Arglist,
		Ceq,
		Cgt,
		Cgt_Un,
		Clt,
		Clt_Un,
		Ldftn,
		Ldvirtftn,
		Ldarg,
		Ldarga,
		Starg,
		Ldloc,
		Ldloca,
		Stloc,
		Localloc,
		Endfilter,
		Unaligned,
		Volatile,
		Tail,
		Initobj,
		Constrained,
		Cpblk,
		Initblk,
		No,
		Rethrow,
		Sizeof,
		Refanytype,
		Readonly
	}
	internal sealed class CodeReader : BinaryStreamReader
	{
		internal readonly MetadataReader reader;

		private int start;

		private MethodDefinition method;

		private MethodBody body;

		private int Offset => base.Position - start;

		public CodeReader(MetadataReader reader)
			: base(reader.image.Stream.value)
		{
			this.reader = reader;
		}

		public int MoveTo(MethodDefinition method)
		{
			this.method = method;
			reader.context = method;
			int position = base.Position;
			base.Position = (int)reader.image.ResolveVirtualAddress((uint)method.RVA);
			return position;
		}

		public void MoveBackTo(int position)
		{
			reader.context = null;
			base.Position = position;
		}

		public MethodBody ReadMethodBody(MethodDefinition method)
		{
			int position = MoveTo(method);
			body = new MethodBody(method);
			ReadMethodBody();
			MoveBackTo(position);
			return body;
		}

		public int ReadCodeSize(MethodDefinition method)
		{
			int position = MoveTo(method);
			int result = ReadCodeSize();
			MoveBackTo(position);
			return result;
		}

		private int ReadCodeSize()
		{
			byte b = ReadByte();
			switch (b & 3)
			{
			case 2:
				return b >> 2;
			case 3:
				Advance(3);
				return (int)ReadUInt32();
			default:
				throw new InvalidOperationException();
			}
		}

		private void ReadMethodBody()
		{
			byte b = ReadByte();
			switch (b & 3)
			{
			case 2:
				body.code_size = b >> 2;
				body.MaxStackSize = 8;
				ReadCode();
				break;
			case 3:
				Advance(-1);
				ReadFatMethod();
				break;
			default:
				throw new InvalidOperationException();
			}
			ISymbolReader symbol_reader = reader.module.symbol_reader;
			if (symbol_reader != null && method.debug_info == null)
			{
				method.debug_info = symbol_reader.Read(method);
			}
			if (method.debug_info != null)
			{
				ReadDebugInfo();
			}
		}

		private void ReadFatMethod()
		{
			ushort num = ReadUInt16();
			body.max_stack_size = ReadUInt16();
			body.code_size = (int)ReadUInt32();
			body.local_var_token = new MetadataToken(ReadUInt32());
			body.init_locals = (num & 0x10) != 0;
			if (body.local_var_token.RID != 0)
			{
				body.variables = ReadVariables(body.local_var_token);
			}
			ReadCode();
			if ((num & 8) != 0)
			{
				ReadSection();
			}
		}

		public VariableDefinitionCollection ReadVariables(MetadataToken local_var_token)
		{
			int position = reader.position;
			VariableDefinitionCollection result = reader.ReadVariables(local_var_token, method);
			reader.position = position;
			return result;
		}

		private void ReadCode()
		{
			start = base.Position;
			int num = body.code_size;
			if (num < 0 || base.Length <= (uint)(num + base.Position))
			{
				num = 0;
			}
			int num2 = start + num;
			Collection<Instruction> collection = (body.instructions = new InstructionCollection(method, (num + 1) / 2));
			while (base.Position < num2)
			{
				int offset = base.Position - start;
				OpCode opCode = ReadOpCode();
				Instruction instruction = new Instruction(offset, opCode);
				if (opCode.OperandType != OperandType.InlineNone)
				{
					instruction.operand = ReadOperand(instruction);
				}
				collection.Add(instruction);
			}
			ResolveBranches(collection);
		}

		private OpCode ReadOpCode()
		{
			byte b = ReadByte();
			return (b != 254) ? OpCodes.OneByteOpCode[b] : OpCodes.TwoBytesOpCode[ReadByte()];
		}

		private object ReadOperand(Instruction instruction)
		{
			switch (instruction.opcode.OperandType)
			{
			case OperandType.InlineSwitch:
			{
				int num = ReadInt32();
				int num2 = Offset + 4 * num;
				int[] array = new int[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = num2 + ReadInt32();
				}
				return array;
			}
			case OperandType.ShortInlineBrTarget:
				return ReadSByte() + Offset;
			case OperandType.InlineBrTarget:
				return ReadInt32() + Offset;
			case OperandType.ShortInlineI:
				if (instruction.opcode == OpCodes.Ldc_I4_S)
				{
					return ReadSByte();
				}
				return ReadByte();
			case OperandType.InlineI:
				return ReadInt32();
			case OperandType.ShortInlineR:
				return ReadSingle();
			case OperandType.InlineR:
				return ReadDouble();
			case OperandType.InlineI8:
				return ReadInt64();
			case OperandType.ShortInlineVar:
				return GetVariable(ReadByte());
			case OperandType.InlineVar:
				return GetVariable(ReadUInt16());
			case OperandType.ShortInlineArg:
				return GetParameter(ReadByte());
			case OperandType.InlineArg:
				return GetParameter(ReadUInt16());
			case OperandType.InlineSig:
				return GetCallSite(ReadToken());
			case OperandType.InlineString:
				return GetString(ReadToken());
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
				return reader.LookupToken(ReadToken());
			default:
				throw new NotSupportedException();
			}
		}

		public string GetString(MetadataToken token)
		{
			return reader.image.UserStringHeap.Read(token.RID);
		}

		public ParameterDefinition GetParameter(int index)
		{
			return body.GetParameter(index);
		}

		public VariableDefinition GetVariable(int index)
		{
			return body.GetVariable(index);
		}

		public CallSite GetCallSite(MetadataToken token)
		{
			return reader.ReadCallSite(token);
		}

		private void ResolveBranches(Collection<Instruction> instructions)
		{
			Instruction[] items = instructions.items;
			int size = instructions.size;
			for (int i = 0; i < size; i++)
			{
				Instruction instruction = items[i];
				switch (instruction.opcode.OperandType)
				{
				case OperandType.InlineBrTarget:
				case OperandType.ShortInlineBrTarget:
					instruction.operand = GetInstruction((int)instruction.operand);
					break;
				case OperandType.InlineSwitch:
				{
					int[] array = (int[])instruction.operand;
					Instruction[] array2 = new Instruction[array.Length];
					for (int j = 0; j < array.Length; j++)
					{
						array2[j] = GetInstruction(array[j]);
					}
					instruction.operand = array2;
					break;
				}
				}
			}
		}

		private Instruction GetInstruction(int offset)
		{
			return GetInstruction(body.Instructions, offset);
		}

		private static Instruction GetInstruction(Collection<Instruction> instructions, int offset)
		{
			int size = instructions.size;
			Instruction[] items = instructions.items;
			if (offset < 0 || offset > items[size - 1].offset)
			{
				return null;
			}
			int num = 0;
			int num2 = size - 1;
			while (num <= num2)
			{
				int num3 = num + (num2 - num) / 2;
				Instruction instruction = items[num3];
				int offset2 = instruction.offset;
				if (offset == offset2)
				{
					return instruction;
				}
				if (offset < offset2)
				{
					num2 = num3 - 1;
				}
				else
				{
					num = num3 + 1;
				}
			}
			return null;
		}

		private void ReadSection()
		{
			Align(4);
			byte b = ReadByte();
			if ((b & 0x40) == 0)
			{
				ReadSmallSection();
			}
			else
			{
				ReadFatSection();
			}
			if ((b & 0x80) != 0)
			{
				ReadSection();
			}
		}

		private void ReadSmallSection()
		{
			int count = ReadByte() / 12;
			Advance(2);
			ReadExceptionHandlers(count, () => ReadUInt16(), () => ReadByte());
		}

		private void ReadFatSection()
		{
			Advance(-1);
			int count = (ReadInt32() >> 8) / 24;
			ReadExceptionHandlers(count, ReadInt32, ReadInt32);
		}

		private void ReadExceptionHandlers(int count, Func<int> read_entry, Func<int> read_length)
		{
			for (int i = 0; i < count; i++)
			{
				ExceptionHandler exceptionHandler = new ExceptionHandler((ExceptionHandlerType)(read_entry() & 7));
				exceptionHandler.TryStart = GetInstruction(read_entry());
				exceptionHandler.TryEnd = GetInstruction(exceptionHandler.TryStart.Offset + read_length());
				exceptionHandler.HandlerStart = GetInstruction(read_entry());
				exceptionHandler.HandlerEnd = GetInstruction(exceptionHandler.HandlerStart.Offset + read_length());
				ReadExceptionHandlerSpecific(exceptionHandler);
				body.ExceptionHandlers.Add(exceptionHandler);
			}
		}

		private void ReadExceptionHandlerSpecific(ExceptionHandler handler)
		{
			switch (handler.HandlerType)
			{
			case ExceptionHandlerType.Catch:
				handler.CatchType = (TypeReference)reader.LookupToken(ReadToken());
				break;
			case ExceptionHandlerType.Filter:
				handler.FilterStart = GetInstruction(ReadInt32());
				break;
			default:
				Advance(4);
				break;
			}
		}

		public MetadataToken ReadToken()
		{
			return new MetadataToken(ReadUInt32());
		}

		private void ReadDebugInfo()
		{
			if (method.debug_info.sequence_points != null)
			{
				ReadSequencePoints();
			}
			if (method.debug_info.scope != null)
			{
				ReadScope(method.debug_info.scope);
			}
			if (method.custom_infos != null)
			{
				ReadCustomDebugInformations(method);
			}
		}

		private void ReadCustomDebugInformations(MethodDefinition method)
		{
			Collection<CustomDebugInformation> custom_infos = method.custom_infos;
			for (int i = 0; i < custom_infos.Count; i++)
			{
				if (custom_infos[i] is StateMachineScopeDebugInformation state_machine_scope)
				{
					ReadStateMachineScope(state_machine_scope);
				}
				if (custom_infos[i] is AsyncMethodBodyDebugInformation async_method)
				{
					ReadAsyncMethodBody(async_method);
				}
			}
		}

		private void ReadAsyncMethodBody(AsyncMethodBodyDebugInformation async_method)
		{
			if (async_method.catch_handler.Offset > -1)
			{
				async_method.catch_handler = new InstructionOffset(GetInstruction(async_method.catch_handler.Offset));
			}
			if (!async_method.yields.IsNullOrEmpty())
			{
				for (int i = 0; i < async_method.yields.Count; i++)
				{
					async_method.yields[i] = new InstructionOffset(GetInstruction(async_method.yields[i].Offset));
				}
			}
			if (!async_method.resumes.IsNullOrEmpty())
			{
				for (int j = 0; j < async_method.resumes.Count; j++)
				{
					async_method.resumes[j] = new InstructionOffset(GetInstruction(async_method.resumes[j].Offset));
				}
			}
		}

		private void ReadStateMachineScope(StateMachineScopeDebugInformation state_machine_scope)
		{
			if (state_machine_scope.scopes.IsNullOrEmpty())
			{
				return;
			}
			foreach (StateMachineScope scope in state_machine_scope.scopes)
			{
				scope.start = new InstructionOffset(GetInstruction(scope.start.Offset));
				Instruction instruction = GetInstruction(scope.end.Offset);
				scope.end = ((instruction == null) ? default(InstructionOffset) : new InstructionOffset(instruction));
			}
		}

		private void ReadSequencePoints()
		{
			MethodDebugInformation debug_info = method.debug_info;
			for (int i = 0; i < debug_info.sequence_points.Count; i++)
			{
				SequencePoint sequencePoint = debug_info.sequence_points[i];
				Instruction instruction = GetInstruction(sequencePoint.Offset);
				if (instruction != null)
				{
					sequencePoint.offset = new InstructionOffset(instruction);
				}
			}
		}

		private void ReadScopes(Collection<ScopeDebugInformation> scopes)
		{
			for (int i = 0; i < scopes.Count; i++)
			{
				ReadScope(scopes[i]);
			}
		}

		private void ReadScope(ScopeDebugInformation scope)
		{
			Instruction instruction = GetInstruction(scope.Start.Offset);
			if (instruction != null)
			{
				scope.Start = new InstructionOffset(instruction);
			}
			Instruction instruction2 = GetInstruction(scope.End.Offset);
			scope.End = ((instruction2 != null) ? new InstructionOffset(instruction2) : default(InstructionOffset));
			if (!scope.variables.IsNullOrEmpty())
			{
				for (int i = 0; i < scope.variables.Count; i++)
				{
					VariableDebugInformation variableDebugInformation = scope.variables[i];
					VariableDefinition variable = GetVariable(variableDebugInformation.Index);
					if (variable != null)
					{
						variableDebugInformation.index = new VariableIndex(variable);
					}
				}
			}
			if (!scope.scopes.IsNullOrEmpty())
			{
				ReadScopes(scope.scopes);
			}
		}

		public ByteBuffer PatchRawMethodBody(MethodDefinition method, CodeWriter writer, out int code_size, out MetadataToken local_var_token)
		{
			int position = MoveTo(method);
			ByteBuffer byteBuffer = new ByteBuffer();
			byte b = ReadByte();
			switch (b & 3)
			{
			case 2:
				byteBuffer.WriteByte(b);
				local_var_token = MetadataToken.Zero;
				code_size = b >> 2;
				PatchRawCode(byteBuffer, code_size, writer);
				break;
			case 3:
				Advance(-1);
				PatchRawFatMethod(byteBuffer, writer, out code_size, out local_var_token);
				break;
			default:
				throw new NotSupportedException();
			}
			MoveBackTo(position);
			return byteBuffer;
		}

		private void PatchRawFatMethod(ByteBuffer buffer, CodeWriter writer, out int code_size, out MetadataToken local_var_token)
		{
			ushort num = ReadUInt16();
			buffer.WriteUInt16(num);
			buffer.WriteUInt16(ReadUInt16());
			code_size = ReadInt32();
			buffer.WriteInt32(code_size);
			local_var_token = ReadToken();
			if (local_var_token.RID != 0)
			{
				VariableDefinitionCollection variableDefinitionCollection = ReadVariables(local_var_token);
				buffer.WriteUInt32((variableDefinitionCollection != null) ? writer.GetStandAloneSignature(variableDefinitionCollection).ToUInt32() : 0u);
			}
			else
			{
				buffer.WriteUInt32(0u);
			}
			PatchRawCode(buffer, code_size, writer);
			if ((num & 8) != 0)
			{
				PatchRawSection(buffer, writer.metadata);
			}
		}

		private void PatchRawCode(ByteBuffer buffer, int code_size, CodeWriter writer)
		{
			MetadataBuilder metadata = writer.metadata;
			buffer.WriteBytes(ReadBytes(code_size));
			int position = buffer.position;
			buffer.position -= code_size;
			while (buffer.position < position)
			{
				byte b = buffer.ReadByte();
				OpCode opCode;
				if (b != 254)
				{
					opCode = OpCodes.OneByteOpCode[b];
				}
				else
				{
					byte b2 = buffer.ReadByte();
					opCode = OpCodes.TwoBytesOpCode[b2];
				}
				switch (opCode.OperandType)
				{
				case OperandType.ShortInlineBrTarget:
				case OperandType.ShortInlineI:
				case OperandType.ShortInlineVar:
				case OperandType.ShortInlineArg:
					buffer.position++;
					break;
				case OperandType.InlineVar:
				case OperandType.InlineArg:
					buffer.position += 2;
					break;
				case OperandType.InlineBrTarget:
				case OperandType.InlineI:
				case OperandType.ShortInlineR:
					buffer.position += 4;
					break;
				case OperandType.InlineI8:
				case OperandType.InlineR:
					buffer.position += 8;
					break;
				case OperandType.InlineSwitch:
				{
					int num = buffer.ReadInt32();
					buffer.position += num * 4;
					break;
				}
				case OperandType.InlineString:
				{
					string text = GetString(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(new MetadataToken(TokenType.String, metadata.user_string_heap.GetStringIndex(text)).ToUInt32());
					break;
				}
				case OperandType.InlineSig:
				{
					CallSite callSite = GetCallSite(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(writer.GetStandAloneSignature(callSite).ToUInt32());
					break;
				}
				case OperandType.InlineField:
				case OperandType.InlineMethod:
				case OperandType.InlineTok:
				case OperandType.InlineType:
				{
					IMetadataTokenProvider provider = reader.LookupToken(new MetadataToken(buffer.ReadUInt32()));
					buffer.position -= 4;
					buffer.WriteUInt32(metadata.LookupToken(provider).ToUInt32());
					break;
				}
				}
			}
		}

		private void PatchRawSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			int position = base.Position;
			Align(4);
			buffer.WriteBytes(base.Position - position);
			byte b = ReadByte();
			if ((b & 0x40) == 0)
			{
				buffer.WriteByte(b);
				PatchRawSmallSection(buffer, metadata);
			}
			else
			{
				PatchRawFatSection(buffer, metadata);
			}
			if ((b & 0x80) != 0)
			{
				PatchRawSection(buffer, metadata);
			}
		}

		private void PatchRawSmallSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			byte b = ReadByte();
			buffer.WriteByte(b);
			Advance(2);
			buffer.WriteUInt16(0);
			int count = b / 12;
			PatchRawExceptionHandlers(buffer, metadata, count, fat_entry: false);
		}

		private void PatchRawFatSection(ByteBuffer buffer, MetadataBuilder metadata)
		{
			Advance(-1);
			int num = ReadInt32();
			buffer.WriteInt32(num);
			int count = (num >> 8) / 24;
			PatchRawExceptionHandlers(buffer, metadata, count, fat_entry: true);
		}

		private void PatchRawExceptionHandlers(ByteBuffer buffer, MetadataBuilder metadata, int count, bool fat_entry)
		{
			for (int i = 0; i < count; i++)
			{
				ExceptionHandlerType exceptionHandlerType;
				if (fat_entry)
				{
					uint num = ReadUInt32();
					exceptionHandlerType = (ExceptionHandlerType)(num & 7);
					buffer.WriteUInt32(num);
				}
				else
				{
					ushort num2 = ReadUInt16();
					exceptionHandlerType = (ExceptionHandlerType)(num2 & 7);
					buffer.WriteUInt16(num2);
				}
				buffer.WriteBytes(ReadBytes(fat_entry ? 16 : 6));
				if (exceptionHandlerType == ExceptionHandlerType.Catch)
				{
					IMetadataTokenProvider provider = reader.LookupToken(ReadToken());
					buffer.WriteUInt32(metadata.LookupToken(provider).ToUInt32());
				}
				else
				{
					buffer.WriteUInt32(ReadUInt32());
				}
			}
		}
	}
	internal sealed class CodeWriter : ByteBuffer
	{
		private readonly uint code_base;

		internal readonly MetadataBuilder metadata;

		private readonly Dictionary<uint, MetadataToken> standalone_signatures;

		private readonly Dictionary<ByteBuffer, uint> tiny_method_bodies;

		private MethodBody body;

		public CodeWriter(MetadataBuilder metadata)
			: base(0)
		{
			code_base = metadata.text_map.GetRVA(TextSegment.Code);
			this.metadata = metadata;
			standalone_signatures = new Dictionary<uint, MetadataToken>();
			tiny_method_bodies = new Dictionary<ByteBuffer, uint>(new ByteBufferEqualityComparer());
		}

		public uint WriteMethodBody(MethodDefinition method)
		{
			uint result;
			if (IsUnresolved(method))
			{
				if (method.rva == 0)
				{
					return 0u;
				}
				result = WriteUnresolvedMethodBody(method);
			}
			else
			{
				if (IsEmptyMethodBody(method.Body))
				{
					return 0u;
				}
				result = WriteResolvedMethodBody(method);
			}
			return result;
		}

		private static bool IsEmptyMethodBody(MethodBody body)
		{
			return body.instructions.IsNullOrEmpty() && body.variables.IsNullOrEmpty();
		}

		private static bool IsUnresolved(MethodDefinition method)
		{
			return method.HasBody && method.HasImage && method.body == null;
		}

		private uint WriteUnresolvedMethodBody(MethodDefinition method)
		{
			CodeReader code = metadata.module.reader.code;
			int code_size;
			MetadataToken local_var_token;
			ByteBuffer byteBuffer = code.PatchRawMethodBody(method, this, out code_size, out local_var_token);
			bool flag = (byteBuffer.buffer[0] & 3) == 3;
			if (flag)
			{
				Align(4);
			}
			uint rva = BeginMethod();
			if (flag || !GetOrMapTinyMethodBody(byteBuffer, ref rva))
			{
				WriteBytes(byteBuffer);
			}
			if (method.debug_info == null)
			{
				return rva;
			}
			ISymbolWriter symbol_writer = metadata.symbol_writer;
			if (symbol_writer != null)
			{
				method.debug_info.code_size = code_size;
				method.debug_info.local_var_token = local_var_token;
				symbol_writer.Write(method.debug_info);
			}
			return rva;
		}

		private uint WriteResolvedMethodBody(MethodDefinition method)
		{
			body = method.Body;
			ComputeHeader();
			uint rva;
			if (RequiresFatHeader())
			{
				Align(4);
				rva = BeginMethod();
				WriteFatHeader();
				WriteInstructions();
				if (body.HasExceptionHandlers)
				{
					WriteExceptionHandlers();
				}
			}
			else
			{
				rva = BeginMethod();
				WriteByte((byte)(2 | (body.CodeSize << 2)));
				WriteInstructions();
				int num = (int)(rva - code_base);
				int num2 = position - num;
				byte[] destinationArray = new byte[num2];
				Array.Copy(buffer, num, destinationArray, 0, num2);
				if (GetOrMapTinyMethodBody(new ByteBuffer(destinationArray), ref rva))
				{
					position = num;
				}
			}
			ISymbolWriter symbol_writer = metadata.symbol_writer;
			if (symbol_writer != null && method.debug_info != null)
			{
				method.debug_info.code_size = body.CodeSize;
				method.debug_info.local_var_token = body.local_var_token;
				symbol_writer.Write(method.debug_info);
			}
			return rva;
		}

		private bool GetOrMapTinyMethodBody(ByteBuffer body, ref uint rva)
		{
			if (tiny_method_bodies.TryGetValue(body, out var value))
			{
				rva = value;
				return true;
			}
			tiny_method_bodies.Add(body, rva);
			return false;
		}

		private void WriteFatHeader()
		{
			MethodBody methodBody = body;
			byte b = 3;
			if (methodBody.InitLocals)
			{
				b |= 0x10;
			}
			if (methodBody.HasExceptionHandlers)
			{
				b |= 8;
			}
			WriteByte(b);
			WriteByte(48);
			WriteInt16((short)methodBody.max_stack_size);
			WriteInt32(methodBody.code_size);
			methodBody.local_var_token = (methodBody.HasVariables ? GetStandAloneSignature(methodBody.Variables) : MetadataToken.Zero);
			WriteMetadataToken(methodBody.local_var_token);
		}

		private void WriteInstructions()
		{
			Collection<Instruction> instructions = body.Instructions;
			Instruction[] items = instructions.items;
			int size = instructions.size;
			for (int i = 0; i < size; i++)
			{
				Instruction instruction = items[i];
				WriteOpCode(instruction.opcode);
				WriteOperand(instruction);
			}
		}

		private void WriteOpCode(OpCode opcode)
		{
			if (opcode.Size == 1)
			{
				WriteByte(opcode.Op2);
				return;
			}
			WriteByte(opcode.Op1);
			WriteByte(opcode.Op2);
		}

		private void WriteOperand(Instruction instruction)
		{
			OpCode opcode = instruction.opcode;
			OperandType operandType = opcode.OperandType;
			if (operandType == OperandType.InlineNone)
			{
				return;
			}
			object operand = instruction.operand;
			if (operand == null && operandType != OperandType.InlineBrTarget && operandType != OperandType.ShortInlineBrTarget)
			{
				throw new ArgumentException();
			}
			switch (operandType)
			{
			case OperandType.InlineSwitch:
			{
				Instruction[] array = (Instruction[])operand;
				WriteInt32(array.Length);
				int num2 = instruction.Offset + opcode.Size + 4 * (array.Length + 1);
				for (int i = 0; i < array.Length; i++)
				{
					WriteInt32(GetTargetOffset(array[i]) - num2);
				}
				break;
			}
			case OperandType.ShortInlineBrTarget:
			{
				Instruction instruction2 = (Instruction)operand;
				int num = ((instruction2 != null) ? GetTargetOffset(instruction2) : body.code_size);
				WriteSByte((sbyte)(num - (instruction.Offset + opcode.Size + 1)));
				break;
			}
			case OperandType.InlineBrTarget:
			{
				Instruction instruction3 = (Instruction)operand;
				int num3 = ((instruction3 != null) ? GetTargetOffset(instruction3) : body.code_size);
				WriteInt32(num3 - (instruction.Offset + opcode.Size + 4));
				break;
			}
			case OperandType.ShortInlineVar:
				WriteByte((byte)GetVariableIndex((VariableDefinition)operand));
				break;
			case OperandType.ShortInlineArg:
				WriteByte((byte)GetParameterIndex((ParameterDefinition)operand));
				break;
			case OperandType.InlineVar:
				WriteInt16((short)GetVariableIndex((VariableDefinition)operand));
				break;
			case OperandType.InlineArg:
				WriteInt16((short)GetParameterIndex((ParameterDefinition)operand));
				break;
			case OperandType.InlineSig:
				WriteMetadataToken(GetStandAloneSignature((CallSite)operand));
				break;
			case OperandType.ShortInlineI:
				if (opcode == OpCodes.Ldc_I4_S)
				{
					WriteSByte((sbyte)operand);
				}
				else
				{
					WriteByte((byte)operand);
				}
				break;
			case OperandType.InlineI:
				WriteInt32((int)operand);
				break;
			case OperandType.InlineI8:
				WriteInt64((long)operand);
				break;
			case OperandType.ShortInlineR:
				WriteSingle((float)operand);
				break;
			case OperandType.InlineR:
				WriteDouble((double)operand);
				break;
			case OperandType.InlineString:
				WriteMetadataToken(new MetadataToken(TokenType.String, GetUserStringIndex((string)operand)));
				break;
			case OperandType.InlineField:
			case OperandType.InlineMethod:
			case OperandType.InlineTok:
			case OperandType.InlineType:
				WriteMetadataToken(metadata.LookupToken((IMetadataTokenProvider)operand));
				break;
			default:
				throw new ArgumentException();
			}
		}

		private int GetTargetOffset(Instruction instruction)
		{
			if (instruction == null)
			{
				Instruction instruction2 = body.instructions[body.instructions.size - 1];
				return instruction2.offset + instruction2.GetSize();
			}
			return instruction.offset;
		}

		private uint GetUserStringIndex(string @string)
		{
			if (@string == null)
			{
				return 0u;
			}
			return metadata.user_string_heap.GetStringIndex(@string);
		}

		private static int GetVariableIndex(VariableDefinition variable)
		{
			return variable.Index;
		}

		private int GetParameterIndex(ParameterDefinition parameter)
		{
			if (body.method.HasThis)
			{
				if (parameter == body.this_parameter)
				{
					return 0;
				}
				return parameter.Index + 1;
			}
			return parameter.Index;
		}

		private bool RequiresFatHeader()
		{
			MethodBody methodBody = body;
			return methodBody.CodeSize >= 64 || methodBody.InitLocals || methodBody.HasVariables || methodBody.HasExceptionHandlers || methodBody.MaxStackSize > 8;
		}

		private void ComputeHeader()
		{
			int num = 0;
			Collection<Instruction> instructions = body.instructions;
			Instruction[] items = instructions.items;
			int size = instructions.size;
			int stack_size = 0;
			int max_stack = 0;
			Dictionary<Instruction, int> stack_sizes = null;
			if (body.HasExceptionHandlers)
			{
				ComputeExceptionHandlerStackSize(ref stack_sizes);
			}
			for (int i = 0; i < size; i++)
			{
				Instruction instruction = items[i];
				instruction.offset = num;
				num += instruction.GetSize();
				ComputeStackSize(instruction, ref stack_sizes, ref stack_size, ref max_stack);
			}
			body.code_size = num;
			body.max_stack_size = max_stack;
		}

		private void ComputeExceptionHandlerStackSize(ref Dictionary<Instruction, int> stack_sizes)
		{
			Collection<ExceptionHandler> exceptionHandlers = body.ExceptionHandlers;
			for (int i = 0; i < exceptionHandlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = exceptionHandlers[i];
				switch (exceptionHandler.HandlerType)
				{
				case ExceptionHandlerType.Catch:
					AddExceptionStackSize(exceptionHandler.HandlerStart, ref stack_sizes);
					break;
				case ExceptionHandlerType.Filter:
					AddExceptionStackSize(exceptionHandler.FilterStart, ref stack_sizes);
					AddExceptionStackSize(exceptionHandler.HandlerStart, ref stack_sizes);
					break;
				}
			}
		}

		private static void AddExceptionStackSize(Instruction handler_start, ref Dictionary<Instruction, int> stack_sizes)
		{
			if (handler_start != null)
			{
				if (stack_sizes == null)
				{
					stack_sizes = new Dictionary<Instruction, int>();
				}
				stack_sizes[handler_start] = 1;
			}
		}

		private static void ComputeStackSize(Instruction instruction, ref Dictionary<Instruction, int> stack_sizes, ref int stack_size, ref int max_stack)
		{
			if (stack_sizes != null && stack_sizes.TryGetValue(instruction, out var value))
			{
				stack_size = value;
			}
			max_stack = System.Math.Max(max_stack, stack_size);
			ComputeStackDelta(instruction, ref stack_size);
			max_stack = System.Math.Max(max_stack, stack_size);
			CopyBranchStackSize(instruction, ref stack_sizes, stack_size);
			ComputeStackSize(instruction, ref stack_size);
		}

		private static void CopyBranchStackSize(Instruction instruction, ref Dictionary<Instruction, int> stack_sizes, int stack_size)
		{
			if (stack_size == 0)
			{
				return;
			}
			switch (instruction.opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
			case OperandType.ShortInlineBrTarget:
				CopyBranchStackSize(ref stack_sizes, (Instruction)instruction.operand, stack_size);
				break;
			case OperandType.InlineSwitch:
			{
				Instruction[] array = (Instruction[])instruction.operand;
				for (int i = 0; i < array.Length; i++)
				{
					CopyBranchStackSize(ref stack_sizes, array[i], stack_size);
				}
				break;
			}
			}
		}

		private static void CopyBranchStackSize(ref Dictionary<Instruction, int> stack_sizes, Instruction target, int stack_size)
		{
			if (stack_sizes == null)
			{
				stack_sizes = new Dictionary<Instruction, int>();
			}
			int num = stack_size;
			if (stack_sizes.TryGetValue(target, out var value))
			{
				num = System.Math.Max(num, value);
			}
			stack_sizes[target] = num;
		}

		private static void ComputeStackSize(Instruction instruction, ref int stack_size)
		{
			FlowControl flowControl = instruction.opcode.FlowControl;
			FlowControl flowControl2 = flowControl;
			if (flowControl2 == FlowControl.Branch || (uint)(flowControl2 - 7) <= 1u)
			{
				stack_size = 0;
			}
		}

		private static void ComputeStackDelta(Instruction instruction, ref int stack_size)
		{
			FlowControl flowControl = instruction.opcode.FlowControl;
			FlowControl flowControl2 = flowControl;
			if (flowControl2 == FlowControl.Call)
			{
				IMethodSignature methodSignature = (IMethodSignature)instruction.operand;
				if (methodSignature.HasImplicitThis() && instruction.opcode.Code != Code.Newobj)
				{
					stack_size--;
				}
				if (methodSignature.HasParameters)
				{
					stack_size -= methodSignature.Parameters.Count;
				}
				if (instruction.opcode.Code == Code.Calli)
				{
					stack_size--;
				}
				if (methodSignature.ReturnType.etype != ElementType.Void || instruction.opcode.Code == Code.Newobj)
				{
					stack_size++;
				}
			}
			else
			{
				ComputePopDelta(instruction.opcode.StackBehaviourPop, ref stack_size);
				ComputePushDelta(instruction.opcode.StackBehaviourPush, ref stack_size);
			}
		}

		private static void ComputePopDelta(StackBehaviour pop_behavior, ref int stack_size)
		{
			switch (pop_behavior)
			{
			case StackBehaviour.Pop1:
			case StackBehaviour.Popi:
			case StackBehaviour.Popref:
				stack_size--;
				break;
			case StackBehaviour.Pop1_pop1:
			case StackBehaviour.Popi_pop1:
			case StackBehaviour.Popi_popi:
			case StackBehaviour.Popi_popi8:
			case StackBehaviour.Popi_popr4:
			case StackBehaviour.Popi_popr8:
			case StackBehaviour.Popref_pop1:
			case StackBehaviour.Popref_popi:
				stack_size -= 2;
				break;
			case StackBehaviour.Popi_popi_popi:
			case StackBehaviour.Popref_popi_popi:
			case StackBehaviour.Popref_popi_popi8:
			case StackBehaviour.Popref_popi_popr4:
			case StackBehaviour.Popref_popi_popr8:
			case StackBehaviour.Popref_popi_popref:
				stack_size -= 3;
				break;
			case StackBehaviour.PopAll:
				stack_size = 0;
				break;
			}
		}

		private static void ComputePushDelta(StackBehaviour push_behaviour, ref int stack_size)
		{
			switch (push_behaviour)
			{
			case StackBehaviour.Push1:
			case StackBehaviour.Pushi:
			case StackBehaviour.Pushi8:
			case StackBehaviour.Pushr4:
			case StackBehaviour.Pushr8:
			case StackBehaviour.Pushref:
				stack_size++;
				break;
			case StackBehaviour.Push1_push1:
				stack_size += 2;
				break;
			}
		}

		private void WriteExceptionHandlers()
		{
			Align(4);
			Collection<ExceptionHandler> exceptionHandlers = body.ExceptionHandlers;
			if (exceptionHandlers.Count < 21 && !RequiresFatSection(exceptionHandlers))
			{
				WriteSmallSection(exceptionHandlers);
			}
			else
			{
				WriteFatSection(exceptionHandlers);
			}
		}

		private static bool RequiresFatSection(Collection<ExceptionHandler> handlers)
		{
			for (int i = 0; i < handlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = handlers[i];
				if (IsFatRange(exceptionHandler.TryStart, exceptionHandler.TryEnd))
				{
					return true;
				}
				if (IsFatRange(exceptionHandler.HandlerStart, exceptionHandler.HandlerEnd))
				{
					return true;
				}
				if (exceptionHandler.HandlerType == ExceptionHandlerType.Filter && IsFatRange(exceptionHandler.FilterStart, exceptionHandler.HandlerStart))
				{
					return true;
				}
			}
			return false;
		}

		private static bool IsFatRange(Instruction start, Instruction end)
		{
			if (start == null)
			{
				throw new ArgumentException();
			}
			if (end == null)
			{
				return true;
			}
			return end.Offset - start.Offset > 255 || start.Offset > 65535;
		}

		private void WriteSmallSection(Collection<ExceptionHandler> handlers)
		{
			WriteByte(1);
			WriteByte((byte)(handlers.Count * 12 + 4));
			WriteBytes(2);
			WriteExceptionHandlers(handlers, delegate(int i)
			{
				WriteUInt16((ushort)i);
			}, delegate(int i)
			{
				WriteByte((byte)i);
			});
		}

		private void WriteFatSection(Collection<ExceptionHandler> handlers)
		{
			WriteByte(65);
			int num = handlers.Count * 24 + 4;
			WriteByte((byte)(num & 0xFF));
			WriteByte((byte)((num >> 8) & 0xFF));
			WriteByte((byte)((num >> 16) & 0xFF));
			WriteExceptionHandlers(handlers, base.WriteInt32, base.WriteInt32);
		}

		private void WriteExceptionHandlers(Collection<ExceptionHandler> handlers, Action<int> write_entry, Action<int> write_length)
		{
			for (int i = 0; i < handlers.Count; i++)
			{
				ExceptionHandler exceptionHandler = handlers[i];
				write_entry((int)exceptionHandler.HandlerType);
				write_entry(exceptionHandler.TryStart.Offset);
				write_length(GetTargetOffset(exceptionHandler.TryEnd) - exceptionHandler.TryStart.Offset);
				write_entry(exceptionHandler.HandlerStart.Offset);
				write_length(GetTargetOffset(exceptionHandler.HandlerEnd) - exceptionHandler.HandlerStart.Offset);
				WriteExceptionHandlerSpecific(exceptionHandler);
			}
		}

		private void WriteExceptionHandlerSpecific(ExceptionHandler handler)
		{
			switch (handler.HandlerType)
			{
			case ExceptionHandlerType.Catch:
				WriteMetadataToken(metadata.LookupToken(handler.CatchType));
				break;
			case ExceptionHandlerType.Filter:
				WriteInt32(handler.FilterStart.Offset);
				break;
			default:
				WriteInt32(0);
				break;
			}
		}

		public MetadataToken GetStandAloneSignature(Collection<VariableDefinition> variables)
		{
			uint localVariableBlobIndex = metadata.GetLocalVariableBlobIndex(variables);
			return GetStandAloneSignatureToken(localVariableBlobIndex);
		}

		public MetadataToken GetStandAloneSignature(CallSite call_site)
		{
			uint callSiteBlobIndex = metadata.GetCallSiteBlobIndex(call_site);
			return call_site.MetadataToken = GetStandAloneSignatureToken(callSiteBlobIndex);
		}

		private MetadataToken GetStandAloneSignatureToken(uint signature)
		{
			if (standalone_signatures.TryGetValue(signature, out var value))
			{
				return value;
			}
			value = new MetadataToken(TokenType.Signature, metadata.AddStandAloneSignature(signature));
			standalone_signatures.Add(signature, value);
			return value;
		}

		private uint BeginMethod()
		{
			return (uint)(code_base + position);
		}

		private void WriteMetadataToken(MetadataToken token)
		{
			WriteUInt32(token.ToUInt32());
		}

		private void Align(int align)
		{
			align--;
			WriteBytes(((position + align) & ~align) - position);
		}
	}
	public enum DocumentType
	{
		Other,
		Text
	}
	public enum DocumentHashAlgorithm
	{
		None,
		MD5,
		SHA1,
		SHA256
	}
	public enum DocumentLanguage
	{
		Other,
		C,
		Cpp,
		CSharp,
		Basic,
		Java,
		Cobol,
		Pascal,
		Cil,
		JScript,
		Smc,
		MCpp,
		FSharp
	}
	public enum DocumentLanguageVendor
	{
		Other,
		Microsoft
	}
	public sealed class Document : DebugInformation
	{
		private string url;

		private Guid type;

		private Guid hash_algorithm;

		private Guid language;

		private Guid language_vendor;

		private byte[] hash;

		private byte[] embedded_source;

		public string Url
		{
			get
			{
				return url;
			}
			set
			{
				url = value;
			}
		}

		public DocumentType Type
		{
			get
			{
				return type.ToType();
			}
			set
			{
				type = value.ToGuid();
			}
		}

		public Guid TypeGuid
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		public DocumentHashAlgorithm HashAlgorithm
		{
			get
			{
				return hash_algorithm.ToHashAlgorithm();
			}
			set
			{
				hash_algorithm = value.ToGuid();
			}
		}

		public Guid HashAlgorithmGuid
		{
			get
			{
				return hash_algorithm;
			}
			set
			{
				hash_algorithm = value;
			}
		}

		public DocumentLanguage Language
		{
			get
			{
				return language.ToLanguage();
			}
			set
			{
				language = value.ToGuid();
			}
		}

		public Guid LanguageGuid
		{
			get
			{
				return language;
			}
			set
			{
				language = value;
			}
		}

		public DocumentLanguageVendor LanguageVendor
		{
			get
			{
				return language_vendor.ToVendor();
			}
			set
			{
				language_vendor = value.ToGuid();
			}
		}

		public Guid LanguageVendorGuid
		{
			get
			{
				return language_vendor;
			}
			set
			{
				language_vendor = value;
			}
		}

		public byte[] Hash
		{
			get
			{
				return hash;
			}
			set
			{
				hash = value;
			}
		}

		public byte[] EmbeddedSource
		{
			get
			{
				return embedded_source;
			}
			set
			{
				embedded_source = value;
			}
		}

		public Document(string url)
		{
			this.url = url;
			hash = Empty<byte>.Array;
			embedded_source = Empty<byte>.Array;
			token = new MetadataToken(TokenType.Document);
		}
	}
	public enum ExceptionHandlerType
	{
		Catch = 0,
		Filter = 1,
		Finally = 2,
		Fault = 4
	}
	public sealed class ExceptionHandler
	{
		private Instruction try_start;

		private Instruction try_end;

		private Instruction filter_start;

		private Instruction handler_start;

		private Instruction handler_end;

		private TypeReference catch_type;

		private ExceptionHandlerType handler_type;

		public Instruction TryStart
		{
			get
			{
				return try_start;
			}
			set
			{
				try_start = value;
			}
		}

		public Instruction TryEnd
		{
			get
			{
				return try_end;
			}
			set
			{
				try_end = value;
			}
		}

		public Instruction FilterStart
		{
			get
			{
				return filter_start;
			}
			set
			{
				filter_start = value;
			}
		}

		public Instruction HandlerStart
		{
			get
			{
				return handler_start;
			}
			set
			{
				handler_start = value;
			}
		}

		public Instruction HandlerEnd
		{
			get
			{
				return handler_end;
			}
			set
			{
				handler_end = value;
			}
		}

		public TypeReference CatchType
		{
			get
			{
				return catch_type;
			}
			set
			{
				catch_type = value;
			}
		}

		public ExceptionHandlerType HandlerType
		{
			get
			{
				return handler_type;
			}
			set
			{
				handler_type = value;
			}
		}

		public ExceptionHandler(ExceptionHandlerType handlerType)
		{
			handler_type = handlerType;
		}
	}
	public sealed class ILProcessor
	{
		private readonly MethodBody body;

		private readonly Collection<Instruction> instructions;

		public MethodBody Body => body;

		internal ILProcessor(MethodBody body)
		{
			this.body = body;
			instructions = body.Instructions;
		}

		public Instruction Create(OpCode opcode)
		{
			return Instruction.Create(opcode);
		}

		public Instruction Create(OpCode opcode, TypeReference type)
		{
			return Instruction.Create(opcode, type);
		}

		public Instruction Create(OpCode opcode, CallSite site)
		{
			return Instruction.Create(opcode, site);
		}

		public Instruction Create(OpCode opcode, MethodReference method)
		{
			return Instruction.Create(opcode, method);
		}

		public Instruction Create(OpCode opcode, FieldReference field)
		{
			return Instruction.Create(opcode, field);
		}

		public Instruction Create(OpCode opcode, string value)
		{
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, sbyte value)
		{
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType == OperandType.ShortInlineVar)
			{
				return Instruction.Create(opcode, body.Variables[value]);
			}
			if (opcode.OperandType == OperandType.ShortInlineArg)
			{
				return Instruction.Create(opcode, body.GetParameter(value));
			}
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType == OperandType.InlineVar)
			{
				return Instruction.Create(opcode, body.Variables[value]);
			}
			if (opcode.OperandType == OperandType.InlineArg)
			{
				return Instruction.Create(opcode, body.GetParameter(value));
			}
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, long value)
		{
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, float value)
		{
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, double value)
		{
			return Instruction.Create(opcode, value);
		}

		public Instruction Create(OpCode opcode, Instruction target)
		{
			return Instruction.Create(opcode, target);
		}

		public Instruction Create(OpCode opcode, Instruction[] targets)
		{
			return Instruction.Create(opcode, targets);
		}

		public Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			return Instruction.Create(opcode, variable);
		}

		public Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			return Instruction.Create(opcode, parameter);
		}

		public void Emit(OpCode opcode)
		{
			Append(Create(opcode));
		}

		public void Emit(OpCode opcode, TypeReference type)
		{
			Append(Create(opcode, type));
		}

		public void Emit(OpCode opcode, MethodReference method)
		{
			Append(Create(opcode, method));
		}

		public void Emit(OpCode opcode, CallSite site)
		{
			Append(Create(opcode, site));
		}

		public void Emit(OpCode opcode, FieldReference field)
		{
			Append(Create(opcode, field));
		}

		public void Emit(OpCode opcode, string value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, byte value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, sbyte value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, int value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, long value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, float value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, double value)
		{
			Append(Create(opcode, value));
		}

		public void Emit(OpCode opcode, Instruction target)
		{
			Append(Create(opcode, target));
		}

		public void Emit(OpCode opcode, Instruction[] targets)
		{
			Append(Create(opcode, targets));
		}

		public void Emit(OpCode opcode, VariableDefinition variable)
		{
			Append(Create(opcode, variable));
		}

		public void Emit(OpCode opcode, ParameterDefinition parameter)
		{
			Append(Create(opcode, parameter));
		}

		public void InsertBefore(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			int num = instructions.IndexOf(target);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("target");
			}
			instructions.Insert(num, instruction);
		}

		public void InsertAfter(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			int num = instructions.IndexOf(target);
			if (num == -1)
			{
				throw new ArgumentOutOfRangeException("target");
			}
			instructions.Insert(num + 1, instruction);
		}

		public void InsertAfter(int index, Instruction instruction)
		{
			if (index < 0 || index >= instructions.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			instructions.Insert(index + 1, instruction);
		}

		public void Append(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			instructions.Add(instruction);
		}

		public void Replace(Instruction target, Instruction instruction)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			InsertAfter(target, instruction);
			Remove(target);
		}

		public void Replace(int index, Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			InsertAfter(index, instruction);
			RemoveAt(index);
		}

		public void Remove(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			if (!instructions.Remove(instruction))
			{
				throw new ArgumentOutOfRangeException("instruction");
			}
		}

		public void RemoveAt(int index)
		{
			if (index < 0 || index >= instructions.Count)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			instructions.RemoveAt(index);
		}

		public void Clear()
		{
			instructions.Clear();
		}
	}
	public sealed class Instruction
	{
		internal int offset;

		internal OpCode opcode;

		internal object operand;

		internal Instruction previous;

		internal Instruction next;

		public int Offset
		{
			get
			{
				return offset;
			}
			set
			{
				offset = value;
			}
		}

		public OpCode OpCode
		{
			get
			{
				return opcode;
			}
			set
			{
				opcode = value;
			}
		}

		public object Operand
		{
			get
			{
				return operand;
			}
			set
			{
				operand = value;
			}
		}

		public Instruction Previous
		{
			get
			{
				return previous;
			}
			set
			{
				previous = value;
			}
		}

		public Instruction Next
		{
			get
			{
				return next;
			}
			set
			{
				next = value;
			}
		}

		internal Instruction(int offset, OpCode opCode)
		{
			this.offset = offset;
			opcode = opCode;
		}

		internal Instruction(OpCode opcode, object operand)
		{
			this.opcode = opcode;
			this.operand = operand;
		}

		public Instruction GetPrototype()
		{
			return new Instruction(opcode, operand);
		}

		public int GetSize()
		{
			int size = opcode.Size;
			switch (opcode.OperandType)
			{
			case OperandType.InlineSwitch:
				return size + (1 + ((Instruction[])operand).Length) * 4;
			case OperandType.InlineI8:
			case OperandType.InlineR:
				return size + 8;
			case OperandType.InlineBrTarget:
			case OperandType.InlineField:
			case OperandType.InlineI:
			case OperandType.InlineMethod:
			case OperandType.InlineSig:
			case OperandType.InlineString:
			case OperandType.InlineTok:
			case OperandType.InlineType:
			case OperandType.ShortInlineR:
				return size + 4;
			case OperandType.InlineVar:
			case OperandType.InlineArg:
				return size + 2;
			case OperandType.ShortInlineBrTarget:
			case OperandType.ShortInlineI:
			case OperandType.ShortInlineVar:
			case OperandType.ShortInlineArg:
				return size + 1;
			default:
				return size;
			}
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			AppendLabel(stringBuilder, this);
			stringBuilder.Append(':');
			stringBuilder.Append(' ');
			stringBuilder.Append(opcode.Name);
			if (operand == null)
			{
				return stringBuilder.ToString();
			}
			stringBuilder.Append(' ');
			switch (opcode.OperandType)
			{
			case OperandType.InlineBrTarget:
			case OperandType.ShortInlineBrTarget:
				AppendLabel(stringBuilder, (Instruction)operand);
				break;
			case OperandType.InlineSwitch:
			{
				Instruction[] array = (Instruction[])operand;
				for (int i = 0; i < array.Length; i++)
				{
					if (i > 0)
					{
						stringBuilder.Append(',');
					}
					AppendLabel(stringBuilder, array[i]);
				}
				break;
			}
			case OperandType.InlineString:
				stringBuilder.Append('"');
				stringBuilder.Append(operand);
				stringBuilder.Append('"');
				break;
			default:
				stringBuilder.Append(Convert.ToString(operand, CultureInfo.InvariantCulture));
				break;
			}
			return stringBuilder.ToString();
		}

		private static void AppendLabel(StringBuilder builder, Instruction instruction)
		{
			builder.Append("IL_");
			builder.Append(instruction.offset.ToString("x4"));
		}

		public static Instruction Create(OpCode opcode)
		{
			if (opcode.OperandType != OperandType.InlineNone)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, null);
		}

		public static Instruction Create(OpCode opcode, TypeReference type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (opcode.OperandType != OperandType.InlineType && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, type);
		}

		public static Instruction Create(OpCode opcode, CallSite site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			if (opcode.Code != Code.Calli)
			{
				throw new ArgumentException("code");
			}
			return new Instruction(opcode, site);
		}

		public static Instruction Create(OpCode opcode, MethodReference method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			if (opcode.OperandType != OperandType.InlineMethod && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, method);
		}

		public static Instruction Create(OpCode opcode, FieldReference field)
		{
			if (field == null)
			{
				throw new ArgumentNullException("field");
			}
			if (opcode.OperandType != OperandType.InlineField && opcode.OperandType != OperandType.InlineTok)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, field);
		}

		public static Instruction Create(OpCode opcode, string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (opcode.OperandType != OperandType.InlineString)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, sbyte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI && opcode != OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, byte value)
		{
			if (opcode.OperandType != OperandType.ShortInlineI || opcode == OpCodes.Ldc_I4_S)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, int value)
		{
			if (opcode.OperandType != OperandType.InlineI)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, long value)
		{
			if (opcode.OperandType != OperandType.InlineI8)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, float value)
		{
			if (opcode.OperandType != OperandType.ShortInlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, double value)
		{
			if (opcode.OperandType != OperandType.InlineR)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, value);
		}

		public static Instruction Create(OpCode opcode, Instruction target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (opcode.OperandType != OperandType.InlineBrTarget && opcode.OperandType != OperandType.ShortInlineBrTarget)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, target);
		}

		public static Instruction Create(OpCode opcode, Instruction[] targets)
		{
			if (targets == null)
			{
				throw new ArgumentNullException("targets");
			}
			if (opcode.OperandType != OperandType.InlineSwitch)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, targets);
		}

		public static Instruction Create(OpCode opcode, VariableDefinition variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (opcode.OperandType != OperandType.ShortInlineVar && opcode.OperandType != OperandType.InlineVar)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, variable);
		}

		public static Instruction Create(OpCode opcode, ParameterDefinition parameter)
		{
			if (parameter == null)
			{
				throw new ArgumentNullException("parameter");
			}
			if (opcode.OperandType != OperandType.ShortInlineArg && opcode.OperandType != OperandType.InlineArg)
			{
				throw new ArgumentException("opcode");
			}
			return new Instruction(opcode, parameter);
		}
	}
	public sealed class MethodBody
	{
		internal readonly MethodDefinition method;

		internal ParameterDefinition this_parameter;

		internal int max_stack_size;

		internal int code_size;

		internal bool init_locals;

		internal MetadataToken local_var_token;

		internal Collection<Instruction> instructions;

		internal Collection<ExceptionHandler> exceptions;

		internal Collection<VariableDefinition> variables;

		public MethodDefinition Method => method;

		public int MaxStackSize
		{
			get
			{
				return max_stack_size;
			}
			set
			{
				max_stack_size = value;
			}
		}

		public int CodeSize => code_size;

		public bool InitLocals
		{
			get
			{
				return init_locals;
			}
			set
			{
				init_locals = value;
			}
		}

		public MetadataToken LocalVarToken
		{
			get
			{
				return local_var_token;
			}
			set
			{
				local_var_token = value;
			}
		}

		public Collection<Instruction> Instructions
		{
			get
			{
				if (instructions == null)
				{
					Interlocked.CompareExchange(ref instructions, new InstructionCollection(method), null);
				}
				return instructions;
			}
		}

		public bool HasExceptionHandlers => !exceptions.IsNullOrEmpty();

		public Collection<ExceptionHandler> ExceptionHandlers
		{
			get
			{
				if (exceptions == null)
				{
					Interlocked.CompareExchange(ref exceptions, new Collection<ExceptionHandler>(), null);
				}
				return exceptions;
			}
		}

		public bool HasVariables => !variables.IsNullOrEmpty();

		public Collection<VariableDefinition> Variables
		{
			get
			{
				if (variables == null)
				{
					Interlocked.CompareExchange(ref variables, new VariableDefinitionCollection(method), null);
				}
				return variables;
			}
		}

		public ParameterDefinition ThisParameter
		{
			get
			{
				if (method == null || method.DeclaringType == null)
				{
					throw new NotSupportedException();
				}
				if (!method.HasThis)
				{
					return null;
				}
				if (this_parameter == null)
				{
					Interlocked.CompareExchange(ref this_parameter, CreateThisParameter(method), null);
				}
				return this_parameter;
			}
		}

		private static ParameterDefinition CreateThisParameter(MethodDefinition method)
		{
			TypeReference typeReference = method.DeclaringType;
			if (typeReference.HasGenericParameters)
			{
				GenericInstanceType genericInstanceType = new GenericInstanceType(typeReference, typeReference.GenericParameters.Count);
				for (int i = 0; i < typeReference.GenericParameters.Count; i++)
				{
					genericInstanceType.GenericArguments.Add(typeReference.GenericParameters[i]);
				}
				typeReference = genericInstanceType;
			}
			if (typeReference.IsValueType || typeReference.IsPrimitive)
			{
				typeReference = new ByReferenceType(typeReference);
			}
			return new ParameterDefinition(typeReference, method);
		}

		public MethodBody(MethodDefinition method)
		{
			this.method = method;
		}

		public ILProcessor GetILProcessor()
		{
			return new ILProcessor(this);
		}
	}
	internal sealed class VariableDefinitionCollection : Collection<VariableDefinition>
	{
		private readonly MethodDefinition method;

		internal VariableDefinitionCollection(MethodDefinition method)
		{
			this.method = method;
		}

		internal VariableDefinitionCollection(MethodDefinition method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		protected override void OnAdd(VariableDefinition item, int index)
		{
			item.index = index;
		}

		protected override void OnInsert(VariableDefinition item, int index)
		{
			item.index = index;
			UpdateVariableIndices(index, 1);
		}

		protected override void OnSet(VariableDefinition item, int index)
		{
			item.index = index;
		}

		protected override void OnRemove(VariableDefinition item, int index)
		{
			UpdateVariableIndices(index + 1, -1, item);
			item.index = -1;
		}

		private void UpdateVariableIndices(int startIndex, int offset, VariableDefinition variableToRemove = null)
		{
			for (int i = startIndex; i < size; i++)
			{
				items[i].index = i + offset;
			}
			MethodDebugInformation methodDebugInformation = ((method == null) ? null : method.debug_info);
			if (methodDebugInformation == null || methodDebugInformation.Scope == null)
			{
				return;
			}
			foreach (ScopeDebugInformation scope in methodDebugInformation.GetScopes())
			{
				if (!scope.HasVariables)
				{
					continue;
				}
				Collection<VariableDebugInformation> variables = scope.Variables;
				int num = -1;
				for (int j = 0; j < variables.Count; j++)
				{
					VariableDebugInformation variableDebugInformation = variables[j];
					if (variableToRemove != null && ((variableDebugInformation.index.IsResolved && variableDebugInformation.index.ResolvedVariable == variableToRemove) || (!variableDebugInformation.index.IsResolved && variableDebugInformation.Index == variableToRemove.Index)))
					{
						num = j;
					}
					else if (!variableDebugInformation.index.IsResolved && variableDebugInformation.Index >= startIndex)
					{
						variableDebugInformation.index = new VariableIndex(variableDebugInformation.Index + offset);
					}
				}
				if (num >= 0)
				{
					variables.RemoveAt(num);
				}
			}
		}
	}
	internal class InstructionCollection : Collection<Instruction>
	{
		private struct InstructionOffsetResolver
		{
			private readonly Instruction[] items;

			private readonly Instruction removed_instruction;

			private readonly Instruction existing_instruction;

			private int cache_offset;

			private int cache_index;

			private Instruction cache_instruction;

			public int LastOffset => cache_offset;

			public InstructionOffsetResolver(Instruction[] instructions, Instruction removedInstruction, Instruction existingInstruction)
			{
				items = instructions;
				removed_instruction = removedInstruction;
				existing_instruction = existingInstruction;
				cache_offset = 0;
				cache_index = 0;
				cache_instruction = items[0];
			}

			public void Restart()
			{
				cache_offset = 0;
				cache_index = 0;
				cache_instruction = items[0];
			}

			public InstructionOffset Resolve(InstructionOffset inputOffset)
			{
				InstructionOffset result = ResolveInstructionOffset(inputOffset);
				if (!result.IsEndOfMethod && result.ResolvedInstruction == removed_instruction)
				{
					result = new InstructionOffset(existing_instruction);
				}
				return result;
			}

			private InstructionOffset ResolveInstructionOffset(InstructionOffset inputOffset)
			{
				if (inputOffset.IsResolved)
				{
					return inputOffset;
				}
				int offset = inputOffset.Offset;
				if (cache_offset == offset)
				{
					return new InstructionOffset(cache_instruction);
				}
				if (cache_offset > offset)
				{
					int num = 0;
					for (int i = 0; i < items.Length; i++)
					{
						if (items[i] == null)
						{
							return new InstructionOffset((i == 0) ? items[0] : items[i - 1]);
						}
						if (num == offset)
						{
							return new InstructionOffset(items[i]);
						}
						if (num > offset)
						{
							return new InstructionOffset((i == 0) ? items[0] : items[i - 1]);
						}
						num += items[i].GetSize();
					}
					return default(InstructionOffset);
				}
				int num2 = cache_offset;
				for (int j = cache_index; j < items.Length; j++)
				{
					cache_index = j;
					cache_offset = num2;
					Instruction instruction = items[j];
					if (instruction == null)
					{
						return new InstructionOffset((j == 0) ? items[0] : items[j - 1]);
					}
					cache_instruction = instruction;
					if (cache_offset == offset)
					{
						return new InstructionOffset(cache_instruction);
					}
					if (cache_offset > offset)
					{
						return new InstructionOffset((j == 0) ? items[0] : items[j - 1]);
					}
					num2 += instruction.GetSize();
				}
				return default(InstructionOffset);
			}
		}

		private readonly MethodDefinition method;

		internal InstructionCollection(MethodDefinition method)
		{
			this.method = method;
		}

		internal InstructionCollection(MethodDefinition method, int capacity)
			: base(capacity)
		{
			this.method = method;
		}

		protected override void OnAdd(Instruction item, int index)
		{
			if (index != 0)
			{
				Instruction instruction = items[index - 1];
				instruction.next = item;
				item.previous = instruction;
			}
		}

		protected override void OnInsert(Instruction item, int index)
		{
			int num = 0;
			if (size != 0)
			{
				Instruction instruction = items[index];
				if (instruction == null)
				{
					Instruction instruction2 = items[index - 1];
					instruction2.next = item;
					item.previous = instruction2;
					return;
				}
				num = instruction.Offset;
				Instruction previous = instruction.previous;
				if (previous != null)
				{
					previous.next = item;
					item.previous = previous;
				}
				instruction.previous = item;
				item.next = instruction;
			}
			UpdateDebugInformation(null, null);
		}

		protected override void OnSet(Instruction item, int index)
		{
			Instruction instruction = items[index];
			item.previous = instruction.previous;
			item.next = instruction.next;
			instruction.previous = null;
			instruction.next = null;
			UpdateDebugInformation(item, instruction);
		}

		protected override void OnRemove(Instruction item, int index)
		{
			Instruction previous = item.previous;
			if (previous != null)
			{
				previous.next = item.next;
			}
			Instruction next = item.next;
			if (next != null)
			{
				next.previous = item.previous;
			}
			RemoveSequencePoint(item);
			UpdateDebugInformation(item, next ?? previous);
			item.previous = null;
			item.next = null;
		}

		private void RemoveSequencePoint(Instruction instruction)
		{
			MethodDebugInformation debug_info = method.debug_info;
			if (debug_info == null || !debug_info.HasSequencePoints)
			{
				return;
			}
			Collection<SequencePoint> sequence_points = debug_info.sequence_points;
			for (int i = 0; i < sequence_points.Count; i++)
			{
				if (sequence_points[i].Offset == instruction.offset)
				{
					sequence_points.RemoveAt(i);
					break;
				}
			}
		}

		private void UpdateDebugInformation(Instruction removedInstruction, Instruction existingInstruction)
		{
			InstructionOffsetResolver resolver = new InstructionOffsetResolver(items, removedInstruction, existingInstruction);
			if (method.debug_info != null)
			{
				UpdateLocalScope(method.debug_info.Scope, ref resolver);
			}
			Collection<CustomDebugInformation> collection = method.custom_infos ?? method.debug_info?.custom_infos;
			if (collection == null)
			{
				return;
			}
			foreach (CustomDebugInformation item in collection)
			{
				CustomDebugInformation customDebugInformation = item;
				CustomDebugInformation customDebugInformation2 = customDebugInformation;
				if (!(customDebugInformation2 is StateMachineScopeDebugInformation debugInfo))
				{
					if (customDebugInformation2 is AsyncMethodBodyDebugInformation debugInfo2)
					{
						UpdateAsyncMethodBody(debugInfo2, ref resolver);
					}
				}
				else
				{
					UpdateStateMachineScope(debugInfo, ref resolver);
				}
			}
		}

		private void UpdateLocalScope(ScopeDebugInformation scope, ref InstructionOffsetResolver resolver)
		{
			if (scope == null)
			{
				return;
			}
			scope.Start = resolver.Resolve(scope.Start);
			if (scope.HasScopes)
			{
				foreach (ScopeDebugInformation scope2 in scope.Scopes)
				{
					UpdateLocalScope(scope2, ref resolver);
				}
			}
			scope.End = resolver.Resolve(scope.End);
		}

		private void UpdateStateMachineScope(StateMachineScopeDebugInformation debugInfo, ref InstructionOffsetResolver resolver)
		{
			resolver.Restart();
			foreach (StateMachineScope scope in debugInfo.Scopes)
			{
				scope.Start = resolver.Resolve(scope.Start);
				scope.End = resolver.Resolve(scope.End);
			}
		}

		private void UpdateAsyncMethodBody(AsyncMethodBodyDebugInformation debugInfo, ref InstructionOffsetResolver resolver)
		{
			if (!debugInfo.CatchHandler.IsResolved)
			{
				resolver.Restart();
				debugInfo.CatchHandler = resolver.Resolve(debugInfo.CatchHandler);
			}
			resolver.Restart();
			for (int i = 0; i < debugInfo.Yields.Count; i++)
			{
				debugInfo.Yields[i] = resolver.Resolve(debugInfo.Yields[i]);
			}
			resolver.Restart();
			for (int j = 0; j < debugInfo.Resumes.Count; j++)
			{
				debugInfo.Resumes[j] = resolver.Resolve(debugInfo.Resumes[j]);
			}
		}
	}
	public enum FlowControl
	{
		Branch,
		Break,
		Call,
		Cond_Branch,
		Meta,
		Next,
		Phi,
		Return,
		Throw
	}
	public enum OpCodeType
	{
		Annotation,
		Macro,
		Nternal,
		Objmodel,
		Prefix,
		Primitive
	}
	public enum OperandType
	{
		InlineBrTarget,
		InlineField,
		InlineI,
		InlineI8,
		InlineMethod,
		InlineNone,
		InlinePhi,
		InlineR,
		InlineSig,
		InlineString,
		InlineSwitch,
		InlineTok,
		InlineType,
		InlineVar,
		InlineArg,
		ShortInlineBrTarget,
		ShortInlineI,
		ShortInlineR,
		ShortInlineVar,
		ShortInlineArg
	}
	public enum StackBehaviour
	{
		Pop0,
		Pop1,
		Pop1_pop1,
		Popi,
		Popi_pop1,
		Popi_popi,
		Popi_popi8,
		Popi_popi_popi,
		Popi_popr4,
		Popi_popr8,
		Popref,
		Popref_pop1,
		Popref_popi,
		Popref_popi_popi,
		Popref_popi_popi8,
		Popref_popi_popr4,
		Popref_popi_popr8,
		Popref_popi_popref,
		PopAll,
		Push0,
		Push1,
		Push1_push1,
		Pushi,
		Pushi8,
		Pushr4,
		Pushr8,
		Pushref,
		Varpop,
		Varpush
	}
	public struct OpCode : IEquatable<OpCode>
	{
		private readonly byte op1;

		private readonly byte op2;

		private readonly byte code;

		private readonly byte flow_control;

		private readonly byte opcode_type;

		private readonly byte operand_type;

		private readonly byte stack_behavior_pop;

		private readonly byte stack_behavior_push;

		public string Name => OpCodeNames.names[(int)Code];

		public int Size => (op1 == byte.MaxValue) ? 1 : 2;

		public byte Op1 => op1;

		public byte Op2 => op2;

		public short Value => (op1 == byte.MaxValue) ? op2 : ((short)((op1 << 8) | op2));

		public Code Code => (Code)code;

		public FlowControl FlowControl => (FlowControl)flow_control;

		public OpCodeType OpCodeType => (OpCodeType)opcode_type;

		public OperandType OperandType => (OperandType)operand_type;

		public StackBehaviour StackBehaviourPop => (StackBehaviour)stack_behavior_pop;

		public StackBehaviour StackBehaviourPush => (StackBehaviour)stack_behavior_push;

		internal OpCode(int x, int y)
		{
			op1 = (byte)(x & 0xFF);
			op2 = (byte)((x >> 8) & 0xFF);
			code = (byte)((x >> 16) & 0xFF);
			flow_control = (byte)((x >> 24) & 0xFF);
			opcode_type = (byte)(y & 0xFF);
			operand_type = (byte)((y >> 8) & 0xFF);
			stack_behavior_pop = (byte)((y >> 16) & 0xFF);
			stack_behavior_push = (byte)((y >> 24) & 0xFF);
			if (op1 == byte.MaxValue)
			{
				OpCodes.OneByteOpCode[op2] = this;
			}
			else
			{
				OpCodes.TwoBytesOpCode[op2] = this;
			}
		}

		public override int GetHashCode()
		{
			return Value;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is OpCode opCode))
			{
				return false;
			}
			return op1 == opCode.op1 && op2 == opCode.op2;
		}

		public bool Equals(OpCode opcode)
		{
			return op1 == opcode.op1 && op2 == opcode.op2;
		}

		public static bool operator ==(OpCode one, OpCode other)
		{
			return one.op1 == other.op1 && one.op2 == other.op2;
		}

		public static bool operator !=(OpCode one, OpCode other)
		{
			return one.op1 != other.op1 || one.op2 != other.op2;
		}

		public override string ToString()
		{
			return Name;
		}
	}
	internal static class OpCodeNames
	{
		internal static readonly string[] names;

		static OpCodeNames()
		{
			byte[] array = new byte[1790]
			{
				3, 110, 111, 112, 5, 98, 114, 101, 97, 107,
				7, 108, 100, 97, 114, 103, 46, 48, 7, 108,
				100, 97, 114, 103, 46, 49, 7, 108, 100, 97,
				114, 103, 46, 50, 7, 108, 100, 97, 114, 103,
				46, 51, 7, 108, 100, 108, 111, 99, 46, 48,
				7, 108, 100, 108, 111, 99, 46, 49, 7, 108,
				100, 108, 111, 99, 46, 50, 7, 108, 100, 108,
				111, 99, 46, 51, 7, 115, 116, 108, 111, 99,
				46, 48, 7, 115, 116, 108, 111, 99, 46, 49,
				7, 115, 116, 108, 111, 99, 46, 50, 7, 115,
				116, 108, 111, 99, 46, 51, 7, 108, 100, 97,
				114, 103, 46, 115, 8, 108, 100, 97, 114, 103,
				97, 46, 115, 7, 115, 116, 97, 114, 103, 46,
				115, 7, 108, 100, 108, 111, 99, 46, 115, 8,
				108, 100, 108, 111, 99, 97, 46, 115, 7, 115,
				116, 108, 111, 99, 46, 115, 6, 108, 100, 110,
				117, 108, 108, 9, 108, 100, 99, 46, 105, 52,
				46, 109, 49, 8, 108, 100, 99, 46, 105, 52,
				46, 48, 8, 108, 100, 99, 46, 105, 52, 46,
				49, 8, 108, 100, 99, 46, 105, 52, 46, 50,
				8, 108, 100, 99, 46, 105, 52, 46, 51, 8,
				108, 100, 99, 46, 105, 52, 46, 52, 8, 108,
				100, 99, 46, 105, 52, 46, 53, 8, 108, 100,
				99, 46, 105, 52, 46, 54, 8, 108, 100, 99,
				46, 105, 52, 46, 55, 8, 108, 100, 99, 46,
				105, 52, 46, 56, 8, 108, 100, 99, 46, 105,
				52, 46, 115, 6, 108, 100, 99, 46, 105, 52,
				6, 108, 100, 99, 46, 105, 56, 6, 108, 100,
				99, 46, 114, 52, 6, 108, 100, 99, 46, 114,
				56, 3, 100, 117, 112, 3, 112, 111, 112, 3,
				106, 109, 112, 4, 99, 97, 108, 108, 5, 99,
				97, 108, 108, 105, 3, 114, 101, 116, 4, 98,
				114, 46, 115, 9, 98, 114, 102, 97, 108, 115,
				101, 46, 115, 8, 98, 114, 116, 114, 117, 101,
				46, 115, 5, 98, 101, 113, 46, 115, 5, 98,
				103, 101, 46, 115, 5, 98, 103, 116, 46, 115,
				5, 98, 108, 101, 46, 115, 5, 98, 108, 116,
				46, 115, 8, 98, 110, 101, 46, 117, 110, 46,
				115, 8, 98, 103, 101, 46, 117, 110, 46, 115,
				8, 98, 103, 116, 46, 117, 110, 46, 115, 8,
				98, 108, 101, 46, 117, 110, 46, 115, 8, 98,
				108, 116, 46, 117, 110, 46, 115, 2, 98, 114,
				7, 98, 114, 102, 97, 108, 115, 101, 6, 98,
				114, 116, 114, 117, 101, 3, 98, 101, 113, 3,
				98, 103, 101, 3, 98, 103, 116, 3, 98, 108,
				101, 3, 98, 108, 116, 6, 98, 110, 101, 46,
				117, 110, 6, 98, 103, 101, 46, 117, 110, 6,
				98, 103, 116, 46, 117, 110, 6, 98, 108, 101,
				46, 117, 110, 6, 98, 108, 116, 46, 117, 110,
				6, 115, 119, 105, 116, 99, 104, 8, 108, 100,
				105, 110, 100, 46, 105, 49, 8, 108, 100, 105,
				110, 100, 46, 117, 49, 8, 108, 100, 105, 110,
				100, 46, 105, 50, 8, 108, 100, 105, 110, 100,
				46, 117, 50, 8, 108, 100, 105, 110, 100, 46,
				105, 52, 8, 108, 100, 105, 110, 100, 46, 117,
				52, 8, 108, 100, 105, 110, 100, 46, 105, 56,
				7, 108, 100, 105, 110, 100, 46, 105, 8, 108,
				100, 105, 110, 100, 46, 114, 52, 8, 108, 100,
				105, 110, 100, 46, 114, 56, 9, 108, 100, 105,
				110, 100, 46, 114, 101, 102, 9, 115, 116, 105,
				110, 100, 46, 114, 101, 102, 8, 115, 116, 105,
				110, 100, 46, 105, 49, 8, 115, 116, 105, 110,
				100, 46, 105, 50, 8, 115, 116, 105, 110, 100,
				46, 105, 52, 8, 115, 116, 105, 110, 100, 46,
				105, 56, 8, 115, 116, 105, 110, 100, 46, 114,
				52, 8, 115, 116, 105, 110, 100, 46, 114, 56,
				3, 97, 100, 100, 3, 115, 117, 98, 3, 109,
				117, 108, 3, 100, 105, 118, 6, 100, 105, 118,
				46, 117, 110, 3, 114, 101, 109, 6, 114, 101,
				109, 46, 117, 110, 3, 97, 110, 100, 2, 111,
				114, 3, 120, 111, 114, 3, 115, 104, 108, 3,
				115, 104, 114, 6, 115, 104, 114, 46, 117, 110,
				3, 110, 101, 103, 3, 110, 111, 116, 7, 99,
				111, 110, 118, 46, 105, 49, 7, 99, 111, 110,
				118, 46, 105, 50, 7, 99, 111, 110, 118, 46,
				105, 52, 7, 99, 111, 110, 118, 46, 105, 56,
				7, 99, 111, 110, 118, 46, 114, 52, 7, 99,
				111, 110, 118, 46, 114, 56, 7, 99, 111, 110,
				118, 46, 117, 52, 7, 99, 111, 110, 118, 46,
				117, 56, 8, 99, 97, 108, 108, 118, 105, 114,
				116, 5, 99, 112, 111, 98, 106, 5, 108, 100,
				111, 98, 106, 5, 108, 100, 115, 116, 114, 6,
				110, 101, 119, 111, 98, 106, 9, 99, 97, 115,
				116, 99, 108, 97, 115, 115, 6, 105, 115, 105,
				110, 115, 116, 9, 99, 111, 110, 118, 46, 114,
				46, 117, 110, 5, 117, 110, 98, 111, 120, 5,
				116, 104, 114, 111, 119, 5, 108, 100, 102, 108,
				100, 6, 108, 100, 102, 108, 100, 97, 5, 115,
				116, 102, 108, 100, 6, 108, 100, 115, 102, 108,
				100, 7, 108, 100, 115, 102, 108, 100, 97, 6,
				115, 116, 115, 102, 108, 100, 5, 115, 116, 111,
				98, 106, 14, 99, 111, 110, 118, 46, 111, 118,
				102, 46, 105, 49, 46, 117, 110, 14, 99, 111,
				110, 118, 46, 111, 118, 102, 46, 105, 50, 46,
				117, 110, 14, 99, 111, 110, 118, 46, 111, 118,
				102, 46, 105, 52, 46, 117, 110, 14, 99, 111,
				110, 118, 46, 111, 118, 102, 46, 105, 56, 46,
				117, 110, 14, 99, 111, 110, 118, 46, 111, 118,
				102, 46, 117, 49, 46, 117, 110, 14, 99, 111,
				110, 118, 46, 111, 118, 102, 46, 117, 50, 46,
				117, 110, 14, 99, 111, 110, 118, 46, 111, 118,
				102, 46, 117, 52, 46, 117, 110, 14, 99, 111,
				110, 118, 46, 111, 118, 102, 46, 117, 56, 46,
				117, 110, 13, 99, 111, 110, 118, 46, 111, 118,
				102, 46, 105, 46, 117, 110, 13, 99, 111, 110,
				118, 46, 111, 118, 102, 46, 117, 46, 117, 110,
				3, 98, 111, 120, 6, 110, 101, 119, 97, 114,
				114, 5, 108, 100, 108, 101, 110, 7, 108, 100,
				101, 108, 101, 109, 97, 9, 108, 100, 101, 108,
				101, 109, 46, 105, 49, 9, 108, 100, 101, 108,
				101, 109, 46, 117, 49, 9, 108, 100, 101, 108,
				101, 109, 46, 105, 50, 9, 108, 100, 101, 108,
				101, 109, 46, 117, 50, 9, 108, 100, 101, 108,
				101, 109, 46, 105, 52, 9, 108, 100, 101, 108,
				101, 109, 46, 117, 52, 9, 108, 100, 101, 108,
				101, 109, 46, 105, 56, 8, 108, 100, 101, 108,
				101, 109, 46, 105, 9, 108, 100, 101, 108, 101,
				109, 46, 114, 52, 9, 108, 100, 101, 108, 101,
				109, 46, 114, 56, 10, 108, 100, 101, 108, 101,
				109, 46, 114, 101, 102, 8, 115, 116, 101, 108,
				101, 109, 46, 105, 9, 115, 116, 101, 108, 101,
				109, 46, 105, 49, 9, 115, 116, 101, 108, 101,
				109, 46, 105, 50, 9, 115, 116, 101, 108, 101,
				109, 46, 105, 52, 9, 115, 116, 101, 108, 101,
				109, 46, 105, 56, 9, 115, 116, 101, 108, 101,
				109, 46, 114, 52, 9, 115, 116, 101, 108, 101,
				109, 46, 114, 56, 10, 115, 116, 101, 108, 101,
				109, 46, 114, 101, 102, 10, 108, 100, 101, 108,
				101, 109, 46, 97, 110, 121, 10, 115, 116, 101,
				108, 101, 109, 46, 97, 110, 121, 9, 117, 110,
				98, 111, 120, 46, 97, 110, 121, 11, 99, 111,
				110, 118, 46, 111, 118, 102, 46, 105, 49, 11,
				99, 111, 110, 118, 46, 111, 118, 102, 46, 117,
				49, 11, 99, 111, 110, 118, 46, 111, 118, 102,
				46, 105, 50, 11, 99, 111, 110, 118, 46, 111,
				118, 102, 46, 117, 50, 11, 99, 111, 110, 118,
				46, 111, 118, 102, 46, 105, 52, 11, 99, 111,
				110, 118, 46, 111, 118, 102, 46, 117, 52, 11,
				99, 111, 110, 118, 46, 111, 118, 102, 46, 105,
				56, 11, 99, 111, 110, 118, 46, 111, 118, 102,
				46, 117, 56, 9, 114, 101, 102, 97, 110, 121,
				118, 97, 108, 8, 99, 107, 102, 105, 110, 105,
				116, 101, 8, 109, 107, 114, 101, 102, 97, 110,
				121, 7, 108, 100, 116, 111, 107, 101, 110, 7,
				99, 111, 110, 118, 46, 117, 50, 7, 99, 111,
				110, 118, 46, 117, 49, 6, 99, 111, 110, 118,
				46, 105, 10, 99, 111, 110, 118, 46, 111, 118,
				102, 46, 105, 10, 99, 111, 110, 118, 46, 111,
				118, 102, 46, 117, 7, 97, 100, 100, 46, 111,
				118, 102, 10, 97, 100, 100, 46, 111, 118, 102,
				46, 117, 110, 7, 109, 117, 108, 46, 111, 118,
				102, 10, 109, 117, 108, 46, 111, 118, 102, 46,
				117, 110, 7, 115, 117, 98, 46, 111, 118, 102,
				10, 115, 117, 98, 46, 111, 118, 102, 46, 117,
				110, 10, 101, 110, 100, 102, 105, 110, 97, 108,
				108, 121, 5, 108, 101, 97, 118, 101, 7, 108,
				101, 97, 118, 101, 46, 115, 7, 115, 116, 105,
				110, 100, 46, 105, 6, 99, 111, 110, 118, 46,
				117, 7, 97, 114, 103, 108, 105, 115, 116, 3,
				99, 101, 113, 3, 99, 103, 116, 6, 99, 103,
				116, 46, 117, 110, 3, 99, 108, 116, 6, 99,
				108, 116, 46, 117, 110, 5, 108, 100, 102, 116,
				110, 9, 108, 100, 118, 105, 114, 116, 102, 116,
				110, 5, 108, 100, 97, 114, 103, 6, 108, 100,
				97, 114, 103, 97, 5, 115, 116, 97, 114, 103,
				5, 108, 100, 108, 111, 99, 6, 108, 100, 108,
				111, 99, 97, 5, 115, 116, 108, 111, 99, 8,
				108, 111, 99, 97, 108, 108, 111, 99, 9, 101,
				110, 100, 102, 105, 108, 116, 101, 114, 10, 117,
				110, 97, 108, 105, 103, 110, 101, 100, 46, 9,
				118, 111, 108, 97, 116, 105, 108, 101, 46, 5,
				116, 97, 105, 108, 46, 7, 105, 110, 105, 116,
				111, 98, 106, 12, 99, 111, 110, 115, 116, 114,
				97, 105, 110, 101, 100, 46, 5, 99, 112, 98,
				108, 107, 7, 105, 110, 105, 116, 98, 108, 107,
				3, 110, 111, 46, 7, 114, 101, 116, 104, 114,
				111, 119, 6, 115, 105, 122, 101, 111, 102, 10,
				114, 101, 102, 97, 110, 121, 116, 121, 112, 101,
				9, 114, 101, 97, 100, 111, 110, 108, 121, 46
			};
			names = new string[219];
			int i = 0;
			int num = 0;
			for (; i < names.Length; i++)
			{
				char[] array2 = new char[array[num++]];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = (char)array[num++];
				}
				names[i] = new string(array2);
			}
		}
	}
	public static class OpCodes
	{
		internal static readonly OpCode[] OneByteOpCode = new OpCode[225];

		internal static readonly OpCode[] TwoBytesOpCode = new OpCode[31];

		public static readonly OpCode Nop = new OpCode(83886335, 318768389);

		public static readonly OpCode Break = new OpCode(16843263, 318768389);

		public static readonly OpCode Ldarg_0 = new OpCode(84017919, 335545601);

		public static readonly OpCode Ldarg_1 = new OpCode(84083711, 335545601);

		public static readonly OpCode Ldarg_2 = new OpCode(84149503, 335545601);

		public static readonly OpCode Ldarg_3 = new OpCode(84215295, 335545601);

		public static readonly OpCode Ldloc_0 = new OpCode(84281087, 335545601);

		public static readonly OpCode Ldloc_1 = new OpCode(84346879, 335545601);

		public static readonly OpCode Ldloc_2 = new OpCode(84412671, 335545601);

		public static readonly OpCode Ldloc_3 = new OpCode(84478463, 335545601);

		public static readonly OpCode Stloc_0 = new OpCode(84544255, 318833921);

		public static readonly OpCode Stloc_1 = new OpCode(84610047, 318833921);

		public static readonly OpCode Stloc_2 = new OpCode(84675839, 318833921);

		public static readonly OpCode Stloc_3 = new OpCode(84741631, 318833921);

		public static readonly OpCode Ldarg_S = new OpCode(84807423, 335549185);

		public static readonly OpCode Ldarga_S = new OpCode(84873215, 369103617);

		public static readonly OpCode Starg_S = new OpCode(84939007, 318837505);

		public static readonly OpCode Ldloc_S = new OpCode(85004799, 335548929);

		public static readonly OpCode Ldloca_S = new OpCode(85070591, 369103361);

		public static readonly OpCode Stloc_S = new OpCode(85136383, 318837249);

		public static readonly OpCode Ldnull = new OpCode(85202175, 436208901);

		public static readonly OpCode Ldc_I4_M1 = new OpCode(85267967, 369100033);

		public static readonly OpCode Ldc_I4_0 = new OpCode(85333759, 369100033);

		public static readonly OpCode Ldc_I4_1 = new OpCode(85399551, 369100033);

		public static readonly OpCode Ldc_I4_2 = new OpCode(85465343, 369100033);

		public static readonly OpCode Ldc_I4_3 = new OpCode(85531135, 369100033);

		public static readonly OpCode Ldc_I4_4 = new OpCode(85596927, 369100033);

		public static readonly OpCode Ldc_I4_5 = new OpCode(85662719, 369100033);

		public static readonly OpCode Ldc_I4_6 = new OpCode(85728511, 369100033);

		public static readonly OpCode Ldc_I4_7 = new OpCode(85794303, 369100033);

		public static readonly OpCode Ldc_I4_8 = new OpCode(85860095, 369100033);

		public static readonly OpCode Ldc_I4_S = new OpCode(85925887, 369102849);

		public static readonly OpCode Ldc_I4 = new OpCode(85991679, 369099269);

		public static readonly OpCode Ldc_I8 = new OpCode(86057471, 385876741);

		public static readonly OpCode Ldc_R4 = new OpCode(86123263, 402657541);

		public static readonly OpCode Ldc_R8 = new OpCode(86189055, 419432197);

		public static readonly OpCode Dup = new OpCode(86255103, 352388357);

		public static readonly OpCode Pop = new OpCode(86320895, 318833925);

		public static readonly OpCode Jmp = new OpCode(36055039, 318768133);

		public static readonly OpCode Call = new OpCode(36120831, 471532549);

		public static readonly OpCode Calli = new OpCode(36186623, 471533573);

		public static readonly OpCode Ret = new OpCode(120138495, 320537861);

		public static readonly OpCode Br_S = new OpCode(2763775, 318770945);

		public static readonly OpCode Brfalse_S = new OpCode(53161215, 318967553);

		public static readonly OpCode Brtrue_S = new OpCode(53227007, 318967553);

		public static readonly OpCode Beq_S = new OpCode(53292799, 318902017);

		public static readonly OpCode Bge_S = new OpCode(53358591, 318902017);

		public static readonly OpCode Bgt_S = new OpCode(53424383, 318902017);

		public static readonly OpCode Ble_S = new OpCode(53490175, 318902017);

		public static readonly OpCode Blt_S = new OpCode(53555967, 318902017);

		public static readonly OpCode Bne_Un_S = new OpCode(53621759, 318902017);

		public static readonly OpCode Bge_Un_S = new OpCode(53687551, 318902017);

		public static readonly OpCode Bgt_Un_S = new OpCode(53753343, 318902017);

		public static readonly OpCode Ble_Un_S = new OpCode(53819135, 318902017);

		public static readonly OpCode Blt_Un_S = new OpCode(53884927, 318902017);

		public static readonly OpCode Br = new OpCode(3619071, 318767109);

		public static readonly OpCode Brfalse = new OpCode(54016511, 318963717);

		public static readonly OpCode Brtrue = new OpCode(54082303, 318963717);

		public static readonly OpCode Beq = new OpCode(54148095, 318898177);

		public static readonly OpCode Bge = new OpCode(54213887, 318898177);

		public static readonly OpCode Bgt = new OpCode(54279679, 318898177);

		public static readonly OpCode Ble = new OpCode(54345471, 318898177);

		public static readonly OpCode Blt = new OpCode(54411263, 318898177);

		public static readonly OpCode Bne_Un = new OpCode(54477055, 318898177);

		public static readonly OpCode Bge_Un = new OpCode(54542847, 318898177);

		public static readonly OpCode Bgt_Un = new OpCode(54608639, 318898177);

		public static readonly OpCode Ble_Un = new OpCode(54674431, 318898177);

		public static readonly OpCode Blt_Un = new OpCode(54740223, 318898177);

		public static readonly OpCode Switch = new OpCode(54806015, 318966277);

		public static readonly OpCode Ldind_I1 = new OpCode(88426239, 369296645);

		public static readonly OpCode Ldind_U1 = new OpCode(88492031, 369296645);

		public static readonly OpCode Ldind_I2 = new OpCode(88557823, 369296645);

		public static readonly OpCode Ldind_U2 = new OpCode(88623615, 369296645);

		public static readonly OpCode Ldind_I4 = new OpCode(88689407, 369296645);

		public static readonly OpCode Ldind_U4 = new OpCode(88755199, 369296645);

		public static readonly OpCode Ldind_I8 = new OpCode(88820991, 386073861);

		public static readonly OpCode Ldind_I = new OpCode(88886783, 369296645);

		public static readonly OpCode Ldind_R4 = new OpCode(88952575, 402851077);

		public static readonly OpCode Ldind_R8 = new OpCode(89018367, 419628293);

		public static readonly OpCode Ldind_Ref = new OpCode(89084159, 436405509);

		public static readonly OpCode Stind_Ref = new OpCode(89149951, 319096069);

		public static readonly OpCode Stind_I1 = new OpCode(89215743, 319096069);

		public static readonly OpCode Stind_I2 = new OpCode(89281535, 319096069);

		public static readonly OpCode Stind_I4 = new OpCode(89347327, 319096069);

		public static readonly OpCode Stind_I8 = new OpCode(89413119, 319161605);

		public static readonly OpCode Stind_R4 = new OpCode(89478911, 319292677);

		public static readonly OpCode Stind_R8 = new OpCode(89544703, 319358213);

		public static readonly OpCode Add = new OpCode(89610495, 335676677);

		public static readonly OpCode Sub = new OpCode(89676287, 335676677);

		public static readonly OpCode Mul = new OpCode(89742079, 335676677);

		public static readonly OpCode Div = new OpCode(89807871, 335676677);

		public static readonly OpCode Div_Un = new OpCode(89873663, 335676677);

		public static readonly OpCode Rem = new OpCode(89939455, 335676677);

		public static readonly OpCode Rem_Un = new OpCode(90005247, 335676677);

		public static readonly OpCode And = new OpCode(90071039, 335676677);

		public static readonly OpCode Or = new OpCode(90136831, 335676677);

		public static readonly OpCode Xor = new OpCode(90202623, 335676677);

		public static readonly OpCode Shl = new OpCode(90268415, 335676677);

		public static readonly OpCode Shr = new OpCode(90334207, 335676677);

		public static readonly OpCode Shr_Un = new OpCode(90399999, 335676677);

		public static readonly OpCode Neg = new OpCode(90465791, 335611141);

		public static readonly OpCode Not = new OpCode(90531583, 335611141);

		public static readonly OpCode Conv_I1 = new OpCode(90597375, 369165573);

		public static readonly OpCode Conv_I2 = new OpCode(90663167, 369165573);

		public static readonly OpCode Conv_I4 = new OpCode(90728959, 369165573);

		public static readonly OpCode Conv_I8 = new OpCode(90794751, 385942789);

		public static readonly OpCode Conv_R4 = new OpCode(90860543, 402720005);

		public static readonly OpCode Conv_R8 = new OpCode(90926335, 419497221);

		public static readonly OpCode Conv_U4 = new OpCode(90992127, 369165573);

		public static readonly OpCode Conv_U8 = new OpCode(91057919, 385942789);

		public static readonly OpCode Callvirt = new OpCode(40792063, 471532547);

		public static readonly OpCode Cpobj = new OpCode(91189503, 319097859);

		public static readonly OpCode Ldobj = new OpCode(91255295, 335744003);

		public static readonly OpCode Ldstr = new OpCode(91321087, 436209923);

		public static readonly OpCode Newobj = new OpCode(41055231, 437978115);

		public static readonly OpCode Castclass = new OpCode(91452671, 436866051);

		public static readonly OpCode Isinst = new OpCode(91518463, 369757187);

		public static readonly OpCode Conv_R_Un = new OpCode(91584255, 419497221);

		public static readonly OpCode Unbox = new OpCode(91650559, 369757189);

		public static readonly OpCode Throw = new OpCode(142047999, 319423747);

		public static readonly OpCode Ldfld = new OpCode(91782143, 336199939);

		public static readonly OpCode Ldflda = new OpCode(91847935, 369754371);

		public static readonly OpCode Stfld = new OpCode(91913727, 319488259);

		public static readonly OpCode Ldsfld = new OpCode(91979519, 335544579);

		public static readonly OpCode Ldsflda = new OpCode(92045311, 369099011);

		public static readonly OpCode Stsfld = new OpCode(92111103, 318832899);

		public static readonly OpCode Stobj = new OpCode(92176895, 319032323);

		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(92242687, 369165573);

		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(92308479, 369165573);

		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(92374271, 369165573);

		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(92440063, 385942789);

		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(92505855, 369165573);

		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(92571647, 369165573);

		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(92637439, 369165573);

		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(92703231, 385942789);

		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(92769023, 369165573);

		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(92834815, 369165573);

		public static readonly OpCode Box = new OpCode(92900607, 436276229);

		public static readonly OpCode Newarr = new OpCode(92966399, 436407299);

		public static readonly OpCode Ldlen = new OpCode(93032191, 369755395);

		public static readonly OpCode Ldelema = new OpCode(93097983, 369888259);

		public static readonly OpCode Ldelem_I1 = new OpCode(93163775, 369886467);

		public static readonly OpCode Ldelem_U1 = new OpCode(93229567, 369886467);

		public static readonly OpCode Ldelem_I2 = new OpCode(93295359, 369886467);

		public static readonly OpCode Ldelem_U2 = new OpCode(93361151, 369886467);

		public static readonly OpCode Ldelem_I4 = new OpCode(93426943, 369886467);

		public static readonly OpCode Ldelem_U4 = new OpCode(93492735, 369886467);

		public static readonly OpCode Ldelem_I8 = new OpCode(93558527, 386663683);

		public static readonly OpCode Ldelem_I = new OpCode(93624319, 369886467);

		public static readonly OpCode Ldelem_R4 = new OpCode(93690111, 403440899);

		public static readonly OpCode Ldelem_R8 = new OpCode(93755903, 420218115);

		public static readonly OpCode Ldelem_Ref = new OpCode(93821695, 436995331);

		public static readonly OpCode Stelem_I = new OpCode(93887487, 319620355);

		public static readonly OpCode Stelem_I1 = new OpCode(93953279, 319620355);

		public static readonly OpCode Stelem_I2 = new OpCode(94019071, 319620355);

		public static readonly OpCode Stelem_I4 = new OpCode(94084863, 319620355);

		public static readonly OpCode Stelem_I8 = new OpCode(94150655, 319685891);

		public static readonly OpCode Stelem_R4 = new OpCode(94216447, 319751427);

		public static readonly OpCode Stelem_R8 = new OpCode(94282239, 319816963);

		public static readonly OpCode Stelem_Ref = new OpCode(94348031, 319882499);

		public static readonly OpCode Ldelem_Any = new OpCode(94413823, 336333827);

		public static readonly OpCode Stelem_Any = new OpCode(94479615, 319884291);

		public static readonly OpCode Unbox_Any = new OpCode(94545407, 336202755);

		public static readonly OpCode Conv_Ovf_I1 = new OpCode(94614527, 369165573);

		public static readonly OpCode Conv_Ovf_U1 = new OpCode(94680319, 369165573);

		public static readonly OpCode Conv_Ovf_I2 = new OpCode(94746111, 369165573);

		public static readonly OpCode Conv_Ovf_U2 = new OpCode(94811903, 369165573);

		public static readonly OpCode Conv_Ovf_I4 = new OpCode(94877695, 369165573);

		public static readonly OpCode Conv_Ovf_U4 = new OpCode(94943487, 369165573);

		public static readonly OpCode Conv_Ovf_I8 = new OpCode(95009279, 385942789);

		public static readonly OpCode Conv_Ovf_U8 = new OpCode(95075071, 385942789);

		public static readonly OpCode Refanyval = new OpCode(95142655, 369167365);

		public static readonly OpCode Ckfinite = new OpCode(95208447, 419497221);

		public static readonly OpCode Mkrefany = new OpCode(95274751, 335744005);

		public static readonly OpCode Ldtoken = new OpCode(95342847, 369101573);

		public static readonly OpCode Conv_U2 = new OpCode(95408639, 369165573);

		public static readonly OpCode Conv_U1 = new OpCode(95474431, 369165573);

		public static readonly OpCode Conv_I = new OpCode(95540223, 369165573);

		public static readonly OpCode Conv_Ovf_I = new OpCode(95606015, 369165573);

		public static readonly OpCode Conv_Ovf_U = new OpCode(95671807, 369165573);

		public static readonly OpCode Add_Ovf = new OpCode(95737599, 335676677);

		public static readonly OpCode Add_Ovf_Un = new OpCode(95803391, 335676677);

		public static readonly OpCode Mul_Ovf = new OpCode(95869183, 335676677);

		public static readonly OpCode Mul_Ovf_Un = new OpCode(95934975, 335676677);

		public static readonly OpCode Sub_Ovf = new OpCode(96000767, 335676677);

		public static readonly OpCode Sub_Ovf_Un = new OpCode(96066559, 335676677);

		public static readonly OpCode Endfinally = new OpCode(129686783, 318768389);

		public static readonly OpCode Leave = new OpCode(12312063, 319946757);

		public static readonly OpCode Leave_S = new OpCode(12377855, 319950593);

		public static readonly OpCode Stind_I = new OpCode(96329727, 319096069);

		public static readonly OpCode Conv_U = new OpCode(96395519, 369165573);

		public static readonly OpCode Arglist = new OpCode(96403710, 369100037);

		public static readonly OpCode Ceq = new OpCode(96469502, 369231109);

		public static readonly OpCode Cgt = new OpCode(96535294, 369231109);

		public static readonly OpCode Cgt_Un = new OpCode(96601086, 369231109);

		public static readonly OpCode Clt = new OpCode(96666878, 369231109);

		public static readonly OpCode Clt_Un = new OpCode(96732670, 369231109);

		public static readonly OpCode Ldftn = new OpCode(96798462, 369099781);

		public static readonly OpCode Ldvirtftn = new OpCode(96864254, 369755141);

		public static readonly OpCode Ldarg = new OpCode(96930302, 335547909);

		public static readonly OpCode Ldarga = new OpCode(96996094, 369102341);

		public static readonly OpCode Starg = new OpCode(97061886, 318836229);

		public static readonly OpCode Ldloc = new OpCode(97127678, 335547653);

		public static readonly OpCode Ldloca = new OpCode(97193470, 369102085);

		public static readonly OpCode Stloc = new OpCode(97259262, 318835973);

		public static readonly OpCode Localloc = new OpCode(97325054, 369296645);

		public static readonly OpCode Endfilter = new OpCode(130945534, 318964997);

		public static readonly OpCode Unaligned = new OpCode(80679678, 318771204);

		public static readonly OpCode Volatile = new OpCode(80745470, 318768388);

		public static readonly OpCode Tail = new OpCode(80811262, 318768388);

		public static readonly OpCode Initobj = new OpCode(97654270, 318966787);

		public static readonly OpCode Constrained = new OpCode(97720062, 318770180);

		public static readonly OpCode Cpblk = new OpCode(97785854, 319227141);

		public static readonly OpCode Initblk = new OpCode(97851646, 319227141);

		public static readonly OpCode No = new OpCode(97917438, 318771204);

		public static readonly OpCode Rethrow = new OpCode(148314878, 318768387);

		public static readonly OpCode Sizeof = new OpCode(98049278, 369101829);

		public static readonly OpCode Refanytype = new OpCode(98115070, 369165573);

		public static readonly OpCode Readonly = new OpCode(98180862, 318768388);
	}
	public sealed class PortablePdbReaderProvider : ISymbolReaderProvider
	{
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			FileStream fileStream = File.OpenRead(Mixin.GetPdbFileName(fileName));
			return GetSymbolReader(module, Disposable.Owned((Stream)fileStream), fileStream.Name);
		}

		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return GetSymbolReader(module, Disposable.NotOwned(symbolStream), symbolStream.GetFileName());
		}

		private ISymbolReader GetSymbolReader(ModuleDefinition module, Disposable<Stream> symbolStream, string fileName)
		{
			uint pdb_heap_offset;
			return new PortablePdbReader(ImageReader.ReadPortablePdb(symbolStream, fileName, out pdb_heap_offset), module);
		}
	}
	public sealed class PortablePdbReader : ISymbolReader, IDisposable
	{
		private readonly Image image;

		private readonly ModuleDefinition module;

		private readonly MetadataReader reader;

		private readonly MetadataReader debug_reader;

		private bool IsEmbedded => reader.image == debug_reader.image;

		internal PortablePdbReader(Image image, ModuleDefinition module)
		{
			this.image = image;
			this.module = module;
			reader = module.reader;
			debug_reader = new MetadataReader(image, module, reader);
		}

		public ISymbolWriterProvider GetWriterProvider()
		{
			return new PortablePdbWriterProvider();
		}

		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			if (image == module.Image)
			{
				return true;
			}
			ImageDebugHeaderEntry[] entries = header.Entries;
			foreach (ImageDebugHeaderEntry entry in entries)
			{
				if (IsMatchingEntry(image.PdbHeap, entry))
				{
					ReadModule();
					return true;
				}
			}
			return false;
		}

		private static bool IsMatchingEntry(PdbHeap heap, ImageDebugHeaderEntry entry)
		{
			if (entry.Directory.Type != ImageDebugType.CodeView)
			{
				return false;
			}
			byte[] data = entry.Data;
			if (data.Length < 24)
			{
				return false;
			}
			int num = ReadInt32(data, 0);
			if (num != 1396986706)
			{
				return false;
			}
			byte[] array = new byte[16];
			Buffer.BlockCopy(data, 4, array, 0, 16);
			Guid guid = new Guid(array);
			Buffer.BlockCopy(heap.Id, 0, array, 0, 16);
			Guid guid2 = new Guid(array);
			return guid == guid2;
		}

		private static int ReadInt32(byte[] bytes, int start)
		{
			return bytes[start] | (bytes[start + 1] << 8) | (bytes[start + 2] << 16) | (bytes[start + 3] << 24);
		}

		private void ReadModule()
		{
			module.custom_infos = debug_reader.GetCustomDebugInformation(module);
		}

		public MethodDebugInformation Read(MethodDefinition method)
		{
			MethodDebugInformation methodDebugInformation = new MethodDebugInformation(method);
			ReadSequencePoints(methodDebugInformation);
			ReadScope(methodDebugInformation);
			ReadStateMachineKickOffMethod(methodDebugInformation);
			ReadCustomDebugInformations(methodDebugInformation);
			return methodDebugInformation;
		}

		private void ReadSequencePoints(MethodDebugInformation method_info)
		{
			method_info.sequence_points = debug_reader.ReadSequencePoints(method_info.method);
		}

		private void ReadScope(MethodDebugInformation method_info)
		{
			method_info.scope = debug_reader.ReadScope(method_info.method);
		}

		private void ReadStateMachineKickOffMethod(MethodDebugInformation method_info)
		{
			method_info.kickoff_method = debug_reader.ReadStateMachineKickoffMethod(method_info.method);
		}

		private void ReadCustomDebugInformations(MethodDebugInformation info)
		{
			info.method.custom_infos = debug_reader.GetCustomDebugInformation(info.method);
		}

		public void Dispose()
		{
			if (!IsEmbedded)
			{
				image.Dispose();
			}
		}
	}
	public sealed class EmbeddedPortablePdbReaderProvider : ISymbolReaderProvider
	{
		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			ImageDebugHeader debugHeader = module.GetDebugHeader();
			ImageDebugHeaderEntry embeddedPortablePdbEntry = debugHeader.GetEmbeddedPortablePdbEntry();
			if (embeddedPortablePdbEntry == null)
			{
				throw new InvalidOperationException();
			}
			return new EmbeddedPortablePdbReader((PortablePdbReader)new PortablePdbReaderProvider().GetSymbolReader(module, GetPortablePdbStream(embeddedPortablePdbEntry)));
		}

		private static Stream GetPortablePdbStream(ImageDebugHeaderEntry entry)
		{
			MemoryStream stream = new MemoryStream(entry.Data);
			BinaryStreamReader binaryStreamReader = new BinaryStreamReader(stream);
			binaryStreamReader.ReadInt32();
			int capacity = binaryStreamReader.ReadInt32();
			MemoryStream memoryStream = new MemoryStream(capacity);
			using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, leaveOpen: true))
			{
				deflateStream.CopyTo(memoryStream);
			}
			return memoryStream;
		}

		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
	public sealed class EmbeddedPortablePdbReader : ISymbolReader, IDisposable
	{
		private readonly PortablePdbReader reader;

		internal EmbeddedPortablePdbReader(PortablePdbReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException();
			}
			this.reader = reader;
		}

		public ISymbolWriterProvider GetWriterProvider()
		{
			return new EmbeddedPortablePdbWriterProvider();
		}

		public bool ProcessDebugHeader(ImageDebugHeader header)
		{
			return reader.ProcessDebugHeader(header);
		}

		public MethodDebugInformation Read(MethodDefinition method)
		{
			return reader.Read(method);
		}

		public void Dispose()
		{
			reader.Dispose();
		}
	}
	public sealed class PortablePdbWriterProvider : ISymbolWriterProvider
	{
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			FileStream value = File.Open(Mixin.GetPdbFileName(fileName), FileMode.OpenOrCreate, FileAccess.ReadWrite);
			return GetSymbolWriter(module, Disposable.Owned((Stream)value), Disposable.NotOwned<Stream>(null));
		}

		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			Mixin.CheckModule(module);
			Mixin.CheckStream(symbolStream);
			return GetSymbolWriter(module, Disposable.Owned((Stream)new MemoryStream()), Disposable.NotOwned(symbolStream));
		}

		private ISymbolWriter GetSymbolWriter(ModuleDefinition module, Disposable<Stream> stream, Disposable<Stream> final_stream)
		{
			MetadataBuilder metadataBuilder = new MetadataBuilder(module, this);
			ImageWriter writer = ImageWriter.CreateDebugWriter(module, metadataBuilder, stream);
			return new PortablePdbWriter(metadataBuilder, module, writer, final_stream);
		}
	}
	public sealed class PortablePdbWriter : ISymbolWriter, IDisposable
	{
		private readonly MetadataBuilder pdb_metadata;

		private readonly ModuleDefinition module;

		private readonly ImageWriter writer;

		private readonly Disposable<Stream> final_stream;

		private MetadataBuilder module_metadata;

		internal byte[] pdb_checksum;

		internal Guid pdb_id_guid;

		internal uint pdb_id_stamp;

		private bool IsEmbedded => writer == null;

		internal PortablePdbWriter(MetadataBuilder pdb_metadata, ModuleDefinition module)
		{
			this.pdb_metadata = pdb_metadata;
			this.module = module;
			module_metadata = module.metadata_builder;
			if (module_metadata != pdb_metadata)
			{
				this.pdb_metadata.metadata_builder = module_metadata;
			}
			pdb_metadata.AddCustomDebugInformations(module);
		}

		internal PortablePdbWriter(MetadataBuilder pdb_metadata, ModuleDefinition module, ImageWriter writer, Disposable<Stream> final_stream)
			: this(pdb_metadata, module)
		{
			this.writer = writer;
			this.final_stream = final_stream;
		}

		public ISymbolReaderProvider GetReaderProvider()
		{
			return new PortablePdbReaderProvider();
		}

		public void Write(MethodDebugInformation info)
		{
			CheckMethodDebugInformationTable();
			pdb_metadata.AddMethodDebugInformation(info);
		}

		public void Write()
		{
			if (!IsEmbedded)
			{
				WritePdbFile();
				if (final_stream.value != null)
				{
					writer.BaseStream.Seek(0L, SeekOrigin.Begin);
					byte[] buffer = new byte[8192];
					CryptoService.CopyStreamChunk(writer.BaseStream, final_stream.value, buffer, (int)writer.BaseStream.Length);
				}
			}
		}

		public ImageDebugHeader GetDebugHeader()
		{
			if (IsEmbedded)
			{
				return new ImageDebugHeader();
			}
			ImageDebugDirectory directory = new ImageDebugDirectory
			{
				MajorVersion = 256,
				MinorVersion = 20557,
				Type = ImageDebugType.CodeView,
				TimeDateStamp = (int)pdb_id_stamp
			};
			ByteBuffer byteBuffer = new ByteBuffer();
			byteBuffer.WriteUInt32(1396986706u);
			byteBuffer.WriteBytes(pdb_id_guid.ToByteArray());
			byteBuffer.WriteUInt32(1u);
			string text = writer.BaseStream.GetFileName();
			if (string.IsNullOrEmpty(text))
			{
				text = module.Assembly.Name.Name + ".pdb";
			}
			byteBuffer.WriteBytes(Encoding.UTF8.GetBytes(text));
			byteBuffer.WriteByte(0);
			byte[] array = new byte[byteBuffer.length];
			Buffer.BlockCopy(byteBuffer.buffer, 0, array, 0, byteBuffer.length);
			directory.SizeOfData = array.Length;
			ImageDebugHeaderEntry imageDebugHeaderEntry = new ImageDebugHeaderEntry(directory, array);
			ImageDebugDirectory directory2 = new ImageDebugDirectory
			{
				MajorVersion = 1,
				MinorVersion = 0,
				Type = ImageDebugType.PdbChecksum,
				TimeDateStamp = 0
			};
			ByteBuffer byteBuffer2 = new ByteBuffer();
			byteBuffer2.WriteBytes(Encoding.UTF8.GetBytes("SHA256"));
			byteBuffer2.WriteByte(0);
			byteBuffer2.WriteBytes(pdb_checksum);
			byte[] array2 = new byte[byteBuffer2.length];
			Buffer.BlockCopy(byteBuffer2.buffer, 0, array2, 0, byteBuffer2.length);
			directory2.SizeOfData = array2.Length;
			ImageDebugHeaderEntry imageDebugHeaderEntry2 = new ImageDebugHeaderEntry(directory2, array2);
			return new ImageDebugHeader(new ImageDebugHeaderEntry[2] { imageDebugHeaderEntry, imageDebugHeaderEntry2 });
		}

		private void CheckMethodDebugInformationTable()
		{
			MethodDebugInformationTable table = pdb_metadata.table_heap.GetTable<MethodDebugInformationTable>(Table.MethodDebugInformation);
			if (table.length <= 0)
			{
				table.rows = new Row<uint, uint>[module_metadata.method_rid - 1];
				table.length = table.rows.Length;
			}
		}

		public void Dispose()
		{
			writer.stream.Dispose();
			final_stream.Dispose();
		}

		private void WritePdbFile()
		{
			WritePdbHeap();
			WriteTableHeap();
			writer.BuildMetadataTextMap();
			writer.WriteMetadataHeader();
			writer.WriteMetadata();
			writer.Flush();
			ComputeChecksumAndPdbId();
			WritePdbId();
		}

		private void WritePdbHeap()
		{
			PdbHeapBuffer pdb_heap = pdb_metadata.pdb_heap;
			pdb_heap.WriteBytes(20);
			pdb_heap.WriteUInt32(module_metadata.entry_point.ToUInt32());
			TableHeapBuffer table_heap = module_metadata.table_heap;
			MetadataTable[] tables = table_heap.tables;
			ulong num = 0uL;
			for (int i = 0; i < tables.Length; i++)
			{
				if (tables[i] != null && tables[i].Length != 0)
				{
					num |= (ulong)(1L << i);
				}
			}
			pdb_heap.WriteUInt64(num);
			for (int j = 0; j < tables.Length; j++)
			{
				if (tables[j] != null && tables[j].Length != 0)
				{
					pdb_heap.WriteUInt32((uint)tables[j].Length);
				}
			}
		}

		private void WriteTableHeap()
		{
			pdb_metadata.table_heap.string_offsets = pdb_metadata.string_heap.WriteStrings();
			pdb_metadata.table_heap.ComputeTableInformations();
			pdb_metadata.table_heap.WriteTableHeap();
		}

		private void ComputeChecksumAndPdbId()
		{
			byte[] buffer = new byte[8192];
			writer.BaseStream.Seek(0L, SeekOrigin.Begin);
			SHA256 sHA = SHA256.Create();
			using (CryptoStream dest_stream = new CryptoStream(Stream.Null, sHA, CryptoStreamMode.Write))
			{
				CryptoService.CopyStreamChunk(writer.BaseStream, dest_stream, buffer, (int)writer.BaseStream.Length);
			}
			pdb_checksum = sHA.Hash;
			ByteBuffer byteBuffer = new ByteBuffer(pdb_checksum);
			pdb_id_guid = new Guid(byteBuffer.ReadBytes(16));
			pdb_id_stamp = byteBuffer.ReadUInt32();
		}

		private void WritePdbId()
		{
			writer.MoveToRVA(TextSegment.PdbHeap);
			writer.WriteBytes(pdb_id_guid.ToByteArray());
			writer.WriteUInt32(pdb_id_stamp);
		}
	}
	public sealed class EmbeddedPortablePdbWriterProvider : ISymbolWriterProvider
	{
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			Mixin.CheckModule(module);
			Mixin.CheckFileName(fileName);
			MemoryStream memoryStream = new MemoryStream();
			PortablePdbWriter writer = (PortablePdbWriter)new PortablePdbWriterProvider().GetSymbolWriter(module, memoryStream);
			return new EmbeddedPortablePdbWriter(memoryStream, writer);
		}

		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
	public sealed class EmbeddedPortablePdbWriter : ISymbolWriter, IDisposable
	{
		private readonly Stream stream;

		private readonly PortablePdbWriter writer;

		internal EmbeddedPortablePdbWriter(Stream stream, PortablePdbWriter writer)
		{
			this.stream = stream;
			this.writer = writer;
		}

		public ISymbolReaderProvider GetReaderProvider()
		{
			return new EmbeddedPortablePdbReaderProvider();
		}

		public void Write(MethodDebugInformation info)
		{
			writer.Write(info);
		}

		public ImageDebugHeader GetDebugHeader()
		{
			ImageDebugHeader debugHeader = writer.GetDebugHeader();
			ImageDebugDirectory directory = new ImageDebugDirectory
			{
				Type = ImageDebugType.EmbeddedPortablePdb,
				MajorVersion = 256,
				MinorVersion = 256
			};
			MemoryStream memoryStream = new MemoryStream();
			BinaryStreamWriter binaryStreamWriter = new BinaryStreamWriter(memoryStream);
			binaryStreamWriter.WriteByte(77);
			binaryStreamWriter.WriteByte(80);
			binaryStreamWriter.WriteByte(68);
			binaryStreamWriter.WriteByte(66);
			binaryStreamWriter.WriteInt32((int)stream.Length);
			stream.Position = 0L;
			using (DeflateStream destination = new DeflateStream(memoryStream, CompressionMode.Compress, leaveOpen: true))
			{
				stream.CopyTo(destination);
			}
			directory.SizeOfData = (int)memoryStream.Length;
			ImageDebugHeaderEntry[] array = new ImageDebugHeaderEntry[debugHeader.Entries.Length + 1];
			for (int i = 0; i < debugHeader.Entries.Length; i++)
			{
				array[i] = debugHeader.Entries[i];
			}
			array[^1] = new ImageDebugHeaderEntry(directory, memoryStream.ToArray());
			return new ImageDebugHeader(array);
		}

		public void Write()
		{
			writer.Write();
		}

		public void Dispose()
		{
			writer.Dispose();
		}
	}
	internal static class PdbGuidMapping
	{
		private static readonly Dictionary<Guid, DocumentLanguage> guid_language;

		private static readonly Dictionary<DocumentLanguage, Guid> language_guid;

		private static readonly Guid type_text;

		private static readonly Guid hash_md5;

		private static readonly Guid hash_sha1;

		private static readonly Guid hash_sha256;

		private static readonly Guid vendor_ms;

		static PdbGuidMapping()
		{
			guid_language = new Dictionary<Guid, DocumentLanguage>();
			language_guid = new Dictionary<DocumentLanguage, Guid>();
			type_text = new Guid("5a869d0b-6611-11d3-bd2a-0000f80849bd");
			hash_md5 = new Guid("406ea660-64cf-4c82-b6f0-42d48172a799");
			hash_sha1 = new Guid("ff1816ec-aa5e-4d10-87f7-6f4963833460");
			hash_sha256 = new Guid("8829d00f-11b8-4213-878b-770e8597ac16");
			vendor_ms = new Guid("994b45c4-e6e9-11d2-903f-00c04fa302a1");
			AddMapping(DocumentLanguage.C, new Guid("63a08714-fc37-11d2-904c-00c04fa302a1"));
			AddMapping(DocumentLanguage.Cpp, new Guid("3a12d0b7-c26c-11d0-b442-00a0244a1dd2"));
			AddMapping(DocumentLanguage.CSharp, new Guid("3f5162f8-07c6-11d3-9053-00c04fa302a1"));
			AddMapping(DocumentLanguage.Basic, new Guid("3a12d0b8-c26c-11d0-b442-00a0244a1dd2"));
			AddMapping(DocumentLanguage.Java, new Guid("3a12d0b4-c26c-11d0-b442-00a0244a1dd2"));
			AddMapping(DocumentLanguage.Cobol, new Guid("af046cd1-d0e1-11d2-977c-00a0c9b4d50c"));
			AddMapping(DocumentLanguage.Pascal, new Guid("af046cd2-d0e1-11d2-977c-00a0c9b4d50c"));
			AddMapping(DocumentLanguage.Cil, new Guid("af046cd3-d0e1-11d2-977c-00a0c9b4d50c"));
			AddMapping(DocumentLanguage.JScript, new Guid("3a12d0b6-c26c-11d0-b442-00a0244a1dd2"));
			AddMapping(DocumentLanguage.Smc, new Guid("0d9b9f7b-6611-11d3-bd2a-0000f80849bd"));
			AddMapping(DocumentLanguage.MCpp, new Guid("4b35fde8-07c6-11d3-9053-00c04fa302a1"));
			AddMapping(DocumentLanguage.FSharp, new Guid("ab4f38c9-b6e6-43ba-be3b-58080b2ccce3"));
		}

		private static void AddMapping(DocumentLanguage language, Guid guid)
		{
			guid_language.Add(guid, language);
			language_guid.Add(language, guid);
		}

		public static DocumentType ToType(this Guid guid)
		{
			if (guid == type_text)
			{
				return DocumentType.Text;
			}
			return DocumentType.Other;
		}

		public static Guid ToGuid(this DocumentType type)
		{
			if (type == DocumentType.Text)
			{
				return type_text;
			}
			return default(Guid);
		}

		public static DocumentHashAlgorithm ToHashAlgorithm(this Guid guid)
		{
			if (guid == hash_md5)
			{
				return DocumentHashAlgorithm.MD5;
			}
			if (guid == hash_sha1)
			{
				return DocumentHashAlgorithm.SHA1;
			}
			if (guid == hash_sha256)
			{
				return DocumentHashAlgorithm.SHA256;
			}
			return DocumentHashAlgorithm.None;
		}

		public static Guid ToGuid(this DocumentHashAlgorithm hash_algo)
		{
			return hash_algo switch
			{
				DocumentHashAlgorithm.MD5 => hash_md5, 
				DocumentHashAlgorithm.SHA1 => hash_sha1, 
				DocumentHashAlgorithm.SHA256 => hash_sha256, 
				_ => default(Guid), 
			};
		}

		public static DocumentLanguage ToLanguage(this Guid guid)
		{
			if (!guid_language.TryGetValue(guid, out var value))
			{
				return DocumentLanguage.Other;
			}
			return value;
		}

		public static Guid ToGuid(this DocumentLanguage language)
		{
			if (!language_guid.TryGetValue(language, out var value))
			{
				return default(Guid);
			}
			return value;
		}

		public static DocumentLanguageVendor ToVendor(this Guid guid)
		{
			if (guid == vendor_ms)
			{
				return DocumentLanguageVendor.Microsoft;
			}
			return DocumentLanguageVendor.Other;
		}

		public static Guid ToGuid(this DocumentLanguageVendor vendor)
		{
			if (vendor == DocumentLanguageVendor.Microsoft)
			{
				return vendor_ms;
			}
			return default(Guid);
		}
	}
	public sealed class SequencePoint
	{
		internal InstructionOffset offset;

		private Document document;

		private int start_line;

		private int start_column;

		private int end_line;

		private int end_column;

		public int Offset => offset.Offset;

		public int StartLine
		{
			get
			{
				return start_line;
			}
			set
			{
				start_line = value;
			}
		}

		public int StartColumn
		{
			get
			{
				return start_column;
			}
			set
			{
				start_column = value;
			}
		}

		public int EndLine
		{
			get
			{
				return end_line;
			}
			set
			{
				end_line = value;
			}
		}

		public int EndColumn
		{
			get
			{
				return end_column;
			}
			set
			{
				end_column = value;
			}
		}

		public bool IsHidden => start_line == 16707566 && start_line == end_line;

		public Document Document
		{
			get
			{
				return document;
			}
			set
			{
				document = value;
			}
		}

		internal SequencePoint(int offset, Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			this.offset = new InstructionOffset(offset);
			this.document = document;
		}

		public SequencePoint(Instruction instruction, Document document)
		{
			if (document == null)
			{
				throw new ArgumentNullException("document");
			}
			offset = new InstructionOffset(instruction);
			this.document = document;
		}
	}
	public struct ImageDebugDirectory
	{
		public const int Size = 28;

		public int Characteristics;

		public int TimeDateStamp;

		public short MajorVersion;

		public short MinorVersion;

		public ImageDebugType Type;

		public int SizeOfData;

		public int AddressOfRawData;

		public int PointerToRawData;
	}
	public enum ImageDebugType
	{
		CodeView = 2,
		Deterministic = 16,
		EmbeddedPortablePdb = 17,
		PdbChecksum = 19
	}
	public sealed class ImageDebugHeader
	{
		private readonly ImageDebugHeaderEntry[] entries;

		public bool HasEntries => !entries.IsNullOrEmpty();

		public ImageDebugHeaderEntry[] Entries => entries;

		public ImageDebugHeader(ImageDebugHeaderEntry[] entries)
		{
			this.entries = entries ?? Empty<ImageDebugHeaderEntry>.Array;
		}

		public ImageDebugHeader()
			: this(Empty<ImageDebugHeaderEntry>.Array)
		{
		}

		public ImageDebugHeader(ImageDebugHeaderEntry entry)
			: this(new ImageDebugHeaderEntry[1] { entry })
		{
		}
	}
	public sealed class ImageDebugHeaderEntry
	{
		private ImageDebugDirectory directory;

		private readonly byte[] data;

		public ImageDebugDirectory Directory
		{
			get
			{
				return directory;
			}
			internal set
			{
				directory = value;
			}
		}

		public byte[] Data => data;

		public ImageDebugHeaderEntry(ImageDebugDirectory directory, byte[] data)
		{
			this.directory = directory;
			this.data = data ?? Empty<byte>.Array;
		}
	}
	public sealed class ScopeDebugInformation : DebugInformation
	{
		internal InstructionOffset start;

		internal InstructionOffset end;

		internal ImportDebugInformation import;

		internal Collection<ScopeDebugInformation> scopes;

		internal Collection<VariableDebugInformation> variables;

		internal Collection<ConstantDebugInformation> constants;

		public InstructionOffset Start
		{
			get
			{
				return start;
			}
			set
			{
				start = value;
			}
		}

		public InstructionOffset End
		{
			get
			{
				return end;
			}
			set
			{
				end = value;
			}
		}

		public ImportDebugInformation Import
		{
			get
			{
				return import;
			}
			set
			{
				import = value;
			}
		}

		public bool HasScopes => !scopes.IsNullOrEmpty();

		public Collection<ScopeDebugInformation> Scopes
		{
			get
			{
				if (scopes == null)
				{
					Interlocked.CompareExchange(ref scopes, new Collection<ScopeDebugInformation>(), null);
				}
				return scopes;
			}
		}

		public bool HasVariables => !variables.IsNullOrEmpty();

		public Collection<VariableDebugInformation> Variables
		{
			get
			{
				if (variables == null)
				{
					Interlocked.CompareExchange(ref variables, new Collection<VariableDebugInformation>(), null);
				}
				return variables;
			}
		}

		public bool HasConstants => !constants.IsNullOrEmpty();

		public Collection<ConstantDebugInformation> Constants
		{
			get
			{
				if (constants == null)
				{
					Interlocked.CompareExchange(ref constants, new Collection<ConstantDebugInformation>(), null);
				}
				return constants;
			}
		}

		internal ScopeDebugInformation()
		{
			token = new MetadataToken(TokenType.LocalScope);
		}

		public ScopeDebugInformation(Instruction start, Instruction end)
			: this()
		{
			if (start == null)
			{
				throw new ArgumentNullException("start");
			}
			this.start = new InstructionOffset(start);
			if (end != null)
			{
				this.end = new InstructionOffset(end);
			}
		}

		public bool TryGetName(VariableDefinition variable, out string name)
		{
			name = null;
			if (variables == null || variables.Count == 0)
			{
				return false;
			}
			for (int i = 0; i < variables.Count; i++)
			{
				if (variables[i].Index == variable.Index)
				{
					name = variables[i].Name;
					return true;
				}
			}
			return false;
		}
	}
	public struct InstructionOffset
	{
		private readonly Instruction instruction;

		private readonly int? offset;

		public int Offset
		{
			get
			{
				if (instruction != null)
				{
					return instruction.Offset;
				}
				if (offset.HasValue)
				{
					return offset.Value;
				}
				throw new NotSupportedException();
			}
		}

		public bool IsEndOfMethod => instruction == null && !offset.HasValue;

		internal bool IsResolved => instruction != null || !offset.HasValue;

		internal Instruction ResolvedInstruction => instruction;

		public InstructionOffset(Instruction instruction)
		{
			if (instruction == null)
			{
				throw new ArgumentNullException("instruction");
			}
			this.instruction = instruction;
			offset = null;
		}

		public InstructionOffset(int offset)
		{
			instruction = null;
			this.offset = offset;
		}
	}
	[Flags]
	public enum VariableAttributes : ushort
	{
		None = 0,
		DebuggerHidden = 1
	}
	public struct VariableIndex
	{
		private readonly VariableDefinition variable;

		private readonly int? index;

		public int Index
		{
			get
			{
				if (variable != null)
				{
					return variable.Index;
				}
				if (index.HasValue)
				{
					return index.Value;
				}
				throw new NotSupportedException();
			}
		}

		internal bool IsResolved => variable != null;

		internal VariableDefinition ResolvedVariable => variable;

		public VariableIndex(VariableDefinition variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			this.variable = variable;
			index = null;
		}

		public VariableIndex(int index)
		{
			variable = null;
			this.index = index;
		}
	}
	public abstract class DebugInformation : ICustomDebugInformationProvider, IMetadataTokenProvider
	{
		internal MetadataToken token;

		internal Collection<CustomDebugInformation> custom_infos;

		public MetadataToken MetadataToken
		{
			get
			{
				return token;
			}
			set
			{
				token = value;
			}
		}

		public bool HasCustomDebugInformations => !custom_infos.IsNullOrEmpty();

		public Collection<CustomDebugInformation> CustomDebugInformations
		{
			get
			{
				if (custom_infos == null)
				{
					Interlocked.CompareExchange(ref custom_infos, new Collection<CustomDebugInformation>(), null);
				}
				return custom_infos;
			}
		}

		internal DebugInformation()
		{
		}
	}
	public sealed class VariableDebugInformation : DebugInformation
	{
		private string name;

		private ushort attributes;

		internal VariableIndex index;

		public int Index => index.Index;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public VariableAttributes Attributes
		{
			get
			{
				return (VariableAttributes)attributes;
			}
			set
			{
				attributes = (ushort)value;
			}
		}

		public bool IsDebuggerHidden
		{
			get
			{
				return attributes.GetAttributes(1);
			}
			set
			{
				attributes = attributes.SetAttributes(1, value);
			}
		}

		internal VariableDebugInformation(int index, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.index = new VariableIndex(index);
			this.name = name;
		}

		public VariableDebugInformation(VariableDefinition variable, string name)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			index = new VariableIndex(variable);
			this.name = name;
			token = new MetadataToken(TokenType.LocalVariable);
		}
	}
	public sealed class ConstantDebugInformation : DebugInformation
	{
		private string name;

		private TypeReference constant_type;

		private object value;

		public string Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public TypeReference ConstantType
		{
			get
			{
				return constant_type;
			}
			set
			{
				constant_type = value;
			}
		}

		public object Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
			}
		}

		public ConstantDebugInformation(string name, TypeReference constant_type, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.constant_type = constant_type;
			this.value = value;
			token = new MetadataToken(TokenType.LocalConstant);
		}
	}
	public enum ImportTargetKind : byte
	{
		ImportNamespace = 1,
		ImportNamespaceInAssembly,
		ImportType,
		ImportXmlNamespaceWithAlias,
		ImportAlias,
		DefineAssemblyAlias,
		DefineNamespaceAlias,
		DefineNamespaceInAssemblyAlias,
		DefineTypeAlias
	}
	public sealed class ImportTarget
	{
		internal ImportTargetKind kind;

		internal string @namespace;

		internal TypeReference type;

		internal AssemblyNameReference reference;

		internal string alias;

		public string Namespace
		{
			get
			{
				return @namespace;
			}
			set
			{
				@namespace = value;
			}
		}

		public TypeReference Type
		{
			get
			{
				return type;
			}
			set
			{
				type = value;
			}
		}

		public AssemblyNameReference AssemblyReference
		{
			get
			{
				return reference;
			}
			set
			{
				reference = value;
			}
		}

		public string Alias
		{
			get
			{
				return alias;
			}
			set
			{
				alias = value;
			}
		}

		public ImportTargetKind Kind
		{
			get
			{
				return kind;
			}
			set
			{
				kind = value;
			}
		}

		public ImportTarget(ImportTargetKind kind)
		{
			this.kind = kind;
		}
	}
	public sealed class ImportDebugInformation : DebugInformation
	{
		internal ImportDebugInformation parent;

		internal Collection<ImportTarget> targets;

		public bool HasTargets => !targets.IsNullOrEmpty();

		public Collection<ImportTarget> Targets
		{
			get
			{
				if (targets == null)
				{
					Interlocked.CompareExchange(ref targets, new Collection<ImportTarget>(), null);
				}
				return targets;
			}
		}

		public ImportDebugInformation Parent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
			}
		}

		public ImportDebugInformation()
		{
			token = new MetadataToken(TokenType.ImportScope);
		}
	}
	public interface ICustomDebugInformationProvider : IMetadataTokenProvider
	{
		bool HasCustomDebugInformations { get; }

		Collection<CustomDebugInformation> CustomDebugInformations { get; }
	}
	public enum CustomDebugInformationKind
	{
		Binary,
		StateMachineScope,
		DynamicVariable,
		DefaultNamespace,
		AsyncMethodBody,
		EmbeddedSource,
		SourceLink
	}
	public abstract class CustomDebugInformation : DebugInformation
	{
		private Guid identifier;

		public Guid Identifier => identifier;

		public abstract CustomDebugInformationKind Kind { get; }

		internal CustomDebugInformation(Guid identifier)
		{
			this.identifier = identifier;
			token = new MetadataToken(TokenType.CustomDebugInformation);
		}
	}
	public sealed class BinaryCustomDebugInformation : CustomDebugInformation
	{
		private byte[] data;

		public byte[] Data
		{
			get
			{
				return data;
			}
			set
			{
				data = value;
			}
		}

		public override CustomDebugInformationKind Kind => CustomDebugInformationKind.Binary;

		public BinaryCustomDebugInformation(Guid identifier, byte[] data)
			: base(identifier)
		{
			this.data = data;
		}
	}
	public sealed class AsyncMethodBodyDebugInformation : CustomDebugInformation
	{
		internal InstructionOffset catch_handler;

		internal Collection<InstructionOffset> yields;

		internal Collection<InstructionOffset> resumes;

		internal Collection<MethodDefinition> resume_methods;

		public static Guid KindIdentifier = new Guid("{54FD2AC5-E925-401A-9C2A-F94F171072F8}");

		public InstructionOffset CatchHandler
		{
			get
			{
				return catch_handler;
			}
			set
			{
				catch_handler = value;
			}
		}

		public Collection<InstructionOffset> Yields
		{
			get
			{
				if (yields == null)
				{
					Interlocked.CompareExchange(ref yields, new Collection<InstructionOffset>(), null);
				}
				return yields;
			}
		}

		public Collection<InstructionOffset> Resumes
		{
			get
			{
				if (resumes == null)
				{
					Interlocked.CompareExchange(ref resumes, new Collection<InstructionOffset>(), null);
				}
				return resumes;
			}
		}

		public Collection<MethodDefinition> ResumeMethods => resume_methods ?? (resume_methods = new Collection<MethodDefinition>());

		public override CustomDebugInformationKind Kind => CustomDebugInformationKind.AsyncMethodBody;

		internal AsyncMethodBodyDebugInformation(int catchHandler)
			: base(KindIdentifier)
		{
			catch_handler = new InstructionOffset(catchHandler);
		}

		public AsyncMethodBodyDebugInformation(Instruction catchHandler)
			: base(KindIdentifier)
		{
			catch_handler = new InstructionOffset(catchHandler);
		}

		public AsyncMethodBodyDebugInformation()
			: base(KindIdentifier)
		{
			catch_handler = new InstructionOffset(-1);
		}
	}
	public sealed class StateMachineScope
	{
		internal InstructionOffset start;

		internal InstructionOffset end;

		public InstructionOffset Start
		{
			get
			{
				return start;
			}
			set
			{
				start = value;
			}
		}

		public InstructionOffset End
		{
			get
			{
				return end;
			}
			set
			{
				end = value;
			}
		}

		internal StateMachineScope(int start, int end)
		{
			this.start = new InstructionOffset(start);
			this.end = new InstructionOffset(end);
		}

		public StateMachineScope(Instruction start, Instruction end)
		{
			this.start = new InstructionOffset(start);
			this.end = ((end != null) ? new InstructionOffset(end) : default(InstructionOffset));
		}
	}
	public sealed class StateMachineScopeDebugInformation : CustomDebugInformation
	{
		internal Collection<StateMachineScope> scopes;

		public static Guid KindIdentifier = new Guid("{6DA9A61E-F8C7-4874-BE62-68BC5630DF71}");

		public Collection<StateMachineScope> Scopes => scopes ?? (scopes = new Collection<StateMachineScope>());

		public override CustomDebugInformationKind Kind => CustomDebugInformationKind.StateMachineScope;

		public StateMachineScopeDebugInformation()
			: base(KindIdentifier)
		{
		}
	}
	public sealed class EmbeddedSourceDebugInformation : CustomDebugInformation
	{
		internal uint index;

		internal MetadataReader debug_reader;

		internal bool resolved;

		internal byte[] content;

		internal bool compress;

		public static Guid KindIdentifier = new Guid("{0E8A571B-6926-466E-B4AD-8AB04611F5FE}");

		public byte[] Content
		{
			get
			{
				if (!resolved)
				{
					Resolve();
				}
				return content;
			}
			set
			{
				content = value;
				resolved = true;
			}
		}

		public bool Compress
		{
			get
			{
				if (!resolved)
				{
					Resolve();
				}
				return compress;
			}
			set
			{
				compress = value;
				resolved = true;
			}
		}

		public override CustomDebugInformationKind Kind => CustomDebugInformationKind.EmbeddedSource;

		internal EmbeddedSourceDebugInformation(uint index, MetadataReader debug_reader)
			: base(KindIdentifier)
		{
			this.index = index;
			this.debug_reader = debug_reader;
		}

		public EmbeddedSourceDebugInformation(byte[] content, bool compress)
			: base(KindIdentifier)
		{
			resolved = true;
			this.content = content;
			this.compress = compress;
		}

		internal byte[] ReadRawEmbeddedSourceDebugInformation()
		{
			if (debug_reader == null)
			{
				throw new InvalidOperationException();
			}
			return debug_reader.ReadRawEmbeddedSourceDebugInformation(index);
		}

		private void Resolve()
		{
			if (!resolved)
			{
				if (debug_reader == null)
				{
					throw new InvalidOperationException();
				}
				Row<byte[], bool> row = debug_reader.ReadEmbeddedSourceDebugInformation(index);
				content = row.Col1;
				compress = row.Col2;
				resolved = true;
			}
		}
	}
	public sealed class SourceLinkDebugInformation : CustomDebugInformation
	{
		internal string content;

		public static Guid KindIdentifier = new Guid("{CC110556-A091-4D38-9FEC-25AB9A351A6A}");

		public string Content
		{
			get
			{
				return content;
			}
			set
			{
				content = value;
			}
		}

		public override CustomDebugInformationKind Kind => CustomDebugInformationKind.SourceLink;

		public SourceLinkDebugInformation(string content)
			: base(KindIdentifier)
		{
			this.content = content;
		}
	}
	public sealed class MethodDebugInformation : DebugInformation
	{
		internal MethodDefinition method;

		internal Collection<SequencePoint> sequence_points;

		internal ScopeDebugInformation scope;

		internal MethodDefinition kickoff_method;

		internal int code_size;

		internal MetadataToken local_var_token;

		public MethodDefinition Method => method;

		public bool HasSequencePoints => !sequence_points.IsNullOrEmpty();

		public Collection<SequencePoint> SequencePoints
		{
			get
			{
				if (sequence_points == null)
				{
					Interlocked.CompareExchange(ref sequence_points, new Collection<SequencePoint>(), null);
				}
				return sequence_points;
			}
		}

		public ScopeDebugInformation Scope
		{
			get
			{
				return scope;
			}
			set
			{
				scope = value;
			}
		}

		public MethodDefinition StateMachineKickOffMethod
		{
			get
			{
				return kickoff_method;
			}
			set
			{
				kickoff_method = value;
			}
		}

		internal MethodDebugInformation(MethodDefinition method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			this.method = method;
			token = new MetadataToken(TokenType.MethodDebugInformation, method.MetadataToken.RID);
		}

		public SequencePoint GetSequencePoint(Instruction instruction)
		{
			if (!HasSequencePoints)
			{
				return null;
			}
			for (int i = 0; i < sequence_points.Count; i++)
			{
				if (sequence_points[i].Offset == instruction.Offset)
				{
					return sequence_points[i];
				}
			}
			return null;
		}

		public IDictionary<Instruction, SequencePoint> GetSequencePointMapping()
		{
			Dictionary<Instruction, SequencePoint> dictionary = new Dictionary<Instruction, SequencePoint>();
			if (!HasSequencePoints || !method.HasBody)
			{
				return dictionary;
			}
			Dictionary<int, SequencePoint> dictionary2 = new Dictionary<int, SequencePoint>(sequence_points.Count);
			for (int i = 0; i < sequence_points.Count; i++)
			{
				if (!dictionary2.ContainsKey(sequence_points[i].Offset))
				{
					dictionary2.Add(sequence_points[i].Offset, sequence_points[i]);
				}
			}
			Collection<Instruction> instructions = method.Body.Instructions;
			for (int j = 0; j < instructions.Count; j++)
			{
				if (dictionary2.TryGetValue(instructions[j].Offset, out var value))
				{
					dictionary.Add(instructions[j], value);
				}
			}
			return dictionary;
		}

		public IEnumerable<ScopeDebugInformation> GetScopes()
		{
			if (scope == null)
			{
				return Empty<ScopeDebugInformation>.Array;
			}
			return GetScopes(new ScopeDebugInformation[1] { scope });
		}

		private static IEnumerable<ScopeDebugInformation> GetScopes(IList<ScopeDebugInformation> scopes)
		{
			for (int i = 0; i < scopes.Count; i++)
			{
				ScopeDebugInformation scope = scopes[i];
				yield return scope;
				if (!scope.HasScopes)
				{
					continue;
				}
				foreach (ScopeDebugInformation scope2 in GetScopes(scope.Scopes))
				{
					yield return scope2;
				}
			}
		}

		public bool TryGetName(VariableDefinition variable, out string name)
		{
			name = null;
			bool flag = false;
			string text = "";
			foreach (ScopeDebugInformation scope in GetScopes())
			{
				if (scope.TryGetName(variable, out var name2))
				{
					if (!flag)
					{
						flag = true;
						text = name2;
					}
					else if (text != name2)
					{
						return false;
					}
				}
			}
			name = text;
			return flag;
		}
	}
	public interface ISymbolReader : IDisposable
	{
		ISymbolWriterProvider GetWriterProvider();

		bool ProcessDebugHeader(ImageDebugHeader header);

		MethodDebugInformation Read(MethodDefinition method);
	}
	public interface ISymbolReaderProvider
	{
		ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName);

		ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream);
	}
	[Serializable]
	public sealed class SymbolsNotFoundException : FileNotFoundException
	{
		public SymbolsNotFoundException(string message)
			: base(message)
		{
		}

		private SymbolsNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	[Serializable]
	public sealed class SymbolsNotMatchingException : InvalidOperationException
	{
		public SymbolsNotMatchingException(string message)
			: base(message)
		{
		}

		private SymbolsNotMatchingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
	public class DefaultSymbolReaderProvider : ISymbolReaderProvider
	{
		private readonly bool throw_if_no_symbol;

		public DefaultSymbolReaderProvider()
			: this(throwIfNoSymbol: true)
		{
		}

		public DefaultSymbolReaderProvider(bool throwIfNoSymbol)
		{
			throw_if_no_symbol = throwIfNoSymbol;
		}

		public ISymbolReader GetSymbolReader(ModuleDefinition module, string fileName)
		{
			if (module.Image.HasDebugTables())
			{
				return null;
			}
			if (module.HasDebugHeader)
			{
				ImageDebugHeader debugHeader = module.GetDebugHeader();
				ImageDebugHeaderEntry embeddedPortablePdbEntry = debugHeader.GetEmbeddedPortablePdbEntry();
				if (embeddedPortablePdbEntry != null)
				{
					return new EmbeddedPortablePdbReaderProvider().GetSymbolReader(module, fileName);
				}
			}
			string pdbFileName = Mixin.GetPdbFileName(fileName);
			if (File.Exists(pdbFileName))
			{
				if (Mixin.IsPortablePdb(Mixin.GetPdbFileName(fileName)))
				{
					return new PortablePdbReaderProvider().GetSymbolReader(module, fileName);
				}
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.NativePdb).GetSymbolReader(module, fileName);
				}
				catch (Exception)
				{
				}
			}
			string mdbFileName = Mixin.GetMdbFileName(fileName);
			if (File.Exists(mdbFileName))
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.Mdb).GetSymbolReader(module, fileName);
				}
				catch (Exception)
				{
				}
			}
			if (throw_if_no_symbol)
			{
				throw new SymbolsNotFoundException($"No symbol found for file: {fileName}");
			}
			return null;
		}

		public ISymbolReader GetSymbolReader(ModuleDefinition module, Stream symbolStream)
		{
			if (module.Image.HasDebugTables())
			{
				return null;
			}
			if (module.HasDebugHeader)
			{
				ImageDebugHeader debugHeader = module.GetDebugHeader();
				ImageDebugHeaderEntry embeddedPortablePdbEntry = debugHeader.GetEmbeddedPortablePdbEntry();
				if (embeddedPortablePdbEntry != null)
				{
					return new EmbeddedPortablePdbReaderProvider().GetSymbolReader(module, "");
				}
			}
			Mixin.CheckStream(symbolStream);
			Mixin.CheckReadSeek(symbolStream);
			long position = symbolStream.Position;
			BinaryStreamReader binaryStreamReader = new BinaryStreamReader(symbolStream);
			int num = binaryStreamReader.ReadInt32();
			symbolStream.Position = position;
			if (num == 1112167234)
			{
				return new PortablePdbReaderProvider().GetSymbolReader(module, symbolStream);
			}
			byte[] array = binaryStreamReader.ReadBytes("Microsoft C/C++ MSF 7.00".Length);
			symbolStream.Position = position;
			bool flag = true;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != (byte)"Microsoft C/C++ MSF 7.00"[i])
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.NativePdb).GetSymbolReader(module, symbolStream);
				}
				catch (Exception)
				{
				}
			}
			long num2 = binaryStreamReader.ReadInt64();
			symbolStream.Position = position;
			if (num2 == 5037318119232611860L)
			{
				try
				{
					return SymbolProvider.GetReaderProvider(SymbolKind.Mdb).GetSymbolReader(module, symbolStream);
				}
				catch (Exception)
				{
				}
			}
			if (throw_if_no_symbol)
			{
				throw new SymbolsNotFoundException($"No symbols found in stream");
			}
			return null;
		}
	}
	internal enum SymbolKind
	{
		NativePdb,
		PortablePdb,
		EmbeddedPortablePdb,
		Mdb
	}
	internal static class SymbolProvider
	{
		private static AssemblyName GetSymbolAssemblyName(SymbolKind kind)
		{
			if (kind == SymbolKind.PortablePdb)
			{
				throw new ArgumentException();
			}
			string symbolNamespace = GetSymbolNamespace(kind);
			AssemblyName name = typeof(SymbolProvider).Assembly.GetName();
			AssemblyName assemblyName = new AssemblyName
			{
				Name = name.Name + "." + symbolNamespace,
				Version = name.Version,
				CultureInfo = name.CultureInfo
			};
			assemblyName.SetPublicKeyToken(name.GetPublicKeyToken());
			return assemblyName;
		}

		private static Type GetSymbolType(SymbolKind kind, string fullname)
		{
			Type type = Type.GetType(fullname);
			if (type != null)
			{
				return type;
			}
			AssemblyName symbolAssemblyName = GetSymbolAssemblyName(kind);
			type = Type.GetType(fullname + ", " + symbolAssemblyName.FullName);
			if (type != null)
			{
				return type;
			}
			try
			{
				Assembly assembly = Assembly.Load(symbolAssemblyName);
				if (assembly != null)
				{
					return assembly.GetType(fullname);
				}
			}
			catch (FileNotFoundException)
			{
			}
			catch (FileLoadException)
			{
			}
			return null;
		}

		public static ISymbolReaderProvider GetReaderProvider(SymbolKind kind)
		{
			switch (kind)
			{
			case SymbolKind.PortablePdb:
				return new PortablePdbReaderProvider();
			case SymbolKind.EmbeddedPortablePdb:
				return new EmbeddedPortablePdbReaderProvider();
			default:
			{
				string symbolTypeName = GetSymbolTypeName(kind, "ReaderProvider");
				Type symbolType = GetSymbolType(kind, symbolTypeName);
				if (symbolType == null)
				{
					throw new TypeLoadException("Could not find symbol provider type " + symbolTypeName);
				}
				return (ISymbolReaderProvider)Activator.CreateInstance(symbolType);
			}
			}
		}

		private static string GetSymbolTypeName(SymbolKind kind, string name)
		{
			return "Mono.Cecil." + GetSymbolNamespace(kind) + "." + kind.ToString() + name;
		}

		private static string GetSymbolNamespace(SymbolKind kind)
		{
			if (kind == SymbolKind.PortablePdb || kind == SymbolKind.EmbeddedPortablePdb)
			{
				return "Cil";
			}
			return kind switch
			{
				SymbolKind.NativePdb => "Pdb", 
				SymbolKind.Mdb => "Mdb", 
				_ => throw new ArgumentException(), 
			};
		}
	}
	public interface ISymbolWriter : IDisposable
	{
		ISymbolReaderProvider GetReaderProvider();

		ImageDebugHeader GetDebugHeader();

		void Write(MethodDebugInformation info);

		void Write();
	}
	public interface ISymbolWriterProvider
	{
		ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName);

		ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream);
	}
	public class DefaultSymbolWriterProvider : ISymbolWriterProvider
	{
		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, string fileName)
		{
			ISymbolReader symbolReader = module.SymbolReader;
			if (symbolReader == null)
			{
				throw new InvalidOperationException();
			}
			if (module.Image != null && module.Image.HasDebugTables())
			{
				return null;
			}
			return symbolReader.GetWriterProvider().GetSymbolWriter(module, fileName);
		}

		public ISymbolWriter GetSymbolWriter(ModuleDefinition module, Stream symbolStream)
		{
			throw new NotSupportedException();
		}
	}
	public sealed class VariableDefinition : VariableReference
	{
		public bool IsPinned => variable_type.IsPinned;

		public VariableDefinition(TypeReference variableType)
			: base(variableType)
		{
		}

		public override VariableDefinition Resolve()
		{
			return this;
		}
	}
	public abstract class VariableReference
	{
		internal int index = -1;

		protected TypeReference variable_type;

		public TypeReference VariableType
		{
			get
			{
				return variable_type;
			}
			set
			{
				variable_type = value;
			}
		}

		public int Index => index;

		internal VariableReference(TypeReference variable_type)
		{
			this.variable_type = variable_type;
		}

		public abstract VariableDefinition Resolve();

		public override string ToString()
		{
			if (index >= 0)
			{
				return "V_" + index;
			}
			return string.Empty;
		}
	}
}
