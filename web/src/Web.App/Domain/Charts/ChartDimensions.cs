using Web.App.ViewModels;

namespace Web.App.Domain.Charts;

public enum ChartDimensions
{
    PerUnit = 0,
    Actuals = 1,
    PercentExpenditure = 2,
    PercentIncome = 3
}

public static class ChartDimensionMapper
{
    public static ChartDimensions ToChartDimension(this SchoolComparisonItSpendViewModel.ResultAsOptions option) => option switch
    {
        SchoolComparisonItSpendViewModel.ResultAsOptions.SpendPerPupil => ChartDimensions.PerUnit,
        SchoolComparisonItSpendViewModel.ResultAsOptions.Actuals => ChartDimensions.Actuals,
        SchoolComparisonItSpendViewModel.ResultAsOptions.PercentExpenditure => ChartDimensions.PercentExpenditure,
        SchoolComparisonItSpendViewModel.ResultAsOptions.PercentIncome => ChartDimensions.PercentIncome,
        _ => throw new ArgumentOutOfRangeException(nameof(option))
    };
}