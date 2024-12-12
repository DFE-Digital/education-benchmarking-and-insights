using Moq;
using Platform.Api.Insight.Census;
using Platform.Sql;
using Xunit;
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
    public async Task ShouldQueryAsyncWhenQueryAsyncWithUrns()
    {
        // arrange
        string[] urns = ["urn1", "urn2"];
        var results = new List<CensusModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<CensusModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "URNS");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryAsync(urns, null, null, null);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolCensus WHERE URN IN @URNS", actualSql?.Trim());
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
        var results = new List<CensusModel>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<CensusModel>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "CompanyNumber", "Phase");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QueryAsync([], companyNumber, null, phase);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolCensus WHERE TrustCompanyNumber = @CompanyNumber AND OverallPhase = @Phase", actualSql?.Trim());
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
    public async Task ShouldQueryFirstOrDefaultAsyncWhenGetAsync()
    {
        // arrange
        const string urn = "urn";
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
        var actual = await _service.GetAsync(urn);

        // assert
        Assert.Equal(result, actual);
        Assert.Equal("SELECT * from SchoolCensus WHERE URN = @URN", actualSql);
        Assert.Equivalent(new
        {
            URN = urn
        }, actualParam, true);
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

    [InlineData(CensusDimensions.Total, "SchoolCensusAvgComparatorSet")]
    [InlineData(CensusDimensions.HeadcountPerFte, "SchoolCensusAvgPerFteComparatorSet")]
    [InlineData(CensusDimensions.PercentWorkforce, "SchoolCensusAvgPercentageOfWorkforceFteComparatorSet")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "SchoolCensusAvgPupilsPerStaffComparatorSet")]
    [Theory]
    public async Task ShouldQueryAsyncWhenGetHistoryAvgComparatorSetAsync(string dimension, string expectedSource)
    {
        const string urn = "123";
        var expected = new List<CensusHistoryModel>
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
            .ReturnsAsync(expected);

        var expectedSql = $"SELECT * FROM {expectedSource} WHERE URN = @URN";

        var actual = await _service.GetHistoryAvgComparatorSetAsync(urn, dimension);

        Assert.Equal(expected, actual);
        Assert.Equal(expectedSql, actualSql);
        Assert.Equivalent(new
        {
            URN = urn,
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldThrowOnInvalidDimensionWhenGetHistoryAvgComparatorSetAsync()
    {
        const string urn = "123";
        var result = new List<CensusHistoryModel>
        {
            new()
        };

        _connection
            .Setup(c => c.QueryAsync<CensusHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(result);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _service.GetHistoryAvgComparatorSetAsync(urn, "invalid"));
        _connection.Verify(c => c.QueryAsync<CensusHistoryModel>(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
    }

    [InlineData(CensusDimensions.Total, "Primary", "Maintained", "SchoolCensusAvgHistoric")]
    [InlineData(CensusDimensions.HeadcountPerFte, "Secondary", "Maintained", "SchoolCensusAvgPerFteHistoric")]
    [InlineData(CensusDimensions.PercentWorkforce, "Pupil Referral Unit", "Maintained", "SchoolCensusAvgPercentageOfWorkforceFteHistoric")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "Nursery", "Maintained", "SchoolCensusAvgPupilsPerStaffHistoric")]
    [InlineData(CensusDimensions.Total, "University Technical College", "Academy", "SchoolCensusAvgHistoric")]
    [InlineData(CensusDimensions.HeadcountPerFte, "Alternative Provision", "Academy", "SchoolCensusAvgPerFteHistoric")]
    [InlineData(CensusDimensions.PercentWorkforce, "Post-16", "Academy", "SchoolCensusAvgPercentageOfWorkforceFteHistoric")]
    [InlineData(CensusDimensions.PupilsPerStaffRole, "Special", "Academy", "SchoolCensusAvgPupilsPerStaffHistoric")]
    [InlineData(CensusDimensions.Total, "All-through", "Academy", "SchoolCensusAvgHistoric")]
    [Theory]
    public async Task ShouldQueryAsyncWhenGetHistoryAvgNationalAsync(string dimension, string phase, string financeType, string expectedSource)
    {
        var expected = new List<CensusHistoryModel>
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
            .ReturnsAsync(expected);

        var expectedSql = $"SELECT * FROM {expectedSource} WHERE FinanceType = @FinanceType AND OverallPhase = @OverallPhase";

        var actual = await _service.GetHistoryAvgNationalAsync(dimension, phase, financeType);

        Assert.Equal(expected, actual);
        Assert.Equal(expectedSql, actualSql);
        Assert.Equivalent(new
        {
            OverallPhase = phase,
            FinanceType = financeType
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldThrowOnInvalidDimensionWhenGetHistoryAvgNationalAsync()
    {
        var result = new List<CensusHistoryModel>
        {
            new()
        };

        _connection
            .Setup(c => c.QueryAsync<CensusHistoryModel>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(result);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _service.GetHistoryAvgNationalAsync("Invalid", "Primary", "Maintained"));
        _connection.Verify(c => c.QueryAsync<CensusHistoryModel>(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
    }
}