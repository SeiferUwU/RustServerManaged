using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace Rust.RenderPipeline.Runtime.RenderingContext;

public class RustResourceDataContext : ContextItem
{
	internal enum ActiveID
	{
		Camera,
		BackBuffer
	}

	public static readonly int gBufferTextureCount = 4;

	private TextureHandle backBufferColor;

	private TextureHandle backBufferDepth;

	private TextureHandle cameraColor;

	private TextureHandle cameraDepth;

	private TextureHandle mainShadowsTexture;

	private TextureHandle additionalShadowsTexture;

	private ComputeBufferHandle shadowCascadesBuffer;

	private ComputeBufferHandle shadowMatricesBuffer;

	private ComputeBufferHandle additionalShadowDataBuffer;

	private ComputeBufferHandle directionalLightDataBuffer;

	private ComputeBufferHandle additionalLightDataBuffer;

	private ComputeBufferHandle lightTilesBuffer;

	private Dictionary<Light, Vector4> lightShadowData;

	private TextureHandle[] gBuffer = new TextureHandle[gBufferTextureCount];

	private TextureHandle cameraOpaqueTexture;

	private TextureHandle cameraDepthTexture;

	private TextureHandle indirectDiffuseHandle;

	private TextureHandle indirectSpecularHandle;

	internal bool IsAccessible { get; set; }

	internal ActiveID ActiveColorID { get; set; }

