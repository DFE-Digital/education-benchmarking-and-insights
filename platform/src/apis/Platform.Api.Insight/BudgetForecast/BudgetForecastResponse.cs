namespace Platform.Api.Insight.BudgetForecast;

public static class BudgetForecastReturnsResponseFactory
{
    public static BudgetForecastReturnResponse Create(BudgetForecastReturnModel model)
    {
        var response = new BudgetForecastReturnResponse
        {
            RunType = model.RunType,
            RunId = model.RunId,
            Year = model.Year,
            CompanyNumber = model.CompanyNumber,
            Category = model.Category,
            Forecast = model.Forecast,
            Actual = model.Actual,
            TotalPupils = model.TotalPupils,
            Slope = model.Slope,
            Variance = model.Variance,
            PercentVariance = model.PercentVariance
        };

        return response;
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

public record BudgetForecastReturnResponse
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Category { get; set; }
    public decimal? Forecast { get; set; }
    public decimal? Actual { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? Slope { get; set; }
    public decimal? Variance { get; set; }
    public decimal? PercentVariance { get; set; }
}

public record BudgetForecastReturnMetricResponse
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}