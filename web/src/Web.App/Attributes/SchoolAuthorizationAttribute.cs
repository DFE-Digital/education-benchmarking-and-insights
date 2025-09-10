using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Identity;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class SchoolAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        if (configuration.GetValue<bool>(EnvironmentVariables.DisableOrganisationClaimCheck))
        {
            return;
        }

        var urn = context.RouteData.Values["urn"]?.ToString();

        var isValid = context.HttpContext.User.Claims.Any(c => urn != null && c.Type == ClaimNames.Schools && c.Value == urn);
        if (!isValid)
        {
            context.Result = new ViewResult
            {
                ViewName = "../Error/Forbidden",
                StatusCode = 403
            };
        }
    }
}