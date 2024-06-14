namespace Web.App.Domain.Insight;

public abstract record IncomeBase
{
    public decimal? TotalIncome { get; set; }
    public decimal? TotalGrantFunding { get; set; }
    public decimal? TotalSelfGeneratedFunding { get; set; }
    public decimal? DirectRevenueFinancing { get; set; }
    public decimal? DirectGrants { get; set; }
    public decimal? PrePost16Funding { get; set; }
    public decimal? OtherDfeGrants { get; set; }
    public decimal? OtherIncomeGrants { get; set; }
    public decimal? GovernmentSource { get; set; }
    public decimal? CommunityGrants { get; set; }
    public decimal? Academies { get; set; }
    public decimal? IncomeFacilitiesServices { get; set; }
    public decimal? IncomeCatering { get; set; }
    public decimal? DonationsVoluntaryFunds { get; set; }
    public decimal? ReceiptsSupplyTeacherInsuranceClaims { get; set; }
    public decimal? InvestmentIncome { get; set; }
    public decimal? OtherSelfGeneratedIncome { get; set; }

    public decimal? SchoolTotalIncome { get; set; }
    public decimal? SchoolTotalGrantFunding { get; set; }
    public decimal? SchoolTotalSelfGeneratedFunding { get; set; }
    public decimal? SchoolDirectRevenueFinancing { get; set; }
    public decimal? SchoolDirectGrants { get; set; }
    public decimal? SchoolPrePost16Funding { get; set; }
    public decimal? SchoolOtherDfeGrants { get; set; }
    public decimal? SchoolOtherIncomeGrants { get; set; }
    public decimal? SchoolGovernmentSource { get; set; }
    public decimal? SchoolCommunityGrants { get; set; }
    public decimal? SchoolAcademies { get; set; }
    public decimal? SchoolIncomeFacilitiesServices { get; set; }
    public decimal? SchoolIncomeCatering { get; set; }
    public decimal? SchoolDonationsVoluntaryFunds { get; set; }
    public decimal? SchoolReceiptsSupplyTeacherInsuranceClaims { get; set; }
    public decimal? SchoolInvestmentIncome { get; set; }
    public decimal? SchoolOtherSelfGeneratedIncome { get; set; }

    public decimal? CentralTotalIncome { get; set; }
    public decimal? CentralTotalGrantFunding { get; set; }
    public decimal? CentralTotalSelfGeneratedFunding { get; set; }
    public decimal? CentralDirectRevenueFinancing { get; set; }
    public decimal? CentralDirectGrants { get; set; }
    public decimal? CentralPrePost16Funding { get; set; }
    public decimal? CentralOtherDfeGrants { get; set; }
    public decimal? CentralOtherIncomeGrants { get; set; }
    public decimal? CentralGovernmentSource { get; set; }
    public decimal? CentralCommunityGrants { get; set; }
    public decimal? CentralAcademies { get; set; }
    public decimal? CentralIncomeFacilitiesServices { get; set; }
    public decimal? CentralIncomeCatering { get; set; }
    public decimal? CentralDonationsVoluntaryFunds { get; set; }
    public decimal? CentralReceiptsSupplyTeacherInsuranceClaims { get; set; }
    public decimal? CentralInvestmentIncome { get; set; }
    public decimal? CentralOtherSelfGeneratedIncome { get; set; }
}

public record SchoolIncome : IncomeBase
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
}

public record TrustIncome : IncomeBase
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record IncomeHistory : IncomeBase
{
    public int? Year { get; set; }
    public string? Term { get; set; }
}