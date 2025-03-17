using AutoFixture;
using Moq;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;
using Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Services;
using Platform.Api.NonFinancial.Shared;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.NonFinancial.Tests.EducationHealthCarePlans.Services;

public class WhenEducationHealthCarePlansServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly EducationHealthCarePlansService _service;

    public WhenEducationHealthCarePlansServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new EducationHealthCarePlansService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetAll()
    {
        // arrange
        const string dimension = Dimensions.EducationHealthCarePlans.Per1000;
        string[] codes = ["code1", "code2", "code3"];
        var results = _fixture.Build<LocalAuthorityNumberOfPlans>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<LocalAuthorityNumberOfPlans>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.Get(codes, dimension, CancellationToken.None);

        // assert
        Assert.Equal(results.Select(Mapper.MapToLocalAuthorityNumberOfPlansResponse), actual);
        Assert.Equal("SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultCurrentPerPopulation WHERE LaCode IN @LaCodes", actualSql);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetHistory()
    {
        // arrange
        const string dimension = Dimensions.EducationHealthCarePlans.Per1000;
        string[] codes = ["code1", "code2", "code3"];
        var results = _fixture.Build<LocalAuthorityNumberOfPlansYear>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<LocalAuthorityNumberOfPlansYear>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        var years = _fixture.Create<YearsModel>();
        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<YearsModel>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(years);

        // act
        var actual = await _service.GetHistory(codes, dimension, CancellationToken.None);

        // assert
        Assert.Equal(years.StartYear, actual?.StartYear);
        Assert.Equal(years.EndYear, actual?.EndYear);
        Assert.Equal(results.Select(Mapper.MapToLocalAuthorityNumberOfPlansYearResponse), actual?.Plans);
        Assert.Equal("SELECT * FROM VW_LocalAuthorityEducationHealthCarePlansDefaultPerPopulation WHERE LaCode IN @LaCodes AND RunId BETWEEN @StartYear AND @EndYear", actualSql);
    }
}