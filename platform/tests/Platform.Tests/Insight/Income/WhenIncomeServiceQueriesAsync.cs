using Moq;
using Platform.Api.Insight.Income;
using Platform.Infrastructure.Sql;
using Xunit;
namespace Platform.Tests.Insight.Income;

public class WhenIncomeServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly IncomeService _service;

    public WhenIncomeServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new IncomeService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetSchoolHistory()
    {
        // arrange
        const string urn = "urn";
        var results = new List<SchoolIncomeHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolIncomeHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM SchoolIncomeHistoric WHERE URN = @URN", actualSql);
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
        var results = new List<SchoolIncomeModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolIncomeModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * from SchoolIncome where URN IN @URNS", actualSql);
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
        var result = new SchoolIncomeModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolIncomeModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM SchoolIncome WHERE URN = @URN", actualSql);
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
        var results = new List<TrustIncomeHistoryModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustIncomeHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM TrustIncomeHistoric WHERE CompanyNumber = @CompanyNumber", actualSql);
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
        var results = new List<TrustIncomeModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<TrustIncomeModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * from TrustIncome where CompanyNumber IN @CompanyNumbers", actualSql);
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
        var result = new TrustIncomeModel();
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<TrustIncomeModel>(It.IsAny<string>(), It.IsAny<object>()))
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
        Assert.Equal("SELECT * FROM TrustIncome WHERE CompanyNumber = @CompanyNumber", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber
        }, actualParam, true);
    }
}