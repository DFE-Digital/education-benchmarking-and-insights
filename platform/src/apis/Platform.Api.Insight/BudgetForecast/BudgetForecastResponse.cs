using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace Platform.Api.Insight.BudgetForecast;

public static class BudgetForecastReturnsResponseFactory
{
    public static BudgetForecastReturnResponse[] CreateForDefaultRunType(IEnumerable<BudgetForecastReturnModel> bfr, IEnumerable<ActualReturnModel> ar)
    {
        var results = bfr.Distinct().ToDictionary(x => x.Year, x => new BudgetForecastReturnResponse
        {
            Year = x.Year,
            Forecast = x.Value,
            ForecastTotalPupils = x.TotalPupils
        });

        foreach (var accountReturn in ar)
        {
            if (results.TryGetValue(accountReturn.Year, out var response))
            {
                response.Actual = accountReturn.Value;
                response.ActualTotalPupils = accountReturn.TotalPupils;
            }
        }

        return results.Values.ToArray();
    }

    public static BudgetForecastReturnMetricResponse Create(BudgetForecastReturnMetricModel model)
    {
        var response = new BudgetForecastReturnMetricResponse
        {
            Year = model.Year,
            Metric = model.Metric,
            Value = model.Value
        };

        return response;
    }
}

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnResponse
{
    public int? Year { get; set; }
    public decimal? Forecast { get; set; }
    public decimal? Actual { get; set; }
    public decimal? ForecastTotalPupils { get; set; }
    public decimal? ActualTotalPupils { get; set; }

    public decimal? Variance => Forecast.HasValue && Actual.HasValue ? Actual - Forecast : null;
    public decimal? PercentVariance => Forecast.HasValue && Actual.HasValue ? 100 - Forecast / Actual * 100 : null;
    public string? VarianceStatus => PercentVariance switch
    {
        < -10 => "AR significantly below forecast",
        >= -10 and < -5 => "AR below forecast",
        >= -5 and < 5 => "Stable forecast",
        >= 5 and < 10 => "AR above forecast",
        >= 10 => "AR significantly above forecast",
        _ => null
    };
}

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnMetricResponse
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}