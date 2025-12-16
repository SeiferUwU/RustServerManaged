#define UNITY_ASSERTIONS
using UnityEngine;
using UnityEngine.Assertions;

namespace Rust.Rendering.IndirectInstancing;

internal struct Frustum
{
	public Plane left;

	public Plane right;

	public Plane down;

	public Plane up;

	public Plane near;

	public Plane far;

	public Frustum(Plane[] planes)
	{
		Assert.AreEqual(planes.Length, 6);
		left = planes[0];
		right = planes[1];
		down = planes[2];
		up = planes[3];
		near = planes[4];
		far = planes[5];
	}

	public static implicit operator Rust.Rendering.IndirectInstancing.Frustum(Plane[] planes)
	{
		return new Rust.Rendering.IndirectInstancing.Frustum(planes);
	}
}
