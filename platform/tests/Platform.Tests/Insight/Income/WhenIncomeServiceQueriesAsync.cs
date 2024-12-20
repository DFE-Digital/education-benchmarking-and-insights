using Moq;
using Platform.Api.Insight.Income;
using Platform.Sql;
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
            .Setup(c => c.QueryAsync<SchoolIncomeHistoryModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
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
    public async Task ShouldQueryAsyncWhenQuerySchoolsAsyncWithUrns()
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
            .Setup(c => c.QueryAsync<SchoolIncomeModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "URNS");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync(urns, null, null, null);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolIncome WHERE URN IN @URNS", actualSql?.Trim());
        var expectedParam = new Dictionary<string, object>
        {
            {
                "URNS", urns
            }
        };
        Assert.Equivalent(expectedParam, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQuerySchoolsAsyncWithCompanyNumberAndPhase()
    {
        // arrange
        const string companyNumber = nameof(companyNumber);
        const string phase = nameof(phase);
        var results = new List<SchoolIncomeModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolIncomeModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "CompanyNumber", "Phase");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync([], companyNumber, null, phase);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolIncome WHERE TrustCompanyNumber = @CompanyNumber AND OverallPhase = @Phase", actualSql?.Trim());
        var expectedParam = new Dictionary<string, object>
        {
            {
                "CompanyNumber", companyNumber
            },
            {
                "Phase", phase
            }
        };
        Assert.Equivalent(expectedParam, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenQuerySchoolsAsyncWithLaCodeAndPhase()
    {
        // arrange
        const string laCode = nameof(laCode);
        const string phase = nameof(phase);
        var results = new List<SchoolIncomeModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolIncomeModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "LaCode", "Phase");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync([], null, laCode, phase);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolIncome WHERE LaCode = @LaCode AND OverallPhase = @Phase", actualSql?.Trim());
        var expectedParam = new Dictionary<string, object>
        {
            {
                "LaCode", laCode
            },
            {
                "Phase", phase
            }
        };
        Assert.Equivalent(expectedParam, actualParam, true);
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
            .Setup(c => c.QueryFirstOrDefaultAsync<SchoolIncomeModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
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
            .Setup(c => c.QueryAsync<TrustIncomeHistoryModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
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
            .Setup(c => c.QueryAsync<TrustIncomeModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
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
            .Setup(c => c.QueryFirstOrDefaultAsync<TrustIncomeModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
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