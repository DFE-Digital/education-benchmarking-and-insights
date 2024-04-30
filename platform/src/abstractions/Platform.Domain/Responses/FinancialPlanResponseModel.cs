using System;

namespace Platform.Domain;

public record FinancialPlanResponseModel
{
    public int Year { get; set; }
    public string? Urn { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public bool IsComplete { get; set; }
};