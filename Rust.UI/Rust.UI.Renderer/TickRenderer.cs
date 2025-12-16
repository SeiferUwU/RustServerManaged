using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rust.UI.Renderer;

public class TickRenderer : MaskableGraphic
{
	public struct Tick
	{
		public float Pos;

		public Color Color;

		public float Height;
	}

	public List<Tick> Ticks = new List<Tick>();

	private static UIVertex[] quad = new UIVertex[4];

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		vh.Clear();
		float height = (base.transform as RectTransform).GetHeight();
		foreach (Tick tick in Ticks)
		{
			UIVertex uIVertex = new UIVertex
			{
				color = tick.Color * color,
				position = new Vector3(tick.Pos, 0f)
			};
			quad[0] = uIVertex;
			uIVertex.position = new Vector3(tick.Pos + 1f, 0f);
			quad[1] = uIVertex;
			uIVertex.position = new Vector3(tick.Pos + 1f, height * tick.Height);
			quad[2] = uIVertex;
			uIVertex.position = new Vector3(tick.Pos, height * tick.Height);
			quad[3] = uIVertex;
			vh.AddUIVertexQuad(quad);
		}
	}
}
