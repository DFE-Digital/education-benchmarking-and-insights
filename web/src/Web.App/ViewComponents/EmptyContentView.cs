using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewComponents;
namespace Web.App.ViewComponents;

public class EmptyContentView() : HtmlContentViewComponentResult(new HtmlString(string.Empty));