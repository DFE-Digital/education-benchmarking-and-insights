﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Platform.Api.Benchmark.Comparators;

public record CharacteristicList
{
    public string[] Values { get; set; } = Array.Empty<string>();
}

public record CharacteristicValueBool
{
    public bool Values { get; set; }
}

public record CharacteristicRange
{
    public decimal From { get; set; }
    public decimal To { get; set; }
}

public static class ExpressionBuilder
{
    public static List<string> AddNotValueFilter(this List<string> list, string fieldName, string? value)
    {
        if (value != null)
        {
            list.Add($"({fieldName} ne '{value}')");
        }

        return list;
    }

    public static List<string> AddRangeFilter(this List<string> list, string fieldName, CharacteristicRange? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"(({fieldName} ge {characteristic.From}) and ({fieldName} le {characteristic.To}))");
        }

        return list;
    }

    public static List<string> AddRangeFilter(this List<string> list, string fieldName, CharacteristicValueBool? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"({fieldName} eq {characteristic.Values.ToString().ToLowerInvariant()})");
        }

        return list;
    }

    public static string BuildFilter(this List<string> list)
    {
        return string.Join(" and ", list);
    }

    public static List<string> AddListSearch(this List<string> list, string fieldName, CharacteristicList? characteristic)
    {
        if (characteristic is not null)
        {
            list.Add($"{fieldName}:({string.Join(" OR ", characteristic.Values.Select(a => $"'{a}'"))})");
        }

        return list;
    }

    public static string BuildSearch(this List<string> list)
    {
        return string.Join(" AND ", list);
    }
}