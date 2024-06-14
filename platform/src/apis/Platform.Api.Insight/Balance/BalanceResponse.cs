namespace Platform.Api.Insight.Balance;

public static class BalanceResponseFactory
{
    public static SchoolBalanceResponse Create(SchoolBalanceModel model, string dimension)
    {
        var response = CreateResponse<SchoolBalanceResponse>(model, dimension);

        response.URN = model.URN;
        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;
        response.TotalPupils = model.TotalPupils;

        return response;
    }

    public static TrustBalanceResponse Create(TrustBalanceModel model, string dimension)
    {
        var response = CreateResponse<TrustBalanceResponse>(model, dimension);

        response.CompanyNumber = model.CompanyNumber;
        response.TrustName = model.TrustName;

        return response;
    }

    public static SchoolBalanceHistoryResponse Create(SchoolBalanceHistoryModel model, string dimension)
    {
        var response = CreateResponse<SchoolBalanceHistoryResponse>(model, dimension);

        response.URN = model.URN;
        response.Year = model.Year;

        return response;
    }

    public static TrustBalanceHistoryResponse Create(TrustBalanceHistoryModel model, string dimension)
    {
        var response = CreateResponse<TrustBalanceHistoryResponse>(model, dimension);

        response.CompanyNumber = model.CompanyNumber;
        response.Year = model.Year;

        return response;
    }


    private static T CreateResponse<T>(BalanceBaseModel model, string dimension)
        where T : BalanceBaseResponse, new()
    {
        var response = new T
        {
            SchoolInYearBalance = CalculateValue(model.InYearBalance, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension),
            CentralInYearBalance = CalculateValue(model.InYearBalanceCS, model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension),
            SchoolRevenueReserve = CalculateValue(model.RevenueReserve, model.TotalPupils, model.TotalIncome, model.TotalExpenditure, dimension),
            CentralRevenueReserve = CalculateValue(model.RevenueReserveCS, model.TotalPupils, model.TotalIncomeCS, model.TotalExpenditureCS, dimension)
        };

        return response;
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
            BalanceDimensions.Actuals => value,
            BalanceDimensions.PerUnit => totalUnit != 0 ? value / totalUnit : 0,
            BalanceDimensions.PercentIncome => totalIncome != 0 ? value / totalIncome * 100 : 0,
            BalanceDimensions.PercentExpenditure => totalExpenditure != 0 ? value / totalExpenditure * 100 : 0,
            _ => null
        };
    }
}

public abstract record BalanceBaseResponse
{
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
    public decimal? SchoolRevenueReserve { get; set; }
    public decimal? CentralRevenueReserve { get; set; }


    public decimal? TotalInYearBalance => CalculateTotal(SchoolInYearBalance, CentralInYearBalance);

    public decimal? TotalRevenueReserve => CalculateTotal(SchoolRevenueReserve, CentralRevenueReserve);

    private static decimal? CalculateTotal(decimal? school, decimal? central)
    {
        return school.GetValueOrDefault() + central.GetValueOrDefault();
    }


}

public record SchoolBalanceResponse : BalanceBaseResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
}

public record TrustBalanceResponse : BalanceBaseResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

public record SchoolBalanceHistoryResponse : BalanceBaseResponse
{
    public string? URN { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}

public record TrustBalanceHistoryResponse : BalanceBaseResponse
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}