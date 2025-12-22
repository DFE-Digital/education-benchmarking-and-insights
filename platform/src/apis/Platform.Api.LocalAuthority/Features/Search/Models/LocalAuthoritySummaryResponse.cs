using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.LocalAuthority.Features.Search.Models;

[ExcludeFromCodeCoverage]
public record LocalAuthoritySummaryResponse
{
    public string? Code { get; set; }
    public string? Name { get; set; }
}