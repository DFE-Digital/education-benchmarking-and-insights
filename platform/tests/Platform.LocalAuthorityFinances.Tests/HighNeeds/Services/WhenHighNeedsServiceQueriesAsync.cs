using AutoFixture;
using Moq;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.LocalAuthorityFinances.Tests.HighNeeds.Services;

public class WhenHighNeedsServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly HighNeedsService _service;

    public WhenHighNeedsServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new HighNeedsService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAndMultiMapWhenGetWithValidDimension()
    {
        // arrange
        string[] codes = ["code1", "code2", "code3"];
        const string dimension = "Actuals";
        var results = _fixture
            .Build<LocalAuthority<Api.LocalAuthorityFinances.Features.HighNeeds.Models.HighNeeds>>()
            .CreateMany()
            .ToArray();

        string? actualSql = null;
        Type[] actualTypes = [];
        string[] actualSplitOn = [];
        _connection
            .Setup(c => c.QueryAsync(It.IsAny<PlatformQuery>(), It.IsAny<Type[]>(), It.IsAny<Func<object[], LocalAuthority<Api.LocalAuthorityFinances.Features.HighNeeds.Models.HighNeeds>>>(), It.IsAny<string[]>()))
            .Callback<PlatformQuery, Type[], Func<object[], LocalAuthority<Api.LocalAuthorityFinances.Features.HighNeeds.Models.HighNeeds>>, string[]>((query, types, _, splitOn) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
                actualTypes = types;
                actualSplitOn = splitOn;
            })
            .ReturnsAsync(results);

        const string expectedSql = "SELECT LaCode AS [Code] , [Name] , OutturnTotalHighNeeds AS [Total] , OutturnTotalPlaceFunding AS [TotalPlaceFunding] , OutturnTotalTopUpFundingMaintained AS [TopUpFundingMaintained] , OutturnTotalTopUpFundingNonMaintained AS [TopUpFundingNonMaintained] , OutturnTotalSenServices AS [SenServices] , OutturnTotalAlternativeProvisionServices AS [AlternativeProvisionServices] , OutturnTotalHospitalServices AS [HospitalServices] , OutturnTotalOtherHealthServices AS [OtherHealthServices] , OutturnTopFundingMaintainedEarlyYears AS [EarlyYears] , OutturnTopFundingMaintainedPrimary AS [Primary] , OutturnTopFundingMaintainedSecondary AS [Secondary] , OutturnTopFundingMaintainedSpecial AS [Special] , OutturnTopFundingMaintainedAlternativeProvision AS [AlternativeProvision] , OutturnTopFundingMaintainedPostSchool AS [PostSchool] , OutturnTopFundingMaintainedIncome AS [Income] , OutturnTopFundingNonMaintainedEarlyYears AS [EarlyYears] , OutturnTopFundingNonMaintainedPrimary AS [Primary] , OutturnTopFundingNonMaintainedSecondary AS [Secondary] , OutturnTopFundingNonMaintainedSpecial AS [Special] , OutturnTopFundingNonMaintainedAlternativeProvision AS [AlternativeProvision] , OutturnTopFundingNonMaintainedPostSchool AS [PostSchool] , OutturnTopFundingNonMaintainedIncome AS [Income] , OutturnPlaceFundingPrimary AS [Primary] , OutturnPlaceFundingSecondary AS [Secondary] , OutturnPlaceFundingSpecial AS [Special] , OutturnPlaceFundingAlternativeProvision AS [AlternativeProvision] , BudgetTotalHighNeeds AS [Total] , BudgetTotalPlaceFunding AS [TotalPlaceFunding] , BudgetTotalTopUpFundingMaintained AS [TopUpFundingMaintained] , BudgetTotalTopUpFundingNonMaintained AS [TopUpFundingNonMaintained] , BudgetTotalSenServices AS [SenServices] , BudgetTotalAlternativeProvisionServices AS [AlternativeProvisionServices] , BudgetTotalHospitalServices AS [HospitalServices] , BudgetTotalOtherHealthServices AS [OtherHealthServices] , BudgetTopFundingMaintainedEarlyYears AS [EarlyYears] , BudgetTopFundingMaintainedPrimary AS [Primary] , BudgetTopFundingMaintainedSecondary AS [Secondary] , BudgetTopFundingMaintainedSpecial AS [Special] , BudgetTopFundingMaintainedAlternativeProvision AS [AlternativeProvision] , BudgetTopFundingMaintainedPostSchool AS [PostSchool] , BudgetTopFundingMaintainedIncome AS [Income] , BudgetTopFundingNonMaintainedEarlyYears AS [EarlyYears] , BudgetTopFundingNonMaintainedPrimary AS [Primary] , BudgetTopFundingNonMaintainedSecondary AS [Secondary] , BudgetTopFundingNonMaintainedSpecial AS [Special] , BudgetTopFundingNonMaintainedAlternativeProvision AS [AlternativeProvision] , BudgetTopFundingNonMaintainedPostSchool AS [PostSchool] , BudgetTopFundingNonMaintainedIncome AS [Income] , BudgetPlaceFundingPrimary AS [Primary] , BudgetPlaceFundingSecondary AS [Secondary] , BudgetPlaceFundingSpecial AS [Special] , BudgetPlaceFundingAlternativeProvision AS [AlternativeProvision]\n FROM VW_LocalAuthorityFinancialDefaultCurrentActual WHERE LaCode IN @LaCodes";
        Type[] expectedTypes = [typeof(LocalAuthorityBase), typeof(HighNeedsBase), typeof(HighNeedsAmount), typeof(TopFunding), typeof(TopFunding), typeof(PlaceFunding), typeof(HighNeedsBase), typeof(HighNeedsAmount), typeof(TopFunding), typeof(TopFunding), typeof(PlaceFunding)];
        string[] expectedSplitOn = ["Total", "TotalPlaceFunding", "EarlyYears", "EarlyYears", "Primary", "Total", "TotalPlaceFunding", "EarlyYears", "EarlyYears", "Primary"];

        // act
        var actual = await _service.Get(codes, dimension);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql);
        Assert.Equal(expectedTypes, actualTypes);
        Assert.Equal(expectedSplitOn, actualSplitOn);
    }
}