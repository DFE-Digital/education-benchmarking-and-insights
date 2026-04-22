// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents the place funding breakdown.
/// </summary>
[ExcludeFromCodeCoverage]
public record PlaceFunding
{
    /// <summary>
    /// Funding for primary school places.
    /// </summary>
    public decimal? Primary { get; set; }

    /// <summary>
    /// Funding for secondary school places.
    /// </summary>
    public decimal? Secondary { get; set; }

    /// <summary>
    /// Funding for special school places.
    /// </summary>
    public decimal? Special { get; set; }

    /// <summary>
    /// Funding for alternative provision places.
    /// </summary>
    public decimal? AlternativeProvision { get; set; }
}