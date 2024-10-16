﻿using Moq;
using Platform.Api.Insight.MetricRagRatings;
using Platform.Sql;
using Xunit;
namespace Platform.Tests.Insight.MetricRagRatings;

public class WhenMetricRagRatingsServiceQueriesAsync
{
    private readonly Mock<IDatabaseConnection> _connection;
    private readonly MetricRagRatingsService _service;

    public WhenMetricRagRatingsServiceQueriesAsync()
    {
        _connection = new Mock<IDatabaseConnection>();

        var dbFactory = new Mock<IDatabaseFactory>();
        dbFactory.Setup(d => d.GetConnection()).ReturnsAsync(_connection.Object);

        _service = new MetricRagRatingsService(dbFactory.Object);
    }

    [Fact]
    public async Task ShouldQueryFirstAsyncWhenQueryAsync()
    {
        // arrange
        _connection
            .Setup(c => c.QueryFirstAsync<string>("SELECT Value from Parameters where Name = 'CurrentYear'", null))
            .Verifiable();

        // act
        var actual = await _service.QueryAsync([], [], []);

        // assert
        _connection.Verify();
    }

    [Theory]
    [InlineData("1,2,3", "4,5,6", "7,8,9", "runType", false, "SELECT * from MetricRAG WHERE RunType = @RunType AND RunId = @RunId AND URN IN @URNS AND SubCategory = 'Total' AND Category IN @categories AND RAG IN @statuses")]
    [InlineData("1,2,3", null, null, "runType", true, "SELECT * from MetricRAG WHERE RunType = @RunType AND RunId = @RunId AND URN IN @URNS")]
    public async Task ShouldQueryAsyncWhenQueryAsync(
        string urns,
        string? categories,
        string? statuses,
        string runType,
        bool includeSubCategories,
        string expectedSql)
    {
        // arrange
        var results = new List<MetricRagRating>
        {
            new()
        };
        string? actualSql = null;
        var actualParam = new Dictionary<string, object>();

        const string year = "year";
        _connection
            .Setup(c => c.QueryFirstAsync<string>("SELECT Value from Parameters where Name = 'CurrentYear'", null))
            .ReturnsAsync(year);

        _connection
            .Setup(c => c.QueryAsync<MetricRagRating>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "RunType", "RunId", "URNS", "categories", "statuses");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryAsync(
            urns.Split(","),
            categories?.Split(",") ?? [],
            statuses?.Split(",") ?? [],
            runType,
            includeSubCategories);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql?.Trim());

        var expectedParam = new Dictionary<string, object>
        {
            {
                "RunType", runType
            },
            {
                "RunId", year
            },
            {
                "URNS", urns.Split(",")
            }
        };
        if (!string.IsNullOrWhiteSpace(categories))
        {
            expectedParam.Add("categories", categories.Split(","));
        }
        if (!string.IsNullOrWhiteSpace(statuses))
        {
            expectedParam.Add("statuses", statuses.Split(","));
        }

        Assert.Equal(expectedParam, actualParam);
    }

    [Theory]
    [InlineData("identifier", "runType", false, "SELECT * from MetricRAG WHERE RunType = @RunType AND RunId = @RunId AND SubCategory = 'Total'")]
    [InlineData("identifier", "runType", true, "SELECT * from MetricRAG WHERE RunType = @RunType AND RunId = @RunId")]
    public async Task ShouldQueryAsyncWhenUserDefinedAsync(string identifier, string runType, bool includeSubCategories, string expectedSql)
    {
        // arrange
        var results = new List<MetricRagRating>
        {
            new()
        };
        string? actualSql = null;
        var actualParam = new Dictionary<string, object>();

        _connection
            .Setup(c => c.QueryAsync<MetricRagRating>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "RunType", "RunId");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.UserDefinedAsync(identifier, runType, includeSubCategories);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql?.Trim());

        var expectedParam = new Dictionary<string, object>
        {
            {
                "RunType", runType
            },
            {
                "RunId", identifier
            }
        };

        Assert.Equal(expectedParam, actualParam);
    }
}