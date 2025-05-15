using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Features.CommercialResources.Parameters;

public record CommercialResourcesParameters : QueryParameters
{
    public string[] Categories { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        Categories = query.ToStringArray("categories");
    }
}