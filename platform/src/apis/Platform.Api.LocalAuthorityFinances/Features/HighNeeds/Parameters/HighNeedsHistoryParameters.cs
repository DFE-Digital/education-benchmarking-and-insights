using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;

public record HighNeedsHistoryParameters : QueryParameters
{
    public string[] Codes { get; internal set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        Codes = query.ToStringArray("code");
    }
}