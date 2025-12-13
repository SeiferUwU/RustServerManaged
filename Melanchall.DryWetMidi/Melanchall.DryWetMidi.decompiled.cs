using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.MusicTheory;
using Melanchall.DryWetMidi.Standards;
using Microsoft.Win32.SafeHandles;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Melanchall.DryWetMidi.Tests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100ada55ed903fbc84b65a00dbc1ba1f7e5412cceb35cf54367d07e70db1f17cdd36ec9427ffaeaa6095aec362889b846786c63258ffdef217417d148d519df20045eb8736cd0e4409b496883762ef00c9c02d0f5a9011f15ea1b08957dc661613fa3167db21aa0ba8e368c6cb3972d269f1b13c759e952f7dff0d7274c6c03aabf")]
[assembly: TargetFramework(".NETFramework,Version=v4.5", FrameworkDisplayName = ".NET Framework 4.5")]
[assembly: AssemblyCompany("melanchall")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("Copyright © Melanchall 2021")]
[assembly: AssemblyDescription("\r\n      DryWetMIDI is the .NET library to work with MIDI files and MIDI devices. It allows:\r\n\r\n      * Read, write and create Standard MIDI Files (SMF). It is also possible to read RMID files where SMF wrapped to RIFF chunk.\r\n      * Work with MIDI devices: send/receive MIDI data, play back and record MIDI data.\r\n      * Finely adjust process of reading and writing. It allows, for example, to read corrupted files and repair them, or build MIDI file validators.\r\n      * Implement custom meta events and custom chunks that can be write to and read from MIDI files.\r\n      * Manage MIDI data either with low-level objects, like event, or high-level ones, like note.\r\n      * Build musical compositions.\r\n      * Perform complex musical tasks like quantizing, notes splitting or converting MIDI files to CSV.\r\n    ")]
[assembly: AssemblyFileVersion("6.0.0.0")]
[assembly: AssemblyInformationalVersion("6.0.0+97266eaf10a2cce8962c48c005a8b3b35bdb14e4")]
[assembly: AssemblyProduct("Melanchall.DryWetMidi")]
[assembly: AssemblyTitle("Melanchall.DryWetMidi")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/melanchall/drywetmidi")]
[assembly: AssemblyVersion("6.0.0.0")]
namespace Melanchall.DryWetMidi.Tools
{
	public enum LengthedObjectTarget
	{
		Start,
		End
	}
	public enum TimeProcessingAction
	{
		Apply,
		Skip
	}
	public sealed class TimeProcessingInstruction
	{
		public static readonly TimeProcessingInstruction Skip = new TimeProcessingInstruction(TimeProcessingAction.Skip, -1L);

		private const long InvalidTime = -1L;

		public TimeProcessingAction Action { get; }

		public long Time { get; }

		public TimeProcessingInstruction(long time)
			: this(TimeProcessingAction.Apply, time)
		{
			ThrowIfArgument.IsNegative("time", time, "Time is negative.");
		}

		private TimeProcessingInstruction(TimeProcessingAction quantizingInstruction, long time)
		{
			Action = quantizingInstruction;
			Time = time;
		}
	}
	internal static class CsvError
	{
		public static void ThrowBadFormat(int lineNumber, string message, Exception innerException = null)
		{
			ThrowBadFormat($"Line {lineNumber}: {message}", innerException);
		}

		public static void ThrowBadFormat(string message, Exception innerException = null)
		{
			throw new FormatException(message, innerException);
		}
	}
	internal sealed class CsvReader : IDisposable
	{
		private const char Quote = '"';

		private readonly StreamReader _streamReader;

		private readonly char _delimiter;

		private readonly char[] _buffer;

		private int _bufferLength;

		private int _indexInBuffer;

		private bool _disposed;

		private int _currentLineNumber;

		public CsvReader(Stream stream, CsvSettings settings)
		{
			_streamReader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true, settings.IoBufferSize, leaveOpen: true);
			_buffer = new char[settings.IoBufferSize];
			_delimiter = settings.CsvDelimiter;
		}

		public CsvRecord ReadRecord()
		{
			int currentLineNumber = _currentLineNumber;
			string text = GetFirstLine();
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			string[] array;
			while (true)
			{
				array = SplitValues(text, _delimiter).ToArray();
				if (array.All(IsValueClosed))
				{
					break;
				}
				string nextLine = GetNextLine();
				if (nextLine == null)
				{
					break;
				}
				text += nextLine;
			}
			return new CsvRecord(currentLineNumber, _currentLineNumber - currentLineNumber, array);
		}

		private string GetFirstLine()
		{
			string nextLine;
			do
			{
				nextLine = GetNextLine();
			}
			while (nextLine?.Trim() == string.Empty);
			return nextLine;
		}

		private string GetNextLine()
		{
			_currentLineNumber++;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			do
			{
				IL_004d:
				if (_indexInBuffer < _bufferLength)
				{
					char c = _buffer[_indexInBuffer];
					if (c == '\r' || c == '\n')
					{
						flag = true;
					}
					else if (flag)
					{
						goto IL_005b;
					}
					stringBuilder.Append(c);
					_indexInBuffer++;
					goto IL_004d;
				}
				goto IL_005b;
				IL_005b:
				if (_indexInBuffer < _bufferLength)
				{
					break;
				}
				FillBuffer();
			}
			while (_bufferLength != 0);
			if (stringBuilder.Length <= 0)
			{
				return null;
			}
			return stringBuilder.ToString();
		}

		private void FillBuffer()
		{
			int num = 0;
			int num2 = _buffer.Length;
			while (num2 > 0)
			{
				int num3 = _streamReader.ReadBlock(_buffer, num, num2);
				if (num3 == 0)
				{
					break;
				}
				num2 -= num3;
				num += num3;
			}
			_bufferLength = _buffer.Length - num2;
			_indexInBuffer = 0;
		}

		private static IEnumerable<string> SplitValues(string input, char delimiter)
		{
			StringBuilder valueBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			foreach (char c in input)
			{
				if (c == delimiter && (!flag || flag2))
				{
					yield return valueBuilder.ToString().Trim();
					valueBuilder.Clear();
					flag2 = false;
					flag = false;
					continue;
				}
				if (c == '"')
				{
					if (!flag)
					{
						flag = true;
					}
					else
					{
						flag2 = !flag2;
					}
				}
				valueBuilder.Append(c);
			}
			yield return valueBuilder.ToString().Trim();
		}

		private static bool IsValueClosed(string value)
		{
			if (string.IsNullOrEmpty(value) || value[0] != '"')
			{
				return true;
			}
			if (value.Length == 1)
			{
				return false;
			}
			return value.Skip(1).Reverse().TakeWhile((char c) => c == '"')
				.Count() % 2 == 1;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_streamReader.Dispose();
				}
				_disposed = true;
			}
		}
	}
	internal sealed class CsvRecord
	{
		public int LineNumber { get; }

		public int LinesCount { get; }

		public string[] Values { get; }

		public CsvRecord(int lineNumber, int linesCount, string[] values)
		{
			LineNumber = lineNumber;
			LinesCount = linesCount;
			Values = values;
		}
	}
	public sealed class CsvSettings
	{
		private int _bufferSize = 1024;

		public char CsvDelimiter { get; set; } = ',';

		public int IoBufferSize
		{
			get
			{
				return _bufferSize;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Buffer size is zero or negative.");
				_bufferSize = value;
			}
		}
	}
	internal sealed class CsvWriter : IDisposable
	{
		private readonly StreamWriter _streamWriter;

		private readonly char _delimiter;

		private bool _disposed;

		public CsvWriter(Stream stream, CsvSettings settings)
		{
			_streamWriter = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true), 1024, leaveOpen: true);
			_delimiter = settings.CsvDelimiter;
		}

		public void WriteRecord(IEnumerable<object> values)
		{
			StreamWriter streamWriter = _streamWriter;
			char delimiter = _delimiter;
			streamWriter.WriteLine(string.Join(delimiter.ToString(), values));
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_streamWriter.Dispose();
				}
				_disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}
	}
	public sealed class CsvConverter
	{
		public void ConvertMidiFileToCsv(MidiFile midiFile, string filePath, bool overwriteFile = false, MidiFileCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			using FileStream stream = FileUtilities.OpenFileForWrite(filePath, overwriteFile);
			ConvertMidiFileToCsv(midiFile, stream, settings);
		}

		public void ConvertMidiFileToCsv(MidiFile midiFile, Stream stream, MidiFileCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("stream", stream);
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream doesn't support writing.", "stream");
			}
			MidiFileToCsvConverter.ConvertToCsv(midiFile, stream, settings ?? new MidiFileCsvConversionSettings());
		}

		public MidiFile ConvertCsvToMidiFile(string filePath, MidiFileCsvConversionSettings settings = null)
		{
			using FileStream stream = FileUtilities.OpenFileForRead(filePath);
			return ConvertCsvToMidiFile(stream, settings);
		}

		public MidiFile ConvertCsvToMidiFile(Stream stream, MidiFileCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream doesn't support reading.", "stream");
			}
			return CsvToMidiFileConverter.ConvertToMidiFile(stream, settings ?? new MidiFileCsvConversionSettings());
		}

		public void ConvertNotesToCsv(IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes, string filePath, TempoMap tempoMap, bool overwriteFile = false, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using FileStream stream = FileUtilities.OpenFileForWrite(filePath, overwriteFile);
			ConvertNotesToCsv(notes, stream, tempoMap, settings);
		}

		public void ConvertNotesToCsv(IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes, Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream doesn't support writing.", "stream");
			}
			NotesToCsvConverter.ConvertToCsv(notes, stream, tempoMap, settings ?? new NoteCsvConversionSettings());
		}

		public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> ConvertCsvToNotes(string filePath, TempoMap tempoMap, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using FileStream stream = FileUtilities.OpenFileForRead(filePath);
			return ConvertCsvToNotes(stream, tempoMap, settings).ToList();
		}

		public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> ConvertCsvToNotes(Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream doesn't support reading.", "stream");
			}
			return CsvToNotesConverter.ConvertToNotes(stream, tempoMap, settings ?? new NoteCsvConversionSettings());
		}
	}
	internal static class CsvUtilities
	{
		private const char Quote = '"';

		private const string QuoteString = "\"";

		private const string DoubleQuote = "\"\"";

		public static string EscapeString(string input)
		{
			return string.Format("{0}{1}{2}", '"', input.Replace("\"", "\"\""), '"');
		}

		public static string UnescapeString(string input)
		{
			if (input.Length > 1 && input[0] == '"' && input[input.Length - 1] == '"')
			{
				input = input.Substring(1, input.Length - 2);
			}
			return input.Replace("\"\"", "\"");
		}
	}
	internal static class CsvToMidiFileConverter
	{
		private static readonly Dictionary<string, RecordType> RecordTypes_DryWetMidi = new Dictionary<string, RecordType>(StringComparer.OrdinalIgnoreCase)
		{
			["Header"] = RecordType.Header,
			["Note"] = RecordType.Note
		};

		private static readonly Dictionary<string, RecordType> RecordTypes_MidiCsv = new Dictionary<string, RecordType>(StringComparer.OrdinalIgnoreCase)
		{
			["Header"] = RecordType.Header,
			["Start_track"] = RecordType.TrackChunkStart,
			["End_track"] = RecordType.TrackChunkEnd,
			["End_of_file"] = RecordType.FileEnd
		};

		public static MidiFile ConvertToMidiFile(Stream stream, MidiFileCsvConversionSettings settings)
		{
			MidiFile midiFile = new MidiFile();
			Dictionary<int, List<TimedMidiEvent>> dictionary = new Dictionary<int, List<TimedMidiEvent>>();
			using (CsvReader csvReader = new CsvReader(stream, settings.CsvSettings))
			{
				int lineNumber = 0;
				Record record;
				while ((record = ReadRecord(csvReader, settings)) != null)
				{
					RecordType? recordType = GetRecordType(record.RecordType, settings);
					if (!recordType.HasValue)
					{
						CsvError.ThrowBadFormat(lineNumber, "Unknown record.");
					}
					switch (recordType)
					{
					case RecordType.Header:
					{
						HeaderChunk headerChunk = ParseHeader(record, settings);
						midiFile.TimeDivision = headerChunk.TimeDivision;
						midiFile.OriginalFormat = (MidiFileFormat)headerChunk.FileFormat;
						break;
					}
					case RecordType.Event:
					{
						MidiEvent midiEvent = ParseEvent(record, settings);
						int value2 = record.TrackNumber.Value;
						AddTimedEvents(dictionary, value2, new TimedMidiEvent(record.Time, midiEvent));
						break;
					}
					case RecordType.Note:
					{
						TimedMidiEvent[] events = ParseNote(record, settings);
						int value = record.TrackNumber.Value;
						AddTimedEvents(dictionary, value, events);
						break;
					}
					}
					lineNumber = record.LineNumber + 1;
				}
			}
			if (!dictionary.Keys.Any())
			{
				return midiFile;
			}
			TempoMap tempoMap = GetTempoMap(dictionary.Values.SelectMany((List<TimedMidiEvent> e) => e), midiFile.TimeDivision);
			TrackChunk[] array = new TrackChunk[dictionary.Keys.Max() + 1];
			for (int num = 0; num < array.Length; num++)
			{
				array[num] = (dictionary.TryGetValue(num, out var value3) ? value3.Select((TimedMidiEvent e) => new TimedEvent(e.Event, TimeConverter.ConvertFrom(e.Time, tempoMap))).ToTrackChunk() : new TrackChunk());
			}
			midiFile.Chunks.AddRange(array);
			return midiFile;
		}

		private static void AddTimedEvents(Dictionary<int, List<TimedMidiEvent>> eventsMap, int trackChunkNumber, params TimedMidiEvent[] events)
		{
			if (!eventsMap.TryGetValue(trackChunkNumber, out var value))
			{
				eventsMap.Add(trackChunkNumber, value = new List<TimedMidiEvent>());
			}
			value.AddRange(events);
		}

		private static TempoMap GetTempoMap(IEnumerable<TimedMidiEvent> timedMidiEvents, TimeDivision timeDivision)
		{
			using TempoMapManager tempoMapManager = new TempoMapManager(timeDivision);
			foreach (TimedMidiEvent item in timedMidiEvents.Where((TimedMidiEvent e) => e.Event is SetTempoEvent).OrderBy((TimedMidiEvent e) => e.Time, new TimeSpanComparer()))
			{
				SetTempoEvent setTempoEvent = (SetTempoEvent)item.Event;
				tempoMapManager.SetTempo(item.Time, new Tempo(setTempoEvent.MicrosecondsPerQuarterNote));
			}
			foreach (TimedMidiEvent item2 in timedMidiEvents.Where((TimedMidiEvent e) => e.Event is TimeSignatureEvent).OrderBy((TimedMidiEvent e) => e.Time, new TimeSpanComparer()))
			{
				TimeSignatureEvent timeSignatureEvent = (TimeSignatureEvent)item2.Event;
				tempoMapManager.SetTimeSignature(item2.Time, new TimeSignature(timeSignatureEvent.Numerator, timeSignatureEvent.Denominator));
			}
			return tempoMapManager.TempoMap;
		}

		private static RecordType? GetRecordType(string recordType, MidiFileCsvConversionSettings settings)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			Dictionary<string, RecordType> obj = ((csvLayout == MidiFileCsvLayout.DryWetMidi) ? RecordTypes_DryWetMidi : RecordTypes_MidiCsv);
			string[] source = EventsNamesProvider.Get(csvLayout);
			if (obj.TryGetValue(recordType, out var value))
			{
				return value;
			}
			if (source.Contains(recordType, StringComparer.OrdinalIgnoreCase))
			{
				return RecordType.Event;
			}
			return null;
		}

		private static HeaderChunk ParseHeader(Record record, MidiFileCsvConversionSettings settings)
		{
			string[] parameters = record.Parameters;
			MidiFileFormat? midiFileFormat = null;
			short result = 0;
			switch (settings.CsvLayout)
			{
			case MidiFileCsvLayout.DryWetMidi:
			{
				if (parameters.Length < 2)
				{
					CsvError.ThrowBadFormat(record.LineNumber, "Parameters count is invalid.");
				}
				if (Enum.TryParse<MidiFileFormat>(parameters[0], ignoreCase: true, out var result3))
				{
					midiFileFormat = result3;
				}
				if (!short.TryParse(parameters[1], out result))
				{
					CsvError.ThrowBadFormat(record.LineNumber, "Invalid time division.");
				}
				break;
			}
			case MidiFileCsvLayout.MidiCsv:
			{
				if (parameters.Length < 3)
				{
					CsvError.ThrowBadFormat(record.LineNumber, "Parameters count is invalid.");
				}
				if (ushort.TryParse(parameters[0], out var result2) && Enum.IsDefined(typeof(MidiFileFormat), result2))
				{
					midiFileFormat = (MidiFileFormat)result2;
				}
				if (!short.TryParse(parameters[2], out result))
				{
					CsvError.ThrowBadFormat(record.LineNumber, "Invalid time division.");
				}
				break;
			}
			}
			return new HeaderChunk
			{
				FileFormat = (ushort)(midiFileFormat.HasValue ? midiFileFormat.Value : ((MidiFileFormat)65535)),
				TimeDivision = TimeDivisionFactory.GetTimeDivision(result)
			};
		}

		private static MidiEvent ParseEvent(Record record, MidiFileCsvConversionSettings settings)
		{
			if (!record.TrackNumber.HasValue)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid track number.");
			}
			if (record.Time == null)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid time.");
			}
			EventParser eventParser = EventParserProvider.Get(record.RecordType, settings.CsvLayout);
			try
			{
				return eventParser(record.Parameters, settings);
			}
			catch (FormatException innerException)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid format of event record.", innerException);
				return null;
			}
		}

		private static TimedMidiEvent[] ParseNote(Record record, MidiFileCsvConversionSettings settings)
		{
			if (!record.TrackNumber.HasValue)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid track number.");
			}
			if (record.Time == null)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid time.");
			}
			string[] parameters = record.Parameters;
			if (parameters.Length < 5)
			{
				CsvError.ThrowBadFormat(record.LineNumber, "Invalid number of parameters provided.");
			}
			int num = -1;
			try
			{
				FourBitNumber channel = (FourBitNumber)TypeParser.FourBitNumber(parameters[++num], settings);
				SevenBitNumber noteNumber = (SevenBitNumber)TypeParser.NoteNumber(parameters[++num], settings);
				TimeSpanUtilities.TryParse(parameters[++num], settings.NoteLengthType, out var timeSpan);
				SevenBitNumber velocity = (SevenBitNumber)TypeParser.SevenBitNumber(parameters[++num], settings);
				SevenBitNumber velocity2 = (SevenBitNumber)TypeParser.SevenBitNumber(parameters[++num], settings);
				return new TimedMidiEvent[2]
				{
					new TimedMidiEvent(record.Time, new NoteOnEvent(noteNumber, velocity)
					{
						Channel = channel
					}),
					new TimedMidiEvent(record.Time.Add(timeSpan, TimeSpanMode.TimeLength), new NoteOffEvent(noteNumber, velocity2)
					{
						Channel = channel
					})
				};
			}
			catch
			{
				CsvError.ThrowBadFormat(record.LineNumber, $"Parameter ({num}) is invalid.");
			}
			return null;
		}

		private static Record ReadRecord(CsvReader csvReader, MidiFileCsvConversionSettings settings)
		{
			CsvRecord csvRecord = csvReader.ReadRecord();
			if (csvRecord == null)
			{
				return null;
			}
			string[] values = csvRecord.Values;
			if (values.Length < 3)
			{
				CsvError.ThrowBadFormat(csvRecord.LineNumber, "Missing required parameters.");
			}
			int result;
			int? trackNumber = (int.TryParse(values[0], out result) ? new int?(result) : ((int?)null));
			TimeSpanUtilities.TryParse(values[1], settings.TimeType, out var timeSpan);
			string text = values[2];
			if (string.IsNullOrEmpty(text))
			{
				CsvError.ThrowBadFormat(csvRecord.LineNumber, "Record type isn't specified.");
			}
			string[] parameters = values.Skip(3).ToArray();
			return new Record(csvRecord.LineNumber, trackNumber, timeSpan, text, parameters);
		}
	}
	internal delegate MidiEvent EventParser(string[] parameters, MidiFileCsvConversionSettings settings);
	internal static class EventParserProvider
	{
		private static readonly Dictionary<string, EventParser> EventsParsers_MidiCsv = new Dictionary<string, EventParser>(StringComparer.OrdinalIgnoreCase)
		{
			["Title_t"] = GetTextEventParser<SequenceTrackNameEvent>(),
			["Copyright_t"] = GetTextEventParser<CopyrightNoticeEvent>(),
			["Instrument_name_t"] = GetTextEventParser<InstrumentNameEvent>(),
			["Marker_t"] = GetTextEventParser<MarkerEvent>(),
			["Cue_point_t"] = GetTextEventParser<CuePointEvent>(),
			["Lyric_t"] = GetTextEventParser<LyricEvent>(),
			["Text_t"] = GetTextEventParser<TextEvent>(),
			["Sequence_number"] = GetEventParser((object[] x) => new SequenceNumberEvent((ushort)x[0]), TypeParser.UShort),
			["MIDI_port"] = GetEventParser((object[] x) => new PortPrefixEvent((byte)x[0]), TypeParser.Byte),
			["Channel_prefix"] = GetEventParser((object[] x) => new ChannelPrefixEvent((byte)x[0]), TypeParser.Byte),
			["Time_signature"] = GetEventParser((object[] x) => new TimeSignatureEvent((byte)x[0], (byte)Math.Pow(2.0, (int)(byte)x[1]), (byte)x[2], (byte)x[3]), TypeParser.Byte, TypeParser.Byte, TypeParser.Byte, TypeParser.Byte),
			["Key_signature"] = GetEventParser((object[] x) => new KeySignatureEvent((sbyte)x[0], (byte)x[1]), TypeParser.SByte, TypeParser.Byte),
			["Tempo"] = GetEventParser((object[] x) => new SetTempoEvent((long)x[0]), TypeParser.Long),
			["SMPTE_offset"] = GetEventParser((object[] x) => new SmpteOffsetEvent(SmpteData.GetFormat((byte)x[0]), SmpteData.GetHours((byte)x[0]), (byte)x[1], (byte)x[2], (byte)x[3], (byte)x[4]), TypeParser.Byte, TypeParser.Byte, TypeParser.Byte, TypeParser.Byte, TypeParser.Byte),
			["Sequencer_specific"] = GetBytesBasedEventParser((object[] x) => new SequencerSpecificEvent((byte[])x[1])),
			["Unknown_meta_event"] = GetBytesBasedEventParser((object[] x) => new UnknownMetaEvent((byte)x[0], (byte[])x[2]), TypeParser.Byte),
			["Note_on_c"] = GetNoteEventParser<NoteOnEvent>(2),
			["Note_off_c"] = GetNoteEventParser<NoteOffEvent>(2),
			["Pitch_bend_c"] = GetEventParser((object[] x) => new PitchBendEvent((ushort)x[1])
			{
				Channel = (FourBitNumber)x[0]
			}, TypeParser.FourBitNumber, TypeParser.UShort),
			["Control_c"] = GetChannelEventParser<ControlChangeEvent>(2),
			["Program_c"] = GetChannelEventParser<ProgramChangeEvent>(1),
			["Channel_aftertouch_c"] = GetChannelEventParser<ChannelAftertouchEvent>(1),
			["Poly_aftertouch_c"] = GetNoteEventParser<ChannelAftertouchEvent>(2),
			["System_exclusive"] = GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1])),
			["System_exclusive_packet"] = GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1]))
		};

		private static readonly Dictionary<string, EventParser> EventsParsers_DryWetMidi = new Dictionary<string, EventParser>(StringComparer.OrdinalIgnoreCase)
		{
			["Sequence/Track Name"] = GetTextEventParser<SequenceTrackNameEvent>(),
			["Copyright Notice"] = GetTextEventParser<CopyrightNoticeEvent>(),
			["Instrument Name"] = GetTextEventParser<InstrumentNameEvent>(),
			["Marker"] = GetTextEventParser<MarkerEvent>(),
			["Cue Point"] = GetTextEventParser<CuePointEvent>(),
			["Lyric"] = GetTextEventParser<LyricEvent>(),
			["Text"] = GetTextEventParser<TextEvent>(),
			["Sequence Number"] = GetEventParser((object[] x) => new SequenceNumberEvent((ushort)x[0]), TypeParser.UShort),
			["Port Prefix"] = GetEventParser((object[] x) => new PortPrefixEvent((byte)x[0]), TypeParser.Byte),
			["Channel Prefix"] = GetEventParser((object[] x) => new ChannelPrefixEvent((byte)x[0]), TypeParser.Byte),
			["Time Signature"] = GetEventParser((object[] x) => new TimeSignatureEvent((byte)x[0], (byte)x[1], (byte)x[2], (byte)x[3]), TypeParser.Byte, TypeParser.Byte, TypeParser.Byte, TypeParser.Byte),
			["Key Signature"] = GetEventParser((object[] x) => new KeySignatureEvent((sbyte)x[0], (byte)x[1]), TypeParser.SByte, TypeParser.Byte),
			["Set Tempo"] = GetEventParser((object[] x) => new SetTempoEvent((long)x[0]), TypeParser.Long),
			["SMPTE Offset"] = GetEventParser((object[] x) => new SmpteOffsetEvent(SmpteData.GetFormat((byte)x[0]), SmpteData.GetHours((byte)x[0]), (byte)x[1], (byte)x[2], (byte)x[3], (byte)x[4]), TypeParser.Byte, TypeParser.Byte, TypeParser.Byte, TypeParser.Byte, TypeParser.Byte),
			["Sequencer Specific"] = GetBytesBasedEventParser((object[] x) => new SequencerSpecificEvent((byte[])x[1])),
			["Unknown Meta"] = GetBytesBasedEventParser((object[] x) => new UnknownMetaEvent((byte)x[0], (byte[])x[2]), TypeParser.Byte),
			["Note On"] = GetNoteEventParser<NoteOnEvent>(2),
			["Note Off"] = GetNoteEventParser<NoteOffEvent>(2),
			["Pitch Bend"] = GetEventParser((object[] x) => new PitchBendEvent((ushort)x[1])
			{
				Channel = (FourBitNumber)x[0]
			}, TypeParser.FourBitNumber, TypeParser.UShort),
			["Control Change"] = GetChannelEventParser<ControlChangeEvent>(2),
			["Program Change"] = GetChannelEventParser<ProgramChangeEvent>(1),
			["Channel Aftertouch"] = GetChannelEventParser<ChannelAftertouchEvent>(1),
			["Note Aftertouch"] = GetNoteEventParser<ChannelAftertouchEvent>(2),
			["System Exclusive"] = GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1])),
			["System Exclusive Packet"] = GetBytesBasedEventParser((object[] x) => new NormalSysExEvent((byte[])x[1]))
		};

		public static EventParser Get(string eventName, MidiFileCsvLayout layout)
		{
			return layout switch
			{
				MidiFileCsvLayout.DryWetMidi => EventsParsers_DryWetMidi[eventName], 
				MidiFileCsvLayout.MidiCsv => EventsParsers_MidiCsv[eventName], 
				_ => null, 
			};
		}

		private static EventParser GetBytesBasedEventParser(Func<object[], MidiEvent> eventCreator, params ParameterParser[] parametersParsers)
		{
			return delegate(string[] p, MidiFileCsvConversionSettings s)
			{
				if (p.Length < parametersParsers.Length)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.");
				}
				List<object> list = new List<object>(parametersParsers.Length);
				int i = 0;
				for (i = 0; i < parametersParsers.Length; i++)
				{
					ParameterParser parameterParser = parametersParsers[i];
					try
					{
						object item = parameterParser(p[i], s);
						list.Add(item);
					}
					catch
					{
						CsvError.ThrowBadFormat($"Parameter ({i}) is invalid.");
					}
				}
				if (p.Length < i)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.");
				}
				int num = 0;
				try
				{
					num = int.Parse(p[i]);
					list.Add(num);
				}
				catch
				{
					CsvError.ThrowBadFormat($"Parameter ({i}) is invalid.");
				}
				i++;
				if (p.Length < i + num)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.");
				}
				try
				{
					byte[] item2 = p.Skip(i).Select(delegate(string x)
					{
						byte result = (byte)TypeParser.Byte(x, s);
						i++;
						return result;
					}).ToArray();
					list.Add(item2);
				}
				catch
				{
					CsvError.ThrowBadFormat($"Parameter ({i}) is invalid.");
				}
				return eventCreator(list.ToArray());
			};
		}

		private static EventParser GetTextEventParser<TEvent>() where TEvent : BaseTextEvent
		{
			return GetEventParser((object[] x) => new TEvent
			{
				Text = (string)x[0]
			}, TypeParser.String);
		}

		private static EventParser GetNoteEventParser<TEvent>(int parametersNumber) where TEvent : ChannelEvent
		{
			return GetChannelEventParser<TEvent>(new ParameterParser[1] { TypeParser.NoteNumber }.Concat(from i in Enumerable.Range(0, parametersNumber - 1)
				select TypeParser.SevenBitNumber).ToArray());
		}

		private static EventParser GetChannelEventParser<TEvent>(int parametersNumber) where TEvent : ChannelEvent
		{
			return GetChannelEventParser<TEvent>((from i in Enumerable.Range(0, parametersNumber)
				select TypeParser.SevenBitNumber).ToArray());
		}

		private static EventParser GetChannelEventParser<TEvent>(ParameterParser[] parametersParsers) where TEvent : ChannelEvent
		{
			return GetEventParser(delegate(object[] x)
			{
				TEvent val = new TEvent
				{
					Channel = (FourBitNumber)x[0],
					_dataByte1 = Convert.ToByte(x[1])
				};
				if (x.Length > 2)
				{
					val._dataByte2 = Convert.ToByte(x[2]);
				}
				return val;
			}, new ParameterParser[1] { TypeParser.FourBitNumber }.Concat(parametersParsers).ToArray());
		}

		private static EventParser GetEventParser(Func<object[], MidiEvent> eventCreator, params ParameterParser[] parametersParsers)
		{
			return delegate(string[] p, MidiFileCsvConversionSettings s)
			{
				if (p.Length < parametersParsers.Length)
				{
					CsvError.ThrowBadFormat("Invalid number of parameters provided.");
				}
				List<object> list = new List<object>(parametersParsers.Length);
				for (int i = 0; i < parametersParsers.Length; i++)
				{
					ParameterParser parameterParser = parametersParsers[i];
					try
					{
						object item = parameterParser(p[i], s);
						list.Add(item);
					}
					catch
					{
						CsvError.ThrowBadFormat($"Parameter ({i}) is invalid.");
					}
				}
				return eventCreator(list.ToArray());
			};
		}
	}
	internal static class EventsNamesProvider
	{
		private static readonly Dictionary<MidiFileCsvLayout, string[]> EventsNames = new Dictionary<MidiFileCsvLayout, string[]>
		{
			[MidiFileCsvLayout.DryWetMidi] = GetEventsNames(typeof(DryWetMidiRecordTypes.Events)),
			[MidiFileCsvLayout.MidiCsv] = GetEventsNames(typeof(MidiCsvRecordTypes.Events))
		};

		public static string[] Get(MidiFileCsvLayout layout)
		{
			return EventsNames[layout];
		}

		private static string[] GetEventsNames(Type eventNamesClassType)
		{
			return (from fi in eventNamesClassType.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy)
				where fi.IsLiteral && !fi.IsInitOnly
				select fi.GetValue(null).ToString()).ToArray();
		}
	}
	internal delegate object ParameterParser(string parameter, MidiFileCsvConversionSettings settings);
	internal sealed class Record
	{
		public int LineNumber { get; }

		public int? TrackNumber { get; }

		public ITimeSpan Time { get; }

		public string RecordType { get; }

		public string[] Parameters { get; }

		public Record(int lineNumber, int? trackNumber, ITimeSpan time, string recordType, string[] parameters)
		{
			LineNumber = lineNumber;
			TrackNumber = trackNumber;
			Time = time;
			RecordType = recordType;
			Parameters = parameters;
		}
	}
	internal enum RecordType
	{
		Header,
		TrackChunkStart,
		TrackChunkEnd,
		FileEnd,
		Event,
		Note
	}
	internal sealed class TimedMidiEvent
	{
		public ITimeSpan Time { get; }

		public MidiEvent Event { get; }

		public TimedMidiEvent(ITimeSpan time, MidiEvent midiEvent)
		{
			Time = time;
			Event = midiEvent;
		}
	}
	internal static class TypeParser
	{
		public static readonly ParameterParser Byte = (string p, MidiFileCsvConversionSettings s) => byte.Parse(p);

		public static readonly ParameterParser SByte = (string p, MidiFileCsvConversionSettings s) => sbyte.Parse(p);

		public static readonly ParameterParser Long = (string p, MidiFileCsvConversionSettings s) => long.Parse(p);

		public static readonly ParameterParser UShort = (string p, MidiFileCsvConversionSettings s) => ushort.Parse(p);

		public static readonly ParameterParser String = (string p, MidiFileCsvConversionSettings s) => CsvUtilities.UnescapeString(p);

		public static readonly ParameterParser Int = (string p, MidiFileCsvConversionSettings s) => int.Parse(p);

		public static readonly ParameterParser FourBitNumber = (string p, MidiFileCsvConversionSettings s) => (FourBitNumber)byte.Parse(p);

		public static readonly ParameterParser SevenBitNumber = (string p, MidiFileCsvConversionSettings s) => (SevenBitNumber)byte.Parse(p);

		public static readonly ParameterParser NoteNumber = (string p, MidiFileCsvConversionSettings s) => s.NoteNumberFormat switch
		{
			NoteNumberFormat.NoteNumber => SevenBitNumber(p, s), 
			NoteNumberFormat.Letter => Melanchall.DryWetMidi.MusicTheory.Note.Parse(p).NoteNumber, 
			_ => null, 
		};
	}
	public sealed class MidiFileCsvConversionSettings
	{
		private MidiFileCsvLayout _csvLayout;

		private TimeSpanType _timeType = TimeSpanType.Midi;

		private TimeSpanType _noteLengthType = TimeSpanType.Midi;

		private NoteFormat _noteFormat = NoteFormat.Events;

		private NoteNumberFormat _noteNumberFormat;

		public MidiFileCsvLayout CsvLayout
		{
			get
			{
				return _csvLayout;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_csvLayout = value;
			}
		}

		public TimeSpanType TimeType
		{
			get
			{
				return _timeType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_timeType = value;
			}
		}

		public TimeSpanType NoteLengthType
		{
			get
			{
				return _noteLengthType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteLengthType = value;
			}
		}

		public NoteFormat NoteFormat
		{
			get
			{
				return _noteFormat;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteFormat = value;
			}
		}

		public NoteNumberFormat NoteNumberFormat
		{
			get
			{
				return _noteNumberFormat;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteNumberFormat = value;
			}
		}

		public CsvSettings CsvSettings { get; } = new CsvSettings();
	}
	public enum MidiFileCsvLayout
	{
		DryWetMidi,
		MidiCsv
	}
	public enum NoteFormat
	{
		Note,
		Events
	}
	internal static class DryWetMidiRecordTypes
	{
		public static class File
		{
			public const string Header = "Header";
		}

		public static class Events
		{
			public const string SequenceTrackName = "Sequence/Track Name";

			public const string CopyrightNotice = "Copyright Notice";

			public const string InstrumentName = "Instrument Name";

			public const string Marker = "Marker";

			public const string CuePoint = "Cue Point";

			public const string Lyric = "Lyric";

			public const string Text = "Text";

			public const string SequenceNumber = "Sequence Number";

			public const string PortPrefix = "Port Prefix";

			public const string ChannelPrefix = "Channel Prefix";

			public const string TimeSignature = "Time Signature";

			public const string KeySignature = "Key Signature";

			public const string SetTempo = "Set Tempo";

			public const string SmpteOffset = "SMPTE Offset";

			public const string SequencerSpecific = "Sequencer Specific";

			public const string UnknownMeta = "Unknown Meta";

			public const string NoteOn = "Note On";

			public const string NoteOff = "Note Off";

			public const string PitchBend = "Pitch Bend";

			public const string ControlChange = "Control Change";

			public const string ProgramChange = "Program Change";

			public const string ChannelAftertouch = "Channel Aftertouch";

			public const string NoteAftertouch = "Note Aftertouch";

			public const string SysExCompleted = "System Exclusive";

			public const string SysExIncompleted = "System Exclusive Packet";
		}

		public const string Note = "Note";
	}
	internal static class MidiCsvRecordTypes
	{
		public static class File
		{
			public const string Header = "Header";

			public const string TrackChunkStart = "Start_track";

			public const string TrackChunkEnd = "End_track";

			public const string FileEnd = "End_of_file";
		}

		public static class Events
		{
			public const string SequenceTrackName = "Title_t";

			public const string CopyrightNotice = "Copyright_t";

			public const string InstrumentName = "Instrument_name_t";

			public const string Marker = "Marker_t";

			public const string CuePoint = "Cue_point_t";

			public const string Lyric = "Lyric_t";

			public const string Text = "Text_t";

			public const string SequenceNumber = "Sequence_number";

			public const string PortPrefix = "MIDI_port";

			public const string ChannelPrefix = "Channel_prefix";

			public const string TimeSignature = "Time_signature";

			public const string KeySignature = "Key_signature";

			public const string SetTempo = "Tempo";

			public const string SmpteOffset = "SMPTE_offset";

			public const string SequencerSpecific = "Sequencer_specific";

			public const string UnknownMeta = "Unknown_meta_event";

			public const string NoteOn = "Note_on_c";

			public const string NoteOff = "Note_off_c";

			public const string PitchBend = "Pitch_bend_c";

			public const string ControlChange = "Control_c";

			public const string ProgramChange = "Program_c";

			public const string ChannelAftertouch = "Channel_aftertouch_c";

			public const string NoteAftertouch = "Poly_aftertouch_c";

			public const string SysExCompleted = "System_exclusive";

			public const string SysExIncompleted = "System_exclusive_packet";
		}
	}
	internal delegate string EventNameGetter(MidiEvent midiEvent);
	internal static class EventNameGetterProvider
	{
		private static readonly Dictionary<Type, EventNameGetter> EventsTypes_MidiCsv = new Dictionary<Type, EventNameGetter>
		{
			[typeof(SequenceTrackNameEvent)] = GetType("Title_t"),
			[typeof(CopyrightNoticeEvent)] = GetType("Copyright_t"),
			[typeof(InstrumentNameEvent)] = GetType("Instrument_name_t"),
			[typeof(MarkerEvent)] = GetType("Marker_t"),
			[typeof(CuePointEvent)] = GetType("Cue_point_t"),
			[typeof(LyricEvent)] = GetType("Lyric_t"),
			[typeof(TextEvent)] = GetType("Text_t"),
			[typeof(SequenceNumberEvent)] = GetType("Sequence_number"),
			[typeof(PortPrefixEvent)] = GetType("MIDI_port"),
			[typeof(ChannelPrefixEvent)] = GetType("Channel_prefix"),
			[typeof(TimeSignatureEvent)] = GetType("Time_signature"),
			[typeof(KeySignatureEvent)] = GetType("Key_signature"),
			[typeof(SetTempoEvent)] = GetType("Tempo"),
			[typeof(SmpteOffsetEvent)] = GetType("SMPTE_offset"),
			[typeof(SequencerSpecificEvent)] = GetType("Sequencer_specific"),
			[typeof(UnknownMetaEvent)] = GetType("Unknown_meta_event"),
			[typeof(NoteOnEvent)] = GetType("Note_on_c"),
			[typeof(NoteOffEvent)] = GetType("Note_off_c"),
			[typeof(PitchBendEvent)] = GetType("Pitch_bend_c"),
			[typeof(ControlChangeEvent)] = GetType("Control_c"),
			[typeof(ProgramChangeEvent)] = GetType("Program_c"),
			[typeof(ChannelAftertouchEvent)] = GetType("Channel_aftertouch_c"),
			[typeof(NoteAftertouchEvent)] = GetType("Poly_aftertouch_c"),
			[typeof(NormalSysExEvent)] = GetSysExType("System_exclusive", "System_exclusive_packet"),
			[typeof(EscapeSysExEvent)] = GetSysExType("System_exclusive", "System_exclusive_packet")
		};

		private static readonly Dictionary<Type, EventNameGetter> EventsTypes_DryWetMidi = new Dictionary<Type, EventNameGetter>
		{
			[typeof(SequenceTrackNameEvent)] = GetType("Sequence/Track Name"),
			[typeof(CopyrightNoticeEvent)] = GetType("Copyright Notice"),
			[typeof(InstrumentNameEvent)] = GetType("Instrument Name"),
			[typeof(MarkerEvent)] = GetType("Marker"),
			[typeof(CuePointEvent)] = GetType("Cue Point"),
			[typeof(LyricEvent)] = GetType("Lyric"),
			[typeof(TextEvent)] = GetType("Text"),
			[typeof(SequenceNumberEvent)] = GetType("Sequence Number"),
			[typeof(PortPrefixEvent)] = GetType("Port Prefix"),
			[typeof(ChannelPrefixEvent)] = GetType("Channel Prefix"),
			[typeof(TimeSignatureEvent)] = GetType("Time Signature"),
			[typeof(KeySignatureEvent)] = GetType("Key Signature"),
			[typeof(SetTempoEvent)] = GetType("Set Tempo"),
			[typeof(SmpteOffsetEvent)] = GetType("SMPTE Offset"),
			[typeof(SequencerSpecificEvent)] = GetType("Sequencer Specific"),
			[typeof(UnknownMetaEvent)] = GetType("Unknown Meta"),
			[typeof(NoteOnEvent)] = GetType("Note On"),
			[typeof(NoteOffEvent)] = GetType("Note Off"),
			[typeof(PitchBendEvent)] = GetType("Pitch Bend"),
			[typeof(ControlChangeEvent)] = GetType("Control Change"),
			[typeof(ProgramChangeEvent)] = GetType("Program Change"),
			[typeof(ChannelAftertouchEvent)] = GetType("Channel Aftertouch"),
			[typeof(NoteAftertouchEvent)] = GetType("Note Aftertouch"),
			[typeof(NormalSysExEvent)] = GetSysExType("System Exclusive", "System Exclusive Packet"),
			[typeof(EscapeSysExEvent)] = GetSysExType("System Exclusive", "System Exclusive Packet")
		};

		public static EventNameGetter Get(Type eventType, MidiFileCsvLayout layout)
		{
			return layout switch
			{
				MidiFileCsvLayout.DryWetMidi => EventsTypes_DryWetMidi[eventType], 
				MidiFileCsvLayout.MidiCsv => EventsTypes_MidiCsv[eventType], 
				_ => null, 
			};
		}

		private static EventNameGetter GetType(string type)
		{
			return (MidiEvent e) => type;
		}

		private static EventNameGetter GetSysExType(string completedType, string incompletedType)
		{
			return (MidiEvent e) => (!((SysExEvent)e).Completed) ? incompletedType : completedType;
		}
	}
	internal delegate object[] EventParametersGetter(MidiEvent midiEvent, MidiFileCsvConversionSettings settings);
	internal static class EventParametersGetterProvider
	{
		private static readonly Dictionary<Type, EventParametersGetter> EventsParametersGetters = new Dictionary<Type, EventParametersGetter>
		{
			[typeof(SequenceTrackNameEvent)] = GetParameters<SequenceTrackNameEvent>((SequenceTrackNameEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(CopyrightNoticeEvent)] = GetParameters<CopyrightNoticeEvent>((CopyrightNoticeEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(InstrumentNameEvent)] = GetParameters<InstrumentNameEvent>((InstrumentNameEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(MarkerEvent)] = GetParameters<MarkerEvent>((MarkerEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(CuePointEvent)] = GetParameters<CuePointEvent>((CuePointEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(LyricEvent)] = GetParameters<LyricEvent>((LyricEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(TextEvent)] = GetParameters<TextEvent>((TextEvent e, MidiFileCsvConversionSettings s) => e.Text),
			[typeof(SequenceNumberEvent)] = GetParameters<SequenceNumberEvent>((SequenceNumberEvent e, MidiFileCsvConversionSettings s) => e.Number),
			[typeof(PortPrefixEvent)] = GetParameters<PortPrefixEvent>((PortPrefixEvent e, MidiFileCsvConversionSettings s) => e.Port),
			[typeof(ChannelPrefixEvent)] = GetParameters<ChannelPrefixEvent>((ChannelPrefixEvent e, MidiFileCsvConversionSettings s) => e.Channel),
			[typeof(TimeSignatureEvent)] = GetParameters<TimeSignatureEvent>((TimeSignatureEvent e, MidiFileCsvConversionSettings s) => e.Numerator, (TimeSignatureEvent e, MidiFileCsvConversionSettings s) => s.CsvLayout switch
			{
				MidiFileCsvLayout.DryWetMidi => e.Denominator, 
				MidiFileCsvLayout.MidiCsv => (byte)Math.Log((int)e.Denominator, 2.0), 
				_ => null, 
			}, (TimeSignatureEvent e, MidiFileCsvConversionSettings s) => e.ClocksPerClick, (TimeSignatureEvent e, MidiFileCsvConversionSettings s) => e.ThirtySecondNotesPerBeat),
			[typeof(KeySignatureEvent)] = GetParameters<KeySignatureEvent>((KeySignatureEvent e, MidiFileCsvConversionSettings s) => e.Key, (KeySignatureEvent e, MidiFileCsvConversionSettings s) => e.Scale),
			[typeof(SetTempoEvent)] = GetParameters<SetTempoEvent>((SetTempoEvent e, MidiFileCsvConversionSettings s) => e.MicrosecondsPerQuarterNote),
			[typeof(SmpteOffsetEvent)] = GetParameters<SmpteOffsetEvent>((SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => SmpteData.GetFormatAndHours(e.Format, e.Hours), (SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.Minutes, (SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.Seconds, (SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.Frames, (SmpteOffsetEvent e, MidiFileCsvConversionSettings s) => e.SubFrames),
			[typeof(SequencerSpecificEvent)] = GetParameters<SequencerSpecificEvent>((SequencerSpecificEvent e, MidiFileCsvConversionSettings s) => e.Data.Length, (SequencerSpecificEvent e, MidiFileCsvConversionSettings s) => e.Data),
			[typeof(UnknownMetaEvent)] = GetParameters<UnknownMetaEvent>((UnknownMetaEvent e, MidiFileCsvConversionSettings s) => e.StatusByte, (UnknownMetaEvent e, MidiFileCsvConversionSettings s) => e.Data.Length, (UnknownMetaEvent e, MidiFileCsvConversionSettings s) => e.Data),
			[typeof(NoteOnEvent)] = GetParameters<NoteOnEvent>((NoteOnEvent e, MidiFileCsvConversionSettings s) => e.Channel, (NoteOnEvent e, MidiFileCsvConversionSettings s) => FormatNoteNumber(e.NoteNumber, s), (NoteOnEvent e, MidiFileCsvConversionSettings s) => e.Velocity),
			[typeof(NoteOffEvent)] = GetParameters<NoteOffEvent>((NoteOffEvent e, MidiFileCsvConversionSettings s) => e.Channel, (NoteOffEvent e, MidiFileCsvConversionSettings s) => FormatNoteNumber(e.NoteNumber, s), (NoteOffEvent e, MidiFileCsvConversionSettings s) => e.Velocity),
			[typeof(PitchBendEvent)] = GetParameters<PitchBendEvent>((PitchBendEvent e, MidiFileCsvConversionSettings s) => e.Channel, (PitchBendEvent e, MidiFileCsvConversionSettings s) => e.PitchValue),
			[typeof(ControlChangeEvent)] = GetParameters<ControlChangeEvent>((ControlChangeEvent e, MidiFileCsvConversionSettings s) => e.Channel, (ControlChangeEvent e, MidiFileCsvConversionSettings s) => e.ControlNumber, (ControlChangeEvent e, MidiFileCsvConversionSettings s) => e.ControlValue),
			[typeof(ProgramChangeEvent)] = GetParameters<ProgramChangeEvent>((ProgramChangeEvent e, MidiFileCsvConversionSettings s) => e.Channel, (ProgramChangeEvent e, MidiFileCsvConversionSettings s) => e.ProgramNumber),
			[typeof(ChannelAftertouchEvent)] = GetParameters<ChannelAftertouchEvent>((ChannelAftertouchEvent e, MidiFileCsvConversionSettings s) => e.Channel, (ChannelAftertouchEvent e, MidiFileCsvConversionSettings s) => e.AftertouchValue),
			[typeof(NoteAftertouchEvent)] = GetParameters<NoteAftertouchEvent>((NoteAftertouchEvent e, MidiFileCsvConversionSettings s) => e.Channel, (NoteAftertouchEvent e, MidiFileCsvConversionSettings s) => FormatNoteNumber(e.NoteNumber, s), (NoteAftertouchEvent e, MidiFileCsvConversionSettings s) => e.AftertouchValue),
			[typeof(NormalSysExEvent)] = GetParameters<NormalSysExEvent>((NormalSysExEvent e, MidiFileCsvConversionSettings s) => e.Data.Length, (NormalSysExEvent e, MidiFileCsvConversionSettings s) => e.Data),
			[typeof(EscapeSysExEvent)] = GetParameters<EscapeSysExEvent>((EscapeSysExEvent e, MidiFileCsvConversionSettings s) => e.Data.Length, (EscapeSysExEvent e, MidiFileCsvConversionSettings s) => e.Data)
		};

		public static EventParametersGetter Get(Type eventType)
		{
			return EventsParametersGetters[eventType];
		}

		private static EventParametersGetter GetParameters<T>(params Func<T, MidiFileCsvConversionSettings, object>[] parametersGetters) where T : MidiEvent
		{
			return (MidiEvent e, MidiFileCsvConversionSettings s) => parametersGetters.Select((Func<T, MidiFileCsvConversionSettings, object> g) => g((T)e, s)).ToArray();
		}

		private static object FormatNoteNumber(SevenBitNumber noteNumber, MidiFileCsvConversionSettings settings)
		{
			if (settings.CsvLayout == MidiFileCsvLayout.MidiCsv)
			{
				return noteNumber;
			}
			return NoteCsvConversionUtilities.FormatNoteNumber(noteNumber, settings.NoteNumberFormat);
		}
	}
	internal static class MidiFileToCsvConverter
	{
		public static void ConvertToCsv(MidiFile midiFile, Stream stream, MidiFileCsvConversionSettings settings)
		{
			using CsvWriter csvWriter = new CsvWriter(stream, settings.CsvSettings);
			int num = 0;
			TempoMap tempoMap = midiFile.GetTempoMap();
			WriteHeader(csvWriter, midiFile, settings, tempoMap);
			foreach (TrackChunk trackChunk in midiFile.GetTrackChunks())
			{
				WriteTrackChunkStart(csvWriter, num, settings, tempoMap);
				long time = 0L;
				IEnumerable<TimedEvent> timedEventsLazy = trackChunk.Events.GetTimedEventsLazy(cloneEvent: false);
				IEnumerable<ITimedObject> enumerable;
				if (settings.CsvLayout != MidiFileCsvLayout.MidiCsv && settings.NoteFormat != NoteFormat.Events)
				{
					IEnumerable<ITimedObject> objects = timedEventsLazy.GetObjects(ObjectType.TimedEvent | ObjectType.Note);
					enumerable = objects;
				}
				else
				{
					IEnumerable<ITimedObject> objects = timedEventsLazy;
					enumerable = objects;
				}
				foreach (ITimedObject item in enumerable)
				{
					time = item.Time;
					if (item is TimedEvent timedEvent)
					{
						WriteTimedEvent(timedEvent, csvWriter, num, time, settings, tempoMap);
					}
					else if (item is Melanchall.DryWetMidi.Interaction.Note note)
					{
						WriteNote(note, csvWriter, num, time, settings, tempoMap);
					}
				}
				WriteTrackChunkEnd(csvWriter, num, time, settings, tempoMap);
				num++;
			}
			WriteFileEnd(csvWriter, settings, tempoMap);
		}

		private static void WriteNote(Melanchall.DryWetMidi.Interaction.Note note, CsvWriter csvWriter, int trackNumber, long time, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			object obj = ((settings.NoteNumberFormat == NoteNumberFormat.NoteNumber) ? ((object)note.NoteNumber) : note);
			ITimeSpan timeSpan = TimeConverter.ConvertTo(note.Length, settings.NoteLengthType, tempoMap);
			WriteRecord(csvWriter, trackNumber, time, "Note", settings, tempoMap, note.Channel, obj, timeSpan, note.Velocity, note.OffVelocity);
		}

		private static void WriteTimedEvent(TimedEvent timedEvent, CsvWriter csvWriter, int trackNumber, long time, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiEvent midiEvent = timedEvent.Event;
			Type type = midiEvent.GetType();
			string type2 = EventNameGetterProvider.Get(type, settings.CsvLayout)(midiEvent);
			object[] parameters = EventParametersGetterProvider.Get(type)(midiEvent, settings);
			WriteRecord(csvWriter, trackNumber, time, type2, settings, tempoMap, parameters);
		}

		private static void WriteHeader(CsvWriter csvWriter, MidiFile midiFile, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileFormat? midiFileFormat = null;
			try
			{
				midiFileFormat = midiFile.OriginalFormat;
			}
			catch
			{
			}
			int num = midiFile.GetTrackChunks().Count();
			switch (settings.CsvLayout)
			{
			case MidiFileCsvLayout.DryWetMidi:
				WriteRecord(csvWriter, null, null, "Header", settings, tempoMap, midiFileFormat, midiFile.TimeDivision.ToInt16());
				break;
			case MidiFileCsvLayout.MidiCsv:
				WriteRecord(csvWriter, 0, 0L, "Header", settings, tempoMap, midiFileFormat.HasValue ? ((int)midiFileFormat.Value) : ((num > 1) ? 1 : 0), num, midiFile.TimeDivision.ToInt16());
				break;
			}
		}

		private static void WriteTrackChunkStart(CsvWriter csvWriter, int trackNumber, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout != MidiFileCsvLayout.DryWetMidi && csvLayout == MidiFileCsvLayout.MidiCsv)
			{
				WriteRecord(csvWriter, trackNumber, 0L, "Start_track", settings, tempoMap);
			}
		}

		private static void WriteTrackChunkEnd(CsvWriter csvWriter, int trackNumber, long time, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout != MidiFileCsvLayout.DryWetMidi && csvLayout == MidiFileCsvLayout.MidiCsv)
			{
				WriteRecord(csvWriter, trackNumber, time, "End_track", settings, tempoMap);
			}
		}

		private static void WriteFileEnd(CsvWriter csvWriter, MidiFileCsvConversionSettings settings, TempoMap tempoMap)
		{
			MidiFileCsvLayout csvLayout = settings.CsvLayout;
			if (csvLayout != MidiFileCsvLayout.DryWetMidi && csvLayout == MidiFileCsvLayout.MidiCsv)
			{
				WriteRecord(csvWriter, 0, 0L, "End_of_file", settings, tempoMap);
			}
		}

		private static void WriteRecord(CsvWriter csvWriter, int? trackNumber, long? time, string type, MidiFileCsvConversionSettings settings, TempoMap tempoMap, params object[] parameters)
		{
			ITimeSpan timeSpan = ((!time.HasValue) ? null : TimeConverter.ConvertTo(time.Value, settings.TimeType, tempoMap));
			IEnumerable<object> second = parameters.SelectMany(ProcessParameter);
			csvWriter.WriteRecord(new object[3] { trackNumber, timeSpan, type }.Concat(second));
		}

		private static object[] ProcessParameter(object parameter)
		{
			if (parameter == null)
			{
				return new object[1] { string.Empty };
			}
			if (parameter is byte[] source)
			{
				return source.OfType<object>().ToArray();
			}
			if (parameter is string input)
			{
				parameter = CsvUtilities.EscapeString(input);
			}
			return new object[1] { parameter };
		}
	}
	internal static class CsvToNotesConverter
	{
		public static IEnumerable<Melanchall.DryWetMidi.Interaction.Note> ConvertToNotes(Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings)
		{
			using CsvReader csvReader = new CsvReader(stream, settings.CsvSettings);
			CsvRecord csvRecord;
			while ((csvRecord = csvReader.ReadRecord()) != null)
			{
				string[] values = csvRecord.Values;
				if (values.Length < 6)
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Missing required parameters.");
				}
				if (!TimeSpanUtilities.TryParse(values[0], settings.TimeType, out var timeSpan))
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid time.");
				}
				if (!FourBitNumber.TryParse(values[1], out var fourBitNumber))
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid channel.");
				}
				if (!TryParseNoteNumber(values[2], settings.NoteNumberFormat, out var result))
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid note number or letter.");
				}
				if (!TimeSpanUtilities.TryParse(values[3], settings.NoteLengthType, out var timeSpan2))
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid length.");
				}
				if (!SevenBitNumber.TryParse(values[4], out var sevenBitNumber))
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid velocity.");
				}
				if (!SevenBitNumber.TryParse(values[5], out var sevenBitNumber2))
				{
					CsvError.ThrowBadFormat(csvRecord.LineNumber, "Invalid off velocity.");
				}
				long time = TimeConverter.ConvertFrom(timeSpan, tempoMap);
				long length = LengthConverter.ConvertFrom(timeSpan2, time, tempoMap);
				yield return new Melanchall.DryWetMidi.Interaction.Note(result, length, time)
				{
					Channel = fourBitNumber,
					Velocity = sevenBitNumber,
					OffVelocity = sevenBitNumber2
				};
			}
		}

		public static bool TryParseNoteNumber(string input, NoteNumberFormat noteNumberFormat, out SevenBitNumber result)
		{
			result = default(SevenBitNumber);
			switch (noteNumberFormat)
			{
			case NoteNumberFormat.NoteNumber:
				return SevenBitNumber.TryParse(input, out result);
			case NoteNumberFormat.Letter:
			{
				if (!Melanchall.DryWetMidi.MusicTheory.Note.TryParse(input, out var note))
				{
					return false;
				}
				result = note.NoteNumber;
				return true;
			}
			default:
				return false;
			}
		}
	}
	public sealed class NoteCsvConversionSettings
	{
		private TimeSpanType _timeType = TimeSpanType.Midi;

		private TimeSpanType _noteLengthType = TimeSpanType.Midi;

		private NoteNumberFormat _noteNumberFormat;

		public TimeSpanType TimeType
		{
			get
			{
				return _timeType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_timeType = value;
			}
		}

		public TimeSpanType NoteLengthType
		{
			get
			{
				return _noteLengthType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteLengthType = value;
			}
		}

		public NoteNumberFormat NoteNumberFormat
		{
			get
			{
				return _noteNumberFormat;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteNumberFormat = value;
			}
		}

		public CsvSettings CsvSettings { get; } = new CsvSettings();
	}
	internal static class NoteCsvConversionUtilities
	{
		public static object FormatNoteNumber(SevenBitNumber noteNumber, NoteNumberFormat noteNumberFormat)
		{
			return noteNumberFormat switch
			{
				NoteNumberFormat.NoteNumber => noteNumber, 
				NoteNumberFormat.Letter => Melanchall.DryWetMidi.MusicTheory.Note.Get(noteNumber), 
				_ => null, 
			};
		}
	}
	public enum NoteNumberFormat
	{
		NoteNumber,
		Letter
	}
	internal static class NotesToCsvConverter
	{
		public static void ConvertToCsv(IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes, Stream stream, TempoMap tempoMap, NoteCsvConversionSettings settings)
		{
			using CsvWriter csvWriter = new CsvWriter(stream, settings.CsvSettings);
			foreach (Melanchall.DryWetMidi.Interaction.Note item in notes.Where((Melanchall.DryWetMidi.Interaction.Note n) => n != null))
			{
				csvWriter.WriteRecord(new object[6]
				{
					item.TimeAs(settings.TimeType, tempoMap),
					item.Channel,
					NoteCsvConversionUtilities.FormatNoteNumber(item.NoteNumber, settings.NoteNumberFormat),
					item.LengthAs(settings.NoteLengthType, tempoMap),
					item.Velocity,
					item.OffVelocity
				});
			}
		}
	}
	public abstract class LengthedObjectsSplitter<TObject> where TObject : ILengthedObject
	{
		internal const double ZeroRatio = 0.0;

		internal const double FullLengthRatio = 1.0;

		public IEnumerable<TObject> SplitByStep(IEnumerable<TObject> objects, ITimeSpan step, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject @object in objects)
			{
				if (@object == null)
				{
					yield return default(TObject);
					continue;
				}
				if (@object.Length == 0L)
				{
					yield return CloneObject(@object);
					continue;
				}
				long time = @object.Time;
				long endTime = time + @object.Length;
				long time2 = time;
				TObject val = CloneObject(@object);
				while (time2 < endTime && val != null)
				{
					long num = LengthConverter.ConvertFrom(step, time2, tempoMap);
					if (num == 0L)
					{
						throw new InvalidOperationException("Step is too small.");
					}
					time2 += num;
					SplitLengthedObject<TObject> parts = SplitObject(val, time2);
					yield return parts.LeftPart;
					val = parts.RightPart;
				}
			}
		}

		public IEnumerable<TObject> SplitByPartsNumber(IEnumerable<TObject> objects, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject obj in objects)
			{
				if (obj == null)
				{
					yield return default(TObject);
					continue;
				}
				if (partsNumber == 1)
				{
					yield return CloneObject(obj);
					continue;
				}
				if (obj.Length == 0L)
				{
					foreach (int item in Enumerable.Range(0, partsNumber))
					{
						_ = item;
						yield return CloneObject(obj);
					}
					continue;
				}
				long time = obj.Time;
				TObject val = CloneObject(obj);
				int partsRemaining = partsNumber;
				while (partsRemaining > 1 && val != null)
				{
					ITimeSpan length = val.LengthAs(lengthType, tempoMap).Divide(partsRemaining);
					time += LengthConverter.ConvertFrom(length, time, tempoMap);
					SplitLengthedObject<TObject> parts = SplitObject(val, time);
					yield return parts.LeftPart;
					val = parts.RightPart;
					partsRemaining--;
				}
				if (val != null)
				{
					yield return val;
				}
			}
		}

		public IEnumerable<TObject> SplitByGrid(IEnumerable<TObject> objects, IGrid grid, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			long lastObjectEndTime = (from o in objects
				where o != null
				select o.Time + o.Length).DefaultIfEmpty().Max();
			List<long> times = grid.GetTimes(tempoMap).TakeWhile((long t) => t < lastObjectEndTime).Distinct()
				.ToList();
			times.Sort();
			foreach (TObject @object in objects)
			{
				if (@object == null)
				{
					yield return default(TObject);
					continue;
				}
				long startTime = @object.Time;
				long endTime = startTime + @object.Length;
				IEnumerable<long> enumerable = times.SkipWhile((long t) => t <= startTime).TakeWhile((long t) => t < endTime);
				TObject val = CloneObject(@object);
				foreach (long item in enumerable)
				{
					SplitLengthedObject<TObject> parts = SplitObject(val, item);
					yield return parts.LeftPart;
					val = parts.RightPart;
				}
				yield return val;
			}
		}

		public IEnumerable<TObject> SplitAtDistance(IEnumerable<TObject> objects, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject @object in objects)
			{
				if (@object == null)
				{
					yield return default(TObject);
					continue;
				}
				SplitLengthedObject<TObject> parts = SplitObjectAtDistance(@object, distance, from, tempoMap);
				if (parts.LeftPart != null)
				{
					yield return parts.LeftPart;
				}
				if (parts.RightPart != null)
				{
					yield return parts.RightPart;
				}
			}
		}

		public IEnumerable<TObject> SplitAtDistance(IEnumerable<TObject> objects, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TObject @object in objects)
			{
				if (@object == null)
				{
					yield return default(TObject);
					continue;
				}
				ITimeSpan distance = @object.LengthAs(lengthType, tempoMap).Multiply(ratio);
				SplitLengthedObject<TObject> parts = SplitObjectAtDistance(@object, distance, from, tempoMap);
				if (parts.LeftPart != null)
				{
					yield return parts.LeftPart;
				}
				if (parts.RightPart != null)
				{
					yield return parts.RightPart;
				}
			}
		}

		protected abstract TObject CloneObject(TObject obj);

		protected abstract SplitLengthedObject<TObject> SplitObject(TObject obj, long time);

		private SplitLengthedObject<TObject> SplitObjectAtDistance(TObject obj, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap)
		{
			ITimeSpan time = ((from == LengthedObjectTarget.Start) ? ((MidiTimeSpan)obj.Time).Add(distance, TimeSpanMode.TimeLength) : ((MidiTimeSpan)(obj.Time + obj.Length)).Subtract(distance, TimeSpanMode.TimeLength));
			return SplitObject(obj, TimeConverter.ConvertFrom(time, tempoMap));
		}
	}
	public sealed class ChordsSplitter : LengthedObjectsSplitter<Melanchall.DryWetMidi.Interaction.Chord>
	{
		protected override Melanchall.DryWetMidi.Interaction.Chord CloneObject(Melanchall.DryWetMidi.Interaction.Chord obj)
		{
			return obj.Clone();
		}

		protected override SplitLengthedObject<Melanchall.DryWetMidi.Interaction.Chord> SplitObject(Melanchall.DryWetMidi.Interaction.Chord obj, long time)
		{
			return obj.Split(time);
		}
	}
	public sealed class NotesSplitter : LengthedObjectsSplitter<Melanchall.DryWetMidi.Interaction.Note>
	{
		protected override Melanchall.DryWetMidi.Interaction.Note CloneObject(Melanchall.DryWetMidi.Interaction.Note obj)
		{
			return obj.Clone();
		}

		protected override SplitLengthedObject<Melanchall.DryWetMidi.Interaction.Note> SplitObject(Melanchall.DryWetMidi.Interaction.Note obj, long time)
		{
			return obj.Split(time);
		}
	}
	public static class ChordsSplitterUtilities
	{
		public static void SplitChordsByStep(this TrackChunk trackChunk, ITimeSpan step, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkChords(trackChunk, chordDetectionSettings, (ChordsSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord> chords) => splitter.SplitByStep(chords, step, tempoMap));
		}

		public static void SplitChordsByStep(this IEnumerable<TrackChunk> trackChunks, ITimeSpan step, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsByStep(step, tempoMap, chordDetectionSettings);
			}
		}

		public static void SplitChordsByStep(this MidiFile midiFile, ITimeSpan step, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("step", step);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsByStep(step, tempoMap, chordDetectionSettings);
		}

		public static void SplitChordsByPartsNumber(this TrackChunk trackChunk, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkChords(trackChunk, chordDetectionSettings, (ChordsSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord> chords) => splitter.SplitByPartsNumber(chords, partsNumber, lengthType, tempoMap));
		}

		public static void SplitChordsByPartsNumber(this IEnumerable<TrackChunk> trackChunks, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsByPartsNumber(partsNumber, lengthType, tempoMap, chordDetectionSettings);
			}
		}

		public static void SplitChordsByPartsNumber(this MidiFile midiFile, int partsNumber, TimeSpanType lengthType, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsByPartsNumber(partsNumber, lengthType, tempoMap, chordDetectionSettings);
		}

		public static void SplitChordsByGrid(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkChords(trackChunk, chordDetectionSettings, (ChordsSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord> chords) => splitter.SplitByGrid(chords, grid, tempoMap));
		}

		public static void SplitChordsByGrid(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsByGrid(grid, tempoMap, chordDetectionSettings);
			}
		}

		public static void SplitChordsByGrid(this MidiFile midiFile, IGrid grid, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsByGrid(grid, tempoMap, settings);
		}

		public static void SplitChordsAtDistance(this TrackChunk trackChunk, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkChords(trackChunk, chordDetectionSettings, (ChordsSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord> chords) => splitter.SplitAtDistance(chords, distance, from, tempoMap));
		}

		public static void SplitChordsAtDistance(this IEnumerable<TrackChunk> trackChunks, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsAtDistance(distance, from, tempoMap, chordDetectionSettings);
			}
		}

		public static void SplitChordsAtDistance(this MidiFile midiFile, ITimeSpan distance, LengthedObjectTarget from, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsAtDistance(distance, from, tempoMap, chordDetectionSettings);
		}

		public static void SplitChordsAtDistance(this TrackChunk trackChunk, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkChords(trackChunk, chordDetectionSettings, (ChordsSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord> chords) => splitter.SplitAtDistance(chords, ratio, lengthType, from, tempoMap));
		}

		public static void SplitChordsAtDistance(this IEnumerable<TrackChunk> trackChunks, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitChordsAtDistance(ratio, lengthType, from, tempoMap, chordDetectionSettings);
			}
		}

		public static void SplitChordsAtDistance(this MidiFile midiFile, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, ChordDetectionSettings chordDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitChordsAtDistance(ratio, lengthType, from, tempoMap, chordDetectionSettings);
		}

		private static void SplitTrackChunkChords(TrackChunk trackChunk, ChordDetectionSettings chordDetectionSettings, Func<ChordsSplitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord>, IEnumerable<Melanchall.DryWetMidi.Interaction.Chord>> splitOperation)
		{
			using ChordsManager chordsManager = trackChunk.ManageChords(chordDetectionSettings);
			ChordsCollection chords = chordsManager.Chords;
			ChordsSplitter arg = new ChordsSplitter();
			List<Melanchall.DryWetMidi.Interaction.Chord> objects = splitOperation(arg, chords).ToList();
			chords.Clear();
			chords.Add(objects);
		}
	}
	public static class NotesSplitterUtilities
	{
		public static void SplitNotesByStep(this TrackChunk trackChunk, ITimeSpan step, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkNotes(trackChunk, noteDetectionSettings, (NotesSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes) => splitter.SplitByStep(notes, step, tempoMap));
		}

		public static void SplitNotesByStep(this IEnumerable<TrackChunk> trackChunks, ITimeSpan step, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("step", step);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesByStep(step, tempoMap, noteDetectionSettings);
			}
		}

		public static void SplitNotesByStep(this MidiFile midiFile, ITimeSpan step, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("step", step);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesByStep(step, tempoMap, noteDetectionSettings);
		}

		public static void SplitNotesByPartsNumber(this TrackChunk trackChunk, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkNotes(trackChunk, noteDetectionSettings, (NotesSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes) => splitter.SplitByPartsNumber(notes, partsNumber, lengthType, tempoMap));
		}

		public static void SplitNotesByPartsNumber(this IEnumerable<TrackChunk> trackChunks, int partsNumber, TimeSpanType lengthType, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesByPartsNumber(partsNumber, lengthType, tempoMap, noteDetectionSettings);
			}
		}

		public static void SplitNotesByPartsNumber(this MidiFile midiFile, int partsNumber, TimeSpanType lengthType, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNonpositive("partsNumber", partsNumber, "Parts number is zero or negative.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesByPartsNumber(partsNumber, lengthType, tempoMap, noteDetectionSettings);
		}

		public static void SplitNotesByGrid(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkNotes(trackChunk, noteDetectionSettings, (NotesSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes) => splitter.SplitByGrid(notes, grid, tempoMap));
		}

		public static void SplitNotesByGrid(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesByGrid(grid, tempoMap, noteDetectionSettings);
			}
		}

		public static void SplitNotesByGrid(this MidiFile midiFile, IGrid grid, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesByGrid(grid, tempoMap, noteDetectionSettings);
		}

		public static void SplitNotesAtDistance(this TrackChunk trackChunk, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkNotes(trackChunk, noteDetectionSettings, (NotesSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes) => splitter.SplitAtDistance(notes, distance, from, tempoMap));
		}

		public static void SplitNotesAtDistance(this IEnumerable<TrackChunk> trackChunks, ITimeSpan distance, LengthedObjectTarget from, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesAtDistance(distance, from, tempoMap, noteDetectionSettings);
			}
		}

		public static void SplitNotesAtDistance(this MidiFile midiFile, ITimeSpan distance, LengthedObjectTarget from, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesAtDistance(distance, from, tempoMap, noteDetectionSettings);
		}

		public static void SplitNotesAtDistance(this TrackChunk trackChunk, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			SplitTrackChunkNotes(trackChunk, noteDetectionSettings, (NotesSplitter splitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes) => splitter.SplitAtDistance(notes, ratio, lengthType, from, tempoMap));
		}

		public static void SplitNotesAtDistance(this IEnumerable<TrackChunk> trackChunks, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, TempoMap tempoMap, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.SplitNotesAtDistance(ratio, lengthType, from, tempoMap, noteDetectionSettings);
			}
		}

		public static void SplitNotesAtDistance(this MidiFile midiFile, double ratio, TimeSpanType lengthType, LengthedObjectTarget from, NoteDetectionSettings noteDetectionSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsOutOfRange("ratio", ratio, 0.0, 1.0, $"Ratio is out of [{0.0}; {1.0}] range.");
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsInvalidEnumValue("from", from);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().SplitNotesAtDistance(ratio, lengthType, from, tempoMap, noteDetectionSettings);
		}

		private static void SplitTrackChunkNotes(TrackChunk trackChunk, NoteDetectionSettings noteDetectionSettings, Func<NotesSplitter, IEnumerable<Melanchall.DryWetMidi.Interaction.Note>, IEnumerable<Melanchall.DryWetMidi.Interaction.Note>> splitOperation)
		{
			using NotesManager notesManager = trackChunk.ManageNotes(noteDetectionSettings);
			NotesCollection notes = notesManager.Notes;
			NotesSplitter arg = new NotesSplitter();
			List<Melanchall.DryWetMidi.Interaction.Note> objects = splitOperation(arg, notes).ToList();
			notes.Clear();
			notes.Add(objects);
		}
	}
	internal sealed class MidiFileSlicer : IDisposable
	{
		private sealed class TimedEventsHolder : IDisposable
		{
			private bool _disposed;

			public IEnumerator<TimedEvent> Enumerator { get; }

			public List<TimedEvent> EventsToCopyToNextPart { get; } = new List<TimedEvent>();

			public List<TimedEvent> EventsToStartNextPart { get; } = new List<TimedEvent>();

			public TimedEventsHolder(IEnumerator<TimedEvent> timedEventsEumerator)
			{
				Enumerator = timedEventsEumerator;
				Enumerator.MoveNext();
			}

			private void Dispose(bool disposing)
			{
				if (!_disposed)
				{
					if (disposing)
					{
						Enumerator.Dispose();
					}
					_disposed = true;
				}
			}

			public void Dispose()
			{
				Dispose(disposing: true);
			}
		}

		private static readonly Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>> DefaultUpdatePredicates = new Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>>
		{
			[MidiEventType.ChannelAftertouch] = delegate(MidiEvent midiEvent, MidiEvent existingMidiEvent)
			{
				FourBitNumber channel = ((ChannelAftertouchEvent)midiEvent).Channel;
				return (byte)((ChannelAftertouchEvent)existingMidiEvent).Channel == (byte)channel;
			},
			[MidiEventType.ControlChange] = delegate(MidiEvent midiEvent, MidiEvent existingMidiEvent)
			{
				ControlChangeEvent obj = (ControlChangeEvent)midiEvent;
				SevenBitNumber controlNumber = obj.ControlNumber;
				FourBitNumber channel = obj.Channel;
				ControlChangeEvent controlChangeEvent = (ControlChangeEvent)existingMidiEvent;
				return (byte)controlChangeEvent.ControlNumber == (byte)controlNumber && (byte)controlChangeEvent.Channel == (byte)channel;
			},
			[MidiEventType.NoteAftertouch] = delegate(MidiEvent midiEvent, MidiEvent existingMidiEvent)
			{
				NoteAftertouchEvent obj = (NoteAftertouchEvent)midiEvent;
				SevenBitNumber noteNumber = obj.NoteNumber;
				FourBitNumber channel = obj.Channel;
				NoteAftertouchEvent noteAftertouchEvent = (NoteAftertouchEvent)existingMidiEvent;
				return (byte)noteAftertouchEvent.NoteNumber == (byte)noteNumber && (byte)noteAftertouchEvent.Channel == (byte)channel;
			},
			[MidiEventType.PitchBend] = delegate(MidiEvent midiEvent, MidiEvent existingMidiEvent)
			{
				FourBitNumber channel = ((PitchBendEvent)midiEvent).Channel;
				return (byte)((PitchBendEvent)existingMidiEvent).Channel == (byte)channel;
			},
			[MidiEventType.ProgramChange] = delegate(MidiEvent midiEvent, MidiEvent existingMidiEvent)
			{
				FourBitNumber channel = ((ProgramChangeEvent)midiEvent).Channel;
				return (byte)((ProgramChangeEvent)existingMidiEvent).Channel == (byte)channel;
			},
			[MidiEventType.CopyrightNotice] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.InstrumentName] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.ProgramName] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.SequenceTrackName] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.DeviceName] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.PortPrefix] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.SetTempo] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.ChannelPrefix] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.SequenceNumber] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.KeySignature] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.SmpteOffset] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true,
			[MidiEventType.TimeSignature] = (MidiEvent midiEvent, MidiEvent existingMidiEvent) => true
		};

		private readonly TimedEventsHolder[] _timedEventsHolders;

		private readonly TimeDivision _timeDivision;

		private long _lastTime;

		private bool _disposed;

		public bool AllEventsProcessed => _timedEventsHolders.All((TimedEventsHolder c) => !c.EventsToStartNextPart.Any() && c.Enumerator.Current == null);

		private MidiFileSlicer(TimeDivision timeDivision, IEnumerator<TimedEvent>[] timedEventsEnumerators)
		{
			_timedEventsHolders = timedEventsEnumerators.Select((IEnumerator<TimedEvent> e) => new TimedEventsHolder(e)).ToArray();
			_timeDivision = timeDivision;
		}

		public MidiFile GetNextSlice(long endTime, SliceMidiFileSettings settings)
		{
			return new MidiFile((from e in GetNextTimedEvents(endTime, settings.PreserveTimes, settings.Markers?.PartStartMarkerEventFactory, settings.Markers?.PartEndMarkerEventFactory, settings.Markers?.EmptyPartMarkerEventFactory)
				select e.ToTrackChunk() into c
				where settings.PreserveTrackChunks || c.Events.Any()
				select c).ToList())
			{
				TimeDivision = _timeDivision.Clone()
			};
		}

		public static MidiFileSlicer CreateFromFile(MidiFile midiFile)
		{
			IEnumerator<TimedEvent>[] timedEventsEnumerators = (from c in midiFile.GetTrackChunks()
				select c.Events.GetTimedEvents().GetEnumerator()).ToArray();
			return new MidiFileSlicer(midiFile.TimeDivision, timedEventsEnumerators);
		}

		private IEnumerable<IEnumerable<TimedEvent>> GetNextTimedEvents(long endTime, bool preserveTimes, Func<MidiEvent> partStartMarkerEventFactory, Func<MidiEvent> partEndMarkerEventFactory, Func<MidiEvent> emptyPartMarkerEventFactory)
		{
			bool isPartEmpty = true;
			for (int i = 0; i < _timedEventsHolders.Length; i++)
			{
				TimedEventsHolder obj = _timedEventsHolders[i];
				IEnumerator<TimedEvent> enumerator = obj.Enumerator;
				List<TimedEvent> eventsToCopyToNextPart = obj.EventsToCopyToNextPart;
				List<TimedEvent> eventsToStartNextPart = obj.EventsToStartNextPart;
				int newEventsStartIndex;
				List<TimedEvent> list = PrepareTakenTimedEvents(eventsToCopyToNextPart, preserveTimes, eventsToStartNextPart, out newEventsStartIndex);
				do
				{
					TimedEvent current = enumerator.Current;
					if (current == null)
					{
						break;
					}
					long time = current.Time;
					if (time > endTime)
					{
						break;
					}
					if (time == endTime)
					{
						if (!TryToMoveEdgeNoteOffsToPreviousPart(current, list))
						{
							eventsToStartNextPart.Add(current);
						}
					}
					else
					{
						UpdateEventsToCopyToNextPart(eventsToCopyToNextPart, current);
						list.Add(current);
					}
				}
				while (enumerator.MoveNext());
				isPartEmpty &= newEventsStartIndex >= list.Count;
				if (!preserveTimes)
				{
					MoveEventsToStart(list, newEventsStartIndex, _lastTime);
				}
				if (isPartEmpty && i == _timedEventsHolders.Length - 1 && emptyPartMarkerEventFactory != null)
				{
					list.Insert(0, new TimedEvent(emptyPartMarkerEventFactory(), preserveTimes ? _lastTime : 0));
				}
				if (partStartMarkerEventFactory != null)
				{
					list.Insert(0, new TimedEvent(partStartMarkerEventFactory(), preserveTimes ? _lastTime : 0));
				}
				if (partEndMarkerEventFactory != null)
				{
					list.Add(new TimedEvent(partEndMarkerEventFactory(), preserveTimes ? endTime : (endTime - _lastTime)));
				}
				yield return list;
			}
			_lastTime = endTime;
		}

		private static bool TryToMoveEdgeNoteOffsToPreviousPart(TimedEvent timedEvent, List<TimedEvent> takenTimedEvents)
		{
			if (timedEvent.Event is NoteOffEvent)
			{
				takenTimedEvents.Add(timedEvent);
				return true;
			}
			return false;
		}

		private static void MoveEventsToStart(List<TimedEvent> takenTimedEvents, int startIndex, long partStartTime)
		{
			for (int i = startIndex; i < takenTimedEvents.Count; i++)
			{
				takenTimedEvents[i].Time -= partStartTime;
			}
		}

		private List<TimedEvent> PrepareTakenTimedEvents(List<TimedEvent> eventsToCopyToNextPart, bool preserveTimes, List<TimedEvent> eventsToStartNextPart, out int newEventsStartIndex)
		{
			List<TimedEvent> list = new List<TimedEvent>(eventsToCopyToNextPart);
			list.ForEach(delegate(TimedEvent e)
			{
				e.Time = (preserveTimes ? _lastTime : 0);
			});
			newEventsStartIndex = list.Count;
			list.AddRange(eventsToStartNextPart);
			eventsToStartNextPart.Clear();
			foreach (TimedEvent item in list)
			{
				UpdateEventsToCopyToNextPart(eventsToCopyToNextPart, item);
			}
			return list;
		}

		private static void UpdateEventsToCopyToNextPart(List<TimedEvent> eventsToCopyToNextPart, TimedEvent timedEvent)
		{
			MidiEventType eventType = timedEvent.Event.EventType;
			bool flag = false;
			for (int i = 0; i < eventsToCopyToNextPart.Count; i++)
			{
				if (flag)
				{
					break;
				}
				TimedEvent timedEvent2 = eventsToCopyToNextPart[i];
				if (timedEvent2.Event.EventType == eventType)
				{
					flag = DefaultUpdatePredicates[eventType](timedEvent.Event, timedEvent2.Event);
					if (flag)
					{
						eventsToCopyToNextPart.RemoveAt(i);
						eventsToCopyToNextPart.Insert(i, timedEvent.Clone());
					}
				}
			}
			if (!flag && DefaultUpdatePredicates.ContainsKey(eventType))
			{
				eventsToCopyToNextPart.Add(timedEvent.Clone());
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}
			if (disposing)
			{
				TimedEventsHolder[] timedEventsHolders = _timedEventsHolders;
				for (int i = 0; i < timedEventsHolders.Length; i++)
				{
					timedEventsHolders[i].Dispose();
				}
			}
			_disposed = true;
		}
	}
	public static class MidiFileSplitter
	{
		public static IEnumerable<MidiFile> SplitByChunks(this MidiFile midiFile, SplitFileByChunksSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			settings = settings ?? new SplitFileByChunksSettings();
			foreach (MidiChunk item in midiFile.Chunks.Where((MidiChunk c) => settings.Filter?.Invoke(c) ?? true))
			{
				yield return new MidiFile(item.Clone())
				{
					TimeDivision = midiFile.TimeDivision.Clone()
				};
			}
		}

		public static IEnumerable<MidiFile> SplitByChannel(this MidiFile midiFile, SplitFileByChannelSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			settings = settings ?? new SplitFileByChannelSettings();
			bool[] channelsUsed = new bool[(byte)FourBitNumber.MaxValue + 1];
			Dictionary<FourBitNumber, List<TimedEvent>> timedEventsByChannel = FourBitNumber.Values.ToDictionary((FourBitNumber channel) => channel, (FourBitNumber channel) => new List<TimedEvent>());
			IEnumerable<Tuple<TimedEvent, int>> enumerable = midiFile.GetTrackChunks().GetTimedEventsLazy();
			Predicate<TimedEvent> filter = settings.Filter;
			if (filter != null)
			{
				enumerable = enumerable.Where((Tuple<TimedEvent, int> e) => filter(e.Item1));
			}
			foreach (Tuple<TimedEvent, int> item2 in enumerable)
			{
				TimedEvent item = item2.Item1;
				if (item.Event is ChannelEvent channelEvent)
				{
					timedEventsByChannel[channelEvent.Channel].Add(item);
					channelsUsed[(byte)channelEvent.Channel] = true;
				}
				else if (settings.CopyNonChannelEventsToEachFile)
				{
					FourBitNumber[] values = FourBitNumber.Values;
					foreach (FourBitNumber key in values)
					{
						timedEventsByChannel[key].Add(item);
					}
				}
			}
			if (Array.TrueForAll(channelsUsed, (bool c) => !c))
			{
				MidiFile midiFile2 = midiFile.Clone();
				if (filter != null)
				{
					midiFile2.RemoveTimedEvents((TimedEvent e) => !filter(e));
				}
				yield return midiFile2;
				yield break;
			}
			foreach (FourBitNumber item3 in FourBitNumber.Values.Where((FourBitNumber c) => channelsUsed[(byte)c]))
			{
				MidiFile midiFile3 = timedEventsByChannel[item3].ToFile();
				midiFile3.TimeDivision = midiFile.TimeDivision.Clone();
				yield return midiFile3;
			}
		}

		public static IEnumerable<MidiFile> SplitByNotes(this MidiFile midiFile, SplitFileByNotesSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			settings = settings ?? new SplitFileByNotesSettings();
			if (!settings.IgnoreChannel)
			{
				return midiFile.SplitByNotes((NoteEvent noteEvent) => noteEvent.GetNoteId(), settings.Filter, settings.CopyNonNoteEventsToEachFile);
			}
			return midiFile.SplitByNotes((NoteEvent noteEvent) => noteEvent.NoteNumber, settings.Filter, settings.CopyNonNoteEventsToEachFile);
		}

		public static IEnumerable<MidiFile> SplitByGrid(this MidiFile midiFile, IGrid grid, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			if (!midiFile.GetEvents().Any())
			{
				yield break;
			}
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = PrepareMidiFileForSlicing(midiFile, grid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			using MidiFileSlicer slicer = MidiFileSlicer.CreateFromFile(midiFile);
			foreach (long time in grid.GetTimes(tempoMap))
			{
				if (time != 0L)
				{
					yield return slicer.GetNextSlice(time, settings);
					if (slicer.AllEventsProcessed)
					{
						break;
					}
				}
			}
			if (!slicer.AllEventsProcessed)
			{
				yield return slicer.GetNextSlice(long.MaxValue, settings);
			}
		}

		public static MidiFile SkipPart(this MidiFile midiFile, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(partLength);
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = PrepareMidiFileForSlicing(midiFile, arbitraryGrid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			long endTime = arbitraryGrid.GetTimes(tempoMap).First();
			using MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile);
			midiFileSlicer.GetNextSlice(endTime, settings);
			return midiFileSlicer.GetNextSlice(long.MaxValue, settings);
		}

		public static MidiFile TakePart(this MidiFile midiFile, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(partLength);
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = PrepareMidiFileForSlicing(midiFile, arbitraryGrid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			long endTime = arbitraryGrid.GetTimes(tempoMap).First();
			using MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile);
			return midiFileSlicer.GetNextSlice(endTime, settings);
		}

		public static MidiFile TakePart(this MidiFile midiFile, ITimeSpan partStart, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partStart", partStart);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(partStart, partStart.Add(partLength, TimeSpanMode.TimeLength));
			settings = settings ?? new SliceMidiFileSettings();
			midiFile = PrepareMidiFileForSlicing(midiFile, arbitraryGrid, settings);
			TempoMap tempoMap = midiFile.GetTempoMap();
			long[] array = arbitraryGrid.GetTimes(tempoMap).ToArray();
			using MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile);
			midiFileSlicer.GetNextSlice(array[0], settings);
			return midiFileSlicer.GetNextSlice(array[1], settings);
		}

		public static MidiFile CutPart(this MidiFile midiFile, ITimeSpan partStart, ITimeSpan partLength, SliceMidiFileSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("partStart", partStart);
			ThrowIfArgument.IsNull("partLength", partLength);
			ArbitraryGrid arbitraryGrid = new ArbitraryGrid(partStart, partStart.Add(partLength, TimeSpanMode.TimeLength));
			string partsStartId = Guid.NewGuid().ToString();
			string partEndId = Guid.NewGuid().ToString();
			settings = settings ?? new SliceMidiFileSettings();
			SliceMidiFileSettings sliceMidiFileSettings = new SliceMidiFileSettings
			{
				PreserveTrackChunks = true,
				PreserveTimes = settings.PreserveTimes,
				SplitNotes = settings.SplitNotes,
				Markers = new SliceMidiFileMarkers
				{
					PartStartMarkerEventFactory = () => new MarkerEvent(partsStartId),
					PartEndMarkerEventFactory = () => new MarkerEvent(partEndId)
				},
				NoteDetectionSettings = settings.NoteDetectionSettings
			};
			TempoMap tempoMap = midiFile.GetTempoMap();
			long[] times = arbitraryGrid.GetTimes(tempoMap).ToArray();
			IEnumerable<List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>>> enumerable;
			if (!settings.SplitNotes)
			{
				enumerable = from c in midiFile.GetTrackChunks()
					select new List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>>();
			}
			else
			{
				IEnumerable<List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>>> enumerable2 = midiFile.GetTrackChunks().Select(delegate(TrackChunk c)
				{
					IEnumerable<Melanchall.DryWetMidi.Interaction.Note> enumerable4 = c.Events.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(settings.NoteDetectionSettings).OfType<Melanchall.DryWetMidi.Interaction.Note>();
					List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>> list2 = new List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>>();
					foreach (Melanchall.DryWetMidi.Interaction.Note item in enumerable4)
					{
						if (item.Time + item.Length > times[0])
						{
							if (item.Time >= times[1])
							{
								break;
							}
							if (item.Time < times[0] && item.Time + item.Length > times[1])
							{
								list2.Add(Tuple.Create(item.GetNoteId(), item.Velocity, item.OffVelocity));
							}
						}
					}
					return list2;
				}).ToList();
				enumerable = enumerable2;
			}
			IEnumerable<List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>>> enumerable3 = enumerable;
			midiFile = PrepareMidiFileForSlicing(midiFile, arbitraryGrid, sliceMidiFileSettings);
			MidiFile midiFile2 = new MidiFile
			{
				TimeDivision = midiFile.TimeDivision
			};
			using (MidiFileSlicer midiFileSlicer = MidiFileSlicer.CreateFromFile(midiFile))
			{
				MidiFile nextSlice = midiFileSlicer.GetNextSlice(times[0], sliceMidiFileSettings);
				midiFileSlicer.GetNextSlice(times[1], sliceMidiFileSettings);
				MidiFile nextSlice2 = midiFileSlicer.GetNextSlice(Math.Max(times.Last(), midiFile.GetDuration<MidiTimeSpan>()) + 1, sliceMidiFileSettings);
				if (sliceMidiFileSettings.PreserveTimes)
				{
					long partLengthInTicks = times[1] - times[0];
					nextSlice2.ProcessTimedEvents(delegate(TimedEvent e)
					{
						e.Time -= partLengthInTicks;
					});
				}
				using IEnumerator<TrackChunk> enumerator = nextSlice.GetTrackChunks().GetEnumerator();
				using IEnumerator<TrackChunk> enumerator2 = nextSlice2.GetTrackChunks().GetEnumerator();
				using IEnumerator<List<Tuple<NoteId, SevenBitNumber, SevenBitNumber>>> enumerator3 = enumerable3.GetEnumerator();
				while (enumerator.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext())
				{
					TrackChunk trackChunk = new TrackChunk();
					EventsCollection events = trackChunk.Events;
					events.AddRange(enumerator.Current.Events);
					events.AddRange(enumerator2.Current.Events);
					events.RemoveTimedEvents(delegate(TimedEvent e)
					{
						if (!(e.Event is MarkerEvent markerEvent))
						{
							return false;
						}
						return markerEvent.Text == partsStartId || markerEvent.Text == partEndId;
					});
					if (settings.SplitNotes && enumerator3.Current.Any())
					{
						List<TimedEvent> list = events.GetTimedEventsLazy(cloneEvent: false).SkipWhile((TimedEvent e) => e.Time < times[0]).TakeWhile((TimedEvent e) => e.Time == times[0])
							.ToList();
						List<MidiEvent> eventsToRemove = new List<MidiEvent>();
						foreach (Tuple<NoteId, SevenBitNumber, SevenBitNumber> notesDescriptor in enumerator3.Current)
						{
							TimedEvent[] array = list.Where(delegate(TimedEvent e)
							{
								if (!(e.Event is NoteEvent noteEvent))
								{
									return false;
								}
								if (!noteEvent.GetNoteId().Equals(notesDescriptor.Item1))
								{
									return false;
								}
								return (noteEvent is NoteOnEvent noteOnEvent) ? ((byte)noteOnEvent.Velocity == (byte)notesDescriptor.Item2) : ((byte)((NoteOffEvent)noteEvent).Velocity == (byte)notesDescriptor.Item3);
							}).ToArray();
							foreach (TimedEvent timedEvent in array)
							{
								list.Remove(timedEvent);
								eventsToRemove.Add(timedEvent.Event);
							}
						}
						events.RemoveTimedEvents((TimedEvent e) => eventsToRemove.Contains(e.Event));
					}
					if (settings.PreserveTrackChunks || events.Any())
					{
						midiFile2.Chunks.Add(trackChunk);
					}
				}
			}
			return midiFile2;
		}

		private static IEnumerable<MidiFile> SplitByNotes<TNoteId>(this MidiFile midiFile, Func<NoteEvent, TNoteId> getNoteId, Predicate<TimedEvent> filter, bool copyNonNoteEventsToEachFile)
		{
			Dictionary<TNoteId, List<TimedEvent>> dictionary = new Dictionary<TNoteId, List<TimedEvent>>();
			List<TimedEvent> list = new List<TimedEvent>();
			IEnumerable<Tuple<TimedEvent, int>> enumerable = midiFile.GetTrackChunks().GetTimedEventsLazy();
			if (filter != null)
			{
				enumerable = enumerable.Where((Tuple<TimedEvent, int> e) => filter(e.Item1));
			}
			foreach (Tuple<TimedEvent, int> item2 in enumerable)
			{
				TimedEvent item = item2.Item1;
				if (item.Event is NoteEvent arg)
				{
					TNoteId key = getNoteId(arg);
					if (!dictionary.TryGetValue(key, out var value))
					{
						dictionary.Add(key, value = new List<TimedEvent>());
						if (copyNonNoteEventsToEachFile)
						{
							value.AddRange(list);
						}
					}
					value.Add(item);
				}
				else
				{
					if (!copyNonNoteEventsToEachFile)
					{
						continue;
					}
					foreach (KeyValuePair<TNoteId, List<TimedEvent>> item3 in dictionary)
					{
						item3.Value.Add(item);
					}
					list.Add(item);
				}
			}
			if (!dictionary.Any())
			{
				MidiFile midiFile2 = midiFile.Clone();
				if (filter != null)
				{
					midiFile2.RemoveTimedEvents((TimedEvent e) => !filter(e));
				}
				yield return midiFile2;
				yield break;
			}
			foreach (KeyValuePair<TNoteId, List<TimedEvent>> item4 in dictionary)
			{
				MidiFile midiFile3 = item4.Value.ToFile();
				midiFile3.TimeDivision = midiFile.TimeDivision.Clone();
				yield return midiFile3;
			}
		}

		private static MidiFile PrepareMidiFileForSlicing(MidiFile midiFile, IGrid grid, SliceMidiFileSettings settings)
		{
			if (settings.SplitNotes)
			{
				midiFile = midiFile.Clone();
				midiFile.SplitNotesByGrid(grid, settings.NoteDetectionSettings);
			}
			return midiFile;
		}
	}
	public class SliceMidiFileSettings
	{
		public bool SplitNotes { get; set; } = true;

		public bool PreserveTimes { get; set; }

		public bool PreserveTrackChunks { get; set; }

		public SliceMidiFileMarkers Markers { get; set; }

		public NoteDetectionSettings NoteDetectionSettings { get; set; }
	}
	public sealed class SplitFileByChannelSettings
	{
		public bool CopyNonChannelEventsToEachFile { get; set; } = true;

		public Predicate<TimedEvent> Filter { get; set; }
	}
	public sealed class SplitFileByChunksSettings
	{
		public Predicate<MidiChunk> Filter { get; set; }
	}
	public sealed class SplitFileByNotesSettings
	{
		public bool CopyNonNoteEventsToEachFile { get; set; } = true;

		public Predicate<TimedEvent> Filter { get; set; }

		public bool IgnoreChannel { get; set; }
	}
	public sealed class SliceMidiFileMarkers
	{
		public Func<MidiEvent> PartStartMarkerEventFactory { get; set; }

		public Func<MidiEvent> PartEndMarkerEventFactory { get; set; }

		public Func<MidiEvent> EmptyPartMarkerEventFactory { get; set; }
	}
	public sealed class NotesMerger
	{
		private sealed class NoteHolder
		{
			private readonly Melanchall.DryWetMidi.Interaction.Note _note;

			private readonly VelocityMerger _velocityMerger;

			private readonly VelocityMerger _offVelocityMerger;

			public long EndTime { get; set; }

			public NoteHolder(Melanchall.DryWetMidi.Interaction.Note note, VelocityMerger velocityMerger, VelocityMerger offVelocityMerger)
			{
				_note = note;
				_velocityMerger = velocityMerger;
				_offVelocityMerger = offVelocityMerger;
				_velocityMerger.Initialize(note.Velocity);
				_offVelocityMerger.Initialize(note.OffVelocity);
				EndTime = _note.Time + _note.Length;
			}

			public void MergeVelocities(Melanchall.DryWetMidi.Interaction.Note note)
			{
				_velocityMerger.Merge(note.Velocity);
				_offVelocityMerger.Merge(note.OffVelocity);
			}

			public Melanchall.DryWetMidi.Interaction.Note GetResultNote()
			{
				_note.Length = EndTime - _note.Time;
				_note.Velocity = _velocityMerger.Velocity;
				_note.OffVelocity = _offVelocityMerger.Velocity;
				return _note;
			}
		}

		private static readonly Dictionary<VelocityMergingPolicy, Func<VelocityMerger>> VelocityMergers = new Dictionary<VelocityMergingPolicy, Func<VelocityMerger>>
		{
			[VelocityMergingPolicy.First] = () => new FirstVelocityMerger(),
			[VelocityMergingPolicy.Last] = () => new LastVelocityMerger(),
			[VelocityMergingPolicy.Min] = () => new MinVelocityMerger(),
			[VelocityMergingPolicy.Max] = () => new MaxVelocityMerger(),
			[VelocityMergingPolicy.Average] = () => new AverageVelocityMerger()
		};

		public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> Merge(IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes, TempoMap tempoMap, NotesMergingSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			settings = settings ?? new NotesMergingSettings();
			Dictionary<NoteId, NoteHolder> currentNotes = new Dictionary<NoteId, NoteHolder>();
			Type toleranceType = settings.Tolerance.GetType();
			foreach (Melanchall.DryWetMidi.Interaction.Note note in from n in notes
				where n != null
				orderby n.Time
				select n)
			{
				NoteId noteId = note.GetNoteId();
				if (!currentNotes.TryGetValue(noteId, out var value))
				{
					currentNotes.Add(noteId, CreateNoteHolder(note, settings));
					continue;
				}
				long endTime = value.EndTime;
				if (LengthConverter.ConvertTo((MidiTimeSpan)Math.Max(0L, note.Time - endTime), toleranceType, endTime, tempoMap).CompareTo(settings.Tolerance) <= 0)
				{
					long endTime2 = Math.Max(note.Time + note.Length, endTime);
					value.EndTime = endTime2;
					value.MergeVelocities(note);
				}
				else
				{
					yield return currentNotes[noteId].GetResultNote();
					currentNotes[noteId] = CreateNoteHolder(note, settings);
				}
			}
			foreach (NoteHolder value2 in currentNotes.Values)
			{
				yield return value2.GetResultNote();
			}
		}

		private static NoteHolder CreateNoteHolder(Melanchall.DryWetMidi.Interaction.Note note, NotesMergingSettings settings)
		{
			return new NoteHolder(note.Clone(), VelocityMergers[settings.VelocityMergingPolicy](), VelocityMergers[settings.OffVelocityMergingPolicy]());
		}
	}
	public static class NotesMergerUtilities
	{
		public static void MergeNotes(this TrackChunk trackChunk, TempoMap tempoMap, NotesMergingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			settings = settings ?? new NotesMergingSettings();
			using NotesManager notesManager = trackChunk.ManageNotes(settings.NoteDetectionSettings);
			NotesCollection notes = notesManager.Notes;
			List<Melanchall.DryWetMidi.Interaction.Note> objects = new NotesMerger().Merge(notes.Where((Melanchall.DryWetMidi.Interaction.Note n) => settings.Filter == null || settings.Filter(n)), tempoMap, settings).ToList();
			notes.Clear();
			notes.Add(objects);
		}

		public static void MergeNotes(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, NotesMergingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk item in trackChunks.Where((TrackChunk c) => c != null))
			{
				item.MergeNotes(tempoMap, settings);
			}
		}

		public static void MergeNotes(this MidiFile midiFile, NotesMergingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().MergeNotes(tempoMap, settings);
		}
	}
	public sealed class NotesMergingSettings
	{
		private VelocityMergingPolicy _velocityMergingPolicy;

		private VelocityMergingPolicy _offVelocityMergingPolicy = VelocityMergingPolicy.Last;

		private ITimeSpan _tolerance = new MidiTimeSpan();

		public VelocityMergingPolicy VelocityMergingPolicy
		{
			get
			{
				return _velocityMergingPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_velocityMergingPolicy = value;
			}
		}

		public VelocityMergingPolicy OffVelocityMergingPolicy
		{
			get
			{
				return _offVelocityMergingPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_offVelocityMergingPolicy = value;
			}
		}

		public ITimeSpan Tolerance
		{
			get
			{
				return _tolerance;
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				_tolerance = value;
			}
		}

		public Predicate<Melanchall.DryWetMidi.Interaction.Note> Filter { get; set; }

		public NoteDetectionSettings NoteDetectionSettings { get; set; } = new NoteDetectionSettings();
	}
	internal sealed class AverageVelocityMerger : VelocityMerger
	{
		private readonly List<SevenBitNumber> _velocities = new List<SevenBitNumber>();

		public override SevenBitNumber Velocity => (SevenBitNumber)(byte)MathUtilities.Round(_velocities.Average((SevenBitNumber v) => (byte)v));

		public override void Initialize(SevenBitNumber velocity)
		{
			_velocities.Clear();
			_velocities.Add(velocity);
		}

		public override void Merge(SevenBitNumber velocity)
		{
			_velocities.Add(velocity);
		}
	}
	internal sealed class FirstVelocityMerger : VelocityMerger
	{
		public override void Merge(SevenBitNumber velocity)
		{
		}
	}
	internal sealed class LastVelocityMerger : VelocityMerger
	{
		public override void Merge(SevenBitNumber velocity)
		{
			_velocity = velocity;
		}
	}
	internal sealed class MaxVelocityMerger : VelocityMerger
	{
		public override void Merge(SevenBitNumber velocity)
		{
			_velocity = (SevenBitNumber)Math.Max(_velocity, velocity);
		}
	}
	internal sealed class MinVelocityMerger : VelocityMerger
	{
		public override void Merge(SevenBitNumber velocity)
		{
			_velocity = (SevenBitNumber)Math.Min(_velocity, velocity);
		}
	}
	internal abstract class VelocityMerger
	{
		protected SevenBitNumber _velocity;

		public virtual SevenBitNumber Velocity => _velocity;

		public virtual void Initialize(SevenBitNumber velocity)
		{
			_velocity = velocity;
		}

		public abstract void Merge(SevenBitNumber velocity);
	}
	public enum VelocityMergingPolicy
	{
		First,
		Last,
		Min,
		Max,
		Average
	}
	public sealed class QuantizedTime
	{
		public long NewTime { get; }

		public long GridTime { get; }

		public ITimeSpan Shift { get; }

		public long DistanceToGridTime { get; }

		public ITimeSpan ConvertedDistanceToGridTime { get; }

		internal QuantizedTime(long newTime, long gridTime, ITimeSpan shift, long distanceToGridTime, ITimeSpan convertedDistanceToGridTime)
		{
			NewTime = newTime;
			GridTime = gridTime;
			Shift = shift;
			DistanceToGridTime = distanceToGridTime;
			ConvertedDistanceToGridTime = convertedDistanceToGridTime;
		}
	}
	public abstract class Quantizer<TObject, TSettings> where TSettings : QuantizingSettings<TObject>, new()
	{
		protected void QuantizeInternal(IEnumerable<TObject> objects, IGrid grid, TempoMap tempoMap, TSettings settings)
		{
			settings = settings ?? new TSettings();
			Func<TObject, bool> predicate = (TObject o) => o != null && (settings.Filter?.Invoke(o) ?? true);
			long lastTime = (from o in objects.Where(predicate)
				select GetObjectTime(o, settings)).DefaultIfEmpty().Max();
			List<long> grid2 = GetGridTimes(grid, lastTime, tempoMap).ToList();
			foreach (TObject item in objects.Where(predicate))
			{
				long objectTime = GetObjectTime(item, settings);
				QuantizedTime quantizedTime = FindNearestTime(grid2, objectTime, settings.DistanceCalculationType, settings.QuantizingLevel, tempoMap);
				TimeProcessingInstruction timeProcessingInstruction = OnObjectQuantizing(item, quantizedTime, grid, tempoMap, settings);
				switch (timeProcessingInstruction.Action)
				{
				case TimeProcessingAction.Apply:
					SetObjectTime(item, timeProcessingInstruction.Time, settings);
					break;
				}
			}
		}

		protected abstract long GetObjectTime(TObject obj, TSettings settings);

		protected abstract void SetObjectTime(TObject obj, long time, TSettings settings);

		protected abstract TimeProcessingInstruction OnObjectQuantizing(TObject obj, QuantizedTime quantizedTime, IGrid grid, TempoMap tempoMap, TSettings settings);

		private static IEnumerable<long> GetGridTimes(IGrid grid, long lastTime, TempoMap tempoMap)
		{
			IEnumerable<long> times = grid.GetTimes(tempoMap);
			using IEnumerator<long> enumerator = times.GetEnumerator();
			while (enumerator.MoveNext() && enumerator.Current < lastTime)
			{
				yield return enumerator.Current;
			}
			yield return enumerator.Current;
		}

		private static QuantizedTime FindNearestTime(IReadOnlyList<long> grid, long time, TimeSpanType distanceCalculationType, double quantizingLevel, TempoMap tempoMap)
		{
			long distanceToGridTime = -1L;
			ITimeSpan timeSpan = TimeSpanUtilities.GetMaxTimeSpan(distanceCalculationType);
			long num = -1L;
			for (int i = 0; i < grid.Count; i++)
			{
				long num2 = grid[i];
				long num3 = Math.Abs(time - num2);
				ITimeSpan timeSpan2 = LengthConverter.ConvertTo(num3, distanceCalculationType, Math.Min(time, num2), tempoMap);
				if (timeSpan2.CompareTo(timeSpan) >= 0)
				{
					break;
				}
				distanceToGridTime = num3;
				timeSpan = timeSpan2;
				num = num2;
			}
			ITimeSpan timeSpan3 = timeSpan.Multiply(quantizingLevel);
			ITimeSpan timeSpan4 = TimeConverter.ConvertTo(time, distanceCalculationType, tempoMap);
			return new QuantizedTime(TimeConverter.ConvertFrom((num > time) ? timeSpan4.Add(timeSpan3, TimeSpanMode.TimeLength) : timeSpan4.Subtract(timeSpan3, TimeSpanMode.TimeLength), tempoMap), num, timeSpan3, distanceToGridTime, timeSpan);
		}
	}
	public abstract class QuantizingSettings<TObject>
	{
		private const double NoQuantizingLevel = 0.0;

		private const double FullQuantizingLevel = 1.0;

		private TimeSpanType _distanceCalculationType = TimeSpanType.Midi;

		private double _quantizingLevel = 1.0;

		public TimeSpanType DistanceCalculationType
		{
			get
			{
				return _distanceCalculationType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_distanceCalculationType = value;
			}
		}

		public double QuantizingLevel
		{
			get
			{
				return _quantizingLevel;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, 0.0, 1.0, $"Value is out of [{0.0}; {1.0}] range.");
				_quantizingLevel = value;
			}
		}

		public Predicate<TObject> Filter { get; set; }
	}
	public class ChordsQuantizingSettings : LengthedObjectsQuantizingSettings<Melanchall.DryWetMidi.Interaction.Chord>
	{
		public ChordDetectionSettings ChordDetectionSettings { get; set; } = new ChordDetectionSettings();
	}
	public class ChordsQuantizer : LengthedObjectsQuantizer<Melanchall.DryWetMidi.Interaction.Chord, ChordsQuantizingSettings>
	{
	}
	public abstract class LengthedObjectsQuantizer<TObject, TSettings> : Quantizer<TObject, TSettings> where TObject : ILengthedObject where TSettings : LengthedObjectsQuantizingSettings<TObject>, new()
	{
		public void Quantize(IEnumerable<TObject> objects, IGrid grid, TempoMap tempoMap, TSettings settings = null)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			QuantizeInternal(objects, grid, tempoMap, settings);
		}

		private static TimeProcessingInstruction CorrectObjectOnStartQuantizing(TObject obj, long time, TempoMap tempoMap, TSettings settings)
		{
			if (settings.FixOppositeEnd)
			{
				long oldTime = obj.Time + obj.Length;
				if (time > oldTime)
				{
					TimeProcessingInstruction timeProcessingInstruction = ProcessQuantizingBeyondFixedEnd(ref time, ref oldTime, settings.QuantizingBeyondFixedEndPolicy, "Start time is going to be beyond the end one.");
					if (timeProcessingInstruction != null)
					{
						return timeProcessingInstruction;
					}
				}
				long length = oldTime - time;
				obj.Length = length;
			}
			else
			{
				ITimeSpan length2 = obj.LengthAs(settings.LengthType, tempoMap);
				long length3 = LengthConverter.ConvertFrom(length2, time, tempoMap);
				obj.Length = length3;
			}
			return new TimeProcessingInstruction(time);
		}

		private static TimeProcessingInstruction CorrectObjectOnEndQuantizing(TObject obj, long time, TempoMap tempoMap, TSettings settings)
		{
			if (settings.FixOppositeEnd)
			{
				long oldTime = obj.Time;
				if (time < oldTime)
				{
					TimeProcessingInstruction timeProcessingInstruction = ProcessQuantizingBeyondFixedEnd(ref time, ref oldTime, settings.QuantizingBeyondFixedEndPolicy, "End time is going to be beyond the start one.");
					if (timeProcessingInstruction != null)
					{
						return timeProcessingInstruction;
					}
				}
				long length = time - oldTime;
				obj.Length = length;
			}
			else
			{
				ITimeSpan timeSpan = obj.LengthAs(settings.LengthType, tempoMap);
				long num = ((settings.LengthType == TimeSpanType.Midi) ? (time - obj.Length) : TimeConverter.ConvertFrom(((MidiTimeSpan)time).Subtract(timeSpan, TimeSpanMode.TimeLength), tempoMap));
				if (num < 0)
				{
					switch (settings.QuantizingBeyondZeroPolicy)
					{
					case QuantizingBeyondZeroPolicy.Skip:
						return TimeProcessingInstruction.Skip;
					case QuantizingBeyondZeroPolicy.Abort:
						throw new InvalidOperationException("Object is going to be moved beyond zero.");
					case QuantizingBeyondZeroPolicy.FixAtZero:
					{
						long length2 = time;
						obj.Length = length2;
						break;
					}
					}
				}
				else
				{
					long length3 = LengthConverter.ConvertFrom(timeSpan, num, tempoMap);
					obj.Length = length3;
				}
			}
			return new TimeProcessingInstruction(time);
		}

		private static TimeProcessingInstruction ProcessQuantizingBeyondFixedEnd(ref long newTime, ref long oldTime, QuantizingBeyondFixedEndPolicy quantizingBeyondFixedEndPolicy, string errorMessage)
		{
			switch (quantizingBeyondFixedEndPolicy)
			{
			case QuantizingBeyondFixedEndPolicy.Skip:
				return TimeProcessingInstruction.Skip;
			case QuantizingBeyondFixedEndPolicy.Abort:
				throw new InvalidOperationException(errorMessage);
			case QuantizingBeyondFixedEndPolicy.CollapseAndFix:
				newTime = oldTime;
				break;
			case QuantizingBeyondFixedEndPolicy.CollapseAndMove:
				oldTime = newTime;
				break;
			case QuantizingBeyondFixedEndPolicy.SwapEnds:
			{
				long num = newTime;
				newTime = oldTime;
				oldTime = num;
				break;
			}
			}
			return null;
		}

		protected sealed override long GetObjectTime(TObject obj, TSettings settings)
		{
			LengthedObjectTarget quantizingTarget = settings.QuantizingTarget;
			return quantizingTarget switch
			{
				LengthedObjectTarget.Start => obj.Time, 
				LengthedObjectTarget.End => obj.Time + obj.Length, 
				_ => throw new NotSupportedException($"{quantizingTarget} quantization target is not supported to get time."), 
			};
		}

		protected sealed override void SetObjectTime(TObject obj, long time, TSettings settings)
		{
			LengthedObjectTarget quantizingTarget = settings.QuantizingTarget;
			switch (quantizingTarget)
			{
			case LengthedObjectTarget.Start:
				obj.Time = time;
				break;
			case LengthedObjectTarget.End:
			{
				long time2 = time - obj.Length;
				obj.Time = time2;
				break;
			}
			default:
				throw new NotSupportedException($"{quantizingTarget} quantization target is not supported to set time.");
			}
		}

		protected override TimeProcessingInstruction OnObjectQuantizing(TObject obj, QuantizedTime quantizedTime, IGrid grid, TempoMap tempoMap, TSettings settings)
		{
			long newTime = quantizedTime.NewTime;
			return settings.QuantizingTarget switch
			{
				LengthedObjectTarget.Start => CorrectObjectOnStartQuantizing(obj, newTime, tempoMap, settings), 
				LengthedObjectTarget.End => CorrectObjectOnEndQuantizing(obj, newTime, tempoMap, settings), 
				_ => new TimeProcessingInstruction(newTime), 
			};
		}
	}
	public abstract class LengthedObjectsQuantizingSettings<TObject> : QuantizingSettings<TObject> where TObject : ILengthedObject
	{
		private TimeSpanType _lengthType = TimeSpanType.Midi;

		private LengthedObjectTarget _quantizingTarget;

		private QuantizingBeyondZeroPolicy _quantizingBeyondZeroPolicy;

		private QuantizingBeyondFixedEndPolicy _quantizingBeyondFixedEndPolicy;

		public TimeSpanType LengthType
		{
			get
			{
				return _lengthType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_lengthType = value;
			}
		}

		public LengthedObjectTarget QuantizingTarget
		{
			get
			{
				return _quantizingTarget;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_quantizingTarget = value;
			}
		}

		public QuantizingBeyondZeroPolicy QuantizingBeyondZeroPolicy
		{
			get
			{
				return _quantizingBeyondZeroPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_quantizingBeyondZeroPolicy = value;
			}
		}

		public QuantizingBeyondFixedEndPolicy QuantizingBeyondFixedEndPolicy
		{
			get
			{
				return _quantizingBeyondFixedEndPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_quantizingBeyondFixedEndPolicy = value;
			}
		}

		public bool FixOppositeEnd { get; set; }
	}
	public enum QuantizingBeyondFixedEndPolicy
	{
		CollapseAndFix,
		CollapseAndMove,
		SwapEnds,
		Skip,
		Abort
	}
	public enum QuantizingBeyondZeroPolicy
	{
		FixAtZero,
		Skip,
		Abort
	}
	public class NotesQuantizingSettings : LengthedObjectsQuantizingSettings<Melanchall.DryWetMidi.Interaction.Note>
	{
		public NoteDetectionSettings NoteDetectionSettings { get; set; } = new NoteDetectionSettings();
	}
	public class NotesQuantizer : LengthedObjectsQuantizer<Melanchall.DryWetMidi.Interaction.Note, NotesQuantizingSettings>
	{
	}
	public class TimedEventsQuantizingSettings : QuantizingSettings<TimedEvent>
	{
	}
	public class TimedEventsQuantizer : Quantizer<TimedEvent, TimedEventsQuantizingSettings>
	{
		public void Quantize(IEnumerable<TimedEvent> objects, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			QuantizeInternal(objects, grid, tempoMap, settings);
		}

		protected sealed override long GetObjectTime(TimedEvent obj, TimedEventsQuantizingSettings settings)
		{
			return obj.Time;
		}

		protected sealed override void SetObjectTime(TimedEvent obj, long time, TimedEventsQuantizingSettings settings)
		{
			obj.Time = time;
		}

		protected override TimeProcessingInstruction OnObjectQuantizing(TimedEvent obj, QuantizedTime quantizedTime, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings)
		{
			return new TimeProcessingInstruction(quantizedTime.NewTime);
		}
	}
	public static class ChordsQuantizerUtilities
	{
		public static void QuantizeChords(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, ChordsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using ChordsManager chordsManager = trackChunk.ManageChords(settings?.ChordDetectionSettings);
			new ChordsQuantizer().Quantize(chordsManager.Chords, grid, tempoMap, settings);
		}

		public static void QuantizeChords(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, ChordsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.QuantizeChords(grid, tempoMap, settings);
			}
		}

		public static void QuantizeChords(this MidiFile midiFile, IGrid grid, ChordsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().QuantizeChords(grid, tempoMap, settings);
		}
	}
	public static class NotesQuantizerUtilities
	{
		public static void QuantizeNotes(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, NotesQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using NotesManager notesManager = trackChunk.ManageNotes(settings?.NoteDetectionSettings);
			new NotesQuantizer().Quantize(notesManager.Notes, grid, tempoMap, settings);
		}

		public static void QuantizeNotes(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, NotesQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.QuantizeNotes(grid, tempoMap, settings);
			}
		}

		public static void QuantizeNotes(this MidiFile midiFile, IGrid grid, NotesQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().QuantizeNotes(grid, tempoMap, settings);
		}
	}
	public static class TimedEventsQuantizerUtilities
	{
		public static void QuantizeTimedEvents(this TrackChunk trackChunk, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using TimedEventsManager timedEventsManager = trackChunk.ManageTimedEvents();
			new TimedEventsQuantizer().Quantize(timedEventsManager.Events, grid, tempoMap, settings);
		}

		public static void QuantizeTimedEvents(this IEnumerable<TrackChunk> trackChunks, IGrid grid, TempoMap tempoMap, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("grid", grid);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.QuantizeTimedEvents(grid, tempoMap, settings);
			}
		}

		public static void QuantizeTimedEvents(this MidiFile midiFile, IGrid grid, TimedEventsQuantizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("grid", grid);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().QuantizeTimedEvents(grid, tempoMap, settings);
		}
	}
	public abstract class Randomizer<TObject, TSettings> where TSettings : RandomizingSettings<TObject>, new()
	{
		private readonly Random _random = new Random();

		protected void RandomizeInternal(IEnumerable<TObject> objects, IBounds bounds, TempoMap tempoMap, TSettings settings)
		{
			settings = settings ?? new TSettings();
			Func<TObject, bool> predicate = (TObject o) => o != null && (settings.Filter?.Invoke(o) ?? true);
			foreach (TObject item in objects.Where(predicate))
			{
				long objectTime = GetObjectTime(item, settings);
				objectTime = RandomizeTime(objectTime, bounds, _random, tempoMap);
				TimeProcessingInstruction timeProcessingInstruction = OnObjectRandomizing(item, objectTime, settings);
				switch (timeProcessingInstruction.Action)
				{
				case TimeProcessingAction.Apply:
					SetObjectTime(item, timeProcessingInstruction.Time, settings);
					break;
				}
			}
		}

		protected abstract long GetObjectTime(TObject obj, TSettings settings);

		protected abstract void SetObjectTime(TObject obj, long time, TSettings settings);

		protected abstract TimeProcessingInstruction OnObjectRandomizing(TObject obj, long time, TSettings settings);

		private static long RandomizeTime(long time, IBounds bounds, Random random, TempoMap tempoMap)
		{
			Tuple<long, long> bounds2 = bounds.GetBounds(time, tempoMap);
			long num = Math.Max(0L, bounds2.Item1) - 1;
			int maxValue = (int)Math.Abs(bounds2.Item2 - num);
			return num + random.Next(maxValue) + 1;
		}
	}
	public abstract class RandomizingSettings<TObject>
	{
		public Predicate<TObject> Filter { get; set; }
	}
	public sealed class ConstantBounds : IBounds
	{
		public ITimeSpan LeftSize { get; }

		public ITimeSpan RightSize { get; }

		public ConstantBounds(ITimeSpan size)
			: this(size, size)
		{
		}

		public ConstantBounds(ITimeSpan leftSize, ITimeSpan rightSize)
		{
			ThrowIfArgument.IsNull("leftSize", leftSize);
			ThrowIfArgument.IsNull("rightSize", rightSize);
			LeftSize = leftSize;
			RightSize = rightSize;
		}

		private static long CalculateBoundaryTime(long time, ITimeSpan size, MathOperation operation, TempoMap tempoMap)
		{
			ITimeSpan timeSpan = (MidiTimeSpan)time;
			switch (operation)
			{
			case MathOperation.Add:
				timeSpan = timeSpan.Add(size, TimeSpanMode.TimeLength);
				break;
			case MathOperation.Subtract:
			{
				ITimeSpan timeSpan2;
				if (TimeConverter.ConvertFrom(size, tempoMap) <= time)
				{
					timeSpan2 = timeSpan.Subtract(size, TimeSpanMode.TimeLength);
				}
				else
				{
					ITimeSpan timeSpan3 = (MidiTimeSpan)0L;
					timeSpan2 = timeSpan3;
				}
				timeSpan = timeSpan2;
				break;
			}
			}
			return TimeConverter.ConvertFrom(timeSpan, tempoMap);
		}

		public Tuple<long, long> GetBounds(long time, TempoMap tempoMap)
		{
			return Tuple.Create(CalculateBoundaryTime(time, LeftSize, MathOperation.Subtract, tempoMap), CalculateBoundaryTime(time, RightSize, MathOperation.Add, tempoMap));
		}
	}
	public interface IBounds
	{
		Tuple<long, long> GetBounds(long time, TempoMap tempoMap);
	}
	public sealed class ChordsRandomizingSettings : LengthedObjectsRandomizingSettings<Melanchall.DryWetMidi.Interaction.Chord>
	{
		public ChordDetectionSettings ChordDetectionSettings { get; set; } = new ChordDetectionSettings();
	}
	public sealed class ChordsRandomizer : LengthedObjectsRandomizer<Melanchall.DryWetMidi.Interaction.Chord, ChordsRandomizingSettings>
	{
	}
	public abstract class LengthedObjectsRandomizer<TObject, TSettings> : Randomizer<TObject, TSettings> where TObject : ILengthedObject where TSettings : LengthedObjectsRandomizingSettings<TObject>, new()
	{
		public void Randomize(IEnumerable<TObject> objects, IBounds bounds, TempoMap tempoMap, TSettings settings = null)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			RandomizeInternal(objects, bounds, tempoMap, settings);
		}

		protected sealed override long GetObjectTime(TObject obj, TSettings settings)
		{
			LengthedObjectTarget randomizingTarget = settings.RandomizingTarget;
			return randomizingTarget switch
			{
				LengthedObjectTarget.Start => obj.Time, 
				LengthedObjectTarget.End => obj.Time + obj.Length, 
				_ => throw new NotSupportedException($"{randomizingTarget} randomization target is not supported to get time."), 
			};
		}

		protected sealed override void SetObjectTime(TObject obj, long time, TSettings settings)
		{
			LengthedObjectTarget randomizingTarget = settings.RandomizingTarget;
			switch (randomizingTarget)
			{
			case LengthedObjectTarget.Start:
				obj.Time = time;
				break;
			case LengthedObjectTarget.End:
			{
				long time2 = time - obj.Length;
				obj.Time = time2;
				break;
			}
			default:
				throw new NotSupportedException($"{randomizingTarget} randomization target is not supported to set time.");
			}
		}

		protected override TimeProcessingInstruction OnObjectRandomizing(TObject obj, long time, TSettings settings)
		{
			switch (settings.RandomizingTarget)
			{
			case LengthedObjectTarget.Start:
				if (settings.FixOppositeEnd)
				{
					long length2 = obj.Time + obj.Length - time;
					obj.Length = length2;
				}
				break;
			case LengthedObjectTarget.End:
				if (settings.FixOppositeEnd)
				{
					long length = time - obj.Time;
					obj.Length = length;
				}
				break;
			}
			return new TimeProcessingInstruction(time);
		}
	}
	public abstract class LengthedObjectsRandomizingSettings<TObject> : RandomizingSettings<TObject> where TObject : ILengthedObject
	{
		private LengthedObjectTarget _randomizingTarget;

		public LengthedObjectTarget RandomizingTarget
		{
			get
			{
				return _randomizingTarget;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_randomizingTarget = value;
			}
		}

		public bool FixOppositeEnd { get; set; }
	}
	public sealed class NotesRandomizingSettings : LengthedObjectsRandomizingSettings<Melanchall.DryWetMidi.Interaction.Note>
	{
		public NoteDetectionSettings NoteDetectionSettings { get; set; } = new NoteDetectionSettings();
	}
	public sealed class NotesRandomizer : LengthedObjectsRandomizer<Melanchall.DryWetMidi.Interaction.Note, NotesRandomizingSettings>
	{
	}
	public sealed class TimedEventsRandomizingSettings : RandomizingSettings<TimedEvent>
	{
	}
	public sealed class TimedEventsRandomizer : Randomizer<TimedEvent, TimedEventsRandomizingSettings>
	{
		public void Randomize(IEnumerable<TimedEvent> objects, IBounds bounds, TempoMap tempoMap, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			RandomizeInternal(objects, bounds, tempoMap, settings);
		}

		protected override long GetObjectTime(TimedEvent obj, TimedEventsRandomizingSettings settings)
		{
			return obj.Time;
		}

		protected override void SetObjectTime(TimedEvent obj, long time, TimedEventsRandomizingSettings settings)
		{
			obj.Time = time;
		}

		protected override TimeProcessingInstruction OnObjectRandomizing(TimedEvent obj, long time, TimedEventsRandomizingSettings settings)
		{
			return new TimeProcessingInstruction(time);
		}
	}
	public static class ChordsRandomizerUtilities
	{
		public static void RandomizeChords(this TrackChunk trackChunk, IBounds bounds, TempoMap tempoMap, ChordsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using ChordsManager chordsManager = trackChunk.ManageChords(settings?.ChordDetectionSettings);
			new ChordsRandomizer().Randomize(chordsManager.Chords, bounds, tempoMap, settings);
		}

		public static void RandomizeChords(this IEnumerable<TrackChunk> trackChunks, IBounds bounds, TempoMap tempoMap, ChordsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.RandomizeChords(bounds, tempoMap, settings);
			}
		}

		public static void RandomizeChords(this MidiFile midiFile, IBounds bounds, ChordsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("bounds", bounds);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().RandomizeChords(bounds, tempoMap, settings);
		}
	}
	public static class NotesRandomizerUtilities
	{
		public static void RandomizeNotes(this TrackChunk trackChunk, IBounds bounds, TempoMap tempoMap, NotesRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using NotesManager notesManager = trackChunk.ManageNotes(settings?.NoteDetectionSettings);
			new NotesRandomizer().Randomize(notesManager.Notes, bounds, tempoMap, settings);
		}

		public static void RandomizeNotes(this IEnumerable<TrackChunk> trackChunks, IBounds bounds, TempoMap tempoMap, NotesRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.RandomizeNotes(bounds, tempoMap, settings);
			}
		}

		public static void RandomizeNotes(this MidiFile midiFile, IBounds bounds, NotesRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("bounds", bounds);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().RandomizeNotes(bounds, tempoMap, settings);
		}
	}
	public static class TimedEventsRandomizerUtilities
	{
		public static void RandomizeTimedEvents(this TrackChunk trackChunk, IBounds bounds, TempoMap tempoMap, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			using TimedEventsManager timedEventsManager = trackChunk.ManageTimedEvents();
			new TimedEventsRandomizer().Randomize(timedEventsManager.Events, bounds, tempoMap, settings);
		}

		public static void RandomizeTimedEvents(this IEnumerable<TrackChunk> trackChunks, IBounds bounds, TempoMap tempoMap, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("bounds", bounds);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.RandomizeTimedEvents(bounds, tempoMap, settings);
			}
		}

		public static void RandomizeTimedEvents(this MidiFile midiFile, IBounds bounds, TimedEventsRandomizingSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("bounds", bounds);
			TempoMap tempoMap = midiFile.GetTempoMap();
			midiFile.GetTrackChunks().RandomizeTimedEvents(bounds, tempoMap, settings);
		}
	}
}
namespace Melanchall.DryWetMidi.Standards
{
	public enum GeneralMidi2PercussionSet : byte
	{
		Standard = 0,
		Room = 8,
		Power = 16,
		Electronic = 24,
		Analog = 25,
		Jazz = 32,
		Brush = 40,
		Orchestra = 48,
		Sfx = 56
	}
	public enum GeneralMidi2Program
	{
		AcousticGrandPiano,
		AcousticGrandPianoWide,
		AcousticGrandPianoDark,
		BrightAcousticPiano,
		BrightAcousticPianoWide,
		ElectricGrandPiano,
		ElectricGrandPianoWide,
		HonkyTonkPiano,
		HonkyTonkPianoWide,
		ElectricPiano1,
		DetunedElectricPiano1,
		ElectricPiano1VelocityMix,
		SixtiesElectricPiano,
		ElectricPiano2,
		DetunedElectricPiano2,
		ElectricPiano2VelocityMix,
		EpLegend,
		EpPhase,
		Harpsichord,
		HarpsichordOctaveMix,
		HarpsichordWide,
		HarpsichordWithKeyOff,
		Clavi,
		PulseClavi,
		Celesta,
		Glockenspiel,
		MusicBox,
		Vibraphone,
		VibraphoneWide,
		Marimba,
		MarimbaWide,
		Xylophone,
		TubularBells,
		ChurchBell,
		Carillon,
		Dulcimer,
		DrawbarOrgan,
		DetunedDrawbarOrgan,
		ItalianSixtiesOrgan,
		DrawbarOrgan2,
		PercussiveOrgan,
		DetunedPercussiveOrgan,
		PercussiveOrgan2,
		RockOrgan,
		ChurchOrgan,
		ChurchOrganOctaveMix,
		DetunedChurchOrgan,
		ReedOrgan,
		PuffOrgan,
		Accordion,
		Accordion2,
		Harmonica,
		TangoAccordion,
		AcousticGuitarNylon,
		Ukulele,
		AcousticGuitarNylonKeyOff,
		AcousticGuitarNylon2,
		AcousticGuitarSteel,
		TwelveStringsGuitar,
		Mandolin,
		SteelGuitarWithBodySound,
		ElectricGuitarJazz,
		ElectricGuitarPedalSteel,
		ElectricGuitarClean,
		ElectricGuitarDetunedClean,
		MidToneGuitar,
		ElectricGuitarMuted,
		ElectricGuitarFunkyCutting,
		ElectricGuitarMutedVeloSw,
		JazzMan,
		OverdrivenGuitar,
		GuitarPinch,
		DistortionGuitar,
		DistortionGuitarWithFeedback,
		DistortedRhythmGuitar,
		GuitarHarmonics,
		GuitarFeedback,
		AcousticBass,
		ElectricBassFinger,
		FingerSlapBass,
		ElectricBassPick,
		FretlessBass,
		SlapBass1,
		SlapBass2,
		SynthBass1,
		SynthBassWarm,
		SynthBass3Resonance,
		ClaviBass,
		Hammer,
		SynthBass2,
		SynthBass4Attack,
		SynthBassRubber,
		AttackPulse,
		Violin,
		ViolinSlowAttack,
		Viola,
		Cello,
		Contrabass,
		TremoloStrings,
		PizzicatoStrings,
		OrchestralHarp,
		YangChin,
		Timpani,
		StringEnsembles1,
		StringsAndBrass,
		SixtiesStrings,
		StringEnsembles2,
		SynthStrings1,
		SynthStrings3,
		SynthStrings2,
		ChoirAahs,
		ChoirAahs2,
		VoiceOohs,
		Humming,
		SynthVoice,
		AnalogVoice,
		OrchestraHit,
		BassHitPlus,
		SixthHit,
		EuroHit,
		Trumpet,
		DarkTrumpetSoft,
		Trombone,
		Trombone2,
		BrightTrombone,
		Tuba,
		MutedTrumpet,
		MutedTrumpet2,
		FrenchHorn,
		FrenchHorn2Warm,
		BrassSection,
		BrassSection2OctaveMix,
		SynthBrass1,
		SynthBrass3,
		AnalogSynthBrass1,
		JumpBrass,
		SynthBrass2,
		SynthBrass4,
		AnalogSynthBrass2,
		SopranoSax,
		AltoSax,
		TenorSax,
		BaritoneSax,
		Oboe,
		EnglishHorn,
		Bassoon,
		Clarinet,
		Piccolo,
		Flute,
		Recorder,
		PanFlute,
		BlownBottle,
		Shakuhachi,
		Whistle,
		Ocarina,
		Lead1Square,
		Lead1ASquare2,
		Lead1BSine,
		Lead2Sawtooth,
		Lead2ASawtooth2,
		Lead2BSawPulse,
		Lead2CDoubleSawtooth,
		Lead2DSequencedAnalog,
		Lead3Calliope,
		Lead4Chiff,
		Lead5Charang,
		Lead5AWireLead,
		Lead6Voice,
		Lead7Fifths,
		Lead8BassLead,
		Lead8ASoftWrl,
		Pad1NewAge,
		Pad2Warm,
		Pad2ASinePad,
		Pad3Polysynth,
		Pad4Choir,
		Pad4AItopia,
		Pad5Bowed,
		Pad6Metallic,
		Pad7Halo,
		Pad8Sweep,
		Fx1Rain,
		Fx2Soundtrack,
		Fx3Crystal,
		Fx3ASynthMallet,
		Fx4Atmosphere,
		Fx5Brightness,
		Fx6Goblins,
		Fx7Echoes,
		Fx7AEchoBell,
		Fx7BEchoPan,
		Fx8SciFi,
		Sitar,
		Sitar2Bend,
		Banjo,
		Shamisen,
		Koto,
		TaishoKoto,
		Kalimba,
		BagPipe,
		Fiddle,
		Shanai,
		TinkleBell,
		Agogo,
		SteelDrums,
		Woodblock,
		Castanets,
		TaikoDrum,
		ConcertBassDrum,
		MelodicTom,
		MelodicTom2Power,
		SynthDrum,
		RhythmBoxTom,
		ElectricDrum,
		ReverseCymbal,
		GuitarFretNoise,
		GuitarCuttingNoise,
		AcousticBassStringSlap,
		BreathNoise,
		FluteKeyClick,
		Seashore,
		Rain,
		Thunder,
		Wind,
		Stream,
		Bubble,
		BirdTweet,
		Dog,
		HorseGallop,
		BirdTweet2,
		TelephoneRing,
		TelephoneRing2,
		DoorCreaking,
		Door,
		Scratch,
		WindChime,
		Helicopter,
		CarEngine,
		CarStop,
		CarPass,
		CarCrash,
		Siren,
		Train,
		Jetplane,
		Starship,
		BurstNoise,
		Applause,
		Laughing,
		Screaming,
		Punch,
		HeartBeat,
		Footsteps,
		Gunshot,
		MachineGun,
		Lasergun,
		Explosion
	}
	public static class GeneralMidi2Utilities
	{
		private sealed class GeneralMidi2ProgramData
		{
			public GeneralMidi2Program GeneralMidi2Program { get; }

			public GeneralMidiProgram GeneralMidiProgram { get; }

			public SevenBitNumber BankMsb { get; }

			public SevenBitNumber BankLsb { get; }

			public GeneralMidi2ProgramData(GeneralMidi2Program generalMidi2Program, GeneralMidiProgram generalMidiProgram, SevenBitNumber bankMsb, SevenBitNumber bankLsb)
			{
				GeneralMidi2Program = generalMidi2Program;
				GeneralMidiProgram = generalMidiProgram;
				BankMsb = bankMsb;
				BankLsb = bankLsb;
			}
		}

		private const byte MelodyChannelBankMsb = 121;

		private const byte RhythmChannelBankMsb = 120;

		private static readonly Dictionary<GeneralMidi2Program, GeneralMidi2ProgramData> ProgramsData = new IEnumerable<GeneralMidi2ProgramData>[128]
		{
			GetProgramsData(GeneralMidiProgram.AcousticGrandPiano, GeneralMidi2Program.AcousticGrandPiano, GeneralMidi2Program.AcousticGrandPianoWide, GeneralMidi2Program.AcousticGrandPianoDark),
			GetProgramsData(GeneralMidiProgram.BrightAcousticPiano, GeneralMidi2Program.BrightAcousticPiano, GeneralMidi2Program.BrightAcousticPianoWide),
			GetProgramsData(GeneralMidiProgram.ElectricGrandPiano, GeneralMidi2Program.ElectricGrandPiano, GeneralMidi2Program.ElectricGrandPianoWide),
			GetProgramsData(GeneralMidiProgram.HonkyTonkPiano, GeneralMidi2Program.HonkyTonkPiano, GeneralMidi2Program.HonkyTonkPianoWide),
			GetProgramsData(GeneralMidiProgram.ElectricPiano1, GeneralMidi2Program.ElectricPiano1, GeneralMidi2Program.DetunedElectricPiano1, GeneralMidi2Program.ElectricPiano1VelocityMix, GeneralMidi2Program.SixtiesElectricPiano),
			GetProgramsData(GeneralMidiProgram.ElectricPiano2, GeneralMidi2Program.ElectricPiano2, GeneralMidi2Program.DetunedElectricPiano2, GeneralMidi2Program.ElectricPiano2VelocityMix, GeneralMidi2Program.EpLegend, GeneralMidi2Program.EpPhase),
			GetProgramsData(GeneralMidiProgram.Harpsichord, GeneralMidi2Program.Harpsichord, GeneralMidi2Program.HarpsichordOctaveMix, GeneralMidi2Program.HarpsichordWide, GeneralMidi2Program.HarpsichordWithKeyOff),
			GetProgramsData(GeneralMidiProgram.Clavi, GeneralMidi2Program.Clavi, GeneralMidi2Program.PulseClavi),
			GetProgramsData(GeneralMidiProgram.Celesta, GeneralMidi2Program.Celesta),
			GetProgramsData(GeneralMidiProgram.Glockenspiel, GeneralMidi2Program.Glockenspiel),
			GetProgramsData(GeneralMidiProgram.MusicBox, GeneralMidi2Program.MusicBox),
			GetProgramsData(GeneralMidiProgram.Vibraphone, GeneralMidi2Program.Vibraphone, GeneralMidi2Program.VibraphoneWide),
			GetProgramsData(GeneralMidiProgram.Marimba, GeneralMidi2Program.Marimba, GeneralMidi2Program.MarimbaWide),
			GetProgramsData(GeneralMidiProgram.Xylophone, GeneralMidi2Program.Xylophone),
			GetProgramsData(GeneralMidiProgram.TubularBells, GeneralMidi2Program.TubularBells, GeneralMidi2Program.ChurchBell, GeneralMidi2Program.Carillon),
			GetProgramsData(GeneralMidiProgram.Dulcimer, GeneralMidi2Program.Dulcimer),
			GetProgramsData(GeneralMidiProgram.DrawbarOrgan, GeneralMidi2Program.DrawbarOrgan, GeneralMidi2Program.DetunedDrawbarOrgan, GeneralMidi2Program.ItalianSixtiesOrgan, GeneralMidi2Program.DrawbarOrgan2),
			GetProgramsData(GeneralMidiProgram.PercussiveOrgan, GeneralMidi2Program.PercussiveOrgan, GeneralMidi2Program.DetunedPercussiveOrgan, GeneralMidi2Program.PercussiveOrgan2),
			GetProgramsData(GeneralMidiProgram.RockOrgan, GeneralMidi2Program.RockOrgan),
			GetProgramsData(GeneralMidiProgram.ChurchOrgan, GeneralMidi2Program.ChurchOrgan, GeneralMidi2Program.ChurchOrganOctaveMix, GeneralMidi2Program.DetunedChurchOrgan),
			GetProgramsData(GeneralMidiProgram.ReedOrgan, GeneralMidi2Program.ReedOrgan, GeneralMidi2Program.PuffOrgan),
			GetProgramsData(GeneralMidiProgram.Accordion, GeneralMidi2Program.Accordion, GeneralMidi2Program.Accordion2),
			GetProgramsData(GeneralMidiProgram.Harmonica, GeneralMidi2Program.Harmonica),
			GetProgramsData(GeneralMidiProgram.TangoAccordion, GeneralMidi2Program.TangoAccordion),
			GetProgramsData(GeneralMidiProgram.AcousticGuitar1, GeneralMidi2Program.AcousticGuitarNylon, GeneralMidi2Program.Ukulele, GeneralMidi2Program.AcousticGuitarNylonKeyOff, GeneralMidi2Program.AcousticGuitarNylon2),
			GetProgramsData(GeneralMidiProgram.AcousticGuitar2, GeneralMidi2Program.AcousticGuitarSteel, GeneralMidi2Program.TwelveStringsGuitar, GeneralMidi2Program.Mandolin, GeneralMidi2Program.SteelGuitarWithBodySound),
			GetProgramsData(GeneralMidiProgram.ElectricGuitar1, GeneralMidi2Program.ElectricGuitarJazz, GeneralMidi2Program.ElectricGuitarPedalSteel),
			GetProgramsData(GeneralMidiProgram.ElectricGuitar2, GeneralMidi2Program.ElectricGuitarClean, GeneralMidi2Program.ElectricGuitarDetunedClean, GeneralMidi2Program.MidToneGuitar),
			GetProgramsData(GeneralMidiProgram.ElectricGuitar3, GeneralMidi2Program.ElectricGuitarMuted, GeneralMidi2Program.ElectricGuitarFunkyCutting, GeneralMidi2Program.ElectricGuitarMutedVeloSw, GeneralMidi2Program.JazzMan),
			GetProgramsData(GeneralMidiProgram.OverdrivenGuitar, GeneralMidi2Program.OverdrivenGuitar, GeneralMidi2Program.GuitarPinch),
			GetProgramsData(GeneralMidiProgram.DistortionGuitar, GeneralMidi2Program.DistortionGuitar, GeneralMidi2Program.DistortionGuitarWithFeedback, GeneralMidi2Program.DistortedRhythmGuitar),
			GetProgramsData(GeneralMidiProgram.GuitarHarmonics, GeneralMidi2Program.GuitarHarmonics, GeneralMidi2Program.GuitarFeedback),
			GetProgramsData(GeneralMidiProgram.AcousticBass, GeneralMidi2Program.AcousticBass),
			GetProgramsData(GeneralMidiProgram.ElectricBass1, GeneralMidi2Program.ElectricBassFinger, GeneralMidi2Program.FingerSlapBass),
			GetProgramsData(GeneralMidiProgram.ElectricBass2, GeneralMidi2Program.ElectricBassPick),
			GetProgramsData(GeneralMidiProgram.FretlessBass, GeneralMidi2Program.FretlessBass),
			GetProgramsData(GeneralMidiProgram.SlapBass1, GeneralMidi2Program.SlapBass1),
			GetProgramsData(GeneralMidiProgram.SlapBass2, GeneralMidi2Program.SlapBass2),
			GetProgramsData(GeneralMidiProgram.SynthBass1, GeneralMidi2Program.SynthBass1, GeneralMidi2Program.SynthBassWarm, GeneralMidi2Program.SynthBass3Resonance, GeneralMidi2Program.ClaviBass, GeneralMidi2Program.Hammer),
			GetProgramsData(GeneralMidiProgram.SynthBass2, GeneralMidi2Program.SynthBass2, GeneralMidi2Program.SynthBass4Attack, GeneralMidi2Program.SynthBassRubber, GeneralMidi2Program.AttackPulse),
			GetProgramsData(GeneralMidiProgram.Violin, GeneralMidi2Program.Violin, GeneralMidi2Program.ViolinSlowAttack),
			GetProgramsData(GeneralMidiProgram.Viola, GeneralMidi2Program.Viola),
			GetProgramsData(GeneralMidiProgram.Cello, GeneralMidi2Program.Cello),
			GetProgramsData(GeneralMidiProgram.Contrabass, GeneralMidi2Program.Contrabass),
			GetProgramsData(GeneralMidiProgram.TremoloStrings, GeneralMidi2Program.TremoloStrings),
			GetProgramsData(GeneralMidiProgram.PizzicatoStrings, GeneralMidi2Program.PizzicatoStrings),
			GetProgramsData(GeneralMidiProgram.OrchestralHarp, GeneralMidi2Program.OrchestralHarp, GeneralMidi2Program.YangChin),
			GetProgramsData(GeneralMidiProgram.Timpani, GeneralMidi2Program.Timpani),
			GetProgramsData(GeneralMidiProgram.StringEnsemble1, GeneralMidi2Program.StringEnsembles1, GeneralMidi2Program.StringsAndBrass, GeneralMidi2Program.SixtiesStrings),
			GetProgramsData(GeneralMidiProgram.StringEnsemble2, GeneralMidi2Program.StringEnsembles2),
			GetProgramsData(GeneralMidiProgram.SynthStrings1, GeneralMidi2Program.SynthStrings1, GeneralMidi2Program.SynthStrings3),
			GetProgramsData(GeneralMidiProgram.SynthStrings2, GeneralMidi2Program.SynthStrings2),
			GetProgramsData(GeneralMidiProgram.ChoirAahs, GeneralMidi2Program.ChoirAahs, GeneralMidi2Program.ChoirAahs2),
			GetProgramsData(GeneralMidiProgram.VoiceOohs, GeneralMidi2Program.VoiceOohs, GeneralMidi2Program.Humming),
			GetProgramsData(GeneralMidiProgram.SynthVoice, GeneralMidi2Program.SynthVoice, GeneralMidi2Program.AnalogVoice),
			GetProgramsData(GeneralMidiProgram.OrchestraHit, GeneralMidi2Program.OrchestraHit, GeneralMidi2Program.BassHitPlus, GeneralMidi2Program.SixthHit, GeneralMidi2Program.EuroHit),
			GetProgramsData(GeneralMidiProgram.Trumpet, GeneralMidi2Program.Trumpet, GeneralMidi2Program.DarkTrumpetSoft),
			GetProgramsData(GeneralMidiProgram.Trombone, GeneralMidi2Program.Trombone, GeneralMidi2Program.Trombone2, GeneralMidi2Program.BrightTrombone),
			GetProgramsData(GeneralMidiProgram.Tuba, GeneralMidi2Program.Tuba),
			GetProgramsData(GeneralMidiProgram.MutedTrumpet, GeneralMidi2Program.MutedTrumpet, GeneralMidi2Program.MutedTrumpet2),
			GetProgramsData(GeneralMidiProgram.FrenchHorn, GeneralMidi2Program.FrenchHorn, GeneralMidi2Program.FrenchHorn2Warm),
			GetProgramsData(GeneralMidiProgram.BrassSection, GeneralMidi2Program.BrassSection, GeneralMidi2Program.BrassSection2OctaveMix),
			GetProgramsData(GeneralMidiProgram.SynthBrass1, GeneralMidi2Program.SynthBrass1, GeneralMidi2Program.SynthBrass3, GeneralMidi2Program.AnalogSynthBrass1, GeneralMidi2Program.JumpBrass),
			GetProgramsData(GeneralMidiProgram.SynthBrass2, GeneralMidi2Program.SynthBrass2, GeneralMidi2Program.SynthBrass4, GeneralMidi2Program.AnalogSynthBrass2),
			GetProgramsData(GeneralMidiProgram.SopranoSax, GeneralMidi2Program.SopranoSax),
			GetProgramsData(GeneralMidiProgram.AltoSax, GeneralMidi2Program.AltoSax),
			GetProgramsData(GeneralMidiProgram.TenorSax, GeneralMidi2Program.TenorSax),
			GetProgramsData(GeneralMidiProgram.BaritoneSax, GeneralMidi2Program.BaritoneSax),
			GetProgramsData(GeneralMidiProgram.Oboe, GeneralMidi2Program.Oboe),
			GetProgramsData(GeneralMidiProgram.EnglishHorn, GeneralMidi2Program.EnglishHorn),
			GetProgramsData(GeneralMidiProgram.Bassoon, GeneralMidi2Program.Bassoon),
			GetProgramsData(GeneralMidiProgram.Clarinet, GeneralMidi2Program.Clarinet),
			GetProgramsData(GeneralMidiProgram.Piccolo, GeneralMidi2Program.Piccolo),
			GetProgramsData(GeneralMidiProgram.Flute, GeneralMidi2Program.Flute),
			GetProgramsData(GeneralMidiProgram.Recorder, GeneralMidi2Program.Recorder),
			GetProgramsData(GeneralMidiProgram.PanFlute, GeneralMidi2Program.PanFlute),
			GetProgramsData(GeneralMidiProgram.BlownBottle, GeneralMidi2Program.BlownBottle),
			GetProgramsData(GeneralMidiProgram.Shakuhachi, GeneralMidi2Program.Shakuhachi),
			GetProgramsData(GeneralMidiProgram.Whistle, GeneralMidi2Program.Whistle),
			GetProgramsData(GeneralMidiProgram.Ocarina, GeneralMidi2Program.Ocarina),
			GetProgramsData(GeneralMidiProgram.Lead1, GeneralMidi2Program.Lead1Square, GeneralMidi2Program.Lead1ASquare2, GeneralMidi2Program.Lead1BSine),
			GetProgramsData(GeneralMidiProgram.Lead2, GeneralMidi2Program.Lead2Sawtooth, GeneralMidi2Program.Lead2ASawtooth2, GeneralMidi2Program.Lead2BSawPulse, GeneralMidi2Program.Lead2CDoubleSawtooth, GeneralMidi2Program.Lead2DSequencedAnalog),
			GetProgramsData(GeneralMidiProgram.Lead3, GeneralMidi2Program.Lead3Calliope),
			GetProgramsData(GeneralMidiProgram.Lead4, GeneralMidi2Program.Lead4Chiff),
			GetProgramsData(GeneralMidiProgram.Lead5, GeneralMidi2Program.Lead5Charang, GeneralMidi2Program.Lead5AWireLead),
			GetProgramsData(GeneralMidiProgram.Lead6, GeneralMidi2Program.Lead6Voice),
			GetProgramsData(GeneralMidiProgram.Lead7, GeneralMidi2Program.Lead7Fifths),
			GetProgramsData(GeneralMidiProgram.Lead8, GeneralMidi2Program.Lead8BassLead, GeneralMidi2Program.Lead8ASoftWrl),
			GetProgramsData(GeneralMidiProgram.Pad1, GeneralMidi2Program.Pad1NewAge),
			GetProgramsData(GeneralMidiProgram.Pad2, GeneralMidi2Program.Pad2Warm, GeneralMidi2Program.Pad2ASinePad),
			GetProgramsData(GeneralMidiProgram.Pad3, GeneralMidi2Program.Pad3Polysynth),
			GetProgramsData(GeneralMidiProgram.Pad4, GeneralMidi2Program.Pad4Choir, GeneralMidi2Program.Pad4AItopia),
			GetProgramsData(GeneralMidiProgram.Pad5, GeneralMidi2Program.Pad5Bowed),
			GetProgramsData(GeneralMidiProgram.Pad6, GeneralMidi2Program.Pad6Metallic),
			GetProgramsData(GeneralMidiProgram.Pad7, GeneralMidi2Program.Pad7Halo),
			GetProgramsData(GeneralMidiProgram.Pad8, GeneralMidi2Program.Pad8Sweep),
			GetProgramsData(GeneralMidiProgram.Fx1, GeneralMidi2Program.Fx1Rain),
			GetProgramsData(GeneralMidiProgram.Fx2, GeneralMidi2Program.Fx2Soundtrack),
			GetProgramsData(GeneralMidiProgram.Fx3, GeneralMidi2Program.Fx3Crystal, GeneralMidi2Program.Fx3ASynthMallet),
			GetProgramsData(GeneralMidiProgram.Fx4, GeneralMidi2Program.Fx4Atmosphere),
			GetProgramsData(GeneralMidiProgram.Fx5, GeneralMidi2Program.Fx5Brightness),
			GetProgramsData(GeneralMidiProgram.Fx6, GeneralMidi2Program.Fx6Goblins),
			GetProgramsData(GeneralMidiProgram.Fx7, GeneralMidi2Program.Fx7Echoes, GeneralMidi2Program.Fx7AEchoBell, GeneralMidi2Program.Fx7BEchoPan),
			GetProgramsData(GeneralMidiProgram.Fx8, GeneralMidi2Program.Fx8SciFi),
			GetProgramsData(GeneralMidiProgram.Sitar, GeneralMidi2Program.Sitar, GeneralMidi2Program.Sitar2Bend),
			GetProgramsData(GeneralMidiProgram.Banjo, GeneralMidi2Program.Banjo),
			GetProgramsData(GeneralMidiProgram.Shamisen, GeneralMidi2Program.Shamisen),
			GetProgramsData(GeneralMidiProgram.Koto, GeneralMidi2Program.Koto, GeneralMidi2Program.TaishoKoto),
			GetProgramsData(GeneralMidiProgram.Kalimba, GeneralMidi2Program.Kalimba),
			GetProgramsData(GeneralMidiProgram.BagPipe, GeneralMidi2Program.BagPipe),
			GetProgramsData(GeneralMidiProgram.Fiddle, GeneralMidi2Program.Fiddle),
			GetProgramsData(GeneralMidiProgram.Shanai, GeneralMidi2Program.Shanai),
			GetProgramsData(GeneralMidiProgram.TinkleBell, GeneralMidi2Program.TinkleBell),
			GetProgramsData(GeneralMidiProgram.Agogo, GeneralMidi2Program.Agogo),
			GetProgramsData(GeneralMidiProgram.SteelDrums, GeneralMidi2Program.SteelDrums),
			GetProgramsData(GeneralMidiProgram.Woodblock, GeneralMidi2Program.Woodblock, GeneralMidi2Program.Castanets),
			GetProgramsData(GeneralMidiProgram.TaikoDrum, GeneralMidi2Program.TaikoDrum, GeneralMidi2Program.ConcertBassDrum),
			GetProgramsData(GeneralMidiProgram.MelodicTom, GeneralMidi2Program.MelodicTom, GeneralMidi2Program.MelodicTom2Power),
			GetProgramsData(GeneralMidiProgram.SynthDrum, GeneralMidi2Program.SynthDrum, GeneralMidi2Program.RhythmBoxTom, GeneralMidi2Program.ElectricDrum),
			GetProgramsData(GeneralMidiProgram.ReverseCymbal, GeneralMidi2Program.ReverseCymbal),
			GetProgramsData(GeneralMidiProgram.GuitarFretNoise, GeneralMidi2Program.GuitarFretNoise, GeneralMidi2Program.GuitarCuttingNoise, GeneralMidi2Program.AcousticBassStringSlap),
			GetProgramsData(GeneralMidiProgram.BreathNoise, GeneralMidi2Program.BreathNoise, GeneralMidi2Program.FluteKeyClick),
			GetProgramsData(GeneralMidiProgram.Seashore, GeneralMidi2Program.Seashore, GeneralMidi2Program.Rain, GeneralMidi2Program.Thunder, GeneralMidi2Program.Wind, GeneralMidi2Program.Stream, GeneralMidi2Program.Bubble),
			GetProgramsData(GeneralMidiProgram.BirdTweet, GeneralMidi2Program.BirdTweet, GeneralMidi2Program.Dog, GeneralMidi2Program.HorseGallop, GeneralMidi2Program.BirdTweet2),
			GetProgramsData(GeneralMidiProgram.TelephoneRing, GeneralMidi2Program.TelephoneRing, GeneralMidi2Program.TelephoneRing2, GeneralMidi2Program.DoorCreaking, GeneralMidi2Program.Door, GeneralMidi2Program.Scratch, GeneralMidi2Program.WindChime),
			GetProgramsData(GeneralMidiProgram.Helicopter, GeneralMidi2Program.Helicopter, GeneralMidi2Program.CarEngine, GeneralMidi2Program.CarStop, GeneralMidi2Program.CarPass, GeneralMidi2Program.CarCrash, GeneralMidi2Program.Siren, GeneralMidi2Program.Train, GeneralMidi2Program.Jetplane, GeneralMidi2Program.Starship, GeneralMidi2Program.BurstNoise),
			GetProgramsData(GeneralMidiProgram.Applause, GeneralMidi2Program.Applause, GeneralMidi2Program.Laughing, GeneralMidi2Program.Screaming, GeneralMidi2Program.Punch, GeneralMidi2Program.HeartBeat, GeneralMidi2Program.Footsteps),
			GetProgramsData(GeneralMidiProgram.Gunshot, GeneralMidi2Program.Gunshot, GeneralMidi2Program.MachineGun, GeneralMidi2Program.Lasergun, GeneralMidi2Program.Explosion)
		}.SelectMany((IEnumerable<GeneralMidi2ProgramData> d) => d).ToDictionary((GeneralMidi2ProgramData d) => d.GeneralMidi2Program, (GeneralMidi2ProgramData d) => d);

		public static IEnumerable<MidiEvent> GetProgramEvents(this GeneralMidi2Program program, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("program", program);
			GeneralMidi2ProgramData generalMidi2ProgramData = ProgramsData[program];
			return new MidiEvent[3]
			{
				ControlName.BankSelect.GetControlChangeEvent(generalMidi2ProgramData.BankMsb, channel),
				ControlName.LsbForBankSelect.GetControlChangeEvent(generalMidi2ProgramData.BankLsb, channel),
				generalMidi2ProgramData.GeneralMidiProgram.GetProgramEvent(channel)
			};
		}

		public static IEnumerable<MidiEvent> GetPercussionSetEvents(this GeneralMidi2PercussionSet percussionSet, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussionSet", percussionSet);
			return new MidiEvent[3]
			{
				ControlName.BankSelect.GetControlChangeEvent((SevenBitNumber)120, channel),
				ControlName.LsbForBankSelect.GetControlChangeEvent((SevenBitNumber)0, channel),
				percussionSet.GetProgramEvent(channel)
			};
		}

		public static MidiEvent GetProgramEvent(this GeneralMidi2PercussionSet percussionSet, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussionSet", percussionSet);
			return new ProgramChangeEvent(percussionSet.AsSevenBitNumber())
			{
				Channel = channel
			};
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2PercussionSet percussionSet)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussionSet", percussionSet);
			return (SevenBitNumber)(byte)percussionSet;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2AnalogPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2BrushPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2ElectronicPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2JazzPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2OrchestraPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2PowerPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2RoomPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2SfxPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidi2StandardPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2AnalogPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2BrushPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2ElectronicPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2JazzPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2OrchestraPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2PowerPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2RoomPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2SfxPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidi2StandardPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2AnalogPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2BrushPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2ElectronicPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2JazzPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2OrchestraPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2PowerPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2RoomPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2SfxPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidi2StandardPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		private static IEnumerable<GeneralMidi2ProgramData> GetProgramsData(GeneralMidiProgram generalMidiProgram, params GeneralMidi2Program[] programs)
		{
			return programs.Select((GeneralMidi2Program p, int i) => GetProgramData(p, generalMidiProgram, 121, (byte)i));
		}

		private static GeneralMidi2ProgramData GetProgramData(GeneralMidi2Program generalMidi2Program, GeneralMidiProgram generalMidiProgram, byte bankMsb, byte bankLsb)
		{
			return new GeneralMidi2ProgramData(generalMidi2Program, generalMidiProgram, (SevenBitNumber)bankMsb, (SevenBitNumber)bankLsb);
		}
	}
	public enum GeneralMidi2AnalogPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		AnalogBassDrum,
		AnalogRimShot,
		AnalogSnare1,
		HandClap,
		ElectricSnare,
		AnalogLowTom2,
		AnalogClosedHiHat1,
		AnalogLowTom1,
		AnalogClosedHiHat2,
		AnalogMidTom2,
		AnalogOpenHiHat,
		AnalogMidTom1,
		AnalogHiTom2,
		AnalogCymbal,
		AnalogHiTom1,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		AnalogCowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		AnalogHighConga,
		AnalogMidConga,
		AnalogLowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		AnalogMaracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		AnalogClaves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralMidi2BrushPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		JazzKick2,
		JazzKick1,
		SideStick,
		BrushTap,
		BrushSlap,
		BrushSwirl,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralMidi2ElectronicPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		ElectricBassDrum,
		SideStick,
		ElectricSnare1,
		HandClap,
		ElectricSnare2,
		ElectricLowTom2,
		ClosedHiHat,
		ElectricLowTom1,
		PedalHiHat,
		ElectricMidTom2,
		OpenHiHat,
		ElectricMidTom1,
		ElectricHiTom2,
		CrashCymbal1,
		ElectricHiTom1,
		RideCymbal1,
		ReverseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralMidi2JazzPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		JazzKick2,
		JazzKick1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralMidi2OrchestraPercussion : byte
	{
		ClosedHiHat2 = 27,
		PedalHiHat,
		OpenHiHat2,
		RideCymbal1,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		ConcertBassDrum2,
		ConcertBassDrum1,
		SideStick,
		ConcertSnareDrum,
		Castanets,
		ConcertSnareDrum2,
		TimpaniF,
		TimpaniFSharp,
		TimpaniG,
		TimpaniGSharp,
		TimpaniA,
		TimpaniASharp,
		TimpaniB,
		TimpaniC,
		TimpaniCSharp,
		TimpaniD,
		TimpaniDSharp,
		TimpaniE,
		TimpaniF2,
		Tambourine,
		SplashCymbal,
		Cowbell,
		ConcertCymbal2,
		Vibraslap,
		ConcertCymbal1,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets2,
		MuteSurdo,
		OpenSurdo,
		Applause
	}
	public enum GeneralMidi2PowerPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		PowerKickDrum,
		SideStick,
		PowerSnareDrum,
		HandClap,
		ElectricSnare,
		PowerLowTom2,
		ClosedHiHat,
		PowerLowTom1,
		PedalHiHat,
		PowerMidTom2,
		OpenHiHat,
		PowerMidTom1,
		PowerHiTom2,
		CrashCymbal1,
		PowerHiTom1,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralMidi2RoomPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		BassDrum1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		RoomLowTom2,
		ClosedHiHat,
		RoomLowTom1,
		PedalHiHat,
		RoomMidTom2,
		OpenHiHat,
		RoomMidTom1,
		RoomHiTom2,
		CrashCymbal1,
		RoomHiTom1,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralMidi2SfxPercussion : byte
	{
		HighQ = 39,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		GuitarFretNoise,
		GuitarCuttingNoiseUp,
		GuitarCuttingNoiseDown,
		StringSlapOfDoubleBass,
		FlKeyClick,
		Laughing,
		Scream,
		Punch,
		HeartBeat,
		Footsteps1,
		Footsteps2,
		Applause,
		DoorCreaking,
		Door,
		Scratch,
		WindChimes,
		CarEngine,
		CarStop,
		CarPass,
		CarCrash,
		Siren,
		Train,
		Jetplane,
		Helicopter,
		Starship,
		GunShot,
		MachineGun,
		Lasergun,
		Explosion,
		Dog,
		HorseGallop,
		Birds,
		Rain,
		Thunder,
		Wind,
		Seashore,
		Stream,
		Bubble
	}
	public enum GeneralMidi2StandardPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		BassDrum1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public static class GeneralMidi
	{
		public static readonly FourBitNumber PercussionChannel = (FourBitNumber)9;
	}
	public enum GeneralMidiPercussion : byte
	{
		AcousticBassDrum = 35,
		BassDrum1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle
	}
	public enum GeneralMidiProgram : byte
	{
		AcousticGrandPiano,
		BrightAcousticPiano,
		ElectricGrandPiano,
		HonkyTonkPiano,
		ElectricPiano1,
		ElectricPiano2,
		Harpsichord,
		Clavi,
		Celesta,
		Glockenspiel,
		MusicBox,
		Vibraphone,
		Marimba,
		Xylophone,
		TubularBells,
		Dulcimer,
		DrawbarOrgan,
		PercussiveOrgan,
		RockOrgan,
		ChurchOrgan,
		ReedOrgan,
		Accordion,
		Harmonica,
		TangoAccordion,
		AcousticGuitar1,
		AcousticGuitar2,
		ElectricGuitar1,
		ElectricGuitar2,
		ElectricGuitar3,
		OverdrivenGuitar,
		DistortionGuitar,
		GuitarHarmonics,
		AcousticBass,
		ElectricBass1,
		ElectricBass2,
		FretlessBass,
		SlapBass1,
		SlapBass2,
		SynthBass1,
		SynthBass2,
		Violin,
		Viola,
		Cello,
		Contrabass,
		TremoloStrings,
		PizzicatoStrings,
		OrchestralHarp,
		Timpani,
		StringEnsemble1,
		StringEnsemble2,
		SynthStrings1,
		SynthStrings2,
		ChoirAahs,
		VoiceOohs,
		SynthVoice,
		OrchestraHit,
		Trumpet,
		Trombone,
		Tuba,
		MutedTrumpet,
		FrenchHorn,
		BrassSection,
		SynthBrass1,
		SynthBrass2,
		SopranoSax,
		AltoSax,
		TenorSax,
		BaritoneSax,
		Oboe,
		EnglishHorn,
		Bassoon,
		Clarinet,
		Piccolo,
		Flute,
		Recorder,
		PanFlute,
		BlownBottle,
		Shakuhachi,
		Whistle,
		Ocarina,
		Lead1,
		Lead2,
		Lead3,
		Lead4,
		Lead5,
		Lead6,
		Lead7,
		Lead8,
		Pad1,
		Pad2,
		Pad3,
		Pad4,
		Pad5,
		Pad6,
		Pad7,
		Pad8,
		Fx1,
		Fx2,
		Fx3,
		Fx4,
		Fx5,
		Fx6,
		Fx7,
		Fx8,
		Sitar,
		Banjo,
		Shamisen,
		Koto,
		Kalimba,
		BagPipe,
		Fiddle,
		Shanai,
		TinkleBell,
		Agogo,
		SteelDrums,
		Woodblock,
		TaikoDrum,
		MelodicTom,
		SynthDrum,
		ReverseCymbal,
		GuitarFretNoise,
		BreathNoise,
		Seashore,
		BirdTweet,
		TelephoneRing,
		Helicopter,
		Applause,
		Gunshot
	}
	public static class GeneralMidiUtilities
	{
		public static SevenBitNumber AsSevenBitNumber(this GeneralMidiProgram program)
		{
			ThrowIfArgument.IsInvalidEnumValue("program", program);
			return (SevenBitNumber)(byte)program;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralMidiPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static MidiEvent GetProgramEvent(this GeneralMidiProgram program, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("program", program);
			return new ProgramChangeEvent(program.AsSevenBitNumber())
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralMidiPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralMidiPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}
	}
	public enum GeneralSoundPercussionSet : byte
	{
		Standard = 0,
		Room = 8,
		Power = 16,
		Electronic = 24,
		Tr808 = 25,
		Jazz = 32,
		Brush = 40,
		Orchestra = 48,
		Sfx = 56,
		Cm6432L = 127
	}
	public static class GeneralSoundUtilities
	{
		private const byte RhythmChannelBankMsb = 120;

		public static IEnumerable<MidiEvent> GetPercussionSetEvents(this GeneralSoundPercussionSet percussionSet, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussionSet", percussionSet);
			return new MidiEvent[3]
			{
				ControlName.BankSelect.GetControlChangeEvent((SevenBitNumber)120, channel),
				ControlName.LsbForBankSelect.GetControlChangeEvent((SevenBitNumber)0, channel),
				percussionSet.GetProgramEvent(channel)
			};
		}

		public static MidiEvent GetProgramEvent(this GeneralSoundPercussionSet percussionSet, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussionSet", percussionSet);
			return new ProgramChangeEvent(percussionSet.AsSevenBitNumber())
			{
				Channel = channel
			};
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundPercussionSet percussionSet)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussionSet", percussionSet);
			return (SevenBitNumber)(byte)percussionSet;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundCm6432LPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundTr808Percussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundBrushPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundElectronicPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundJazzPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundOrchestraPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundPowerPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundRoomPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundSfxPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static SevenBitNumber AsSevenBitNumber(this GeneralSoundStandardPercussion percussion)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return (SevenBitNumber)(byte)percussion;
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundTr808Percussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundCm6432LPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundBrushPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundElectronicPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundJazzPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundOrchestraPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundPowerPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundRoomPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundSfxPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOnEvent GetNoteOnEvent(this GeneralSoundStandardPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOnEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundTr808Percussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundCm6432LPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundBrushPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundElectronicPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundJazzPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundOrchestraPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundPowerPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundRoomPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundSfxPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}

		public static NoteOffEvent GetNoteOffEvent(this GeneralSoundStandardPercussion percussion, SevenBitNumber velocity, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("percussion", percussion);
			return new NoteOffEvent(percussion.AsSevenBitNumber(), velocity)
			{
				Channel = channel
			};
		}
	}
	public enum GeneralSoundBrushPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		JazzKick2,
		JazzKick1,
		SideStick,
		BrushTap,
		BrushSlap,
		BrushSwirl,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralSoundCm6432LPercussion : byte
	{
		AcousticBassDrum1 = 35,
		AcousticBassDrum2 = 36,
		RimShot = 37,
		AcousticSnareDrum = 38,
		HandClap = 39,
		ElectronicSnareDrum = 40,
		AcousticLowTom1 = 41,
		ClosedHiHat = 42,
		AcousticLowTom2 = 43,
		OpenHiHat1 = 44,
		AcousticMidTom1 = 45,
		OpenHiHat2 = 46,
		AcousticMidTom2 = 47,
		AcousticHiTom1 = 48,
		CrashCymbal = 49,
		AcousticHiTom2 = 50,
		RideCymbal = 51,
		Tambourine = 54,
		Cowbell = 56,
		HighBongo = 60,
		LowBongo = 61,
		MuteHiConga = 62,
		HighConga = 63,
		LowConga = 64,
		HighTimbale = 65,
		LowTimbale = 66,
		HighAgogo = 67,
		LowAgogo = 68,
		Cabasa = 69,
		Maracas = 70,
		ShortWhistle = 71,
		LongWhistle = 72,
		Quijada = 73,
		Claves = 75,
		Laughing = 76,
		Screaming = 77,
		Punch = 78,
		Heartbeat = 79,
		Footsteps1 = 80,
		Footsteps2 = 81,
		Applause = 82,
		DoorCreaking = 83,
		DoorClosing = 84,
		Scratch = 85,
		WindChimes = 86,
		CarEngine = 87,
		CarBrakes = 88,
		CarPassing = 89,
		CarCrash = 90,
		Siren = 91,
		Train = 92,
		JetPlane = 93,
		Helicopter = 94,
		Starship = 95,
		GunShot = 96,
		MachineGun = 97,
		LaserGun = 98,
		Explosion = 99,
		DogBark = 100,
		HorseGallop = 101,
		BirdsTweet = 102,
		Rain = 103,
		Thunder = 104,
		Wind = 105,
		Seashore = 106,
		Stream = 107,
		Bubble = 108
	}
	public enum GeneralSoundElectronicPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		ElectricBassDrum,
		SideStick,
		ElectricSnare1,
		HandClap,
		ElectricSnare2,
		ElectricLowTom2,
		ClosedHiHat,
		ElectricLowTom1,
		PedalHiHat,
		ElectricMidTom2,
		OpenHiHat,
		ElectricMidTom1,
		ElectricHiTom2,
		CrashCymbal1,
		ElectricHiTom1,
		RideCymbal1,
		ReverseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralSoundJazzPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		JazzKick2,
		JazzKick1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralSoundOrchestraPercussion : byte
	{
		ClosedHiHat2 = 27,
		PedalHiHat,
		OpenHiHat2,
		RideCymbal1,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		ConcertBassDrum2,
		ConcertBassDrum1,
		SideStick,
		ConcertSnareDrum,
		Castanets,
		ConcertSnareDrum2,
		TimpaniF,
		TimpaniFSharp,
		TimpaniG,
		TimpaniGSharp,
		TimpaniA,
		TimpaniASharp,
		TimpaniB,
		TimpaniC,
		TimpaniCSharp,
		TimpaniD,
		TimpaniDSharp,
		TimpaniE,
		TimpaniF2,
		Tambourine,
		SplashCymbal,
		Cowbell,
		ConcertCymbal2,
		Vibraslap,
		ConcertCymbal1,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets2,
		MuteSurdo,
		OpenSurdo,
		Applause
	}
	public enum GeneralSoundPowerPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		PowerKickDrum,
		SideStick,
		PowerSnareDrum,
		HandClap,
		ElectricSnare,
		PowerLowTom2,
		ClosedHiHat,
		PowerLowTom1,
		PedalHiHat,
		PowerMidTom2,
		OpenHiHat,
		PowerMidTom1,
		PowerHiTom2,
		CrashCymbal1,
		PowerHiTom1,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralSoundRoomPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		BassDrum1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		RoomLowTom2,
		ClosedHiHat,
		RoomLowTom1,
		PedalHiHat,
		RoomMidTom2,
		OpenHiHat,
		RoomMidTom1,
		RoomHiTom2,
		CrashCymbal1,
		RoomHiTom1,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralSoundSfxPercussion : byte
	{
		HighQ = 39,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		GuitarFretNoise,
		GuitarCuttingNoiseUp,
		GuitarCuttingNoiseDown,
		StringSlapOfDoubleBass,
		FlKeyClick,
		Laughing,
		Scream,
		Punch,
		HeartBeat,
		Footsteps1,
		Footsteps2,
		Applause,
		DoorCreaking,
		Door,
		Scratch,
		WindChimes,
		CarEngine,
		CarStop,
		CarPass,
		CarCrash,
		Siren,
		Train,
		Jetplane,
		Helicopter,
		Starship,
		GunShot,
		MachineGun,
		Lasergun,
		Explosion,
		Dog,
		HorseGallop,
		Birds,
		Rain,
		Thunder,
		Wind,
		Seashore,
		Stream,
		Bubble
	}
	public enum GeneralSoundStandardPercussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		BassDrum1,
		SideStick,
		AcousticSnare,
		HandClap,
		ElectricSnare,
		LowFloorTom,
		ClosedHiHat,
		HighFloorTom,
		PedalHiHat,
		LowTom,
		OpenHiHat,
		LowMidTom,
		HiMidTom,
		CrashCymbal1,
		HighTom,
		RideCymbal1,
		ChineseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		MuteHiConga,
		OpenHiConga,
		LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
	public enum GeneralSoundTr808Percussion : byte
	{
		HighQ = 27,
		Slap,
		ScratchPush,
		ScratchPull,
		Sticks,
		SquareClick,
		MetronomeClick,
		MetronomeBell,
		AcousticBassDrum,
		Tr808BassDrum,
		Tr808RimShot,
		Tr808SnareDrum,
		HandClap,
		SnaeDrum2,
		Tr808LowTom2,
		Tr808ClosedHiHat,
		Tr808LowTom1,
		Tr808PedalHiHat,
		Tr808MidTom2,
		Tr808OpenHiHat,
		Tr808MidTom1,
		Tr808HiTom2,
		Tr808Cymbal,
		Tr808HiTom1,
		RideCymbal1,
		ReverseCymbal,
		RideBell,
		Tambourine,
		SplashCymbal,
		Tr808Cowbell,
		CrashCymbal2,
		Vibraslap,
		RideCymbal2,
		HiBongo,
		LowBongo,
		Tr808HiConga,
		Tr808MidConga,
		Tr808LowConga,
		HighTimbale,
		LowTimbale,
		HighAgogo,
		LowAgogo,
		Cabasa,
		Tr808Maracas,
		ShortWhistle,
		LongWhistle,
		ShortGuiro,
		LongGuiro,
		Tr808Claves,
		HiWoodBlock,
		LowWoodBlock,
		MuteCuica,
		OpenCuica,
		MuteTriangle,
		OpenTriangle,
		Shaker,
		JingleBell,
		Belltree,
		Castanets,
		MuteSurdo,
		OpenSurdo
	}
}
namespace Melanchall.DryWetMidi.MusicTheory
{
	public sealed class ChordProgression
	{
		public IEnumerable<Chord> Chords { get; }

		public ChordProgression(IEnumerable<Chord> chords)
		{
			ThrowIfArgument.IsNull("chords", chords);
			ThrowIfArgument.ContainsNull("chords", chords);
			Chords = chords;
		}

		public ChordProgression(params Chord[] chords)
			: this((IEnumerable<Chord>)chords)
		{
		}

		public static bool TryParse(string input, Scale scale, out ChordProgression chordProgression)
		{
			return ParsingUtilities.TryParse(input, GetParsing(input, scale), out chordProgression);
		}

		public static ChordProgression Parse(string input, Scale scale)
		{
			return ParsingUtilities.Parse(input, GetParsing(input, scale));
		}

		private static Parsing<ChordProgression> GetParsing(string input, Scale scale)
		{
			ChordProgression chordProgression;
			ParsingResult result = ChordProgressionParser.TryParse(input, scale, out chordProgression);
			return delegate(string i, out ChordProgression cp)
			{
				cp = chordProgression;
				return result;
			};
		}

		public static bool operator ==(ChordProgression chordProgression1, ChordProgression chordProgression2)
		{
			if ((object)chordProgression1 == chordProgression2)
			{
				return true;
			}
			if ((object)chordProgression1 == null || (object)chordProgression2 == null)
			{
				return false;
			}
			return chordProgression1.Chords.SequenceEqual(chordProgression2.Chords);
		}

		public static bool operator !=(ChordProgression chordProgression1, ChordProgression chordProgression2)
		{
			return !(chordProgression1 == chordProgression2);
		}

		public override string ToString()
		{
			return string.Join("; ", Chords);
		}

		public override bool Equals(object obj)
		{
			return this == obj as ChordProgression;
		}

		public override int GetHashCode()
		{
			int num = 17;
			foreach (Chord chord in Chords)
			{
				num = num * 23 + chord.GetHashCode();
			}
			return num;
		}
	}
	internal static class ChordProgressionParser
	{
		private const char PartsDelimiter = '-';

		private const string ScaleDegreeGroupName = "sd";

		private static readonly string ScaleDegreeGroup = "(?<sd>(?i:M{0,4}(CM|CD|D?C{0,3})(XC|XL|L?X{0,3})(IX|IV|V?I{0,3})))";

		private static readonly string[] Patterns = new string[1] { ScaleDegreeGroup + "\\s*" + ChordParser.ChordCharacteristicsGroup };

		private static Dictionary<char, int> RomanMap = new Dictionary<char, int>
		{
			['i'] = 1,
			['v'] = 5,
			['x'] = 10,
			['l'] = 50,
			['c'] = 100,
			['d'] = 500,
			['m'] = 1000
		};

		internal static ParsingResult TryParse(string input, Scale scale, out ChordProgression chordProgression)
		{
			chordProgression = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			string[] array = input.Split(new char[1] { '-' }, StringSplitOptions.RemoveEmptyEntries);
			List<Chord> list = new List<Chord>();
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				Match match = ParsingUtilities.Match(array2[i], Patterns, ignoreCase: false);
				if (match == null)
				{
					return ParsingResult.NotMatched;
				}
				Group obj = match.Groups["sd"];
				string text = obj.Value.ToLower();
				if (!string.IsNullOrWhiteSpace(text))
				{
					int num = RomanToInteger(text);
					NoteName step = scale.GetStep(num - 1);
					string value = match.Value;
					int index = match.Index;
					int index2 = obj.Index;
					Chord chord;
					ParsingResult parsingResult = ChordParser.TryParse(value.Substring(0, index2 - index) + step.ToString() + value.Substring(index2 - index + obj.Length), out chord);
					if (parsingResult.Status != ParsingStatus.Parsed)
					{
						return parsingResult;
					}
					list.Add(chord);
				}
			}
			chordProgression = new ChordProgression(list);
			return ParsingResult.Parsed;
		}

		private static int RomanToInteger(string roman)
		{
			int num = 0;
			for (int i = 0; i < roman.Length; i++)
			{
				num = ((i + 1 >= roman.Length || RomanMap[roman[i]] >= RomanMap[roman[i + 1]]) ? (num + RomanMap[roman[i]]) : (num - RomanMap[roman[i]]));
			}
			return num;
		}
	}
	public sealed class Chord
	{
		private static readonly Dictionary<ChordQuality, IntervalDefinition[]> IntervalsByQuality = new Dictionary<ChordQuality, IntervalDefinition[]>
		{
			[ChordQuality.Major] = new IntervalDefinition[2]
			{
				new IntervalDefinition(3, IntervalQuality.Major),
				new IntervalDefinition(5, IntervalQuality.Perfect)
			},
			[ChordQuality.Minor] = new IntervalDefinition[2]
			{
				new IntervalDefinition(3, IntervalQuality.Minor),
				new IntervalDefinition(5, IntervalQuality.Perfect)
			},
			[ChordQuality.Augmented] = new IntervalDefinition[2]
			{
				new IntervalDefinition(3, IntervalQuality.Major),
				new IntervalDefinition(5, IntervalQuality.Augmented)
			},
			[ChordQuality.Diminished] = new IntervalDefinition[2]
			{
				new IntervalDefinition(3, IntervalQuality.Minor),
				new IntervalDefinition(5, IntervalQuality.Diminished)
			}
		};

		private IReadOnlyCollection<string> _chordNames;

		public ICollection<NoteName> NotesNames { get; }

		public NoteName RootNoteName => NotesNames.First();

		public Chord(ICollection<NoteName> notesNames)
		{
			ThrowIfArgument.IsNull("notesNames", notesNames);
			ThrowIfArgument.ContainsInvalidEnumValue("notesNames", notesNames);
			ThrowIfArgument.IsEmptyCollection("notesNames", notesNames, "Notes names collection is empty.");
			NotesNames = notesNames;
		}

		public Chord(NoteName rootNoteName, params NoteName[] notesNamesAboveRoot)
			: this(new NoteName[1] { rootNoteName }.Concat(notesNamesAboveRoot ?? Enumerable.Empty<NoteName>()).ToArray())
		{
		}

		public Chord(NoteName rootNoteName, IEnumerable<Interval> intervalsFromRoot)
		{
			ThrowIfArgument.IsInvalidEnumValue("rootNoteName", rootNoteName);
			ThrowIfArgument.IsNull("intervalsFromRoot", intervalsFromRoot);
			NotesNames = (from i in new Interval[1] { Interval.Zero }.Concat(intervalsFromRoot)
				where i != null
				orderby i.HalfSteps
				select rootNoteName.Transpose(i)).ToArray();
		}

		public Chord(NoteName rootNoteName, params Interval[] intervalsFromRoot)
			: this(rootNoteName, (IEnumerable<Interval>)intervalsFromRoot)
		{
		}

		public IReadOnlyCollection<string> GetNames()
		{
			if (_chordNames != null)
			{
				return _chordNames;
			}
			IList<string> chordNames = ChordsNamesTable.GetChordNames(NotesNames.ToArray());
			return _chordNames = new ReadOnlyCollection<string>(chordNames);
		}

		public static bool TryParse(string input, out Chord chord)
		{
			return ParsingUtilities.TryParse(input, (Parsing<Chord>)ChordParser.TryParse, out chord);
		}

		public static Chord Parse(string input)
		{
			return ParsingUtilities.Parse<Chord>(input, ChordParser.TryParse);
		}

		public static Chord GetByTriad(NoteName rootNoteName, ChordQuality chordQuality, params Interval[] intervalsFromRoot)
		{
			ThrowIfArgument.IsInvalidEnumValue("rootNoteName", rootNoteName);
			ThrowIfArgument.IsInvalidEnumValue("chordQuality", chordQuality);
			ThrowIfArgument.IsNull("intervalsFromRoot", intervalsFromRoot);
			IntervalDefinition[] source = IntervalsByQuality[chordQuality];
			return new Chord(rootNoteName, source.Select((IntervalDefinition i) => Interval.FromDefinition(i)).Concat(intervalsFromRoot));
		}

		public static bool operator ==(Chord chord1, Chord chord2)
		{
			if ((object)chord1 == chord2)
			{
				return true;
			}
			if ((object)chord1 == null || (object)chord2 == null)
			{
				return false;
			}
			return chord1.NotesNames.SequenceEqual(chord2.NotesNames);
		}

		public static bool operator !=(Chord chord1, Chord chord2)
		{
			return !(chord1 == chord2);
		}

		public override string ToString()
		{
			return string.Join(" ", NotesNames.Select((NoteName n) => n.ToString().Replace("Sharp", "#")));
		}

		public override bool Equals(object obj)
		{
			return this == obj as Chord;
		}

		public override int GetHashCode()
		{
			int num = 17;
			foreach (NoteName notesName in NotesNames)
			{
				num = num * 23 + notesName.GetHashCode();
			}
			return num;
		}
	}
	internal static class ChordParser
	{
		private const string RootNoteNameGroupName = "rn";

		private const string BassNoteNameGroupName = "bn";

		private const string ChordCharacteristicsGroupName = "cc";

		public static readonly string ChordCharacteristicsGroup = "(?<cc>.*?)";

		private static readonly string RootNoteNameGroup = "(?<rn>" + string.Join("|", NoteNameParser.GetPatterns()) + ")";

		private static readonly string BassNoteNameGroup = "(?<bn>" + string.Join("|", NoteNameParser.GetPatterns()) + ")";

		private static readonly string[] Patterns = new string[1] { "(?i:" + RootNoteNameGroup + ")" + ChordCharacteristicsGroup + "((\\/(?i:" + BassNoteNameGroup + "))|$)" };

		private const string ChordCharacteristicIsUnknown = "Chord characteristic is unknown.";

		internal static ParsingResult TryParse(string input, out Chord chord)
		{
			chord = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns, ignoreCase: false);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			NoteName noteName;
			ParsingResult parsingResult = NoteNameParser.TryParse(match.Groups["rn"].Value, out noteName);
			if (parsingResult.Status != ParsingStatus.Parsed)
			{
				return parsingResult;
			}
			NoteName? bassNoteName = null;
			Group obj = match.Groups["bn"];
			if (obj.Success)
			{
				NoteName noteName2;
				ParsingResult parsingResult2 = NoteNameParser.TryParse(obj.Value, out noteName2);
				if (parsingResult2.Status != ParsingStatus.Parsed)
				{
					return parsingResult2;
				}
				bassNoteName = noteName2;
			}
			NoteName[] chordNotesNames = ChordsNamesTable.GetChordNotesNames(noteName, match.Groups["cc"].Value, bassNoteName);
			if (!chordNotesNames.Any())
			{
				return ParsingResult.Error("Chord characteristic is unknown.");
			}
			chord = new Chord(chordNotesNames);
			return ParsingResult.Parsed;
		}
	}
	public enum ChordQuality
	{
		Major,
		Minor,
		Augmented,
		Diminished
	}
	internal static class ChordsNamesTable
	{
		private sealed class NameDefinition
		{
			public int[][] Intervals { get; }

			public string[] Names { get; }

			public NameDefinition(int[][] intervals, params string[] names)
			{
				Intervals = intervals;
				Names = names;
			}
		}

		private static readonly NameDefinition[] NamesDefinitions = new NameDefinition[37]
		{
			new NameDefinition(new int[1][] { new int[3] { 0, 4, 7 } }, "maj", "M", string.Empty),
			new NameDefinition(new int[1][] { new int[3] { 0, 3, 7 } }, "min", "m"),
			new NameDefinition(new int[1][] { new int[3] { 0, 5, 7 } }, "sus4"),
			new NameDefinition(new int[1][] { new int[3] { 0, 2, 7 } }, "sus2"),
			new NameDefinition(new int[1][] { new int[3] { 0, 4, 6 } }, "b5"),
			new NameDefinition(new int[1][] { new int[3] { 0, 3, 6 } }, "dim"),
			new NameDefinition(new int[1][] { new int[3] { 0, 4, 8 } }, "aug"),
			new NameDefinition(new int[1][] { new int[4] { 0, 3, 7, 9 } }, "min6", "m6"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 7, 9 } }, "maj6", "M6", "6"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 7, 10 } }, "7"),
			new NameDefinition(new int[1][] { new int[4] { 0, 5, 7, 10 } }, "7sus4"),
			new NameDefinition(new int[1][] { new int[4] { 0, 2, 7, 10 } }, "7sus2"),
			new NameDefinition(new int[1][] { new int[4] { 0, 3, 7, 10 } }, "min7", "m7"),
			new NameDefinition(new int[4][]
			{
				new int[5] { 0, 3, 7, 10, 14 },
				new int[4] { 0, 3, 10, 14 },
				new int[3] { 3, 10, 14 },
				new int[4] { 3, 7, 10, 14 }
			}, "min9", "min7(9)", "m9", "m7(9)"),
			new NameDefinition(new int[4][]
			{
				new int[6] { 0, 3, 7, 10, 14, 17 },
				new int[5] { 0, 3, 10, 14, 17 },
				new int[4] { 3, 10, 14, 17 },
				new int[5] { 3, 7, 10, 14, 17 }
			}, "min11", "min7(9,11)", "m11", "m7(9,11)"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 7, 11 } }, "maj7"),
			new NameDefinition(new int[4][]
			{
				new int[5] { 0, 4, 7, 11, 14 },
				new int[4] { 0, 4, 11, 14 },
				new int[3] { 4, 11, 14 },
				new int[4] { 4, 7, 11, 14 }
			}, "maj7(9)", "M7(9)"),
			new NameDefinition(new int[4][]
			{
				new int[6] { 0, 4, 7, 11, 14, 18 },
				new int[5] { 0, 4, 11, 14, 18 },
				new int[4] { 4, 11, 14, 18 },
				new int[5] { 4, 7, 11, 14, 18 }
			}, "maj7(#11)", "M7(#11)"),
			new NameDefinition(new int[4][]
			{
				new int[5] { 0, 4, 7, 11, 21 },
				new int[4] { 0, 4, 11, 21 },
				new int[3] { 4, 11, 21 },
				new int[4] { 4, 7, 11, 21 }
			}, "maj7(13)", "M7(13)"),
			new NameDefinition(new int[4][]
			{
				new int[6] { 0, 4, 7, 11, 14, 21 },
				new int[5] { 0, 4, 11, 14, 21 },
				new int[4] { 4, 11, 14, 21 },
				new int[5] { 4, 7, 11, 14, 21 }
			}, "maj7(9,13)", "M7(9,13)"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 8, 11 } }, "maj7#5", "M7#5"),
			new NameDefinition(new int[2][]
			{
				new int[5] { 0, 4, 8, 11, 14 },
				new int[4] { 4, 8, 11, 14 }
			}, "maj7#5(9)", "M7#5(9)"),
			new NameDefinition(new int[1][] { new int[4] { 0, 3, 7, 11 } }, "minMaj7", "mM7"),
			new NameDefinition(new int[4][]
			{
				new int[5] { 0, 3, 7, 11, 14 },
				new int[4] { 0, 3, 11, 14 },
				new int[3] { 3, 11, 14 },
				new int[4] { 3, 7, 11, 14 }
			}, "minMaj7(9)", "mM7(9)"),
			new NameDefinition(new int[1][] { new int[2] { 0, 7 } }, "5"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 6, 10 } }, "7b5", "dom7dim5", "7dim5"),
			new NameDefinition(new int[1][] { new int[4] { 0, 3, 6, 10 } }, "ø", "ø7", "m7b5", "min7dim5", "m7dim5", "min7b5", "m7b5"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 8, 10 } }, "aug7"),
			new NameDefinition(new int[1][] { new int[4] { 0, 3, 6, 9 } }, "dim7"),
			new NameDefinition(new int[1][] { new int[4] { 0, 4, 7, 14 } }, "add9"),
			new NameDefinition(new int[1][] { new int[4] { 0, 3, 7, 14 } }, "minAdd9", "mAdd9"),
			new NameDefinition(new int[4][]
			{
				new int[5] { 0, 4, 7, 9, 14 },
				new int[4] { 4, 7, 9, 14 },
				new int[4] { 0, 4, 9, 14 },
				new int[3] { 4, 9, 14 }
			}, "maj6(9)", "6(9)", "6/9", "M6/9", "M6(9)"),
			new NameDefinition(new int[4][]
			{
				new int[5] { 0, 3, 7, 9, 14 },
				new int[4] { 3, 7, 9, 14 },
				new int[4] { 0, 3, 9, 14 },
				new int[3] { 3, 9, 14 }
			}, "min6(9)", "m6(9)", "m6/9", "min6/9"),
			new NameDefinition(new int[1][] { new int[5] { 0, 4, 7, 10, 14 } }, "9"),
			new NameDefinition(new int[1][] { new int[5] { 0, 2, 7, 10, 14 } }, "9sus2"),
			new NameDefinition(new int[1][] { new int[5] { 0, 5, 7, 10, 14 } }, "9sus4"),
			new NameDefinition(new int[1][] { new int[6] { 0, 4, 7, 10, 14, 17 } }, "11")
		}.OrderByDescending((NameDefinition d) => d.Intervals.First().Length).ToArray();

		public static NoteName[] GetChordNotesNames(NoteName rootNoteName, string chordCharacteristic, NoteName? bassNoteName)
		{
			List<NoteName> list = new List<NoteName>();
			if (bassNoteName.HasValue)
			{
				list.Add(bassNoteName.Value);
			}
			NameDefinition nameDefinition = NamesDefinitions.FirstOrDefault((NameDefinition d) => d.Names.Contains(chordCharacteristic.Replace(" ", string.Empty)));
			if (nameDefinition != null)
			{
				list.AddRange(from i in nameDefinition.Intervals.First()
					select rootNoteName.Transpose(Interval.FromHalfSteps(i)));
			}
			return list.ToArray();
		}

		public static IList<string> GetChordNames(NoteName[] notesNames)
		{
			List<string> list = new List<string>();
			if (!notesNames.Any())
			{
				return list;
			}
			HashSet<string> hashSet = new HashSet<string>();
			foreach (NoteName[] permutation in MathUtilities.GetPermutations(notesNames))
			{
				string item = new string(permutation.Select((NoteName n) => (char)n).ToArray());
				if (hashSet.Add(item))
				{
					list.AddRange(GetChordNamesByPermutation(permutation));
				}
			}
			return (from n in list.Distinct()
				orderby n.Length
				select n).ToArray();
		}

		private static IList<string> GetChordNamesByPermutation(NoteName[] notesNames)
		{
			List<string> list = new List<string>(GetChordNamesInternal(notesNames));
			NoteName firstNoteName = notesNames.First();
			list.AddRange(from n in GetChordNamesInternal(notesNames.Skip(1).ToArray())
				select n + "/" + firstNoteName.ToString().Replace("Sharp", "#"));
			return list;
		}

		private static List<string> GetChordNamesInternal(ICollection<NoteName> notesNames)
		{
			List<string> list = new List<string>();
			if (!notesNames.Any())
			{
				return list;
			}
			NoteName rootNoteName = notesNames.First();
			int[] array = (from i in ChordUtilities.GetIntervalsFromRootNote(notesNames)
				select i.HalfSteps).ToArray();
			NameDefinition[] namesDefinitions = NamesDefinitions;
			foreach (NameDefinition nameDefinition in namesDefinitions)
			{
				bool flag = false;
				int[][] intervals = nameDefinition.Intervals;
				foreach (int[] array2 in intervals)
				{
					int[] array3 = array;
					if (array2[0] != 0)
					{
						continue;
					}
					array3 = new int[1].Concat(array).ToArray();
					bool flag2 = array3.Length >= array2.Length;
					int num3 = 0;
					int num4 = 0;
					while (num4 < array2.Length && num4 < array3.Length && flag2)
					{
						int num5 = array2[num4];
						if (array3[num4] != num5 && !array3.Contains(num5 - 12) && !array3.Contains(num5 - 24))
						{
							flag2 = false;
						}
						num4++;
						num3++;
					}
					for (; num3 < array3.Length && flag2; num3++)
					{
						if (!array3.Contains(array3[num3] - 12) && !array3.Contains(array3[num3] - 24))
						{
							flag2 = false;
						}
					}
					flag |= flag2 && num3 >= array3.Length;
					if (flag)
					{
						break;
					}
				}
				if (flag)
				{
					list.AddRange(nameDefinition.Names.Select((string n) => rootNoteName.ToString().Replace("Sharp", "#") + n));
					break;
				}
			}
			return list;
		}
	}
	public static class ChordUtilities
	{
		public static IEnumerable<Interval> GetIntervalsFromRootNote(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return GetIntervalsFromRootNote(chord.NotesNames);
		}

		public static IEnumerable<Interval> GetIntervalsBetweenNotes(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return (from i in GetIntervals(chord)
				select Interval.FromHalfSteps((byte)i)).ToList();
		}

		public static Note ResolveRootNote(this Chord chord, Octave octave)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			return octave.GetNote(chord.RootNoteName);
		}

		public static IEnumerable<Note> ResolveNotes(this Chord chord, Octave octave)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			Note rootNote = chord.ResolveRootNote(octave);
			List<Note> list = new List<Note>();
			list.Add(rootNote);
			list.AddRange(from i in chord.GetIntervalsFromRootNote()
				select rootNote + i);
			return list;
		}

		public static IEnumerable<Chord> GetInversions(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			foreach (NoteName[] permutation in MathUtilities.GetPermutations(chord.NotesNames.ToArray()))
			{
				if (permutation[0] != chord.RootNoteName)
				{
					yield return new Chord(permutation.ToArray());
				}
			}
		}

		internal static IEnumerable<Interval> GetIntervalsFromRootNote(ICollection<NoteName> notesNames)
		{
			SevenBitNumber sevenBitNumber = SevenBitNumber.MinValue;
			List<Interval> list = new List<Interval>();
			foreach (SevenBitNumber interval in GetIntervals(notesNames))
			{
				if ((byte)sevenBitNumber + (byte)interval > (byte)SevenBitNumber.MaxValue)
				{
					throw new InvalidOperationException($"Some interval(s) are greater than {SevenBitNumber.MaxValue}.");
				}
				sevenBitNumber = (SevenBitNumber)(byte)((byte)sevenBitNumber + (byte)interval);
				list.Add(Interval.GetUp(sevenBitNumber));
			}
			return list;
		}

		private static IEnumerable<SevenBitNumber> GetIntervals(Chord chord)
		{
			return GetIntervals(chord.NotesNames);
		}

		private static IEnumerable<SevenBitNumber> GetIntervals(ICollection<NoteName> notesNames)
		{
			int num = (int)notesNames.First();
			foreach (NoteName noteName in notesNames.Skip(1))
			{
				int num2 = (int)(noteName - num);
				if (num2 <= 0)
				{
					num2 += 12;
				}
				yield return (SevenBitNumber)(byte)num2;
				num = (int)noteName;
			}
		}
	}
	public sealed class Interval : IComparable<Interval>
	{
		private static readonly Dictionary<SevenBitNumber, Dictionary<IntervalDirection, Interval>> Cache = new Dictionary<SevenBitNumber, Dictionary<IntervalDirection, Interval>>();

		private IReadOnlyCollection<IntervalDefinition> _intervalDefinitions;

		public static readonly Interval Zero = FromHalfSteps(0);

		public static readonly Interval One = FromHalfSteps(1);

		public static readonly Interval Two = FromHalfSteps(2);

		public static readonly Interval Three = FromHalfSteps(3);

		public static readonly Interval Four = FromHalfSteps(4);

		public static readonly Interval Five = FromHalfSteps(5);

		public static readonly Interval Six = FromHalfSteps(6);

		public static readonly Interval Seven = FromHalfSteps(7);

		public static readonly Interval Eight = FromHalfSteps(8);

		public static readonly Interval Nine = FromHalfSteps(9);

		public static readonly Interval Ten = FromHalfSteps(10);

		public static readonly Interval Eleven = FromHalfSteps(11);

		public static readonly Interval Twelve = FromHalfSteps(12);

		private static readonly Dictionary<IntervalQuality, Dictionary<int, int>> IntervalsHalfTones = new Dictionary<IntervalQuality, Dictionary<int, int>>
		{
			[IntervalQuality.Perfect] = new Dictionary<int, int>
			{
				[1] = 0,
				[4] = 5,
				[5] = 7,
				[8] = 12
			},
			[IntervalQuality.Minor] = new Dictionary<int, int>
			{
				[2] = 1,
				[3] = 3,
				[6] = 8,
				[7] = 10
			},
			[IntervalQuality.Major] = new Dictionary<int, int>
			{
				[2] = 2,
				[3] = 4,
				[6] = 9,
				[7] = 11
			},
			[IntervalQuality.Diminished] = new Dictionary<int, int>
			{
				[1] = -1,
				[2] = 0,
				[3] = 2,
				[4] = 4,
				[5] = 6,
				[6] = 7,
				[7] = 9,
				[8] = 11
			},
			[IntervalQuality.Augmented] = new Dictionary<int, int>
			{
				[1] = 1,
				[2] = 3,
				[3] = 5,
				[4] = 6,
				[5] = 8,
				[6] = 10,
				[7] = 12
			}
		};

		private static readonly IntervalQuality?[] QualitiesPattern = new IntervalQuality?[12]
		{
			IntervalQuality.Perfect,
			IntervalQuality.Minor,
			IntervalQuality.Major,
			IntervalQuality.Minor,
			IntervalQuality.Major,
			IntervalQuality.Perfect,
			null,
			IntervalQuality.Perfect,
			IntervalQuality.Minor,
			IntervalQuality.Major,
			IntervalQuality.Minor,
			IntervalQuality.Major
		};

		private static readonly Dictionary<int, IntervalQuality> AdditionalQualitiesPattern = new Dictionary<int, IntervalQuality>
		{
			[1] = IntervalQuality.Augmented,
			[4] = IntervalQuality.Augmented,
			[5] = IntervalQuality.Diminished
		};

		private static readonly int[] IntervalNumbersOffsets = new int[12]
		{
			1, 2, 2, 3, 3, 4, 5, 5, 6, 6,
			7, 7
		};

		public SevenBitNumber Size { get; }

		public IntervalDirection Direction { get; }

		public int HalfSteps
		{
			get
			{
				if (Direction != IntervalDirection.Up)
				{
					return -(byte)Size;
				}
				return (byte)Size;
			}
		}

		private Interval(SevenBitNumber size, IntervalDirection direction)
		{
			Size = size;
			Direction = direction;
		}

		public Interval Up()
		{
			return Get(Size, IntervalDirection.Up);
		}

		public Interval Down()
		{
			return Get(Size, IntervalDirection.Down);
		}

		public IReadOnlyCollection<IntervalDefinition> GetIntervalDefinitions()
		{
			if (_intervalDefinitions != null)
			{
				return _intervalDefinitions;
			}
			List<IntervalDefinition> list = new List<IntervalDefinition>();
			IntervalQuality? intervalQuality = QualitiesPattern[(byte)Size % 12];
			int num = 7 * ((byte)Size / 12) + IntervalNumbersOffsets[(byte)Size % 12];
			if (intervalQuality.HasValue)
			{
				list.Add(new IntervalDefinition(num, intervalQuality.Value));
				IntervalQuality intervalQuality2 = IntervalQuality.Augmented;
				switch (intervalQuality.Value)
				{
				case IntervalQuality.Perfect:
					intervalQuality2 = ((num != 1) ? AdditionalQualitiesPattern[num % 7] : IntervalQuality.Diminished);
					if (num % 7 == 1)
					{
						if (num > 1)
						{
							list.Add(new IntervalDefinition(num - 1, IntervalQuality.Augmented));
						}
						list.Add(new IntervalDefinition(num + 1, IntervalQuality.Diminished));
						return _intervalDefinitions = new ReadOnlyCollection<IntervalDefinition>(list);
					}
					break;
				case IntervalQuality.Minor:
					intervalQuality2 = IntervalQuality.Augmented;
					break;
				case IntervalQuality.Major:
					intervalQuality2 = IntervalQuality.Diminished;
					break;
				}
				switch (intervalQuality2)
				{
				case IntervalQuality.Augmented:
					list.Add(new IntervalDefinition(num - 1, IntervalQuality.Augmented));
					break;
				case IntervalQuality.Diminished:
					list.Add(new IntervalDefinition(num + 1, IntervalQuality.Diminished));
					break;
				}
			}
			else
			{
				list.Add(new IntervalDefinition(num, IntervalQuality.Diminished));
				list.Add(new IntervalDefinition(num - 1, IntervalQuality.Augmented));
			}
			return _intervalDefinitions = new ReadOnlyCollection<IntervalDefinition>(list);
		}

		public static bool IsPerfect(int intervalNumber)
		{
			ThrowIfArgument.IsLessThan("intervalNumber", intervalNumber, 1, "Interval number is less than 1.");
			int num = intervalNumber % 7 - 1;
			if (num != 0 && num != 3)
			{
				return num == 4;
			}
			return true;
		}

		public static bool IsQualityApplicable(IntervalQuality intervalQuality, int intervalNumber)
		{
			ThrowIfArgument.IsInvalidEnumValue("intervalQuality", intervalQuality);
			ThrowIfArgument.IsLessThan("intervalNumber", intervalNumber, 1, "Interval number is less than 1.");
			switch (intervalQuality)
			{
			case IntervalQuality.Perfect:
				return IsPerfect(intervalNumber);
			case IntervalQuality.Major:
			case IntervalQuality.Minor:
				return !IsPerfect(intervalNumber);
			case IntervalQuality.Diminished:
				return intervalNumber >= 2;
			case IntervalQuality.Augmented:
				return true;
			default:
				return false;
			}
		}

		public static Interval Get(IntervalQuality intervalQuality, int intervalNumber)
		{
			ThrowIfArgument.IsInvalidEnumValue("intervalQuality", intervalQuality);
			ThrowIfArgument.IsLessThan("intervalNumber", intervalNumber, 1, "Interval number is less than 1.");
			if (!IsQualityApplicable(intervalQuality, intervalNumber))
			{
				throw new ArgumentException($"{intervalQuality} quality is not applicable to interval number of {intervalNumber}.", "intervalQuality");
			}
			int num = 8;
			if (intervalQuality == IntervalQuality.Minor || intervalQuality == IntervalQuality.Major || intervalQuality == IntervalQuality.Augmented)
			{
				num = 7;
			}
			int num2 = ((intervalNumber > num) ? ((intervalNumber - 1) / 7 * 12) : 0);
			int key = intervalNumber;
			if (intervalNumber > num)
			{
				key = (intervalNumber - 1) % 7 + 1;
			}
			Dictionary<int, int> dictionary = IntervalsHalfTones[intervalQuality];
			return FromHalfSteps(num2 + dictionary[key]);
		}

		public static Interval Get(SevenBitNumber intervalSize, IntervalDirection direction)
		{
			ThrowIfArgument.IsInvalidEnumValue("direction", direction);
			if (!Cache.TryGetValue(intervalSize, out var value))
			{
				Cache.Add(intervalSize, value = new Dictionary<IntervalDirection, Interval>());
			}
			if (!value.TryGetValue(direction, out var value2))
			{
				value.Add(direction, value2 = new Interval(intervalSize, direction));
			}
			return value2;
		}

		public static Interval GetUp(SevenBitNumber intervalSize)
		{
			return Get(intervalSize, IntervalDirection.Up);
		}

		public static Interval GetDown(SevenBitNumber intervalSize)
		{
			return Get(intervalSize, IntervalDirection.Down);
		}

		public static Interval FromHalfSteps(int halfSteps)
		{
			ThrowIfArgument.IsOutOfRange("halfSteps", halfSteps, -(byte)SevenBitNumber.MaxValue, (byte)SevenBitNumber.MaxValue, "Half steps number is out of range.");
			return Get((SevenBitNumber)(byte)Math.Abs(halfSteps), (Math.Sign(halfSteps) < 0) ? IntervalDirection.Down : IntervalDirection.Up);
		}

		public static Interval FromDefinition(IntervalDefinition intervalDefinition)
		{
			ThrowIfArgument.IsNull("intervalDefinition", intervalDefinition);
			return Get(intervalDefinition.Quality, intervalDefinition.Number);
		}

		public static bool TryParse(string input, out Interval interval)
		{
			return ParsingUtilities.TryParse(input, (Parsing<Interval>)IntervalParser.TryParse, out interval);
		}

		public static Interval Parse(string input)
		{
			return ParsingUtilities.Parse<Interval>(input, IntervalParser.TryParse);
		}

		public static implicit operator int(Interval interval)
		{
			return interval.HalfSteps;
		}

		public static implicit operator Interval(SevenBitNumber interval)
		{
			return GetUp(interval);
		}

		public static bool operator ==(Interval interval1, Interval interval2)
		{
			if ((object)interval1 == interval2)
			{
				return true;
			}
			if ((object)interval1 == null || (object)interval2 == null)
			{
				return false;
			}
			return interval1.HalfSteps == interval2.HalfSteps;
		}

		public static bool operator !=(Interval interval1, Interval interval2)
		{
			return !(interval1 == interval2);
		}

		public static Interval operator +(Interval interval, int halfSteps)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return FromHalfSteps(interval.HalfSteps + halfSteps);
		}

		public static Interval operator -(Interval interval, int halfSteps)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return FromHalfSteps(interval.HalfSteps - halfSteps);
		}

		public static Interval operator *(Interval interval, int multiplier)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return FromHalfSteps(interval.HalfSteps * multiplier);
		}

		public static Interval operator /(Interval interval, int divisor)
		{
			ThrowIfArgument.IsNull("interval", interval);
			if (divisor == 0)
			{
				throw new ArgumentOutOfRangeException("divisor", divisor, "Divisor is zero.");
			}
			return FromHalfSteps(interval.HalfSteps / divisor);
		}

		public static Interval operator +(Interval interval)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return interval.Up();
		}

		public static Interval operator -(Interval interval)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return interval.Down();
		}

		public int CompareTo(Interval other)
		{
			return HalfSteps.CompareTo(other.HalfSteps);
		}

		public override string ToString()
		{
			return string.Format("{0}{1}", (Direction == IntervalDirection.Up) ? "+" : "-", Size);
		}

		public override bool Equals(object obj)
		{
			return this == obj as Interval;
		}

		public override int GetHashCode()
		{
			return HalfSteps.GetHashCode();
		}
	}
	public sealed class IntervalDefinition
	{
		private static readonly Dictionary<IntervalQuality, char> QualitiesSymbols = new Dictionary<IntervalQuality, char>
		{
			[IntervalQuality.Perfect] = 'P',
			[IntervalQuality.Minor] = 'm',
			[IntervalQuality.Major] = 'M',
			[IntervalQuality.Augmented] = 'A',
			[IntervalQuality.Diminished] = 'd'
		};

		public int Number { get; }

		public IntervalQuality Quality { get; }

		public IntervalDefinition(int number, IntervalQuality quality)
		{
			ThrowIfArgument.IsLessThan("number", number, 1, "Interval number is less than 1.");
			ThrowIfArgument.IsInvalidEnumValue("quality", quality);
			Number = number;
			Quality = quality;
		}

		public static bool operator ==(IntervalDefinition intervalDefinition1, IntervalDefinition intervalDefinition2)
		{
			if ((object)intervalDefinition1 == intervalDefinition2)
			{
				return true;
			}
			if ((object)intervalDefinition1 == null || (object)intervalDefinition2 == null)
			{
				return false;
			}
			if (intervalDefinition1.Number == intervalDefinition2.Number)
			{
				return intervalDefinition1.Quality == intervalDefinition2.Quality;
			}
			return false;
		}

		public static bool operator !=(IntervalDefinition intervalDefinition1, IntervalDefinition intervalDefinition2)
		{
			return !(intervalDefinition1 == intervalDefinition2);
		}

		public override string ToString()
		{
			return $"{QualitiesSymbols[Quality]}{Number}";
		}

		public override bool Equals(object obj)
		{
			return this == obj as IntervalDefinition;
		}

		public override int GetHashCode()
		{
			return (17 * 23 + Number.GetHashCode()) * 23 + Quality.GetHashCode();
		}
	}
	public enum IntervalDirection
	{
		Up,
		Down
	}
	internal static class IntervalParser
	{
		private const string HalfStepsGroupName = "hs";

		private const string IntervalQualityGroupName = "q";

		private const string IntervalNumberGroupName = "n";

		private static readonly string HalfStepsGroup = ParsingUtilities.GetIntegerNumberGroup("hs");

		private static readonly string IntervalGroup = "(?<q>P|p|M|m|D|d|A|a)(?<n>\\d+)";

		private static readonly string[] Patterns = new string[2] { IntervalGroup, HalfStepsGroup };

		private static readonly Dictionary<string, IntervalQuality> IntervalQualitiesByLetters = new Dictionary<string, IntervalQuality>
		{
			["P"] = IntervalQuality.Perfect,
			["p"] = IntervalQuality.Perfect,
			["M"] = IntervalQuality.Major,
			["m"] = IntervalQuality.Minor,
			["D"] = IntervalQuality.Diminished,
			["d"] = IntervalQuality.Diminished,
			["A"] = IntervalQuality.Augmented,
			["a"] = IntervalQuality.Augmented
		};

		private const string HalfStepsNumberIsOutOfRange = "Interval's half steps number is out of range.";

		private const string IntervalNumberIsOutOfRange = "Interval's number is out of range.";

		internal static IEnumerable<string> GetPatterns()
		{
			return Patterns;
		}

		internal static ParsingResult TryParse(string input, out Interval interval)
		{
			interval = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns, ignoreCase: false);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			Group obj = match.Groups["q"];
			if (!obj.Success)
			{
				if (!ParsingUtilities.ParseInt(match, "hs", 0, out var value) || !IntervalUtilities.IsIntervalValid(value))
				{
					return ParsingResult.Error("Interval's half steps number is out of range.");
				}
				interval = Interval.FromHalfSteps(value);
				return ParsingResult.Parsed;
			}
			IntervalQuality intervalQuality = IntervalQualitiesByLetters[obj.Value];
			if (!ParsingUtilities.ParseInt(match, "n", 0, out var value2) || value2 < 1)
			{
				return ParsingResult.Error("Interval's number is out of range.");
			}
			interval = Interval.Get(intervalQuality, value2);
			return ParsingResult.Parsed;
		}
	}
	public enum IntervalQuality
	{
		Perfect,
		Major,
		Minor,
		Augmented,
		Diminished
	}
	internal static class IntervalUtilities
	{
		internal static bool IsIntervalValid(int halfSteps)
		{
			if (halfSteps >= -(byte)SevenBitNumber.MaxValue)
			{
				return halfSteps <= (byte)SevenBitNumber.MaxValue;
			}
			return false;
		}
	}
	public static class Notes
	{
		public static readonly Note CMinus1 = Note.Get((SevenBitNumber)0);

		public static readonly Note CSharpMinus1 = Note.Get((SevenBitNumber)1);

		public static readonly Note DMinus1 = Note.Get((SevenBitNumber)2);

		public static readonly Note DSharpMinus1 = Note.Get((SevenBitNumber)3);

		public static readonly Note EMinus1 = Note.Get((SevenBitNumber)4);

		public static readonly Note FMinus1 = Note.Get((SevenBitNumber)5);

		public static readonly Note FSharpMinus1 = Note.Get((SevenBitNumber)6);

		public static readonly Note GMinus1 = Note.Get((SevenBitNumber)7);

		public static readonly Note GSharpMinus1 = Note.Get((SevenBitNumber)8);

		public static readonly Note AMinus1 = Note.Get((SevenBitNumber)9);

		public static readonly Note ASharpMinus1 = Note.Get((SevenBitNumber)10);

		public static readonly Note BMinus1 = Note.Get((SevenBitNumber)11);

		public static readonly Note C0 = Note.Get((SevenBitNumber)12);

		public static readonly Note CSharp0 = Note.Get((SevenBitNumber)13);

		public static readonly Note D0 = Note.Get((SevenBitNumber)14);

		public static readonly Note DSharp0 = Note.Get((SevenBitNumber)15);

		public static readonly Note E0 = Note.Get((SevenBitNumber)16);

		public static readonly Note F0 = Note.Get((SevenBitNumber)17);

		public static readonly Note FSharp0 = Note.Get((SevenBitNumber)18);

		public static readonly Note G0 = Note.Get((SevenBitNumber)19);

		public static readonly Note GSharp0 = Note.Get((SevenBitNumber)20);

		public static readonly Note A0 = Note.Get((SevenBitNumber)21);

		public static readonly Note ASharp0 = Note.Get((SevenBitNumber)22);

		public static readonly Note B0 = Note.Get((SevenBitNumber)23);

		public static readonly Note C1 = Note.Get((SevenBitNumber)24);

		public static readonly Note CSharp1 = Note.Get((SevenBitNumber)25);

		public static readonly Note D1 = Note.Get((SevenBitNumber)26);

		public static readonly Note DSharp1 = Note.Get((SevenBitNumber)27);

		public static readonly Note E1 = Note.Get((SevenBitNumber)28);

		public static readonly Note F1 = Note.Get((SevenBitNumber)29);

		public static readonly Note FSharp1 = Note.Get((SevenBitNumber)30);

		public static readonly Note G1 = Note.Get((SevenBitNumber)31);

		public static readonly Note GSharp1 = Note.Get((SevenBitNumber)32);

		public static readonly Note A1 = Note.Get((SevenBitNumber)33);

		public static readonly Note ASharp1 = Note.Get((SevenBitNumber)34);

		public static readonly Note B1 = Note.Get((SevenBitNumber)35);

		public static readonly Note C2 = Note.Get((SevenBitNumber)36);

		public static readonly Note CSharp2 = Note.Get((SevenBitNumber)37);

		public static readonly Note D2 = Note.Get((SevenBitNumber)38);

		public static readonly Note DSharp2 = Note.Get((SevenBitNumber)39);

		public static readonly Note E2 = Note.Get((SevenBitNumber)40);

		public static readonly Note F2 = Note.Get((SevenBitNumber)41);

		public static readonly Note FSharp2 = Note.Get((SevenBitNumber)42);

		public static readonly Note G2 = Note.Get((SevenBitNumber)43);

		public static readonly Note GSharp2 = Note.Get((SevenBitNumber)44);

		public static readonly Note A2 = Note.Get((SevenBitNumber)45);

		public static readonly Note ASharp2 = Note.Get((SevenBitNumber)46);

		public static readonly Note B2 = Note.Get((SevenBitNumber)47);

		public static readonly Note C3 = Note.Get((SevenBitNumber)48);

		public static readonly Note CSharp3 = Note.Get((SevenBitNumber)49);

		public static readonly Note D3 = Note.Get((SevenBitNumber)50);

		public static readonly Note DSharp3 = Note.Get((SevenBitNumber)51);

		public static readonly Note E3 = Note.Get((SevenBitNumber)52);

		public static readonly Note F3 = Note.Get((SevenBitNumber)53);

		public static readonly Note FSharp3 = Note.Get((SevenBitNumber)54);

		public static readonly Note G3 = Note.Get((SevenBitNumber)55);

		public static readonly Note GSharp3 = Note.Get((SevenBitNumber)56);

		public static readonly Note A3 = Note.Get((SevenBitNumber)57);

		public static readonly Note ASharp3 = Note.Get((SevenBitNumber)58);

		public static readonly Note B3 = Note.Get((SevenBitNumber)59);

		public static readonly Note C4 = Note.Get((SevenBitNumber)60);

		public static readonly Note CSharp4 = Note.Get((SevenBitNumber)61);

		public static readonly Note D4 = Note.Get((SevenBitNumber)62);

		public static readonly Note DSharp4 = Note.Get((SevenBitNumber)63);

		public static readonly Note E4 = Note.Get((SevenBitNumber)64);

		public static readonly Note F4 = Note.Get((SevenBitNumber)65);

		public static readonly Note FSharp4 = Note.Get((SevenBitNumber)66);

		public static readonly Note G4 = Note.Get((SevenBitNumber)67);

		public static readonly Note GSharp4 = Note.Get((SevenBitNumber)68);

		public static readonly Note A4 = Note.Get((SevenBitNumber)69);

		public static readonly Note ASharp4 = Note.Get((SevenBitNumber)70);

		public static readonly Note B4 = Note.Get((SevenBitNumber)71);

		public static readonly Note C5 = Note.Get((SevenBitNumber)72);

		public static readonly Note CSharp5 = Note.Get((SevenBitNumber)73);

		public static readonly Note D5 = Note.Get((SevenBitNumber)74);

		public static readonly Note DSharp5 = Note.Get((SevenBitNumber)75);

		public static readonly Note E5 = Note.Get((SevenBitNumber)76);

		public static readonly Note F5 = Note.Get((SevenBitNumber)77);

		public static readonly Note FSharp5 = Note.Get((SevenBitNumber)78);

		public static readonly Note G5 = Note.Get((SevenBitNumber)79);

		public static readonly Note GSharp5 = Note.Get((SevenBitNumber)80);

		public static readonly Note A5 = Note.Get((SevenBitNumber)81);

		public static readonly Note ASharp5 = Note.Get((SevenBitNumber)82);

		public static readonly Note B5 = Note.Get((SevenBitNumber)83);

		public static readonly Note C6 = Note.Get((SevenBitNumber)84);

		public static readonly Note CSharp6 = Note.Get((SevenBitNumber)85);

		public static readonly Note D6 = Note.Get((SevenBitNumber)86);

		public static readonly Note DSharp6 = Note.Get((SevenBitNumber)87);

		public static readonly Note E6 = Note.Get((SevenBitNumber)88);

		public static readonly Note F6 = Note.Get((SevenBitNumber)89);

		public static readonly Note FSharp6 = Note.Get((SevenBitNumber)90);

		public static readonly Note G6 = Note.Get((SevenBitNumber)91);

		public static readonly Note GSharp6 = Note.Get((SevenBitNumber)92);

		public static readonly Note A6 = Note.Get((SevenBitNumber)93);

		public static readonly Note ASharp6 = Note.Get((SevenBitNumber)94);

		public static readonly Note B6 = Note.Get((SevenBitNumber)95);

		public static readonly Note C7 = Note.Get((SevenBitNumber)96);

		public static readonly Note CSharp7 = Note.Get((SevenBitNumber)97);

		public static readonly Note D7 = Note.Get((SevenBitNumber)98);

		public static readonly Note DSharp7 = Note.Get((SevenBitNumber)99);

		public static readonly Note E7 = Note.Get((SevenBitNumber)100);

		public static readonly Note F7 = Note.Get((SevenBitNumber)101);

		public static readonly Note FSharp7 = Note.Get((SevenBitNumber)102);

		public static readonly Note G7 = Note.Get((SevenBitNumber)103);

		public static readonly Note GSharp7 = Note.Get((SevenBitNumber)104);

		public static readonly Note A7 = Note.Get((SevenBitNumber)105);

		public static readonly Note ASharp7 = Note.Get((SevenBitNumber)106);

		public static readonly Note B7 = Note.Get((SevenBitNumber)107);

		public static readonly Note C8 = Note.Get((SevenBitNumber)108);

		public static readonly Note CSharp8 = Note.Get((SevenBitNumber)109);

		public static readonly Note D8 = Note.Get((SevenBitNumber)110);

		public static readonly Note DSharp8 = Note.Get((SevenBitNumber)111);

		public static readonly Note E8 = Note.Get((SevenBitNumber)112);

		public static readonly Note F8 = Note.Get((SevenBitNumber)113);

		public static readonly Note FSharp8 = Note.Get((SevenBitNumber)114);

		public static readonly Note G8 = Note.Get((SevenBitNumber)115);

		public static readonly Note GSharp8 = Note.Get((SevenBitNumber)116);

		public static readonly Note A8 = Note.Get((SevenBitNumber)117);

		public static readonly Note ASharp8 = Note.Get((SevenBitNumber)118);

		public static readonly Note B8 = Note.Get((SevenBitNumber)119);

		public static readonly Note C9 = Note.Get((SevenBitNumber)120);

		public static readonly Note CSharp9 = Note.Get((SevenBitNumber)121);

		public static readonly Note D9 = Note.Get((SevenBitNumber)122);

		public static readonly Note DSharp9 = Note.Get((SevenBitNumber)123);

		public static readonly Note E9 = Note.Get((SevenBitNumber)124);

		public static readonly Note F9 = Note.Get((SevenBitNumber)125);

		public static readonly Note FSharp9 = Note.Get((SevenBitNumber)126);

		public static readonly Note G9 = Note.Get((SevenBitNumber)127);
	}
	public sealed class Note : IComparable<Note>
	{
		internal const string SharpLongString = "Sharp";

		internal const string SharpShortString = "#";

		internal const string FlatLongString = "Flat";

		internal const string FlatShortString = "b";

		private static readonly ConcurrentDictionary<SevenBitNumber, Note> Cache = new ConcurrentDictionary<SevenBitNumber, Note>();

		public SevenBitNumber NoteNumber { get; }

		public NoteName NoteName => NoteUtilities.GetNoteName(NoteNumber);

		public int Octave => NoteUtilities.GetNoteOctave(NoteNumber);

		private Note(SevenBitNumber noteNumber)
		{
			NoteNumber = noteNumber;
		}

		public Note Transpose(Interval interval)
		{
			return Get((SevenBitNumber)(byte)((byte)NoteNumber + interval.HalfSteps));
		}

		public static Note Get(SevenBitNumber noteNumber)
		{
			if (!Cache.TryGetValue(noteNumber, out var value))
			{
				Cache.TryAdd(noteNumber, value = new Note(noteNumber));
			}
			return value;
		}

		public static Note Get(NoteName noteName, int octave)
		{
			return Get(NoteUtilities.GetNoteNumber(noteName, octave));
		}

		public static bool TryParse(string input, out Note note)
		{
			return ParsingUtilities.TryParse(input, (Parsing<Note>)NoteParser.TryParse, out note);
		}

		public static Note Parse(string input)
		{
			return ParsingUtilities.Parse<Note>(input, NoteParser.TryParse);
		}

		public static bool operator ==(Note note1, Note note2)
		{
			if ((object)note1 == note2)
			{
				return true;
			}
			if ((object)note1 == null || (object)note2 == null)
			{
				return false;
			}
			return (byte)note1.NoteNumber == (byte)note2.NoteNumber;
		}

		public static bool operator !=(Note note1, Note note2)
		{
			return !(note1 == note2);
		}

		public static Note operator +(Note note, int halfSteps)
		{
			ThrowIfArgument.IsNull("note", note);
			return note.Transpose(Interval.FromHalfSteps(halfSteps));
		}

		public static Note operator -(Note note, int halfSteps)
		{
			return note + -halfSteps;
		}

		public int CompareTo(Note other)
		{
			return NoteNumber.CompareTo(other.NoteNumber);
		}

		public override string ToString()
		{
			return string.Format("{0}{1}", NoteName.ToString().Replace("Sharp", "#"), Octave);
		}

		public override bool Equals(object obj)
		{
			return this == obj as Note;
		}

		public override int GetHashCode()
		{
			return NoteNumber.GetHashCode();
		}
	}
	public enum NoteName
	{
		C,
		CSharp,
		D,
		DSharp,
		E,
		F,
		FSharp,
		G,
		GSharp,
		A,
		ASharp,
		B
	}
	internal static class NoteNameParser
	{
		private const string NoteLetterGroupName = "n";

		private const string AccidentalGroupName = "a";

		private static readonly string NoteNameGroup = "(?<n>C|D|E|F|G|A|B)";

		private static readonly string AccidentalGroup = "((?<a>" + Regex.Escape("#") + "|Sharp|b|Flat)\\s*)+?";

		private static readonly string[] Patterns = new string[2]
		{
			NoteNameGroup + "\\s*" + AccidentalGroup,
			NoteNameGroup ?? ""
		};

		internal static IEnumerable<string> GetPatterns()
		{
			return Patterns;
		}

		internal static ParsingResult TryParse(string input, out NoteName noteName)
		{
			noteName = NoteName.C;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			Group obj = match.Groups["n"];
			int num = (int)(NoteName)Enum.Parse(typeof(NoteName), obj.Value);
			Group obj2 = match.Groups["a"];
			if (obj2.Success)
			{
				foreach (Capture capture in obj2.Captures)
				{
					string value = capture.Value;
					if (string.Equals(value, "#", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "Sharp", StringComparison.OrdinalIgnoreCase))
					{
						num++;
					}
					else if (string.Equals(value, "b", StringComparison.OrdinalIgnoreCase) || string.Equals(value, "Flat", StringComparison.OrdinalIgnoreCase))
					{
						num--;
					}
				}
			}
			num %= 12;
			if (num < 0)
			{
				num = 12 + num;
			}
			noteName = (NoteName)num;
			return ParsingResult.Parsed;
		}
	}
	internal static class NoteParser
	{
		private const string NoteNameGroupName = "n";

		private const string OctaveGroupName = "o";

		private static readonly string OctaveGroup = ParsingUtilities.GetIntegerNumberGroup("o");

		private static readonly string[] Patterns = (from p in NoteNameParser.GetPatterns()
			select "(?<n>" + p + ")\\s*" + OctaveGroup).ToArray();

		private const string OctaveIsOutOfRange = "Octave number is out of range.";

		private const string NoteIsOutOfRange = "Note is out of range.";

		internal static ParsingResult TryParse(string input, out Note note)
		{
			note = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			NoteName noteName;
			ParsingResult parsingResult = NoteNameParser.TryParse(match.Groups["n"].Value, out noteName);
			if (parsingResult.Status != ParsingStatus.Parsed)
			{
				return parsingResult;
			}
			if (!ParsingUtilities.ParseInt(match, "o", Octave.Middle.Number, out var value))
			{
				return ParsingResult.Error("Octave number is out of range.");
			}
			if (!NoteUtilities.IsNoteValid(noteName, value))
			{
				return ParsingResult.Error("Note is out of range.");
			}
			note = Note.Get(noteName, value);
			return ParsingResult.Parsed;
		}
	}
	public static class NoteUtilities
	{
		private const int OctaveOffset = 1;

		public static NoteName Transpose(this NoteName noteName, Interval interval)
		{
			ThrowIfArgument.IsInvalidEnumValue("noteName", noteName);
			ThrowIfArgument.IsNull("interval", interval);
			int num = (int)(noteName + (int)interval) % 12;
			if (num < 0)
			{
				num += 12;
			}
			return (NoteName)num;
		}

		public static NoteName GetNoteName(SevenBitNumber noteNumber)
		{
			return (NoteName)((byte)noteNumber % 12);
		}

		public static int GetNoteOctave(SevenBitNumber noteNumber)
		{
			return (byte)noteNumber / 12 - 1;
		}

		public static SevenBitNumber GetNoteNumber(NoteName noteName, int octave)
		{
			ThrowIfArgument.IsInvalidEnumValue("noteName", noteName);
			int num = CalculateNoteNumber(noteName, octave);
			if (!IsNoteNumberValid(num))
			{
				throw new ArgumentException("Note number is out of range for the specified note name and octave.", "octave");
			}
			return (SevenBitNumber)(byte)num;
		}

		internal static bool IsNoteValid(NoteName noteName, int octave)
		{
			return IsNoteNumberValid(CalculateNoteNumber(noteName, octave));
		}

		internal static bool IsNoteNumberValid(int noteNumber)
		{
			if (noteNumber >= (byte)SevenBitNumber.MinValue)
			{
				return noteNumber <= (byte)SevenBitNumber.MaxValue;
			}
			return false;
		}

		private static int CalculateNoteNumber(NoteName noteName, int octave)
		{
			return (int)((octave + 1) * 12 + noteName);
		}
	}
	public sealed class Octave
	{
		private static readonly ConcurrentDictionary<int, Octave> Cache = new ConcurrentDictionary<int, Octave>();

		private readonly Dictionary<NoteName, Note> _notes;

		public const int OctaveSize = 12;

		public static readonly int MinOctaveNumber = NoteUtilities.GetNoteOctave(SevenBitNumber.MinValue);

		public static readonly int MaxOctaveNumber = NoteUtilities.GetNoteOctave(SevenBitNumber.MaxValue);

		public static readonly Octave Middle = Get(4);

		public int Number { get; }

		public Note C => GetNote(NoteName.C);

		public Note CSharp => GetNote(NoteName.CSharp);

		public Note D => GetNote(NoteName.D);

		public Note DSharp => GetNote(NoteName.DSharp);

		public Note E => GetNote(NoteName.E);

		public Note F => GetNote(NoteName.F);

		public Note FSharp => GetNote(NoteName.FSharp);

		public Note G => GetNote(NoteName.G);

		public Note GSharp => GetNote(NoteName.GSharp);

		public Note A => GetNote(NoteName.A);

		public Note ASharp => GetNote(NoteName.ASharp);

		public Note B => GetNote(NoteName.B);

		private Octave(int octave)
		{
			Number = octave;
			_notes = (from NoteName n in Enum.GetValues(typeof(NoteName))
				where NoteUtilities.IsNoteValid(n, octave)
				select n).ToDictionary((NoteName n) => n, (NoteName n) => Note.Get(n, octave));
		}

		public Note GetNote(NoteName noteName)
		{
			ThrowIfArgument.IsInvalidEnumValue("noteName", noteName);
			if (!_notes.TryGetValue(noteName, out var value))
			{
				throw new InvalidOperationException($"Unable to get the {noteName} note.");
			}
			return value;
		}

		public static Octave Get(int octaveNumber)
		{
			ThrowIfArgument.IsOutOfRange("octaveNumber", octaveNumber, MinOctaveNumber, MaxOctaveNumber, $"Octave number is out of [{MinOctaveNumber}, {MaxOctaveNumber}] range.");
			if (!Cache.TryGetValue(octaveNumber, out var value))
			{
				Cache.TryAdd(octaveNumber, value = new Octave(octaveNumber));
			}
			return value;
		}

		public static bool TryParse(string input, out Octave octave)
		{
			return ParsingUtilities.TryParse(input, (Parsing<Octave>)OctaveParser.TryParse, out octave);
		}

		public static Octave Parse(string input)
		{
			return ParsingUtilities.Parse<Octave>(input, OctaveParser.TryParse);
		}

		public static bool operator ==(Octave octave1, Octave octave2)
		{
			if ((object)octave1 == octave2)
			{
				return true;
			}
			if ((object)octave1 == null || (object)octave2 == null)
			{
				return false;
			}
			return octave1.Number == octave2.Number;
		}

		public static bool operator !=(Octave octave1, Octave octave2)
		{
			return !(octave1 == octave2);
		}

		public override string ToString()
		{
			return $"Octave {Number}";
		}

		public override bool Equals(object obj)
		{
			return this == obj as Octave;
		}

		public override int GetHashCode()
		{
			return Number.GetHashCode();
		}
	}
	internal static class OctaveParser
	{
		private const string OctaveNumberGroupName = "o";

		private static readonly string OctaveNumberGroup = ParsingUtilities.GetIntegerNumberGroup("o");

		private static readonly string[] Patterns = new string[1] { OctaveNumberGroup };

		private const string OctaveIsOutOfRange = "Octave number is out of range.";

		internal static ParsingResult TryParse(string input, out Octave octave)
		{
			octave = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			if (!ParsingUtilities.ParseInt(match, "o", Octave.Middle.Number, out var value) || value < Octave.MinOctaveNumber || value > Octave.MaxOctaveNumber)
			{
				return ParsingResult.Error("Octave number is out of range.");
			}
			octave = Octave.Get(value);
			return ParsingResult.Parsed;
		}
	}
	public sealed class Scale
	{
		public IEnumerable<Interval> Intervals { get; }

		public NoteName RootNote { get; }

		public Scale(IEnumerable<Interval> intervals, NoteName rootNote)
		{
			ThrowIfArgument.IsNull("intervals", intervals);
			ThrowIfArgument.IsInvalidEnumValue("rootNote", rootNote);
			Intervals = intervals;
			RootNote = rootNote;
		}

		public static bool TryParse(string input, out Scale scale)
		{
			return ParsingUtilities.TryParse(input, (Parsing<Scale>)ScaleParser.TryParse, out scale);
		}

		public static Scale Parse(string input)
		{
			return ParsingUtilities.Parse<Scale>(input, ScaleParser.TryParse);
		}

		public static bool operator ==(Scale scale1, Scale scale2)
		{
			if ((object)scale1 == scale2)
			{
				return true;
			}
			if ((object)scale1 == null || (object)scale2 == null)
			{
				return false;
			}
			if (scale1.RootNote == scale2.RootNote)
			{
				return scale1.Intervals.SequenceEqual(scale2.Intervals);
			}
			return false;
		}

		public static bool operator !=(Scale scale1, Scale scale2)
		{
			return !(scale1 == scale2);
		}

		public override string ToString()
		{
			return string.Format("{0} {1}", RootNote, string.Join(" ", Intervals));
		}

		public override bool Equals(object obj)
		{
			return this == obj as Scale;
		}

		public override int GetHashCode()
		{
			return (17 * 23 + RootNote.GetHashCode()) * 23 + Intervals.GetHashCode();
		}
	}
	public enum ScaleDegree
	{
		Tonic,
		Supertonic,
		Mediant,
		Subdominant,
		Dominant,
		Submediant,
		LeadingTone
	}
	public static class ScaleIntervals
	{
		[Melanchall.DryWetMidi.Common.DisplayName("aeolian")]
		public static readonly IEnumerable<Interval> Aeolian = GetIntervals(2, 1, 2, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("altered")]
		public static readonly IEnumerable<Interval> Altered = GetIntervals(1, 2, 1, 2, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("arabian")]
		public static readonly IEnumerable<Interval> Arabian = GetIntervals(2, 2, 1, 1, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("augmented")]
		public static readonly IEnumerable<Interval> Augmented = GetIntervals(3, 1, 3, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("augmented heptatonic")]
		public static readonly IEnumerable<Interval> AugmentedHeptatonic = GetIntervals(3, 1, 1, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("balinese")]
		public static readonly IEnumerable<Interval> Balinese = GetIntervals(1, 2, 2, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("bebop")]
		public static readonly IEnumerable<Interval> Bebop = GetIntervals(2, 2, 1, 2, 2, 1, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("bebop dominant")]
		public static readonly IEnumerable<Interval> BebopDominant = GetIntervals(2, 2, 1, 2, 2, 1, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("bebop locrian")]
		public static readonly IEnumerable<Interval> BebopLocrian = GetIntervals(1, 2, 2, 1, 1, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("bebop major")]
		public static readonly IEnumerable<Interval> BebopMajor = GetIntervals(2, 2, 1, 2, 1, 1, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("bebop minor")]
		public static readonly IEnumerable<Interval> BebopMinor = GetIntervals(2, 1, 1, 1, 2, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("blues")]
		public static readonly IEnumerable<Interval> Blues = GetIntervals(3, 2, 1, 1, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("chinese")]
		public static readonly IEnumerable<Interval> Chinese = GetIntervals(4, 2, 1, 4, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("chromatic")]
		public static readonly IEnumerable<Interval> Chromatic = GetIntervals(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("composite blues")]
		public static readonly IEnumerable<Interval> CompositeBlues = GetIntervals(2, 1, 1, 1, 1, 1, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("diminished")]
		public static readonly IEnumerable<Interval> Diminished = GetIntervals(2, 1, 2, 1, 2, 1, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("diminished whole tone")]
		public static readonly IEnumerable<Interval> DiminishedWholeTone = GetIntervals(1, 2, 1, 2, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("dominant")]
		public static readonly IEnumerable<Interval> Dominant = GetIntervals(2, 2, 1, 2, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("dorian")]
		public static readonly IEnumerable<Interval> Dorian = GetIntervals(2, 1, 2, 2, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("dorian #4")]
		public static readonly IEnumerable<Interval> Dorian4 = GetIntervals(2, 1, 3, 1, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("dorian b2")]
		public static readonly IEnumerable<Interval> DorianB2 = GetIntervals(1, 2, 2, 2, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("double harmonic lydian")]
		public static readonly IEnumerable<Interval> DoubleHarmonicLydian = GetIntervals(1, 3, 2, 1, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("double harmonic major")]
		public static readonly IEnumerable<Interval> DoubleHarmonicMajor = GetIntervals(1, 3, 1, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("egyptian")]
		public static readonly IEnumerable<Interval> Egyptian = GetIntervals(2, 3, 2, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("enigmatic")]
		public static readonly IEnumerable<Interval> Enigmatic = GetIntervals(1, 3, 2, 2, 2, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("flamenco")]
		public static readonly IEnumerable<Interval> Flamenco = GetIntervals(1, 2, 1, 2, 1, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("flat six pentatonic")]
		public static readonly IEnumerable<Interval> FlatSixPentatonic = GetIntervals(2, 2, 3, 1, 4);

		[Melanchall.DryWetMidi.Common.DisplayName("flat three pentatonic")]
		public static readonly IEnumerable<Interval> FlatThreePentatonic = GetIntervals(2, 1, 4, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("gypsy")]
		public static readonly IEnumerable<Interval> Gypsy = GetIntervals(1, 3, 1, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("harmonic major")]
		public static readonly IEnumerable<Interval> HarmonicMajor = GetIntervals(2, 2, 1, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("harmonic minor")]
		public static readonly IEnumerable<Interval> HarmonicMinor = GetIntervals(2, 1, 2, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("hindu")]
		public static readonly IEnumerable<Interval> Hindu = GetIntervals(2, 2, 1, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("hirajoshi")]
		public static readonly IEnumerable<Interval> Hirajoshi = GetIntervals(2, 1, 4, 1, 4);

		[Melanchall.DryWetMidi.Common.DisplayName("hungarian major")]
		public static readonly IEnumerable<Interval> HungarianMajor = GetIntervals(3, 1, 2, 1, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("hungarian minor")]
		public static readonly IEnumerable<Interval> HungarianMinor = GetIntervals(2, 1, 3, 1, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("ichikosucho")]
		public static readonly IEnumerable<Interval> Ichikosucho = GetIntervals(2, 2, 1, 1, 1, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("in-sen")]
		public static readonly IEnumerable<Interval> InSen = GetIntervals(1, 4, 2, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("indian")]
		public static readonly IEnumerable<Interval> Indian = GetIntervals(4, 1, 2, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("ionian")]
		public static readonly IEnumerable<Interval> Ionian = GetIntervals(2, 2, 1, 2, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("ionian augmented")]
		public static readonly IEnumerable<Interval> IonianAugmented = GetIntervals(2, 2, 1, 3, 1, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("ionian pentatonic")]
		public static readonly IEnumerable<Interval> IonianPentatonic = GetIntervals(4, 1, 2, 4, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("iwato")]
		public static readonly IEnumerable<Interval> Iwato = GetIntervals(1, 4, 1, 4, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("kafi raga")]
		public static readonly IEnumerable<Interval> KafiRaga = GetIntervals(3, 1, 1, 2, 2, 1, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("kumoi")]
		public static readonly IEnumerable<Interval> Kumoi = GetIntervals(2, 1, 4, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("kumoijoshi")]
		public static readonly IEnumerable<Interval> Kumoijoshi = GetIntervals(1, 4, 2, 1, 4);

		[Melanchall.DryWetMidi.Common.DisplayName("leading whole tone")]
		public static readonly IEnumerable<Interval> LeadingWholeTone = GetIntervals(2, 2, 2, 2, 2, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("locrian")]
		public static readonly IEnumerable<Interval> Locrian = GetIntervals(1, 2, 2, 1, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("locrian #2")]
		public static readonly IEnumerable<Interval> Locrian2 = GetIntervals(2, 1, 2, 1, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("locrian major")]
		public static readonly IEnumerable<Interval> LocrianMajor = GetIntervals(2, 2, 1, 1, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("locrian pentatonic")]
		public static readonly IEnumerable<Interval> LocrianPentatonic = GetIntervals(3, 2, 1, 4, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian")]
		public static readonly IEnumerable<Interval> Lydian = GetIntervals(2, 2, 2, 1, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian #5P pentatonic")]
		public static readonly IEnumerable<Interval> Lydian5PPentatonic = GetIntervals(4, 2, 2, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian #9")]
		public static readonly IEnumerable<Interval> Lydian9 = GetIntervals(1, 3, 2, 1, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian augmented")]
		public static readonly IEnumerable<Interval> LydianAugmented = GetIntervals(2, 2, 2, 2, 1, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian b7")]
		public static readonly IEnumerable<Interval> LydianB7 = GetIntervals(2, 2, 2, 1, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian diminished")]
		public static readonly IEnumerable<Interval> LydianDiminished = GetIntervals(2, 1, 3, 1, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian dominant")]
		public static readonly IEnumerable<Interval> LydianDominant = GetIntervals(2, 2, 2, 1, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian dominant pentatonic")]
		public static readonly IEnumerable<Interval> LydianDominantPentatonic = GetIntervals(4, 2, 1, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian minor")]
		public static readonly IEnumerable<Interval> LydianMinor = GetIntervals(2, 2, 2, 1, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("lydian pentatonic")]
		public static readonly IEnumerable<Interval> LydianPentatonic = GetIntervals(4, 2, 1, 4, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("major")]
		public static readonly IEnumerable<Interval> Major = GetIntervals(2, 2, 1, 2, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("major blues")]
		public static readonly IEnumerable<Interval> MajorBlues = GetIntervals(2, 1, 1, 3, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("major flat two pentatonic")]
		public static readonly IEnumerable<Interval> MajorFlatTwoPentatonic = GetIntervals(1, 3, 3, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("major pentatonic")]
		public static readonly IEnumerable<Interval> MajorPentatonic = GetIntervals(2, 2, 3, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("malkos raga")]
		public static readonly IEnumerable<Interval> MalkosRaga = GetIntervals(3, 2, 3, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("melodic minor")]
		public static readonly IEnumerable<Interval> MelodicMinor = GetIntervals(2, 1, 2, 2, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("melodic minor fifth mode")]
		public static readonly IEnumerable<Interval> MelodicMinorFifthMode = GetIntervals(2, 2, 1, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("melodic minor second mode")]
		public static readonly IEnumerable<Interval> MelodicMinorSecondMode = GetIntervals(1, 2, 2, 2, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("minor")]
		public static readonly IEnumerable<Interval> Minor = GetIntervals(2, 1, 2, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("minor #7M pentatonic")]
		public static readonly IEnumerable<Interval> Minor7MPentatonic = GetIntervals(3, 2, 2, 4, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("minor bebop")]
		public static readonly IEnumerable<Interval> MinorBebop = GetIntervals(2, 1, 2, 2, 1, 2, 1, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("minor blues")]
		public static readonly IEnumerable<Interval> MinorBlues = GetIntervals(3, 2, 1, 1, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("minor hexatonic")]
		public static readonly IEnumerable<Interval> MinorHexatonic = GetIntervals(2, 1, 2, 2, 4, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("minor pentatonic")]
		public static readonly IEnumerable<Interval> MinorPentatonic = GetIntervals(3, 2, 2, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("minor seven flat five pentatonic")]
		public static readonly IEnumerable<Interval> MinorSevenFlatFivePentatonic = GetIntervals(3, 2, 1, 4, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("minor six diminished")]
		public static readonly IEnumerable<Interval> MinorSixDiminished = GetIntervals(2, 1, 2, 2, 1, 1, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("minor six pentatonic")]
		public static readonly IEnumerable<Interval> MinorSixPentatonic = GetIntervals(3, 2, 2, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("mixolydian")]
		public static readonly IEnumerable<Interval> Mixolydian = GetIntervals(2, 2, 1, 2, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("mixolydian b6M")]
		public static readonly IEnumerable<Interval> MixolydianB6M = GetIntervals(2, 2, 1, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("mixolydian pentatonic")]
		public static readonly IEnumerable<Interval> MixolydianPentatonic = GetIntervals(4, 1, 2, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("mystery #1")]
		public static readonly IEnumerable<Interval> Mystery1 = GetIntervals(1, 3, 2, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("neopolitan")]
		public static readonly IEnumerable<Interval> Neopolitan = GetIntervals(1, 2, 2, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("neopolitan major")]
		public static readonly IEnumerable<Interval> NeopolitanMajor = GetIntervals(1, 2, 2, 2, 2, 2, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("neopolitan major pentatonic")]
		public static readonly IEnumerable<Interval> NeopolitanMajorPentatonic = GetIntervals(4, 1, 1, 4, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("neopolitan minor")]
		public static readonly IEnumerable<Interval> NeopolitanMinor = GetIntervals(1, 2, 2, 2, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("oriental")]
		public static readonly IEnumerable<Interval> Oriental = GetIntervals(1, 3, 1, 1, 3, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("pelog")]
		public static readonly IEnumerable<Interval> Pelog = GetIntervals(1, 2, 4, 1, 4);

		[Melanchall.DryWetMidi.Common.DisplayName("pentatonic")]
		public static readonly IEnumerable<Interval> Pentatonic = GetIntervals(2, 2, 3, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("persian")]
		public static readonly IEnumerable<Interval> Persian = GetIntervals(1, 3, 1, 1, 2, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("phrygian")]
		public static readonly IEnumerable<Interval> Phrygian = GetIntervals(1, 2, 2, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("phrygian major")]
		public static readonly IEnumerable<Interval> PhrygianMajor = GetIntervals(1, 3, 1, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("piongio")]
		public static readonly IEnumerable<Interval> Piongio = GetIntervals(2, 3, 2, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("pomeroy")]
		public static readonly IEnumerable<Interval> Pomeroy = GetIntervals(1, 2, 1, 2, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("prometheus")]
		public static readonly IEnumerable<Interval> Prometheus = GetIntervals(2, 2, 2, 3, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("prometheus neopolitan")]
		public static readonly IEnumerable<Interval> PrometheusNeopolitan = GetIntervals(1, 3, 2, 3, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("purvi raga")]
		public static readonly IEnumerable<Interval> PurviRaga = GetIntervals(1, 3, 1, 1, 1, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("ritusen")]
		public static readonly IEnumerable<Interval> Ritusen = GetIntervals(2, 3, 2, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("romanian minor")]
		public static readonly IEnumerable<Interval> RomanianMinor = GetIntervals(2, 1, 3, 1, 2, 1, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("scriabin")]
		public static readonly IEnumerable<Interval> Scriabin = GetIntervals(1, 3, 3, 2, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("six tone symmetric")]
		public static readonly IEnumerable<Interval> SixToneSymmetric = GetIntervals(1, 3, 1, 3, 1, 3);

		[Melanchall.DryWetMidi.Common.DisplayName("spanish")]
		public static readonly IEnumerable<Interval> Spanish = GetIntervals(1, 3, 1, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("spanish heptatonic")]
		public static readonly IEnumerable<Interval> SpanishHeptatonic = GetIntervals(1, 2, 1, 1, 2, 1, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("super locrian")]
		public static readonly IEnumerable<Interval> SuperLocrian = GetIntervals(1, 2, 1, 2, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("super locrian pentatonic")]
		public static readonly IEnumerable<Interval> SuperLocrianPentatonic = GetIntervals(3, 1, 2, 4, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("todi raga")]
		public static readonly IEnumerable<Interval> TodiRaga = GetIntervals(1, 2, 3, 1, 1, 3, 1);

		[Melanchall.DryWetMidi.Common.DisplayName("vietnamese 1")]
		public static readonly IEnumerable<Interval> Vietnamese1 = GetIntervals(3, 2, 2, 1, 4);

		[Melanchall.DryWetMidi.Common.DisplayName("vietnamese 2")]
		public static readonly IEnumerable<Interval> Vietnamese2 = GetIntervals(3, 2, 2, 3, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("whole tone")]
		public static readonly IEnumerable<Interval> WholeTone = GetIntervals(2, 2, 2, 2, 2, 2);

		[Melanchall.DryWetMidi.Common.DisplayName("whole tone pentatonic")]
		public static readonly IEnumerable<Interval> WholeTonePentatonic = GetIntervals(4, 2, 2, 2, 2);

		public static IEnumerable<Interval> GetByName(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Scale's name");
			FieldInfo[] fields = typeof(ScaleIntervals).GetFields(BindingFlags.Static | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				string text = (Attribute.GetCustomAttribute(fieldInfo, typeof(Melanchall.DryWetMidi.Common.DisplayNameAttribute)) as Melanchall.DryWetMidi.Common.DisplayNameAttribute)?.Name;
				if (!string.IsNullOrWhiteSpace(text) && text.Equals(name, StringComparison.InvariantCultureIgnoreCase) && fieldInfo.GetValue(null) is IEnumerable<Interval> result)
				{
					return result;
				}
			}
			return null;
		}

		private static IEnumerable<Interval> GetIntervals(params int[] intervalsInHalfSteps)
		{
			return intervalsInHalfSteps.Select((int i) => Interval.FromHalfSteps(i)).ToArray();
		}
	}
	internal static class ScaleParser
	{
		private const string RootNoteNameGroupName = "rn";

		private const string IntervalsMnemonicGroupName = "im";

		private const string IntervalGroupName = "i";

		private static readonly string IntervalGroup = "(?<i>(" + string.Join("|", IntervalParser.GetPatterns()) + ")\\s*)+";

		private static readonly string IntervalsMnemonicGroup = "(?<im>.+?)";

		private static readonly string[] Patterns = (from p in NoteNameParser.GetPatterns()
			select "(?<rn>" + p + ")\\s*(" + IntervalGroup + "|" + IntervalsMnemonicGroup + ")").ToArray();

		private const string ScaleIsUnknown = "Scale is unknown.";

		internal static ParsingResult TryParse(string input, out Scale scale)
		{
			scale = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			NoteName noteName;
			ParsingResult parsingResult = NoteNameParser.TryParse(match.Groups["rn"].Value, out noteName);
			if (parsingResult.Status != ParsingStatus.Parsed)
			{
				return parsingResult;
			}
			Group obj = match.Groups["i"];
			IEnumerable<Interval> enumerable;
			if (obj.Success)
			{
				var source = obj.Captures.OfType<Capture>().Select(delegate(Capture c)
				{
					Interval interval;
					ParsingResult parsingResult2 = IntervalParser.TryParse(c.Value, out interval);
					return new
					{
						Interval = interval,
						ParsingResult = parsingResult2
					};
				}).ToArray();
				var anon = source.FirstOrDefault(r => r.ParsingResult.Status != ParsingStatus.Parsed);
				if (anon != null)
				{
					return anon.ParsingResult;
				}
				enumerable = source.Select(r => r.Interval).ToArray();
			}
			else
			{
				enumerable = ScaleIntervals.GetByName(match.Groups["im"].Value);
			}
			if (enumerable == null)
			{
				return ParsingResult.Error("Scale is unknown.");
			}
			scale = new Scale(enumerable, noteName);
			return ParsingResult.Parsed;
		}
	}
	public static class ScaleUtilities
	{
		public static NoteName GetDegree(this Scale scale, ScaleDegree degree)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsInvalidEnumValue("degree", degree);
			ThrowIfDegreeIsOutOfRange(scale, degree);
			return scale.GetStep((int)degree);
		}

		public static NoteName GetStep(this Scale scale, int step)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNegative("step", step, "Step is negative.");
			return scale.GetNotesNames().Skip(step).First();
		}

		public static IEnumerable<Note> GetNotes(this Scale scale)
		{
			ThrowIfArgument.IsNull("scale", scale);
			int noteNumber = (byte)SevenBitNumber.Values.SkipWhile((SevenBitNumber number) => NoteUtilities.GetNoteName(number) != scale.RootNote).First();
			yield return Note.Get((SevenBitNumber)(byte)noteNumber);
			while (true)
			{
				foreach (Interval interval in scale.Intervals)
				{
					noteNumber += (int)interval;
					if (NoteUtilities.IsNoteNumberValid(noteNumber))
					{
						yield return Note.Get((SevenBitNumber)(byte)noteNumber);
						continue;
					}
					yield break;
				}
			}
		}

		public static IEnumerable<NoteName> GetNotesNames(this Scale scale)
		{
			ThrowIfArgument.IsNull("scale", scale);
			int lastNoteNumber = (int)scale.RootNote;
			yield return scale.RootNote;
			while (true)
			{
				foreach (Interval interval in scale.Intervals)
				{
					int noteNumber = (lastNoteNumber + (int)interval) % 12;
					yield return (NoteName)noteNumber;
					lastNoteNumber = noteNumber;
				}
			}
		}

		public static IEnumerable<Note> GetAscendingNotes(this Scale scale, Note rootNote)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("rootNote", rootNote);
			return scale.GetNotes().SkipWhile((Note n) => n != rootNote);
		}

		public static IEnumerable<Note> GetDescendingNotes(this Scale scale, Note rootNote)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("rootNote", rootNote);
			return new Note[1] { rootNote }.Concat(scale.GetNotes().TakeWhile((Note n) => n != rootNote).Reverse());
		}

		public static bool IsNoteInScale(this Scale scale, Note note)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("note", note);
			return scale.GetNotes().Contains(note);
		}

		public static Note GetNextNote(this Scale scale, Note note)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("note", note);
			return scale.GetAscendingNotes(note).Skip(1).FirstOrDefault();
		}

		public static Note GetPreviousNote(this Scale scale, Note note)
		{
			ThrowIfArgument.IsNull("scale", scale);
			ThrowIfArgument.IsNull("note", note);
			return scale.GetDescendingNotes(note).Skip(1).FirstOrDefault();
		}

		private static void ThrowIfDegreeIsOutOfRange(Scale scale, ScaleDegree degree)
		{
			if ((int)degree >= scale.Intervals.Count())
			{
				throw new ArgumentOutOfRangeException("degree", degree, "Degree is out of range for the scale.");
			}
		}
	}
}
namespace Melanchall.DryWetMidi.Multimedia
{
	public interface IClockDrivenObject
	{
		void TickClock();
	}
	public sealed class MidiClock : IDisposable
	{
		private const double DefaultSpeed = 1.0;

		private bool _disposed;

		private readonly bool _startImmediately;

		private readonly Stopwatch _stopwatch = new Stopwatch();

		private TimeSpan _startTime = TimeSpan.Zero;

		private double _speed = 1.0;

		private readonly TickGenerator _tickGenerator;

		public TimeSpan Interval { get; }

		public bool IsRunning => _stopwatch.IsRunning;

		public TimeSpan CurrentTime { get; private set; } = TimeSpan.Zero;

		public double Speed
		{
			get
			{
				return _speed;
			}
			set
			{
				EnsureIsNotDisposed();
				ThrowIfArgument.IsNegative("value", value, "Speed is negative.");
				bool isRunning = IsRunning;
				Stop();
				_startTime = _stopwatch.Elapsed;
				_speed = value;
				if (isRunning)
				{
					Start();
				}
			}
		}

		public event EventHandler Ticked;

		public MidiClock(bool startImmediately, TickGenerator tickGenerator, TimeSpan interval)
		{
			ThrowIfArgument.IsLessThan("interval", interval, TimeSpan.FromMilliseconds(1.0), "Interval is less than 1 ms.");
			_startImmediately = startImmediately;
			_tickGenerator = tickGenerator;
			if (_tickGenerator != null)
			{
				_tickGenerator.TickGenerated += OnTickGenerated;
			}
			Interval = interval;
		}

		~MidiClock()
		{
			Dispose(disposing: false);
		}

		public void Start()
		{
			EnsureIsNotDisposed();
			if (!IsRunning)
			{
				_tickGenerator?.TryStart(Interval);
				_stopwatch.Start();
				if (_startImmediately)
				{
					OnTicked();
				}
			}
		}

		public void Stop()
		{
			EnsureIsNotDisposed();
			StopInternally();
		}

		public void Restart()
		{
			EnsureIsNotDisposed();
			Stop();
			ResetCurrentTime();
			Start();
		}

		public void ResetCurrentTime()
		{
			EnsureIsNotDisposed();
			SetCurrentTime(TimeSpan.Zero);
		}

		public void SetCurrentTime(TimeSpan time)
		{
			EnsureIsNotDisposed();
			_stopwatch.Reset();
			_startTime = time;
			CurrentTime = time;
		}

		public void Tick()
		{
			if (IsRunning && !_disposed)
			{
				CurrentTime = _startTime + new TimeSpan(MathUtilities.RoundToLong((double)_stopwatch.Elapsed.Ticks * Speed));
				OnTicked();
			}
		}

		internal void StopInternally()
		{
			if (!_disposed)
			{
				_stopwatch.Stop();
				_tickGenerator?.TryStop();
			}
		}

		internal void StopShortly()
		{
			_stopwatch.Stop();
		}

		private void OnTickGenerated(object sender, EventArgs e)
		{
			Tick();
		}

		private void OnTicked()
		{
			this.Ticked?.Invoke(this, EventArgs.Empty);
		}

		private void EnsureIsNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("MIDI clock is disposed.");
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing && _tickGenerator != null)
				{
					_tickGenerator.TickGenerated -= OnTickGenerated;
					_tickGenerator.Dispose();
				}
				_disposed = true;
			}
		}
	}
	public sealed class MidiClockSettings
	{
		private Func<TickGenerator> _createTickGeneratorCallback = () => new HighPrecisionTickGenerator();

		public Func<TickGenerator> CreateTickGeneratorCallback
		{
			get
			{
				return _createTickGeneratorCallback;
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				_createTickGeneratorCallback = value;
			}
		}
	}
	public sealed class HighPrecisionTickGenerator : TickGenerator
	{
		public static readonly TimeSpan MinInterval = TimeSpan.FromMilliseconds(1.0);

		public static readonly TimeSpan MaxInterval = TimeSpan.FromMilliseconds(2147483647.0);

		private bool _disposed;

		private TickGeneratorApi.TimerCallback_Win _tickCallback_Win;

		private TickGeneratorApi.TimerCallback_Mac _tickCallback_Mac;

		private IntPtr _tickGeneratorInfo;

		~HighPrecisionTickGenerator()
		{
			Dispose(disposing: false);
		}

		protected override void Start(TimeSpan interval)
		{
			ThrowIfArgument.IsOutOfRange("interval", interval, MinInterval, MaxInterval, $"Interval is out of [{MinInterval}, {MaxInterval}] range.");
			int intervalInMilliseconds = (int)interval.TotalMilliseconds;
			CommonApi.API_TYPE aPI_TYPE = CommonApiProvider.Api.Api_GetApiType();
			TickGeneratorApi.TG_STARTRESULT tG_STARTRESULT = TickGeneratorApi.TG_STARTRESULT.TG_STARTRESULT_OK;
			switch (aPI_TYPE)
			{
			case CommonApi.API_TYPE.API_TYPE_WIN:
				tG_STARTRESULT = StartHighPrecisionTickGenerator_Win(intervalInMilliseconds, out _tickGeneratorInfo);
				break;
			case CommonApi.API_TYPE.API_TYPE_MAC:
				tG_STARTRESULT = StartHighPrecisionTickGenerator_Mac(intervalInMilliseconds, out _tickGeneratorInfo);
				break;
			}
			if (tG_STARTRESULT != TickGeneratorApi.TG_STARTRESULT.TG_STARTRESULT_OK)
			{
				throw new TickGeneratorException("Failed to start high-precision tick generator.", (int)tG_STARTRESULT);
			}
		}

		protected override void Stop()
		{
			TickGeneratorApi.TG_STOPRESULT tG_STOPRESULT = StopInternal();
			if (tG_STOPRESULT != TickGeneratorApi.TG_STOPRESULT.TG_STOPRESULT_OK)
			{
				throw new TickGeneratorException("Failed to stop high-precision tick generator.", (int)tG_STOPRESULT);
			}
		}

		private void OnTick_Win(uint uID, uint uMsg, uint dwUser, uint dw1, uint dw2)
		{
			OnTick();
		}

		private void OnTick_Mac()
		{
			OnTick();
		}

		private void OnTick()
		{
			if (base.IsRunning && !_disposed)
			{
				GenerateTick();
			}
		}

		private TickGeneratorApi.TG_STOPRESULT StopInternal()
		{
			if (_tickGeneratorInfo == IntPtr.Zero)
			{
				return TickGeneratorApi.TG_STOPRESULT.TG_STOPRESULT_OK;
			}
			TickGeneratorApi.TG_STOPRESULT result = TickGeneratorApiProvider.Api.Api_StopHighPrecisionTickGenerator(_tickGeneratorInfo);
			_tickGeneratorInfo = IntPtr.Zero;
			return result;
		}

		private TickGeneratorApi.TG_STARTRESULT StartHighPrecisionTickGenerator_Win(int intervalInMilliseconds, out IntPtr tickGeneratorInfo)
		{
			_tickCallback_Win = OnTick_Win;
			return TickGeneratorApiProvider.Api.Api_StartHighPrecisionTickGenerator_Win(intervalInMilliseconds, _tickCallback_Win, out tickGeneratorInfo);
		}

		private TickGeneratorApi.TG_STARTRESULT StartHighPrecisionTickGenerator_Mac(int intervalInMilliseconds, out IntPtr tickGeneratorInfo)
		{
			_tickCallback_Mac = OnTick_Mac;
			return TickGeneratorApiProvider.Api.Api_StartHighPrecisionTickGenerator_Mac(intervalInMilliseconds, _tickCallback_Mac, out tickGeneratorInfo);
		}

		public override void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				StopInternal();
				_disposed = true;
			}
		}
	}
	public sealed class RegularPrecisionTickGenerator : TickGenerator
	{
		public static readonly TimeSpan MinInterval = TimeSpan.FromMilliseconds(1.0);

		public static readonly TimeSpan MaxInterval = TimeSpan.FromMilliseconds(2147483647.0);

		private System.Timers.Timer _timer;

		private bool _disposed;

		protected override void Start(TimeSpan interval)
		{
			ThrowIfArgument.IsOutOfRange("interval", interval, MinInterval, MaxInterval, $"Interval is out of [{MinInterval}, {MaxInterval}] range.");
			_timer = new System.Timers.Timer(interval.TotalMilliseconds);
			_timer.Elapsed += OnElapsed;
			_timer.Start();
		}

		protected override void Stop()
		{
			_timer.Stop();
		}

		private void OnElapsed(object sender, ElapsedEventArgs e)
		{
			if (base.IsRunning && !_disposed)
			{
				GenerateTick();
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing && base.IsRunning)
				{
					_timer.Stop();
					_timer.Elapsed -= OnElapsed;
					_timer.Dispose();
				}
				_disposed = true;
			}
		}
	}
	public abstract class TickGenerator : IDisposable
	{
		protected bool IsRunning { get; set; }

		public event EventHandler TickGenerated;

		internal void TryStart(TimeSpan interval)
		{
			if (!IsRunning)
			{
				Start(interval);
				IsRunning = true;
			}
		}

		internal void TryStop()
		{
			if (IsRunning)
			{
				Stop();
				IsRunning = false;
			}
		}

		protected void GenerateTick()
		{
			this.TickGenerated?.Invoke(this, EventArgs.Empty);
		}

		protected abstract void Start(TimeSpan interval);

		protected abstract void Stop();

		public virtual void Dispose()
		{
			Dispose(disposing: true);
		}

		protected virtual void Dispose(bool disposing)
		{
		}
	}
	internal abstract class TickGeneratorApi : NativeApi
	{
		public enum TG_STARTRESULT
		{
			TG_STARTRESULT_OK = 0,
			TG_STARTRESULT_CANTGETDEVICECAPABILITIES = 1,
			TG_STARTRESULT_CANTSETTIMERCALLBACK = 2,
			TG_STARTRESULT_NORESOURCES = 101,
			TG_STARTRESULT_BADTHREADATTRIBUTE = 102,
			TG_STARTRESULT_UNKNOWNERROR = 199
		}

		public enum TG_STOPRESULT
		{
			TG_STOPRESULT_OK,
			TG_STOPRESULT_CANTENDPERIOD,
			TG_STOPRESULT_CANTKILLEVENT
		}

		public delegate void TimerCallback_Win(uint uID, uint uMsg, uint dwUser, uint dw1, uint dw2);

		public delegate void TimerCallback_Mac();

		public abstract TG_STARTRESULT Api_StartHighPrecisionTickGenerator_Win(int interval, TimerCallback_Win callback, out IntPtr info);

		public abstract TG_STARTRESULT Api_StartHighPrecisionTickGenerator_Mac(int interval, TimerCallback_Mac callback, out IntPtr info);

		public abstract TG_STOPRESULT Api_StopHighPrecisionTickGenerator(IntPtr info);
	}
	internal sealed class TickGeneratorApi32 : TickGeneratorApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native32";

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern TG_STARTRESULT StartHighPrecisionTickGenerator_Win(int interval, TimerCallback_Win callback, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern TG_STARTRESULT StartHighPrecisionTickGenerator_Mac(int interval, TimerCallback_Mac callback, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern TG_STOPRESULT StopHighPrecisionTickGenerator(IntPtr info);

		public override TG_STARTRESULT Api_StartHighPrecisionTickGenerator_Win(int interval, TimerCallback_Win callback, out IntPtr info)
		{
			return StartHighPrecisionTickGenerator_Win(interval, callback, out info);
		}

		public override TG_STARTRESULT Api_StartHighPrecisionTickGenerator_Mac(int interval, TimerCallback_Mac callback, out IntPtr info)
		{
			return StartHighPrecisionTickGenerator_Mac(interval, callback, out info);
		}

		public override TG_STOPRESULT Api_StopHighPrecisionTickGenerator(IntPtr info)
		{
			return StopHighPrecisionTickGenerator(info);
		}
	}
	internal sealed class TickGeneratorApi64 : TickGeneratorApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native64";

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		public static extern TG_STARTRESULT StartHighPrecisionTickGenerator_Win(int interval, TimerCallback_Win callback, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		public static extern TG_STARTRESULT StartHighPrecisionTickGenerator_Mac(int interval, TimerCallback_Mac callback, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		public static extern TG_STOPRESULT StopHighPrecisionTickGenerator(IntPtr info);

		public override TG_STARTRESULT Api_StartHighPrecisionTickGenerator_Win(int interval, TimerCallback_Win callback, out IntPtr info)
		{
			return StartHighPrecisionTickGenerator_Win(interval, callback, out info);
		}

		public override TG_STARTRESULT Api_StartHighPrecisionTickGenerator_Mac(int interval, TimerCallback_Mac callback, out IntPtr info)
		{
			return StartHighPrecisionTickGenerator_Mac(interval, callback, out info);
		}

		public override TG_STOPRESULT Api_StopHighPrecisionTickGenerator(IntPtr info)
		{
			return StopHighPrecisionTickGenerator(info);
		}
	}
	internal static class TickGeneratorApiProvider
	{
		private static readonly bool Is64Bit = IntPtr.Size == 8;

		private static TickGeneratorApi _api;

		public static TickGeneratorApi Api
		{
			get
			{
				if (_api == null)
				{
					_api = (Is64Bit ? ((TickGeneratorApi)new TickGeneratorApi64()) : ((TickGeneratorApi)new TickGeneratorApi32()));
				}
				return _api;
			}
		}
	}
	public sealed class TickGeneratorException : MidiException
	{
		public int ErrorCode { get; }

		public TickGeneratorException(string message, int errorCode)
			: base(message)
		{
			ErrorCode = errorCode;
		}
	}
	internal abstract class CommonApi : NativeApi
	{
		public enum API_TYPE
		{
			API_TYPE_WIN,
			API_TYPE_MAC
		}

		public abstract API_TYPE Api_GetApiType();

		public abstract bool Api_CanCompareDevices();
	}
	internal sealed class CommonApi32 : CommonApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native32";

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern API_TYPE GetApiType();

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern bool CanCompareDevices();

		public override API_TYPE Api_GetApiType()
		{
			return GetApiType();
		}

		public override bool Api_CanCompareDevices()
		{
			return CanCompareDevices();
		}
	}
	internal sealed class CommonApi64 : CommonApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native64";

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern API_TYPE GetApiType();

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern bool CanCompareDevices();

		public override API_TYPE Api_GetApiType()
		{
			return GetApiType();
		}

		public override bool Api_CanCompareDevices()
		{
			return CanCompareDevices();
		}
	}
	internal static class CommonApiProvider
	{
		private static readonly bool Is64Bit = IntPtr.Size == 8;

		private static CommonApi _api;

		public static CommonApi Api
		{
			get
			{
				if (_api == null)
				{
					_api = (Is64Bit ? ((CommonApi)new CommonApi64()) : ((CommonApi)new CommonApi32()));
				}
				return _api;
			}
		}
	}
	public sealed class DevicesConnector
	{
		public IInputDevice InputDevice { get; }

		public IReadOnlyCollection<IOutputDevice> OutputDevices { get; }

		public bool AreDevicesConnected { get; private set; }

		public DevicesConnectorEventCallback EventCallback { get; set; }

		public DevicesConnector(IInputDevice inputDevice, params IOutputDevice[] outputDevices)
		{
			ThrowIfArgument.IsNull("inputDevice", inputDevice);
			ThrowIfArgument.IsNull("outputDevices", outputDevices);
			ThrowIfArgument.ContainsNull("outputDevices", outputDevices);
			InputDevice = inputDevice;
			OutputDevices = (IReadOnlyCollection<IOutputDevice>)(object)outputDevices;
		}

		public void Connect()
		{
			if (!AreDevicesConnected)
			{
				InputDevice.EventReceived += OnEventReceived;
				AreDevicesConnected = true;
			}
		}

		public void Disconnect()
		{
			AreDevicesConnected = false;
			InputDevice.EventReceived -= OnEventReceived;
		}

		private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
		{
			if (!AreDevicesConnected)
			{
				return;
			}
			MidiEvent midiEvent = e.Event;
			DevicesConnectorEventCallback eventCallback = EventCallback;
			if (((eventCallback == null) ? midiEvent : eventCallback(midiEvent)) == null)
			{
				return;
			}
			foreach (IOutputDevice outputDevice in OutputDevices)
			{
				if (AreDevicesConnected)
				{
					outputDevice.SendEvent(e.Event);
				}
			}
		}
	}
	public delegate MidiEvent DevicesConnectorEventCallback(MidiEvent inputMidiEvent);
	public static class DevicesConnectorUtilities
	{
		public static DevicesConnector Connect(this IInputDevice inputDevice, params IOutputDevice[] outputDevices)
		{
			ThrowIfArgument.IsNull("inputDevice", inputDevice);
			ThrowIfArgument.IsNull("outputDevices", outputDevices);
			ThrowIfArgument.ContainsNull("outputDevices", outputDevices);
			DevicesConnector devicesConnector = new DevicesConnector(inputDevice, outputDevices);
			devicesConnector.Connect();
			return devicesConnector;
		}
	}
	public sealed class DeviceAddedRemovedEventArgs : EventArgs
	{
		public MidiDevice Device { get; }

		internal DeviceAddedRemovedEventArgs(MidiDevice device)
		{
			Device = device;
		}
	}
	public sealed class DevicesWatcher
	{
		private static volatile DevicesWatcher _instance;

		private static object _lockObject = new object();

		public static DevicesWatcher Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							MidiDevicesSession.GetSessionHandle();
							_instance = new DevicesWatcher();
							MidiDevicesSession.InputDeviceAdded += _instance.OnInputDeviceAdded;
							MidiDevicesSession.InputDeviceRemoved += _instance.OnInputDeviceRemoved;
							MidiDevicesSession.OutputDeviceAdded += _instance.OnOutputDeviceAdded;
							MidiDevicesSession.OutputDeviceRemoved += _instance.OnOutputDeviceRemoved;
						}
					}
				}
				return _instance;
			}
		}

		public event EventHandler<DeviceAddedRemovedEventArgs> DeviceAdded;

		public event EventHandler<DeviceAddedRemovedEventArgs> DeviceRemoved;

		private DevicesWatcher()
		{
		}

		private void OnInputDeviceAdded(object sender, IntPtr info)
		{
			this.DeviceAdded?.Invoke(this, new DeviceAddedRemovedEventArgs(new InputDevice(info, MidiDevice.CreationContext.AddedDevice)));
		}

		private void OnInputDeviceRemoved(object sender, IntPtr info)
		{
			this.DeviceRemoved?.Invoke(this, new DeviceAddedRemovedEventArgs(new InputDevice(info, MidiDevice.CreationContext.RemovedDevice)));
		}

		private void OnOutputDeviceAdded(object sender, IntPtr info)
		{
			this.DeviceAdded?.Invoke(this, new DeviceAddedRemovedEventArgs(new OutputDevice(info, MidiDevice.CreationContext.AddedDevice)));
		}

		private void OnOutputDeviceRemoved(object sender, IntPtr info)
		{
			this.DeviceRemoved?.Invoke(this, new DeviceAddedRemovedEventArgs(new OutputDevice(info, MidiDevice.CreationContext.RemovedDevice)));
		}
	}
	public sealed class ErrorOccurredEventArgs : EventArgs
	{
		public Exception Exception { get; }

		internal ErrorOccurredEventArgs(Exception exception)
		{
			Exception = exception;
		}
	}
	public interface IInputDevice
	{
		bool IsListeningForEvents { get; }

		event EventHandler<MidiEventReceivedEventArgs> EventReceived;

		void StartEventsListening();

		void StopEventsListening();
	}
	public sealed class InputDevice : MidiDevice, IInputDevice
	{
		private const int SysExBufferSize = 2048;

		private const int ChannelParametersBufferSize = 2;

		private static readonly int MidiTimeCodeComponentsCount = Enum.GetValues(typeof(MidiTimeCodeComponent)).Length;

		private static InputDeviceProperty[] _supportedProperties;

		private readonly BytesToMidiEventConverter _bytesToMidiEventConverter = new BytesToMidiEventConverter(2);

		private InputDeviceApi.Callback_Win _callback_Win;

		private InputDeviceApi.Callback_Mac _callback_Mac;

		private readonly byte[] _channelParametersBuffer = new byte[2];

		private readonly Dictionary<MidiTimeCodeComponent, FourBitNumber> _midiTimeCodeComponents = new Dictionary<MidiTimeCodeComponent, FourBitNumber>();

		private readonly CommonApi.API_TYPE _apiType;

		private readonly int _hashCode;

		public override string Name
		{
			get
			{
				MidiDevice.EnsureSessionIsCreated();
				EnsureDeviceIsNotRemoved();
				NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_GetDeviceName(_info, out var name));
				return name;
			}
		}

		public bool RaiseMidiTimeCodeReceived { get; set; } = true;

		public bool IsListeningForEvents { get; private set; }

		public SilentNoteOnPolicy SilentNoteOnPolicy
		{
			get
			{
				return _bytesToMidiEventConverter.SilentNoteOnPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_bytesToMidiEventConverter.SilentNoteOnPolicy = value;
			}
		}

		public event EventHandler<MidiEventReceivedEventArgs> EventReceived;

		public event EventHandler<MidiTimeCodeReceivedEventArgs> MidiTimeCodeReceived;

		internal InputDevice(IntPtr info, CreationContext context)
			: base(info, context)
		{
			_apiType = CommonApiProvider.Api.Api_GetApiType();
			_hashCode = InputDeviceApiProvider.Api.Api_GetDeviceHashCode(info);
			_bytesToMidiEventConverter.SilentNoteOnPolicy = SilentNoteOnPolicy.NoteOn;
		}

		~InputDevice()
		{
			Dispose(disposing: false);
		}

		public void StartEventsListening()
		{
			if (!IsListeningForEvents)
			{
				EnsureDeviceIsNotDisposed();
				EnsureDeviceIsNotRemoved();
				MidiDevice.EnsureSessionIsCreated();
				EnsureHandleIsCreated();
				NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_Connect(_handle));
				IsListeningForEvents = true;
			}
		}

		public void StopEventsListening()
		{
			if (IsListeningForEvents && !(_handle == IntPtr.Zero))
			{
				EnsureDeviceIsNotDisposed();
				EnsureDeviceIsNotRemoved();
				MidiDevice.EnsureSessionIsCreated();
				NativeApi.HandleResult(StopEventsListeningSilently());
			}
		}

		public object GetProperty(InputDeviceProperty property)
		{
			ThrowIfArgument.IsInvalidEnumValue("property", property);
			EnsureDeviceIsNotDisposed();
			EnsureDeviceIsNotRemoved();
			MidiDevice.EnsureSessionIsCreated();
			if (!GetSupportedProperties().Contains(property))
			{
				throw new ArgumentException("Property is not supported.", "property");
			}
			InputDeviceApi api = InputDeviceApiProvider.Api;
			switch (property)
			{
			case InputDeviceProperty.Product:
			{
				NativeApi.HandleResult(api.Api_GetDeviceProduct(_info, out var product));
				return product;
			}
			case InputDeviceProperty.Manufacturer:
			{
				NativeApi.HandleResult(api.Api_GetDeviceManufacturer(_info, out var manufacturer));
				return manufacturer;
			}
			case InputDeviceProperty.DriverVersion:
			{
				NativeApi.HandleResult(api.Api_GetDeviceDriverVersion(_info, out var driverVersion));
				return driverVersion;
			}
			case InputDeviceProperty.UniqueId:
			{
				NativeApi.HandleResult(api.Api_GetDeviceUniqueId(_info, out var uniqueId));
				return uniqueId;
			}
			case InputDeviceProperty.DriverOwner:
			{
				NativeApi.HandleResult(api.Api_GetDeviceDriverOwner(_info, out var driverOwner));
				return driverOwner;
			}
			default:
				throw new NotSupportedException("Property is not supported.");
			}
		}

		public static InputDeviceProperty[] GetSupportedProperties()
		{
			if (_supportedProperties != null)
			{
				return _supportedProperties;
			}
			return _supportedProperties = (from p in Enum.GetValues(typeof(InputDeviceProperty)).OfType<InputDeviceProperty>()
				where InputDeviceApiProvider.Api.Api_IsPropertySupported(p)
				select p).ToArray();
		}

		public static int GetDevicesCount()
		{
			MidiDevice.EnsureSessionIsCreated();
			return InputDeviceApiProvider.Api.Api_GetDevicesCount();
		}

		public static IEnumerable<InputDevice> GetAll()
		{
			MidiDevice.EnsureSessionIsCreated();
			int devicesCount = GetDevicesCount();
			for (int i = 0; i < devicesCount; i++)
			{
				yield return GetByIndex(i);
			}
		}

		public static InputDevice GetByIndex(int index)
		{
			int devicesCount = GetDevicesCount();
			ThrowIfArgument.IsOutOfRange("index", index, 0, devicesCount - 1, "Index is less than zero or greater than devices count minus 1.");
			MidiDevice.EnsureSessionIsCreated();
			NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_GetDeviceInfo(index, out var info));
			return new InputDevice(info, CreationContext.User);
		}

		public static InputDevice GetByName(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Device name");
			MidiDevice.EnsureSessionIsCreated();
			InputDevice inputDevice = GetAll().FirstOrDefault((InputDevice d) => d.Name == name);
			if (inputDevice == null)
			{
				throw new ArgumentException("There is no MIDI input device '" + name + "'.", "name");
			}
			return inputDevice;
		}

		private void OnEventReceived(MidiEvent midiEvent)
		{
			this.EventReceived?.Invoke(this, new MidiEventReceivedEventArgs(midiEvent));
			if (RaiseMidiTimeCodeReceived && midiEvent is MidiTimeCodeEvent midiTimeCodeEvent)
			{
				TryRaiseMidiTimeCodeReceived(midiTimeCodeEvent);
			}
		}

		private void OnMidiTimeCodeReceived(MidiTimeCodeType timeCodeType, int hours, int minutes, int seconds, int frames)
		{
			this.MidiTimeCodeReceived?.Invoke(this, new MidiTimeCodeReceivedEventArgs(timeCodeType, hours, minutes, seconds, frames));
		}

		private void EnsureHandleIsCreated()
		{
			if (!(_handle != IntPtr.Zero))
			{
				IntPtr sessionHandle = MidiDevicesSession.GetSessionHandle();
				switch (_apiType)
				{
				case CommonApi.API_TYPE.API_TYPE_WIN:
					_callback_Win = OnMessage_Win;
					NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_OpenDevice_Win(_info, sessionHandle, _callback_Win, 2048, out _handle));
					break;
				case CommonApi.API_TYPE.API_TYPE_MAC:
					_callback_Mac = OnMessage_Mac;
					NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_OpenDevice_Mac(_info, sessionHandle, _callback_Mac, out _handle));
					break;
				default:
					throw new NotSupportedException($"{_apiType} API is not supported.");
				}
			}
		}

		private void DestroyHandle()
		{
			if (!(_handle == IntPtr.Zero))
			{
				InputDeviceApiProvider.Api.Api_CloseDevice(_handle);
				_handle = IntPtr.Zero;
			}
		}

		private void OnMessage_Win(IntPtr hMidi, NativeApi.MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
		{
			if (IsListeningForEvents && base.IsEnabled)
			{
				switch (wMsg)
				{
				case NativeApi.MidiMessage.MIM_DATA:
				case NativeApi.MidiMessage.MIM_MOREDATA:
					OnShortMessage(dwParam1.ToInt32());
					break;
				case NativeApi.MidiMessage.MIM_LONGDATA:
					OnSysExMessage(dwParam1);
					break;
				case NativeApi.MidiMessage.MIM_ERROR:
					OnInvalidShortEvent(dwParam1.ToInt32());
					break;
				case NativeApi.MidiMessage.MIM_LONGERROR:
					OnInvalidSysExEvent(dwParam1);
					break;
				}
			}
		}

		private void OnMessage_Mac(IntPtr pktlist, IntPtr readProcRefCon, IntPtr srcConnRefCon)
		{
			if (!IsListeningForEvents || !base.IsEnabled)
			{
				return;
			}
			byte[] array = null;
			try
			{
				NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_GetEventData(pktlist, 0, out var data, out var length));
				array = new byte[length];
				Marshal.Copy(data, array, 0, length);
				if (array[0] == 240)
				{
					byte[] array2 = new byte[length - 1];
					Buffer.BlockCopy(array, 1, array2, 0, array2.Length);
					NormalSysExEvent midiEvent = new NormalSysExEvent(array2);
					OnEventReceived(midiEvent);
					return;
				}
				byte? b = null;
				using MemoryStream stream = new MemoryStream(array);
				using MidiReader midiReader = new MidiReader(stream, new ReaderSettings());
				midiReader.Position = 0L;
				while (midiReader.Position < length)
				{
					byte b2 = midiReader.ReadByte();
					if (b2 <= (byte)SevenBitNumber.MaxValue)
					{
						if (!b.HasValue)
						{
							throw new UnexpectedRunningStatusException();
						}
						b2 = b.Value;
						midiReader.Position--;
					}
					b = b2;
					MidiEvent midiEvent2 = EventReaderFactory.GetReader(b2, smfOnly: false).Read(midiReader, _bytesToMidiEventConverter.ReadingSettings, b2);
					OnEventReceived(midiEvent2);
				}
			}
			catch (Exception innerException)
			{
				MidiDeviceException ex = new MidiDeviceException("Failed to parse message.", innerException);
				ex.Data.Add("Data", array);
				OnError(ex);
			}
		}

		private void OnInvalidShortEvent(int message)
		{
			MidiDeviceException ex = new MidiDeviceException("Invalid short event received.");
			ex.Data["StatusByte"] = message.GetFourthByte();
			ex.Data["FirstDataByte"] = message.GetThirdByte();
			ex.Data["SecondDataByte"] = message.GetSecondByte();
			OnError(ex);
		}

		private void OnInvalidSysExEvent(IntPtr headerPointer)
		{
			NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_GetSysExBufferData(headerPointer, out var data, out var size));
			byte[] array = new byte[size];
			Marshal.Copy(data, array, 0, size);
			MidiDeviceException ex = new MidiDeviceException("Invalid system exclusive event received.");
			ex.Data.Add("Data", array);
			OnError(ex);
		}

		private void OnShortMessage(int message)
		{
			try
			{
				byte statusByte = (byte)(message & 0xFF);
				_channelParametersBuffer[0] = (byte)((message >> 8) & 0xFF);
				_channelParametersBuffer[1] = (byte)((message >> 16) & 0xFF);
				MidiEvent midiEvent = _bytesToMidiEventConverter.Convert(statusByte, _channelParametersBuffer);
				OnEventReceived(midiEvent);
			}
			catch (Exception innerException)
			{
				MidiDeviceException ex = new MidiDeviceException("Failed to parse short message.", innerException);
				ex.Data.Add("Message", message);
				OnError(ex);
			}
		}

		private void OnSysExMessage(IntPtr sysExHeaderPointer)
		{
			byte[] array = null;
			try
			{
				NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_GetSysExBufferData(sysExHeaderPointer, out var data, out var size));
				array = new byte[size - 1];
				Marshal.Copy(IntPtr.Add(data, 1), array, 0, array.Length);
				NormalSysExEvent midiEvent = new NormalSysExEvent(array);
				OnEventReceived(midiEvent);
				NativeApi.HandleResult(InputDeviceApiProvider.Api.Api_RenewSysExBuffer(_handle, 2048));
			}
			catch (Exception innerException)
			{
				MidiDeviceException ex = new MidiDeviceException("Failed to parse system exclusive message.", innerException);
				ex.Data.Add("Data", array);
				OnError(ex);
			}
		}

		private void TryRaiseMidiTimeCodeReceived(MidiTimeCodeEvent midiTimeCodeEvent)
		{
			MidiTimeCodeComponent component = midiTimeCodeEvent.Component;
			FourBitNumber componentValue = midiTimeCodeEvent.ComponentValue;
			_midiTimeCodeComponents[component] = componentValue;
			if (_midiTimeCodeComponents.Count == MidiTimeCodeComponentsCount)
			{
				byte frames = DataTypesUtilities.Combine(_midiTimeCodeComponents[MidiTimeCodeComponent.FramesMsb], _midiTimeCodeComponents[MidiTimeCodeComponent.FramesLsb]);
				byte minutes = DataTypesUtilities.Combine(_midiTimeCodeComponents[MidiTimeCodeComponent.MinutesMsb], _midiTimeCodeComponents[MidiTimeCodeComponent.MinutesLsb]);
				byte seconds = DataTypesUtilities.Combine(_midiTimeCodeComponents[MidiTimeCodeComponent.SecondsMsb], _midiTimeCodeComponents[MidiTimeCodeComponent.SecondsLsb]);
				byte num = DataTypesUtilities.Combine(_midiTimeCodeComponents[MidiTimeCodeComponent.HoursMsbAndTimeCodeType], _midiTimeCodeComponents[MidiTimeCodeComponent.HoursLsb]);
				int hours = num & 0x1F;
				MidiTimeCodeType timeCodeType = (MidiTimeCodeType)((num >> 5) & 3);
				OnMidiTimeCodeReceived(timeCodeType, hours, minutes, seconds, frames);
				_midiTimeCodeComponents.Clear();
			}
		}

		private InputDeviceApi.IN_DISCONNECTRESULT StopEventsListeningSilently()
		{
			IsListeningForEvents = false;
			return InputDeviceApiProvider.Api.Api_Disconnect(_handle);
		}

		public static bool operator ==(InputDevice inputDevice1, InputDevice inputDevice2)
		{
			if ((object)inputDevice1 == inputDevice2)
			{
				return true;
			}
			if ((object)inputDevice1 == null || (object)inputDevice2 == null)
			{
				return false;
			}
			return inputDevice1.Equals(inputDevice2);
		}

		public static bool operator !=(InputDevice inputDevice1, InputDevice inputDevice2)
		{
			return !(inputDevice1 == inputDevice2);
		}

		public override bool Equals(object obj)
		{
			InputDevice inputDevice = obj as InputDevice;
			if (inputDevice == null)
			{
				return false;
			}
			if (!CommonApiProvider.Api.Api_CanCompareDevices())
			{
				return _info.Equals(inputDevice._info);
			}
			return InputDeviceApiProvider.Api.Api_AreDevicesEqual(_info, inputDevice._info);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override string ToString()
		{
			string text = base.ToString();
			return "Input device" + (string.IsNullOrWhiteSpace(text) ? string.Empty : (" (" + text + ")"));
		}

		internal override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_bytesToMidiEventConverter.Dispose();
				}
				if (_handle != IntPtr.Zero)
				{
					StopEventsListeningSilently();
					DestroyHandle();
				}
				_disposed = true;
			}
		}
	}
	internal abstract class InputDeviceApi : NativeApi
	{
		public enum IN_GETINFORESULT
		{
			IN_GETINFORESULT_OK,
			IN_GETINFORESULT_BADDEVICEID,
			IN_GETINFORESULT_INVALIDSTRUCTURE,
			IN_GETINFORESULT_NODRIVER,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_GETINFORESULT_NOMEMORY
		}

		public enum IN_OPENRESULT
		{
			IN_OPENRESULT_OK = 0,
			[NativeErrorType(NativeErrorType.InUse)]
			IN_OPENRESULT_ALLOCATED = 1,
			IN_OPENRESULT_BADDEVICEID = 2,
			IN_OPENRESULT_INVALIDFLAG = 3,
			IN_OPENRESULT_INVALIDSTRUCTURE = 4,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_OPENRESULT_NOMEMORY = 5,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_OPENRESULT_PREPAREBUFFER_NOMEMORY = 6,
			IN_OPENRESULT_PREPAREBUFFER_INVALIDHANDLE = 7,
			IN_OPENRESULT_PREPAREBUFFER_INVALIDADDRESS = 8,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_OPENRESULT_ADDBUFFER_NOMEMORY = 9,
			IN_OPENRESULT_ADDBUFFER_STILLPLAYING = 10,
			IN_OPENRESULT_ADDBUFFER_UNPREPARED = 11,
			IN_OPENRESULT_ADDBUFFER_INVALIDHANDLE = 12,
			IN_OPENRESULT_ADDBUFFER_INVALIDSTRUCTURE = 13,
			IN_OPENRESULT_INVALIDCLIENT = 101,
			IN_OPENRESULT_INVALIDPORT = 102,
			IN_OPENRESULT_WRONGTHREAD = 103,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			IN_OPENRESULT_NOTPERMITTED = 104,
			IN_OPENRESULT_UNKNOWNERROR = 105
		}

		public enum IN_CLOSERESULT
		{
			IN_CLOSERESULT_OK,
			IN_CLOSERESULT_RESET_INVALIDHANDLE,
			IN_CLOSERESULT_CLOSE_STILLPLAYING,
			IN_CLOSERESULT_CLOSE_INVALIDHANDLE,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_CLOSERESULT_CLOSE_NOMEMORY,
			IN_CLOSERESULT_UNPREPAREBUFFER_STILLPLAYING,
			IN_CLOSERESULT_UNPREPAREBUFFER_INVALIDSTRUCTURE,
			IN_CLOSERESULT_UNPREPAREBUFFER_INVALIDHANDLE
		}

		public enum IN_RENEWSYSEXBUFFERRESULT
		{
			IN_RENEWSYSEXBUFFERRESULT_OK,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_RENEWSYSEXBUFFERRESULT_PREPAREBUFFER_NOMEMORY,
			IN_RENEWSYSEXBUFFERRESULT_PREPAREBUFFER_INVALIDHANDLE,
			IN_RENEWSYSEXBUFFERRESULT_PREPAREBUFFER_INVALIDADDRESS,
			[NativeErrorType(NativeErrorType.NoMemory)]
			IN_RENEWSYSEXBUFFERRESULT_ADDBUFFER_NOMEMORY,
			IN_RENEWSYSEXBUFFERRESULT_ADDBUFFER_STILLPLAYING,
			IN_RENEWSYSEXBUFFERRESULT_ADDBUFFER_UNPREPARED,
			IN_RENEWSYSEXBUFFERRESULT_ADDBUFFER_INVALIDHANDLE,
			IN_RENEWSYSEXBUFFERRESULT_ADDBUFFER_INVALIDSTRUCTURE,
			IN_RENEWSYSEXBUFFERRESULT_UNPREPAREBUFFER_STILLPLAYING,
			IN_RENEWSYSEXBUFFERRESULT_UNPREPAREBUFFER_INVALIDSTRUCTURE,
			IN_RENEWSYSEXBUFFERRESULT_UNPREPAREBUFFER_INVALIDHANDLE
		}

		public enum IN_CONNECTRESULT
		{
			IN_CONNECTRESULT_OK = 0,
			IN_CONNECTRESULT_INVALIDHANDLE = 1,
			IN_CONNECTRESULT_UNKNOWNERROR = 101,
			IN_CONNECTRESULT_INVALIDPORT = 102,
			IN_CONNECTRESULT_WRONGTHREAD = 103,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			IN_CONNECTRESULT_NOTPERMITTED = 104,
			IN_CONNECTRESULT_UNKNOWNENDPOINT = 105,
			IN_CONNECTRESULT_WRONGENDPOINT = 106
		}

		public enum IN_DISCONNECTRESULT
		{
			IN_DISCONNECTRESULT_OK = 0,
			IN_DISCONNECTRESULT_INVALIDHANDLE = 1,
			IN_DISCONNECTRESULT_UNKNOWNERROR = 101,
			IN_DISCONNECTRESULT_INVALIDPORT = 102,
			IN_DISCONNECTRESULT_WRONGTHREAD = 103,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			IN_DISCONNECTRESULT_NOTPERMITTED = 104,
			IN_DISCONNECTRESULT_UNKNOWNENDPOINT = 105,
			IN_DISCONNECTRESULT_WRONGENDPOINT = 106,
			IN_DISCONNECTRESULT_NOCONNECTION = 107
		}

		public enum IN_GETEVENTDATARESULT
		{
			IN_GETEVENTDATARESULT_OK
		}

		public enum IN_GETSYSEXDATARESULT
		{
			IN_GETSYSEXDATARESULT_OK
		}

		public enum IN_GETPROPERTYRESULT
		{
			IN_GETPROPERTYRESULT_OK = 0,
			IN_GETPROPERTYRESULT_UNKNOWNENDPOINT = 101,
			IN_GETPROPERTYRESULT_TOOLONG = 102,
			IN_GETPROPERTYRESULT_UNKNOWNPROPERTY = 103,
			IN_GETPROPERTYRESULT_UNKNOWNERROR = 104
		}

		public delegate void Callback_Win(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);

		public delegate void Callback_Mac(IntPtr pktlist, IntPtr readProcRefCon, IntPtr srcConnRefCon);

		public abstract int Api_GetDevicesCount();

		public abstract IN_GETINFORESULT Api_GetDeviceInfo(int deviceIndex, out IntPtr info);

		public abstract int Api_GetDeviceHashCode(IntPtr info);

		public abstract bool Api_AreDevicesEqual(IntPtr info1, IntPtr info2);

		public abstract IN_OPENRESULT Api_OpenDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, int sysExBufferSize, out IntPtr handle);

		public abstract IN_OPENRESULT Api_OpenDevice_Mac(IntPtr info, IntPtr sessionHandle, Callback_Mac callback, out IntPtr handle);

		public abstract IN_CLOSERESULT Api_CloseDevice(IntPtr handle);

		public abstract IN_RENEWSYSEXBUFFERRESULT Api_RenewSysExBuffer(IntPtr handle, int size);

		public abstract IN_CONNECTRESULT Api_Connect(IntPtr handle);

		public abstract IN_DISCONNECTRESULT Api_Disconnect(IntPtr handle);

		public abstract IN_GETEVENTDATARESULT Api_GetEventData(IntPtr packetList, int packetIndex, out IntPtr data, out int length);

		public abstract IN_GETSYSEXDATARESULT Api_GetSysExBufferData(IntPtr header, out IntPtr data, out int size);

		public abstract bool Api_IsPropertySupported(InputDeviceProperty property);

		public abstract IN_GETPROPERTYRESULT Api_GetDeviceName(IntPtr info, out string name);

		public abstract IN_GETPROPERTYRESULT Api_GetDeviceManufacturer(IntPtr info, out string manufacturer);

		public abstract IN_GETPROPERTYRESULT Api_GetDeviceProduct(IntPtr info, out string product);

		public abstract IN_GETPROPERTYRESULT Api_GetDeviceDriverVersion(IntPtr info, out int driverVersion);

		public abstract IN_GETPROPERTYRESULT Api_GetDeviceUniqueId(IntPtr info, out int uniqueId);

		public abstract IN_GETPROPERTYRESULT Api_GetDeviceDriverOwner(IntPtr info, out string driverOwner);
	}
	internal sealed class InputDeviceApi32 : InputDeviceApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native32";

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern int GetInputDevicesCount();

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETINFORESULT GetInputDeviceInfo(int deviceIndex, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern int GetInputDeviceHashCode(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern bool AreInputDevicesEqual(IntPtr info1, IntPtr info2);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceName(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceManufacturer(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceProduct(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceDriverVersion(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_OPENRESULT OpenInputDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, int sysExBufferSize, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_OPENRESULT OpenInputDevice_Mac(IntPtr info, IntPtr sessionHandle, Callback_Mac callback, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_CLOSERESULT CloseInputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_RENEWSYSEXBUFFERRESULT RenewInputDeviceSysExBuffer(IntPtr handle, int size);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_CONNECTRESULT ConnectToInputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_DISCONNECTRESULT DisconnectFromInputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETEVENTDATARESULT GetEventDataFromInputDevice(IntPtr packetList, int packetIndex, out IntPtr data, out int length);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETSYSEXDATARESULT GetInputDeviceSysExBufferData(IntPtr header, out IntPtr data, out int size);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern bool IsInputDevicePropertySupported(InputDeviceProperty property);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceUniqueId(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceDriverOwner(IntPtr info, out IntPtr value);

		public override int Api_GetDevicesCount()
		{
			return GetInputDevicesCount();
		}

		public override IN_GETINFORESULT Api_GetDeviceInfo(int deviceIndex, out IntPtr info)
		{
			return GetInputDeviceInfo(deviceIndex, out info);
		}

		public override int Api_GetDeviceHashCode(IntPtr info)
		{
			return GetInputDeviceHashCode(info);
		}

		public override bool Api_AreDevicesEqual(IntPtr info1, IntPtr info2)
		{
			return AreInputDevicesEqual(info1, info2);
		}

		public override IN_OPENRESULT Api_OpenDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, int sysExBufferSize, out IntPtr handle)
		{
			return OpenInputDevice_Win(info, sessionHandle, callback, sysExBufferSize, out handle);
		}

		public override IN_OPENRESULT Api_OpenDevice_Mac(IntPtr info, IntPtr sessionHandle, Callback_Mac callback, out IntPtr handle)
		{
			return OpenInputDevice_Mac(info, sessionHandle, callback, out handle);
		}

		public override IN_CLOSERESULT Api_CloseDevice(IntPtr handle)
		{
			return CloseInputDevice(handle);
		}

		public override IN_RENEWSYSEXBUFFERRESULT Api_RenewSysExBuffer(IntPtr handle, int size)
		{
			return RenewInputDeviceSysExBuffer(handle, size);
		}

		public override IN_CONNECTRESULT Api_Connect(IntPtr handle)
		{
			return ConnectToInputDevice(handle);
		}

		public override IN_DISCONNECTRESULT Api_Disconnect(IntPtr handle)
		{
			return DisconnectFromInputDevice(handle);
		}

		public override IN_GETEVENTDATARESULT Api_GetEventData(IntPtr packetList, int packetIndex, out IntPtr data, out int length)
		{
			return GetEventDataFromInputDevice(packetList, packetIndex, out data, out length);
		}

		public override IN_GETSYSEXDATARESULT Api_GetSysExBufferData(IntPtr header, out IntPtr data, out int size)
		{
			return GetInputDeviceSysExBufferData(header, out data, out size);
		}

		public override bool Api_IsPropertySupported(InputDeviceProperty property)
		{
			return IsInputDevicePropertySupported(property);
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceName(IntPtr info, out string name)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceName = GetInputDeviceName(info, out value);
			name = GetStringFromPointer(value);
			return inputDeviceName;
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceManufacturer(IntPtr info, out string manufacturer)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceManufacturer = GetInputDeviceManufacturer(info, out value);
			manufacturer = GetStringFromPointer(value);
			return inputDeviceManufacturer;
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceProduct(IntPtr info, out string product)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceProduct = GetInputDeviceProduct(info, out value);
			product = GetStringFromPointer(value);
			return inputDeviceProduct;
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceDriverVersion(IntPtr info, out int driverVersion)
		{
			return GetInputDeviceDriverVersion(info, out driverVersion);
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceUniqueId(IntPtr info, out int uniqueId)
		{
			return GetInputDeviceUniqueId(info, out uniqueId);
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceDriverOwner(IntPtr info, out string driverOwner)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceDriverOwner = GetInputDeviceDriverOwner(info, out value);
			driverOwner = GetStringFromPointer(value);
			return inputDeviceDriverOwner;
		}
	}
	internal sealed class InputDeviceApi64 : InputDeviceApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native64";

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern int GetInputDevicesCount();

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETINFORESULT GetInputDeviceInfo(int deviceIndex, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern int GetInputDeviceHashCode(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern bool AreInputDevicesEqual(IntPtr info1, IntPtr info2);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceName(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceManufacturer(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceProduct(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceDriverVersion(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_OPENRESULT OpenInputDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, int sysExBufferSize, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_OPENRESULT OpenInputDevice_Mac(IntPtr info, IntPtr sessionHandle, Callback_Mac callback, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_CLOSERESULT CloseInputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_RENEWSYSEXBUFFERRESULT RenewInputDeviceSysExBuffer(IntPtr handle, int size);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_CONNECTRESULT ConnectToInputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_DISCONNECTRESULT DisconnectFromInputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETEVENTDATARESULT GetEventDataFromInputDevice(IntPtr packetList, int packetIndex, out IntPtr data, out int length);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETSYSEXDATARESULT GetInputDeviceSysExBufferData(IntPtr header, out IntPtr data, out int size);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern bool IsInputDevicePropertySupported(InputDeviceProperty property);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceUniqueId(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IN_GETPROPERTYRESULT GetInputDeviceDriverOwner(IntPtr info, out IntPtr value);

		public override int Api_GetDevicesCount()
		{
			return GetInputDevicesCount();
		}

		public override IN_GETINFORESULT Api_GetDeviceInfo(int deviceIndex, out IntPtr info)
		{
			return GetInputDeviceInfo(deviceIndex, out info);
		}

		public override int Api_GetDeviceHashCode(IntPtr info)
		{
			return GetInputDeviceHashCode(info);
		}

		public override bool Api_AreDevicesEqual(IntPtr info1, IntPtr info2)
		{
			return AreInputDevicesEqual(info1, info2);
		}

		public override IN_OPENRESULT Api_OpenDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, int sysExBufferSize, out IntPtr handle)
		{
			return OpenInputDevice_Win(info, sessionHandle, callback, sysExBufferSize, out handle);
		}

		public override IN_OPENRESULT Api_OpenDevice_Mac(IntPtr info, IntPtr sessionHandle, Callback_Mac callback, out IntPtr handle)
		{
			return OpenInputDevice_Mac(info, sessionHandle, callback, out handle);
		}

		public override IN_CLOSERESULT Api_CloseDevice(IntPtr handle)
		{
			return CloseInputDevice(handle);
		}

		public override IN_RENEWSYSEXBUFFERRESULT Api_RenewSysExBuffer(IntPtr handle, int size)
		{
			return RenewInputDeviceSysExBuffer(handle, size);
		}

		public override IN_CONNECTRESULT Api_Connect(IntPtr handle)
		{
			return ConnectToInputDevice(handle);
		}

		public override IN_DISCONNECTRESULT Api_Disconnect(IntPtr handle)
		{
			return DisconnectFromInputDevice(handle);
		}

		public override IN_GETEVENTDATARESULT Api_GetEventData(IntPtr packetList, int packetIndex, out IntPtr data, out int length)
		{
			return GetEventDataFromInputDevice(packetList, packetIndex, out data, out length);
		}

		public override IN_GETSYSEXDATARESULT Api_GetSysExBufferData(IntPtr header, out IntPtr data, out int size)
		{
			return GetInputDeviceSysExBufferData(header, out data, out size);
		}

		public override bool Api_IsPropertySupported(InputDeviceProperty property)
		{
			return IsInputDevicePropertySupported(property);
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceName(IntPtr info, out string name)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceName = GetInputDeviceName(info, out value);
			name = GetStringFromPointer(value);
			return inputDeviceName;
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceManufacturer(IntPtr info, out string manufacturer)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceManufacturer = GetInputDeviceManufacturer(info, out value);
			manufacturer = GetStringFromPointer(value);
			return inputDeviceManufacturer;
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceProduct(IntPtr info, out string product)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceProduct = GetInputDeviceProduct(info, out value);
			product = GetStringFromPointer(value);
			return inputDeviceProduct;
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceDriverVersion(IntPtr info, out int driverVersion)
		{
			return GetInputDeviceDriverVersion(info, out driverVersion);
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceUniqueId(IntPtr info, out int uniqueId)
		{
			return GetInputDeviceUniqueId(info, out uniqueId);
		}

		public override IN_GETPROPERTYRESULT Api_GetDeviceDriverOwner(IntPtr info, out string driverOwner)
		{
			IntPtr value;
			IN_GETPROPERTYRESULT inputDeviceDriverOwner = GetInputDeviceDriverOwner(info, out value);
			driverOwner = GetStringFromPointer(value);
			return inputDeviceDriverOwner;
		}
	}
	internal static class InputDeviceApiProvider
	{
		private static readonly bool Is64Bit = IntPtr.Size == 8;

		private static InputDeviceApi _api;

		public static InputDeviceApi Api
		{
			get
			{
				if (_api == null)
				{
					_api = (Is64Bit ? ((InputDeviceApi)new InputDeviceApi64()) : ((InputDeviceApi)new InputDeviceApi32()));
				}
				return _api;
			}
		}
	}
	public enum InputDeviceProperty
	{
		Product,
		Manufacturer,
		DriverVersion,
		UniqueId,
		DriverOwner
	}
	public sealed class MidiEventReceivedEventArgs : EventArgs
	{
		public MidiEvent Event { get; }

		public MidiEventReceivedEventArgs(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			Event = midiEvent;
		}
	}
	public sealed class MidiTimeCodeReceivedEventArgs : EventArgs
	{
		public MidiTimeCodeType Format { get; }

		public int Hours { get; }

		public int Minutes { get; }

		public int Seconds { get; }

		public int Frames { get; }

		internal MidiTimeCodeReceivedEventArgs(MidiTimeCodeType timeCodeType, int hours, int minutes, int seconds, int frames)
		{
			Format = timeCodeType;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
			Frames = frames;
		}
	}
	public abstract class MidiDevice : IDisposable
	{
		internal enum CreationContext
		{
			User,
			VirtualDevice,
			RemovedDevice,
			AddedDevice
		}

		private static readonly Dictionary<CreationContext, string> ContextsDescriptions = new Dictionary<CreationContext, string>
		{
			[CreationContext.User] = string.Empty,
			[CreationContext.VirtualDevice] = "subdevice of a virtual device",
			[CreationContext.AddedDevice] = "from 'Device added' notification",
			[CreationContext.RemovedDevice] = "from 'Device removed' notification"
		};

		protected IntPtr _info = IntPtr.Zero;

		protected IntPtr _handle = IntPtr.Zero;

		protected bool _disposed;

		public bool IsEnabled { get; set; } = true;

		public abstract string Name { get; }

		internal CreationContext Context { get; }

		public event EventHandler<ErrorOccurredEventArgs> ErrorOccurred;

		internal MidiDevice(IntPtr info, CreationContext context)
		{
			_info = info;
			Context = context;
		}

		~MidiDevice()
		{
			Dispose(disposing: false);
		}

		protected void EnsureDeviceIsNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("Device is disposed.");
			}
		}

		protected void EnsureDeviceIsNotRemoved()
		{
			if (Context == CreationContext.RemovedDevice)
			{
				throw new InvalidOperationException("Operation can't be performed on removed device.");
			}
		}

		protected void OnError(Exception exception)
		{
			this.ErrorOccurred?.Invoke(this, new ErrorOccurredEventArgs(exception));
		}

		protected static void EnsureSessionIsCreated()
		{
			MidiDevicesSession.GetSessionHandle();
		}

		public override string ToString()
		{
			return ContextsDescriptions[Context];
		}

		public void Dispose()
		{
			if (Context == CreationContext.VirtualDevice)
			{
				throw new InvalidOperationException("Disposing of a subdevice of a virtual device is prohibited.");
			}
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		internal abstract void Dispose(bool disposing);
	}
	public sealed class MidiDeviceException : MidiException
	{
		public int? ErrorCode { get; }

		public MidiDeviceException()
		{
		}

		public MidiDeviceException(string message)
			: base(message)
		{
		}

		public MidiDeviceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		public MidiDeviceException(string message, int errorCode)
			: this(message)
		{
			ErrorCode = errorCode;
		}
	}
	internal abstract class NativeApi
	{
		[AttributeUsage(AttributeTargets.Field)]
		public sealed class NativeErrorTypeAttribute : Attribute
		{
			public NativeErrorType ErrorType { get; }

			public NativeErrorTypeAttribute(NativeErrorType errorType)
			{
				ErrorType = errorType;
			}
		}

		public enum NativeErrorType
		{
			NoMemory,
			InUse,
			NotPermitted,
			Busy
		}

		public enum MidiMessage
		{
			MIM_CLOSE = 962,
			MIM_DATA = 963,
			MIM_ERROR = 965,
			MIM_LONGDATA = 964,
			MIM_LONGERROR = 966,
			MIM_MOREDATA = 972,
			MIM_OPEN = 961,
			MOM_CLOSE = 968,
			MOM_DONE = 969,
			MOM_OPEN = 967,
			MOM_POSITIONCB = 970
		}

		protected const string LibraryName32 = "Melanchall_DryWetMidi_Native32";

		protected const string LibraryName64 = "Melanchall_DryWetMidi_Native64";

		private static readonly Dictionary<NativeErrorType, string> ErrorsDescriptions = new Dictionary<NativeErrorType, string>
		{
			[NativeErrorType.NoMemory] = "There is no memory in the system to complete the operation",
			[NativeErrorType.InUse] = "Device is already in use",
			[NativeErrorType.NotPermitted] = "The process doesn’t have privileges for the requested operation",
			[NativeErrorType.Busy] = "The hardware is busy with other data"
		};

		protected string GetStringFromPointer(IntPtr stringPointer)
		{
			if (!(stringPointer != IntPtr.Zero))
			{
				return string.Empty;
			}
			return Marshal.PtrToStringAnsi(stringPointer);
		}

		public static void HandleResult<T>(T result)
		{
			int num = Convert.ToInt32(result);
			if (num == 0)
			{
				return;
			}
			string arg = ((typeof(T).GetFields(BindingFlags.Static | BindingFlags.Public).First((FieldInfo f) => f.GetValue(null).Equals(result)).GetCustomAttributes(typeof(NativeErrorTypeAttribute))
				.FirstOrDefault() is NativeErrorTypeAttribute nativeErrorTypeAttribute) ? ErrorsDescriptions[nativeErrorTypeAttribute.ErrorType] : "Internal error");
			throw new MidiDeviceException($"{arg} ({result}).", num);
		}
	}
	public interface IOutputDevice
	{
		event EventHandler<MidiEventSentEventArgs> EventSent;

		void PrepareForEventsSending();

		void SendEvent(MidiEvent midiEvent);
	}
	public sealed class MidiEventSentEventArgs : EventArgs
	{
		public MidiEvent Event { get; }

		public MidiEventSentEventArgs(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			Event = midiEvent;
		}
	}
	public sealed class OutputDevice : MidiDevice, IOutputDevice
	{
		private const int ShortEventBufferSize = 3;

		private static readonly IEventWriter ChannelEventWriter = new ChannelEventWriter();

		private static readonly IEventWriter SystemRealTimeEventWriter = new SystemRealTimeEventWriter();

		private static OutputDeviceProperty[] _supportedProperties;

		private readonly MidiEventToBytesConverter _midiEventToBytesConverter = new MidiEventToBytesConverter(3);

		private readonly BytesToMidiEventConverter _bytesToMidiEventConverter = new BytesToMidiEventConverter();

		private OutputDeviceApi.Callback_Win _callback;

		private readonly CommonApi.API_TYPE _apiType;

		private readonly int _hashCode;

		public override string Name
		{
			get
			{
				MidiDevice.EnsureSessionIsCreated();
				EnsureDeviceIsNotRemoved();
				NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_GetDeviceName(_info, out var name));
				return name;
			}
		}

		public event EventHandler<MidiEventSentEventArgs> EventSent;

		internal OutputDevice(IntPtr info, CreationContext context)
			: base(info, context)
		{
			_apiType = CommonApiProvider.Api.Api_GetApiType();
			_hashCode = OutputDeviceApiProvider.Api.Api_GetDeviceHashCode(info);
		}

		~OutputDevice()
		{
			Dispose(disposing: false);
		}

		public void SendEvent(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			if (base.IsEnabled)
			{
				EnsureDeviceIsNotDisposed();
				EnsureDeviceIsNotRemoved();
				MidiDevice.EnsureSessionIsCreated();
				EnsureHandleIsCreated();
				if (midiEvent is ChannelEvent || midiEvent is SystemCommonEvent || midiEvent is SystemRealTimeEvent)
				{
					int message = PackShortEvent(midiEvent);
					NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_SendShortEvent(_handle, message));
					OnEventSent(midiEvent);
				}
				else if (midiEvent is SysExEvent sysExEvent)
				{
					SendSysExEvent(sysExEvent);
				}
			}
		}

		public void TurnAllNotesOff()
		{
			EnsureDeviceIsNotDisposed();
			EnsureDeviceIsNotRemoved();
			MidiDevice.EnsureSessionIsCreated();
			EnsureHandleIsCreated();
			foreach (NoteOffEvent item in from channel in FourBitNumber.Values
				from noteNumber in SevenBitNumber.Values
				select new NoteOffEvent(noteNumber, SevenBitNumber.MinValue)
				{
					Channel = channel
				})
			{
				SendEvent(item);
			}
		}

		public void PrepareForEventsSending()
		{
			MidiDevice.EnsureSessionIsCreated();
			EnsureHandleIsCreated();
		}

		public static int GetDevicesCount()
		{
			MidiDevice.EnsureSessionIsCreated();
			return OutputDeviceApiProvider.Api.Api_GetDevicesCount();
		}

		public object GetProperty(OutputDeviceProperty property)
		{
			ThrowIfArgument.IsInvalidEnumValue("property", property);
			EnsureDeviceIsNotDisposed();
			EnsureDeviceIsNotRemoved();
			MidiDevice.EnsureSessionIsCreated();
			if (!GetSupportedProperties().Contains(property))
			{
				throw new ArgumentException("Property is not supported.", "property");
			}
			OutputDeviceApi api = OutputDeviceApiProvider.Api;
			switch (property)
			{
			case OutputDeviceProperty.Product:
			{
				NativeApi.HandleResult(api.Api_GetDeviceProduct(_info, out var manufacturer));
				return manufacturer;
			}
			case OutputDeviceProperty.Manufacturer:
			{
				NativeApi.HandleResult(api.Api_GetDeviceManufacturer(_info, out var manufacturer2));
				return manufacturer2;
			}
			case OutputDeviceProperty.DriverVersion:
			{
				NativeApi.HandleResult(api.Api_GetDeviceDriverVersion(_info, out var driverVersion));
				return driverVersion;
			}
			case OutputDeviceProperty.Technology:
			{
				NativeApi.HandleResult(api.Api_GetDeviceTechnology(_info, out var deviceType));
				return deviceType;
			}
			case OutputDeviceProperty.UniqueId:
			{
				NativeApi.HandleResult(api.Api_GetDeviceUniqueId(_info, out var uniqueId));
				return uniqueId;
			}
			case OutputDeviceProperty.VoicesNumber:
			{
				NativeApi.HandleResult(api.Api_GetDeviceVoicesNumber(_info, out var voicesNumber));
				return voicesNumber;
			}
			case OutputDeviceProperty.NotesNumber:
			{
				NativeApi.HandleResult(api.Api_GetDeviceNotesNumber(_info, out var notesNumber));
				return notesNumber;
			}
			case OutputDeviceProperty.Channels:
			{
				NativeApi.HandleResult(api.Api_GetDeviceChannelsMask(_info, out var channelsMask));
				return (from channel in FourBitNumber.Values
					let isChannelSupported = (channelsMask >> (int)(byte)channel) & 1
					where isChannelSupported == 1
					select channel).ToArray();
			}
			case OutputDeviceProperty.Options:
			{
				NativeApi.HandleResult(api.Api_GetDeviceOptions(_info, out var option));
				return option;
			}
			case OutputDeviceProperty.DriverOwner:
			{
				NativeApi.HandleResult(api.Api_GetDeviceDriverOwner(_info, out var driverOwner));
				return driverOwner;
			}
			default:
				throw new NotSupportedException("Property is not supported.");
			}
		}

		public static OutputDeviceProperty[] GetSupportedProperties()
		{
			if (_supportedProperties != null)
			{
				return _supportedProperties;
			}
			return _supportedProperties = (from p in Enum.GetValues(typeof(OutputDeviceProperty)).OfType<OutputDeviceProperty>()
				where OutputDeviceApiProvider.Api.Api_IsPropertySupported(p)
				select p).ToArray();
		}

		public static IEnumerable<OutputDevice> GetAll()
		{
			MidiDevice.EnsureSessionIsCreated();
			int devicesCount = GetDevicesCount();
			for (int i = 0; i < devicesCount; i++)
			{
				yield return GetByIndex(i);
			}
		}

		public static OutputDevice GetByIndex(int index)
		{
			int devicesCount = GetDevicesCount();
			ThrowIfArgument.IsOutOfRange("index", index, 0, devicesCount - 1, "Index is less than zero or greater than devices count minus 1.");
			MidiDevice.EnsureSessionIsCreated();
			NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_GetDeviceInfo(index, out var info));
			return new OutputDevice(info, CreationContext.User);
		}

		public static OutputDevice GetByName(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Device name");
			MidiDevice.EnsureSessionIsCreated();
			OutputDevice outputDevice = GetAll().FirstOrDefault((OutputDevice d) => d.Name == name);
			if (outputDevice == null)
			{
				throw new ArgumentException("There is no output MIDI device '" + name + "'.", "name");
			}
			return outputDevice;
		}

		private void EnsureHandleIsCreated()
		{
			if (!(_handle != IntPtr.Zero))
			{
				IntPtr sessionHandle = MidiDevicesSession.GetSessionHandle();
				switch (_apiType)
				{
				case CommonApi.API_TYPE.API_TYPE_WIN:
					_callback = OnMessage;
					NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_OpenDevice_Win(_info, sessionHandle, _callback, out _handle));
					break;
				case CommonApi.API_TYPE.API_TYPE_MAC:
					NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_OpenDevice_Mac(_info, sessionHandle, out _handle));
					break;
				default:
					throw new NotSupportedException($"{_apiType} API is not supported.");
				}
			}
		}

		private void DestroyHandle()
		{
			if (!(_handle == IntPtr.Zero))
			{
				OutputDeviceApiProvider.Api.Api_CloseDevice(_handle);
				_handle = IntPtr.Zero;
			}
		}

		private void SendSysExEvent(SysExEvent sysExEvent)
		{
			byte[] data = sysExEvent.Data;
			if (data != null && data.Any())
			{
				switch (_apiType)
				{
				case CommonApi.API_TYPE.API_TYPE_WIN:
					SendSysExEventData_Win(data);
					break;
				case CommonApi.API_TYPE.API_TYPE_MAC:
					SendSysExEventData_Mac(data);
					OnEventSent(sysExEvent);
					break;
				default:
					throw new NotSupportedException($"{_apiType} API is not supported.");
				}
			}
		}

		private void SendSysExEventData_Win(byte[] data)
		{
			int num = data.Length + 1;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.WriteByte(intPtr, 240);
			Marshal.Copy(data, 0, IntPtr.Add(intPtr, 1), data.Length);
			NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_SendSysExEvent_Win(_handle, intPtr, num));
		}

		private void SendSysExEventData_Mac(byte[] data)
		{
			byte[] array = new byte[data.Length + 1];
			array[0] = 240;
			Buffer.BlockCopy(data, 0, array, 1, data.Length);
			NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_SendSysExEvent_Mac(_handle, array, (ushort)array.Length));
		}

		private int PackShortEvent(MidiEvent midiEvent)
		{
			if (midiEvent is ChannelEvent channelEvent)
			{
				return ChannelEventWriter.GetStatusByte(channelEvent) | (channelEvent._dataByte1 << 8) | (channelEvent._dataByte2 << 16);
			}
			if (midiEvent is SystemRealTimeEvent midiEvent2)
			{
				return SystemRealTimeEventWriter.GetStatusByte(midiEvent2);
			}
			byte[] array = _midiEventToBytesConverter.Convert(midiEvent, 3);
			return array[0] + (array[1] << 8) + (array[2] << 16);
		}

		private void OnMessage(IntPtr hMidi, NativeApi.MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2)
		{
			if (wMsg == NativeApi.MidiMessage.MOM_DONE)
			{
				OnSysExEventSent(dwParam1);
			}
		}

		private void OnSysExEventSent(IntPtr sysExHeaderPointer)
		{
			byte[] array = null;
			try
			{
				NativeApi.HandleResult(OutputDeviceApiProvider.Api.Api_GetSysExBufferData(_handle, sysExHeaderPointer, out var data, out var size));
				array = new byte[size - 1];
				Marshal.Copy(IntPtr.Add(data, 1), array, 0, array.Length);
				Marshal.FreeHGlobal(data);
				NormalSysExEvent midiEvent = new NormalSysExEvent(array);
				OnEventSent(midiEvent);
			}
			catch (Exception innerException)
			{
				MidiDeviceException ex = new MidiDeviceException("Failed to parse sent system exclusive event.", innerException);
				ex.Data.Add("Data", array);
				OnError(ex);
			}
		}

		private void OnEventSent(MidiEvent midiEvent)
		{
			this.EventSent?.Invoke(this, new MidiEventSentEventArgs(midiEvent));
		}

		public static bool operator ==(OutputDevice outputDevice1, OutputDevice outputDevice2)
		{
			if ((object)outputDevice1 == outputDevice2)
			{
				return true;
			}
			if ((object)outputDevice1 == null || (object)outputDevice2 == null)
			{
				return false;
			}
			return outputDevice1.Equals(outputDevice2);
		}

		public static bool operator !=(OutputDevice outputDevice1, OutputDevice outputDevice2)
		{
			return !(outputDevice1 == outputDevice2);
		}

		public override bool Equals(object obj)
		{
			OutputDevice outputDevice = obj as OutputDevice;
			if (outputDevice == null)
			{
				return false;
			}
			if (!CommonApiProvider.Api.Api_CanCompareDevices())
			{
				return _info.Equals(outputDevice._info);
			}
			return OutputDeviceApiProvider.Api.Api_AreDevicesEqual(_info, outputDevice._info);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public override string ToString()
		{
			string text = base.ToString();
			return "Output device" + (string.IsNullOrWhiteSpace(text) ? string.Empty : (" (" + text + ")"));
		}

		internal override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_midiEventToBytesConverter.Dispose();
					_bytesToMidiEventConverter.Dispose();
				}
				DestroyHandle();
				_disposed = true;
			}
		}
	}
	internal abstract class OutputDeviceApi : NativeApi
	{
		public enum OUT_GETINFORESULT
		{
			OUT_GETINFORESULT_OK,
			OUT_GETINFORESULT_BADDEVICEID,
			OUT_GETINFORESULT_INVALIDSTRUCTURE,
			OUT_GETINFORESULT_NODRIVER,
			[NativeErrorType(NativeErrorType.NoMemory)]
			OUT_GETINFORESULT_NOMEMORY
		}

		public enum OUT_OPENRESULT
		{
			OUT_OPENRESULT_OK = 0,
			[NativeErrorType(NativeErrorType.InUse)]
			OUT_OPENRESULT_ALLOCATED = 1,
			OUT_OPENRESULT_BADDEVICEID = 2,
			OUT_OPENRESULT_INVALIDFLAG = 3,
			OUT_OPENRESULT_INVALIDSTRUCTURE = 4,
			[NativeErrorType(NativeErrorType.NoMemory)]
			OUT_OPENRESULT_NOMEMORY = 5,
			OUT_OPENRESULT_INVALIDCLIENT = 101,
			OUT_OPENRESULT_INVALIDPORT = 102,
			OUT_OPENRESULT_WRONGTHREAD = 103,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			OUT_OPENRESULT_NOTPERMITTED = 104,
			OUT_OPENRESULT_UNKNOWNERROR = 105
		}

		public enum OUT_CLOSERESULT
		{
			OUT_CLOSERESULT_OK,
			OUT_CLOSERESULT_RESET_INVALIDHANDLE,
			OUT_CLOSERESULT_CLOSE_STILLPLAYING,
			OUT_CLOSERESULT_CLOSE_INVALIDHANDLE,
			[NativeErrorType(NativeErrorType.NoMemory)]
			OUT_CLOSERESULT_CLOSE_NOMEMORY
		}

		public enum OUT_SENDSHORTRESULT
		{
			OUT_SENDSHORTRESULT_OK = 0,
			OUT_SENDSHORTRESULT_BADOPENMODE = 1,
			[NativeErrorType(NativeErrorType.Busy)]
			OUT_SENDSHORTRESULT_NOTREADY = 2,
			OUT_SENDSHORTRESULT_INVALIDHANDLE = 3,
			OUT_SENDSHORTRESULT_INVALIDCLIENT = 101,
			OUT_SENDSHORTRESULT_INVALIDPORT = 102,
			OUT_SENDSHORTRESULT_WRONGENDPOINT = 103,
			OUT_SENDSHORTRESULT_UNKNOWNENDPOINT = 104,
			OUT_SENDSHORTRESULT_COMMUNICATIONERROR = 105,
			OUT_SENDSHORTRESULT_SERVERSTARTERROR = 106,
			OUT_SENDSHORTRESULT_WRONGTHREAD = 107,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			OUT_SENDSHORTRESULT_NOTPERMITTED = 108,
			OUT_SENDSHORTRESULT_UNKNOWNERROR = 109
		}

		public enum OUT_SENDSYSEXRESULT
		{
			OUT_SENDSYSEXRESULT_OK = 0,
			OUT_SENDSYSEXRESULT_PREPAREBUFFER_INVALIDHANDLE = 1,
			OUT_SENDSYSEXRESULT_PREPAREBUFFER_INVALIDADDRESS = 2,
			[NativeErrorType(NativeErrorType.NoMemory)]
			OUT_SENDSYSEXRESULT_PREPAREBUFFER_NOMEMORY = 3,
			[NativeErrorType(NativeErrorType.Busy)]
			OUT_SENDSYSEXRESULT_NOTREADY = 4,
			OUT_SENDSYSEXRESULT_UNPREPARED = 5,
			OUT_SENDSYSEXRESULT_INVALIDHANDLE = 6,
			OUT_SENDSYSEXRESULT_INVALIDSTRUCTURE = 7,
			OUT_SENDSYSEXRESULT_INVALIDCLIENT = 101,
			OUT_SENDSYSEXRESULT_INVALIDPORT = 102,
			OUT_SENDSYSEXRESULT_WRONGENDPOINT = 103,
			OUT_SENDSYSEXRESULT_UNKNOWNENDPOINT = 104,
			OUT_SENDSYSEXRESULT_COMMUNICATIONERROR = 105,
			OUT_SENDSYSEXRESULT_SERVERSTARTERROR = 106,
			OUT_SENDSYSEXRESULT_WRONGTHREAD = 107,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			OUT_SENDSYSEXRESULT_NOTPERMITTED = 108,
			OUT_SENDSYSEXRESULT_UNKNOWNERROR = 109
		}

		public enum OUT_GETSYSEXDATARESULT
		{
			OUT_GETSYSEXDATARESULT_OK,
			OUT_GETSYSEXDATARESULT_STILLPLAYING,
			OUT_GETSYSEXDATARESULT_INVALIDSTRUCTURE,
			OUT_GETSYSEXDATARESULT_INVALIDHANDLE
		}

		public enum OUT_GETPROPERTYRESULT
		{
			OUT_GETPROPERTYRESULT_OK = 0,
			OUT_GETPROPERTYRESULT_UNKNOWNENDPOINT = 101,
			OUT_GETPROPERTYRESULT_TOOLONG = 102,
			OUT_GETPROPERTYRESULT_UNKNOWNPROPERTY = 103,
			OUT_GETPROPERTYRESULT_UNKNOWNERROR = 104
		}

		public delegate void Callback_Win(IntPtr hMidi, MidiMessage wMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);

		public abstract int Api_GetDevicesCount();

		public abstract OUT_GETINFORESULT Api_GetDeviceInfo(int deviceIndex, out IntPtr info);

		public abstract int Api_GetDeviceHashCode(IntPtr info);

		public abstract bool Api_AreDevicesEqual(IntPtr info1, IntPtr info2);

		public abstract OUT_OPENRESULT Api_OpenDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, out IntPtr handle);

		public abstract OUT_OPENRESULT Api_OpenDevice_Mac(IntPtr info, IntPtr sessionHandle, out IntPtr handle);

		public abstract OUT_CLOSERESULT Api_CloseDevice(IntPtr handle);

		public abstract OUT_SENDSHORTRESULT Api_SendShortEvent(IntPtr handle, int message);

		public abstract OUT_SENDSYSEXRESULT Api_SendSysExEvent_Mac(IntPtr handle, byte[] data, ushort dataSize);

		public abstract OUT_SENDSYSEXRESULT Api_SendSysExEvent_Win(IntPtr handle, IntPtr data, int size);

		public abstract OUT_GETSYSEXDATARESULT Api_GetSysExBufferData(IntPtr handle, IntPtr header, out IntPtr data, out int size);

		public abstract bool Api_IsPropertySupported(OutputDeviceProperty property);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceName(IntPtr info, out string name);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceManufacturer(IntPtr info, out string manufacturer);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceProduct(IntPtr info, out string manufacturer);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceDriverVersion(IntPtr info, out int driverVersion);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceTechnology(IntPtr info, out OutputDeviceTechnology deviceType);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceUniqueId(IntPtr info, out int uniqueId);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceVoicesNumber(IntPtr info, out int voicesNumber);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceNotesNumber(IntPtr info, out int notesNumber);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceChannelsMask(IntPtr info, out int channelsMask);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceOptions(IntPtr info, out OutputDeviceOption option);

		public abstract OUT_GETPROPERTYRESULT Api_GetDeviceDriverOwner(IntPtr info, out string driverOwner);
	}
	internal sealed class OutputDeviceApi32 : OutputDeviceApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native32";

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern int GetOutputDevicesCount();

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETINFORESULT GetOutputDeviceInfo(int deviceIndex, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern int GetOutputDeviceHashCode(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern bool AreOutputDevicesEqual(IntPtr info1, IntPtr info2);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceName(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceManufacturer(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceProduct(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceDriverVersion(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_OPENRESULT OpenOutputDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_OPENRESULT OpenOutputDevice_Mac(IntPtr info, IntPtr sessionHandle, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_CLOSERESULT CloseOutputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_SENDSHORTRESULT SendShortEventToOutputDevice(IntPtr handle, int message);

		[DllImport("Melanchall_DryWetMidi_Native32", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		private static extern OUT_SENDSYSEXRESULT SendSysExEventToOutputDevice_Mac(IntPtr handle, byte[] data, ushort dataSize);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_SENDSYSEXRESULT SendSysExEventToOutputDevice_Win(IntPtr handle, IntPtr data, int size);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETSYSEXDATARESULT GetOutputDeviceSysExBufferData(IntPtr handle, IntPtr header, out IntPtr data, out int size);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern bool IsOutputDevicePropertySupported(OutputDeviceProperty property);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceTechnology(IntPtr info, out OutputDeviceTechnology value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceUniqueId(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceVoicesNumber(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceNotesNumber(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceChannelsMask(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceOptions(IntPtr info, out OutputDeviceOption value);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceDriverOwner(IntPtr info, out IntPtr value);

		public override int Api_GetDevicesCount()
		{
			return GetOutputDevicesCount();
		}

		public override OUT_GETINFORESULT Api_GetDeviceInfo(int deviceIndex, out IntPtr info)
		{
			return GetOutputDeviceInfo(deviceIndex, out info);
		}

		public override int Api_GetDeviceHashCode(IntPtr info)
		{
			return GetOutputDeviceHashCode(info);
		}

		public override bool Api_AreDevicesEqual(IntPtr info1, IntPtr info2)
		{
			return AreOutputDevicesEqual(info1, info2);
		}

		public override OUT_OPENRESULT Api_OpenDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, out IntPtr handle)
		{
			return OpenOutputDevice_Win(info, sessionHandle, callback, out handle);
		}

		public override OUT_OPENRESULT Api_OpenDevice_Mac(IntPtr info, IntPtr sessionHandle, out IntPtr handle)
		{
			return OpenOutputDevice_Mac(info, sessionHandle, out handle);
		}

		public override OUT_CLOSERESULT Api_CloseDevice(IntPtr handle)
		{
			return CloseOutputDevice(handle);
		}

		public override OUT_SENDSHORTRESULT Api_SendShortEvent(IntPtr handle, int message)
		{
			return SendShortEventToOutputDevice(handle, message);
		}

		public override OUT_SENDSYSEXRESULT Api_SendSysExEvent_Mac(IntPtr handle, byte[] data, ushort dataSize)
		{
			return SendSysExEventToOutputDevice_Mac(handle, data, dataSize);
		}

		public override OUT_SENDSYSEXRESULT Api_SendSysExEvent_Win(IntPtr handle, IntPtr data, int size)
		{
			return SendSysExEventToOutputDevice_Win(handle, data, size);
		}

		public override OUT_GETSYSEXDATARESULT Api_GetSysExBufferData(IntPtr handle, IntPtr header, out IntPtr data, out int size)
		{
			return GetOutputDeviceSysExBufferData(handle, header, out data, out size);
		}

		public override bool Api_IsPropertySupported(OutputDeviceProperty property)
		{
			return IsOutputDevicePropertySupported(property);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceName(IntPtr info, out string name)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceName = GetOutputDeviceName(info, out value);
			name = GetStringFromPointer(value);
			return outputDeviceName;
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceManufacturer(IntPtr info, out string manufacturer)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceManufacturer = GetOutputDeviceManufacturer(info, out value);
			manufacturer = GetStringFromPointer(value);
			return outputDeviceManufacturer;
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceProduct(IntPtr info, out string product)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceProduct = GetOutputDeviceProduct(info, out value);
			product = GetStringFromPointer(value);
			return outputDeviceProduct;
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceDriverVersion(IntPtr info, out int driverVersion)
		{
			return GetOutputDeviceDriverVersion(info, out driverVersion);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceTechnology(IntPtr info, out OutputDeviceTechnology deviceType)
		{
			return GetOutputDeviceTechnology(info, out deviceType);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceUniqueId(IntPtr info, out int uniqueId)
		{
			return GetOutputDeviceUniqueId(info, out uniqueId);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceVoicesNumber(IntPtr info, out int voicesNumber)
		{
			return GetOutputDeviceVoicesNumber(info, out voicesNumber);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceNotesNumber(IntPtr info, out int notesNumber)
		{
			return GetOutputDeviceNotesNumber(info, out notesNumber);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceChannelsMask(IntPtr info, out int channelsMask)
		{
			return GetOutputDeviceChannelsMask(info, out channelsMask);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceOptions(IntPtr info, out OutputDeviceOption option)
		{
			return GetOutputDeviceOptions(info, out option);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceDriverOwner(IntPtr info, out string driverOwner)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceDriverOwner = GetOutputDeviceDriverOwner(info, out value);
			driverOwner = GetStringFromPointer(value);
			return outputDeviceDriverOwner;
		}
	}
	internal sealed class OutputDeviceApi64 : OutputDeviceApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native64";

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern int GetOutputDevicesCount();

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETINFORESULT GetOutputDeviceInfo(int deviceIndex, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern int GetOutputDeviceHashCode(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern bool AreOutputDevicesEqual(IntPtr info1, IntPtr info2);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceName(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceManufacturer(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceProduct(IntPtr info, out IntPtr value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceDriverVersion(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_OPENRESULT OpenOutputDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_OPENRESULT OpenOutputDevice_Mac(IntPtr info, IntPtr sessionHandle, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_CLOSERESULT CloseOutputDevice(IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_SENDSHORTRESULT SendShortEventToOutputDevice(IntPtr handle, int message);

		[DllImport("Melanchall_DryWetMidi_Native64", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
		private static extern OUT_SENDSYSEXRESULT SendSysExEventToOutputDevice_Mac(IntPtr handle, byte[] data, ushort dataSize);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_SENDSYSEXRESULT SendSysExEventToOutputDevice_Win(IntPtr handle, IntPtr data, int size);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETSYSEXDATARESULT GetOutputDeviceSysExBufferData(IntPtr handle, IntPtr header, out IntPtr data, out int size);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern bool IsOutputDevicePropertySupported(OutputDeviceProperty property);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceTechnology(IntPtr info, out OutputDeviceTechnology value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceUniqueId(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceVoicesNumber(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceNotesNumber(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceChannelsMask(IntPtr info, out int value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceOptions(IntPtr info, out OutputDeviceOption value);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern OUT_GETPROPERTYRESULT GetOutputDeviceDriverOwner(IntPtr info, out IntPtr value);

		public override int Api_GetDevicesCount()
		{
			return GetOutputDevicesCount();
		}

		public override OUT_GETINFORESULT Api_GetDeviceInfo(int deviceIndex, out IntPtr info)
		{
			return GetOutputDeviceInfo(deviceIndex, out info);
		}

		public override int Api_GetDeviceHashCode(IntPtr info)
		{
			return GetOutputDeviceHashCode(info);
		}

		public override bool Api_AreDevicesEqual(IntPtr info1, IntPtr info2)
		{
			return AreOutputDevicesEqual(info1, info2);
		}

		public override OUT_OPENRESULT Api_OpenDevice_Win(IntPtr info, IntPtr sessionHandle, Callback_Win callback, out IntPtr handle)
		{
			return OpenOutputDevice_Win(info, sessionHandle, callback, out handle);
		}

		public override OUT_OPENRESULT Api_OpenDevice_Mac(IntPtr info, IntPtr sessionHandle, out IntPtr handle)
		{
			return OpenOutputDevice_Mac(info, sessionHandle, out handle);
		}

		public override OUT_CLOSERESULT Api_CloseDevice(IntPtr handle)
		{
			return CloseOutputDevice(handle);
		}

		public override OUT_SENDSHORTRESULT Api_SendShortEvent(IntPtr handle, int message)
		{
			return SendShortEventToOutputDevice(handle, message);
		}

		public override OUT_SENDSYSEXRESULT Api_SendSysExEvent_Mac(IntPtr handle, byte[] data, ushort dataSize)
		{
			return SendSysExEventToOutputDevice_Mac(handle, data, dataSize);
		}

		public override OUT_SENDSYSEXRESULT Api_SendSysExEvent_Win(IntPtr handle, IntPtr data, int size)
		{
			return SendSysExEventToOutputDevice_Win(handle, data, size);
		}

		public override OUT_GETSYSEXDATARESULT Api_GetSysExBufferData(IntPtr handle, IntPtr header, out IntPtr data, out int size)
		{
			return GetOutputDeviceSysExBufferData(handle, header, out data, out size);
		}

		public override bool Api_IsPropertySupported(OutputDeviceProperty property)
		{
			return IsOutputDevicePropertySupported(property);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceName(IntPtr info, out string name)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceName = GetOutputDeviceName(info, out value);
			name = GetStringFromPointer(value);
			return outputDeviceName;
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceManufacturer(IntPtr info, out string manufacturer)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceManufacturer = GetOutputDeviceManufacturer(info, out value);
			manufacturer = GetStringFromPointer(value);
			return outputDeviceManufacturer;
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceProduct(IntPtr info, out string product)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceProduct = GetOutputDeviceProduct(info, out value);
			product = GetStringFromPointer(value);
			return outputDeviceProduct;
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceDriverVersion(IntPtr info, out int driverVersion)
		{
			return GetOutputDeviceDriverVersion(info, out driverVersion);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceTechnology(IntPtr info, out OutputDeviceTechnology deviceType)
		{
			return GetOutputDeviceTechnology(info, out deviceType);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceUniqueId(IntPtr info, out int uniqueId)
		{
			return GetOutputDeviceUniqueId(info, out uniqueId);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceVoicesNumber(IntPtr info, out int voicesNumber)
		{
			return GetOutputDeviceVoicesNumber(info, out voicesNumber);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceNotesNumber(IntPtr info, out int notesNumber)
		{
			return GetOutputDeviceNotesNumber(info, out notesNumber);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceChannelsMask(IntPtr info, out int channelsMask)
		{
			return GetOutputDeviceChannelsMask(info, out channelsMask);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceOptions(IntPtr info, out OutputDeviceOption option)
		{
			return GetOutputDeviceOptions(info, out option);
		}

		public override OUT_GETPROPERTYRESULT Api_GetDeviceDriverOwner(IntPtr info, out string driverOwner)
		{
			IntPtr value;
			OUT_GETPROPERTYRESULT outputDeviceDriverOwner = GetOutputDeviceDriverOwner(info, out value);
			driverOwner = GetStringFromPointer(value);
			return outputDeviceDriverOwner;
		}
	}
	internal static class OutputDeviceApiProvider
	{
		private static readonly bool Is64Bit = IntPtr.Size == 8;

		private static OutputDeviceApi _api;

		public static OutputDeviceApi Api
		{
			get
			{
				if (_api == null)
				{
					_api = (Is64Bit ? ((OutputDeviceApi)new OutputDeviceApi64()) : ((OutputDeviceApi)new OutputDeviceApi32()));
				}
				return _api;
			}
		}
	}
	[Flags]
	public enum OutputDeviceOption
	{
		Unknown = 0,
		PatchCaching = 1,
		LeftRightVolume = 2,
		Stream = 4,
		Volume = 8
	}
	public enum OutputDeviceProperty
	{
		Product,
		Manufacturer,
		DriverVersion,
		Technology,
		UniqueId,
		VoicesNumber,
		NotesNumber,
		Channels,
		Options,
		DriverOwner
	}
	public enum OutputDeviceTechnology
	{
		Unknown,
		MidiPort,
		Synth,
		SquareSynth,
		FmSynth,
		Mapper,
		Wavetable,
		SoftwareSynth
	}
	public delegate MidiEvent EventCallback(MidiEvent rawEvent, long rawTime, TimeSpan playbackTime);
	public delegate NotePlaybackData NoteCallback(NotePlaybackData rawNoteData, long rawTime, long rawLength, TimeSpan playbackTime);
	public sealed class NotePlaybackData
	{
		public static readonly NotePlaybackData SkipNote = new NotePlaybackData(playNote: false);

		public SevenBitNumber NoteNumber { get; }

		public SevenBitNumber Velocity { get; }

		public SevenBitNumber OffVelocity { get; }

		public FourBitNumber Channel { get; }

		internal bool PlayNote { get; }

		public NotePlaybackData(SevenBitNumber noteNumber, SevenBitNumber velocity, SevenBitNumber offVelocity, FourBitNumber channel)
			: this(playNote: true)
		{
			NoteNumber = noteNumber;
			Velocity = velocity;
			OffVelocity = offVelocity;
			Channel = channel;
		}

		private NotePlaybackData(bool playNote)
		{
			PlayNote = playNote;
		}

		internal NoteOnEvent GetNoteOnEvent()
		{
			return new NoteOnEvent(NoteNumber, Velocity)
			{
				Channel = Channel
			};
		}

		internal NoteOffEvent GetNoteOffEvent()
		{
			return new NoteOffEvent(NoteNumber, OffVelocity)
			{
				Channel = Channel
			};
		}
	}
	public sealed class PlaybackCurrentTime
	{
		public Playback Playback { get; }

		public ITimeSpan Time { get; }

		internal PlaybackCurrentTime(Playback playback, ITimeSpan time)
		{
			Playback = playback;
			Time = time;
		}
	}
	public sealed class PlaybackCurrentTimeChangedEventArgs : EventArgs
	{
		public IEnumerable<PlaybackCurrentTime> Times { get; }

		internal PlaybackCurrentTimeChangedEventArgs(IEnumerable<PlaybackCurrentTime> times)
		{
			Times = times;
		}
	}
	public sealed class PlaybackCurrentTimeWatcher : IDisposable, IClockDrivenObject
	{
		private static readonly TimeSpan DefaultPollingInterval = TimeSpan.FromMilliseconds(100.0);

		private static readonly Lazy<PlaybackCurrentTimeWatcher> _lazyInstance = new Lazy<PlaybackCurrentTimeWatcher>(() => new PlaybackCurrentTimeWatcher());

		private readonly Dictionary<Playback, TimeSpanType> _playbacks = new Dictionary<Playback, TimeSpanType>();

		private readonly object _playbacksLock = new object();

		private readonly MidiClockSettings _clockSettings;

		private MidiClock _clock;

		private TimeSpan _pollingInterval = DefaultPollingInterval;

		private bool _disposed;

		public static PlaybackCurrentTimeWatcher Instance => _lazyInstance.Value;

		public TimeSpan PollingInterval
		{
			get
			{
				return _pollingInterval;
			}
			set
			{
				_pollingInterval = value;
				RecreateClock();
			}
		}

		public IEnumerable<Playback> Playbacks
		{
			get
			{
				lock (_playbacksLock)
				{
					return _playbacks.Keys;
				}
			}
		}

		public bool IsWatching => _clock?.IsRunning ?? false;

		public event EventHandler<PlaybackCurrentTimeChangedEventArgs> CurrentTimeChanged;

		private PlaybackCurrentTimeWatcher(MidiClockSettings clockSettings = null)
		{
			_clockSettings = clockSettings ?? new MidiClockSettings();
			PollingInterval = DefaultPollingInterval;
		}

		public void Start()
		{
			EnsureIsNotDisposed();
			_clock.Start();
		}

		public void Stop()
		{
			EnsureIsNotDisposed();
			_clock.Stop();
		}

		public void AddPlayback(Playback playback, TimeSpanType timeType)
		{
			ThrowIfArgument.IsNull("playback", playback);
			ThrowIfArgument.IsInvalidEnumValue("timeType", timeType);
			EnsureIsNotDisposed();
			lock (_playbacksLock)
			{
				_playbacks[playback] = timeType;
			}
		}

		public void RemovePlayback(Playback playback)
		{
			ThrowIfArgument.IsNull("playback", playback);
			EnsureIsNotDisposed();
			lock (_playbacksLock)
			{
				_playbacks.Remove(playback);
				if (!_playbacks.Any())
				{
					RecreateClock();
				}
			}
		}

		public void RemoveAllPlaybacks()
		{
			EnsureIsNotDisposed();
			lock (_playbacksLock)
			{
				_playbacks.Clear();
			}
			RecreateClock();
		}

		private void OnTick(object sender, EventArgs e)
		{
			if (_disposed || !IsWatching)
			{
				return;
			}
			List<PlaybackCurrentTime> list = new List<PlaybackCurrentTime>();
			lock (_playbacksLock)
			{
				foreach (KeyValuePair<Playback, TimeSpanType> playback in _playbacks)
				{
					ITimeSpan currentTime = playback.Key.GetCurrentTime(playback.Value);
					list.Add(new PlaybackCurrentTime(playback.Key, currentTime));
				}
			}
			if (list.Any())
			{
				OnCurrentTimeChanged(list);
			}
		}

		private void OnCurrentTimeChanged(IEnumerable<PlaybackCurrentTime> times)
		{
			this.CurrentTimeChanged?.Invoke(this, new PlaybackCurrentTimeChangedEventArgs(times));
		}

		private void EnsureIsNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("Playback current time watcher is disposed.");
			}
		}

		private void DisposeClock()
		{
			if (_clock != null)
			{
				_clock.Stop();
				_clock.Ticked -= OnTick;
				_clock.Dispose();
			}
		}

		private void CreateClock(TimeSpan pollingInterval)
		{
			_clock = new MidiClock(startImmediately: true, _clockSettings.CreateTickGeneratorCallback(), pollingInterval);
			_clock.Ticked += OnTick;
		}

		private void RecreateClock()
		{
			bool isWatching = IsWatching;
			DisposeClock();
			CreateClock(PollingInterval);
			if (isWatching)
			{
				Start();
			}
		}

		public void TickClock()
		{
			EnsureIsNotDisposed();
			_clock?.Tick();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					DisposeClock();
				}
				_disposed = true;
			}
		}
	}
	public sealed class MidiEventPlayedEventArgs : EventArgs
	{
		public MidiEvent Event { get; }

		public object Metadata { get; }

		internal MidiEventPlayedEventArgs(MidiEvent midiEvent, object metadata)
		{
			Event = midiEvent;
			Metadata = metadata;
		}
	}
	public sealed class NotesEventArgs : EventArgs
	{
		public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> Notes { get; }

		internal NotesEventArgs(params Melanchall.DryWetMidi.Interaction.Note[] notes)
		{
			Notes = notes;
		}
	}
	public class Playback : IDisposable, IClockDrivenObject
	{
		private static readonly TimeSpan ClockInterval = TimeSpan.FromMilliseconds(1.0);

		private static readonly TimeSpan MinPlaybackTime = TimeSpan.Zero;

		private static readonly TimeSpan MaxPlaybackTime = TimeSpan.MaxValue;

		private readonly IEnumerator<PlaybackEvent> _eventsEnumerator;

		private readonly TimeSpan _duration;

		private readonly long _durationInTicks;

		private ITimeSpan _playbackStart;

		private TimeSpan _playbackStartMetric = MinPlaybackTime;

		private ITimeSpan _playbackEnd;

		private TimeSpan _playbackEndMetric = MaxPlaybackTime;

		private bool _hasBeenStarted;

		private readonly MidiClock _clock;

		private readonly ConcurrentDictionary<NotePlaybackEventMetadata, byte> _activeNotesMetadata = new ConcurrentDictionary<NotePlaybackEventMetadata, byte>();

		private readonly List<NotePlaybackEventMetadata> _notesMetadata;

		private readonly PlaybackDataTracker _playbackDataTracker;

		private bool _disposed;

		public TempoMap TempoMap { get; }

		public IOutputDevice OutputDevice { get; set; }

		public bool IsRunning => _clock.IsRunning;

		public bool Loop { get; set; }

		public bool InterruptNotesOnStop { get; set; }

		public bool TrackNotes { get; set; }

		public bool TrackProgram
		{
			get
			{
				return _playbackDataTracker.TrackProgram;
			}
			set
			{
				if (_playbackDataTracker.TrackProgram != value)
				{
					_playbackDataTracker.TrackProgram = value;
					if (value)
					{
						SendTrackedData(PlaybackDataTracker.TrackedParameterType.Program);
					}
				}
			}
		}

		public bool TrackPitchValue
		{
			get
			{
				return _playbackDataTracker.TrackPitchValue;
			}
			set
			{
				if (_playbackDataTracker.TrackPitchValue != value)
				{
					_playbackDataTracker.TrackPitchValue = value;
					if (value)
					{
						SendTrackedData(PlaybackDataTracker.TrackedParameterType.PitchValue);
					}
				}
			}
		}

		public bool TrackControlValue
		{
			get
			{
				return _playbackDataTracker.TrackControlValue;
			}
			set
			{
				if (_playbackDataTracker.TrackControlValue != value)
				{
					_playbackDataTracker.TrackControlValue = value;
					if (value)
					{
						SendTrackedData(PlaybackDataTracker.TrackedParameterType.ControlValue);
					}
				}
			}
		}

		public double Speed
		{
			get
			{
				return _clock.Speed;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Speed is zero or negative.");
				EnsureIsNotDisposed();
				_clock.Speed = value;
			}
		}

		public PlaybackSnapping Snapping { get; }

		public NoteCallback NoteCallback { get; set; }

		public EventCallback EventCallback { get; set; }

		public ITimeSpan PlaybackStart
		{
			get
			{
				return _playbackStart;
			}
			set
			{
				_playbackStart = value;
				_playbackStartMetric = ((_playbackStart != null) ? ((TimeSpan)TimeConverter.ConvertTo<MetricTimeSpan>(_playbackStart, TempoMap)) : MinPlaybackTime);
			}
		}

		public ITimeSpan PlaybackEnd
		{
			get
			{
				return _playbackEnd;
			}
			set
			{
				_playbackEnd = value;
				_playbackEndMetric = ((_playbackEnd != null) ? ((TimeSpan)TimeConverter.ConvertTo<MetricTimeSpan>(_playbackEnd, TempoMap)) : MaxPlaybackTime);
			}
		}

		public event EventHandler Started;

		public event EventHandler Stopped;

		public event EventHandler Finished;

		public event EventHandler RepeatStarted;

		public event EventHandler<NotesEventArgs> NotesPlaybackStarted;

		public event EventHandler<NotesEventArgs> NotesPlaybackFinished;

		public event EventHandler<MidiEventPlayedEventArgs> EventPlayed;

		public event EventHandler<ErrorOccurredEventArgs> DeviceErrorOccurred;

		public Playback(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			playbackSettings = playbackSettings ?? new PlaybackSettings();
			NoteDetectionSettings settings = playbackSettings.NoteDetectionSettings ?? new NoteDetectionSettings();
			ICollection<PlaybackEvent> playbackEvents = GetPlaybackEvents(timedObjects.GetNotesAndTimedEventsLazy(settings, completeObjectsAllowed: true), tempoMap);
			_eventsEnumerator = playbackEvents.GetEnumerator();
			_eventsEnumerator.MoveNext();
			PlaybackEvent playbackEvent = playbackEvents.LastOrDefault();
			_duration = playbackEvent?.Time ?? TimeSpan.Zero;
			_durationInTicks = playbackEvent?.RawTime ?? 0;
			_notesMetadata = (from e in playbackEvents
				select e.Metadata.Note into m
				where m != null
				select m).ToList();
			_notesMetadata.Sort((NotePlaybackEventMetadata m1, NotePlaybackEventMetadata m2) => m1.StartTime.CompareTo(m2.StartTime));
			TempoMap = tempoMap;
			MidiClockSettings midiClockSettings = playbackSettings.ClockSettings ?? new MidiClockSettings();
			_clock = new MidiClock(startImmediately: false, midiClockSettings.CreateTickGeneratorCallback(), ClockInterval);
			_clock.Ticked += OnClockTicked;
			Snapping = new PlaybackSnapping(playbackEvents, tempoMap);
			_playbackDataTracker = new PlaybackDataTracker(TempoMap);
			foreach (PlaybackEvent item in playbackEvents)
			{
				_playbackDataTracker.InitializeData(item.Event, item.RawTime, item.Metadata.TimedEvent.Metadata);
			}
		}

		public Playback(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
			: this(timedObjects, tempoMap, playbackSettings)
		{
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			OutputDevice = outputDevice;
		}

		~Playback()
		{
			Dispose(disposing: false);
		}

		public ITimeSpan GetDuration(TimeSpanType durationType)
		{
			ThrowIfArgument.IsInvalidEnumValue("durationType", durationType);
			return TimeConverter.ConvertTo((MetricTimeSpan)_duration, durationType, TempoMap);
		}

		public TTimeSpan GetDuration<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return TimeConverter.ConvertTo<TTimeSpan>((MetricTimeSpan)_duration, TempoMap);
		}

		public ITimeSpan GetCurrentTime(TimeSpanType timeType)
		{
			ThrowIfArgument.IsInvalidEnumValue("timeType", timeType);
			return TimeConverter.ConvertTo((MetricTimeSpan)_clock.CurrentTime, timeType, TempoMap);
		}

		public TTimeSpan GetCurrentTime<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return TimeConverter.ConvertTo<TTimeSpan>((MetricTimeSpan)_clock.CurrentTime, TempoMap);
		}

		public void Start()
		{
			EnsureIsNotDisposed();
			if (!_clock.IsRunning)
			{
				if (!_hasBeenStarted)
				{
					MoveToStart();
				}
				OutputDevice?.PrepareForEventsSending();
				SendTrackedData();
				StopStartNotes();
				_clock.Start();
				_hasBeenStarted = true;
				OnStarted();
			}
		}

		public void Stop()
		{
			EnsureIsNotDisposed();
			if (!IsRunning)
			{
				return;
			}
			_clock.Stop();
			if (InterruptNotesOnStop)
			{
				TimeSpan currentTime = _clock.CurrentTime;
				List<Melanchall.DryWetMidi.Interaction.Note> list = new List<Melanchall.DryWetMidi.Interaction.Note>();
				foreach (NotePlaybackEventMetadata key in _activeNotesMetadata.Keys)
				{
					if (TryPlayNoteEvent(key, isNoteOnEvent: false, currentTime, out var note))
					{
						list.Add(note);
					}
				}
				OnNotesPlaybackFinished(list.ToArray());
				_activeNotesMetadata.Clear();
			}
			OnStopped();
		}

		public void Play()
		{
			EnsureIsNotDisposed();
			Start();
			SpinWait.SpinUntil(() => !_clock.IsRunning);
		}

		public bool MoveToSnapPoint(SnapPoint snapPoint)
		{
			ThrowIfArgument.IsNull("snapPoint", snapPoint);
			EnsureIsNotDisposed();
			if (!snapPoint.IsEnabled)
			{
				return false;
			}
			return TryToMoveToSnapPoint(snapPoint);
		}

		public bool MoveToFirstSnapPoint()
		{
			EnsureIsNotDisposed();
			SnapPoint nextSnapPoint = Snapping.GetNextSnapPoint(TimeSpan.Zero);
			return TryToMoveToSnapPoint(nextSnapPoint);
		}

		public bool MoveToFirstSnapPoint<TData>(TData data)
		{
			EnsureIsNotDisposed();
			SnapPoint<TData> nextSnapPoint = Snapping.GetNextSnapPoint(TimeSpan.Zero, data);
			return TryToMoveToSnapPoint(nextSnapPoint);
		}

		public bool MoveToPreviousSnapPoint(SnapPointsGroup snapPointsGroup)
		{
			ThrowIfArgument.IsNull("snapPointsGroup", snapPointsGroup);
			EnsureIsNotDisposed();
			SnapPoint previousSnapPoint = Snapping.GetPreviousSnapPoint(_clock.CurrentTime, snapPointsGroup);
			return TryToMoveToSnapPoint(previousSnapPoint);
		}

		public bool MoveToPreviousSnapPoint()
		{
			EnsureIsNotDisposed();
			SnapPoint previousSnapPoint = Snapping.GetPreviousSnapPoint(_clock.CurrentTime);
			return TryToMoveToSnapPoint(previousSnapPoint);
		}

		public bool MoveToPreviousSnapPoint<TData>(TData data)
		{
			EnsureIsNotDisposed();
			SnapPoint<TData> previousSnapPoint = Snapping.GetPreviousSnapPoint(_clock.CurrentTime, data);
			return TryToMoveToSnapPoint(previousSnapPoint);
		}

		public bool MoveToNextSnapPoint(SnapPointsGroup snapPointsGroup)
		{
			ThrowIfArgument.IsNull("snapPointsGroup", snapPointsGroup);
			EnsureIsNotDisposed();
			SnapPoint nextSnapPoint = Snapping.GetNextSnapPoint(_clock.CurrentTime, snapPointsGroup);
			return TryToMoveToSnapPoint(nextSnapPoint);
		}

		public bool MoveToNextSnapPoint()
		{
			EnsureIsNotDisposed();
			SnapPoint nextSnapPoint = Snapping.GetNextSnapPoint(_clock.CurrentTime);
			return TryToMoveToSnapPoint(nextSnapPoint);
		}

		public bool MoveToNextSnapPoint<TData>(TData data)
		{
			EnsureIsNotDisposed();
			SnapPoint<TData> nextSnapPoint = Snapping.GetNextSnapPoint(_clock.CurrentTime, data);
			return TryToMoveToSnapPoint(nextSnapPoint);
		}

		public void MoveToStart()
		{
			EnsureIsNotDisposed();
			MoveToTime(PlaybackStart ?? new MetricTimeSpan());
		}

		public void MoveToTime(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			EnsureIsNotDisposed();
			if (TimeConverter.ConvertFrom(time, TempoMap) > _durationInTicks)
			{
				time = (MetricTimeSpan)_duration;
			}
			bool isRunning = IsRunning;
			SetStartTime(time);
			if (isRunning)
			{
				SendTrackedData();
				StopStartNotes();
				_clock.Start();
			}
		}

		public void MoveForward(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			EnsureIsNotDisposed();
			MetricTimeSpan metricTimeSpan = _clock.CurrentTime;
			MoveToTime(metricTimeSpan.Add(step, TimeSpanMode.TimeLength));
		}

		public void MoveBack(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			EnsureIsNotDisposed();
			MetricTimeSpan metricTimeSpan = _clock.CurrentTime;
			ITimeSpan time;
			if (!(TimeConverter.ConvertTo<MetricTimeSpan>(step, TempoMap) > metricTimeSpan))
			{
				time = metricTimeSpan.Subtract(step, TimeSpanMode.TimeLength);
			}
			else
			{
				ITimeSpan timeSpan = new MetricTimeSpan();
				time = timeSpan;
			}
			MoveToTime(time);
		}

		protected virtual bool TryPlayEvent(MidiEvent midiEvent, object metadata)
		{
			OutputDevice?.SendEvent(midiEvent);
			return true;
		}

		protected virtual IEnumerable<TimedEvent> GetTimedEvents(ITimedObject timedObject)
		{
			return Enumerable.Empty<TimedEvent>();
		}

		private bool TryToMoveToSnapPoint(SnapPoint snapPoint)
		{
			if (snapPoint != null)
			{
				MoveToTime((MetricTimeSpan)snapPoint.Time);
			}
			return snapPoint != null;
		}

		private void SendTrackedData(PlaybackDataTracker.TrackedParameterType trackedParameterType = PlaybackDataTracker.TrackedParameterType.All)
		{
			foreach (PlaybackDataTracker.EventWithMetadata item in _playbackDataTracker.GetEventsAtTime(_clock.CurrentTime, trackedParameterType))
			{
				PlayEvent(item.Event, item.Metadata);
			}
		}

		private void StopStartNotes()
		{
			if (TrackNotes)
			{
				TimeSpan currentTime = _clock.CurrentTime;
				NotePlaybackEventMetadata[] notesToPlay = (from m in _notesMetadata.SkipWhile((NotePlaybackEventMetadata m) => m.EndTime <= currentTime).TakeWhile((NotePlaybackEventMetadata m) => m.StartTime < currentTime)
					where m.StartTime < currentTime && m.EndTime > currentTime
					select m).Distinct().ToArray();
				NotePlaybackEventMetadata[] array = notesToPlay.Where((NotePlaybackEventMetadata n) => !_activeNotesMetadata.Keys.Contains(n)).ToArray();
				NotePlaybackEventMetadata[] array2 = _activeNotesMetadata.Keys.Where((NotePlaybackEventMetadata n) => !notesToPlay.Contains(n)).ToArray();
				OutputDevice?.PrepareForEventsSending();
				List<Melanchall.DryWetMidi.Interaction.Note> list = new List<Melanchall.DryWetMidi.Interaction.Note>();
				NotePlaybackEventMetadata[] array3 = array2;
				Melanchall.DryWetMidi.Interaction.Note note;
				foreach (NotePlaybackEventMetadata noteMetadata in array3)
				{
					TryPlayNoteEvent(noteMetadata, isNoteOnEvent: false, currentTime, out note);
					list.Add(note);
				}
				OnNotesPlaybackFinished(list.ToArray());
				list.Clear();
				array3 = array;
				foreach (NotePlaybackEventMetadata noteMetadata2 in array3)
				{
					TryPlayNoteEvent(noteMetadata2, isNoteOnEvent: true, currentTime, out note);
					list.Add(note);
				}
				OnNotesPlaybackStarted(list.ToArray());
			}
		}

		private void OnStarted()
		{
			this.Started?.Invoke(this, EventArgs.Empty);
		}

		private void OnStopped()
		{
			this.Stopped?.Invoke(this, EventArgs.Empty);
		}

		private void OnFinished()
		{
			this.Finished?.Invoke(this, EventArgs.Empty);
		}

		private void OnRepeatStarted()
		{
			this.RepeatStarted?.Invoke(this, EventArgs.Empty);
		}

		private void OnNotesPlaybackStarted(params Melanchall.DryWetMidi.Interaction.Note[] notes)
		{
			this.NotesPlaybackStarted?.Invoke(this, new NotesEventArgs(notes));
		}

		private void OnNotesPlaybackFinished(params Melanchall.DryWetMidi.Interaction.Note[] notes)
		{
			this.NotesPlaybackFinished?.Invoke(this, new NotesEventArgs(notes));
		}

		private void OnEventPlayed(MidiEvent midiEvent, object metadata)
		{
			this.EventPlayed?.Invoke(this, new MidiEventPlayedEventArgs(midiEvent, metadata));
		}

		private void OnDeviceErrorOccurred(Exception exception)
		{
			this.DeviceErrorOccurred?.Invoke(this, new ErrorOccurredEventArgs(exception));
		}

		private void OnClockTicked(object sender, EventArgs e)
		{
			do
			{
				TimeSpan currentTime = _clock.CurrentTime;
				if (currentTime >= _playbackEndMetric)
				{
					break;
				}
				PlaybackEvent current = _eventsEnumerator.Current;
				if (current == null)
				{
					continue;
				}
				if (current.Time > currentTime)
				{
					return;
				}
				MidiEvent midiEvent = current.Event;
				if (midiEvent == null)
				{
					continue;
				}
				if (!IsRunning)
				{
					return;
				}
				if (TryPlayNoteEvent(current, out var note))
				{
					if (note != null)
					{
						if (current.Event is NoteOnEvent)
						{
							OnNotesPlaybackStarted(note);
						}
						else
						{
							OnNotesPlaybackFinished(note);
						}
					}
				}
				else
				{
					EventCallback eventCallback = EventCallback;
					if (eventCallback != null)
					{
						midiEvent = eventCallback(midiEvent.Clone(), current.RawTime, currentTime);
					}
					if (midiEvent != null)
					{
						PlayEvent(midiEvent, current.Metadata.TimedEvent.Metadata);
					}
				}
			}
			while (_eventsEnumerator.MoveNext());
			if (!Loop)
			{
				_clock.StopInternally();
				OnFinished();
				return;
			}
			_clock.StopShortly();
			_clock.ResetCurrentTime();
			_eventsEnumerator.Reset();
			_eventsEnumerator.MoveNext();
			MoveToStart();
			_clock.Start();
			OnRepeatStarted();
		}

		private void EnsureIsNotDisposed()
		{
			if (_disposed)
			{
				throw new ObjectDisposedException("Playback is disposed.");
			}
		}

		private void SetStartTime(ITimeSpan time)
		{
			TimeSpan timeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(time, TempoMap);
			_clock.SetCurrentTime(timeSpan);
			_eventsEnumerator.Reset();
			do
			{
				_eventsEnumerator.MoveNext();
			}
			while (_eventsEnumerator.Current != null && _eventsEnumerator.Current.Time < timeSpan);
		}

		private void PlayEvent(MidiEvent midiEvent, object metadata)
		{
			_playbackDataTracker.UpdateCurrentData(midiEvent, metadata);
			try
			{
				if (TryPlayEvent(midiEvent, metadata))
				{
					OnEventPlayed(midiEvent, metadata);
				}
			}
			catch (Exception exception)
			{
				OnDeviceErrorOccurred(exception);
			}
		}

		private bool TryPlayNoteEvent(NotePlaybackEventMetadata noteMetadata, bool isNoteOnEvent, TimeSpan time, out Melanchall.DryWetMidi.Interaction.Note note)
		{
			return TryPlayNoteEvent(noteMetadata, null, isNoteOnEvent, time, out note);
		}

		private bool TryPlayNoteEvent(PlaybackEvent playbackEvent, out Melanchall.DryWetMidi.Interaction.Note note)
		{
			return TryPlayNoteEvent(playbackEvent.Metadata.Note, playbackEvent.Event, playbackEvent.Event is NoteOnEvent, playbackEvent.Time, out note);
		}

		private bool TryPlayNoteEvent(NotePlaybackEventMetadata noteMetadata, MidiEvent midiEvent, bool isNoteOnEvent, TimeSpan time, out Melanchall.DryWetMidi.Interaction.Note note)
		{
			note = null;
			if (noteMetadata == null)
			{
				return false;
			}
			NotePlaybackData notePlaybackData = noteMetadata.NotePlaybackData;
			NoteCallback noteCallback = NoteCallback;
			if (noteCallback != null && midiEvent is NoteOnEvent)
			{
				notePlaybackData = noteCallback(noteMetadata.RawNotePlaybackData, noteMetadata.RawNote.Time, noteMetadata.RawNote.Length, time);
				noteMetadata.SetCustomNotePlaybackData(notePlaybackData);
			}
			note = noteMetadata.RawNote;
			if (noteMetadata.IsCustomNotePlaybackDataSet)
			{
				if (notePlaybackData == null || !notePlaybackData.PlayNote)
				{
					midiEvent = null;
				}
				else
				{
					note = noteMetadata.GetEffectiveNote();
					midiEvent = ((midiEvent is NoteOnEvent) ? ((NoteEvent)notePlaybackData.GetNoteOnEvent()) : ((NoteEvent)notePlaybackData.GetNoteOffEvent()));
				}
			}
			else if (midiEvent == null)
			{
				midiEvent = (isNoteOnEvent ? ((NoteEvent)notePlaybackData.GetNoteOnEvent()) : ((NoteEvent)notePlaybackData.GetNoteOffEvent()));
			}
			if (midiEvent != null)
			{
				TimedEvent timedEvent = (isNoteOnEvent ? noteMetadata.RawNote.TimedNoteOnEvent : noteMetadata.RawNote.TimedNoteOffEvent);
				PlayEvent(midiEvent, (timedEvent as IMetadata)?.Metadata);
				if (midiEvent is NoteOnEvent)
				{
					_activeNotesMetadata.TryAdd(noteMetadata, 0);
				}
				else
				{
					_activeNotesMetadata.TryRemove(noteMetadata, out var _);
				}
			}
			else
			{
				note = null;
			}
			return true;
		}

		private ICollection<PlaybackEvent> GetPlaybackEvents(IEnumerable<ITimedObject> timedObjects, TempoMap tempoMap)
		{
			List<PlaybackEvent> list = new List<PlaybackEvent>();
			foreach (ITimedObject timedObject in timedObjects)
			{
				bool flag = false;
				foreach (TimedEvent timedEvent2 in GetTimedEvents(timedObject))
				{
					list.Add(GetPlaybackEvent(timedEvent2, tempoMap));
					flag = true;
				}
				if (flag)
				{
					continue;
				}
				if (timedObject is Melanchall.DryWetMidi.Interaction.Chord chord)
				{
					list.AddRange(GetPlaybackEvents(chord, tempoMap));
				}
				else if (timedObject is Melanchall.DryWetMidi.Interaction.Note note)
				{
					list.AddRange(GetPlaybackEvents(note, tempoMap));
				}
				else if (timedObject is TimedEvent timedEvent)
				{
					list.Add(GetPlaybackEvent(timedEvent, tempoMap));
				}
				else if (timedObject is RegisteredParameter registeredParameter)
				{
					list.AddRange(from e in registeredParameter.GetTimedEvents()
						select GetPlaybackEvent(e, tempoMap));
				}
			}
			return list.OrderBy((PlaybackEvent e) => e, new PlaybackEventsComparer()).ToList();
		}

		private static PlaybackEvent GetPlaybackEvent(TimedEvent timedEvent, TempoMap tempoMap)
		{
			PlaybackEvent playbackEvent = new PlaybackEvent(timedEvent.Event, timedEvent.TimeAs<MetricTimeSpan>(tempoMap), timedEvent.Time);
			playbackEvent.Metadata.TimedEvent = new TimedEventPlaybackEventMetadata((timedEvent as IMetadata)?.Metadata);
			return playbackEvent;
		}

		private static IEnumerable<PlaybackEvent> GetPlaybackEvents(Melanchall.DryWetMidi.Interaction.Chord chord, TempoMap tempoMap)
		{
			foreach (Melanchall.DryWetMidi.Interaction.Note note in chord.Notes)
			{
				foreach (PlaybackEvent playbackEvent in GetPlaybackEvents(note, tempoMap))
				{
					yield return playbackEvent;
				}
			}
		}

		private static IEnumerable<PlaybackEvent> GetPlaybackEvents(Melanchall.DryWetMidi.Interaction.Note note, TempoMap tempoMap)
		{
			TimeSpan startTime = note.TimeAs<MetricTimeSpan>(tempoMap);
			TimeSpan endTime = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time + note.Length, tempoMap);
			NotePlaybackEventMetadata noteMetadata = new NotePlaybackEventMetadata(note, startTime, endTime);
			yield return GetPlaybackEventWithNoteMetadata(note.TimedNoteOnEvent, tempoMap, noteMetadata);
			yield return GetPlaybackEventWithNoteMetadata(note.TimedNoteOffEvent, tempoMap, noteMetadata);
		}

		private static PlaybackEvent GetPlaybackEventWithNoteMetadata(TimedEvent timedEvent, TempoMap tempoMap, NotePlaybackEventMetadata noteMetadata)
		{
			PlaybackEvent playbackEvent = new PlaybackEvent(timedEvent.Event, timedEvent.TimeAs<MetricTimeSpan>(tempoMap), timedEvent.Time);
			playbackEvent.Metadata.Note = noteMetadata;
			playbackEvent.Metadata.TimedEvent = new TimedEventPlaybackEventMetadata((timedEvent as IMetadata)?.Metadata);
			return playbackEvent;
		}

		public void TickClock()
		{
			EnsureIsNotDisposed();
			_clock?.Tick();
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Stop();
					_clock.Ticked -= OnClockTicked;
					_clock.Dispose();
					_eventsEnumerator.Dispose();
				}
				_disposed = true;
			}
		}
	}
	internal sealed class PlaybackDataTracker
	{
		[Flags]
		public enum TrackedParameterType
		{
			Program = 1,
			PitchValue = 2,
			ControlValue = 4,
			All = 7
		}

		public sealed class EventWithMetadata
		{
			public MidiEvent Event { get; }

			public object Metadata { get; }

			public EventWithMetadata(MidiEvent midiEvent, object metadata)
			{
				Event = midiEvent;
				Metadata = metadata;
			}
		}

		private abstract class DataChange<TData> : IMetadata
		{
			public TData Data { get; }

			public object Metadata { get; set; }

			public bool IsDefault { get; set; }

			public DataChange(TData data, object metadata)
			{
				Data = data;
				Metadata = metadata;
			}

			public DataChange(TData data, object metadata, bool isDefault)
				: this(data, metadata)
			{
				IsDefault = isDefault;
			}
		}

		private sealed class ProgramChange : DataChange<SevenBitNumber>
		{
			public ProgramChange(SevenBitNumber programNumber, object metadata)
				: base(programNumber, metadata)
			{
			}

			public ProgramChange(SevenBitNumber programNumber, object metadata, bool isDefault)
				: base(programNumber, metadata, isDefault)
			{
			}
		}

		private sealed class PitchValueChange : DataChange<ushort>
		{
			public PitchValueChange(ushort pitchValue, object metadata)
				: base(pitchValue, metadata)
			{
			}

			public PitchValueChange(ushort pitchValue, object metadata, bool isDefault)
				: base(pitchValue, metadata, isDefault)
			{
			}
		}

		private sealed class ControlValueChange : DataChange<SevenBitNumber>
		{
			public ControlValueChange(SevenBitNumber controlValue, object metadata)
				: base(controlValue, metadata)
			{
			}

			public ControlValueChange(SevenBitNumber controlValue, object metadata, bool isDefault)
				: base(controlValue, metadata, isDefault)
			{
			}
		}

		private static readonly ProgramChange DefaultProgramChange = new ProgramChange(SevenBitNumber.MinValue, null, isDefault: true);

		private static readonly PitchValueChange DefaultPitchValueChange = new PitchValueChange(0, null, isDefault: true);

		private static readonly ControlValueChange DefaultControlValueChange = new ControlValueChange(SevenBitNumber.MinValue, null, isDefault: true);

		private readonly ProgramChange[] _currentProgramChanges = new ProgramChange[(byte)FourBitNumber.MaxValue + 1];

		private readonly ValueLine<ProgramChange>[] _programsChangesLinesByChannels = FourBitNumber.Values.Select((FourBitNumber n) => new ValueLine<ProgramChange>(DefaultProgramChange)).ToArray();

		private readonly PitchValueChange[] _currentPitchValues = new PitchValueChange[(byte)FourBitNumber.MaxValue + 1];

		private readonly ValueLine<PitchValueChange>[] _pitchValuesLinesByChannel = FourBitNumber.Values.Select((FourBitNumber n) => new ValueLine<PitchValueChange>(DefaultPitchValueChange)).ToArray();

		private readonly Dictionary<SevenBitNumber, ControlValueChange>[] _currentControlsValuesChangesByChannel = new Dictionary<SevenBitNumber, ControlValueChange>[(byte)FourBitNumber.MaxValue + 1];

		private readonly Dictionary<SevenBitNumber, ValueLine<ControlValueChange>>[] _controlsValuesChangesLinesByChannel = FourBitNumber.Values.Select((FourBitNumber n) => new Dictionary<SevenBitNumber, ValueLine<ControlValueChange>>()).ToArray();

		private readonly TempoMap _tempoMap;

		private readonly Dictionary<TrackedParameterType, Func<long, IEnumerable<EventWithMetadata>>> _getParameterEventsAtTime;

		public bool TrackProgram { get; set; }

		public bool TrackPitchValue { get; set; }

		public bool TrackControlValue { get; set; }

		public PlaybackDataTracker(TempoMap tempoMap)
		{
			_tempoMap = tempoMap;
			_getParameterEventsAtTime = new Dictionary<TrackedParameterType, Func<long, IEnumerable<EventWithMetadata>>>
			{
				[TrackedParameterType.Program] = (long time) => GetProgramChangeEventsAtTime(time),
				[TrackedParameterType.PitchValue] = (long time) => GetPitchBendEventsAtTime(time),
				[TrackedParameterType.ControlValue] = (long time) => GetControlChangeEventsAtTime(time)
			};
		}

		public void InitializeData(MidiEvent midiEvent, long time, object metadata)
		{
			InitializeProgramChangeData(midiEvent as ProgramChangeEvent, time, metadata);
			InitializePitchBendData(midiEvent as PitchBendEvent, time, metadata);
			InitializeControlData(midiEvent as ControlChangeEvent, time, metadata);
		}

		public void UpdateCurrentData(MidiEvent midiEvent, object metadata)
		{
			UpdateCurrentProgramChangeData(midiEvent as ProgramChangeEvent, metadata);
			UpdateCurrentPitchBendData(midiEvent as PitchBendEvent, metadata);
			UpdateCurrentControlData(midiEvent as ControlChangeEvent, metadata);
		}

		public IEnumerable<EventWithMetadata> GetEventsAtTime(TimeSpan time, TrackedParameterType trackedParameterType)
		{
			long convertedTime = TimeConverter.ConvertFrom((MetricTimeSpan)time, _tempoMap);
			foreach (KeyValuePair<TrackedParameterType, Func<long, IEnumerable<EventWithMetadata>>> item in _getParameterEventsAtTime)
			{
				if (!trackedParameterType.HasFlag(item.Key))
				{
					continue;
				}
				foreach (EventWithMetadata item2 in item.Value(convertedTime))
				{
					yield return item2;
				}
			}
		}

		private void UpdateCurrentProgramChangeData(ProgramChangeEvent programChangeEvent, object metadata)
		{
			if (programChangeEvent != null)
			{
				_currentProgramChanges[(byte)programChangeEvent.Channel] = new ProgramChange(programChangeEvent.ProgramNumber, metadata);
			}
		}

		private void InitializeProgramChangeData(ProgramChangeEvent programChangeEvent, long time, object metadata)
		{
			if (programChangeEvent != null)
			{
				_programsChangesLinesByChannels[(byte)programChangeEvent.Channel].SetValue(time, new ProgramChange(programChangeEvent.ProgramNumber, metadata));
			}
		}

		private IEnumerable<EventWithMetadata> GetProgramChangeEventsAtTime(long time)
		{
			if (!TrackProgram)
			{
				yield break;
			}
			FourBitNumber[] values = FourBitNumber.Values;
			foreach (FourBitNumber fourBitNumber in values)
			{
				ValueChange<ProgramChange> valueChangeAtTime = _programsChangesLinesByChannels[(byte)fourBitNumber].GetValueChangeAtTime(time);
				if ((object)valueChangeAtTime == null || valueChangeAtTime.Time != time)
				{
					ProgramChange programChange = ((valueChangeAtTime != null) ? valueChangeAtTime.Value : DefaultProgramChange);
					ProgramChange programChange2 = _currentProgramChanges[(byte)fourBitNumber];
					if ((byte)programChange.Data != (byte?)programChange2?.Data && (programChange2 != null || !programChange.IsDefault))
					{
						yield return new EventWithMetadata(new ProgramChangeEvent(programChange.Data)
						{
							Channel = fourBitNumber
						}, programChange.Metadata);
					}
				}
			}
		}

		private void UpdateCurrentPitchBendData(PitchBendEvent pitchBendEvent, object metadata)
		{
			if (pitchBendEvent != null)
			{
				_currentPitchValues[(byte)pitchBendEvent.Channel] = new PitchValueChange(pitchBendEvent.PitchValue, metadata);
			}
		}

		private void InitializePitchBendData(PitchBendEvent pitchBendEvent, long time, object metadata)
		{
			if (pitchBendEvent != null)
			{
				_pitchValuesLinesByChannel[(byte)pitchBendEvent.Channel].SetValue(time, new PitchValueChange(pitchBendEvent.PitchValue, metadata));
			}
		}

		private IEnumerable<EventWithMetadata> GetPitchBendEventsAtTime(long time)
		{
			if (!TrackPitchValue)
			{
				yield break;
			}
			FourBitNumber[] values = FourBitNumber.Values;
			foreach (FourBitNumber fourBitNumber in values)
			{
				ValueChange<PitchValueChange> valueChangeAtTime = _pitchValuesLinesByChannel[(byte)fourBitNumber].GetValueChangeAtTime(time);
				if ((object)valueChangeAtTime == null || valueChangeAtTime.Time != time)
				{
					PitchValueChange pitchValueChange = ((valueChangeAtTime != null) ? valueChangeAtTime.Value : DefaultPitchValueChange);
					PitchValueChange pitchValueChange2 = _currentPitchValues[(byte)fourBitNumber];
					if (pitchValueChange.Data != pitchValueChange2?.Data && (pitchValueChange2 != null || !pitchValueChange.IsDefault))
					{
						yield return new EventWithMetadata(new PitchBendEvent(pitchValueChange.Data)
						{
							Channel = fourBitNumber
						}, pitchValueChange.Metadata);
					}
				}
			}
		}

		private void UpdateCurrentControlData(ControlChangeEvent controlChangeEvent, object metadata)
		{
			if (controlChangeEvent != null)
			{
				Dictionary<SevenBitNumber, ControlValueChange> dictionary = _currentControlsValuesChangesByChannel[(byte)controlChangeEvent.Channel];
				if (dictionary == null)
				{
					dictionary = (_currentControlsValuesChangesByChannel[(byte)controlChangeEvent.Channel] = new Dictionary<SevenBitNumber, ControlValueChange>());
				}
				dictionary[controlChangeEvent.ControlNumber] = new ControlValueChange(controlChangeEvent.ControlValue, metadata);
			}
		}

		private void InitializeControlData(ControlChangeEvent controlChangeEvent, long time, object metadata)
		{
			if (controlChangeEvent != null)
			{
				Dictionary<SevenBitNumber, ValueLine<ControlValueChange>> dictionary = _controlsValuesChangesLinesByChannel[(byte)controlChangeEvent.Channel];
				if (!dictionary.TryGetValue(controlChangeEvent.ControlNumber, out var value))
				{
					dictionary.Add(controlChangeEvent.ControlNumber, value = new ValueLine<ControlValueChange>(DefaultControlValueChange));
				}
				value.SetValue(time, new ControlValueChange(controlChangeEvent.ControlValue, metadata));
			}
		}

		private IEnumerable<EventWithMetadata> GetControlChangeEventsAtTime(long time)
		{
			if (!TrackControlValue)
			{
				yield break;
			}
			FourBitNumber[] values = FourBitNumber.Values;
			foreach (FourBitNumber channel in values)
			{
				Dictionary<SevenBitNumber, ValueLine<ControlValueChange>> controlsValuesChangesLinesByControlNumber = _controlsValuesChangesLinesByChannel[(byte)channel];
				Dictionary<SevenBitNumber, ControlValueChange> currentControlsValuesChangesByControlNumber = _currentControlsValuesChangesByChannel[(byte)channel];
				SevenBitNumber[] values2 = SevenBitNumber.Values;
				foreach (SevenBitNumber sevenBitNumber in values2)
				{
					if (!controlsValuesChangesLinesByControlNumber.TryGetValue(sevenBitNumber, out var value))
					{
						continue;
					}
					ValueChange<ControlValueChange> valueChangeAtTime = value.GetValueChangeAtTime(time);
					if ((object)valueChangeAtTime == null || valueChangeAtTime.Time != time)
					{
						ControlValueChange controlValueChange = ((valueChangeAtTime != null) ? valueChangeAtTime.Value : DefaultControlValueChange);
						ControlValueChange value2 = null;
						currentControlsValuesChangesByControlNumber?.TryGetValue(sevenBitNumber, out value2);
						if ((byte)controlValueChange.Data != (byte?)value2?.Data && (value2 != null || !controlValueChange.IsDefault))
						{
							yield return new EventWithMetadata(new ControlChangeEvent(sevenBitNumber, controlValueChange.Data)
							{
								Channel = channel
							}, controlValueChange.Metadata);
						}
					}
				}
			}
		}
	}
	internal sealed class PlaybackEvent
	{
		public MidiEvent Event { get; }

		public TimeSpan Time { get; }

		public long RawTime { get; }

		public PlaybackEventMetadata Metadata { get; } = new PlaybackEventMetadata();

		public PlaybackEvent(MidiEvent midiEvent, TimeSpan time, long rawTime)
		{
			Event = midiEvent;
			Time = time;
			RawTime = rawTime;
		}
	}
	public delegate MidiEvent PlaybackEventCallback(MidiEvent midiEvent, TimeSpan time, long rawTime);
	internal sealed class NotePlaybackEventMetadata
	{
		public Melanchall.DryWetMidi.Interaction.Note RawNote { get; }

		public TimeSpan StartTime { get; }

		public TimeSpan EndTime { get; }

		public NotePlaybackData RawNotePlaybackData { get; }

		public NotePlaybackData NotePlaybackData { get; private set; }

		public bool IsCustomNotePlaybackDataSet { get; private set; }

		public NotePlaybackEventMetadata(Melanchall.DryWetMidi.Interaction.Note note, TimeSpan startTime, TimeSpan endTime)
		{
			RawNote = note;
			StartTime = startTime;
			EndTime = endTime;
			RawNotePlaybackData = new NotePlaybackData(RawNote.NoteNumber, RawNote.Velocity, RawNote.OffVelocity, RawNote.Channel);
			NotePlaybackData = RawNotePlaybackData;
		}

		public Melanchall.DryWetMidi.Interaction.Note GetEffectiveNote()
		{
			NotePlaybackData notePlaybackData = NotePlaybackData;
			if (notePlaybackData == null)
			{
				return null;
			}
			Melanchall.DryWetMidi.Interaction.Note note = RawNote.Clone();
			note.NoteNumber = notePlaybackData.NoteNumber;
			note.Velocity = notePlaybackData.Velocity;
			note.OffVelocity = notePlaybackData.OffVelocity;
			note.Channel = notePlaybackData.Channel;
			return note;
		}

		public void SetCustomNotePlaybackData(NotePlaybackData notePlaybackData)
		{
			NotePlaybackData = notePlaybackData;
			IsCustomNotePlaybackDataSet = true;
		}
	}
	internal sealed class PlaybackEventMetadata
	{
		public NotePlaybackEventMetadata Note { get; set; }

		public TimedEventPlaybackEventMetadata TimedEvent { get; set; }
	}
	internal sealed class TimedEventPlaybackEventMetadata
	{
		public object Metadata { get; }

		public TimedEventPlaybackEventMetadata(object metadata)
		{
			Metadata = metadata;
		}
	}
	internal sealed class PlaybackEventsComparer : IComparer<PlaybackEvent>
	{
		public int Compare(PlaybackEvent x, PlaybackEvent y)
		{
			long num = x.RawTime - y.RawTime;
			if (num != 0L)
			{
				return Math.Sign(num);
			}
			ChannelEvent channelEvent = x.Event as ChannelEvent;
			ChannelEvent channelEvent2 = y.Event as ChannelEvent;
			if (channelEvent == null || channelEvent2 == null)
			{
				return 0;
			}
			if (!(channelEvent is NoteEvent) && channelEvent2 is NoteEvent)
			{
				return -1;
			}
			if (channelEvent is NoteEvent && !(channelEvent2 is NoteEvent))
			{
				return 1;
			}
			return 0;
		}
	}
	public sealed class PlaybackSettings
	{
		public MidiClockSettings ClockSettings { get; set; }

		public NoteDetectionSettings NoteDetectionSettings { get; set; }
	}
	public static class PlaybackUtilities
	{
		public static Playback GetPlayback(this IEnumerable<MidiEvent> events, TempoMap tempoMap, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("events", events);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return new Playback(events.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(playbackSettings?.NoteDetectionSettings ?? new NoteDetectionSettings()), tempoMap, outputDevice, playbackSettings);
		}

		public static Playback GetPlayback(this IEnumerable<MidiEvent> events, TempoMap tempoMap, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("events", events);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return new Playback(events.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(playbackSettings?.NoteDetectionSettings ?? new NoteDetectionSettings()), tempoMap, playbackSettings);
		}

		public static Playback GetPlayback(this TrackChunk trackChunk, TempoMap tempoMap, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return new Playback(trackChunk.Events.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(playbackSettings?.NoteDetectionSettings ?? new NoteDetectionSettings()), tempoMap, outputDevice, playbackSettings);
		}

		public static Playback GetPlayback(this TrackChunk trackChunk, TempoMap tempoMap, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return new Playback(trackChunk.Events.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(playbackSettings?.NoteDetectionSettings ?? new NoteDetectionSettings()), tempoMap, playbackSettings);
		}

		public static Playback GetPlayback(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return new Playback(from o in trackChunks.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(playbackSettings?.NoteDetectionSettings ?? new NoteDetectionSettings())
				select o.Item1, tempoMap, outputDevice, playbackSettings);
		}

		public static Playback GetPlayback(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return new Playback(from o in trackChunks.GetTimedEventsLazy().GetNotesAndTimedEventsLazy(playbackSettings?.NoteDetectionSettings ?? new NoteDetectionSettings())
				select o.Item1, tempoMap, playbackSettings);
		}

		public static Playback GetPlayback(this MidiFile midiFile, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return midiFile.GetTrackChunks().GetPlayback(midiFile.GetTempoMap(), outputDevice, playbackSettings);
		}

		public static Playback GetPlayback(this MidiFile midiFile, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.GetTrackChunks().GetPlayback(midiFile.GetTempoMap(), playbackSettings);
		}

		public static Playback GetPlayback(this Pattern pattern, TempoMap tempoMap, FourBitNumber channel, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return pattern.ToTrackChunk(tempoMap, channel).GetPlayback(tempoMap, outputDevice, playbackSettings);
		}

		public static Playback GetPlayback(this Pattern pattern, TempoMap tempoMap, FourBitNumber channel, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return pattern.ToTrackChunk(tempoMap, channel).GetPlayback(tempoMap, playbackSettings);
		}

		public static Playback GetPlayback<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, SevenBitNumber programNumber, PlaybackSettings playbackSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			return GetMusicalObjectsPlayback(objects, tempoMap, outputDevice, (FourBitNumber channel) => new ProgramChangeEvent[1]
			{
				new ProgramChangeEvent(programNumber)
				{
					Channel = channel
				}
			}, playbackSettings);
		}

		public static Playback GetPlayback<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidiProgram generalMidiProgram, PlaybackSettings playbackSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue("generalMidiProgram", generalMidiProgram);
			return GetMusicalObjectsPlayback(objects, tempoMap, outputDevice, (FourBitNumber channel) => new MidiEvent[1] { generalMidiProgram.GetProgramEvent(channel) }, playbackSettings);
		}

		public static Playback GetPlayback<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidi2Program generalMidi2Program, PlaybackSettings playbackSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue("generalMidi2Program", generalMidi2Program);
			return GetMusicalObjectsPlayback(objects, tempoMap, outputDevice, (FourBitNumber channel) => generalMidi2Program.GetProgramEvents(channel), playbackSettings);
		}

		public static void Play(this TrackChunk trackChunk, TempoMap tempoMap, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			using Playback playback = trackChunk.GetPlayback(tempoMap, outputDevice, playbackSettings);
			playback.Play();
		}

		public static void Play(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			using Playback playback = trackChunks.GetPlayback(tempoMap, outputDevice, playbackSettings);
			playback.Play();
		}

		public static void Play(this MidiFile midiFile, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			midiFile.GetTrackChunks().Play(midiFile.GetTempoMap(), outputDevice, playbackSettings);
		}

		public static void Play(this Pattern pattern, TempoMap tempoMap, FourBitNumber channel, IOutputDevice outputDevice, PlaybackSettings playbackSettings = null)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			pattern.ToTrackChunk(tempoMap, channel).Play(tempoMap, outputDevice, playbackSettings);
		}

		public static void Play<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, SevenBitNumber programNumber, PlaybackSettings playbackSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			using Playback playback = objects.GetPlayback(tempoMap, outputDevice, programNumber, playbackSettings);
			playback.Play();
		}

		public static void Play<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidiProgram generalMidiProgram, PlaybackSettings playbackSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue("generalMidiProgram", generalMidiProgram);
			using Playback playback = objects.GetPlayback(tempoMap, outputDevice, generalMidiProgram, playbackSettings);
			playback.Play();
		}

		public static void Play<TObject>(this IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, GeneralMidi2Program generalMidi2Program, PlaybackSettings playbackSettings = null) where TObject : IMusicalObject, ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("outputDevice", outputDevice);
			ThrowIfArgument.IsInvalidEnumValue("generalMidi2Program", generalMidi2Program);
			using Playback playback = objects.GetPlayback(tempoMap, outputDevice, generalMidi2Program, playbackSettings);
			playback.Play();
		}

		private static Playback GetMusicalObjectsPlayback<TObject>(IEnumerable<TObject> objects, TempoMap tempoMap, IOutputDevice outputDevice, Func<FourBitNumber, IEnumerable<MidiEvent>> programChangeEventsGetter, PlaybackSettings playbackSettings) where TObject : IMusicalObject, ITimedObject
		{
			return new Playback(objects.Select((TObject n) => n.Channel).Distinct().SelectMany(programChangeEventsGetter)
				.Select((Func<MidiEvent, ITimedObject>)((MidiEvent e) => new TimedEvent(e)))
				.Concat((IEnumerable<ITimedObject>)objects), tempoMap, outputDevice, playbackSettings);
		}
	}
	public sealed class PlaybackSnapping
	{
		private readonly List<SnapPoint> _snapPoints = new List<SnapPoint>();

		private readonly IEnumerable<PlaybackEvent> _playbackEvents;

		private readonly TempoMap _tempoMap;

		private readonly TimeSpan _maxTime;

		private SnapPointsGroup _noteStartSnapPointsGroup;

		private SnapPointsGroup _noteEndSnapPointsGroup;

		public IEnumerable<SnapPoint> SnapPoints => _snapPoints.AsReadOnly();

		public bool IsEnabled { get; set; } = true;

		internal PlaybackSnapping(IEnumerable<PlaybackEvent> playbackEvents, TempoMap tempoMap)
		{
			_playbackEvents = playbackEvents;
			_tempoMap = tempoMap;
			_maxTime = _playbackEvents.LastOrDefault()?.Time ?? TimeSpan.Zero;
		}

		public SnapPoint<TData> AddSnapPoint<TData>(ITimeSpan time, TData data)
		{
			ThrowIfArgument.IsNull("time", time);
			TimeSpan timeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(time, _tempoMap);
			if (timeSpan == TimeSpan.Zero)
			{
				timeSpan = new TimeSpan(1L);
			}
			SnapPoint<TData> snapPoint = new SnapPoint<TData>(timeSpan, data);
			_snapPoints.Add(snapPoint);
			return snapPoint;
		}

		public SnapPoint<Guid> AddSnapPoint(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			return AddSnapPoint(time, Guid.NewGuid());
		}

		public void RemoveSnapPoint<TData>(SnapPoint<TData> snapPoint)
		{
			ThrowIfArgument.IsNull("snapPoint", snapPoint);
			_snapPoints.Remove(snapPoint);
		}

		public void RemoveSnapPointsByData<TData>(Predicate<TData> predicate)
		{
			ThrowIfArgument.IsNull("predicate", predicate);
			_snapPoints.RemoveAll((SnapPoint p) => p is SnapPoint<TData> snapPoint && predicate(snapPoint.Data));
		}

		public void Clear()
		{
			_snapPoints.Clear();
		}

		public SnapPointsGroup SnapToGrid(IGrid grid)
		{
			ThrowIfArgument.IsNull("grid", grid);
			SnapPointsGroup snapPointsGroup = new SnapPointsGroup();
			foreach (long time in grid.GetTimes(_tempoMap))
			{
				TimeSpan timeSpan = TimeConverter.ConvertTo<MetricTimeSpan>(time, _tempoMap);
				if (timeSpan > _maxTime)
				{
					break;
				}
				if (timeSpan == TimeSpan.Zero)
				{
					timeSpan = new TimeSpan(1L);
				}
				_snapPoints.Add(new SnapPoint(timeSpan)
				{
					SnapPointsGroup = snapPointsGroup
				});
			}
			return snapPointsGroup;
		}

		public SnapPointsGroup SnapToNotesStarts()
		{
			return _noteStartSnapPointsGroup ?? (_noteStartSnapPointsGroup = SnapToNoteEvents(snapToNoteOn: true));
		}

		public SnapPointsGroup SnapToNotesEnds()
		{
			return _noteEndSnapPointsGroup ?? (_noteEndSnapPointsGroup = SnapToNoteEvents(snapToNoteOn: false));
		}

		internal SnapPoint GetNextSnapPoint(TimeSpan time, SnapPointsGroup snapPointsGroup)
		{
			return GetNextSnapPoints(GetActiveSnapPoints(snapPointsGroup), time).FirstOrDefault();
		}

		internal SnapPoint GetNextSnapPoint(TimeSpan time)
		{
			return GetNextSnapPoints(GetActiveSnapPoints(), time).FirstOrDefault();
		}

		internal SnapPoint<TData> GetNextSnapPoint<TData>(TimeSpan time, TData data)
		{
			return (SnapPoint<TData>)GetNextSnapPoints(GetActiveSnapPoints(), time).FirstOrDefault((SnapPoint p) => IsSnapPointWithData(p, data));
		}

		internal SnapPoint GetPreviousSnapPoint(TimeSpan time, SnapPointsGroup snapPointsGroup)
		{
			return GetPreviousSnapPoints(GetActiveSnapPoints(snapPointsGroup), time).LastOrDefault();
		}

		internal SnapPoint GetPreviousSnapPoint(TimeSpan time)
		{
			return GetPreviousSnapPoints(GetActiveSnapPoints(), time).LastOrDefault();
		}

		internal SnapPoint<TData> GetPreviousSnapPoint<TData>(TimeSpan time, TData data)
		{
			return (SnapPoint<TData>)GetPreviousSnapPoints(GetActiveSnapPoints(), time).LastOrDefault((SnapPoint p) => IsSnapPointWithData(p, data));
		}

		internal IEnumerable<SnapPoint> GetActiveSnapPoints()
		{
			if (IsEnabled)
			{
				return from p in _snapPoints
					where p.IsEnabled && (p.SnapPointsGroup?.IsEnabled ?? true)
					orderby p.Time
					select p;
			}
			return Enumerable.Empty<SnapPoint>();
		}

		private IEnumerable<SnapPoint> GetNextSnapPoints(IEnumerable<SnapPoint> snapPoints, TimeSpan time)
		{
			return snapPoints.SkipWhile((SnapPoint p) => p.Time <= time);
		}

		private IEnumerable<SnapPoint> GetPreviousSnapPoints(IEnumerable<SnapPoint> snapPoints, TimeSpan time)
		{
			return snapPoints.TakeWhile((SnapPoint p) => p.Time < time);
		}

		private bool IsSnapPointWithData<TData>(SnapPoint snapPoint, TData data)
		{
			if (snapPoint is SnapPoint<TData> { Data: var data2 })
			{
				return data2.Equals(data);
			}
			return false;
		}

		private SnapPointsGroup SnapToNoteEvents(bool snapToNoteOn)
		{
			List<ITimeSpan> list = new List<ITimeSpan>();
			foreach (PlaybackEvent playbackEvent in _playbackEvents)
			{
				if (playbackEvent.Metadata.Note != null && playbackEvent.Event is NoteOnEvent == snapToNoteOn)
				{
					list.Add((MetricTimeSpan)playbackEvent.Time);
				}
			}
			return SnapToGrid(new ArbitraryGrid(list));
		}

		private IEnumerable<SnapPoint> GetActiveSnapPoints(SnapPointsGroup snapPointsGroup)
		{
			return from p in GetActiveSnapPoints()
				where p.SnapPointsGroup == snapPointsGroup
				select p;
		}
	}
	public class SnapPoint
	{
		public bool IsEnabled { get; set; } = true;

		public TimeSpan Time { get; }

		public SnapPointsGroup SnapPointsGroup { get; internal set; }

		internal SnapPoint(TimeSpan time)
		{
			Time = time;
		}
	}
	public sealed class SnapPoint<TData> : SnapPoint
	{
		public TData Data { get; }

		internal SnapPoint(TimeSpan time, TData data)
			: base(time)
		{
			Data = data;
		}
	}
	public sealed class SnapPointsGroup
	{
		public bool IsEnabled { get; set; } = true;

		internal SnapPointsGroup()
		{
		}
	}
	public sealed class Recording : IDisposable
	{
		private readonly List<RecordingEvent> _events = new List<RecordingEvent>();

		private readonly Stopwatch _stopwatch = new Stopwatch();

		private bool _disposed;

		public TempoMap TempoMap { get; }

		public IInputDevice InputDevice { get; }

		public bool IsRunning => _stopwatch.IsRunning;

		public event EventHandler Started;

		public event EventHandler Stopped;

		public Recording(TempoMap tempoMap, IInputDevice inputDevice)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsNull("inputDevice", inputDevice);
			TempoMap = tempoMap;
			InputDevice = inputDevice;
			InputDevice.EventReceived += OnEventReceived;
		}

		public ITimeSpan GetDuration(TimeSpanType durationType)
		{
			ThrowIfArgument.IsInvalidEnumValue("durationType", durationType);
			TimeSpan? timeSpan = _events.LastOrDefault()?.Time;
			return TimeConverter.ConvertTo((timeSpan.HasValue ? ((MetricTimeSpan)timeSpan.GetValueOrDefault()) : null) ?? new MetricTimeSpan(), durationType, TempoMap);
		}

		public TTimeSpan GetDuration<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			TimeSpan? timeSpan = _events.LastOrDefault()?.Time;
			return TimeConverter.ConvertTo<TTimeSpan>((timeSpan.HasValue ? ((MetricTimeSpan)timeSpan.GetValueOrDefault()) : null) ?? new MetricTimeSpan(), TempoMap);
		}

		public IReadOnlyList<TimedEvent> GetEvents()
		{
			return _events.Select((RecordingEvent e) => new TimedEvent(e.Event, TimeConverter.ConvertFrom((MetricTimeSpan)e.Time, TempoMap))).ToList().AsReadOnly();
		}

		public void Start()
		{
			if (!IsRunning)
			{
				if (!InputDevice.IsListeningForEvents)
				{
					throw new InvalidOperationException("Input device is not listening for MIDI events. Call StartEventsListening prior to start recording.");
				}
				_stopwatch.Start();
				OnStarted();
			}
		}

		public void Stop()
		{
			if (IsRunning)
			{
				_stopwatch.Stop();
				OnStopped();
			}
		}

		private void OnStarted()
		{
			this.Started?.Invoke(this, EventArgs.Empty);
		}

		private void OnStopped()
		{
			this.Stopped?.Invoke(this, EventArgs.Empty);
		}

		private void OnEventReceived(object sender, MidiEventReceivedEventArgs e)
		{
			if (IsRunning)
			{
				_events.Add(new RecordingEvent(e.Event, _stopwatch.Elapsed));
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Stop();
					InputDevice.EventReceived -= OnEventReceived;
				}
				_disposed = true;
			}
		}
	}
	internal sealed class RecordingEvent
	{
		public MidiEvent Event { get; }

		public TimeSpan Time { get; }

		public RecordingEvent(MidiEvent midiEvent, TimeSpan time)
		{
			Event = midiEvent;
			Time = time;
		}
	}
	public static class RecordingUtilities
	{
		public static TrackChunk ToTrackChunk(this Recording recording)
		{
			ThrowIfArgument.IsNull("recording", recording);
			if (recording.IsRunning)
			{
				throw new ArgumentException("Recording is in progress.", "recording");
			}
			return recording.GetEvents().ToTrackChunk();
		}

		public static MidiFile ToFile(this Recording recording)
		{
			ThrowIfArgument.IsNull("recording", recording);
			if (recording.IsRunning)
			{
				throw new ArgumentException("Recording is in progress.", "recording");
			}
			TrackChunk trackChunk = recording.ToTrackChunk();
			MidiFile midiFile = new MidiFile(trackChunk);
			midiFile.ReplaceTempoMap(recording.TempoMap);
			return midiFile;
		}
	}
	internal static class MidiDevicesSession
	{
		private static readonly object _lockObject = new object();

		private static IntPtr _name;

		private static IntPtr _handle;

		private static MidiDevicesSessionApi.InputDeviceCallback _inputDeviceCallback;

		private static MidiDevicesSessionApi.OutputDeviceCallback _outputDeviceCallback;

		internal static event EventHandler<IntPtr> InputDeviceAdded;

		internal static event EventHandler<IntPtr> InputDeviceRemoved;

		internal static event EventHandler<IntPtr> OutputDeviceAdded;

		internal static event EventHandler<IntPtr> OutputDeviceRemoved;

		public static IntPtr GetSessionHandle()
		{
			lock (_lockObject)
			{
				if (_handle == IntPtr.Zero)
				{
					_name = Marshal.StringToHGlobalAuto(Guid.NewGuid().ToString());
					CommonApi.API_TYPE aPI_TYPE = CommonApiProvider.Api.Api_GetApiType();
					MidiDevicesSessionApi.SESSION_OPENRESULT result = MidiDevicesSessionApi.SESSION_OPENRESULT.SESSION_OPENRESULT_OK;
					switch (aPI_TYPE)
					{
					case CommonApi.API_TYPE.API_TYPE_MAC:
						_inputDeviceCallback = InputDeviceCallback;
						_outputDeviceCallback = OutputDeviceCallback;
						result = MidiDevicesSessionApiProvider.Api.Api_OpenSession_Mac(_name, _inputDeviceCallback, _outputDeviceCallback, out _handle);
						break;
					case CommonApi.API_TYPE.API_TYPE_WIN:
						result = MidiDevicesSessionApiProvider.Api.Api_OpenSession_Win(_name, out _handle);
						break;
					}
					NativeApi.HandleResult(result);
				}
				return _handle;
			}
		}

		private static void InputDeviceCallback(IntPtr info, bool operation)
		{
			if (operation)
			{
				MidiDevicesSession.InputDeviceAdded?.Invoke(null, info);
			}
			else
			{
				MidiDevicesSession.InputDeviceRemoved?.Invoke(null, info);
			}
		}

		private static void OutputDeviceCallback(IntPtr info, bool operation)
		{
			if (operation)
			{
				MidiDevicesSession.OutputDeviceAdded?.Invoke(null, info);
			}
			else
			{
				MidiDevicesSession.OutputDeviceRemoved?.Invoke(null, info);
			}
		}
	}
	internal abstract class MidiDevicesSessionApi : NativeApi
	{
		public enum SESSION_OPENRESULT
		{
			SESSION_OPENRESULT_OK = 0,
			SESSION_OPENRESULT_SERVERSTARTERROR = 101,
			SESSION_OPENRESULT_WRONGTHREAD = 102,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			SESSION_OPENRESULT_NOTPERMITTED = 103,
			SESSION_OPENRESULT_UNKNOWNERROR = 104
		}

		public enum SESSION_CLOSERESULT
		{
			SESSION_CLOSERESULT_OK
		}

		public delegate void InputDeviceCallback(IntPtr info, bool operation);

		public delegate void OutputDeviceCallback(IntPtr info, bool operation);

		public abstract SESSION_OPENRESULT Api_OpenSession_Mac(IntPtr name, InputDeviceCallback inputDeviceCallback, OutputDeviceCallback outputDeviceCallback, out IntPtr handle);

		public abstract SESSION_OPENRESULT Api_OpenSession_Win(IntPtr name, out IntPtr handle);

		public abstract SESSION_CLOSERESULT Api_CloseSession(IntPtr handle);
	}
	internal sealed class MidiDevicesSessionApi32 : MidiDevicesSessionApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native32";

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern SESSION_OPENRESULT OpenSession_Mac(IntPtr name, InputDeviceCallback inputDeviceCallback, OutputDeviceCallback outputDeviceCallback, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern SESSION_OPENRESULT OpenSession_Win(IntPtr name, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern SESSION_CLOSERESULT CloseSession(IntPtr handle);

		public override SESSION_OPENRESULT Api_OpenSession_Mac(IntPtr name, InputDeviceCallback inputDeviceCallback, OutputDeviceCallback outputDeviceCallback, out IntPtr handle)
		{
			return OpenSession_Mac(name, inputDeviceCallback, outputDeviceCallback, out handle);
		}

		public override SESSION_OPENRESULT Api_OpenSession_Win(IntPtr name, out IntPtr handle)
		{
			return OpenSession_Win(name, out handle);
		}

		public override SESSION_CLOSERESULT Api_CloseSession(IntPtr handle)
		{
			return CloseSession(handle);
		}
	}
	internal sealed class MidiDevicesSessionApi64 : MidiDevicesSessionApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native64";

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern SESSION_OPENRESULT OpenSession_Mac(IntPtr name, InputDeviceCallback inputDeviceCallback, OutputDeviceCallback outputDeviceCallback, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern SESSION_OPENRESULT OpenSession_Win(IntPtr name, out IntPtr handle);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern SESSION_CLOSERESULT CloseSession(IntPtr handle);

		public override SESSION_OPENRESULT Api_OpenSession_Mac(IntPtr name, InputDeviceCallback inputDeviceCallback, OutputDeviceCallback outputDeviceCallback, out IntPtr handle)
		{
			return OpenSession_Mac(name, inputDeviceCallback, outputDeviceCallback, out handle);
		}

		public override SESSION_OPENRESULT Api_OpenSession_Win(IntPtr name, out IntPtr handle)
		{
			return OpenSession_Win(name, out handle);
		}

		public override SESSION_CLOSERESULT Api_CloseSession(IntPtr handle)
		{
			return CloseSession(handle);
		}
	}
	internal static class MidiDevicesSessionApiProvider
	{
		private static readonly bool Is64Bit = IntPtr.Size == 8;

		private static MidiDevicesSessionApi _api;

		public static MidiDevicesSessionApi Api
		{
			get
			{
				if (_api == null)
				{
					_api = (Is64Bit ? ((MidiDevicesSessionApi)new MidiDevicesSessionApi64()) : ((MidiDevicesSessionApi)new MidiDevicesSessionApi32()));
				}
				return _api;
			}
		}
	}
	public sealed class VirtualDevice : MidiDevice
	{
		private readonly string _name;

		private VirtualDeviceApi.Callback_Mac _callback_Mac;

		public override string Name => _name;

		public InputDevice InputDevice { get; private set; }

		public OutputDevice OutputDevice { get; private set; }

		internal VirtualDevice(string name)
			: base(IntPtr.Zero, CreationContext.User)
		{
			_name = name;
			if (CommonApiProvider.Api.Api_GetApiType() == CommonApi.API_TYPE.API_TYPE_MAC)
			{
				InitializeDevice_Mac();
			}
		}

		public static VirtualDevice Create(string name)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("name", name, "Device name");
			if (CommonApiProvider.Api.Api_GetApiType() != CommonApi.API_TYPE.API_TYPE_MAC)
			{
				throw new NotSupportedException("Virtual device creation is not supported on the current operating system.");
			}
			return new VirtualDevice(name);
		}

		private void OnMessage_Mac(IntPtr pktlist, IntPtr readProcRefCon, IntPtr srcConnRefCon)
		{
			VirtualDeviceApi.VIRTUAL_SENDBACKRESULT vIRTUAL_SENDBACKRESULT = VirtualDeviceApiProvider.Api.Api_SendDataBack(pktlist, readProcRefCon);
			if (vIRTUAL_SENDBACKRESULT != VirtualDeviceApi.VIRTUAL_SENDBACKRESULT.VIRTUAL_SENDBACKRESULT_OK)
			{
				MidiDeviceException exception = new MidiDeviceException($"Failed to send data back ({vIRTUAL_SENDBACKRESULT}).", (int)vIRTUAL_SENDBACKRESULT);
				OnError(exception);
			}
		}

		private void InitializeDevice_Mac()
		{
			IntPtr sessionHandle = MidiDevicesSession.GetSessionHandle();
			_callback_Mac = OnMessage_Mac;
			NativeApi.HandleResult(VirtualDeviceApiProvider.Api.Api_OpenDevice_Mac(Name, sessionHandle, _callback_Mac, out _info));
			IntPtr info = VirtualDeviceApiProvider.Api.Api_GetInputDeviceInfo(_info);
			InputDevice = new InputDevice(info, CreationContext.VirtualDevice);
			IntPtr info2 = VirtualDeviceApiProvider.Api.Api_GetOutputDeviceInfo(_info);
			OutputDevice = new OutputDevice(info2, CreationContext.VirtualDevice);
		}

		public override string ToString()
		{
			return "Virtual device";
		}

		internal override void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (_info != IntPtr.Zero)
				{
					InputDevice.Dispose(disposing);
					OutputDevice.Dispose(disposing);
					VirtualDeviceApiProvider.Api.Api_CloseDevice(_info);
					_info = IntPtr.Zero;
				}
				_disposed = true;
			}
		}
	}
	internal abstract class VirtualDeviceApi : NativeApi
	{
		public enum VIRTUAL_OPENRESULT
		{
			VIRTUAL_OPENRESULT_OK = 0,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			VIRTUAL_OPENRESULT_CREATESOURCE_NOTPERMITTED = 101,
			VIRTUAL_OPENRESULT_CREATESOURCE_SERVERSTARTERROR = 102,
			VIRTUAL_OPENRESULT_CREATESOURCE_WRONGTHREAD = 103,
			VIRTUAL_OPENRESULT_CREATESOURCE_UNKNOWNERROR = 104,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			VIRTUAL_OPENRESULT_CREATEDESTINATION_NOTPERMITTED = 105,
			VIRTUAL_OPENRESULT_CREATEDESTINATION_SERVERSTARTERROR = 106,
			VIRTUAL_OPENRESULT_CREATEDESTINATION_WRONGTHREAD = 107,
			VIRTUAL_OPENRESULT_CREATEDESTINATION_UNKNOWNERROR = 108
		}

		public enum VIRTUAL_CLOSERESULT
		{
			VIRTUAL_CLOSERESULT_OK = 0,
			VIRTUAL_CLOSERESULT_DISPOSESOURCE_UNKNOWNENDPOINT = 101,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			VIRTUAL_CLOSERESULT_DISPOSESOURCE_NOTPERMITTED = 102,
			VIRTUAL_CLOSERESULT_DISPOSESOURCE_UNKNOWNERROR = 103,
			VIRTUAL_CLOSERESULT_DISPOSEDESTINATION_UNKNOWNENDPOINT = 104,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			VIRTUAL_CLOSERESULT_DISPOSEDESTINATION_NOTPERMITTED = 105,
			VIRTUAL_CLOSERESULT_DISPOSEDESTINATION_UNKNOWNERROR = 106
		}

		public enum VIRTUAL_SENDBACKRESULT
		{
			VIRTUAL_SENDBACKRESULT_OK = 0,
			VIRTUAL_SENDBACKRESULT_UNKNOWNERROR_TE = 1,
			VIRTUAL_SENDBACKRESULT_UNKNOWNENDPOINT = 101,
			VIRTUAL_SENDBACKRESULT_WRONGENDPOINT = 102,
			[NativeErrorType(NativeErrorType.NotPermitted)]
			VIRTUAL_SENDBACKRESULT_NOTPERMITTED = 103,
			VIRTUAL_SENDBACKRESULT_SERVERSTARTERROR = 104,
			VIRTUAL_SENDBACKRESULT_WRONGTHREAD = 105,
			VIRTUAL_SENDBACKRESULT_UNKNOWNERROR = 106,
			VIRTUAL_SENDBACKRESULT_MESSAGESENDERROR = 107
		}

		public delegate void Callback_Mac(IntPtr pktlist, IntPtr readProcRefCon, IntPtr srcConnRefCon);

		public abstract VIRTUAL_OPENRESULT Api_OpenDevice_Mac(string name, IntPtr sessionHandle, Callback_Mac callback, out IntPtr info);

		public abstract VIRTUAL_CLOSERESULT Api_CloseDevice(IntPtr info);

		public abstract VIRTUAL_SENDBACKRESULT Api_SendDataBack(IntPtr pktlist, IntPtr readProcRefCon);

		public abstract IntPtr Api_GetInputDeviceInfo(IntPtr info);

		public abstract IntPtr Api_GetOutputDeviceInfo(IntPtr info);
	}
	internal sealed class VirtualDeviceApi32 : VirtualDeviceApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native32";

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern VIRTUAL_OPENRESULT OpenVirtualDevice_Mac(IntPtr name, IntPtr sessionHandle, Callback_Mac callback, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern VIRTUAL_CLOSERESULT CloseVirtualDevice(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern VIRTUAL_SENDBACKRESULT SendDataBackFromVirtualDevice(IntPtr pktlist, IntPtr readProcRefCon);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IntPtr GetInputDeviceInfoFromVirtualDevice(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native32", ExactSpelling = true)]
		private static extern IntPtr GetOutputDeviceInfoFromVirtualDevice(IntPtr info);

		public override VIRTUAL_OPENRESULT Api_OpenDevice_Mac(string name, IntPtr sessionHandle, Callback_Mac callback, out IntPtr info)
		{
			return OpenVirtualDevice_Mac(Marshal.StringToHGlobalAnsi(name), sessionHandle, callback, out info);
		}

		public override VIRTUAL_CLOSERESULT Api_CloseDevice(IntPtr info)
		{
			return CloseVirtualDevice(info);
		}

		public override VIRTUAL_SENDBACKRESULT Api_SendDataBack(IntPtr pktlist, IntPtr readProcRefCon)
		{
			return SendDataBackFromVirtualDevice(pktlist, readProcRefCon);
		}

		public override IntPtr Api_GetInputDeviceInfo(IntPtr info)
		{
			return GetInputDeviceInfoFromVirtualDevice(info);
		}

		public override IntPtr Api_GetOutputDeviceInfo(IntPtr info)
		{
			return GetOutputDeviceInfoFromVirtualDevice(info);
		}
	}
	internal sealed class VirtualDeviceApi64 : VirtualDeviceApi
	{
		private const string LibraryName = "Melanchall_DryWetMidi_Native64";

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern VIRTUAL_OPENRESULT OpenVirtualDevice_Mac(IntPtr name, IntPtr sessionHandle, Callback_Mac callback, out IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern VIRTUAL_CLOSERESULT CloseVirtualDevice(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern VIRTUAL_SENDBACKRESULT SendDataBackFromVirtualDevice(IntPtr pktlist, IntPtr readProcRefCon);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IntPtr GetInputDeviceInfoFromVirtualDevice(IntPtr info);

		[DllImport("Melanchall_DryWetMidi_Native64", ExactSpelling = true)]
		private static extern IntPtr GetOutputDeviceInfoFromVirtualDevice(IntPtr info);

		public override VIRTUAL_OPENRESULT Api_OpenDevice_Mac(string name, IntPtr sessionHandle, Callback_Mac callback, out IntPtr info)
		{
			return OpenVirtualDevice_Mac(Marshal.StringToHGlobalAnsi(name), sessionHandle, callback, out info);
		}

		public override VIRTUAL_CLOSERESULT Api_CloseDevice(IntPtr info)
		{
			return CloseVirtualDevice(info);
		}

		public override VIRTUAL_SENDBACKRESULT Api_SendDataBack(IntPtr pktlist, IntPtr readProcRefCon)
		{
			return SendDataBackFromVirtualDevice(pktlist, readProcRefCon);
		}

		public override IntPtr Api_GetInputDeviceInfo(IntPtr info)
		{
			return GetInputDeviceInfoFromVirtualDevice(info);
		}

		public override IntPtr Api_GetOutputDeviceInfo(IntPtr info)
		{
			return GetOutputDeviceInfoFromVirtualDevice(info);
		}
	}
	internal static class VirtualDeviceApiProvider
	{
		private static readonly bool Is64Bit = IntPtr.Size == 8;

		private static VirtualDeviceApi _api;

		public static VirtualDeviceApi Api
		{
			get
			{
				if (_api == null)
				{
					_api = (Is64Bit ? ((VirtualDeviceApi)new VirtualDeviceApi64()) : ((VirtualDeviceApi)new VirtualDeviceApi32()));
				}
				return _api;
			}
		}
	}
}
namespace Melanchall.DryWetMidi.Interaction
{
	public class Chord : ILengthedObject, ITimedObject, IMusicalObject, INotifyTimeChanged, INotifyLengthChanged
	{
		private FourBitNumber? _channel;

		private SevenBitNumber? _velocity;

		private SevenBitNumber? _offVelocity;

		public NotesCollection Notes { get; }

		public long Time
		{
			get
			{
				return Notes.FirstOrDefault()?.Time ?? 0;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				long time = Time;
				if (value == time)
				{
					return;
				}
				foreach (Note note in Notes)
				{
					long num = note.Time - time;
					note.Time = value + num;
				}
				this.TimeChanged?.Invoke(this, new TimeChangedEventArgs(time, value));
			}
		}

		public long Length
		{
			get
			{
				if (!Notes.Any())
				{
					return 0L;
				}
				long num = long.MaxValue;
				long num2 = long.MinValue;
				foreach (Note note in Notes)
				{
					long time = note.Time;
					num = Math.Min(time, num);
					num2 = Math.Max(time + note.Length, num2);
				}
				return num2 - num;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				long length = Length;
				if (value == length)
				{
					return;
				}
				long num = value - Length;
				foreach (Note note in Notes)
				{
					note.Length += num;
				}
				this.LengthChanged?.Invoke(this, new LengthChangedEventArgs(length, value));
			}
		}

		public FourBitNumber Channel
		{
			get
			{
				if (_channel.HasValue)
				{
					return _channel.Value;
				}
				FourBitNumber? fourBitNumber = null;
				foreach (Note note in Notes)
				{
					if (fourBitNumber.HasValue && (byte)note.Channel != (byte?)fourBitNumber)
					{
						throw new InvalidOperationException("Chord's notes have different channels.");
					}
					fourBitNumber = note.Channel;
				}
				if (!fourBitNumber.HasValue)
				{
					throw new InvalidOperationException("Chord is empty.");
				}
				FourBitNumber? fourBitNumber2 = (_channel = fourBitNumber);
				return fourBitNumber2.Value;
			}
			set
			{
				foreach (Note note in Notes)
				{
					note.Channel = value;
				}
				_channel = value;
			}
		}

		public SevenBitNumber Velocity
		{
			get
			{
				if (_velocity.HasValue)
				{
					return _velocity.Value;
				}
				SevenBitNumber? sevenBitNumber = null;
				foreach (Note note in Notes)
				{
					if (sevenBitNumber.HasValue && (byte)note.Velocity != (byte?)sevenBitNumber)
					{
						throw new InvalidOperationException("Chord's notes have different velocities.");
					}
					sevenBitNumber = note.Velocity;
				}
				if (!sevenBitNumber.HasValue)
				{
					throw new InvalidOperationException("Chord is empty.");
				}
				SevenBitNumber? sevenBitNumber2 = (_velocity = sevenBitNumber);
				return sevenBitNumber2.Value;
			}
			set
			{
				foreach (Note note in Notes)
				{
					note.Velocity = value;
				}
				_velocity = value;
			}
		}

		public SevenBitNumber OffVelocity
		{
			get
			{
				if (_offVelocity.HasValue)
				{
					return _offVelocity.Value;
				}
				SevenBitNumber? sevenBitNumber = null;
				foreach (Note note in Notes)
				{
					if (sevenBitNumber.HasValue && (byte)note.OffVelocity != (byte?)sevenBitNumber)
					{
						throw new InvalidOperationException("Chord's notes have different off-velocities.");
					}
					sevenBitNumber = note.OffVelocity;
				}
				if (!sevenBitNumber.HasValue)
				{
					throw new InvalidOperationException("Chord is empty.");
				}
				SevenBitNumber? sevenBitNumber2 = (_offVelocity = sevenBitNumber);
				return sevenBitNumber2.Value;
			}
			set
			{
				foreach (Note note in Notes)
				{
					note.OffVelocity = value;
				}
				_offVelocity = value;
			}
		}

		public event NotesCollectionChangedEventHandler NotesCollectionChanged;

		public event EventHandler<TimeChangedEventArgs> TimeChanged;

		public event EventHandler<LengthChangedEventArgs> LengthChanged;

		public Chord()
			: this(Enumerable.Empty<Note>())
		{
		}

		public Chord(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			Notes = new NotesCollection(notes);
			Notes.CollectionChanged += OnNotesCollectionChanged;
		}

		public Chord(params Note[] notes)
			: this((IEnumerable<Note>)notes)
		{
		}

		public Chord(IEnumerable<Note> notes, long time)
			: this(notes)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			Time = time;
		}

		public virtual Chord Clone()
		{
			return new Chord(Notes.Select((Note note) => note.Clone()));
		}

		public SplitLengthedObject<Chord> Split(long time)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			long time2 = Time;
			long num = time2 + Length;
			if (time <= time2)
			{
				return new SplitLengthedObject<Chord>(null, Clone());
			}
			if (time >= num)
			{
				return new SplitLengthedObject<Chord>(Clone(), null);
			}
			SplitLengthedObject<Note>[] source = Notes.Select((Note n) => n.Split(time)).ToArray();
			Chord leftPart = new Chord(from p in source
				select p.LeftPart into p
				where p != null
				select p);
			Chord rightPart = new Chord(from p in source
				select p.RightPart into p
				where p != null
				select p);
			return new SplitLengthedObject<Chord>(leftPart, rightPart);
		}

		private void OnNotesCollectionChanged(NotesCollection collection, NotesCollectionChangedEventArgs args)
		{
			_channel = null;
			_velocity = null;
			_offVelocity = null;
			this.NotesCollectionChanged?.Invoke(collection, args);
		}

		public override string ToString()
		{
			NotesCollection notes = Notes;
			if (!notes.Any())
			{
				return "Empty notes collection";
			}
			return string.Join(" ", notes.OrderBy((Note n) => n.NoteNumber));
		}
	}
	public sealed class ChordDetectionSettings
	{
		private const int DefaultNotesMinCount = 1;

		private static readonly long DefaultNotesTolerance;

		private int _notesMinCount = 1;

		private long _notesTolerance = DefaultNotesTolerance;

		private ChordSearchContext _chordSearchContext;

		public int NotesMinCount
		{
			get
			{
				return _notesMinCount;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				_notesMinCount = value;
			}
		}

		public long NotesTolerance
		{
			get
			{
				return _notesTolerance;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Value is negative.");
				_notesTolerance = value;
			}
		}

		public NoteDetectionSettings NoteDetectionSettings { get; set; } = new NoteDetectionSettings();

		public ChordSearchContext ChordSearchContext
		{
			get
			{
				return _chordSearchContext;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_chordSearchContext = value;
			}
		}
	}
	internal sealed class ChordsBuilder
	{
		private class ChordDescriptor
		{
			private readonly int _notesMinCount;

			public long Time { get; }

			public List<Note> Notes { get; } = new List<Note>(3);

			public bool IsSealed { get; set; }

			public bool IsCompleted => Notes.Count >= _notesMinCount;

			public ChordDescriptor(Note firstNote, int notesMinCount)
			{
				Time = firstNote.Time;
				Notes.Add(firstNote);
				_notesMinCount = notesMinCount;
			}
		}

		private sealed class ChordDescriptorIndexed : ChordDescriptor
		{
			public int EventsCollectionIndex { get; set; }

			public ChordDescriptorIndexed(Note firstNote, int notesMinCount)
				: base(firstNote, notesMinCount)
			{
			}
		}

		private readonly ChordDetectionSettings _chordDetectionSettings;

		public ChordsBuilder(ChordDetectionSettings chordDetectionSettings)
		{
			_chordDetectionSettings = chordDetectionSettings ?? new ChordDetectionSettings();
		}

		public IEnumerable<Chord> GetChordsLazy(IEnumerable<TimedEvent> timedEvents, bool collectTimedEvents = false, List<TimedEvent> collectedTimedEvents = null)
		{
			LinkedList<ChordDescriptor> chordsDescriptors = new LinkedList<ChordDescriptor>();
			LinkedListNode<ChordDescriptor>[] chordsDescriptorsByChannel = new LinkedListNode<ChordDescriptor>[(byte)FourBitNumber.MaxValue + 1];
			IEnumerable<Note> notesLazy = new NotesBuilder(_chordDetectionSettings.NoteDetectionSettings).GetNotesLazy(timedEvents, collectTimedEvents, collectedTimedEvents);
			foreach (Note note in notesLazy)
			{
				LinkedListNode<ChordDescriptor> linkedListNode = chordsDescriptorsByChannel[(byte)note.Channel];
				if (linkedListNode == null || linkedListNode.List == null)
				{
					CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, note);
					continue;
				}
				ChordDescriptor value = linkedListNode.Value;
				if (CanNoteBeAddedToChord(value, note, _chordDetectionSettings.NotesTolerance))
				{
					value.Notes.Add(note);
					continue;
				}
				value.IsSealed = true;
				if (linkedListNode.Previous == null)
				{
					foreach (Chord chord in GetChords(linkedListNode, chordsDescriptors, getSealedOnly: true))
					{
						yield return chord;
					}
				}
				CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, note);
			}
			foreach (Chord chord2 in GetChords(chordsDescriptors.First, chordsDescriptors, getSealedOnly: false))
			{
				yield return chord2;
			}
		}

		public IEnumerable<Chord> GetChordsLazy(IEnumerable<Tuple<TimedEvent, int>> timedEvents, bool collectTimedEvents = false, List<Tuple<TimedEvent, int>> collectedTimedEvents = null)
		{
			LinkedList<ChordDescriptorIndexed> chordsDescriptors = new LinkedList<ChordDescriptorIndexed>();
			LinkedListNode<ChordDescriptorIndexed>[] chordsDescriptorsByChannel = new LinkedListNode<ChordDescriptorIndexed>[(byte)FourBitNumber.MaxValue + 1];
			IEnumerable<Tuple<Note, int>> indexedNotesLazy = new NotesBuilder(_chordDetectionSettings.NoteDetectionSettings).GetIndexedNotesLazy(timedEvents, collectTimedEvents, collectedTimedEvents);
			bool eventsCollectionShouldMatch = _chordDetectionSettings.ChordSearchContext == ChordSearchContext.SingleEventsCollection;
			foreach (Tuple<Note, int> noteTuple in indexedNotesLazy)
			{
				Note note = noteTuple.Item1;
				LinkedListNode<ChordDescriptorIndexed> linkedListNode = chordsDescriptorsByChannel[(byte)note.Channel];
				if (linkedListNode == null || linkedListNode.List == null)
				{
					CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, note, noteTuple.Item2);
					continue;
				}
				ChordDescriptorIndexed value = linkedListNode.Value;
				if (CanNoteBeAddedToChord(value, note, _chordDetectionSettings.NotesTolerance, noteTuple.Item2, eventsCollectionShouldMatch))
				{
					value.Notes.Add(note);
					continue;
				}
				value.IsSealed = true;
				if (linkedListNode.Previous == null)
				{
					foreach (Chord chord in GetChords(linkedListNode, chordsDescriptors, getSealedOnly: true))
					{
						yield return chord;
					}
				}
				CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, note, noteTuple.Item2);
			}
			foreach (Chord chord2 in GetChords(chordsDescriptors.First, chordsDescriptors, getSealedOnly: false))
			{
				yield return chord2;
			}
		}

		private IEnumerable<Chord> GetChords<TDescriptor>(LinkedListNode<TDescriptor> startChordDescriptorNode, LinkedList<TDescriptor> chordsDescriptors, bool getSealedOnly) where TDescriptor : ChordDescriptor
		{
			LinkedListNode<TDescriptor> chordDescriptorNode = startChordDescriptorNode;
			while (chordDescriptorNode != null)
			{
				TDescriptor value = chordDescriptorNode.Value;
				if (!getSealedOnly || value.IsSealed)
				{
					if (value.IsCompleted)
					{
						yield return new Chord(value.Notes);
					}
					LinkedListNode<TDescriptor> next = chordDescriptorNode.Next;
					chordsDescriptors.Remove(chordDescriptorNode);
					chordDescriptorNode = next;
					continue;
				}
				break;
			}
		}

		private void CreateChordDescriptor(LinkedList<ChordDescriptor> chordsDescriptors, LinkedListNode<ChordDescriptor>[] chordsDescriptorsByChannel, Note note)
		{
			ChordDescriptor value = new ChordDescriptor(note, _chordDetectionSettings.NotesMinCount);
			chordsDescriptorsByChannel[(byte)note.Channel] = chordsDescriptors.AddLast(value);
		}

		private void CreateChordDescriptor(LinkedList<ChordDescriptorIndexed> chordsDescriptors, LinkedListNode<ChordDescriptorIndexed>[] chordsDescriptorsByChannel, Note note, int noteOnIndex)
		{
			ChordDescriptorIndexed value = new ChordDescriptorIndexed(note, _chordDetectionSettings.NotesMinCount)
			{
				EventsCollectionIndex = noteOnIndex
			};
			chordsDescriptorsByChannel[(byte)note.Channel] = chordsDescriptors.AddLast(value);
		}

		private static bool CanNoteBeAddedToChord(ChordDescriptor chordDescriptor, Note note, long notesTolerance)
		{
			return note.Time - chordDescriptor.Time <= notesTolerance;
		}

		private static bool CanNoteBeAddedToChord(ChordDescriptorIndexed chordDescriptor, Note note, long notesTolerance, int eventsCollectionIndex, bool eventsCollectionShouldMatch)
		{
			if (note.Time - chordDescriptor.Time <= notesTolerance)
			{
				if (eventsCollectionShouldMatch)
				{
					return chordDescriptor.EventsCollectionIndex == eventsCollectionIndex;
				}
				return true;
			}
			return false;
		}
	}
	public sealed class ChordsCollection : TimedObjectsCollection<Chord>
	{
		public event ChordsCollectionChangedEventHandler CollectionChanged;

		internal ChordsCollection(IEnumerable<Chord> chords)
			: base(chords)
		{
		}

		protected override void OnObjectsAdded(IEnumerable<Chord> addedObjects)
		{
			base.OnObjectsAdded(addedObjects);
			OnCollectionChanged(addedObjects, null);
		}

		protected override void OnObjectsRemoved(IEnumerable<Chord> removedObjects)
		{
			base.OnObjectsRemoved(removedObjects);
			OnCollectionChanged(null, removedObjects);
		}

		private void OnCollectionChanged(IEnumerable<Chord> addedChords, IEnumerable<Chord> removedChords)
		{
			this.CollectionChanged?.Invoke(this, new ChordsCollectionChangedEventArgs(addedChords, removedChords));
		}
	}
	public sealed class ChordsCollectionChangedEventArgs : EventArgs
	{
		public IEnumerable<Chord> AddedChords { get; }

		public IEnumerable<Chord> RemovedChords { get; }

		public ChordsCollectionChangedEventArgs(IEnumerable<Chord> addedChords, IEnumerable<Chord> removedChords)
		{
			AddedChords = addedChords;
			RemovedChords = removedChords;
		}
	}
	public delegate void ChordsCollectionChangedEventHandler(ChordsCollection collection, ChordsCollectionChangedEventArgs args);
	public enum ChordSearchContext
	{
		SingleEventsCollection,
		AllEventsCollections
	}
	public sealed class ChordsManager : IDisposable
	{
		private readonly NotesManager _notesManager;

		private bool _disposed;

		public ChordsCollection Chords { get; }

		public ChordsManager(EventsCollection eventsCollection, ChordDetectionSettings settings = null, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			_notesManager = eventsCollection.ManageNotes(settings?.NoteDetectionSettings, sameTimeEventsComparison);
			Chords = new ChordsCollection(_notesManager.Notes.GetChords(settings));
			Chords.CollectionChanged += OnChordsCollectionChanged;
		}

		public void SaveChanges()
		{
			_notesManager.SaveChanges();
		}

		private void OnChordsCollectionChanged(ChordsCollection collection, ChordsCollectionChangedEventArgs args)
		{
			IEnumerable<Chord> addedChords = args.AddedChords;
			if (addedChords != null)
			{
				foreach (Chord item in addedChords)
				{
					AddNotes(item.Notes);
					SubscribeToChordEvents(item);
				}
			}
			IEnumerable<Chord> removedChords = args.RemovedChords;
			if (removedChords == null)
			{
				return;
			}
			foreach (Chord item2 in removedChords)
			{
				RemoveNotes(item2.Notes);
				UnsubscribeFromChordEvents(item2);
			}
		}

		private void OnChordNotesCollectionChanged(NotesCollection collection, NotesCollectionChangedEventArgs args)
		{
			IEnumerable<Note> addedNotes = args.AddedNotes;
			if (addedNotes != null)
			{
				AddNotes(addedNotes);
			}
			IEnumerable<Note> removedNotes = args.RemovedNotes;
			if (removedNotes != null)
			{
				RemoveNotes(removedNotes);
			}
		}

		private void SubscribeToChordEvents(Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			chord.NotesCollectionChanged += OnChordNotesCollectionChanged;
		}

		private void UnsubscribeFromChordEvents(Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			chord.NotesCollectionChanged -= OnChordNotesCollectionChanged;
		}

		private void AddNotes(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			_notesManager.Notes.Add(notes);
		}

		private void RemoveNotes(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			_notesManager.Notes.Remove(notes);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}
			if (disposing)
			{
				foreach (Chord chord in Chords)
				{
					UnsubscribeFromChordEvents(chord);
				}
				Chords.CollectionChanged -= OnChordsCollectionChanged;
				SaveChanges();
			}
			_disposed = true;
		}
	}
	public static class ChordsManagingUtilities
	{
		private interface IObjectDescriptor
		{
			bool ChordStart { get; }

			ITimedObject TimedObject { get; }
		}

		private interface IObjectDescriptorIndexed : IObjectDescriptor
		{
			Tuple<ITimedObject, int, int> IndexedTimedObject { get; }
		}

		private class TimedEventDescriptor : IObjectDescriptor
		{
			public bool ChordStart { get; }

			public ITimedObject TimedObject { get; }

			public TimedEventDescriptor(TimedEvent timedEvent)
			{
				TimedObject = timedEvent;
			}
		}

		private class CompleteChordDescriptor : IObjectDescriptor
		{
			public bool ChordStart { get; }

			public ITimedObject TimedObject { get; }

			public CompleteChordDescriptor(Chord chord)
			{
				TimedObject = chord;
			}
		}

		private sealed class TimedEventDescriptorIndexed : TimedEventDescriptor, IObjectDescriptorIndexed, IObjectDescriptor
		{
			private readonly int _index;

			public Tuple<ITimedObject, int, int> IndexedTimedObject => Tuple.Create(base.TimedObject, _index, _index);

			public TimedEventDescriptorIndexed(TimedEvent timedEvent, int index)
				: base(timedEvent)
			{
				_index = index;
			}
		}

		private class NoteDescriptor : IObjectDescriptor
		{
			public bool ChordStart { get; }

			public ITimedObject TimedObject { get; }

			public NoteDescriptor(Note note, bool chordStart)
			{
				TimedObject = note;
				ChordStart = chordStart;
			}
		}

		private class NoteDescriptorIndexed : NoteDescriptor, IObjectDescriptorIndexed, IObjectDescriptor
		{
			private readonly int _noteOnIndex;

			private readonly int _noteOffIndex;

			public Tuple<ITimedObject, int, int> IndexedTimedObject => Tuple.Create(base.TimedObject, _noteOnIndex, _noteOffIndex);

			public NoteDescriptorIndexed(Note note, int noteOnIndex, int noteOffIndex, bool chordStart)
				: base(note, chordStart)
			{
				_noteOnIndex = noteOnIndex;
				_noteOffIndex = noteOffIndex;
			}
		}

		private sealed class ChordDescriptor
		{
			private readonly int _notesMinCount;

			public long Time { get; }

			public List<LinkedListNode<IObjectDescriptor>> NotesNodes { get; } = new List<LinkedListNode<IObjectDescriptor>>(3);

			public bool IsSealed { get; set; }

			public bool IsCompleted => NotesNodes.Count >= _notesMinCount;

			public ChordDescriptor(long time, LinkedListNode<IObjectDescriptor> firstNoteNode, int notesMinCount)
			{
				Time = time;
				NotesNodes.Add(firstNoteNode);
				_notesMinCount = notesMinCount;
			}
		}

		private sealed class ChordDescriptorIndexed
		{
			private readonly int _notesMinCount;

			public long Time { get; }

			public int EventsCollectionIndex { get; set; }

			public List<LinkedListNode<IObjectDescriptorIndexed>> NotesNodes { get; } = new List<LinkedListNode<IObjectDescriptorIndexed>>(3);

			public bool IsSealed { get; set; }

			public bool IsCompleted => NotesNodes.Count >= _notesMinCount;

			public ChordDescriptorIndexed(long time, LinkedListNode<IObjectDescriptorIndexed> firstNoteNode, int notesMinCount)
			{
				Time = time;
				NotesNodes.Add(firstNoteNode);
				_notesMinCount = notesMinCount;
			}
		}

		public static Chord SetTimeAndLength(this Chord chord, ITimeSpan time, ITimeSpan length, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			chord.Time = TimeConverter.ConvertFrom(time, tempoMap);
			chord.Length = LengthConverter.ConvertFrom(length, chord.Time, tempoMap);
			return chord;
		}

		public static ChordsManager ManageChords(this EventsCollection eventsCollection, ChordDetectionSettings settings = null, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return new ChordsManager(eventsCollection, settings, sameTimeEventsComparison);
		}

		public static ChordsManager ManageChords(this TrackChunk trackChunk, ChordDetectionSettings settings = null, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.ManageChords(settings, sameTimeEventsComparison);
		}

		public static ICollection<Chord> GetChords(this IEnumerable<MidiEvent> midiEvents, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiEvents", midiEvents);
			List<Chord> list = new List<Chord>();
			foreach (Chord item in midiEvents.GetTimedEventsLazy().GetChordsAndNotesAndTimedEventsLazy(settings).OfType<Chord>())
			{
				list.Add(item);
			}
			return list;
		}

		public static ICollection<Chord> GetChords(this EventsCollection eventsCollection, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			List<Chord> list = new List<Chord>();
			IEnumerable<Chord> chordsLazy = new ChordsBuilder(settings).GetChordsLazy(eventsCollection.GetTimedEventsLazy());
			list.AddRange(chordsLazy);
			return list;
		}

		public static ICollection<Chord> GetChords(this TrackChunk trackChunk, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.GetChords(settings);
		}

		public static ICollection<Chord> GetChords(this IEnumerable<TrackChunk> trackChunks, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			EventsCollection[] array = trackChunks.Select((TrackChunk c) => c.Events).ToArray();
			switch (array.Length)
			{
			case 0:
				return new Chord[0];
			case 1:
				return array[0].GetChords(settings);
			default:
			{
				int num = array.Sum((EventsCollection e) => e.Count);
				List<Chord> list = new List<Chord>(num / 3);
				IEnumerable<Chord> chordsLazy = new ChordsBuilder(settings).GetChordsLazy(array.GetTimedEventsLazy(num));
				list.AddRange(chordsLazy);
				return list;
			}
			}
		}

		public static ICollection<Chord> GetChords(this MidiFile file, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().GetChords(settings);
		}

		public static IEnumerable<Chord> GetChords(this IEnumerable<Note> notes, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("notes", notes);
			return notes.GetChordsAndNotesAndTimedEventsLazy(settings).OfType<Chord>().ToArray();
		}

		public static int ProcessChords(this EventsCollection eventsCollection, Action<Chord> action, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			return eventsCollection.ProcessChords(action, (Chord chord) => true, settings);
		}

		public static int ProcessChords(this EventsCollection eventsCollection, Action<Chord> action, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return eventsCollection.ProcessChords(action, match, settings, canTimeOrLengthBeChanged: true);
		}

		public static int ProcessChords(this TrackChunk trackChunk, Action<Chord> action, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			return trackChunk.ProcessChords(action, (Chord chord) => true, settings);
		}

		public static int ProcessChords(this TrackChunk trackChunk, Action<Chord> action, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return trackChunk.Events.ProcessChords(action, match, settings);
		}

		public static int ProcessChords(this IEnumerable<TrackChunk> trackChunks, Action<Chord> action, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			return trackChunks.ProcessChords(action, (Chord chord) => true, settings);
		}

		public static int ProcessChords(this IEnumerable<TrackChunk> trackChunks, Action<Chord> action, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return trackChunks.ProcessChords(action, match, settings, canTimeOrLengthBeChanged: true);
		}

		public static int ProcessChords(this MidiFile file, Action<Chord> action, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			return file.ProcessChords(action, (Chord chord) => true, settings);
		}

		public static int ProcessChords(this MidiFile file, Action<Chord> action, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return file.GetTrackChunks().ProcessChords(action, match, settings);
		}

		public static int RemoveChords(this EventsCollection eventsCollection, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return eventsCollection.RemoveChords((Chord chord) => true, settings);
		}

		public static int RemoveChords(this EventsCollection eventsCollection, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("match", match);
			int num = eventsCollection.ProcessChords(delegate(Chord c)
			{
				foreach (Note note in c.Notes)
				{
					MidiEvent midiEvent = note.TimedNoteOnEvent.Event;
					bool flag = (note.TimedNoteOffEvent.Event.Flag = true);
					midiEvent.Flag = flag;
				}
			}, match, settings, canTimeOrLengthBeChanged: false);
			if (num == 0)
			{
				return 0;
			}
			eventsCollection.RemoveTimedEvents((TimedEvent e) => e.Event.Flag);
			return num;
		}

		public static int RemoveChords(this TrackChunk trackChunk, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.RemoveChords((Chord note) => true, settings);
		}

		public static int RemoveChords(this TrackChunk trackChunk, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("match", match);
			return trackChunk.Events.RemoveChords(match, settings);
		}

		public static int RemoveChords(this IEnumerable<TrackChunk> trackChunks, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return trackChunks.RemoveChords((Chord note) => true, settings);
		}

		public static int RemoveChords(this IEnumerable<TrackChunk> trackChunks, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("match", match);
			int num = trackChunks.ProcessChords(delegate(Chord c)
			{
				foreach (Note note in c.Notes)
				{
					MidiEvent midiEvent = note.TimedNoteOnEvent.Event;
					bool flag = (note.TimedNoteOffEvent.Event.Flag = true);
					midiEvent.Flag = flag;
				}
			}, match, settings, canTimeOrLengthBeChanged: false);
			if (num == 0)
			{
				return 0;
			}
			trackChunks.RemoveTimedEvents((TimedEvent e) => e.Event.Flag);
			return num;
		}

		public static int RemoveChords(this MidiFile file, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.RemoveChords((Chord chord) => true, settings);
		}

		public static int RemoveChords(this MidiFile file, Predicate<Chord> match, ChordDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("match", match);
			return file.GetTrackChunks().RemoveChords(match, settings);
		}

		public static Melanchall.DryWetMidi.MusicTheory.Chord GetMusicTheoryChord(this Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return new Melanchall.DryWetMidi.MusicTheory.Chord((from n in chord.Notes
				orderby n.NoteNumber
				select n.NoteName).ToArray());
		}

		internal static IEnumerable<Tuple<ITimedObject, int[]>> GetChordsAndNotesAndTimedEventsLazy(this IEnumerable<Tuple<TimedEvent, int>> timedEvents, ChordDetectionSettings settings)
		{
			settings = settings ?? new ChordDetectionSettings();
			LinkedList<IObjectDescriptorIndexed> timedObjects = new LinkedList<IObjectDescriptorIndexed>();
			LinkedList<ChordDescriptorIndexed> chordsDescriptors = new LinkedList<ChordDescriptorIndexed>();
			LinkedListNode<ChordDescriptorIndexed>[] chordsDescriptorsByChannel = new LinkedListNode<ChordDescriptorIndexed>[(byte)FourBitNumber.MaxValue + 1];
			long notesTolerance = settings.NotesTolerance;
			bool eventsCollectionShouldMatch = settings.ChordSearchContext == ChordSearchContext.SingleEventsCollection;
			foreach (Tuple<ITimedObject, int, int> timedObjectTuple in timedEvents.GetNotesAndTimedEventsLazy(settings.NoteDetectionSettings ?? new NoteDetectionSettings()))
			{
				ITimedObject item = timedObjectTuple.Item1;
				if (item is TimedEvent timedEvent)
				{
					if (timedObjects.Count == 0)
					{
						yield return Tuple.Create(item, new int[1] { timedObjectTuple.Item2 });
					}
					else
					{
						timedObjects.AddLast(new TimedEventDescriptorIndexed(timedEvent, timedObjectTuple.Item2));
					}
					continue;
				}
				Note note = (Note)item;
				LinkedListNode<ChordDescriptorIndexed> linkedListNode = chordsDescriptorsByChannel[(byte)note.Channel];
				if (timedObjects.Count == 0 || linkedListNode == null || linkedListNode.List == null)
				{
					CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, timedObjects, note, timedObjectTuple.Item2, timedObjectTuple.Item3, settings);
					continue;
				}
				ChordDescriptorIndexed value = linkedListNode.Value;
				if (CanNoteBeAddedToChord(value, note, notesTolerance, timedObjectTuple.Item2, eventsCollectionShouldMatch))
				{
					LinkedListNode<IObjectDescriptorIndexed> item2 = timedObjects.AddLast(new NoteDescriptorIndexed(note, timedObjectTuple.Item2, timedObjectTuple.Item3, chordStart: false));
					value.NotesNodes.Add(item2);
					continue;
				}
				value.IsSealed = true;
				if (linkedListNode.Previous == null)
				{
					foreach (Tuple<ITimedObject, int[]> timedObject in GetTimedObjects(linkedListNode, chordsDescriptors, timedObjects, getSealedOnly: true))
					{
						yield return timedObject;
					}
				}
				CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, timedObjects, note, timedObjectTuple.Item2, timedObjectTuple.Item3, settings);
			}
			foreach (Tuple<ITimedObject, int[]> timedObject2 in GetTimedObjects(chordsDescriptors.First, chordsDescriptors, timedObjects, getSealedOnly: false))
			{
				yield return timedObject2;
			}
		}

		internal static IEnumerable<ITimedObject> GetChordsAndNotesAndTimedEventsLazy(this IEnumerable<TimedEvent> timedEvents, ChordDetectionSettings settings)
		{
			settings = settings ?? new ChordDetectionSettings();
			return timedEvents.GetNotesAndTimedEventsLazy(settings.NoteDetectionSettings ?? new NoteDetectionSettings()).GetChordsAndNotesAndTimedEventsLazy(settings);
		}

		internal static IEnumerable<ITimedObject> GetChordsAndNotesAndTimedEventsLazy(this IEnumerable<ITimedObject> notesAndTimedEvents, ChordDetectionSettings settings)
		{
			return notesAndTimedEvents.GetChordsAndNotesAndTimedEventsLazy(settings, chordsAllowed: false);
		}

		internal static IEnumerable<ITimedObject> GetChordsAndNotesAndTimedEventsLazy(this IEnumerable<ITimedObject> notesAndTimedEvents, ChordDetectionSettings settings, bool chordsAllowed)
		{
			settings = settings ?? new ChordDetectionSettings();
			LinkedList<IObjectDescriptor> timedObjects = new LinkedList<IObjectDescriptor>();
			LinkedList<ChordDescriptor> chordsDescriptors = new LinkedList<ChordDescriptor>();
			LinkedListNode<ChordDescriptor>[] chordsDescriptorsByChannel = new LinkedListNode<ChordDescriptor>[(byte)FourBitNumber.MaxValue + 1];
			foreach (ITimedObject notesAndTimedEvent in notesAndTimedEvents)
			{
				if (chordsAllowed && notesAndTimedEvent is Chord chord)
				{
					if (timedObjects.Count == 0)
					{
						yield return chord;
					}
					else
					{
						timedObjects.AddLast(new CompleteChordDescriptor(chord));
					}
					continue;
				}
				if (notesAndTimedEvent is TimedEvent timedEvent)
				{
					if (timedObjects.Count == 0)
					{
						yield return notesAndTimedEvent;
					}
					else
					{
						timedObjects.AddLast(new TimedEventDescriptor(timedEvent));
					}
					continue;
				}
				Note note = (Note)notesAndTimedEvent;
				LinkedListNode<ChordDescriptor> linkedListNode = chordsDescriptorsByChannel[(byte)note.Channel];
				if (timedObjects.Count == 0 || linkedListNode == null || linkedListNode.List == null)
				{
					CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, timedObjects, note, settings);
					continue;
				}
				ChordDescriptor value = linkedListNode.Value;
				if (CanNoteBeAddedToChord(value, note, settings.NotesTolerance))
				{
					LinkedListNode<IObjectDescriptor> item = timedObjects.AddLast(new NoteDescriptor(note, chordStart: false));
					value.NotesNodes.Add(item);
					continue;
				}
				value.IsSealed = true;
				if (linkedListNode.Previous == null)
				{
					foreach (ITimedObject timedObject in GetTimedObjects(linkedListNode, chordsDescriptors, timedObjects, getSealedOnly: true))
					{
						yield return timedObject;
					}
				}
				CreateChordDescriptor(chordsDescriptors, chordsDescriptorsByChannel, timedObjects, note, settings);
			}
			foreach (ITimedObject timedObject2 in GetTimedObjects(chordsDescriptors.First, chordsDescriptors, timedObjects, getSealedOnly: false))
			{
				yield return timedObject2;
			}
		}

		internal static int ProcessChords(this EventsCollection eventsCollection, Action<Chord> action, Predicate<Chord> match, ChordDetectionSettings settings, bool canTimeOrLengthBeChanged)
		{
			settings = settings ?? new ChordDetectionSettings();
			int num = 0;
			bool flag = false;
			List<TimedEvent> list = (canTimeOrLengthBeChanged ? new List<TimedEvent>(eventsCollection.Count) : null);
			foreach (Chord item in new ChordsBuilder(settings).GetChordsLazy(eventsCollection.GetTimedEventsLazy(cloneEvent: false), canTimeOrLengthBeChanged, list))
			{
				if (match(item))
				{
					long time = item.Time;
					long length = item.Length;
					action(item);
					flag |= item.Time != time || item.Length != length;
					num++;
				}
			}
			if (flag)
			{
				eventsCollection.SortAndUpdateEvents(list);
			}
			return num;
		}

		internal static int ProcessChords(this IEnumerable<TrackChunk> trackChunks, Action<Chord> action, Predicate<Chord> match, ChordDetectionSettings settings, bool canTimeOrLengthBeChanged)
		{
			settings = settings ?? new ChordDetectionSettings();
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int num = array.Sum((EventsCollection c) => c.Count);
			int num2 = 0;
			bool flag = false;
			List<Tuple<TimedEvent, int>> list = (canTimeOrLengthBeChanged ? new List<Tuple<TimedEvent, int>>(num) : null);
			foreach (Chord item in new ChordsBuilder(settings).GetChordsLazy(array.GetTimedEventsLazy(num, cloneEvent: false), canTimeOrLengthBeChanged, list))
			{
				if (match(item))
				{
					long time = item.Time;
					long length = item.Length;
					action(item);
					flag |= item.Time != time || item.Length != length;
					num2++;
				}
			}
			if (flag)
			{
				array.SortAndUpdateEvents(list);
			}
			return num2;
		}

		private static IEnumerable<ITimedObject> GetTimedObjects(LinkedListNode<ChordDescriptor> startChordDescriptorNode, LinkedList<ChordDescriptor> chordsDescriptors, LinkedList<IObjectDescriptor> timedObjects, bool getSealedOnly)
		{
			LinkedListNode<ChordDescriptor> chordDescriptorNode = startChordDescriptorNode;
			while (chordDescriptorNode != null)
			{
				ChordDescriptor value = chordDescriptorNode.Value;
				if (getSealedOnly && !value.IsSealed)
				{
					break;
				}
				foreach (LinkedListNode<IObjectDescriptor> notesNode in value.NotesNodes)
				{
					timedObjects.Remove(notesNode);
				}
				if (value.IsCompleted)
				{
					yield return new Chord(value.NotesNodes.Select((LinkedListNode<IObjectDescriptor> n) => (Note)((NoteDescriptor)n.Value).TimedObject));
				}
				LinkedListNode<IObjectDescriptor> node = timedObjects.First;
				while (node != null && !node.Value.ChordStart)
				{
					yield return node.Value.TimedObject;
					LinkedListNode<IObjectDescriptor> next = node.Next;
					timedObjects.Remove(node);
					node = next;
				}
				LinkedListNode<ChordDescriptor> next2 = chordDescriptorNode.Next;
				chordsDescriptors.Remove(chordDescriptorNode);
				chordDescriptorNode = next2;
			}
		}

		private static IEnumerable<Tuple<ITimedObject, int[]>> GetTimedObjects(LinkedListNode<ChordDescriptorIndexed> startChordDescriptorNode, LinkedList<ChordDescriptorIndexed> chordsDescriptors, LinkedList<IObjectDescriptorIndexed> timedObjects, bool getSealedOnly)
		{
			LinkedListNode<ChordDescriptorIndexed> chordDescriptorNode = startChordDescriptorNode;
			while (chordDescriptorNode != null)
			{
				ChordDescriptorIndexed chordDescriptor = chordDescriptorNode.Value;
				if (getSealedOnly && !chordDescriptor.IsSealed)
				{
					break;
				}
				if (chordDescriptor.IsCompleted)
				{
					int count = chordDescriptor.NotesNodes.Count;
					Note[] array = new Note[count];
					int[] array2 = new int[count * 2];
					for (int i = 0; i < count; i++)
					{
						LinkedListNode<IObjectDescriptorIndexed> node = chordDescriptor.NotesNodes[i];
						timedObjects.Remove(node);
						IObjectDescriptorIndexed value = chordDescriptor.NotesNodes[i].Value;
						array[i] = (Note)value.TimedObject;
						array2[i * 2] = value.IndexedTimedObject.Item2;
						array2[i * 2 + 1] = value.IndexedTimedObject.Item3;
					}
					yield return Tuple.Create((ITimedObject)new Chord(array), array2);
				}
				LinkedListNode<IObjectDescriptorIndexed> node2 = timedObjects.First;
				while (node2 != null && (!chordDescriptor.IsCompleted || !node2.Value.ChordStart))
				{
					Tuple<ITimedObject, int, int> indexedTimedObject = node2.Value.IndexedTimedObject;
					yield return Tuple.Create(indexedTimedObject.Item1, new int[2] { indexedTimedObject.Item2, indexedTimedObject.Item3 });
					LinkedListNode<IObjectDescriptorIndexed> next = node2.Next;
					timedObjects.Remove(node2);
					node2 = next;
				}
				LinkedListNode<ChordDescriptorIndexed> next2 = chordDescriptorNode.Next;
				chordsDescriptors.Remove(chordDescriptorNode);
				chordDescriptorNode = next2;
			}
		}

		private static void CreateChordDescriptor(LinkedList<ChordDescriptor> chordsDescriptors, LinkedListNode<ChordDescriptor>[] chordsDescriptorsByChannel, LinkedList<IObjectDescriptor> timedObjects, Note note, ChordDetectionSettings settings)
		{
			LinkedListNode<IObjectDescriptor> firstNoteNode = timedObjects.AddLast(new NoteDescriptor(note, chordStart: true));
			ChordDescriptor value = new ChordDescriptor(note.Time, firstNoteNode, settings.NotesMinCount);
			chordsDescriptorsByChannel[(byte)note.Channel] = chordsDescriptors.AddLast(value);
		}

		private static void CreateChordDescriptor(LinkedList<ChordDescriptorIndexed> chordsDescriptors, LinkedListNode<ChordDescriptorIndexed>[] chordsDescriptorsByChannel, LinkedList<IObjectDescriptorIndexed> timedObjects, Note note, int noteOnIndex, int noteOffIndex, ChordDetectionSettings settings)
		{
			LinkedListNode<IObjectDescriptorIndexed> firstNoteNode = timedObjects.AddLast(new NoteDescriptorIndexed(note, noteOnIndex, noteOffIndex, chordStart: true));
			ChordDescriptorIndexed value = new ChordDescriptorIndexed(note.Time, firstNoteNode, settings.NotesMinCount)
			{
				EventsCollectionIndex = noteOnIndex
			};
			chordsDescriptorsByChannel[(byte)note.Channel] = chordsDescriptors.AddLast(value);
		}

		private static bool CanNoteBeAddedToChord(ChordDescriptor chordDescriptor, Note note, long notesTolerance)
		{
			return note.Time - chordDescriptor.Time <= notesTolerance;
		}

		private static bool CanNoteBeAddedToChord(ChordDescriptorIndexed chordDescriptor, Note note, long notesTolerance, int eventsCollectionIndex, bool eventsCollectionShouldMatch)
		{
			if (note.Time - chordDescriptor.Time <= notesTolerance)
			{
				if (eventsCollectionShouldMatch)
				{
					return chordDescriptor.EventsCollectionIndex == eventsCollectionIndex;
				}
				return true;
			}
			return false;
		}
	}
	public static class GetObjectsUtilities
	{
		private static readonly object NoSeparationNoteDescriptor = new object();

		private static readonly Dictionary<RestSeparationPolicy, Func<Note, object>> NoteDescriptorProviders = new Dictionary<RestSeparationPolicy, Func<Note, object>>
		{
			[RestSeparationPolicy.NoSeparation] = (Note n) => NoSeparationNoteDescriptor,
			[RestSeparationPolicy.SeparateByChannel] = (Note n) => n.Channel,
			[RestSeparationPolicy.SeparateByNoteNumber] = (Note n) => n.NoteNumber,
			[RestSeparationPolicy.SeparateByChannelAndNoteNumber] = (Note n) => n.GetNoteId()
		};

		private static readonly Dictionary<RestSeparationPolicy, bool> SetRestChannel = new Dictionary<RestSeparationPolicy, bool>
		{
			[RestSeparationPolicy.NoSeparation] = false,
			[RestSeparationPolicy.SeparateByChannel] = true,
			[RestSeparationPolicy.SeparateByNoteNumber] = false,
			[RestSeparationPolicy.SeparateByChannelAndNoteNumber] = true
		};

		private static readonly Dictionary<RestSeparationPolicy, bool> SetRestNoteNumber = new Dictionary<RestSeparationPolicy, bool>
		{
			[RestSeparationPolicy.NoSeparation] = false,
			[RestSeparationPolicy.SeparateByChannel] = false,
			[RestSeparationPolicy.SeparateByNoteNumber] = true,
			[RestSeparationPolicy.SeparateByChannelAndNoteNumber] = true
		};

		public static ICollection<ITimedObject> GetObjects(this IEnumerable<MidiEvent> midiEvents, ObjectType objectType, ObjectDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiEvents", midiEvents);
			return midiEvents.GetTimedEventsLazy().GetObjectsFromSortedTimedObjects(0, objectType, settings);
		}

		public static ICollection<ITimedObject> GetObjects(this EventsCollection eventsCollection, ObjectType objectType, ObjectDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return eventsCollection.GetTimedEventsLazy().GetObjectsFromSortedTimedObjects(eventsCollection.Count / 2, objectType, settings);
		}

		public static ICollection<ITimedObject> GetObjects(this TrackChunk trackChunk, ObjectType objectType, ObjectDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.GetObjects(objectType, settings);
		}

		public static ICollection<ITimedObject> GetObjects(this IEnumerable<TrackChunk> trackChunks, ObjectType objectType, ObjectDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int num = array.Sum((EventsCollection c) => c.Count);
			IEnumerable<Tuple<TimedEvent, int>> timedEventsLazy = array.GetTimedEventsLazy(num);
			IEnumerable<ITimedObject> processedTimedObjects = timedEventsLazy.Select((Tuple<TimedEvent, int> o) => o.Item1);
			if (objectType.HasFlag(ObjectType.Chord) || objectType.HasFlag(ObjectType.Note) || objectType.HasFlag(ObjectType.Rest))
			{
				processedTimedObjects = ((!objectType.HasFlag(ObjectType.Chord)) ? (from o in timedEventsLazy.GetNotesAndTimedEventsLazy(settings?.NoteDetectionSettings ?? new NoteDetectionSettings())
					select o.Item1) : (from o in timedEventsLazy.GetChordsAndNotesAndTimedEventsLazy(settings?.ChordDetectionSettings ?? new ChordDetectionSettings())
					select o.Item1));
			}
			return processedTimedObjects.GetObjectsFromSortedTimedObjects(num / 2, objectType, settings, createNotes: false);
		}

		public static ICollection<ITimedObject> GetObjects(this MidiFile midiFile, ObjectType objectType, ObjectDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.GetTrackChunks().GetObjects(objectType, settings);
		}

		public static ICollection<ITimedObject> GetObjects(this IEnumerable<ITimedObject> timedObjects, ObjectType objectType, ObjectDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			bool getChords = objectType.HasFlag(ObjectType.Chord);
			bool getNotes = objectType.HasFlag(ObjectType.Note);
			int num = 0;
			List<ITimedObject> list = new List<ITimedObject>();
			foreach (ITimedObject timedObject in timedObjects)
			{
				if (TryProcessTimedEvent(timedObject as TimedEvent, list) || TryProcessNote(timedObject as Note, list, getNotes, getChords) || TryProcessChord(timedObject as Chord, list, getNotes, getChords))
				{
					num++;
				}
			}
			return list.OrderBy((ITimedObject o) => o.Time).GetObjectsFromSortedTimedObjects(num, objectType, settings);
		}

		private static bool TryProcessTimedEvent(TimedEvent timedEvent, List<ITimedObject> processedTimedObjects)
		{
			if (timedEvent == null)
			{
				return false;
			}
			processedTimedObjects.Add(timedEvent);
			return true;
		}

		private static bool TryProcessNote(Note note, List<ITimedObject> processedTimedObjects, bool getNotes, bool getChords)
		{
			if (note == null)
			{
				return false;
			}
			if (getNotes || getChords)
			{
				processedTimedObjects.Add(note);
			}
			else
			{
				processedTimedObjects.Add(note.TimedNoteOnEvent);
				processedTimedObjects.Add(note.TimedNoteOffEvent);
			}
			return true;
		}

		private static bool TryProcessChord(Chord chord, List<ITimedObject> processedTimedObjects, bool getNotes, bool getChords)
		{
			if (chord == null)
			{
				return false;
			}
			if (getChords)
			{
				processedTimedObjects.Add(chord);
			}
			else if (getNotes)
			{
				processedTimedObjects.AddRange(chord.Notes);
			}
			else
			{
				foreach (Note note in chord.Notes)
				{
					processedTimedObjects.Add(note.TimedNoteOnEvent);
					processedTimedObjects.Add(note.TimedNoteOffEvent);
				}
			}
			return true;
		}

		private static ICollection<ITimedObject> GetObjectsFromSortedTimedObjects(this IEnumerable<ITimedObject> processedTimedObjects, int resultCollectionSize, ObjectType objectType, ObjectDetectionSettings settings, bool createNotes = true)
		{
			bool flag = objectType.HasFlag(ObjectType.Chord);
			bool flag2 = objectType.HasFlag(ObjectType.Note);
			bool flag3 = objectType.HasFlag(ObjectType.Rest);
			bool flag4 = objectType.HasFlag(ObjectType.TimedEvent);
			settings = settings ?? new ObjectDetectionSettings();
			NoteDetectionSettings settings2 = settings.NoteDetectionSettings ?? new NoteDetectionSettings();
			ChordDetectionSettings settings3 = settings.ChordDetectionSettings ?? new ChordDetectionSettings();
			RestDetectionSettings restDetectionSettings = settings.RestDetectionSettings ?? new RestDetectionSettings();
			IEnumerable<ITimedObject> enumerable = processedTimedObjects;
			if (createNotes && (flag || flag2 || flag3))
			{
				IEnumerable<ITimedObject> notesAndTimedEventsLazy = processedTimedObjects.GetNotesAndTimedEventsLazy(settings2, completeObjectsAllowed: true);
				enumerable = (flag ? notesAndTimedEventsLazy.GetChordsAndNotesAndTimedEventsLazy(settings3, chordsAllowed: true) : notesAndTimedEventsLazy);
			}
			List<ITimedObject> list = ((resultCollectionSize > 0) ? new List<ITimedObject>(resultCollectionSize) : new List<ITimedObject>());
			Dictionary<object, long> dictionary = new Dictionary<object, long>();
			Func<Note, object> func = NoteDescriptorProviders[restDetectionSettings.RestSeparationPolicy];
			bool flag5 = SetRestChannel[restDetectionSettings.RestSeparationPolicy];
			bool flag6 = SetRestNoteNumber[restDetectionSettings.RestSeparationPolicy];
			foreach (ITimedObject item4 in enumerable)
			{
				if (flag && item4 is Chord item)
				{
					list.Add(item);
				}
				if (flag2 && item4 is Note item2)
				{
					list.Add(item2);
				}
				if (flag4 && item4 is TimedEvent item3)
				{
					list.Add(item3);
				}
				if (!flag3 || !(item4 is Note note))
				{
					continue;
				}
				object key = func(note);
				dictionary.TryGetValue(key, out var value);
				if (note.Time > value)
				{
					Rest rest = new Rest(value, note.Time - value, flag5 ? new FourBitNumber?(note.Channel) : ((FourBitNumber?)null), flag6 ? new SevenBitNumber?(note.NoteNumber) : ((SevenBitNumber?)null));
					if (list.Count > 0)
					{
						int num = list.Count - 1;
						while (num >= 0 && rest.Time < list[num].Time)
						{
							num--;
						}
						num++;
						if (num >= list.Count)
						{
							list.Add(rest);
						}
						else
						{
							list.Insert(num, rest);
						}
					}
					else
					{
						list.Add(rest);
					}
				}
				dictionary[key] = Math.Max(value, note.Time + note.Length);
			}
			list.TrimExcess();
			return list;
		}
	}
	public sealed class ObjectDetectionSettings
	{
		public NoteDetectionSettings NoteDetectionSettings { get; set; } = new NoteDetectionSettings();

		public ChordDetectionSettings ChordDetectionSettings { get; set; } = new ChordDetectionSettings();

		public RestDetectionSettings RestDetectionSettings { get; set; } = new RestDetectionSettings();
	}
	[Flags]
	public enum ObjectType
	{
		TimedEvent = 1,
		Note = 2,
		Chord = 4,
		Rest = 8
	}
	public sealed class Rest : ILengthedObject, ITimedObject
	{
		private readonly long _time;

		private readonly long _length;

		public long Time
		{
			get
			{
				return _time;
			}
			set
			{
				throw new InvalidOperationException("Setting time of rest is not allowed.");
			}
		}

		public long Length
		{
			get
			{
				return _length;
			}
			set
			{
				throw new InvalidOperationException("Setting length of rest is not allowed.");
			}
		}

		public FourBitNumber? Channel { get; }

		public SevenBitNumber? NoteNumber { get; }

		internal Rest(long time, long length, FourBitNumber? channel, SevenBitNumber? noteNumber)
		{
			_time = time;
			_length = length;
			Channel = channel;
			NoteNumber = noteNumber;
		}

		public static bool operator ==(Rest rest1, Rest rest2)
		{
			if ((object)rest1 == rest2)
			{
				return true;
			}
			if ((object)rest1 == null || (object)rest2 == null)
			{
				return false;
			}
			if (rest1.Time == rest2.Time && rest1.Length == rest2.Length && (byte?)rest1.Channel == (byte?)rest2.Channel)
			{
				return (byte?)rest1.NoteNumber == (byte?)rest2.NoteNumber;
			}
			return false;
		}

		public static bool operator !=(Rest rest1, Rest rest2)
		{
			return !(rest1 == rest2);
		}

		public override string ToString()
		{
			return $"Rest (channel = {Channel}, note number = {NoteNumber})";
		}

		public override bool Equals(object obj)
		{
			return this == obj as Rest;
		}

		public override int GetHashCode()
		{
			return (((17 * 23 + Time.GetHashCode()) * 23 + Length.GetHashCode()) * 23 + Channel.GetHashCode()) * 23 + NoteNumber.GetHashCode();
		}
	}
	public enum RestSeparationPolicy
	{
		NoSeparation,
		SeparateByChannel,
		SeparateByNoteNumber,
		SeparateByChannelAndNoteNumber
	}
	public sealed class RestDetectionSettings
	{
		private RestSeparationPolicy _restSeparationPolicy;

		public RestSeparationPolicy RestSeparationPolicy
		{
			get
			{
				return _restSeparationPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_restSeparationPolicy = value;
			}
		}
	}
	public sealed class ArbitraryGrid : IGrid
	{
		public IEnumerable<ITimeSpan> Times { get; }

		public ArbitraryGrid(IEnumerable<ITimeSpan> times)
		{
			ThrowIfArgument.IsNull("times", times);
			ThrowIfArgument.ContainsNull("times", times);
			Times = times;
		}

		public ArbitraryGrid(params ITimeSpan[] times)
			: this((IEnumerable<ITimeSpan>)times)
		{
		}

		public IEnumerable<long> GetTimes(TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return Times.Select((ITimeSpan t) => TimeConverter.ConvertFrom(t, tempoMap));
		}
	}
	public interface IGrid
	{
		IEnumerable<long> GetTimes(TempoMap tempoMap);
	}
	public sealed class SteppedGrid : IGrid
	{
		public ITimeSpan Start { get; }

		public IEnumerable<ITimeSpan> Steps { get; }

		public SteppedGrid(ITimeSpan step)
			: this((MidiTimeSpan)0L, step)
		{
		}

		public SteppedGrid(ITimeSpan start, ITimeSpan step)
		{
			ThrowIfArgument.IsNull("start", start);
			ThrowIfArgument.IsNull("step", step);
			Start = start;
			Steps = new ITimeSpan[1] { step };
		}

		public SteppedGrid(IEnumerable<ITimeSpan> steps)
			: this((MidiTimeSpan)0L, steps)
		{
		}

		public SteppedGrid(ITimeSpan start, IEnumerable<ITimeSpan> steps)
		{
			ThrowIfArgument.IsNull("start", start);
			ThrowIfArgument.IsNull("steps", steps);
			ThrowIfArgument.ContainsNull("steps", steps);
			Start = start;
			Steps = steps;
		}

		public IEnumerable<long> GetTimes(TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (!Steps.Any())
			{
				yield break;
			}
			long time = TimeConverter.ConvertFrom(Start, tempoMap);
			yield return time;
			while (true)
			{
				foreach (ITimeSpan step in Steps)
				{
					time += LengthConverter.ConvertFrom(step, time, tempoMap);
					yield return time;
				}
			}
		}
	}
	public interface IMusicalObject
	{
		FourBitNumber Channel { get; }
	}
	public interface ILengthedObject : ITimedObject
	{
		long Length { get; set; }
	}
	public interface INotifyLengthChanged
	{
		event EventHandler<LengthChangedEventArgs> LengthChanged;
	}
	public sealed class LengthChangedEventArgs : EventArgs
	{
		public long OldLength { get; }

		public long NewLength { get; }

		internal LengthChangedEventArgs(long oldLength, long newLength)
		{
			OldLength = oldLength;
			NewLength = newLength;
		}
	}
	public enum LengthedObjectPart
	{
		Start,
		End,
		Entire
	}
	public static class LengthedObjectUtilities
	{
		public static TLength LengthAs<TLength>(this ILengthedObject obj, TempoMap tempoMap) where TLength : ITimeSpan
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return LengthConverter.ConvertTo<TLength>(obj.Length, obj.Time, tempoMap);
		}

		public static ITimeSpan LengthAs(this ILengthedObject obj, TimeSpanType lengthType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return LengthConverter.ConvertTo(obj.Length, lengthType, obj.Time, tempoMap);
		}

		public static TTime EndTimeAs<TTime>(this ILengthedObject obj, TempoMap tempoMap) where TTime : ITimeSpan
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo<TTime>(obj.Time + obj.Length, tempoMap);
		}

		public static ITimeSpan EndTimeAs(this ILengthedObject obj, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsInvalidEnumValue("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo(obj.Time + obj.Length, timeType, tempoMap);
		}

		public static IEnumerable<TObject> StartAtTime<TObject>(this IEnumerable<TObject> objects, long time) where TObject : ILengthedObject
		{
			return objects.AtTime(time, LengthedObjectPart.Start);
		}

		public static IEnumerable<TObject> EndAtTime<TObject>(this IEnumerable<TObject> objects, long time) where TObject : ILengthedObject
		{
			return objects.AtTime(time, LengthedObjectPart.End);
		}

		public static IEnumerable<TObject> StartAtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap) where TObject : ILengthedObject
		{
			return objects.AtTime(time, tempoMap, LengthedObjectPart.Start);
		}

		public static IEnumerable<TObject> EndAtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap) where TObject : ILengthedObject
		{
			return objects.AtTime(time, tempoMap, LengthedObjectPart.End);
		}

		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, long time, LengthedObjectPart matchBy) where TObject : ILengthedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsInvalidEnumValue("matchBy", matchBy);
			return objects.Where((TObject o) => o != null && IsObjectAtTime(o, time, matchBy));
		}

		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap, LengthedObjectPart matchBy) where TObject : ILengthedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsInvalidEnumValue("matchBy", matchBy);
			long time2 = TimeConverter.ConvertFrom(time, tempoMap);
			return objects.AtTime(time2, matchBy);
		}

		private static bool IsObjectAtTime<TObject>(TObject obj, long time, LengthedObjectPart matchBy) where TObject : ILengthedObject
		{
			long time2 = obj.Time;
			if (time2 == time && (matchBy == LengthedObjectPart.Start || matchBy == LengthedObjectPart.Entire))
			{
				return true;
			}
			long num = time2 + obj.Length;
			if (num == time && (matchBy == LengthedObjectPart.End || matchBy == LengthedObjectPart.Entire))
			{
				return true;
			}
			if (matchBy == LengthedObjectPart.Entire && time >= time2)
			{
				return time <= num;
			}
			return false;
		}
	}
	public sealed class SplitLengthedObject<TObject> where TObject : ILengthedObject
	{
		public TObject LeftPart { get; }

		public TObject RightPart { get; }

		internal SplitLengthedObject(TObject leftPart, TObject rightPart)
		{
			LeftPart = leftPart;
			RightPart = rightPart;
		}
	}
	public class Note : ILengthedObject, ITimedObject, IMusicalObject, INotifyTimeChanged, INotifyLengthChanged
	{
		public static readonly SevenBitNumber DefaultVelocity = (SevenBitNumber)100;

		public long Time
		{
			get
			{
				return TimedNoteOnEvent.Time;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				long time = Time;
				if (value != time)
				{
					TimedNoteOffEvent.Time = value + Length;
					TimedNoteOnEvent.Time = value;
					this.TimeChanged?.Invoke(this, new TimeChangedEventArgs(time, value));
				}
			}
		}

		public long Length
		{
			get
			{
				return TimedNoteOffEvent.Time - TimedNoteOnEvent.Time;
			}
			set
			{
				ThrowIfLengthArgument.IsNegative("value", value);
				long length = Length;
				if (value != length)
				{
					TimedNoteOffEvent.Time = TimedNoteOnEvent.Time + value;
					this.LengthChanged?.Invoke(this, new LengthChangedEventArgs(length, value));
				}
			}
		}

		public SevenBitNumber NoteNumber
		{
			get
			{
				return ((NoteOnEvent)TimedNoteOnEvent.Event).NoteNumber;
			}
			set
			{
				((NoteOnEvent)TimedNoteOnEvent.Event).NoteNumber = value;
				((NoteOffEvent)TimedNoteOffEvent.Event).NoteNumber = value;
			}
		}

		public SevenBitNumber Velocity
		{
			get
			{
				return ((NoteOnEvent)TimedNoteOnEvent.Event).Velocity;
			}
			set
			{
				((NoteOnEvent)TimedNoteOnEvent.Event).Velocity = value;
			}
		}

		public SevenBitNumber OffVelocity
		{
			get
			{
				return ((NoteOffEvent)TimedNoteOffEvent.Event).Velocity;
			}
			set
			{
				((NoteOffEvent)TimedNoteOffEvent.Event).Velocity = value;
			}
		}

		public FourBitNumber Channel
		{
			get
			{
				return ((NoteOnEvent)TimedNoteOnEvent.Event).Channel;
			}
			set
			{
				((NoteOnEvent)TimedNoteOnEvent.Event).Channel = value;
				((NoteOffEvent)TimedNoteOffEvent.Event).Channel = value;
			}
		}

		public NoteName NoteName => ((NoteOnEvent)TimedNoteOnEvent.Event).GetNoteName();

		public int Octave => ((NoteOnEvent)TimedNoteOnEvent.Event).GetNoteOctave();

		internal TimedEvent TimedNoteOnEvent { get; } = new TimedEvent(new NoteOnEvent
		{
			Velocity = DefaultVelocity
		});

		internal TimedEvent TimedNoteOffEvent { get; } = new TimedEvent(new NoteOffEvent());

		internal Melanchall.DryWetMidi.MusicTheory.Note UnderlyingNote => Melanchall.DryWetMidi.MusicTheory.Note.Get(NoteNumber);

		public event EventHandler<TimeChangedEventArgs> TimeChanged;

		public event EventHandler<LengthChangedEventArgs> LengthChanged;

		public Note(NoteName noteName, int octave)
			: this(noteName, octave, 0L)
		{
		}

		public Note(NoteName noteName, int octave, long length)
			: this(noteName, octave, length, 0L)
		{
		}

		public Note(NoteName noteName, int octave, long length, long time)
			: this(NoteUtilities.GetNoteNumber(noteName, octave))
		{
			Length = length;
			Time = time;
		}

		public Note(SevenBitNumber noteNumber)
			: this(noteNumber, 0L)
		{
		}

		public Note(SevenBitNumber noteNumber, long length)
			: this(noteNumber, length, 0L)
		{
		}

		public Note(SevenBitNumber noteNumber, long length, long time)
		{
			NoteNumber = noteNumber;
			Length = length;
			Time = time;
		}

		internal Note(TimedEvent timedNoteOnEvent, TimedEvent timedNoteOffEvent)
		{
			NoteOnEvent noteOnEvent = (NoteOnEvent)timedNoteOnEvent.Event;
			NoteOffEvent noteOffEvent = (NoteOffEvent)timedNoteOffEvent.Event;
			TimedNoteOnEvent = timedNoteOnEvent;
			TimedNoteOffEvent = timedNoteOffEvent;
			Velocity = noteOnEvent.Velocity;
			OffVelocity = noteOffEvent.Velocity;
			Channel = noteOnEvent.Channel;
		}

		public TimedEvent GetTimedNoteOnEvent()
		{
			return TimedNoteOnEvent.Clone();
		}

		public TimedEvent GetTimedNoteOffEvent()
		{
			return TimedNoteOffEvent.Clone();
		}

		public void SetNoteNameAndOctave(NoteName noteName, int octave)
		{
			NoteNumber = NoteUtilities.GetNoteNumber(noteName, octave);
		}

		public virtual Note Clone()
		{
			TimedEvent timedNoteOnEvent = GetTimedNoteOnEvent();
			timedNoteOnEvent._time = TimedNoteOnEvent.Time;
			TimedEvent timedNoteOffEvent = GetTimedNoteOffEvent();
			timedNoteOffEvent._time = TimedNoteOffEvent.Time;
			return new Note(timedNoteOnEvent, timedNoteOffEvent);
		}

		public SplitLengthedObject<Note> Split(long time)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			long time2 = Time;
			long num = time2 + Length;
			if (time <= time2)
			{
				return new SplitLengthedObject<Note>(null, Clone());
			}
			if (time >= num)
			{
				return new SplitLengthedObject<Note>(Clone(), null);
			}
			Note note = Clone();
			note.Length = time - time2;
			Note note2 = Clone();
			note2.Time = time;
			note2.Length = num - time;
			return new SplitLengthedObject<Note>(note, note2);
		}

		public override string ToString()
		{
			return UnderlyingNote.ToString();
		}
	}
	public sealed class NoteDetectionSettings
	{
		private NoteStartDetectionPolicy _noteStartDetectionPolicy;

		private NoteSearchContext _noteSearchContext;

		public NoteStartDetectionPolicy NoteStartDetectionPolicy
		{
			get
			{
				return _noteStartDetectionPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteStartDetectionPolicy = value;
			}
		}

		public NoteSearchContext NoteSearchContext
		{
			get
			{
				return _noteSearchContext;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noteSearchContext = value;
			}
		}
	}
	internal sealed class NotesBuilder
	{
		private class NoteDescriptor
		{
			public TimedEvent NoteOnTimedEvent { get; }

			public TimedEvent NoteOffTimedEvent { get; set; }

			public bool IsCompleted => NoteOffTimedEvent != null;

			public NoteDescriptor(TimedEvent noteOnTimedEvent)
			{
				NoteOnTimedEvent = noteOnTimedEvent;
			}

			public Note GetNote()
			{
				if (!IsCompleted)
				{
					return null;
				}
				return new Note(NoteOnTimedEvent, NoteOffTimedEvent);
			}
		}

		private sealed class IndexedNoteDescriptor : NoteDescriptor
		{
			public int EventsCollectionIndex { get; }

			public IndexedNoteDescriptor(TimedEvent noteOnTimedEvent, int eventsCollectionIndex)
				: base(noteOnTimedEvent)
			{
				EventsCollectionIndex = eventsCollectionIndex;
			}

			public Tuple<Note, int> GetIndexedNote()
			{
				if (!base.IsCompleted)
				{
					return null;
				}
				return Tuple.Create(new Note(base.NoteOnTimedEvent, base.NoteOffTimedEvent), EventsCollectionIndex);
			}
		}

		private abstract class NoteOnsHolderBase<TDescriptor> where TDescriptor : NoteDescriptor
		{
			private const int DefaultCapacity = 2;

			private readonly NoteStartDetectionPolicy _noteStartDetectionPolicy;

			private readonly Stack<LinkedListNode<TDescriptor>> _nodesStack;

			private readonly Queue<LinkedListNode<TDescriptor>> _nodesQueue;

			public int Count => _noteStartDetectionPolicy switch
			{
				NoteStartDetectionPolicy.LastNoteOn => _nodesStack.Count, 
				NoteStartDetectionPolicy.FirstNoteOn => _nodesQueue.Count, 
				_ => -1, 
			};

			public NoteOnsHolderBase(NoteStartDetectionPolicy noteStartDetectionPolicy)
			{
				switch (noteStartDetectionPolicy)
				{
				case NoteStartDetectionPolicy.LastNoteOn:
					_nodesStack = new Stack<LinkedListNode<TDescriptor>>(2);
					break;
				case NoteStartDetectionPolicy.FirstNoteOn:
					_nodesQueue = new Queue<LinkedListNode<TDescriptor>>(2);
					break;
				}
				_noteStartDetectionPolicy = noteStartDetectionPolicy;
			}

			public void Add(LinkedListNode<TDescriptor> noteOnNode)
			{
				switch (_noteStartDetectionPolicy)
				{
				case NoteStartDetectionPolicy.LastNoteOn:
					_nodesStack.Push(noteOnNode);
					break;
				case NoteStartDetectionPolicy.FirstNoteOn:
					_nodesQueue.Enqueue(noteOnNode);
					break;
				}
			}

			public LinkedListNode<TDescriptor> GetNext()
			{
				return _noteStartDetectionPolicy switch
				{
					NoteStartDetectionPolicy.LastNoteOn => _nodesStack.Pop(), 
					NoteStartDetectionPolicy.FirstNoteOn => _nodesQueue.Dequeue(), 
					_ => null, 
				};
			}
		}

		private sealed class NoteOnsHolder : NoteOnsHolderBase<NoteDescriptor>
		{
			public NoteOnsHolder(NoteStartDetectionPolicy noteStartDetectionPolicy)
				: base(noteStartDetectionPolicy)
			{
			}
		}

		private sealed class IndexedNoteOnsHolder : NoteOnsHolderBase<IndexedNoteDescriptor>
		{
			public IndexedNoteOnsHolder(NoteStartDetectionPolicy noteStartDetectionPolicy)
				: base(noteStartDetectionPolicy)
			{
			}
		}

		private readonly NoteDetectionSettings _noteDetectionSettings;

		public NotesBuilder(NoteDetectionSettings noteDetectionSettings)
		{
			_noteDetectionSettings = noteDetectionSettings ?? new NoteDetectionSettings();
		}

		public IEnumerable<Note> GetNotesLazy(IEnumerable<TimedEvent> timedEvents, bool collectTimedEvents = false, List<TimedEvent> collectedTimedEvents = null)
		{
			LinkedList<NoteDescriptor> notesDescriptors = new LinkedList<NoteDescriptor>();
			Dictionary<int, NoteOnsHolder> notesDescriptorsNodes = new Dictionary<int, NoteOnsHolder>();
			foreach (TimedEvent timedEvent in timedEvents)
			{
				if (collectTimedEvents)
				{
					collectedTimedEvents.Add(timedEvent);
				}
				switch (timedEvent.Event.EventType)
				{
				case MidiEventType.NoteOn:
				{
					int noteEventId2 = GetNoteEventId((NoteOnEvent)timedEvent.Event);
					LinkedListNode<NoteDescriptor> noteOnNode = notesDescriptors.AddLast(new NoteDescriptor(timedEvent));
					if (!notesDescriptorsNodes.TryGetValue(noteEventId2, out var value2))
					{
						notesDescriptorsNodes.Add(noteEventId2, value2 = new NoteOnsHolder(_noteDetectionSettings.NoteStartDetectionPolicy));
					}
					value2.Add(noteOnNode);
					break;
				}
				case MidiEventType.NoteOff:
				{
					int noteEventId = GetNoteEventId((NoteOffEvent)timedEvent.Event);
					LinkedListNode<NoteDescriptor> next;
					if (!notesDescriptorsNodes.TryGetValue(noteEventId, out var value) || value.Count == 0 || (next = value.GetNext()).List == null)
					{
						break;
					}
					next.Value.NoteOffTimedEvent = timedEvent;
					if (next.Previous == null)
					{
						LinkedListNode<NoteDescriptor> n = next;
						while (n != null && n.Value.IsCompleted)
						{
							yield return n.Value.GetNote();
							LinkedListNode<NoteDescriptor> next2 = n.Next;
							notesDescriptors.Remove(n);
							n = next2;
						}
					}
					break;
				}
				}
			}
			foreach (NoteDescriptor item in notesDescriptors)
			{
				Note note = item.GetNote();
				if (note != null)
				{
					yield return note;
				}
			}
		}

		public IEnumerable<Note> GetNotesLazy(IEnumerable<Tuple<TimedEvent, int>> timedEvents, bool collectTimedEvents = false, List<Tuple<TimedEvent, int>> collectedTimedEvents = null)
		{
			LinkedList<NoteDescriptor> notesDescriptors = new LinkedList<NoteDescriptor>();
			Dictionary<Tuple<int, int>, NoteOnsHolder> notesDescriptorsNodes = new Dictionary<Tuple<int, int>, NoteOnsHolder>();
			bool respectEventsCollectionIndex = _noteDetectionSettings.NoteSearchContext == NoteSearchContext.SingleEventsCollection;
			foreach (Tuple<TimedEvent, int> timedEvent in timedEvents)
			{
				if (collectTimedEvents)
				{
					collectedTimedEvents.Add(timedEvent);
				}
				TimedEvent item = timedEvent.Item1;
				switch (item.Event.EventType)
				{
				case MidiEventType.NoteOn:
				{
					Tuple<int, int> key2 = Tuple.Create(GetNoteEventId((NoteOnEvent)item.Event), respectEventsCollectionIndex ? timedEvent.Item2 : (-1));
					LinkedListNode<NoteDescriptor> noteOnNode = notesDescriptors.AddLast(new NoteDescriptor(item));
					if (!notesDescriptorsNodes.TryGetValue(key2, out var value2))
					{
						notesDescriptorsNodes.Add(key2, value2 = new NoteOnsHolder(_noteDetectionSettings.NoteStartDetectionPolicy));
					}
					value2.Add(noteOnNode);
					break;
				}
				case MidiEventType.NoteOff:
				{
					Tuple<int, int> key = Tuple.Create(GetNoteEventId((NoteOffEvent)item.Event), respectEventsCollectionIndex ? timedEvent.Item2 : (-1));
					LinkedListNode<NoteDescriptor> next;
					if (!notesDescriptorsNodes.TryGetValue(key, out var value) || value.Count == 0 || (next = value.GetNext()).List == null)
					{
						break;
					}
					next.Value.NoteOffTimedEvent = item;
					if (next.Previous == null)
					{
						LinkedListNode<NoteDescriptor> n = next;
						while (n != null && n.Value.IsCompleted)
						{
							yield return n.Value.GetNote();
							LinkedListNode<NoteDescriptor> next2 = n.Next;
							notesDescriptors.Remove(n);
							n = next2;
						}
					}
					break;
				}
				}
			}
			foreach (NoteDescriptor item2 in notesDescriptors)
			{
				Note note = item2.GetNote();
				if (note != null)
				{
					yield return note;
				}
			}
		}

		public IEnumerable<Tuple<Note, int>> GetIndexedNotesLazy(IEnumerable<Tuple<TimedEvent, int>> timedEvents, bool collectTimedEvents = false, List<Tuple<TimedEvent, int>> collectedTimedEvents = null)
		{
			LinkedList<IndexedNoteDescriptor> notesDescriptors = new LinkedList<IndexedNoteDescriptor>();
			Dictionary<Tuple<int, int>, IndexedNoteOnsHolder> notesDescriptorsNodes = new Dictionary<Tuple<int, int>, IndexedNoteOnsHolder>();
			bool respectEventsCollectionIndex = _noteDetectionSettings.NoteSearchContext == NoteSearchContext.SingleEventsCollection;
			foreach (Tuple<TimedEvent, int> timedEvent in timedEvents)
			{
				if (collectTimedEvents)
				{
					collectedTimedEvents.Add(timedEvent);
				}
				TimedEvent item = timedEvent.Item1;
				switch (item.Event.EventType)
				{
				case MidiEventType.NoteOn:
				{
					Tuple<int, int> key2 = Tuple.Create(GetNoteEventId((NoteOnEvent)item.Event), respectEventsCollectionIndex ? timedEvent.Item2 : (-1));
					LinkedListNode<IndexedNoteDescriptor> noteOnNode = notesDescriptors.AddLast(new IndexedNoteDescriptor(item, timedEvent.Item2));
					if (!notesDescriptorsNodes.TryGetValue(key2, out var value2))
					{
						notesDescriptorsNodes.Add(key2, value2 = new IndexedNoteOnsHolder(_noteDetectionSettings.NoteStartDetectionPolicy));
					}
					value2.Add(noteOnNode);
					break;
				}
				case MidiEventType.NoteOff:
				{
					Tuple<int, int> key = Tuple.Create(GetNoteEventId((NoteOffEvent)item.Event), respectEventsCollectionIndex ? timedEvent.Item2 : (-1));
					LinkedListNode<IndexedNoteDescriptor> next;
					if (!notesDescriptorsNodes.TryGetValue(key, out var value) || value.Count == 0 || (next = value.GetNext()).List == null)
					{
						break;
					}
					next.Value.NoteOffTimedEvent = item;
					if (next.Previous == null)
					{
						LinkedListNode<IndexedNoteDescriptor> n = next;
						while (n != null && n.Value.IsCompleted)
						{
							yield return n.Value.GetIndexedNote();
							LinkedListNode<IndexedNoteDescriptor> next2 = n.Next;
							notesDescriptors.Remove(n);
							n = next2;
						}
					}
					break;
				}
				}
			}
			foreach (IndexedNoteDescriptor item2 in notesDescriptors)
			{
				Tuple<Note, int> indexedNote = item2.GetIndexedNote();
				if (indexedNote != null)
				{
					yield return indexedNote;
				}
			}
		}

		private static int GetNoteEventId(NoteEvent noteEvent)
		{
			return (byte)noteEvent.Channel * 1000 + (byte)noteEvent.NoteNumber;
		}
	}
	public sealed class NotesCollection : TimedObjectsCollection<Note>
	{
		public event NotesCollectionChangedEventHandler CollectionChanged;

		internal NotesCollection(IEnumerable<Note> notes)
			: base(notes)
		{
		}

		protected override void OnObjectsAdded(IEnumerable<Note> addedObjects)
		{
			base.OnObjectsAdded(addedObjects);
			OnCollectionChanged(addedObjects, null);
		}

		protected override void OnObjectsRemoved(IEnumerable<Note> removedObjects)
		{
			base.OnObjectsRemoved(removedObjects);
			OnCollectionChanged(null, removedObjects);
		}

		private void OnCollectionChanged(IEnumerable<Note> addedNotes, IEnumerable<Note> removedNotes)
		{
			this.CollectionChanged?.Invoke(this, new NotesCollectionChangedEventArgs(addedNotes, removedNotes));
		}
	}
	public sealed class NotesCollectionChangedEventArgs : EventArgs
	{
		public IEnumerable<Note> AddedNotes { get; }

		public IEnumerable<Note> RemovedNotes { get; }

		public NotesCollectionChangedEventArgs(IEnumerable<Note> addedNotes, IEnumerable<Note> removedNotes)
		{
			AddedNotes = addedNotes;
			RemovedNotes = removedNotes;
		}
	}
	public delegate void NotesCollectionChangedEventHandler(NotesCollection collection, NotesCollectionChangedEventArgs args);
	public enum NoteSearchContext
	{
		SingleEventsCollection,
		AllEventsCollections
	}
	public sealed class NotesManager : IDisposable
	{
		private readonly TimedEventsManager _timedEventsManager;

		private bool _disposed;

		public NotesCollection Notes { get; }

		public NotesManager(EventsCollection eventsCollection, NoteDetectionSettings settings = null, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			_timedEventsManager = eventsCollection.ManageTimedEvents(sameTimeEventsComparison);
			Notes = new NotesCollection(_timedEventsManager.Events.GetNotesAndTimedEventsLazy(settings).OfType<Note>());
			Notes.CollectionChanged += OnNotesCollectionChanged;
		}

		public void SaveChanges()
		{
			foreach (Note note in Notes)
			{
				NoteOnEvent obj = (NoteOnEvent)note.TimedNoteOnEvent.Event;
				NoteOffEvent noteOffEvent = (NoteOffEvent)note.TimedNoteOffEvent.Event;
				FourBitNumber channel = (noteOffEvent.Channel = note.Channel);
				obj.Channel = channel;
				SevenBitNumber noteNumber = (noteOffEvent.NoteNumber = note.NoteNumber);
				obj.NoteNumber = noteNumber;
				obj.Velocity = note.Velocity;
				noteOffEvent.Velocity = note.OffVelocity;
			}
			_timedEventsManager.SaveChanges();
		}

		private void OnNotesCollectionChanged(NotesCollection collection, NotesCollectionChangedEventArgs args)
		{
			IEnumerable<Note> addedNotes = args.AddedNotes;
			if (addedNotes != null)
			{
				_timedEventsManager.Events.Add(GetNotesTimedEvents(addedNotes));
			}
			IEnumerable<Note> removedNotes = args.RemovedNotes;
			if (removedNotes != null)
			{
				_timedEventsManager.Events.Remove(GetNotesTimedEvents(removedNotes));
			}
		}

		private static IEnumerable<TimedEvent> GetNotesTimedEvents(IEnumerable<Note> notes)
		{
			ThrowIfArgument.IsNull("notes", notes);
			return notes.SelectMany((Note n) => new TimedEvent[2] { n.TimedNoteOnEvent, n.TimedNoteOffEvent });
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					Notes.CollectionChanged -= OnNotesCollectionChanged;
					SaveChanges();
				}
				_disposed = true;
			}
		}
	}
	public static class NotesManagingUtilities
	{
		private abstract class NoteOnsHolderBase<TDescriptor> where TDescriptor : IObjectDescriptor
		{
			private const int DefaultCapacity = 2;

			private readonly NoteStartDetectionPolicy _noteStartDetectionPolicy;

			private readonly Stack<LinkedListNode<TDescriptor>> _nodesStack;

			private readonly Queue<LinkedListNode<TDescriptor>> _nodesQueue;

			public int Count => _noteStartDetectionPolicy switch
			{
				NoteStartDetectionPolicy.LastNoteOn => _nodesStack.Count, 
				NoteStartDetectionPolicy.FirstNoteOn => _nodesQueue.Count, 
				_ => -1, 
			};

			public NoteOnsHolderBase(NoteStartDetectionPolicy noteStartDetectionPolicy)
			{
				switch (noteStartDetectionPolicy)
				{
				case NoteStartDetectionPolicy.LastNoteOn:
					_nodesStack = new Stack<LinkedListNode<TDescriptor>>(2);
					break;
				case NoteStartDetectionPolicy.FirstNoteOn:
					_nodesQueue = new Queue<LinkedListNode<TDescriptor>>(2);
					break;
				}
				_noteStartDetectionPolicy = noteStartDetectionPolicy;
			}

			public void Add(LinkedListNode<TDescriptor> noteOnNode)
			{
				switch (_noteStartDetectionPolicy)
				{
				case NoteStartDetectionPolicy.LastNoteOn:
					_nodesStack.Push(noteOnNode);
					break;
				case NoteStartDetectionPolicy.FirstNoteOn:
					_nodesQueue.Enqueue(noteOnNode);
					break;
				}
			}

			public LinkedListNode<TDescriptor> GetNext()
			{
				return _noteStartDetectionPolicy switch
				{
					NoteStartDetectionPolicy.LastNoteOn => _nodesStack.Pop(), 
					NoteStartDetectionPolicy.FirstNoteOn => _nodesQueue.Dequeue(), 
					_ => null, 
				};
			}
		}

		private sealed class NoteOnsHolder : NoteOnsHolderBase<IObjectDescriptor>
		{
			public NoteOnsHolder(NoteStartDetectionPolicy noteStartDetectionPolicy)
				: base(noteStartDetectionPolicy)
			{
			}
		}

		private sealed class NoteOnsHolderIndexed : NoteOnsHolderBase<IObjectDescriptorIndexed>
		{
			public NoteOnsHolderIndexed(NoteStartDetectionPolicy noteStartDetectionPolicy)
				: base(noteStartDetectionPolicy)
			{
			}
		}

		private interface IObjectDescriptor
		{
			bool IsCompleted { get; }

			ITimedObject GetObject();
		}

		private interface IObjectDescriptorIndexed : IObjectDescriptor
		{
			Tuple<ITimedObject, int, int> GetIndexedObject();
		}

		private class NoteDescriptor : IObjectDescriptor
		{
			public TimedEvent NoteOnTimedEvent { get; }

			public TimedEvent NoteOffTimedEvent { get; set; }

			public bool IsCompleted => NoteOffTimedEvent != null;

			public NoteDescriptor(TimedEvent noteOnTimedEvent)
			{
				NoteOnTimedEvent = noteOnTimedEvent;
			}

			public ITimedObject GetObject()
			{
				if (!IsCompleted)
				{
					return NoteOnTimedEvent;
				}
				return new Note(NoteOnTimedEvent, NoteOffTimedEvent);
			}
		}

		private class CompleteObjectDescriptor : IObjectDescriptor
		{
			private readonly ITimedObject _timedObject;

			public bool IsCompleted { get; } = true;

			public CompleteObjectDescriptor(ITimedObject timedObject)
			{
				_timedObject = timedObject;
			}

			public ITimedObject GetObject()
			{
				return _timedObject;
			}
		}

		private sealed class NoteDescriptorIndexed : NoteDescriptor, IObjectDescriptorIndexed, IObjectDescriptor
		{
			private readonly int _noteOnIndex;

			public int NoteOffIndex { get; set; }

			public NoteDescriptorIndexed(TimedEvent noteOnTimedEvent, int noteOnIndex)
				: base(noteOnTimedEvent)
			{
				_noteOnIndex = noteOnIndex;
				NoteOffIndex = _noteOnIndex;
			}

			public Tuple<ITimedObject, int, int> GetIndexedObject()
			{
				return Tuple.Create(GetObject(), _noteOnIndex, NoteOffIndex);
			}
		}

		private class TimedEventDescriptor : IObjectDescriptor
		{
			public TimedEvent TimedEvent { get; }

			public bool IsCompleted { get; } = true;

			public TimedEventDescriptor(TimedEvent timedEvent)
			{
				TimedEvent = timedEvent;
			}

			public ITimedObject GetObject()
			{
				return TimedEvent;
			}
		}

		private sealed class TimedEventDescriptorIndexed : TimedEventDescriptor, IObjectDescriptorIndexed, IObjectDescriptor
		{
			private readonly int _index;

			public TimedEventDescriptorIndexed(TimedEvent timedEvent, int index)
				: base(timedEvent)
			{
				_index = index;
			}

			public Tuple<ITimedObject, int, int> GetIndexedObject()
			{
				return Tuple.Create(GetObject(), _index, _index);
			}
		}

		public static Note SetTimeAndLength(this Note note, ITimeSpan time, ITimeSpan length, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("note", note);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			note.Time = TimeConverter.ConvertFrom(time, tempoMap);
			note.Length = LengthConverter.ConvertFrom(length, note.Time, tempoMap);
			return note;
		}

		public static NotesManager ManageNotes(this EventsCollection eventsCollection, NoteDetectionSettings settings = null, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return new NotesManager(eventsCollection, settings, sameTimeEventsComparison);
		}

		public static NotesManager ManageNotes(this TrackChunk trackChunk, NoteDetectionSettings settings = null, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.ManageNotes(settings, sameTimeEventsComparison);
		}

		public static ICollection<Note> GetNotes(this IEnumerable<MidiEvent> midiEvents, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("midiEvents", midiEvents);
			List<Note> list = new List<Note>();
			IEnumerable<Note> notesLazy = new NotesBuilder(settings).GetNotesLazy(midiEvents.GetTimedEventsLazy());
			list.AddRange(notesLazy);
			return list;
		}

		public static ICollection<Note> GetNotes(this EventsCollection eventsCollection, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			List<Note> list = new List<Note>();
			IEnumerable<Note> notesLazy = new NotesBuilder(settings).GetNotesLazy(eventsCollection.GetTimedEventsLazy());
			list.AddRange(notesLazy);
			return list;
		}

		public static ICollection<Note> GetNotes(this TrackChunk trackChunk, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.GetNotes(settings);
		}

		public static ICollection<Note> GetNotes(this IEnumerable<TrackChunk> trackChunks, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			EventsCollection[] array = trackChunks.Select((TrackChunk c) => c.Events).ToArray();
			switch (array.Length)
			{
			case 0:
				return new Note[0];
			case 1:
				return array[0].GetNotes(settings);
			default:
			{
				int num = array.Sum((EventsCollection e) => e.Count);
				List<Note> list = new List<Note>(num / 3);
				IEnumerable<Note> notesLazy = new NotesBuilder(settings).GetNotesLazy(array.GetTimedEventsLazy(num));
				list.AddRange(notesLazy);
				return list;
			}
			}
		}

		public static ICollection<Note> GetNotes(this MidiFile file, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().GetNotes(settings);
		}

		public static int ProcessNotes(this EventsCollection eventsCollection, Action<Note> action, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			return eventsCollection.ProcessNotes(action, (Note note) => true, settings);
		}

		public static int ProcessNotes(this EventsCollection eventsCollection, Action<Note> action, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return eventsCollection.ProcessNotes(action, match, settings, canTimeOrLengthBeChanged: true);
		}

		public static int ProcessNotes(this TrackChunk trackChunk, Action<Note> action, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			return trackChunk.ProcessNotes(action, (Note note) => true, settings);
		}

		public static int ProcessNotes(this TrackChunk trackChunk, Action<Note> action, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			return trackChunk.Events.ProcessNotes(action, match, settings);
		}

		public static int ProcessNotes(this IEnumerable<TrackChunk> trackChunks, Action<Note> action, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			return trackChunks.ProcessNotes(action, (Note note) => true, settings);
		}

		public static int ProcessNotes(this IEnumerable<TrackChunk> trackChunks, Action<Note> action, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return trackChunks.ProcessNotes(action, match, settings, canTimeOrLengthBeChanged: true);
		}

		public static int ProcessNotes(this MidiFile file, Action<Note> action, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			return file.ProcessNotes(action, (Note note) => true, settings);
		}

		public static int ProcessNotes(this MidiFile file, Action<Note> action, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return file.GetTrackChunks().ProcessNotes(action, match, settings);
		}

		public static int RemoveNotes(this EventsCollection eventsCollection, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return eventsCollection.RemoveNotes((Note note) => true, settings);
		}

		public static int RemoveNotes(this EventsCollection eventsCollection, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("match", match);
			int num = eventsCollection.ProcessNotes(delegate(Note n)
			{
				MidiEvent midiEvent = n.TimedNoteOnEvent.Event;
				bool flag = (n.TimedNoteOffEvent.Event.Flag = true);
				midiEvent.Flag = flag;
			}, match, settings, canTimeOrLengthBeChanged: false);
			if (num == 0)
			{
				return 0;
			}
			eventsCollection.RemoveTimedEvents((TimedEvent e) => e.Event.Flag);
			return num;
		}

		public static int RemoveNotes(this TrackChunk trackChunk, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.RemoveNotes((Note note) => true, settings);
		}

		public static int RemoveNotes(this TrackChunk trackChunk, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("match", match);
			return trackChunk.Events.RemoveNotes(match, settings);
		}

		public static int RemoveNotes(this IEnumerable<TrackChunk> trackChunks, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return trackChunks.RemoveNotes((Note note) => true, settings);
		}

		public static int RemoveNotes(this IEnumerable<TrackChunk> trackChunks, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("match", match);
			int num = trackChunks.ProcessNotes(delegate(Note n)
			{
				MidiEvent midiEvent = n.TimedNoteOnEvent.Event;
				bool flag = (n.TimedNoteOffEvent.Event.Flag = true);
				midiEvent.Flag = flag;
			}, match, settings, canTimeOrLengthBeChanged: false);
			if (num == 0)
			{
				return 0;
			}
			trackChunks.RemoveTimedEvents((TimedEvent e) => e.Event.Flag);
			return num;
		}

		public static int RemoveNotes(this MidiFile file, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.RemoveNotes((Note note) => true, settings);
		}

		public static int RemoveNotes(this MidiFile file, Predicate<Note> match, NoteDetectionSettings settings = null)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("match", match);
			return file.GetTrackChunks().RemoveNotes(match, settings);
		}

		public static Melanchall.DryWetMidi.MusicTheory.Note GetMusicTheoryNote(this Note note)
		{
			ThrowIfArgument.IsNull("note", note);
			return note.UnderlyingNote;
		}

		internal static int ProcessNotes(this IEnumerable<TrackChunk> trackChunks, Action<Note> action, Predicate<Note> match, NoteDetectionSettings noteDetectionSettings, bool canTimeOrLengthBeChanged)
		{
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int num = array.Sum((EventsCollection c) => c.Count);
			int num2 = 0;
			bool flag = false;
			List<Tuple<TimedEvent, int>> list = (canTimeOrLengthBeChanged ? new List<Tuple<TimedEvent, int>>(num) : null);
			foreach (Note item in new NotesBuilder(noteDetectionSettings).GetNotesLazy(array.GetTimedEventsLazy(num, cloneEvent: false), canTimeOrLengthBeChanged, list))
			{
				if (match(item))
				{
					long time = item.TimedNoteOnEvent.Time;
					long time2 = item.TimedNoteOffEvent.Time;
					action(item);
					flag |= item.TimedNoteOnEvent.Time != time || item.TimedNoteOffEvent.Time != time2;
					num2++;
				}
			}
			if (flag)
			{
				array.SortAndUpdateEvents(list);
			}
			return num2;
		}

		internal static int ProcessNotes(this EventsCollection eventsCollection, Action<Note> action, Predicate<Note> match, NoteDetectionSettings noteDetectionSettings, bool canTimeOrLengthBeChanged)
		{
			int num = 0;
			bool flag = false;
			List<TimedEvent> list = (canTimeOrLengthBeChanged ? new List<TimedEvent>(eventsCollection.Count) : null);
			foreach (Note item in new NotesBuilder(noteDetectionSettings).GetNotesLazy(eventsCollection.GetTimedEventsLazy(cloneEvent: false), canTimeOrLengthBeChanged, list))
			{
				if (match(item))
				{
					long time = item.TimedNoteOnEvent.Time;
					long time2 = item.TimedNoteOffEvent.Time;
					action(item);
					flag |= item.TimedNoteOnEvent.Time != time || item.TimedNoteOffEvent.Time != time2;
					num++;
				}
			}
			if (flag)
			{
				eventsCollection.SortAndUpdateEvents(list);
			}
			return num;
		}

		internal static IEnumerable<Tuple<ITimedObject, int, int>> GetNotesAndTimedEventsLazy(this IEnumerable<Tuple<TimedEvent, int>> timedEvents, NoteDetectionSettings settings)
		{
			LinkedList<IObjectDescriptorIndexed> objectsDescriptors = new LinkedList<IObjectDescriptorIndexed>();
			Dictionary<Tuple<int, int>, NoteOnsHolderIndexed> notesDescriptorsNodes = new Dictionary<Tuple<int, int>, NoteOnsHolderIndexed>();
			bool respectEventsCollectionIndex = settings.NoteSearchContext == NoteSearchContext.SingleEventsCollection;
			foreach (Tuple<TimedEvent, int> timedEvent in timedEvents)
			{
				TimedEvent item = timedEvent.Item1;
				switch (item.Event.EventType)
				{
				case MidiEventType.NoteOn:
				{
					Tuple<int, int> key2 = Tuple.Create(GetNoteEventId((NoteOnEvent)item.Event), respectEventsCollectionIndex ? timedEvent.Item2 : (-1));
					LinkedListNode<IObjectDescriptorIndexed> noteOnNode = objectsDescriptors.AddLast(new NoteDescriptorIndexed(item, timedEvent.Item2));
					if (!notesDescriptorsNodes.TryGetValue(key2, out var value2))
					{
						notesDescriptorsNodes.Add(key2, value2 = new NoteOnsHolderIndexed(settings.NoteStartDetectionPolicy));
					}
					value2.Add(noteOnNode);
					break;
				}
				case MidiEventType.NoteOff:
				{
					Tuple<int, int> key = Tuple.Create(GetNoteEventId((NoteOffEvent)item.Event), respectEventsCollectionIndex ? timedEvent.Item2 : (-1));
					LinkedListNode<IObjectDescriptorIndexed> next;
					if (!notesDescriptorsNodes.TryGetValue(key, out var value) || value.Count == 0 || (next = value.GetNext()).List == null)
					{
						objectsDescriptors.AddLast(new TimedEventDescriptorIndexed(item, timedEvent.Item2));
						break;
					}
					NoteDescriptorIndexed obj = (NoteDescriptorIndexed)next.Value;
					obj.NoteOffTimedEvent = item;
					obj.NoteOffIndex = timedEvent.Item2;
					if (next.Previous == null)
					{
						LinkedListNode<IObjectDescriptorIndexed> n = next;
						while (n != null && n.Value.IsCompleted)
						{
							yield return n.Value.GetIndexedObject();
							LinkedListNode<IObjectDescriptorIndexed> next2 = n.Next;
							objectsDescriptors.Remove(n);
							n = next2;
						}
					}
					break;
				}
				default:
					if (objectsDescriptors.Count == 0)
					{
						yield return Tuple.Create((ITimedObject)item, timedEvent.Item2, timedEvent.Item2);
					}
					else
					{
						objectsDescriptors.AddLast(new TimedEventDescriptorIndexed(item, timedEvent.Item2));
					}
					break;
				}
			}
			foreach (IObjectDescriptorIndexed item2 in objectsDescriptors)
			{
				yield return item2.GetIndexedObject();
			}
		}

		internal static IEnumerable<ITimedObject> GetNotesAndTimedEventsLazy(this IEnumerable<TimedEvent> timedEvents, NoteDetectionSettings settings)
		{
			return timedEvents.GetNotesAndTimedEventsLazy(settings, completeObjectsAllowed: false);
		}

		internal static IEnumerable<ITimedObject> GetNotesAndTimedEventsLazy(this IEnumerable<ITimedObject> timedObjects, NoteDetectionSettings settings, bool completeObjectsAllowed)
		{
			settings = settings ?? new NoteDetectionSettings();
			LinkedList<IObjectDescriptor> objectsDescriptors = new LinkedList<IObjectDescriptor>();
			Dictionary<int, NoteOnsHolder> notesDescriptorsNodes = new Dictionary<int, NoteOnsHolder>();
			foreach (ITimedObject timedObject in timedObjects)
			{
				if (completeObjectsAllowed && !(timedObject is TimedEvent))
				{
					if (objectsDescriptors.Count == 0)
					{
						yield return timedObject;
					}
					else
					{
						objectsDescriptors.AddLast(new CompleteObjectDescriptor(timedObject));
					}
					continue;
				}
				TimedEvent timedEvent = (TimedEvent)timedObject;
				switch (timedEvent.Event.EventType)
				{
				case MidiEventType.NoteOn:
				{
					int noteEventId2 = GetNoteEventId((NoteOnEvent)timedEvent.Event);
					LinkedListNode<IObjectDescriptor> noteOnNode = objectsDescriptors.AddLast(new NoteDescriptor(timedEvent));
					if (!notesDescriptorsNodes.TryGetValue(noteEventId2, out var value2))
					{
						notesDescriptorsNodes.Add(noteEventId2, value2 = new NoteOnsHolder(settings.NoteStartDetectionPolicy));
					}
					value2.Add(noteOnNode);
					break;
				}
				case MidiEventType.NoteOff:
				{
					int noteEventId = GetNoteEventId((NoteOffEvent)timedEvent.Event);
					LinkedListNode<IObjectDescriptor> next;
					if (!notesDescriptorsNodes.TryGetValue(noteEventId, out var value) || value.Count == 0 || (next = value.GetNext()).List == null)
					{
						objectsDescriptors.AddLast(new TimedEventDescriptor(timedEvent));
						break;
					}
					((NoteDescriptor)next.Value).NoteOffTimedEvent = timedEvent;
					if (next.Previous == null)
					{
						LinkedListNode<IObjectDescriptor> n = next;
						while (n != null && n.Value.IsCompleted)
						{
							yield return n.Value.GetObject();
							LinkedListNode<IObjectDescriptor> next2 = n.Next;
							objectsDescriptors.Remove(n);
							n = next2;
						}
					}
					break;
				}
				default:
					if (objectsDescriptors.Count == 0)
					{
						yield return timedEvent;
					}
					else
					{
						objectsDescriptors.AddLast(new TimedEventDescriptor(timedEvent));
					}
					break;
				}
			}
			foreach (IObjectDescriptor item in objectsDescriptors)
			{
				yield return item.GetObject();
			}
		}

		private static int GetNoteEventId(NoteEvent noteEvent)
		{
			return (byte)noteEvent.Channel * 1000 + (byte)noteEvent.NoteNumber;
		}
	}
	public enum NoteStartDetectionPolicy
	{
		FirstNoteOn,
		LastNoteOn
	}
	public static class ResizeNotesUtilities
	{
		public static void ResizeNotes(this IEnumerable<Note> notes, ITimeSpan length, TimeSpanType distanceCalculationType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsInvalidEnumValue("distanceCalculationType", distanceCalculationType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (distanceCalculationType == TimeSpanType.BarBeatTicks || distanceCalculationType == TimeSpanType.BarBeatFraction)
			{
				throw new ArgumentException("Bar/beat distance calculation type is not supported.", "distanceCalculationType");
			}
			IEnumerable<Note> enumerable = notes.Where((Note n) => n != null);
			if (!enumerable.Any())
			{
				return;
			}
			long num = long.MaxValue;
			long num2 = 0L;
			foreach (Note item in enumerable)
			{
				long time = item.Time;
				long val = time + item.Length;
				num = Math.Min(num, time);
				num2 = Math.Max(num2, val);
			}
			ITimeSpan timeSpan = LengthConverter.ConvertTo(num2 - num, distanceCalculationType, num, tempoMap);
			double ratio = TimeSpanUtilities.Divide(LengthConverter.ConvertTo(length, distanceCalculationType, num, tempoMap), timeSpan);
			ITimeSpan startTime = TimeConverter.ConvertTo(num, distanceCalculationType, tempoMap);
			ResizeNotesByRatio(enumerable, ratio, distanceCalculationType, tempoMap, startTime);
		}

		public static void ResizeNotes(this IEnumerable<Note> notes, double ratio, TimeSpanType distanceCalculationType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNegative("ratio", ratio, "Ratio is negative");
			ThrowIfArgument.IsInvalidEnumValue("distanceCalculationType", distanceCalculationType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			if (distanceCalculationType == TimeSpanType.BarBeatTicks || distanceCalculationType == TimeSpanType.BarBeatFraction)
			{
				throw new ArgumentException("BarBeat distance calculation type is not supported.", "distanceCalculationType");
			}
			IEnumerable<Note> enumerable = notes.Where((Note n) => n != null);
			if (enumerable.Any())
			{
				ITimeSpan startTime = TimeConverter.ConvertTo(enumerable.Select((Note n) => n.Time).Min(), distanceCalculationType, tempoMap);
				ResizeNotesByRatio(enumerable, ratio, distanceCalculationType, tempoMap, startTime);
			}
		}

		private static void ResizeNotesByRatio(IEnumerable<Note> notes, double ratio, TimeSpanType distanceCalculationType, TempoMap tempoMap, ITimeSpan startTime)
		{
			foreach (Note note in notes)
			{
				ITimeSpan timeSpan = note.LengthAs(distanceCalculationType, tempoMap);
				ITimeSpan timeSpan2 = note.TimeAs(distanceCalculationType, tempoMap).Subtract(startTime, TimeSpanMode.TimeTime).Multiply(ratio);
				note.Time = TimeConverter.ConvertFrom(startTime.Add(timeSpan2, TimeSpanMode.TimeLength), tempoMap);
				ITimeSpan length = timeSpan.Multiply(ratio);
				note.Length = LengthConverter.ConvertFrom(length, note.Time, tempoMap);
			}
		}
	}
	public abstract class Parameter : ITimedObject, INotifyTimeChanged
	{
		private long _time;

		private ParameterValueType _valueType;

		public FourBitNumber Channel { get; set; }

		public ParameterValueType ValueType
		{
			get
			{
				return _valueType;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", _valueType);
				_valueType = value;
			}
		}

		public long Time
		{
			get
			{
				return _time;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				long time = Time;
				if (value != time)
				{
					_time = value;
					this.TimeChanged?.Invoke(this, new TimeChangedEventArgs(time, value));
				}
			}
		}

		public event EventHandler<TimeChangedEventArgs> TimeChanged;

		public abstract IEnumerable<TimedEvent> GetTimedEvents();
	}
	public enum ParameterValueType
	{
		Exact,
		Increment,
		Decrement
	}
	public sealed class ChannelCoarseTuningParameter : RegisteredParameter
	{
		public const sbyte MinHalfSteps = -64;

		public const sbyte MaxHalfSteps = 63;

		private sbyte _halfSteps;

		public sbyte HalfSteps
		{
			get
			{
				return _halfSteps;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, -64, 63, $"Half-steps number is out of [{(sbyte)(-64)}; {(sbyte)63}] range.");
				_halfSteps = value;
			}
		}

		public ChannelCoarseTuningParameter()
			: base(RegisteredParameterType.ChannelCoarseTuning)
		{
		}

		public ChannelCoarseTuningParameter(sbyte halfSteps)
			: this(halfSteps, ParameterValueType.Exact)
		{
		}

		public ChannelCoarseTuningParameter(sbyte halfSteps, ParameterValueType valueType)
			: this()
		{
			HalfSteps = halfSteps;
			base.ValueType = valueType;
		}

		private int GetSteps()
		{
			return HalfSteps - -64;
		}

		protected override void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb)
		{
			int steps = GetSteps();
			msb = (SevenBitNumber)(byte)steps;
			lsb = null;
		}

		protected override int GetIncrementStepsCount()
		{
			return GetSteps();
		}

		public override string ToString()
		{
			return $"{base.ToString()}: {HalfSteps} half-steps";
		}
	}
	public sealed class ChannelFineTuningParameter : RegisteredParameter
	{
		public const float MinCents = -100f;

		public const float MaxCents = 100f;

		private const int CentsRangeSize = 16383;

		private const float CentResolution = 81.915f;

		private float _cents;

		public float Cents
		{
			get
			{
				return _cents;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, -100.0, 100.0, $"Cents number is out of [{-100f}; {100f}] range.");
				_cents = value;
			}
		}

		public ChannelFineTuningParameter()
			: base(RegisteredParameterType.ChannelFineTuning)
		{
		}

		public ChannelFineTuningParameter(float cents)
			: this(cents, ParameterValueType.Exact)
		{
		}

		public ChannelFineTuningParameter(float cents, ParameterValueType valueType)
			: this()
		{
			Cents = cents;
			base.ValueType = valueType;
		}

		private int GetSteps()
		{
			return MathUtilities.EnsureInBounds((int)Math.Round((Cents + 100f) * 81.915f), 0, 16383);
		}

		protected override void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb)
		{
			ushort number = (ushort)GetSteps();
			msb = number.GetHead();
			lsb = number.GetTail();
		}

		protected override int GetIncrementStepsCount()
		{
			return GetSteps();
		}

		public override string ToString()
		{
			return $"{base.ToString()}: {Cents} cents";
		}
	}
	public sealed class ModulationDepthRangeParameter : RegisteredParameter
	{
		public static readonly SevenBitNumber DefaultHalfSteps = (SevenBitNumber)0;

		public static readonly float DefaultCents = 50f;

		public const float MinCents = 0f;

		public const float MaxCents = 100f;

		private const float CentResolution = 1.28f;

		private float _cents = DefaultCents;

		public SevenBitNumber HalfSteps { get; set; } = DefaultHalfSteps;

		public float Cents
		{
			get
			{
				return _cents;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, 0.0, 100.0, $"Cents number is out of [{0f}; {100f}] range.");
				_cents = value;
			}
		}

		public ModulationDepthRangeParameter()
			: base(RegisteredParameterType.ModulationDepthRange)
		{
		}

		public ModulationDepthRangeParameter(SevenBitNumber halfSteps, float cents)
			: this(halfSteps, cents, ParameterValueType.Exact)
		{
		}

		public ModulationDepthRangeParameter(SevenBitNumber halfSteps, float cents, ParameterValueType valueType)
			: this()
		{
			HalfSteps = halfSteps;
			Cents = cents;
			base.ValueType = valueType;
		}

		protected override void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb)
		{
			msb = HalfSteps;
			lsb = (SevenBitNumber)(byte)MathUtilities.EnsureInBounds((int)Math.Round(Cents * 1.28f), (byte)SevenBitNumber.MinValue, (byte)SevenBitNumber.MaxValue);
		}

		protected override int GetIncrementStepsCount()
		{
			throw new NotImplementedException();
		}

		public override string ToString()
		{
			return $"{base.ToString()}: {HalfSteps} half-steps, {Cents} cents";
		}
	}
	public sealed class PitchBendSensitivityParameter : RegisteredParameter
	{
		public static readonly SevenBitNumber DefaultHalfSteps = (SevenBitNumber)2;

		public static readonly SevenBitNumber DefaultCents = (SevenBitNumber)0;

		public SevenBitNumber HalfSteps { get; set; } = DefaultHalfSteps;

		public SevenBitNumber Cents { get; set; } = DefaultCents;

		public PitchBendSensitivityParameter()
			: base(RegisteredParameterType.PitchBendSensitivity)
		{
		}

		public PitchBendSensitivityParameter(SevenBitNumber halfSteps, SevenBitNumber cents)
			: this(halfSteps, cents, ParameterValueType.Exact)
		{
		}

		public PitchBendSensitivityParameter(SevenBitNumber halfSteps, SevenBitNumber cents, ParameterValueType valueType)
			: this()
		{
			HalfSteps = halfSteps;
			Cents = cents;
			base.ValueType = valueType;
		}

		protected override void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb)
		{
			msb = HalfSteps;
			lsb = Cents;
		}

		protected override int GetIncrementStepsCount()
		{
			return (byte)HalfSteps * 100 + (byte)Cents;
		}

		public override string ToString()
		{
			return $"{base.ToString()}: {HalfSteps} half-steps, {Cents} cents";
		}
	}
	public abstract class RegisteredParameter : Parameter
	{
		public RegisteredParameterType ParameterType { get; }

		protected RegisteredParameter(RegisteredParameterType parameterType)
		{
			ParameterType = parameterType;
		}

		protected abstract void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb);

		protected abstract int GetIncrementStepsCount();

		public override IEnumerable<TimedEvent> GetTimedEvents()
		{
			List<Tuple<ControlName, SevenBitNumber>> list = new List<Tuple<ControlName, SevenBitNumber>>
			{
				Tuple.Create(ControlName.RegisteredParameterNumberMsb, RegisteredParameterNumbers.GetMsb(ParameterType)),
				Tuple.Create(ControlName.RegisteredParameterNumberLsb, RegisteredParameterNumbers.GetLsb(ParameterType))
			};
			switch (base.ValueType)
			{
			case ParameterValueType.Exact:
			{
				GetData(out var msb, out var lsb);
				list.Add(Tuple.Create(ControlName.DataEntryMsb, msb));
				if (lsb.HasValue)
				{
					list.Add(Tuple.Create(ControlName.LsbForDataEntry, lsb.Value));
				}
				break;
			}
			case ParameterValueType.Increment:
			case ParameterValueType.Decrement:
			{
				ControlName controlName = ((base.ValueType == ParameterValueType.Increment) ? ControlName.DataIncrement : ControlName.DataDecrement);
				list.AddRange(from i in Enumerable.Range(0, GetIncrementStepsCount())
					select Tuple.Create(controlName, SevenBitNumber.MaxValue));
				break;
			}
			}
			list.Add(Tuple.Create(ControlName.RegisteredParameterNumberMsb, (SevenBitNumber)127));
			list.Add(Tuple.Create(ControlName.RegisteredParameterNumberLsb, (SevenBitNumber)127));
			return list.Select((Tuple<ControlName, SevenBitNumber> controlChange) => new TimedEvent(controlChange.Item1.GetControlChangeEvent(controlChange.Item2, base.Channel), base.Time));
		}

		public override string ToString()
		{
			return $"RPN {ParameterType} set to {base.ValueType}";
		}
	}
	internal static class RegisteredParameterNumbers
	{
		private static readonly Dictionary<RegisteredParameterType, SevenBitNumber> Msbs = new Dictionary<RegisteredParameterType, SevenBitNumber>
		{
			[RegisteredParameterType.PitchBendSensitivity] = (SevenBitNumber)0,
			[RegisteredParameterType.ChannelFineTuning] = (SevenBitNumber)0,
			[RegisteredParameterType.ChannelCoarseTuning] = (SevenBitNumber)0,
			[RegisteredParameterType.TuningProgramChange] = (SevenBitNumber)0,
			[RegisteredParameterType.TuningBankSelect] = (SevenBitNumber)0,
			[RegisteredParameterType.ModulationDepthRange] = (SevenBitNumber)0
		};

		private static readonly Dictionary<RegisteredParameterType, SevenBitNumber> Lsbs = new Dictionary<RegisteredParameterType, SevenBitNumber>
		{
			[RegisteredParameterType.PitchBendSensitivity] = (SevenBitNumber)0,
			[RegisteredParameterType.ChannelFineTuning] = (SevenBitNumber)1,
			[RegisteredParameterType.ChannelCoarseTuning] = (SevenBitNumber)2,
			[RegisteredParameterType.TuningProgramChange] = (SevenBitNumber)3,
			[RegisteredParameterType.TuningBankSelect] = (SevenBitNumber)4,
			[RegisteredParameterType.ModulationDepthRange] = (SevenBitNumber)5
		};

		public static SevenBitNumber GetMsb(RegisteredParameterType parameterType)
		{
			return Msbs[parameterType];
		}

		public static SevenBitNumber GetLsb(RegisteredParameterType parameterType)
		{
			return Lsbs[parameterType];
		}
	}
	public enum RegisteredParameterType : byte
	{
		PitchBendSensitivity,
		ChannelFineTuning,
		ChannelCoarseTuning,
		TuningProgramChange,
		TuningBankSelect,
		ModulationDepthRange
	}
	public sealed class TuningBankSelectParameter : RegisteredParameter
	{
		public SevenBitNumber BankNumber { get; set; }

		public TuningBankSelectParameter()
			: base(RegisteredParameterType.TuningBankSelect)
		{
		}

		public TuningBankSelectParameter(SevenBitNumber bankNumber)
			: this(bankNumber, ParameterValueType.Exact)
		{
		}

		public TuningBankSelectParameter(SevenBitNumber bankNumber, ParameterValueType valueType)
			: this()
		{
			BankNumber = bankNumber;
			base.ValueType = valueType;
		}

		protected override void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb)
		{
			msb = BankNumber;
			lsb = null;
		}

		protected override int GetIncrementStepsCount()
		{
			return (byte)BankNumber;
		}

		public override string ToString()
		{
			return $"{base.ToString()}: bank #{BankNumber}";
		}
	}
	public sealed class TuningProgramChangeParameter : RegisteredParameter
	{
		public SevenBitNumber ProgramNumber { get; set; }

		public TuningProgramChangeParameter()
			: base(RegisteredParameterType.TuningProgramChange)
		{
		}

		public TuningProgramChangeParameter(SevenBitNumber programNumber)
			: this(programNumber, ParameterValueType.Exact)
		{
		}

		public TuningProgramChangeParameter(SevenBitNumber programNumber, ParameterValueType valueType)
			: this()
		{
			ProgramNumber = programNumber;
			base.ValueType = valueType;
		}

		protected override void GetData(out SevenBitNumber msb, out SevenBitNumber? lsb)
		{
			msb = ProgramNumber;
			lsb = null;
		}

		protected override int GetIncrementStepsCount()
		{
			return (byte)ProgramNumber;
		}

		public override string ToString()
		{
			return $"{base.ToString()}: program #{ProgramNumber}";
		}
	}
	internal interface ITempoMapValuesCache
	{
		IEnumerable<TempoMapLine> InvalidateOnLines { get; }

		void Invalidate(TempoMap tempoMap);
	}
	public sealed class Tempo
	{
		public static readonly Tempo Default = new Tempo(500000L);

		private const int MicrosecondsInMinute = 60000000;

		private const int MicrosecondsInMillisecond = 1000;

		public long MicrosecondsPerQuarterNote { get; }

		public double BeatsPerMinute => 60000000.0 / (double)MicrosecondsPerQuarterNote;

		public Tempo(long microsecondsPerQuarterNote)
		{
			ThrowIfArgument.IsNonpositive("microsecondsPerQuarterNote", microsecondsPerQuarterNote, "Number of microseconds per quarter note is zero or negative.");
			MicrosecondsPerQuarterNote = microsecondsPerQuarterNote;
		}

		public static Tempo FromMillisecondsPerQuarterNote(long millisecondsPerQuarterNote)
		{
			ThrowIfArgument.IsNonpositive("millisecondsPerQuarterNote", millisecondsPerQuarterNote, "Number of milliseconds per quarter note is zero or negative.");
			return new Tempo(millisecondsPerQuarterNote * 1000);
		}

		public static Tempo FromBeatsPerMinute(double beatsPerMinute)
		{
			ThrowIfArgument.IsNonpositive("beatsPerMinute", beatsPerMinute, "Number of beats per minute is zero or negative.");
			return new Tempo(MathUtilities.RoundToLong(60000000.0 / beatsPerMinute));
		}

		public static bool operator ==(Tempo tempo1, Tempo tempo2)
		{
			if ((object)tempo1 == tempo2)
			{
				return true;
			}
			if ((object)tempo1 == null || (object)tempo2 == null)
			{
				return false;
			}
			return tempo1.MicrosecondsPerQuarterNote == tempo2.MicrosecondsPerQuarterNote;
		}

		public static bool operator !=(Tempo tempo1, Tempo tempo2)
		{
			return !(tempo1 == tempo2);
		}

		public static bool operator >(Tempo tempo1, Tempo tempo2)
		{
			ThrowIfArgument.IsNull("tempo1", tempo1);
			ThrowIfArgument.IsNull("tempo2", tempo2);
			return tempo1.MicrosecondsPerQuarterNote > tempo2.MicrosecondsPerQuarterNote;
		}

		public static bool operator >=(Tempo tempo1, Tempo tempo2)
		{
			ThrowIfArgument.IsNull("tempo1", tempo1);
			ThrowIfArgument.IsNull("tempo2", tempo2);
			return tempo1.MicrosecondsPerQuarterNote >= tempo2.MicrosecondsPerQuarterNote;
		}

		public static bool operator <(Tempo tempo1, Tempo tempo2)
		{
			ThrowIfArgument.IsNull("tempo1", tempo1);
			ThrowIfArgument.IsNull("tempo2", tempo2);
			return tempo1.MicrosecondsPerQuarterNote < tempo2.MicrosecondsPerQuarterNote;
		}

		public static bool operator <=(Tempo tempo1, Tempo tempo2)
		{
			ThrowIfArgument.IsNull("tempo1", tempo1);
			ThrowIfArgument.IsNull("tempo2", tempo2);
			return tempo1.MicrosecondsPerQuarterNote <= tempo2.MicrosecondsPerQuarterNote;
		}

		public override string ToString()
		{
			return $"{MicrosecondsPerQuarterNote} μs/qnote";
		}

		public override bool Equals(object obj)
		{
			return this == obj as Tempo;
		}

		public override int GetHashCode()
		{
			return MicrosecondsPerQuarterNote.GetHashCode();
		}
	}
	public sealed class TempoMap
	{
		public static readonly TempoMap Default = new TempoMap(new TicksPerQuarterNoteTimeDivision());

		private ValueLine<TimeSignature> _timeSignatureLine;

		private ValueLine<Tempo> _tempoLine;

		private readonly List<ITempoMapValuesCache> _valuesCaches = new List<ITempoMapValuesCache>();

		private bool _isTempoMapReady = true;

		public TimeDivision TimeDivision { get; internal set; }

		internal ValueLine<TimeSignature> TimeSignatureLine
		{
			get
			{
				return _timeSignatureLine;
			}
			set
			{
				if (_timeSignatureLine != null)
				{
					_timeSignatureLine.ValuesChanged -= OnTimeSignatureChanged;
				}
				_timeSignatureLine = value;
				_timeSignatureLine.ValuesChanged += OnTimeSignatureChanged;
			}
		}

		internal ValueLine<Tempo> TempoLine
		{
			get
			{
				return _tempoLine;
			}
			set
			{
				if (_tempoLine != null)
				{
					_tempoLine.ValuesChanged -= OnTempoChanged;
				}
				_tempoLine = value;
				_tempoLine.ValuesChanged += OnTempoChanged;
			}
		}

		internal bool IsTempoMapReady
		{
			get
			{
				return _isTempoMapReady;
			}
			set
			{
				if (_isTempoMapReady != value)
				{
					_isTempoMapReady = value;
					if (_isTempoMapReady)
					{
						InvalidateCaches(TempoMapLine.Tempo);
						InvalidateCaches(TempoMapLine.TimeSignature);
					}
				}
			}
		}

		internal TempoMap(TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			TimeDivision = timeDivision;
			TempoLine = new ValueLine<Tempo>(Tempo.Default);
			TimeSignatureLine = new ValueLine<TimeSignature>(TimeSignature.Default);
		}

		public IEnumerable<ValueChange<Tempo>> GetTempoChanges()
		{
			return _tempoLine;
		}

		public Tempo GetTempoAtTime(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			long time2 = TimeConverter.ConvertFrom(time, this);
			return TempoLine.GetValueAtTime(time2);
		}

		public IEnumerable<ValueChange<TimeSignature>> GetTimeSignatureChanges()
		{
			return _timeSignatureLine;
		}

		public TimeSignature GetTimeSignatureAtTime(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			long time2 = TimeConverter.ConvertFrom(time, this);
			return TimeSignatureLine.GetValueAtTime(time2);
		}

		public TempoMap Clone()
		{
			TempoMap tempoMap = new TempoMap(TimeDivision.Clone());
			tempoMap.TempoLine.ReplaceValues(TempoLine);
			tempoMap.TimeSignatureLine.ReplaceValues(TimeSignatureLine);
			return tempoMap;
		}

		public static TempoMap Create(Tempo tempo, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("tempo", tempo);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = Default.Clone();
			SetGlobalTempo(tempoMap, tempo);
			SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		public static TempoMap Create(Tempo tempo)
		{
			ThrowIfArgument.IsNull("tempo", tempo);
			TempoMap tempoMap = Default.Clone();
			SetGlobalTempo(tempoMap, tempo);
			return tempoMap;
		}

		public static TempoMap Create(TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = Default.Clone();
			SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		public static TempoMap Create(TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			return new TempoMap(timeDivision);
		}

		public static TempoMap Create(TimeDivision timeDivision, Tempo tempo)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("tempo", tempo);
			TempoMap tempoMap = new TempoMap(timeDivision);
			SetGlobalTempo(tempoMap, tempo);
			return tempoMap;
		}

		public static TempoMap Create(TimeDivision timeDivision, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = new TempoMap(timeDivision);
			SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		public static TempoMap Create(TimeDivision timeDivision, Tempo tempo, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("tempo", tempo);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap tempoMap = new TempoMap(timeDivision);
			SetGlobalTempo(tempoMap, tempo);
			SetGlobalTimeSignature(tempoMap, timeSignature);
			return tempoMap;
		}

		internal TempoMap Flip(long centerTime)
		{
			return new TempoMap(TimeDivision)
			{
				TempoLine = TempoLine.Reverse(centerTime),
				TimeSignatureLine = TimeSignatureLine.Reverse(centerTime)
			};
		}

		internal TCache GetValuesCache<TCache>() where TCache : ITempoMapValuesCache, new()
		{
			TCache val = _valuesCaches.OfType<TCache>().FirstOrDefault();
			if (val == null)
			{
				_valuesCaches.Add(val = new TCache());
				val.Invalidate(this);
			}
			return val;
		}

		private static void SetGlobalTempo(TempoMap tempoMap, Tempo tempo)
		{
			tempoMap.TempoLine.SetValue(0L, tempo);
		}

		private static void SetGlobalTimeSignature(TempoMap tempoMap, TimeSignature timeSignature)
		{
			tempoMap.TimeSignatureLine.SetValue(0L, timeSignature);
		}

		private void InvalidateCaches(TempoMapLine tempoMapLine)
		{
			if (!IsTempoMapReady)
			{
				return;
			}
			foreach (ITempoMapValuesCache item in _valuesCaches.Where((ITempoMapValuesCache c) => c.InvalidateOnLines?.Contains(tempoMapLine) ?? false))
			{
				item.Invalidate(this);
			}
		}

		private void OnTimeSignatureChanged(object sender, EventArgs args)
		{
			InvalidateCaches(TempoMapLine.TimeSignature);
		}

		private void OnTempoChanged(object sender, EventArgs args)
		{
			InvalidateCaches(TempoMapLine.Tempo);
		}
	}
	internal enum TempoMapLine
	{
		Tempo,
		TimeSignature
	}
	public sealed class TempoMapManager : IDisposable
	{
		private readonly IEnumerable<TimedEventsManager> _timedEventsManagers;

		private bool _disposed;

		public TempoMap TempoMap { get; }

		public TempoMapManager()
			: this(new TicksPerQuarterNoteTimeDivision())
		{
		}

		public TempoMapManager(TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			TempoMap = new TempoMap(timeDivision);
		}

		public TempoMapManager(TimeDivision timeDivision, IEnumerable<EventsCollection> eventsCollections)
		{
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsEmptyCollection("eventsCollections", eventsCollections, "Collection of EventsCollection is empty.");
			_timedEventsManagers = (from events in eventsCollections
				where events != null
				select events.ManageTimedEvents()).ToList();
			TempoMap = new TempoMap(timeDivision);
			CollectTimeSignatureChanges();
			CollectTempoChanges();
		}

		public void SetTimeSignature(long time, TimeSignature timeSignature)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			TempoMap.TimeSignatureLine.SetValue(time, timeSignature);
		}

		public void SetTimeSignature(ITimeSpan time, TimeSignature timeSignature)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("timeSignature", timeSignature);
			SetTimeSignature(TimeConverter.ConvertFrom(time, TempoMap), timeSignature);
		}

		public void ClearTimeSignature(long startTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			TempoMap.TimeSignatureLine.DeleteValues(startTime);
		}

		public void ClearTimeSignature(ITimeSpan startTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			ClearTimeSignature(TimeConverter.ConvertFrom(startTime, TempoMap));
		}

		public void ClearTimeSignature(long startTime, long endTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			ThrowIfTimeArgument.EndIsNegative("endTime", endTime);
			TempoMap.TimeSignatureLine.DeleteValues(startTime, endTime);
		}

		public void ClearTimeSignature(ITimeSpan startTime, ITimeSpan endTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			ThrowIfArgument.IsNull("endTime", endTime);
			ClearTimeSignature(TimeConverter.ConvertFrom(startTime, TempoMap), TimeConverter.ConvertFrom(endTime, TempoMap));
		}

		public void SetTempo(long time, Tempo tempo)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempo", tempo);
			TempoMap.TempoLine.SetValue(time, tempo);
		}

		public void SetTempo(ITimeSpan time, Tempo tempo)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempo", tempo);
			SetTempo(TimeConverter.ConvertFrom(time, TempoMap), tempo);
		}

		public void ClearTempo(long startTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			TempoMap.TempoLine.DeleteValues(startTime);
		}

		public void ClearTempo(ITimeSpan startTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			ClearTempo(TimeConverter.ConvertFrom(startTime, TempoMap));
		}

		public void ClearTempo(long startTime, long endTime)
		{
			ThrowIfTimeArgument.StartIsNegative("startTime", startTime);
			ThrowIfTimeArgument.EndIsNegative("endTime", endTime);
			TempoMap.TempoLine.DeleteValues(startTime, endTime);
		}

		public void ClearTempo(ITimeSpan startTime, ITimeSpan endTime)
		{
			ThrowIfArgument.IsNull("startTime", startTime);
			ThrowIfArgument.IsNull("endTime", endTime);
			ClearTempo(TimeConverter.ConvertFrom(startTime, TempoMap), TimeConverter.ConvertFrom(endTime, TempoMap));
		}

		public void ClearTempoMap()
		{
			TempoMap.TempoLine.Clear();
			TempoMap.TimeSignatureLine.Clear();
		}

		public void ReplaceTempoMap(TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			TempoMap.TimeDivision = tempoMap.TimeDivision.Clone();
			TempoMap.TempoLine.ReplaceValues(tempoMap.TempoLine);
			TempoMap.TimeSignatureLine.ReplaceValues(tempoMap.TimeSignatureLine);
		}

		public void SaveChanges()
		{
			if (_timedEventsManagers == null)
			{
				return;
			}
			foreach (TimedEventsCollection item in _timedEventsManagers.Select((TimedEventsManager m) => m.Events))
			{
				item.RemoveAll(IsTempoMapEvent);
			}
			TimedEventsCollection events = _timedEventsManagers.First().Events;
			events.Add(TempoMap.TempoLine.Select(GetSetTempoTimedEvent));
			events.Add(TempoMap.TimeSignatureLine.Select(GetTimeSignatureTimedEvent));
			foreach (TimedEventsManager timedEventsManager in _timedEventsManagers)
			{
				timedEventsManager.SaveChanges();
			}
		}

		private IEnumerable<TimedEvent> GetTimedEvents(Func<TimedEvent, bool> predicate)
		{
			return _timedEventsManagers.SelectMany((TimedEventsManager m) => m.Events).Where(predicate);
		}

		private void CollectTimeSignatureChanges()
		{
			foreach (TimedEvent timedEvent in GetTimedEvents(IsTimeSignatureEvent))
			{
				if (timedEvent.Event is TimeSignatureEvent timeSignatureEvent)
				{
					TempoMap.TimeSignatureLine.SetValue(timedEvent.Time, new TimeSignature(timeSignatureEvent.Numerator, timeSignatureEvent.Denominator));
				}
			}
		}

		private void CollectTempoChanges()
		{
			foreach (TimedEvent timedEvent in GetTimedEvents(IsTempoEvent))
			{
				if (timedEvent.Event is SetTempoEvent setTempoEvent)
				{
					TempoMap.TempoLine.SetValue(timedEvent.Time, new Tempo(setTempoEvent.MicrosecondsPerQuarterNote));
				}
			}
		}

		private static bool IsTempoMapEvent(TimedEvent timedEvent)
		{
			if (!IsTempoEvent(timedEvent))
			{
				return IsTimeSignatureEvent(timedEvent);
			}
			return true;
		}

		private static bool IsTempoEvent(TimedEvent timedEvent)
		{
			return timedEvent?.Event is SetTempoEvent;
		}

		private static bool IsTimeSignatureEvent(TimedEvent timedEvent)
		{
			return timedEvent?.Event is TimeSignatureEvent;
		}

		private static TimedEvent GetSetTempoTimedEvent(ValueChange<Tempo> tempoChange)
		{
			return new TimedEvent(new SetTempoEvent(tempoChange.Value.MicrosecondsPerQuarterNote), tempoChange.Time);
		}

		private static TimedEvent GetTimeSignatureTimedEvent(ValueChange<TimeSignature> timeSignatureChange)
		{
			TimeSignature value = timeSignatureChange.Value;
			return new TimedEvent(new TimeSignatureEvent((byte)value.Numerator, (byte)value.Denominator), timeSignatureChange.Time);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					SaveChanges();
				}
				_disposed = true;
			}
		}
	}
	public static class TempoMapManagingUtilities
	{
		public static TempoMapManager ManageTempoMap(this IEnumerable<EventsCollection> eventsCollections, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			return new TempoMapManager(timeDivision, eventsCollections);
		}

		public static TempoMapManager ManageTempoMap(this IEnumerable<TrackChunk> trackChunks, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			return trackChunks.Select((TrackChunk c) => c.Events).ManageTempoMap(timeDivision);
		}

		public static TempoMapManager ManageTempoMap(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().ManageTempoMap(file.TimeDivision);
		}

		public static TempoMap GetTempoMap(this IEnumerable<TrackChunk> trackChunks, TimeDivision timeDivision)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("timeDivision", timeDivision);
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int eventsCount = array.Sum((EventsCollection c) => c.Count);
			TempoMap tempoMap = new TempoMap(timeDivision);
			foreach (Tuple<TimedEvent, int> item2 in array.GetTimedEventsLazy(eventsCount))
			{
				TimedEvent item = item2.Item1;
				MidiEvent midiEvent = item.Event;
				switch (midiEvent.EventType)
				{
				case MidiEventType.TimeSignature:
				{
					TimeSignatureEvent timeSignatureEvent = (TimeSignatureEvent)midiEvent;
					tempoMap.TimeSignatureLine.SetValue(item.Time, new TimeSignature(timeSignatureEvent.Numerator, timeSignatureEvent.Denominator));
					break;
				}
				case MidiEventType.SetTempo:
				{
					SetTempoEvent setTempoEvent = (SetTempoEvent)midiEvent;
					tempoMap.TempoLine.SetValue(item.Time, new Tempo(setTempoEvent.MicrosecondsPerQuarterNote));
					break;
				}
				}
			}
			return tempoMap;
		}

		public static TempoMap GetTempoMap(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().GetTempoMap(file.TimeDivision);
		}

		public static void ReplaceTempoMap(this IEnumerable<EventsCollection> eventsCollections, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("eventsCollections", eventsCollections);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsEmptyCollection("eventsCollections", eventsCollections, "Collection of EventsCollection is empty.");
			using TempoMapManager tempoMapManager = eventsCollections.ManageTempoMap(tempoMap.TimeDivision);
			tempoMapManager.ReplaceTempoMap(tempoMap);
		}

		public static void ReplaceTempoMap(this IEnumerable<TrackChunk> trackChunks, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			ThrowIfArgument.IsEmptyCollection("trackChunks", trackChunks, "Collection of TrackChunk is empty.");
			trackChunks.Select((TrackChunk c) => c.Events).ReplaceTempoMap(tempoMap);
		}

		public static void ReplaceTempoMap(this MidiFile file, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			IEnumerable<TrackChunk> trackChunks = file.GetTrackChunks();
			ThrowIfArgument.IsEmptyCollection("trackChunks", trackChunks, "Collection of TrackChunk of the file is empty.");
			trackChunks.ReplaceTempoMap(tempoMap);
			file.TimeDivision = tempoMap.TimeDivision.Clone();
		}
	}
	public sealed class TimeSignature
	{
		public static readonly TimeSignature Default = new TimeSignature(4, 4);

		public int Numerator { get; }

		public int Denominator { get; }

		public TimeSignature(int numerator, int denominator)
		{
			ThrowIfArgument.IsNonpositive("numerator", numerator, "Numerator is zero or negative.");
			ThrowIfArgument.IsNonpositive("denominator", denominator, "Denominator is zero or negative.");
			ThrowIfArgument.DoesntSatisfyCondition("denominator", denominator, MathUtilities.IsPowerOfTwo, "Denominator is not a power of two.");
			Numerator = numerator;
			Denominator = denominator;
		}

		public static bool operator ==(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			if ((object)timeSignature1 == timeSignature2)
			{
				return true;
			}
			if ((object)timeSignature1 == null || (object)timeSignature2 == null)
			{
				return false;
			}
			if (timeSignature1.Numerator == timeSignature2.Numerator)
			{
				return timeSignature1.Denominator == timeSignature2.Denominator;
			}
			return false;
		}

		public static bool operator !=(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			return !(timeSignature1 == timeSignature2);
		}

		public static bool operator <(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			ThrowIfArgument.IsNull("timeSignature1", timeSignature1);
			ThrowIfArgument.IsNull("timeSignature2", timeSignature2);
			return (double)timeSignature1.Numerator / (double)timeSignature1.Denominator < (double)timeSignature2.Numerator / (double)timeSignature2.Denominator;
		}

		public static bool operator <=(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			ThrowIfArgument.IsNull("timeSignature1", timeSignature1);
			ThrowIfArgument.IsNull("timeSignature2", timeSignature2);
			return (double)timeSignature1.Numerator / (double)timeSignature1.Denominator <= (double)timeSignature2.Numerator / (double)timeSignature2.Denominator;
		}

		public static bool operator >(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			ThrowIfArgument.IsNull("timeSignature1", timeSignature1);
			ThrowIfArgument.IsNull("timeSignature2", timeSignature2);
			return (double)timeSignature1.Numerator / (double)timeSignature1.Denominator > (double)timeSignature2.Numerator / (double)timeSignature2.Denominator;
		}

		public static bool operator >=(TimeSignature timeSignature1, TimeSignature timeSignature2)
		{
			ThrowIfArgument.IsNull("timeSignature1", timeSignature1);
			ThrowIfArgument.IsNull("timeSignature2", timeSignature2);
			return (double)timeSignature1.Numerator / (double)timeSignature1.Denominator >= (double)timeSignature2.Numerator / (double)timeSignature2.Denominator;
		}

		public override string ToString()
		{
			return $"{Numerator}/{Denominator}";
		}

		public override bool Equals(object obj)
		{
			return this == obj as TimeSignature;
		}

		public override int GetHashCode()
		{
			return (17 * 23 + Numerator.GetHashCode()) * 23 + Denominator.GetHashCode();
		}
	}
	public class TimedEvent : ITimedObject, INotifyTimeChanged
	{
		internal long _time;

		public MidiEvent Event { get; }

		public long Time
		{
			get
			{
				return _time;
			}
			set
			{
				ThrowIfTimeArgument.IsNegative("value", value);
				long time = _time;
				if (value != time)
				{
					_time = value;
					this.TimeChanged?.Invoke(this, new TimeChangedEventArgs(time, value));
				}
			}
		}

		public event EventHandler<TimeChangedEventArgs> TimeChanged;

		public TimedEvent(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			Event = midiEvent;
		}

		public TimedEvent(MidiEvent midiEvent, long time)
			: this(midiEvent)
		{
			Time = time;
		}

		public virtual TimedEvent Clone()
		{
			return new TimedEvent(Event.Clone())
			{
				_time = _time
			};
		}

		public override string ToString()
		{
			return $"Event at {Time}: {Event}";
		}
	}
	public sealed class TimedEventsCollection : TimedObjectsCollection<TimedEvent>
	{
		private readonly TimedEventsComparer _eventsComparer;

		internal TimedEventsCollection(IEnumerable<TimedEvent> events, Comparison<MidiEvent> sameTimeEventsComparison)
			: base(events)
		{
			_eventsComparer = new TimedEventsComparer(sameTimeEventsComparison);
		}

		public override IEnumerator<TimedEvent> GetEnumerator()
		{
			return _objects.OrderBy((TimedEvent e) => e, _eventsComparer).GetEnumerator();
		}
	}
	internal sealed class TimedEventsComparer : IComparer<TimedEvent>
	{
		private readonly Comparison<MidiEvent> _sameTimeEventsComparison;

		internal TimedEventsComparer(Comparison<MidiEvent> sameTimeEventsComparison)
		{
			_sameTimeEventsComparison = sameTimeEventsComparison;
		}

		public int Compare(TimedEvent x, TimedEvent y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			int num = Math.Sign(x.Time - y.Time);
			if (num != 0)
			{
				return num;
			}
			return _sameTimeEventsComparison?.Invoke(x.Event, y.Event) ?? 0;
		}
	}
	public sealed class TimedEventsManager : IDisposable
	{
		private readonly EventsCollection _eventsCollection;

		private bool _disposed;

		public TimedEventsCollection Events { get; }

		public TimedEventsManager(EventsCollection eventsCollection, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			_eventsCollection = eventsCollection;
			Events = new TimedEventsCollection(eventsCollection.GetTimedEventsLazy(), sameTimeEventsComparison);
		}

		public void SaveChanges()
		{
			_eventsCollection.Clear();
			long num = 0L;
			foreach (TimedEvent @event in Events)
			{
				MidiEvent midiEvent = @event.Event;
				midiEvent.DeltaTime = @event.Time - num;
				_eventsCollection.Add(midiEvent);
				num = @event.Time;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					SaveChanges();
				}
				_disposed = true;
			}
		}
	}
	public static class TimedEventsManagingUtilities
	{
		public static TimedEvent SetTime(this TimedEvent timedEvent, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("timedEvent", timedEvent);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			timedEvent.Time = TimeConverter.ConvertFrom(time, tempoMap);
			return timedEvent;
		}

		public static TimedEventsManager ManageTimedEvents(this EventsCollection eventsCollection, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			return new TimedEventsManager(eventsCollection, sameTimeEventsComparison);
		}

		public static TimedEventsManager ManageTimedEvents(this TrackChunk trackChunk, Comparison<MidiEvent> sameTimeEventsComparison = null)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.ManageTimedEvents(sameTimeEventsComparison);
		}

		public static ICollection<TimedEvent> GetTimedEvents(this EventsCollection eventsCollection)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			List<TimedEvent> list = new List<TimedEvent>(eventsCollection.Count);
			foreach (TimedEvent item in eventsCollection.GetTimedEventsLazy())
			{
				list.Add(item);
			}
			return list;
		}

		public static ICollection<TimedEvent> GetTimedEvents(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.GetTimedEvents();
		}

		public static ICollection<TimedEvent> GetTimedEvents(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int num = array.Sum((EventsCollection c) => c.Count);
			List<TimedEvent> list = new List<TimedEvent>(num);
			foreach (Tuple<TimedEvent, int> item in array.GetTimedEventsLazy(num))
			{
				list.Add(item.Item1);
			}
			return list;
		}

		public static ICollection<TimedEvent> GetTimedEvents(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().GetTimedEvents();
		}

		public static void AddEvent(this TimedEventsCollection eventsCollection, MidiEvent midiEvent, long time)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsOfInvalidType<SystemRealTimeEvent, SystemCommonEvent>("midiEvent", midiEvent, "Event is either system real-time or system common one.");
			ThrowIfTimeArgument.IsNegative("time", time);
			eventsCollection.Add(new TimedEvent(midiEvent, time));
		}

		public static void AddEvent(this TimedEventsCollection eventsCollection, MidiEvent midiEvent, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsOfInvalidType<SystemRealTimeEvent, SystemCommonEvent>("midiEvent", midiEvent, "Event is either system real-time or system common one.");
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			eventsCollection.AddEvent(midiEvent, TimeConverter.ConvertFrom(time, tempoMap));
		}

		public static int ProcessTimedEvents(this EventsCollection eventsCollection, Action<TimedEvent> action)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			return eventsCollection.ProcessTimedEvents(action, (TimedEvent timedEvent) => true);
		}

		public static int ProcessTimedEvents(this EventsCollection eventsCollection, Action<TimedEvent> action, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			int num = 0;
			bool flag = false;
			List<TimedEvent> list = new List<TimedEvent>(eventsCollection.Count);
			foreach (TimedEvent item in eventsCollection.GetTimedEventsLazy(cloneEvent: false))
			{
				if (match(item))
				{
					long time = item.Time;
					action(item);
					flag = item.Time != time;
					num++;
				}
				list.Add(item);
			}
			if (flag)
			{
				eventsCollection.SortAndUpdateEvents(list);
			}
			return num;
		}

		public static int ProcessTimedEvents(this TrackChunk trackChunk, Action<TimedEvent> action)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			return trackChunk.ProcessTimedEvents(action, (TimedEvent timedEvent) => true);
		}

		public static int ProcessTimedEvents(this TrackChunk trackChunk, Action<TimedEvent> action, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return trackChunk.Events.ProcessTimedEvents(action, match);
		}

		public static int ProcessTimedEvents(this IEnumerable<TrackChunk> trackChunks, Action<TimedEvent> action)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			return trackChunks.ProcessTimedEvents(action, (TimedEvent timedEvent) => true);
		}

		public static int ProcessTimedEvents(this IEnumerable<TrackChunk> trackChunks, Action<TimedEvent> action, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int num = array.Sum((EventsCollection c) => c.Count);
			int num2 = 0;
			bool flag = false;
			List<Tuple<TimedEvent, int>> list = new List<Tuple<TimedEvent, int>>(num);
			foreach (Tuple<TimedEvent, int> item2 in array.GetTimedEventsLazy(num, cloneEvent: false))
			{
				TimedEvent item = item2.Item1;
				if (match(item))
				{
					long deltaTime = item.Event.DeltaTime;
					long time = item.Time;
					action(item);
					item.Event.DeltaTime = deltaTime;
					flag = item.Time != time;
					num2++;
				}
				list.Add(item2);
			}
			if (flag)
			{
				array.SortAndUpdateEvents(list);
			}
			return num2;
		}

		public static int ProcessTimedEvents(this MidiFile file, Action<TimedEvent> action)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			return file.ProcessTimedEvents(action, (TimedEvent timedEvent) => true);
		}

		public static int ProcessTimedEvents(this MidiFile file, Action<TimedEvent> action, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("action", action);
			ThrowIfArgument.IsNull("match", match);
			return file.GetTrackChunks().ProcessTimedEvents(action, match);
		}

		public static int RemoveTimedEvents(this EventsCollection eventsCollection)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			int count = eventsCollection.Count;
			eventsCollection.Clear();
			return count;
		}

		public static int RemoveTimedEvents(this EventsCollection eventsCollection, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("match", match);
			int count = eventsCollection.Count;
			int num = 0;
			long num2 = 0L;
			long num3 = 0L;
			List<MidiEvent> events = eventsCollection._events;
			for (int i = 0; i < count; i++)
			{
				num2 += events[i].DeltaTime;
				TimedEvent obj = new TimedEvent(events[i], num2);
				if (match(obj))
				{
					num++;
					continue;
				}
				events[i].DeltaTime = num2 - num3;
				events[i - num] = events[i];
				num3 = num2;
			}
			if (num > 0)
			{
				events.RemoveRange(count - num, num);
			}
			return num;
		}

		public static int RemoveTimedEvents(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			int count = trackChunk.Events.Count;
			trackChunk.Events.Clear();
			return count;
		}

		public static int RemoveTimedEvents(this TrackChunk trackChunk, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("match", match);
			return trackChunk.Events.RemoveTimedEvents(match);
		}

		public static int RemoveTimedEvents(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			int num = 0;
			foreach (TrackChunk trackChunk in trackChunks)
			{
				num += trackChunk.RemoveTimedEvents();
			}
			return num;
		}

		public static int RemoveTimedEvents(this IEnumerable<TrackChunk> trackChunks, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("match", match);
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int num = array.Sum((EventsCollection c) => c.Count);
			int num2 = array.Length;
			switch (num2)
			{
			case 0:
				return 0;
			case 1:
				return array[0].RemoveTimedEvents(match);
			default:
			{
				int[] array2 = new int[num2];
				int[] array3 = array.Select((EventsCollection c) => c.Count - 1).ToArray();
				long[] array4 = new long[num2];
				long[] array5 = new long[num2];
				int[] array6 = new int[num2];
				for (int num3 = 0; num3 < num; num3++)
				{
					int num4 = 0;
					long num5 = long.MaxValue;
					for (int num6 = 0; num6 < num2; num6++)
					{
						int num7 = array2[num6];
						if (num7 <= array3[num6])
						{
							long num8 = array[num6][num7].DeltaTime + array4[num6];
							if (num8 < num5)
							{
								num5 = num8;
								num4 = num6;
							}
						}
					}
					MidiEvent midiEvent = array[num4][array2[num4]];
					TimedEvent obj = new TimedEvent(midiEvent, num5);
					if (match(obj))
					{
						array6[num4]++;
					}
					else
					{
						midiEvent.DeltaTime = num5 - array5[num4];
						array[num4][array2[num4] - array6[num4]] = midiEvent;
						array5[num4] = num5;
					}
					array4[num4] = num5;
					array2[num4]++;
				}
				for (int num9 = 0; num9 < num2; num9++)
				{
					int num10 = array6[num9];
					if (num10 > 0)
					{
						array[num9]._events.RemoveRange(array[num9].Count - num10, num10);
					}
				}
				return array6.Sum();
			}
			}
		}

		public static int RemoveTimedEvents(this MidiFile file)
		{
			ThrowIfArgument.IsNull("file", file);
			return file.GetTrackChunks().RemoveTimedEvents();
		}

		public static int RemoveTimedEvents(this MidiFile file, Predicate<TimedEvent> match)
		{
			ThrowIfArgument.IsNull("file", file);
			ThrowIfArgument.IsNull("match", match);
			return file.GetTrackChunks().RemoveTimedEvents(match);
		}

		internal static IEnumerable<Tuple<TimedEvent, int>> GetTimedEventsLazy(this IEnumerable<TrackChunk> trackChunks, bool cloneEvent = true)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			EventsCollection[] array = (from c in trackChunks
				where c != null
				select c.Events).ToArray();
			int eventsCount = array.Sum((EventsCollection c) => c.Count);
			return array.GetTimedEventsLazy(eventsCount, cloneEvent);
		}

		internal static IEnumerable<Tuple<TimedEvent, int>> GetTimedEventsLazy(this EventsCollection[] eventsCollections, int eventsCount, bool cloneEvent = true)
		{
			int eventsCollectionsCount = eventsCollections.Length;
			switch (eventsCollectionsCount)
			{
			case 1:
				foreach (TimedEvent item in eventsCollections[0].GetTimedEventsLazy(cloneEvent: false))
				{
					yield return Tuple.Create(item, 0);
				}
				yield break;
			case 0:
				yield break;
			}
			int[] eventsCollectionIndices = new int[eventsCollectionsCount];
			int[] eventsCollectionMaxIndices = eventsCollections.Select((EventsCollection c) => c.Count - 1).ToArray();
			long[] eventsCollectionTimes = new long[eventsCollectionsCount];
			for (int i = 0; i < eventsCount; i++)
			{
				int eventsCollectionIndex = 0;
				long minTime = long.MaxValue;
				for (int num = 0; num < eventsCollectionsCount; num++)
				{
					int num2 = eventsCollectionIndices[num];
					if (num2 <= eventsCollectionMaxIndices[num])
					{
						long num3 = eventsCollections[num][num2].DeltaTime + eventsCollectionTimes[num];
						if (num3 < minTime)
						{
							minTime = num3;
							eventsCollectionIndex = num;
						}
					}
				}
				MidiEvent midiEvent = eventsCollections[eventsCollectionIndex][eventsCollectionIndices[eventsCollectionIndex]];
				TimedEvent timedEvent = new TimedEvent(cloneEvent ? midiEvent.Clone() : midiEvent);
				timedEvent._time = minTime;
				yield return Tuple.Create(timedEvent, eventsCollectionIndex);
				eventsCollectionTimes[eventsCollectionIndex] = minTime;
				eventsCollectionIndices[eventsCollectionIndex]++;
			}
		}

		internal static IEnumerable<TimedEvent> GetTimedEventsLazy(this IEnumerable<MidiEvent> events, bool cloneEvent = true)
		{
			long time = 0L;
			foreach (MidiEvent @event in events)
			{
				if (@event != null)
				{
					time += @event.DeltaTime;
					TimedEvent timedEvent = new TimedEvent(cloneEvent ? @event.Clone() : @event);
					timedEvent._time = time;
					yield return timedEvent;
				}
			}
		}

		internal static void SortAndUpdateEvents(this EventsCollection eventsCollection, IEnumerable<TimedEvent> timedEvents)
		{
			long num = 0L;
			int num2 = 0;
			foreach (TimedEvent item in timedEvents.OrderBy((TimedEvent e) => e.Time))
			{
				MidiEvent midiEvent = item.Event;
				midiEvent.DeltaTime = item.Time - num;
				eventsCollection[num2++] = midiEvent;
				num = item.Time;
			}
		}

		internal static void SortAndUpdateEvents(this EventsCollection[] eventsCollections, IEnumerable<Tuple<TimedEvent, int>> timedEvents)
		{
			long[] array = new long[eventsCollections.Length];
			int[] array2 = new int[eventsCollections.Length];
			foreach (Tuple<TimedEvent, int> item in timedEvents.OrderBy((Tuple<TimedEvent, int> e) => e.Item1.Time))
			{
				MidiEvent midiEvent = item.Item1.Event;
				midiEvent.DeltaTime = item.Item1.Time - array[item.Item2];
				eventsCollections[item.Item2][array2[item.Item2]++] = midiEvent;
				array[item.Item2] = item.Item1.Time;
			}
		}
	}
	public interface INotifyTimeChanged
	{
		event EventHandler<TimeChangedEventArgs> TimeChanged;
	}
	public interface ITimedObject
	{
		long Time { get; set; }
	}
	public sealed class TimeChangedEventArgs : EventArgs
	{
		public long OldTime { get; }

		public long NewTime { get; }

		internal TimeChangedEventArgs(long oldTime, long newTime)
		{
			OldTime = oldTime;
			NewTime = newTime;
		}
	}
	public abstract class TimedObjectsCollection<TObject> : IEnumerable<TObject>, IEnumerable where TObject : ITimedObject
	{
		protected readonly List<TObject> _objects = new List<TObject>();

		internal TimedObjectsCollection(IEnumerable<TObject> objects)
		{
			_objects.AddRange(objects.Where((TObject o) => o != null));
		}

		public void Add(IEnumerable<TObject> objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			List<TObject> list = objects.Where((TObject o) => o != null).ToList();
			_objects.AddRange(list);
			OnObjectsAdded(list);
		}

		public void Add(params TObject[] objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			Add((IEnumerable<TObject>)objects);
		}

		public void Remove(IEnumerable<TObject> objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			List<TObject> list = new List<TObject>();
			foreach (TObject @object in objects)
			{
				if (_objects.Remove(@object))
				{
					list.Add(@object);
				}
			}
			OnObjectsRemoved(list);
		}

		public void Remove(params TObject[] objects)
		{
			ThrowIfArgument.IsNull("objects", objects);
			Remove((IEnumerable<TObject>)objects);
		}

		public void RemoveAll(Predicate<TObject> match)
		{
			ThrowIfArgument.IsNull("match", match);
			List<TObject> removedObjects = _objects.Where((TObject o) => match(o)).ToList();
			_objects.RemoveAll(match);
			OnObjectsRemoved(removedObjects);
		}

		public void Clear()
		{
			List<TObject> removedObjects = _objects.ToList();
			_objects.Clear();
			OnObjectsRemoved(removedObjects);
		}

		protected virtual void OnObjectsAdded(IEnumerable<TObject> addedObjects)
		{
		}

		protected virtual void OnObjectsRemoved(IEnumerable<TObject> removedObjects)
		{
		}

		public virtual IEnumerator<TObject> GetEnumerator()
		{
			return _objects.OrderBy((TObject o) => o.Time).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal sealed class TimedObjectsComparer<TObject> : IComparer<TObject> where TObject : ITimedObject
	{
		public int Compare(TObject x, TObject y)
		{
			if ((object)x == (object)y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return Math.Sign(x.Time - y.Time);
		}
	}
	public static class TimedObjectUtilities
	{
		public static TTime TimeAs<TTime>(this ITimedObject obj, TempoMap tempoMap) where TTime : ITimeSpan
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo<TTime>(obj.Time, tempoMap);
		}

		public static ITimeSpan TimeAs(this ITimedObject obj, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("obj", obj);
			ThrowIfArgument.IsInvalidEnumValue("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeConverter.ConvertTo(obj.Time, timeType, tempoMap);
		}

		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, long time) where TObject : ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfTimeArgument.IsNegative("time", time);
			return objects.Where((TObject o) => o.Time == time);
		}

		public static IEnumerable<TObject> AtTime<TObject>(this IEnumerable<TObject> objects, ITimeSpan time, TempoMap tempoMap) where TObject : ITimedObject
		{
			ThrowIfArgument.IsNull("objects", objects);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			long time2 = TimeConverter.ConvertFrom(time, tempoMap);
			return objects.AtTime(time2);
		}

		public static TrackChunk ToTrackChunk(this IEnumerable<ITimedObject> timedObjects)
		{
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			TrackChunk trackChunk = new TrackChunk();
			trackChunk.AddObjects(timedObjects);
			return trackChunk;
		}

		public static MidiFile ToFile(this IEnumerable<ITimedObject> timedObjects)
		{
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			return new MidiFile(timedObjects.ToTrackChunk());
		}

		public static void AddObjects(this EventsCollection eventsCollection, IEnumerable<ITimedObject> timedObjects)
		{
			ThrowIfArgument.IsNull("eventsCollection", eventsCollection);
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			ICollection<ITimedObject> objects = timedObjects.GetObjects(ObjectType.TimedEvent);
			if (objects.Count != 0)
			{
				EventsCollection eventsCollection2 = new EventsCollection();
				AddTimedEventsToEventsCollection(eventsCollection2, objects);
				int eventsCount = eventsCollection.Count + eventsCollection2.Count;
				TimedEvent[] timedObjects2 = (from e in new EventsCollection[2] { eventsCollection, eventsCollection2 }.GetTimedEventsLazy(eventsCount, cloneEvent: false)
					select e.Item1).ToArray();
				eventsCollection.Clear();
				AddTimedEventsToEventsCollection(eventsCollection, timedObjects2);
			}
		}

		public static void AddObjects(this TrackChunk trackChunk, IEnumerable<ITimedObject> timedObjects)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("timedObjects", timedObjects);
			trackChunk.Events.AddObjects(timedObjects);
		}

		private static void AddTimedEventsToEventsCollection(EventsCollection eventsCollection, IEnumerable<ITimedObject> timedObjects)
		{
			long num = 0L;
			foreach (TimedEvent timedObject in timedObjects)
			{
				MidiEvent midiEvent = timedObject.Event;
				if (midiEvent is ChannelEvent || midiEvent is MetaEvent || midiEvent is SysExEvent)
				{
					midiEvent.DeltaTime = timedObject.Time - num;
					eventsCollection._events.Add(midiEvent);
					num = timedObject.Time;
				}
			}
		}
	}
	internal sealed class BarBeatFractionTimeSpanConverter : ITimeSpanConverter
	{
		private const double FractionalBeatsEpsilon = 1E-06;

		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new BarBeatFractionTimeSpan();
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			long endTime = time + timeSpan;
			ValueLine<TimeSignature> timeSignatureLine = tempoMap.TimeSignatureLine;
			List<ValueChange<TimeSignature>> list = timeSignatureLine.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < endTime).ToList();
			long num = 0L;
			for (int num2 = 0; num2 < list.Count - 1; num2++)
			{
				ValueChange<TimeSignature> valueChange = list[num2];
				long time2 = list[num2 + 1].Time;
				int barLength = BarBeatUtilities.GetBarLength(valueChange.Value, ticksPerQuarterNote);
				num += (time2 - valueChange.Time) / barLength;
			}
			long num3 = list.FirstOrDefault()?.Time ?? time;
			long num4 = list.LastOrDefault()?.Time ?? time;
			TimeSignature valueAtTime = timeSignatureLine.GetValueAtTime(time);
			TimeSignature valueAtTime2 = timeSignatureLine.GetValueAtTime(num4);
			CalculateComponents(num3 - time, valueAtTime, ticksPerQuarterNote, out var bars, out var beats, out var fraction);
			CalculateComponents(time + timeSpan - num4, valueAtTime2, ticksPerQuarterNote, out var bars2, out var beats2, out var fraction2);
			num += bars + bars2;
			long num5 = beats + beats2;
			if (num5 > 0 && beats > 0 && num5 >= valueAtTime.Numerator)
			{
				num++;
				num5 -= valueAtTime.Numerator;
			}
			double num6 = fraction + fraction2;
			num5 += (long)Math.Truncate(num6);
			num6 -= Math.Truncate(num6);
			return new BarBeatFractionTimeSpan(num, (double)num5 + num6);
		}

		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = (BarBeatFractionTimeSpan)timeSpan;
			if (barBeatFractionTimeSpan.Bars == 0L && barBeatFractionTimeSpan.Beats < 1E-06)
			{
				return 0L;
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			ValueLine<TimeSignature> timeSignatureLine = tempoMap.TimeSignatureLine;
			double beats = barBeatFractionTimeSpan.Beats;
			long bars = barBeatFractionTimeSpan.Bars;
			long num = (long)Math.Truncate(beats);
			double num2 = beats - Math.Truncate(beats);
			TimeSignature valueAtTime = timeSignatureLine.GetValueAtTime(time);
			int barLength = BarBeatUtilities.GetBarLength(valueAtTime, ticksPerQuarterNote);
			int beatLength = BarBeatUtilities.GetBeatLength(valueAtTime, ticksPerQuarterNote);
			long totalTicks = bars * barLength + num * beatLength + ConvertFractionToTicks(num2, beatLength);
			List<ValueChange<TimeSignature>> source = timeSignatureLine.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < time + totalTicks).ToList();
			long num3 = 0L;
			long num4 = 0L;
			ValueChange<TimeSignature> valueChange = source.FirstOrDefault();
			TimeSignature timeSignature = valueChange?.Value ?? valueAtTime;
			long lastTime = valueChange?.Time ?? time;
			CalculateComponents(lastTime - time, valueAtTime, ticksPerQuarterNote, out var bars2, out var beats2, out var fraction);
			bars -= bars2;
			if (bars > 0)
			{
				foreach (ValueChange<TimeSignature> item in timeSignatureLine.Where((ValueChange<TimeSignature> v) => v.Time > lastTime).ToList())
				{
					long num5 = item.Time - lastTime;
					num3 = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
					num4 = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
					long num6 = Math.Min(num5 / num3, bars);
					bars -= num6;
					lastTime += num6 * num3;
					if (bars == 0L)
					{
						break;
					}
					timeSignature = item.Value;
				}
				if (bars > 0)
				{
					num3 = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
					num4 = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
					lastTime += bars * num3;
				}
			}
			if (num == beats2 && Math.Abs(num2 - fraction) < 1E-06)
			{
				return lastTime - time;
			}
			if (beats2 > num && num3 > 0)
			{
				lastTime += -num3 + (valueAtTime.Numerator - beats2) * num4;
				beats2 = 0L;
			}
			if (beats2 < num)
			{
				num4 = BarBeatUtilities.GetBeatLength(timeSignatureLine.GetValueAtTime(lastTime), ticksPerQuarterNote);
				lastTime += (num - beats2) * num4;
			}
			if (fraction > num2 && num4 > 0)
			{
				lastTime += -num4 + ConvertFractionToTicks(num2 + 1.0 - fraction, num4);
			}
			if (fraction < num2)
			{
				if (num4 == 0L)
				{
					num4 = BarBeatUtilities.GetBeatLength(timeSignatureLine.GetValueAtTime(lastTime), ticksPerQuarterNote);
				}
				lastTime += ConvertFractionToTicks(num2 - fraction, num4);
			}
			return lastTime - time;
		}

		private static void CalculateComponents(long totalTicks, TimeSignature timeSignature, short ticksPerQuarterNote, out long bars, out long beats, out double fraction)
		{
			int barLength = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
			bars = Math.DivRem(totalTicks, barLength, out var result);
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
			beats = Math.DivRem(result, beatLength, out result);
			fraction = (double)result / (double)beatLength;
		}

		private static long ConvertFractionToTicks(double fraction, long beatLength)
		{
			return MathUtilities.RoundToLong((double)beatLength * fraction);
		}
	}
	internal sealed class BarBeatTicksTimeSpanConverter : ITimeSpanConverter
	{
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new BarBeatTicksTimeSpan();
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			long endTime = time + timeSpan;
			ValueLine<TimeSignature> timeSignatureLine = tempoMap.TimeSignatureLine;
			List<ValueChange<TimeSignature>> list = timeSignatureLine.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < endTime).ToList();
			long num = 0L;
			for (int num2 = 0; num2 < list.Count - 1; num2++)
			{
				ValueChange<TimeSignature> valueChange = list[num2];
				long time2 = list[num2 + 1].Time;
				int barLength = BarBeatUtilities.GetBarLength(valueChange.Value, ticksPerQuarterNote);
				num += (time2 - valueChange.Time) / barLength;
			}
			long num3 = list.FirstOrDefault()?.Time ?? time;
			long num4 = list.LastOrDefault()?.Time ?? time;
			TimeSignature valueAtTime = timeSignatureLine.GetValueAtTime(time);
			TimeSignature valueAtTime2 = timeSignatureLine.GetValueAtTime(num4);
			CalculateComponents(num3 - time, valueAtTime, ticksPerQuarterNote, out var bars, out var beats, out var ticks);
			CalculateComponents(time + timeSpan - num4, valueAtTime2, ticksPerQuarterNote, out var bars2, out var beats2, out var ticks2);
			num += bars + bars2;
			long num5 = beats + beats2;
			if (num5 > 0 && beats > 0 && num5 >= valueAtTime.Numerator)
			{
				num++;
				num5 -= valueAtTime.Numerator;
			}
			long num6 = ticks + ticks2;
			if (num6 > 0)
			{
				int beatLength = BarBeatUtilities.GetBeatLength(valueAtTime, ticksPerQuarterNote);
				if (ticks > 0 && num6 >= beatLength)
				{
					num5++;
					num6 -= beatLength;
				}
			}
			return new BarBeatTicksTimeSpan(num, num5, num6);
		}

		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = (BarBeatTicksTimeSpan)timeSpan;
			if (barBeatTicksTimeSpan.Bars == 0L && barBeatTicksTimeSpan.Beats == 0L && barBeatTicksTimeSpan.Ticks == 0L)
			{
				return 0L;
			}
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			ValueLine<TimeSignature> timeSignatureLine = tempoMap.TimeSignatureLine;
			long bars = barBeatTicksTimeSpan.Bars;
			long beats = barBeatTicksTimeSpan.Beats;
			long ticks = barBeatTicksTimeSpan.Ticks;
			TimeSignature valueAtTime = timeSignatureLine.GetValueAtTime(time);
			int barLength = BarBeatUtilities.GetBarLength(valueAtTime, ticksPerQuarterNote);
			int beatLength = BarBeatUtilities.GetBeatLength(valueAtTime, ticksPerQuarterNote);
			long totalTicks = bars * barLength + beats * beatLength + ticks;
			List<ValueChange<TimeSignature>> source = timeSignatureLine.Where((ValueChange<TimeSignature> v) => v.Time > time && v.Time < time + totalTicks).ToList();
			long num = 0L;
			long num2 = 0L;
			ValueChange<TimeSignature> valueChange = source.FirstOrDefault();
			TimeSignature timeSignature = valueChange?.Value ?? valueAtTime;
			long lastTime = valueChange?.Time ?? time;
			CalculateComponents(lastTime - time, valueAtTime, ticksPerQuarterNote, out var bars2, out var beats2, out var ticks2);
			bars -= bars2;
			foreach (ValueChange<TimeSignature> item in timeSignatureLine.Where((ValueChange<TimeSignature> v) => v.Time > lastTime).ToList())
			{
				long num3 = item.Time - lastTime;
				num = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
				num2 = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
				long num4 = Math.Min(num3 / num, bars);
				bars -= num4;
				lastTime += num4 * num;
				if (bars == 0L)
				{
					break;
				}
				timeSignature = item.Value;
			}
			if (bars > 0)
			{
				num = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
				num2 = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
				lastTime += bars * num;
			}
			if (beats == beats2 && ticks == ticks2)
			{
				return lastTime - time;
			}
			if (beats2 > beats && num > 0)
			{
				lastTime += -num + (valueAtTime.Numerator - beats2) * num2;
				beats2 = 0L;
			}
			if (beats2 < beats)
			{
				num2 = BarBeatUtilities.GetBeatLength(timeSignatureLine.GetValueAtTime(lastTime), ticksPerQuarterNote);
				lastTime += (beats - beats2) * num2;
			}
			if (ticks2 > ticks && num2 > 0)
			{
				lastTime += -num2 + beatLength - ticks2;
				ticks2 = 0L;
			}
			if (ticks2 < ticks)
			{
				lastTime += ticks - ticks2;
			}
			return lastTime - time;
		}

		private static void CalculateComponents(long totalTicks, TimeSignature timeSignature, short ticksPerQuarterNote, out long bars, out long beats, out long ticks)
		{
			int barLength = BarBeatUtilities.GetBarLength(timeSignature, ticksPerQuarterNote);
			bars = Math.DivRem(totalTicks, barLength, out ticks);
			int beatLength = BarBeatUtilities.GetBeatLength(timeSignature, ticksPerQuarterNote);
			beats = Math.DivRem(ticks, beatLength, out ticks);
		}
	}
	internal interface ITimeSpanConverter
	{
		ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap);

		long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap);
	}
	public static class LengthConverter
	{
		public static TTimeSpan ConvertTo<TTimeSpan>(long length, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, time, tempoMap);
		}

		public static ITimeSpan ConvertTo(long length, TimeSpanType lengthType, long time, TempoMap tempoMap)
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, time, tempoMap);
		}

		public static TTimeSpan ConvertTo<TTimeSpan>(long length, ITimeSpan time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		public static ITimeSpan ConvertTo(long length, TimeSpanType lengthType, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfLengthArgument.IsNegative("length", length);
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan length, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, time, tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan length, TimeSpanType lengthType, long time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, time, tempoMap);
		}

		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan length, ITimeSpan time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(length, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan length, TimeSpanType lengthType, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsInvalidEnumValue("lengthType", lengthType);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan length, Type lengthType, long time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("lengthType", lengthType);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, time, tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan length, Type lengthType, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("lengthType", lengthType);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(length, lengthType, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}

		public static long ConvertFrom(ITimeSpan length, long time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertFrom(length, time, tempoMap);
		}

		public static long ConvertFrom(ITimeSpan length, ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("length", length);
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertFrom(length, TimeConverter.ConvertFrom(time, tempoMap), tempoMap);
		}
	}
	internal sealed class MathTimeSpanConverter : ITimeSpanConverter
	{
		private static readonly Dictionary<TimeSpanMode, Func<MathTimeSpan, long, TempoMap, long>> Converters = new Dictionary<TimeSpanMode, Func<MathTimeSpan, long, TempoMap, long>>
		{
			[TimeSpanMode.TimeTime] = ConvertFromTimeTime,
			[TimeSpanMode.TimeLength] = ConvertFromTimeLength,
			[TimeSpanMode.LengthLength] = ConvertFromLengthLength
		};

		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			throw new NotSupportedException("Conversion to the MathTimeSpan is not supported.");
		}

		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			MathTimeSpan mathTimeSpan = (MathTimeSpan)timeSpan;
			if (Converters.TryGetValue(mathTimeSpan.Mode, out var value))
			{
				return value(mathTimeSpan, time, tempoMap);
			}
			throw new ArgumentException($"{mathTimeSpan.Mode} mode is not supported by the converter.", "timeSpan");
		}

		private static long ConvertFromLengthLength(MathTimeSpan mathTimeSpan, long time, TempoMap tempoMap)
		{
			long num = LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan1, time, tempoMap);
			long num2 = time + num;
			return mathTimeSpan.Operation switch
			{
				MathOperation.Add => num + LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num2, tempoMap), 
				MathOperation.Subtract => num - LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num2, tempoMap.Flip(num2)), 
				_ => throw new ArgumentException($"{mathTimeSpan.Operation} is not supported by the converter.", "mathTimeSpan"), 
			};
		}

		private static long ConvertFromTimeLength(MathTimeSpan mathTimeSpan, long time, TempoMap tempoMap)
		{
			long num = TimeConverter.ConvertFrom(mathTimeSpan.TimeSpan1, tempoMap);
			return mathTimeSpan.Operation switch
			{
				MathOperation.Add => num + LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num, tempoMap), 
				MathOperation.Subtract => num - LengthConverter.ConvertFrom(mathTimeSpan.TimeSpan2, num, tempoMap.Flip(num)), 
				_ => throw new ArgumentException($"{mathTimeSpan.Operation} is not supported by the converter.", "mathTimeSpan"), 
			};
		}

		private static long ConvertFromTimeTime(MathTimeSpan mathTimeSpan, long time, TempoMap tempoMap)
		{
			ITimeSpan timeSpan = mathTimeSpan.TimeSpan1;
			ITimeSpan timeSpan2 = mathTimeSpan.TimeSpan2;
			MathTimeSpan mathTimeSpan2 = mathTimeSpan.TimeSpan1 as MathTimeSpan;
			if (mathTimeSpan2 != null)
			{
				timeSpan = TimeSpanConverter.ConvertTo(mathTimeSpan2, mathTimeSpan2.TimeSpan1.GetType(), time, tempoMap);
			}
			if (mathTimeSpan.Operation == MathOperation.Subtract)
			{
				ITimeSpan timeSpan3 = TimeConverter.ConvertTo(timeSpan2, timeSpan.GetType(), tempoMap);
				return TimeSpanConverter.ConvertFrom(timeSpan.Subtract(timeSpan3, TimeSpanMode.TimeTime), time, tempoMap);
			}
			throw new ArgumentException($"{mathTimeSpan.Operation} is not supported by the converter.", "mathTimeSpan");
		}
	}
	internal sealed class MetricTimeSpanConverter : ITimeSpanConverter
	{
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			if (tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new MetricTimeSpan();
			}
			MetricTimeSpan metricTimeSpan = TicksToMetricTimeSpan(time, tempoMap);
			return TicksToMetricTimeSpan(time + timeSpan, tempoMap) - metricTimeSpan;
		}

		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			if (tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			MetricTimeSpan metricTimeSpan = (MetricTimeSpan)timeSpan;
			if ((TimeSpan)metricTimeSpan == TimeSpan.Zero)
			{
				return 0L;
			}
			return MetricTimeSpanToTicks(TicksToMetricTimeSpan(time, tempoMap) + metricTimeSpan, tempoMap) - time;
		}

		private static MetricTimeSpan TicksToMetricTimeSpan(long timeSpan, TempoMap tempoMap)
		{
			if (timeSpan == 0L)
			{
				return new MetricTimeSpan();
			}
			MetricTempoMapValuesCache valuesCache = tempoMap.GetValuesCache<MetricTempoMapValuesCache>();
			MetricTempoMapValuesCache.AccumulatedMicroseconds lastElementBelowThreshold = MathUtilities.GetLastElementBelowThreshold(valuesCache.Microseconds, timeSpan, (MetricTempoMapValuesCache.AccumulatedMicroseconds m) => m.Time);
			double num = lastElementBelowThreshold?.Microseconds ?? 0.0;
			long num2 = lastElementBelowThreshold?.Time ?? 0;
			double microsecondsPerTick = lastElementBelowThreshold?.MicrosecondsPerTick ?? valuesCache.DefaultMicrosecondsPerTick;
			return new MetricTimeSpan(RoundMicroseconds(num + GetMicroseconds(timeSpan - num2, microsecondsPerTick)));
		}

		private static long MetricTimeSpanToTicks(MetricTimeSpan timeSpan, TempoMap tempoMap)
		{
			long timeMicroseconds = timeSpan.TotalMicroseconds;
			if (timeMicroseconds == 0L)
			{
				return 0L;
			}
			MetricTempoMapValuesCache valuesCache = tempoMap.GetValuesCache<MetricTempoMapValuesCache>();
			MetricTempoMapValuesCache.AccumulatedMicroseconds accumulatedMicroseconds = valuesCache.Microseconds.TakeWhile((MetricTempoMapValuesCache.AccumulatedMicroseconds m) => m.Microseconds < (double)timeMicroseconds).LastOrDefault();
			double num = accumulatedMicroseconds?.Microseconds ?? 0.0;
			long num2 = accumulatedMicroseconds?.Time ?? 0;
			double num3 = accumulatedMicroseconds?.TicksPerMicrosecond ?? valuesCache.DefaultTicksPerMicrosecond;
			return RoundMicroseconds((double)num2 + ((double)timeMicroseconds - num) * num3);
		}

		private static double GetMicroseconds(long time, double microsecondsPerTick)
		{
			return (double)time * microsecondsPerTick;
		}

		private static long RoundMicroseconds(double microseconds)
		{
			return MathUtilities.RoundToLong(microseconds);
		}
	}
	internal sealed class MidiTimeSpanConverter : ITimeSpanConverter
	{
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			return (MidiTimeSpan)timeSpan;
		}

		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			return ((MidiTimeSpan)timeSpan).TimeSpan;
		}
	}
	internal sealed class MusicalTimeSpanConverter : ITimeSpanConverter
	{
		public ITimeSpan ConvertTo(long timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			if (timeSpan == 0L)
			{
				return new MusicalTimeSpan();
			}
			Tuple<long, long> tuple = MathUtilities.SolveDiophantineEquation(4 * ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote, -timeSpan);
			return new MusicalTimeSpan(Math.Abs(tuple.Item1), Math.Abs(tuple.Item2));
		}

		public long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division is not supported for time span conversion.", "tempoMap");
			}
			MusicalTimeSpan musicalTimeSpan = (MusicalTimeSpan)timeSpan;
			if (musicalTimeSpan.Numerator == 0L)
			{
				return 0L;
			}
			return MathUtilities.RoundToLong(4.0 * (double)musicalTimeSpan.Numerator * (double)ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote / (double)musicalTimeSpan.Denominator);
		}
	}
	public static class TimeConverter
	{
		public static TTimeSpan ConvertTo<TTimeSpan>(long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(time, 0L, tempoMap);
		}

		public static ITimeSpan ConvertTo(long time, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsInvalidEnumValue("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(time, timeType, 0L, tempoMap);
		}

		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo<TTimeSpan>(time, 0L, tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan time, TimeSpanType timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsInvalidEnumValue("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(time, timeType, 0L, tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan time, Type timeType, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("timeType", timeType);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertTo(time, timeType, 0L, tempoMap);
		}

		public static long ConvertFrom(ITimeSpan time, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("time", time);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			return TimeSpanConverter.ConvertFrom(time, 0L, tempoMap);
		}
	}
	internal static class TimeSpanConverter
	{
		private static readonly Dictionary<TimeSpanType, Type> TimeSpansTypes = new Dictionary<TimeSpanType, Type>
		{
			[TimeSpanType.Midi] = typeof(MidiTimeSpan),
			[TimeSpanType.Metric] = typeof(MetricTimeSpan),
			[TimeSpanType.Musical] = typeof(MusicalTimeSpan),
			[TimeSpanType.BarBeatTicks] = typeof(BarBeatTicksTimeSpan),
			[TimeSpanType.BarBeatFraction] = typeof(BarBeatFractionTimeSpan)
		};

		private static readonly Dictionary<Type, ITimeSpanConverter> Converters = new Dictionary<Type, ITimeSpanConverter>
		{
			[typeof(MidiTimeSpan)] = new MidiTimeSpanConverter(),
			[typeof(MetricTimeSpan)] = new MetricTimeSpanConverter(),
			[typeof(MusicalTimeSpan)] = new MusicalTimeSpanConverter(),
			[typeof(BarBeatTicksTimeSpan)] = new BarBeatTicksTimeSpanConverter(),
			[typeof(BarBeatFractionTimeSpan)] = new BarBeatFractionTimeSpanConverter(),
			[typeof(MathTimeSpan)] = new MathTimeSpanConverter()
		};

		public static TTimeSpan ConvertTo<TTimeSpan>(long timeSpan, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			return (TTimeSpan)GetConverter<TTimeSpan>().ConvertTo(timeSpan, time, tempoMap);
		}

		public static ITimeSpan ConvertTo(long timeSpan, TimeSpanType timeSpanType, long time, TempoMap tempoMap)
		{
			return GetConverter(timeSpanType).ConvertTo(timeSpan, time, tempoMap);
		}

		public static TTimeSpan ConvertTo<TTimeSpan>(ITimeSpan timeSpan, long time, TempoMap tempoMap) where TTimeSpan : ITimeSpan
		{
			if (timeSpan is TTimeSpan)
			{
				return (TTimeSpan)timeSpan.Clone();
			}
			return ConvertTo<TTimeSpan>(ConvertFrom(timeSpan, time, tempoMap), time, tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan timeSpan, TimeSpanType timeSpanType, long time, TempoMap tempoMap)
		{
			if (timeSpan.GetType() == TimeSpansTypes[timeSpanType])
			{
				return timeSpan.Clone();
			}
			return ConvertTo(ConvertFrom(timeSpan, time, tempoMap), timeSpanType, time, tempoMap);
		}

		public static ITimeSpan ConvertTo(ITimeSpan timeSpan, Type timeSpanType, long time, TempoMap tempoMap)
		{
			if (timeSpan.GetType() == timeSpanType)
			{
				return timeSpan.Clone();
			}
			return GetConverter(timeSpanType).ConvertTo(ConvertFrom(timeSpan, time, tempoMap), time, tempoMap);
		}

		public static long ConvertFrom(ITimeSpan timeSpan, long time, TempoMap tempoMap)
		{
			return GetConverter(timeSpan.GetType()).ConvertFrom(timeSpan, time, tempoMap);
		}

		private static ITimeSpanConverter GetConverter<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return GetConverter(typeof(TTimeSpan));
		}

		private static ITimeSpanConverter GetConverter(TimeSpanType timeSpanType)
		{
			if (!TimeSpansTypes.TryGetValue(timeSpanType, out var value))
			{
				throw new NotSupportedException($"Converter for {timeSpanType} is not supported.");
			}
			return GetConverter(value);
		}

		private static ITimeSpanConverter GetConverter(Type timeSpanType)
		{
			if (Converters.TryGetValue(timeSpanType, out var value))
			{
				return value;
			}
			throw new NotSupportedException($"Converter for {timeSpanType} is not supported.");
		}
	}
	public interface ITimeSpan : IComparable
	{
		ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode);

		ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode);

		ITimeSpan Multiply(double multiplier);

		ITimeSpan Divide(double divisor);

		ITimeSpan Clone();
	}
	public enum MathOperation
	{
		Add,
		Subtract
	}
	internal static class BarBeatFractionTimeSpanParser
	{
		private const string BarsGroupName = "bars";

		private const string BeatsGroupName = "beats";

		private static readonly string BarsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("bars");

		private static readonly string BeatsGroup = ParsingUtilities.GetNonnegativeDoubleNumberGroup("beats");

		private static readonly string Divider = Regex.Escape("_");

		private static readonly string[] Patterns = new string[1] { BarsGroup + "\\s*" + Divider + "\\s*" + BeatsGroup };

		private const string BarsIsOutOfRange = "Bars number is out of range.";

		private const string BeatsIsOutOfRange = "Beats number is out of range.";

		internal static ParsingResult TryParse(string input, out BarBeatFractionTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "bars", 0L, out var value))
			{
				return ParsingResult.Error("Bars number is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeDouble(match, "beats", 0.0, out var value2))
			{
				return ParsingResult.Error("Beats number is out of range.");
			}
			timeSpan = new BarBeatFractionTimeSpan(value, value2);
			return ParsingResult.Parsed;
		}
	}
	internal static class BarBeatTicksTimeSpanParser
	{
		private const string BarsGroupName = "bars";

		private const string BeatsGroupName = "beats";

		private const string TicksGroupName = "ticks";

		private static readonly string BarsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("bars");

		private static readonly string BeatsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("beats");

		private static readonly string TicksGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("ticks");

		private static readonly string Divider = Regex.Escape(".");

		private static readonly string[] Patterns = new string[1] { BarsGroup + "\\s*" + Divider + "\\s*" + BeatsGroup + "\\s*" + Divider + "\\s*" + TicksGroup };

		private const string BarsIsOutOfRange = "Bars number is out of range.";

		private const string BeatsIsOutOfRange = "Beats number is out of range.";

		private const string TicksIsOutOfRange = "Ticks number is out of range.";

		internal static ParsingResult TryParse(string input, out BarBeatTicksTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "bars", 0L, out var value))
			{
				return ParsingResult.Error("Bars number is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "beats", 0L, out var value2))
			{
				return ParsingResult.Error("Beats number is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "ticks", 0L, out var value3))
			{
				return ParsingResult.Error("Ticks number is out of range.");
			}
			timeSpan = new BarBeatTicksTimeSpan(value, value2, value3);
			return ParsingResult.Parsed;
		}
	}
	internal static class MetricTimeSpanParser
	{
		private const string HoursGroupName = "h";

		private const string MinutesGroupName = "m";

		private const string SecondsGroupName = "s";

		private const string MillisecondsGroupName = "ms";

		private static readonly string HoursGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("h");

		private static readonly string MinutesGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("m");

		private static readonly string SecondsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("s");

		private static readonly string MillisecondsGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("ms");

		private static readonly string LetteredHoursGroup = HoursGroup + "\\s*h";

		private static readonly string LetteredMinutesGroup = MinutesGroup + "\\s*m";

		private static readonly string LetteredSecondsGroup = SecondsGroup + "\\s*s";

		private static readonly string LetteredMillisecondsGroup = MillisecondsGroup + "\\s*ms";

		private static readonly string Divider = Regex.Escape(":");

		private static readonly string[] Patterns = new string[18]
		{
			HoursGroup + "\\s*" + Divider + "\\s*" + MinutesGroup + "\\s*" + Divider + "\\s*" + SecondsGroup + "\\s*" + Divider + "\\s*" + MillisecondsGroup,
			HoursGroup + "\\s*" + Divider + "\\s*" + MinutesGroup + "\\s*" + Divider + "\\s*" + SecondsGroup,
			MinutesGroup + "\\s*" + Divider + "\\s*" + SecondsGroup,
			LetteredHoursGroup + "\\s*" + LetteredMinutesGroup + "\\s*" + LetteredSecondsGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredHoursGroup + "\\s*" + LetteredMinutesGroup + "\\s*" + LetteredSecondsGroup,
			LetteredHoursGroup + "\\s*" + LetteredMinutesGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredHoursGroup + "\\s*" + LetteredSecondsGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredMinutesGroup + "\\s*" + LetteredSecondsGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredHoursGroup + "\\s*" + LetteredMinutesGroup,
			LetteredHoursGroup + "\\s*" + LetteredSecondsGroup,
			LetteredHoursGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredMinutesGroup + "\\s*" + LetteredSecondsGroup,
			LetteredMinutesGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredSecondsGroup + "\\s*" + LetteredMillisecondsGroup,
			LetteredHoursGroup,
			LetteredMinutesGroup,
			LetteredSecondsGroup,
			LetteredMillisecondsGroup
		};

		private const string HoursIsOutOfRange = "Hours number is out of range.";

		private const string MinutesIsOutOfRange = "Minutes number is out of range.";

		private const string SecondsIsOutOfRange = "Seconds number is out of range.";

		private const string MillisecondsIsOutOfRange = "Milliseconds number is out of range.";

		internal static ParsingResult TryParse(string input, out MetricTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			if (!ParsingUtilities.ParseNonnegativeInt(match, "h", 0, out var value))
			{
				return ParsingResult.Error("Hours number is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeInt(match, "m", 0, out var value2))
			{
				return ParsingResult.Error("Minutes number is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeInt(match, "s", 0, out var value3))
			{
				return ParsingResult.Error("Seconds number is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeInt(match, "ms", 0, out var value4))
			{
				return ParsingResult.Error("Milliseconds number is out of range.");
			}
			timeSpan = new MetricTimeSpan(value, value2, value3, value4);
			return ParsingResult.Parsed;
		}
	}
	internal static class MidiTimeSpanParser
	{
		private const string TimeSpanGroupName = "ts";

		private static readonly string TimeSpanGroup = ParsingUtilities.GetNonnegativeIntegerNumberGroup("ts");

		private static readonly string[] Patterns = new string[1] { TimeSpanGroup ?? "" };

		private const string OutOfRange = "Time span is out of range.";

		internal static ParsingResult TryParse(string input, out MidiTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "ts", 0L, out var value))
			{
				return ParsingResult.Error("Time span is out of range.");
			}
			timeSpan = new MidiTimeSpan(value);
			return ParsingResult.Parsed;
		}
	}
	internal static class MusicalTimeSpanParser
	{
		private static readonly Dictionary<string, Tuple<int, int>> Fractions = new Dictionary<string, Tuple<int, int>>
		{
			["w"] = Tuple.Create(1, 1),
			["h"] = Tuple.Create(1, 2),
			["q"] = Tuple.Create(1, 4),
			["e"] = Tuple.Create(1, 8),
			["s"] = Tuple.Create(1, 16)
		};

		private static readonly Dictionary<string, Tuple<int, int>> Tuplets = new Dictionary<string, Tuple<int, int>>
		{
			["t"] = Tuple.Create(3, 2),
			["d"] = Tuple.Create(2, 3)
		};

		private const string NumeratorGroupName = "n";

		private const string DenominatorGroupName = "d";

		private const string FractionMnemonicGroupName = "fm";

		private const string TupletNotesCountGroupName = "tn";

		private const string TupletSpaceSizeGroupName = "ts";

		private const string TupletMnemonicGroupName = "tm";

		private const string DotsGroupName = "dt";

		private static readonly string FractionGroup = "(?<n>\\d+)?\\/(?<d>\\d+)";

		private static readonly string FractionMnemonicGroup = GetMnemonicGroup("fm", Fractions.Keys);

		private static readonly string TupletGroup = "\\[\\s*(?<tn>\\d+)\\s*\\:\\s*(?<ts>\\d+)\\s*\\]";

		private static readonly string TupletMnemonicGroup = GetMnemonicGroup("tm", Tuplets.Keys);

		private static readonly string DotsGroup = "(?<dt>\\.+)";

		private static readonly string[] Patterns = new string[1] { "(" + FractionGroup + "|" + FractionMnemonicGroup + ")\\s*(" + TupletGroup + "|" + TupletMnemonicGroup + ")?\\s*" + DotsGroup + "?" };

		private const string NumeratorIsOutOfRange = "Numerator is out of range.";

		private const string DenominatorIsOutOfRange = "Denominator is out of range.";

		private const string TupletNotesCountIsOutOfRange = "Tuplet's notes count is out of range.";

		private const string TupletSpaceSizeIsOutOfRange = "Tuplet's space size is out of range.";

		internal static ParsingResult TryParse(string input, out MusicalTimeSpan timeSpan)
		{
			timeSpan = null;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			Match match = ParsingUtilities.Match(input, Patterns);
			if (match == null)
			{
				return ParsingResult.NotMatched;
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "n", 1L, out var value))
			{
				return ParsingResult.Error("Numerator is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeLong(match, "d", 1L, out var value2))
			{
				return ParsingResult.Error("Denominator is out of range.");
			}
			Group obj = match.Groups["fm"];
			if (obj.Success)
			{
				Tuple<int, int> tuple = Fractions[obj.Value];
				value = tuple.Item1;
				value2 = tuple.Item2;
			}
			if (!ParsingUtilities.ParseNonnegativeInt(match, "tn", 1, out var value3))
			{
				return ParsingResult.Error("Tuplet's notes count is out of range.");
			}
			if (!ParsingUtilities.ParseNonnegativeInt(match, "ts", 1, out var value4))
			{
				return ParsingResult.Error("Tuplet's space size is out of range.");
			}
			Group obj2 = match.Groups["tm"];
			if (obj2.Success)
			{
				Tuple<int, int> tuple2 = Tuplets[obj2.Value];
				value3 = tuple2.Item1;
				value4 = tuple2.Item2;
			}
			Group obj3 = match.Groups["dt"];
			int dotsCount = (obj3.Success ? obj3.Value.Length : 0);
			timeSpan = new MusicalTimeSpan(value, value2).Dotted(dotsCount).Tuplet(value3, value4);
			return ParsingResult.Parsed;
		}

		private static string GetMnemonicGroup(string groupName, IEnumerable<string> mnemonics)
		{
			return "(?<" + groupName + ">[" + string.Join(string.Empty, mnemonics) + "])";
		}
	}
	public sealed class BarBeatFractionTimeSpan : ITimeSpan, IComparable, IComparable<BarBeatFractionTimeSpan>, IEquatable<BarBeatFractionTimeSpan>
	{
		private const double FractionEpsilon = 1E-05;

		public long Bars { get; }

		public double Beats { get; }

		public BarBeatFractionTimeSpan()
			: this(0L, 0.0)
		{
		}

		public BarBeatFractionTimeSpan(long bars)
			: this(bars, 0.0)
		{
		}

		public BarBeatFractionTimeSpan(long bars, double beats)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNegative("beats", beats, "Beats number is negative.");
			Bars = bars;
			Beats = beats;
		}

		public static bool TryParse(string input, out BarBeatFractionTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse(input, (Parsing<BarBeatFractionTimeSpan>)BarBeatFractionTimeSpanParser.TryParse, out timeSpan);
		}

		public static BarBeatFractionTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<BarBeatFractionTimeSpan>(input, BarBeatFractionTimeSpanParser.TryParse);
		}

		public static bool operator ==(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			return timeSpan1?.Equals(timeSpan2) ?? ((object)timeSpan2 == null);
		}

		public static bool operator !=(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		public static BarBeatFractionTimeSpan operator +(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new BarBeatFractionTimeSpan(timeSpan1.Bars + timeSpan2.Bars, timeSpan1.Beats + timeSpan2.Beats);
		}

		public static BarBeatFractionTimeSpan operator -(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1 < timeSpan2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new BarBeatFractionTimeSpan(timeSpan1.Bars - timeSpan2.Bars, timeSpan1.Beats - timeSpan2.Beats);
		}

		public static bool operator <(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		public static bool operator >(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		public static bool operator <=(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		public static bool operator >=(BarBeatFractionTimeSpan timeSpan1, BarBeatFractionTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BarBeatFractionTimeSpan);
		}

		public override int GetHashCode()
		{
			return (17 * 23 + Bars.GetHashCode()) * 23 + Beats.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Bars}_{Beats.ToString(CultureInfo.InvariantCulture)}";
		}

		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = timeSpan as BarBeatFractionTimeSpan;
			if (!(barBeatFractionTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + barBeatFractionTimeSpan;
		}

		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			BarBeatFractionTimeSpan barBeatFractionTimeSpan = timeSpan as BarBeatFractionTimeSpan;
			if (!(barBeatFractionTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - barBeatFractionTimeSpan;
		}

		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new BarBeatFractionTimeSpan(MathUtilities.RoundToLong((double)Bars * multiplier), Beats * multiplier);
		}

		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new BarBeatFractionTimeSpan(MathUtilities.RoundToLong((double)Bars / divisor), Beats / divisor);
		}

		public ITimeSpan Clone()
		{
			return new BarBeatFractionTimeSpan(Bars, Beats);
		}

		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is BarBeatFractionTimeSpan other2))
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return CompareTo(other2);
		}

		public int CompareTo(BarBeatFractionTimeSpan other)
		{
			if ((object)other == null)
			{
				return 1;
			}
			long num = Bars - other.Bars;
			double num2 = Beats - other.Beats;
			return Math.Sign((num != 0L) ? ((double)num) : num2);
		}

		public bool Equals(BarBeatFractionTimeSpan other)
		{
			if ((object)this == other)
			{
				return true;
			}
			if ((object)other == null)
			{
				return false;
			}
			if (Bars == other.Bars)
			{
				return Math.Abs(Beats - other.Beats) < 1E-05;
			}
			return false;
		}
	}
	public sealed class BarBeatTicksTimeSpan : ITimeSpan, IComparable, IComparable<BarBeatTicksTimeSpan>, IEquatable<BarBeatTicksTimeSpan>
	{
		public long Bars { get; }

		public long Beats { get; }

		public long Ticks { get; }

		public BarBeatTicksTimeSpan()
			: this(0L, 0L)
		{
		}

		public BarBeatTicksTimeSpan(long bars)
			: this(bars, 0L)
		{
		}

		public BarBeatTicksTimeSpan(long bars, long beats)
			: this(bars, beats, 0L)
		{
		}

		public BarBeatTicksTimeSpan(long bars, long beats, long ticks)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNegative("beats", beats, "Beats number is negative.");
			ThrowIfArgument.IsNegative("ticks", ticks, "Ticks number is negative.");
			Bars = bars;
			Beats = beats;
			Ticks = ticks;
		}

		public static bool TryParse(string input, out BarBeatTicksTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse(input, (Parsing<BarBeatTicksTimeSpan>)BarBeatTicksTimeSpanParser.TryParse, out timeSpan);
		}

		public static BarBeatTicksTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<BarBeatTicksTimeSpan>(input, BarBeatTicksTimeSpanParser.TryParse);
		}

		public static bool operator ==(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			return timeSpan1?.Equals(timeSpan2) ?? ((object)timeSpan2 == null);
		}

		public static bool operator !=(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		public static BarBeatTicksTimeSpan operator +(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new BarBeatTicksTimeSpan(timeSpan1.Bars + timeSpan2.Bars, timeSpan1.Beats + timeSpan2.Beats, timeSpan1.Ticks + timeSpan2.Ticks);
		}

		public static BarBeatTicksTimeSpan operator -(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1 < timeSpan2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new BarBeatTicksTimeSpan(timeSpan1.Bars - timeSpan2.Bars, timeSpan1.Beats - timeSpan2.Beats, timeSpan1.Ticks - timeSpan2.Ticks);
		}

		public static bool operator <(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		public static bool operator >(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		public static bool operator <=(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		public static bool operator >=(BarBeatTicksTimeSpan timeSpan1, BarBeatTicksTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as BarBeatTicksTimeSpan);
		}

		public override int GetHashCode()
		{
			return ((17 * 23 + Bars.GetHashCode()) * 23 + Beats.GetHashCode()) * 23 + Ticks.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Bars}.{Beats}.{Ticks}";
		}

		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = timeSpan as BarBeatTicksTimeSpan;
			if (!(barBeatTicksTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + barBeatTicksTimeSpan;
		}

		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			BarBeatTicksTimeSpan barBeatTicksTimeSpan = timeSpan as BarBeatTicksTimeSpan;
			if (!(barBeatTicksTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - barBeatTicksTimeSpan;
		}

		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new BarBeatTicksTimeSpan(MathUtilities.RoundToLong((double)Bars * multiplier), MathUtilities.RoundToLong((double)Beats * multiplier), MathUtilities.RoundToLong((double)Ticks * multiplier));
		}

		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new BarBeatTicksTimeSpan(MathUtilities.RoundToLong((double)Bars / divisor), MathUtilities.RoundToLong((double)Beats / divisor), MathUtilities.RoundToLong((double)Ticks / divisor));
		}

		public ITimeSpan Clone()
		{
			return new BarBeatTicksTimeSpan(Bars, Beats, Ticks);
		}

		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is BarBeatTicksTimeSpan other2))
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return CompareTo(other2);
		}

		public int CompareTo(BarBeatTicksTimeSpan other)
		{
			if ((object)other == null)
			{
				return 1;
			}
			long num = Bars - other.Bars;
			long num2 = Beats - other.Beats;
			long num3 = Ticks - other.Ticks;
			return Math.Sign((num != 0L) ? num : ((num2 != 0L) ? num2 : num3));
		}

		public bool Equals(BarBeatTicksTimeSpan other)
		{
			if ((object)this == other)
			{
				return true;
			}
			if ((object)other == null)
			{
				return false;
			}
			if (Bars == other.Bars && Beats == other.Beats)
			{
				return Ticks == other.Ticks;
			}
			return false;
		}
	}
	public sealed class MathTimeSpan : ITimeSpan, IComparable
	{
		private const string TimeModeString = "T";

		private const string LengthModeString = "L";

		private static readonly Dictionary<TimeSpanMode, Tuple<string, string>> ModeStrings = new Dictionary<TimeSpanMode, Tuple<string, string>>
		{
			[TimeSpanMode.TimeTime] = Tuple.Create("T", "T"),
			[TimeSpanMode.TimeLength] = Tuple.Create("T", "L"),
			[TimeSpanMode.LengthLength] = Tuple.Create("L", "L")
		};

		public ITimeSpan TimeSpan1 { get; }

		public ITimeSpan TimeSpan2 { get; }

		public MathOperation Operation { get; }

		public TimeSpanMode Mode { get; }

		internal MathTimeSpan(ITimeSpan timeSpan1, ITimeSpan timeSpan2, MathOperation operation, TimeSpanMode mode)
		{
			TimeSpan1 = timeSpan1;
			TimeSpan2 = timeSpan2;
			Operation = operation;
			Mode = mode;
		}

		public static bool operator ==(MathTimeSpan timeSpan1, MathTimeSpan timeSpan2)
		{
			if ((object)timeSpan1 == timeSpan2)
			{
				return true;
			}
			if ((object)timeSpan1 == null || (object)timeSpan2 == null)
			{
				return false;
			}
			if (timeSpan1.TimeSpan1.Equals(timeSpan2.TimeSpan1) && timeSpan1.TimeSpan2.Equals(timeSpan2.TimeSpan2) && timeSpan1.Operation == timeSpan2.Operation)
			{
				return timeSpan1.Mode == timeSpan2.Mode;
			}
			return false;
		}

		public static bool operator !=(MathTimeSpan timeSpan1, MathTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		public override string ToString()
		{
			string text = ((Operation == MathOperation.Add) ? "+" : "-");
			Tuple<string, string> tuple = ModeStrings[Mode];
			return $"({TimeSpan1}{tuple.Item1} {text} {TimeSpan2}{tuple.Item2})";
		}

		public override bool Equals(object obj)
		{
			return this == obj as MathTimeSpan;
		}

		public override int GetHashCode()
		{
			return (((17 * 23 + TimeSpan1.GetHashCode()) * 23 + TimeSpan2.GetHashCode()) * 23 + Operation.GetHashCode()) * 23 + Mode.GetHashCode();
		}

		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			return TimeSpanUtilities.Add(this, timeSpan, mode);
		}

		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			return TimeSpanUtilities.Subtract(this, timeSpan, mode);
		}

		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MathTimeSpan(TimeSpan1.Multiply(multiplier), TimeSpan2.Multiply(multiplier), Operation, Mode);
		}

		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNegative("divisor", divisor, "Divisor is negative.");
			return new MathTimeSpan(TimeSpan1.Divide(divisor), TimeSpan2.Divide(divisor), Operation, Mode);
		}

		public ITimeSpan Clone()
		{
			return new MathTimeSpan(TimeSpan1.Clone(), TimeSpan2.Clone(), Operation, Mode);
		}

		public int CompareTo(object other)
		{
			throw new InvalidOperationException("Cannot compare MathTimeSpan.");
		}
	}
	public sealed class MetricTimeSpan : ITimeSpan, IComparable, IComparable<MetricTimeSpan>, IEquatable<MetricTimeSpan>
	{
		private const int MicrosecondsInMillisecond = 1000;

		private const long TicksInMicrosecond = 10L;

		private readonly TimeSpan _timeSpan;

		public long TotalMicroseconds
		{
			get
			{
				TimeSpan timeSpan = _timeSpan;
				return timeSpan.Ticks / 10;
			}
		}

		public int Hours
		{
			get
			{
				TimeSpan timeSpan = _timeSpan;
				return timeSpan.Hours;
			}
		}

		public int Minutes
		{
			get
			{
				TimeSpan timeSpan = _timeSpan;
				return timeSpan.Minutes;
			}
		}

		public int Seconds
		{
			get
			{
				TimeSpan timeSpan = _timeSpan;
				return timeSpan.Seconds;
			}
		}

		public int Milliseconds
		{
			get
			{
				TimeSpan timeSpan = _timeSpan;
				return timeSpan.Milliseconds;
			}
		}

		public MetricTimeSpan()
			: this(0L)
		{
		}

		public MetricTimeSpan(long totalMicroseconds)
		{
			ThrowIfArgument.IsNegative("totalMicroseconds", totalMicroseconds, "Number of microseconds is negative.");
			_timeSpan = new TimeSpan(totalMicroseconds * 10);
		}

		public MetricTimeSpan(TimeSpan timeSpan)
		{
			_timeSpan = timeSpan;
		}

		public MetricTimeSpan(int hours, int minutes, int seconds)
			: this(hours, minutes, seconds, 0)
		{
		}

		public MetricTimeSpan(int hours, int minutes, int seconds, int milliseconds)
		{
			ThrowIfArgument.IsNegative("hours", hours, "Number of hours is negative.");
			ThrowIfArgument.IsNegative("minutes", minutes, "Number of minutes is negative.");
			ThrowIfArgument.IsNegative("seconds", seconds, "Number of seconds is negative.");
			ThrowIfArgument.IsNegative("milliseconds", milliseconds, "Number of milliseconds is negative.");
			_timeSpan = new TimeSpan(0, hours, minutes, seconds, milliseconds);
		}

		public double Divide(MetricTimeSpan timeSpan)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			TimeSpan timeSpan2 = timeSpan._timeSpan;
			if (timeSpan2.Ticks == 0L)
			{
				throw new DivideByZeroException("Dividing by zero time span.");
			}
			timeSpan2 = _timeSpan;
			double num = timeSpan2.Ticks;
			timeSpan2 = timeSpan._timeSpan;
			return num / (double)timeSpan2.Ticks;
		}

		public static bool TryParse(string input, out MetricTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse(input, (Parsing<MetricTimeSpan>)MetricTimeSpanParser.TryParse, out timeSpan);
		}

		public static MetricTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<MetricTimeSpan>(input, MetricTimeSpanParser.TryParse);
		}

		public static implicit operator MetricTimeSpan(TimeSpan timeSpan)
		{
			return new MetricTimeSpan(timeSpan);
		}

		public static implicit operator TimeSpan(MetricTimeSpan timeSpan)
		{
			return timeSpan._timeSpan;
		}

		public static bool operator ==(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			return timeSpan1?.Equals(timeSpan2) ?? ((object)timeSpan2 == null);
		}

		public static bool operator !=(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		public static MetricTimeSpan operator +(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new MetricTimeSpan(timeSpan1.TotalMicroseconds + timeSpan2.TotalMicroseconds);
		}

		public static MetricTimeSpan operator -(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1 < timeSpan2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new MetricTimeSpan(timeSpan1.TotalMicroseconds - timeSpan2.TotalMicroseconds);
		}

		public static bool operator <(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		public static bool operator >(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		public static bool operator <=(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		public static bool operator >=(MetricTimeSpan timeSpan1, MetricTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MetricTimeSpan);
		}

		public override int GetHashCode()
		{
			return TotalMicroseconds.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Hours}:{Minutes}:{Seconds}:{Milliseconds}";
		}

		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			MetricTimeSpan metricTimeSpan = timeSpan as MetricTimeSpan;
			if (!(metricTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + metricTimeSpan;
		}

		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			MetricTimeSpan metricTimeSpan = timeSpan as MetricTimeSpan;
			if (!(metricTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - metricTimeSpan;
		}

		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MetricTimeSpan(MathUtilities.RoundToLong((double)TotalMicroseconds * multiplier));
		}

		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new MetricTimeSpan(MathUtilities.RoundToLong((double)TotalMicroseconds / divisor));
		}

		public ITimeSpan Clone()
		{
			return new MetricTimeSpan(_timeSpan);
		}

		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is MetricTimeSpan other2))
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return CompareTo(other2);
		}

		public int CompareTo(MetricTimeSpan other)
		{
			if ((object)other == null)
			{
				return 1;
			}
			TimeSpan timeSpan = _timeSpan;
			return timeSpan.CompareTo(other._timeSpan);
		}

		public bool Equals(MetricTimeSpan other)
		{
			if ((object)this == other)
			{
				return true;
			}
			if ((object)other == null)
			{
				return false;
			}
			return _timeSpan == other._timeSpan;
		}
	}
	public sealed class MidiTimeSpan : ITimeSpan, IComparable, IComparable<MidiTimeSpan>, IEquatable<MidiTimeSpan>
	{
		public long TimeSpan { get; }

		public MidiTimeSpan()
			: this(0L)
		{
		}

		public MidiTimeSpan(long timeSpan)
		{
			ThrowIfLengthArgument.IsNegative("timeSpan", timeSpan);
			TimeSpan = timeSpan;
		}

		public double Divide(MidiTimeSpan timeSpan)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			if ((long)timeSpan == 0L)
			{
				throw new DivideByZeroException("Dividing by zero time span.");
			}
			return (double)TimeSpan / (double)(long)timeSpan;
		}

		public static bool TryParse(string input, out MidiTimeSpan timeSpan)
		{
			return MidiTimeSpanParser.TryParse(input, out timeSpan).Status == ParsingStatus.Parsed;
		}

		public static MidiTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<MidiTimeSpan>(input, MidiTimeSpanParser.TryParse);
		}

		public static explicit operator MidiTimeSpan(long timeSpan)
		{
			return new MidiTimeSpan(timeSpan);
		}

		public static implicit operator long(MidiTimeSpan timeSpan)
		{
			return timeSpan.TimeSpan;
		}

		public static bool operator ==(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			return timeSpan1?.Equals(timeSpan2) ?? ((object)timeSpan2 == null);
		}

		public static bool operator !=(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		public static MidiTimeSpan operator +(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return new MidiTimeSpan(timeSpan1.TimeSpan + timeSpan2.TimeSpan);
		}

		public static MidiTimeSpan operator -(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			if (timeSpan1.TimeSpan < timeSpan2.TimeSpan)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new MidiTimeSpan(timeSpan1.TimeSpan - timeSpan2.TimeSpan);
		}

		public static bool operator <(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan < timeSpan2.TimeSpan;
		}

		public static bool operator >(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan > timeSpan2.TimeSpan;
		}

		public static bool operator <=(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan <= timeSpan2.TimeSpan;
		}

		public static bool operator >=(MidiTimeSpan timeSpan1, MidiTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.TimeSpan >= timeSpan2.TimeSpan;
		}

		public override string ToString()
		{
			return TimeSpan.ToString();
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MidiTimeSpan);
		}

		public override int GetHashCode()
		{
			return TimeSpan.GetHashCode();
		}

		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			MidiTimeSpan midiTimeSpan = timeSpan as MidiTimeSpan;
			if (!(midiTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + midiTimeSpan;
		}

		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			MidiTimeSpan midiTimeSpan = timeSpan as MidiTimeSpan;
			if (!(midiTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - midiTimeSpan;
		}

		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MidiTimeSpan(MathUtilities.RoundToLong((double)TimeSpan * multiplier));
		}

		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new MidiTimeSpan(MathUtilities.RoundToLong((double)TimeSpan / divisor));
		}

		public ITimeSpan Clone()
		{
			return new MidiTimeSpan(TimeSpan);
		}

		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is MidiTimeSpan other2))
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return CompareTo(other2);
		}

		public int CompareTo(MidiTimeSpan other)
		{
			if ((object)other == null)
			{
				return 1;
			}
			return Math.Sign(TimeSpan - other.TimeSpan);
		}

		public bool Equals(MidiTimeSpan other)
		{
			if ((object)this == other)
			{
				return true;
			}
			if ((object)other == null)
			{
				return false;
			}
			return TimeSpan == other.TimeSpan;
		}
	}
	public sealed class MusicalTimeSpan : ITimeSpan, IComparable, IComparable<MusicalTimeSpan>, IEquatable<MusicalTimeSpan>
	{
		public static readonly MusicalTimeSpan Whole = new MusicalTimeSpan(1L);

		public static readonly MusicalTimeSpan Half = new MusicalTimeSpan(2L);

		public static readonly MusicalTimeSpan Quarter = new MusicalTimeSpan(4L);

		public static readonly MusicalTimeSpan Eighth = new MusicalTimeSpan(8L);

		public static readonly MusicalTimeSpan Sixteenth = new MusicalTimeSpan(16L);

		public static readonly MusicalTimeSpan ThirtySecond = new MusicalTimeSpan(32L);

		public static readonly MusicalTimeSpan SixtyFourth = new MusicalTimeSpan(64L);

		private const long ZeroTimeSpanNumerator = 0L;

		private const long ZeroTimeSpanDenominator = 1L;

		private const long FractionNumerator = 1L;

		private const int WholeFraction = 1;

		private const int HalfFraction = 2;

		private const int QuarterFraction = 4;

		private const int EighthFraction = 8;

		private const int SixteenthFraction = 16;

		private const int ThirtySecondFraction = 32;

		private const int SixtyFourthFraction = 64;

		private const int TripletNotesCount = 3;

		private const int TripletSpaceSize = 2;

		private const int DupletNotesCount = 2;

		private const int DupletSpaceSize = 3;

		private const int SingleDotCount = 1;

		private const int DoubleDotCount = 2;

		private const int NumberOfDigitsAfterDecimalPoint = 3;

		private static readonly int FractionPartMultiplier = (int)Math.Pow(10.0, 3.0);

		public long Numerator { get; }

		public long Denominator { get; }

		public MusicalTimeSpan()
			: this(0L, 1L)
		{
		}

		public MusicalTimeSpan(long fraction)
			: this(1L, fraction)
		{
		}

		public MusicalTimeSpan(long numerator, long denominator, bool simplify = true)
		{
			ThrowIfArgument.IsNegative("numerator", numerator, "Numerator is negative.");
			ThrowIfArgument.IsNonpositive("denominator", denominator, "Denominator is zero or negative.");
			long num = (simplify ? MathUtilities.GreatestCommonDivisor(numerator, denominator) : 1);
			Numerator = numerator / num;
			Denominator = denominator / num;
		}

		public MusicalTimeSpan Dotted(int dotsCount)
		{
			ThrowIfArgument.IsNegative("dotsCount", dotsCount, "Dots count is negative.");
			return new MusicalTimeSpan(Numerator * ((1 << dotsCount + 1) - 1), Denominator * (1 << dotsCount));
		}

		public MusicalTimeSpan SingleDotted()
		{
			return Dotted(1);
		}

		public MusicalTimeSpan DoubleDotted()
		{
			return Dotted(2);
		}

		public MusicalTimeSpan Tuplet(int tupletNotesCount, int tupletSpaceSize)
		{
			ThrowIfArgument.IsNonpositive("tupletNotesCount", tupletNotesCount, "Tuplet's notes count is zero or negative.");
			ThrowIfArgument.IsNonpositive("tupletSpaceSize", tupletSpaceSize, "Tuplet's space size is zero or negative.");
			return new MusicalTimeSpan(Numerator * tupletSpaceSize, Denominator * tupletNotesCount);
		}

		public MusicalTimeSpan Triplet()
		{
			return Tuplet(3, 2);
		}

		public MusicalTimeSpan Duplet()
		{
			return Tuplet(2, 3);
		}

		public double Divide(MusicalTimeSpan timeSpan)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			if (timeSpan.Numerator == 0L)
			{
				throw new DivideByZeroException("Dividing by zero time span.");
			}
			return (double)Numerator * (double)timeSpan.Denominator / (double)(Denominator * timeSpan.Numerator);
		}

		public MusicalTimeSpan ChangeDenominator(long denominator)
		{
			ThrowIfArgument.IsNonpositive("denominator", denominator, "Denominator is zero or negative.");
			return new MusicalTimeSpan(MathUtilities.RoundToLong((double)denominator / (double)Denominator * (double)Numerator), denominator, simplify: false);
		}

		public static bool TryParse(string input, out MusicalTimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse(input, (Parsing<MusicalTimeSpan>)MusicalTimeSpanParser.TryParse, out timeSpan);
		}

		public static MusicalTimeSpan Parse(string input)
		{
			return ParsingUtilities.Parse<MusicalTimeSpan>(input, MusicalTimeSpanParser.TryParse);
		}

		private static void ReduceToCommonDenominator(MusicalTimeSpan fraction1, MusicalTimeSpan fraction2, out long numerator1, out long numerator2, out long denominator)
		{
			denominator = MathUtilities.LeastCommonMultiple(fraction1.Denominator, fraction2.Denominator);
			numerator1 = fraction1.Numerator * denominator / fraction1.Denominator;
			numerator2 = fraction2.Numerator * denominator / fraction2.Denominator;
		}

		public static bool operator ==(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			return timeSpan1?.Equals(timeSpan2) ?? ((object)timeSpan2 == null);
		}

		public static bool operator !=(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			return !(timeSpan1 == timeSpan2);
		}

		public static MusicalTimeSpan operator *(MusicalTimeSpan timeSpan, long number)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsNegative("number", number, "Number is negative.");
			return new MusicalTimeSpan(timeSpan.Numerator * number, timeSpan.Denominator);
		}

		public static MusicalTimeSpan operator *(long number, MusicalTimeSpan timeSpan)
		{
			return timeSpan * number;
		}

		public static MusicalTimeSpan operator /(MusicalTimeSpan timeSpan, long number)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsNonpositive("number", number, "Number is zero or negative.");
			return new MusicalTimeSpan(timeSpan.Numerator, timeSpan.Denominator * number);
		}

		public static MusicalTimeSpan operator +(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			ReduceToCommonDenominator(timeSpan1, timeSpan2, out var numerator, out var numerator2, out var denominator);
			return new MusicalTimeSpan(numerator + numerator2, denominator);
		}

		public static MusicalTimeSpan operator -(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			ReduceToCommonDenominator(timeSpan1, timeSpan2, out var numerator, out var numerator2, out var denominator);
			if (numerator < numerator2)
			{
				throw new ArgumentException("First time span is less than second one.", "timeSpan1");
			}
			return new MusicalTimeSpan(numerator - numerator2, denominator);
		}

		public static bool operator <(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) < 0;
		}

		public static bool operator >(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) > 0;
		}

		public static bool operator <=(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) <= 0;
		}

		public static bool operator >=(MusicalTimeSpan timeSpan1, MusicalTimeSpan timeSpan2)
		{
			ThrowIfArgument.IsNull("timeSpan1", timeSpan1);
			ThrowIfArgument.IsNull("timeSpan2", timeSpan2);
			return timeSpan1.CompareTo(timeSpan2) >= 0;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as MusicalTimeSpan);
		}

		public override int GetHashCode()
		{
			return (17 * 23 + Numerator.GetHashCode()) * 23 + Denominator.GetHashCode();
		}

		public override string ToString()
		{
			return $"{Numerator}/{Denominator}";
		}

		public ITimeSpan Add(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			MusicalTimeSpan musicalTimeSpan = timeSpan as MusicalTimeSpan;
			if (!(musicalTimeSpan != null))
			{
				return TimeSpanUtilities.Add(this, timeSpan, mode);
			}
			return this + musicalTimeSpan;
		}

		public ITimeSpan Subtract(ITimeSpan timeSpan, TimeSpanMode mode)
		{
			ThrowIfArgument.IsNull("timeSpan", timeSpan);
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			MusicalTimeSpan musicalTimeSpan = timeSpan as MusicalTimeSpan;
			if (!(musicalTimeSpan != null))
			{
				return TimeSpanUtilities.Subtract(this, timeSpan, mode);
			}
			return this - musicalTimeSpan;
		}

		public ITimeSpan Multiply(double multiplier)
		{
			ThrowIfArgument.IsNegative("multiplier", multiplier, "Multiplier is negative.");
			return new MusicalTimeSpan(MathUtilities.RoundToLong((double)Numerator * MathUtilities.Round(multiplier, 3) * (double)FractionPartMultiplier), Denominator * FractionPartMultiplier);
		}

		public ITimeSpan Divide(double divisor)
		{
			ThrowIfArgument.IsNonpositive("divisor", divisor, "Divisor is zero or negative.");
			return new MusicalTimeSpan(Numerator * FractionPartMultiplier, MathUtilities.RoundToLong((double)Denominator * MathUtilities.Round(divisor, 3) * (double)FractionPartMultiplier));
		}

		public ITimeSpan Clone()
		{
			return new MusicalTimeSpan(Numerator, Denominator);
		}

		public int CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is MusicalTimeSpan other2))
			{
				throw new ArgumentException("Time span is of different type.", "other");
			}
			return CompareTo(other2);
		}

		public int CompareTo(MusicalTimeSpan other)
		{
			if ((object)other == null)
			{
				return 1;
			}
			return Math.Sign(((double)Numerator * (double)other.Denominator - (double)other.Numerator * (double)Denominator) / ((double)Denominator * (double)other.Denominator));
		}

		public bool Equals(MusicalTimeSpan other)
		{
			if ((object)this == other)
			{
				return true;
			}
			if ((object)other == null)
			{
				return false;
			}
			ReduceToCommonDenominator(this, other, out var numerator, out var numerator2, out var _);
			return numerator == numerator2;
		}
	}
	internal sealed class MetricTempoMapValuesCache : ITempoMapValuesCache
	{
		internal sealed class AccumulatedMicroseconds
		{
			public long Time { get; }

			public double Microseconds { get; }

			public double MicrosecondsPerTick { get; }

			public double TicksPerMicrosecond { get; }

			public AccumulatedMicroseconds(long time, double microseconds, double microsecondsPerTick)
			{
				Time = time;
				Microseconds = microseconds;
				MicrosecondsPerTick = microsecondsPerTick;
				TicksPerMicrosecond = 1.0 / microsecondsPerTick;
			}
		}

		public AccumulatedMicroseconds[] Microseconds { get; private set; }

		public double DefaultMicrosecondsPerTick { get; private set; }

		public double DefaultTicksPerMicrosecond { get; private set; }

		public IEnumerable<TempoMapLine> InvalidateOnLines { get; } = new TempoMapLine[1];

		private static double GetMicroseconds(long time, Tempo tempo, short ticksPerQuarterNote)
		{
			return (double)(time * tempo.MicrosecondsPerQuarterNote) / (double)ticksPerQuarterNote;
		}

		public void Invalidate(TempoMap tempoMap)
		{
			List<AccumulatedMicroseconds> list = new List<AccumulatedMicroseconds>();
			short ticksPerQuarterNote = ((TicksPerQuarterNoteTimeDivision)tempoMap.TimeDivision).TicksPerQuarterNote;
			double num = 0.0;
			long num2 = 0L;
			Tempo tempo = Tempo.Default;
			foreach (ValueChange<Tempo> item in tempoMap.TempoLine)
			{
				long time = item.Time;
				num += GetMicroseconds(time - num2, tempo, ticksPerQuarterNote);
				tempo = item.Value;
				num2 = time;
				list.Add(new AccumulatedMicroseconds(time, num, (double)tempo.MicrosecondsPerQuarterNote / (double)ticksPerQuarterNote));
			}
			Microseconds = list.ToArray();
			DefaultMicrosecondsPerTick = (double)Tempo.Default.MicrosecondsPerQuarterNote / (double)ticksPerQuarterNote;
			DefaultTicksPerMicrosecond = 1.0 / DefaultMicrosecondsPerTick;
		}
	}
	public sealed class TimeSpanComparer : IComparer<ITimeSpan>
	{
		public int Compare(ITimeSpan x, ITimeSpan y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return x.CompareTo(y);
		}
	}
	public enum TimeSpanMode
	{
		TimeTime,
		TimeLength,
		LengthLength
	}
	public enum TimeSpanType
	{
		Metric,
		Musical,
		BarBeatTicks,
		BarBeatFraction,
		Midi
	}
	public static class TimeSpanUtilities
	{
		private static readonly Dictionary<TimeSpanType, Parsing<ITimeSpan>> Parsers = new Dictionary<TimeSpanType, Parsing<ITimeSpan>>
		{
			[TimeSpanType.Midi] = GetParsing<MidiTimeSpan>(MidiTimeSpanParser.TryParse),
			[TimeSpanType.BarBeatTicks] = GetParsing<BarBeatTicksTimeSpan>(BarBeatTicksTimeSpanParser.TryParse),
			[TimeSpanType.BarBeatFraction] = GetParsing<BarBeatFractionTimeSpan>(BarBeatFractionTimeSpanParser.TryParse),
			[TimeSpanType.Metric] = GetParsing<MetricTimeSpan>(MetricTimeSpanParser.TryParse),
			[TimeSpanType.Musical] = GetParsing<MusicalTimeSpan>(MusicalTimeSpanParser.TryParse)
		};

		private static readonly Dictionary<TimeSpanType, ITimeSpan> MaximumTimeSpans = new Dictionary<TimeSpanType, ITimeSpan>
		{
			[TimeSpanType.Midi] = new MidiTimeSpan(long.MaxValue),
			[TimeSpanType.Metric] = new MetricTimeSpan(TimeSpan.MaxValue),
			[TimeSpanType.Musical] = new MusicalTimeSpan(long.MaxValue, 1L),
			[TimeSpanType.BarBeatTicks] = new BarBeatTicksTimeSpan(long.MaxValue, long.MaxValue, long.MaxValue),
			[TimeSpanType.BarBeatFraction] = new BarBeatFractionTimeSpan(long.MaxValue, double.MaxValue)
		};

		private static readonly Dictionary<TimeSpanType, ITimeSpan> ZeroTimeSpans = new Dictionary<TimeSpanType, ITimeSpan>
		{
			[TimeSpanType.Midi] = new MidiTimeSpan(),
			[TimeSpanType.Metric] = new MetricTimeSpan(),
			[TimeSpanType.Musical] = new MusicalTimeSpan(),
			[TimeSpanType.BarBeatTicks] = new BarBeatTicksTimeSpan(),
			[TimeSpanType.BarBeatFraction] = new BarBeatFractionTimeSpan()
		};

		public static bool TryParse(string input, out ITimeSpan timeSpan)
		{
			timeSpan = null;
			foreach (Parsing<ITimeSpan> value in Parsers.Values)
			{
				if (ParsingUtilities.TryParse(input, value, out timeSpan))
				{
					return true;
				}
			}
			return false;
		}

		public static bool TryParse(string input, TimeSpanType timeSpanType, out ITimeSpan timeSpan)
		{
			return ParsingUtilities.TryParse(input, Parsers[timeSpanType], out timeSpan);
		}

		public static ITimeSpan Parse(string input)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("input", input, "Input string");
			foreach (Parsing<ITimeSpan> value in Parsers.Values)
			{
				ITimeSpan result;
				ParsingResult parsingResult = value(input, out result);
				if (parsingResult.Status == ParsingStatus.Parsed)
				{
					return result;
				}
				if (parsingResult.Status == ParsingStatus.FormatError)
				{
					throw parsingResult.Exception;
				}
			}
			throw new FormatException("Time span has unknown format.");
		}

		public static ITimeSpan GetMaxTimeSpan(TimeSpanType timeSpanType)
		{
			ThrowIfArgument.IsInvalidEnumValue("timeSpanType", timeSpanType);
			return MaximumTimeSpans[timeSpanType];
		}

		public static ITimeSpan GetZeroTimeSpan(TimeSpanType timeSpanType)
		{
			ThrowIfArgument.IsInvalidEnumValue("timeSpanType", timeSpanType);
			return ZeroTimeSpans[timeSpanType];
		}

		public static TTimeSpan GetZeroTimeSpan<TTimeSpan>() where TTimeSpan : ITimeSpan
		{
			return (TTimeSpan)ZeroTimeSpans.Values.FirstOrDefault((ITimeSpan timeSpan) => timeSpan is TTimeSpan);
		}

		internal static double Divide(ITimeSpan timeSpan1, ITimeSpan timeSpan2)
		{
			MetricTimeSpan metricTimeSpan = timeSpan1 as MetricTimeSpan;
			if (metricTimeSpan != null)
			{
				return metricTimeSpan.Divide(timeSpan2 as MetricTimeSpan);
			}
			MidiTimeSpan midiTimeSpan = timeSpan1 as MidiTimeSpan;
			if (midiTimeSpan != null)
			{
				return midiTimeSpan.Divide(timeSpan2 as MidiTimeSpan);
			}
			MusicalTimeSpan musicalTimeSpan = timeSpan1 as MusicalTimeSpan;
			if (musicalTimeSpan != null)
			{
				return musicalTimeSpan.Divide(timeSpan2 as MusicalTimeSpan);
			}
			throw new NotSupportedException($"Dividing of time span of the '{timeSpan1.GetType()}' type is not supported.");
		}

		internal static ITimeSpan Add(ITimeSpan timeSpan1, ITimeSpan timeSpan2, TimeSpanMode mode)
		{
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			if (mode == TimeSpanMode.TimeTime)
			{
				throw new ArgumentException("Times cannot be added.", "mode");
			}
			return new MathTimeSpan(timeSpan1, timeSpan2, MathOperation.Add, mode);
		}

		internal static ITimeSpan Subtract(ITimeSpan timeSpan1, ITimeSpan timeSpan2, TimeSpanMode mode)
		{
			ThrowIfArgument.IsInvalidEnumValue("mode", mode);
			return new MathTimeSpan(timeSpan1, timeSpan2, MathOperation.Subtract, mode);
		}

		private static Parsing<ITimeSpan> GetParsing<TTimeSpan>(Parsing<TTimeSpan> parsing) where TTimeSpan : ITimeSpan
		{
			return delegate(string input, out ITimeSpan timeSpan)
			{
				TTimeSpan result2;
				ParsingResult result = parsing(input, out result2);
				timeSpan = result2;
				return result;
			};
		}
	}
	public static class BarBeatUtilities
	{
		public static int GetBarLength(long bars, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			Tuple<TimeSignature, short> timeSignatureAndTicksPerQuarterNote = GetTimeSignatureAndTicksPerQuarterNote(bars, tempoMap);
			return GetBarLength(timeSignatureAndTicksPerQuarterNote.Item1, timeSignatureAndTicksPerQuarterNote.Item2);
		}

		public static int GetBeatLength(long bars, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNegative("bars", bars, "Bars number is negative.");
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			Tuple<TimeSignature, short> timeSignatureAndTicksPerQuarterNote = GetTimeSignatureAndTicksPerQuarterNote(bars, tempoMap);
			return GetBeatLength(timeSignatureAndTicksPerQuarterNote.Item1, timeSignatureAndTicksPerQuarterNote.Item2);
		}

		internal static int GetBarLength(TimeSignature timeSignature, short ticksPerQuarterNote)
		{
			int beatLength = GetBeatLength(timeSignature, ticksPerQuarterNote);
			return timeSignature.Numerator * beatLength;
		}

		internal static int GetBeatLength(TimeSignature timeSignature, short ticksPerQuarterNote)
		{
			return 4 * ticksPerQuarterNote / timeSignature.Denominator;
		}

		private static Tuple<TimeSignature, short> GetTimeSignatureAndTicksPerQuarterNote(long bars, TempoMap tempoMap)
		{
			TicksPerQuarterNoteTimeDivision ticksPerQuarterNoteTimeDivision = tempoMap.TimeDivision as TicksPerQuarterNoteTimeDivision;
			if (ticksPerQuarterNoteTimeDivision == null)
			{
				throw new ArgumentException("Time division of the tempo map is not supported.", "tempoMap");
			}
			long time = TimeConverter.ConvertFrom(new BarBeatTicksTimeSpan(bars), tempoMap);
			TimeSignature valueAtTime = tempoMap.TimeSignatureLine.GetValueAtTime(time);
			short ticksPerQuarterNote = ticksPerQuarterNoteTimeDivision.TicksPerQuarterNote;
			return Tuple.Create(valueAtTime, ticksPerQuarterNote);
		}
	}
	public static class MidiFileUtilities
	{
		public static TTimeSpan GetDuration<TTimeSpan>(this MidiFile midiFile) where TTimeSpan : class, ITimeSpan
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			TempoMap tempoMap = midiFile.GetTempoMap();
			TimedEvent timedEvent = (from e in midiFile.GetTrackChunks().GetTimedEventsLazy(cloneEvent: false)
				select e.Item1).LastOrDefault();
			return ((timedEvent != null) ? timedEvent.TimeAs<TTimeSpan>(tempoMap) : null) ?? TimeSpanUtilities.GetZeroTimeSpan<TTimeSpan>();
		}

		public static ITimeSpan GetDuration(this MidiFile midiFile, TimeSpanType durationType)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsInvalidEnumValue("durationType", durationType);
			TempoMap tempoMap = midiFile.GetTempoMap();
			return (from e in midiFile.GetTrackChunks().GetTimedEventsLazy(cloneEvent: false)
				select e.Item1).LastOrDefault()?.TimeAs(durationType, tempoMap) ?? TimeSpanUtilities.GetZeroTimeSpan(durationType);
		}

		public static bool IsEmpty(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return !midiFile.GetEvents().Any();
		}

		public static void ShiftEvents(this MidiFile midiFile, ITimeSpan distance)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("distance", distance);
			midiFile.GetTrackChunks().ShiftEvents(distance, midiFile.GetTempoMap());
		}

		public static void Resize(this MidiFile midiFile, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNull("length", length);
			if (!midiFile.IsEmpty())
			{
				TempoMap tempoMap = midiFile.GetTempoMap();
				ITimeSpan timeSpan = TimeConverter.ConvertTo(midiFile.GetDuration<MidiTimeSpan>(), length.GetType(), tempoMap);
				double ratio = TimeSpanUtilities.Divide(length, timeSpan);
				ResizeByRatio(midiFile, ratio);
			}
		}

		public static void Resize(this MidiFile midiFile, double ratio)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			ThrowIfArgument.IsNegative("ratio", ratio, "Ratio is negative");
			ResizeByRatio(midiFile, ratio);
		}

		private static void ResizeByRatio(MidiFile midiFile, double ratio)
		{
			foreach (TrackChunk trackChunk in midiFile.GetTrackChunks())
			{
				trackChunk.ProcessTimedEvents(delegate(TimedEvent timedEvent)
				{
					timedEvent.Time = MathUtilities.RoundToLong((double)timedEvent.Time * ratio);
				});
			}
		}
	}
	public sealed class NoteId
	{
		public FourBitNumber Channel { get; }

		public SevenBitNumber NoteNumber { get; }

		public NoteId(FourBitNumber channel, SevenBitNumber noteNumber)
		{
			Channel = channel;
			NoteNumber = noteNumber;
		}

		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			if (!(obj is NoteId noteId))
			{
				return false;
			}
			if ((byte)Channel == (byte)noteId.Channel)
			{
				return (byte)NoteNumber == (byte)noteId.NoteNumber;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (byte)Channel * 1000 + (byte)NoteNumber;
		}
	}
	public static class NoteIdUtilities
	{
		public static NoteId GetNoteId(this Note note)
		{
			ThrowIfArgument.IsNull("note", note);
			return new NoteId(note.Channel, note.NoteNumber);
		}

		public static NoteId GetNoteId(this NoteEvent noteEvent)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			return new NoteId(noteEvent.Channel, noteEvent.NoteNumber);
		}
	}
	internal static class ThrowIfLengthArgument
	{
		internal static void IsNegative(string parameterName, long length)
		{
			ThrowIfArgument.IsNegative(parameterName, length, "Length is negative.");
		}
	}
	internal static class ThrowIfNotesTolerance
	{
		internal static void IsNegative(string parameterName, long notesTolerance)
		{
			ThrowIfArgument.IsNegative(parameterName, notesTolerance, "Notes tolerance is negative.");
		}
	}
	internal static class ThrowIfTimeArgument
	{
		internal static void IsNegative(string parameterName, long time)
		{
			ThrowIfArgument.IsNegative(parameterName, time, "Time is negative.");
		}

		internal static void StartIsNegative(string parameterName, long time)
		{
			ThrowIfArgument.IsNegative(parameterName, time, "Start time is negative.");
		}

		internal static void EndIsNegative(string parameterName, long time)
		{
			ThrowIfArgument.IsNegative(parameterName, time, "End time is negative.");
		}
	}
	public static class TrackChunkUtilities
	{
		public static void ShiftEvents(this TrackChunk trackChunk, ITimeSpan distance, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			long num = TimeConverter.ConvertFrom(distance, TempoMap.Create(tempoMap.TimeDivision));
			MidiEvent midiEvent = trackChunk.Events.FirstOrDefault();
			if (midiEvent != null)
			{
				midiEvent.DeltaTime += num;
			}
		}

		public static void ShiftEvents(this IEnumerable<TrackChunk> trackChunks, ITimeSpan distance, TempoMap tempoMap)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			ThrowIfArgument.IsNull("distance", distance);
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			foreach (TrackChunk trackChunk in trackChunks)
			{
				trackChunk.ShiftEvents(distance, tempoMap);
			}
		}
	}
	public sealed class ValueChange<TValue> : ITimedObject
	{
		private readonly long _time;

		public long Time
		{
			get
			{
				return _time;
			}
			set
			{
				throw new InvalidOperationException("Setting time of value change object is not allowed.");
			}
		}

		public TValue Value { get; }

		internal ValueChange(long time, TValue value)
		{
			ThrowIfTimeArgument.IsNegative("time", time);
			ThrowIfArgument.IsNull("value", value);
			_time = time;
			Value = value;
		}

		public static bool operator ==(ValueChange<TValue> change1, ValueChange<TValue> change2)
		{
			if ((object)change1 == change2)
			{
				return true;
			}
			if ((object)change1 == null || (object)change2 == null)
			{
				return false;
			}
			if (change1.Time == change2.Time)
			{
				return change1.Value.Equals(change2.Value);
			}
			return false;
		}

		public static bool operator !=(ValueChange<TValue> change1, ValueChange<TValue> change2)
		{
			return !(change1 == change2);
		}

		public override string ToString()
		{
			return $"{Value} at {Time}";
		}

		public override bool Equals(object obj)
		{
			return this == obj as ValueChange<TValue>;
		}

		public override int GetHashCode()
		{
			return (17 * 23 + Time.GetHashCode()) * 23 + Value.GetHashCode();
		}
	}
	public sealed class ValueLine<TValue> : IEnumerable<ValueChange<TValue>>, IEnumerable
	{
		private readonly TimedObjectsComparer<ValueChange<TValue>> _comparer = new TimedObjectsComparer<ValueChange<TValue>>();

		private readonly List<ValueChange<TValue>> _valueChanges = new List<ValueChange<TValue>>();

		private readonly TValue _defaultValue;

		private bool _valuesChanged = true;

		private long _maxTime = long.MinValue;

		internal event EventHandler ValuesChanged;

		internal ValueLine(TValue defaultValue)
		{
			_defaultValue = defaultValue;
		}

		internal TValue GetValueAtTime(long time)
		{
			ValueChange<TValue> valueChangeAtTime = GetValueChangeAtTime(time);
			if (!(valueChangeAtTime != null))
			{
				return _defaultValue;
			}
			return valueChangeAtTime.Value;
		}

		internal ValueChange<TValue> GetValueChangeAtTime(long time)
		{
			SortValueChanges();
			ValueChange<TValue> result = null;
			int count = _valueChanges.Count;
			for (int i = 0; i < count; i++)
			{
				ValueChange<TValue> valueChange = _valueChanges[i];
				if (valueChange.Time > time)
				{
					break;
				}
				result = valueChange;
			}
			return result;
		}

		internal void SetValue(long time, TValue value)
		{
			if (GetValueAtTime(time).Equals(value))
			{
				return;
			}
			int num = -1;
			for (int num2 = _valueChanges.Count - 1; num2 >= 0; num2--)
			{
				if (_valueChanges[num2].Time == time)
				{
					num = num2;
					break;
				}
			}
			if (num >= 0)
			{
				_valueChanges.RemoveAt(num);
			}
			_valueChanges.Add(new ValueChange<TValue>(time, value));
			bool forceSort = time < _maxTime;
			if (time > _maxTime)
			{
				_maxTime = time;
			}
			OnValuesChanged(forceSort);
		}

		internal void DeleteValues(long startTime)
		{
			DeleteValues(startTime, long.MaxValue);
		}

		internal void DeleteValues(long startTime, long endTime)
		{
			_valueChanges.RemoveAll((ValueChange<TValue> v) => v.Time >= startTime && v.Time <= endTime);
			OnValuesChanged();
		}

		internal void Clear()
		{
			_valueChanges.Clear();
			OnValuesChanged();
		}

		internal void ReplaceValues(ValueLine<TValue> valueLine)
		{
			_valueChanges.Clear();
			_valueChanges.AddRange(valueLine._valueChanges);
			OnValuesChanged();
		}

		internal ValueLine<TValue> Reverse(long centerTime)
		{
			long maxTime = 2 * centerTime;
			IEnumerable<ValueChange<TValue>> source = this.TakeWhile((ValueChange<TValue> c) => c.Time <= maxTime);
			IEnumerable<TValue> first = new TValue[1] { _defaultValue }.Concat(source.Select((ValueChange<TValue> c) => c.Value)).Reverse();
			IEnumerable<long> second = new long[1].Concat(source.Select((ValueChange<TValue> c) => maxTime - c.Time).Reverse());
			ValueLine<TValue> valueLine = new ValueLine<TValue>(_defaultValue);
			valueLine._valueChanges.AddRange(first.Zip(second, (TValue v, long t) => new ValueChange<TValue>(t, v)));
			return valueLine;
		}

		private void OnValuesChanged(bool forceSort = true)
		{
			if (forceSort)
			{
				OnValueChangesNeedSorting();
			}
			this.ValuesChanged?.Invoke(this, EventArgs.Empty);
		}

		private void OnValueChangesNeedSorting()
		{
			_valuesChanged = true;
		}

		private void OnValueChangesSortingCompleted()
		{
			_valuesChanged = false;
		}

		private void SortValueChanges()
		{
			if (_valuesChanged)
			{
				_valueChanges.Sort(_comparer);
				OnValueChangesSortingCompleted();
			}
		}

		public IEnumerator<ValueChange<TValue>> GetEnumerator()
		{
			SortValueChanges();
			return _valueChanges.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
namespace Melanchall.DryWetMidi.Core
{
	internal static class ChunksConverterFactory
	{
		public static IChunksConverter GetConverter(MidiFileFormat format)
		{
			ThrowIfArgument.IsInvalidEnumValue("format", format);
			return format switch
			{
				MidiFileFormat.SingleTrack => new SingleTrackChunksConverter(), 
				MidiFileFormat.MultiTrack => new MultiTrackChunksConverter(), 
				MidiFileFormat.MultiSequence => new MultiSequenceChunksConverter(), 
				_ => throw new NotSupportedException($"Converter for the {format} format is not supported."), 
			};
		}
	}
	internal interface IChunksConverter
	{
		IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks);
	}
	internal sealed class MultiSequenceChunksConverter : IChunksConverter
	{
		public IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			TrackChunk[] array = chunks.OfType<TrackChunk>().ToArray();
			if (array.Length == 0)
			{
				return chunks;
			}
			var source = array.Select((TrackChunk c, int i) => new
			{
				Chunk = c,
				Number = (((int?)GetSequenceNumber(c)) ?? i)
			}).ToArray();
			IChunksConverter singleTrackChunksConverter = ChunksConverterFactory.GetConverter(MidiFileFormat.SingleTrack);
			return (from n in source
				group n by n.Number).SelectMany(g => singleTrackChunksConverter.Convert(g.Select(n => n.Chunk))).Concat(chunks.Where((MidiChunk c) => !(c is TrackChunk)));
		}

		private static ushort? GetSequenceNumber(TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return trackChunk.Events.TakeWhile((MidiEvent m) => m.DeltaTime == 0).OfType<SequenceNumberEvent>().FirstOrDefault()?.Number;
		}
	}
	internal sealed class MultiTrackChunksConverter : IChunksConverter
	{
		private sealed class TrackChunkDescriptor
		{
			public TrackChunk Chunk { get; } = new TrackChunk();

			public long DeltaTime { get; set; }

			public void AddEvent(MidiEvent midiEvent)
			{
				midiEvent.DeltaTime = DeltaTime;
				Chunk.Events.Add(midiEvent);
				DeltaTime = 0L;
			}
		}

		private const int ChannelsCount = 16;

		public IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			TrackChunk[] array = chunks.OfType<TrackChunk>().ToArray();
			if (array.Length != 1)
			{
				return chunks;
			}
			TrackChunkDescriptor[] array2 = (from i in Enumerable.Range(0, 17)
				select new TrackChunkDescriptor()).ToArray();
			FourBitNumber? fourBitNumber = null;
			foreach (MidiEvent midiEvent in array.First().Events.Select((MidiEvent m) => m.Clone()))
			{
				Array.ForEach(array2, delegate(TrackChunkDescriptor d)
				{
					d.DeltaTime += midiEvent.DeltaTime;
				});
				if (midiEvent is ChannelEvent channelEvent)
				{
					array2[(byte)channelEvent.Channel + 1].AddEvent(midiEvent.Clone());
					fourBitNumber = null;
					continue;
				}
				if (!(midiEvent is MetaEvent))
				{
					fourBitNumber = null;
				}
				if (midiEvent is ChannelPrefixEvent channelPrefixEvent)
				{
					fourBitNumber = (FourBitNumber)channelPrefixEvent.Channel;
				}
				if (fourBitNumber.HasValue)
				{
					array2[(byte)fourBitNumber.Value + 1].AddEvent(midiEvent);
				}
				else
				{
					array2[0].AddEvent(midiEvent);
				}
			}
			return (from d in array2
				select d.Chunk into c
				where c.Events.Any()
				select c).Concat(chunks.Where((MidiChunk c) => !(c is TrackChunk)));
		}
	}
	internal sealed class SingleTrackChunksConverter : IChunksConverter
	{
		private sealed class EventDescriptor
		{
			public MidiEvent Event { get; }

			public long AbsoluteTime { get; }

			public int Channel { get; }

			public EventDescriptor(MidiEvent midiEvent, long absoluteTime, int channel)
			{
				Event = midiEvent;
				AbsoluteTime = absoluteTime;
				Channel = channel;
			}
		}

		private sealed class EventDescriptorComparer : IComparer<EventDescriptor>
		{
			public int Compare(EventDescriptor x, EventDescriptor y)
			{
				long num = x.AbsoluteTime - y.AbsoluteTime;
				if (num != 0L)
				{
					return Math.Sign(num);
				}
				MetaEvent metaEvent = x.Event as MetaEvent;
				MetaEvent metaEvent2 = y.Event as MetaEvent;
				if (metaEvent != null && metaEvent2 == null)
				{
					return -1;
				}
				if (metaEvent == null && metaEvent2 != null)
				{
					return 1;
				}
				if (metaEvent == null)
				{
					return 0;
				}
				int num2 = x.Channel - y.Channel;
				if (num2 != 0)
				{
					return num2;
				}
				ChannelPrefixEvent channelPrefixEvent = x.Event as ChannelPrefixEvent;
				ChannelPrefixEvent channelPrefixEvent2 = y.Event as ChannelPrefixEvent;
				if (channelPrefixEvent != null && channelPrefixEvent2 == null)
				{
					return -1;
				}
				if (channelPrefixEvent == null && channelPrefixEvent2 != null)
				{
					return 1;
				}
				return 0;
			}
		}

		public IEnumerable<MidiChunk> Convert(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			TrackChunk[] array = chunks.OfType<TrackChunk>().ToArray();
			if (array.Length == 1)
			{
				return chunks;
			}
			IOrderedEnumerable<EventDescriptor> orderedEnumerable = array.SelectMany(delegate(TrackChunk trackChunk2)
			{
				long absoluteTime = 0L;
				int channel = -1;
				return trackChunk2.Events.Select(delegate(MidiEvent midiEvent2)
				{
					if (midiEvent2 is ChannelPrefixEvent channelPrefixEvent)
					{
						channel = channelPrefixEvent.Channel;
					}
					if (!(midiEvent2 is MetaEvent))
					{
						channel = -1;
					}
					return new EventDescriptor(midiEvent2, absoluteTime += midiEvent2.DeltaTime, channel);
				});
			}).OrderBy((EventDescriptor d) => d, new EventDescriptorComparer());
			TrackChunk trackChunk = new TrackChunk();
			long num = 0L;
			foreach (EventDescriptor item in orderedEnumerable)
			{
				MidiEvent midiEvent = item.Event.Clone();
				midiEvent.DeltaTime = item.AbsoluteTime - num;
				trackChunk.Events.Add(midiEvent);
				num = item.AbsoluteTime;
			}
			return new TrackChunk[1] { trackChunk }.Concat(chunks.Where((MidiChunk c) => !(c is TrackChunk)));
		}
	}
	internal sealed class HeaderChunk : MidiChunk
	{
		public const string Id = "MThd";

		public ushort FileFormat { get; set; }

		public TimeDivision TimeDivision { get; set; }

		public ushort TracksNumber { get; set; }

		internal HeaderChunk()
			: base("MThd")
		{
		}

		public override MidiChunk Clone()
		{
			throw new NotSupportedException("Cloning of a header chunk isnot supported.");
		}

		public override string ToString()
		{
			return $"Header chunk (file format = {FileFormat}, time division = {TimeDivision}, tracks number = {TracksNumber})";
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, uint size)
		{
			ushort num = reader.ReadWord();
			if (settings.UnknownFileFormatPolicy == UnknownFileFormatPolicy.Abort && !Enum.IsDefined(typeof(MidiFileFormat), num))
			{
				throw new UnknownFileFormatException(num);
			}
			FileFormat = num;
			TracksNumber = reader.ReadWord();
			TimeDivision = TimeDivisionFactory.GetTimeDivision(reader.ReadInt16());
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteWord(FileFormat);
			writer.WriteWord(TracksNumber);
			writer.WriteInt16(TimeDivision.ToInt16());
		}

		protected override uint GetContentSize(WritingSettings settings)
		{
			return 6u;
		}
	}
	public sealed class ChunkType
	{
		public Type Type { get; }

		public string Id { get; }

		public ChunkType(Type type, string id)
		{
			Type = type;
			Id = id;
		}
	}
	public sealed class ChunkTypesCollection : IEnumerable<ChunkType>, IEnumerable
	{
		private readonly Dictionary<Type, string> _ids = new Dictionary<Type, string>();

		private readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

		public void Add(Type type, string id)
		{
			_ids.Add(type, id);
			_types.Add(id, type);
		}

		public bool TryGetType(string id, out Type type)
		{
			return _types.TryGetValue(id, out type);
		}

		public bool TryGetId(Type type, out string id)
		{
			return _ids.TryGetValue(type, out id);
		}

		public IEnumerator<ChunkType> GetEnumerator()
		{
			return _ids.Select((KeyValuePair<Type, string> kv) => new ChunkType(kv.Key, kv.Value)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal static class StandardChunkIds
	{
		private static string[] _ids;

		public static string[] GetIds()
		{
			object obj = _ids;
			if (obj == null)
			{
				obj = new string[2] { "MThd", "MTrk" };
				_ids = (string[])obj;
			}
			return (string[])obj;
		}
	}
	public abstract class MidiChunk
	{
		public const int IdLength = 4;

		public string ChunkId { get; }

		protected MidiChunk(string id)
		{
			ThrowIfArgument.IsNull("id", id);
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("ID is empty string.", "id");
			}
			if (id.Length != 4)
			{
				throw new ArgumentException($"ID length doesn't equal {4}.", "id");
			}
			ChunkId = id;
		}

		public abstract MidiChunk Clone();

		public static string[] GetStandardChunkIds()
		{
			return StandardChunkIds.GetIds();
		}

		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2)
		{
			string message;
			return Equals(chunk1, chunk2, out message);
		}

		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2, out string message)
		{
			return Equals(chunk1, chunk2, null, out message);
		}

		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2, MidiChunkEqualityCheckSettings settings)
		{
			string message;
			return Equals(chunk1, chunk2, settings, out message);
		}

		public static bool Equals(MidiChunk chunk1, MidiChunk chunk2, MidiChunkEqualityCheckSettings settings, out string message)
		{
			return MidiChunkEquality.Equals(chunk1, chunk2, settings ?? new MidiChunkEqualityCheckSettings(), out message);
		}

		internal void Read(MidiReader reader, ReadingSettings settings)
		{
			uint num = reader.ReadDword();
			long position = reader.Position;
			ReadContent(reader, settings, num);
			long num2 = reader.Position - position;
			if (settings.InvalidChunkSizePolicy == InvalidChunkSizePolicy.Abort && num2 != num)
			{
				throw new InvalidChunkSizeException(ChunkId, num, num2);
			}
			long num3 = num - num2;
			if (num3 > 0)
			{
				reader.Position += Math.Min(num3, reader.Length);
			}
		}

		internal void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteString(ChunkId);
			uint contentSize = GetContentSize(settings);
			writer.WriteDword(contentSize);
			WriteContent(writer, settings);
		}

		protected abstract void ReadContent(MidiReader reader, ReadingSettings settings, uint size);

		protected abstract void WriteContent(MidiWriter writer, WritingSettings settings);

		protected abstract uint GetContentSize(WritingSettings settings);
	}
	public sealed class TrackChunk : MidiChunk
	{
		public const string Id = "MTrk";

		public EventsCollection Events { get; } = new EventsCollection();

		public TrackChunk()
			: base("MTrk")
		{
		}

		public TrackChunk(IEnumerable<MidiEvent> events)
			: this()
		{
			ThrowIfArgument.IsNull("events", events);
			Events.AddRange(events);
		}

		public TrackChunk(params MidiEvent[] events)
			: this()
		{
			Events.AddRange(events);
		}

		public override MidiChunk Clone()
		{
			return new TrackChunk(Events.Select((MidiEvent e) => e.Clone()));
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, uint size)
		{
			long num = reader.Position + size;
			bool flag = false;
			byte? channelEventStatusByte = null;
			while (reader.Position < num && !reader.EndReached)
			{
				long deltaTime;
				MidiEvent midiEvent = ReadEvent(reader, settings, ref channelEventStatusByte, out deltaTime);
				if (midiEvent == null)
				{
					continue;
				}
				if (midiEvent is EndOfTrackEvent)
				{
					flag = true;
					if (settings.EndOfTrackStoringPolicy == EndOfTrackStoringPolicy.Store)
					{
						Events.Add(midiEvent);
					}
					break;
				}
				Events.Add(midiEvent);
			}
			if (settings.MissedEndOfTrackPolicy == MissedEndOfTrackPolicy.Abort && !flag)
			{
				throw new MissedEndOfTrackEventException();
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			ProcessEvents(settings, delegate(IEventWriter eventWriter, MidiEvent midiEvent, bool writeStatusByte)
			{
				writer.WriteVlqNumber(midiEvent.DeltaTime);
				eventWriter.Write(midiEvent, writer, settings, writeStatusByte);
			});
		}

		protected override uint GetContentSize(WritingSettings settings)
		{
			uint result = 0u;
			ProcessEvents(settings, delegate(IEventWriter eventWriter, MidiEvent midiEvent, bool writeStatusByte)
			{
				result += (uint)(midiEvent.DeltaTime.GetVlqLength() + eventWriter.CalculateSize(midiEvent, settings, writeStatusByte));
			});
			return result;
		}

		public override string ToString()
		{
			return $"Track chunk ({Events.Count} events)";
		}

		private MidiEvent ReadEvent(MidiReader reader, ReadingSettings settings, ref byte? channelEventStatusByte, out long deltaTime)
		{
			deltaTime = reader.ReadVlqLongNumber();
			if (deltaTime < 0)
			{
				deltaTime = 0L;
			}
			byte b = reader.ReadByte();
			if (b <= (byte)SevenBitNumber.MaxValue)
			{
				if (!channelEventStatusByte.HasValue)
				{
					throw new UnexpectedRunningStatusException();
				}
				b = channelEventStatusByte.Value;
				reader.Position--;
			}
			MidiEvent midiEvent = EventReaderFactory.GetReader(b, smfOnly: true).Read(reader, settings, b);
			if (midiEvent is ChannelEvent)
			{
				channelEventStatusByte = b;
			}
			if (midiEvent != null)
			{
				midiEvent._deltaTime = deltaTime;
			}
			return midiEvent;
		}

		private void ProcessEvents(WritingSettings settings, Action<IEventWriter, MidiEvent, bool> eventHandler)
		{
			byte? b = null;
			bool skip = true;
			bool skip2 = true;
			bool skip3 = true;
			MidiEventType midiEventType = MidiEventType.NormalSysEx;
			foreach (MidiEvent @event in Events)
			{
				midiEventType = @event.EventType;
				MidiEvent midiEvent = @event;
				if (midiEvent is SystemCommonEvent || midiEvent is SystemRealTimeEvent || (midiEvent.EventType == MidiEventType.UnknownMeta && settings.DeleteUnknownMetaEvents))
				{
					continue;
				}
				if (settings.NoteOffAsSilentNoteOn && midiEvent is NoteOffEvent noteOffEvent)
				{
					midiEvent = new NoteOnEvent
					{
						DeltaTime = noteOffEvent.DeltaTime,
						Channel = noteOffEvent.Channel,
						NoteNumber = noteOffEvent.NoteNumber
					};
				}
				if ((!settings.DeleteDefaultSetTempo || !TrySkipDefaultSetTempo(midiEvent, ref skip)) && (!settings.DeleteDefaultKeySignature || !TrySkipDefaultKeySignature(midiEvent, ref skip2)) && (!settings.DeleteDefaultTimeSignature || !TrySkipDefaultTimeSignature(midiEvent, ref skip3)))
				{
					IEventWriter writer = EventWriterFactory.GetWriter(midiEvent);
					bool arg = true;
					if (midiEvent is ChannelEvent)
					{
						byte statusByte = writer.GetStatusByte(midiEvent);
						arg = b != statusByte || !settings.UseRunningStatus;
						b = statusByte;
					}
					else
					{
						b = null;
					}
					eventHandler(writer, midiEvent, arg);
				}
			}
			if (midiEventType != MidiEventType.EndOfTrack)
			{
				EndOfTrackEvent endOfTrackEvent = new EndOfTrackEvent();
				IEventWriter writer2 = EventWriterFactory.GetWriter(endOfTrackEvent);
				eventHandler(writer2, endOfTrackEvent, arg3: true);
			}
		}

		private static bool TrySkipDefaultSetTempo(MidiEvent midiEvent, ref bool skip)
		{
			if (skip && midiEvent is SetTempoEvent setTempoEvent)
			{
				if (setTempoEvent.MicrosecondsPerQuarterNote == 500000)
				{
					return true;
				}
				skip = false;
			}
			return false;
		}

		private static bool TrySkipDefaultKeySignature(MidiEvent midiEvent, ref bool skip)
		{
			if (skip && midiEvent is KeySignatureEvent keySignatureEvent)
			{
				if (keySignatureEvent.Key == 0 && keySignatureEvent.Scale == 0)
				{
					return true;
				}
				skip = false;
			}
			return false;
		}

		private static bool TrySkipDefaultTimeSignature(MidiEvent midiEvent, ref bool skip)
		{
			if (skip && midiEvent is TimeSignatureEvent timeSignatureEvent)
			{
				if (timeSignatureEvent.Numerator == 4 && timeSignatureEvent.Denominator == 4 && timeSignatureEvent.ClocksPerClick == 24 && timeSignatureEvent.ThirtySecondNotesPerBeat == 8)
				{
					return true;
				}
				skip = false;
			}
			return false;
		}
	}
	public sealed class UnknownChunk : MidiChunk
	{
		public byte[] Data { get; private set; }

		internal UnknownChunk(string id)
			: base(id)
		{
		}

		public override MidiChunk Clone()
		{
			return new UnknownChunk(base.ChunkId)
			{
				Data = (Data?.Clone() as byte[])
			};
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, uint size)
		{
			if (size == 0)
			{
				switch (settings.ZeroLengthDataPolicy)
				{
				case ZeroLengthDataPolicy.ReadAsEmptyObject:
					Data = new byte[0];
					break;
				case ZeroLengthDataPolicy.ReadAsNull:
					Data = null;
					break;
				}
			}
			else
			{
				long num = reader.Length - reader.Position;
				long val = ((num < size) ? num : size);
				byte[] array = reader.ReadBytes((int)Math.Min(val, 2147483647L));
				if (array.Length < size && settings.NotEnoughBytesPolicy == NotEnoughBytesPolicy.Abort)
				{
					throw new NotEnoughBytesException("Unknown chunk's data cannot be read since the reader's underlying stream doesn't have enough bytes.", size, array.Length);
				}
				Data = array;
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		protected override uint GetContentSize(WritingSettings settings)
		{
			byte[] data = Data;
			if (data == null)
			{
				return 0u;
			}
			return (uint)data.Length;
		}

		public override string ToString()
		{
			return "Unknown chunk (" + base.ChunkId + ")";
		}
	}
	public sealed class ChunksCollection : ICollection<MidiChunk>, IEnumerable<MidiChunk>, IEnumerable
	{
		private readonly List<MidiChunk> _chunks = new List<MidiChunk>();

		public MidiChunk this[int index]
		{
			get
			{
				ThrowIfArgument.IsInvalidIndex("index", index, _chunks.Count);
				return _chunks[index];
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				ThrowIfArgument.IsInvalidIndex("index", index, _chunks.Count);
				_chunks[index] = value;
			}
		}

		public int Count => _chunks.Count;

		public bool IsReadOnly { get; }

		public void Add(MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			_chunks.Add(chunk);
		}

		public void AddRange(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			_chunks.AddRange(chunks.Where((MidiChunk c) => c != null));
		}

		public void Insert(int index, MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			ThrowIfArgument.IsInvalidIndex("index", index, _chunks.Count);
			_chunks.Insert(index, chunk);
		}

		public void InsertRange(int index, IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			ThrowIfArgument.IsInvalidIndex("index", index, _chunks.Count);
			_chunks.InsertRange(index, chunks.Where((MidiChunk c) => c != null));
		}

		public bool Remove(MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			return _chunks.Remove(chunk);
		}

		public void RemoveAt(int index)
		{
			ThrowIfArgument.IsInvalidIndex("index", index, _chunks.Count);
			_chunks.RemoveAt(index);
		}

		public int RemoveAll(Predicate<MidiChunk> match)
		{
			ThrowIfArgument.IsNull("match", match);
			return _chunks.RemoveAll(match);
		}

		public int IndexOf(MidiChunk chunk)
		{
			ThrowIfArgument.IsNull("chunk", chunk);
			return _chunks.IndexOf(chunk);
		}

		public void Clear()
		{
			_chunks.Clear();
		}

		public IEnumerator<MidiChunk> GetEnumerator()
		{
			return _chunks.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _chunks.GetEnumerator();
		}

		public bool Contains(MidiChunk item)
		{
			return _chunks.Contains(item);
		}

		public void CopyTo(MidiChunk[] array, int arrayIndex)
		{
			_chunks.CopyTo(array, arrayIndex);
		}
	}
	public sealed class EventsCollection : ICollection<MidiEvent>, IEnumerable<MidiEvent>, IEnumerable
	{
		internal readonly List<MidiEvent> _events = new List<MidiEvent>();

		public MidiEvent this[int index]
		{
			get
			{
				ThrowIfArgument.IsInvalidIndex("index", index, _events.Count);
				return _events[index];
			}
			set
			{
				ThrowIfArgument.IsNull("value", value);
				ThrowIfArgument.IsInvalidIndex("index", index, _events.Count);
				_events[index] = value;
			}
		}

		public int Count => _events.Count;

		public bool IsReadOnly { get; }

		internal EventsCollection()
		{
		}

		public void Add(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			_events.Add(midiEvent);
		}

		public void AddRange(IEnumerable<MidiEvent> events)
		{
			ThrowIfArgument.IsNull("events", events);
			_events.AddRange(events.Where((MidiEvent e) => e != null));
		}

		public void Insert(int index, MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsInvalidIndex("index", index, _events.Count);
			_events.Insert(index, midiEvent);
		}

		public void InsertRange(int index, IEnumerable<MidiEvent> midiEvents)
		{
			ThrowIfArgument.IsNull("midiEvents", midiEvents);
			ThrowIfArgument.IsInvalidIndex("index", index, _events.Count);
			_events.InsertRange(index, midiEvents);
		}

		public bool Remove(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			return _events.Remove(midiEvent);
		}

		public void RemoveAt(int index)
		{
			ThrowIfArgument.IsInvalidIndex("index", index, _events.Count);
			_events.RemoveAt(index);
		}

		public int RemoveAll(Predicate<MidiEvent> match)
		{
			ThrowIfArgument.IsNull("match", match);
			return _events.RemoveAll(match);
		}

		public int IndexOf(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			return _events.IndexOf(midiEvent);
		}

		public void Clear()
		{
			_events.Clear();
		}

		public IEnumerator<MidiEvent> GetEnumerator()
		{
			return _events.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _events.GetEnumerator();
		}

		public bool Contains(MidiEvent item)
		{
			return _events.Contains(item);
		}

		public void CopyTo(MidiEvent[] array, int arrayIndex)
		{
			_events.CopyTo(array, arrayIndex);
		}
	}
	internal static class MidiChunkEquality
	{
		public static bool Equals(MidiChunk midiChunk1, MidiChunk midiChunk2, MidiChunkEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (midiChunk1 == midiChunk2)
			{
				return true;
			}
			if (midiChunk1 == null || midiChunk2 == null)
			{
				message = "One of chunks is null.";
				return false;
			}
			Type type = midiChunk1.GetType();
			Type type2 = midiChunk2.GetType();
			if (type != type2)
			{
				message = $"Types of chunks are different ({type} vs {type2}).";
				return false;
			}
			if (midiChunk1 is TrackChunk trackChunk)
			{
				TrackChunk trackChunk2 = (TrackChunk)midiChunk2;
				return EventsCollectionEquality.Equals(trackChunk.Events, trackChunk2.Events, settings.EventEqualityCheckSettings, out message);
			}
			if (midiChunk1 is UnknownChunk unknownChunk)
			{
				UnknownChunk unknownChunk2 = (UnknownChunk)midiChunk2;
				string chunkId = unknownChunk.ChunkId;
				string chunkId2 = unknownChunk2.ChunkId;
				if (chunkId != chunkId2)
				{
					message = "IDs of unknown chunks are different (" + chunkId + " vs " + chunkId2 + ").";
					return false;
				}
				if (!ArrayUtilities.Equals(unknownChunk.Data, unknownChunk2.Data))
				{
					message = "Unknown chunks data are different.";
					return false;
				}
				return true;
			}
			bool num = midiChunk1.Equals(midiChunk2);
			if (!num)
			{
				message = $"Chunks {midiChunk1} and {midiChunk2} are not equal by result of Equals call on first chunk.";
			}
			return num;
		}
	}
	public sealed class MidiChunkEqualityCheckSettings
	{
		public MidiEventEqualityCheckSettings EventEqualityCheckSettings { get; set; } = new MidiEventEqualityCheckSettings();
	}
	internal static class EventsCollectionEquality
	{
		public static bool Equals(EventsCollection eventsCollection1, EventsCollection eventsCollection2, MidiEventEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (eventsCollection1 == eventsCollection2)
			{
				return true;
			}
			if (eventsCollection1 == null || eventsCollection2 == null)
			{
				message = "One of events collections is null.";
				return false;
			}
			if (eventsCollection1.Count != eventsCollection2.Count)
			{
				message = $"Counts of events are different ({eventsCollection1.Count} vs {eventsCollection2.Count}).";
				return false;
			}
			for (int i = 0; i < eventsCollection1.Count; i++)
			{
				MidiEvent midiEvent = eventsCollection1[i];
				MidiEvent midiEvent2 = eventsCollection2[i];
				if (!MidiEvent.Equals(midiEvent, midiEvent2, settings, out var message2))
				{
					message = $"Events at position {i} are different. {message2}";
					return false;
				}
			}
			return true;
		}
	}
	internal static class MidiEventEquality
	{
		private static readonly Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>> Comparers = new Dictionary<MidiEventType, Func<MidiEvent, MidiEvent, bool>>
		{
			[MidiEventType.ChannelPrefix] = (MidiEvent e1, MidiEvent e2) => ((ChannelPrefixEvent)e1).Channel == ((ChannelPrefixEvent)e2).Channel,
			[MidiEventType.KeySignature] = delegate(MidiEvent e1, MidiEvent e2)
			{
				KeySignatureEvent keySignatureEvent = (KeySignatureEvent)e1;
				KeySignatureEvent keySignatureEvent2 = (KeySignatureEvent)e2;
				return keySignatureEvent.Key == keySignatureEvent2.Key && keySignatureEvent.Scale == keySignatureEvent2.Scale;
			},
			[MidiEventType.PortPrefix] = (MidiEvent e1, MidiEvent e2) => ((PortPrefixEvent)e1).Port == ((PortPrefixEvent)e2).Port,
			[MidiEventType.SequenceNumber] = (MidiEvent e1, MidiEvent e2) => ((SequenceNumberEvent)e1).Number == ((SequenceNumberEvent)e2).Number,
			[MidiEventType.SetTempo] = (MidiEvent e1, MidiEvent e2) => ((SetTempoEvent)e1).MicrosecondsPerQuarterNote == ((SetTempoEvent)e2).MicrosecondsPerQuarterNote,
			[MidiEventType.SmpteOffset] = delegate(MidiEvent e1, MidiEvent e2)
			{
				SmpteOffsetEvent smpteOffsetEvent = (SmpteOffsetEvent)e1;
				SmpteOffsetEvent smpteOffsetEvent2 = (SmpteOffsetEvent)e2;
				return smpteOffsetEvent.Format == smpteOffsetEvent2.Format && smpteOffsetEvent.Hours == smpteOffsetEvent2.Hours && smpteOffsetEvent.Minutes == smpteOffsetEvent2.Minutes && smpteOffsetEvent.Seconds == smpteOffsetEvent2.Seconds && smpteOffsetEvent.Frames == smpteOffsetEvent2.Frames && smpteOffsetEvent.SubFrames == smpteOffsetEvent2.SubFrames;
			},
			[MidiEventType.TimeSignature] = delegate(MidiEvent e1, MidiEvent e2)
			{
				TimeSignatureEvent timeSignatureEvent = (TimeSignatureEvent)e1;
				TimeSignatureEvent timeSignatureEvent2 = (TimeSignatureEvent)e2;
				return timeSignatureEvent.Numerator == timeSignatureEvent2.Numerator && timeSignatureEvent.Denominator == timeSignatureEvent2.Denominator && timeSignatureEvent.ClocksPerClick == timeSignatureEvent2.ClocksPerClick && timeSignatureEvent.ThirtySecondNotesPerBeat == timeSignatureEvent2.ThirtySecondNotesPerBeat;
			},
			[MidiEventType.EndOfTrack] = (MidiEvent e1, MidiEvent e2) => true,
			[MidiEventType.MidiTimeCode] = delegate(MidiEvent e1, MidiEvent e2)
			{
				MidiTimeCodeEvent midiTimeCodeEvent = (MidiTimeCodeEvent)e1;
				MidiTimeCodeEvent midiTimeCodeEvent2 = (MidiTimeCodeEvent)e2;
				return midiTimeCodeEvent.Component == midiTimeCodeEvent2.Component && (byte)midiTimeCodeEvent.ComponentValue == (byte)midiTimeCodeEvent2.ComponentValue;
			},
			[MidiEventType.SongPositionPointer] = (MidiEvent e1, MidiEvent e2) => ((SongPositionPointerEvent)e1).PointerValue == ((SongPositionPointerEvent)e2).PointerValue,
			[MidiEventType.SongSelect] = (MidiEvent e1, MidiEvent e2) => (byte)((SongSelectEvent)e1).Number == (byte)((SongSelectEvent)e2).Number,
			[MidiEventType.TuneRequest] = (MidiEvent e1, MidiEvent e2) => true
		};

		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, MidiEventEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (midiEvent1 == midiEvent2)
			{
				return true;
			}
			if (midiEvent1 == null || midiEvent2 == null)
			{
				message = "One of events is null.";
				return false;
			}
			if (settings.CompareDeltaTimes)
			{
				long deltaTime = midiEvent1.DeltaTime;
				long deltaTime2 = midiEvent2.DeltaTime;
				if (deltaTime != deltaTime2)
				{
					message = $"Delta-times are different ({deltaTime} vs {deltaTime2}).";
					return false;
				}
			}
			Type type = midiEvent1.GetType();
			Type type2 = midiEvent2.GetType();
			if (type != type2)
			{
				message = $"Types of events are different ({type} vs {type2}).";
				return false;
			}
			if (midiEvent1 is SystemRealTimeEvent)
			{
				return true;
			}
			if (midiEvent1 is ChannelEvent channelEvent)
			{
				ChannelEvent channelEvent2 = (ChannelEvent)midiEvent2;
				FourBitNumber channel = channelEvent.Channel;
				FourBitNumber channel2 = channelEvent2.Channel;
				if ((byte)channel != (byte)channel2)
				{
					message = $"Channels of events are different ({channel} vs {channel2}).";
					return false;
				}
				if (channelEvent._dataByte1 != channelEvent2._dataByte1)
				{
					message = $"First data bytes of events are different ({channelEvent._dataByte1} vs {channelEvent2._dataByte1}).";
					return false;
				}
				if (channelEvent._dataByte2 != channelEvent2._dataByte2)
				{
					message = $"Second data bytes of events are different ({channelEvent._dataByte2} vs {channelEvent2._dataByte2}).";
					return false;
				}
				return true;
			}
			if (midiEvent1 is SysExEvent sysExEvent)
			{
				SysExEvent sysExEvent2 = (SysExEvent)midiEvent2;
				bool completed = sysExEvent.Completed;
				bool completed2 = sysExEvent2.Completed;
				if (completed != completed2)
				{
					message = $"'Completed' state of system exclusive events are different ({completed} vs {completed2}).";
					return false;
				}
				if (!ArrayUtilities.Equals(sysExEvent.Data, sysExEvent2.Data))
				{
					message = "System exclusive events data are different.";
					return false;
				}
				return true;
			}
			if (midiEvent1 is SequencerSpecificEvent sequencerSpecificEvent)
			{
				SequencerSpecificEvent sequencerSpecificEvent2 = (SequencerSpecificEvent)midiEvent2;
				if (!ArrayUtilities.Equals(sequencerSpecificEvent.Data, sequencerSpecificEvent2.Data))
				{
					message = "Sequencer specific events data are different.";
					return false;
				}
				return true;
			}
			if (midiEvent1 is UnknownMetaEvent unknownMetaEvent)
			{
				UnknownMetaEvent unknownMetaEvent2 = (UnknownMetaEvent)midiEvent2;
				byte statusByte = unknownMetaEvent.StatusByte;
				byte statusByte2 = unknownMetaEvent2.StatusByte;
				if (statusByte != statusByte2)
				{
					message = $"Unknown meta events status bytes are different ({statusByte} vs {statusByte2}).";
					return false;
				}
				if (!ArrayUtilities.Equals(unknownMetaEvent.Data, unknownMetaEvent2.Data))
				{
					message = "Unknown meta events data are different.";
					return false;
				}
				return true;
			}
			if (midiEvent1 is BaseTextEvent baseTextEvent)
			{
				BaseTextEvent obj = (BaseTextEvent)midiEvent2;
				string text = baseTextEvent.Text;
				string text2 = obj.Text;
				if (!string.Equals(text, text2, settings.TextComparison))
				{
					message = "Meta events texts are different (" + text + " vs " + text2 + ").";
					return false;
				}
				return true;
			}
			if (Comparers.TryGetValue(midiEvent1.EventType, out var value))
			{
				return value(midiEvent1, midiEvent2);
			}
			bool num = midiEvent1.Equals(midiEvent2);
			if (!num)
			{
				message = $"Events {midiEvent1} and {midiEvent2} are not equal by result of Equals call on first event.";
			}
			return num;
		}
	}
	public sealed class MidiEventEqualityCheckSettings
	{
		private StringComparison _textComparison;

		public bool CompareDeltaTimes { get; set; } = true;

		public StringComparison TextComparison
		{
			get
			{
				return _textComparison;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_textComparison = value;
			}
		}
	}
	internal static class MidiFileEquality
	{
		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, MidiFileEqualityCheckSettings settings, out string message)
		{
			message = null;
			if (midiFile1 == midiFile2)
			{
				return true;
			}
			if (midiFile1 == null || midiFile2 == null)
			{
				message = "One of files is null.";
				return false;
			}
			if (settings.CompareOriginalFormat)
			{
				ushort? originalFormat = midiFile1._originalFormat;
				ushort? originalFormat2 = midiFile2._originalFormat;
				if (originalFormat != originalFormat2)
				{
					message = $"Original formats are different ({originalFormat} vs {originalFormat2}).";
					return false;
				}
			}
			ChunksCollection chunks = midiFile1.Chunks;
			ChunksCollection chunks2 = midiFile2.Chunks;
			if (chunks.Count != chunks2.Count)
			{
				message = $"Counts of chunks are different ({chunks.Count} vs {chunks2.Count}).";
				return false;
			}
			for (int i = 0; i < chunks.Count; i++)
			{
				MidiChunk chunk = chunks[i];
				MidiChunk chunk2 = chunks2[i];
				if (!MidiChunk.Equals(chunk, chunk2, settings.ChunkEqualityCheckSettings, out var message2))
				{
					message = $"Chunks at position {i} are different. {message2}";
					return false;
				}
			}
			return true;
		}
	}
	public sealed class MidiFileEqualityCheckSettings
	{
		public bool CompareOriginalFormat { get; set; } = true;

		public MidiChunkEqualityCheckSettings ChunkEqualityCheckSettings { get; set; } = new MidiChunkEqualityCheckSettings();
	}
	public abstract class ChannelEvent : MidiEvent
	{
		internal byte _dataByte1;

		internal byte _dataByte2;

		public FourBitNumber Channel { get; set; }

		protected ChannelEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		protected byte ReadDataByte(MidiReader reader, ReadingSettings settings)
		{
			byte b = reader.ReadByte();
			if (b > (byte)SevenBitNumber.MaxValue)
			{
				switch (settings.InvalidChannelEventParameterValuePolicy)
				{
				case InvalidChannelEventParameterValuePolicy.Abort:
					throw new InvalidChannelEventParameterValueException(base.EventType, b);
				case InvalidChannelEventParameterValuePolicy.ReadValid:
					b &= (byte)SevenBitNumber.MaxValue;
					break;
				case InvalidChannelEventParameterValuePolicy.SnapToLimits:
					b = SevenBitNumber.MaxValue;
					break;
				}
			}
			return b;
		}
	}
	public abstract class MetaEvent : MidiEvent
	{
		protected MetaEvent()
			: this(MidiEventType.CustomMeta)
		{
		}

		internal MetaEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			ReadContent(reader, settings, size);
		}

		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
			WriteContent(writer, settings);
		}

		internal sealed override int GetSize(WritingSettings settings)
		{
			return GetContentSize(settings);
		}

		public static byte[] GetStandardMetaEventStatusBytes()
		{
			return StandardMetaEventStatusBytes.GetStatusBytes();
		}

		protected abstract void ReadContent(MidiReader reader, ReadingSettings settings, int size);

		protected abstract void WriteContent(MidiWriter writer, WritingSettings settings);

		protected abstract int GetContentSize(WritingSettings settings);
	}
	public abstract class MidiEvent
	{
		public const int UnknownContentSize = -1;

		internal long _deltaTime;

		public MidiEventType EventType { get; }

		public long DeltaTime
		{
			get
			{
				return _deltaTime;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Delta-time is negative.");
				_deltaTime = value;
			}
		}

		internal bool Flag { get; set; }

		public MidiEvent(MidiEventType eventType)
		{
			EventType = eventType;
		}

		internal abstract void Read(MidiReader reader, ReadingSettings settings, int size);

		internal abstract void Write(MidiWriter writer, WritingSettings settings);

		internal abstract int GetSize(WritingSettings settings);

		protected abstract MidiEvent CloneEvent();

		public MidiEvent Clone()
		{
			MidiEvent midiEvent = CloneEvent();
			midiEvent._deltaTime = _deltaTime;
			return midiEvent;
		}

		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2)
		{
			string message;
			return Equals(midiEvent1, midiEvent2, out message);
		}

		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, out string message)
		{
			return Equals(midiEvent1, midiEvent2, null, out message);
		}

		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, MidiEventEqualityCheckSettings settings)
		{
			string message;
			return Equals(midiEvent1, midiEvent2, settings, out message);
		}

		public static bool Equals(MidiEvent midiEvent1, MidiEvent midiEvent2, MidiEventEqualityCheckSettings settings, out string message)
		{
			return MidiEventEquality.Equals(midiEvent1, midiEvent2, settings ?? new MidiEventEqualityCheckSettings(), out message);
		}
	}
	public enum MidiEventType : byte
	{
		NormalSysEx,
		EscapeSysEx,
		SequenceNumber,
		Text,
		CopyrightNotice,
		SequenceTrackName,
		InstrumentName,
		Lyric,
		Marker,
		CuePoint,
		ProgramName,
		DeviceName,
		ChannelPrefix,
		PortPrefix,
		EndOfTrack,
		SetTempo,
		SmpteOffset,
		TimeSignature,
		KeySignature,
		SequencerSpecific,
		UnknownMeta,
		CustomMeta,
		NoteOff,
		NoteOn,
		NoteAftertouch,
		ControlChange,
		ProgramChange,
		ChannelAftertouch,
		PitchBend,
		TimingClock,
		Start,
		Continue,
		Stop,
		ActiveSensing,
		Reset,
		MidiTimeCode,
		SongPositionPointer,
		SongSelect,
		TuneRequest
	}
	public abstract class SysExEvent : MidiEvent
	{
		public const byte EndOfEventByte = 247;

		public bool Completed => Data?.LastOrDefault() == 247;

		public byte[] Data { get; set; }

		protected SysExEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Non-negative size have to be specified in order to read SysEx event.");
			Data = reader.ReadBytes(size);
		}

		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		internal sealed override int GetSize(WritingSettings settings)
		{
			byte[] data = Data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}
	}
	public abstract class SystemCommonEvent : MidiEvent
	{
		protected SystemCommonEvent(MidiEventType eventType)
			: base(eventType)
		{
		}
	}
	public abstract class SystemRealTimeEvent : MidiEvent
	{
		protected SystemRealTimeEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
		}

		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
		}

		internal sealed override int GetSize(WritingSettings settings)
		{
			return 0;
		}
	}
	public sealed class ChannelAftertouchEvent : ChannelEvent
	{
		public SevenBitNumber AftertouchValue
		{
			get
			{
				return (SevenBitNumber)_dataByte1;
			}
			set
			{
				_dataByte1 = value;
			}
		}

		public ChannelAftertouchEvent()
			: base(MidiEventType.ChannelAftertouch)
		{
		}

		public ChannelAftertouchEvent(SevenBitNumber aftertouchValue)
			: this()
		{
			AftertouchValue = aftertouchValue;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_dataByte1 = ReadDataByte(reader, settings);
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_dataByte1);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 1;
		}

		protected override MidiEvent CloneEvent()
		{
			return new ChannelAftertouchEvent
			{
				_dataByte1 = _dataByte1,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Channel Aftertouch [{base.Channel}] ({AftertouchValue})";
		}
	}
	public sealed class ControlChangeEvent : ChannelEvent
	{
		public SevenBitNumber ControlNumber
		{
			get
			{
				return (SevenBitNumber)_dataByte1;
			}
			set
			{
				_dataByte1 = value;
			}
		}

		public SevenBitNumber ControlValue
		{
			get
			{
				return (SevenBitNumber)_dataByte2;
			}
			set
			{
				_dataByte2 = value;
			}
		}

		public ControlChangeEvent()
			: base(MidiEventType.ControlChange)
		{
		}

		public ControlChangeEvent(SevenBitNumber controlNumber, SevenBitNumber controlValue)
			: this()
		{
			ControlNumber = controlNumber;
			ControlValue = controlValue;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_dataByte1 = ReadDataByte(reader, settings);
			_dataByte2 = ReadDataByte(reader, settings);
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_dataByte1);
			writer.WriteByte(_dataByte2);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 2;
		}

		protected override MidiEvent CloneEvent()
		{
			return new ControlChangeEvent
			{
				_dataByte1 = _dataByte1,
				_dataByte2 = _dataByte2,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Control Change [{base.Channel}] ({ControlNumber}, {ControlValue})";
		}
	}
	public sealed class NoteAftertouchEvent : ChannelEvent
	{
		public SevenBitNumber NoteNumber
		{
			get
			{
				return (SevenBitNumber)_dataByte1;
			}
			set
			{
				_dataByte1 = value;
			}
		}

		public SevenBitNumber AftertouchValue
		{
			get
			{
				return (SevenBitNumber)_dataByte2;
			}
			set
			{
				_dataByte2 = value;
			}
		}

		public NoteAftertouchEvent()
			: base(MidiEventType.NoteAftertouch)
		{
		}

		public NoteAftertouchEvent(SevenBitNumber noteNumber, SevenBitNumber aftertouchValue)
			: this()
		{
			NoteNumber = noteNumber;
			AftertouchValue = aftertouchValue;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_dataByte1 = ReadDataByte(reader, settings);
			_dataByte2 = ReadDataByte(reader, settings);
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_dataByte1);
			writer.WriteByte(_dataByte2);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 2;
		}

		protected override MidiEvent CloneEvent()
		{
			return new NoteAftertouchEvent
			{
				_dataByte1 = _dataByte1,
				_dataByte2 = _dataByte2,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Note Aftertouch [{base.Channel}] ({NoteNumber}, {AftertouchValue})";
		}
	}
	public abstract class NoteEvent : ChannelEvent
	{
		public SevenBitNumber NoteNumber
		{
			get
			{
				return (SevenBitNumber)_dataByte1;
			}
			set
			{
				_dataByte1 = value;
			}
		}

		public SevenBitNumber Velocity
		{
			get
			{
				return (SevenBitNumber)_dataByte2;
			}
			set
			{
				_dataByte2 = value;
			}
		}

		protected NoteEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		protected NoteEvent(MidiEventType eventType, SevenBitNumber noteNumber, SevenBitNumber velocity)
			: this(eventType)
		{
			NoteNumber = noteNumber;
			Velocity = velocity;
		}

		internal sealed override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_dataByte1 = ReadDataByte(reader, settings);
			_dataByte2 = ReadDataByte(reader, settings);
		}

		internal sealed override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_dataByte1);
			writer.WriteByte(_dataByte2);
		}

		internal sealed override int GetSize(WritingSettings settings)
		{
			return 2;
		}
	}
	public sealed class NoteOffEvent : NoteEvent
	{
		public NoteOffEvent()
			: base(MidiEventType.NoteOff)
		{
		}

		public NoteOffEvent(SevenBitNumber noteNumber, SevenBitNumber velocity)
			: base(MidiEventType.NoteOff, noteNumber, velocity)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new NoteOffEvent
			{
				_dataByte1 = _dataByte1,
				_dataByte2 = _dataByte2,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Note Off [{base.Channel}] ({base.NoteNumber}, {base.Velocity})";
		}
	}
	public sealed class NoteOnEvent : NoteEvent
	{
		public NoteOnEvent()
			: base(MidiEventType.NoteOn)
		{
		}

		public NoteOnEvent(SevenBitNumber noteNumber, SevenBitNumber velocity)
			: base(MidiEventType.NoteOn, noteNumber, velocity)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new NoteOnEvent
			{
				_dataByte1 = _dataByte1,
				_dataByte2 = _dataByte2,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Note On [{base.Channel}] ({base.NoteNumber}, {base.Velocity})";
		}
	}
	public sealed class PitchBendEvent : ChannelEvent
	{
		public const ushort MinPitchValue = 0;

		public const ushort MaxPitchValue = 16383;

		public ushort PitchValue
		{
			get
			{
				return DataTypesUtilities.CombineAsSevenBitNumbers(_dataByte2, _dataByte1);
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, 0, 16383, $"Pitch value is out of [{(ushort)0}; {(ushort)16383}] range.");
				_dataByte1 = value.GetTail();
				_dataByte2 = value.GetHead();
			}
		}

		public PitchBendEvent()
			: base(MidiEventType.PitchBend)
		{
		}

		public PitchBendEvent(ushort pitchValue)
			: this()
		{
			PitchValue = pitchValue;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_dataByte1 = ReadDataByte(reader, settings);
			_dataByte2 = ReadDataByte(reader, settings);
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_dataByte1);
			writer.WriteByte(_dataByte2);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 2;
		}

		protected override MidiEvent CloneEvent()
		{
			return new PitchBendEvent
			{
				_dataByte1 = _dataByte1,
				_dataByte2 = _dataByte2,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Pitch Bend [{base.Channel}] ({PitchValue})";
		}
	}
	public sealed class ProgramChangeEvent : ChannelEvent
	{
		public SevenBitNumber ProgramNumber
		{
			get
			{
				return (SevenBitNumber)_dataByte1;
			}
			set
			{
				_dataByte1 = value;
			}
		}

		public ProgramChangeEvent()
			: base(MidiEventType.ProgramChange)
		{
		}

		public ProgramChangeEvent(SevenBitNumber programNumber)
			: this()
		{
			ProgramNumber = programNumber;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_dataByte1 = ReadDataByte(reader, settings);
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_dataByte1);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 1;
		}

		protected override MidiEvent CloneEvent()
		{
			return new ProgramChangeEvent
			{
				_dataByte1 = _dataByte1,
				Channel = base.Channel
			};
		}

		public override string ToString()
		{
			return $"Program Change [{base.Channel}] ({ProgramNumber})";
		}
	}
	public sealed class BytesToMidiEventConverter : IDisposable
	{
		private static readonly IEventReader MetaEventReader = new MetaEventReader();

		private readonly MemoryStream _dataBytesStream;

		private readonly MidiReader _midiReader;

		private FfStatusBytePolicy _ffStatusBytePolicy;

		private bool _disposed;

		public UnknownChannelEventPolicy UnknownChannelEventPolicy
		{
			get
			{
				return ReadingSettings.UnknownChannelEventPolicy;
			}
			set
			{
				ReadingSettings.UnknownChannelEventPolicy = value;
			}
		}

		public UnknownChannelEventCallback UnknownChannelEventCallback
		{
			get
			{
				return ReadingSettings.UnknownChannelEventCallback;
			}
			set
			{
				ReadingSettings.UnknownChannelEventCallback = value;
			}
		}

		public SilentNoteOnPolicy SilentNoteOnPolicy
		{
			get
			{
				return ReadingSettings.SilentNoteOnPolicy;
			}
			set
			{
				ReadingSettings.SilentNoteOnPolicy = value;
			}
		}

		public InvalidChannelEventParameterValuePolicy InvalidChannelEventParameterValuePolicy
		{
			get
			{
				return ReadingSettings.InvalidChannelEventParameterValuePolicy;
			}
			set
			{
				ReadingSettings.InvalidChannelEventParameterValuePolicy = value;
			}
		}

		public InvalidMetaEventParameterValuePolicy InvalidMetaEventParameterValuePolicy
		{
			get
			{
				return ReadingSettings.InvalidMetaEventParameterValuePolicy;
			}
			set
			{
				ReadingSettings.InvalidMetaEventParameterValuePolicy = value;
			}
		}

		public InvalidSystemCommonEventParameterValuePolicy InvalidSystemCommonEventParameterValuePolicy
		{
			get
			{
				return ReadingSettings.InvalidSystemCommonEventParameterValuePolicy;
			}
			set
			{
				ReadingSettings.InvalidSystemCommonEventParameterValuePolicy = value;
			}
		}

		public EventTypesCollection CustomMetaEventTypes
		{
			get
			{
				return ReadingSettings.CustomMetaEventTypes;
			}
			set
			{
				ReadingSettings.CustomMetaEventTypes = value;
			}
		}

		public Encoding TextEncoding
		{
			get
			{
				return ReadingSettings.TextEncoding;
			}
			set
			{
				ReadingSettings.TextEncoding = value;
			}
		}

		public DecodeTextCallback DecodeTextCallback
		{
			get
			{
				return ReadingSettings.DecodeTextCallback;
			}
			set
			{
				ReadingSettings.DecodeTextCallback = value;
			}
		}

		public ZeroLengthDataPolicy ZeroLengthDataPolicy
		{
			get
			{
				return ReadingSettings.ZeroLengthDataPolicy;
			}
			set
			{
				ReadingSettings.ZeroLengthDataPolicy = value;
			}
		}

		public NotEnoughBytesPolicy NotEnoughBytesPolicy
		{
			get
			{
				return ReadingSettings.NotEnoughBytesPolicy;
			}
			set
			{
				ReadingSettings.NotEnoughBytesPolicy = value;
			}
		}

		public bool ReadDeltaTimes { get; set; }

		public FfStatusBytePolicy FfStatusBytePolicy
		{
			get
			{
				return _ffStatusBytePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_ffStatusBytePolicy = value;
			}
		}

		internal ReadingSettings ReadingSettings { get; } = new ReadingSettings();

		public BytesToMidiEventConverter(int capacity)
		{
			ThrowIfArgument.IsNegative("capacity", capacity, "Capacity is negative.");
			_dataBytesStream = new MemoryStream(capacity);
			_midiReader = new MidiReader(_dataBytesStream, new ReaderSettings());
		}

		public BytesToMidiEventConverter()
			: this(0)
		{
		}

		public ICollection<MidiEvent> ConvertMultiple(byte[] bytes, int offset, int length)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			ThrowIfArgument.IsEmptyCollection("bytes", bytes, "Bytes is empty array.");
			ThrowIfArgument.IsOutOfRange("offset", offset, 0, bytes.Length - 1, "Offset is out of range.");
			ThrowIfArgument.IsOutOfRange("length", length, 0, bytes.Length - offset, "Length is out of range.");
			PrepareStreamWithBytes(bytes, offset, length);
			List<MidiEvent> list = new List<MidiEvent>();
			byte? b = null;
			try
			{
				while (_midiReader.Position < length)
				{
					long num = 0L;
					if (ReadDeltaTimes)
					{
						num = _midiReader.ReadVlqLongNumber();
						if (num < 0)
						{
							num = 0L;
						}
					}
					byte b2 = _midiReader.ReadByte();
					if (b2 <= (byte)SevenBitNumber.MaxValue)
					{
						if (!b.HasValue)
						{
							throw new UnexpectedRunningStatusException();
						}
						b2 = b.Value;
						_midiReader.Position--;
					}
					MidiEvent midiEvent = ReadEvent(b2);
					if (midiEvent is ChannelEvent)
					{
						b = b2;
					}
					midiEvent.DeltaTime = num;
					list.Add(midiEvent);
				}
			}
			catch (EndOfStreamException innerException)
			{
				if (NotEnoughBytesPolicy == NotEnoughBytesPolicy.Abort)
				{
					throw new NotEnoughBytesException("Not enough bytes to read an event.", innerException);
				}
			}
			return list;
		}

		public ICollection<MidiEvent> ConvertMultiple(byte[] bytes)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			ThrowIfArgument.IsEmptyCollection("bytes", bytes, "Bytes is empty array.");
			return ConvertMultiple(bytes, 0, bytes.Length);
		}

		public MidiEvent Convert(byte statusByte, byte[] dataBytes)
		{
			PrepareStreamWithBytes(dataBytes, 0, (dataBytes != null) ? dataBytes.Length : 0);
			return ReadEvent(statusByte);
		}

		public MidiEvent Convert(byte[] bytes)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			ThrowIfArgument.IsEmptyCollection("bytes", bytes, "Bytes is empty array.");
			return Convert(bytes, 0, bytes.Length);
		}

		public MidiEvent Convert(byte[] bytes, int offset, int length)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			ThrowIfArgument.IsEmptyCollection("bytes", bytes, "Bytes is empty array.");
			ThrowIfArgument.IsOutOfRange("offset", offset, 0, bytes.Length - 1, "Offset is out of range.");
			ThrowIfArgument.IsOutOfRange("length", length, 0, bytes.Length - offset, "Length is out of range.");
			PrepareStreamWithBytes(bytes, offset, length);
			long num = 0L;
			if (ReadDeltaTimes)
			{
				num = _midiReader.ReadVlqLongNumber();
				if (num < 0)
				{
					num = 0L;
				}
			}
			byte statusByte = _midiReader.ReadByte();
			MidiEvent midiEvent = ReadEvent(statusByte);
			midiEvent.DeltaTime = num;
			return midiEvent;
		}

		private void PrepareStreamWithBytes(byte[] bytes, int offset, int length)
		{
			_dataBytesStream.Seek(0L, SeekOrigin.Begin);
			if (bytes != null)
			{
				_dataBytesStream.Write(bytes, offset, length);
			}
			_midiReader.Position = 0L;
		}

		private MidiEvent ReadEvent(byte statusByte)
		{
			IEventReader eventReader = EventReaderFactory.GetReader(statusByte, smfOnly: false);
			if (statusByte == byte.MaxValue && FfStatusBytePolicy == FfStatusBytePolicy.ReadAsMetaEvent)
			{
				eventReader = MetaEventReader;
			}
			return eventReader.Read(_midiReader, ReadingSettings, statusByte);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_dataBytesStream.Dispose();
					_midiReader.Dispose();
				}
				_disposed = true;
			}
		}
	}
	public enum FfStatusBytePolicy
	{
		ReadAsResetEvent,
		ReadAsMetaEvent
	}
	public sealed class MidiEventToBytesConverter : IDisposable
	{
		private readonly MemoryStream _dataBytesStream;

		private readonly MidiWriter _midiWriter;

		private readonly WritingSettings _writingSettings = new WritingSettings();

		private bool _disposed;

		public bool UseRunningStatus
		{
			get
			{
				return _writingSettings.UseRunningStatus;
			}
			set
			{
				_writingSettings.UseRunningStatus = value;
			}
		}

		public bool NoteOffAsSilentNoteOn
		{
			get
			{
				return _writingSettings.NoteOffAsSilentNoteOn;
			}
			set
			{
				_writingSettings.NoteOffAsSilentNoteOn = value;
			}
		}

		public EventTypesCollection CustomMetaEventTypes
		{
			get
			{
				return _writingSettings.CustomMetaEventTypes;
			}
			set
			{
				_writingSettings.CustomMetaEventTypes = value;
			}
		}

		public Encoding TextEncoding
		{
			get
			{
				return _writingSettings.TextEncoding;
			}
			set
			{
				_writingSettings.TextEncoding = value;
			}
		}

		public bool WriteDeltaTimes { get; set; }

		public MidiEventToBytesConverter(int capacity)
		{
			ThrowIfArgument.IsNegative("capacity", capacity, "Capacity is negative.");
			_dataBytesStream = new MemoryStream(capacity);
			_midiWriter = new MidiWriter(_dataBytesStream, new WriterSettings
			{
				UseBuffering = false
			});
		}

		public MidiEventToBytesConverter()
			: this(0)
		{
		}

		public byte[] Convert(MidiEvent midiEvent)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			return Convert(midiEvent, 0);
		}

		public byte[] Convert(MidiEvent midiEvent, int minSize)
		{
			ThrowIfArgument.IsNull("midiEvent", midiEvent);
			ThrowIfArgument.IsNegative("minSize", minSize, "Min size is negative.");
			PrepareStream();
			if (WriteDeltaTimes)
			{
				_midiWriter.WriteVlqNumber(midiEvent.DeltaTime);
			}
			EventWriterFactory.GetWriter(midiEvent).Write(midiEvent, _midiWriter, _writingSettings, writeStatusByte: true);
			return GetBytes(minSize);
		}

		public byte[] Convert(IEnumerable<MidiEvent> midiEvents)
		{
			ThrowIfArgument.IsNull("midiEvents", midiEvents);
			PrepareStream();
			byte? b = null;
			foreach (MidiEvent midiEvent2 in midiEvents)
			{
				MidiEvent midiEvent = midiEvent2;
				if (NoteOffAsSilentNoteOn && midiEvent is NoteOffEvent noteOffEvent)
				{
					midiEvent = new NoteOnEvent
					{
						DeltaTime = noteOffEvent.DeltaTime,
						Channel = noteOffEvent.Channel,
						NoteNumber = noteOffEvent.NoteNumber
					};
				}
				if (WriteDeltaTimes)
				{
					_midiWriter.WriteVlqNumber(midiEvent2.DeltaTime);
				}
				IEventWriter writer = EventWriterFactory.GetWriter(midiEvent2);
				bool writeStatusByte = true;
				if (midiEvent is ChannelEvent)
				{
					byte statusByte = writer.GetStatusByte(midiEvent);
					writeStatusByte = b != statusByte || !UseRunningStatus;
					b = statusByte;
				}
				else
				{
					b = null;
				}
				writer.Write(midiEvent2, _midiWriter, _writingSettings, writeStatusByte);
			}
			return GetBytes(0);
		}

		private byte[] GetBytes(int minSize)
		{
			byte[] buffer = _dataBytesStream.GetBuffer();
			long position = _dataBytesStream.Position;
			byte[] array = new byte[Math.Max(position, minSize)];
			Array.Copy(buffer, 0L, array, 0L, position);
			return array;
		}

		private void PrepareStream()
		{
			_dataBytesStream.Seek(0L, SeekOrigin.Begin);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_midiWriter.Dispose();
					_dataBytesStream.Dispose();
				}
				_disposed = true;
			}
		}
	}
	internal static class EventStatusBytes
	{
		internal static class Global
		{
			public const byte Meta = byte.MaxValue;

			public const byte NormalSysEx = 240;

			public const byte EscapeSysEx = 247;
		}

		internal static class Meta
		{
			public const byte SequenceNumber = 0;

			public const byte Text = 1;

			public const byte CopyrightNotice = 2;

			public const byte SequenceTrackName = 3;

			public const byte InstrumentName = 4;

			public const byte Lyric = 5;

			public const byte Marker = 6;

			public const byte CuePoint = 7;

			public const byte ProgramName = 8;

			public const byte DeviceName = 9;

			public const byte ChannelPrefix = 32;

			public const byte PortPrefix = 33;

			public const byte EndOfTrack = 47;

			public const byte SetTempo = 81;

			public const byte SmpteOffset = 84;

			public const byte TimeSignature = 88;

			public const byte KeySignature = 89;

			public const byte SequencerSpecific = 127;
		}

		internal static class Channel
		{
			public const byte NoteOff = 8;

			public const byte NoteOn = 9;

			public const byte NoteAftertouch = 10;

			public const byte ControlChange = 11;

			public const byte ProgramChange = 12;

			public const byte ChannelAftertouch = 13;

			public const byte PitchBend = 14;
		}

		internal static class SystemRealTime
		{
			public const byte TimingClock = 248;

			public const byte Start = 250;

			public const byte Continue = 251;

			public const byte Stop = 252;

			public const byte ActiveSensing = 254;

			public const byte Reset = byte.MaxValue;
		}

		internal static class SystemCommon
		{
			public const byte MtcQuarterFrame = 241;

			public const byte SongPositionPointer = 242;

			public const byte SongSelect = 243;

			public const byte TuneRequest = 246;
		}
	}
	public sealed class EventType
	{
		public Type Type { get; }

		public byte StatusByte { get; }

		public EventType(Type type, byte statusByte)
		{
			Type = type;
			StatusByte = statusByte;
		}
	}
	public sealed class EventTypesCollection : IEnumerable<EventType>, IEnumerable
	{
		private readonly Dictionary<Type, byte> _statusBytes = new Dictionary<Type, byte>();

		private readonly Dictionary<byte, Type> _types = new Dictionary<byte, Type>();

		public void Add(Type type, byte statusByte)
		{
			_statusBytes.Add(type, statusByte);
			_types.Add(statusByte, type);
		}

		public bool TryGetType(byte statusByte, out Type type)
		{
			return _types.TryGetValue(statusByte, out type);
		}

		public bool TryGetStatusByte(Type type, out byte statusByte)
		{
			return _statusBytes.TryGetValue(type, out statusByte);
		}

		public IEnumerator<EventType> GetEnumerator()
		{
			return _statusBytes.Select((KeyValuePair<Type, byte> kv) => new EventType(kv.Key, kv.Value)).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
	internal static class StandardMetaEventStatusBytes
	{
		private static byte[] _statusBytes;

		public static byte[] GetStatusBytes()
		{
			return _statusBytes ?? (_statusBytes = (from f in typeof(EventStatusBytes.Meta).GetFields(BindingFlags.Static | BindingFlags.Public)
				select (byte)f.GetValue(null)).ToArray());
		}
	}
	public abstract class BaseTextEvent : MetaEvent
	{
		public string Text { get; set; }

		public BaseTextEvent(MidiEventType eventType)
			: base(eventType)
		{
		}

		public BaseTextEvent(MidiEventType eventType, string text)
			: this(eventType)
		{
			Text = text;
		}

		protected sealed override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Text event cannot be read since the size is negative number.");
			if (size == 0)
			{
				switch (settings.ZeroLengthDataPolicy)
				{
				case ZeroLengthDataPolicy.ReadAsEmptyObject:
					Text = string.Empty;
					break;
				case ZeroLengthDataPolicy.ReadAsNull:
					Text = null;
					break;
				}
			}
			else
			{
				byte[] bytes = reader.ReadBytes(size);
				Encoding encoding = settings.TextEncoding ?? SmfConstants.DefaultTextEncoding;
				DecodeTextCallback decodeTextCallback = settings.DecodeTextCallback;
				Text = ((decodeTextCallback != null) ? decodeTextCallback(bytes, settings) : encoding.GetString(bytes));
			}
		}

		protected sealed override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			string text = Text;
			if (!string.IsNullOrEmpty(text))
			{
				byte[] bytes = (settings.TextEncoding ?? SmfConstants.DefaultTextEncoding).GetBytes(text);
				writer.WriteBytes(bytes);
			}
		}

		protected sealed override int GetContentSize(WritingSettings settings)
		{
			if (string.IsNullOrEmpty(Text))
			{
				return 0;
			}
			return (settings.TextEncoding ?? SmfConstants.DefaultTextEncoding).GetByteCount(Text);
		}
	}
	public sealed class ChannelPrefixEvent : MetaEvent
	{
		public byte Channel { get; set; }

		public ChannelPrefixEvent()
			: base(MidiEventType.ChannelPrefix)
		{
		}

		public ChannelPrefixEvent(byte channel)
			: this()
		{
			Channel = channel;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			Channel = reader.ReadByte();
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(Channel);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 1;
		}

		protected override MidiEvent CloneEvent()
		{
			return new ChannelPrefixEvent(Channel);
		}

		public override string ToString()
		{
			return $"Channel Prefix ({Channel})";
		}
	}
	public sealed class CopyrightNoticeEvent : BaseTextEvent
	{
		public CopyrightNoticeEvent()
			: base(MidiEventType.CopyrightNotice)
		{
		}

		public CopyrightNoticeEvent(string text)
			: base(MidiEventType.CopyrightNotice, text)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new CopyrightNoticeEvent(base.Text);
		}

		public override string ToString()
		{
			return "Copyright Notice (" + base.Text + ")";
		}
	}
	public sealed class CuePointEvent : BaseTextEvent
	{
		public CuePointEvent()
			: base(MidiEventType.CuePoint)
		{
		}

		public CuePointEvent(string text)
			: base(MidiEventType.CuePoint, text)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new CuePointEvent(base.Text);
		}

		public override string ToString()
		{
			return "Cue Point (" + base.Text + ")";
		}
	}
	public sealed class DeviceNameEvent : BaseTextEvent
	{
		public DeviceNameEvent()
			: base(MidiEventType.DeviceName)
		{
		}

		public DeviceNameEvent(string deviceName)
			: base(MidiEventType.DeviceName, deviceName)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new DeviceNameEvent(base.Text);
		}

		public override string ToString()
		{
			return "Device Name (" + base.Text + ")";
		}
	}
	public sealed class EndOfTrackEvent : MetaEvent
	{
		internal EndOfTrackEvent()
			: base(MidiEventType.EndOfTrack)
		{
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 0;
		}

		protected override MidiEvent CloneEvent()
		{
			return new EndOfTrackEvent();
		}

		public override string ToString()
		{
			return "End Of Track";
		}
	}
	public sealed class InstrumentNameEvent : BaseTextEvent
	{
		public InstrumentNameEvent()
			: base(MidiEventType.InstrumentName)
		{
		}

		public InstrumentNameEvent(string instrumentName)
			: base(MidiEventType.InstrumentName, instrumentName)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new InstrumentNameEvent(base.Text);
		}

		public override string ToString()
		{
			return "Instrument Name (" + base.Text + ")";
		}
	}
	public sealed class KeySignatureEvent : MetaEvent
	{
		public const sbyte DefaultKey = 0;

		public const byte DefaultScale = 0;

		public const sbyte MinKey = -7;

		public const sbyte MaxKey = 7;

		public const byte MinScale = 0;

		public const byte MaxScale = 1;

		private sbyte _key;

		private byte _scale;

		public sbyte Key
		{
			get
			{
				return _key;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, -7, 7, $"Key is out of [{(sbyte)(-7)}; {(sbyte)7}] range.");
				_key = value;
			}
		}

		public byte Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, 0, 1, $"Scale is out of {(byte)0}-{(byte)1} range.");
				_scale = value;
			}
		}

		public KeySignatureEvent()
			: base(MidiEventType.KeySignature)
		{
		}

		public KeySignatureEvent(sbyte key, byte scale)
			: this()
		{
			Key = key;
			Scale = scale;
		}

		private int ProcessValue(int value, string property, int min, int max, InvalidMetaEventParameterValuePolicy policy)
		{
			if (value >= min && value <= max)
			{
				return value;
			}
			return policy switch
			{
				InvalidMetaEventParameterValuePolicy.Abort => throw new InvalidMetaEventParameterValueException(base.EventType, property, value), 
				InvalidMetaEventParameterValuePolicy.SnapToLimits => Math.Min(Math.Max(value, min), max), 
				_ => value, 
			};
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			InvalidMetaEventParameterValuePolicy invalidMetaEventParameterValuePolicy = settings.InvalidMetaEventParameterValuePolicy;
			Key = (sbyte)ProcessValue(reader.ReadSByte(), "Key", -7, 7, invalidMetaEventParameterValuePolicy);
			Scale = (byte)ProcessValue(reader.ReadByte(), "Scale", 0, 1, invalidMetaEventParameterValuePolicy);
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteSByte(Key);
			writer.WriteByte(Scale);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 2;
		}

		protected override MidiEvent CloneEvent()
		{
			return new KeySignatureEvent
			{
				_key = _key,
				_scale = _scale
			};
		}

		public override string ToString()
		{
			return $"Key Signature ({Key}, {Scale})";
		}
	}
	public sealed class LyricEvent : BaseTextEvent
	{
		public LyricEvent()
			: base(MidiEventType.Lyric)
		{
		}

		public LyricEvent(string text)
			: base(MidiEventType.Lyric, text)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new LyricEvent(base.Text);
		}

		public override string ToString()
		{
			return "Lyric (" + base.Text + ")";
		}
	}
	public sealed class MarkerEvent : BaseTextEvent
	{
		public MarkerEvent()
			: base(MidiEventType.Marker)
		{
		}

		public MarkerEvent(string text)
			: base(MidiEventType.Marker, text)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new MarkerEvent(base.Text);
		}

		public override string ToString()
		{
			return "Marker (" + base.Text + ")";
		}
	}
	public sealed class PortPrefixEvent : MetaEvent
	{
		public byte Port { get; set; }

		public PortPrefixEvent()
			: base(MidiEventType.PortPrefix)
		{
		}

		public PortPrefixEvent(byte port)
			: this()
		{
			Port = port;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			if (size >= 1)
			{
				Port = reader.ReadByte();
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(Port);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 1;
		}

		protected override MidiEvent CloneEvent()
		{
			return new PortPrefixEvent(Port);
		}

		public override string ToString()
		{
			return $"Port Prefix ({Port})";
		}
	}
	public sealed class ProgramNameEvent : BaseTextEvent
	{
		public ProgramNameEvent()
			: base(MidiEventType.ProgramName)
		{
		}

		public ProgramNameEvent(string programName)
			: base(MidiEventType.ProgramName, programName)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new ProgramNameEvent(base.Text);
		}

		public override string ToString()
		{
			return "Program Name (" + base.Text + ")";
		}
	}
	public sealed class SequenceNumberEvent : MetaEvent
	{
		public ushort Number { get; set; }

		public SequenceNumberEvent()
			: base(MidiEventType.SequenceNumber)
		{
		}

		public SequenceNumberEvent(ushort number)
			: this()
		{
			Number = number;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			if (size >= 2)
			{
				Number = reader.ReadWord();
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteWord(Number);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 2;
		}

		protected override MidiEvent CloneEvent()
		{
			return new SequenceNumberEvent(Number);
		}

		public override string ToString()
		{
			return $"Sequence Number ({Number})";
		}
	}
	public sealed class SequencerSpecificEvent : MetaEvent
	{
		public byte[] Data { get; set; }

		public SequencerSpecificEvent()
			: base(MidiEventType.SequencerSpecific)
		{
		}

		public SequencerSpecificEvent(byte[] data)
			: this()
		{
			Data = data;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Sequencer specific event cannot be read since the size is negative number.");
			if (size == 0)
			{
				switch (settings.ZeroLengthDataPolicy)
				{
				case ZeroLengthDataPolicy.ReadAsEmptyObject:
					Data = new byte[0];
					break;
				case ZeroLengthDataPolicy.ReadAsNull:
					Data = null;
					break;
				}
			}
			else
			{
				Data = reader.ReadBytes(size);
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			byte[] data = Data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}

		protected override MidiEvent CloneEvent()
		{
			return new SequencerSpecificEvent(Data?.Clone() as byte[]);
		}

		public override string ToString()
		{
			return "Sequencer Specific";
		}
	}
	public sealed class SequenceTrackNameEvent : BaseTextEvent
	{
		public SequenceTrackNameEvent()
			: base(MidiEventType.SequenceTrackName)
		{
		}

		public SequenceTrackNameEvent(string name)
			: base(MidiEventType.SequenceTrackName, name)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new SequenceTrackNameEvent(base.Text);
		}

		public override string ToString()
		{
			return "Sequence/Track Name (" + base.Text + ")";
		}
	}
	public sealed class SetTempoEvent : MetaEvent
	{
		public const long DefaultMicrosecondsPerQuarterNote = 500000L;

		public const long MinMicrosecondsPerQuarterNote = 1L;

		public const long MaxMicrosecondsPerQuarterNote = 16777215L;

		private long _microsecondsPerBeat = 500000L;

		public long MicrosecondsPerQuarterNote
		{
			get
			{
				return _microsecondsPerBeat;
			}
			set
			{
				ThrowIfArgument.IsOutOfRange("value", value, 1L, 16777215L, $"Number of microseconds per quarter note is out of [{1L}; {16777215L}] range.");
				_microsecondsPerBeat = value;
			}
		}

		public SetTempoEvent()
			: base(MidiEventType.SetTempo)
		{
		}

		public SetTempoEvent(long microsecondsPerQuarterNote)
			: this()
		{
			MicrosecondsPerQuarterNote = microsecondsPerQuarterNote;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			MicrosecondsPerQuarterNote = reader.Read3ByteDword();
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.Write3ByteDword((uint)MicrosecondsPerQuarterNote);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 3;
		}

		protected override MidiEvent CloneEvent()
		{
			return new SetTempoEvent
			{
				_microsecondsPerBeat = _microsecondsPerBeat
			};
		}

		public override string ToString()
		{
			return $"Set Tempo ({MicrosecondsPerQuarterNote})";
		}
	}
	public sealed class SmpteOffsetEvent : MetaEvent
	{
		private SmpteData _smpteData = new SmpteData();

		public SmpteFormat Format
		{
			get
			{
				return _smpteData.Format;
			}
			set
			{
				_smpteData.Format = value;
			}
		}

		public byte Hours
		{
			get
			{
				return _smpteData.Hours;
			}
			set
			{
				_smpteData.Hours = value;
			}
		}

		public byte Minutes
		{
			get
			{
				return _smpteData.Minutes;
			}
			set
			{
				_smpteData.Minutes = value;
			}
		}

		public byte Seconds
		{
			get
			{
				return _smpteData.Seconds;
			}
			set
			{
				_smpteData.Seconds = value;
			}
		}

		public byte Frames
		{
			get
			{
				return _smpteData.Frames;
			}
			set
			{
				_smpteData.Frames = value;
			}
		}

		public byte SubFrames
		{
			get
			{
				return _smpteData.SubFrames;
			}
			set
			{
				_smpteData.SubFrames = value;
			}
		}

		public SmpteOffsetEvent()
			: base(MidiEventType.SmpteOffset)
		{
		}

		public SmpteOffsetEvent(SmpteFormat format, byte hours, byte minutes, byte seconds, byte frames, byte subFrames)
			: this()
		{
			Format = format;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
			Frames = frames;
			SubFrames = subFrames;
		}

		private byte ProcessValue(byte value, string property, byte max, InvalidMetaEventParameterValuePolicy policy)
		{
			if (value <= max)
			{
				return value;
			}
			return policy switch
			{
				InvalidMetaEventParameterValuePolicy.Abort => throw new InvalidMetaEventParameterValueException(base.EventType, property, value), 
				InvalidMetaEventParameterValuePolicy.SnapToLimits => Math.Min(value, max), 
				_ => value, 
			};
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			_smpteData = SmpteData.Read(reader.ReadByte, (byte value, string propertyName, byte max) => ProcessValue(value, propertyName, max, settings.InvalidMetaEventParameterValuePolicy));
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			_smpteData.Write(writer.WriteByte);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 5;
		}

		protected override MidiEvent CloneEvent()
		{
			return new SmpteOffsetEvent(Format, Hours, Minutes, Seconds, Frames, SubFrames);
		}

		public override string ToString()
		{
			return $"SMPTE Offset ({Format}, {Hours}:{Minutes}:{Seconds}:{Frames}:{SubFrames})";
		}
	}
	public sealed class TextEvent : BaseTextEvent
	{
		public TextEvent()
			: base(MidiEventType.Text)
		{
		}

		public TextEvent(string text)
			: base(MidiEventType.Text, text)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new TextEvent(base.Text);
		}

		public override string ToString()
		{
			return "Text (" + base.Text + ")";
		}
	}
	public sealed class TimeSignatureEvent : MetaEvent
	{
		public const byte DefaultNumerator = 4;

		public const byte DefaultDenominator = 4;

		public const byte DefaultClocksPerClick = 24;

		public const byte DefaultThirtySecondNotesPerBeat = 8;

		private byte _denominator = 4;

		public byte Numerator { get; set; } = 4;

		public byte Denominator
		{
			get
			{
				return _denominator;
			}
			set
			{
				ThrowIfArgument.DoesntSatisfyCondition("value", value, MathUtilities.IsPowerOfTwo, "Denominator is zero or is not a power of two.");
				_denominator = value;
			}
		}

		public byte ClocksPerClick { get; set; } = 24;

		public byte ThirtySecondNotesPerBeat { get; set; } = 8;

		public TimeSignatureEvent()
			: base(MidiEventType.TimeSignature)
		{
		}

		public TimeSignatureEvent(byte numerator, byte denominator)
			: this(numerator, denominator, 24, 8)
		{
		}

		public TimeSignatureEvent(byte numerator, byte denominator, byte clocksPerClick, byte thirtySecondNotesPerBeat)
			: this()
		{
			Numerator = numerator;
			Denominator = denominator;
			ClocksPerClick = clocksPerClick;
			ThirtySecondNotesPerBeat = thirtySecondNotesPerBeat;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			Numerator = reader.ReadByte();
			Denominator = (byte)Math.Pow(2.0, (int)reader.ReadByte());
			if (size >= 4)
			{
				ClocksPerClick = reader.ReadByte();
				ThirtySecondNotesPerBeat = reader.ReadByte();
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(Numerator);
			writer.WriteByte((byte)Math.Log((int)Denominator, 2.0));
			writer.WriteByte(ClocksPerClick);
			writer.WriteByte(ThirtySecondNotesPerBeat);
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			return 4;
		}

		protected override MidiEvent CloneEvent()
		{
			return new TimeSignatureEvent
			{
				Numerator = Numerator,
				_denominator = _denominator,
				ClocksPerClick = ClocksPerClick,
				ThirtySecondNotesPerBeat = ThirtySecondNotesPerBeat
			};
		}

		public override string ToString()
		{
			return $"Time Signature ({Numerator}/{Denominator}, {ClocksPerClick} clock/click, {ThirtySecondNotesPerBeat} 32nd/beat)";
		}
	}
	public sealed class UnknownMetaEvent : MetaEvent
	{
		public byte StatusByte { get; }

		public byte[] Data { get; private set; }

		internal UnknownMetaEvent(byte statusByte)
			: this(statusByte, null)
		{
		}

		internal UnknownMetaEvent(byte statusByte, byte[] data)
			: base(MidiEventType.UnknownMeta)
		{
			StatusByte = statusByte;
			Data = data;
		}

		protected override void ReadContent(MidiReader reader, ReadingSettings settings, int size)
		{
			ThrowIfArgument.IsNegative("size", size, "Unknown meta event cannot be read since the size is negative number.");
			if (size == 0)
			{
				switch (settings.ZeroLengthDataPolicy)
				{
				case ZeroLengthDataPolicy.ReadAsEmptyObject:
					Data = new byte[0];
					break;
				case ZeroLengthDataPolicy.ReadAsNull:
					Data = null;
					break;
				}
			}
			else
			{
				Data = reader.ReadBytes(size);
			}
		}

		protected override void WriteContent(MidiWriter writer, WritingSettings settings)
		{
			byte[] data = Data;
			if (data != null)
			{
				writer.WriteBytes(data);
			}
		}

		protected override int GetContentSize(WritingSettings settings)
		{
			byte[] data = Data;
			if (data == null)
			{
				return 0;
			}
			return data.Length;
		}

		protected override MidiEvent CloneEvent()
		{
			return new UnknownMetaEvent(StatusByte, Data?.Clone() as byte[]);
		}

		public override string ToString()
		{
			return $"Unknown meta event ({StatusByte})";
		}
	}
	internal sealed class ChannelEventReader : IEventReader
	{
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			FourBitNumber head = currentStatusByte.GetHead();
			FourBitNumber tail = currentStatusByte.GetTail();
			ChannelEvent channelEvent;
			switch (head)
			{
			case 8:
				channelEvent = new NoteOffEvent();
				break;
			case 9:
				channelEvent = new NoteOnEvent();
				break;
			case 11:
				channelEvent = new ControlChangeEvent();
				break;
			case 14:
				channelEvent = new PitchBendEvent();
				break;
			case 13:
				channelEvent = new ChannelAftertouchEvent();
				break;
			case 12:
				channelEvent = new ProgramChangeEvent();
				break;
			case 10:
				channelEvent = new NoteAftertouchEvent();
				break;
			default:
				ReactOnUnknownChannelEvent(head, tail, reader, settings);
				return null;
			}
			channelEvent.Read(reader, settings, -1);
			channelEvent.Channel = tail;
			if (channelEvent.EventType == MidiEventType.NoteOn)
			{
				NoteOnEvent noteOnEvent = (NoteOnEvent)channelEvent;
				if (settings.SilentNoteOnPolicy == SilentNoteOnPolicy.NoteOff && (byte)noteOnEvent.Velocity == 0)
				{
					channelEvent = new NoteOffEvent
					{
						DeltaTime = noteOnEvent.DeltaTime,
						Channel = noteOnEvent.Channel,
						NoteNumber = noteOnEvent.NoteNumber
					};
				}
			}
			return channelEvent;
		}

		private void ReactOnUnknownChannelEvent(FourBitNumber statusByte, FourBitNumber channel, MidiReader reader, ReadingSettings settings)
		{
			switch (settings.UnknownChannelEventPolicy)
			{
			case UnknownChannelEventPolicy.Abort:
				throw new UnknownChannelEventException(statusByte, channel);
			case UnknownChannelEventPolicy.SkipStatusByte:
				break;
			case UnknownChannelEventPolicy.SkipStatusByteAndOneDataByte:
				reader.Position++;
				break;
			case UnknownChannelEventPolicy.SkipStatusByteAndTwoDataBytes:
				reader.Position += 2L;
				break;
			case UnknownChannelEventPolicy.UseCallback:
			{
				UnknownChannelEventAction unknownChannelEventAction = (settings.UnknownChannelEventCallback ?? throw new InvalidOperationException("Unknown channel event callback is not set."))(statusByte, channel);
				switch (unknownChannelEventAction.Instruction)
				{
				case UnknownChannelEventInstruction.Abort:
					throw new UnknownChannelEventException(statusByte, channel);
				case UnknownChannelEventInstruction.SkipData:
					reader.Position += unknownChannelEventAction.DataBytesToSkipCount;
					break;
				}
				break;
			}
			}
		}
	}
	internal static class EventReaderFactory
	{
		private static readonly IEventReader MetaEventReader = new MetaEventReader();

		private static readonly IEventReader ChannelEventReader = new ChannelEventReader();

		private static readonly IEventReader SysExEventReader = new SysExEventReader();

		private static readonly IEventReader SystemRealTimeEventReader = new SystemRealTimeEventReader();

		private static readonly IEventReader SystemCommonEventReader = new SystemCommonEventReader();

		internal static IEventReader GetReader(byte statusByte, bool smfOnly)
		{
			if (statusByte == 247 || statusByte == 240)
			{
				return SysExEventReader;
			}
			if (!smfOnly)
			{
				switch (statusByte)
				{
				case 248:
				case 250:
				case 251:
				case 252:
				case 254:
				case byte.MaxValue:
					return SystemRealTimeEventReader;
				case 241:
				case 242:
				case 243:
				case 246:
					return SystemCommonEventReader;
				}
			}
			if (statusByte == byte.MaxValue)
			{
				return MetaEventReader;
			}
			return ChannelEventReader;
		}
	}
	internal interface IEventReader
	{
		MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte);
	}
	internal sealed class MetaEventReader : IEventReader
	{
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			byte b = reader.ReadByte();
			int num = reader.ReadVlqNumber();
			MetaEvent metaEvent;
			switch (b)
			{
			case 5:
				metaEvent = new LyricEvent();
				break;
			case 81:
				metaEvent = new SetTempoEvent();
				break;
			case 1:
				metaEvent = new TextEvent();
				break;
			case 3:
				metaEvent = new SequenceTrackNameEvent();
				break;
			case 33:
				metaEvent = new PortPrefixEvent();
				break;
			case 88:
				metaEvent = new TimeSignatureEvent();
				break;
			case 127:
				metaEvent = new SequencerSpecificEvent();
				break;
			case 89:
				metaEvent = new KeySignatureEvent();
				break;
			case 6:
				metaEvent = new MarkerEvent();
				break;
			case 32:
				metaEvent = new ChannelPrefixEvent();
				break;
			case 4:
				metaEvent = new InstrumentNameEvent();
				break;
			case 2:
				metaEvent = new CopyrightNoticeEvent();
				break;
			case 84:
				metaEvent = new SmpteOffsetEvent();
				break;
			case 9:
				metaEvent = new DeviceNameEvent();
				break;
			case 7:
				metaEvent = new CuePointEvent();
				break;
			case 8:
				metaEvent = new ProgramNameEvent();
				break;
			case 0:
				metaEvent = new SequenceNumberEvent();
				break;
			case 47:
				metaEvent = new EndOfTrackEvent();
				break;
			default:
			{
				Type type = null;
				EventTypesCollection customMetaEventTypes = settings.CustomMetaEventTypes;
				metaEvent = ((customMetaEventTypes != null && customMetaEventTypes.TryGetType(b, out type) && IsMetaEventType(type)) ? ((MetaEvent)Activator.CreateInstance(type)) : new UnknownMetaEvent(b));
				break;
			}
			}
			long position = reader.Position;
			metaEvent.Read(reader, settings, num);
			long num2 = reader.Position - position;
			long num3 = num - num2;
			if (num3 > 0)
			{
				reader.Position += num3;
			}
			return metaEvent;
		}

		private static bool IsMetaEventType(Type type)
		{
			if (type != null && type.IsSubclassOf(typeof(MetaEvent)))
			{
				return type.GetConstructor(Type.EmptyTypes) != null;
			}
			return false;
		}
	}
	internal sealed class SysExEventReader : IEventReader
	{
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			int size = reader.ReadVlqNumber();
			SysExEvent sysExEvent = null;
			switch (currentStatusByte)
			{
			case 240:
				sysExEvent = new NormalSysExEvent();
				break;
			case 247:
				sysExEvent = new EscapeSysExEvent();
				break;
			}
			sysExEvent.Read(reader, settings, size);
			return sysExEvent;
		}
	}
	internal sealed class SystemCommonEventReader : IEventReader
	{
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			SystemCommonEvent systemCommonEvent = null;
			switch (currentStatusByte)
			{
			case 241:
				systemCommonEvent = new MidiTimeCodeEvent();
				break;
			case 243:
				systemCommonEvent = new SongSelectEvent();
				break;
			case 242:
				systemCommonEvent = new SongPositionPointerEvent();
				break;
			case 246:
				systemCommonEvent = new TuneRequestEvent();
				break;
			}
			systemCommonEvent?.Read(reader, settings, -1);
			return systemCommonEvent;
		}
	}
	internal sealed class SystemRealTimeEventReader : IEventReader
	{
		public MidiEvent Read(MidiReader reader, ReadingSettings settings, byte currentStatusByte)
		{
			SystemRealTimeEvent systemRealTimeEvent = null;
			switch (currentStatusByte)
			{
			case 254:
				systemRealTimeEvent = new ActiveSensingEvent();
				break;
			case 251:
				systemRealTimeEvent = new ContinueEvent();
				break;
			case byte.MaxValue:
				systemRealTimeEvent = new ResetEvent();
				break;
			case 250:
				systemRealTimeEvent = new StartEvent();
				break;
			case 252:
				systemRealTimeEvent = new StopEvent();
				break;
			case 248:
				systemRealTimeEvent = new TimingClockEvent();
				break;
			}
			systemRealTimeEvent?.Read(reader, settings, -1);
			return systemRealTimeEvent;
		}
	}
	public sealed class EscapeSysExEvent : SysExEvent
	{
		public EscapeSysExEvent()
			: base(MidiEventType.EscapeSysEx)
		{
		}

		public EscapeSysExEvent(byte[] data)
			: this()
		{
			ThrowIfArgument.StartsWithInvalidValue("data", (IEnumerable<byte>)data, (byte)247, $"First data byte mustn't be {(byte)247} ({(byte)247:X2}) since it will be used automatically.");
			base.Data = data;
		}

		protected override MidiEvent CloneEvent()
		{
			return new EscapeSysExEvent(base.Data?.ToArray());
		}

		public override string ToString()
		{
			return "Escape SysEx";
		}
	}
	public sealed class NormalSysExEvent : SysExEvent
	{
		public NormalSysExEvent()
			: base(MidiEventType.NormalSysEx)
		{
		}

		public NormalSysExEvent(byte[] data)
			: this()
		{
			ThrowIfArgument.StartsWithInvalidValue("data", (IEnumerable<byte>)data, (byte)240, $"First data byte mustn't be {(byte)240} ({(byte)240:X2}) since it will be used automatically.");
			base.Data = data;
		}

		protected override MidiEvent CloneEvent()
		{
			return new NormalSysExEvent(base.Data?.ToArray());
		}

		public override string ToString()
		{
			return "Normal SysEx";
		}
	}
	public enum MidiTimeCodeComponent : byte
	{
		FramesLsb,
		FramesMsb,
		SecondsLsb,
		SecondsMsb,
		MinutesLsb,
		MinutesMsb,
		HoursLsb,
		HoursMsbAndTimeCodeType
	}
	public sealed class MidiTimeCodeEvent : SystemCommonEvent
	{
		private static readonly Dictionary<MidiTimeCodeComponent, byte> ComponentValueMasks = new Dictionary<MidiTimeCodeComponent, byte>
		{
			[MidiTimeCodeComponent.FramesLsb] = 15,
			[MidiTimeCodeComponent.FramesMsb] = 1,
			[MidiTimeCodeComponent.SecondsLsb] = 15,
			[MidiTimeCodeComponent.SecondsMsb] = 3,
			[MidiTimeCodeComponent.MinutesLsb] = 15,
			[MidiTimeCodeComponent.MinutesMsb] = 3,
			[MidiTimeCodeComponent.HoursLsb] = 15,
			[MidiTimeCodeComponent.HoursMsbAndTimeCodeType] = 7
		};

		public MidiTimeCodeComponent Component { get; set; }

		public FourBitNumber ComponentValue { get; set; }

		public MidiTimeCodeEvent()
			: base(MidiEventType.MidiTimeCode)
		{
		}

		public MidiTimeCodeEvent(MidiTimeCodeComponent component, FourBitNumber componentValue)
			: this()
		{
			ThrowIfArgument.IsInvalidEnumValue("component", component);
			byte b = ComponentValueMasks[component];
			ThrowIfArgument.IsGreaterThan("componentValue", (byte)componentValue, b, $"Component's value is greater than maximum valid one which is {b}.");
			Component = component;
			ComponentValue = componentValue;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			byte number = reader.ReadByte();
			byte b = number.GetHead();
			if (!Enum.IsDefined(typeof(MidiTimeCodeComponent), b))
			{
				throw new InvalidMidiTimeCodeComponentException(b);
			}
			Component = (MidiTimeCodeComponent)b;
			FourBitNumber fourBitNumber = number.GetTail();
			if ((byte)fourBitNumber > ComponentValueMasks[Component])
			{
				switch (settings.InvalidSystemCommonEventParameterValuePolicy)
				{
				case InvalidSystemCommonEventParameterValuePolicy.Abort:
					throw new InvalidSystemCommonEventParameterValueException(base.EventType, string.Format("{0} (component is {1})", "ComponentValue", Component), (byte)fourBitNumber);
				case InvalidSystemCommonEventParameterValuePolicy.SnapToLimits:
					fourBitNumber = (FourBitNumber)ComponentValueMasks[Component];
					break;
				}
			}
			ComponentValue = fourBitNumber;
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			MidiTimeCodeComponent component = Component;
			byte b = ComponentValueMasks[component];
			int num = (byte)ComponentValue & b;
			byte value = DataTypesUtilities.CombineAsFourBitNumbers((byte)component, (byte)num);
			writer.WriteByte(value);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 1;
		}

		protected override MidiEvent CloneEvent()
		{
			return new MidiTimeCodeEvent(Component, ComponentValue);
		}

		public override string ToString()
		{
			return $"MIDI Time Code ({Component}, {ComponentValue})";
		}
	}
	public enum MidiTimeCodeType : byte
	{
		TwentyFour,
		TwentyFive,
		ThirtyDrop,
		Thirty
	}
	public sealed class SongPositionPointerEvent : SystemCommonEvent
	{
		private SevenBitNumber _lsb;

		private SevenBitNumber _msb;

		public ushort PointerValue
		{
			get
			{
				return DataTypesUtilities.Combine(_msb, _lsb);
			}
			set
			{
				_msb = value.GetHead();
				_lsb = value.GetTail();
			}
		}

		public SongPositionPointerEvent()
			: base(MidiEventType.SongPositionPointer)
		{
		}

		public SongPositionPointerEvent(ushort pointerValue)
			: this()
		{
			PointerValue = pointerValue;
		}

		private SevenBitNumber ProcessValue(byte value, string property, InvalidSystemCommonEventParameterValuePolicy policy)
		{
			if (value > (byte)SevenBitNumber.MaxValue)
			{
				switch (policy)
				{
				case InvalidSystemCommonEventParameterValuePolicy.Abort:
					throw new InvalidSystemCommonEventParameterValueException(base.EventType, property, value);
				case InvalidSystemCommonEventParameterValuePolicy.SnapToLimits:
					return SevenBitNumber.MaxValue;
				}
			}
			return (SevenBitNumber)value;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			_lsb = ProcessValue(reader.ReadByte(), "LSB", settings.InvalidSystemCommonEventParameterValuePolicy);
			_msb = ProcessValue(reader.ReadByte(), "MSB", settings.InvalidSystemCommonEventParameterValuePolicy);
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(_lsb);
			writer.WriteByte(_msb);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 2;
		}

		protected override MidiEvent CloneEvent()
		{
			return new SongPositionPointerEvent(PointerValue);
		}

		public override string ToString()
		{
			return $"Song Position Pointer ({PointerValue})";
		}
	}
	public sealed class SongSelectEvent : SystemCommonEvent
	{
		public SevenBitNumber Number { get; set; }

		public SongSelectEvent()
			: base(MidiEventType.SongSelect)
		{
		}

		public SongSelectEvent(SevenBitNumber number)
			: this()
		{
			Number = number;
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
			byte b = reader.ReadByte();
			if (b > (byte)SevenBitNumber.MaxValue)
			{
				switch (settings.InvalidSystemCommonEventParameterValuePolicy)
				{
				case InvalidSystemCommonEventParameterValuePolicy.Abort:
					throw new InvalidSystemCommonEventParameterValueException(base.EventType, "Number", b);
				case InvalidSystemCommonEventParameterValuePolicy.SnapToLimits:
					b = SevenBitNumber.MaxValue;
					break;
				}
			}
			Number = (SevenBitNumber)b;
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
			writer.WriteByte(Number);
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 1;
		}

		protected override MidiEvent CloneEvent()
		{
			return new SongSelectEvent(Number);
		}

		public override string ToString()
		{
			return $"Song Number ({Number})";
		}
	}
	public sealed class TuneRequestEvent : SystemCommonEvent
	{
		public TuneRequestEvent()
			: base(MidiEventType.TuneRequest)
		{
		}

		internal override void Read(MidiReader reader, ReadingSettings settings, int size)
		{
		}

		internal override void Write(MidiWriter writer, WritingSettings settings)
		{
		}

		internal override int GetSize(WritingSettings settings)
		{
			return 0;
		}

		protected override MidiEvent CloneEvent()
		{
			return new TuneRequestEvent();
		}

		public override string ToString()
		{
			return "Tune Request";
		}
	}
	public sealed class ActiveSensingEvent : SystemRealTimeEvent
	{
		public ActiveSensingEvent()
			: base(MidiEventType.ActiveSensing)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new ActiveSensingEvent();
		}

		public override string ToString()
		{
			return "Active Sensing";
		}
	}
	public sealed class ContinueEvent : SystemRealTimeEvent
	{
		public ContinueEvent()
			: base(MidiEventType.Continue)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new ContinueEvent();
		}

		public override string ToString()
		{
			return "Continue";
		}
	}
	public sealed class ResetEvent : SystemRealTimeEvent
	{
		public ResetEvent()
			: base(MidiEventType.Reset)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new ResetEvent();
		}

		public override string ToString()
		{
			return "Reset";
		}
	}
	public sealed class StartEvent : SystemRealTimeEvent
	{
		public StartEvent()
			: base(MidiEventType.Start)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new StartEvent();
		}

		public override string ToString()
		{
			return "Start";
		}
	}
	public sealed class StopEvent : SystemRealTimeEvent
	{
		public StopEvent()
			: base(MidiEventType.Stop)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new StopEvent();
		}

		public override string ToString()
		{
			return "Stop";
		}
	}
	public sealed class TimingClockEvent : SystemRealTimeEvent
	{
		public TimingClockEvent()
			: base(MidiEventType.TimingClock)
		{
		}

		protected override MidiEvent CloneEvent()
		{
			return new TimingClockEvent();
		}

		public override string ToString()
		{
			return "Timing Clock";
		}
	}
	internal sealed class ChannelEventWriter : IEventWriter
	{
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			midiEvent.Write(writer, settings);
		}

		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			return (writeStatusByte ? 1 : 0) + midiEvent.GetSize(settings);
		}

		public byte GetStatusByte(MidiEvent midiEvent)
		{
			byte head = 0;
			switch (midiEvent.EventType)
			{
			case MidiEventType.NoteOff:
				head = 8;
				break;
			case MidiEventType.NoteOn:
				head = 9;
				break;
			case MidiEventType.ControlChange:
				head = 11;
				break;
			case MidiEventType.PitchBend:
				head = 14;
				break;
			case MidiEventType.ChannelAftertouch:
				head = 13;
				break;
			case MidiEventType.ProgramChange:
				head = 12;
				break;
			case MidiEventType.NoteAftertouch:
				head = 10;
				break;
			}
			FourBitNumber channel = ((ChannelEvent)midiEvent).Channel;
			return DataTypesUtilities.CombineAsFourBitNumbers(head, channel);
		}
	}
	internal static class EventWriterFactory
	{
		private static readonly IEventWriter MetaEventWriter = new MetaEventWriter();

		private static readonly IEventWriter ChannelEventWriter = new ChannelEventWriter();

		private static readonly IEventWriter SysExEventWriter = new SysExEventWriter();

		private static readonly IEventWriter SystemRealTimeEventWriter = new SystemRealTimeEventWriter();

		private static readonly IEventWriter SystemCommonEventWriter = new SystemCommonEventWriter();

		internal static IEventWriter GetWriter(MidiEvent midiEvent)
		{
			if (midiEvent is MetaEvent)
			{
				return MetaEventWriter;
			}
			if (midiEvent is ChannelEvent)
			{
				return ChannelEventWriter;
			}
			if (midiEvent is SystemRealTimeEvent)
			{
				return SystemRealTimeEventWriter;
			}
			if (midiEvent is SystemCommonEvent)
			{
				return SystemCommonEventWriter;
			}
			return SysExEventWriter;
		}
	}
	internal interface IEventWriter
	{
		void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte);

		int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte);

		byte GetStatusByte(MidiEvent midiEvent);
	}
	internal sealed class MetaEventWriter : IEventWriter
	{
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				writer.WriteByte(byte.MaxValue);
			}
			byte statusByte = 0;
			switch (midiEvent.EventType)
			{
			case MidiEventType.Lyric:
				statusByte = 5;
				break;
			case MidiEventType.SetTempo:
				statusByte = 81;
				break;
			case MidiEventType.Text:
				statusByte = 1;
				break;
			case MidiEventType.SequenceTrackName:
				statusByte = 3;
				break;
			case MidiEventType.PortPrefix:
				statusByte = 33;
				break;
			case MidiEventType.TimeSignature:
				statusByte = 88;
				break;
			case MidiEventType.SequencerSpecific:
				statusByte = 127;
				break;
			case MidiEventType.KeySignature:
				statusByte = 89;
				break;
			case MidiEventType.Marker:
				statusByte = 6;
				break;
			case MidiEventType.ChannelPrefix:
				statusByte = 32;
				break;
			case MidiEventType.InstrumentName:
				statusByte = 4;
				break;
			case MidiEventType.CopyrightNotice:
				statusByte = 2;
				break;
			case MidiEventType.SmpteOffset:
				statusByte = 84;
				break;
			case MidiEventType.DeviceName:
				statusByte = 9;
				break;
			case MidiEventType.CuePoint:
				statusByte = 7;
				break;
			case MidiEventType.ProgramName:
				statusByte = 8;
				break;
			case MidiEventType.SequenceNumber:
				statusByte = 0;
				break;
			case MidiEventType.EndOfTrack:
				statusByte = 47;
				break;
			case MidiEventType.UnknownMeta:
				statusByte = ((UnknownMetaEvent)midiEvent).StatusByte;
				break;
			default:
			{
				Type type = midiEvent.GetType();
				EventTypesCollection customMetaEventTypes = settings.CustomMetaEventTypes;
				if (customMetaEventTypes == null)
				{
					_ = 1;
				}
				else
					_ = !customMetaEventTypes.TryGetStatusByte(type, out statusByte);
				break;
			}
			}
			writer.WriteByte(statusByte);
			int size = midiEvent.GetSize(settings);
			writer.WriteVlqNumber(size);
			midiEvent.Write(writer, settings);
		}

		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			int size = midiEvent.GetSize(settings);
			return (writeStatusByte ? 1 : 0) + 1 + size.GetVlqLength() + size;
		}

		public byte GetStatusByte(MidiEvent midiEvent)
		{
			return byte.MaxValue;
		}
	}
	internal sealed class SysExEventWriter : IEventWriter
	{
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			int size = midiEvent.GetSize(settings);
			writer.WriteVlqNumber(size);
			midiEvent.Write(writer, settings);
		}

		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			int size = midiEvent.GetSize(settings);
			return (writeStatusByte ? 1 : 0) + size.GetVlqLength() + size;
		}

		public byte GetStatusByte(MidiEvent midiEvent)
		{
			return midiEvent.EventType switch
			{
				MidiEventType.NormalSysEx => 240, 
				MidiEventType.EscapeSysEx => 247, 
				_ => 0, 
			};
		}
	}
	internal sealed class SystemCommonEventWriter : IEventWriter
	{
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			midiEvent.Write(writer, settings);
		}

		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			return (writeStatusByte ? 1 : 0) + midiEvent.GetSize(settings);
		}

		public byte GetStatusByte(MidiEvent midiEvent)
		{
			return midiEvent.EventType switch
			{
				MidiEventType.MidiTimeCode => 241, 
				MidiEventType.SongSelect => 243, 
				MidiEventType.SongPositionPointer => 242, 
				MidiEventType.TuneRequest => 246, 
				_ => 0, 
			};
		}
	}
	internal sealed class SystemRealTimeEventWriter : IEventWriter
	{
		public void Write(MidiEvent midiEvent, MidiWriter writer, WritingSettings settings, bool writeStatusByte)
		{
			if (writeStatusByte)
			{
				byte statusByte = GetStatusByte(midiEvent);
				writer.WriteByte(statusByte);
			}
			midiEvent.Write(writer, settings);
		}

		public int CalculateSize(MidiEvent midiEvent, WritingSettings settings, bool writeStatusByte)
		{
			return (writeStatusByte ? 1 : 0) + midiEvent.GetSize(settings);
		}

		public byte GetStatusByte(MidiEvent midiEvent)
		{
			return midiEvent.EventType switch
			{
				MidiEventType.ActiveSensing => 254, 
				MidiEventType.Continue => 251, 
				MidiEventType.Reset => byte.MaxValue, 
				MidiEventType.Start => 250, 
				MidiEventType.Stop => 252, 
				MidiEventType.TimingClock => 248, 
				_ => 0, 
			};
		}
	}
	public sealed class InvalidChannelEventParameterValueException : MidiException
	{
		public MidiEventType EventType { get; }

		public byte Value { get; }

		internal InvalidChannelEventParameterValueException(MidiEventType eventType, byte value)
			: base($"{value} is invalid value for parameter of channel event of {eventType} type.")
		{
			EventType = eventType;
			Value = value;
		}
	}
	public sealed class InvalidChunkSizeException : MidiException
	{
		public string ChunkId { get; }

		public long ExpectedSize { get; }

		public long ActualSize { get; }

		internal InvalidChunkSizeException(string chunkId, long expectedSize, long actualSize)
			: base($"Actual size ({actualSize}) of a {chunkId} chunk differs from the one declared in the chunk's header ({expectedSize}).")
		{
			ChunkId = chunkId;
			ExpectedSize = expectedSize;
			ActualSize = actualSize;
		}
	}
	public sealed class InvalidMetaEventParameterValueException : MidiException
	{
		public MidiEventType EventType { get; }

		public string PropertyName { get; }

		public int Value { get; }

		internal InvalidMetaEventParameterValueException(MidiEventType eventType, string propertyName, int value)
			: base($"{value} is invalid value for the {propertyName} property of a meta event of {eventType} type.")
		{
			EventType = eventType;
			PropertyName = propertyName;
			Value = value;
		}
	}
	public sealed class InvalidMidiTimeCodeComponentException : MidiException
	{
		public byte MidiTimeCodeComponent { get; }

		internal InvalidMidiTimeCodeComponentException(byte midiTimeCodeComponent)
			: base($"Invalid MIDI Time Code component ({midiTimeCodeComponent}).")
		{
			MidiTimeCodeComponent = midiTimeCodeComponent;
		}
	}
	public sealed class InvalidSystemCommonEventParameterValueException : MidiException
	{
		public MidiEventType EventType { get; }

		public string ComponentName { get; }

		public int ComponentValue { get; }

		internal InvalidSystemCommonEventParameterValueException(MidiEventType eventType, string componentName, int componentValue)
			: base($"{componentValue} is invalid value for the {componentName} property of a system common event of {eventType} type.")
		{
			EventType = eventType;
			ComponentName = componentName;
			ComponentValue = componentValue;
		}
	}
	public sealed class MissedEndOfTrackEventException : MidiException
	{
		internal MissedEndOfTrackEventException()
			: base("Track chunk doesn't end with End Of Track event.")
		{
		}
	}
	public sealed class NoHeaderChunkException : MidiException
	{
		internal NoHeaderChunkException()
			: base("MIDI file doesn't contain the header chunk.")
		{
		}
	}
	public sealed class NotEnoughBytesException : MidiException
	{
		public long ExpectedCount { get; }

		public long ActualCount { get; }

		internal NotEnoughBytesException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		internal NotEnoughBytesException(string message, long expectedCount, long actualCount)
			: base(message)
		{
			ExpectedCount = expectedCount;
			ActualCount = actualCount;
		}
	}
	public sealed class TooManyTrackChunksException : MidiException
	{
		public int TrackChunksCount { get; }

		internal TooManyTrackChunksException(int trackChunksCount)
			: base($"Count of track chunks to be written ({trackChunksCount}) is greater than the valid maximum ({ushort.MaxValue}).")
		{
			TrackChunksCount = trackChunksCount;
		}
	}
	public sealed class UnexpectedRunningStatusException : MidiException
	{
		internal UnexpectedRunningStatusException()
			: base("Unexpected running status is encountered.")
		{
		}
	}
	public sealed class UnexpectedTrackChunksCountException : MidiException
	{
		public int ExpectedCount { get; }

		public int ActualCount { get; }

		internal UnexpectedTrackChunksCountException(int expectedCount, int actualCount)
			: base($"Count of track chunks is {actualCount} while {expectedCount} expected.")
		{
			ExpectedCount = expectedCount;
			ActualCount = actualCount;
		}
	}
	public sealed class UnknownChannelEventException : MidiException
	{
		public FourBitNumber Channel { get; }

		public FourBitNumber StatusByte { get; }

		internal UnknownChannelEventException(FourBitNumber statusByte, FourBitNumber channel)
			: base($"Unknown channel event (status byte is {statusByte} and channel is {channel}).")
		{
			StatusByte = statusByte;
			Channel = channel;
		}
	}
	public sealed class UnknownChunkException : MidiException
	{
		public string ChunkId { get; }

		internal UnknownChunkException(string chunkId)
			: base("'" + chunkId + "' chunk ID is unknown.")
		{
			ChunkId = chunkId;
		}
	}
	public sealed class UnknownFileFormatException : MidiException
	{
		public ushort FileFormat { get; }

		internal UnknownFileFormatException(ushort fileFormat)
			: base($"File format {fileFormat} is unknown.")
		{
			FileFormat = fileFormat;
		}
	}
	public sealed class MidiFile
	{
		private const string RiffChunkId = "RIFF";

		private const int RmidPreambleSize = 12;

		internal ushort? _originalFormat;

		public TimeDivision TimeDivision { get; set; } = new TicksPerQuarterNoteTimeDivision();

		public ChunksCollection Chunks { get; } = new ChunksCollection();

		public MidiFileFormat OriginalFormat
		{
			get
			{
				if (!_originalFormat.HasValue)
				{
					throw new InvalidOperationException("Unable to get original format of the file.");
				}
				ushort value = _originalFormat.Value;
				if (!Enum.IsDefined(typeof(MidiFileFormat), value))
				{
					throw new UnknownFileFormatException(value);
				}
				return (MidiFileFormat)value;
			}
			internal set
			{
				_originalFormat = (ushort)value;
			}
		}

		public MidiFile()
		{
		}

		public MidiFile(IEnumerable<MidiChunk> chunks)
		{
			ThrowIfArgument.IsNull("chunks", chunks);
			Chunks.AddRange(chunks);
		}

		public MidiFile(params MidiChunk[] chunks)
			: this((IEnumerable<MidiChunk>)chunks)
		{
		}

		public static MidiFile Read(string filePath, ReadingSettings settings = null)
		{
			using FileStream stream = FileUtilities.OpenFileForRead(filePath);
			return Read(stream, settings);
		}

		public void Write(string filePath, bool overwriteFile = false, MidiFileFormat format = MidiFileFormat.MultiTrack, WritingSettings settings = null)
		{
			ThrowIfArgument.IsInvalidEnumValue("format", format);
			using FileStream stream = FileUtilities.OpenFileForWrite(filePath, overwriteFile);
			Write(stream, format, settings);
		}

		public static MidiFile Read(Stream stream, ReadingSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream doesn't support reading.", "stream");
			}
			if (settings == null)
			{
				settings = new ReadingSettings();
			}
			if (settings.ReaderSettings == null)
			{
				settings.ReaderSettings = new ReaderSettings();
			}
			MidiFile midiFile = new MidiFile();
			int? num = null;
			int num2 = 0;
			bool flag = false;
			try
			{
				using (MidiReader midiReader = new MidiReader(stream, settings.ReaderSettings))
				{
					if (midiReader.EndReached)
					{
						throw new ArgumentException("Stream is already read.", "stream");
					}
					long? num3 = null;
					string text = midiReader.ReadString("RIFF".Length);
					if (text == "RIFF")
					{
						midiReader.Position += 12L;
						uint num4 = midiReader.ReadDword();
						num3 = midiReader.Position + num4;
					}
					else
					{
						midiReader.Position -= text.Length;
					}
					while (!midiReader.EndReached && (!num3.HasValue || midiReader.Position < num3))
					{
						MidiChunk midiChunk = ReadChunk(midiReader, settings, num2, num);
						if (midiChunk == null)
						{
							continue;
						}
						if (midiChunk is HeaderChunk headerChunk)
						{
							if (!flag)
							{
								num = headerChunk.TracksNumber;
								midiFile.TimeDivision = headerChunk.TimeDivision;
								midiFile._originalFormat = headerChunk.FileFormat;
							}
							flag = true;
						}
						else
						{
							if (midiChunk is TrackChunk)
							{
								num2++;
							}
							midiFile.Chunks.Add(midiChunk);
						}
					}
					if (num.HasValue && num2 != num)
					{
						ReactOnUnexpectedTrackChunksCount(settings.UnexpectedTrackChunksCountPolicy, num2, num.Value);
					}
				}
				if (!flag)
				{
					midiFile.TimeDivision = null;
					if (settings.NoHeaderChunkPolicy == NoHeaderChunkPolicy.Abort)
					{
						throw new NoHeaderChunkException();
					}
				}
			}
			catch (NotEnoughBytesException exception)
			{
				ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, exception);
			}
			catch (EndOfStreamException exception2)
			{
				ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, exception2);
			}
			return midiFile;
		}

		public void Write(Stream stream, MidiFileFormat format = MidiFileFormat.MultiTrack, WritingSettings settings = null)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsInvalidEnumValue("format", format);
			if (TimeDivision == null)
			{
				throw new InvalidOperationException("Time division is null.");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("Stream doesn't support writing.", "stream");
			}
			if (settings == null)
			{
				settings = new WritingSettings();
			}
			if (settings.WriterSettings == null)
			{
				settings.WriterSettings = new WriterSettings();
			}
			using MidiWriter writer = new MidiWriter(stream, settings.WriterSettings);
			IEnumerable<MidiChunk> enumerable = ChunksConverterFactory.GetConverter(format).Convert(Chunks);
			if (settings.WriteHeaderChunk)
			{
				int num = enumerable.Count((MidiChunk c) => c is TrackChunk);
				if (num > 65535)
				{
					throw new TooManyTrackChunksException(num);
				}
				HeaderChunk headerChunk = new HeaderChunk();
				headerChunk.FileFormat = (ushort)format;
				headerChunk.TimeDivision = TimeDivision;
				headerChunk.TracksNumber = (ushort)num;
				headerChunk.Write(writer, settings);
			}
			foreach (MidiChunk item in enumerable)
			{
				if (!(item is UnknownChunk) || !settings.DeleteUnknownChunks)
				{
					item.Write(writer, settings);
				}
			}
		}

		public MidiFile Clone()
		{
			return new MidiFile(Chunks.Select((MidiChunk c) => c.Clone()))
			{
				TimeDivision = TimeDivision.Clone(),
				_originalFormat = _originalFormat
			};
		}

		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2)
		{
			string message;
			return Equals(midiFile1, midiFile2, out message);
		}

		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, out string message)
		{
			return Equals(midiFile1, midiFile2, null, out message);
		}

		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, MidiFileEqualityCheckSettings settings)
		{
			string message;
			return Equals(midiFile1, midiFile2, settings, out message);
		}

		public static bool Equals(MidiFile midiFile1, MidiFile midiFile2, MidiFileEqualityCheckSettings settings, out string message)
		{
			return MidiFileEquality.Equals(midiFile1, midiFile2, settings ?? new MidiFileEqualityCheckSettings(), out message);
		}

		private static MidiChunk ReadChunk(MidiReader reader, ReadingSettings settings, int actualTrackChunksCount, int? expectedTrackChunksCount)
		{
			MidiChunk midiChunk = null;
			try
			{
				string text = reader.ReadString(4);
				if (text.Length < 4)
				{
					switch (settings.NotEnoughBytesPolicy)
					{
					case NotEnoughBytesPolicy.Abort:
						throw new NotEnoughBytesException("Chunk ID cannot be read since the reader's underlying stream doesn't have enough bytes.", 4L, text.Length);
					case NotEnoughBytesPolicy.Ignore:
						return null;
					}
				}
				midiChunk = ((text == "MThd") ? new HeaderChunk() : ((!(text == "MTrk")) ? TryCreateChunk(text, settings.CustomChunkTypes) : new TrackChunk()));
				if (midiChunk == null)
				{
					switch (settings.UnknownChunkIdPolicy)
					{
					case UnknownChunkIdPolicy.ReadAsUnknownChunk:
						midiChunk = new UnknownChunk(text);
						break;
					case UnknownChunkIdPolicy.Skip:
					{
						uint num = reader.ReadDword();
						reader.Position += num;
						return null;
					}
					case UnknownChunkIdPolicy.Abort:
						throw new UnknownChunkException(text);
					}
				}
				if (midiChunk is TrackChunk && expectedTrackChunksCount.HasValue && actualTrackChunksCount >= expectedTrackChunksCount)
				{
					ExtraTrackChunkPolicy extraTrackChunkPolicy = settings.ExtraTrackChunkPolicy;
					if (extraTrackChunkPolicy != ExtraTrackChunkPolicy.Read && extraTrackChunkPolicy == ExtraTrackChunkPolicy.Skip)
					{
						uint num2 = reader.ReadDword();
						reader.Position += num2;
						return null;
					}
				}
				midiChunk?.Read(reader, settings);
			}
			catch (NotEnoughBytesException exception)
			{
				ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, exception);
			}
			catch (EndOfStreamException exception2)
			{
				ReactOnNotEnoughBytes(settings.NotEnoughBytesPolicy, exception2);
			}
			return midiChunk;
		}

		private static void ReactOnUnexpectedTrackChunksCount(UnexpectedTrackChunksCountPolicy policy, int actualTrackChunksCount, int expectedTrackChunksCount)
		{
			if (policy != UnexpectedTrackChunksCountPolicy.Ignore && policy == UnexpectedTrackChunksCountPolicy.Abort)
			{
				throw new UnexpectedTrackChunksCountException(expectedTrackChunksCount, actualTrackChunksCount);
			}
		}

		private static void ReactOnNotEnoughBytes(NotEnoughBytesPolicy policy, Exception exception)
		{
			if (policy == NotEnoughBytesPolicy.Abort)
			{
				throw new NotEnoughBytesException("MIDI file cannot be read since the reader's underlying stream doesn't have enough bytes.", exception);
			}
		}

		private static MidiChunk TryCreateChunk(string chunkId, ChunkTypesCollection chunksTypes)
		{
			Type type = null;
			if (chunksTypes == null || !chunksTypes.TryGetType(chunkId, out type) || !IsChunkType(type))
			{
				return null;
			}
			return (MidiChunk)Activator.CreateInstance(type);
		}

		private static bool IsChunkType(Type type)
		{
			if (type != null && type.IsSubclassOf(typeof(MidiChunk)))
			{
				return type.GetConstructor(Type.EmptyTypes) != null;
			}
			return false;
		}
	}
	public enum MidiFileFormat : ushort
	{
		SingleTrack,
		MultiTrack,
		MultiSequence
	}
	public sealed class MidiReader : IDisposable
	{
		private static readonly byte[] EmptyByteArray = new byte[0];

		private readonly ReaderSettings _settings;

		private readonly Stream _stream;

		private readonly bool _isStreamWrapped;

		private readonly bool _useBuffering;

		private byte[] _buffer;

		private int _bufferSize;

		private int _bufferPosition;

		private long _bufferStart = -1L;

		private long _position;

		private bool _disposed;

		public long Position
		{
			get
			{
				if (!_useBuffering)
				{
					return _stream.Position;
				}
				return _position;
			}
			set
			{
				if (_useBuffering)
				{
					_bufferPosition += (int)(value - _position);
				}
				else
				{
					_stream.Position = value;
				}
				_position = value;
			}
		}

		public long Length { get; }

		public bool EndReached
		{
			get
			{
				if (Position < Length)
				{
					if (_isStreamWrapped)
					{
						return ((StreamWrapper)_stream).IsEndReached();
					}
					return false;
				}
				return true;
			}
		}

		public MidiReader(Stream stream, ReaderSettings settings)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("settings", settings);
			_settings = settings;
			if (!stream.CanSeek)
			{
				stream = new StreamWrapper(stream, settings.NonSeekableStreamBufferSize);
				_isStreamWrapped = true;
			}
			_stream = stream;
			Length = _stream.Length;
			_useBuffering = _settings.BufferingPolicy != BufferingPolicy.DontUseBuffering && !_isStreamWrapped && !(_stream is MemoryStream);
			if (_useBuffering)
			{
				PrepareBuffer();
			}
		}

		public byte ReadByte()
		{
			if (_useBuffering)
			{
				if (!EnsureBufferIsReadyForReading())
				{
					throw new EndOfStreamException();
				}
				byte result = _buffer[_bufferPosition];
				Position++;
				return result;
			}
			int num = _stream.ReadByte();
			if (num < 0)
			{
				throw new EndOfStreamException();
			}
			return (byte)num;
		}

		public sbyte ReadSByte()
		{
			return (sbyte)ReadByte();
		}

		public byte[] ReadBytes(int count)
		{
			if (_isStreamWrapped && count > _settings.NonSeekableStreamIncrementalBytesReadingThreshold)
			{
				List<byte[]> list = new List<byte[]>();
				while (count > 0)
				{
					byte[] array = ReadBytesInternal(Math.Min(count, _settings.NonSeekableStreamIncrementalBytesReadingStep));
					if (array.Length == 0)
					{
						break;
					}
					count -= array.Length;
					list.Add(array);
				}
				return list.SelectMany((byte[] bytes) => bytes).ToArray();
			}
			return ReadBytesInternal(count);
		}

		public ushort ReadWord()
		{
			byte[] array = ReadBytes(2);
			if (array.Length < 2)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a WORD.", 2L, array.Length);
			}
			return (ushort)((array[0] << 8) + array[1]);
		}

		public uint ReadDword()
		{
			byte[] array = ReadBytes(4);
			if (array.Length < 4)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a DWORD.", 4L, array.Length);
			}
			return (uint)((array[0] << 24) + (array[1] << 16) + (array[2] << 8) + array[3]);
		}

		public short ReadInt16()
		{
			byte[] array = ReadBytes(2);
			if (array.Length < 2)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a INT16.", 2L, array.Length);
			}
			return (short)((array[0] << 8) + array[1]);
		}

		public string ReadString(int count)
		{
			byte[] bytes = ReadBytesInternal(count);
			return SmfConstants.DefaultTextEncoding.GetString(bytes);
		}

		public int ReadVlqNumber()
		{
			return (int)ReadVlqLongNumber();
		}

		public long ReadVlqLongNumber()
		{
			long num = 0L;
			try
			{
				byte b;
				do
				{
					b = ReadByte();
					num = (num << 7) + (b & 0x7F);
				}
				while (b >> 7 != 0);
				return num;
			}
			catch (EndOfStreamException innerException)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a variable-length quantity number.", innerException);
			}
		}

		public uint Read3ByteDword()
		{
			byte[] array = ReadBytes(3);
			if (array.Length < 3)
			{
				throw new NotEnoughBytesException("Not enough bytes in the stream to read a 3-byte DWORD.", 3L, array.Length);
			}
			return (uint)((array[0] << 16) + (array[1] << 8) + array[2]);
		}

		private byte[] ReadBytesInternal(int count)
		{
			if (count == 0)
			{
				return EmptyByteArray;
			}
			if (_useBuffering)
			{
				return ReadBytesWithBuffering(count);
			}
			return ReadBytesWithoutBuffering(count);
		}

		private byte[] ReadBytesWithBuffering(int count)
		{
			if (!EnsureBufferIsReadyForReading())
			{
				return EmptyByteArray;
			}
			if (_bufferPosition + count <= _bufferSize)
			{
				return ReadBytesFromBuffer(count);
			}
			int num = _bufferSize - _bufferPosition;
			if (num == 0)
			{
				return EmptyByteArray;
			}
			byte[] array = ReadBytesFromBuffer(num);
			byte[] array2 = ReadBytesWithBuffering(count - num);
			byte[] array3 = new byte[array.Length + array2.Length];
			Buffer.BlockCopy(array, 0, array3, 0, array.Length);
			Buffer.BlockCopy(array2, 0, array3, array.Length, array2.Length);
			return array3;
		}

		private byte[] ReadBytesFromBuffer(int count)
		{
			byte[] array = new byte[count];
			Buffer.BlockCopy(_buffer, _bufferPosition, array, 0, count);
			Position += count;
			return array;
		}

		private byte[] ReadBytesWithoutBuffering(int count)
		{
			byte[] array = new byte[count];
			int num = 0;
			do
			{
				int num2 = _stream.Read(array, num, count);
				if (num2 == 0)
				{
					break;
				}
				num += num2;
				count -= num2;
			}
			while (count > 0);
			if (num != array.Length)
			{
				byte[] array2 = new byte[num];
				Buffer.BlockCopy(array, 0, array2, 0, num);
				array = array2;
			}
			return array;
		}

		private bool EnsureBufferIsReadyForReading()
		{
			if (EndReached)
			{
				return false;
			}
			if (_position < _bufferStart || _position >= _bufferStart + _bufferSize)
			{
				_stream.Position = _position / _buffer.Length * _buffer.Length;
				_bufferStart = _stream.Position;
				int num = 0;
				int num2 = _buffer.Length;
				do
				{
					int num3 = _stream.Read(_buffer, num, num2);
					if (num3 == 0)
					{
						break;
					}
					num += num3;
					num2 -= num3;
				}
				while (num2 > 0);
				if (num == 0)
				{
					return false;
				}
				_bufferPosition = (int)(_position % _buffer.Length);
				_bufferSize = num;
				return true;
			}
			return _bufferSize > 0;
		}

		private void PrepareBuffer()
		{
			if (!_useBuffering)
			{
				return;
			}
			switch (_settings.BufferingPolicy)
			{
			case BufferingPolicy.BufferAllData:
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					_stream.CopyTo(memoryStream);
					_buffer = memoryStream.ToArray();
				}
				_bufferStart = 0L;
				_bufferSize = _buffer.Length;
				break;
			}
			case BufferingPolicy.UseFixedSizeBuffer:
				_buffer = new byte[_settings.BufferSize];
				break;
			case BufferingPolicy.UseCustomBuffer:
				if (_settings.Buffer == null)
				{
					throw new InvalidOperationException($"Buffer is null for {_settings.BufferingPolicy} buffering policy.");
				}
				_buffer = _settings.Buffer;
				break;
			case BufferingPolicy.DontUseBuffering:
				break;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				_disposed = true;
			}
		}
	}
	public sealed class MidiWriter : IDisposable
	{
		private readonly WriterSettings _settings;

		private readonly Stream _stream;

		private readonly byte[] _numberBuffer = new byte[9];

		private readonly bool _useBuffering;

		private byte[] _buffer;

		private int _bufferPosition;

		private bool _disposed;

		public MidiWriter(Stream stream, WriterSettings settings)
		{
			ThrowIfArgument.IsNull("stream", stream);
			ThrowIfArgument.IsNull("settings", settings);
			_settings = settings;
			_stream = stream;
			_useBuffering = settings.UseBuffering;
			if (_useBuffering)
			{
				PrepareBuffer();
			}
		}

		public void WriteByte(byte value)
		{
			if (_useBuffering)
			{
				if (_bufferPosition == _buffer.Length)
				{
					FlushBuffer();
				}
				_buffer[_bufferPosition] = value;
				_bufferPosition++;
			}
			else
			{
				_stream.WriteByte(value);
			}
		}

		public void WriteBytes(byte[] bytes)
		{
			ThrowIfArgument.IsNull("bytes", bytes);
			WriteBytes(bytes, 0, bytes.Length);
		}

		public void WriteSByte(sbyte value)
		{
			WriteByte((byte)value);
		}

		public void WriteWord(ushort value)
		{
			_numberBuffer[0] = (byte)((value >> 8) & 0xFF);
			_numberBuffer[1] = (byte)(value & 0xFF);
			WriteBytes(_numberBuffer, 0, 2);
		}

		public void WriteDword(uint value)
		{
			_numberBuffer[0] = (byte)((value >> 24) & 0xFF);
			_numberBuffer[1] = (byte)((value >> 16) & 0xFF);
			_numberBuffer[2] = (byte)((value >> 8) & 0xFF);
			_numberBuffer[3] = (byte)(value & 0xFF);
			WriteBytes(_numberBuffer, 0, 4);
		}

		public void WriteInt16(short value)
		{
			_numberBuffer[0] = (byte)((value >> 8) & 0xFF);
			_numberBuffer[1] = (byte)(value & 0xFF);
			WriteBytes(_numberBuffer, 0, 2);
		}

		public void WriteString(string value)
		{
			char[] array = value?.ToCharArray();
			if (array != null && array.Length != 0)
			{
				byte[] bytes = SmfConstants.DefaultTextEncoding.GetBytes(array);
				WriteBytes(bytes);
			}
		}

		public void WriteVlqNumber(int value)
		{
			WriteVlqNumber((long)value);
		}

		public void WriteVlqNumber(long value)
		{
			int vlqBytes = value.GetVlqBytes(_numberBuffer);
			WriteBytes(_numberBuffer, _numberBuffer.Length - vlqBytes, vlqBytes);
		}

		public void Write3ByteDword(uint value)
		{
			_numberBuffer[0] = (byte)((value >> 16) & 0xFF);
			_numberBuffer[1] = (byte)((value >> 8) & 0xFF);
			_numberBuffer[2] = (byte)(value & 0xFF);
			WriteBytes(_numberBuffer, 0, 3);
		}

		private void PrepareBuffer()
		{
			if (_useBuffering)
			{
				_buffer = new byte[_settings.BufferSize];
			}
		}

		private void WriteBytes(byte[] bytes, int offset, int length)
		{
			if (_useBuffering)
			{
				WriteBytesWithBuffering(bytes, offset, length);
			}
			else
			{
				_stream.Write(bytes, offset, length);
			}
		}

		private void FlushBuffer()
		{
			_stream.Write(_buffer, 0, _bufferPosition);
			_bufferPosition = 0;
		}

		private void WriteBytesWithBuffering(byte[] bytes, int offset, int length)
		{
			if (_bufferPosition + length <= _buffer.Length)
			{
				WriteBytesToBuffer(bytes, offset, length);
				return;
			}
			int num = _buffer.Length - _bufferPosition;
			WriteBytesToBuffer(bytes, offset, num);
			FlushBuffer();
			WriteBytesWithBuffering(bytes, offset + num, length - num);
		}

		private void WriteBytesToBuffer(byte[] bytes, int offset, int length)
		{
			Buffer.BlockCopy(bytes, offset, _buffer, _bufferPosition, length);
			_bufferPosition += length;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		private void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}
			if (disposing)
			{
				if (_useBuffering)
				{
					FlushBuffer();
				}
				_stream.Flush();
			}
			_disposed = true;
		}
	}
	public enum BufferingPolicy
	{
		UseFixedSizeBuffer,
		DontUseBuffering,
		UseCustomBuffer,
		BufferAllData
	}
	public delegate string DecodeTextCallback(byte[] bytes, ReadingSettings settings);
	public enum EndOfTrackStoringPolicy
	{
		Omit,
		Store
	}
	public enum ExtraTrackChunkPolicy : byte
	{
		Read,
		Skip
	}
	public enum InvalidChannelEventParameterValuePolicy : byte
	{
		Abort,
		ReadValid,
		SnapToLimits
	}
	public enum InvalidChunkSizePolicy : byte
	{
		Abort,
		Ignore
	}
	public enum InvalidMetaEventParameterValuePolicy
	{
		Abort,
		SnapToLimits
	}
	public enum InvalidSystemCommonEventParameterValuePolicy
	{
		Abort,
		SnapToLimits
	}
	public enum MissedEndOfTrackPolicy : byte
	{
		Ignore,
		Abort
	}
	public enum NoHeaderChunkPolicy
	{
		Abort,
		Ignore
	}
	public enum NotEnoughBytesPolicy
	{
		Abort,
		Ignore
	}
	public sealed class ReaderSettings
	{
		private int _nonSeekableStreamBufferSize = 1024;

		private int _nonSeekableStreamIncrementalBytesReadingThreshold = 16384;

		private int _nonSeekableStreamIncrementalBytesReadingStep = 2048;

		private int _bufferSize = 4096;

		private BufferingPolicy _bufferingPolicy;

		public int NonSeekableStreamBufferSize
		{
			get
			{
				return _nonSeekableStreamBufferSize;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				_nonSeekableStreamBufferSize = value;
			}
		}

		public int NonSeekableStreamIncrementalBytesReadingThreshold
		{
			get
			{
				return _nonSeekableStreamIncrementalBytesReadingThreshold;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Value is negative.");
				_nonSeekableStreamIncrementalBytesReadingThreshold = value;
			}
		}

		public int NonSeekableStreamIncrementalBytesReadingStep
		{
			get
			{
				return _nonSeekableStreamIncrementalBytesReadingStep;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				_nonSeekableStreamIncrementalBytesReadingStep = value;
			}
		}

		public BufferingPolicy BufferingPolicy
		{
			get
			{
				return _bufferingPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_bufferingPolicy = value;
			}
		}

		public int BufferSize
		{
			get
			{
				return _bufferSize;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				_bufferSize = value;
			}
		}

		public byte[] Buffer { get; set; }
	}
	public class ReadingSettings
	{
		private UnexpectedTrackChunksCountPolicy _unexpectedTrackChunksCountPolicy;

		private ExtraTrackChunkPolicy _extraTrackChunkPolicy;

		private UnknownChunkIdPolicy _unknownChunkIdPolicy;

		private MissedEndOfTrackPolicy _missedEndOfTrackPolicy;

		private SilentNoteOnPolicy _silentNoteOnPolicy;

		private InvalidChunkSizePolicy _invalidChunkSizePolicy;

		private UnknownFileFormatPolicy _unknownFileFormatPolicy;

		private UnknownChannelEventPolicy _unknownChannelEventPolicy;

		private InvalidChannelEventParameterValuePolicy _invalidChannelEventParameterValuePolicy;

		private InvalidMetaEventParameterValuePolicy _invalidMetaEventParameterValuePolicy;

		private InvalidSystemCommonEventParameterValuePolicy _invalidSystemCommonEventParameterValuePolicy;

		private NotEnoughBytesPolicy _notEnoughBytesPolicy;

		private NoHeaderChunkPolicy _noHeaderChunkPolicy;

		private ZeroLengthDataPolicy _zeroLengthDataPolicy;

		private EndOfTrackStoringPolicy _endOfTrackStoringPolicy;

		public UnexpectedTrackChunksCountPolicy UnexpectedTrackChunksCountPolicy
		{
			get
			{
				return _unexpectedTrackChunksCountPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_unexpectedTrackChunksCountPolicy = value;
			}
		}

		public ExtraTrackChunkPolicy ExtraTrackChunkPolicy
		{
			get
			{
				return _extraTrackChunkPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_extraTrackChunkPolicy = value;
			}
		}

		public UnknownChunkIdPolicy UnknownChunkIdPolicy
		{
			get
			{
				return _unknownChunkIdPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_unknownChunkIdPolicy = value;
			}
		}

		public MissedEndOfTrackPolicy MissedEndOfTrackPolicy
		{
			get
			{
				return _missedEndOfTrackPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_missedEndOfTrackPolicy = value;
			}
		}

		public SilentNoteOnPolicy SilentNoteOnPolicy
		{
			get
			{
				return _silentNoteOnPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_silentNoteOnPolicy = value;
			}
		}

		public InvalidChunkSizePolicy InvalidChunkSizePolicy
		{
			get
			{
				return _invalidChunkSizePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_invalidChunkSizePolicy = value;
			}
		}

		public UnknownFileFormatPolicy UnknownFileFormatPolicy
		{
			get
			{
				return _unknownFileFormatPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_unknownFileFormatPolicy = value;
			}
		}

		public UnknownChannelEventPolicy UnknownChannelEventPolicy
		{
			get
			{
				return _unknownChannelEventPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_unknownChannelEventPolicy = value;
			}
		}

		public UnknownChannelEventCallback UnknownChannelEventCallback { get; set; }

		public InvalidChannelEventParameterValuePolicy InvalidChannelEventParameterValuePolicy
		{
			get
			{
				return _invalidChannelEventParameterValuePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_invalidChannelEventParameterValuePolicy = value;
			}
		}

		public InvalidMetaEventParameterValuePolicy InvalidMetaEventParameterValuePolicy
		{
			get
			{
				return _invalidMetaEventParameterValuePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_invalidMetaEventParameterValuePolicy = value;
			}
		}

		public InvalidSystemCommonEventParameterValuePolicy InvalidSystemCommonEventParameterValuePolicy
		{
			get
			{
				return _invalidSystemCommonEventParameterValuePolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_invalidSystemCommonEventParameterValuePolicy = value;
			}
		}

		public NotEnoughBytesPolicy NotEnoughBytesPolicy
		{
			get
			{
				return _notEnoughBytesPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_notEnoughBytesPolicy = value;
			}
		}

		public NoHeaderChunkPolicy NoHeaderChunkPolicy
		{
			get
			{
				return _noHeaderChunkPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_noHeaderChunkPolicy = value;
			}
		}

		public ChunkTypesCollection CustomChunkTypes { get; set; }

		public EventTypesCollection CustomMetaEventTypes { get; set; }

		public Encoding TextEncoding { get; set; } = SmfConstants.DefaultTextEncoding;

		public DecodeTextCallback DecodeTextCallback { get; set; }

		public ZeroLengthDataPolicy ZeroLengthDataPolicy
		{
			get
			{
				return _zeroLengthDataPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_zeroLengthDataPolicy = value;
			}
		}

		public EndOfTrackStoringPolicy EndOfTrackStoringPolicy
		{
			get
			{
				return _endOfTrackStoringPolicy;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_endOfTrackStoringPolicy = value;
			}
		}

		public ReaderSettings ReaderSettings { get; set; } = new ReaderSettings();
	}
	public enum SilentNoteOnPolicy : byte
	{
		NoteOff,
		NoteOn
	}
	public enum UnexpectedTrackChunksCountPolicy
	{
		Ignore,
		Abort
	}
	public sealed class UnknownChannelEventAction
	{
		public static readonly UnknownChannelEventAction Abort = new UnknownChannelEventAction(UnknownChannelEventInstruction.Abort, 0);

		public UnknownChannelEventInstruction Instruction { get; }

		public int DataBytesToSkipCount { get; }

		private UnknownChannelEventAction(UnknownChannelEventInstruction instruction, int dataBytesToSkipCount)
		{
			Instruction = instruction;
			DataBytesToSkipCount = dataBytesToSkipCount;
		}

		public static UnknownChannelEventAction SkipData(int dataBytesToSkipCount)
		{
			ThrowIfArgument.IsNegative("dataBytesToSkipCount", dataBytesToSkipCount, "Count of data bytes to skip is negative.");
			return new UnknownChannelEventAction(UnknownChannelEventInstruction.SkipData, dataBytesToSkipCount);
		}
	}
	public delegate UnknownChannelEventAction UnknownChannelEventCallback(FourBitNumber statusByte, FourBitNumber channel);
	public enum UnknownChannelEventInstruction
	{
		Abort,
		SkipData
	}
	public enum UnknownChannelEventPolicy
	{
		Abort,
		SkipStatusByte,
		SkipStatusByteAndOneDataByte,
		SkipStatusByteAndTwoDataBytes,
		UseCallback
	}
	public enum UnknownChunkIdPolicy : byte
	{
		ReadAsUnknownChunk,
		Skip,
		Abort
	}
	public enum UnknownFileFormatPolicy
	{
		Ignore,
		Abort
	}
	public enum ZeroLengthDataPolicy
	{
		ReadAsEmptyObject,
		ReadAsNull
	}
	public sealed class SmpteTimeDivision : TimeDivision
	{
		public SmpteFormat Format { get; }

		public byte Resolution { get; }

		public SmpteTimeDivision(SmpteFormat format, byte resolution)
		{
			ThrowIfArgument.IsInvalidEnumValue("format", format);
			Format = format;
			Resolution = resolution;
		}

		public static bool operator ==(SmpteTimeDivision timeDivision1, SmpteTimeDivision timeDivision2)
		{
			if ((object)timeDivision1 == timeDivision2)
			{
				return true;
			}
			if ((object)timeDivision1 == null || (object)timeDivision2 == null)
			{
				return false;
			}
			if (timeDivision1.Format == timeDivision2.Format)
			{
				return timeDivision1.Resolution == timeDivision2.Resolution;
			}
			return false;
		}

		public static bool operator !=(SmpteTimeDivision timeDivision1, SmpteTimeDivision timeDivision2)
		{
			return !(timeDivision1 == timeDivision2);
		}

		internal override short ToInt16()
		{
			return (short)(-DataTypesUtilities.Combine((byte)Format, Resolution));
		}

		public override TimeDivision Clone()
		{
			return new SmpteTimeDivision(Format, Resolution);
		}

		public override string ToString()
		{
			return $"{Format} frames / sec, {Resolution} subdivisions / frame";
		}

		public override bool Equals(object obj)
		{
			return this == obj as SmpteTimeDivision;
		}

		public override int GetHashCode()
		{
			return (17 * 23 + Format.GetHashCode()) * 23 + Resolution.GetHashCode();
		}
	}
	public sealed class TicksPerQuarterNoteTimeDivision : TimeDivision
	{
		public const short DefaultTicksPerQuarterNote = 96;

		public short TicksPerQuarterNote { get; } = 96;

		public TicksPerQuarterNoteTimeDivision()
		{
		}

		public TicksPerQuarterNoteTimeDivision(short ticksPerQuarterNote)
		{
			ThrowIfArgument.IsNegative("ticksPerQuarterNote", ticksPerQuarterNote, "Ticks per quarter-note must be non-negative number.");
			TicksPerQuarterNote = ticksPerQuarterNote;
		}

		public static bool operator ==(TicksPerQuarterNoteTimeDivision timeDivision1, TicksPerQuarterNoteTimeDivision timeDivision2)
		{
			if ((object)timeDivision1 == timeDivision2)
			{
				return true;
			}
			if ((object)timeDivision1 == null || (object)timeDivision2 == null)
			{
				return false;
			}
			return timeDivision1.TicksPerQuarterNote == timeDivision2.TicksPerQuarterNote;
		}

		public static bool operator !=(TicksPerQuarterNoteTimeDivision timeDivision1, TicksPerQuarterNoteTimeDivision timeDivision2)
		{
			return !(timeDivision1 == timeDivision2);
		}

		internal override short ToInt16()
		{
			return TicksPerQuarterNote;
		}

		public override TimeDivision Clone()
		{
			return new TicksPerQuarterNoteTimeDivision(TicksPerQuarterNote);
		}

		public override string ToString()
		{
			return $"{TicksPerQuarterNote} ticks/qnote";
		}

		public override bool Equals(object obj)
		{
			return this == obj as TicksPerQuarterNoteTimeDivision;
		}

		public override int GetHashCode()
		{
			return TicksPerQuarterNote.GetHashCode();
		}
	}
	public abstract class TimeDivision
	{
		internal abstract short ToInt16();

		public abstract TimeDivision Clone();
	}
	internal static class TimeDivisionFactory
	{
		internal static TimeDivision GetTimeDivision(short division)
		{
			if (division < 0)
			{
				division = (short)(-division);
				return new SmpteTimeDivision((SmpteFormat)division.GetHead(), division.GetTail());
			}
			return new TicksPerQuarterNoteTimeDivision(division);
		}
	}
	internal static class ArrayUtilities
	{
		internal static bool Equals<T>(T[] array1, T[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			return array1.SequenceEqual(array2);
		}

		internal static int GetHashCode<T>(T[] array)
		{
			return ((IStructuralEquatable)array)?.GetHashCode((IEqualityComparer)EqualityComparer<T>.Default) ?? 0;
		}
	}
	public static class ControlUtilities
	{
		public static ControlName GetControlName(this ControlChangeEvent controlChangeEvent)
		{
			ThrowIfArgument.IsNull("controlChangeEvent", controlChangeEvent);
			return GetControlName(controlChangeEvent.ControlNumber);
		}

		public static SevenBitNumber AsSevenBitNumber(this ControlName controlName)
		{
			ThrowIfArgument.IsInvalidEnumValue("controlName", controlName);
			return (SevenBitNumber)(byte)controlName;
		}

		public static ControlChangeEvent GetControlChangeEvent(this ControlName controlName, SevenBitNumber controlValue, FourBitNumber channel)
		{
			ThrowIfArgument.IsInvalidEnumValue("controlName", controlName);
			return new ControlChangeEvent(controlName.AsSevenBitNumber(), controlValue)
			{
				Channel = channel
			};
		}

		private static ControlName GetControlName(SevenBitNumber controlNumber)
		{
			byte b = controlNumber;
			if (!Enum.IsDefined(typeof(ControlName), b))
			{
				return ControlName.Undefined;
			}
			return (ControlName)b;
		}
	}
	internal static class FileUtilities
	{
		private const uint GENERIC_READ = 2147483648u;

		private const uint GENERIC_WRITE = 1073741824u;

		private const uint CREATE_NEW = 1u;

		private const uint CREATE_ALWAYS = 2u;

		private const uint OPEN_EXISTING = 3u;

		private const uint FILE_SHARE_NONE = 0u;

		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode, IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

		internal static FileStream OpenFileForRead(string filePath)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("filePath", filePath, "File path");
			try
			{
				return File.OpenRead(filePath);
			}
			catch (PathTooLongException)
			{
				return new FileStream(GetFileHandle(filePath, 2147483648u, 3u), FileAccess.Read);
			}
		}

		internal static FileStream OpenFileForWrite(string filePath, bool overwriteFile)
		{
			ThrowIfArgument.IsNullOrWhiteSpaceString("filePath", filePath, "File path");
			try
			{
				return File.Open(filePath, (!overwriteFile) ? FileMode.CreateNew : FileMode.Create);
			}
			catch (PathTooLongException)
			{
				return new FileStream(GetFileHandle(filePath, 1073741824u, (!overwriteFile) ? 1u : 2u), FileAccess.Write);
			}
		}

		private static SafeFileHandle GetFileHandle(string filePath, uint fileAccess, uint creationDisposition)
		{
			SafeFileHandle safeFileHandle = CreateFile("\\\\?\\" + filePath, fileAccess, 0u, IntPtr.Zero, creationDisposition, 0u, IntPtr.Zero);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (safeFileHandle.IsInvalid)
			{
				throw new Win32Exception(lastWin32Error);
			}
			return safeFileHandle;
		}
	}
	public static class MidiFileUtilities
	{
		public static IEnumerable<FourBitNumber> GetChannels(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.GetTrackChunks().GetChannels();
		}

		internal static IEnumerable<MidiEvent> GetEvents(this MidiFile midiFile)
		{
			return midiFile.GetTrackChunks().SelectMany((TrackChunk c) => c.Events);
		}
	}
	public static class NoteEventUtilities
	{
		public static NoteName GetNoteName(this NoteEvent noteEvent)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			return NoteUtilities.GetNoteName(noteEvent.NoteNumber);
		}

		public static int GetNoteOctave(this NoteEvent noteEvent)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			return NoteUtilities.GetNoteOctave(noteEvent.NoteNumber);
		}

		public static void SetNoteNumber(this NoteEvent noteEvent, NoteName noteName, int octave)
		{
			ThrowIfArgument.IsNull("noteEvent", noteEvent);
			noteEvent.NoteNumber = NoteUtilities.GetNoteNumber(noteName, octave);
		}

		public static bool IsNoteOnCorrespondToNoteOff(NoteOnEvent noteOnEvent, NoteOffEvent noteOffEvent)
		{
			ThrowIfArgument.IsNull("noteOnEvent", noteOnEvent);
			ThrowIfArgument.IsNull("noteOffEvent", noteOffEvent);
			if ((byte)noteOnEvent.Channel == (byte)noteOffEvent.Channel)
			{
				return (byte)noteOnEvent.NoteNumber == (byte)noteOffEvent.NoteNumber;
			}
			return false;
		}
	}
	public static class SmfConstants
	{
		public static Encoding DefaultTextEncoding => Encoding.ASCII;
	}
	internal sealed class SmpteData
	{
		private const byte MaxHours = 23;

		private const byte MaxMinutes = 59;

		private const byte MaxSeconds = 59;

		private const byte MaxSubFrames = 99;

		private const int FormatMask = 96;

		private const int FormatOffset = 5;

		private const int HoursMask = 31;

		private static readonly Dictionary<SmpteFormat, byte> MaxFrames = new Dictionary<SmpteFormat, byte>
		{
			[SmpteFormat.TwentyFour] = 23,
			[SmpteFormat.TwentyFive] = 24,
			[SmpteFormat.ThirtyDrop] = 28,
			[SmpteFormat.Thirty] = 29
		};

		private static readonly SmpteFormat[] Formats = new SmpteFormat[4]
		{
			SmpteFormat.TwentyFour,
			SmpteFormat.TwentyFive,
			SmpteFormat.ThirtyDrop,
			SmpteFormat.Thirty
		};

		private SmpteFormat _format = SmpteFormat.TwentyFour;

		private byte _hours;

		private byte _minutes;

		private byte _seconds;

		private byte _frames;

		private byte _subFrames;

		public SmpteFormat Format
		{
			get
			{
				return _format;
			}
			set
			{
				ThrowIfArgument.IsInvalidEnumValue("value", value);
				_format = value;
			}
		}

		public byte Hours
		{
			get
			{
				return _hours;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", value, 23, $"Hours number is out of valid range (0-{(byte)23}).");
				_hours = value;
			}
		}

		public byte Minutes
		{
			get
			{
				return _minutes;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", value, 59, $"Minutes number is out of valid range (0-{(byte)59}).");
				_minutes = value;
			}
		}

		public byte Seconds
		{
			get
			{
				return _seconds;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", value, 59, $"Seconds number is out of valid range (0-{(byte)59}).");
				_seconds = value;
			}
		}

		public byte Frames
		{
			get
			{
				return _frames;
			}
			set
			{
				byte b = MaxFrames[Format];
				ThrowIfArgument.IsGreaterThan("value", value, b, $"Frames number is out of valid range (0-{b}).");
				_frames = value;
			}
		}

		public byte SubFrames
		{
			get
			{
				return _subFrames;
			}
			set
			{
				ThrowIfArgument.IsGreaterThan("value", value, 99, $"Sub-frames number is out of valid range (0-{(byte)99}).");
				_subFrames = value;
			}
		}

		public SmpteData()
		{
		}

		public SmpteData(SmpteFormat format, byte hours, byte minutes, byte seconds, byte frames, byte subFrames)
		{
			Format = format;
			Hours = hours;
			Minutes = minutes;
			Seconds = seconds;
			Frames = frames;
			SubFrames = subFrames;
		}

		public static SmpteData Read(Func<byte> byteReader, Func<byte, string, byte, byte> valueProcessor)
		{
			byte formatAndHours = byteReader();
			SmpteFormat format = GetFormat(formatAndHours);
			byte hours = valueProcessor(GetHours(formatAndHours), "Hours", 23);
			byte minutes = valueProcessor(byteReader(), "Minutes", 59);
			byte seconds = valueProcessor(byteReader(), "Seconds", 59);
			byte frames = valueProcessor(byteReader(), "Frames", MaxFrames[format]);
			byte subFrames = valueProcessor(byteReader(), "SubFrames", 99);
			return new SmpteData(format, hours, minutes, seconds, frames, subFrames);
		}

		public void Write(Action<byte> byteWriter)
		{
			byteWriter(GetFormatAndHours());
			byteWriter(Minutes);
			byteWriter(Seconds);
			byteWriter(Frames);
			byteWriter(SubFrames);
		}

		internal static SmpteFormat GetFormat(byte formatAndHours)
		{
			return Formats[(formatAndHours & 0x60) >> 5];
		}

		internal static byte GetHours(byte formatAndHours)
		{
			return (byte)(formatAndHours & 0x1F);
		}

		internal byte GetFormatAndHours()
		{
			return GetFormatAndHours(Format, Hours);
		}

		internal static byte GetFormatAndHours(SmpteFormat smpteFormat, byte hours)
		{
			byte b = 0;
			switch (smpteFormat)
			{
			case SmpteFormat.TwentyFive:
				b = 1;
				break;
			case SmpteFormat.ThirtyDrop:
				b = 2;
				break;
			case SmpteFormat.Thirty:
				b = 3;
				break;
			}
			return (byte)((b << 5) | hours);
		}
	}
	public static class TrackChunkUtilities
	{
		public static IEnumerable<TrackChunk> GetTrackChunks(this MidiFile midiFile)
		{
			ThrowIfArgument.IsNull("midiFile", midiFile);
			return midiFile.Chunks.OfType<TrackChunk>();
		}

		public static TrackChunk Merge(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return ConvertTrackChunks(trackChunks, MidiFileFormat.SingleTrack).First();
		}

		public static IEnumerable<TrackChunk> Explode(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return ConvertTrackChunks(new TrackChunk[1] { trackChunk }, MidiFileFormat.MultiTrack);
		}

		public static IEnumerable<FourBitNumber> GetChannels(this TrackChunk trackChunk)
		{
			ThrowIfArgument.IsNull("trackChunk", trackChunk);
			return (from e in trackChunk.Events.OfType<ChannelEvent>()
				select e.Channel).Distinct().ToArray();
		}

		public static IEnumerable<FourBitNumber> GetChannels(this IEnumerable<TrackChunk> trackChunks)
		{
			ThrowIfArgument.IsNull("trackChunks", trackChunks);
			return trackChunks.Where((TrackChunk c) => c != null).SelectMany(GetChannels).Distinct()
				.ToArray();
		}

		private static IEnumerable<TrackChunk> ConvertTrackChunks(IEnumerable<TrackChunk> trackChunks, MidiFileFormat format)
		{
			return ChunksConverterFactory.GetConverter(format).Convert(trackChunks).OfType<TrackChunk>();
		}
	}
	public sealed class WriterSettings
	{
		private int _bufferSize = 4096;

		public bool UseBuffering { get; set; } = true;

		public int BufferSize
		{
			get
			{
				return _bufferSize;
			}
			set
			{
				ThrowIfArgument.IsNonpositive("value", value, "Value is zero or negative.");
				_bufferSize = value;
			}
		}
	}
	public class WritingSettings
	{
		public bool UseRunningStatus { get; set; }

		public bool NoteOffAsSilentNoteOn { get; set; }

		public bool DeleteDefaultTimeSignature { get; set; }

		public bool DeleteDefaultKeySignature { get; set; }

		public bool DeleteDefaultSetTempo { get; set; }

		public bool DeleteUnknownMetaEvents { get; set; }

		public bool DeleteUnknownChunks { get; set; }

		public bool WriteHeaderChunk { get; set; } = true;

		public EventTypesCollection CustomMetaEventTypes { get; set; }

		public Encoding TextEncoding { get; set; } = SmfConstants.DefaultTextEncoding;

		public WriterSettings WriterSettings { get; set; } = new WriterSettings();
	}
}
namespace Melanchall.DryWetMidi.Composing
{
	internal sealed class AddAnchorAction : PatternAction
	{
		public object Anchor { get; }

		public AddAnchorAction()
			: this(null)
		{
		}

		public AddAnchorAction(object anchor)
		{
			Anchor = anchor;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State == PatternActionState.Enabled)
			{
				context.AnchorTime(Anchor, time);
			}
			return PatternActionResult.DoNothing;
		}

		public override PatternAction Clone()
		{
			return new AddAnchorAction(Anchor);
		}
	}
	internal sealed class AddChordAction : PatternAction
	{
		public ChordDescriptor ChordDescriptor { get; }

		public AddChordAction(ChordDescriptor chordDescriptor)
		{
			ChordDescriptor = chordDescriptor;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State == PatternActionState.Excluded)
			{
				return PatternActionResult.DoNothing;
			}
			context.SaveTime(time);
			long chordLength = LengthConverter.ConvertFrom(ChordDescriptor.Length, time, context.TempoMap);
			if (base.State == PatternActionState.Disabled)
			{
				return new PatternActionResult(time + chordLength);
			}
			return new PatternActionResult(time + chordLength, ChordDescriptor.Notes.Select((Melanchall.DryWetMidi.MusicTheory.Note d) => new Melanchall.DryWetMidi.Interaction.Note(d.NoteNumber, chordLength, time)
			{
				Channel = context.Channel,
				Velocity = ChordDescriptor.Velocity
			}));
		}

		public override PatternAction Clone()
		{
			return new AddChordAction(new ChordDescriptor(ChordDescriptor.Notes, ChordDescriptor.Velocity, ChordDescriptor.Length.Clone()));
		}
	}
	internal sealed class AddNoteAction : PatternAction
	{
		public NoteDescriptor NoteDescriptor { get; }

		public AddNoteAction(NoteDescriptor noteDescriptor)
		{
			NoteDescriptor = noteDescriptor;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State == PatternActionState.Excluded)
			{
				return PatternActionResult.DoNothing;
			}
			context.SaveTime(time);
			long num = LengthConverter.ConvertFrom(NoteDescriptor.Length, time, context.TempoMap);
			if (base.State == PatternActionState.Disabled)
			{
				return new PatternActionResult(time + num);
			}
			Melanchall.DryWetMidi.Interaction.Note note = new Melanchall.DryWetMidi.Interaction.Note(NoteDescriptor.Note.NoteNumber, num, time)
			{
				Channel = context.Channel,
				Velocity = NoteDescriptor.Velocity
			};
			return new PatternActionResult(time + num, new Melanchall.DryWetMidi.Interaction.Note[1] { note });
		}

		public override PatternAction Clone()
		{
			return new AddNoteAction(new NoteDescriptor(NoteDescriptor.Note, NoteDescriptor.Velocity, NoteDescriptor.Length.Clone()));
		}
	}
	internal sealed class AddPatternAction : PatternAction
	{
		public Pattern Pattern { get; }

		public AddPatternAction(Pattern pattern)
		{
			Pattern = pattern;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			context.SaveTime(time);
			PatternContext context2 = new PatternContext(context.TempoMap, context.Channel);
			return Pattern.InvokeActions(time, context2);
		}

		public override PatternAction Clone()
		{
			return new AddPatternAction(Pattern.Clone());
		}
	}
	internal sealed class AddTextEventAction<TEvent> : PatternAction where TEvent : BaseTextEvent
	{
		public string Text { get; }

		public AddTextEventAction(string text)
		{
			Text = text;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TimedEvent timedEvent = new TimedEvent((BaseTextEvent)Activator.CreateInstance(typeof(TEvent), Text), time);
			return new PatternActionResult(time, new TimedEvent[1] { timedEvent });
		}

		public override PatternAction Clone()
		{
			return new AddTextEventAction<TEvent>(Text);
		}
	}
	internal enum AnchorPosition
	{
		First,
		Last,
		Nth
	}
	internal sealed class MoveToAnchorAction : PatternAction
	{
		public object Anchor { get; }

		public AnchorPosition AnchorPosition { get; }

		public int Index { get; }

		public MoveToAnchorAction(AnchorPosition position)
			: this(null, position)
		{
		}

		public MoveToAnchorAction(object anchor, AnchorPosition position)
			: this(anchor, position, -1)
		{
		}

		public MoveToAnchorAction(AnchorPosition position, int index)
			: this(null, position, index)
		{
		}

		public MoveToAnchorAction(object anchor, AnchorPosition position, int index)
		{
			Anchor = anchor;
			AnchorPosition = position;
			Index = index;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			IReadOnlyList<long> anchorTimes = context.GetAnchorTimes(Anchor);
			long value = 0L;
			switch (AnchorPosition)
			{
			case AnchorPosition.First:
				value = anchorTimes.First();
				break;
			case AnchorPosition.Last:
				value = anchorTimes.Last();
				break;
			case AnchorPosition.Nth:
				value = anchorTimes[Index];
				break;
			}
			return new PatternActionResult(value);
		}

		public override PatternAction Clone()
		{
			return new MoveToAnchorAction(Anchor, AnchorPosition, Index);
		}
	}
	internal sealed class MoveToTimeAction : PatternAction
	{
		public ITimeSpan Time { get; }

		public MoveToTimeAction()
			: this(null)
		{
		}

		public MoveToTimeAction(ITimeSpan time)
		{
			Time = time;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			if (Time != null)
			{
				context.SaveTime(time);
			}
			return new PatternActionResult((Time != null) ? new long?(TimeConverter.ConvertFrom(Time, context.TempoMap)) : context.RestoreTime());
		}

		public override PatternAction Clone()
		{
			return new MoveToTimeAction(Time?.Clone());
		}
	}
	internal sealed class SetGeneralMidi2ProgramAction : PatternAction
	{
		public GeneralMidi2Program Program { get; }

		public SetGeneralMidi2ProgramAction(GeneralMidi2Program program)
		{
			Program = program;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			IEnumerable<TimedEvent> events = from e in Program.GetProgramEvents(context.Channel)
				select new TimedEvent(e, time);
			return new PatternActionResult(time, events);
		}

		public override PatternAction Clone()
		{
			return new SetGeneralMidi2ProgramAction(Program);
		}
	}
	internal sealed class SetGeneralMidiProgramAction : PatternAction
	{
		public GeneralMidiProgram Program { get; }

		public SetGeneralMidiProgramAction(GeneralMidiProgram program)
		{
			Program = program;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TimedEvent timedEvent = new TimedEvent(Program.GetProgramEvent(context.Channel), time);
			return new PatternActionResult(time, new TimedEvent[1] { timedEvent });
		}

		public override PatternAction Clone()
		{
			return new SetGeneralMidiProgramAction(Program);
		}
	}
	internal sealed class SetProgramNumberAction : PatternAction
	{
		public SevenBitNumber ProgramNumber { get; }

		public SetProgramNumberAction(SevenBitNumber programNumber)
		{
			ProgramNumber = programNumber;
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TimedEvent timedEvent = new TimedEvent(new ProgramChangeEvent(ProgramNumber)
			{
				Channel = context.Channel
			}, time);
			return new PatternActionResult(time, new TimedEvent[1] { timedEvent });
		}

		public override PatternAction Clone()
		{
			return new SetProgramNumberAction(ProgramNumber);
		}
	}
	internal abstract class StepAction : PatternAction
	{
		public ITimeSpan Step { get; }

		public StepAction(ITimeSpan step)
		{
			Step = step;
		}
	}
	internal sealed class StepBackAction : StepAction
	{
		public StepBackAction(ITimeSpan step)
			: base(step)
		{
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			TempoMap tempoMap = context.TempoMap;
			context.SaveTime(time);
			return new PatternActionResult(Math.Max(TimeConverter.ConvertFrom(((MidiTimeSpan)time).Subtract(base.Step, TimeSpanMode.TimeLength), tempoMap), 0L));
		}

		public override PatternAction Clone()
		{
			return new StepBackAction(base.Step.Clone());
		}
	}
	internal sealed class StepForwardAction : StepAction
	{
		public StepForwardAction(ITimeSpan step)
			: base(step)
		{
		}

		public override PatternActionResult Invoke(long time, PatternContext context)
		{
			if (base.State != PatternActionState.Enabled)
			{
				return PatternActionResult.DoNothing;
			}
			context.SaveTime(time);
			return new PatternActionResult(time + LengthConverter.ConvertFrom(base.Step, time, context.TempoMap));
		}

		public override PatternAction Clone()
		{
			return new StepForwardAction(base.Step.Clone());
		}
	}
	public sealed class ChordDescriptor
	{
		public IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> Notes { get; }

		public SevenBitNumber Velocity { get; }

		public ITimeSpan Length { get; }

		public ChordDescriptor(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, SevenBitNumber velocity, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("length", length);
			Notes = notes;
			Velocity = velocity;
			Length = length;
		}

		public static bool operator ==(ChordDescriptor chordDescriptor1, ChordDescriptor chordDescriptor2)
		{
			if ((object)chordDescriptor1 == chordDescriptor2)
			{
				return true;
			}
			if ((object)chordDescriptor1 == null || (object)chordDescriptor2 == null)
			{
				return false;
			}
			if (chordDescriptor1.Notes.SequenceEqual(chordDescriptor2.Notes) && (byte)chordDescriptor1.Velocity == (byte)chordDescriptor2.Velocity)
			{
				return chordDescriptor1.Length.Equals(chordDescriptor2.Length);
			}
			return false;
		}

		public static bool operator !=(ChordDescriptor chordDescriptor1, ChordDescriptor chordDescriptor2)
		{
			return !(chordDescriptor1 == chordDescriptor2);
		}

		public override string ToString()
		{
			return string.Format("{0} [{1}]: {2}", string.Join(" ", Notes), Velocity, Length);
		}

		public override bool Equals(object obj)
		{
			return this == obj as ChordDescriptor;
		}

		public override int GetHashCode()
		{
			return ((17 * 23 + Notes.GetHashCode()) * 23 + Velocity.GetHashCode()) * 23 + Length.GetHashCode();
		}
	}
	public sealed class NoteDescriptor
	{
		public Melanchall.DryWetMidi.MusicTheory.Note Note { get; }

		public SevenBitNumber Velocity { get; }

		public ITimeSpan Length { get; }

		public NoteDescriptor(Melanchall.DryWetMidi.MusicTheory.Note note, SevenBitNumber velocity, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("note", note);
			ThrowIfArgument.IsNull("length", length);
			Note = note;
			Velocity = velocity;
			Length = length;
		}

		public static bool operator ==(NoteDescriptor noteDescriptor1, NoteDescriptor noteDescriptor2)
		{
			if ((object)noteDescriptor1 == noteDescriptor2)
			{
				return true;
			}
			if ((object)noteDescriptor1 == null || (object)noteDescriptor2 == null)
			{
				return false;
			}
			if (noteDescriptor1.Note == noteDescriptor2.Note && (byte)noteDescriptor1.Velocity == (byte)noteDescriptor2.Velocity)
			{
				return noteDescriptor1.Length.Equals(noteDescriptor2.Length);
			}
			return false;
		}

		public static bool operator !=(NoteDescriptor noteDescriptor1, NoteDescriptor noteDescriptor2)
		{
			return !(noteDescriptor1 == noteDescriptor2);
		}

		public override string ToString()
		{
			return $"{Note} [{Velocity}]: {Length}";
		}

		public override bool Equals(object obj)
		{
			return this == obj as NoteDescriptor;
		}

		public override int GetHashCode()
		{
			return ((17 * 23 + Note.GetHashCode()) * 23 + Velocity.GetHashCode()) * 23 + Length.GetHashCode();
		}
	}
	public sealed class Pattern
	{
		internal IEnumerable<PatternAction> Actions { get; }

		internal Pattern(IEnumerable<PatternAction> actions)
		{
			Actions = actions;
		}

		public TrackChunk ToTrackChunk(TempoMap tempoMap, FourBitNumber channel)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			PatternContext context = new PatternContext(tempoMap, channel);
			PatternActionResult patternActionResult = InvokeActions(0L, context);
			IEnumerable<ITimedObject> events = patternActionResult.Events;
			return (events ?? Enumerable.Empty<TimedEvent>()).Concat(patternActionResult.Notes ?? Enumerable.Empty<Melanchall.DryWetMidi.Interaction.Note>()).ToTrackChunk();
		}

		public TrackChunk ToTrackChunk(TempoMap tempoMap)
		{
			return ToTrackChunk(tempoMap, FourBitNumber.MinValue);
		}

		public MidiFile ToFile(TempoMap tempoMap, FourBitNumber channel)
		{
			ThrowIfArgument.IsNull("tempoMap", tempoMap);
			TrackChunk trackChunk = ToTrackChunk(tempoMap, channel);
			MidiFile midiFile = new MidiFile(trackChunk);
			midiFile.ReplaceTempoMap(tempoMap);
			return midiFile;
		}

		public MidiFile ToFile(TempoMap tempoMap)
		{
			return ToFile(tempoMap, FourBitNumber.MinValue);
		}

		public Pattern Clone()
		{
			return new Pattern(Actions.Select((PatternAction a) => a.Clone()).ToList());
		}

		internal PatternActionResult InvokeActions(long time, PatternContext context)
		{
			List<Melanchall.DryWetMidi.Interaction.Note> list = new List<Melanchall.DryWetMidi.Interaction.Note>();
			List<TimedEvent> list2 = new List<TimedEvent>();
			foreach (PatternAction action in Actions)
			{
				PatternActionResult patternActionResult = action.Invoke(time, context);
				long? time2 = patternActionResult.Time;
				if (time2.HasValue)
				{
					time = time2.Value;
				}
				IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes = patternActionResult.Notes;
				if (notes != null)
				{
					list.AddRange(notes);
				}
				IEnumerable<TimedEvent> events = patternActionResult.Events;
				if (events != null)
				{
					list2.AddRange(events);
				}
			}
			return new PatternActionResult(time, list, list2);
		}
	}
	internal abstract class PatternAction
	{
		public PatternActionState State { get; set; }

		public abstract PatternActionResult Invoke(long time, PatternContext context);

		public abstract PatternAction Clone();
	}
	internal sealed class PatternActionResult
	{
		public static readonly PatternActionResult DoNothing = new PatternActionResult();

		public long? Time { get; }

		public IEnumerable<Melanchall.DryWetMidi.Interaction.Note> Notes { get; }

		public IEnumerable<TimedEvent> Events { get; }

		public PatternActionResult()
		{
		}

		public PatternActionResult(long? time)
			: this(time, null, null)
		{
		}

		public PatternActionResult(long? time, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes)
			: this(time, notes, null)
		{
		}

		public PatternActionResult(long? time, IEnumerable<TimedEvent> events)
			: this(time, null, events)
		{
		}

		public PatternActionResult(long? time, IEnumerable<Melanchall.DryWetMidi.Interaction.Note> notes, IEnumerable<TimedEvent> events)
		{
			Time = time;
			Notes = notes;
			Events = events;
		}
	}
	public enum PatternActionState
	{
		Enabled,
		Disabled,
		Excluded
	}
	public sealed class PatternBuilder
	{
		public static readonly SevenBitNumber DefaultVelocity = Melanchall.DryWetMidi.Interaction.Note.DefaultVelocity;

		public static readonly ITimeSpan DefaultNoteLength = MusicalTimeSpan.Quarter;

		public static readonly ITimeSpan DefaultStep = MusicalTimeSpan.Quarter;

		public static readonly Octave DefaultOctave = Octave.Middle;

		public static readonly Melanchall.DryWetMidi.MusicTheory.Note DefaultRootNote = Octave.Middle.C;

		private readonly List<PatternAction> _actions = new List<PatternAction>();

		private readonly Dictionary<object, int> _anchorCounters = new Dictionary<object, int>();

		private int _globalAnchorsCounter;

		public SevenBitNumber Velocity { get; private set; } = DefaultVelocity;

		public ITimeSpan NoteLength { get; private set; } = DefaultNoteLength;

		public ITimeSpan Step { get; private set; } = DefaultStep;

		public Octave Octave { get; private set; } = DefaultOctave;

		public Melanchall.DryWetMidi.MusicTheory.Note RootNote { get; private set; } = DefaultRootNote;

		public PatternBuilder()
		{
		}

		public PatternBuilder(Pattern pattern)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ReplayPattern(pattern);
		}

		public PatternBuilder Note(Interval interval)
		{
			return Note(interval, NoteLength, Velocity);
		}

		public PatternBuilder Note(Interval interval, ITimeSpan length)
		{
			return Note(interval, length, Velocity);
		}

		public PatternBuilder Note(Interval interval, SevenBitNumber velocity)
		{
			return Note(interval, NoteLength, velocity);
		}

		public PatternBuilder Note(Interval interval, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("interval", interval);
			return Note(RootNote.Transpose(interval), length, velocity);
		}

		public PatternBuilder Note(NoteName noteName)
		{
			return Note(noteName, NoteLength, Velocity);
		}

		public PatternBuilder Note(NoteName noteName, ITimeSpan length)
		{
			return Note(noteName, length, Velocity);
		}

		public PatternBuilder Note(NoteName noteName, SevenBitNumber velocity)
		{
			return Note(noteName, NoteLength, velocity);
		}

		public PatternBuilder Note(NoteName noteName, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsInvalidEnumValue("noteName", noteName);
			return Note(Octave.GetNote(noteName), length, velocity);
		}

		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note)
		{
			return Note(note, NoteLength, Velocity);
		}

		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note, ITimeSpan length)
		{
			return Note(note, length, Velocity);
		}

		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note, SevenBitNumber velocity)
		{
			return Note(note, NoteLength, velocity);
		}

		public PatternBuilder Note(Melanchall.DryWetMidi.MusicTheory.Note note, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("note", note);
			ThrowIfArgument.IsNull("length", length);
			return AddAction(new AddNoteAction(new NoteDescriptor(note, velocity, length)));
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return Chord(chord.ResolveNotes(Octave), NoteLength, Velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			return Chord(chord.ResolveNotes(octave), NoteLength, Velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("length", length);
			return Chord(chord.ResolveNotes(Octave), length, Velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave, ITimeSpan length)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			ThrowIfArgument.IsNull("length", length);
			return Chord(chord.ResolveNotes(octave), length, Velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			return Chord(chord.ResolveNotes(Octave), NoteLength, velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			return Chord(chord.ResolveNotes(octave), NoteLength, velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("length", length);
			return Chord(chord.ResolveNotes(Octave), length, velocity);
		}

		public PatternBuilder Chord(Melanchall.DryWetMidi.MusicTheory.Chord chord, Octave octave, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("chord", chord);
			ThrowIfArgument.IsNull("octave", octave);
			ThrowIfArgument.IsNull("length", length);
			return Chord(chord.ResolveNotes(octave), length, velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName)
		{
			return Chord(intervals, rootNoteName, NoteLength, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName, ITimeSpan length)
		{
			return Chord(intervals, rootNoteName, length, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName, SevenBitNumber velocity)
		{
			return Chord(intervals, rootNoteName, NoteLength, velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, NoteName rootNoteName, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsInvalidEnumValue("rootNoteName", rootNoteName);
			return Chord(intervals, Octave.GetNote(rootNoteName), length, velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, Melanchall.DryWetMidi.MusicTheory.Note rootNote)
		{
			return Chord(intervals, rootNote, NoteLength, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> interval, Melanchall.DryWetMidi.MusicTheory.Note rootNote, ITimeSpan length)
		{
			return Chord(interval, rootNote, length, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, Melanchall.DryWetMidi.MusicTheory.Note rootNote, SevenBitNumber velocity)
		{
			return Chord(intervals, rootNote, NoteLength, velocity);
		}

		public PatternBuilder Chord(IEnumerable<Interval> intervals, Melanchall.DryWetMidi.MusicTheory.Note rootNote, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("intervals", intervals);
			ThrowIfArgument.IsNull("rootNote", rootNote);
			return Chord(new Melanchall.DryWetMidi.MusicTheory.Note[1] { rootNote }.Concat(intervals.Where((Interval i) => i != null).Select(rootNote.Transpose)), length, velocity);
		}

		public PatternBuilder Chord(IEnumerable<NoteName> noteNames)
		{
			return Chord(noteNames, NoteLength, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<NoteName> noteNames, ITimeSpan length)
		{
			return Chord(noteNames, length, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<NoteName> noteNames, SevenBitNumber velocity)
		{
			return Chord(noteNames, NoteLength, velocity);
		}

		public PatternBuilder Chord(IEnumerable<NoteName> noteNames, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("noteNames", noteNames);
			ThrowIfArgument.IsNull("length", length);
			return Chord(noteNames.Select((NoteName n) => Octave.GetNote(n)), length, velocity);
		}

		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes)
		{
			return Chord(notes, NoteLength, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, ITimeSpan length)
		{
			return Chord(notes, length, Velocity);
		}

		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, SevenBitNumber velocity)
		{
			return Chord(notes, NoteLength, velocity);
		}

		public PatternBuilder Chord(IEnumerable<Melanchall.DryWetMidi.MusicTheory.Note> notes, ITimeSpan length, SevenBitNumber velocity)
		{
			ThrowIfArgument.IsNull("notes", notes);
			ThrowIfArgument.IsNull("length", length);
			return AddAction(new AddChordAction(new ChordDescriptor(notes, velocity, length)));
		}

		public PatternBuilder Pattern(Pattern pattern)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			return AddAction(new AddPatternAction(pattern));
		}

		public PatternBuilder Anchor(object anchor)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			return AddAction(new AddAnchorAction(anchor));
		}

		public PatternBuilder Anchor()
		{
			return AddAction(new AddAnchorAction());
		}

		public PatternBuilder MoveToFirstAnchor(object anchor)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			if (GetAnchorCounter(anchor) < 1)
			{
				throw new ArgumentException($"There are no anchors with the '{anchor}' key.", "anchor");
			}
			return AddAction(new MoveToAnchorAction(anchor, AnchorPosition.First));
		}

		public PatternBuilder MoveToFirstAnchor()
		{
			if (GetAnchorCounter(null) < 1)
			{
				throw new InvalidOperationException("There are no anchors.");
			}
			return AddAction(new MoveToAnchorAction(AnchorPosition.First));
		}

		public PatternBuilder MoveToLastAnchor(object anchor)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			if (GetAnchorCounter(anchor) < 1)
			{
				throw new ArgumentException($"There are no anchors with the '{anchor}' key.", "anchor");
			}
			return AddAction(new MoveToAnchorAction(anchor, AnchorPosition.Last));
		}

		public PatternBuilder MoveToLastAnchor()
		{
			if (GetAnchorCounter(null) < 1)
			{
				throw new InvalidOperationException("There are no anchors.");
			}
			return AddAction(new MoveToAnchorAction(AnchorPosition.Last));
		}

		public PatternBuilder MoveToNthAnchor(object anchor, int index)
		{
			ThrowIfArgument.IsNull("anchor", anchor);
			int anchorCounter = GetAnchorCounter(anchor);
			ThrowIfArgument.IsOutOfRange("index", index, 0, anchorCounter - 1, "Index is out of range.");
			return AddAction(new MoveToAnchorAction(anchor, AnchorPosition.Nth, index));
		}

		public PatternBuilder MoveToNthAnchor(int index)
		{
			int anchorCounter = GetAnchorCounter(null);
			ThrowIfArgument.IsOutOfRange("index", index, 0, anchorCounter - 1, "Index is out of range.");
			return AddAction(new MoveToAnchorAction(AnchorPosition.Nth, index));
		}

		public PatternBuilder StepForward(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			return AddAction(new StepForwardAction(step));
		}

		public PatternBuilder StepForward()
		{
			return AddAction(new StepForwardAction(Step));
		}

		public PatternBuilder StepBack(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			return AddAction(new StepBackAction(step));
		}

		public PatternBuilder StepBack()
		{
			return AddAction(new StepBackAction(Step));
		}

		public PatternBuilder MoveToTime(ITimeSpan time)
		{
			ThrowIfArgument.IsNull("time", time);
			return AddAction(new MoveToTimeAction(time));
		}

		public PatternBuilder MoveToPreviousTime()
		{
			return AddAction(new MoveToTimeAction());
		}

		public PatternBuilder Repeat(int actionsCount, int repetitionsCount)
		{
			ThrowIfArgument.IsNegative("actionsCount", actionsCount, "Actions count is negative.");
			ThrowIfArgument.IsGreaterThan("actionsCount", actionsCount, _actions.Count, "Actions count is greater than existing actions count.");
			ThrowIfArgument.IsNegative("repetitionsCount", repetitionsCount, "Repetitions count is negative.");
			return RepeatActions(actionsCount, repetitionsCount);
		}

		public PatternBuilder Repeat(int repetitionsCount)
		{
			ThrowIfArgument.IsNegative("repetitionsCount", repetitionsCount, "Repetitions count is negative.");
			if (!_actions.Any())
			{
				throw new InvalidOperationException("There is no action to repeat.");
			}
			return RepeatActions(1, repetitionsCount);
		}

		public PatternBuilder Repeat()
		{
			if (!_actions.Any())
			{
				throw new InvalidOperationException("There is no action to repeat.");
			}
			return RepeatActions(1, 1);
		}

		public PatternBuilder Lyrics(string text)
		{
			ThrowIfArgument.IsNull("text", text);
			return AddAction(new AddTextEventAction<LyricEvent>(text));
		}

		public PatternBuilder Marker(string marker)
		{
			ThrowIfArgument.IsNull("marker", marker);
			return AddAction(new AddTextEventAction<MarkerEvent>(marker));
		}

		public PatternBuilder ProgramChange(SevenBitNumber programNumber)
		{
			return AddAction(new SetProgramNumberAction(programNumber));
		}

		public PatternBuilder ProgramChange(GeneralMidiProgram program)
		{
			ThrowIfArgument.IsInvalidEnumValue("program", program);
			return AddAction(new SetGeneralMidiProgramAction(program));
		}

		public PatternBuilder ProgramChange(GeneralMidi2Program program)
		{
			ThrowIfArgument.IsInvalidEnumValue("program", program);
			return AddAction(new SetGeneralMidi2ProgramAction(program));
		}

		public PatternBuilder SetRootNote(Melanchall.DryWetMidi.MusicTheory.Note rootNote)
		{
			ThrowIfArgument.IsNull("rootNote", rootNote);
			RootNote = rootNote;
			return this;
		}

		public PatternBuilder SetVelocity(SevenBitNumber velocity)
		{
			Velocity = velocity;
			return this;
		}

		public PatternBuilder SetNoteLength(ITimeSpan length)
		{
			ThrowIfArgument.IsNull("length", length);
			NoteLength = length;
			return this;
		}

		public PatternBuilder SetStep(ITimeSpan step)
		{
			ThrowIfArgument.IsNull("step", step);
			Step = step;
			return this;
		}

		public PatternBuilder SetOctave(Octave octave)
		{
			ThrowIfArgument.IsNull("octave", octave);
			Octave = octave;
			return this;
		}

		public Pattern Build()
		{
			return new Pattern(_actions.ToList());
		}

		public PatternBuilder ReplayPattern(Pattern pattern)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			foreach (PatternAction action in pattern.Actions)
			{
				AddAction(action);
			}
			return this;
		}

		private PatternBuilder AddAction(PatternAction patternAction)
		{
			if (patternAction is AddAnchorAction addAnchorAction)
			{
				UpdateAnchorsCounters(addAnchorAction.Anchor);
			}
			_actions.Add(patternAction);
			return this;
		}

		private int GetAnchorCounter(object anchor)
		{
			if (anchor == null)
			{
				return _globalAnchorsCounter;
			}
			if (!_anchorCounters.TryGetValue(anchor, out var value))
			{
				throw new ArgumentException($"Anchor {anchor} doesn't exist.", "anchor");
			}
			return value;
		}

		private void UpdateAnchorsCounters(object anchor)
		{
			_globalAnchorsCounter++;
			if (anchor != null)
			{
				if (!_anchorCounters.ContainsKey(anchor))
				{
					_anchorCounters.Add(anchor, 0);
				}
				_anchorCounters[anchor]++;
			}
		}

		private PatternBuilder RepeatActions(int actionsCount, int repetitionsCount)
		{
			List<PatternAction> actionsToRepeat = _actions.Skip(_actions.Count - actionsCount).ToList();
			foreach (PatternAction item in Enumerable.Range(0, repetitionsCount).SelectMany((int i) => actionsToRepeat))
			{
				AddAction(item);
			}
			return this;
		}
	}
	internal sealed class PatternContext
	{
		private readonly Stack<long> _timeHistory = new Stack<long>();

		private readonly Dictionary<object, List<long>> _anchors = new Dictionary<object, List<long>>();

		private readonly List<long> _anchorsList = new List<long>();

		public TempoMap TempoMap { get; }

		public FourBitNumber Channel { get; }

		public PatternContext(TempoMap tempoMap, FourBitNumber channel)
		{
			TempoMap = tempoMap;
			Channel = channel;
		}

		public void SaveTime(long time)
		{
			_timeHistory.Push(time);
		}

		public long? RestoreTime()
		{
			if (!_timeHistory.Any())
			{
				return null;
			}
			return _timeHistory.Pop();
		}

		public void AnchorTime(object anchor, long time)
		{
			GetAnchorTimesList(anchor).Add(time);
			if (anchor != null)
			{
				_anchorsList.Add(time);
			}
		}

		public IReadOnlyList<long> GetAnchorTimes(object anchor)
		{
			return GetAnchorTimesList(anchor).AsReadOnly();
		}

		private List<long> GetAnchorTimesList(object anchor)
		{
			if (anchor == null)
			{
				return _anchorsList;
			}
			if (!_anchors.TryGetValue(anchor, out var value))
			{
				_anchors.Add(anchor, value = new List<long>());
			}
			return value;
		}
	}
	public static class PatternUtilities
	{
		private static readonly NoteSelection AllNotesSelection = (int i, NoteDescriptor d) => true;

		private static readonly ChordSelection AllChordsSelection = (int i, ChordDescriptor d) => true;

		public static Pattern TransformNotes(this Pattern pattern, NoteTransformation noteTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("noteTransformation", noteTransformation);
			return pattern.TransformNotes(AllNotesSelection, noteTransformation, recursive);
		}

		public static Pattern TransformNotes(this Pattern pattern, NoteSelection noteSelection, NoteTransformation noteTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("noteSelection", noteSelection);
			ThrowIfArgument.IsNull("noteTransformation", noteTransformation);
			ObjectWrapper<int> noteIndexWrapper = new ObjectWrapper<int>();
			return TransformNotes(pattern, noteIndexWrapper, noteSelection, noteTransformation, recursive);
		}

		public static Pattern TransformChords(this Pattern pattern, ChordTransformation chordTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("chordTransformation", chordTransformation);
			return pattern.TransformChords(AllChordsSelection, chordTransformation, recursive);
		}

		public static Pattern TransformChords(this Pattern pattern, ChordSelection chordSelection, ChordTransformation chordTransformation, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("chordSelection", chordSelection);
			ThrowIfArgument.IsNull("chordTransformation", chordTransformation);
			ObjectWrapper<int> chordIndexWrapper = new ObjectWrapper<int>();
			return TransformChords(pattern, chordIndexWrapper, chordSelection, chordTransformation, recursive);
		}

		public static IEnumerable<Pattern> SplitAtAnchor(this Pattern pattern, object anchor, bool removeEmptyPatterns = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("anchor", anchor);
			return SplitAtActions(pattern, (PatternAction a) => (a as AddAnchorAction)?.Anchor == anchor, removeEmptyPatterns);
		}

		public static IEnumerable<Pattern> SplitAtAllAnchors(this Pattern pattern, bool removeEmptyPatterns = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			return SplitAtActions(pattern, (PatternAction a) => a is AddAnchorAction, removeEmptyPatterns);
		}

		public static IEnumerable<Pattern> SplitAtMarker(this Pattern pattern, string marker, bool removeEmptyPatterns = true, StringComparison stringComparison = StringComparison.CurrentCulture)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("marker", marker);
			ThrowIfArgument.IsInvalidEnumValue("stringComparison", stringComparison);
			return SplitAtActions(pattern, (PatternAction a) => (a as AddTextEventAction<MarkerEvent>)?.Text.Equals(marker, stringComparison) ?? false, removeEmptyPatterns);
		}

		public static IEnumerable<Pattern> SplitAtAllMarkers(this Pattern pattern, bool removeEmptyPatterns = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			return SplitAtActions(pattern, (PatternAction a) => a is AddTextEventAction<MarkerEvent>, removeEmptyPatterns);
		}

		public static Pattern CombineInSequence(this IEnumerable<Pattern> patterns)
		{
			ThrowIfArgument.IsNull("patterns", patterns);
			PatternBuilder patternBuilder = new PatternBuilder();
			foreach (Pattern item in patterns.Where((Pattern p) => p != null))
			{
				patternBuilder.Pattern(item);
			}
			return patternBuilder.Build();
		}

		public static Pattern CombineInParallel(this IEnumerable<Pattern> patterns)
		{
			ThrowIfArgument.IsNull("patterns", patterns);
			PatternBuilder patternBuilder = new PatternBuilder();
			using (IEnumerator<Pattern> enumerator = patterns.Where((Pattern p) => p != null).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					patternBuilder.Pattern(enumerator.Current);
					while (enumerator.MoveNext())
					{
						patternBuilder.MoveToPreviousTime().Pattern(enumerator.Current);
					}
				}
			}
			return patternBuilder.Build();
		}

		public static void SetNotesState(this Pattern pattern, NoteSelection noteSelection, PatternActionState state, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("noteSelection", noteSelection);
			ThrowIfArgument.IsInvalidEnumValue("state", state);
			ObjectWrapper<int> noteIndexWrapper = new ObjectWrapper<int>();
			SetNotesState(pattern, noteIndexWrapper, noteSelection, state, recursive);
		}

		public static void SetChordsState(this Pattern pattern, ChordSelection chordSelection, PatternActionState state, bool recursive = true)
		{
			ThrowIfArgument.IsNull("pattern", pattern);
			ThrowIfArgument.IsNull("chordSelection", chordSelection);
			ThrowIfArgument.IsInvalidEnumValue("state", state);
			ObjectWrapper<int> chordIndexWrapper = new ObjectWrapper<int>();
			SetChordsState(pattern, chordIndexWrapper, chordSelection, state, recursive);
		}

		private static IEnumerable<Pattern> SplitAtActions(Pattern pattern, Predicate<PatternAction> actionSelector, bool removeEmptyPatterns)
		{
			List<PatternAction> list = new List<PatternAction>();
			foreach (PatternAction action in pattern.Actions)
			{
				if (!actionSelector(action))
				{
					list.Add(action);
					continue;
				}
				if (list.Any() || !removeEmptyPatterns)
				{
					yield return new Pattern(list.AsReadOnly());
				}
				list = new List<PatternAction>();
			}
			if (list.Any())
			{
				yield return new Pattern(list.AsReadOnly());
			}
		}

		private static Pattern TransformNotes(Pattern pattern, ObjectWrapper<int> noteIndexWrapper, NoteSelection noteSelection, NoteTransformation noteTransformation, bool recursive)
		{
			return new Pattern(pattern.Actions.Select(delegate(PatternAction a)
			{
				if (a is AddNoteAction addNoteAction && noteSelection(noteIndexWrapper.Object++, addNoteAction.NoteDescriptor))
				{
					return new AddNoteAction(noteTransformation(addNoteAction.NoteDescriptor));
				}
				AddPatternAction addPatternAction = a as AddPatternAction;
				return (addPatternAction != null && recursive) ? new AddPatternAction(TransformNotes(addPatternAction.Pattern, noteIndexWrapper, noteSelection, noteTransformation, recursive)) : a.Clone();
			}).ToList());
		}

		private static Pattern TransformChords(Pattern pattern, ObjectWrapper<int> chordIndexWrapper, ChordSelection chordSelection, ChordTransformation chordTransformation, bool recursive)
		{
			return new Pattern(pattern.Actions.Select(delegate(PatternAction a)
			{
				if (a is AddChordAction addChordAction && chordSelection(chordIndexWrapper.Object++, addChordAction.ChordDescriptor))
				{
					return new AddChordAction(chordTransformation(addChordAction.ChordDescriptor));
				}
				AddPatternAction addPatternAction = a as AddPatternAction;
				return (addPatternAction != null && recursive) ? new AddPatternAction(TransformChords(addPatternAction.Pattern, chordIndexWrapper, chordSelection, chordTransformation, recursive)) : a.Clone();
			}).ToList());
		}

		private static void SetNotesState(Pattern pattern, ObjectWrapper<int> noteIndexWrapper, NoteSelection noteSelection, PatternActionState state, bool recursive)
		{
			foreach (PatternAction action in pattern.Actions)
			{
				if (action is AddNoteAction addNoteAction && noteSelection(noteIndexWrapper.Object++, addNoteAction.NoteDescriptor))
				{
					addNoteAction.State = state;
				}
				AddPatternAction addPatternAction = action as AddPatternAction;
				if (addPatternAction != null && recursive)
				{
					SetNotesState(addPatternAction.Pattern, noteIndexWrapper, noteSelection, state, recursive);
				}
			}
		}

		private static void SetChordsState(Pattern pattern, ObjectWrapper<int> chordIndexWrapper, ChordSelection chordSelection, PatternActionState state, bool recursive)
		{
			foreach (PatternAction action in pattern.Actions)
			{
				if (action is AddChordAction addChordAction && chordSelection(chordIndexWrapper.Object++, addChordAction.ChordDescriptor))
				{
					addChordAction.State = state;
				}
				AddPatternAction addPatternAction = action as AddPatternAction;
				if (addPatternAction != null && recursive)
				{
					SetChordsState(addPatternAction.Pattern, chordIndexWrapper, chordSelection, state, recursive);
				}
			}
		}
	}
	public delegate bool ChordSelection(int noteIndex, ChordDescriptor chordDescriptor);
	public delegate bool NoteSelection(int noteIndex, NoteDescriptor noteDescriptor);
	public delegate ChordDescriptor ChordTransformation(ChordDescriptor chordDescriptor);
	public delegate NoteDescriptor NoteTransformation(NoteDescriptor noteDescriptor);
}
namespace Melanchall.DryWetMidi.Common
{
	internal sealed class CircularBuffer<T>
	{
		private readonly int _capacity;

		private readonly T[] _buffer;

		private int _start;

		private int _index = -1;

		private int _position;

		public bool IsFull { get; private set; }

		public CircularBuffer(int capacity)
		{
			_buffer = new T[capacity];
			_capacity = capacity;
		}

		public void Add(T value)
		{
			if (_position >= GetItemsCount())
			{
				_position = Math.Min(_position + 1, _capacity);
			}
			if (IsFull || _index == _capacity - 1)
			{
				_start = (_start + 1) % _capacity;
				IsFull = true;
			}
			_index = (_index + 1) % _capacity;
			_buffer[_index] = value;
		}

		public T[] MovePositionForward(int offset)
		{
			T[] array = GetItems().Skip(_position).Take(offset).ToArray();
			_position += array.Length;
			return array;
		}

		public void MovePositionBack(int offset)
		{
			if (offset > _position)
			{
				throw new InvalidOperationException("Failed to move position back beyond the start of the buffer.");
			}
			_position -= offset;
		}

		private int GetItemsCount()
		{
			if (!IsFull)
			{
				return _index + 1;
			}
			return _capacity;
		}

		private IEnumerable<T> GetItems()
		{
			IEnumerable<T> first = Enumerable.Empty<T>();
			if (IsFull)
			{
				if (_start == 0)
				{
					return _buffer;
				}
				first = GetItems(_start, _capacity - 1);
			}
			return first.Concat(GetItems(0, _index));
		}

		private IEnumerable<T> GetItems(int start, int end)
		{
			for (int i = start; i <= end; i++)
			{
				yield return _buffer[i];
			}
		}
	}
	public enum ControlName : byte
	{
		BankSelect = 0,
		Modulation = 1,
		BreathController = 2,
		FootController = 4,
		PortamentoTime = 5,
		DataEntryMsb = 6,
		ChannelVolume = 7,
		Balance = 8,
		Pan = 10,
		ExpressionController = 11,
		EffectControl1 = 12,
		EffectControl2 = 13,
		GeneralPurposeController1 = 16,
		GeneralPurposeController2 = 17,
		GeneralPurposeController3 = 18,
		GeneralPurposeController4 = 19,
		LsbForBankSelect = 32,
		LsbForModulation = 33,
		LsbForBreathController = 34,
		LsbForController3 = 35,
		LsbForFootController = 36,
		LsbForPortamentoTime = 37,
		LsbForDataEntry = 38,
		LsbForChannelVolume = 39,
		LsbForBalance = 40,
		LsbForController9 = 41,
		LsbForPan = 42,
		LsbForExpressionController = 43,
		LsbForEffectControl1 = 44,
		LsbForEffectControl2 = 45,
		LsbForController14 = 46,
		LsbForController15 = 47,
		LsbForGeneralPurposeController1 = 48,
		LsbForGeneralPurposeController2 = 49,
		LsbForGeneralPurposeController3 = 50,
		LsbForGeneralPurposeController4 = 51,
		LsbForController20 = 52,
		LsbForController21 = 53,
		LsbForController22 = 54,
		LsbForController23 = 55,
		LsbForController24 = 56,
		LsbForController25 = 57,
		LsbForController26 = 58,
		LsbForController27 = 59,
		LsbForController28 = 60,
		LsbForController29 = 61,
		LsbForController30 = 62,
		LsbForController31 = 63,
		DamperPedal = 64,
		Portamento = 65,
		Sostenuto = 66,
		SoftPedal = 67,
		LegatoFootswitch = 68,
		Hold2 = 69,
		SoundController1 = 70,
		SoundController2 = 71,
		SoundController3 = 72,
		SoundController4 = 73,
		SoundController5 = 74,
		SoundController6 = 75,
		SoundController7 = 76,
		SoundController8 = 77,
		SoundController9 = 78,
		SoundController10 = 79,
		GeneralPurposeController5 = 80,
		GeneralPurposeController6 = 81,
		GeneralPurposeController7 = 82,
		GeneralPurposeController8 = 83,
		PortamentoControl = 84,
		HighResolutionVelocityPrefix = 88,
		Effects1Depth = 91,
		Effects2Depth = 92,
		Effects3Depth = 93,
		Effects4Depth = 94,
		Effects5Depth = 95,
		DataIncrement = 96,
		DataDecrement = 97,
		NonRegisteredParameterNumberLsb = 98,
		NonRegisteredParameterNumberMsb = 99,
		RegisteredParameterNumberLsb = 100,
		RegisteredParameterNumberMsb = 101,
		AllSoundOff = 120,
		ResetAllControllers = 121,
		LocalControl = 122,
		AllNotesOff = 123,
		OmniModeOff = 124,
		OmniModeOn = 125,
		MonoModeOn = 126,
		PolyModeOn = 127,
		Undefined = byte.MaxValue
	}
	internal static class DataTypesUtilities
	{
		public static byte Combine(FourBitNumber head, FourBitNumber tail)
		{
			return (byte)(((byte)head << 4) | (byte)tail);
		}

		public static ushort Combine(SevenBitNumber head, SevenBitNumber tail)
		{
			return (ushort)(((byte)head << 7) | (byte)tail);
		}

		public static ushort Combine(byte head, byte tail)
		{
			return (ushort)((head << 8) | tail);
		}

		public static ushort CombineAsSevenBitNumbers(byte head, byte tail)
		{
			return (ushort)((head << 7) | tail);
		}

		public static byte CombineAsFourBitNumbers(byte head, byte tail)
		{
			return (byte)((head << 4) | tail);
		}

		public static uint Combine(ushort head, ushort tail)
		{
			return (uint)((head << 16) | tail);
		}

		public static FourBitNumber GetTail(this byte number)
		{
			return (FourBitNumber)(byte)(number & (byte)FourBitNumber.MaxValue);
		}

		public static SevenBitNumber GetTail(this ushort number)
		{
			return (SevenBitNumber)(byte)(number & (byte)SevenBitNumber.MaxValue);
		}

		public static byte GetTail(this short number)
		{
			return (byte)(number & 0xFF);
		}

		public static ushort GetTail(this uint number)
		{
			return (ushort)(number & 0xFFFF);
		}

		public static FourBitNumber GetHead(this byte number)
		{
			return (FourBitNumber)(byte)(number >> 4);
		}

		public static SevenBitNumber GetHead(this ushort number)
		{
			return (SevenBitNumber)(byte)(number >> 7);
		}

		public static byte GetHead(this short number)
		{
			return (byte)(number >> 8);
		}

		public static ushort GetHead(this uint number)
		{
			return (ushort)(number >> 16);
		}

		public static int GetVlqLength(this int number)
		{
			int num = 1;
			if (number > 127)
			{
				num++;
				if (number > 16383)
				{
					num++;
					if (number > 2097151)
					{
						num++;
						if (number > 268435455)
						{
							num++;
						}
					}
				}
			}
			return num;
		}

		public static int GetVlqLength(this long number)
		{
			int num = 1;
			if (number > 127)
			{
				num++;
				if (number > 16383)
				{
					num++;
					if (number > 2097151)
					{
						num++;
						if (number > 268435455)
						{
							num++;
							if (number > 34359738367L)
							{
								num++;
								if (number > 4398046511103L)
								{
									num++;
									if (number > 562949953421311L)
									{
										num++;
										if (number > 72057594037927935L)
										{
											num++;
										}
									}
								}
							}
						}
					}
				}
			}
			return num;
		}

		public static byte[] GetVlqBytes(this int number)
		{
			return ((long)number).GetVlqBytes();
		}

		public static byte[] GetVlqBytes(this long number)
		{
			byte[] array = new byte[number.GetVlqLength()];
			number.GetVlqBytes(array);
			return array;
		}

		public static byte GetFirstByte(this int number)
		{
			return (byte)((number >> 24) & 0xFF);
		}

		public static byte GetSecondByte(this int number)
		{
			return (byte)((number >> 16) & 0xFF);
		}

		public static byte GetThirdByte(this int number)
		{
			return (byte)((number >> 8) & 0xFF);
		}

		public static byte GetFourthByte(this int number)
		{
			return (byte)(number & 0xFF);
		}

		internal static int GetVlqBytes(this long number, byte[] buffer)
		{
			int num = 1;
			int num2 = buffer.Length - 1;
			buffer[num2--] = (byte)(number & 0x7F);
			while ((number >>= 7) > 0)
			{
				buffer[num2--] = (byte)(0x80 | (number & 0x7F));
				num++;
			}
			return num;
		}
	}
	public struct FourBitNumber : IComparable<FourBitNumber>, IConvertible
	{
		public static readonly FourBitNumber MinValue = new FourBitNumber(0);

		public static readonly FourBitNumber MaxValue = new FourBitNumber(15);

		public static readonly FourBitNumber[] Values = (from value in Enumerable.Range((byte)MinValue, (byte)MaxValue - (byte)MinValue + 1)
			select (FourBitNumber)(byte)value).ToArray();

		private const byte Min = 0;

		private const byte Max = 15;

		private readonly byte _value;

		public FourBitNumber(byte value)
		{
			ThrowIfArgument.IsOutOfRange("value", value, 0, 15, "Value is out of range valid for four-bit number.");
			_value = value;
		}

		public static bool TryParse(string input, out FourBitNumber fourBitNumber)
		{
			fourBitNumber = default(FourBitNumber);
			byte result;
			bool num = ShortByteParser.TryParse(input, 0, 15, out result).Status == ParsingStatus.Parsed;
			if (num)
			{
				fourBitNumber = (FourBitNumber)result;
			}
			return num;
		}

		public static FourBitNumber Parse(string input)
		{
			byte result;
			ParsingResult parsingResult = ShortByteParser.TryParse(input, 0, 15, out result);
			if (parsingResult.Status == ParsingStatus.Parsed)
			{
				return (FourBitNumber)result;
			}
			throw parsingResult.Exception;
		}

		public static implicit operator byte(FourBitNumber number)
		{
			return number._value;
		}

		public static explicit operator FourBitNumber(byte number)
		{
			return new FourBitNumber(number);
		}

		public int CompareTo(FourBitNumber other)
		{
			byte value = _value;
			return value.CompareTo(other._value);
		}

		public TypeCode GetTypeCode()
		{
			byte value = _value;
			return value.GetTypeCode();
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToBoolean(provider);
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToChar(provider);
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToSByte(provider);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToByte(provider);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToInt16(provider);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToUInt16(provider);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToInt32(provider);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToUInt32(provider);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToInt64(provider);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToUInt64(provider);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToSingle(provider);
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToDouble(provider);
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToDecimal(provider);
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToDateTime(provider);
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			byte value = _value;
			return value.ToString(provider);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)_value).ToType(conversionType, provider);
		}

		public override string ToString()
		{
			byte value = _value;
			return value.ToString();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is FourBitNumber))
			{
				return false;
			}
			return ((FourBitNumber)obj)._value == _value;
		}

		public override int GetHashCode()
		{
			byte value = _value;
			return value.GetHashCode();
		}
	}
	public struct SevenBitNumber : IComparable<SevenBitNumber>, IConvertible
	{
		public static readonly SevenBitNumber MinValue = new SevenBitNumber(0);

		public static readonly SevenBitNumber MaxValue = new SevenBitNumber(127);

		public static readonly SevenBitNumber[] Values = (from value in Enumerable.Range((byte)MinValue, (byte)MaxValue - (byte)MinValue + 1)
			select (SevenBitNumber)(byte)value).ToArray();

		private const byte Min = 0;

		private const byte Max = 127;

		private readonly byte _value;

		public SevenBitNumber(byte value)
		{
			ThrowIfArgument.IsOutOfRange("value", value, 0, 127, "Value is out of range valid for seven-bit number.");
			_value = value;
		}

		public static bool TryParse(string input, out SevenBitNumber sevenBitNumber)
		{
			sevenBitNumber = default(SevenBitNumber);
			byte result;
			bool num = ShortByteParser.TryParse(input, 0, 127, out result).Status == ParsingStatus.Parsed;
			if (num)
			{
				sevenBitNumber = (SevenBitNumber)result;
			}
			return num;
		}

		public static SevenBitNumber Parse(string input)
		{
			byte result;
			ParsingResult parsingResult = ShortByteParser.TryParse(input, 0, 127, out result);
			if (parsingResult.Status == ParsingStatus.Parsed)
			{
				return (SevenBitNumber)result;
			}
			throw parsingResult.Exception;
		}

		public static implicit operator byte(SevenBitNumber number)
		{
			return number._value;
		}

		public static explicit operator SevenBitNumber(byte number)
		{
			return new SevenBitNumber(number);
		}

		public int CompareTo(SevenBitNumber other)
		{
			byte value = _value;
			return value.CompareTo(other._value);
		}

		public TypeCode GetTypeCode()
		{
			byte value = _value;
			return value.GetTypeCode();
		}

		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToBoolean(provider);
		}

		char IConvertible.ToChar(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToChar(provider);
		}

		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToSByte(provider);
		}

		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToByte(provider);
		}

		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToInt16(provider);
		}

		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToUInt16(provider);
		}

		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToInt32(provider);
		}

		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToUInt32(provider);
		}

		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToInt64(provider);
		}

		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToUInt64(provider);
		}

		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToSingle(provider);
		}

		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToDouble(provider);
		}

		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToDecimal(provider);
		}

		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			return ((IConvertible)_value).ToDateTime(provider);
		}

		string IConvertible.ToString(IFormatProvider provider)
		{
			byte value = _value;
			return value.ToString(provider);
		}

		object IConvertible.ToType(Type conversionType, IFormatProvider provider)
		{
			return ((IConvertible)_value).ToType(conversionType, provider);
		}

		public override string ToString()
		{
			byte value = _value;
			return value.ToString();
		}

		public override bool Equals(object obj)
		{
			if (!(obj is SevenBitNumber))
			{
				return false;
			}
			return ((SevenBitNumber)obj)._value == _value;
		}

		public override int GetHashCode()
		{
			byte value = _value;
			return value.GetHashCode();
		}
	}
	internal static class ShortByteParser
	{
		internal static ParsingResult TryParse(string input, byte minValue, byte maxValue, out byte result)
		{
			result = 0;
			if (string.IsNullOrWhiteSpace(input))
			{
				return ParsingResult.EmptyInputString;
			}
			if (!byte.TryParse(input.Trim(), out var result2) || result2 < minValue || result2 > maxValue)
			{
				return ParsingResult.Error("Number is invalid or is out of valid range.");
			}
			result = result2;
			return ParsingResult.Parsed;
		}
	}
	internal sealed class DisplayNameAttribute : Attribute
	{
		public string Name { get; }

		public DisplayNameAttribute(string name)
		{
			Name = name;
		}
	}
	public interface IMetadata
	{
		object Metadata { get; set; }
	}
	internal sealed class Logger : IDisposable
	{
		private static Logger _instance;

		private FileStream _fileStream;

		private StreamWriter _streamWriter;

		private bool _disposed;

		public static Logger Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new Logger();
				}
				return _instance;
			}
		}

		private Logger()
		{
		}

		public void WriteLine(string filePath, string line)
		{
			EnsureStreamCreated(filePath);
			_streamWriter.WriteLine(line);
		}

		public void Write(string filePath, string text)
		{
			EnsureStreamCreated(filePath);
			_streamWriter.Write(text);
		}

		public void Dispose()
		{
			Dispose(disposing: true);
		}

		public void EnsureStreamCreated(string filePath)
		{
			if (_streamWriter == null)
			{
				_fileStream = File.OpenWrite(filePath);
				_streamWriter = new StreamWriter(_fileStream);
			}
		}

		private void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					_streamWriter.Dispose();
					_fileStream.Dispose();
				}
				_fileStream = null;
				_streamWriter = null;
				_disposed = true;
			}
		}
	}
	internal static class MathUtilities
	{
		public static T GetLastElementBelowThreshold<T>(T[] elements, long keyThreshold, Func<T, long> keySelector)
		{
			int num = 0;
			int num2 = elements.Length - 1;
			while (num <= num2)
			{
				int num3 = (num + num2) / 2;
				T arg = elements[num3];
				long num4 = keySelector(arg);
				if (num4 > keyThreshold)
				{
					num2 = num3 - 1;
					continue;
				}
				if (num4 < keyThreshold)
				{
					num = num3 + 1;
					continue;
				}
				if (num3 <= 0)
				{
					return default(T);
				}
				return elements[num3 - 1];
			}
			if (num <= 0)
			{
				return default(T);
			}
			return elements[num - 1];
		}

		public static int EnsureInBounds(int value, int min, int max)
		{
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		public static bool IsPowerOfTwo(int value)
		{
			if (value != 0)
			{
				return (value & (value - 1)) == 0;
			}
			return false;
		}

		public static long LeastCommonMultiple(long a, long b)
		{
			ThrowIfArgument.IsNonpositive("a", a, "First number is zero or negative.");
			ThrowIfArgument.IsNonpositive("b", b, "Second number is zero or negative.");
			long num;
			long num2;
			if (a > b)
			{
				num = a;
				num2 = b;
			}
			else
			{
				num = b;
				num2 = a;
			}
			for (int i = 1; i < num2; i++)
			{
				if (num * i % num2 == 0L)
				{
					return i * num;
				}
			}
			return num * num2;
		}

		public static long GreatestCommonDivisor(long a, long b)
		{
			while (b != 0L)
			{
				long num = a % b;
				a = b;
				b = num;
			}
			return a;
		}

		public static Tuple<long, long> SolveDiophantineEquation(long a, long b)
		{
			long num = GreatestCommonDivisor(a, b);
			return Tuple.Create(b / num, -a / num);
		}

		public static double Round(double value)
		{
			return Math.Round(value, MidpointRounding.AwayFromZero);
		}

		public static double Round(double value, int digits)
		{
			return Math.Round(value, digits, MidpointRounding.AwayFromZero);
		}

		public static long RoundToLong(double value)
		{
			return (long)Round(value);
		}

		public static IEnumerable<T[]> GetPermutations<T>(T[] objects)
		{
			return GetPermutations(objects, objects.Length);
		}

		private static IEnumerable<T[]> GetPermutations<T>(T[] objects, int k)
		{
			if (k == 1)
			{
				yield return objects;
				yield break;
			}
			foreach (T[] permutation in GetPermutations(objects, k - 1))
			{
				yield return permutation;
			}
			for (int i = 0; i < k - 1; i++)
			{
				int num = ((k % 2 == 0) ? i : 0);
				int num2 = k - 1;
				ref readonly T reference = ref objects[num];
				object obj = objects[num2];
				if (reference.Equals(obj))
				{
					break;
				}
				T val = objects[num];
				objects[num] = objects[num2];
				objects[num2] = val;
				foreach (T[] permutation2 in GetPermutations(objects, k - 1))
				{
					yield return permutation2;
				}
			}
		}
	}
	public abstract class MidiException : Exception
	{
		internal MidiException()
		{
		}

		internal MidiException(string message)
			: base(message)
		{
		}

		internal MidiException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
	internal sealed class ObjectWrapper<TObject>
	{
		public TObject Object { get; set; }
	}
	internal delegate ParsingResult Parsing<T>(string input, out T result);
	internal sealed class ParsingResult
	{
		public static readonly ParsingResult Parsed = new ParsingResult(ParsingStatus.Parsed);

		public static readonly ParsingResult EmptyInputString = new ParsingResult(ParsingStatus.EmptyInputString);

		public static readonly ParsingResult NotMatched = new ParsingResult(ParsingStatus.NotMatched);

		private readonly string _error;

		public ParsingStatus Status { get; }

		public Exception Exception => Status switch
		{
			ParsingStatus.EmptyInputString => new ArgumentException("Input string is null or contains white-spaces only."), 
			ParsingStatus.NotMatched => new FormatException("Input string has invalid format."), 
			ParsingStatus.FormatError => new FormatException(_error), 
			_ => null, 
		};

		private ParsingResult(string error)
			: this(ParsingStatus.FormatError, error)
		{
		}

		private ParsingResult(ParsingStatus status)
			: this(status, null)
		{
		}

		private ParsingResult(ParsingStatus status, string error)
		{
			Status = status;
			_error = error;
		}

		public static ParsingResult Error(string error)
		{
			return new ParsingResult(error);
		}
	}
	internal enum ParsingStatus
	{
		Parsed,
		EmptyInputString,
		NotMatched,
		FormatError
	}
	internal static class ParsingUtilities
	{
		private const NumberStyles NonnegativeIntegerNumberStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite;

		private const NumberStyles IntegerNumberStyle = NumberStyles.Integer;

		private const NumberStyles NonnegativeDoubleNumberStyle = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint;

		public static bool TryParse<T>(string input, Parsing<T> parsing, out T result)
		{
			return parsing(input, out result).Status == ParsingStatus.Parsed;
		}

		public static T Parse<T>(string input, Parsing<T> parsing)
		{
			T result;
			ParsingResult parsingResult = parsing(input, out result);
			if (parsingResult.Status == ParsingStatus.Parsed)
			{
				return result;
			}
			throw parsingResult.Exception;
		}

		public static string GetNonnegativeIntegerNumberGroup(string groupName)
		{
			return "(?<" + groupName + ">\\d+)";
		}

		public static string GetIntegerNumberGroup(string groupName)
		{
			return "(?<" + groupName + ">[\\+\\-]?\\d+)";
		}

		public static string GetNonnegativeDoubleNumberGroup(string groupName)
		{
			return "(?<" + groupName + ">\\d+(.\\d+)?)";
		}

		public static Match Match(string input, IEnumerable<string> patterns, bool ignoreCase = true)
		{
			return patterns.Select((string p) => Regex.Match(input.Trim(), "^" + p + "$", ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None)).FirstOrDefault((Match m) => m.Success);
		}

		public static Match[] Matches(string input, IEnumerable<string> patterns, bool ignoreCase = true)
		{
			return patterns.Select((string p) => Regex.Matches(input.Trim(), p, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None).OfType<Match>().ToArray()).FirstOrDefault((Match[] m) => m.Any());
		}

		public static bool ParseNonnegativeInt(Match match, string groupName, int defaultValue, out int value)
		{
			return ParseInt(match, groupName, defaultValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, out value);
		}

		public static bool ParseInt(Match match, string groupName, int defaultValue, out int value)
		{
			return ParseInt(match, groupName, defaultValue, NumberStyles.Integer, out value);
		}

		public static bool ParseNonnegativeDouble(Match match, string groupName, double defaultValue, out double value)
		{
			return ParseDouble(match, groupName, defaultValue, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowDecimalPoint, out value);
		}

		public static bool ParseNonnegativeLong(Match match, string groupName, long defaultValue, out long value)
		{
			value = defaultValue;
			Group obj = match.Groups[groupName];
			if (obj.Success)
			{
				return long.TryParse(obj.Value, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite, null, out value);
			}
			return true;
		}

		private static bool ParseInt(Match match, string groupName, int defaultValue, NumberStyles numberStyle, out int value)
		{
			value = defaultValue;
			Group obj = match.Groups[groupName];
			if (obj.Success)
			{
				return int.TryParse(obj.Value, numberStyle, null, out value);
			}
			return true;
		}

		private static bool ParseDouble(Match match, string groupName, double defaultValue, NumberStyles numberStyle, out double value)
		{
			value = defaultValue;
			Group obj = match.Groups[groupName];
			if (obj.Success)
			{
				return double.TryParse(obj.Value, numberStyle, CultureInfo.InvariantCulture, out value);
			}
			return true;
		}
	}
	public enum SmpteFormat : byte
	{
		TwentyFour = 24,
		TwentyFive = 25,
		ThirtyDrop = 29,
		Thirty = 30
	}
	internal sealed class StreamWrapper : Stream
	{
		private readonly Stream _stream;

		private readonly CircularBuffer<byte> _buffer;

		private readonly byte[] _peekBuffer = new byte[1];

		private readonly byte[] _skipBytesBuffer = new byte[1024];

		private long _position;

		public override bool CanRead => true;

		public override bool CanSeek => true;

		public override bool CanWrite => false;

		public override long Length => long.MaxValue;

		public override long Position
		{
			get
			{
				return _position;
			}
			set
			{
				ThrowIfArgument.IsNegative("value", value, "Position is negative.");
				int num = (int)(value - _position);
				if (num != 0)
				{
					if (num > 0)
					{
						SkipBytes(num);
					}
					else
					{
						_buffer.MovePositionBack(-num);
					}
					_position = value;
				}
			}
		}

		public StreamWrapper(Stream stream, int bufferCapacity)
		{
			_stream = stream;
			_buffer = new CircularBuffer<byte>(bufferCapacity);
		}

		public bool IsEndReached()
		{
			if (Read(_peekBuffer, 0, 1) == 0)
			{
				return true;
			}
			Position--;
			return false;
		}

		public override void Flush()
		{
			throw new NotSupportedException();
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			byte[] array = _buffer.MovePositionForward(count);
			Buffer.BlockCopy(array, 0, buffer, offset, array.Length);
			offset += array.Length;
			int num = _stream.Read(buffer, offset, count - array.Length);
			for (int i = 0; i < num; i++)
			{
				_buffer.Add(buffer[offset + i]);
			}
			int num2 = array.Length + num;
			_position += num2;
			if (count > 0 && num2 == 0)
			{
				_position = long.MaxValue;
			}
			return num2;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		private void SkipBytes(int count)
		{
			while (count > 0)
			{
				int num = Read(_skipBytesBuffer, 0, Math.Min(count, _skipBytesBuffer.Length));
				if (num != 0)
				{
					count -= num;
					continue;
				}
				break;
			}
		}
	}
	internal static class ThrowIfArgument
	{
		private const int MinNonnegativeValue = 0;

		private const int MinPositiveValue = 1;

		internal static void IsNull(string parameterName, object argument)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}

		internal static void ContainsNull<T>(string parameterName, IEnumerable<T> argument)
		{
			if (argument.Any((T e) => e == null))
			{
				throw new ArgumentException("Collection contains null.", parameterName);
			}
		}

		internal static void IsInvalidEnumValue<TEnum>(string parameterName, TEnum argument) where TEnum : struct
		{
			if (!Enum.IsDefined(typeof(TEnum), argument))
			{
				throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(argument), typeof(TEnum));
			}
		}

		internal static void IsOutOfRange(string parameterName, TimeSpan value, TimeSpan min, TimeSpan max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		internal static void IsOutOfRange(string parameterName, int value, int min, int max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		internal static void IsOutOfRange(string parameterName, long value, long min, long max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		internal static void IsOutOfRange(string parameterName, double value, double min, double max, string message)
		{
			if (value < min || value > max)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		internal static void IsOutOfRange(string parameterName, int value, string message, params int[] values)
		{
			if (Array.IndexOf(values, value) < 0)
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		internal static void DoesntSatisfyCondition(string parameterName, int value, Predicate<int> condition, string message)
		{
			if (!condition(value))
			{
				throw new ArgumentOutOfRangeException(parameterName, value, message);
			}
		}

		internal static void IsGreaterThan(string parameterName, int value, int reference, string message)
		{
			IsOutOfRange(parameterName, value, int.MinValue, reference, message);
		}

		internal static void IsGreaterThan(string parameterName, long value, long reference, string message)
		{
			IsOutOfRange(parameterName, value, long.MinValue, reference, message);
		}

		internal static void IsLessThan(string parameterName, TimeSpan value, TimeSpan reference, string message)
		{
			IsOutOfRange(parameterName, value, reference, TimeSpan.MaxValue, message);
		}

		internal static void IsLessThan(string parameterName, int value, int reference, string message)
		{
			IsOutOfRange(parameterName, value, reference, int.MaxValue, message);
		}

		internal static void IsLessThan(string parameterName, long value, long reference, string message)
		{
			IsOutOfRange(parameterName, value, reference, long.MaxValue, message);
		}

		internal static void IsLessThan(string parameterName, double value, double reference, string message)
		{
			IsOutOfRange(parameterName, value, reference, double.MaxValue, message);
		}

		internal static void IsNegative(string parameterName, int value, string message)
		{
			IsLessThan(parameterName, value, 0, message);
		}

		internal static void IsNegative(string parameterName, long value, string message)
		{
			IsLessThan(parameterName, value, 0L, message);
		}

		internal static void IsNegative(string parameterName, double value, string message)
		{
			IsLessThan(parameterName, value, 0.0, message);
		}

		internal static void IsNonpositive(string parameterName, int value, string message)
		{
			IsLessThan(parameterName, value, 1, message);
		}

		internal static void IsNonpositive(string parameterName, long value, string message)
		{
			IsLessThan(parameterName, value, 1L, message);
		}

		internal static void IsNonpositive(string parameterName, double value, string message)
		{
			IsLessThan(parameterName, value, double.Epsilon, message);
		}

		internal static void IsNullOrWhiteSpaceString(string parameterName, string value, string stringDescription)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(stringDescription + " is null or contains white-spaces only.", parameterName);
			}
		}

		internal static void IsNullOrEmptyString(string parameterName, string value, string stringDescription)
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException(stringDescription + " is null or empty.", parameterName);
			}
		}

		internal static void IsInvalidIndex(string parameterName, int index, int collectionSize)
		{
			IsOutOfRange(parameterName, index, 0, collectionSize, "Index is out of range.");
		}

		internal static void IsEmptyCollection<T>(string parameterName, IEnumerable<T> collection, string message)
		{
			if (!collection.Any())
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		internal static void ContainsInvalidEnumValue<TEnum>(string parameterName, IEnumerable<TEnum> argument) where TEnum : struct
		{
			foreach (TEnum item in argument)
			{
				if (!Enum.IsDefined(typeof(TEnum), item))
				{
					throw new InvalidEnumArgumentException(parameterName, Convert.ToInt32(item), typeof(TEnum));
				}
			}
		}

		internal static void StartsWithInvalidValue<T>(string parameterName, IEnumerable<T> collection, T invalidValue, string message)
		{
			if (collection != null && collection.First().Equals(invalidValue))
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		internal static void IsOfInvalidType<TInvalidType>(string parameterName, object parameterValue, string message)
		{
			if (parameterValue is TInvalidType)
			{
				throw new ArgumentException(message, parameterName);
			}
		}

		internal static void IsOfInvalidType<TInvalidType1, TInvalidType2>(string parameterName, object parameterValue, string message)
		{
			if (parameterValue is TInvalidType1 || parameterValue is TInvalidType2)
			{
				throw new ArgumentException(message, parameterName);
			}
		}
	}
}
