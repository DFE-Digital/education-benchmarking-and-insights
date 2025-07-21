using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Identity;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TrustAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        if (configuration.GetValue<bool>(EnvironmentVariables.DisableOrganisationClaimCheck))
        {
            return;
        }

        var companyNumber = context.RouteData.Values["companyNumber"]?.ToString();

        var isValid = context.HttpContext.User.Claims.Any(c =>
            companyNumber != null && c.Type == ClaimNames.Trusts && c.Value.Contains(companyNumber));
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