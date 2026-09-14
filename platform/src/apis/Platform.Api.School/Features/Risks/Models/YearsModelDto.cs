using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Risks.Models;

[ExcludeFromCodeCoverage]
public record YearsModelDto
{
    public int StartYear { get; init; }
    public int EndYear { get; init; }
}
