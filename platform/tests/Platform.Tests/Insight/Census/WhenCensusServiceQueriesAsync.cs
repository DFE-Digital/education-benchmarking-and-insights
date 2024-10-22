using Moq;
using Platform.Api.Insight.Census;
using Platform.Sql;
using Xunit;
using SqlBuilder = Dapper.SqlBuilder;

namespace Platform.Tests.Insight.Census;

public class WhenCensusServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly CensusService _service;

    public WhenCensusServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new CensusService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetHistory()
    {
        // arrange
        const string urn = "urn";
        var results = new List<CensusHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<CensusHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetHistoryAsync(urn);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolCensusHistoric WHERE URN = @URN", actualSql);
        Assert.Equivalent(new
        {
            URN = urn
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQueryAsync()
    {
        // arrange
        string[] urns = ["urn1", "urn2"];
        var results = new List<CensusModel>
        {
            new()
        };
        var actualParam = new Dictionary<string, object>();

        _connection
            .Setup(c => c.QueryAsync<CensusModel>(It.IsAny<SqlBuilder.Template>()))
            .Callback((SqlBuilder.Template template) =>
            {
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(template.Parameters, "URNS");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryAsync(urns);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(["URNS"], actualParam.Keys.ToArray());
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetAsync()
    {
        // arrange
        const string urn = "urn";
        var result = new CensusModel();
        var actualParam = new Dictionary<string, object>();

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<CensusModel>(It.IsAny<SqlBuilder.Template>()))
            .Callback((SqlBuilder.Template template) =>
            {
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(template.Parameters, "URN");
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetAsync(urn);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal(["URN"], actualParam.Keys.ToArray());
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetCustomAsync()
    {
        // arrange
        const string urn = "urn";
        const string identifier = "identifier";
        var result = new CensusModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<CensusModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetCustomAsync(urn, identifier);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * from SchoolCensusCustom WHERE URN = @URN AND RunId = @RunId", actualSql);
        Assert.Equivalent(new
        {
            URN = urn,
            RunId = identifier
        }, actualParam, true);
    }
}