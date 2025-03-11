using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;

public record HighNeedsDimensionedParameters : HighNeedsParameters
{
    public string? Dimension { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);
        Dimension = query["dimension"];
    }
}