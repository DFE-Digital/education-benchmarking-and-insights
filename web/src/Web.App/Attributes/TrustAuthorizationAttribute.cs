using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TrustAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var companyNumber = context.RouteData.Values["companyNumber"]?.ToString();

        var isValid = context.HttpContext.User.Claims.Any(c => companyNumber != null && c.Type == "Trusts" && c.Value.Contains(companyNumber));
        if (!isValid)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}