	public TextureHandle ActiveColorTexture
	{
		get
		{
			if (!CheckAndWarnAboutAccessibility())
			{
				return TextureHandle.nullHandle;
			}
			return ActiveColorID switch
			{
				ActiveID.Camera => CameraColor, 
				ActiveID.BackBuffer => BackBufferColor, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
		}
	}

	internal ActiveID ActiveDepthID { get; set; }

	public TextureHandle ActiveDepthTexture
	{
		get
		{
			if (!CheckAndWarnAboutAccessibility())
			{
				return TextureHandle.nullHandle;
			}
			return ActiveDepthID switch
			{
				ActiveID.Camera => CameraDepth, 
				ActiveID.BackBuffer => BackBufferDepth, 
				_ => throw new ArgumentOutOfRangeException(), 
			};
		}
	}

	public TextureHandle BackBufferColor
	{
		get
		{
			return CheckAndGetTextureHandle(ref backBufferColor);
		}
		internal set
		{
			CheckAndSetTextureHandle(ref backBufferColor, value);
		}
	}

	public TextureHandle BackBufferDepth
	{
		get
		{
			return CheckAndGetTextureHandle(ref backBufferDepth);
		}
		internal set
		{
			CheckAndSetTextureHandle(ref backBufferDepth, value);
		}
	}

	public TextureHandle CameraColor
	{
		get
		{
			return CheckAndGetTextureHandle(ref cameraColor);
		}
		set
		{
			CheckAndSetTextureHandle(ref cameraColor, value);
		}
	}

	public TextureHandle CameraDepth
	{
		get
		{
			return CheckAndGetTextureHandle(ref cameraDepth);
		}
		set
		{
			CheckAndSetTextureHandle(ref cameraDepth, value);
		}
	}

	public TextureHandle MainShadowsTexture
	{
		get
		{
			return CheckAndGetTextureHandle(ref mainShadowsTexture);
		}
		set
		{
			CheckAndSetTextureHandle(ref mainShadowsTexture, value);
		}
	}

	public TextureHandle AdditionalShadowsTexture
	{
		get
		{
			return CheckAndGetTextureHandle(ref additionalShadowsTexture);
		}
		set
		{
			CheckAndSetTextureHandle(ref additionalShadowsTexture, value);
		}
	}

	public ComputeBufferHandle ShadowCascadesBuffer
	{
		get
		{
			return CheckAndGetComputeBufferHandle(ref shadowCascadesBuffer);
		}
		set
		{
			CheckAndSetComputeBufferHandle(ref shadowCascadesBuffer, value);
		}
	}

	public ComputeBufferHandle ShadowMatricesBuffer
	{
		get
		{
			return CheckAndGetComputeBufferHandle(ref shadowMatricesBuffer);
		}
		set
		{
			CheckAndSetComputeBufferHandle(ref shadowMatricesBuffer, value);
		}
	}

	public ComputeBufferHandle AdditionalShadowDataBuffer
	{
		get
		{
			return CheckAndGetComputeBufferHandle(ref additionalShadowDataBuffer);
		}
		set
		{
			CheckAndSetComputeBufferHandle(ref additionalShadowDataBuffer, value);
		}
	}

	public ComputeBufferHandle DirectionalLightDataBuffer
	{
		get
		{
			return CheckAndGetComputeBufferHandle(ref directionalLightDataBuffer);
		}
		set
		{
			CheckAndSetComputeBufferHandle(ref directionalLightDataBuffer, value);
		}
	}

	public ComputeBufferHandle AdditionalLightDataBuffer
	{
		get
		{
			return CheckAndGetComputeBufferHandle(ref additionalLightDataBuffer);
		}
		set
		{
			CheckAndSetComputeBufferHandle(ref additionalLightDataBuffer, value);
		}
	}

	public ComputeBufferHandle LightTilesBuffer
	{
		get
		{
			return CheckAndGetComputeBufferHandle(ref lightTilesBuffer);
		}
		set
		{
			CheckAndSetComputeBufferHandle(ref lightTilesBuffer, value);
		}
	}

	public Dictionary<Light, Vector4> LightShadowData
	{
		get
		{
			return lightShadowData;
		}
		set
		{
			lightShadowData = value;
		}
	}

	public TextureHandle[] GBuffer
	{
		get
		{
			return CheckAndGetTextureHandle(ref gBuffer);
		}
		set
		{
			CheckAndSetTextureHandle(ref gBuffer, value);
		}
	}

	public TextureHandle CameraOpaqueTexture
	{
		get
		{
			return CheckAndGetTextureHandle(ref cameraOpaqueTexture);
		}
		internal set
		{
			CheckAndSetTextureHandle(ref cameraOpaqueTexture, value);
		}
	}

	public TextureHandle CameraDepthTexture
	{
		get
		{
			return CheckAndGetTextureHandle(ref cameraDepthTexture);
		}
		internal set
		{
			CheckAndSetTextureHandle(ref cameraDepthTexture, value);
		}
	}

	public TextureHandle IndirectDiffuseHandle
	{
		get
		{
			return CheckAndGetTextureHandle(ref indirectDiffuseHandle);
		}
		internal set
		{
			CheckAndSetTextureHandle(ref indirectDiffuseHandle, value);
		}
	}

	public TextureHandle IndirectSpecularHandle
	{
		get
		{
			return CheckAndGetTextureHandle(ref indirectSpecularHandle);
		}
		internal set
		{
			CheckAndSetTextureHandle(ref indirectSpecularHandle, value);
		}
	}

	internal void InitFrame()
	{
		IsAccessible = true;
	}

	internal void EndFrame()
	{
		IsAccessible = false;
	}

	private void CheckAndSetTextureHandle(ref TextureHandle handle, TextureHandle newHandle)
	{
		if (CheckAndWarnAboutAccessibility())
		{
			handle = newHandle;
		}
	}

	private TextureHandle CheckAndGetTextureHandle(ref TextureHandle handle)
	{
		if (CheckAndWarnAboutAccessibility())
		{
			return handle;
		}
		return TextureHandle.nullHandle;
	}

	private void CheckAndSetTextureHandle(ref TextureHandle[] handle, TextureHandle[] newHandle)
	{
		if (CheckAndWarnAboutAccessibility())
		{
			if (handle == null || handle.Length != newHandle.Length)
			{
				handle = new TextureHandle[newHandle.Length];
			}
			for (int i = 0; i < newHandle.Length; i++)
			{
				handle[i] = newHandle[i];
			}
		}
	}

	private TextureHandle[] CheckAndGetTextureHandle(ref TextureHandle[] handle)
	{
		if (!CheckAndWarnAboutAccessibility())
		{
			return new TextureHandle[1] { TextureHandle.nullHandle };
		}
		return handle;
	}

	private ComputeBufferHandle CheckAndGetComputeBufferHandle(ref ComputeBufferHandle handle)
	{
		if (CheckAndWarnAboutAccessibility())
		{
			return handle;
		}
		return ComputeBufferHandle.nullHandle;
	}

	private void CheckAndSetComputeBufferHandle(ref ComputeBufferHandle handle, ComputeBufferHandle newHandle)
	{
		if (CheckAndWarnAboutAccessibility())
		{
			handle = newHandle;
		}
	}

	private bool CheckAndWarnAboutAccessibility()
	{
		if (!IsAccessible)
		{
			Debug.LogError("Trying to access Universal Resources outside of the current frame setup.");
		}
		return IsAccessible;
	}

	public override void Reset()
	{
		backBufferColor = TextureHandle.nullHandle;
		backBufferDepth = TextureHandle.nullHandle;
		cameraColor = TextureHandle.nullHandle;
		cameraDepth = TextureHandle.nullHandle;
		mainShadowsTexture = TextureHandle.nullHandle;
		additionalShadowsTexture = TextureHandle.nullHandle;
		shadowCascadesBuffer = ComputeBufferHandle.nullHandle;
		shadowMatricesBuffer = ComputeBufferHandle.nullHandle;
		additionalShadowDataBuffer = ComputeBufferHandle.nullHandle;
		directionalLightDataBuffer = ComputeBufferHandle.nullHandle;
		additionalLightDataBuffer = ComputeBufferHandle.nullHandle;
		lightTilesBuffer = ComputeBufferHandle.nullHandle;
		cameraOpaqueTexture = TextureHandle.nullHandle;
		cameraDepthTexture = TextureHandle.nullHandle;
		indirectDiffuseHandle = TextureHandle.nullHandle;
		indirectSpecularHandle = TextureHandle.nullHandle;
		for (int i = 0; i < gBuffer.Length; i++)
		{
			gBuffer[i] = TextureHandle.nullHandle;
		}
	}
}
