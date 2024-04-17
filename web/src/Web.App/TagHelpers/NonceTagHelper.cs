using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Web.App.TagHelpers;

[HtmlTargetElement("script", Attributes = "add-nonce")]
public class NonceTagHelper : TagHelper
{
    [ViewContext]
    public ViewContext? ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var attributes = context.AllAttributes["add-nonce"];

        if (bool.TryParse(attributes.Value.ToString(), out var shouldAdd) && shouldAdd)
        {
            output.Attributes.Remove(attributes);
            output.Attributes.Add(new TagHelperAttribute("nonce", ViewContext?.HttpContext.Items["csp-nonce"]));
        }
    }
}