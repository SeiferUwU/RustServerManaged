using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Facepunch.NativeMeshSimplification;

public class NativeMeshSimplifier : IDisposable
{
	internal struct Triangle
	{
		public int3 vIndex;

		public float3 n;

		public float4 err;

		public bool deleted;

		public bool dirty;
	}

	internal struct Vertex
	{
		public float3 p;

		public SymmetricMatrix q;

		public int tStart;

		public int tCount;

		public bool border;
	}

	internal struct Ref
	{
		public int tId;

		public int tVertex;
	}

	internal struct SymmetricMatrix
	{
		private unsafe fixed float m[10];

		public unsafe float this[int i]
		{
			get
			{
				return m[i];
			}
			set
			{
				m[i] = value;
			}
		}

		private unsafe SymmetricMatrix(float m11, float m12, float m13, float m14, float m22, float m23, float m24, float m33, float m34, float m44)
		{
			m[0] = m11;
			m[1] = m12;
			m[2] = m13;
			m[3] = m14;
			m[4] = m22;
			m[5] = m23;
			m[6] = m24;
			m[7] = m33;
			m[8] = m34;
			m[9] = m44;
		}

		public static SymmetricMatrix Plane(float a, float b, float c, float d)
		{
			return new SymmetricMatrix
			{
				[0] = a * a,
				[1] = a * b,
				[2] = a * c,
				[3] = a * d,
				[4] = b * b,
				[5] = b * c,
				[6] = b * d,
				[7] = c * c,
				[8] = c * d,
				[9] = d * d
			};
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe float Det(int a11, int a12, int a13, int a21, int a22, int a23, int a31, int a32, int a33)
		{
			return math.determinant(new float3x3(m[a11], m[a12], m[a13], m[a21], m[a22], m[a23], m[a31], m[a32], m[a33]));
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SymmetricMatrix operator +(SymmetricMatrix m, SymmetricMatrix n)
		{
			return new SymmetricMatrix(m[0] + n[0], m[1] + n[1], m[2] + n[2], m[3] + n[3], m[4] + n[4], m[5] + n[5], m[6] + n[6], m[7] + n[7], m[8] + n[8], m[9] + n[9]);
		}

		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void ArrayBoundsCheck(int i)
		{
			if (i < 0 || i > 9)
			{
				throw new IndexOutOfRangeException();
			}
		}
	}

	private NativeList<Vertex> _vertices;

	private NativeList<Triangle> _triangles;

	private NativeList<Ref> _refs;

	public NativeMeshSimplifier()
	{
		_vertices = new NativeList<Vertex>(Allocator.Persistent);
		_triangles = new NativeList<Triangle>(Allocator.Persistent);
		_refs = new NativeList<Ref>(Allocator.Persistent);
	}

	public void Dispose()
	{
		_vertices.Dispose();
		_triangles.Dispose();
		_refs.Dispose();
	}

	public JobHandle ScheduleMeshSimplify(float reductionModifier, NativeList<float3> verticesIn, NativeList<int> indicesIn, NativeList<float3> verticesOut, NativeList<int> indicesOut, JobHandle inputDeps)
	{
		inputDeps = new Facepunch.NativeMeshSimplification.PopulateArraysJob
		{
			VerticesIn = verticesIn,
			VerticesOut = _vertices,
			IndicesIn = indicesIn,
			TrianglesOut = _triangles
		}.Schedule(inputDeps);
		inputDeps = new Facepunch.NativeMeshSimplification.SimplifyMeshJob
		{
			MaxIterations = 128,
			Aggressiveness = 7,
			ReductionTarget = reductionModifier,
			Vertices = _vertices,
			Triangles = _triangles,
			Refs = _refs
		}.Schedule(inputDeps);
		inputDeps = new Facepunch.NativeMeshSimplification.CopyBackJob
		{
			DstVertices = verticesOut,
			DstIndices = indicesOut,
			SrcVertices = _vertices,
			SrcTriangles = _triangles
		}.Schedule(inputDeps);
		return inputDeps;
	}
}
