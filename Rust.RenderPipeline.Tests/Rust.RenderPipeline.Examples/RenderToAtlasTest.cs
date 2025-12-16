using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Examples;

public class RenderToAtlasTest : MonoBehaviour
{
	private static readonly int atlasTextureId = Shader.PropertyToID("_AtlasTextureTest");

	[SerializeField]
	private Texture2D testTexture;

	private Texture2DAtlas texture2DAtlas;

	private Vector4 scaleOffset;

	private CommandBuffer commandBuffer;

	[ContextMenu("Render To Atlas")]
	private void RenderToAtlas()
	{
		if (texture2DAtlas == null)
		{
			texture2DAtlas = new Texture2DAtlas(1024, 1024, GraphicsFormat.R8G8B8A8_SRGB);
		}
		commandBuffer = new CommandBuffer();
		if (texture2DAtlas.AllocateTexture(commandBuffer, ref scaleOffset, testTexture, testTexture.width, testTexture.height))
		{
			Debug.Log("Texture allocated!");
		}
		texture2DAtlas.UpdateTexture(commandBuffer, testTexture, ref scaleOffset);
		commandBuffer.SetGlobalTexture(atlasTextureId, texture2DAtlas.AtlasTexture);
		Graphics.ExecuteCommandBuffer(commandBuffer);
		commandBuffer.Clear();
		commandBuffer = null;
	}
}
