using System.Diagnostics.CodeAnalysis;

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record LocalAuthority
{
    public string? Code { get; set; }
    public string? Name { get; set; }
};