namespace Rust.RenderPipeline.Runtime;

public enum RenderPassEvent
{
	BeforeRendering = 0,
	BeforeRenderingShadows = 50,
	AfterRenderingShadows = 100,
	BeforeRenderingPrePasses = 150,
	AfterRenderingPrePasses = 200,
	BeforeRenderingGBuffer = 210,
	AfterRenderingGBuffer = 220,
	BeforeRenderingDeferredLights = 230,
	BeforeRenderingDeferredIndirectLighting = 240,
	AfterRenderingDeferredIndirectLighting = 250,
	BeforeRenderingCombinedIndirectLighting = 260,
	AfterRenderingCombinedIndirectLighting = 270,
	AfterRenderingDeferredLights = 280,
	BeforeRenderingOpaques = 290,
	AfterRenderingOpaques = 300,
	BeforeRenderingSkybox = 350,
	AfterRenderingSkybox = 400,
	BeforeRenderingTransparents = 450,
	AfterRenderingTransparents = 500,
	BeforeRenderingPostProcessing = 550,
	AfterRenderingPostProcessing = 600,
	AfterRendering = 1000
}
