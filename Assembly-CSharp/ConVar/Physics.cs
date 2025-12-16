using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ConVar;

[Factory("physics")]
public class Physics : ConsoleSystem
{
	private class PhysxCell
	{
		public Bounds Bounds;

		public Vector2i GridPosition;

		public int Id;

		public List<Collider> Colliders;
	}

	[ServerVar(Help = "The collision detection mode that dropped items and corpses should use")]
	public static int droppedmode = 2;

	[ServerVar(Help = "Send effects to clients when physics objects collide")]
	public static bool sendeffects = true;

	[ServerVar]
	public static bool groundwatchdebug = false;

	[ServerVar]
	public static int groundwatchfails = 1;

	[ServerVar]
	public static float groundwatchdelay = 0.1f;

	[ServerVar(Help = "The collision detection mode that server-side ragdolls should use")]
	public static int serverragdollmode = 3;

	private const float baseGravity = -9.81f;

	private static bool _serversideragdolls = false;

	[ServerVar]
	public static float towingmaxlinearaccelfromjoint = 40f;

	[ServerVar]
	public static bool allowplayertempragdoll = true;

	[ServerVar]
	public static bool allowhorsetempragdoll = true;

	[ClientVar]
	[ServerVar]
	public static bool batchsynctransforms = true;

	private static bool _treecollision = true;

	private static Bounds _bounds = new Bounds
	{
		center = Vector3.zero,
		extents = new Vector3(5000f, 4000f, 5000f)
	};

	[ServerVar]
	public static float bouncethreshold
	{
		get
		{
			return UnityEngine.Physics.bounceThreshold;
		}
		set
		{
			UnityEngine.Physics.bounceThreshold = value;
		}
	}

	[ServerVar]
	public static float sleepthreshold
	{
		get
		{
			return UnityEngine.Physics.sleepThreshold;
		}
		set
		{
			UnityEngine.Physics.sleepThreshold = value;
		}
	}

	[ServerVar(Help = "The default solver iteration count permitted for any rigid bodies (default 7). Must be positive")]
	public static int solveriterationcount
	{
		get
		{
			return UnityEngine.Physics.defaultSolverIterations;
		}
		set
		{
			UnityEngine.Physics.defaultSolverIterations = value;
		}
	}

	[ReplicatedVar(Help = "Gravity multiplier", Default = "1.0")]
	public static float gravity
	{
		get
		{
			return UnityEngine.Physics.gravity.y / -9.81f;
		}
		set
		{
			UnityEngine.Physics.gravity = new Vector3(0f, value * -9.81f, 0f);
		}
	}

	[ReplicatedVar(Help = "Do ragdoll physics calculations on the server, or use the old client-side system", Saved = true, ShowInAdminUI = true)]
	public static bool serversideragdolls
	{
		get
		{
			return _serversideragdolls;
		}
		set
		{
			_serversideragdolls = value;
			UnityEngine.Physics.IgnoreLayerCollision(9, 13, !_serversideragdolls);
			UnityEngine.Physics.IgnoreLayerCollision(9, 11, !_serversideragdolls);
			UnityEngine.Physics.IgnoreLayerCollision(9, 28, !_serversideragdolls);
		}
	}

	[ClientVar]
	[ServerVar]
	public static bool autosynctransforms
	{
		get
		{
			return UnityEngine.Physics.autoSyncTransforms;
		}
		set
		{
			UnityEngine.Physics.autoSyncTransforms = value;
		}
	}

	[ReplicatedVar(Help = "Do players and vehicles collide with trees?", Saved = true, ShowInAdminUI = true)]
	public static bool treecollision
	{
		get
		{
			return _treecollision;
		}
		set
		{
			_treecollision = value;
			UnityEngine.Physics.IgnoreLayerCollision(15, 30, !_treecollision);
			UnityEngine.Physics.IgnoreLayerCollision(12, 30, !_treecollision);
		}
	}

	internal static void ApplyDropped(Rigidbody rigidBody)
	{
		if (droppedmode <= 0)
		{
			rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
		}
		if (droppedmode == 1)
		{
			rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
		}
		if (droppedmode == 2)
		{
			rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
		}
		if (droppedmode >= 3)
		{
			rigidBody.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
		}
	}

