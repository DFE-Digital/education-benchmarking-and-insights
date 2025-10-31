using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolWorkforceFormViewModel(
    string code,
    string formPrefix,
    int maxRows,
    string defaultSort,
    Dictionary<string, StringValues> otherFormValues,
    string tabId,
    string path,
    IQueryCollection query)
{
    public static readonly SchoolsSummaryWorkforceDimensions.ResultAsOptions[] FilterDimensions =
    [
        SchoolsSummaryWorkforceDimensions.ResultAsOptions.PercentPupil,
        SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals
    ];

    public LocalAuthoritySchoolWorkforce[] Results = [];

    public static string TableId => "local-authority-school-workforce-table";

    public string Code => code;
    public string FormPrefix => formPrefix;
    public int MaxRows => maxRows;
    public string DefaultSort => defaultSort;
    public Dictionary<string, StringValues> OtherFormValues => otherFormValues;
    public string TabId => tabId;
    public string Path => path;
    public IQueryCollection Query => query;

    public bool AllRows { get; init; }
    public bool FiltersVisible { get; init; }
    public SchoolsSummaryWorkforceDimensions.ResultAsOptions ResultAs { get; init; } = SchoolsSummaryWorkforceDimensions.ResultAsOptions.PercentPupil;
    public OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases { get; init; } = [];
    public NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions { get; init; } = [];
    public SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions { get; init; } = [];
    public SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions { get; init; } = [];
    public string? Sort { get; init; }

    public bool HasFilters => SelectedOverallPhases.Length > 0
                              || SelectedNurseryProvisions.Length > 0
                              || SelectedSpecialProvisions.Length > 0
                              || SelectedSixthFormProvisions.Length > 0;

    public string? Fragment => string.IsNullOrWhiteSpace(tabId) ? null : $"#{tabId}";

    public RouteValueDictionary RouteValuesOnClear =>
        new(
            new[]
                {
                    new KeyValuePair<string, object?>("code", Code),
                    new KeyValuePair<string, object?>($"{FormPrefix}{FormFieldNames.FiltersVisible}", FormFieldValues.Show),
                    new KeyValuePair<string, object?>($"{FormPrefix}{FormFieldNames.ResultAs}", (int)ResultAs),
                    new KeyValuePair<string, object?>($"{FormPrefix}{FormFieldNames.Sort}", Sort)
                }
                .Concat(OtherFormValues.Select(kvp => new KeyValuePair<string, object?>(kvp.Key, kvp.Value)))
        );

    public static class FormFieldNames
    {
        public const string FiltersVisible = "filter";
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