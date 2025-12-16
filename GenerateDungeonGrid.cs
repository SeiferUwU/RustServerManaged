using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerateDungeonGrid : ProceduralComponent
{
	private class PathNode
	{
		public MonumentInfo monument;

		public PathFinder.Node node;
	}

	private class PathSegment
	{
		public PathFinder.Node start;

		public PathFinder.Node end;
	}

	private class PathLink
	{
		public PathLinkSide downwards;

		public PathLinkSide upwards;
	}

	private class PathLinkSide
	{
		public PathLinkSegment origin;

		public List<PathLinkSegment> segments;

		public PathLinkSegment prevSegment
		{
			get
			{
				if (segments.Count <= 0)
				{
					return origin;
				}
				return segments[segments.Count - 1];
			}
		}
	}

	private class PathLinkSegment
	{
		public Vector3 position;

		public Quaternion rotation;

		public Vector3 scale;

		public Prefab<DungeonGridLink> prefab;

		public DungeonGridLink link;

		public Transform downSocket => link.DownSocket;

		public Transform upSocket => link.UpSocket;

		public DungeonGridLinkType downType => link.DownType;

		public DungeonGridLinkType upType => link.UpType;
	}

	private struct PrefabReplacement
	{
		public Vector2i gridPosition;

		public Vector3 worldPosition;

		public int distance;

		public Prefab<DungeonGridCell> prefab;
	}

	public string TunnelFolder = string.Empty;

	public string StationFolder = string.Empty;

	public string UpwardsFolder = string.Empty;

	public string TransitionFolder = string.Empty;

	public string LinkFolder = string.Empty;

	public InfrastructureType ConnectionType = InfrastructureType.Tunnel;

	public int CellSize = 216;

	public float LinkHeight = 1.5f;

	public float LinkRadius = 3f;

	public float LinkTransition = 9f;

	private const int MaxDepth = 100000;

	public override bool RunOnCache => true;

	public override void Process(uint seed)
	{
		if (World.Cached)
		{
			return;
		}
		if (World.Networked)
		{
			World.Spawn("Dungeon");
		}
		else
		{
			if (ConnectionType == InfrastructureType.Tunnel && !World.Config.BelowGroundRails)
			{
				return;
			}
			Prefab<DungeonGridCell>[] array = Prefab.Load<DungeonGridCell>("assets/bundled/prefabs/autospawn/" + TunnelFolder, null, null, useProbabilities: true, useWorldConfig: false);
			if (array == null || array.Length == 0)
			{
				return;
			}
			Prefab<DungeonGridCell>[] array2 = Prefab.Load<DungeonGridCell>("assets/bundled/prefabs/autospawn/" + StationFolder, null, null, useProbabilities: true, useWorldConfig: false);
			if (array2 == null || array2.Length == 0)
			{
				return;
			}
			Prefab<DungeonGridCell>[] array3 = Prefab.Load<DungeonGridCell>("assets/bundled/prefabs/autospawn/" + UpwardsFolder, null, null, useProbabilities: true, useWorldConfig: false);
			if (array3 == null)
			{
				return;
			}
			Prefab<DungeonGridCell>[] array4 = Prefab.Load<DungeonGridCell>("assets/bundled/prefabs/autospawn/" + TransitionFolder, null, null, useProbabilities: true, useWorldConfig: false);
			if (array4 == null)
			{
				return;
			}
			Prefab<DungeonGridLink>[] array5 = Prefab.Load<DungeonGridLink>("assets/bundled/prefabs/autospawn/" + LinkFolder, null, null, useProbabilities: true, useWorldConfig: false);
			if (array5 == null)
			{
				return;
			}
			array5 = array5.OrderByDescending((Prefab<DungeonGridLink> x) => x.Component.Priority).ToArray();
			List<DungeonGridInfo> list = (TerrainMeta.Path ? TerrainMeta.Path.DungeonGridEntrances : null);
			WorldSpaceGrid<Prefab<DungeonGridCell>> worldSpaceGrid = new WorldSpaceGrid<Prefab<DungeonGridCell>>(TerrainMeta.Size.x, CellSize, WorldSpaceGrid.RoundingMode.Down);
			int[,] array6 = new int[worldSpaceGrid.CellCount, worldSpaceGrid.CellCount];
			DungeonGridConnectionHash[,] hashmap = new DungeonGridConnectionHash[worldSpaceGrid.CellCount, worldSpaceGrid.CellCount];
			PathFinder pathFinder = new PathFinder(array6, diagonals: false);
			int cellCount = worldSpaceGrid.CellCount;
			int num = 0;
			int num2 = worldSpaceGrid.CellCount - 1;
			for (int num3 = 0; num3 < cellCount; num3++)
			{
				for (int num4 = 0; num4 < cellCount; num4++)
				{
					array6[num4, num3] = 1;
				}
			}
			List<PathSegment> list2 = new List<PathSegment>();
			List<PathLink> list3 = new List<PathLink>();
			List<PathNode> list4 = new List<PathNode>();
			List<PathNode> unconnectedNodeList = new List<PathNode>();
			List<PathNode> secondaryNodeList = new List<PathNode>();
			List<PathFinder.Point> list5 = new List<PathFinder.Point>();
			List<PathFinder.Point> list6 = new List<PathFinder.Point>();
			List<PathFinder.Point> list7 = new List<PathFinder.Point>();
			foreach (DungeonGridInfo item2 in list)
			{
				DungeonGridInfo entrance = item2;
				TerrainPathConnect[] componentsInChildren = entrance.GetComponentsInChildren<TerrainPathConnect>(includeInactive: true);
				foreach (TerrainPathConnect terrainPathConnect in componentsInChildren)
				{
					if (terrainPathConnect.Type != ConnectionType)
					{
						continue;
					}
					Vector2i cellPos = worldSpaceGrid.WorldToGridCoords(terrainPathConnect.transform.position);
					if (array6[cellPos.x, cellPos.y] == int.MaxValue)
					{
						continue;
					}
					PathFinder.Node stationNode = pathFinder.FindClosestWalkable(new PathFinder.Point(cellPos.x, cellPos.y), 1);
					if (stationNode == null)
					{
						continue;
					}
					Prefab<DungeonGridCell> prefab = ((cellPos.x > num) ? worldSpaceGrid[cellPos.x - 1, cellPos.y] : null);
					Prefab<DungeonGridCell> prefab2 = ((cellPos.x < num2) ? worldSpaceGrid[cellPos.x + 1, cellPos.y] : null);
					Prefab<DungeonGridCell> prefab3 = ((cellPos.y > num) ? worldSpaceGrid[cellPos.x, cellPos.y - 1] : null);
					Prefab<DungeonGridCell> obj = ((cellPos.y < num2) ? worldSpaceGrid[cellPos.x, cellPos.y + 1] : null);
					DungeonGridConnectionType dungeonGridConnectionType = prefab?.Component.East ?? DungeonGridConnectionType.None;
					DungeonGridConnectionType dungeonGridConnectionType2 = prefab2?.Component.West ?? DungeonGridConnectionType.None;
					DungeonGridConnectionType dungeonGridConnectionType3 = prefab3?.Component.North ?? DungeonGridConnectionType.None;
					DungeonGridConnectionType dungeonGridConnectionType4 = obj?.Component.South ?? DungeonGridConnectionType.None;
					bool flag = prefab != null || cellPos.x <= num;
					bool flag2 = prefab2 != null || cellPos.x >= num2;
					bool flag3 = prefab3 != null || cellPos.y <= num;
					bool flag4 = obj != null || cellPos.y >= num2;
					Prefab<DungeonGridCell> prefab4 = null;
					float num6 = float.MaxValue;
					ArrayEx.Shuffle(array2, ref seed);
					Prefab<DungeonGridCell>[] array7 = array2;
					foreach (Prefab<DungeonGridCell> prefab5 in array7)
					{
						if ((flag && prefab5.Component.West != dungeonGridConnectionType) || (flag2 && prefab5.Component.East != dungeonGridConnectionType2) || (flag3 && prefab5.Component.South != dungeonGridConnectionType3) || (flag4 && prefab5.Component.North != dungeonGridConnectionType4))
						{
							continue;
						}
						DungeonVolume componentInChildren = prefab5.Object.GetComponentInChildren<DungeonVolume>();
						DungeonVolume componentInChildren2 = entrance.GetComponentInChildren<DungeonVolume>();
						OBB bounds = componentInChildren.GetBounds(worldSpaceGrid.GridToWorldCoords(cellPos), Quaternion.identity);
						OBB bounds2 = componentInChildren2.GetBounds(entrance.transform.position, Quaternion.identity);
						if (!bounds.Intersects2D(bounds2))
						{
							DungeonGridLink componentInChildren3 = prefab5.Object.GetComponentInChildren<DungeonGridLink>();
							Vector3 vector = worldSpaceGrid.GridToWorldCoords(new Vector2i(cellPos.x, cellPos.y)) + componentInChildren3.UpSocket.localPosition;
							float num8 = (terrainPathConnect.transform.position - vector).Magnitude2D();
							if (!(num6 < num8))
							{
								prefab4 = prefab5;
								num6 = num8;
							}
						}
					}
					bool isStartPoint;
					if (prefab4 != null)
					{
						worldSpaceGrid[cellPos.x, cellPos.y] = prefab4;
						array6[cellPos.x, cellPos.y] = int.MaxValue;
						isStartPoint = secondaryNodeList.Count == 0;
						secondaryNodeList.RemoveAll((PathNode x) => x.node.point == stationNode.point);
						unconnectedNodeList.RemoveAll((PathNode x) => x.node.point == stationNode.point);
						if (prefab4.Component.West != DungeonGridConnectionType.None)
						{
							AddNode(cellPos.x - 1, cellPos.y);
						}
						if (prefab4.Component.East != DungeonGridConnectionType.None)
						{
							AddNode(cellPos.x + 1, cellPos.y);
						}
						if (prefab4.Component.South != DungeonGridConnectionType.None)
						{
							AddNode(cellPos.x, cellPos.y - 1);
						}
						if (prefab4.Component.North != DungeonGridConnectionType.None)
						{
							AddNode(cellPos.x, cellPos.y + 1);
						}
						PathLink pathLink = new PathLink();
						DungeonGridLink componentInChildren4 = entrance.gameObject.GetComponentInChildren<DungeonGridLink>();
						Vector3 position = entrance.transform.position;
						Vector3 eulerAngles = entrance.transform.rotation.eulerAngles;
						DungeonGridLink componentInChildren5 = prefab4.Object.GetComponentInChildren<DungeonGridLink>();
						Vector3 position2 = worldSpaceGrid.GridToWorldCoords(new Vector2i(cellPos.x, cellPos.y));
						Vector3 zero = Vector3.zero;
						pathLink.downwards = new PathLinkSide();
						pathLink.downwards.origin = new PathLinkSegment();
						pathLink.downwards.origin.position = position;
						pathLink.downwards.origin.rotation = Quaternion.Euler(eulerAngles);
						pathLink.downwards.origin.scale = Vector3.one;
						pathLink.downwards.origin.link = componentInChildren4;
						pathLink.downwards.segments = new List<PathLinkSegment>();
						pathLink.upwards = new PathLinkSide();
						pathLink.upwards.origin = new PathLinkSegment();
						pathLink.upwards.origin.position = position2;
						pathLink.upwards.origin.rotation = Quaternion.Euler(zero);
						pathLink.upwards.origin.scale = Vector3.one;
						pathLink.upwards.origin.link = componentInChildren5;
						pathLink.upwards.segments = new List<PathLinkSegment>();
						list3.Add(pathLink);
					}
					void AddNode(int x, int y)
					{
						PathFinder.Node node8 = pathFinder.FindClosestWalkable(new PathFinder.Point(x, y), 1);
						if (node8 != null)
						{
							PathNode item = new PathNode
							{
								monument = (TerrainMeta.Path ? TerrainMeta.Path.FindClosest(TerrainMeta.Path.Monuments, entrance.transform.position) : entrance.transform.GetComponentInParent<MonumentInfo>()),
								node = node8
							};
							if (isStartPoint)
							{
								secondaryNodeList.Add(item);
							}
							else
							{
								unconnectedNodeList.Add(item);
							}
							DungeonGridConnectionHash dungeonGridConnectionHash4 = hashmap[node8.point.x, node8.point.y];
							DungeonGridConnectionHash dungeonGridConnectionHash5 = hashmap[stationNode.point.x, stationNode.point.y];
							if (node8.point.x > stationNode.point.x)
							{
								dungeonGridConnectionHash4.West = true;
								dungeonGridConnectionHash5.East = true;
							}
							if (node8.point.x < stationNode.point.x)
							{
								dungeonGridConnectionHash4.East = true;
								dungeonGridConnectionHash5.West = true;
							}
							if (node8.point.y > stationNode.point.y)
							{
								dungeonGridConnectionHash4.South = true;
								dungeonGridConnectionHash5.North = true;
							}
							if (node8.point.y < stationNode.point.y)
							{
								dungeonGridConnectionHash4.North = true;
								dungeonGridConnectionHash5.South = true;
							}
							hashmap[node8.point.x, node8.point.y] = dungeonGridConnectionHash4;
							hashmap[stationNode.point.x, stationNode.point.y] = dungeonGridConnectionHash5;
						}
					}
				}
			}
			while (unconnectedNodeList.Count != 0 || secondaryNodeList.Count != 0)
			{
				if (unconnectedNodeList.Count == 0)
				{
					PathNode node = secondaryNodeList[0];
					unconnectedNodeList.AddRange(secondaryNodeList.Where((PathNode x) => x.monument == node.monument));
					secondaryNodeList.RemoveAll((PathNode x) => x.monument == node.monument);
					Vector2i vector2i = worldSpaceGrid.WorldToGridCoords(node.monument.transform.position);
					pathFinder.PushPoint = new PathFinder.Point(vector2i.x, vector2i.y);
					pathFinder.PushRadius = (pathFinder.PushDistance = 2);
					pathFinder.PushMultiplier = 16;
				}
				list7.Clear();
				list7.AddRange(unconnectedNodeList.Select((PathNode x) => x.node.point));
				list6.Clear();
				list6.AddRange(list4.Select((PathNode x) => x.node.point));
				list6.AddRange(secondaryNodeList.Select((PathNode x) => x.node.point));
				list6.AddRange(list5);
				PathFinder.Node node2 = pathFinder.FindPathUndirected(list6, list7, 100000);
				if (node2 == null)
				{
					PathNode node3 = unconnectedNodeList[0];
					secondaryNodeList.AddRange(unconnectedNodeList.Where((PathNode x) => x.monument == node3.monument));
					unconnectedNodeList.RemoveAll((PathNode x) => x.monument == node3.monument);
					secondaryNodeList.Remove(node3);
					list4.Add(node3);
					continue;
				}
				PathSegment segment = new PathSegment();
				for (PathFinder.Node node4 = node2; node4 != null; node4 = node4.next)
				{
					if (node4 == node2)
					{
						segment.start = node4;
					}
					if (node4.next == null)
					{
						segment.end = node4;
					}
				}
				list2.Add(segment);
				PathNode node5 = unconnectedNodeList.Find((PathNode x) => x.node.point == segment.start.point || x.node.point == segment.end.point);
				secondaryNodeList.AddRange(unconnectedNodeList.Where((PathNode x) => x.monument == node5.monument));
				unconnectedNodeList.RemoveAll((PathNode x) => x.monument == node5.monument);
				secondaryNodeList.Remove(node5);
				list4.Add(node5);
				PathNode pathNode = secondaryNodeList.Find((PathNode x) => x.node.point == segment.start.point || x.node.point == segment.end.point);
				if (pathNode != null)
				{
					secondaryNodeList.Remove(pathNode);
					list4.Add(pathNode);
				}
				for (PathFinder.Node node6 = node2; node6 != null; node6 = node6.next)
				{
					if (node6 != node2 && node6.next != null)
					{
						list5.Add(node6.point);
					}
				}
			}
			foreach (PathSegment item3 in list2)
			{
				PathFinder.Node node7 = item3.start;
				while (node7 != null && node7.next != null)
				{
					DungeonGridConnectionHash dungeonGridConnectionHash = hashmap[node7.point.x, node7.point.y];
					DungeonGridConnectionHash dungeonGridConnectionHash2 = hashmap[node7.next.point.x, node7.next.point.y];
					if (node7.point.x > node7.next.point.x)
					{
						dungeonGridConnectionHash.West = true;
						dungeonGridConnectionHash2.East = true;
					}
					if (node7.point.x < node7.next.point.x)
					{
						dungeonGridConnectionHash.East = true;
						dungeonGridConnectionHash2.West = true;
					}
					if (node7.point.y > node7.next.point.y)
					{
						dungeonGridConnectionHash.South = true;
						dungeonGridConnectionHash2.North = true;
					}
					if (node7.point.y < node7.next.point.y)
					{
						dungeonGridConnectionHash.North = true;
						dungeonGridConnectionHash2.South = true;
					}
					hashmap[node7.point.x, node7.point.y] = dungeonGridConnectionHash;
					hashmap[node7.next.point.x, node7.next.point.y] = dungeonGridConnectionHash2;
					node7 = node7.next;
				}
			}
			for (int num9 = 0; num9 < worldSpaceGrid.CellCount; num9++)
			{
				for (int num10 = 0; num10 < worldSpaceGrid.CellCount; num10++)
				{
					if (array6[num9, num10] == int.MaxValue)
					{
						continue;
					}
					DungeonGridConnectionHash dungeonGridConnectionHash3 = hashmap[num9, num10];
					if (dungeonGridConnectionHash3.Value == 0)
					{
						continue;
					}
					ArrayEx.Shuffle(array, ref seed);
					Prefab<DungeonGridCell>[] array7 = array;
					foreach (Prefab<DungeonGridCell> prefab6 in array7)
					{
						Prefab<DungeonGridCell> prefab7 = ((num9 > num) ? worldSpaceGrid[num9 - 1, num10] : null);
						if (((prefab7 != null) ? ((prefab6.Component.West == prefab7.Component.East) ? 1 : 0) : (dungeonGridConnectionHash3.West ? ((int)prefab6.Component.West) : ((prefab6.Component.West == DungeonGridConnectionType.None) ? 1 : 0))) == 0)
						{
							continue;
						}
						Prefab<DungeonGridCell> prefab8 = ((num9 < num2) ? worldSpaceGrid[num9 + 1, num10] : null);
						if (((prefab8 != null) ? ((prefab6.Component.East == prefab8.Component.West) ? 1 : 0) : (dungeonGridConnectionHash3.East ? ((int)prefab6.Component.East) : ((prefab6.Component.East == DungeonGridConnectionType.None) ? 1 : 0))) == 0)
						{
							continue;
						}
						Prefab<DungeonGridCell> prefab9 = ((num10 > num) ? worldSpaceGrid[num9, num10 - 1] : null);
						if (((prefab9 != null) ? ((prefab6.Component.South == prefab9.Component.North) ? 1 : 0) : (dungeonGridConnectionHash3.South ? ((int)prefab6.Component.South) : ((prefab6.Component.South == DungeonGridConnectionType.None) ? 1 : 0))) == 0)
						{
							continue;
						}
						Prefab<DungeonGridCell> prefab10 = ((num10 < num2) ? worldSpaceGrid[num9, num10 + 1] : null);
						if (((prefab10 != null) ? (prefab6.Component.North == prefab10.Component.South) : (dungeonGridConnectionHash3.North ? ((byte)prefab6.Component.North != 0) : (prefab6.Component.North == DungeonGridConnectionType.None))) && (prefab6.Component.West == DungeonGridConnectionType.None || prefab7 == null || !prefab6.Component.ShouldAvoid(prefab7.ID)) && (prefab6.Component.East == DungeonGridConnectionType.None || prefab8 == null || !prefab6.Component.ShouldAvoid(prefab8.ID)) && (prefab6.Component.South == DungeonGridConnectionType.None || prefab9 == null || !prefab6.Component.ShouldAvoid(prefab9.ID)) && (prefab6.Component.North == DungeonGridConnectionType.None || prefab10 == null || !prefab6.Component.ShouldAvoid(prefab10.ID)))
						{
							worldSpaceGrid[num9, num10] = prefab6;
							bool num11 = prefab7 == null || prefab6.Component.WestVariant == prefab7.Component.EastVariant;
							bool flag5 = prefab9 == null || prefab6.Component.SouthVariant == prefab9.Component.NorthVariant;
							if (num11 && flag5)
							{
								break;
							}
						}
					}
				}
			}
			Vector3 zero2 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			do
			{
				zero3 = zero2;
				for (int num12 = 0; num12 < worldSpaceGrid.CellCount; num12++)
				{
					for (int num13 = 0; num13 < worldSpaceGrid.CellCount; num13++)
					{
						Prefab<DungeonGridCell> prefab11 = worldSpaceGrid[num12, num13];
						if (prefab11 != null)
						{
							Vector2i cellPos2 = new Vector2i(num12, num13);
							Vector3 vector2 = worldSpaceGrid.GridToWorldCoords(cellPos2);
							while (!IsValidPrefabPlacement(prefab11, zero2 + vector2, Quaternion.identity, Vector3.one, EnvironmentType.Underground | EnvironmentType.Building))
							{
								zero2.y -= 3f;
							}
						}
					}
				}
			}
			while (zero2 != zero3);
			foreach (PathLink item4 in list3)
			{
				item4.upwards.origin.position += zero2;
			}
			foreach (PathLink item5 in list3)
			{
				Vector3 vector3 = item5.upwards.origin.position + item5.upwards.origin.rotation * Vector3.Scale(item5.upwards.origin.upSocket.localPosition, item5.upwards.origin.scale);
				Vector3 vector4 = item5.downwards.origin.position + item5.downwards.origin.rotation * Vector3.Scale(item5.downwards.origin.downSocket.localPosition, item5.downwards.origin.scale) - vector3;
				Vector3[] array8 = new Vector3[2]
				{
					new Vector3(0f, 1f, 0f),
					new Vector3(1f, 1f, 1f)
				};
				for (int num5 = 0; num5 < array8.Length; num5++)
				{
					Vector3 b = array8[num5];
					int num14 = 0;
					int num15 = 0;
					while (vector4.magnitude > 1f && (num14 < 8 || num15 < 8))
					{
						bool flag6 = num14 > 2 && num15 > 2;
						bool flag7 = num14 > 4 && num15 > 4;
						Prefab<DungeonGridLink> prefab12 = null;
						Vector3 vector5 = Vector3.zero;
						int num16 = int.MinValue;
						Vector3 position3 = Vector3.zero;
						Quaternion rotation = Quaternion.identity;
						PathLinkSegment prevSegment = item5.downwards.prevSegment;
						Vector3 vector6 = prevSegment.position + prevSegment.rotation * Vector3.Scale(prevSegment.scale, prevSegment.downSocket.localPosition);
						Quaternion quaternion = prevSegment.rotation * prevSegment.downSocket.localRotation;
						Prefab<DungeonGridLink>[] array9 = array5;
						foreach (Prefab<DungeonGridLink> prefab13 in array9)
						{
							float num17 = SeedRandom.Value(ref seed);
							DungeonGridLink component = prefab13.Component;
							if (prevSegment.downType != component.UpType)
							{
								continue;
							}
							switch (component.DownType)
							{
							case DungeonGridLinkType.Elevator:
								if (flag6 || b.x != 0f || b.z != 0f)
								{
									continue;
								}
								break;
							case DungeonGridLinkType.Transition:
								if (b.x != 0f || b.z != 0f)
								{
									continue;
								}
								break;
							}
							int num18 = ((!flag6) ? component.Priority : 0);
							if (num16 > num18)
							{
								continue;
							}
							Quaternion quaternion2 = quaternion * Quaternion.Inverse(component.UpSocket.localRotation);
							Quaternion quaternion3 = quaternion2 * component.DownSocket.localRotation;
							PathLinkSegment prevSegment2 = item5.upwards.prevSegment;
							Quaternion quaternion4 = prevSegment2.rotation * prevSegment2.upSocket.localRotation;
							if (component.Rotation > 0)
							{
								if (Quaternion.Angle(quaternion4, quaternion3) > (float)component.Rotation)
								{
									continue;
								}
								Quaternion quaternion5 = quaternion4 * Quaternion.Inverse(quaternion3);
								quaternion2 *= quaternion5;
								quaternion3 *= quaternion5;
							}
							Vector3 vector7 = vector6 - quaternion2 * component.UpSocket.localPosition;
							Vector3 vector8 = quaternion2 * (component.DownSocket.localPosition - component.UpSocket.localPosition);
							Vector3 a = vector4 + vector5;
							Vector3 a2 = vector4 + vector8;
							float magnitude = a.magnitude;
							float magnitude2 = a2.magnitude;
							Vector3 vector9 = Vector3.Scale(a, b);
							Vector3 vector10 = Vector3.Scale(a2, b);
							float magnitude3 = vector9.magnitude;
							float magnitude4 = vector10.magnitude;
							if (vector5 != Vector3.zero)
							{
								if (magnitude3 < magnitude4 || (magnitude3 == magnitude4 && magnitude < magnitude2) || (magnitude3 == magnitude4 && magnitude == magnitude2 && num17 < 0.5f))
								{
									continue;
								}
							}
							else if (magnitude3 <= magnitude4)
							{
								continue;
							}
							if (Mathf.Abs(vector10.x) - Mathf.Abs(vector9.x) > 0.01f || (Mathf.Abs(vector10.x) > 0.01f && a.x * a2.x < 0f) || Mathf.Abs(vector10.y) - Mathf.Abs(vector9.y) > 0.01f || (Mathf.Abs(vector10.y) > 0.01f && a.y * a2.y < 0f) || Mathf.Abs(vector10.z) - Mathf.Abs(vector9.z) > 0.01f || (Mathf.Abs(vector10.z) > 0.01f && a.z * a2.z < 0f) || (flag6 && b.x == 0f && b.z == 0f && component.DownType == DungeonGridLinkType.Default && ((Mathf.Abs(a2.x) > 0.01f && Mathf.Abs(a2.x) < LinkRadius * 2f - 0.1f) || (Mathf.Abs(a2.z) > 0.01f && Mathf.Abs(a2.z) < LinkRadius * 2f - 0.1f))))
							{
								continue;
							}
							num16 = num18;
							if (b.x == 0f && b.z == 0f)
							{
								if (!flag6 && Mathf.Abs(a2.y) < LinkTransition - 0.1f)
								{
									continue;
								}
							}
							else if ((!flag6 && magnitude4 > 0.01f && (Mathf.Abs(a2.x) < LinkRadius * 2f - 0.1f || Mathf.Abs(a2.z) < LinkRadius * 2f - 0.1f)) || (!flag7 && magnitude4 > 0.01f && (Mathf.Abs(a2.x) < LinkRadius * 1f - 0.1f || Mathf.Abs(a2.z) < LinkRadius * 1f - 0.1f)))
							{
								continue;
							}
							if (!flag6 || !(magnitude4 < 0.01f) || !(magnitude2 < 0.01f) || !(Quaternion.Angle(quaternion4, quaternion3) > 10f))
							{
								prefab12 = prefab13;
								vector5 = vector8;
								num16 = num18;
								position3 = vector7;
								rotation = quaternion2;
							}
						}
						if (vector5 != Vector3.zero)
						{
							PathLinkSegment pathLinkSegment = new PathLinkSegment();
							pathLinkSegment.position = position3;
							pathLinkSegment.rotation = rotation;
							pathLinkSegment.scale = Vector3.one;
							pathLinkSegment.prefab = prefab12;
							pathLinkSegment.link = prefab12.Component;
							item5.downwards.segments.Add(pathLinkSegment);
							vector4 += vector5;
						}
						else
						{
							num15++;
						}
						if (b.x > 0f || b.z > 0f)
						{
							Prefab<DungeonGridLink> prefab14 = null;
							Vector3 vector11 = Vector3.zero;
							int num19 = int.MinValue;
							Vector3 position4 = Vector3.zero;
							Quaternion rotation2 = Quaternion.identity;
							PathLinkSegment prevSegment3 = item5.upwards.prevSegment;
							Vector3 vector12 = prevSegment3.position + prevSegment3.rotation * Vector3.Scale(prevSegment3.scale, prevSegment3.upSocket.localPosition);
							Quaternion quaternion6 = prevSegment3.rotation * prevSegment3.upSocket.localRotation;
							array9 = array5;
							foreach (Prefab<DungeonGridLink> prefab15 in array9)
							{
								float num20 = SeedRandom.Value(ref seed);
								DungeonGridLink component2 = prefab15.Component;
								if (prevSegment3.upType != component2.DownType)
								{
									continue;
								}
								switch (component2.DownType)
								{
								case DungeonGridLinkType.Elevator:
									if (flag6 || b.x != 0f || b.z != 0f)
									{
										continue;
									}
									break;
								case DungeonGridLinkType.Transition:
									if (b.x != 0f || b.z != 0f)
									{
										continue;
									}
									break;
								}
								int num21 = ((!flag6) ? component2.Priority : 0);
								if (num19 > num21)
								{
									continue;
								}
								Quaternion quaternion7 = quaternion6 * Quaternion.Inverse(component2.DownSocket.localRotation);
								Quaternion quaternion8 = quaternion7 * component2.UpSocket.localRotation;
								PathLinkSegment prevSegment4 = item5.downwards.prevSegment;
								Quaternion quaternion9 = prevSegment4.rotation * prevSegment4.downSocket.localRotation;
								if (component2.Rotation > 0)
								{
									if (Quaternion.Angle(quaternion9, quaternion8) > (float)component2.Rotation)
									{
										continue;
									}
									Quaternion quaternion10 = quaternion9 * Quaternion.Inverse(quaternion8);
									quaternion7 *= quaternion10;
									quaternion8 *= quaternion10;
								}
								Vector3 vector13 = vector12 - quaternion7 * component2.DownSocket.localPosition;
								Vector3 vector14 = quaternion7 * (component2.UpSocket.localPosition - component2.DownSocket.localPosition);
								Vector3 a3 = vector4 - vector11;
								Vector3 a4 = vector4 - vector14;
								float magnitude5 = a3.magnitude;
								float magnitude6 = a4.magnitude;
								Vector3 vector15 = Vector3.Scale(a3, b);
								Vector3 vector16 = Vector3.Scale(a4, b);
								float magnitude7 = vector15.magnitude;
								float magnitude8 = vector16.magnitude;
								if (vector11 != Vector3.zero)
								{
									if (magnitude7 < magnitude8 || (magnitude7 == magnitude8 && magnitude5 < magnitude6) || (magnitude7 == magnitude8 && magnitude5 == magnitude6 && num20 < 0.5f))
									{
										continue;
									}
								}
								else if (magnitude7 <= magnitude8)
								{
									continue;
								}
								if (Mathf.Abs(vector16.x) - Mathf.Abs(vector15.x) > 0.01f || (Mathf.Abs(vector16.x) > 0.01f && a3.x * a4.x < 0f) || Mathf.Abs(vector16.y) - Mathf.Abs(vector15.y) > 0.01f || (Mathf.Abs(vector16.y) > 0.01f && a3.y * a4.y < 0f) || Mathf.Abs(vector16.z) - Mathf.Abs(vector15.z) > 0.01f || (Mathf.Abs(vector16.z) > 0.01f && a3.z * a4.z < 0f) || (flag6 && b.x == 0f && b.z == 0f && component2.UpType == DungeonGridLinkType.Default && ((Mathf.Abs(a4.x) > 0.01f && Mathf.Abs(a4.x) < LinkRadius * 2f - 0.1f) || (Mathf.Abs(a4.z) > 0.01f && Mathf.Abs(a4.z) < LinkRadius * 2f - 0.1f))))
								{
									continue;
								}
								num19 = num21;
								if (b.x == 0f && b.z == 0f)
								{
									if (!flag6 && Mathf.Abs(a4.y) < LinkTransition - 0.1f)
									{
										continue;
									}
								}
								else if ((!flag6 && magnitude8 > 0.01f && (Mathf.Abs(a4.x) < LinkRadius * 2f - 0.1f || Mathf.Abs(a4.z) < LinkRadius * 2f - 0.1f)) || (!flag7 && magnitude8 > 0.01f && (Mathf.Abs(a4.x) < LinkRadius * 1f - 0.1f || Mathf.Abs(a4.z) < LinkRadius * 1f - 0.1f)))
								{
									continue;
								}
								if (!flag6 || !(magnitude8 < 0.01f) || !(magnitude6 < 0.01f) || !(Quaternion.Angle(quaternion9, quaternion8) > 10f))
								{
									prefab14 = prefab15;
									vector11 = vector14;
									num19 = num21;
									position4 = vector13;
									rotation2 = quaternion7;
								}
							}
							if (vector11 != Vector3.zero)
							{
								PathLinkSegment pathLinkSegment2 = new PathLinkSegment();
								pathLinkSegment2.position = position4;
								pathLinkSegment2.rotation = rotation2;
								pathLinkSegment2.scale = Vector3.one;
								pathLinkSegment2.prefab = prefab14;
								pathLinkSegment2.link = prefab14.Component;
								item5.upwards.segments.Add(pathLinkSegment2);
								vector4 -= vector11;
							}
							else
							{
								num14++;
							}
						}
						else
						{
							num14++;
						}
					}
				}
			}
			foreach (PathLink item6 in list3)
			{
				foreach (PathLinkSegment segment2 in item6.downwards.segments)
				{
					World.AddPrefab("Dungeon", segment2.prefab, segment2.position, segment2.rotation, segment2.scale);
				}
				foreach (PathLinkSegment segment3 in item6.upwards.segments)
				{
					World.AddPrefab("Dungeon", segment3.prefab, segment3.position, segment3.rotation, segment3.scale);
				}
			}
			if (TerrainMeta.Path.Rails.Count > 0)
			{
				List<PrefabReplacement> list8 = new List<PrefabReplacement>();
				for (int num22 = 0; num22 < worldSpaceGrid.CellCount; num22++)
				{
					for (int num23 = 0; num23 < worldSpaceGrid.CellCount; num23++)
					{
						Prefab<DungeonGridCell> prefab16 = worldSpaceGrid[num22, num23];
						if (prefab16 == null || !prefab16.Component.Replaceable)
						{
							continue;
						}
						Vector2i vector2i2 = new Vector2i(num22, num23);
						Vector3 vector17 = worldSpaceGrid.GridToWorldCoords(vector2i2) + zero2;
						Prefab<DungeonGridCell>[] array7 = array3;
						foreach (Prefab<DungeonGridCell> prefab17 in array7)
						{
							if (prefab16.Component.North != prefab17.Component.North || prefab16.Component.South != prefab17.Component.South || prefab16.Component.West != prefab17.Component.West || prefab16.Component.East != prefab17.Component.East || !IsValidPrefabPlacement(prefab17, vector17, Quaternion.identity, Vector3.one, EnvironmentType.Underground) || !prefab17.ApplyTerrainChecks(vector17, Quaternion.identity, Vector3.one) || !prefab17.ApplyTerrainFilters(vector17, Quaternion.identity, Vector3.one))
							{
								continue;
							}
							MonumentInfo componentInChildren6 = prefab17.Object.GetComponentInChildren<MonumentInfo>();
							Vector3 vector18 = vector17;
							if ((bool)componentInChildren6)
							{
								vector18 += componentInChildren6.transform.position;
							}
							if (!(vector18.y < 1f))
							{
								float distanceToAboveGroundRail = GetDistanceToAboveGroundRail(vector18);
								if (!(distanceToAboveGroundRail < 200f))
								{
									list8.Add(new PrefabReplacement
									{
										gridPosition = vector2i2,
										worldPosition = vector18,
										distance = Mathf.RoundToInt(distanceToAboveGroundRail),
										prefab = prefab17
									});
								}
							}
						}
					}
				}
				list8.Shuffle(ref seed);
				list8.Sort((PrefabReplacement prefabReplacement, PrefabReplacement prefabReplacement2) => prefabReplacement.distance.CompareTo(prefabReplacement2.distance));
				int num24 = 2;
				while (num24 > 0 && list8.Count > 0)
				{
					num24--;
					PrefabReplacement replacement = list8[0];
					worldSpaceGrid[replacement.gridPosition.x, replacement.gridPosition.y] = replacement.prefab;
					list8.RemoveAll((PrefabReplacement prefabReplacement) => (prefabReplacement.worldPosition - replacement.worldPosition).magnitude < 1500f);
				}
			}
			for (int num25 = 0; num25 < worldSpaceGrid.CellCount; num25++)
			{
				for (int num26 = 0; num26 < worldSpaceGrid.CellCount; num26++)
				{
					Prefab<DungeonGridCell> prefab18 = worldSpaceGrid[num25, num26];
					if (prefab18 != null)
					{
						Vector2i cellPos3 = new Vector2i(num25, num26);
						Vector3 vector19 = worldSpaceGrid.GridToWorldCoords(cellPos3);
						World.AddPrefab("Dungeon", prefab18, zero2 + vector19, Quaternion.identity, Vector3.one);
					}
				}
			}
			for (int num27 = 0; num27 < worldSpaceGrid.CellCount - 1; num27++)
			{
				for (int num28 = 0; num28 < worldSpaceGrid.CellCount - 1; num28++)
				{
					Prefab<DungeonGridCell> prefab19 = worldSpaceGrid[num27, num28];
					Prefab<DungeonGridCell> prefab20 = worldSpaceGrid[num27 + 1, num28];
					Prefab<DungeonGridCell> prefab21 = worldSpaceGrid[num27, num28 + 1];
					Prefab<DungeonGridCell>[] array7;
					if (prefab19 != null && prefab20 != null && prefab19.Component.EastVariant != prefab20.Component.WestVariant)
					{
						ArrayEx.Shuffle(array4, ref seed);
						array7 = array4;
						foreach (Prefab<DungeonGridCell> prefab22 in array7)
						{
							if (prefab22.Component.West == prefab19.Component.East && prefab22.Component.East == prefab20.Component.West && prefab22.Component.WestVariant == prefab19.Component.EastVariant && prefab22.Component.EastVariant == prefab20.Component.WestVariant)
							{
								Vector2i cellPos4 = new Vector2i(num27, num28);
								Vector3 vector20 = worldSpaceGrid.GridToWorldCoords(cellPos4) + new Vector3(worldSpaceGrid.CellSizeHalf, 0f, 0f);
								World.AddPrefab("Dungeon", prefab22, zero2 + vector20, Quaternion.identity, Vector3.one);
								break;
							}
						}
					}
					if (prefab19 == null || prefab21 == null || prefab19.Component.NorthVariant == prefab21.Component.SouthVariant)
					{
						continue;
					}
					ArrayEx.Shuffle(array4, ref seed);
					array7 = array4;
					foreach (Prefab<DungeonGridCell> prefab23 in array7)
					{
						if (prefab23.Component.South == prefab19.Component.North && prefab23.Component.North == prefab21.Component.South && prefab23.Component.SouthVariant == prefab19.Component.NorthVariant && prefab23.Component.NorthVariant == prefab21.Component.SouthVariant)
						{
							Vector2i cellPos5 = new Vector2i(num27, num28);
							Vector3 vector21 = worldSpaceGrid.GridToWorldCoords(cellPos5) + new Vector3(0f, 0f, worldSpaceGrid.CellSizeHalf);
							World.AddPrefab("Dungeon", prefab23, zero2 + vector21, Quaternion.identity, Vector3.one);
							break;
						}
					}
				}
			}
		}
		bool IsValidPrefabPlacement(Prefab<DungeonGridCell> prefab24, Vector3 pos, Quaternion rot, Vector3 scale, EnvironmentType type)
		{
			Vector3 vector22 = Vector3.up * 9f;
			Vector3 vector23 = Vector3.up * (LinkTransition + 1f);
			if (!prefab24.CheckEnvironmentVolumesBelowAltitude(pos + vector22, rot, scale, EnvironmentType.Underground, EnvironmentType.Entrance) && !prefab24.CheckEnvironmentVolumesInsideTerrain(pos + vector22, rot, scale, EnvironmentType.TrainTunnels, EnvironmentType.Entrance))
			{
				return false;
			}
			if (prefab24.CheckEnvironmentVolumes(pos + vector23, rot, scale, type))
			{
				return false;
			}
			if (prefab24.CheckEnvironmentVolumes(pos, rot, scale, type))
			{
				return false;
			}
			return true;
		}
	}

	private float GetDistanceToAboveGroundRail(Vector3 pos)
	{
		float num = float.MaxValue;
		if ((bool)TerrainMeta.Path)
		{
			foreach (PathList rail in TerrainMeta.Path.Rails)
			{
				Vector3[] points = rail.Path.Points;
				foreach (Vector3 a in points)
				{
					num = Mathf.Min(num, Vector3Ex.Distance2D(a, pos));
				}
			}
		}
		return num;
	}
}
