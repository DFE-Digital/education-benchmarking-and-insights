using Web.App.Domain;
using Web.App.Domain.Charts;

namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolFinancialViewModel(string code, string formPrefix) : LocalAuthoritySchoolFinancialFormViewModel(code, formPrefix);

public class LocalAuthoritySchoolFilterAccordionViewModel<T> : LocalAuthoritySchoolFilterAccordionViewModelBase
{
    public LocalAuthoritySchoolFilterAccordionViewModel
    (
        string formPrefix,
        string heading,
        string formFieldName,
        T[] allFilters,
        T[] selectedFilters,
        Func<T, string> labelSelector,
        Func<T, string> valueSelector) : base(formPrefix, heading, formFieldName)
    {
        AllFilters = allFilters.Cast<object>().ToArray();
        SelectedFilters = selectedFilters.Cast<object>().ToArray();
        LabelSelector = l => labelSelector.Invoke((T)l);
        ValueSelector = v => valueSelector.Invoke((T)v);
    }
}

public abstract class LocalAuthoritySchoolFilterAccordionViewModelBase(
    string formPrefix,
    string heading,
    string formFieldName)
{
    public string Heading => heading;
    public string FormFieldName => $"{formPrefix}{formFieldName}";
    public object[]? AllFilters { get; init; }
    public object[]? SelectedFilters { get; init; }
    public Func<object, string>? LabelSelector { get; init; }
    public Func<object, string>? ValueSelector { get; init; }
}

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

    public class FormFieldNames
    {
        public const string ResultAs = "as";
        public const string SelectedOverallPhases = "phase";
        public const string SelectedNurseryProvisions = "nursery";
    }
}