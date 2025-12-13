using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security;
using System.Security.Permissions;
using System.Text;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: InternalsVisibleTo("Cronos.Tests")]
[assembly: TargetFramework(".NETStandard,Version=v2.0", FrameworkDisplayName = "")]
[assembly: AssemblyCompany("Andrey Dorokhov, Sergey Odinokov")]
[assembly: AssemblyConfiguration("Release")]
[assembly: AssemblyCopyright("Copyright © 2016-2018 Sergey Odinokov.")]
[assembly: AssemblyDescription("A fully-featured .NET library for parsing Cron expressions and calculating next occurrences that was designed with time zones in mind and correctly handles daylight saving time transitions.")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyInformationalVersion("0.7.1")]
[assembly: AssemblyProduct("Cronos")]
[assembly: AssemblyTitle("Cronos")]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.7.1.0")]
[module: UnverifiableCode]
namespace Cronos;

internal static class CalendarHelper
{
	private const int DaysPerWeekCount = 7;

	private const long TicksPerMillisecond = 10000L;

	private const long TicksPerSecond = 10000000L;

	private const long TicksPerMinute = 600000000L;

	private const long TicksPerHour = 36000000000L;

	private const long TicksPerDay = 864000000000L;

	private const int DaysPerYear = 365;

	private const int DaysPer4Years = 1461;

	private const int DaysPer100Years = 36524;

	private const int DaysPer400Years = 146097;

	private static readonly int[] DaysToMonth365 = new int[13]
	{
		0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
		304, 334, 365
	};

	private static readonly int[] DaysToMonth366 = new int[13]
	{
		0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
		305, 335, 366
	};

