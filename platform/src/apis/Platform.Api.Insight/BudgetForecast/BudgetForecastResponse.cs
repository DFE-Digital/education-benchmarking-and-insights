using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
namespace Platform.Api.Insight.BudgetForecast;

public static class BudgetForecastReturnsResponseFactory
{
    public static BudgetForecastReturnResponse[] CreateForDefaultRunType(IEnumerable<BudgetForecastReturnModel> models)
    {
        var results = new Dictionary<int, BudgetForecastReturnResponse>();
        foreach (var model in models
                     .Where(m => m.RunType == "default")
                     .Where(m => m.Year != null)
                     .Where(m => m.RunId != null)
                     .OrderBy(m => m.RunId)
                     .ThenBy(m => m.Year))
        {
            if (!int.TryParse(model.RunId, out var runId))
            {
                throw new ArithmeticException($"Expected {nameof(model.RunId)} to be of type int for {nameof(model.RunType)} default but found '{model.RunId}'");
            }

            if (!results.TryGetValue(model.Year!.Value, out var response))
            {
                response = new BudgetForecastReturnResponse();
            }

            response.Year = model.Year;

            // year being processed is in the future compared to the RunId corresponding to the BFR submission date
            if (model.Year > runId)
            {
                // ensure the latest forecast is set for the year being processed in the case of multiple differing
                // forecasts over the range of historical BFR submissions
                response.Forecast = model.Value;
                response.ForecastTotalPupils = model.TotalPupils;
            }
            else if (model.Value == 0)
            {
                // historical zero values should be skipped
                continue;
            }
            else if (response.Actual == null)
            {
                // sanity check that the first actual values are not overwritten by a subsequent RunId year's values
                response.Actual = model.Value;
                response.ActualTotalPupils = model.TotalPupils;
            }

            results[response.Year.Value] = response;
        }

        return results.Values.ToArray();
    }

    public static BudgetForecastReturnMetricResponse Create(BudgetForecastReturnMetricModel model)
    {
        var response = new BudgetForecastReturnMetricResponse
        {
            RunType = model.RunType,
            RunId = model.RunId,
            Year = model.Year,
            CompanyNumber = model.CompanyNumber,
            Metric = model.Metric,
            Value = model.Value
        };

        return response;
    }
}

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnResponse
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }

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