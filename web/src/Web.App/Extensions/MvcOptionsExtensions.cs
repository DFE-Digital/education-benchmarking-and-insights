using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace Web.App.Extensions;

public static partial class MvcOptionsExtensions
{
    public static void SetModelBindingOptions(this MvcOptions options)
    {
        options.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(SetAttemptedValueIsInvalidAccessor);
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