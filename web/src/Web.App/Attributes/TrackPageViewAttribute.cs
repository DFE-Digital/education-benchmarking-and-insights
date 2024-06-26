using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Web.App.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class TrackPageViewAttribute : TypeFilterAttribute
{
    public TrackPageViewAttribute(string pageName, params string[] properties) : base(typeof(TrackPageViewFilter))
    {
        Arguments =
        [
            pageName,
            properties
        ];
    }
}

public class TrackPageViewFilter(ILogger<TrackPageViewFilter> logger, string pageName, string[] properties) : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var message = new StringBuilder("Loaded page {pageName}");
        var args = new List<object>(new[] { pageName });

        if (properties.Length > 0)
        {
            message.Append(" with ");
        }

        foreach (var property in properties)
        {
            message.Append(property);
            message.Append(" {");
            message.Append(property);
            message.Append("},");
            args.Add(context.RouteData.Values[property] ?? string.Empty);
        }

#pragma warning disable CA2254
        logger.LogInformation(message.ToString().TrimEnd(','), args.ToArray());
#pragma warning restore CA2254
        base.OnActionExecuted(context);
    }
}