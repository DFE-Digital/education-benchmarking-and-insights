using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewComponents;

public class LocalAuthoritySchoolFinancialViewComponentTests
{
    private readonly LocalAuthoritySchoolFinancialViewComponent _component;
    private readonly PathString _path = "/test/path";

    public LocalAuthoritySchoolFinancialViewComponentTests()
    {
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Path = _path
            }
        };
        var viewContext = new ViewContext
        {
            HttpContext = httpContext
        };
        _component = new LocalAuthoritySchoolFinancialViewComponent
        {
            ViewComponentContext = new ViewComponentContext
            {
                ViewContext = viewContext
            }
        };
    }

    [Fact]
    public async Task ShouldReturnCode()
    {
        // arrange
        const string code = nameof(code);

        // act
        var result = await _component.InvokeAsync(code) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
        Assert.NotNull(model);
        Assert.Equal(code, model.Code);
    }
}