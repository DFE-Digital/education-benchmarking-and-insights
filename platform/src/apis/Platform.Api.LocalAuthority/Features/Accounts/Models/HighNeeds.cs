// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents the high needs financial data.
/// </summary>
[ExcludeFromCodeCoverage]
public record HighNeeds : HighNeedsBase
{
    /// <summary>
    /// The high needs amount breakdown.
    /// </summary>
    public HighNeedsAmount? HighNeedsAmount { get; set; }

    /// <summary>
    /// Top-up funding for maintained schools.
    /// </summary>
    public TopFunding? Maintained { get; set; }

    /// <summary>
    /// Top-up funding for non-maintained schools.
    /// </summary>
    public TopFunding? NonMaintained { get; set; }

    /// <summary>
    /// Place funding breakdown.
    /// </summary>
    public PlaceFunding? PlaceFunding { get; set; }

    /// <summary>
    /// SEN transport breakdown.
    /// </summary>
    public SenTransport? SenTransport { get; set; }

    /// <summary>
    /// SEN services breakdown.
    /// </summary>
    public CentralSenServices? CentralSenServices { get; set; }
}

/// <summary>
/// The base record for high needs financial data.
/// </summary>
[ExcludeFromCodeCoverage]
public record HighNeedsBase
{
    /// <summary>
    /// The total high needs financial data.
    /// </summary>
    public decimal? Total { get; set; }
}