using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class PlatformQueryTests
{
    [Fact]
    public void ShouldAddUrnEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE URN = @URN\n";
        var builder = new MockPlatformQuery()
            .WhereUrnEqual("12345");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddUrnInParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE URN IN @URNS\n";
        var builder = new MockPlatformQuery()
            .WhereUrnIn(["12345", "12346"]);
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAdRunIdBetweenParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE RunId BETWEEN @StartYear AND @EndYear\n";
        var builder = new MockPlatformQuery()
            .WhereRunIdBetween(0,1);
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddRunIdEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE RunId = @RunId\n";
        var builder = new MockPlatformQuery()
            .WhereRunIdEqual("12345");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddTrustCompanyNumberEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE TrustCompanyNumber = @CompanyNumber\n";
        var builder = new MockPlatformQuery()
            .WhereTrustCompanyNumberEqual("12345");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddCompanyNumberEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE CompanyNumber = @CompanyNumber\n";
        var builder = new MockPlatformQuery()
            .WhereCompanyNumberEqual("12345");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddCompanyNumberInParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE CompanyNumber IN @CompanyNumbers\n";
        var builder = new MockPlatformQuery()
            .WhereCompanyNumberIn(["12345", "12346"]);
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddLaCodeEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE LaCode = @LaCode\n";
        var builder = new MockPlatformQuery()
            .WhereLaCodeEqual("123");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddOverallPhaseEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE OverallPhase = @Phase\n";
        var builder = new MockPlatformQuery()
            .WhereOverallPhaseEqual("phase");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
    
    [Fact]
    public void ShouldAddFinanceTypeEqualParameter()
    {
        const string expected = "SELECT * FROM Mock WHERE FinanceType = @FinanceType\n";
        var builder = new MockPlatformQuery()
            .WhereFinanceTypeEqual("type");
        
        Assert.Equal(expected, builder.QueryTemplate.RawSql);
    }
}

public class MockPlatformQuery() : PlatformQuery("SELECT * FROM Mock /**where**/") { }