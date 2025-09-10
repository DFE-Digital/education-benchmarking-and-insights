// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global

namespace Web.App.Domain.LocalAuthorities;

public record HighNeeds
{
    public decimal? Total { get; set; }
    public HighNeedsAmount? HighNeedsAmount { get; set; }
    public TopFunding? Maintained { get; set; }
    public TopFunding? NonMaintained { get; set; }
    public PlaceFunding? PlaceFunding { get; set; }
}