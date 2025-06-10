using Moq;
using Platform.Api.Content.Features.Years.Models;
using Platform.Api.Content.Features.Years.Services;
using Platform.Sql;
using Platform.Sql.QueryBuilders;
using Xunit;

namespace Platform.Content.Tests.Years.Services;

public class WhenReturnYearsServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly YearsService _service;

    public WhenReturnYearsServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new YearsService(dbFactory.Object);
    }

    [Fact]
    public async Task QueryFirstOrDefaultAsync()
    {
        const string aar = nameof(aar);
        const string cfr = nameof(cfr);
        const string s251 = nameof(s251);

        _connection
            .SetupSequence(c => c.QueryFirstOrDefaultAsync<string>(It.IsAny<PlatformQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(aar)
            .ReturnsAsync(cfr)
            .ReturnsAsync(s251);

        var actual = await _service.GetCurrentReturnYears(CancellationToken.None);

        var expected = new FinanceYears
        {
            Aar = aar,
            Cfr = cfr,
            S251 = s251
        };
        Assert.Equivalent(expected, actual);
    }
}