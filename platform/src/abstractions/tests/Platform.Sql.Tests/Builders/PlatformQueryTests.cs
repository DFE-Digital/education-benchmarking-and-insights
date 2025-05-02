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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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

        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam1, expectedParam2);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddLaCodeInParameter()
    {
        const string expectedParam = "LaCodes";
        var expectedValue = new[] { "12345", "12346" };
        var expectedSql = BuildExpectedQuery("WHERE LaCode IN @LaCodes");

        var builder = new MockPlatformQuery().WhereLaCodesIn(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
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
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddFederationLeadUrnEqualParameter()
    {
        const string expectedParam = "FederationLeadURN";
        const string expectedValue = "urn";
        var expectedSql = BuildExpectedQuery("WHERE FederationLeadURN = @FederationLeadURN");

        var builder = new MockPlatformQuery().WhereFederationLeadUrnEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddCodeEqualParameter()
    {
        const string expectedParam = "Code";
        const string expectedValue = "code";
        var expectedSql = BuildExpectedQuery("WHERE Code = @Code");

        var builder = new MockPlatformQuery().WhereCodeEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddUrnInCurrentFinancesParameter()
    {
        var expectedSql = BuildExpectedQuery("WHERE URN IN (SELECT URN FROM Financial WHERE RunType = 'default' AND RunId = (SELECT Value FROM Parameters WHERE Name = 'CurrentYear'))");

        var builder = new MockPlatformQuery().WhereUrnInCurrentFinances();

        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddTypeInParameter()
    {
        const string expectedParam = "Types";
        var expectedValue = new[] { "transparency-aar", "transparency-cfr" };
        var expectedSql = BuildExpectedQuery("WHERE Type IN @Types");

        var builder = new MockPlatformQuery().WhereTypeIn(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddOrderBy()
    {
        const string expectedValue = "Code";
        var expectedSql = BuildExpectedQuery(string.Empty, "ORDER BY Code");

        var builder = new MockPlatformQuery().OrderBy(expectedValue);

        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddUserIdEqualParameter()
    {
        const string expectedParam = "UserId";
        const string expectedValue = "userId";
        var expectedSql = BuildExpectedQuery("WHERE UserId = @UserId");

        var builder = new MockPlatformQuery().WhereUserIdEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddWhereStatusInParameter()
    {
        const string expectedParam = "Statuses";
        const string expectedValue1 = nameof(expectedValue1);
        const string expectedValue2 = nameof(expectedValue2);
        var expectedSql = BuildExpectedQuery("WHERE Status IN @Statuses");

        var builder = new MockPlatformQuery().WhereStatusIn(expectedValue1, expectedValue2);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(new[] { expectedValue1, expectedValue2 }, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Theory]
    [InlineData(true, 1)]
    [InlineData(false, 0)]
    public void ShouldAddActiveParameter(bool active, int expectedValue)
    {
        const string expectedParam = "Active";
        var expectedSql = BuildExpectedQuery("WHERE Active = @Active");

        var builder = new MockPlatformQuery().WhereActive(active);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddOrganisationIdEqualParameter()
    {
        const string expectedParam = "OrganisationId";
        const string expectedValue = "organisationId";
        var expectedSql = BuildExpectedQuery("WHERE OrganisationId = @OrganisationId");

        var builder = new MockPlatformQuery().WhereOrganisationIdEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddOrganisationTypeEqualParameter()
    {
        const string expectedParam = "OrganisationType";
        const string expectedValue = "organisationType";
        var expectedSql = BuildExpectedQuery("WHERE OrganisationType = @OrganisationType");

        var builder = new MockPlatformQuery().WhereOrganisationTypeEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddTypeEqualParameter()
    {
        const string expectedParam = "Type";
        const string expectedValue = "type";
        var expectedSql = BuildExpectedQuery("WHERE Type = @Type");

        var builder = new MockPlatformQuery().WhereTypeEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddStatusEqualParameter()
    {
        const string expectedParam = "Status";
        const string expectedValue = "status";
        var expectedSql = BuildExpectedQuery("WHERE Status = @Status");

        var builder = new MockPlatformQuery().WhereStatusEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddIdEqualParameter()
    {
        const string expectedParam = "Id";
        const string expectedValue = "id";
        var expectedSql = BuildExpectedQuery("WHERE Id = @Id");

        var builder = new MockPlatformQuery().WhereIdEqual(expectedValue);
        var parameters = builder.QueryTemplate.Parameters?.GetTemplateParameters(expectedParam);

        Assert.NotNull(parameters);
        Assert.Single(parameters);
        Assert.Contains(expectedParam, parameters.Keys);
        Assert.Equal(expectedValue, parameters[expectedParam]);
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    [Fact]
    public void ShouldAddValueIsNotNull()
    {
        var expectedSql = BuildExpectedQuery("WHERE Value IS NOT NULL");
        var builder = new MockPlatformQuery().WhereValueIsNotNull();
        Assert.Equal(expectedSql, builder.QueryTemplate.RawSql);
    }

    private static string BuildExpectedQuery(string wherePart, string? orderByPart = null) =>
        $"{MockPlatformQuery.Sql
            .Replace("/**where**/", wherePart)
            .Replace("/**orderby**/", orderByPart)}\n";
}

public class MockPlatformQuery() : PlatformQuery(Sql)
{
    public const string Sql = "SELECT * FROM Mock /**where**//**orderby**/";
}