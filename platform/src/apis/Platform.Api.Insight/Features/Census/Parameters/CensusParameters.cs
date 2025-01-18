using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.Insight.Features.Census.Parameters;

public record CensusParameters : QueryParameters
{
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = Dimensions.Census.Total;

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Census.Total;
        Category = query["category"];
    }
}