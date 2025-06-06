using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Establishment.Features.Schools.Models;

[ExcludeFromCodeCoverage]
public record SchoolStatus
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public bool IsPartOfTrust { get; set; }
    public bool IsMat { get; set; }
}