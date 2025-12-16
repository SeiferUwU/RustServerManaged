using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace Rust.RenderPipeline.Runtime;

public static class PipelineUtils
{
	private static Dictionary<CameraEvent, string> cameraEventToString;

	public static Dictionary<CameraEvent, string> CameraEventToString
	{
		get
		{
			if (cameraEventToString != null)
			{
				return cameraEventToString;
			}
			cameraEventToString = Enum.GetValues(typeof(CameraEvent)).Cast<CameraEvent>().ToDictionary((CameraEvent k) => k, (CameraEvent v) => v.ToString());
			return cameraEventToString;
		}
	}
}
