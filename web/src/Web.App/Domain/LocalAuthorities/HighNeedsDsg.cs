// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Domain.LocalAuthorities;

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