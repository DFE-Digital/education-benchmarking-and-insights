using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record OutturnDto
{
    public string? LaCode { get; set; }
    public string? Name { get; set; }
    public string? TotalPupils { get; set; }
    public string? OutturnTotalHighNeeds { get; set; }
    public string? OutturnTotalPlaceFunding { get; set; }
    public string? OutturnTotalTopUpFundingMaintained { get; set; }
    public string? OutturnTotalTopUpFundingNonMaintained { get; set; }
    public string? OutturnTotalSenServices { get; set; }
    public string? OutturnTotalAlternativeProvisionServices { get; set; }
    public string? OutturnTotalHospitalServices { get; set; }
    public string? OutturnTotalOtherHealthServices { get; set; }
    public string? OutturnTopFundingMaintainedEarlyYears { get; set; }
    public string? OutturnTopFundingMaintainedPrimary { get; set; }
    public string? OutturnTopFundingMaintainedSecondary { get; set; }
    public string? OutturnTopFundingMaintainedSpecial { get; set; }
    public string? OutturnTopFundingMaintainedAlternativeProvision { get; set; }
    public string? OutturnTopFundingMaintainedPostSchool { get; set; }
    public string? OutturnTopFundingMaintainedIncome { get; set; }
    public string? OutturnTopFundingNonMaintainedEarlyYears { get; set; }
    public string? OutturnTopFundingNonMaintainedPrimary { get; set; }
    public string? OutturnTopFundingNonMaintainedSecondary { get; set; }
    public string? OutturnTopFundingNonMaintainedSpecial { get; set; }
    public string? OutturnTopFundingNonMaintainedAlternativeProvision { get; set; }
    public string? OutturnTopFundingNonMaintainedPostSchool { get; set; }
    public string? OutturnTopFundingNonMaintainedIncome { get; set; }
    public string? OutturnPlaceFundingPrimary { get; set; }
    public string? OutturnPlaceFundingSecondary { get; set; }
    public string? OutturnPlaceFundingSpecial { get; set; }
    public string? OutturnPlaceFundingAlternativeProvision { get; set; }
    public string? OutturnSENTransportDSG { get; set; }
    public string? OutturnHometoSchoolTransportPre16 { get; set; }
    public string? OutturnHometoSchoolTransport1618 { get; set; }
    public string? OutturnHometoSchoolTransport1925 { get; set; }
    public string? OutturnEdPsychologyService { get; set; }
    public string? OutturnSENAdmin { get; set; }

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