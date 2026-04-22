// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents the top-up funding breakdown.
/// </summary>
[ExcludeFromCodeCoverage]
public record TopFunding
{
    /// <summary>
    /// Top-up funding for early years.
    /// </summary>
    public decimal? EarlyYears { get; set; }

    /// <summary>
    /// Top-up funding for primary schools.
    /// </summary>
    public decimal? Primary { get; set; }

    /// <summary>
    /// Top-up funding for secondary schools.
    /// </summary>
    public decimal? Secondary { get; set; }

    /// <summary>
    /// Top-up funding for special schools.
    /// </summary>
    public decimal? Special { get; set; }

    /// <summary>
    /// Top-up funding for alternative provision.
    /// </summary>
    public decimal? AlternativeProvision { get; set; }

    /// <summary>
    /// Top-up funding for post-school education.
    /// </summary>
    public decimal? PostSchool { get; set; }

    /// <summary>
    /// Income related to top-up funding.
    /// </summary>
    public decimal? Income { get; set; }
}