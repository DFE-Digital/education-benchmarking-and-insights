using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.App.Extensions;

[ExcludeFromCodeCoverage]
public static class ModelStateExtensions
{
    public static bool HasError(this ModelStateDictionary state, string key)
    {
        if (state.TryGetValue(key, out var entry))
            return entry.ValidationState == ModelValidationState.Invalid;

        return false;
    }

    public static string? GetAttemptedValueOrDefault(this ModelStateDictionary state, string key,
        string? defaultValue = null)
    {
        return state.TryGetValue(key, out var entry) ? entry.AttemptedValue : defaultValue;
    }
}