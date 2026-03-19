// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Domain.LocalAuthorities;

public record LocalAuthority
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public double? Population2To18 { get; set; }
    public double? TotalPupils { get; set; }
    public LocalAuthoritySchool[] Schools { get; set; } = [];
    public LocalAuthorityHeadlineStatistics HeadlineStatistics { get; set; } = new();
}