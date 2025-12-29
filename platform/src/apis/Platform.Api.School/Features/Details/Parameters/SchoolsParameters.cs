using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Parameters;

public record SchoolsParameters : QueryParameters
{
    public string[] Schools { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        Schools = query.ToStringArray("urns");
    }
}