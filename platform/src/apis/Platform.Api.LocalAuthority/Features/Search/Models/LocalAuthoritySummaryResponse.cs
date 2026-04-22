using System.Diagnostics.CodeAnalysis;

// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global

namespace Platform.Api.LocalAuthority.Features.Search.Models;

/// <summary>
/// Represents a summary of a local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record LocalAuthoritySummaryResponse
{
    /// <summary>
    /// The three-digit local authority code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The name of the local authority.
    /// </summary>
    public string? Name { get; set; }
}