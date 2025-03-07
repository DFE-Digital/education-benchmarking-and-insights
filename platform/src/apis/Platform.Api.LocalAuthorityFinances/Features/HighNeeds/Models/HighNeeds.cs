// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record HighNeeds
{
    public decimal? Total { get; set; }
    public HighNeedsAmount HighNeedsAmount { get; set; } = new();
    public TopFunding Maintained { get; set; } = new();
    public TopFunding NonMaintained { get; set; } = new();
    public PlaceFunding PlaceFunding { get; set; } = new();
}