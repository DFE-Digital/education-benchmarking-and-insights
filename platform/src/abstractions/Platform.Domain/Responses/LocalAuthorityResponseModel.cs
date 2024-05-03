using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record LocalAuthorityResponseModel
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}