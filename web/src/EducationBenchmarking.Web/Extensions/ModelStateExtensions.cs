using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EducationBenchmarking.Web.Extensions;

[ExcludeFromCodeCoverage]
public static class ModelStateExtensions
{
    public static bool HasError(this ModelStateDictionary state, string key)
    {
        if (state.TryGetValue(key, out var entry))
            return entry.ValidationState == ModelValidationState.Invalid;

        return false;
    }
}