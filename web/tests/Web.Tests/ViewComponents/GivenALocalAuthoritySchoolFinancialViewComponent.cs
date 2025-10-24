using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewComponents;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable")]
public class LocalAuthoritySchoolFinancialViewComponentTests
{
    private readonly LocalAuthoritySchoolFinancialViewComponent _component;
    private readonly HttpContext _httpContext;
    private readonly PathString _path = "/test/path";
    private readonly Mock<ILocalAuthoritiesApi> _localAuthorityApi = new();
    private readonly Fixture _fixture = new();
    private const Dimensions.ResultAsOptions DefaultDimension = Dimensions.ResultAsOptions.PercentIncome;
    private const string DefaultSort = "TotalExpenditure~desc";

    public LocalAuthoritySchoolFinancialViewComponentTests()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Path = _path;
        var viewContext = new ViewContext
        {
            HttpContext = _httpContext
        };
        _component = new LocalAuthoritySchoolFinancialViewComponent(_localAuthorityApi.Object)
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
        Dimensions.ResultAsOptions,
        TheoryFilters,
        string?> FormValuesTestData => new()
    {
        { "f.", "", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort },
        { "f.", "?f.filter=show", false, true, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort },
        { "f.", "?f.filter=hide", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort },
        { "f.", "?f.as=0", false, false, false, Dimensions.ResultAsOptions.SpendPerPupil, new TheoryFilters([], [], [], []), DefaultSort },
        { "f.", "?f.as=1", false, false, false, Dimensions.ResultAsOptions.Actuals, new TheoryFilters([], [], [], []), DefaultSort },
        { "f.", "?f.as=2", false, false, false, Dimensions.ResultAsOptions.PercentExpenditure, new TheoryFilters([], [], [], []), DefaultSort },
        { "f.", "?f.as=3", false, false, false, Dimensions.ResultAsOptions.PercentIncome, new TheoryFilters([], [], [], []), DefaultSort },
        {
            "f.", "?f.phase=0&f.phase=1&f.phase=2", false, false, true, DefaultDimension, new TheoryFilters([
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Special
            ],
            [], [], []), DefaultSort
        },
        {
            "f.", "?f.phase=0&f.phase=1&f.as=1&other=value", false, false, true, Dimensions.ResultAsOptions.Actuals, new TheoryFilters([
                OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                OverallPhaseTypes.OverallPhaseTypeFilter.Secondary
            ],
            [], [], []), DefaultSort
        },
        { "f.", "?f.phase=0", false, false, true, DefaultDimension, new TheoryFilters([OverallPhaseTypes.OverallPhaseTypeFilter.Primary], [], [], []), DefaultSort },
        { "f.", "?f.nursery=1", false, false, true, DefaultDimension, new TheoryFilters([], [NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses], [], []), DefaultSort },
        { "f.", "?f.special=2", false, false, true, DefaultDimension, new TheoryFilters([], [], [SpecialProvisions.SpecialProvisionFilter.NotApplicable], []), DefaultSort },
        { "f.", "?f.sixth=3", false, false, true, DefaultDimension, new TheoryFilters([], [], [], [SixthFormProvisions.SixthFormProvisionFilter.NotRecorded]), DefaultSort },
        { "f.", "?f.sort=SchoolName~asc", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), "SchoolName~asc" },
        { "f.", "?f.rows=all", true, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort }
    };

    public static TheoryData<
        string,
        int,
        string,
        List<QueryParameter>> ApiQueryTestData => new()
    {
        { "f.", 5, "", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5")] },
        { "f.", 5, "?f.as=1", [new QueryParameter("dimension", Dimensions.ResultAsOptions.Actuals.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5")] },
        { "f.", 5, "?f.sort=SchoolName~asc", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", "SchoolName"), new QueryParameter("sortOrder", "asc"), new QueryParameter("limit", "5")] },
        { "f.", 5, "?f.rows=all", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last())] },
        { "f.", 5, "?f.phase=0&f.phase=1", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"), new QueryParameter ( "overallPhase", "Primary" ), new QueryParameter ( "overallPhase", "Secondary" )] },
        { "f.", 5, "?f.nursery=1", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"), new QueryParameter ( "nurseryProvision", "No Nursery Classes" )] },
        { "f.", 5, "?f.special=2", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"), new QueryParameter ( "specialClassesProvision", "Not applicable" )] },
        { "f.", 5, "?f.sixth=3", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"), new QueryParameter ( "sixthFormProvision", "Not recorded" )] }
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
        bool expectedHasFilters,
        Dimensions.ResultAsOptions expectedResultAs,
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
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
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

    [Theory]
    [MemberData(nameof(ApiQueryTestData))]
    public async Task ShouldCallApiWithValuesFromQuery(
        string formPrefix,
        int maxRows,
        string query,
        List<QueryParameter> expectedQuery)
    {
        // arrange
        const string code = nameof(code);
        _httpContext.Request.QueryString = new QueryString(query);

        var rows = _fixture
            .Build<LocalAuthoritySchoolFinancial>()
            .CreateMany(maxRows)
            .ToArray();
        ApiQuery? actualQuery = null;
        _localAuthorityApi
            .Setup(l => l.GetSchoolsFinance(code, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .Callback<string, ApiQuery?, CancellationToken>((_, apiQuery, _) =>
            {
                actualQuery = apiQuery;
            })
            .ReturnsAsync(ApiResult.Ok(rows))
            .Verifiable();

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort) as ViewViewComponentResult;

        // assert
        _localAuthorityApi.Verify();
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolFinancialViewModel;
        Assert.NotNull(model);
        Assert.Equivalent(rows, model.Results);
        Assert.Equivalent(expectedQuery.Select(q => q), actualQuery?.Select(q => q), true);
    }

    public record TheoryFilters(
        OverallPhaseTypes.OverallPhaseTypeFilter[] Phases,
        NurseryProvisions.NurseryProvisionFilter[] NurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] SpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] SixthFormProvisions);
}