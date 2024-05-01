using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Identity;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SchoolAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var urn = context.RouteData.Values["urn"]?.ToString();

        var isValid = context.HttpContext.User.Claims.Any(c => urn != null && c.Type == ClaimNames.Schools && c.Value == urn);
        if (!isValid)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}