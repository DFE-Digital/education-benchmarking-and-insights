using Web.App.Extensions;
// ReSharper disable ConvertToExtensionBlock

namespace Web.App.Domain.Charts;

public static class SchoolSpendingDimensions
{
    public enum ResultAsOptions
    {
        SpendPerUnit = 0,
        Actuals = 1,
        PercentExpenditure = 2,
        PercentIncome = 3
    }

    public static readonly ResultAsOptions[] AllResultsAsOptions =
    [
        ResultAsOptions.SpendPerUnit,
        ResultAsOptions.Actuals,
        ResultAsOptions.PercentExpenditure,
        ResultAsOptions.PercentIncome,
    ];

    public enum BandingsAsOptions
    {
        WellAbove = 0,
        Above = 1
    }

    public static readonly BandingsAsOptions[] AllBandingsAsOptions =
    [
        BandingsAsOptions.WellAbove,
        BandingsAsOptions.Above
    ];

    public static string GetQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerUnit => "PerUnit",
        ResultAsOptions.Actuals => "Actuals",
        ResultAsOptions.PercentExpenditure => "PercentExpenditure",
        ResultAsOptions.PercentIncome => "PercentIncome",
        _ => ""
    };

    public static string GetValueType(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerUnit or ResultAsOptions.Actuals => ValueType.Currency,
        ResultAsOptions.PercentExpenditure or ResultAsOptions.PercentIncome => ValueType.Percent,
        _ => ""
    };

    public static string GetXAxisLabel(this ResultAsOptions option, string set) => option switch
    {
        ResultAsOptions.SpendPerUnit => set == ComparatorSetTypes.Building ? "£ per m²" : "£ per pupil",
        ResultAsOptions.Actuals => "actuals",
        ResultAsOptions.PercentExpenditure => "percentage of expenditure",
        ResultAsOptions.PercentIncome => "percentage of income",
        _ => ""
    };

    public static string GetFormattedValue(this ResultAsOptions option, decimal? value) => option switch
    {
        ResultAsOptions.SpendPerUnit or ResultAsOptions.Actuals => value.ToCurrency(),
        ResultAsOptions.PercentExpenditure or ResultAsOptions.PercentIncome => value.ToPercent(),
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetTableHeader(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerUnit or ResultAsOptions.Actuals => "Amount",
        ResultAsOptions.PercentExpenditure or ResultAsOptions.PercentIncome => "Percentage",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetResultsAsDescription(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerUnit => "£ per unit",
        ResultAsOptions.Actuals => "actuals",
        ResultAsOptions.PercentExpenditure => "% of expenditure",
        ResultAsOptions.PercentIncome => "% of income",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetBandingAsDescription(this BandingsAsOptions option) => option switch
    {
        BandingsAsOptions.WellAbove => "Well above average",
        BandingsAsOptions.Above => "Above average",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}
