using AutoFixture;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Domain;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities.Services;

public class WhenLocalAuthorityRankingServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly Fixture _fixture = new();
    private readonly LocalAuthorityRankingService _service;

    public WhenLocalAuthorityRankingServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new LocalAuthorityRankingService(dbFactory.Object);
    }

    [Theory]
    [InlineData(Ranking.LocalAuthorityNationalRanking.SpendAsPercentageOfBudget, "VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfBudget")]
    [InlineData(Ranking.LocalAuthorityNationalRanking.SpendAsPercentageOfFunding, "VW_LocalAuthorityFinancialDefaultCurrentSpendAsPercentageOfFunding")]
    public async Task ShouldQueryAsyncWhenGetRanking(string ranking, string expectedView)
    {
        // arrange
        const string sort = Ranking.Sort.Asc;
        var results = _fixture.Build<LocalAuthorityRank>().CreateMany().ToArray();
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryAsync<LocalAuthorityRank>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .Callback<PlatformQuery, CancellationToken>((query, _) =>
            {
                actualSql = query.QueryTemplate.RawSql.Trim();
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetRanking(ranking, sort, CancellationToken.None);

        // assert
        Assert.Equal(results, actual.Ranking);
        Assert.Equal($"SELECT LaCode AS Code , Name , Value , RANK() OVER (ORDER BY Value) AS [Rank]\n FROM {expectedView} WHERE Value IS NOT NULL", actualSql);
    }
}