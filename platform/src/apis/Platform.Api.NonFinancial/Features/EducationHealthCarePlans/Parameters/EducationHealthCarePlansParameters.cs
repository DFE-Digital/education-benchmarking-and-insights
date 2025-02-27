using System.Collections.Specialized;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;

public record EducationHealthCarePlansParameters : QueryParameters
{
    public string[] Codes { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        Codes = query.ToStringArray("code");
    }
}