// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

public record PlaceFunding
{
    public decimal? Primary { get; set; }
    public decimal? Secondary { get; set; }
    public decimal? Special { get; set; }
    public decimal? AlternativeProvision { get; set; }
}