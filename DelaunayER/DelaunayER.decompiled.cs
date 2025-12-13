using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[assembly: AssemblyTitle("EasyRoads")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Raoul")]
[assembly: AssemblyProduct("EasyRoads")]
[assembly: AssemblyCopyright("Copyright © Raoul 2009")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]
[assembly: Guid("82025ffa-a302-445c-a95f-98b5746502e5")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: AssemblyVersion("1.0.0.0")]
public class EdgeER
{
	public PointER StartPoint;

	public PointER EndPoint;

	public EdgeER(PointER startPoint, PointER endPoint)
	{
		StartPoint = startPoint;
		EndPoint = endPoint;
	}

	public override int GetHashCode()
	{
		return StartPoint.GetHashCode() ^ EndPoint.GetHashCode();
	}

	public override bool Equals(object obj)
	{
		return this == (EdgeER)obj;
	}

	public static bool operator ==(EdgeER left, EdgeER right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		return (left.StartPoint == right.StartPoint && left.EndPoint == right.EndPoint) || (left.StartPoint == right.EndPoint && left.EndPoint == right.StartPoint);
	}

	public static bool operator !=(EdgeER left, EdgeER right)
	{
		return left != right;
	}
}
public class PointER
{
	public float x;

	public float y;

	public float z;

	public PointER(float x, float y, float z)
	{
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public override int GetHashCode()
	{
		int hashCode = x.ToString().GetHashCode();
		int hashCode2 = y.ToString().GetHashCode();
		int hashCode3 = z.ToString().GetHashCode();
		return hashCode ^ hashCode2 ^ hashCode3;
	}

	public override bool Equals(object obj)
	{
		return this == (PointER)obj;
	}

	public static bool operator ==(PointER left, PointER right)
	{
		if ((object)left == right)
		{
			return true;
		}
		if ((object)left == null || (object)right == null)
		{
			return false;
		}
		if (left.x != right.x)
		{
			return false;
		}
		if (left.y != right.y)
		{
			return false;
		}
		return true;
	}

	public static bool operator !=(PointER left, PointER right)
	{
		return !(left == right);
	}
}
public class delaunayER
{
	private void Start()
	{
		List<Vector3> list = new List<Vector3>();
		List<PointER> list2 = new List<PointER>();
		for (int i = 0; i < 25; i++)
		{
			for (int j = 0; j < 25; j++)
			{
				Vector3 item = new Vector3(UnityEngine.Random.value * 25f, 0f, UnityEngine.Random.value * 25f);
				list.Add(item);
				list2.Add(new PointER(item.x, item.z, 0f));
			}
		}
		List<TriangleER> list3 = Triangulate(list2);
		List<int> list4 = new List<int>();
		for (int i = 0; i < list3.Count; i++)
		{
			list4.Add(FindVertice(new Vector3(list3[i].Vertex1.x, list3[i].Vertex1.z, list3[i].Vertex1.y), list));
			list4.Add(FindVertice(new Vector3(list3[i].Vertex3.x, list3[i].Vertex3.z, list3[i].Vertex3.y), list));
			list4.Add(FindVertice(new Vector3(list3[i].Vertex2.x, list3[i].Vertex2.z, list3[i].Vertex2.y), list));
		}
		Mesh mesh = new Mesh();
		mesh.vertices = list.ToArray();
		mesh.triangles = list4.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		Debug.Log("Done 2 " + list4.Count);
	}

	private void Update()
	{
	}

	public static int FindVertice(Vector3 v, List<Vector3> vecs)
	{
		for (int i = 0; i < vecs.Count; i++)
		{
			if (vecs[i].x == v.x && vecs[i].z == v.z)
			{
				return i;
			}
		}
		return 0;
	}

	public static List<TriangleER> Triangulate(List<PointER> triangulationPoints)
	{
		if (triangulationPoints.Count < 3)
		{
			throw new ArgumentException("Can not triangulate less than three vertices!");
		}
		List<TriangleER> list = new List<TriangleER>();
		TriangleER triangleER = SuperTriangle(triangulationPoints);
		list.Add(triangleER);
		for (int i = 0; i < triangulationPoints.Count; i++)
		{
			List<EdgeER> list2 = new List<EdgeER>();
			for (int num = list.Count - 1; num >= 0; num--)
			{
				TriangleER triangleER2 = list[num];
				if (triangleER2.ContainsInCircumcircle(triangulationPoints[i]) > 0.0)
				{
					list2.Add(new EdgeER(triangleER2.Vertex1, triangleER2.Vertex2));
					list2.Add(new EdgeER(triangleER2.Vertex2, triangleER2.Vertex3));
					list2.Add(new EdgeER(triangleER2.Vertex3, triangleER2.Vertex1));
					list.RemoveAt(num);
				}
			}
			for (int num = list2.Count - 2; num >= 0; num--)
			{
				for (int num2 = list2.Count - 1; num2 >= num + 1; num2--)
				{
					if (list2[num] == list2[num2])
					{
						list2.RemoveAt(num2);
						list2.RemoveAt(num);
						num2--;
					}
				}
			}
			for (int num = 0; num < list2.Count; num++)
			{
				list.Add(new TriangleER(list2[num].StartPoint, list2[num].EndPoint, triangulationPoints[i]));
			}
		}
		for (int i = list.Count - 1; i >= 0; i--)
		{
			if (list[i].SharesVertexWith(triangleER))
			{
				list.RemoveAt(i);
			}
		}
		return list;
	}

	public static TriangleER SuperTriangle(List<PointER> triangulationPoints)
	{
		float num = triangulationPoints[0].x;
		for (int i = 1; i < triangulationPoints.Count; i++)
		{
			float num2 = Mathf.Abs(triangulationPoints[i].x);
			float num3 = Mathf.Abs(triangulationPoints[i].y);
			if (num2 > num)
			{
				num = num2;
			}
			if (num3 > num)
			{
				num = num3;
			}
		}
		PointER vertex = new PointER(10f * num, 0f, 0f);
		PointER vertex2 = new PointER(0f, 10f * num, 0f);
		PointER vertex3 = new PointER(-10f * num, -10f * num, 0f);
		return new TriangleER(vertex, vertex2, vertex3);
	}
}
public class TriangleER
{
	public PointER Vertex1;

	public PointER Vertex2;

	public PointER Vertex3;

	public TriangleER(PointER vertex1, PointER vertex2, PointER vertex3)
	{
		Vertex1 = vertex1;
		Vertex2 = vertex2;
		Vertex3 = vertex3;
	}

	public double ContainsInCircumcircle(PointER point)
	{
		double num = Vertex1.x - point.x;
		double num2 = Vertex1.y - point.y;
		double num3 = Vertex2.x - point.x;
		double num4 = Vertex2.y - point.y;
		double num5 = Vertex3.x - point.x;
		double num6 = Vertex3.y - point.y;
		double num7 = num * num4 - num3 * num2;
		double num8 = num3 * num6 - num5 * num4;
		double num9 = num5 * num2 - num * num6;
		double num10 = num * num + num2 * num2;
		double num11 = num3 * num3 + num4 * num4;
		double num12 = num5 * num5 + num6 * num6;
		return num10 * num8 + num11 * num9 + num12 * num7;
	}

	public bool SharesVertexWith(TriangleER triangle)
	{
		if (Vertex1.x == triangle.Vertex1.x && Vertex1.y == triangle.Vertex1.y)
		{
			return true;
		}
		if (Vertex1.x == triangle.Vertex2.x && Vertex1.y == triangle.Vertex2.y)
		{
			return true;
		}
		if (Vertex1.x == triangle.Vertex3.x && Vertex1.y == triangle.Vertex3.y)
		{
			return true;
		}
		if (Vertex2.x == triangle.Vertex1.x && Vertex2.y == triangle.Vertex1.y)
		{
			return true;
		}
		if (Vertex2.x == triangle.Vertex2.x && Vertex2.y == triangle.Vertex2.y)
		{
			return true;
		}
		if (Vertex2.x == triangle.Vertex3.x && Vertex2.y == triangle.Vertex3.y)
		{
			return true;
		}
		if (Vertex3.x == triangle.Vertex1.x && Vertex3.y == triangle.Vertex1.y)
		{
			return true;
		}
		if (Vertex3.x == triangle.Vertex2.x && Vertex3.y == triangle.Vertex2.y)
		{
			return true;
		}
		if (Vertex3.x == triangle.Vertex3.x && Vertex3.y == triangle.Vertex3.y)
		{
			return true;
		}
		return false;
	}
}
