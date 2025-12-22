using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;

[ExcludeFromCodeCoverage]
public record YearsModelDto
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}