using System;
using System.Linq;
using Rust.Workshop;
using UnityEngine;

[CreateAssetMenu(menuName = "Rust/Skins/Skinnable")]
public class Skinnable : ScriptableObject
{
	[Serializable]
	public class Group
	{
		public string Name = "MAIN";

		public Material Material;

		public int MaxTextureSize = 1024;
	}

	public string Name;

	public string ItemName;

	public GameObject EntityPrefab;

	public string EntityPrefabName;

	public GameObject EntityFemalePrefab;

	public string EntityFemalePrefabName;

	public GameObject ViewmodelPrefab;

	public string ViewmodelPrefabName;

	public Mesh[] MeshDownloads;

	public string[] MeshDownloadPaths;

	public Category Category;

	public bool HideInWorkshopUpload;

	[Header("Workshop Preview settings")]
	public float WorkshopDefaultZoom;

	public Vector3 WorkshopDefaultRotationOffset = Vector3.zero;

	public Group[] Groups;

	public static Skinnable[] All;

	[NonSerialized]
	private Material[] _sourceMaterials;

	public Material[] SourceMaterials
	{
		get
		{
			if (_sourceMaterials == null)
			{
				_sourceMaterials = new Material[Groups.Length];
				for (int i = 0; i < Groups.Length; i++)
				{
					_sourceMaterials[i] = Groups[i].Material;
				}
			}
			return _sourceMaterials;
		}
	}

	public static Skinnable FindForItem(string itemType)
	{
		return All.FirstOrDefault((Skinnable x) => string.Compare(x.ItemName, itemType, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(x.Name, itemType, StringComparison.OrdinalIgnoreCase) == 0);
	}

	public static Skinnable FindForEntity(string entityName)
	{
		return All.FirstOrDefault((Skinnable x) => string.Compare(x.EntityPrefabName, entityName, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare(x.EntityFemalePrefabName, entityName, StringComparison.OrdinalIgnoreCase) == 0);
	}
}
