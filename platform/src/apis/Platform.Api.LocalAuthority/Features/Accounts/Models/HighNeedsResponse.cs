using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.LocalAuthority.Features.Accounts.Models;

[ExcludeFromCodeCoverage]
public record HighNeedsResponse
{
    public string? Code { get; set; }
    public string? TotalPupils { get; set; }
    public string? TotalHighNeeds { get; set; }
    public string? TotalPlaceFunding { get; set; }
    public string? TotalTopUpFundingMaintained { get; set; }
    public string? TotalTopUpFundingNonMaintained { get; set; }
    public string? TotalSenServices { get; set; }
    public string? TotalAlternativeProvisionServices { get; set; }
    public string? TotalHospitalServices { get; set; }
    public string? TotalOtherHealthServices { get; set; }
    public string? TopFundingMaintainedEarlyYears { get; set; }
    public string? TopFundingMaintainedPrimary { get; set; }
    public string? TopFundingMaintainedSecondary { get; set; }
    public string? TopFundingMaintainedSpecial { get; set; }
    public string? TopFundingMaintainedAlternativeProvision { get; set; }
    public string? TopFundingMaintainedPostSchool { get; set; }
    public string? TopFundingMaintainedIncome { get; set; }
    public string? TopFundingNonMaintainedEarlyYears { get; set; }
    public string? TopFundingNonMaintainedPrimary { get; set; }
    public string? TopFundingNonMaintainedSecondary { get; set; }
    public string? TopFundingNonMaintainedSpecial { get; set; }
    public string? TopFundingNonMaintainedAlternativeProvision { get; set; }
    public string? TopFundingNonMaintainedPostSchool { get; set; }
    public string? TopFundingNonMaintainedIncome { get; set; }
    public string? PlaceFundingPrimary { get; set; }
    public string? PlaceFundingSecondary { get; set; }
    public string? PlaceFundingSpecial { get; set; }
    public string? PlaceFundingAlternativeProvision { get; set; }
    public string? SenTransportDsg { get; set; }
    public string? HometoSchoolTransportPre16 { get; set; }
    public string? HometoSchoolTransport1618 { get; set; }
    public string? HometoSchoolTransport1925 { get; set; }
    public string? EdPsychologyService { get; set; }
    public string? SenAdmin { get; set; }
}