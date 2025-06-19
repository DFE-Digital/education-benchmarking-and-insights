// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record HighNeedsDsgYear : HighNeedsDsg
{
    public string? Code { get; set; }
    public int? Year { get; set; }
}

public record HighNeedsDsg
{
    public decimal? DsgFunding { get; set; }
    public decimal? AcademyRecoupment { get; set; }
}