using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.PostProcessing;

public class ColorLutPass
{
	private static readonly ProfilingSampler sampler = new ProfilingSampler("Color LUT");

	private static readonly int colorGradingLutId = Shader.PropertyToID("_ColorGradingLUT");

	private static readonly int colorAdjustmentsId = Shader.PropertyToID("_ColorAdjustments");

	private static readonly int colorFilterId = Shader.PropertyToID("_ColorFilter");

	private static readonly int whiteBalanceId = Shader.PropertyToID("_WhiteBalance");

	private static readonly int splitToningShadowsId = Shader.PropertyToID("_SplitToningShadows");

	private static readonly int splitToningHighlightsId = Shader.PropertyToID("_SplitToningHighlights");

	private static readonly int channelMixerRedId = Shader.PropertyToID("_ChannelMixerRed");

	private static readonly int channelMixerGreenId = Shader.PropertyToID("_ChannelMixerGreen");

	private static readonly int channelMixerBlueId = Shader.PropertyToID("_ChannelMixerBlue");

	private static readonly int smhShadowsId = Shader.PropertyToID("_SMHShadows");

	private static readonly int smhMidtonesId = Shader.PropertyToID("_SMHMidtones");

	private static readonly int smhHighlightsId = Shader.PropertyToID("_SMHHighlights");

	private static readonly int smhRangeId = Shader.PropertyToID("_SMHRange");

	private static readonly int colorGradingLutParametersId = Shader.PropertyToID("_ColorGradingLUTParameters");

	private static readonly int colorGradingLutInLogCId = Shader.PropertyToID("_ColorGradingLUTInLogC");

	private static readonly GraphicsFormat colorFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.HDR);

	private int colorLutResolution;

	private TextureHandle colorLut;

	private PostFXSettings postFXSettings;

	private PostFXBlitter postFXBlitter;

	private bool allowHDR;

	private void ConfigureColorAdjustments(CommandBuffer commandBuffer, PostFXSettings settings)
	{
		PostFXSettings.ColorAdjustmentsSettings colorAdjustments = settings.ColorAdjustments;
		commandBuffer.SetGlobalVector(colorAdjustmentsId, new Vector4(Mathf.Pow(2f, colorAdjustments.postExposure), colorAdjustments.contrast * 0.01f + 1f, colorAdjustments.hueShift * 0.0027777778f, colorAdjustments.saturation * 0.01f + 1f));
		commandBuffer.SetGlobalColor(colorFilterId, colorAdjustments.colorFilter.linear);
	}

	private void ConfigureWhiteBalance(CommandBuffer commandBuffer, PostFXSettings settings)
	{
		PostFXSettings.WhiteBalanceSettings whiteBalance = settings.WhiteBalance;
		commandBuffer.SetGlobalVector(whiteBalanceId, ColorUtils.ColorBalanceToLMSCoeffs(whiteBalance.temperature, whiteBalance.tint));
	}

	private void ConfigureSplitToning(CommandBuffer commandBuffer, PostFXSettings settings)
	{
		PostFXSettings.SplitToningSettings splitToning = settings.SplitToning;
		Color shadows = splitToning.shadows;
		shadows.a = splitToning.balance * 0.01f;
		commandBuffer.SetGlobalColor(splitToningShadowsId, shadows);
		commandBuffer.SetGlobalColor(splitToningHighlightsId, splitToning.highlights);
	}

	private void ConfigureChannelMixer(CommandBuffer commandBuffer, PostFXSettings settings)
	{
		PostFXSettings.ChannelMixerSettings channelMixer = settings.ChannelMixer;
		commandBuffer.SetGlobalVector(channelMixerRedId, channelMixer.red);
		commandBuffer.SetGlobalVector(channelMixerGreenId, channelMixer.green);
		commandBuffer.SetGlobalVector(channelMixerBlueId, channelMixer.blue);
	}

	private void ConfigureShadowsMidtonesHighlights(CommandBuffer commandBuffer, PostFXSettings settings)
	{
		PostFXSettings.ShadowsMidtonesHighlightsSettings shadowsMidtonesHighlights = settings.ShadowsMidtonesHighlights;
		commandBuffer.SetGlobalColor(smhShadowsId, shadowsMidtonesHighlights.shadows.linear);
		commandBuffer.SetGlobalColor(smhMidtonesId, shadowsMidtonesHighlights.midtones.linear);
		commandBuffer.SetGlobalColor(smhHighlightsId, shadowsMidtonesHighlights.highlights.linear);
		commandBuffer.SetGlobalVector(smhRangeId, new Vector4(shadowsMidtonesHighlights.shadowsStart, shadowsMidtonesHighlights.shadowsEnd, shadowsMidtonesHighlights.highlightsStart, shadowsMidtonesHighlights.highLightsEnd));
	}

	private void Render(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		ConfigureColorAdjustments(cmd, postFXSettings);
		ConfigureWhiteBalance(cmd, postFXSettings);
		ConfigureSplitToning(cmd, postFXSettings);
		ConfigureChannelMixer(cmd, postFXSettings);
		ConfigureShadowsMidtonesHighlights(cmd, postFXSettings);
		int num = colorLutResolution;
		int num2 = num * num;
		cmd.SetGlobalVector(colorGradingLutParametersId, new Vector4(num, 0.5f / (float)num2, 0.5f / (float)num, (float)num / ((float)num - 1f)));
		PostFXSettings.ToneMappingSettings.Mode mode = postFXSettings.ToneMapping.mode;
		PostFXPass.Pass pass = (PostFXPass.Pass)(7 + mode);
		cmd.SetGlobalFloat(colorGradingLutInLogCId, (allowHDR && pass != PostFXPass.Pass.ColorGradingNone) ? 1f : 0f);
		postFXBlitter.Draw(cmd, colorLut, pass);
		cmd.SetGlobalVector(colorGradingLutParametersId, new Vector4(1f / (float)num2, 1f / (float)num, (float)num - 1f));
		cmd.SetGlobalTexture(colorGradingLutId, colorLut);
	}

	public static TextureHandle Record(RenderGraph renderGraph, ContextContainer frameData, PostFXBlitter postFXBlitter)
	{
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		int num = (int)frameData.Get<RustRenderingContext>().pipelineSettings.colorLutResolution;
		ColorLutPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<ColorLutPass>(sampler.name, out passData, sampler);
		passData.colorLutResolution = num;
		passData.postFXSettings = rustCameraContext.PostFXSettings;
		passData.allowHDR = rustCameraContext.CameraBufferSettings.allowHDR;
		passData.postFXBlitter = postFXBlitter;
		int num2 = num;
		int width = num2 * num2;
		TextureDesc textureDesc = new TextureDesc(width, num2);
		textureDesc.colorFormat = colorFormat;
		textureDesc.name = "Color LUT";
		TextureDesc desc = textureDesc;
		passData.colorLut = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc));
		renderGraphBuilder.SetRenderFunc(delegate(ColorLutPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
		return passData.colorLut;
	}
}
