// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

public record HighNeeds : HighNeedsBase
{
    public HighNeedsAmount? HighNeedsAmount { get; set; }
    public TopFunding? Maintained { get; set; }
    public TopFunding? NonMaintained { get; set; }
    public PlaceFunding? PlaceFunding { get; set; }
}

public record HighNeedsBase
{
    public decimal? Total { get; set; }
}