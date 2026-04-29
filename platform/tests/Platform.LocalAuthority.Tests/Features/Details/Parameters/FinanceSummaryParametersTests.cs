using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Details.Models;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Details.Parameters;

public class FinanceSummaryParametersTests
{
    [Fact]
    public void ShouldSetDefaultValuesWhenQueryIsEmpty()
    {
        var query = new NameValueCollection();
        var parameters = new FinanceSummaryParameters();

        parameters.SetValues(query);

        Assert.Equal(Dimensions.Finance.Actuals, parameters.Dimension);
        Assert.Equal(FinanceSummarySortFields.SchoolName, parameters.SortField);
        Assert.Equal(SortDirection.Asc, parameters.SortOrder);
        Assert.Empty(parameters.OverallPhase);
        Assert.Empty(parameters.NurseryProvision);
        Assert.Empty(parameters.SixthFormProvision);
        Assert.Empty(parameters.SpecialClassesProvision);
        Assert.Null(parameters.Limit);
    }

    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var query = new NameValueCollection
        {
            { "dimension", "testDimension" },
            { "sortField", "testField" },
            { "sortOrder", "testOrder" },
            { "overallPhase", "phase1,phase2" },
            { "nurseryProvision", "nursery1" },
            { "sixthFormProvision", "sixth1,sixth2" },
            { "specialClassesProvision", "special1" },
            { "limit", "10" }
        };

        var parameters = new FinanceSummaryParameters();
        parameters.SetValues(query);

        Assert.Equal("testDimension", parameters.Dimension);
        Assert.Equal("testField", parameters.SortField);
        Assert.Equal("testOrder", parameters.SortOrder);
        Assert.Equal(new[] { "phase1", "phase2" }, parameters.OverallPhase);
        Assert.Equal(new[] { "nursery1" }, parameters.NurseryProvision);
        Assert.Equal(new[] { "sixth1", "sixth2" }, parameters.SixthFormProvision);
        Assert.Equal(new[] { "special1" }, parameters.SpecialClassesProvision);
        Assert.Equal("10", parameters.Limit);
    }
}