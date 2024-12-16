using Moq;
using Platform.Api.Insight.Expenditure;
using Platform.Sql;
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
    public async Task ShouldQueryAsyncWhenQuerySchoolsAsyncWithUrns()
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
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "URNS");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync(urns, null, null, null);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolExpenditure WHERE URN IN @URNS", actualSql?.Trim());
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
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "CompanyNumber", "Phase");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync([], companyNumber, null, phase);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolExpenditure WHERE TrustCompanyNumber = @CompanyNumber AND OverallPhase = @Phase", actualSql?.Trim());
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
                actualParam = TestDatabase.GetDictionaryFromDynamicParameters(param, "LaCode", "Phase");
            })
            .ReturnsAsync(results);

        // act
        var actual = await _service.QuerySchoolsAsync([], null, laCode, phase);

        // assert
        Assert.Equal(results, actual);
        Assert.Equal("SELECT * from SchoolExpenditure WHERE LaCode = @LaCode AND OverallPhase = @Phase", actualSql?.Trim());
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

    [InlineData(ExpenditureDimensions.Actuals, "SchoolExpenditureAvgComparatorSet")]
    [InlineData(ExpenditureDimensions.PerUnit, "SchoolExpenditureAvgPerUnitComparatorSet")]
    [InlineData(ExpenditureDimensions.PercentIncome, "SchoolExpenditureAvgPercentageOfIncomeComparatorSet")]
    [InlineData(ExpenditureDimensions.PercentExpenditure, "SchoolExpenditureAvgPercentageOfExpenditureComparatorSet")]
    [Theory]
    public async Task ShouldQueryAsyncWhenGetSchoolHistoryAvgComparatorSetAsync(string dimension, string expectedSource)
    {
        const string urn = "123";
        var expected = new List<SchoolExpenditureHistoryResponse>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolExpenditureHistoryResponse>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(expected);

        var expectedSql = $"SELECT * FROM {expectedSource} WHERE URN = @URN";

        var actual = await _service.GetSchoolHistoryAvgComparatorSetAsync(urn, dimension);

        Assert.Equal(expected, actual);
        Assert.Equal(expectedSql, actualSql);
        Assert.Equivalent(new
        {
            URN = urn,
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldThrowOnInvalidDimensionWhenGetSchoolHistoryAvgComparatorSetAsync()
    {
        const string urn = "123";
        var result = new List<SchoolExpenditureHistoryResponse>
        {
            new()
        };

        _connection
            .Setup(c => c.QueryAsync<SchoolExpenditureHistoryResponse>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(result);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _service.GetSchoolHistoryAvgComparatorSetAsync(urn, "invalid"));
        _connection.Verify(c => c.QueryAsync<SchoolExpenditureHistoryResponse>(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
    }

    [InlineData(ExpenditureDimensions.Actuals, "Primary", "Maintained", "SchoolExpenditureAvgHistoric")]
    [InlineData(ExpenditureDimensions.PerUnit, "Secondary", "Maintained", "SchoolExpenditureAvgPerUnitHistoric")]
    [InlineData(ExpenditureDimensions.PercentIncome, "Pupil Referral Unit", "Maintained", "SchoolExpenditureAvgPercentageOfIncomeHistoric")]
    [InlineData(ExpenditureDimensions.PercentExpenditure, "Nursery", "Maintained", "SchoolExpenditureAvgPercentageOfExpenditureHistoric")]
    [InlineData(ExpenditureDimensions.Actuals, "University Technical College", "Academy", "SchoolExpenditureAvgHistoric")]
    [InlineData(ExpenditureDimensions.PerUnit, "Alternative Provision", "Academy", "SchoolExpenditureAvgPerUnitHistoric")]
    [InlineData(ExpenditureDimensions.PercentIncome, "Post-16", "Academy", "SchoolExpenditureAvgPercentageOfIncomeHistoric")]
    [InlineData(ExpenditureDimensions.PercentExpenditure, "Special", "Academy", "SchoolExpenditureAvgPercentageOfExpenditureHistoric")]
    [InlineData(ExpenditureDimensions.Actuals, "All-through", "Academy", "SchoolExpenditureAvgHistoric")]
    [Theory]
    public async Task ShouldQueryAsyncWhenGetSchoolHistoryAvgNationalAsync(string dimension, string phase, string financeType, string expectedSource)
    {
        var expected = new List<SchoolExpenditureHistoryResponse>
        {
            new()
        };
        string? actualSql = null;
        object? actualParam = null;

        _connection
            .Setup(c => c.QueryAsync<SchoolExpenditureHistoryResponse>(It.IsAny<string>(), It.IsAny<object>()))
            .Callback((string sql, object? param) =>
            {
                actualSql = sql;
                actualParam = param;
            })
            .ReturnsAsync(expected);

        var expectedSql = $"SELECT * FROM {expectedSource} WHERE FinanceType = @FinanceType AND OverallPhase = @OverallPhase";

        var actual = await _service.GetSchoolHistoryAvgNationalAsync(dimension, phase, financeType);

        Assert.Equal(expected, actual);
        Assert.Equal(expectedSql, actualSql);
        Assert.Equivalent(new
        {
            OverallPhase = phase,
            FinanceType = financeType
        }, actualParam, true);
    }

    [Fact]
    public async Task ShouldThrowOnInvalidDimensionWhenGetSchoolHistoryAvgNationalAsync()
    {
        var queryParams = new ExpenditureNationalAvgParameters()
        {
            Dimension = "Invalid"
        };
        var result = new List<SchoolExpenditureHistoryResponse>
        {
            new()
        };

        _connection
            .Setup(c => c.QueryAsync<SchoolExpenditureHistoryResponse>(It.IsAny<string>(), It.IsAny<object>()))
            .ReturnsAsync(result);

        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() =>
            _service.GetSchoolHistoryAvgNationalAsync("invalid", "Primary", "Maintained"));
        _connection.Verify(c => c.QueryAsync<SchoolExpenditureHistoryResponse>(It.IsAny<string>(), It.IsAny<object>()), Times.Never());
    }
}