using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.School.Features.Census.Parameters;

public record SeniorLeadershipParameters : QueryParameters
{
    public string[] Urns { get; private set; } = [];
    public string Dimension { get; private set; } = Dimensions.Census.Total;

    public override void SetValues(NameValueCollection query)
    {
        Urns = query["urns"]?.Split(',') ?? [];
        Dimension = query["dimension"] ?? Dimensions.Census.Total;
    }
}