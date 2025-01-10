using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class YearsSchoolQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = new YearsSchoolQuery("urn");
        
        const string expected = "SELECT * FROM VW_YearsSchool WHERE URN = @URN\n";
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
}

public class YearsOverallPhaseQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = new YearsOverallPhaseQuery("phase", "type");

        const string expected = "SELECT * FROM VW_YearsOverallPhase WHERE OverallPhase = @Phase AND FinanceType = @FinanceType\n";
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
}

public class YearsTrustQueryTests
{
    [Fact]
    public void ShouldReturnSql()
    {
        var builder = new YearsTrustQuery("companyNumber");

        const string expected = "SELECT * FROM VW_YearsTrust WHERE CompanyNumber = @CompanyNumber\n";
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
}