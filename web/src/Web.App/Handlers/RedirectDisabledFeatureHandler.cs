using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;

namespace Web.App.Handlers;

public class RedirectDisabledFeatureHandler : IDisabledFeaturesHandler
{
    public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
    {
        context.Result = context.Result = new ViewResult
        {
            ViewName = "../Error/FeatureDisabled",
            StatusCode = 403
        };
        return Task.CompletedTask;
    }
}