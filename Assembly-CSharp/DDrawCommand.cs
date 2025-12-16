using UnityEngine;

public class DDrawCommand
{
	public static string Sphere(Vector3 position, float duration, Color color, float radius, bool distanceFade = true, bool zTest = true)
	{
		return ConsoleSystem.BuildCommand("ddraw.sphere", duration, color, position, radius, distanceFade, zTest);
	}

	public static string Box(Vector3 position, float duration, Color color, Vector3 size, Quaternion rotation = default(Quaternion), bool distanceFade = true, bool zTest = true)
	{
		return ConsoleSystem.BuildCommand("ddraw.box", duration, color, position, size.ToString(), ((rotation != default(Quaternion)) ? rotation : Quaternion.identity).eulerAngles, distanceFade, zTest);
	}

	public static string Text(Vector3 position, float duration, Color color, string text, bool distanceFade = true, bool zTest = false)
	{
		return ConsoleSystem.BuildCommand("ddraw.text", duration, color, position, text, distanceFade, zTest);
	}
}
