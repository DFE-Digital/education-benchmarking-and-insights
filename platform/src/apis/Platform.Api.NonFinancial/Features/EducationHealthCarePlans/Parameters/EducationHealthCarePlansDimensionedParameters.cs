using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions.Extensions;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;

public record EducationHealthCarePlansDimensionedParameters : EducationHealthCarePlansParameters
{
    public string Dimension { get; private set; } = Dimensions.EducationHealthCarePlans.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        if (query.TryGetValue("dimension", out var dimension))
        {
            Dimension = dimension;
        }
    }
}