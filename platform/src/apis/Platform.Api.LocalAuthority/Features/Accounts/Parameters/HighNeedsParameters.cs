using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Accounts.Parameters;

public record HighNeedsParameters : QueryParameters
{
    public string[] Codes { get; private set; } = [];
    public string Dimension { get; private set; } = Dimensions.HighNeeds.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        Codes = query.ToStringArray("code");

        if (query.TryGetValue("dimension", out var dimension))
        {
            Dimension = dimension;
        }
    }
}