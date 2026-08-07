using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain.LocalAuthorities;

[ExcludeFromCodeCoverage]
public record LocalAuthorityRiskIndicators
{
    public string Urn { get; init; } = string.Empty;
    public string SchoolName { get; init; } = string.Empty;
    public decimal Overall { get; init; }
    public TagColour OverallRating { get; init; } = TagColour.Grey;
    public decimal Financial { get; init; }
    public decimal SchoolAndPupil { get; init; }
    public decimal EducationalPerformance { get; init; }
}
