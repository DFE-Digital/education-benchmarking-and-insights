using Moq;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Sql;
using Xunit;

namespace Platform.Insight.Tests.MetricRagRatings.Services;

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
    public async Task ShouldQueryCurrentYearWhenQueryAsync()
    {
        // arrange
        _connection
            .Setup(c => c.QueryFirstAsync<string>("SELECT Value from Parameters where Name = 'CurrentYear'", null, It.IsAny<CancellationToken>()))
            .Verifiable();

        // act
        await _service.QueryAsync(["urn"], [], [], null, null, null);

        // assert
        _connection.Verify();
    }

    [Theory]
    [InlineData(new[] { "1,2,3" }, new[] { "4", "5", "6" }, new[] { "7", "8", "9" }, null, null, null, "runType", false,
        "SELECT * from SchoolMetricRAG WHERE RunType = @RunType AND RunId = @RunId AND URN IN @URNS AND SubCategory = 'Total' AND Category IN @categories AND RAG IN @statuses")]
    [InlineData(new[] { "1,2,3" }, new string[0], new string[0], null, null, null, "runType", true, "SELECT * from SchoolMetricRAG WHERE RunType = @RunType AND RunId = @RunId AND URN IN @URNS")]
    [InlineData(new string[0], new string[0], new string[0], "companyNumber", null, null, "runType", true, "SELECT * from SchoolMetricRAG WHERE RunType = @RunType AND RunId = @RunId AND TrustCompanyNumber = @CompanyNumber")]
    [InlineData(new string[0], new string[0], new string[0], null, "laCode", "phase", "runType", true, "SELECT * from SchoolMetricRAG WHERE RunType = @RunType AND RunId = @RunId AND LaCode = @LaCode AND OverallPhase = @Phase")]
    public async Task ShouldQueryAsyncWhenQueryAsync(
        string[] urns,
        string[] categories,
        string[] statuses,
        string? companyNumber,
        string? laCode,
        string? phase,
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
            .Setup(c => c.QueryFirstAsync<string>("SELECT Value from Parameters where Name = 'CurrentYear'", null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(year);

        _connection
            .Setup(c => c.QueryAsync<MetricRagRating>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = param?.GetTemplateParameters("RunType", "RunId", "URNS", "CompanyNumber", "LaCode", "Phase", "categories", "statuses");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryAsync(
            urns,
            categories,
            statuses,
            companyNumber,
            laCode,
            phase,
            runType,
            includeSubCategories);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql?.Trim());

        var expectedParam = new Dictionary<string, object>
        {
            { "RunType", runType },
            { "RunId", year }
        };

        if (urns.Length != 0)
        {
            expectedParam.Add("URNS", urns);
        }

        if (categories.Length != 0)
        {
            expectedParam.Add("categories", categories);
        }

        if (statuses.Length != 0)
        {
            expectedParam.Add("statuses", statuses);
        }

        if (!string.IsNullOrEmpty(companyNumber))
        {
            expectedParam.Add("CompanyNumber", companyNumber);
        }

        if (!string.IsNullOrEmpty(laCode))
        {
            expectedParam.Add("LaCode", laCode);
        }

        if (!string.IsNullOrEmpty(phase))
        {
            expectedParam.Add("Phase", phase);
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
            .Setup(c => c.QueryAsync<MetricRagRating>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Callback((string sql, object? param, CancellationToken _) =>
            {
                actualSql = sql;
                actualParam = param?.GetTemplateParameters("RunType", "RunId");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.UserDefinedAsync(identifier, runType, includeSubCategories);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal(expectedSql, actualSql?.Trim());

        var expectedParam = new Dictionary<string, object>
        {
            { "RunType", runType },
            { "RunId", identifier }
        };

        Assert.Equal(expectedParam, actualParam);
    }

    [Fact]
    public async Task ShouldNotQueryAndThrowExceptionWhenNoFilterSupplied()
    {
        var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _service.QueryAsync([], [], [], null, null, null));

        Assert.NotNull(exception);
        _connection
            .Verify(c => c.QueryAsync<MetricRagRating>(It.IsAny<string>(), It.IsAny<object>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}