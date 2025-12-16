using System;
using UnityEngine;

namespace Rust.RenderPipeline.Runtime;

[Serializable]
public class RustRenderPipelineSettings
{
	public enum ColorLutResolution
	{
		_16 = 0x10,
		_32 = 0x20,
		_64 = 0x40
	}

	public enum RenderPath
	{
		ForwardPlus,
		Deferred
	}

	[Serializable]
	public struct PipelineShaders
	{
		public Shader cameraRendererShader;

		public Shader cameraDebuggerShader;

		public Shader deferredLightingShader;

		public Shader deferredIndirectLightingShader;

		public Shader coreBlit;

		public Shader coreBlitColorAndDepth;
	}

	[Serializable]
	public struct PipelineTextures
	{
		public Texture blueNoise;

		public Texture preIntegratedFgdGgx;

		public Texture environmentBrdfLut;
	}

	public RenderPath renderPath;

	public CameraBufferSettings cameraBuffer = new CameraBufferSettings
	{
		allowHDR = true,
		renderScale = 1f,
		fxaaSettings = new CameraBufferSettings.FxaaSettings
		{
			fixedThreshold = 0.0833f,
			relativeThreshold = 0.166f,
			subpixelBlending = 0.75f
		}
	};

	public bool useSrpBatching = true;

	public ForwardPlusSettings forwardPlus;

	public ShadowSettings shadows;

	public bool postProcessingEnabled = true;

	public PostFXSettings postFXSettings;

	public ColorLutResolution colorLutResolution = ColorLutResolution._32;

	public PipelineShaders pipelineShaders;

	public PipelineTextures pipelineTextures;
}
