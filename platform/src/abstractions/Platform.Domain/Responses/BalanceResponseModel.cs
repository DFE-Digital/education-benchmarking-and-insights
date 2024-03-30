using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record BalanceResponseModel
{
    public int YearEnd { get; private set; }
    public Dimension Dimension { get; private set; }
    public decimal? InYearBalance { get; private set; }
    public decimal? RevenueReserve { get; private set; }

    public static BalanceResponseModel Create(SchoolTrustFinancialDataObject? dataObject, int term, Dimension dimension = Dimension.Actuals)
    {
        return dataObject is null
            ? new BalanceResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
            }
            : new BalanceResponseModel
            {
                YearEnd = term,
                Dimension = dimension,
                InYearBalance = CalculationValue(dataObject.TotalIncome - dataObject.TotalExpenditure, dataObject, dimension),
                RevenueReserve = CalculationValue(dataObject.RevenueReserve, dataObject, dimension)
            };
    }

    private static decimal CalculationValue(decimal value, SchoolTrustFinancialDataObject dataObject, Dimension dimension)
    {
        return dimension switch
        {
            Dimension.Actuals => value,
            Dimension.PoundPerPupil => dataObject.NoPupils != 0 ? value / dataObject.NoPupils : 0,
            Dimension.PercentIncome => dataObject.TotalIncome != 0 ? value / dataObject.TotalIncome * 100 : 0,
            Dimension.PercentExpenditure => dataObject.TotalExpenditure != 0 ? value / dataObject.TotalExpenditure * 100 : 0,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}