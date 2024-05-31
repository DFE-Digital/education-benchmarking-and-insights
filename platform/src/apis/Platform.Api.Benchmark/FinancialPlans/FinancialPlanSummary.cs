using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Benchmark.FinancialPlans;

[ExcludeFromCodeCoverage]
public record FinancialPlanSummary
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsComplete { get; set; }
};