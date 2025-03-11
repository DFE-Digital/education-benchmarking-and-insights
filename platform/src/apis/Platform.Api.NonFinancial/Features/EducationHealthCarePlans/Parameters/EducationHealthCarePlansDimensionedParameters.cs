using System.Collections.Specialized;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;

public record EducationHealthCarePlansDimensionedParameters : EducationHealthCarePlansParameters
{
    public string? Dimension { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);
        Dimension = query["dimension"];
    }
}