using System.Diagnostics.CodeAnalysis;
using Platform.Json;

namespace Platform.Api.Insight.Features.CommercialResources.Models;

[ExcludeFromCodeCoverage]
public record CommercialResource
{
    public string? Title { get; set; }
    public string? Url { get; set; }
    public CategoryCollection? Category { get; set; }
    public CategoryCollection? SubCategory { get; set; }
}

public class CategoryCollection(string[] categories)
{
    public CategoryCollection() : this([])
    {
    }

    public override string ToString() => categories.ToJson();

    public static CategoryCollection FromString(string? value)
    {
        var values = value?.FromJson<string[]>() ?? [];
        return new CategoryCollection(values);
    }

    public string[] Items => categories;
}
