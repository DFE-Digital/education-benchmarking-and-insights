using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace Platform.Api.Benchmark.Comparators;

[ExcludeFromCodeCoverage]
public record CharacteristicList
{
    public string[] Values { get; set; } = Array.Empty<string>();
}

[ExcludeFromCodeCoverage]
public record CharacteristicValueBool
{
    public bool Values { get; set; }
}

[ExcludeFromCodeCoverage]
public record CharacteristicRange
{
    public decimal From { get; set; }
    public decimal To { get; set; }
}

[ExcludeFromCodeCoverage]
public record CharacteristicDateRange
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
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

    public static string BuildFilter(this List<string> list) => string.Join(" and ", list);

    public static List<string> ListSearch(this List<string> list, string fieldName, CharacteristicList? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"{fieldName}:({string.Join(" OR ", characteristic.Values.Select(a => $"'{a}'"))})");
        }

        return list;
    }

    public static string BuildSearch(this List<string> list) => string.Join(" AND ", list);
}