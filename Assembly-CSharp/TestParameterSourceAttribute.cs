using System;

[AttributeUsage(AttributeTargets.Method)]
public class TestParameterSourceAttribute : Attribute
{
	public string SourceName { get; private set; }

	public TestParameterSourceAttribute(string sourceName)
	{
		SourceName = sourceName;
	}
}
