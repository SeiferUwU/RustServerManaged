using System;
using UnityEngine;

namespace Rust.DLSS;

public struct DLSSData
{
	public IntPtr color;

	public IntPtr depth;

	public IntPtr motion;

	public IntPtr exposure;

	public IntPtr colorOutput;

	public int resetAccumulation;

	public float sharpness;

	public Vector2 jitterOffset;

	public Vector2 mVScale;

	public Vector2 viewportOffset;

	public Vector2 viewportSize;
}
