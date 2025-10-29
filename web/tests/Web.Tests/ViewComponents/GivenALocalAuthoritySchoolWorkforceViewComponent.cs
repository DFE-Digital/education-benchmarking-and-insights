using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Web.App.Domain;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewComponents;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable")]
public class LocalAuthoritySchoolWorkforceViewComponentTests
{
    private readonly LocalAuthoritySchoolWorkforceViewComponent _component;
    private readonly HttpContext _httpContext;
    private readonly PathString _path = "/test/path";
    private const SchoolsSummaryWorkforceDimensions.ResultAsOptions DefaultDimension = SchoolsSummaryWorkforceDimensions.ResultAsOptions.PercentPupil;
    private const string DefaultSort = "TotalExpenditure~desc";

    public LocalAuthoritySchoolWorkforceViewComponentTests()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Path = _path;
        var viewContext = new ViewContext
        {
            HttpContext = _httpContext
        };
        _component = new LocalAuthoritySchoolWorkforceViewComponent
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
        bool,
        SchoolsSummaryWorkforceDimensions.ResultAsOptions,
        TheoryFilters,
        string?> FormValuesTestData => new()
    {
        { "w.", "", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort },
        { "w.", "?w.filter=show", false, true, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort },
        { "w.", "?w.filter=hide", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort },
        { "w.", "?w.as=0", false, false, false, SchoolsSummaryWorkforceDimensions.ResultAsOptions.PercentPupil, new TheoryFilters([], [], [], []), DefaultSort },
        { "w.", "?w.as=1", false, false, false, SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals, new TheoryFilters([], [], [], []), DefaultSort },
        {
            "w.", "?w.phase=0&w.phase=1&w.phase=2", false, false, true, DefaultDimension, new TheoryFilters([
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Special
            ],
            [], [], []), DefaultSort
        },
        {
            "w.", "?w.phase=0&w.phase=1&w.as=1&other=value", false, false, true, SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals, new TheoryFilters([
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary
            ],
            [], [], []), DefaultSort
        },
        { "w.", "?w.phase=0", false, false, true, DefaultDimension, new TheoryFilters([OverallPhaseTypes.OverallPhaseTypeFilter.Primary], [], [], []), DefaultSort },
        { "w.", "?w.nursery=1", false, false, true, DefaultDimension, new TheoryFilters([], [NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses], [], []), DefaultSort },
        { "w.", "?w.special=2", false, false, true, DefaultDimension, new TheoryFilters([], [], [SpecialProvisions.SpecialProvisionFilter.NotApplicable], []), DefaultSort },
        { "w.", "?w.sixth=3", false, false, true, DefaultDimension, new TheoryFilters([], [], [], [SixthFormProvisions.SixthFormProvisionFilter.NotRecorded]), DefaultSort },
        { "w.", "?w.sort=SchoolName~asc", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), "SchoolName~asc" },
        { "w.", "?w.rows=all", true, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort }
    };

    [Fact]
    public async Task ShouldReturnPassThroughValues()
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceViewModel;
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
        bool expectedHasFilters,
        SchoolsSummaryWorkforceDimensions.ResultAsOptions expectedResultAs,
        TheoryFilters expectedFilters,
        string? expectedSort)
    {
        // arrange
        const string code = nameof(code);
        const int maxRows = 3;
        _httpContext.Request.QueryString = new QueryString(query);
        var (expectedSelectedOverallPhases,
            expectedSelectedNurseryProvisions,
            expectedSelectedSpecialProvisions,
            expectedSelectedSixthFormProvisions) = expectedFilters;

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedAllRows, model.AllRows);
        Assert.Equal(expectedFiltersVisible, model.FiltersVisible);
        Assert.Equal(expectedHasFilters, model.HasFilters);
        Assert.Equal(expectedResultAs, model.ResultAs);
        Assert.Equal(expectedSelectedOverallPhases, model.SelectedOverallPhases);
        Assert.Equal(expectedSelectedNurseryProvisions, model.SelectedNurseryProvisions);
        Assert.Equal(expectedSelectedSpecialProvisions, model.SelectedSpecialProvisions);
        Assert.Equal(expectedSelectedSixthFormProvisions, model.SelectedSixthFormProvisions);
        Assert.Equal(expectedSort, model.Sort);
    }

    //TODO: add test to cover assertions for api calls

    public record TheoryFilters(
        OverallPhaseTypes.OverallPhaseTypeFilter[] Phases,
        NurseryProvisions.NurseryProvisionFilter[] NurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] SpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] SixthFormProvisions);
}