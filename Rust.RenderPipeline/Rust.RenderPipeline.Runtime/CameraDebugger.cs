using System.Diagnostics;
using Rust.RenderPipeline.Runtime.Passes;
using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public class CameraDebugger
{
	private enum ShaderPasses
	{
		GBufferRendering,
		IndirectLightingRendering,
		ForwardPlusTiles
	}

	private enum GBufferTarget
	{
		None,
		GBuffer0,
		GBuffer1,
		GBuffer2,
		GBuffer3
	}

	private enum DeferredIndirectLightingTarget
	{
		None,
		Diffuse,
		Specular
	}

	private const string FORWARD_PLUS_PANEL_NAME = "Forward+";

	private const string DEFERRED_PANEL_NAME = "Deferred";

	private static readonly int opacityID = Shader.PropertyToID("_DebugOpacity");

	private static readonly int selectedGBufferTextureId = Shader.PropertyToID("_SelectedGBufferTexture");

	private static readonly int showGBufferTargetAlphaId = Shader.PropertyToID("_ShowGBufferTargetAlpha");

	private static readonly int selectedIndirectLightingTextureId = Shader.PropertyToID("_SelectedIndirectLightingTexture");

	private static readonly int showIndirectLightingAlphaId = Shader.PropertyToID("_ShowIndirectLightingAlpha");

	private static Material material;

	private static bool showTiles;

	private static float opacity = 0.5f;

	private static GBufferTarget gBufferTarget;

	private static bool showGBufferTargetAlpha;

	private static DeferredIndirectLightingTarget indirectLightingTarget;

	private static bool showIndirectLightingAlpha;

	public static bool IsActive
	{
		get
		{
			if ((!showTiles || !(opacity > 0f)) && gBufferTarget == GBufferTarget.None)
			{
				return indirectLightingTarget != DeferredIndirectLightingTarget.None;
			}
			return true;
		}
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void Initialize(Material cameraDebuggerMaterial)
	{
		material = cameraDebuggerMaterial;
		DebugManager.instance.GetPanel("Forward+", createIfNull: true).children.Add(new DebugUI.FloatField
		{
			displayName = "Opacity",
			tooltip = "Opacity of the debug overlay.",
			min = () => 0f,
			max = () => 1f,
			getter = () => opacity,
			setter = delegate(float value)
			{
				opacity = value;
			}
		}, new DebugUI.BoolField
		{
			displayName = "Show Tiles",
			tooltip = "Whether the debug overlay is shown.",
			getter = () => showTiles,
			setter = delegate(bool value)
			{
				showTiles = value;
			}
		});
		DebugManager.instance.GetPanel("Deferred", createIfNull: true).children.Add(new DebugUI.EnumField
		{
			displayName = "Show GBuffer Output",
			tooltip = "Displays the output of the selected GBuffer render target.",
			getIndex = () => (int)gBufferTarget,
			setIndex = delegate(int value)
			{
				gBufferTarget = (GBufferTarget)value;
			},
			getter = () => (int)gBufferTarget,
			setter = delegate(int value)
			{
				gBufferTarget = (GBufferTarget)value;
			},
			autoEnum = typeof(GBufferTarget)
		}, new DebugUI.BoolField
		{
			displayName = "Show GBuffer Target Alpha",
			tooltip = "Whether the selected GBuffer target is displaying the alpha of the texture rather than RGB.",
			getter = () => showGBufferTargetAlpha,
			setter = delegate(bool value)
			{
				showGBufferTargetAlpha = value;
			}
		}, new DebugUI.EnumField
		{
			displayName = "Show Deferred Indirect Lighting Target",
			tooltip = "Displays the selected deferred indirect lighting render target.",
			getIndex = () => (int)indirectLightingTarget,
			setIndex = delegate(int value)
			{
				indirectLightingTarget = (DeferredIndirectLightingTarget)value;
			},
			getter = () => (int)indirectLightingTarget,
			setter = delegate(int value)
			{
				indirectLightingTarget = (DeferredIndirectLightingTarget)value;
			},
			autoEnum = typeof(DeferredIndirectLightingTarget)
		}, new DebugUI.BoolField
		{
			displayName = "Show Indirect Lighting Target Alpha",
			tooltip = "Whether the selected Indirect Lighting target is displaying the alpha of the texture rather than RGB.",
			getter = () => showIndirectLightingAlpha,
			setter = delegate(bool value)
			{
				showIndirectLightingAlpha = value;
			}
		});
	}

	private static void DrawFullscreenEffect(CommandBuffer cmd, ShaderPasses pass)
	{
		cmd.DrawProcedural(Matrix4x4.identity, material, (int)pass, MeshTopology.Triangles, 3);
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void Render(RenderGraphContext context, DebugPass pass)
	{
		CommandBuffer cmd = context.cmd;
		if (gBufferTarget != GBufferTarget.None)
		{
			cmd.SetGlobalTexture(selectedGBufferTextureId, pass.gBufferHandles[(int)(gBufferTarget - 1)]);
			cmd.SetGlobalInt(showGBufferTargetAlphaId, showGBufferTargetAlpha ? 1 : 0);
			DrawFullscreenEffect(cmd, ShaderPasses.GBufferRendering);
		}
		if (indirectLightingTarget != DeferredIndirectLightingTarget.None)
		{
			TextureHandle textureHandle = indirectLightingTarget switch
			{
				DeferredIndirectLightingTarget.Diffuse => pass.indirectDiffuseHandle, 
				DeferredIndirectLightingTarget.Specular => pass.indirectSpecularHandle, 
				DeferredIndirectLightingTarget.None => TextureHandle.nullHandle, 
				_ => TextureHandle.nullHandle, 
			};
			cmd.SetGlobalTexture(selectedIndirectLightingTextureId, textureHandle);
			cmd.SetGlobalInt(showIndirectLightingAlphaId, showIndirectLightingAlpha ? 1 : 0);
			DrawFullscreenEffect(cmd, ShaderPasses.IndirectLightingRendering);
		}
		if (showTiles)
		{
			cmd.SetGlobalFloat(opacityID, opacity);
			DrawFullscreenEffect(cmd, ShaderPasses.ForwardPlusTiles);
		}
		context.renderContext.ExecuteCommandBuffer(cmd);
		cmd.Clear();
	}

	[Conditional("DEVELOPMENT_BUILD")]
	[Conditional("UNITY_EDITOR")]
	public static void Cleanup()
	{
		DebugManager.instance.RemovePanel("Forward+");
		DebugManager.instance.RemovePanel("Deferred");
	}
}
