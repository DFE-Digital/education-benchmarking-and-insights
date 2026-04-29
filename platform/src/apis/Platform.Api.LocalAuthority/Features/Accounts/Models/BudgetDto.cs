using System.Diagnostics.CodeAnalysis;
// ReSharper disable InconsistentNaming
// ReSharper disable ClassNeverInstantiated.Global

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record BudgetDto
{
    public string? LaCode { get; set; }
    public string? TotalPupils { get; set; }
    public string? BudgetTotalHighNeeds { get; set; }
    public string? BudgetTotalPlaceFunding { get; set; }
    public string? BudgetTotalTopUpFundingMaintained { get; set; }
    public string? BudgetTotalTopUpFundingNonMaintained { get; set; }
    public string? BudgetTotalSenServices { get; set; }
    public string? BudgetTotalAlternativeProvisionServices { get; set; }
    public string? BudgetTotalHospitalServices { get; set; }
    public string? BudgetTotalOtherHealthServices { get; set; }
    public string? BudgetTopFundingMaintainedEarlyYears { get; set; }
    public string? BudgetTopFundingMaintainedPrimary { get; set; }
    public string? BudgetTopFundingMaintainedSecondary { get; set; }
    public string? BudgetTopFundingMaintainedSpecial { get; set; }
    public string? BudgetTopFundingMaintainedAlternativeProvision { get; set; }
    public string? BudgetTopFundingMaintainedPostSchool { get; set; }
    public string? BudgetTopFundingMaintainedIncome { get; set; }
    public string? BudgetTopFundingNonMaintainedEarlyYears { get; set; }
    public string? BudgetTopFundingNonMaintainedPrimary { get; set; }
    public string? BudgetTopFundingNonMaintainedSecondary { get; set; }
    public string? BudgetTopFundingNonMaintainedSpecial { get; set; }
    public string? BudgetTopFundingNonMaintainedAlternativeProvision { get; set; }
    public string? BudgetTopFundingNonMaintainedPostSchool { get; set; }
    public string? BudgetTopFundingNonMaintainedIncome { get; set; }
    public string? BudgetPlaceFundingPrimary { get; set; }
    public string? BudgetPlaceFundingSecondary { get; set; }
    public string? BudgetPlaceFundingSpecial { get; set; }
    public string? BudgetPlaceFundingAlternativeProvision { get; set; }
    public string? BudgetSENTransport { get; set; }
    public string? BudgetHometoSchoolTransportPre16 { get; set; }
    public string? BudgetHometoSchoolTransport1618 { get; set; }
    public string? BudgetHometoSchoolTransport1925 { get; set; }
    public string? BudgetEdPsychologyService { get; set; }
    public string? BudgetSENAdmin { get; set; }

    public static readonly string[] Fields =
    [
        nameof(LaCode),
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
        nameof(BudgetSENTransport),
        nameof(BudgetHometoSchoolTransportPre16),
        nameof(BudgetHometoSchoolTransport1618),
        nameof(BudgetHometoSchoolTransport1925),
        nameof(BudgetEdPsychologyService),
        nameof(BudgetSENAdmin)
    ];
}