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
        bool,
        Dimensions.ResultAsOptions,
        OverallPhaseTypes.OverallPhaseTypeFilter[],
        NurseryProvisions.NurseryProvisionFilter[],
        SpecialProvisions.SpecialProvisionFilter[],
        SixthFormProvisions.SixthFormProvisionFilter[],
        string?> FormValuesTestData => new()
    {
        { "f.", "", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [], null },
        { "f.", "?f.filter=show", false, true, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [], null },
        { "f.", "?f.filter=hide", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [], null },
        { "f.", "?f.as=0", false, false, Dimensions.ResultAsOptions.SpendPerPupil, [], [], [], [], null },
        { "f.", "?f.as=1", false, false, Dimensions.ResultAsOptions.Actuals, [], [], [], [], null },
        { "f.", "?f.as=2", false, false, Dimensions.ResultAsOptions.PercentExpenditure, [], [], [], [], null },
        { "f.", "?f.as=3", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [], null },
        {
            "f.", "?f.phase=0&f.phase=1&f.phase=2", false, false, Dimensions.ResultAsOptions.PercentIncome, [
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Special
            ],
            [], [], [], null
        },
        {
            "f.", "?f.phase=0&f.phase=1&f.as=1&other=value", false, false, Dimensions.ResultAsOptions.Actuals, [
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary
            ],
            [], [], [], null
        },
        { "f.", "?f.phase=0", false, false, Dimensions.ResultAsOptions.PercentIncome, [OverallPhaseTypes.OverallPhaseTypeFilter.Primary], [], [], [], null },
        { "f.", "?f.nursery=1", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses], [], [], null },
        { "f.", "?f.special=2", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [SpecialProvisions.SpecialProvisionFilter.NotApplicable], [], null },
        { "f.", "?f.sixth=3", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [SixthFormProvisions.SixthFormProvisionFilter.NotRecorded], null },
        { "f.", "?f.sort=SchoolName~asc", false, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [], "SchoolName~asc" },
        { "f.", "?f.rows=all", true, false, Dimensions.ResultAsOptions.PercentIncome, [], [], [], [], null }
    };

    [Fact]
    public async Task ShouldReturnPassThroughValues()
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
        Assert.NotNull(model);
        Assert.Equal(code, model.Code);
        Assert.Equal(formPrefix, model.FormPrefix);
        Assert.Equal(maxRows, model.MaxRows);
    }

    [Theory]
    [MemberData(nameof(FormValuesTestData))]
    public async Task ShouldReturnFormValuesFromQuery(
        string formPrefix,
        string query,
        bool expectedAllRows,
        bool expectedFiltersVisible,
        Dimensions.ResultAsOptions expectedResultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] expectedSelectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] expectedSelectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] expectedSelectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] expectedSelectedSixthFormProvisions,
        string? expectedSort)
    {
        // arrange
        const string code = nameof(code);
        const int maxRows = 3;
        _httpContext.Request.QueryString = new QueryString(query);

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedAllRows, model.AllRows);
        Assert.Equal(expectedFiltersVisible, model.FiltersVisible);
        Assert.Equal(expectedResultAs, model.ResultAs);
        Assert.Equal(expectedSelectedOverallPhases, model.SelectedOverallPhases);
        Assert.Equal(expectedSelectedNurseryProvisions, model.SelectedNurseryProvisions);
        Assert.Equal(expectedSelectedSpecialProvisions, model.SelectedSpecialProvisions);
        Assert.Equal(expectedSelectedSixthFormProvisions, model.SelectedSixthFormProvisions);
        Assert.Equal(expectedSort, model.Sort);
    }
}