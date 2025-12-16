using UnityEngine;

namespace Rust.Rendering.IndirectInstancing;

[DefaultExecutionOrder(-1)]
public class IndirectInstancingCamera : SingletonComponent<IndirectInstancingCamera>
{
	public Shader[] supportedShaders;
}
