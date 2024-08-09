using Moq;
using Platform.Api.Insight.Expenditure;
using Platform.Infrastructure.Sql;
using Xunit;
namespace Platform.Tests.Insight.Expenditure;

public class WhenExpenditureServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly ExpenditureService _service;

    public WhenExpenditureServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new ExpenditureService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetSchoolHistory()
    {
        // arrange
        const string urn = "urn";
        var results = new List<SchoolExpenditureHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolExpenditureHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM SchoolExpenditureHistoric WHERE URN = @URN", actualSql);
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
        var results = new List<SchoolExpenditureModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolExpenditureModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * from SchoolExpenditure where URN IN @URNS", actualSql);
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
        var result = new SchoolExpenditureModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolExpenditureModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM SchoolExpenditure WHERE URN = @URN", actualSql);
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
        var results = new List<TrustExpenditureHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustExpenditureHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM TrustExpenditureHistoric WHERE CompanyNumber = @CompanyNumber", actualSql);
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
        var results = new List<TrustExpenditureModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustExpenditureModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * from TrustExpenditure where CompanyNumber IN @CompanyNumbers", actualSql);
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
        var result = new TrustExpenditureModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<TrustExpenditureModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM TrustExpenditure WHERE CompanyNumber = @CompanyNumber", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetCustomAsync()
    {
        // arrange
        const string urn = "urn";
        const string identifier = "identifier";
        var result = new SchoolExpenditureModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolExpenditureModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(result);

        // act
        var actual = await _service.GetCustomSchoolAsync(urn, identifier);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * FROM SchoolExpenditureCustom WHERE URN = @URN AND RunId = @RunId", actualSql);
        Assert.Equivalent(new
        {
            URN = urn,
            RunId = identifier
        }, actualParam, true);
    }
}