namespace Platform.Api.Insight.Features.BudgetForecast.Responses;

public record BudgetForecastReturnResponse
{
    public int? Year { get; set; }
    public decimal? Forecast { get; set; }
    public decimal? Actual { get; set; }
    public decimal? ForecastTotalPupils { get; set; }
    public decimal? ActualTotalPupils { get; set; }

    public decimal? Variance => Forecast.HasValue && Actual.HasValue ? Actual - Forecast : null;
    public decimal? PercentVariance => Forecast.HasValue && Actual.GetValueOrDefault() != default ? 100 - Forecast / Actual * 100 : null;

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