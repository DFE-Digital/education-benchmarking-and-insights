// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record TopFunding
{
    public decimal? EarlyYears { get; set; }
    public decimal? Primary { get; set; }
    public decimal? Secondary { get; set; }
    public decimal? Special { get; set; }
    public decimal? AlternativeProvision { get; set; }
    public decimal? PostSchool { get; set; }
    public decimal? Income { get; set; }
}