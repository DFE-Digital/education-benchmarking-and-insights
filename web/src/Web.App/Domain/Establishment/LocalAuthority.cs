using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record LocalAuthority
{
    public string? Code { get; set; }
    public string? Name { get; set; }

    public LocalAuthoritySchool[] Schools { get; set; } = [];
}