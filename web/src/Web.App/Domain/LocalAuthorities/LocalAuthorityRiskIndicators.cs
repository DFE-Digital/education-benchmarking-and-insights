using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthorityRiskIndicators
{
    public string Urn { get; init; } = string.Empty;
    public string SchoolName { get; init; } = string.Empty;
    public string OverallGrade { get; init; } = string.Empty;
    public decimal Overall { get; init; }
    public string OverallGradeColour => MapGradeToTagColour(OverallGrade);
    public decimal Financial { get; init; }
    public decimal SchoolAndPupil { get; init; }
    public decimal EducationalPerformance { get; init; }

    private static string MapGradeToTagColour(string? grade) =>
        grade switch
        {
            "G" => "green",
            "A" => "yellow",
            "R" => "red",
            _ => "grey"
        };
}
