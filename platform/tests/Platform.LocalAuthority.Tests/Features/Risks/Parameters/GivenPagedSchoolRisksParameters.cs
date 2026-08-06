using System.Collections.Specialized;
using Platform.Api.LocalAuthority.Features.Risks.Parameters;
using Platform.Domain;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Risks.Parameters;

public class GivenPagedSchoolRisksParameters
{
    [Fact]
    public void ShouldSetValuesFromQuery()
    {
        var query = new NameValueCollection
        {
            { "code", "LA1,LA2" },
            { "page", "2" },
            { "pageSize", "15" },
            { "sortField", "sortField" },
            { "sortOrder", "sortOrder" },
            { "phase", "phase" }
        };

        var parameters = new PagedSchoolRisksParameters();
        parameters.SetValues(query);

        Assert.Equal(["LA1", "LA2"], parameters.Codes);
        Assert.Equal(2, parameters.Page);
        Assert.Equal(15, parameters.PageSize);
        Assert.Equal("sortField", parameters.SortField);
        Assert.Equal("sortOrder", parameters.SortOrder);
        Assert.Equal("phase", parameters.Phase);
    }

    [Fact]
    public void ShouldSetDefaultValuesWhenQueryEmpty()
    {
        var query = new NameValueCollection();

        var parameters = new PagedSchoolRisksParameters();
        parameters.SetValues(query);

        Assert.Empty(parameters.Codes);
        Assert.Equal(1, parameters.Page);
        Assert.Equal(10, parameters.PageSize);
    }
}
