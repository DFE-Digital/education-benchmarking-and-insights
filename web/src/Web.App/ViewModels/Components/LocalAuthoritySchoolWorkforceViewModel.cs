using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels.Components;

// todo: deprecate base class
public class LocalAuthoritySchoolWorkforceViewModel(string code, string formPrefix, int maxRows, string defaultSort)
    : LocalAuthoritySchoolWorkforceFormViewModel(code, formPrefix, maxRows, defaultSort);

public class LocalAuthoritySchoolWorkforceFormViewModel(string code, string formPrefix, int maxRows, string defaultSort)
{
    public static readonly WorkforceDimensions.ResultAsOptions[] FilterDimensions =
    [
        WorkforceDimensions.ResultAsOptions.PercentPerPupil,
        WorkforceDimensions.ResultAsOptions.Actuals,
    ];

    public LocalAuthoritySchoolWorkforce[] Results = [];

    public static string TableId => "local-authority-school-workforce-table";

    public string Code => code;
    public string FormPrefix => formPrefix;
    public int MaxRows => maxRows;
    public string DefaultSort => defaultSort;

    public bool AllRows { get; init; }
    public bool FiltersVisible { get; init; }
    public WorkforceDimensions.ResultAsOptions ResultAs { get; init; } = WorkforceDimensions.ResultAsOptions.PercentPerPupil;
    public OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases { get; init; } = [];
    public NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions { get; init; } = [];
    public SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions { get; init; } = [];
    public SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions { get; init; } = [];
    public string? Sort { get; init; }

    public bool HasFilters => SelectedOverallPhases.Length > 0
                              || SelectedNurseryProvisions.Length > 0
                              || SelectedSpecialProvisions.Length > 0
                              || SelectedSixthFormProvisions.Length > 0;

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