	[ServerVar]
	public static void print_colliders_per_cell(Arg arg)
	{
		List<PhysxCell> collidersPerBroadphaseCell = GetCollidersPerBroadphaseCell();
		if (collidersPerBroadphaseCell.Count == 0)
		{
			arg.ReplyWith("No colliders found");
		}
		int num = collidersPerBroadphaseCell.Sum((PhysxCell x) => x.Colliders.Count);
		StringBuilder stringBuilder = new StringBuilder();
		PhysxCell[] array = collidersPerBroadphaseCell.OrderByDescending((PhysxCell x) => x.Colliders.Count).ToArray();
		stringBuilder.AppendLine($"Found {num} in {array.Length} cells, cell size {array[0].Bounds.size}");
		PhysxCell[] array2 = array;
		foreach (PhysxCell physxCell in array2)
		{
			if (physxCell.Colliders.Count != 0)
			{
				stringBuilder.AppendLine($"Id: {physxCell.Id} Position: {physxCell.GridPosition} Center: {physxCell.Bounds.center} Count: {physxCell.Colliders.Count}");
			}
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	[ServerVar]
	public static void print_colliders_per_prefab(Arg arg)
	{
		ICollection<Collider> collection = null;
		int cellId = arg.GetInt(0, -1);
		if (cellId >= 0 && cellId < 256)
		{
			PhysxCell physxCell = GetCollidersPerBroadphaseCell().FirstOrDefault((PhysxCell x) => x.Id == cellId);
			if (physxCell == null)
			{
				arg.ReplyWith($"Cell Id '{cellId}' not found");
				return;
			}
			collection = physxCell.Colliders;
		}
		if (collection == null)
		{
			collection = GetAllColliders();
		}
		Dictionary<string, int> dictionary = new Dictionary<string, int>();
		foreach (Collider item in collection)
		{
			BaseEntity baseEntity = GameObjectEx.ToBaseEntity(item);
			string text = "NULL";
			text = ((!(baseEntity == null) && !baseEntity.IsDestroyed) ? baseEntity.ShortPrefabName : item.name);
			dictionary.TryGetValue(text, out var value);
			dictionary[text] = value + 1;
		}
		StringBuilder stringBuilder = new StringBuilder();
		if (collection.Count == 0)
		{
			Debug.Log("No colliders found");
		}
		stringBuilder.AppendLine($"Found {collection.Count} colliders in {dictionary.Count} unique prefabs");
		foreach (KeyValuePair<string, int> item2 in dictionary.OrderByDescending((KeyValuePair<string, int> x) => x.Value))
		{
			stringBuilder.AppendLine($"Entity: {item2.Key} Count: {item2.Value}");
		}
		arg.ReplyWith(stringBuilder.ToString());
	}

	private static ICollection<Collider> GetAllColliders()
	{
		return (from collider in UnityEngine.Object.FindObjectsByType<Collider>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
			where collider != null && collider.transform != null && collider.enabled
			select collider).ToArray();
	}

	private static List<PhysxCell> GetCollidersPerBroadphaseCell()
	{
		ICollection<Collider> allColliders = GetAllColliders();
		int subdivisions = 16;
		Vector3 cellSize = new Vector3(_bounds.size.x / (float)subdivisions, _bounds.size.y, _bounds.size.z / (float)subdivisions);
		int num = 0;
		Dictionary<Vector2i, List<Collider>> dictionary = new Dictionary<Vector2i, List<Collider>>();
		foreach (Collider item in allColliders)
		{
			if (item == null || item.transform == null || !item.enabled)
			{
				continue;
			}
			Vector2i physxCell = GetPhysxCell(item.transform.position, cellSize);
			if (physxCell.x < 0 || physxCell.y < 0 || physxCell.x >= subdivisions || physxCell.y >= subdivisions)
			{
				num++;
				continue;
			}
			_ = subdivisions;
			if (!dictionary.TryGetValue(physxCell, out var value))
			{
				value = (dictionary[physxCell] = new List<Collider>());
			}
			value.Add(item);
		}
		return dictionary.Select((KeyValuePair<Vector2i, List<Collider>> x) => new PhysxCell
		{
			GridPosition = x.Key,
			Bounds = new Bounds(_bounds.min + new Vector3((float)x.Key.x * cellSize.x, _bounds.size.y / 2f, (float)x.Key.y * cellSize.z), cellSize),
			Id = x.Key.x + x.Key.y * subdivisions,
			Colliders = x.Value
		}).ToList();
	}

	private static Vector2i GetPhysxCell(Vector3 position, Vector3 cellSize)
	{
		int x = Mathf.FloorToInt((position.x - _bounds.min.x) / cellSize.x);
		int y = Mathf.FloorToInt((position.z - _bounds.min.z) / cellSize.z);
		return new Vector2i(x, y);
	}

	[ServerVar(Help = "(center vec3) (extents vec3)")]
	public static void setbounds(Arg arg)
	{
		Vector3 vector = arg.GetVector3(0);
		Vector3 vector2 = arg.GetVector3(1);
		Bounds bounds = new Bounds
		{
			center = vector,
			extents = vector2
		};
		arg.ReplyWith($"Setting PhysicsBounds to {bounds}");
		try
		{
			UnityEngine.Physics.RebuildBroadphaseRegions(bounds, 16);
			_bounds = bounds;
		}
		catch (Exception ex)
		{
			arg.ReplyWith("Failed to set bounds: " + ex.Message);
		}
	}

	[ServerVar]
	public static void getbounds(Arg arg)
	{
		arg.ReplyWith($"Bounds (center={_bounds.center}, extents={_bounds.extents})");
	}
}
