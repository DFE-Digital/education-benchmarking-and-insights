using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Web.App.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent TrackedAnchor(this IHtmlHelper htmlHelper, TrackedLinks link, string href, string content,
            string? hiddenContent = null, string? target = null, string[]? rel = null, params string[] classes)
        {
            var anchorTag = new TagBuilder("a");

            classes = classes.Length == 0 ? ["govuk-link", "govuk-link--no-visited-state"] : classes;

            anchorTag.AddCssClass(string.Join(" ", classes));
            anchorTag.InnerHtml.Append(content);
            anchorTag.MergeAttribute("href", href);
            anchorTag.MergeAttribute("data-id", link.GetStringValue());

            if (rel is not null && rel.Length > 0)
            {
                anchorTag.MergeAttribute("rel", string.Join(" ", rel));
            }

            if (!string.IsNullOrEmpty(target))
            {
                anchorTag.MergeAttribute("target", target);
            }

            if (!string.IsNullOrEmpty(hiddenContent))
            {
                anchorTag.InnerHtml.AppendHtml($"<span class=\"govuk-visually-hidden\"> {hiddenContent}</span>");
            }

            return anchorTag;
        }
    }
}