// ReSharper disable ClassNeverInstantiated.Global
namespace Web.App.Domain.LocalAuthorities;

public record HighNeedsSpending
{
    public string? Code { get; set; }
    public string? Name { get; set; }
    public decimal? TotalPupils { get; set; }
    public decimal? TotalPlaceFunding { get; set; }
    public decimal? TotalTopUpFundingMaintained { get; set; }
    public decimal? TotalTopUpFundingNonMaintained { get; set; }
    public decimal? TotalSenServices { get; set; }
    public decimal? TotalAlternativeProvisionServices { get; set; }
    public decimal? TotalHospitalServices { get; set; }
    public decimal? TotalOtherHealthServices { get; set; }
    public decimal? TopFundingMaintainedEarlyYears { get; set; }
    public decimal? TopFundingMaintainedPrimary { get; set; }
    public decimal? TopFundingMaintainedSecondary { get; set; }
    public decimal? TopFundingMaintainedSpecial { get; set; }
    public decimal? TopFundingMaintainedAlternativeProvision { get; set; }
    public decimal? TopFundingMaintainedPostSchool { get; set; }
    public decimal? TopFundingMaintainedIncome { get; set; }
    public decimal? TopFundingNonMaintainedEarlyYears { get; set; }
    public decimal? TopFundingNonMaintainedPrimary { get; set; }
    public decimal? TopFundingNonMaintainedSecondary { get; set; }
    public decimal? TopFundingNonMaintainedSpecial { get; set; }
    public decimal? TopFundingNonMaintainedAlternativeProvision { get; set; }
    public decimal? TopFundingNonMaintainedPostSchool { get; set; }
    public decimal? TopFundingNonMaintainedIncome { get; set; }
    public decimal? PlaceFundingPrimary { get; set; }
    public decimal? PlaceFundingSecondary { get; set; }
    public decimal? PlaceFundingSpecial { get; set; }
    public decimal? PlaceFundingAlternativeProvision { get; set; }
    public decimal? SenTransportDsg { get; set; }
    public decimal? HometoSchoolTransportPre16 { get; set; }
    public decimal? HometoSchoolTransport1618 { get; set; }
    public decimal? HometoSchoolTransport1925 { get; set; }
    public decimal? EdPsychologyService { get; set; }
    public decimal? SenAdmin { get; set; }
}
