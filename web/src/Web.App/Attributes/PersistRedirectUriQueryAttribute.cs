using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Web.App.Attributes;

public class PersistRedirectUriQueryAttribute : ActionFilterAttribute
{
    public const string RedirectUriQuery = "redirectUri";
    private string? _testing;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _testing = context.HttpContext.Request.Query[RedirectUriQuery];
        base.OnActionExecuting(context);
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!string.IsNullOrWhiteSpace(_testing))
        {
            switch (context.Result)
            {
                case ViewResult view:
                    view.ViewData[RedirectUriQuery] = _testing;
                    break;
                case RedirectToActionResult redirect:
                    redirect.RouteValues ??= new RouteValueDictionary();
                    redirect.RouteValues[RedirectUriQuery] = _testing;
                    break;
                case RedirectToRouteResult redirect:
                    redirect.RouteValues ??= new RouteValueDictionary();
                    redirect.RouteValues[RedirectUriQuery] = _testing;
                    break;
            }
        }

        base.OnResultExecuting(context);
    }
}