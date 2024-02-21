using EducationBenchmarking.Web.Attributes;

namespace EducationBenchmarking.Web.Extensions;

public static class EnumExtensions
{
    public static string GetStringValue(this Enum value)
    {
        var type = value.GetType();
        var fieldInfo = type.GetField(value.ToString());
        var attribs = fieldInfo?
            .GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];

        return attribs is null || attribs.Length == 0 ? "" : attribs[0].StringValue;
    }
}