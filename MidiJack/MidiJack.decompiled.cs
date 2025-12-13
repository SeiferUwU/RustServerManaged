using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyVersion("0.0.0.0")]
[CompilerGenerated]
[EditorBrowsable(EditorBrowsableState.Never)]
[GeneratedCode("Unity.MonoScriptGenerator.MonoScriptInfoGenerator", null)]
internal class UnitySourceGeneratedAssemblyMonoScriptTypes_v1
{
	private struct MonoScriptData
	{
		public byte[] FilePathsData;

		public byte[] TypesData;

		public int TotalTypes;

		public int TotalFiles;

		public bool IsEditorOnly;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static MonoScriptData Get()
	{
		return new MonoScriptData
		{
			FilePathsData = new byte[132]
			{
				0, 0, 0, 1, 0, 0, 0, 32, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 77, 105, 100, 105, 74, 97,
				99, 107, 92, 77, 105, 100, 105, 46, 99, 115,
				0, 0, 0, 2, 0, 0, 0, 38, 92, 65,
				115, 115, 101, 116, 115, 92, 80, 108, 117, 103,
				105, 110, 115, 92, 77, 105, 100, 105, 74, 97,
				99, 107, 92, 77, 105, 100, 105, 68, 114, 105,
				118, 101, 114, 46, 99, 115, 0, 0, 0, 1,
				0, 0, 0, 38, 92, 65, 115, 115, 101, 116,
				115, 92, 80, 108, 117, 103, 105, 110, 115, 92,
				77, 105, 100, 105, 74, 97, 99, 107, 92, 77,
				105, 100, 105, 77, 97, 115, 116, 101, 114, 46,
				99, 115
			},
			TypesData = new byte[110]
			{
				0, 0, 0, 0, 20, 77, 105, 100, 105, 74,
				97, 99, 107, 124, 77, 105, 100, 105, 77, 101,
				115, 115, 97, 103, 101, 0, 0, 0, 0, 19,
				77, 105, 100, 105, 74, 97, 99, 107, 124, 77,
				105, 100, 105, 68, 114, 105, 118, 101, 114, 0,
				0, 0, 0, 32, 77, 105, 100, 105, 74, 97,
				99, 107, 46, 77, 105, 100, 105, 68, 114, 105,
				118, 101, 114, 124, 67, 104, 97, 110, 110, 101,
				108, 83, 116, 97, 116, 101, 0, 0, 0, 0,
				19, 77, 105, 100, 105, 74, 97, 99, 107, 124,
				77, 105, 100, 105, 77, 97, 115, 116, 101, 114
			},
			TotalFiles = 3,
			TotalTypes = 4,
			IsEditorOnly = false
		};
	}
}
namespace MidiJack;

public enum MidiChannel
{
	Ch1,
	Ch2,
	Ch3,
	Ch4,
	Ch5,
	Ch6,
	Ch7,
	Ch8,
	Ch9,
	Ch10,
	Ch11,
	Ch12,
	Ch13,
	Ch14,
	Ch15,
	Ch16,
	All
}
public struct MidiMessage
{
	public uint source;

	public byte status;

	public byte data1;

	public byte data2;

	public MidiMessage(ulong data)
	{
		source = (uint)(data & 0xFFFFFFFFu);
		status = (byte)((data >> 32) & 0xFF);
		data1 = (byte)((data >> 40) & 0xFF);
		data2 = (byte)((data >> 48) & 0xFF);
	}

	public override string ToString()
	{
		return $"s({status:X2}) d({data1:X2},{data2:X2}) from {source:X8}";
	}
}
public class MidiDriver
{
	private class ChannelState
	{
		public float[] _noteArray;

		public Dictionary<int, float> _knobMap;

		public ChannelState()
		{
			_noteArray = new float[128];
			_knobMap = new Dictionary<int, float>();
		}
	}

	public delegate void NoteOnDelegate(MidiChannel channel, int note, float velocity);

	public delegate void NoteOffDelegate(MidiChannel channel, int note);

	public delegate void KnobDelegate(MidiChannel channel, int knobNumber, float knobValue);

	private ChannelState[] _channelArray;

	private int _lastFrame;

	private bool enabled;

	private static MidiDriver _instance;

	public NoteOnDelegate noteOnDelegate { get; set; }

	public NoteOffDelegate noteOffDelegate { get; set; }

	public KnobDelegate knobDelegate { get; set; }

