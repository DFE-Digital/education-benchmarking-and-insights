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

    public static TheoryData<
        string,
        string,
        bool,
        Dimensions.ResultAsOptions,
        OverallPhaseTypes.OverallPhaseTypeFilter[],
        NurseryProvisions.NurseryProvisionFilter[],
        SpecialProvisions.SpecialProvisionFilter[],
        SixthFormProvisions.SixthFormProvisionFilter[]> FormValuesTestData => new()
    {
        { "f.", "", true, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [] },
        { "f.", "?f.filter=show", true, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [] },
        { "f.", "?f.filter=hide", false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [] },
        { "f.", "?f.as=0", true, Dimensions.ResultAsOptions.SpendPerPupil, [], [], [], [] },
        { "f.", "?f.as=1", true, Dimensions.ResultAsOptions.Actuals, [], [], [], [] },
        { "f.", "?f.as=2", true, Dimensions.ResultAsOptions.PercentExpenditure, [], [], [], [] },
        { "f.", "?f.as=3", true, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [] },
        {
            "f.", "?f.phase=0&f.phase=1&f.phase=2", true, Dimensions.ResultAsOptions.PercentIncome, [
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Special
            ],
            [], [], []
        },
        {
            "f.", "?f.phase=0&f.phase=1&f.as=1&other=value", true, Dimensions.ResultAsOptions.Actuals, [
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary
            ],
            [], [], []
        },
        { "f.", "?f.phase=0", true, Dimensions.ResultAsOptions.PercentIncome, [OverallPhaseTypes.OverallPhaseTypeFilter.Primary], [], [], [] },
        { "f.", "?f.nursery=1", true, Dimensions.ResultAsOptions.PercentIncome, [], [NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses], [], [] },
        { "f.", "?f.special=2", true, Dimensions.ResultAsOptions.PercentIncome, [], [], [SpecialProvisions.SpecialProvisionFilter.NotApplicable], [] },
        { "f.", "?f.sixth=3", true, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [SixthFormProvisions.SixthFormProvisionFilter.NotRecorded] }
    };

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
    [MemberData(nameof(FormValuesTestData))]
    public async Task ShouldReturnFormValuesFromQuery(
        string formPrefix,
        string query,
        bool expectedFiltersVisible,
        Dimensions.ResultAsOptions expectedResultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] expectedSelectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] expectedSelectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] expectedSelectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] expectedSelectedSixthFormProvisions)
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
        Assert.Equal(expectedFiltersVisible, model.FiltersVisible);
        Assert.Equal(expectedResultAs, model.ResultAs);
        Assert.Equal(expectedSelectedOverallPhases, model.SelectedOverallPhases);
        Assert.Equal(expectedSelectedNurseryProvisions, model.SelectedNurseryProvisions);
        Assert.Equal(expectedSelectedSpecialProvisions, model.SelectedSpecialProvisions);
        Assert.Equal(expectedSelectedSixthFormProvisions, model.SelectedSixthFormProvisions);
    }
}