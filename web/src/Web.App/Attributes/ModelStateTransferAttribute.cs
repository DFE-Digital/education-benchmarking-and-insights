using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Web.App.Middleware.ModelState;

namespace Web.App.Attributes;

// https://andrewlock.net/post-redirect-get-using-tempdata-in-asp-net-core/
public abstract class ModelStateTransferAttribute : ActionFilterAttribute
{
    protected const string Key = nameof(ModelStateTransferAttribute);
}

public class ExportModelStateAttribute : ModelStateTransferAttribute
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        // Only export when ModelState is not valid
        if (!filterContext.ModelState.IsValid)
        {
            // Export if we are redirecting
            if (filterContext.Result is RedirectResult or RedirectToRouteResult or RedirectToActionResult)
            {
                if (filterContext is { Controller: Controller controller, ModelState: not null })
                {
                    var modelState = ModelStateSerialiser.Serialise(filterContext.ModelState);
                    controller.TempData[Key] = modelState;
                }
            }
        }

        base.OnActionExecuted(filterContext);
    }
}

public class ImportModelStateAttribute : ModelStateTransferAttribute
{
    public override void OnActionExecuted(ActionExecutedContext filterContext)
    {
        var controller = filterContext.Controller as Controller;
        if (controller?.TempData[Key] is string serialisedModelState)
        {
            // Only Import if we are viewing
            if (filterContext.Result is ViewResult)
            {
                var modelState = ModelStateSerialiser.Deserialise(serialisedModelState);
                filterContext.ModelState.Merge(modelState);
            }
            else
            {
                // Otherwise remove it
                controller.TempData.Remove(Key);
            }
        }

        base.OnActionExecuted(filterContext);
    }
}