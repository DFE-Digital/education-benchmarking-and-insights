// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.Controllers.Api.Responses;

public record LocalAuthorityHighNeedsHistoryResponse
{
    public int? Year { get; init; }
    public string? Term { get; init; }
    public LocalAuthorityHighNeedsApiResponse? Outturn { get; init; }
    public LocalAuthorityHighNeedsApiResponse? Budget { get; init; }
}

public record LocalAuthorityHighNeedsComparisonResponse
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public double? Population2To18 { get; init; }
    public LocalAuthorityHighNeedsApiResponse? Outturn { get; init; }
    public LocalAuthorityHighNeedsApiResponse? Budget { get; init; }
}

public record LocalAuthorityHighNeedsApiResponse
{
    public decimal? HighNeedsAmountTotalPlaceFunding { get; init; }
    public decimal? HighNeedsAmountTopUpFundingMaintained { get; init; }
    public decimal? HighNeedsAmountTopUpFundingNonMaintained { get; init; }
    public decimal? HighNeedsAmountSenServices { get; init; }
    public decimal? HighNeedsAmountAlternativeProvisionServices { get; init; }
    public decimal? HighNeedsAmountHospitalServices { get; init; }
    public decimal? HighNeedsAmountOtherHealthServices { get; init; }

    public decimal? MaintainedEarlyYears { get; init; }
    public decimal? MaintainedPrimary { get; init; }
    public decimal? MaintainedSecondary { get; init; }
    public decimal? MaintainedSpecial { get; init; }
    public decimal? MaintainedAlternativeProvision { get; init; }
    public decimal? MaintainedPostSchool { get; init; }
    public decimal? MaintainedIncome { get; init; }

    public decimal? NonMaintainedEarlyYears { get; init; }
    public decimal? NonMaintainedPrimary { get; init; }
    public decimal? NonMaintainedSecondary { get; init; }
    public decimal? NonMaintainedSpecial { get; init; }
    public decimal? NonMaintainedAlternativeProvision { get; init; }
    public decimal? NonMaintainedPostSchool { get; init; }
    public decimal? NonMaintainedIncome { get; init; }

    public decimal? PlaceFundingPrimary { get; init; }
    public decimal? PlaceFundingSecondary { get; init; }
    public decimal? PlaceFundingSpecial { get; init; }
    public decimal? PlaceFundingAlternativeProvision { get; init; }
}

public record LocalAuthorityHighNeedsHistoryDashboardResponse
{
    public int? Year { get; internal set; }
    public decimal? Outturn { get; init; }
    public decimal? Budget { get; init; }
    public decimal? Balance { get; init; }
}