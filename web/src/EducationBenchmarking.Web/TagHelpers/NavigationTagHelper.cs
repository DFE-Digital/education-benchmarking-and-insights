using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SmartBreadcrumbs;
using System.Text.Encodings.Web;

namespace EducationBenchmarking.Web.TagHelpers;

[HtmlTargetElement("navigation")]
public class NavigationTagHelper(
    BreadcrumbManager breadcrumbManager, IUrlHelperFactory urlHelperFactory,
    IActionContextAccessor actionContextAccessor, HtmlEncoder htmlEncoder
) : BreadcrumbTagHelper(breadcrumbManager, urlHelperFactory, actionContextAccessor, htmlEncoder)
{
    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        if (ViewContext.ViewData[ViewDataConstants.Backlink] is BacklinkInfo backlinkInfo)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", backlinkInfo.Action);
            output.AddClass("govuk-back-link", HtmlEncoder.Default);
            output.Content.SetContent(backlinkInfo.Title);
        }
        else
        {
            await base.ProcessAsync(context, output);
        }
    }
}
public class BacklinkInfo(string? action, string title = "Back")
{
    public string Action { get; } = action ?? throw new ArgumentNullException(nameof(action));
    public string Title { get; } = title;
}