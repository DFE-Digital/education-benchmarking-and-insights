// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record HighNeeds : HighNeedsBase
{
    public HighNeedsAmount? HighNeedsAmount { get; set; }
    public TopFunding? Maintained { get; set; }
    public TopFunding? NonMaintained { get; set; }
    public PlaceFunding? PlaceFunding { get; set; }
}

[ExcludeFromCodeCoverage]
public record HighNeedsBase
{
    public decimal? Total { get; set; }
}