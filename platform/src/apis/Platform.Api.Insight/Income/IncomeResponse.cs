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

        var schoolTotalIncome = CalcSchool(model.TotalIncome, model, parameters.Dimension);
        var centralTotalIncome = CalcCentral(model.TotalIncomeCS, model, parameters.Dimension);
        var totalIncome = model.TotalIncome.GetValueOrDefault() + model.TotalIncomeCS.GetValueOrDefault();

        var response = new T
        {
            SchoolTotalIncome = parameters.IncludeBreakdown ? schoolTotalIncome : null,
            CentralTotalIncome = parameters.IncludeBreakdown ? centralTotalIncome : null,
            TotalIncome = CalcTotal(totalIncome, model, parameters.Dimension)
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

    private static void SetDirectRevenueFinancing<T>(IncomeBaseModel model, IncomeParameters parameters, T response)
        where T : IncomeBaseResponse, new()
    {
        var schoolDirectRevenueFinancing = CalcSchool(model.DirectRevenueFinancing, model, parameters.Dimension);
        var centralDirectRevenueFinancing = CalcCentral(model.DirectRevenueFinancingCS, model, parameters.Dimension);
        var directRevenueFinancing = model.DirectRevenueFinancing.GetValueOrDefault() + model.DirectRevenueFinancingCS.GetValueOrDefault();

        response.SchoolDirectRevenueFinancing = parameters.IncludeBreakdown ? schoolDirectRevenueFinancing : null;
        response.CentralDirectRevenueFinancing = parameters.IncludeBreakdown ? centralDirectRevenueFinancing : null;
        response.DirectRevenueFinancing = CalcTotal(directRevenueFinancing, model, parameters.Dimension);
    }

    private static void SetSelfGenerated<T>(IncomeBaseModel model, IncomeParameters parameters, T response)
        where T : IncomeBaseResponse, new()
    {
        var schoolTotalSelfGeneratedFunding = CalcSchool(model.TotalSelfGeneratedFunding, model, parameters.Dimension);
        var schoolIncomeFacilitiesServices = CalcSchool(model.IncomeFacilitiesServices, model, parameters.Dimension);
        var schoolIncomeCatering = CalcSchool(model.IncomeCateringServices, model, parameters.Dimension);
        var schoolDonationsVoluntaryFunds = CalcSchool(model.DonationsVoluntaryFunds, model, parameters.Dimension);
        var schoolReceiptsSupplyTeacherInsuranceClaims = CalcSchool(model.ReceiptsSupplyTeacherInsuranceClaims, model, parameters.Dimension);
        var schoolInvestmentIncome = CalcSchool(model.InvestmentIncome, model, parameters.Dimension);
        var schoolOtherSelfGeneratedIncome = CalcSchool(model.OtherSelfGeneratedIncome, model, parameters.Dimension);

        var centralTotalSelfGeneratedFunding = CalcCentral(model.TotalSelfGeneratedFundingCS, model, parameters.Dimension);
        var centralIncomeFacilitiesServices = CalcCentral(model.IncomeFacilitiesServicesCS, model, parameters.Dimension);
        var centralIncomeCatering = CalcCentral(model.IncomeCateringServicesCS, model, parameters.Dimension);
        var centralDonationsVoluntaryFunds = CalcCentral(model.DonationsVoluntaryFundsCS, model, parameters.Dimension);
        var centralReceiptsSupplyTeacherInsuranceClaims = CalcCentral(model.ReceiptsSupplyTeacherInsuranceClaimsCS, model, parameters.Dimension);
        var centralInvestmentIncome = CalcCentral(model.InvestmentIncomeCS, model, parameters.Dimension);
        var centralOtherSelfGeneratedIncome = CalcCentral(model.OtherSelfGeneratedIncomeCS, model, parameters.Dimension);

        var totalSelfGeneratedFunding = model.TotalSelfGeneratedFunding.GetValueOrDefault() + model.TotalSelfGeneratedFundingCS.GetValueOrDefault();
        var incomeFacilitiesServices = model.IncomeFacilitiesServices.GetValueOrDefault() + model.IncomeFacilitiesServicesCS.GetValueOrDefault();
        var incomeCatering = model.IncomeCateringServices.GetValueOrDefault() + model.IncomeCateringServicesCS.GetValueOrDefault();
        var donationsVoluntaryFunds = model.DonationsVoluntaryFunds.GetValueOrDefault() + model.DonationsVoluntaryFundsCS.GetValueOrDefault();
        var receiptsSupplyTeacherInsuranceClaims = model.ReceiptsSupplyTeacherInsuranceClaims.GetValueOrDefault() + model.ReceiptsSupplyTeacherInsuranceClaimsCS.GetValueOrDefault();
        var investmentIncome = model.InvestmentIncome.GetValueOrDefault() + model.InvestmentIncomeCS.GetValueOrDefault();
        var otherSelfGeneratedIncome = model.OtherSelfGeneratedIncome.GetValueOrDefault() + model.OtherSelfGeneratedIncomeCS.GetValueOrDefault();

        response.SchoolTotalSelfGeneratedFunding = parameters.IncludeBreakdown ? schoolTotalSelfGeneratedFunding : null;
        response.SchoolIncomeFacilitiesServices = parameters.IncludeBreakdown ? schoolIncomeFacilitiesServices : null;
        response.SchoolIncomeCatering = parameters.IncludeBreakdown ? schoolIncomeCatering : null;
        response.SchoolDonationsVoluntaryFunds = parameters.IncludeBreakdown ? schoolDonationsVoluntaryFunds : null;
        response.SchoolReceiptsSupplyTeacherInsuranceClaims = parameters.IncludeBreakdown ? schoolReceiptsSupplyTeacherInsuranceClaims : null;
        response.SchoolInvestmentIncome = parameters.IncludeBreakdown ? schoolInvestmentIncome : null;
        response.SchoolOtherSelfGeneratedIncome = parameters.IncludeBreakdown ? schoolOtherSelfGeneratedIncome : null;

        response.CentralTotalSelfGeneratedFunding = parameters.IncludeBreakdown ? centralTotalSelfGeneratedFunding : null;
        response.CentralIncomeFacilitiesServices = parameters.IncludeBreakdown ? centralIncomeFacilitiesServices : null;
        response.CentralIncomeCatering = parameters.IncludeBreakdown ? centralIncomeCatering : null;
        response.CentralDonationsVoluntaryFunds = parameters.IncludeBreakdown ? centralDonationsVoluntaryFunds : null;
        response.CentralReceiptsSupplyTeacherInsuranceClaims = parameters.IncludeBreakdown ? centralReceiptsSupplyTeacherInsuranceClaims : null;
        response.CentralInvestmentIncome = parameters.IncludeBreakdown ? centralInvestmentIncome : null;
        response.CentralOtherSelfGeneratedIncome = parameters.IncludeBreakdown ? centralOtherSelfGeneratedIncome : null;

        response.TotalSelfGeneratedFunding = CalcTotal(totalSelfGeneratedFunding, model, parameters.Dimension);
        response.IncomeFacilitiesServices = CalcTotal(incomeFacilitiesServices, model, parameters.Dimension);
        response.IncomeCatering = CalcTotal(incomeCatering, model, parameters.Dimension);
        response.DonationsVoluntaryFunds = CalcTotal(donationsVoluntaryFunds, model, parameters.Dimension);
        response.ReceiptsSupplyTeacherInsuranceClaims = CalcTotal(receiptsSupplyTeacherInsuranceClaims, model, parameters.Dimension);
        response.InvestmentIncome = CalcTotal(investmentIncome, model, parameters.Dimension);
        response.OtherSelfGeneratedIncome = CalcTotal(otherSelfGeneratedIncome, model, parameters.Dimension);
    }

    private static void SetGrantFunding<T>(IncomeBaseModel model, IncomeParameters parameters, T response)
        where T : IncomeBaseResponse, new()
    {
        var schoolTotalGrantFunding = CalcSchool(model.TotalGrantFunding, model, parameters.Dimension);
        var schoolDirectGrants = CalcSchool(model.DirectGrants, model, parameters.Dimension);
        var schoolPrePost16Funding = CalcSchool(model.PrePost16Funding, model, parameters.Dimension);
        var schoolOtherDfeGrants = CalcSchool(model.OtherDfeGrants, model, parameters.Dimension);
        var schoolOtherIncomeGrants = CalcSchool(model.OtherIncomeGrants, model, parameters.Dimension);
        var schoolGovernmentSource = CalcSchool(model.GovernmentSource, model, parameters.Dimension);
        var schoolCommunityGrants = CalcSchool(model.CommunityGrants, model, parameters.Dimension);
        var schoolAcademies = CalcSchool(model.Academies, model, parameters.Dimension);

        var centralTotalGrantFunding = CalcCentral(model.TotalGrantFundingCS, model, parameters.Dimension);
        var centralDirectGrants = CalcCentral(model.DirectGrantsCS, model, parameters.Dimension);
        var centralPrePost16Funding = CalcCentral(model.PrePost16FundingCS, model, parameters.Dimension);
        var centralOtherDfeGrants = CalcCentral(model.OtherDfeGrantsCS, model, parameters.Dimension);
        var centralOtherIncomeGrants = CalcCentral(model.OtherIncomeGrantsCS, model, parameters.Dimension);
        var centralGovernmentSource = CalcCentral(model.GovernmentSourceCS, model, parameters.Dimension);
        var centralCommunityGrants = CalcCentral(model.CommunityGrantsCS, model, parameters.Dimension);
        var centralAcademies = CalcCentral(model.AcademiesCS, model, parameters.Dimension);

        var totalGrantFunding = model.TotalGrantFunding.GetValueOrDefault() + model.TotalGrantFundingCS.GetValueOrDefault();
        var directGrants = model.DirectGrants.GetValueOrDefault() + model.DirectGrantsCS.GetValueOrDefault();
        var prePost16Funding = model.PrePost16Funding.GetValueOrDefault() + model.PrePost16FundingCS.GetValueOrDefault();
        var otherDfeGrants = model.OtherDfeGrants.GetValueOrDefault() + model.OtherDfeGrantsCS.GetValueOrDefault();
        var otherIncomeGrants = model.OtherIncomeGrants.GetValueOrDefault() + model.OtherIncomeGrantsCS.GetValueOrDefault();
        var governmentSource = model.GovernmentSource.GetValueOrDefault() + model.GovernmentSourceCS.GetValueOrDefault();
        var communityGrants = model.CommunityGrants.GetValueOrDefault() + model.CommunityGrantsCS.GetValueOrDefault();
        var academies = model.Academies.GetValueOrDefault() + model.AcademiesCS.GetValueOrDefault();

        response.SchoolTotalGrantFunding = parameters.IncludeBreakdown ? schoolTotalGrantFunding : null;
        response.SchoolDirectGrants = parameters.IncludeBreakdown ? schoolDirectGrants : null;
        response.SchoolPrePost16Funding = parameters.IncludeBreakdown ? schoolPrePost16Funding : null;
        response.SchoolOtherDfeGrants = parameters.IncludeBreakdown ? schoolOtherDfeGrants : null;
        response.SchoolOtherIncomeGrants = parameters.IncludeBreakdown ? schoolOtherIncomeGrants : null;
        response.SchoolGovernmentSource = parameters.IncludeBreakdown ? schoolGovernmentSource : null;
        response.SchoolCommunityGrants = parameters.IncludeBreakdown ? schoolCommunityGrants : null;
        response.SchoolAcademies = parameters.IncludeBreakdown ? schoolAcademies : null;

        response.CentralTotalGrantFunding = parameters.IncludeBreakdown ? centralTotalGrantFunding : null;
        response.CentralDirectGrants = parameters.IncludeBreakdown ? centralDirectGrants : null;
        response.CentralPrePost16Funding = parameters.IncludeBreakdown ? centralPrePost16Funding : null;
        response.CentralOtherDfeGrants = parameters.IncludeBreakdown ? centralOtherDfeGrants : null;
        response.CentralOtherIncomeGrants = parameters.IncludeBreakdown ? centralOtherIncomeGrants : null;
        response.CentralGovernmentSource = parameters.IncludeBreakdown ? centralGovernmentSource : null;
        response.CentralCommunityGrants = parameters.IncludeBreakdown ? centralCommunityGrants : null;
        response.CentralAcademies = parameters.IncludeBreakdown ? centralAcademies : null;

        response.TotalGrantFunding = CalcTotal(totalGrantFunding, model, parameters.Dimension);
        response.DirectGrants = CalcTotal(directGrants, model, parameters.Dimension);
        response.PrePost16Funding = CalcTotal(prePost16Funding, model, parameters.Dimension);
        response.OtherDfeGrants = CalcTotal(otherDfeGrants, model, parameters.Dimension);
        response.OtherIncomeGrants = CalcTotal(otherIncomeGrants, model, parameters.Dimension);
        response.GovernmentSource = CalcTotal(governmentSource, model, parameters.Dimension);
        response.CommunityGrants = CalcTotal(communityGrants, model, parameters.Dimension);
        response.Academies = CalcTotal(academies, model, parameters.Dimension);
    }

    private static decimal? CalculateTotal(decimal? school, decimal? central, string dimension)
    {
        return dimension switch
        {
            IncomeDimensions.Actuals => school.GetValueOrDefault() + central.GetValueOrDefault(),
            IncomeDimensions.PerUnit => school.GetValueOrDefault() + central.GetValueOrDefault(),
            IncomeDimensions.PercentIncome => (school.GetValueOrDefault() + central.GetValueOrDefault()) / 2,
            IncomeDimensions.PercentExpenditure => (school.GetValueOrDefault() + central.GetValueOrDefault()) / 2,
            _ => null
        };
    }

    private static decimal? CalcTotal(decimal value, IncomeBaseModel model, string dimension)
    {
        var totalIncome = model.TotalIncome.GetValueOrDefault() + model.TotalIncomeCS.GetValueOrDefault();
        var totalExpenditure = model.TotalExpenditure.GetValueOrDefault() + model.TotalExpenditureCS.GetValueOrDefault();

        return CalculateValue(value, model.TotalPupils, totalIncome, totalExpenditure, dimension);
    }

    private static decimal? CalcSchool(decimal? value, IncomeBaseModel model, string dimension)
    {
        return CalculateValue(value, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);
    }

    private static decimal? CalcCentral(decimal? value, IncomeBaseModel model, string dimension)
    {
        return CalculateValue(value, model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);
    }

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

public record SchoolIncomeResponse : IncomeBaseResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
}

public record TrustIncomeResponse : IncomeBaseResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record SchoolIncomeHistoryResponse : IncomeBaseResponse
{
    public string? URN { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}

public record TrustIncomeHistoryResponse : IncomeBaseResponse
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}