	public static MidiDriver Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new MidiDriver();
			}
			return _instance;
		}
	}

	public float GetKey(MidiChannel channel, int noteNumber)
	{
		UpdateIfNeeded();
		float num = _channelArray[(int)channel]._noteArray[noteNumber];
		if (num > 1f)
		{
			return num - 1f;
		}
		if (num > 0f)
		{
			return num;
		}
		return 0f;
	}

	public bool GetKeyDown(MidiChannel channel, int noteNumber)
	{
		UpdateIfNeeded();
		return _channelArray[(int)channel]._noteArray[noteNumber] > 1f;
	}

	public bool GetKeyUp(MidiChannel channel, int noteNumber)
	{
		UpdateIfNeeded();
		return _channelArray[(int)channel]._noteArray[noteNumber] < 0f;
	}

	public int[] GetKnobNumbers(MidiChannel channel)
	{
		UpdateIfNeeded();
		ChannelState obj = _channelArray[(int)channel];
		int[] array = new int[obj._knobMap.Count];
		obj._knobMap.Keys.CopyTo(array, 0);
		return array;
	}

	public float GetKnob(MidiChannel channel, int knobNumber, float defaultValue)
	{
		UpdateIfNeeded();
		ChannelState channelState = _channelArray[(int)channel];
		if (channelState._knobMap.ContainsKey(knobNumber))
		{
			return channelState._knobMap[knobNumber];
		}
		return defaultValue;
	}

	private MidiDriver()
	{
		_channelArray = new ChannelState[17];
		for (int i = 0; i < 17; i++)
		{
			_channelArray[i] = new ChannelState();
		}
	}

	public void ToggleEnabled(bool state)
	{
		enabled = state;
	}

	public void ClearOutstandingData()
	{
		while (DequeueIncomingData() != 0L)
		{
		}
	}

	private void UpdateIfNeeded()
	{
		if (Application.isPlaying)
		{
			int frameCount = Time.frameCount;
			if (frameCount != _lastFrame)
			{
				Update();
				_lastFrame = frameCount;
			}
		}
	}

	public void Update()
	{
		if (!enabled)
		{
			DequeueIncomingData();
			return;
		}
		ChannelState[] channelArray = _channelArray;
		foreach (ChannelState channelState in channelArray)
		{
			for (int j = 0; j < 128; j++)
			{
				float num = channelState._noteArray[j];
				if (num > 1f)
				{
					channelState._noteArray[j] = num - 1f;
				}
				else if (num < 0f)
				{
					channelState._noteArray[j] = 0f;
				}
			}
		}
		while (true)
		{
			ulong num2 = DequeueIncomingData();
			if (num2 == 0L)
			{
				break;
			}
			MidiMessage midiMessage = new MidiMessage(num2);
			int num3 = midiMessage.status >> 4;
			int num4 = midiMessage.status & 0xF;
			if (num3 == 9)
			{
				float num5 = 1f / 127f * (float)(int)midiMessage.data2 + 1f;
				_channelArray[num4]._noteArray[midiMessage.data1] = num5;
				_channelArray[16]._noteArray[midiMessage.data1] = num5;
				if (noteOnDelegate != null)
				{
					noteOnDelegate((MidiChannel)num4, midiMessage.data1, num5 - 1f);
				}
			}
			if (num3 == 8 || (num3 == 9 && midiMessage.data2 == 0))
			{
				_channelArray[num4]._noteArray[midiMessage.data1] = -1f;
				_channelArray[16]._noteArray[midiMessage.data1] = -1f;
				if (noteOffDelegate != null)
				{
					noteOffDelegate((MidiChannel)num4, midiMessage.data1);
				}
			}
			if (num3 == 11)
			{
				float num6 = 1f / 127f * (float)(int)midiMessage.data2;
				_channelArray[num4]._knobMap[midiMessage.data1] = num6;
				_channelArray[16]._knobMap[midiMessage.data1] = num6;
				if (knobDelegate != null)
				{
					knobDelegate((MidiChannel)num4, midiMessage.data1, num6);
				}
			}
		}
	}

	[DllImport("MidiJackPlugin", EntryPoint = "MidiJackDequeueIncomingData")]
	public static extern ulong DequeueIncomingData();
}
public static class MidiMaster
{
	public static MidiDriver.NoteOnDelegate noteOnDelegate
	{
		get
		{
			return MidiDriver.Instance.noteOnDelegate;
		}
		set
		{
			MidiDriver.Instance.noteOnDelegate = value;
		}
	}

	public static MidiDriver.NoteOffDelegate noteOffDelegate
	{
		get
		{
			return MidiDriver.Instance.noteOffDelegate;
		}
		set
		{
			MidiDriver.Instance.noteOffDelegate = value;
		}
	}

	public static MidiDriver.KnobDelegate knobDelegate
	{
		get
		{
			return MidiDriver.Instance.knobDelegate;
		}
		set
		{
			MidiDriver.Instance.knobDelegate = value;
		}
	}

	public static float GetKey(MidiChannel channel, int noteNumber)
	{
		return MidiDriver.Instance.GetKey(channel, noteNumber);
	}

	public static float GetKey(int noteNumber)
	{
		return MidiDriver.Instance.GetKey(MidiChannel.All, noteNumber);
	}

	public static bool GetKeyDown(MidiChannel channel, int noteNumber)
	{
		return MidiDriver.Instance.GetKeyDown(channel, noteNumber);
	}

	public static bool GetKeyDown(int noteNumber)
	{
		return MidiDriver.Instance.GetKeyDown(MidiChannel.All, noteNumber);
	}

	public static bool GetKeyUp(MidiChannel channel, int noteNumber)
	{
		return MidiDriver.Instance.GetKeyUp(channel, noteNumber);
	}

	public static bool GetKeyUp(int noteNumber)
	{
		return MidiDriver.Instance.GetKeyUp(MidiChannel.All, noteNumber);
	}

	public static int[] GetKnobNumbers(MidiChannel channel)
	{
		return MidiDriver.Instance.GetKnobNumbers(channel);
	}

	public static int[] GetKnobNumbers()
	{
		return MidiDriver.Instance.GetKnobNumbers(MidiChannel.All);
	}

	public static float GetKnob(MidiChannel channel, int knobNumber, float defaultValue = 0f)
	{
		return MidiDriver.Instance.GetKnob(channel, knobNumber, defaultValue);
	}

	public static float GetKnob(int knobNumber, float defaultValue = 0f)
	{
		return MidiDriver.Instance.GetKnob(MidiChannel.All, knobNumber, defaultValue);
	}

	public static void ToggleEnabled(bool state)
	{
		MidiDriver.Instance.ToggleEnabled(state);
	}
}
