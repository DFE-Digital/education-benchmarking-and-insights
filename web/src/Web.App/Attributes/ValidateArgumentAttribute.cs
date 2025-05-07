using System.Net;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidateArgumentAttribute(string argumentName, Regex regex) : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.TryGetValue(argumentName, out var value))
        {
            var parsed = value?.ToString();

            // Only attempt to validate if non-null or empty value. This _should_ always be the case,
            // otherwise ASP.NET action binding would fail and a `404` would be served by ASP.NET anyway.
            if (!string.IsNullOrWhiteSpace(parsed) && !regex.IsMatch(parsed))
            {
                context.Result = new ViewResult
                {
                    ViewName = "../Error/NotFound",
                    StatusCode = (int)HttpStatusCode.BadRequest
                };
            }
        }

        base.OnActionExecuting(context);
    }
}

public static partial class ValidateArgumentAttributeRegex
{
    [GeneratedRegex(@"^\d{6}$")]
    public static partial Regex UrnRegex();

    [GeneratedRegex(@"^\d{8}$")]
    public static partial Regex CompanyNumberRegex();
}

public class ValidateUrnAttribute() : ValidateArgumentAttribute("urn", ValidateArgumentAttributeRegex.UrnRegex());
public class ValidateCompanyNumberAttribute() : ValidateArgumentAttribute("companyNumber", ValidateArgumentAttributeRegex.CompanyNumberRegex());
