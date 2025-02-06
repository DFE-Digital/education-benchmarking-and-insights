using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Web.App.ViewModels.ModelBinders;

namespace Web.App.Extensions;

public static partial class MvcOptionsExtensions
{
    public static void SetModelBindingOptions(this MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(SetAttemptedValueIsInvalidAccessor);

        // add custom binder provider that assigns custom binders on a per-Type basis
        var fallbackProvider = options.ModelBinderProviders.First(p => p is ComplexObjectModelBinderProvider);
        var customProvider = new CustomModelBinderProvider(fallbackProvider);
        options.ModelBinderProviders.Insert(0, customProvider);
    }

    internal static string SetAttemptedValueIsInvalidAccessor(string value, string field)
    {
        var matches = UppercaseRegex().Matches(field);
        var mutatedField = new StringBuilder(field);
        foreach (Match match in matches)
        {
            mutatedField[match.Index] = match.Value.ToLowerInvariant().First();
        }

        return $"Enter {mutatedField} in the correct format";
    }

    [GeneratedRegex("(?<![A-Z])[A-Z](?![A-Z])")]
    private static partial Regex UppercaseRegex();
}