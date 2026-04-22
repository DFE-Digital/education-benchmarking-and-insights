// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents high needs data for a specific year and local authority.
/// </summary>
[ExcludeFromCodeCoverage]
public record HighNeedsYear : HighNeeds
{
    /// <summary>
    /// The three-digit local authority code.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// The academic or financial year.
    /// </summary>
    public int? Year { get; set; }
}

[ExcludeFromCodeCoverage]
public record HighNeedsYearBase
{
    public string? Code { get; set; }
    public string? RunId { get; set; }
}