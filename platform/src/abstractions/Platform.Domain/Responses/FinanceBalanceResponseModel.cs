using System;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Domain;

[ExcludeFromCodeCoverage]
public record FinanceBalanceResponseModel
{
    public int YearEnd { get; set; }
    public string? Urn { get; private set; }
    public string? Name { get; private set; }
    public string? SchoolType { get; private set; }
    public string? LocalAuthority { get; private set; }
    public decimal NumberPupils { get; private set; }
    public BalancePayloadResponseModel? Payload { get; private set; }

    public static FinanceBalanceResponseModel Create(SchoolTrustFinancialDataObject dataObject, int term, Dimension dimension = Dimension.Actuals)
    {
        return new FinanceBalanceResponseModel
        {
            YearEnd = term,
            Urn = dataObject.Urn.ToString(),
            Name = dataObject.SchoolName,
            SchoolType = dataObject.Type,
            LocalAuthority = dataObject.La.ToString(),
            NumberPupils = dataObject.NoPupils,
            Payload = BalancePayloadResponseModel.Create(dataObject, dimension)
        };
    }
}

[ExcludeFromCodeCoverage]
public record BalancePayloadResponseModel
{
    public Dimension Dimension { get; private set; }
    public decimal InYearBalance { get; private set; }
    public decimal RevenueReserve { get; private set; }

    public static BalancePayloadResponseModel Create(SchoolTrustFinancialDataObject dataObject, Dimension dimension)
    {
        return new BalancePayloadResponseModel
        {
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
            Dimension.PoundPerPupil => value / dataObject.NoPupils,
            Dimension.PercentIncome => value / dataObject.TotalIncome * 100,
            Dimension.PercentExpenditure => value / dataObject.TotalExpenditure * 100,
            _ => throw new ArgumentOutOfRangeException(nameof(dimension), dimension, "Unknown value dimension")
        };
    }
}