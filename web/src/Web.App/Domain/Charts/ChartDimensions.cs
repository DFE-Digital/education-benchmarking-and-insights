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

    public static string GetValueFormat(this ResultAsOptions option) => option switch
    {
        ResultAsOptions.SpendPerPupil => "$,~s",
        ResultAsOptions.Actuals => "$,~s",
        ResultAsOptions.PercentExpenditure => ".1%",
        ResultAsOptions.PercentIncome => ".1%",
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
}