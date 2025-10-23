using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolFinancialViewModel(string code, string formPrefix, int maxRows)
    : LocalAuthoritySchoolFinancialFormViewModel(code, formPrefix, maxRows);

public class LocalAuthoritySchoolFinancialFormViewModel(string code, string formPrefix, int maxRows)
{
    public static readonly Dimensions.ResultAsOptions[] FilterDimensions =
    [
        Dimensions.ResultAsOptions.SpendPerPupil,
        Dimensions.ResultAsOptions.Actuals,
        Dimensions.ResultAsOptions.PercentExpenditure,
        Dimensions.ResultAsOptions.PercentIncome
    ];

    public LocalAuthoritySchoolFinancial[] Results = [];

    public static string TableId => "local-authority-school-financial-table";

    public string Code => code;
    public string FormPrefix => formPrefix;
    public int MaxRows => maxRows;

    public bool AllRows { get; init; }
    public bool FiltersVisible { get; init; }
    public Dimensions.ResultAsOptions ResultAs { get; set; } = Dimensions.ResultAsOptions.PercentIncome;
    public OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases { get; init; } = [];
    public NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions { get; init; } = [];
    public SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions { get; init; } = [];
    public SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions { get; init; } = [];
    public string? Sort { get; init; }

    public static class FormFieldNames
    {
        public const string FiltersVisible = "filter";
        public const string ResetFields = "__resetFields";
        public const string ResultAs = "as";
        public const string Rows = "rows";
        public const string SelectedOverallPhases = "phase";
        public const string SelectedNurseryProvisions = "nursery";
        public const string SelectedSpecialProvisions = "special";
        public const string SelectedSixthFormProvisions = "sixth";
        public const string Sort = "sort";
    }

    public static class FormFieldValues
    {
        public const string All = "all";
        public const string Hide = "hide";
        public const string Show = "show";
    }
}