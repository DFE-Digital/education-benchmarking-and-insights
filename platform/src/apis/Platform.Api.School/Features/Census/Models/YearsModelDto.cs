using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.School.Features.Census.Models;

[ExcludeFromCodeCoverage]
public record YearsModelDto
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}