using Moq;
using Platform.Api.Insight.BudgetForecast;
using Platform.Sql;
using Xunit;
namespace Platform.Tests.Insight.BudgetForecast;

public class WhenBudgetForecastServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly BudgetForecastService _service;

    public WhenBudgetForecastServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new BudgetForecastService(dbFactory.Object);
    }

    [Theory]
    [InlineData("companyNumber", "runType", "category", "runId", "SELECT * from BudgetForecastReturn WHERE CompanyNumber = @CompanyNumber AND RunType = @RunType AND Category = @Category AND RunId = @RunId")]
    public async Task ShouldQueryAsyncWhenGetBudgetForecastReturns(string companyNumber, string runType, string category, string? runId, string expectedSql)
    {
        // arrange
        var results = new List<BudgetForecastReturnModel>
        {
            new()
        };
        string? actualSql = null;
        var actualParam = new Dictionary<string, object>();

        _connection
            .Setup(c => c.QueryAsync<BudgetForecastReturnModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "CompanyNumber", "RunType", "Category", "RunId");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetBudgetForecastReturnsAsync(companyNumber, runType, category, runId);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql?.Trim());

        var expectedParam = new Dictionary<string, object>
        {
            {
                "CompanyNumber", companyNumber
            },
            {
                "RunType", runType
            },
            {
                "Category", category
            }
        };
        
        expectedParam.Add("RunId", runId);

        Assert.Equal(expectedParam, actualParam);
    }

    [Fact]
    public async Task ShouldQueryAsyncWhenGetBudgetForecastReturnMetrics()
    {
        // arrange
        const string companyNumber = "companyNumber";
        const string runType = "runType";
        var results = new List<BudgetForecastReturnMetricModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<BudgetForecastReturnMetricModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetBudgetForecastReturnMetricsAsync(companyNumber, runType);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from BudgetForecastReturnMetric where CompanyNumber = @CompanyNumber and RunType = @RunType", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber,
            RunType = runType
        }, actualParam, true);
    }

    [Theory]
    [InlineData("companyNumber", "default", "category", 123)]
    [InlineData("companyNumber", "NotDefault", "category", null)]
    public async Task ShouldExecuteScalarAsyncWhenGetBudgetForecastCurrentYear(string companyNumber, string runType, string category, int? expectedResult)
    {
        // arrange
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.ExecuteScalarAsync<int?>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(expectedResult);

        // act
        var actual = await _service.GetBudgetForecastCurrentYearAsync(companyNumber, runType, category);

        // assert
        Assert.Equal(expectedResult, actual);
        if (expectedResult == null)
        {
            return;
        }

        Assert.Equal("select convert(int, max(RunId)) from BudgetForecastReturn where CompanyNumber = @CompanyNumber and RunType = @RunType and Category = @Category", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            Category = category
        }, actualParam, true);
    }
}