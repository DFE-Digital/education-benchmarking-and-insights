using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record BudgetDto
{
    public string? LaCode { get; set; }
    public string? Name { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? BudgetTotalHighNeeds { get; set; }
    public decimal? BudgetTotalPlaceFunding { get; set; }
    public decimal? BudgetTotalTopUpFundingMaintained { get; set; }
    public decimal? BudgetTotalTopUpFundingNonMaintained { get; set; }
    public decimal? BudgetTotalSenServices { get; set; }
    public decimal? BudgetTotalAlternativeProvisionServices { get; set; }
    public decimal? BudgetTotalHospitalServices { get; set; }
    public decimal? BudgetTotalOtherHealthServices { get; set; }
    public decimal? BudgetTopFundingMaintainedEarlyYears { get; set; }
    public decimal? BudgetTopFundingMaintainedPrimary { get; set; }
    public decimal? BudgetTopFundingMaintainedSecondary { get; set; }
    public decimal? BudgetTopFundingMaintainedSpecial { get; set; }
    public decimal? BudgetTopFundingMaintainedAlternativeProvision { get; set; }
    public decimal? BudgetTopFundingMaintainedPostSchool { get; set; }
    public decimal? BudgetTopFundingMaintainedIncome { get; set; }
    public decimal? BudgetTopFundingNonMaintainedEarlyYears { get; set; }
    public decimal? BudgetTopFundingNonMaintainedPrimary { get; set; }
    public decimal? BudgetTopFundingNonMaintainedSecondary { get; set; }
    public decimal? BudgetTopFundingNonMaintainedSpecial { get; set; }
    public decimal? BudgetTopFundingNonMaintainedAlternativeProvision { get; set; }
    public decimal? BudgetTopFundingNonMaintainedPostSchool { get; set; }
    public decimal? BudgetTopFundingNonMaintainedIncome { get; set; }
    public decimal? BudgetPlaceFundingPrimary { get; set; }
    public decimal? BudgetPlaceFundingSecondary { get; set; }
    public decimal? BudgetPlaceFundingSpecial { get; set; }
    public decimal? BudgetPlaceFundingAlternativeProvision { get; set; }
    public decimal? BudgetSENTransportDSG { get; set; }
    public decimal? BudgetHometoSchoolTransportPre16 { get; set; }
    public decimal? BudgetHometoSchoolTransport1618 { get; set; }
    public decimal? BudgetHometoSchoolTransport1925 { get; set; }
    public decimal? BudgetEdPsychologyService { get; set; }
    public decimal? BudgetSENAdmin { get; set; }

    public static readonly string[] Fields =
    [
        nameof(LaCode),
        nameof(Name),
        nameof(TotalPupils),
        nameof(BudgetTotalHighNeeds),
        nameof(BudgetTotalPlaceFunding),
        nameof(BudgetTotalTopUpFundingMaintained),
        nameof(BudgetTotalTopUpFundingNonMaintained),
        nameof(BudgetTotalSenServices),
        nameof(BudgetTotalAlternativeProvisionServices),
        nameof(BudgetTotalHospitalServices),
        nameof(BudgetTotalOtherHealthServices),
        nameof(BudgetTopFundingMaintainedEarlyYears),
        nameof(BudgetTopFundingMaintainedPrimary),
        nameof(BudgetTopFundingMaintainedSecondary),
        nameof(BudgetTopFundingMaintainedSpecial),
        nameof(BudgetTopFundingMaintainedAlternativeProvision),
        nameof(BudgetTopFundingMaintainedPostSchool),
        nameof(BudgetTopFundingMaintainedIncome),
        nameof(BudgetTopFundingNonMaintainedEarlyYears),
        nameof(BudgetTopFundingNonMaintainedPrimary),
        nameof(BudgetTopFundingNonMaintainedSecondary),
        nameof(BudgetTopFundingNonMaintainedSpecial),
        nameof(BudgetTopFundingNonMaintainedAlternativeProvision),
        nameof(BudgetTopFundingNonMaintainedPostSchool),
        nameof(BudgetTopFundingNonMaintainedIncome),
        nameof(BudgetPlaceFundingPrimary),
        nameof(BudgetPlaceFundingSecondary),
        nameof(BudgetPlaceFundingSpecial),
        nameof(BudgetPlaceFundingAlternativeProvision),
        nameof(BudgetSENTransportDSG),
        nameof(BudgetHometoSchoolTransportPre16),
        nameof(BudgetHometoSchoolTransport1618),
        nameof(BudgetHometoSchoolTransport1925),
        nameof(BudgetEdPsychologyService),
        nameof(BudgetSENAdmin)
    ];
}