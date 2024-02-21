using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EducationBenchmarking.Web.Extensions;

public static class HtmlHelperExtensions
{
    public static Task TrackedAnchorAsync(this IHtmlHelper htmlHelper, TrackedLinks link, string href, string content, string? hiddenContent = null, string? target = null, string[]? rel = null, params string[] classes)
    {
         classes = classes.Length == 0 ? ["govuk-link", "govuk-link--no-visited-state"] : classes;
         var viewModel = new TrackedAnchorViewModel(link.GetStringValue(), href, content, hiddenContent, target, rel, classes);
        return htmlHelper.RenderPartialAsync("_TrackedAnchor",viewModel);
    }
}