	private static readonly int[] DaysInMonth = new int[13]
	{
		-1, 31, 28, 31, 30, 31, 30, 31, 31, 30,
		31, 30, 31
	};

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsGreaterThan(int year1, int month1, int day1, int year2, int month2, int day2)
	{
		if (year1 != year2)
		{
			return year1 > year2;
		}
		if (month1 != month2)
		{
			return month1 > month2;
		}
		if (day2 != day1)
		{
			return day1 > day2;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static long DateTimeToTicks(int year, int month, int day, int hour, int minute, int second)
	{
		int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? DaysToMonth366 : DaysToMonth365);
		int num = year - 1;
		return (num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 864000000000L + ((long)hour * 3600L + (long)minute * 60L + second) * 10000000;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void FillDateTimeParts(long ticks, out int second, out int minute, out int hour, out int day, out int month, out int year)
	{
		second = (int)(ticks / 10000000 % 60);
		if (ticks % 10000000 != 0L)
		{
			second++;
		}
		minute = (int)(ticks / 600000000 % 60);
		hour = (int)(ticks / 36000000000L % 24);
		int num = (int)(ticks / 864000000000L);
		int num2 = num / 146097;
		num -= num2 * 146097;
		int num3 = num / 36524;
		if (num3 == 4)
		{
			num3 = 3;
		}
		num -= num3 * 36524;
		int num4 = num / 1461;
		num -= num4 * 1461;
		int num5 = num / 365;
		if (num5 == 4)
		{
			num5 = 3;
		}
		year = num2 * 400 + num3 * 100 + num4 * 4 + num5 + 1;
		num -= num5 * 365;
		int[] array = ((num5 == 3 && (num4 != 24 || num3 == 3)) ? DaysToMonth366 : DaysToMonth365);
		month = (num >> 5) + 1;
		while (num >= array[month])
		{
			month++;
		}
		day = num - array[month - 1] + 1;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static DayOfWeek GetDayOfWeek(int year, int month, int day)
	{
		int[] array = ((year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? DaysToMonth366 : DaysToMonth365);
		int num = year - 1;
		return (DayOfWeek)((int)((num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1) * 864000000000L / 864000000000L + 1) % 7);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int GetDaysInMonth(int year, int month)
	{
		if (month != 2 || year % 4 != 0)
		{
			return DaysInMonth[month];
		}
		if (year % 100 == 0 && year % 400 != 0)
		{
			return 28;
		}
		return 29;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static int MoveToNearestWeekDay(int year, int month, int day)
	{
		DayOfWeek dayOfWeek = GetDayOfWeek(year, month, day);
		if (dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday)
		{
			return day;
		}
		if (dayOfWeek != DayOfWeek.Sunday)
		{
			if (day != CronField.DaysOfMonth.First)
			{
				return day - 1;
			}
			return day + 2;
		}
		if (day != GetDaysInMonth(year, month))
		{
			return day + 1;
		}
		return day - 2;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsNthDayOfWeek(int day, int n)
	{
		if (day - 7 * n < CronField.DaysOfMonth.First)
		{
			return day - 7 * (n - 1) >= CronField.DaysOfMonth.First;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool IsLastDayOfWeek(int year, int month, int day)
	{
		return day + 7 > GetDaysInMonth(year, month);
	}
}
public sealed class CronExpression : IEquatable<CronExpression>
{
	private const long NotFound = 0L;

	private const int MinNthDayOfWeek = 1;

	private const int MaxNthDayOfWeek = 5;

	private const int SundayBits = 129;

	private const int MaxYear = 2099;

	private static readonly TimeZoneInfo UtcTimeZone = TimeZoneInfo.Utc;

	private static readonly CronExpression Yearly = Parse("0 0 1 1 *");

	private static readonly CronExpression Weekly = Parse("0 0 * * 0");

	private static readonly CronExpression Monthly = Parse("0 0 1 * *");

	private static readonly CronExpression Daily = Parse("0 0 * * *");

	private static readonly CronExpression Hourly = Parse("0 * * * *");

	private static readonly CronExpression Minutely = Parse("* * * * *");

	private static readonly CronExpression Secondly = Parse("* * * * * *", CronFormat.IncludeSeconds);

	private static readonly int[] DeBruijnPositions = new int[64]
	{
		0, 1, 2, 53, 3, 7, 54, 27, 4, 38,
		41, 8, 34, 55, 48, 28, 62, 5, 39, 46,
		44, 42, 22, 9, 24, 35, 59, 56, 49, 18,
		29, 11, 63, 52, 6, 26, 37, 40, 33, 47,
		61, 45, 43, 21, 23, 58, 17, 10, 51, 25,
		36, 32, 60, 20, 57, 16, 50, 31, 19, 15,
		30, 14, 13, 12
	};

	private long _second;

	private long _minute;

	private int _hour;

	private int _dayOfMonth;

	private short _month;

	private byte _dayOfWeek;

	private byte _nthDayOfWeek;

	private byte _lastMonthOffset;

	private CronExpressionFlag _flags;

	private CronExpression()
	{
	}

	public static CronExpression Parse(string expression)
	{
		return Parse(expression, CronFormat.Standard);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public unsafe static CronExpression Parse(string expression, CronFormat format)
	{
		if (string.IsNullOrEmpty(expression))
		{
			throw new ArgumentNullException("expression");
		}
		fixed (char* ptr = expression)
		{
			char* pointer = ptr;
			SkipWhiteSpaces(ref pointer);
			CronExpression cronExpression;
			if (Accept(ref pointer, '@'))
			{
				cronExpression = ParseMacro(ref pointer);
				SkipWhiteSpaces(ref pointer);
				if (cronExpression == null || !IsEndOfString(*pointer))
				{
					ThrowFormatException("Macro: Unexpected character '{0}' on position {1}.", *pointer, pointer - ptr);
				}
				return cronExpression;
			}
			cronExpression = new CronExpression();
			if (format == CronFormat.IncludeSeconds)
			{
				cronExpression._second = ParseField(CronField.Seconds, ref pointer, ref cronExpression._flags);
				ParseWhiteSpace(CronField.Seconds, ref pointer);
			}
			else
			{
				SetBit(ref cronExpression._second, CronField.Seconds.First);
			}
			cronExpression._minute = ParseField(CronField.Minutes, ref pointer, ref cronExpression._flags);
			ParseWhiteSpace(CronField.Minutes, ref pointer);
			cronExpression._hour = (int)ParseField(CronField.Hours, ref pointer, ref cronExpression._flags);
			ParseWhiteSpace(CronField.Hours, ref pointer);
			cronExpression._dayOfMonth = (int)ParseDayOfMonth(ref pointer, ref cronExpression._flags, ref cronExpression._lastMonthOffset);
			ParseWhiteSpace(CronField.DaysOfMonth, ref pointer);
			cronExpression._month = (short)ParseField(CronField.Months, ref pointer, ref cronExpression._flags);
			ParseWhiteSpace(CronField.Months, ref pointer);
			cronExpression._dayOfWeek = (byte)ParseDayOfWeek(ref pointer, ref cronExpression._flags, ref cronExpression._nthDayOfWeek);
			ParseEndOfString(ref pointer);
			if ((cronExpression._dayOfWeek & 0x81) != 0)
			{
				cronExpression._dayOfWeek |= 129;
			}
			return cronExpression;
		}
	}

	public DateTime? GetNextOccurrence(DateTime fromUtc, bool inclusive = false)
	{
		if (fromUtc.Kind != DateTimeKind.Utc)
		{
			ThrowWrongDateTimeKindException("fromUtc");
		}
		long num = FindOccurrence(fromUtc.Ticks, inclusive);
		if (num == 0L)
		{
			return null;
		}
		return new DateTime(num, DateTimeKind.Utc);
	}

	public IEnumerable<DateTime> GetOccurrences(DateTime fromUtc, DateTime toUtc, bool fromInclusive = true, bool toInclusive = false)
	{
		if (fromUtc > toUtc)
		{
			ThrowFromShouldBeLessThanToException("fromUtc", "toUtc");
		}
		DateTime? occurrence = GetNextOccurrence(fromUtc, fromInclusive);
		while (occurrence < toUtc || (occurrence == toUtc && toInclusive))
		{
			yield return occurrence.Value;
			occurrence = GetNextOccurrence(occurrence.Value);
		}
	}

	public DateTime? GetNextOccurrence(DateTime fromUtc, TimeZoneInfo zone, bool inclusive = false)
	{
		if (fromUtc.Kind != DateTimeKind.Utc)
		{
			ThrowWrongDateTimeKindException("fromUtc");
		}
		if (zone == UtcTimeZone)
		{
			long num = FindOccurrence(fromUtc.Ticks, inclusive);
			if (num == 0L)
			{
				return null;
			}
			return new DateTime(num, DateTimeKind.Utc);
		}
		DateTimeOffset fromUtc2 = new DateTimeOffset(fromUtc);
		return GetOccurrenceConsideringTimeZone(fromUtc2, zone, inclusive)?.UtcDateTime;
	}

	public IEnumerable<DateTime> GetOccurrences(DateTime fromUtc, DateTime toUtc, TimeZoneInfo zone, bool fromInclusive = true, bool toInclusive = false)
	{
		if (fromUtc > toUtc)
		{
			ThrowFromShouldBeLessThanToException("fromUtc", "toUtc");
		}
		DateTime? occurrence = GetNextOccurrence(fromUtc, zone, fromInclusive);
		while (occurrence < toUtc || (occurrence == toUtc && toInclusive))
		{
			yield return occurrence.Value;
			occurrence = GetNextOccurrence(occurrence.Value, zone);
		}
	}

	public DateTimeOffset? GetNextOccurrence(DateTimeOffset from, TimeZoneInfo zone, bool inclusive = false)
	{
		if (zone == UtcTimeZone)
		{
			long num = FindOccurrence(from.UtcTicks, inclusive);
			if (num == 0L)
			{
				return null;
			}
			return new DateTimeOffset(num, TimeSpan.Zero);
		}
		return GetOccurrenceConsideringTimeZone(from, zone, inclusive);
	}

	public IEnumerable<DateTimeOffset> GetOccurrences(DateTimeOffset from, DateTimeOffset to, TimeZoneInfo zone, bool fromInclusive = true, bool toInclusive = false)
	{
		if (from > to)
		{
			ThrowFromShouldBeLessThanToException("from", "to");
		}
		DateTimeOffset? occurrence = GetNextOccurrence(from, zone, fromInclusive);
		while (occurrence < to || (occurrence == to && toInclusive))
		{
			yield return occurrence.Value;
			occurrence = GetNextOccurrence(occurrence.Value, zone);
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		AppendFieldValue(stringBuilder, CronField.Seconds, _second).Append(' ');
		AppendFieldValue(stringBuilder, CronField.Minutes, _minute).Append(' ');
		AppendFieldValue(stringBuilder, CronField.Hours, _hour).Append(' ');
		AppendDayOfMonth(stringBuilder, _dayOfMonth).Append(' ');
		AppendFieldValue(stringBuilder, CronField.Months, _month).Append(' ');
		AppendDayOfWeek(stringBuilder, _dayOfWeek);
		return stringBuilder.ToString();
	}

	public bool Equals(CronExpression other)
	{
		if (other == null)
		{
			return false;
		}
		if (_second == other._second && _minute == other._minute && _hour == other._hour && _dayOfMonth == other._dayOfMonth && _month == other._month && _dayOfWeek == other._dayOfWeek && _nthDayOfWeek == other._nthDayOfWeek && _lastMonthOffset == other._lastMonthOffset)
		{
			return _flags == other._flags;
		}
		return false;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as CronExpression);
	}

	public override int GetHashCode()
	{
		return (((((((((((((((_second.GetHashCode() * 397) ^ _minute.GetHashCode()) * 397) ^ _hour) * 397) ^ _dayOfMonth) * 397) ^ _month.GetHashCode()) * 397) ^ _dayOfWeek.GetHashCode()) * 397) ^ _nthDayOfWeek.GetHashCode()) * 397) ^ _lastMonthOffset.GetHashCode()) * 397) ^ (int)_flags;
	}

	public static bool operator ==(CronExpression left, CronExpression right)
	{
		return object.Equals(left, right);
	}

	public static bool operator !=(CronExpression left, CronExpression right)
	{
		return !object.Equals(left, right);
	}

	private DateTimeOffset? GetOccurrenceConsideringTimeZone(DateTimeOffset fromUtc, TimeZoneInfo zone, bool inclusive)
	{
		if (!DateTimeHelper.IsRound(fromUtc))
		{
			fromUtc = DateTimeHelper.FloorToSeconds(fromUtc);
			inclusive = false;
		}
		DateTimeOffset dateTimeOffset = TimeZoneInfo.ConvertTime(fromUtc, zone);
		DateTime dateTime = dateTimeOffset.DateTime;
		if (TimeZoneHelper.IsAmbiguousTime(zone, dateTime))
		{
			TimeSpan offset = dateTimeOffset.Offset;
			TimeSpan baseUtcOffset = zone.BaseUtcOffset;
			if (baseUtcOffset != offset)
			{
				TimeSpan daylightOffset = TimeZoneHelper.GetDaylightOffset(zone, dateTime);
				DateTime dateTime2 = TimeZoneHelper.GetDaylightTimeEnd(zone, dateTime, daylightOffset).DateTime;
				long num = FindOccurrence(dateTime.Ticks, dateTime2.Ticks, inclusive);
				if (num != 0L)
				{
					return new DateTimeOffset(num, daylightOffset);
				}
				dateTime = TimeZoneHelper.GetStandardTimeStart(zone, dateTime, daylightOffset).DateTime;
				inclusive = true;
			}
			DateTime dateTime3 = TimeZoneHelper.GetAmbiguousIntervalEnd(zone, dateTime).DateTime;
			if (HasFlag(CronExpressionFlag.Interval))
			{
				long num2 = FindOccurrence(dateTime.Ticks, dateTime3.Ticks - 1, inclusive);
				if (num2 != 0L)
				{
					return new DateTimeOffset(num2, baseUtcOffset);
				}
			}
			dateTime = dateTime3;
			inclusive = true;
		}
		long num3 = FindOccurrence(dateTime.Ticks, inclusive);
		if (num3 == 0L)
		{
			return null;
		}
		DateTime dateTime4 = new DateTime(num3);
		if (zone.IsInvalidTime(dateTime4))
		{
			return TimeZoneHelper.GetDaylightTimeStart(zone, dateTime4);
		}
		if (TimeZoneHelper.IsAmbiguousTime(zone, dateTime4))
		{
			TimeSpan daylightOffset2 = TimeZoneHelper.GetDaylightOffset(zone, dateTime4);
			return new DateTimeOffset(dateTime4, daylightOffset2);
		}
		return new DateTimeOffset(dateTime4, zone.GetUtcOffset(dateTime4));
	}

	private long FindOccurrence(long startTimeTicks, long endTimeTicks, bool startInclusive)
	{
		long num = FindOccurrence(startTimeTicks, startInclusive);
		if (num == 0L || num > endTimeTicks)
		{
			return 0L;
		}
		return num;
	}

	private long FindOccurrence(long ticks, bool startInclusive)
	{
		if (!startInclusive)
		{
			ticks++;
		}
		CalendarHelper.FillDateTimeParts(ticks, out var second, out var minute, out var hour, out var day, out var month, out var year);
		int firstSet = GetFirstSet(_dayOfMonth);
		int fieldValue = second;
		int fieldValue2 = minute;
		int fieldValue3 = hour;
		int fieldValue4 = day;
		int fieldValue5 = month;
		int num = year;
		if (!GetBit(_second, fieldValue) && !Move(_second, ref fieldValue))
		{
			fieldValue2++;
		}
		if (!GetBit(_minute, fieldValue2) && !Move(_minute, ref fieldValue2))
		{
			fieldValue3++;
		}
		if (!GetBit(_hour, fieldValue3) && !Move(_hour, ref fieldValue3))
		{
			fieldValue4++;
		}
		if (HasFlag(CronExpressionFlag.NearestWeekday))
		{
			fieldValue4 = CronField.DaysOfMonth.First;
		}
		if ((GetBit(_dayOfMonth, fieldValue4) || Move(_dayOfMonth, ref fieldValue4)) && GetBit(_month, fieldValue5))
		{
			goto IL_00f6;
		}
		goto IL_01bc;
		IL_01bc:
		if (!Move(_month, ref fieldValue5) && ++num >= 2099)
		{
			return 0L;
		}
		fieldValue4 = firstSet;
		goto IL_00f6;
		IL_00f6:
		while (fieldValue4 <= GetLastDayOfMonth(num, fieldValue5))
		{
			if (HasFlag(CronExpressionFlag.DayOfMonthLast))
			{
				fieldValue4 = GetLastDayOfMonth(num, fieldValue5);
			}
			int num2 = fieldValue4;
			if (HasFlag(CronExpressionFlag.NearestWeekday))
			{
				fieldValue4 = CalendarHelper.MoveToNearestWeekDay(num, fieldValue5, fieldValue4);
			}
			if (IsDayOfWeekMatch(num, fieldValue5, fieldValue4))
			{
				if (!CalendarHelper.IsGreaterThan(num, fieldValue5, fieldValue4, year, month, day))
				{
					if (fieldValue3 <= hour)
					{
						if (fieldValue2 > minute)
						{
							goto IL_017d;
						}
						goto IL_018a;
					}
				}
				else
				{
					fieldValue3 = GetFirstSet(_hour);
				}
				fieldValue2 = GetFirstSet(_minute);
				goto IL_017d;
			}
			goto IL_01a5;
			IL_01a5:
			fieldValue4 = num2;
			if (!Move(_dayOfMonth, ref fieldValue4))
			{
				break;
			}
			continue;
			IL_018a:
			long num3 = CalendarHelper.DateTimeToTicks(num, fieldValue5, fieldValue4, fieldValue3, fieldValue2, fieldValue);
			if (num3 >= ticks)
			{
				return num3;
			}
			goto IL_01a5;
			IL_017d:
			fieldValue = GetFirstSet(_second);
			goto IL_018a;
		}
		goto IL_01bc;
	}

	private static bool Move(long fieldBits, ref int fieldValue)
	{
		if (fieldBits >> ++fieldValue == 0L)
		{
			fieldValue = GetFirstSet(fieldBits);
			return false;
		}
		fieldValue += GetFirstSet(fieldBits >> fieldValue);
		return true;
	}

	private int GetLastDayOfMonth(int year, int month)
	{
		return CalendarHelper.GetDaysInMonth(year, month) - _lastMonthOffset;
	}

	private bool IsDayOfWeekMatch(int year, int month, int day)
	{
		if ((HasFlag(CronExpressionFlag.DayOfWeekLast) && !CalendarHelper.IsLastDayOfWeek(year, month, day)) || (HasFlag(CronExpressionFlag.NthDayOfWeek) && !CalendarHelper.IsNthDayOfWeek(day, _nthDayOfWeek)))
		{
			return false;
		}
		if (_dayOfWeek == CronField.DaysOfWeek.AllBits)
		{
			return true;
		}
		DayOfWeek dayOfWeek = CalendarHelper.GetDayOfWeek(year, month, day);
		return ((_dayOfWeek >> (int)dayOfWeek) & 1) != 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int GetFirstSet(long value)
	{
		ulong num = (ulong)((value & -value) * 157587932685088877L) >> 58;
		return DeBruijnPositions[num];
	}

	private bool HasFlag(CronExpressionFlag value)
	{
		return (_flags & value) != 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static void SkipWhiteSpaces(ref char* pointer)
	{
		while (IsWhiteSpace(*pointer))
		{
			pointer++;
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static void ParseWhiteSpace(CronField prevField, ref char* pointer)
	{
		if (!IsWhiteSpace(*pointer))
		{
			ThrowFormatException(prevField, "Unexpected character '{0}'.", *pointer);
		}
		SkipWhiteSpaces(ref pointer);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static void ParseEndOfString(ref char* pointer)
	{
		if (!IsWhiteSpace(*pointer) && !IsEndOfString(*pointer))
		{
			ThrowFormatException(CronField.DaysOfWeek, "Unexpected character '{0}'.", *pointer);
		}
		SkipWhiteSpaces(ref pointer);
		if (!IsEndOfString(*pointer))
		{
			ThrowFormatException("Unexpected character '{0}'.", *pointer);
		}
	}

	private unsafe static CronExpression ParseMacro(ref char* pointer)
	{
		switch (ToUpper(*(pointer++)))
		{
		case 65:
			if (AcceptCharacter(ref pointer, 'N') && AcceptCharacter(ref pointer, 'N') && AcceptCharacter(ref pointer, 'U') && AcceptCharacter(ref pointer, 'A') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'Y'))
			{
				return Yearly;
			}
			return null;
		case 68:
			if (AcceptCharacter(ref pointer, 'A') && AcceptCharacter(ref pointer, 'I') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'Y'))
			{
				return Daily;
			}
			return null;
		case 69:
			if (AcceptCharacter(ref pointer, 'V') && AcceptCharacter(ref pointer, 'E') && AcceptCharacter(ref pointer, 'R') && AcceptCharacter(ref pointer, 'Y') && Accept(ref pointer, '_'))
			{
				if (AcceptCharacter(ref pointer, 'M') && AcceptCharacter(ref pointer, 'I') && AcceptCharacter(ref pointer, 'N') && AcceptCharacter(ref pointer, 'U') && AcceptCharacter(ref pointer, 'T') && AcceptCharacter(ref pointer, 'E'))
				{
					return Minutely;
				}
				if (*(pointer - 1) != '_')
				{
					return null;
				}
				if (AcceptCharacter(ref pointer, 'S') && AcceptCharacter(ref pointer, 'E') && AcceptCharacter(ref pointer, 'C') && AcceptCharacter(ref pointer, 'O') && AcceptCharacter(ref pointer, 'N') && AcceptCharacter(ref pointer, 'D'))
				{
					return Secondly;
				}
			}
			return null;
		case 72:
			if (AcceptCharacter(ref pointer, 'O') && AcceptCharacter(ref pointer, 'U') && AcceptCharacter(ref pointer, 'R') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'Y'))
			{
				return Hourly;
			}
			return null;
		case 77:
			if (AcceptCharacter(ref pointer, 'O') && AcceptCharacter(ref pointer, 'N') && AcceptCharacter(ref pointer, 'T') && AcceptCharacter(ref pointer, 'H') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'Y'))
			{
				return Monthly;
			}
			if (ToUpper(*(pointer - 1)) == 77 && AcceptCharacter(ref pointer, 'I') && AcceptCharacter(ref pointer, 'D') && AcceptCharacter(ref pointer, 'N') && AcceptCharacter(ref pointer, 'I') && AcceptCharacter(ref pointer, 'G') && AcceptCharacter(ref pointer, 'H') && AcceptCharacter(ref pointer, 'T'))
			{
				return Daily;
			}
			return null;
		case 87:
			if (AcceptCharacter(ref pointer, 'E') && AcceptCharacter(ref pointer, 'E') && AcceptCharacter(ref pointer, 'K') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'Y'))
			{
				return Weekly;
			}
			return null;
		case 89:
			if (AcceptCharacter(ref pointer, 'E') && AcceptCharacter(ref pointer, 'A') && AcceptCharacter(ref pointer, 'R') && AcceptCharacter(ref pointer, 'L') && AcceptCharacter(ref pointer, 'Y'))
			{
				return Yearly;
			}
			return null;
		default:
			pointer--;
			return null;
		}
	}

	private unsafe static long ParseField(CronField field, ref char* pointer, ref CronExpressionFlag flags)
	{
		if (Accept(ref pointer, '*') || Accept(ref pointer, '?'))
		{
			if (field.CanDefineInterval)
			{
				flags |= CronExpressionFlag.Interval;
			}
			return ParseStar(field, ref pointer);
		}
		int low = ParseValue(field, ref pointer);
		long num = ParseRange(field, ref pointer, low, ref flags);
		if (Accept(ref pointer, ','))
		{
			num |= ParseList(field, ref pointer, ref flags);
		}
		return num;
	}

	private unsafe static long ParseDayOfMonth(ref char* pointer, ref CronExpressionFlag flags, ref byte lastDayOffset)
	{
		CronField daysOfMonth = CronField.DaysOfMonth;
		if (Accept(ref pointer, '*') || Accept(ref pointer, '?'))
		{
			return ParseStar(daysOfMonth, ref pointer);
		}
		if (AcceptCharacter(ref pointer, 'L'))
		{
			return ParseLastDayOfMonth(daysOfMonth, ref pointer, ref flags, ref lastDayOffset);
		}
		int num = ParseValue(daysOfMonth, ref pointer);
		if (AcceptCharacter(ref pointer, 'W'))
		{
			flags |= CronExpressionFlag.NearestWeekday;
			return GetBit(num);
		}
		long num2 = ParseRange(daysOfMonth, ref pointer, num, ref flags);
		if (Accept(ref pointer, ','))
		{
			num2 |= ParseList(daysOfMonth, ref pointer, ref flags);
		}
		return num2;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static long ParseDayOfWeek(ref char* pointer, ref CronExpressionFlag flags, ref byte nthWeekDay)
	{
		CronField daysOfWeek = CronField.DaysOfWeek;
		if (Accept(ref pointer, '*') || Accept(ref pointer, '?'))
		{
			return ParseStar(daysOfWeek, ref pointer);
		}
		int num = ParseValue(daysOfWeek, ref pointer);
		if (AcceptCharacter(ref pointer, 'L'))
		{
			return ParseLastWeekDay(num, ref flags);
		}
		if (Accept(ref pointer, '#'))
		{
			return ParseNthWeekDay(daysOfWeek, ref pointer, num, ref flags, out nthWeekDay);
		}
		long num2 = ParseRange(daysOfWeek, ref pointer, num, ref flags);
		if (Accept(ref pointer, ','))
		{
			num2 |= ParseList(daysOfWeek, ref pointer, ref flags);
		}
		return num2;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static long ParseStar(CronField field, ref char* pointer)
	{
		if (!Accept(ref pointer, '/'))
		{
			return field.AllBits;
		}
		return ParseStep(field, ref pointer, field.First, field.Last);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static long ParseList(CronField field, ref char* pointer, ref CronExpressionFlag flags)
	{
		int low = ParseValue(field, ref pointer);
		long num = ParseRange(field, ref pointer, low, ref flags);
		while (Accept(ref pointer, ','))
		{
			num |= ParseList(field, ref pointer, ref flags);
		}
		return num;
	}

	private unsafe static long ParseRange(CronField field, ref char* pointer, int low, ref CronExpressionFlag flags)
	{
		if (!Accept(ref pointer, '-'))
		{
			if (!Accept(ref pointer, '/'))
			{
				return GetBit(low);
			}
			if (field.CanDefineInterval)
			{
				flags |= CronExpressionFlag.Interval;
			}
			return ParseStep(field, ref pointer, low, field.Last);
		}
		if (field.CanDefineInterval)
		{
			flags |= CronExpressionFlag.Interval;
		}
		int num = ParseValue(field, ref pointer);
		if (Accept(ref pointer, '/'))
		{
			return ParseStep(field, ref pointer, low, num);
		}
		return GetBits(field, low, num, 1);
	}

	private unsafe static long ParseStep(CronField field, ref char* pointer, int low, int high)
	{
		int step = ParseNumber(field, ref pointer, 1, field.Last);
		return GetBits(field, low, high, step);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static long ParseLastDayOfMonth(CronField field, ref char* pointer, ref CronExpressionFlag flags, ref byte lastMonthOffset)
	{
		flags |= CronExpressionFlag.DayOfMonthLast;
		if (Accept(ref pointer, '-'))
		{
			lastMonthOffset = (byte)ParseNumber(field, ref pointer, 0, field.Last - 1);
		}
		if (AcceptCharacter(ref pointer, 'W'))
		{
			flags |= CronExpressionFlag.NearestWeekday;
		}
		return field.AllBits;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static long ParseNthWeekDay(CronField field, ref char* pointer, int dayOfWeek, ref CronExpressionFlag flags, out byte nthDayOfWeek)
	{
		nthDayOfWeek = (byte)ParseNumber(field, ref pointer, 1, 5);
		flags |= CronExpressionFlag.NthDayOfWeek;
		return GetBit(dayOfWeek);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static long ParseLastWeekDay(int dayOfWeek, ref CronExpressionFlag flags)
	{
		flags |= CronExpressionFlag.DayOfWeekLast;
		return GetBit(dayOfWeek);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool Accept(ref char* pointer, char character)
	{
		if (*pointer == character)
		{
			pointer++;
			return true;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static bool AcceptCharacter(ref char* pointer, char character)
	{
		if (ToUpper(*pointer) == character)
		{
			pointer++;
			return true;
		}
		return false;
	}

	private unsafe static int ParseNumber(CronField field, ref char* pointer, int low, int high)
	{
		int number = GetNumber(ref pointer, null);
		if (number == -1 || number < low || number > high)
		{
			ThrowFormatException(field, "Value must be a number between {0} and {1} (all inclusive).", low, high);
		}
		return number;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private unsafe static int ParseValue(CronField field, ref char* pointer)
	{
		int number = GetNumber(ref pointer, field.Names);
		if (number == -1 || number < field.First || number > field.Last)
		{
			ThrowFormatException(field, "Value must be a number between {0} and {1} (all inclusive).", field.First, field.Last);
		}
		return number;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static StringBuilder AppendFieldValue(StringBuilder expressionBuilder, CronField field, long fieldValue)
	{
		if (field.AllBits == fieldValue)
		{
			return expressionBuilder.Append('*');
		}
		if (field == CronField.DaysOfWeek)
		{
			fieldValue &= ~(1 << field.Last);
		}
		int firstSet = GetFirstSet(fieldValue);
		while (true)
		{
			expressionBuilder.Append(firstSet);
			if (fieldValue >> ++firstSet == 0L)
			{
				break;
			}
			expressionBuilder.Append(',');
			firstSet = GetFirstSet(fieldValue >> firstSet << firstSet);
		}
		return expressionBuilder;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private StringBuilder AppendDayOfMonth(StringBuilder expressionBuilder, int domValue)
	{
		if (HasFlag(CronExpressionFlag.DayOfMonthLast))
		{
			expressionBuilder.Append('L');
			if (_lastMonthOffset != 0)
			{
				expressionBuilder.Append($"-{_lastMonthOffset}");
			}
		}
		else
		{
			AppendFieldValue(expressionBuilder, CronField.DaysOfMonth, (uint)domValue);
		}
		if (HasFlag(CronExpressionFlag.NearestWeekday))
		{
			expressionBuilder.Append('W');
		}
		return expressionBuilder;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private void AppendDayOfWeek(StringBuilder expressionBuilder, int dowValue)
	{
		AppendFieldValue(expressionBuilder, CronField.DaysOfWeek, dowValue);
		if (HasFlag(CronExpressionFlag.DayOfWeekLast))
		{
			expressionBuilder.Append('L');
		}
		else if (HasFlag(CronExpressionFlag.NthDayOfWeek))
		{
			expressionBuilder.Append($"#{_nthDayOfWeek}");
		}
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static long GetBits(CronField field, int num1, int num2, int step)
	{
		if (num2 < num1)
		{
			return GetReversedRangeBits(field, num1, num2, step);
		}
		if (step == 1)
		{
			return (1L << num2 + 1) - (1L << num1);
		}
		return GetRangeBits(num1, num2, step);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static long GetRangeBits(int low, int high, int step)
	{
		long value = 0L;
		for (int i = low; i <= high; i += step)
		{
			SetBit(ref value, i);
		}
		return value;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static long GetReversedRangeBits(CronField field, int num1, int num2, int step)
	{
		int num3 = field.Last;
		if (field == CronField.DaysOfWeek)
		{
			num3--;
		}
		long rangeBits = GetRangeBits(num1, num3, step);
		num1 = field.First + step - (num3 - num1) % step - 1;
		return rangeBits | GetRangeBits(num1, num2, step);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static long GetBit(int num1)
	{
		return 1L << num1;
	}

	private unsafe static int GetNumber(ref char* pointer, int[] names)
	{
		if (IsDigit(*pointer))
		{
			int numeric = GetNumeric(*(pointer++));
			if (!IsDigit(*pointer))
			{
				return numeric;
			}
			numeric = numeric * 10 + GetNumeric(*(pointer++));
			if (!IsDigit(*pointer))
			{
				return numeric;
			}
			return -1;
		}
		if (names == null)
		{
			return -1;
		}
		if (!IsLetter(*pointer))
		{
			return -1;
		}
		int num = ToUpper(*(pointer++));
		if (!IsLetter(*pointer))
		{
			return -1;
		}
		num |= ToUpper(*(pointer++)) << 8;
		if (!IsLetter(*pointer))
		{
			return -1;
		}
		num |= ToUpper(*(pointer++)) << 16;
		int num2 = names.Length;
		for (int i = 0; i < num2; i++)
		{
			if (num == names[i])
			{
				return i;
			}
		}
		return -1;
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void ThrowFormatException(CronField field, string format, params object[] args)
	{
		throw new CronFormatException(field, string.Format(format, args));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void ThrowFormatException(string format, params object[] args)
	{
		throw new CronFormatException(string.Format(format, args));
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void ThrowFromShouldBeLessThanToException(string fromName, string toName)
	{
		throw new ArgumentException("The value of the " + fromName + " argument should be less than the value of the " + toName + " argument.", fromName);
	}

	[MethodImpl(MethodImplOptions.NoInlining)]
	private static void ThrowWrongDateTimeKindException(string paramName)
	{
		throw new ArgumentException("The supplied DateTime must have the Kind property set to Utc", paramName);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool GetBit(long value, int index)
	{
		return (value & (1L << index)) != 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static void SetBit(ref long value, int index)
	{
		value |= 1L << index;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsEndOfString(int code)
	{
		return code == 0;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsWhiteSpace(int code)
	{
		if (code != 9)
		{
			return code == 32;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsDigit(int code)
	{
		if (code >= 48)
		{
			return code <= 57;
		}
		return false;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static bool IsLetter(int code)
	{
		if (code < 65 || code > 90)
		{
			if (code >= 97)
			{
				return code <= 122;
			}
			return false;
		}
		return true;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int GetNumeric(int code)
	{
		return code - 48;
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static int ToUpper(int code)
	{
		if (code >= 97 && code <= 122)
		{
			return code - 32;
		}
		return code;
	}
}
[Flags]
internal enum CronExpressionFlag : byte
{
	DayOfMonthLast = 1,
	DayOfWeekLast = 2,
	Interval = 4,
	NearestWeekday = 8,
	NthDayOfWeek = 0x10
}
internal sealed class CronField
{
	private static readonly string[] MonthNames;

	private static readonly string[] DayOfWeekNames;

	private static readonly int[] MonthNamesArray;

	private static readonly int[] DayOfWeekNamesArray;

	public static readonly CronField DaysOfWeek;

	public static readonly CronField Months;

	public static readonly CronField DaysOfMonth;

	public static readonly CronField Hours;

	public static readonly CronField Minutes;

	public static readonly CronField Seconds;

	public readonly string Name;

	public readonly int First;

	public readonly int Last;

	public readonly int[] Names;

	public readonly bool CanDefineInterval;

	public readonly long AllBits;

	static CronField()
	{
		MonthNames = new string[13]
		{
			null, "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP",
			"OCT", "NOV", "DEC"
		};
		DayOfWeekNames = new string[8] { "SUN", "MON", "TUE", "WED", "THU", "FRI", "SAT", "SUN" };
		MonthNamesArray = new int[MonthNames.Length];
		DayOfWeekNamesArray = new int[DayOfWeekNames.Length];
		DaysOfWeek = new CronField("Days of week", 0, 7, DayOfWeekNamesArray, canDefineInterval: false);
		Months = new CronField("Months", 1, 12, MonthNamesArray, canDefineInterval: false);
		DaysOfMonth = new CronField("Days of month", 1, 31, null, canDefineInterval: false);
		Hours = new CronField("Hours", 0, 23, null, canDefineInterval: true);
		Minutes = new CronField("Minutes", 0, 59, null, canDefineInterval: true);
		Seconds = new CronField("Seconds", 0, 59, null, canDefineInterval: true);
		for (int i = 1; i < MonthNames.Length; i++)
		{
			string text = MonthNames[i].ToUpperInvariant();
			_ = new char[3]
			{
				text[0],
				text[1],
				text[2]
			};
			int num = (int)(text[0] | ((uint)text[1] << 8) | ((uint)text[2] << 16));
			MonthNamesArray[i] = num;
		}
		for (int j = 0; j < DayOfWeekNames.Length; j++)
		{
			string text2 = DayOfWeekNames[j].ToUpperInvariant();
			_ = new char[3]
			{
				text2[0],
				text2[1],
				text2[2]
			};
			int num2 = (int)(text2[0] | ((uint)text2[1] << 8) | ((uint)text2[2] << 16));
			DayOfWeekNamesArray[j] = num2;
		}
	}

	private CronField(string name, int first, int last, int[] names, bool canDefineInterval)
	{
		Name = name;
		First = first;
		Last = last;
		Names = names;
		CanDefineInterval = canDefineInterval;
		for (int i = First; i <= Last; i++)
		{
			AllBits |= 1L << i;
		}
	}

	public override string ToString()
	{
		return Name;
	}
}
[Flags]
public enum CronFormat
{
	Standard = 0,
	IncludeSeconds = 1
}
[Serializable]
public class CronFormatException : FormatException
{
	public CronFormatException(string message)
		: base(message)
	{
	}

	internal CronFormatException(CronField field, string message)
		: this($"{field}: {message}")
	{
	}
}
internal static class DateTimeHelper
{
	private static readonly TimeSpan OneSecond = TimeSpan.FromSeconds(1.0);

	public static DateTimeOffset FloorToSeconds(DateTimeOffset dateTimeOffset)
	{
		return dateTimeOffset.AddTicks(-GetExtraTicks(dateTimeOffset.Ticks));
	}

	public static bool IsRound(DateTimeOffset dateTimeOffset)
	{
		return GetExtraTicks(dateTimeOffset.Ticks) == 0;
	}

	private static long GetExtraTicks(long ticks)
	{
		TimeSpan oneSecond = OneSecond;
		return ticks % oneSecond.Ticks;
	}
}
internal static class TimeZoneHelper
{
	public static bool IsAmbiguousTime(TimeZoneInfo zone, DateTime ambiguousTime)
	{
		return zone.IsAmbiguousTime(ambiguousTime.AddTicks(1L));
	}

	public static TimeSpan GetDaylightOffset(TimeZoneInfo zone, DateTime ambiguousDateTime)
	{
		TimeSpan[] ambiguousOffsets = GetAmbiguousOffsets(zone, ambiguousDateTime);
		TimeSpan baseUtcOffset = zone.BaseUtcOffset;
		if (ambiguousOffsets[0] != baseUtcOffset)
		{
			return ambiguousOffsets[0];
		}
		return ambiguousOffsets[1];
	}

	public static DateTimeOffset GetDaylightTimeStart(TimeZoneInfo zone, DateTime invalidDateTime)
	{
		DateTime dateTime = new DateTime(invalidDateTime.Year, invalidDateTime.Month, invalidDateTime.Day, invalidDateTime.Hour, invalidDateTime.Minute, 0, 0, invalidDateTime.Kind);
		while (zone.IsInvalidTime(dateTime))
		{
			dateTime = dateTime.AddMinutes(1.0);
		}
		TimeSpan utcOffset = zone.GetUtcOffset(dateTime);
		return new DateTimeOffset(dateTime, utcOffset);
	}

	public static DateTimeOffset GetStandardTimeStart(TimeZoneInfo zone, DateTime ambiguousTime, TimeSpan daylightOffset)
	{
		return new DateTimeOffset(GetDstTransitionEndDateTime(zone, ambiguousTime), daylightOffset).ToOffset(zone.BaseUtcOffset);
	}

	public static DateTimeOffset GetAmbiguousIntervalEnd(TimeZoneInfo zone, DateTime ambiguousTime)
	{
		return new DateTimeOffset(GetDstTransitionEndDateTime(zone, ambiguousTime), zone.BaseUtcOffset);
	}

	public static DateTimeOffset GetDaylightTimeEnd(TimeZoneInfo zone, DateTime ambiguousTime, TimeSpan daylightOffset)
	{
		return new DateTimeOffset(GetDstTransitionEndDateTime(zone, ambiguousTime).AddTicks(-1L), daylightOffset);
	}

	private static TimeSpan[] GetAmbiguousOffsets(TimeZoneInfo zone, DateTime ambiguousTime)
	{
		return zone.GetAmbiguousTimeOffsets(ambiguousTime.AddTicks(1L));
	}

	private static DateTime GetDstTransitionEndDateTime(TimeZoneInfo zone, DateTime ambiguousDateTime)
	{
		DateTime dateTime = new DateTime(ambiguousDateTime.Year, ambiguousDateTime.Month, ambiguousDateTime.Day, ambiguousDateTime.Hour, ambiguousDateTime.Minute, 0, 0, ambiguousDateTime.Kind);
		while (zone.IsAmbiguousTime(dateTime))
		{
			dateTime = dateTime.AddMinutes(1.0);
		}
		return dateTime;
	}
}
