using UnityEngine;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

[DisallowMultipleComponent]
[RequireComponent(typeof(Camera))]
public class RustRenderPipelineCamera : MonoBehaviour
{
	[SerializeField]
	private CameraSettings settings;

	private ProfilingSampler sampler;

	public CameraSettings Settings => settings ?? (settings = new CameraSettings());

	public ProfilingSampler Sampler => sampler ?? (sampler = new ProfilingSampler(GetComponent<Camera>().name));
}
