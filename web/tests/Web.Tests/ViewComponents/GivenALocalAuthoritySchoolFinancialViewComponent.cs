using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewComponents;

public class LocalAuthoritySchoolFinancialViewComponentTests
{
    private readonly LocalAuthoritySchoolFinancialViewComponent _component;
    private readonly HttpContext _httpContext;
    private readonly PathString _path = "/test/path";

    public LocalAuthoritySchoolFinancialViewComponentTests()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Path = _path;
        var viewContext = new ViewContext
        {
            HttpContext = _httpContext
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
    public async Task ShouldReturnCodeAndFormPrefix()
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);

        // act
        var result = await _component.InvokeAsync(code, formPrefix) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
        Assert.NotNull(model);
        Assert.Equal(code, model.Code);
        Assert.Equal(formPrefix, model.FormPrefix);
    }

    [Theory]
    [InlineData("f.", "", new OverallPhaseTypes.OverallPhaseTypeFilter[0], Dimensions.ResultAsOptions.SpendPerPupil)]
    [InlineData("f.", "?f.as=0", new OverallPhaseTypes.OverallPhaseTypeFilter[0], Dimensions.ResultAsOptions.SpendPerPupil)]
    [InlineData("f.", "?f.as=1", new OverallPhaseTypes.OverallPhaseTypeFilter[0], Dimensions.ResultAsOptions.Actuals)]
    [InlineData("f.", "?f.as=2", new OverallPhaseTypes.OverallPhaseTypeFilter[0], Dimensions.ResultAsOptions.PercentExpenditure)]
    [InlineData("f.", "?f.as=3", new OverallPhaseTypes.OverallPhaseTypeFilter[0], Dimensions.ResultAsOptions.PercentIncome)]
    [InlineData("f.", "?f.phase=0", new[] { OverallPhaseTypes.OverallPhaseTypeFilter.Primary }, Dimensions.ResultAsOptions.SpendPerPupil)]
    [InlineData("f.", "?f.phase=0&f.phase=1&f.phase=2", new[] { OverallPhaseTypes.OverallPhaseTypeFilter.Primary, OverallPhaseTypes.OverallPhaseTypeFilter.Secondary, OverallPhaseTypes.OverallPhaseTypeFilter.Special }, Dimensions.ResultAsOptions.SpendPerPupil)]
    [InlineData("f.", "?f.phase=0&f.phase=1&f.as=1&other=value", new[] { OverallPhaseTypes.OverallPhaseTypeFilter.Primary, OverallPhaseTypes.OverallPhaseTypeFilter.Secondary }, Dimensions.ResultAsOptions.Actuals)]
    public async Task ShouldReturnFormValuesFromQuery(string formPrefix, string query, OverallPhaseTypes.OverallPhaseTypeFilter[] expectedSelectedOverallPhases, Dimensions.ResultAsOptions expectedResultAs)
    {
        // arrange
        const string code = nameof(code);
        _httpContext.Request.QueryString = new QueryString(query);

        // act
        var result = await _component.InvokeAsync(code, formPrefix) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedSelectedOverallPhases, model.SelectedOverallPhases);
        Assert.Equal(expectedResultAs, model.ResultAs);
    }
}