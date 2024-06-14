namespace Platform.Api.Insight.Income;

public static class IncomeResponseFactory
{
    public static SchoolIncomeResponse Create(SchoolIncomeModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<SchoolIncomeResponse>(model, dimension, category);

        response.URN = model.URN;
        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;
        response.TotalPupils = model.TotalPupils;

        return response;
    }

    public static TrustIncomeResponse Create(TrustIncomeModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<TrustIncomeResponse>(model, dimension, category);

        response.CompanyNumber = model.CompanyNumber;
        response.TrustName = model.TrustName;

        return response;
    }

    public static SchoolIncomeHistoryResponse Create(SchoolIncomeHistoryModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<SchoolIncomeHistoryResponse>(model, dimension, category);

        response.URN = model.URN;
        response.Year = model.Year;

        return response;
    }

    public static TrustIncomeHistoryResponse Create(TrustIncomeHistoryModel model, string dimension,
        string? category = null)
    {
        var response = CreateResponse<TrustIncomeHistoryResponse>(model, dimension, category);

        response.CompanyNumber = model.CompanyNumber;
        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(IncomeBaseModel model, string dimension, string? category)
        where T : IncomeBaseResponse, new()
    {
        var response = new T();

        response.SchoolTotalIncome = CalculateValue(model.TotalIncome,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.CentralTotalIncome = CalculateValue(model.TotalIncomeCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.TotalIncome =
            CalculateTotal(response.SchoolTotalIncome, response.CentralTotalIncome, dimension);

        if (category is null or IncomeCategories.GrantFunding)
        {
            SetGrantFunding(model, dimension, response);
        }

        if (category is null or IncomeCategories.SelfGenerated)
        {
            SetSelfGenerated(model, dimension, response);
        }

        if (category is null or IncomeCategories.DirectRevenueFinancing)
        {
            SetDirectRevenueFinancing(model, dimension, response);
        }

        return response;
    }

    private static void SetDirectRevenueFinancing<T>(IncomeBaseModel model, string dimension, T response)
        where T : IncomeBaseResponse, new()
    {
        response.SchoolDirectRevenueFinancing = CalculateValue(model.DirectRevenueFinancing,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.CentralDirectRevenueFinancing = CalculateValue(model.DirectRevenueFinancingCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.DirectRevenueFinancing = CalculateTotal(response.SchoolDirectRevenueFinancing,
            response.CentralDirectRevenueFinancing, dimension);
    }

    private static void SetSelfGenerated<T>(IncomeBaseModel model, string dimension, T response)
        where T : IncomeBaseResponse, new()
    {
        response.SchoolTotalSelfGeneratedFunding = CalculateValue(model.TotalSelfGeneratedFunding,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolIncomeFacilitiesServices = CalculateValue(model.IncomeFacilitiesServices, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolIncomeCatering = CalculateValue(model.IncomeCateringServices, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolDonationsVoluntaryFunds = CalculateValue(model.DonationsVoluntaryFunds,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolReceiptsSupplyTeacherInsuranceClaims = CalculateValue(model.ReceiptsSupplyTeacherInsuranceClaims,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolInvestmentIncome = CalculateValue(model.InvestmentIncome,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolOtherSelfGeneratedIncome = CalculateValue(model.OtherSelfGeneratedIncome,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalSelfGeneratedFunding = CalculateValue(model.TotalSelfGeneratedFundingCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralIncomeFacilitiesServices = CalculateValue(model.IncomeFacilitiesServicesCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralIncomeCatering = CalculateValue(model.IncomeCateringServicesCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralDonationsVoluntaryFunds = CalculateValue(model.DonationsVoluntaryFundsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralReceiptsSupplyTeacherInsuranceClaims = CalculateValue(model.ReceiptsSupplyTeacherInsuranceClaimsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralInvestmentIncome = CalculateValue(model.InvestmentIncomeCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralOtherSelfGeneratedIncome = CalculateValue(model.OtherSelfGeneratedIncomeCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalSelfGeneratedFunding = CalculateTotal(response.SchoolTotalSelfGeneratedFunding,
            response.CentralTotalSelfGeneratedFunding, dimension);
        response.IncomeFacilitiesServices =
            CalculateTotal(response.SchoolIncomeFacilitiesServices, response.CentralIncomeFacilitiesServices, dimension);
        response.IncomeCatering = CalculateTotal(response.SchoolIncomeCatering,
            response.CentralIncomeCatering, dimension);
        response.DonationsVoluntaryFunds = CalculateTotal(response.SchoolDonationsVoluntaryFunds,
            response.CentralDonationsVoluntaryFunds, dimension);
        response.ReceiptsSupplyTeacherInsuranceClaims = CalculateTotal(response.SchoolReceiptsSupplyTeacherInsuranceClaims,
            response.CentralReceiptsSupplyTeacherInsuranceClaims, dimension);
        response.InvestmentIncome = CalculateTotal(response.SchoolInvestmentIncome,
            response.CentralInvestmentIncome, dimension);
        response.OtherSelfGeneratedIncome = CalculateTotal(response.SchoolOtherSelfGeneratedIncome,
            response.CentralOtherSelfGeneratedIncome, dimension);
    }

    private static void SetGrantFunding<T>(IncomeBaseModel model, string dimension, T response)
        where T : IncomeBaseResponse, new()
    {
        response.SchoolTotalGrantFunding = CalculateValue(model.TotalGrantFunding,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolDirectGrants = CalculateValue(model.DirectGrants, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolPrePost16Funding = CalculateValue(model.PrePost16Funding, model.TotalPupils,
            model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolOtherDfeGrants = CalculateValue(model.OtherDfeGrants,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolOtherIncomeGrants = CalculateValue(model.OtherIncomeGrants,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolGovernmentSource = CalculateValue(model.GovernmentSource,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolCommunityGrants = CalculateValue(model.CommunityGrants,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);

        response.SchoolAcademies = CalculateValue(model.Academies,
            model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension);


        response.CentralTotalGrantFunding = CalculateValue(model.TotalGrantFundingCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralDirectGrants = CalculateValue(model.DirectGrantsCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralPrePost16Funding = CalculateValue(model.PrePost16FundingCS, model.TotalPupils,
            model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralOtherDfeGrants = CalculateValue(model.OtherDfeGrantsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralOtherIncomeGrants = CalculateValue(model.OtherIncomeGrantsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralGovernmentSource = CalculateValue(model.GovernmentSourceCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralCommunityGrants = CalculateValue(model.CommunityGrantsCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);

        response.CentralAcademies = CalculateValue(model.AcademiesCS,
            model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension);


        response.TotalGrantFunding = CalculateTotal(response.SchoolTotalGrantFunding,
            response.CentralTotalGrantFunding, dimension);
        response.DirectGrants =
            CalculateTotal(response.SchoolDirectGrants, response.CentralDirectGrants, dimension);
        response.PrePost16Funding = CalculateTotal(response.SchoolPrePost16Funding,
            response.CentralPrePost16Funding, dimension);
        response.OtherDfeGrants = CalculateTotal(response.SchoolOtherDfeGrants,
            response.CentralOtherDfeGrants, dimension);
        response.OtherIncomeGrants = CalculateTotal(response.SchoolOtherIncomeGrants,
            response.CentralOtherIncomeGrants, dimension);
        response.GovernmentSource = CalculateTotal(response.SchoolGovernmentSource,
            response.CentralGovernmentSource, dimension);
        response.CommunityGrants = CalculateTotal(response.SchoolCommunityGrants,
            response.CentralCommunityGrants, dimension);
        response.Academies = CalculateTotal(response.SchoolAcademies,
            response.CentralAcademies, dimension);
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