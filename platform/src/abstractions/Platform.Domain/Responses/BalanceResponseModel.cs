using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record BalanceResponseModel
{
    public int YearEnd { get; private set; }
    public string Term => $"{YearEnd - 1} to {YearEnd}";
    public decimal? InYearBalance { get; private set; }
    public decimal? RevenueReserve { get; private set; }

    private static BalanceResponseModel CreateEmpty(int term)
    {
        return new BalanceResponseModel
        {
            YearEnd = term
        };
    }
    
    public static BalanceResponseModel Create(BalanceDataObject? dataObject, int term, string dimension)
    {
        if (dataObject is null)
        {
            return CreateEmpty(term);
        }
        
        return new BalanceResponseModel
            {
                YearEnd = term,
                InYearBalance = CalculationValue(dataObject.TotalIncome - dataObject.TotalExpenditure, dataObject, dimension),
                RevenueReserve = CalculationValue(dataObject.RevenueReserve, dataObject, dimension)
            };
    }

    private static decimal CalculationValue(decimal value, BalanceDataObject dataObject, string dimension)
    {
        return dimension switch
        {
            BalanceDimensions.Actuals => value,
            BalanceDimensions.PoundPerPupil => dataObject.NoPupils != 0 ? value / dataObject.NoPupils : 0,
            BalanceDimensions.PercentIncome => dataObject.TotalIncome != 0 ? value / dataObject.TotalIncome * 100 : 0,
            BalanceDimensions.PercentExpenditure => dataObject.TotalExpenditure != 0 ? value / dataObject.TotalExpenditure * 100 : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}