using Platform.Api.Insight.BudgetForecast;
using Xunit;
namespace Platform.Tests.Insight.BudgetForecast;

public class WhenBudgetForecastReturnsResponseFactoryCreatesResponse
{
    /// <summary>
    /// Sample source input:
    /// 
    /// | RunId | Year | Value |
    /// | ----- | ---- | ----- |
    /// | 2020  | 2018 | 0     |
    /// | 2020  | 2019 | 1_002 |
    /// | 2020  | 2020 | 1_003 |
    /// | 2020  | 2021 | 1_004 |
    /// | 2020  | 2022 | 1_005 |
    /// | 2020  | 2023 | 1_006 |
    /// | 2021  | 2019 | 1_002 |
    /// | 2021  | 2020 | 1_003 |
    /// | 2021  | 2021 | 1_007 |
    /// | 2021  | 2022 | 1_008 |
    /// | 2021  | 2023 | 1_009 |
    /// | 2021  | 2024 | 1_010 |
    /// | 2022  | 2020 | 1_003 |
    /// | 2022  | 2021 | 1_007 |
    /// | 2022  | 2022 | 1_011 |
    /// | 2022  | 2023 | 1_012 |
    /// | 2022  | 2024 | 1_013 |
    /// | 2022  | 2025 | 1_014 |
    ///
    /// Expected output:
    /// 
    /// | Year | Actual | Forecast |
    /// | ---- | ------ | -------- |
    /// | 2019 | 1_002  |          |
    /// | 2020 | 1_003  |          |
    /// | 2021 | 1_007  | 1_004    |
    /// | 2022 | 1_011  | 1_008    |
    /// | 2023 |        | 1_012    |
    /// | 2024 |        | 1_013    |
    /// | 2025 |        | 1_014    |
    /// </summary>
    [Fact]
    public void ShouldBuildResponseModelForDefaultRunType()
    {
        // arrange
        var models = new BudgetForecastReturnModel[]
        {
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2018,
                Value = 0,
                TotalPupils = 0
            },
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2019,
                Value = 1_002,
                TotalPupils = 2_002
            },
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2020,
                Value = 1_003,
                TotalPupils = 2_003
            },
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2021,
                Value = 1_004,
                TotalPupils = 2_004
            },
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2022,
                Value = 1_005,
                TotalPupils = 2_005
            },
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2023,
                Value = 1_006,
                TotalPupils = 2_006
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2019,
                Value = 1_002,
                TotalPupils = 2_002
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2020,
                Value = 1_003,
                TotalPupils = 2_003
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2021,
                Value = 1_007,
                TotalPupils = 2_007
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2022,
                Value = 1_008,
                TotalPupils = 2_008
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2023,
                Value = 1_009,
                TotalPupils = 2_009
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2024,
                Value = 1_010,
                TotalPupils = 2_010
            },
            new()
            {
                RunType = "default",
                RunId = "2022",
                Year = 2020,
                Value = 1_002,
                TotalPupils = 2_002
            },
            new()
            {
                RunType = "default",
                RunId = "2022",
                Year = 2021,
                Value = 1_003,
                TotalPupils = 2_003
            },
            new()
            {
                RunType = "default",
                RunId = "2022",
                Year = 2022,
                Value = 1_011,
                TotalPupils = 2_011
            },
            new()
            {
                RunType = "default",
                RunId = "2022",
                Year = 2023,
                Value = 1_012,
                TotalPupils = 2_012
            },
            new()
            {
                RunType = "default",
                RunId = "2022",
                Year = 2024,
                Value = 1_013,
                TotalPupils = 2_013
            },
            new()
            {
                RunType = "default",
                RunId = "2022",
                Year = 2025,
                Value = 1_014,
                TotalPupils = 2_014
            }
        };

        // act
        var actual = BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(models);

        // assert
        var year2019 = actual.ElementAt(0);
        Assert.Equal(2019, year2019.Year);
        Assert.Equal(1_002, year2019.Actual);
        Assert.Null(year2019.Forecast);
        Assert.Null(year2019.Variance);
        Assert.Null(year2019.PercentVariance);

        var year2020 = actual.ElementAt(1);
        Assert.Equal(2020, year2020.Year);
        Assert.Equal(1_003, year2020.Actual);
        Assert.Null(year2020.Forecast);
        Assert.Null(year2020.Variance);
        Assert.Null(year2020.PercentVariance);

        var year2021 = actual.ElementAt(2);
        Assert.Equal(2021, year2021.Year);
        Assert.Equal(1_007, year2021.Actual);
        Assert.Equal(1_004, year2021.Forecast);
        Assert.Equal(3, year2021.Variance);
        Assert.Equal("0.298", year2021.PercentVariance.GetValueOrDefault().ToString("0.000"));
        Assert.Equal("Stable forecast", year2021.VarianceStatus);

        var year2022 = actual.ElementAt(3);
        Assert.Equal(2022, year2022.Year);
        Assert.Equal(1_011, year2022.Actual);
        Assert.Equal(1_008, year2022.Forecast);
        Assert.Equal(3, year2022.Variance);
        Assert.Equal("Stable forecast", year2022.VarianceStatus);

        var year2023 = actual.ElementAt(4);
        Assert.Equal(2023, year2023.Year);
        Assert.Null(year2023.Actual);
        Assert.Equal(1_012, year2023.Forecast);
        Assert.Null(year2023.Variance);
        Assert.Null(year2023.PercentVariance);

        var year2024 = actual.ElementAt(5);
        Assert.Equal(2024, year2024.Year);
        Assert.Null(year2024.Actual);
        Assert.Equal(1_013, year2024.Forecast);
        Assert.Null(year2024.Variance);
        Assert.Null(year2024.PercentVariance);

        var year2025 = actual.ElementAt(6);
        Assert.Equal(2025, year2025.Year);
        Assert.Null(year2025.Actual);
        Assert.Equal(1_014, year2025.Forecast);
        Assert.Null(year2025.Variance);
        Assert.Null(year2025.PercentVariance);
    }

    [Fact]
    public void ShouldThrowExceptionForMalformedDefaultRunType()
    {
        // arrange
        var models = new BudgetForecastReturnModel[]
        {
            new()
            {
                RunType = "default",
                RunId = "runId",
                Year = 2018,
                Value = 1_001
            }
        };

        // act
        var actual = Assert.Throws<ArithmeticException>(() => BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(models));

        // assert
        Assert.Equal("Expected RunId to be of type int for RunType default but found 'runId'", actual.Message);
    }

    [Theory]
    [InlineData(1_000, 500, "AR significantly below forecast")]
    [InlineData(1_000, 950, "AR below forecast")]
    [InlineData(1_000, 1_010, "Stable forecast")]
    [InlineData(1_000, 1_100, "AR above forecast")]
    [InlineData(1_000, 1_500, "AR significantly above forecast")]
    public void ShouldSetExpectedVarianceStatus(decimal previousValue, decimal thisValue, string status)
    {
        // arrange
        var models = new BudgetForecastReturnModel[]
        {
            new()
            {
                RunType = "default",
                RunId = "2020",
                Year = 2021,
                Value = previousValue
            },
            new()
            {
                RunType = "default",
                RunId = "2021",
                Year = 2021,
                Value = thisValue
            }
        };

        // act
        var actual = BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(models).Single();

        // assert
        Assert.Equal(status, actual.VarianceStatus);
    }
}