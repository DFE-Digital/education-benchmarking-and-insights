using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Features.Census.Responses;

[ExcludeFromCodeCoverage]
public record CensusSchoolResponse : CensusResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}