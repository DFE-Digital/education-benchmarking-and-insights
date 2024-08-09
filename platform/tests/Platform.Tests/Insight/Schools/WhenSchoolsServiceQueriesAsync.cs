using Moq;
using Platform.Api.Insight.Schools;
using Platform.Infrastructure.Sql;
using Xunit;
namespace Platform.Tests.Insight.Schools;

public class WhenSchoolsServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly SchoolsService _service;

    public WhenSchoolsServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new SchoolsService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQueryCharacteristic()
    {
        // arrange
        string[] urns = ["urn1", "urn2"];
        var results = new List<SchoolCharacteristic>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolCharacteristic>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryCharacteristicAsync(urns);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM SchoolCharacteristic WHERE URN IN @URNS", actualSql);
        Assert.Equivalent(new
        {
            URNS = urns
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncCharacteristic()
    {
        // arrange
        const string urn = "urn";
        var result = new SchoolCharacteristic();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolCharacteristic>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.CharacteristicAsync(urn);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM SchoolCharacteristic WHERE URN = @URN", actualSql);
        Assert.Equivalent(new
        {
            URN = urn
        }, actualParam, true);
    }
}