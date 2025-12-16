using Rust.RenderPipeline.Runtime.RenderingContext;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime.Passes.PostProcessing;

public class BloomPass
{
	private const int MAX_BLOOM_PYRAMID_LEVELS = 16;

	private static readonly ProfilingSampler sampler = new ProfilingSampler("Bloom");

	private static readonly int bicubicUpsamplingId = Shader.PropertyToID("_BloomBicubicUpsampling");

	private static readonly int intensityId = Shader.PropertyToID("_BloomIntensity");

	private static readonly int thresholdId = Shader.PropertyToID("_BloomThreshold");

	private static readonly int fxSource2Id = Shader.PropertyToID("_PostFXSource2");

	private readonly TextureHandle[] pyramid = new TextureHandle[33];

	private TextureHandle colorSource;

	private TextureHandle bloomResult;

	private PostFXBlitter postFXBlitter;

	private PostFXSettings.BloomSettings bloomSettings;

	private int stepCount;

	private void Render(RenderGraphContext context)
	{
		CommandBuffer cmd = context.cmd;
		Vector4 value = default(Vector4);
		value.x = Mathf.GammaToLinearSpace(bloomSettings.threshold);
		value.y = value.x * bloomSettings.thresholdKnee;
		value.z = 2f * value.y;
		value.w = 0.25f / (value.y + 1E-05f);
		value.y -= value.x;
		cmd.SetGlobalVector(thresholdId, value);
		postFXBlitter.Draw(cmd, colorSource, pyramid[0], bloomSettings.fadeFireflies ? PostFXPass.Pass.BloomPrefilterFireflies : PostFXPass.Pass.BloomPrefilter);
		int num = 0;
		int num2 = 2;
		int i;
		for (i = 0; i < stepCount; i++)
		{
			int num3 = num2 - 1;
			postFXBlitter.Draw(cmd, pyramid[num], pyramid[num3], PostFXPass.Pass.BloomHorizontal);
			postFXBlitter.Draw(cmd, pyramid[num3], pyramid[num2], PostFXPass.Pass.BloomVertical);
			num = num2;
			num2 += 2;
		}
		cmd.SetGlobalFloat(bicubicUpsamplingId, bloomSettings.bicubicUpsampling ? 1f : 0f);
		PostFXPass.Pass pass;
		PostFXPass.Pass pass2;
		float value2;
		if (bloomSettings.mode == PostFXSettings.BloomSettings.Mode.Additive)
		{
			pass = (pass2 = PostFXPass.Pass.BloomAdd);
			cmd.SetGlobalFloat(intensityId, 1f);
			value2 = bloomSettings.intensity;
		}
		else
		{
			pass = PostFXPass.Pass.BloomScatter;
			pass2 = PostFXPass.Pass.BloomScatterFinal;
			cmd.SetGlobalFloat(intensityId, bloomSettings.scatter);
			value2 = Mathf.Min(bloomSettings.intensity, 1f);
		}
		if (i > 1)
		{
			num2 -= 5;
			for (i--; i > 0; i--)
			{
				cmd.SetGlobalTexture(fxSource2Id, pyramid[num2 + 1]);
				postFXBlitter.Draw(cmd, pyramid[num], pyramid[num2], pass);
				num = num2;
				num2 -= 2;
			}
		}
		cmd.SetGlobalFloat(intensityId, value2);
		cmd.SetGlobalTexture(fxSource2Id, colorSource);
		postFXBlitter.Draw(cmd, pyramid[num], bloomResult, pass2);
	}

	public static TextureHandle Record(RenderGraph renderGraph, ContextContainer frameData, PostFXBlitter postFXBlitter)
	{
		RustResourceDataContext rustResourceDataContext = frameData.Get<RustResourceDataContext>();
		RustCameraContext rustCameraContext = frameData.Get<RustCameraContext>();
		PostFXSettings.BloomSettings bloom = rustCameraContext.PostFXSettings.Bloom;
		Camera camera = rustCameraContext.Camera;
		Vector2Int vector2Int = (bloom.ignoreRenderScale ? new Vector2Int(camera.pixelWidth, camera.pixelHeight) : rustCameraContext.CameraBufferSize) / 2;
		if (bloom.maxIterations == 0 || bloom.intensity <= 0f || vector2Int.y < bloom.downscaleLimit * 2 || vector2Int.x < bloom.downscaleLimit * 2)
		{
			return rustResourceDataContext.CameraColor;
		}
		BloomPass passData;
		using RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<BloomPass>(sampler.name, out passData, sampler);
		passData.postFXBlitter = postFXBlitter;
		passData.colorSource = renderGraphBuilder.ReadTexture(rustResourceDataContext.CameraColor);
		passData.bloomSettings = bloom;
		TextureDesc textureDesc = new TextureDesc(vector2Int.x, vector2Int.y);
		textureDesc.colorFormat = SystemInfo.GetGraphicsFormat(rustCameraContext.CameraBufferSettings.allowHDR ? DefaultFormat.HDR : DefaultFormat.LDR);
		textureDesc.name = "Bloom Prefilter";
		TextureDesc desc = textureDesc;
		TextureHandle[] array = passData.pyramid;
		array[0] = renderGraphBuilder.CreateTransientTexture(in desc);
		vector2Int /= 2;
		int num = 1;
		int num2 = 0;
		while (num2 < bloom.maxIterations && vector2Int.y >= bloom.downscaleLimit && vector2Int.x >= bloom.downscaleLimit)
		{
			desc.width = vector2Int.x;
			desc.height = vector2Int.y;
			desc.name = "Bloom Pyramid H";
			array[num] = renderGraphBuilder.CreateTransientTexture(in desc);
			desc.name = "Bloom Pyramid V";
			array[num + 1] = renderGraphBuilder.CreateTransientTexture(in desc);
			vector2Int /= 2;
			num2++;
			num += 2;
		}
		passData.stepCount = num2;
		desc.width = rustCameraContext.CameraBufferSize.x;
		desc.height = rustCameraContext.CameraBufferSize.y;
		desc.name = "Bloom Result";
		passData.bloomResult = renderGraphBuilder.WriteTexture(renderGraph.CreateTexture(in desc));
		renderGraphBuilder.SetRenderFunc(delegate(BloomPass pass, RenderGraphContext context)
		{
			pass.Render(context);
		});
		return passData.bloomResult;
	}
}
