using Microsoft.AspNetCore.Http;
using Platform.Functions;
namespace Platform.Api.Insight.Census;

public record CensusParameters : QueryParameters
{
    public string? Category { get; internal set; }
    public string Dimension { get; internal set; } = CensusDimensions.Total;

    public override void SetValues(IQueryCollection query)
    {
        if (query.TryGetValue("category", out var category) && !string.IsNullOrWhiteSpace(category))
        {
            Category = category.ToString();
        }

        if (query.TryGetValue("dimension", out var dimension) && !string.IsNullOrWhiteSpace(dimension))
        {
            Dimension = dimension.ToString();
        }
    }
}