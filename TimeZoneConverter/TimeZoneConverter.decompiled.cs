using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Microsoft.CodeAnalysis;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: AssemblyCompany("Matt Johnson-Pint")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyDescription("Lightweight library to convert quickly between IANA, Windows, and Rails time zone names.")]
[assembly: AssemblyFileVersion("6.1.0.0")]
[assembly: AssemblyInformationalVersion("6.1.0+dcac3b18c5eefe7ef71bfb997ec073820c0e1d98")]
[assembly: AssemblyProduct("TimeZoneConverter")]
[assembly: AssemblyTitle("TimeZoneConverter")]
[assembly: AssemblyMetadata("RepositoryUrl", "https://github.com/mattjohnsonpint/TimeZoneConverter")]
[assembly: AssemblyVersion("6.1.0.0")]
namespace Microsoft.CodeAnalysis
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	internal sealed class EmbeddedAttribute : Attribute
	{
	}
}
namespace System.Runtime.CompilerServices
{
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableAttribute : Attribute
	{
		public readonly byte[] NullableFlags;

		public NullableAttribute(byte P_0)
		{
			NullableFlags = new byte[1] { P_0 };
		}

		public NullableAttribute(byte[] P_0)
		{
			NullableFlags = P_0;
		}
	}
	[CompilerGenerated]
	[Microsoft.CodeAnalysis.Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	internal sealed class NullableContextAttribute : Attribute
	{
		public readonly byte Flag;

		public NullableContextAttribute(byte P_0)
		{
			Flag = P_0;
		}
	}
}
namespace TimeZoneConverter
{
	internal static class CustomTimeZoneFactory
	{
		private const string TrollTimeZoneId = "Antarctica/Troll";

		private static readonly Lazy<TimeZoneInfo> TrollTimeZone = new Lazy<TimeZoneInfo>(CreateTrollTimeZone);

		public static bool TryGetTimeZoneInfo(string timeZoneId, [MaybeNullWhen(false)] out TimeZoneInfo timeZoneInfo)
		{
			if (timeZoneId.Equals("Antarctica/Troll", StringComparison.OrdinalIgnoreCase))
			{
				timeZoneInfo = TrollTimeZone.Value;
				return true;
			}
			timeZoneInfo = null;
			return false;
		}

