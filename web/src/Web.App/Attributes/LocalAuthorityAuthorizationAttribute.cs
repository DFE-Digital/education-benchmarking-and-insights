using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Extensions;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class LocalAuthorityAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var code = context.RouteData.Values["code"]?.ToString();

        var isValid = context.HttpContext.User.HasLocalAuthorityAuthorisation(code, configuration);
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
