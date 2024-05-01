namespace Web.App.Domain;

public record Income
{
    public int YearEnd { get; set; }
    public string? Term { get; set; }
    public string? Dimension { get; set; }
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
}