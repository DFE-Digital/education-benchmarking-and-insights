using System.Collections.Specialized;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Parameters;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlansLocalAuthoritiesHistory.Parameters;

public class EducationHealthCarePlansParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "code", "101" },
            { "code", "102" },
            { "code", "103" }
        };

        var parameters = new EducationHealthCarePlansParameters();
        parameters.SetValues(values);

        Assert.Equal(["101", "102", "103"], parameters.Codes);
    }
}