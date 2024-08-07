﻿using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.BudgetForecast;

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnModel
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Category { get; set; }
    public decimal? Value { get; set; }
    public decimal? TotalPupils { get; set; }
}

[ExcludeFromCodeCoverage]
public record BudgetForecastReturnMetricModel
{
    public string? RunType { get; set; }
    public string? RunId { get; set; }
    public int? Year { get; set; }
    public string? CompanyNumber { get; set; }
    public string? Metric { get; set; }
    public decimal? Value { get; set; }
}