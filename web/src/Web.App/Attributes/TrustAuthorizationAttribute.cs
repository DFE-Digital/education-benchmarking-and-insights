using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Extensions;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TrustAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var companyNumber = context.RouteData.Values["companyNumber"]?.ToString();

        var isValid = context.HttpContext.User.HasTrustAuthorisation(companyNumber, configuration);
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