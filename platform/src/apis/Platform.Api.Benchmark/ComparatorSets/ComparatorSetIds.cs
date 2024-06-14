using System;
using System.Collections.Generic;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

public class ComparatorSetIds : List<string>
{
    public override string ToString()
    {
        return ToArray().ToJson();
    }

    public static ComparatorSetIds FromString(string? value)
    {
        var ids = value?.FromJson<string[]>() ?? Array.Empty<string>();
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