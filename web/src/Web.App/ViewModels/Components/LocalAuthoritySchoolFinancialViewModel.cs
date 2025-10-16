using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolFinancialViewModel(string code, string formPrefix) : LocalAuthoritySchoolFinancialFormViewModel(code, formPrefix);

public class LocalAuthoritySchoolFinancialFormViewModel(string code, string formPrefix)
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

    public Dimensions.ResultAsOptions ResultAs { get; init; } = Dimensions.ResultAsOptions.SpendPerPupil;
    public OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases { get; init; } = [];
    public NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions { get; init; } = [];

    public static class FormFieldNames
    {
        public const string ResultAs = "as";
        public const string SelectedOverallPhases = "phase";
        public const string SelectedNurseryProvisions = "nursery";
    }
}