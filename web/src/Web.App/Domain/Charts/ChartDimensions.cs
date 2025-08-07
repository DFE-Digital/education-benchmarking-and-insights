using Web.App.Extensions;

namespace Web.App.Domain.Charts;

public static class ChartDimensions
{
    public enum ResultAsOptions
    {
        SpendPerPupil = 0,
        Actuals = 1,
        PercentExpenditure = 2,
        PercentIncome = 3
    }

    public static string GetQueryParam(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerPupil => "PerUnit",
        ResultAsOptions.Actuals => "Actuals",
        ResultAsOptions.PercentExpenditure => "PercentExpenditure",
        ResultAsOptions.PercentIncome => "PercentIncome",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetChartValueType(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerPupil or ResultAsOptions.Actuals => ChartValueType.Currency,
        ResultAsOptions.PercentExpenditure or ResultAsOptions.PercentIncome => ChartValueType.Percent,
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetXAxisLabel(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerPupil => "£ per pupil",
        ResultAsOptions.Actuals => "actuals",
        ResultAsOptions.PercentExpenditure => "percentage of expenditure",
        ResultAsOptions.PercentIncome => "percentage of income",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetFormattedValue(this ResultAsOptions option, decimal? value) => option switch
    {
        ResultAsOptions.SpendPerPupil or ResultAsOptions.Actuals => value.ToCurrency(),
        ResultAsOptions.PercentExpenditure or ResultAsOptions.PercentIncome => value.ToPercent(),
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };

    public static string GetTableHeader(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerPupil or ResultAsOptions.Actuals => "Amount",
        ResultAsOptions.PercentExpenditure or ResultAsOptions.PercentIncome => "Percentage",
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}