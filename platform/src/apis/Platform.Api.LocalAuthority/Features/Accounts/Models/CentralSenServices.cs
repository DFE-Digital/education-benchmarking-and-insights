// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents the SEN services breakdown.
/// </summary>
[ExcludeFromCodeCoverage]
public record CentralSenServices
{
    public decimal? EdPsychologyService { get; set; }
    public decimal? SenAdmin { get; set; }
}