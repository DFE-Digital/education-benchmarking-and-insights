// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record PlaceFunding
{
    public decimal? Primary { get; set; }
    public decimal? Secondary { get; set; }
    public decimal? Special { get; set; }
    public decimal? AlternativeProvision { get; set; }
}