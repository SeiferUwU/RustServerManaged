using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.PostProcessing;

public class PostFXPass
{
	public enum Pass
	{
		BloomPrefilter,
		BloomPrefilterFireflies,
		BloomHorizontal,
		BloomVertical,
		BloomAdd,
		BloomScatter,
		BloomScatterFinal,
		ColorGradingNone,
		ColorGradingACES,
		ColorGradingNeutral,
		ColorGradingReinhard,
		ApplyColorGrading,
		ApplyColorGradingWithLuma,
		FXAA,
		FXAAWithLuma,
		FinalRescale,
		Copy
	}

	private enum ScaleMode
	{
		None,
		Bilinear,
		Bicubic
	}

	private static readonly ProfilingSampler groupSampler = new ProfilingSampler("Post FX");

	private static readonly ProfilingSampler finalSampler = new ProfilingSampler("Final Post FX");

	private static readonly int copyBicubicId = Shader.PropertyToID("_CopyBicubic");

	private static readonly int fxaaConfigId = Shader.PropertyToID("_FXAAConfig");

	private static readonly int finalSrcBlendId = Shader.PropertyToID("_FinalSrcBlend");

	private static readonly int finalDstBlendId = Shader.PropertyToID("_FinalDstBlend");

	private static readonly GlobalKeyword fxaaLowKeyword = GlobalKeyword.Create("FXAA_QUALITY_LOW");

	private static readonly GlobalKeyword fxaaMediumKeyword = GlobalKeyword.Create("FXAA_QUALITY_MEDIUM");

	private static readonly GraphicsFormat colorFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);

	private bool keepAlpha;

	private ScaleMode scaleMode;

	private TextureHandle colorSource;

	private TextureHandle colorGradingResult;

	private TextureHandle scaledResult;

	private CameraBufferSettings.FxaaSettings fxaaSettings;

	private PostFXBlitter postFXBlitter;

	private void ConfigureFxaa(CommandBuffer commandBuffer)
	{
		commandBuffer.SetKeyword(in fxaaLowKeyword, fxaaSettings.quality == CameraBufferSettings.FxaaSettings.Quality.Low);
		commandBuffer.SetKeyword(in fxaaMediumKeyword, fxaaSettings.quality == CameraBufferSettings.FxaaSettings.Quality.Medium);
		commandBuffer.SetGlobalVector(fxaaConfigId, new Vector4(fxaaSettings.fixedThreshold, fxaaSettings.relativeThreshold, fxaaSettings.subpixelBlending));
	}

	private void Render(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		cmd.SetGlobalFloat(finalSrcBlendId, 1f);
		cmd.SetGlobalFloat(finalDstBlendId, 0f);
		RenderTargetIdentifier renderTargetIdentifier;
		Pass pass;
		if (fxaaSettings.enabled)
		{
			renderTargetIdentifier = colorGradingResult;
			pass = (keepAlpha ? Pass.FXAA : Pass.FXAAWithLuma);
			ConfigureFxaa(cmd);
			postFXBlitter.Draw(cmd, colorSource, renderTargetIdentifier, keepAlpha ? Pass.ApplyColorGrading : Pass.ApplyColorGradingWithLuma);
		}
		else
		{
			renderTargetIdentifier = colorSource;
			pass = Pass.ApplyColorGrading;
		}
		if (scaleMode == ScaleMode.None)
		{
			postFXBlitter.DrawFinal(cmd, renderTargetIdentifier, pass);
		}
		else
		{
			postFXBlitter.Draw(cmd, renderTargetIdentifier, scaledResult, pass);
			cmd.SetGlobalFloat(copyBicubicId, (scaleMode == ScaleMode.Bicubic) ? 1f : 0f);
			postFXBlitter.DrawFinal(cmd, scaledResult, Pass.FinalRescale);
		}
		context.renderContext.ExecuteCommandBuffer(cmd);
		cmd.Clear();
	}

	public static void Record(RenderGraph renderGraph, ContextContainer frameData)
	{
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		using (new RenderGraphProfilingScope(renderGraph, groupSampler))
		{
			bool flag = rustCameraContext.CameraSettings.keepAlpha;
			PostFXBlitter postFXBlitter = new PostFXBlitter(frameData);
			TextureHandle input = BloomPass.Record(renderGraph, frameData, postFXBlitter);
			TextureHandle input2 = ColorLutPass.Record(renderGraph, frameData, postFXBlitter);
			PostFXPass passData;
			using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<PostFXPass>(finalSampler.name, out passData, finalSampler);
			passData.keepAlpha = flag;
			passData.colorSource = renderGraphBuilder.ReadTexture(in input);
			passData.fxaaSettings = rustCameraContext.CameraBufferSettings.fxaaSettings;
			passData.postFXBlitter = postFXBlitter;
			renderGraphBuilder.ReadTexture(in input2);
			if (rustCameraContext.CameraBufferSize.x == rustCameraContext.Camera.pixelWidth)
			{
				passData.scaleMode = ScaleMode.None;
			}
			else
			{
				passData.scaleMode = ((rustCameraContext.CameraBufferSettings.bicubicRescaling != CameraBufferSettings.BicubicRescalingMode.UpAndDown && (rustCameraContext.CameraBufferSettings.bicubicRescaling != CameraBufferSettings.BicubicRescalingMode.UpOnly || rustCameraContext.CameraBufferSize.x >= rustCameraContext.Camera.pixelWidth)) ? ScaleMode.Bilinear : ScaleMode.Bicubic);
			}
			bool enabled = rustCameraContext.CameraBufferSettings.fxaaSettings.enabled;
			if (enabled || passData.scaleMode != ScaleMode.None)
			{
				TextureDesc textureDesc = new TextureDesc(rustCameraContext.CameraBufferSize.x, rustCameraContext.CameraBufferSize.y);
				textureDesc.colorFormat = colorFormat;
				TextureDesc desc = textureDesc;
				if (enabled)
				{
					desc.name = "Color Grading Result";
					passData.colorGradingResult = renderGraphBuilder.CreateTransientTexture(in desc);
				}
				if (passData.scaleMode != ScaleMode.None)
				{
					desc.name = "Scaled Result";
					passData.scaledResult = renderGraphBuilder.CreateTransientTexture(in desc);
				}
			}
			renderGraphBuilder.SetRenderFunc(delegate(PostFXPass pass, RenderGraphContext context)
			{
				pass.Render(context);
			});
		}
	}
}
