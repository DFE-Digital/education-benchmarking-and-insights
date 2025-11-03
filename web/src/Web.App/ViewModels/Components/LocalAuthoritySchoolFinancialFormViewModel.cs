using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolFinancialFormViewModel(
    string code,
    string formPrefix,
    int maxRows,
    string defaultSort,
    string tabId,
    Dictionary<string, StringValues>? otherFormValues,
    string? path,
    IQueryCollection? query)
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
    public string DefaultSort => defaultSort;
    public Dictionary<string, StringValues>? OtherFormValues => otherFormValues;
    public string TabId => tabId;

    public bool AllRows { get; init; }
    public bool FiltersVisible { get; init; }
    public Dimensions.ResultAsOptions ResultAs { get; init; } = Dimensions.ResultAsOptions.PercentIncome;
    public OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases { get; init; } = [];
    public NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions { get; init; } = [];
    public SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions { get; init; } = [];
    public SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions { get; init; } = [];
    public string? Sort { get; init; }

    public bool HasFilters => SelectedOverallPhases.Length > 0
                              || SelectedNurseryProvisions.Length > 0
                              || SelectedSpecialProvisions.Length > 0
                              || SelectedSixthFormProvisions.Length > 0;

    public string Path => path ?? string.Empty;
    public IQueryCollection Query => query ?? new QueryCollection();
    public string? Fragment => string.IsNullOrWhiteSpace(tabId) ? null : $"#{tabId}";

    public RouteValueDictionary RouteValuesOnClear
    {
        get
        {
            RouteValueDictionary routeValues = new(
                [
                    new KeyValuePair<string, object?>("code", Code),
                    new KeyValuePair<string, object?>($"{FormPrefix}{FormFieldNames.FiltersVisible}", FormFieldValues.Show),
                    new KeyValuePair<string, object?>($"{FormPrefix}{FormFieldNames.ResultAs}", (int)ResultAs),
                    new KeyValuePair<string, object?>($"{FormPrefix}{FormFieldNames.Sort}", Sort)
                ]
            );

            if (OtherFormValues == null)
            {
                return routeValues;
            }

            foreach (var (key, value) in OtherFormValues)
            {
                routeValues[key] = value;
            }

            return routeValues;
        }
    }

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