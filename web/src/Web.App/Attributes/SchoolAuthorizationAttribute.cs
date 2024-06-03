using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement;
using Web.App.Identity;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SchoolAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public async void OnAuthorization(AuthorizationFilterContext context)
    {

        var featureManager = context.HttpContext.RequestServices.GetRequiredService<IFeatureManager>();
        if (await featureManager.IsEnabledAsync(FeatureFlags.DisableOrganisationClaimCheck))
        {
            return;
        }

        var urn = context.RouteData.Values["urn"]?.ToString();

        var isValid = context.HttpContext.User.Claims.Any(c => urn != null && c.Type == ClaimNames.Schools && c.Value == urn);
        if (!isValid)
        {
            context.Result = new UnauthorizedResult();
        }
    }
}