namespace Web.App.ViewModels.Components;

// todo: unit test
public class LocalAuthoritySchoolFinancialFinancialFilterAccordionSectionViewModel<T> : LocalAuthoritySchoolFinancialFilterAccordionSectionViewModelBase
{
    public LocalAuthoritySchoolFinancialFinancialFilterAccordionSectionViewModel
    (
        string accordionId,
        int sectionIndex,
        string formPrefix,
        string heading,
        string formFieldName,
        T[] allFilters,
        T[] selectedFilters,
        Func<T, string> labelSelector,
        Func<T, string> valueSelector) : base(accordionId, sectionIndex, formPrefix, heading, formFieldName)
    {
        AllFilters = allFilters.Cast<object>().ToArray();
        SelectedFilters = selectedFilters.Cast<object>().ToArray();
        LabelSelector = l => labelSelector.Invoke((T)l);
        ValueSelector = v => valueSelector.Invoke((T)v);
    }
}

public abstract class LocalAuthoritySchoolFinancialFilterAccordionSectionViewModelBase(
    string accordionId,
    int sectionIndex,
    string formPrefix,
    string heading,
    string formFieldName)
{
    public string AccordionId => accordionId;
    public int SectionIndex => sectionIndex;
    public string Heading => heading;
    public string FormFieldName => $"{formPrefix}{formFieldName}";
    public object[] AllFilters { get; init; } = [];
    public object[] SelectedFilters { get; init; } = [];
    public Func<object, string> LabelSelector { get; init; } = _ => string.Empty;
    public Func<object, string> ValueSelector { get; init; } = _ => string.Empty;
}