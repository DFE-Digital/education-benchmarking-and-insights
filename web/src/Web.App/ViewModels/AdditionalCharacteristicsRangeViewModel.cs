using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Web.App.Extensions;
namespace Web.App.ViewModels;

public class AdditionalCharacteristicsRangeViewModel()
{
    public AdditionalCharacteristicsRangeViewModel(
        ViewDataDictionary<SchoolComparatorsByCharacteristicViewModel> viewData,
        string title,
        string selectedFieldName,
        string fromFieldName,
        string toFieldName,
        string? schoolName,
        string? schoolValueFormatted,
        string? suffix,
        string? inputSuffix = null) : this()
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

        SchoolName = schoolName;
        SchoolValueFormatted = schoolValueFormatted;
        Suffix = suffix;
        InputSuffix = inputSuffix;
    }

    public string Title { get; private set; } = string.Empty;
    public string SelectedFieldName { get; private set; } = string.Empty;
    public string FromFieldName { get; private set; } = string.Empty;
    public string ToFieldName { get; private set; } = string.Empty;
    public bool Selected { get; private set; }
    public string? ValueFrom { get; private set; }
    public string? ValueTo { get; private set; }
    public bool HasError { get; }
    public string[] Errors { get; private set; } = [];
    public string? SchoolName { get; private set; }
    public string? SchoolValueFormatted { get; private set; }
    public string? Suffix { get; private set; }
    public string? InputSuffix { get; private set; }
}