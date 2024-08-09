using Moq;
using Platform.Api.Insight.Trusts;
using Platform.Infrastructure.Sql;
using Xunit;
namespace Platform.Tests.Insight.Trusts;

public class WhenTrustsServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly TrustsService _service;

    public WhenTrustsServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new TrustsService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQueryCharacteristic()
    {
        // arrange
        string[] companyNumbers = ["urn1", "urn2"];
        var results = new List<TrustCharacteristic>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustCharacteristic>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryCharacteristicAsync(companyNumbers);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM TrustCharacteristic WHERE CompanyNumber IN @CompanyNumbers", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumbers = companyNumbers
        }, actualParam, true);
    }
}