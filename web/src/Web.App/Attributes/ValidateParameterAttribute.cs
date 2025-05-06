using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateParameterAttribute(string argumentName, string friendlyName, Regex regex) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (filterContext.ActionArguments.TryGetValue(argumentName, out var value))
        {
            var parsed = value?.ToString();
            if (!string.IsNullOrWhiteSpace(parsed) && !regex.IsMatch(parsed))
            {
                filterContext.Result = new JsonResult(new ValidationResult($"Invalid {friendlyName}", [argumentName]))
                {
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        base.OnActionExecuting(filterContext);
    }
}

public static partial class ValidateParameterAttributeRegex
{
    [GeneratedRegex(@"^\d{6}$")]
    public static partial Regex UrnRegex();
}

public class ValidateUrnAttribute() : ValidateParameterAttribute("urn", "URN", ValidateParameterAttributeRegex.UrnRegex());