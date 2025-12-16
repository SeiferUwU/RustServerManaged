using ConVar;
using UnityEngine;

namespace Rust.Ai.Gen2;

public class BaseNPC2 : BaseCombatEntity
{
	[SerializeField]
	private float mass = 45f;

	public override bool IsNpc => true;

	public bool IsAnimal => true;

	public override float RealisticMass => mass;

	public string displayName
	{
		get
		{
			PrefabInformation prefabInformation = null;
			if (base.isServer)
			{
				prefabInformation = PrefabAttribute.server.Find<PrefabInformation>(prefabID);
			}
			if (prefabInformation == null)
			{
				if (AI.logIssues)
				{
					Debug.LogError("PrefabInformation not found for " + Categorize() + ")");
				}
				return "NPC";
			}
			return prefabInformation.title.english;
		}
	}

	public override float MaxVelocity()
	{
		return 10f;
	}

	public override void InitShared()
	{
		base.InitShared();
		if (base.isServer)
		{
			Query.Server.AddBrain(this);
		}
	}

	public override void DestroyShared()
	{
		base.DestroyShared();
		if (base.isServer && !Application.isQuitting)
		{
			Query.Server.RemoveBrain(this);
		}
	}
}
