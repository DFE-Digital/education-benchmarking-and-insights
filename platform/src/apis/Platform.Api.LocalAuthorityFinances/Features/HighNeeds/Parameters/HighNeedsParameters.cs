using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;

public record HighNeedsParameters : QueryParameters
{
    public string[] Codes { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        Codes = query.ToStringArray("code");
    }
}