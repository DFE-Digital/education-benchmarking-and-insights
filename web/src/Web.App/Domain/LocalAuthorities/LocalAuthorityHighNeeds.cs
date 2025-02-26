// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Domain.LocalAuthorities;

public record LocalAuthorityHighNeeds
{
    public string? Code { get; set; }
    public HighNeedsAmount HighNeedsAmount { get; set; } = new();
    public TopFunding Maintained { get; set; } = new();
    public TopFunding NonMaintained { get; set; } = new();
    public PlaceFunding PlaceFunding { get; set; } = new();
}

public record LocalAuthorityHighNeedsYear : LocalAuthorityHighNeeds
{
    public int? Year { get; set; }
}