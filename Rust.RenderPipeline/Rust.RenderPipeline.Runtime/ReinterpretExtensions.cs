using System.Runtime.InteropServices;

namespace Rust.RenderPipeline.Runtime;

public static class ReinterpretExtensions
{
	[StructLayout(LayoutKind.Explicit)]
	private struct IntFloat
	{
		[FieldOffset(0)]
		public int intValue;

		[FieldOffset(0)]
		public float floatValue;
	}

	public static float ReinterpretAsFloat(this int value)
	{
		IntFloat intFloat = new IntFloat
		{
			intValue = value
		};
		return intFloat.floatValue;
	}
}
