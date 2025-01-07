using Microsoft.AspNetCore.Http;
using Platform.Functions;
namespace Platform.Api.Insight.Income;

public record IncomeParameters : QueryParameters
{
    public string Dimension { get; private set; } = IncomeDimensions.Actuals;

    public override void SetValues(IQueryCollection query)
    {
        if (query.TryGetValue("dimension", out var dimension) && !string.IsNullOrWhiteSpace(dimension))
        {
            Dimension = dimension.ToString();
        }
    }
}