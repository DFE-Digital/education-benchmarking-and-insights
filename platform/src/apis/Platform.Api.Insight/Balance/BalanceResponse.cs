using System.Diagnostics.CodeAnalysis;
namespace Platform.Api.Insight.Balance;

public static class BalanceResponseFactory
{
    public static SchoolBalanceResponse Create(SchoolBalanceModel model, BalanceParameters parameters)
    {
        var response = CreateResponse<SchoolBalanceResponse>(model, parameters);

        response.URN = model.URN;
        response.SchoolName = model.SchoolName;
        response.SchoolType = model.SchoolType;
        response.LAName = model.LAName;
        response.TotalPupils = model.TotalPupils;

        return response;
    }

    public static TrustBalanceResponse Create(TrustBalanceModel model, BalanceParameters parameters)
    {
        var response = CreateResponse<TrustBalanceResponse>(model, parameters);

        response.CompanyNumber = model.CompanyNumber;
        response.TrustName = model.TrustName;

        return response;
    }

    public static SchoolBalanceHistoryResponse Create(SchoolBalanceHistoryModel model, BalanceParameters parameters)
    {
        var response = CreateResponse<SchoolBalanceHistoryResponse>(model, parameters);

        response.URN = model.URN;
        response.Year = model.Year;

        return response;
    }

    public static TrustBalanceHistoryResponse Create(TrustBalanceHistoryModel model, BalanceParameters parameters)
    {
        var response = CreateResponse<TrustBalanceHistoryResponse>(model, parameters);

        response.CompanyNumber = model.CompanyNumber;
        response.Year = model.Year;

        return response;
    }

    private static T CreateResponse<T>(BalanceBaseModel model, BalanceParameters parameters) where T : BalanceBaseResponse, new() => new()
    {
        SchoolInYearBalance = CalcAmount(model.InYearBalance - model.InYearBalanceCS.GetValueOrDefault(), model, parameters),
        CentralInYearBalance = CalcAmount(model.InYearBalanceCS, model, parameters),
        SchoolRevenueReserve = CalcAmount(model.RevenueReserve - model.RevenueReserveCS.GetValueOrDefault(), model, parameters),
        CentralRevenueReserve = CalcAmount(model.RevenueReserveCS, model, parameters),
        InYearBalance = CalcTotal(model.InYearBalance, model.InYearBalanceCS.GetValueOrDefault(), model, parameters),
        RevenueReserve = CalcTotal(model.RevenueReserve, model.RevenueReserveCS.GetValueOrDefault(), model, parameters)
    };

    private static decimal? CalcTotal(decimal? value, decimal valueCentral, BalanceBaseModel model, BalanceParameters parameters)
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

    private static decimal? CalcAmount(decimal? value, BalanceBaseModel model, BalanceParameters parameters) => parameters.ExcludeCentralServices
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
            BalanceDimensions.Actuals => value,
            BalanceDimensions.PerUnit => totalUnit != 0 ? value / totalUnit : 0,
            BalanceDimensions.PercentIncome => totalIncome != 0 ? value / totalIncome * 100 : 0,
            BalanceDimensions.PercentExpenditure => totalExpenditure != 0 ? value / totalExpenditure * 100 : 0,
            _ => null
        };
    }
}

[ExcludeFromCodeCoverage]
public abstract record BalanceBaseResponse
{
    public decimal? SchoolInYearBalance { get; set; }
    public decimal? CentralInYearBalance { get; set; }
    public decimal? SchoolRevenueReserve { get; set; }
    public decimal? CentralRevenueReserve { get; set; }
    public decimal? InYearBalance { get; set; }
    public decimal? RevenueReserve { get; set; }
}

[ExcludeFromCodeCoverage]
public record SchoolBalanceResponse : BalanceBaseResponse
{
    public string? URN { get; set; }
    public string? SchoolName { get; set; }
    public string? SchoolType { get; set; }
    public string? LAName { get; set; }
    public decimal? TotalPupils { get; set; }
}

[ExcludeFromCodeCoverage]
public record TrustBalanceResponse : BalanceBaseResponse
{
    public string? CompanyNumber { get; set; }
    public string? TrustName { get; set; }
}

[ExcludeFromCodeCoverage]
public record SchoolBalanceHistoryResponse : BalanceBaseResponse
{
    public string? URN { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}

[ExcludeFromCodeCoverage]
public record TrustBalanceHistoryResponse : BalanceBaseResponse
{
    public string? CompanyNumber { get; set; }
    public int? Year { get; set; }
    public string? Term => Year != null ? $"{Year - 1} to {Year}" : null;
}