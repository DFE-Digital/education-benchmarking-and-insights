// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

public record History<T>
{
    public int? StartYear { get; set; }
    public int? EndYear { get; set; }
    public T[]? Outturn { get; set; } = [];
    public T[]? Budget { get; set; } = [];
}

public record LocalAuthorityHighNeedsYear : LocalAuthorityHighNeeds
{
    public int? Year { get; set; }
}

public record LocalAuthorityHighNeeds
{
    public string? Code { get; set; }
    public HighNeedsAmount HighNeedsAmount { get; set; } = new();
    public TopFunding Maintained { get; set; } = new();
    public TopFunding NonMaintained { get; set; } = new();
    public PlaceFunding PlaceFunding { get; set; } = new();
}

public record HighNeedsAmount
{
    public decimal? TotalPlaceFunding { get; set; }
    public decimal? TopUpFundingMaintained { get; set; }
    public decimal? TopUpFundingNonMaintained { get; set; }
    public decimal? SenServices { get; set; }
    public decimal? AlternativeProvisionServices { get; set; }
    public decimal? HospitalServices { get; set; }
    public decimal? OtherHealthServices { get; set; }
}

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

public record PlaceFunding
{
    public decimal? Primary { get; set; }
    public decimal? Secondary { get; set; }
    public decimal? Special { get; set; }
    public decimal? AlternativeProvision { get; set; }
}