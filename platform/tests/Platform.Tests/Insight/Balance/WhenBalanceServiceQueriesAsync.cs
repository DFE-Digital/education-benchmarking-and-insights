using Moq;
using Platform.Api.Insight.Balance;
using Platform.Infrastructure.Sql;
using Xunit;
namespace Platform.Tests.Insight.Balance;

public class WhenBalanceServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly BalanceService _service;

    public WhenBalanceServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new BalanceService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetSchoolHistory()
    {
        // arrange
        const string urn = "urn";
        var results = new List<SchoolBalanceHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolBalanceHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetSchoolHistoryAsync(urn);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM SchoolBalanceHistoric WHERE URN = @URN", actualSql);
        Assert.Equivalent(new
        {
            URN = urn
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQuerySchoolsAsync()
    {
        // arrange
        string[] urns = ["urn1", "urn2"];
        var results = new List<SchoolBalanceModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolBalanceModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync(urns);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolBalance where URN IN @URNS", actualSql);
        Assert.Equivalent(new
        {
            URNS = urns
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetSchoolAsync()
    {
        // arrange
        const string urn = "urn";
        var result = new SchoolBalanceModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolBalanceModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetSchoolAsync(urn);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM SchoolBalance WHERE URN = @URN", actualSql);
        Assert.Equivalent(new
        {
            URN = urn
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetTrustHistory()
    {
        // arrange
        const string companyNumber = "companyNumber";
        var results = new List<TrustBalanceHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustBalanceHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetTrustHistoryAsync(companyNumber);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * FROM TrustBalanceHistoric WHERE CompanyNumber = @CompanyNumber", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQueryTrustsAsync()
    {
        // arrange
        string[] companyNumbers = ["companyNumber1", "companyNumber2"];
        var results = new List<TrustBalanceModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustBalanceModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryTrustsAsync(companyNumbers);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from TrustBalance where CompanyNumber IN @CompanyNumbers", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumbers = companyNumbers
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetTrustAsync()
    {
        // arrange
        const string companyNumber = "companyNumber";
        var result = new TrustBalanceModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<TrustBalanceModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetTrustAsync(companyNumber);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM TrustBalance WHERE CompanyNumber = @CompanyNumber", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber
        }, actualParam, true);
    }
}