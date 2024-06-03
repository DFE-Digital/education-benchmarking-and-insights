using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement;
using Web.App.Identity;

namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TrustAuthorizationAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public async void OnAuthorization(AuthorizationFilterContext context)
    {
        var featureManager = context.HttpContext.RequestServices.GetRequiredService<IFeatureManager>();
        if (await featureManager.IsEnabledAsync(FeatureFlags.DisableOrganisationClaimCheck))
        {
            return;
        }

        var companyNumber = context.RouteData.Values["companyNumber"]?.ToString();

        var isValid = context.HttpContext.User.Claims.Any(c =>
            companyNumber != null && c.Type == ClaimNames.Trusts && c.Value.Contains(companyNumber));
        if (!isValid)
        {
            context.Result = new UnauthorizedResult();
        }
    }

}