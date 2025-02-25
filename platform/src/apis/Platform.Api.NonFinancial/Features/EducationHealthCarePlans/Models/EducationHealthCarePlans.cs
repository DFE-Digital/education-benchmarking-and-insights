using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.NonFinancial.Features.EducationHealthCarePlans.Models;

[ExcludeFromCodeCoverage]
public record History<T>
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public T[]? Plans { get; set; } = [];
}

public record LocalAuthorityNumberOfPlansYear : LocalAuthorityNumberOfPlans
{
    public int? Year { get; set; }
}

public record LocalAuthorityNumberOfPlans
{
    public string? Code { get; set; }
    public decimal? Total { get; set; }
    public decimal? Mainstream { get; set; }
    public decimal? Resourced { get; set; }
    public decimal? Special { get; set; }
    public decimal? Independent { get; set; }
    public decimal? Hospital { get; set; }
    public decimal? Post16 { get; set; }
    public decimal? Other { get; set; }
}