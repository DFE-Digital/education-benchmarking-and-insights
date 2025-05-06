using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Features.Schools.Parameters;

public record SchoolsParameters : QueryParameters
{
    public string[] Schools { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        Schools = query.ToStringArray("urns");
    }
}