using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Schools;

public record SchoolsParameters : QueryParameters
{
    public string[] Schools { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
    {
        Schools = query.ToStringArray("urns");
    }
}