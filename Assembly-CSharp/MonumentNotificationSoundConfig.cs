using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rust/MonumentNotificationSoundConfig")]
public class MonumentNotificationSoundConfig : BaseScriptableObject
{
	public enum MonumentType
	{
		Undefined,
		Excavator,
		SmallOilRig,
		LargeOilRig,
		CargoShip
	}

	[Serializable]
	public class Data
	{
		[HideInInspector]
		public MonumentType MonumentType;

		public SoundDefinition NotificationSound;
	}

	private static MonumentNotificationSoundConfig _instance;

	public List<Data> entries = new List<Data>();

	public static MonumentNotificationSoundConfig instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FileSystem.Load<MonumentNotificationSoundConfig>("assets/content/sound/monuments/monumentnotificationsoundconfig.asset");
			}
			if (_instance == null)
			{
				Debug.LogError("Failed to load MonumentNotificationSoundConfig");
			}
			return _instance;
		}
	}
}
