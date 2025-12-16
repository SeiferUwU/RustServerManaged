using System;
using UnityEngine;

namespace Facepunch.Models;

[JsonModel]
public struct AppInfo
{
	public int Version => 3;

	public BuildInfo Build => BuildInfo.Current;

	public string Name => SystemInfo.deviceName;

	public string Os => SystemInfo.operatingSystem;

	public string Cpu => SystemInfo.processorType;

	public int CpuCount => SystemInfo.processorCount;

	public int Mem => SystemInfo.graphicsMemorySize;

	public string Gpu => SystemInfo.graphicsDeviceName;

	public int GpuMem => SystemInfo.graphicsMemorySize;

	public string Arch
	{
		get
		{
			if (IntPtr.Size != 4)
			{
				return "x64";
			}
			return "x86";
		}
	}

	public string UserId => Application.Integration.UserId;

	public string UserName => Application.Integration.UserName;

	public string ServerAddress => Application.Integration.ServerAddress;

	public string ServerName => Application.Integration.ServerName;

	public string LevelName => Application.Integration.LevelName;

	public string LevelPos
	{
		get
		{
			if (!(Camera.main == null))
			{
				return Camera.main.transform.position.ToString();
			}
			return "0 0 0";
		}
	}

	public string LevelRot
	{
		get
		{
			if (!(Camera.main == null))
			{
				return Camera.main.transform.eulerAngles.ToString();
			}
			return "0 0 0";
		}
	}

	public int MinutesPlayed => Application.Integration.MinutesPlayed;

	public string Image { get; set; }
}
