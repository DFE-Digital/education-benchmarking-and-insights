using System.Collections.Specialized;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Parameters;
using Platform.Domain;
using Platform.Api.LocalAuthorityFinances.Features.Schools.Models;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.Schools.Parameters;

public class FinanceSummaryParametersTests
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var values = new NameValueCollection
        {
            { "dimension", "Actuals" },
            { "sortField", "TotalExpenditure" },
            { "sortOrder", "DESC" },
            { "overallPhase", "Primary" },
            { "nurseryProvision", "Not applicable" },
            { "nurseryProvision", "Has Nursery Classes" },
            { "sixthFormProvision", "Has a sixth form" },
            { "specialClassesProvision", "Has Special Classes" },
            { "limit", "7" }
        };

        var parameters = new FinanceSummaryParameters();
        parameters.SetValues(values);

        Assert.Equal("Actuals", parameters.Dimension);
        Assert.Equal("TotalExpenditure", parameters.SortField);
        Assert.Equal("DESC", parameters.SortOrder);
        Assert.Equal("Primary", parameters.OverallPhase);
        Assert.Equal(["Not applicable", "Has Nursery Classes"], parameters.NurseryProvision);
        Assert.Equal(["Has a sixth form"], parameters.SixthFormProvision);
        Assert.Equal(["Has Special Classes"], parameters.SpecialClassesProvision);
        Assert.Equal("7", parameters.Limit);
    }

    [Fact]
    public void ShouldUseDefaultsWhenQueryIsEmpty()
    {
        var values = new NameValueCollection();

        var parameters = new FinanceSummaryParameters();
        parameters.SetValues(values);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Equal(FinanceSummarySortFields.SchoolName, parameters.SortField);
        Assert.Equal(SortDirection.Asc, parameters.SortOrder);
        Assert.Null(parameters.OverallPhase);
        Assert.Empty(parameters.NurseryProvision);
        Assert.Empty(parameters.SixthFormProvision);
        Assert.Empty(parameters.SpecialClassesProvision);
        Assert.Null(parameters.Limit);
    }
}