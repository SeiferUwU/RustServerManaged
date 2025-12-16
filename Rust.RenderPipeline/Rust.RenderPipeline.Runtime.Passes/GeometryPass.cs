using System;
using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

namespace Rust.RenderPipeline.Runtime.Passes;

public class GeometryPass
{
	private static readonly ProfilingSampler profilingSamplerOpaque = new ProfilingSampler("Opaque Geometry");

	private static readonly ProfilingSampler profilingSamplerTransparent = new ProfilingSampler("Transparent Geometry");

	private static readonly ShaderTagId[] shaderTagIds = new ShaderTagId[4]
	{
		new ShaderTagId("SRPDefaultUnlit"),
		new ShaderTagId("RustLit"),
		new ShaderTagId("ForwardBase"),
		new ShaderTagId("Vertex")
	};

	private RendererListHandle renderListHandle;

	private void Render(RenderGraphContext context)
	{
		context.cmd.DrawRendererList(renderListHandle);
		context.renderContext.ExecuteCommandBuffer(context.cmd);
		context.cmd.Clear();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData, bool opaque)
	{
		RustRenderingContext rustRenderingContext = frameData.Get<RustRenderingContext>();
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		int renderingLayerMask = rustCameraContext.CameraSettings.renderingLayerMask;
		ProfilingSampler profilingSampler = (opaque ? profilingSamplerOpaque : profilingSamplerTransparent);
		GeometryPass passData;
		RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<GeometryPass>(profilingSampler.name, out passData, profilingSampler);
		try
		{
			rustRenderingContext.perObjectData = PerObjectData.LightProbe | PerObjectData.ReflectionProbes;
			GeometryPass geometryPass = passData;
			RendererListDesc desc = new RendererListDesc(shaderTagIds, rustRenderingContext.cullResults, rustCameraContext.Camera)
			{
				sortingCriteria = (opaque ? SortingCriteria.CommonOpaque : SortingCriteria.CommonTransparent),
				rendererConfiguration = rustRenderingContext.perObjectData,
				renderQueueRange = (opaque ? RenderQueueRange.opaque : RenderQueueRange.transparent),
				renderingLayerMask = (uint)renderingLayerMask
			};
			geometryPass.renderListHandle = renderGraphBuilder.UseRendererList(renderGraph.CreateRendererList(in desc));
			renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraColor);
			renderGraphBuilder.ReadWriteTexture(rustResourceDataContext.CameraDepth);
			if (!opaque)
			{
				if (rustResourceDataContext.CameraOpaqueTexture.IsValid())
				{
					renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraOpaqueTexture);
				}
				if (rustResourceDataContext.CameraDepthTexture.IsValid())
				{
					renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraDepthTexture);
				}
			}
			renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.DirectionalLightDataBuffer);
			renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.AdditionalLightDataBuffer);
			if (rustResourceDataContext.LightTilesBuffer.IsValid())
			{
				renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.LightTilesBuffer);
			}
			renderGraphBuilder.ReadTexture(rustResourceDataContext.MainShadowsTexture);
			renderGraphBuilder.ReadTexture(rustResourceDataContext.AdditionalShadowsTexture);
			renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.ShadowCascadesBuffer);
			renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.ShadowMatricesBuffer);
			renderGraphBuilder.ReadComputeBuffer(rustResourceDataContext.AdditionalShadowDataBuffer);
			renderGraphBuilder.SetRenderFunc(delegate(GeometryPass pass, RenderGraphContext context)
			{
				pass.Render(context);
			});
		}
		finally
		{
			((IDisposable)renderGraphBuilder/*cast due to .constrained prefix*/).Dispose();
		}
	}
}
