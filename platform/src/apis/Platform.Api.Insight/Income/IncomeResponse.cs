using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.Income;

public static class IncomeResponseFactory
{
    public static SchoolIncomeResponse Create(SchoolIncomeModel model, IncomeParameters parameters)
    {
        var response = CreateResponse<SchoolIncomeResponse>(model, parameters);

        response.URN = model.URN;
        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;
        response.TotalPupils = model.TotalPupils;

        return response;
    }

    public static TrustIncomeResponse Create(TrustIncomeModel model, IncomeParameters parameters)
    {
        var response = CreateResponse<TrustIncomeResponse>(model, parameters);

        response.CompanyNumber = model.CompanyNumber;
        response.TrustName = model.TrustName;

        return response;
    }

    public static SchoolIncomeHistoryResponse Create(SchoolIncomeHistoryModel model, IncomeParameters parameters)
    {
        var response = CreateResponse<SchoolIncomeHistoryResponse>(model, parameters);

        response.URN = model.URN;
        response.Year = model.Year;

        return response;
    }

    public static TrustIncomeHistoryResponse Create(TrustIncomeHistoryModel model, IncomeParameters parameters)
    {
        var response = CreateResponse<TrustIncomeHistoryResponse>(model, parameters);

        response.CompanyNumber = model.CompanyNumber;
        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(IncomeBaseModel model, IncomeParameters parameters)
        where T : IncomeBaseResponse, new()
    {
        var response = new T
        {
            SchoolTotalIncome = CalcAmount(model.TotalIncome - model.TotalIncomeCS.GetValueOrDefault(), model, parameters),
            CentralTotalIncome = CalcAmount(model.TotalIncomeCS, model, parameters),
            TotalIncome = CalcTotal(model.TotalIncome, model.TotalIncomeCS.GetValueOrDefault(), model, parameters)
        };

        if (parameters.Category is null or IncomeCategories.GrantFunding)
        {
            SetGrantFunding(model, parameters, response);
        }

        if (parameters.Category is null or IncomeCategories.SelfGenerated)
        {
            SetSelfGenerated(model, parameters, response);
        }

        if (parameters.Category is null or IncomeCategories.DirectRevenueFinancing)
        {
            SetDirectRevenueFinancing(model, parameters, response);
        }

        return response;
    }

    private static void SetDirectRevenueFinancing<T>(IncomeBaseModel model, IncomeParameters parameters, T response) where T : IncomeBaseResponse, new()
    {
        response.SchoolDirectRevenueFinancing = CalcAmount(model.DirectRevenueFinancing - model.DirectRevenueFinancingCS.GetValueOrDefault(), model, parameters);
        response.CentralDirectRevenueFinancing = CalcAmount(model.DirectRevenueFinancingCS, model, parameters);
        response.DirectRevenueFinancing = CalcTotal(model.DirectRevenueFinancing, model.DirectRevenueFinancingCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetSelfGenerated<T>(IncomeBaseModel model, IncomeParameters parameters, T response) where T : IncomeBaseResponse, new()
    {
        response.SchoolTotalSelfGeneratedFunding = CalcAmount(model.TotalSelfGeneratedFunding - model.TotalSelfGeneratedFundingCS.GetValueOrDefault(), model, parameters);
        response.SchoolIncomeFacilitiesServices = CalcAmount(model.IncomeFacilitiesServices - model.IncomeFacilitiesServicesCS.GetValueOrDefault(), model, parameters);
        response.SchoolIncomeCatering = CalcAmount(model.IncomeCateringServices - model.IncomeCateringServicesCS.GetValueOrDefault(), model, parameters);
        response.SchoolDonationsVoluntaryFunds = CalcAmount(model.DonationsVoluntaryFunds - model.DonationsVoluntaryFundsCS.GetValueOrDefault(), model, parameters);
        response.SchoolReceiptsSupplyTeacherInsuranceClaims = CalcAmount(model.ReceiptsSupplyTeacherInsuranceClaimsCS - model.TotalSelfGeneratedFundingCS.GetValueOrDefault(), model, parameters);
        response.SchoolInvestmentIncome = CalcAmount(model.InvestmentIncome - model.InvestmentIncomeCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherSelfGeneratedIncome = CalcAmount(model.OtherSelfGeneratedIncome - model.OtherSelfGeneratedIncomeCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalSelfGeneratedFunding = CalcAmount(model.TotalSelfGeneratedFundingCS, model, parameters);
        response.CentralIncomeFacilitiesServices = CalcAmount(model.IncomeFacilitiesServicesCS, model, parameters);
        response.CentralIncomeCatering = CalcAmount(model.IncomeCateringServicesCS, model, parameters);
        response.CentralDonationsVoluntaryFunds = CalcAmount(model.DonationsVoluntaryFundsCS, model, parameters);
        response.CentralReceiptsSupplyTeacherInsuranceClaims = CalcAmount(model.ReceiptsSupplyTeacherInsuranceClaimsCS, model, parameters);
        response.CentralInvestmentIncome = CalcAmount(model.InvestmentIncomeCS, model, parameters);
        response.CentralOtherSelfGeneratedIncome = CalcAmount(model.OtherSelfGeneratedIncomeCS, model, parameters);

        response.TotalSelfGeneratedFunding = CalcTotal(model.TotalSelfGeneratedFunding, model.TotalSelfGeneratedFundingCS.GetValueOrDefault(), model, parameters);
        response.IncomeFacilitiesServices = CalcTotal(model.IncomeFacilitiesServices, model.IncomeFacilitiesServicesCS.GetValueOrDefault(), model, parameters);
        response.IncomeCatering = CalcTotal(model.IncomeCateringServices, model.IncomeCateringServicesCS.GetValueOrDefault(), model, parameters);
        response.DonationsVoluntaryFunds = CalcTotal(model.DonationsVoluntaryFunds, model.DonationsVoluntaryFundsCS.GetValueOrDefault(), model, parameters);
        response.ReceiptsSupplyTeacherInsuranceClaims = CalcTotal(model.ReceiptsSupplyTeacherInsuranceClaims, model.TotalSelfGeneratedFundingCS.GetValueOrDefault(), model, parameters);
        response.InvestmentIncome = CalcTotal(model.InvestmentIncome, model.InvestmentIncomeCS.GetValueOrDefault(), model, parameters);
        response.OtherSelfGeneratedIncome = CalcTotal(model.OtherSelfGeneratedIncome, model.OtherSelfGeneratedIncomeCS.GetValueOrDefault(), model, parameters);
    }

    private static void SetGrantFunding<T>(IncomeBaseModel model, IncomeParameters parameters, T response) where T : IncomeBaseResponse, new()
    {
        response.SchoolTotalGrantFunding = CalcAmount(model.TotalGrantFunding - model.TotalGrantFundingCS.GetValueOrDefault(), model, parameters);
        response.SchoolDirectGrants = CalcAmount(model.DirectGrants - model.DirectGrantsCS.GetValueOrDefault(), model, parameters);
        response.SchoolPrePost16Funding = CalcAmount(model.PrePost16Funding - model.PrePost16FundingCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherDfeGrants = CalcAmount(model.OtherDfeGrants - model.OtherDfeGrantsCS.GetValueOrDefault(), model, parameters);
        response.SchoolOtherIncomeGrants = CalcAmount(model.OtherIncomeGrants - model.OtherIncomeGrantsCS.GetValueOrDefault(), model, parameters);
        response.SchoolGovernmentSource = CalcAmount(model.GovernmentSource - model.GovernmentSourceCS.GetValueOrDefault(), model, parameters);
        response.SchoolCommunityGrants = CalcAmount(model.CommunityGrants - model.CommunityGrantsCS.GetValueOrDefault(), model, parameters);
        response.SchoolAcademies = CalcAmount(model.Academies - model.AcademiesCS.GetValueOrDefault(), model, parameters);

        response.CentralTotalGrantFunding = CalcAmount(model.TotalGrantFundingCS, model, parameters);
        response.CentralDirectGrants = CalcAmount(model.DirectGrantsCS, model, parameters);
        response.CentralPrePost16Funding = CalcAmount(model.PrePost16FundingCS, model, parameters);
        response.CentralOtherDfeGrants = CalcAmount(model.OtherDfeGrantsCS, model, parameters);
        response.CentralOtherIncomeGrants = CalcAmount(model.OtherIncomeGrantsCS, model, parameters);
        response.CentralGovernmentSource = CalcAmount(model.GovernmentSourceCS, model, parameters);
        response.CentralCommunityGrants = CalcAmount(model.CommunityGrantsCS, model, parameters);
        response.CentralAcademies = CalcAmount(model.AcademiesCS, model, parameters);

        response.TotalGrantFunding = CalcTotal(model.TotalGrantFunding, model.TotalGrantFundingCS.GetValueOrDefault(), model, parameters);
        response.DirectGrants = CalcTotal(model.DirectGrants, model.DirectGrantsCS.GetValueOrDefault(), model, parameters);
        response.PrePost16Funding = CalcTotal(model.PrePost16Funding, model.PrePost16FundingCS.GetValueOrDefault(), model, parameters);
        response.OtherDfeGrants = CalcTotal(model.OtherDfeGrants, model.OtherDfeGrantsCS.GetValueOrDefault(), model, parameters);
        response.OtherIncomeGrants = CalcTotal(model.OtherIncomeGrants, model.OtherIncomeGrantsCS.GetValueOrDefault(), model, parameters);
        response.GovernmentSource = CalcTotal(model.GovernmentSource, model.GovernmentSourceCS.GetValueOrDefault(), model, parameters);
        response.CommunityGrants = CalcTotal(model.CommunityGrants, model.CommunityGrantsCS.GetValueOrDefault(), model, parameters);
        response.Academies = CalcTotal(model.Academies, model.AcademiesCS.GetValueOrDefault(), model, parameters);
    }

    private static decimal? CalcTotal(decimal? value, decimal valueCentral, IncomeBaseModel model, IncomeParameters parameters)
    {
        var totalIncome = model.TotalIncome;
        var totalExpenditure = model.TotalExpenditure;
        if (parameters.ExcludeCentralServices)
        {
            value -= valueCentral;
            totalIncome = model.TotalIncome.GetValueOrDefault() - model.TotalIncomeCS.GetValueOrDefault();
            totalExpenditure = model.TotalExpenditure.GetValueOrDefault() - model.TotalExpenditureCS.GetValueOrDefault();
        }

        return CalculateValue(value, model.TotalPupils, totalIncome, totalExpenditure, parameters.Dimension);
    }

    private static decimal? CalcAmount(decimal? value, IncomeBaseModel model, IncomeParameters parameters) => parameters.ExcludeCentralServices
        ? null
        : CalculateValue(value, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, parameters.Dimension);

    private static decimal? CalculateValue(decimal? value, decimal? totalUnit, decimal? totalIncome,
        decimal? totalExpenditure, string dimension)
    {
        if (value == null)
        {
            return value;
        }

        return dimension switch
        {
            IncomeDimensions.Actuals => value,
            IncomeDimensions.PerUnit => totalUnit != 0 ? value / totalUnit : 0,
            IncomeDimensions.PercentIncome => totalIncome != 0 ? value / totalIncome * 100 : 0,
            IncomeDimensions.PercentExpenditure => totalExpenditure != 0 ? value / totalExpenditure * 100 : 0,
            _ => null
        };
    }
}

[ExcludeFromCodeCoverage]
public abstract record IncomeBaseResponse
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

[ExcludeFromCodeCoverage]
public record SchoolIncomeResponse : IncomeBaseResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
}

[ExcludeFromCodeCoverage]
public record TrustIncomeResponse : IncomeBaseResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

[ExcludeFromCodeCoverage]
public record SchoolIncomeHistoryResponse : IncomeBaseResponse
{
    public string? URN { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}

[ExcludeFromCodeCoverage]
public record TrustIncomeHistoryResponse : IncomeBaseResponse
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}