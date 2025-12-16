using System;
using UnityEngine;

namespace Rust.Ai.Gen2;

[Serializable]
public class State_MoveToBreakFoundation : State_CrocCharge
{
	private const float maxHorizontalDist = 10f;

	private Vector3 attackLocation;

	private bool FindReachableLocation(out Vector3 location)
	{
		location = default(Vector3);
		if (!base.Senses.FindTarget(out var target) || !target.ToNonNpcPlayer(out var player))
		{
			return false;
		}
		Vector3 position = target.transform.position;
		if (Vector3.Distance(Owner.transform.position, position) > 50f)
		{
			return false;
		}
		if (BaseNetworkableEx.Is<BuildingBlock>(State_CrocBreakFoundation.FindNearestTwigFoundationOnTargetBuilding(base.Agent, player), out var entAsT) && base.Agent.SamplePosition(entAsT.ClosestPoint(Owner.transform.position), out var sample, 10f))
		{
			location = sample;
			return true;
		}
		if (base.Agent.SamplePosition(position, out var sample2, 3f))
		{
			location = sample2;
			return true;
		}
		return false;
	}

	public override EFSMStateStatus OnStateEnter()
	{
		if (!base.Senses.FindTarget(out var target) || !target.ToNonNpcPlayer(out var _))
		{
			return EFSMStateStatus.Failure;
		}
		if (!FindReachableLocation(out attackLocation))
		{
			return EFSMStateStatus.Failure;
		}
		Vector3 vector = attackLocation + Owner.bounds.extents.y * Vector3.up;
		Vector3 direction = target.CenterPoint() - vector;
		if (GamePhysics.Trace(new Ray(vector, direction), 0f, out var _, direction.magnitude, 1503731969))
		{
			return EFSMStateStatus.Failure;
		}
		return base.OnStateEnter();
	}

	protected override bool GetMoveDestination(out Vector3 destination)
	{
		destination = attackLocation;
		return true;
	}
}
