using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

public class CoverImage : MaskableGraphic
{
	[SerializeField]
	private Sprite _sprite;

	[SerializeField]
	private Texture _texture;

	public float scaleFactor = 1f;

	public Sprite sprite
	{
		get
		{
			return _sprite;
		}
		set
		{
			if (!(value == _sprite))
			{
				_sprite = value;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public Texture texture
	{
		get
		{
			return _texture;
		}
		set
		{
			if (!(value == texture))
			{
				_texture = value;
				_sprite = null;
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}
	}

	public override Texture mainTexture
	{
		get
		{
			if (_sprite != null)
			{
				return _sprite.texture;
			}
			if (_texture != null)
			{
				return _texture;
			}
			return Graphic.s_WhiteTexture;
		}
	}

	public CoverImage()
	{
		base.useLegacyMeshGeneration = false;
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		if (_sprite != null && _sprite.texture != null)
		{
			Rect dst = GetPixelAdjustedRect();
			Vector4 vector = new Vector4(dst.x, dst.y, dst.x + dst.width, dst.y + dst.height);
			Color32 color = this.color;
			Rect spriteCoverRect = GetSpriteCoverRect01(in dst, _sprite.rect.size);
			spriteCoverRect = ScaleUVRectAroundCenter(spriteCoverRect, 1f / scaleFactor);
			Vector4 outerUV = DataUtility.GetOuterUV(_sprite);
			float x = Mathf.Lerp(outerUV.x, outerUV.z, spriteCoverRect.xMin);
			float y = Mathf.Lerp(outerUV.y, outerUV.w, spriteCoverRect.yMin);
			float x2 = Mathf.Lerp(outerUV.x, outerUV.z, spriteCoverRect.xMax);
			float y2 = Mathf.Lerp(outerUV.y, outerUV.w, spriteCoverRect.yMax);
			vh.Clear();
			vh.AddVert(new Vector3(vector.x, vector.y), color, new Vector2(x, y));
			vh.AddVert(new Vector3(vector.x, vector.w), color, new Vector2(x, y2));
			vh.AddVert(new Vector3(vector.z, vector.w), color, new Vector2(x2, y2));
			vh.AddVert(new Vector3(vector.z, vector.y), color, new Vector2(x2, y));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}
		else if (_texture != null)
		{
			Rect dst2 = GetPixelAdjustedRect();
			Vector4 vector2 = new Vector4(dst2.x, dst2.y, dst2.x + dst2.width, dst2.y + dst2.height);
			Color32 color2 = this.color;
			Rect textureCoverRect = GetTextureCoverRect(in dst2, _texture);
			textureCoverRect = ScaleUVRectAroundCenter(textureCoverRect, 1f / scaleFactor);
			float num = (float)_texture.width * _texture.texelSize.x;
			float num2 = (float)_texture.height * _texture.texelSize.y;
			vh.Clear();
			vh.AddVert(new Vector3(vector2.x, vector2.y), color2, new Vector2(textureCoverRect.xMin * num, textureCoverRect.yMin * num2));
			vh.AddVert(new Vector3(vector2.x, vector2.w), color2, new Vector2(textureCoverRect.xMin * num, textureCoverRect.yMax * num2));
			vh.AddVert(new Vector3(vector2.z, vector2.w), color2, new Vector2(textureCoverRect.xMax * num, textureCoverRect.yMax * num2));
			vh.AddVert(new Vector3(vector2.z, vector2.y), color2, new Vector2(textureCoverRect.xMax * num, textureCoverRect.yMin * num2));
			vh.AddTriangle(0, 1, 2);
			vh.AddTriangle(2, 3, 0);
		}
		else
		{
			base.OnPopulateMesh(vh);
		}
	}

	private static Rect GetCoverRect01(in Rect dst, Vector2 imgPixelSize)
	{
		Vector2 size = dst.size;
		Vector2 vector = imgPixelSize;
		float num = size.x / size.y;
		float num2 = vector.x / vector.y;
		float num3 = ((num >= num2) ? (size.x / vector.x) : (size.y / vector.y));
		float num4 = vector.x * num3;
		float num5 = vector.y * num3;
		return new Rect
		{
			x = (num4 - size.x) / 2f / num4,
			y = (num5 - size.y) / 2f / num5,
			width = size.x / num4,
			height = size.y / num5
		};
	}

	private Rect GetSpriteCoverRect01(in Rect dst, Vector2 spritePixelSize)
	{
		return GetCoverRect01(in dst, spritePixelSize);
	}

	private Rect GetTextureCoverRect(in Rect dst, Texture texture)
	{
		return GetCoverRect01(in dst, new Vector2(texture.width, texture.height));
	}

	protected override void OnDidApplyAnimationProperties()
	{
		SetMaterialDirty();
		SetVerticesDirty();
	}

	protected override void OnRectTransformDimensionsChange()
	{
		SetVerticesDirty();
	}

	private Rect ScaleUVRectAroundCenter(Rect uvRect, float scale)
	{
		Vector2 center = uvRect.center;
		Vector2 vector = uvRect.size * scale;
		return new Rect(center - vector * 0.5f, vector);
	}
}
