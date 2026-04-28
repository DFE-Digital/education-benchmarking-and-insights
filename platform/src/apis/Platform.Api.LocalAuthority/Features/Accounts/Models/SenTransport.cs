// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

/// <summary>
/// Represents the SEN transport breakdown.
/// </summary>
[ExcludeFromCodeCoverage]
public record SenTransport
{
    public decimal? SenTransportDsg { get; set; }
    public decimal? HomeToSchoolTransportPre16 { get; set; }
    public decimal? HomeToSchoolTransport16To18 { get; set; }
    public decimal? HomeToSchoolTransport19To25 { get; set; }
}