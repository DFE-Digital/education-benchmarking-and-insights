using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolFinancialViewModel(string code, string formPrefix)
{
    public static readonly Dimensions.ResultAsOptions[] FilterDimensions =
    [
        Dimensions.ResultAsOptions.SpendPerPupil,
        Dimensions.ResultAsOptions.Actuals,
        Dimensions.ResultAsOptions.PercentExpenditure,
        Dimensions.ResultAsOptions.PercentIncome
    ];

    public string Code => code;
    public string FormPrefix => formPrefix;
    public string? Other => "{\"something\":123}";

    public Dimensions.ResultAsOptions ResultAs { get; set; } = Dimensions.ResultAsOptions.SpendPerPupil;
    public OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases { get; set; } = [];
}