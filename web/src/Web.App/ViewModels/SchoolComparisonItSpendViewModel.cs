using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;

namespace Web.App.ViewModels;

public class SchoolComparisonItSpendViewModel(School school, SchoolComparisonSubCategoriesViewModel subCategories)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolComparisonSubCategoriesViewModel SubCategories => subCategories;

    public ViewAsOptions ViewAs { get; set; } = ViewAsOptions.Chart;
    public ChartDimensions.ResultAsOptions ResultAs { get; set; } = ChartDimensions.ResultAsOptions.SpendPerPupil;
    public ItSpendingCategories.SubCategoryFilter[] SelectedSubCategories { get; set; } = [];
    public Func<decimal?, string> FormatValue => value =>
    {
        return ResultAs switch
        {
            ChartDimensions.ResultAsOptions.SpendPerPupil or ChartDimensions.ResultAsOptions.Actuals => value.ToCurrencyWithPrecisionIfSmall(),
            ChartDimensions.ResultAsOptions.PercentExpenditure or ChartDimensions.ResultAsOptions.PercentIncome => value.ToPercent(),
            _ => value.ToSimpleDisplay()
        };
    };
    public string ExpenditureTableColumnHeader =>
        ResultAs is ChartDimensions.ResultAsOptions.PercentExpenditure or ChartDimensions.ResultAsOptions.PercentIncome
            ? "Percentage"
            : "Amount";

    public enum ViewAsOptions
    {
        Chart = 0,
        Table = 1
    }
}