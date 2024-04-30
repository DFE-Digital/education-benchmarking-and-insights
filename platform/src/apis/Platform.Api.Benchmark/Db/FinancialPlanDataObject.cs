using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.Db;

[ExcludeFromCodeCoverage]
[Table("FinancialPlan")]
public record FinancialPlanDataObject
{
    [ExplicitKey] public int Year { get; set; }
    [ExplicitKey] public string? Urn { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public int Version { get; set; }
    public bool IsComplete { get; set; }

    public string? Input { get; set; }
    public string? DeploymentPlan { get; set; }
}