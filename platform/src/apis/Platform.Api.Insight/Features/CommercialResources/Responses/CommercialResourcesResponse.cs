using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.CommercialResources.Responses;

[ExcludeFromCodeCoverage]
public record CommercialResourcesResponse
{
    public string? Category { get; set; }
    public string? SubCategory { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }
}