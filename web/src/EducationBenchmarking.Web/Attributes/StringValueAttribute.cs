using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Attributes;

[ExcludeFromCodeCoverage]
public class StringValueAttribute(string value) : Attribute
{
    public string StringValue { get; protected set; } = value;
}