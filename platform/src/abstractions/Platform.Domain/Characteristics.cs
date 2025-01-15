using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record CharacteristicList
{
    public string[] Values { get; init; } = [];
}

[ExcludeFromCodeCoverage]
public record CharacteristicValueBool
{
    public bool Values { get; init; }
}

[ExcludeFromCodeCoverage]
public record CharacteristicRange
{
    public decimal From { get; init; }
    public decimal To { get; init; }
}

[ExcludeFromCodeCoverage]
public record CharacteristicDateRange
{
    public DateTime From { get; init; }
    public DateTime To { get; init; }
}

public static class ExpressionBuilder
{
    public static List<string> NotValueFilter(this List<string> list, string fieldName, string? value)
    {
        if (value != null)
        {
            list.Add($"({fieldName} ne '{value}')");
        }

        return list;
    }

    public static List<string> RangeFilter(this List<string> list, string fieldName, CharacteristicRange? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"(({fieldName} ge {characteristic.From}) and ({fieldName} le {characteristic.To}))");
        }

        return list;
    }

    public static List<string> RangeFilter(this List<string> list, string fieldName, CharacteristicValueBool? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"({fieldName} eq {characteristic.Values.ToString().ToLowerInvariant()})");
        }

        return list;
    }

    public static List<string> RangeFilter(this List<string> list, string fieldName, CharacteristicDateRange? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"(({fieldName} ge {characteristic.From:s}Z) and ({fieldName} le {characteristic.To:s}Z))");
        }

        return list;
    }

    public static List<string> ValuesFilter(this List<string> list, string fieldName, CharacteristicList? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"(({fieldName} eq {string.Join($" or ({fieldName} eq ", characteristic.Values.Select(a => $"'{a}')"))})");
        }

        return list;
    }

    public static string BuildFilter(this List<string> list) => string.Join(" and ", list);

    public static List<string> ListSearch(this List<string> list, string fieldName, CharacteristicList? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"{fieldName}:({string.Join(" OR ", characteristic.Values.Select(a => $"\"{a}\""))})");
        }

        return list;
    }

    public static string BuildSearch(this List<string> list) => list.Count > 0 ? string.Join(" AND ", list) : "*";
}