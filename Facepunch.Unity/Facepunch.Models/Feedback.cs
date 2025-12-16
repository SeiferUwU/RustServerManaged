using System.Collections.Generic;

namespace Facepunch.Models;

[JsonModel]
public struct Feedback
{
	public string Subject;

	public string Message;

	public ReportType Type;

	public string TargetReportType;

	public string TargetId;

	public string TargetName;

	public string TargetEntity;

	public List<ulong> UsersInRange;

	public AppInfo AppInfo;
}
