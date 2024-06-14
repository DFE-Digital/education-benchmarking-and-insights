namespace Platform.Api.Insight.Income;

public abstract record IncomeBaseModel
{
    public decimal? TotalPupils { get; set; }
    public decimal? TotalIncome { get; set; }
    public decimal? TotalExpenditure { get; set; }
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
    public decimal? IncomeCateringServices { get; set; }
    public decimal? DonationsVoluntaryFunds { get; set; }
    public decimal? ReceiptsSupplyTeacherInsuranceClaims { get; set; }
    public decimal? InvestmentIncome { get; set; }
    public decimal? OtherSelfGeneratedIncome { get; set; }

    public decimal? TotalIncomeCS { get; set; }
    public decimal? TotalExpenditureCS { get; set; }
    public decimal? TotalGrantFundingCS { get; set; }
    public decimal? TotalSelfGeneratedFundingCS { get; set; }
    public decimal? DirectRevenueFinancingCS { get; set; }
    public decimal? DirectGrantsCS { get; set; }
    public decimal? PrePost16FundingCS { get; set; }
    public decimal? OtherDfeGrantsCS { get; set; }
    public decimal? OtherIncomeGrantsCS { get; set; }
    public decimal? GovernmentSourceCS { get; set; }
    public decimal? CommunityGrantsCS { get; set; }
    public decimal? AcademiesCS { get; set; }
    public decimal? IncomeFacilitiesServicesCS { get; set; }
    public decimal? IncomeCateringServicesCS { get; set; }
    public decimal? DonationsVoluntaryFundsCS { get; set; }
    public decimal? ReceiptsSupplyTeacherInsuranceClaimsCS { get; set; }
    public decimal? InvestmentIncomeCS { get; set; }
    public decimal? OtherSelfGeneratedIncomeCS { get; set; }
}

public record SchoolIncomeModel : IncomeBaseModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
}

public record SchoolIncomeHistoryModel : IncomeBaseModel
{
    public string? URN { get; set; }
    public int? Year { get; set; }
}

public record TrustIncomeModel : IncomeBaseModel
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record TrustIncomeHistoryModel : IncomeBaseModel
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
}