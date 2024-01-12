using System.Diagnostics.CodeAnalysis;

namespace EducationBenchmarking.Web.Domain;

[ExcludeFromCodeCoverage]
public class AreaOfInvestigation
{
    public string AreaGroup { get; set; }
    public string AreaName { get; set; }
    public decimal Score { get; set; }
    public string? RatingText { get; set; }
    public string? RatingColour { get; set; }
}