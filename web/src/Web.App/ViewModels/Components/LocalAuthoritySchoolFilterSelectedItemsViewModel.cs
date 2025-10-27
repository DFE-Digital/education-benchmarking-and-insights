namespace Web.App.ViewModels.Components;

public class LocalAuthoritySchoolFilterSelectedItemsViewModel<T> : LocalAuthoritySchoolFilterSelectedItemsViewModelBase
{
    public LocalAuthoritySchoolFilterSelectedItemsViewModel
    (
        string formPrefix,
        string heading,
        string formFieldName,
        T[] selectedFilters,
        Func<T, string> labelSelector,
        Func<T, string> valueSelector) : base(formPrefix, heading, formFieldName)
    {
        SelectedFilters = selectedFilters.Cast<object>().ToArray();
        LabelSelector = l => labelSelector.Invoke((T)l);
        ValueSelector = v => valueSelector.Invoke((T)v);
    }
}

/// <summary>
///     This non-generic base model must be consumed by the partial view due to
///     c# type invariance when attempting to use a generic type parameter for the model.
///     The generic class above is only used to provide type safety when providing data
///     to the partial view and otherwise boxes the type when setting the base properties.
/// </summary>
public abstract class LocalAuthoritySchoolFilterSelectedItemsViewModelBase(
    string formPrefix,
    string heading,
    string formFieldName)
{
    public string Heading => heading;
    public string FormFieldName => $"{formPrefix}{formFieldName}";
    public object[] AllFilters { get; init; } = [];
    public object[] SelectedFilters { get; init; } = [];
    public Func<object, string> LabelSelector { get; init; } = _ => string.Empty;
    public Func<object, string> ValueSelector { get; init; } = _ => string.Empty;
}