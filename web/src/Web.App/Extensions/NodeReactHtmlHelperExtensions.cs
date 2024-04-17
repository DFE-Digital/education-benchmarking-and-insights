using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using NodeReact;
using NodeReact.AspNetCore;

namespace Web.App.Extensions;

// Customised version of
// https://github.com/DaniilSokolyuk/NodeReact.NET/blob/master/NodeReact/AspNetCore/HtmlHelperExtensions.cs
// to resolve the CSP nonce from context instead of `ScriptNonceProvider`, which is unable to get the Context
public static class NodeReactHtmlHelperExtensions
{
    /// <summary>
    /// Renders the JavaScript required to initialise all components client-side. This will
    /// attach event handlers to the server-rendered HTML.
    /// </summary>
    /// <returns>JavaScript for all components</returns>
    public static IHtmlContent ReactInitJavaScript(this IHtmlHelper htmlHelper, bool delayedLambda = false)
    {
        var scopedContext =
            htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<IReactScopedContext>();
        var config = htmlHelper.ViewContext.HttpContext.RequestServices.GetRequiredService<ReactConfiguration>();

        return new ActionHtmlString(writer => WriteScriptTag(writer,
            bodyWriter => scopedContext.GetInitJavaScript(bodyWriter),
            config.ScriptNonceProvider ??
            (() => htmlHelper.ViewContext?.HttpContext.Items["csp-nonce"]?.ToString() ?? ""), delayedLambda));
    }

    private static void WriteScriptTag(TextWriter writer, Action<TextWriter> bodyWriter,
        Func<string> scriptNonceProvider, bool delayedLambda = false)
    {
        writer.Write("<script");
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (scriptNonceProvider != null)
        {
            writer.Write(" nonce=\"");
            writer.Write(scriptNonceProvider());
            writer.Write("\"");
        }

        writer.Write(">");

        if (delayedLambda)
        {
            writer.Write("window.ReactJsAsyncInit = function() {");
        }

        bodyWriter(writer);

        if (delayedLambda)
        {
            writer.Write("};");
        }

        writer.Write("</script>");
    }
}