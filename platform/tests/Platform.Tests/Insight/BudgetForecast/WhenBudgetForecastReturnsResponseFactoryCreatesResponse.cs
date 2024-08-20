using Platform.Api.Insight.BudgetForecast;
using Xunit;
namespace Platform.Tests.Insight.BudgetForecast;

public class WhenBudgetForecastReturnsResponseFactoryCreatesResponse
{
    /// <summary>
    /// Sample source input:
    /// 
    /// | Year | Value |
    /// | ---- | ----- |
    /// | 2021 | 1_007 |
    /// | 2022 | 1_011 |
    /// | 2023 | 1_012 |
    /// | 2024 | 1_013 |
    /// | 2025 | 1_014 |
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
        var bfr = new BudgetForecastReturnModel[]
        {
            new()
            {
                Year = 2020,
                Value = 1_002,
                TotalPupils = 2_002
            },
            new()
            {
                Year = 2021,
                Value = 1_003,
                TotalPupils = 2_003
            },
            new()
            {
                Year = 2022,
                Value = 1_011,
                TotalPupils = 2_011
            },
            new()
            {
                Year = 2023,
                Value = 1_012,
                TotalPupils = 2_012
            },
            new()
            {
                Year = 2024,
                Value = 1_013,
                TotalPupils = 2_013
            },
            new()
            {
                Year = 2025,
                Value = 1_014,
                TotalPupils = 2_014
            }
        };

        var ar = new ActualReturnModel[]
        {
            new()
            {
                Year = 2020,
                Value = 1_007,
                TotalPupils = 2_002
            },
            new()
            {
                Year = 2021,
                Value = 1_004,
                TotalPupils = 2_003
            },
            new()
            {
                Year = 2022,
                Value = 1_061,
                TotalPupils = 2_011
            }
        };

        // act
        var actual = BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(bfr, ar);

        // assert
        var year2020 = actual.ElementAt(0);
        Assert.Equal(2020, year2020.Year);
        Assert.Equal(1_007, year2020.Actual);
        Assert.Equal(1_002, year2020.Forecast);
        Assert.Equal(5, year2020.Variance);
        Assert.Equal("0.497", year2020.PercentVariance.GetValueOrDefault().ToString("0.000"));
        Assert.Equal("Stable forecast", year2020.VarianceStatus);

        var year2021 = actual.ElementAt(1);
        Assert.Equal(2021, year2021.Year);
        Assert.Equal(1_004, year2021.Actual);
        Assert.Equal(1_003, year2021.Forecast);
        Assert.Equal(1, year2021.Variance);
        Assert.Equal("0.100", year2021.PercentVariance.GetValueOrDefault().ToString("0.000"));
        Assert.Equal("Stable forecast", year2021.VarianceStatus);

        var year2022 = actual.ElementAt(2);
        Assert.Equal(2022, year2022.Year);
        Assert.Equal(1_061, year2022.Actual);
        Assert.Equal(1_011, year2022.Forecast);
        Assert.Equal(50, year2022.Variance);
        Assert.Equal("Stable forecast", year2022.VarianceStatus);

        var year2023 = actual.ElementAt(3);
        Assert.Equal(2023, year2023.Year);
        Assert.Null(year2023.Actual);
        Assert.Equal(1_012, year2023.Forecast);
        Assert.Null(year2023.Variance);
        Assert.Null(year2023.PercentVariance);

        var year2024 = actual.ElementAt(4);
        Assert.Equal(2024, year2024.Year);
        Assert.Null(year2024.Actual);
        Assert.Equal(1_013, year2024.Forecast);
        Assert.Null(year2024.Variance);
        Assert.Null(year2024.PercentVariance);

        var year2025 = actual.ElementAt(5);
        Assert.Equal(2025, year2025.Year);
        Assert.Null(year2025.Actual);
        Assert.Equal(1_014, year2025.Forecast);
        Assert.Null(year2025.Variance);
        Assert.Null(year2025.PercentVariance);
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
        var bfr = new BudgetForecastReturnModel[]
        {
            new()
            {
                Year = 2021,
                Value = previousValue
            }
        };

        var ar = new ActualReturnModel[]
        {
            new()
            {
                Year = 2021,
                Value = thisValue
            }
        };

        // act
        var actual = BudgetForecastReturnsResponseFactory.CreateForDefaultRunType(bfr, ar).Single();

        // assert
        Assert.Equal(status, actual.VarianceStatus);
    }
}