using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Insight.Income;

[ExcludeFromCodeCoverage]
public record IncomeYearsModel
{
    public int StartYear { get; set; }
    public int EndYear { get; set; }
}

[ExcludeFromCodeCoverage]
public abstract record IncomeModel
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
    public decimal? IncomeCateringServices { get; set; }
    public decimal? DonationsVoluntaryFunds { get; set; }
    public decimal? ReceiptsSupplyTeacherInsuranceClaims { get; set; }
    public decimal? InvestmentIncome { get; set; }
    public decimal? OtherSelfGeneratedIncome { get; set; }
}

[ExcludeFromCodeCoverage]
public record IncomeSchoolModel : IncomeModel
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public int? PeriodCoveredByReturn { get; set; }
}

[ExcludeFromCodeCoverage]
public record IncomeHistoryModel : IncomeModel
{
    public int? RunId { get; set; }
}