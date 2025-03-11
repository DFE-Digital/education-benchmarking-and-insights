using System.Collections.Specialized;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlans.Parameters;

public class EducationHealthCarePlansDimensionedParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        const string dimension = nameof(dimension);
        var values = new NameValueCollection
        {
            { "code", "101" },
            { "code", "102" },
            { "code", "103" },
            { "dimension", dimension }
        };

        var parameters = new EducationHealthCarePlansDimensionedParameters();
        parameters.SetValues(values);

        Assert.Equal(["101", "102", "103"], parameters.Codes);
        Assert.Equal(dimension, parameters.Dimension);
    }
}