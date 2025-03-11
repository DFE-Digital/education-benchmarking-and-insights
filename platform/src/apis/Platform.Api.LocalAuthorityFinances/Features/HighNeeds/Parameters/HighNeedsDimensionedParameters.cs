using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;

public record HighNeedsDimensionedParameters : HighNeedsParameters
{
    public string Dimension { get; private set; } = Dimensions.HighNeeds.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        if (query.TryGetValue("dimension", out var dimension))
        {
            Dimension = dimension;
        }
    }
}