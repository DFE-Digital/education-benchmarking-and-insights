using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Platform.Json;

namespace Platform.Api.Insight.Features.CommercialResources.Models;

[ExcludeFromCodeCoverage]
public record CommercialResourcesResponse
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public CommercialResourcesList? Category { get; set; }
    public CommercialResourcesList? SubCategory { get; set; }
}

public class CommercialResourcesList : List<string>
{
    public override string ToString() => ToArray().ToJson();

    public static CommercialResourcesList FromString(string? value)
    {
        var items = value?.FromJson<string[]>() ?? [];
        var result = new CommercialResourcesList();
        result.AddRange(items);
        return result;
    }
}