using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Web.App.Extensions;
namespace Web.App.ViewModels;

public abstract class AdditionalCharacteristicsViewModel
{
    protected AdditionalCharacteristicsViewModel(
        ViewDataDictionary<SchoolComparatorsByCharacteristicViewModel> viewData,
        string title,
        string selectedFieldName,
        string? schoolName,
        string? schoolValueFormatted,
        string? prefix,
        string? suffix)
    {
        Title = title;

        SelectedFieldName = selectedFieldName;
        var defaultSelectedFieldValue = string.Empty;
        if (viewData.TryGetValue(selectedFieldName, out var selectedFieldValue))
        {
            defaultSelectedFieldValue = selectedFieldValue?.ToString();
        }
        Selected = bool.TrueString.Equals(
            viewData.ModelState.GetAttemptedValueOrDefault(selectedFieldName, defaultSelectedFieldValue), StringComparison.OrdinalIgnoreCase);

        SchoolName = schoolName;
        SchoolValueFormatted = schoolValueFormatted;
        Prefix = prefix;
        Suffix = suffix;
    }

    public string Title { get; private set; }
    public string SelectedFieldName { get; private set; }
    public bool Selected { get; private set; }
    public bool HasError { get; protected init; }
    public string[] Errors { get; protected init; } = [];
    public string? SchoolName { get; private set; }
    public string? SchoolValueFormatted { get; private set; }
    public string? Prefix { get; private set; }
    public string? Suffix { get; private set; }
}

public class AdditionalCharacteristicsRangeViewModel : AdditionalCharacteristicsViewModel
{
    public AdditionalCharacteristicsRangeViewModel(
        ViewDataDictionary<SchoolComparatorsByCharacteristicViewModel> viewData,
        string title,
        string selectedFieldName,
        string fromFieldName,
        string toFieldName,
        string? schoolName,
        string? schoolValueFormatted,
        string? prefix = null,
        string? suffix = null,
        string? inputSuffix = null)
        : base(viewData, title, selectedFieldName, schoolName, schoolValueFormatted, prefix, suffix)
    {
        FromFieldName = fromFieldName;
        var defaultFromValue = string.Empty;
        if (viewData.TryGetValue(fromFieldName, out var fromValue))
        {
            defaultFromValue = fromValue?.ToString();
        }
        ValueFrom = viewData.ModelState.GetAttemptedValueOrDefault(fromFieldName, defaultFromValue);

        ToFieldName = toFieldName;
        var defaultToValue = string.Empty;
        if (viewData.TryGetValue(toFieldName, out var toValue))
        {
            defaultToValue = toValue?.ToString();
        }
        ValueTo = viewData.ModelState.GetAttemptedValueOrDefault(toFieldName, defaultToValue);

        HasError = viewData.ModelState.HasError(fromFieldName) || viewData.ModelState.HasError(toFieldName);
        Errors = HasError
            ? (viewData.ModelState[fromFieldName]?.Errors ?? [])
            .Concat(viewData.ModelState[toFieldName]?.Errors ?? [])
            .Select(e => e.ErrorMessage)
            .ToArray()
            : [];

        InputSuffix = inputSuffix;
    }

    public string FromFieldName { get; private set; }
    public string ToFieldName { get; private set; }
    public string? ValueFrom { get; private set; }
    public string? ValueTo { get; private set; }
    public string? InputSuffix { get; private set; }
}

public class AdditionalCharacteristicsSelectViewModel : AdditionalCharacteristicsViewModel
{
    public AdditionalCharacteristicsSelectViewModel(
        ViewDataDictionary<SchoolComparatorsByCharacteristicViewModel> viewData,
        string title,
        string selectedFieldName,
        string valueFieldName,
        string? schoolName,
        string? schoolValueFormatted,
        string[] options,
        string? prefix = null,
        string? suffix = null)
        : base(viewData, title, selectedFieldName, schoolName, schoolValueFormatted, prefix, suffix)
    {
        ValueFieldName = valueFieldName;
        var defaultValue = string.Empty;
        if (viewData.TryGetValue(valueFieldName, out var value))
        {
            defaultValue = value?.ToString();
        }
        var rawValue = viewData.ModelState.GetAttemptedValueOrDefault(valueFieldName, defaultValue);
        Values = string.IsNullOrWhiteSpace(rawValue) ? [] : rawValue.Split(",");

        HasError = viewData.ModelState.HasError(valueFieldName);
        Errors = HasError ? [viewData.ModelState[valueFieldName]?.Errors.First().ErrorMessage!] : [];
        Options = options;
    }

    public string ValueFieldName { get; private set; }
    public string[] Values { get; private set; }
    public string[] Options { get; private set; }
}