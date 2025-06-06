﻿using Moq;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Sql;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast.Services;

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
    public async Task ShouldQueryAsyncWhenGetBudgetForecastReturns(string companyNumber, string runType, string category, string runId, string expectedSql)
    {
        // arrange
        var results = new List<BudgetForecastReturnModel>
        {
            new()
        };
        string? actualSql = null;
        var actualParam = new Dictionary<string, object>();

        _connection
            .Setup(c => c.QueryAsync<BudgetForecastReturnModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = param?.GetTemplateParameters("CompanyNumber", "RunType", "Category", "RunId");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetBudgetForecastReturnsAsync(companyNumber, runType, category, runId);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql?.Trim());

        var expectedParam = new Dictionary<string, object>
        {
            { "CompanyNumber", companyNumber },
            { "RunType", runType },
            { "Category", category }
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
            .Setup(c => c.QueryFirstOrDefaultAsync<int>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(2021);

        _connection
            .Setup(c => c.QueryAsync<BudgetForecastReturnMetricModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.GetBudgetForecastReturnMetricsAsync(companyNumber, runType);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from BudgetForecastReturnMetric where CompanyNumber = @CompanyNumber and RunType = @RunType AND Year >= @StartYear AND Year <= @EndYear", actualSql);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber,
            RunType = runType,
            StartYear = 2019,
            EndYear = 2021

        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetBudgetForecastCurrentYear()
    {
        // arrange
        string? actualSql = null;

        _connection
            .Setup(c => c.QueryFirstOrDefaultAsync<int?>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? _, CancellationToken _) =>
            {
                actualSql = sql;
            })
            .ReturnsAsync(2023);

        // act
        await _service.GetBudgetForecastCurrentYearAsync();

        // assert

        Assert.Equal("SELECT Value FROM Parameters WHERE Name = @Name", actualSql);
    }

    [Theory]
    [InlineData("123", "revenue reserve", 2021)]
    public async Task ShouldQueryAsyncWhenGetActualReturnsAsyncWithValidCategory(string companyNumber, string category, int runId)
    {
        // arrange
        var models = new List<ActualReturnModel>
        {
            new()
        };

        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<ActualReturnModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(models);

        // act
        var results = await _service.GetActualReturnsAsync(companyNumber, category, runId.ToString());

        // assert
        Assert.Equal("SELECT Year, RevenueReserve 'Value', TotalPupils FROM TrustBalanceHistoric WHERE CompanyNumber = @CompanyNumber AND Year >= @StartYear AND Year <= @EndYear", actualSql);
        Assert.Equal(models, results);
        Assert.Equivalent(new
        {
            CompanyNumber = companyNumber,
            StartYear = runId - 2,
            EndYear = runId
        }, actualParam, true);
    }

    [Theory]
    [InlineData("123", "invalid", 2021)]
    public async Task ShouldNotQueryAsyncWhenGetActualReturnsAsyncWithInvalidCategory(string companyNumber, string category, int runId)
    {
        var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.GetActualReturnsAsync(companyNumber, category, runId.ToString()));

        Assert.NotNull(exception);
        _connection
            .Verify(c => c.QueryAsync<ActualReturnModel>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}