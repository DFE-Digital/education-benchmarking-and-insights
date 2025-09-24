using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebUtilities;

namespace Web.App.Attributes;

public class PersistRedirectUriQueryAttribute : ActionFilterAttribute
{
    public const string RedirectUriQuery = "redirectUri";
    private string? _redirectUriValue;

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        _redirectUriValue = context.HttpContext.Request.Query[RedirectUriQuery];
        base.OnActionExecuting(context);
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if (!string.IsNullOrWhiteSpace(_redirectUriValue))
        {
            switch (context.Result)
            {
                case ViewResult view:
                    view.ViewData[RedirectUriQuery] = _redirectUriValue;
                    break;
                case RedirectResult redirect:
                    redirect.Url = QueryHelpers.AddQueryString(redirect.Url, RedirectUriQuery, _redirectUriValue);
                    break;
                case RedirectToActionResult redirect:
                    redirect.RouteValues ??= new RouteValueDictionary();
                    redirect.RouteValues[RedirectUriQuery] = _redirectUriValue;
                    break;
                case RedirectToRouteResult redirect:
                    redirect.RouteValues ??= new RouteValueDictionary();
                    redirect.RouteValues[RedirectUriQuery] = _redirectUriValue;
                    break;
            }
        }

        base.OnResultExecuting(context);
    }
}