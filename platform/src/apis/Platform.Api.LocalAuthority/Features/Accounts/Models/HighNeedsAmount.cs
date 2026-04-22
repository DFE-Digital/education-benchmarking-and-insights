// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents the breakdown of high needs amounts.
/// </summary>
[ExcludeFromCodeCoverage]
public record HighNeedsAmount
{
    /// <summary>
    /// The total place funding.
    /// </summary>
    public decimal? TotalPlaceFunding { get; set; }

    /// <summary>
    /// The top-up funding for maintained schools.
    /// </summary>
    public decimal? TopUpFundingMaintained { get; set; }

    /// <summary>
    /// The top-up funding for non-maintained schools.
    /// </summary>
    public decimal? TopUpFundingNonMaintained { get; set; }

    /// <summary>
    /// The funding for SEN services.
    /// </summary>
    public decimal? SenServices { get; set; }

    /// <summary>
    /// The funding for alternative provision services.
    /// </summary>
    public decimal? AlternativeProvisionServices { get; set; }

    /// <summary>
    /// The funding for hospital services.
    /// </summary>
    public decimal? HospitalServices { get; set; }

    /// <summary>
    /// The funding for other health services.
    /// </summary>
    public decimal? OtherHealthServices { get; set; }
}