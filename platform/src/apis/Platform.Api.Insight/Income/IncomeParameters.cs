using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Income;

public record IncomeParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; internal set; }
    public string? Category { get; internal set; }
    public string Dimension { get; internal set; } = IncomeDimensions.Actuals;

    public override void SetValues(IQueryCollection query)
    {
        ExcludeCentralServices = query.ToBool("excludeCentralServices");

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