using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Web.App.Extensions;
namespace Web.App.ViewModels;

public abstract class AdditionalCharacteristicsViewModel
{
    protected AdditionalCharacteristicsViewModel(
        ViewDataDictionary viewData,
        string title,
        string selectedFieldName,
        string? defaultSelectedFieldValue,
        string? schoolName,
        string? schoolValueFormatted,
        string? prefix,
        string? suffix)
    {
        Title = title;

        SelectedFieldName = selectedFieldName;
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
        ViewDataDictionary viewData,
        string title,
        string selectedFieldName,
        string? defaultSelectedFieldValue,
        string fromFieldName,
        string? defaultFromValue,
        string toFieldName,
        string? defaultToValue,
        string? schoolName,
        string? schoolValueFormatted,
        string? prefix = null,
        string? suffix = null,
        string? inputSuffix = null,
        string? inputsSuffix = null,
        bool? wide = null)
        : base(viewData, title, selectedFieldName, defaultSelectedFieldValue, schoolName, schoolValueFormatted, prefix, suffix)
    {
        FromFieldName = fromFieldName;
        if (viewData.TryGetValue(fromFieldName, out var fromValue))
        {
            defaultFromValue = fromValue?.ToString();
        }
        ValueFrom = viewData.ModelState.GetAttemptedValueOrDefault(fromFieldName, defaultFromValue);

        ToFieldName = toFieldName;
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
        InputsSuffix = inputsSuffix;
        Wide = wide;
    }

    public string FromFieldName { get; private set; }
    public string ToFieldName { get; private set; }
    public string? ValueFrom { get; private set; }
    public string? ValueTo { get; private set; }
    public string? InputSuffix { get; private set; }
    public string? InputsSuffix { get; private set; }
    public bool? Wide { get; private set; }
}

public class AdditionalCharacteristicsSelectViewModel : AdditionalCharacteristicsViewModel
{
    public AdditionalCharacteristicsSelectViewModel(
        ViewDataDictionary viewData,
        string title,
        string selectedFieldName,
        string? defaultSelectedFieldValue,
        string valueFieldName,
        string? defaultValue,
        string? schoolName,
        string? schoolValueFormatted,
        string[] options,
        bool multiSelect = true,
        string? prefix = null,
        string? suffix = null)
        : base(viewData, title, selectedFieldName, defaultSelectedFieldValue, schoolName, schoolValueFormatted, prefix, suffix)
    {
        ValueFieldName = valueFieldName;
        if (viewData.TryGetValue(valueFieldName, out var value))
        {
            defaultValue = value?.ToString();
        }
        var rawValue = viewData.ModelState.GetAttemptedValueOrDefault(valueFieldName, defaultValue);
        Values = string.IsNullOrWhiteSpace(rawValue) ? [] : rawValue.Split(",");

        HasError = viewData.ModelState.HasError(valueFieldName);
        Errors = HasError ? [viewData.ModelState[valueFieldName]?.Errors.First().ErrorMessage!] : [];
        Options = options;
        MultiSelect = multiSelect;
    }

    public string ValueFieldName { get; private set; }
    public string[] Values { get; private set; }
    public string[] Options { get; private set; }
    public bool MultiSelect { get; private set; }
}