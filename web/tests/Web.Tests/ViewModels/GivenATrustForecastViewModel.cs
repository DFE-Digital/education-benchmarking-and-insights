using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenATrustForecastViewModel
{
    private const int Year = 2023;
    private readonly Fixture _fixture = new();
    private readonly Trust _trust;

    public GivenATrustForecastViewModel()
    {
        _trust = _fixture.Create<Trust>();
    }

    [Fact]
    public void WhenBalanceInDeficit()
    {
        var metrics = Array.Empty<BudgetForecastReturnMetric>();
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                Year = Year,
                Actual = -1_000_000m
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.BalancesInDeficit);
        Assert.True(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenBalancesForecastingDeficit()
    {
        var metrics = Array.Empty<BudgetForecastReturnMetric>();
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                Year = Year,
                Actual = 1_000_000m
            },
            new BudgetForecastReturn
            {
                Year = Year + 1,
                Forecast = -1_000_000m
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.BalancesForecastingDeficit);
        Assert.True(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenSteepDeclineInBalances()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = -1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = -1
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Staff costs as percentage of income",
                Value = 50
            }
        };
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                VarianceStatus = "AR above forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.SteepDeclineInBalances);
        Assert.True(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenSteepDeclineInBalancesAndHighProportionStaffCosts()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = -1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = -1
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Staff costs as percentage of income",
                Value = 80
            }
        };
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                VarianceStatus = "AR above forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.SteepDeclineInBalancesAndHighProportionStaffCosts);
        Assert.True(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenDeclineInBalancesButNoForecastDecline()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = -1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = 0
            }
        };
        var returns = Array.Empty<BudgetForecastReturn>();

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.DeclineInBalancesButNoForecastDecline);
        Assert.False(vm.IsRed);
        Assert.True(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenDeclineInBalancesButAboveForecastHistory()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = -1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = -1
            }
        };
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                VarianceStatus = "AR above forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "AR above forecast"
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.DeclineInBalancesButAboveForecastHistory);
        Assert.False(vm.IsRed);
        Assert.True(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenSteepInclineInBalancesForecastButBelowForecastHistory()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = 1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = 1
            }
        };
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                VarianceStatus = "AR significantly below forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "AR significantly below forecast"
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.SteepInclineInBalancesForecastButBelowForecastHistory);
        Assert.False(vm.IsRed);
        Assert.True(vm.IsAmber);
        Assert.False(vm.IsGreen);
    }

    [Fact]
    public void WhenBalancesStableAndPositive()
    {
        var metrics = Array.Empty<BudgetForecastReturnMetric>();
        var returns = new[]
        {
            new BudgetForecastReturn
            {
               Year = Year - 1,
               Actual = 1_000_000
            },
            new BudgetForecastReturn
            {
                Year = Year,
                Actual = 1_100_000
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.BalancesStableAndPositive);
        Assert.False(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.True(vm.IsGreen);
    }

    [Fact]
    public void WhenBalancesIncreasingSteadily()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = 1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = 0
            }
        };
        var returns = Array.Empty<BudgetForecastReturn>();

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.BalancesIncreasingSteadily);
        Assert.False(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.True(vm.IsGreen);
    }

    [Fact]
    public void WhenBalancesIncreasingSteeply()
    {
        var metrics = new[]
        {
            new BudgetForecastReturnMetric
            {
                Metric = "Slope",
                Value = 1.23m
            },
            new BudgetForecastReturnMetric
            {
                Metric = "Slope flag",
                Value = 1
            }
        };
        var returns = new[]
        {
            new BudgetForecastReturn
            {
                VarianceStatus = "AR significantly below forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            },
            new BudgetForecastReturn
            {
                VarianceStatus = "Stable forecast"
            }
        };

        var vm = new TrustForecastViewModel(_trust, metrics, returns, Year);

        Assert.True(vm.BalancesIncreasingSteeply);
        Assert.False(vm.IsRed);
        Assert.False(vm.IsAmber);
        Assert.True(vm.IsGreen);
    }
}