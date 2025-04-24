using System.Diagnostics.CodeAnalysis;
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.Establishment.Features.LocalAuthorities.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySummary
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}