﻿using System.Collections.Generic;
using Platform.Json;

namespace Platform.Api.Benchmark.Features.ComparatorSets.Models;

public class ComparatorSetIds : List<string>
{
    public override string ToString() => ToArray().ToJson();

    public static ComparatorSetIds FromString(string? value)
    {
        var ids = value?.FromJson<string[]>() ?? [];
        var result = new ComparatorSetIds();
        result.AddRange(ids);
        return result;
    }

    public static ComparatorSetIds FromCollection(string[] value)
    {
        var result = new ComparatorSetIds();
        result.AddRange(value);
        return result;
    }
}