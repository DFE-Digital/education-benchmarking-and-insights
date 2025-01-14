using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Sql.Tests.Builders;

public class PlatformQueryTests
{
    [Fact]
    public void ShouldAddUrnEqualParameter()
    {
        const string expectedParam = "URN";
        const string expectedValue = "12345";
        var expectedSql = BuildExpectedQuery("WHERE URN = @URN");
        
        var builder = new MockPlatformQuery().WhereUrnEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddUrnInParameter()
    {
        const string expectedParam = "URNS";
        var expectedValue = new[] { "12345", "12346" };
        var expectedSql = BuildExpectedQuery("WHERE URN IN @URNS");
        var builder = new MockPlatformQuery().WhereUrnIn(expectedValue);

        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAdRunIdBetweenParameter()
    {
        const string expectedParam1 = "StartYear";
        const int expectedValue1 = 2020;
        const string expectedParam2 = "EndYear";
        const int expectedValue2 = 2025;
        var expectedSql = BuildExpectedQuery("WHERE RunId BETWEEN @StartYear AND @EndYear");

        var builder = new MockPlatformQuery().WhereRunIdBetween(expectedValue1, expectedValue2);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam1, expectedParam2);

        Assert.Equal(2, parameters.Count);
        Assert.Contains(expectedParam1, parameters.Keys);
        Assert.Contains(expectedParam2, parameters.Keys);
        Assert.Equal(expectedValue1, parameters[expectedParam1]);
        Assert.Equal(expectedValue2, parameters[expectedParam2]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddRunIdEqualParameter()
    {
        const string expectedParam = "RunId";
        const string expectedValue = "12345";
        var expectedSql = BuildExpectedQuery("WHERE RunId = @RunId");

        var builder = new MockPlatformQuery().WhereRunIdEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddTrustCompanyNumberEqualParameter()
    {
        const string expectedParam = "CompanyNumber";
        const string expectedValue = "12345";
        var expectedSql = BuildExpectedQuery("WHERE TrustCompanyNumber = @CompanyNumber");

        var builder = new MockPlatformQuery().WhereTrustCompanyNumberEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddCompanyNumberEqualParameter()
    {
        const string expectedParam = "CompanyNumber";
        const string expectedValue = "12345";
        var expectedSql = BuildExpectedQuery("WHERE CompanyNumber = @CompanyNumber");

        var builder = new MockPlatformQuery().WhereCompanyNumberEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddCompanyNumberInParameter()
    {
        const string expectedParam = "CompanyNumbers";
        var expectedValue = new[] { "12345", "12346" };
        var expectedSql = BuildExpectedQuery("WHERE CompanyNumber IN @CompanyNumbers");

        var builder = new MockPlatformQuery().WhereCompanyNumberIn(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddLaCodeEqualParameter()
    {
        const string expectedParam = "LaCode";
        const string expectedValue = "123";
        var expectedSql = BuildExpectedQuery("WHERE LaCode = @LaCode");

        var builder = new MockPlatformQuery().WhereLaCodeEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddOverallPhaseEqualParameter()
    {
        const string expectedParam = "Phase";
        const string expectedValue = "Primary";
        var expectedSql = BuildExpectedQuery("WHERE OverallPhase = @Phase");

        var builder = new MockPlatformQuery().WhereOverallPhaseEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddFinanceTypeEqualParameter()
    {
        const string expectedParam = "FinanceType";
        const string expectedValue = "Maintained";
        var expectedSql = BuildExpectedQuery("WHERE FinanceType = @FinanceType");

        var builder = new MockPlatformQuery().WhereFinanceTypeEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters.GetTemplateParameters(expectedParam);

        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    private static string BuildExpectedQuery(string wherePart) =>
        $"{MockPlatformQuery.Sql.Replace("/**where**/", wherePart)}\n";
}

public class MockPlatformQuery() : PlatformQuery(Sql)
{
    public const string Sql = "SELECT * FROM Mock /**where**/";
}