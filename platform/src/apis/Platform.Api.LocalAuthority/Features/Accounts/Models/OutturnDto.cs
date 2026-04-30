using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record OutturnDto
{
    public string? LaCode { get; set; }
    public string? Name { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? OutturnTotalHighNeeds { get; set; }
    public decimal? OutturnTotalPlaceFunding { get; set; }
    public decimal? OutturnTotalTopUpFundingMaintained { get; set; }
    public decimal? OutturnTotalTopUpFundingNonMaintained { get; set; }
    public decimal? OutturnTotalSenServices { get; set; }
    public decimal? OutturnTotalAlternativeProvisionServices { get; set; }
    public decimal? OutturnTotalHospitalServices { get; set; }
    public decimal? OutturnTotalOtherHealthServices { get; set; }
    public decimal? OutturnTopFundingMaintainedEarlyYears { get; set; }
    public decimal? OutturnTopFundingMaintainedPrimary { get; set; }
    public decimal? OutturnTopFundingMaintainedSecondary { get; set; }
    public decimal? OutturnTopFundingMaintainedSpecial { get; set; }
    public decimal? OutturnTopFundingMaintainedAlternativeProvision { get; set; }
    public decimal? OutturnTopFundingMaintainedPostSchool { get; set; }
    public decimal? OutturnTopFundingMaintainedIncome { get; set; }
    public decimal? OutturnTopFundingNonMaintainedEarlyYears { get; set; }
    public decimal? OutturnTopFundingNonMaintainedPrimary { get; set; }
    public decimal? OutturnTopFundingNonMaintainedSecondary { get; set; }
    public decimal? OutturnTopFundingNonMaintainedSpecial { get; set; }
    public decimal? OutturnTopFundingNonMaintainedAlternativeProvision { get; set; }
    public decimal? OutturnTopFundingNonMaintainedPostSchool { get; set; }
    public decimal? OutturnTopFundingNonMaintainedIncome { get; set; }
    public decimal? OutturnPlaceFundingPrimary { get; set; }
    public decimal? OutturnPlaceFundingSecondary { get; set; }
    public decimal? OutturnPlaceFundingSpecial { get; set; }
    public decimal? OutturnPlaceFundingAlternativeProvision { get; set; }
    public decimal? OutturnSENTransportDSG { get; set; }
    public decimal? OutturnHometoSchoolTransportPre16 { get; set; }
    public decimal? OutturnHometoSchoolTransport1618 { get; set; }
    public decimal? OutturnHometoSchoolTransport1925 { get; set; }
    public decimal? OutturnEdPsychologyService { get; set; }
    public decimal? OutturnSENAdmin { get; set; }

    public static readonly string[] Fields =
    [
        nameof(LaCode),
        nameof(Name),
        nameof(TotalPupils),
        nameof(OutturnTotalHighNeeds),
        nameof(OutturnTotalPlaceFunding),
        nameof(OutturnTotalTopUpFundingMaintained),
        nameof(OutturnTotalTopUpFundingNonMaintained),
        nameof(OutturnTotalSenServices),
        nameof(OutturnTotalAlternativeProvisionServices),
        nameof(OutturnTotalHospitalServices),
        nameof(OutturnTotalOtherHealthServices),
        nameof(OutturnTopFundingMaintainedEarlyYears),
        nameof(OutturnTopFundingMaintainedPrimary),
        nameof(OutturnTopFundingMaintainedSecondary),
        nameof(OutturnTopFundingMaintainedSpecial),
        nameof(OutturnTopFundingMaintainedAlternativeProvision),
        nameof(OutturnTopFundingMaintainedPostSchool),
        nameof(OutturnTopFundingMaintainedIncome),
        nameof(OutturnTopFundingNonMaintainedEarlyYears),
        nameof(OutturnTopFundingNonMaintainedPrimary),
        nameof(OutturnTopFundingNonMaintainedSecondary),
        nameof(OutturnTopFundingNonMaintainedSpecial),
        nameof(OutturnTopFundingNonMaintainedAlternativeProvision),
        nameof(OutturnTopFundingNonMaintainedPostSchool),
        nameof(OutturnTopFundingNonMaintainedIncome),
        nameof(OutturnPlaceFundingPrimary),
        nameof(OutturnPlaceFundingSecondary),
        nameof(OutturnPlaceFundingSpecial),
        nameof(OutturnPlaceFundingAlternativeProvision),
        nameof(OutturnSENTransportDSG),
        nameof(OutturnHometoSchoolTransportPre16),
        nameof(OutturnHometoSchoolTransport1618),
        nameof(OutturnHometoSchoolTransport1925),
        nameof(OutturnEdPsychologyService),
        nameof(OutturnSENAdmin)
    ];
}