		private static TimeZoneInfo CreateTrollTimeZone()
		{
			TimeSpan zero = TimeSpan.Zero;
			TimeZoneInfo.AdjustmentRule[] array = new TimeZoneInfo.AdjustmentRule[1];
			DateTime minValue = DateTime.MinValue;
			DateTime date = minValue.Date;
			minValue = DateTime.MaxValue;
			array[0] = TimeZoneInfo.AdjustmentRule.CreateAdjustmentRule(date, minValue.Date, TimeSpan.FromHours(2.0), TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 1, 0, 0), 3, 5, DayOfWeek.Sunday), TimeZoneInfo.TransitionTime.CreateFloatingDateRule(new DateTime(1, 1, 1, 3, 0, 0), 10, 5, DayOfWeek.Sunday));
			return TimeZoneInfo.CreateCustomTimeZone("Antarctica/Troll", zero, "(UTC+00:00) Troll Station, Antarctica", "Greenwich Mean Time", "Central European Summer Time", array);
		}
	}
	internal static class DataLoader
	{
		public static void Populate(IDictionary<string, string> ianaMap, IDictionary<string, string> windowsMap, IDictionary<string, string> railsMap, IDictionary<string, IList<string>> inverseRailsMap, IDictionary<string, string> links, IDictionary<string, IList<string>> ianaTerritoryZones)
		{
			IEnumerable<string> embeddedData = GetEmbeddedData("TimeZoneConverter.Data.Mapping.csv.gz");
			IEnumerable<string> embeddedData2 = GetEmbeddedData("TimeZoneConverter.Data.Aliases.csv.gz");
			IEnumerable<string> embeddedData3 = GetEmbeddedData("TimeZoneConverter.Data.RailsMapping.csv.gz");
			IEnumerable<string> embeddedData4 = GetEmbeddedData("TimeZoneConverter.Data.Territories.csv.gz");
			foreach (string item in embeddedData2)
			{
				string[] array = item.Split(new char[1] { ',' });
				string value = array[0];
				string[] array2 = array[1].Split();
				foreach (string key in array2)
				{
					links.Add(key, value);
				}
			}
			foreach (string item2 in embeddedData4)
			{
				string[] array3 = item2.Split(new char[1] { ',' });
				string key2 = array3[0];
				List<string> value2 = new List<string>(array3[1].Split(new char[1] { ' ' }));
				ianaTerritoryZones.Add(key2, value2);
			}
			Dictionary<string, IList<string>> dictionary = new Dictionary<string, IList<string>>();
			foreach (string item3 in embeddedData)
			{
				string[] array4 = item3.Split(new char[1] { ',' });
				string text = array4[0];
				string text2 = array4[1];
				string[] array5 = array4[2].Split();
				string key3 = text2 + "|" + text;
				windowsMap.Add(key3, array5[0]);
				string[] array2 = array5;
				foreach (string key4 in array2)
				{
					if (!ianaMap.ContainsKey(key4))
					{
						ianaMap.Add(key4, text);
					}
				}
				if (array5.Length > 1)
				{
					array2 = array5;
					foreach (string text3 in array2)
					{
						dictionary.Add(text3, array5.Except(new string[1] { text3 }).ToArray());
					}
				}
			}
			List<KeyValuePair<string, string>> list = links.ToList();
			while (list.Count > 0)
			{
				List<KeyValuePair<string, string>> list2 = new List<KeyValuePair<string, string>>();
				foreach (KeyValuePair<string, string> item4 in list)
				{
					string value3;
					bool flag = ianaMap.TryGetValue(item4.Key, out value3);
					string value4;
					bool flag2 = ianaMap.TryGetValue(item4.Value, out value4);
					if (!(flag && flag2))
					{
						if (!flag && flag2)
						{
							ianaMap.Add(item4.Key, value4);
						}
						else if (!flag2 && flag)
						{
							ianaMap.Add(item4.Value, value3);
						}
						else
						{
							list2.Add(item4);
						}
					}
				}
				list = list2;
			}
			foreach (string item5 in embeddedData3)
			{
				string[] array6 = item5.Split(new char[1] { ',' });
				string text4 = array6[0];
				string[] array7 = array6[1].Split();
				for (int j = 0; j < array7.Length; j++)
				{
					string text5 = array7[j];
					if (j == 0)
					{
						railsMap.Add(text4, text5);
						continue;
					}
					inverseRailsMap.Add(text5, new string[1] { text4 });
				}
			}
			foreach (IGrouping<string, string> item6 in from x in railsMap
				group x.Key by x.Value)
			{
				inverseRailsMap.Add(item6.Key, item6.ToList());
			}
			foreach (string key5 in ianaMap.Keys)
			{
				if (inverseRailsMap.ContainsKey(key5) || links.ContainsKey(key5) || !dictionary.TryGetValue(key5, out var value5))
				{
					continue;
				}
				foreach (string item7 in value5)
				{
					if (inverseRailsMap.TryGetValue(item7, out IList<string> value6))
					{
						inverseRailsMap.Add(key5, value6);
						break;
					}
				}
			}
			foreach (KeyValuePair<string, string> link in links)
			{
				IList<string> value8;
				if (!inverseRailsMap.ContainsKey(link.Key))
				{
					if (inverseRailsMap.TryGetValue(link.Value, out IList<string> value7))
					{
						inverseRailsMap.Add(link.Key, value7);
					}
				}
				else if (!inverseRailsMap.ContainsKey(link.Value) && inverseRailsMap.TryGetValue(link.Key, out value8))
				{
					inverseRailsMap.Add(link.Value, value8);
				}
			}
			foreach (string key6 in ianaMap.Keys)
			{
				if (!inverseRailsMap.ContainsKey(key6) && ianaMap.TryGetValue(key6, out string value9) && windowsMap.TryGetValue("001|" + value9, out string value10) && inverseRailsMap.TryGetValue(value10, out IList<string> value11))
				{
					inverseRailsMap.Add(key6, value11);
				}
			}
		}

		private static IEnumerable<string> GetEmbeddedData(string resourceName)
		{
			Assembly assembly = typeof(DataLoader).Assembly;
			using Stream compressedStream = assembly.GetManifestResourceStream(resourceName) ?? throw new MissingManifestResourceException();
			using GZipStream stream = new GZipStream(compressedStream, CompressionMode.Decompress);
			using StreamReader reader = new StreamReader(stream);
			while (true)
			{
				string text = reader.ReadLine();
				if (text != null)
				{
					yield return text;
					continue;
				}
				break;
			}
		}
	}
	public enum LinkResolution
	{
		Default,
		Canonical,
		Original
	}
	public static class TZConvert
	{
		private static readonly bool IsWindows;

		private static readonly Dictionary<string, string> IanaMap;

		private static readonly Dictionary<string, string> WindowsMap;

		private static readonly Dictionary<string, string> RailsMap;

		private static readonly Dictionary<string, IList<string>> InverseRailsMap;

		private static readonly Dictionary<string, string> Links;

		private static readonly Dictionary<string, TimeZoneInfo> SystemTimeZones;

		private static readonly IDictionary<string, IList<string>> IanaTerritoryZones;

		public static IReadOnlyCollection<string> KnownIanaTimeZoneNames { get; }

		public static IReadOnlyCollection<string> KnownWindowsTimeZoneIds { get; }

		public static IReadOnlyCollection<string> KnownRailsTimeZoneNames { get; }

		static TZConvert()
		{
			IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
			IanaMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			WindowsMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			RailsMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			InverseRailsMap = new Dictionary<string, IList<string>>(StringComparer.OrdinalIgnoreCase);
			Links = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			IanaTerritoryZones = new Dictionary<string, IList<string>>(StringComparer.OrdinalIgnoreCase);
			DataLoader.Populate(IanaMap, WindowsMap, RailsMap, InverseRailsMap, Links, IanaTerritoryZones);
			HashSet<string> hashSet = new HashSet<string>(IanaMap.Select<KeyValuePair<string, string>, string>((KeyValuePair<string, string> x) => x.Key));
			HashSet<string> hashSet2 = new HashSet<string>(WindowsMap.Keys.Select((string x) => x.Split(new char[1] { '|' })[1]).Distinct());
			HashSet<string> hashSet3 = new HashSet<string>(RailsMap.Select<KeyValuePair<string, string>, string>((KeyValuePair<string, string> x) => x.Key));
			hashSet.Add("Antarctica/Troll");
			hashSet.Remove("Canada/East-Saskatchewan");
			hashSet.Remove("US/Pacific-New");
			hashSet2.Remove("Kamchatka Standard Time");
			hashSet2.Remove("Mid-Atlantic Standard Time");
			KnownIanaTimeZoneNames = hashSet;
			KnownWindowsTimeZoneIds = hashSet2;
			KnownRailsTimeZoneNames = hashSet3;
			SystemTimeZones = GetSystemTimeZones();
		}

		public static IReadOnlyDictionary<string, IReadOnlyCollection<string>> GetIanaTimeZoneNamesByTerritory(bool fullList = false)
		{
			if (fullList)
			{
				return new ReadOnlyDictionary<string, IReadOnlyCollection<string>>(IanaTerritoryZones.ToDictionary<KeyValuePair<string, IList<string>>, string, IReadOnlyCollection<string>>((KeyValuePair<string, IList<string>> x) => x.Key, (KeyValuePair<string, IList<string>> x) => x.Value.OrderBy((string zone) => zone).ToList().AsReadOnly()));
			}
			string windowsTimeZoneId;
			return new ReadOnlyDictionary<string, IReadOnlyCollection<string>>(IanaTerritoryZones.ToDictionary<KeyValuePair<string, IList<string>>, string, IReadOnlyCollection<string>>((KeyValuePair<string, IList<string>> x) => x.Key, (KeyValuePair<string, IList<string>> x) => (from zone in x.Value
				select (!TryIanaToWindows(zone, out windowsTimeZoneId)) ? zone : WindowsToIana(windowsTimeZoneId, x.Key) into zone
				orderby zone
				select zone).Distinct().ToList().AsReadOnly()));
		}

		public static string IanaToWindows(string ianaTimeZoneName)
		{
			if (TryIanaToWindows(ianaTimeZoneName, out string windowsTimeZoneId))
			{
				return windowsTimeZoneId;
			}
			throw new InvalidTimeZoneException("\"" + ianaTimeZoneName + "\" was not recognized as a valid IANA time zone name, or has no equivalent Windows time zone.");
		}

		public static bool TryIanaToWindows(string ianaTimeZoneName, [MaybeNullWhen(false)] out string windowsTimeZoneId)
		{
			return IanaMap.TryGetValue(ianaTimeZoneName, out windowsTimeZoneId);
		}

		public static string WindowsToIana(string windowsTimeZoneId, string territoryCode = "001")
		{
			return WindowsToIana(windowsTimeZoneId, territoryCode, LinkResolution.Default);
		}

		public static string WindowsToIana(string windowsTimeZoneId, LinkResolution linkResolutionMode)
		{
			return WindowsToIana(windowsTimeZoneId, "001", linkResolutionMode);
		}

		public static string WindowsToIana(string windowsTimeZoneId, string territoryCode, LinkResolution linkResolutionMode)
		{
			if (TryWindowsToIana(windowsTimeZoneId, territoryCode, out string ianaTimeZoneName, linkResolutionMode))
			{
				return ianaTimeZoneName;
			}
			throw new InvalidTimeZoneException("\"" + windowsTimeZoneId + "\" was not recognized as a valid Windows time zone ID.");
		}

		public static bool TryWindowsToIana(string windowsTimeZoneId, [MaybeNullWhen(false)] out string ianaTimeZoneName)
		{
			return TryWindowsToIana(windowsTimeZoneId, "001", out ianaTimeZoneName, LinkResolution.Default);
		}

		public static bool TryWindowsToIana(string windowsTimeZoneId, [MaybeNullWhen(false)] out string ianaTimeZoneName, LinkResolution linkResolutionMode)
		{
			return TryWindowsToIana(windowsTimeZoneId, "001", out ianaTimeZoneName, linkResolutionMode);
		}

		public static bool TryWindowsToIana(string windowsTimeZoneId, string territoryCode, [MaybeNullWhen(false)] out string ianaTimeZoneName)
		{
			return TryWindowsToIana(windowsTimeZoneId, territoryCode, out ianaTimeZoneName, LinkResolution.Default);
		}

		public static bool TryWindowsToIana(string windowsTimeZoneId, string territoryCode, [MaybeNullWhen(false)] out string ianaTimeZoneName, LinkResolution linkResolutionMode)
		{
			string value;
			bool flag = WindowsMap.TryGetValue(territoryCode + "|" + windowsTimeZoneId, out value);
			string value2 = null;
			if (territoryCode != "001" && (linkResolutionMode == LinkResolution.Default || !flag))
			{
				bool flag2 = WindowsMap.TryGetValue("001|" + windowsTimeZoneId, out value2);
				if (!flag)
				{
					flag = flag2;
					value = value2;
				}
			}
			if (!flag)
			{
				ianaTimeZoneName = null;
				return false;
			}
			ianaTimeZoneName = value;
			switch (linkResolutionMode)
			{
			case LinkResolution.Default:
				if (value2 == null || value == value2)
				{
					ianaTimeZoneName = ResolveLink(value);
				}
				else
				{
					string text = ResolveLink(value2);
					string text2 = ResolveLink(value);
					if (text != text2 && !WindowsMap.ContainsValue(text2))
					{
						ianaTimeZoneName = text2;
					}
				}
				return true;
			case LinkResolution.Canonical:
				ianaTimeZoneName = ResolveLink(value);
				return true;
			case LinkResolution.Original:
				return true;
			default:
				throw new ArgumentOutOfRangeException("linkResolutionMode", linkResolutionMode, null);
			}
		}

		private static string ResolveLink(string linkOrZone)
		{
			if (!Links.TryGetValue(linkOrZone, out string value))
			{
				return linkOrZone;
			}
			return value;
		}

		public static TimeZoneInfo GetTimeZoneInfo(string windowsOrIanaTimeZoneId)
		{
			if (TryGetTimeZoneInfo(windowsOrIanaTimeZoneId, out TimeZoneInfo timeZoneInfo))
			{
				return timeZoneInfo;
			}
			throw new TimeZoneNotFoundException("\"" + windowsOrIanaTimeZoneId + "\" was not found.");
		}

		public static bool TryGetTimeZoneInfo(string windowsOrIanaTimeZoneId, [MaybeNullWhen(false)] out TimeZoneInfo timeZoneInfo)
		{
			if (string.Equals(windowsOrIanaTimeZoneId, "UTC", StringComparison.OrdinalIgnoreCase))
			{
				timeZoneInfo = TimeZoneInfo.Utc;
				return true;
			}
			if (SystemTimeZones.TryGetValue(windowsOrIanaTimeZoneId, out timeZoneInfo))
			{
				return true;
			}
			if (((IsWindows && TryIanaToWindows(windowsOrIanaTimeZoneId, out string windowsTimeZoneId)) || TryWindowsToIana(windowsOrIanaTimeZoneId, out windowsTimeZoneId, LinkResolution.Original)) && SystemTimeZones.TryGetValue(windowsTimeZoneId, out timeZoneInfo))
			{
				return true;
			}
			if (CustomTimeZoneFactory.TryGetTimeZoneInfo(windowsOrIanaTimeZoneId, out timeZoneInfo))
			{
				return true;
			}
			return false;
		}

		public static IList<string> IanaToRails(string ianaTimeZoneName)
		{
			if (TryIanaToRails(ianaTimeZoneName, out IList<string> railsTimeZoneNames))
			{
				return railsTimeZoneNames;
			}
			throw new InvalidTimeZoneException("\"" + ianaTimeZoneName + "\" was not recognized as a valid IANA time zone name, or has no equivalent Rails time zone.");
		}

		public static bool TryIanaToRails(string ianaTimeZoneName, out IList<string> railsTimeZoneNames)
		{
			if (InverseRailsMap.TryGetValue(ianaTimeZoneName, out railsTimeZoneNames))
			{
				return true;
			}
			if (TryIanaToWindows(ianaTimeZoneName, out string windowsTimeZoneId) && TryWindowsToIana(windowsTimeZoneId, out string ianaTimeZoneName2) && InverseRailsMap.TryGetValue(ianaTimeZoneName2, out railsTimeZoneNames))
			{
				return true;
			}
			railsTimeZoneNames = Array.Empty<string>();
			return false;
		}

		public static string RailsToIana(string railsTimeZoneName)
		{
			if (TryRailsToIana(railsTimeZoneName, out string ianaTimeZoneName))
			{
				return ianaTimeZoneName;
			}
			throw new InvalidTimeZoneException("\"" + railsTimeZoneName + "\" was not recognized as a valid Rails time zone name.");
		}

		public static bool TryRailsToIana(string railsTimeZoneName, [MaybeNullWhen(false)] out string ianaTimeZoneName)
		{
			return RailsMap.TryGetValue(railsTimeZoneName, out ianaTimeZoneName);
		}

		public static string RailsToWindows(string railsTimeZoneName)
		{
			if (TryRailsToWindows(railsTimeZoneName, out string windowsTimeZoneId))
			{
				return windowsTimeZoneId;
			}
			throw new InvalidTimeZoneException("\"" + railsTimeZoneName + "\" was not recognized as a valid Rails time zone name.");
		}

		public static bool TryRailsToWindows(string railsTimeZoneName, [MaybeNullWhen(false)] out string windowsTimeZoneId)
		{
			if (TryRailsToIana(railsTimeZoneName, out string ianaTimeZoneName) && TryIanaToWindows(ianaTimeZoneName, out windowsTimeZoneId))
			{
				return true;
			}
			windowsTimeZoneId = null;
			return false;
		}

		public static IList<string> WindowsToRails(string windowsTimeZoneId, string territoryCode = "001")
		{
			if (TryWindowsToRails(windowsTimeZoneId, territoryCode, out IList<string> railsTimeZoneNames))
			{
				return railsTimeZoneNames;
			}
			throw new InvalidTimeZoneException("\"" + windowsTimeZoneId + "\" was not recognized as a valid Windows time zone ID, or has no equivalent Rails time zone.");
		}

		public static bool TryWindowsToRails(string windowsTimeZoneId, out IList<string> railsTimeZoneNames)
		{
			return TryWindowsToRails(windowsTimeZoneId, "001", out railsTimeZoneNames);
		}

		public static bool TryWindowsToRails(string windowsTimeZoneId, string territoryCode, out IList<string> railsTimeZoneNames)
		{
			if (TryWindowsToIana(windowsTimeZoneId, territoryCode, out string ianaTimeZoneName) && TryIanaToRails(ianaTimeZoneName, out railsTimeZoneNames))
			{
				return true;
			}
			railsTimeZoneNames = Array.Empty<string>();
			return false;
		}

		private static Dictionary<string, TimeZoneInfo> GetSystemTimeZones()
		{
			TimeZoneInfo.ClearCachedData();
			Dictionary<string, TimeZoneInfo> dictionary = TimeZoneInfo.GetSystemTimeZones().GroupBy((TimeZoneInfo x) => x.Id, StringComparer.OrdinalIgnoreCase).ToDictionary((IGrouping<string, TimeZoneInfo> x) => x.Key, (IGrouping<string, TimeZoneInfo> x) => x.First(), StringComparer.OrdinalIgnoreCase);
			if (IsWindows)
			{
				return dictionary;
			}
			foreach (string knownIanaTimeZoneName in KnownIanaTimeZoneNames)
			{
				if (!dictionary.ContainsKey(knownIanaTimeZoneName))
				{
					try
					{
						TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(knownIanaTimeZoneName);
						dictionary.Add(timeZoneInfo.Id, timeZoneInfo);
					}
					catch
					{
					}
				}
			}
			return dictionary;
		}
	}
}
namespace System.Diagnostics.CodeAnalysis
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class AllowNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DisallowNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class DoesNotReturnIfAttribute : Attribute
	{
		public bool ParameterValue { get; }

		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			ParameterValue = parameterValue;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MaybeNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MaybeNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public MaybeNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullAttribute : Attribute
	{
		public string[] Members { get; }

		public MemberNotNullAttribute(string member)
		{
			Members = new string[1] { member };
		}

		public MemberNotNullAttribute(params string[] members)
		{
			Members = members;
		}
	}
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class MemberNotNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public string[] Members { get; }

		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			ReturnValue = returnValue;
			Members = new string[1] { member };
		}

		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			ReturnValue = returnValue;
			Members = members;
		}
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullAttribute : Attribute
	{
	}
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullIfNotNullAttribute : Attribute
	{
		public string ParameterName { get; }

		public NotNullIfNotNullAttribute(string parameterName)
		{
			ParameterName = parameterName;
		}
	}
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ExcludeFromCodeCoverage]
	[DebuggerNonUserCode]
	internal sealed class NotNullWhenAttribute : Attribute
	{
		public bool ReturnValue { get; }

		public NotNullWhenAttribute(bool returnValue)
		{
			ReturnValue = returnValue;
		}
	}
}
