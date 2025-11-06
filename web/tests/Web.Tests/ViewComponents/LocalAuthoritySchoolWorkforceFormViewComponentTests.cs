using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Newtonsoft.Json;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewComponents;

[SuppressMessage("Usage", "xUnit1045:Avoid using TheoryData type arguments that might not be serializable")]
public class LocalAuthoritySchoolWorkforceFormViewComponentTests
{
    private const SchoolsSummaryWorkforceDimensions.ResultAsOptions DefaultDimension = SchoolsSummaryWorkforceDimensions.ResultAsOptions.PercentPupil;
    private const string DefaultSort = "PupilTeacherRatio~desc";
    private readonly LocalAuthoritySchoolWorkforceFormViewComponent _component;
    private readonly Mock<IHttpContextAccessor> _contextAccessor = new();
    private readonly Fixture _fixture = new();
    private readonly HttpContext _httpContext;
    private readonly Mock<ILocalAuthoritiesApi> _localAuthorityApi = new();
    private readonly PathString _path = "/test/path";

    public LocalAuthoritySchoolWorkforceFormViewComponentTests()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Path = _path;
        _contextAccessor.Setup(a => a.HttpContext).Returns(_httpContext);
        _component = new LocalAuthoritySchoolWorkforceFormViewComponent(_contextAccessor.Object, _localAuthorityApi.Object);
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
                [], [], []),
            DefaultSort
        },
        {
            "w.", "?w.phase=0&w.phase=1&w.as=1&other=value", false, false, true, SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals, new TheoryFilters([
                    OverallPhaseTypes.OverallPhaseTypeFilter.Primary,
                    OverallPhaseTypes.OverallPhaseTypeFilter.Secondary
                ],
                [], [], []),
            DefaultSort
        },
        { "w.", "?w.phase=0", false, false, true, DefaultDimension, new TheoryFilters([OverallPhaseTypes.OverallPhaseTypeFilter.Primary], [], [], []), DefaultSort },
        { "w.", "?w.nursery=1", false, false, true, DefaultDimension, new TheoryFilters([], [NurseryProvisions.NurseryProvisionFilter.HasNoNurseryClasses], [], []), DefaultSort },
        { "w.", "?w.special=2", false, false, true, DefaultDimension, new TheoryFilters([], [], [SpecialProvisions.SpecialProvisionFilter.NotApplicable], []), DefaultSort },
        { "w.", "?w.sixth=3", false, false, true, DefaultDimension, new TheoryFilters([], [], [], [SixthFormProvisions.SixthFormProvisionFilter.NotRecorded]), DefaultSort },
        { "w.", "?w.sort=SchoolName~asc", false, false, false, DefaultDimension, new TheoryFilters([], [], [], []), "SchoolName~asc" },
        { "w.", "?w.rows=all", true, false, false, DefaultDimension, new TheoryFilters([], [], [], []), DefaultSort }
    };

    public static TheoryData<
        string,
        int,
        string,
        List<QueryParameter>> ApiQueryTestData => new()
    {
        { "w.", 5, "", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5")] },
        {
            "w.", 5, "?w.as=1", [
                new QueryParameter("dimension", SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()),
                new QueryParameter("limit", "5")
            ]
        },
        { "w.", 5, "?w.sort=SchoolName~asc", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", "SchoolName"), new QueryParameter("sortOrder", "asc"), new QueryParameter("limit", "5")] },
        { "w.", 5, "?w.rows=all", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last())] },
        {
            "w.", 5, "?w.phase=0&w.phase=1", [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("overallPhase", "Primary"), new QueryParameter("overallPhase", "Secondary")
            ]
        },
        {
            "w.", 5, "?w.nursery=1", [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("nurseryProvision", "No Nursery Classes")
            ]
        },
        {
            "w.", 5, "?w.special=2", [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("specialClassesProvision", "Not applicable")
            ]
        },
        {
            "w.", 5, "?w.sixth=3", [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("sixthFormProvision", "Not recorded")
            ]
        }
    };

    [Fact]
    public async Task ShouldReturnPassThroughValues()
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;
        const string otherFormPrefix = nameof(otherFormPrefix);
        const string tabId = nameof(tabId);

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(code, model.Code);
        Assert.Equal(formPrefix, model.FormPrefix);
        Assert.Equal(maxRows, model.MaxRows);
        Assert.Equal(tabId, model.TabId);
    }

    [Theory]
    [InlineData("/path", "/path")]
    [InlineData(null, "")]
    public async Task ShouldReturnExpectedPath(string? path, string expectedPath)
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;
        const string otherFormPrefix = nameof(otherFormPrefix);
        const string tabId = nameof(tabId);

        _contextAccessor.Reset();
        if (path != null)
        {
            _httpContext.Request.Path = path;
            _contextAccessor.Setup(a => a.HttpContext).Returns(_httpContext);
        }

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedPath, model.Path);
    }

    [Theory]
    [InlineData("?key=value", """[{"key":"key","value":["value"]}]""")]
    [InlineData(null, "[]")]
    public async Task ShouldReturnExpectedQuery(string? query, string? expectedQuery)
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;
        const string otherFormPrefix = nameof(otherFormPrefix);
        const string tabId = nameof(tabId);

        _contextAccessor.Reset();
        if (query != null)
        {
            _httpContext.Request.Path = _path;
            _httpContext.Request.QueryString = QueryString.FromUriComponent(query);
            _contextAccessor.Setup(a => a.HttpContext).Returns(_httpContext);
        }

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedQuery, model.Query.ToJson(Formatting.None));
    }

    [Theory]
    [InlineData("?x.a=1&x.b=2&y.c=3&y.d=4&z.e=5", "x.", """{"x.a":["1"],"x.b":["2"]}""")]
    public async Task ShouldReturnExpectedOtherFormValues(string query, string otherFormPrefix, string? expectedOtherFormValues)
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;
        const string tabId = nameof(tabId);
        _httpContext.Request.QueryString = QueryString.FromUriComponent(query);

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedOtherFormValues, model.OtherFormValues.ToJson(Formatting.None));
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData("tab-id", "#tab-id")]
    public async Task ShouldReturnExpectedFragment(string? tabId, string? expectedFragment)
    {
        // arrange
        const string code = nameof(code);
        const string formPrefix = nameof(formPrefix);
        const int maxRows = 123;
        const string otherFormPrefix = nameof(otherFormPrefix);

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId ?? string.Empty) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedFragment, model.Fragment);
    }

    [Theory]
    [InlineData("123", "?f.other=keep&w.sort=TotalPupils~desc&w.filter=show&w.as=1&w.phase=drop", "w.", "f.", """{"code":"123","w.filter":"show","w.as":1,"w.sort":"TotalPupils~desc","f.other":["keep"]}""")]
    [InlineData("123", "?w.sort=TotalPupils~desc&w.filter=show&w.as=1&w.phase=drop", "w.", "f.", """{"code":"123","w.filter":"show","w.as":1,"w.sort":"TotalPupils~desc"}""")]
    [InlineData("123", "?f.other=drop&w.sort=TotalPupils~desc&w.filter=show&w.as=1&w.phase=drop", "w.", "x.", """{"code":"123","w.filter":"show","w.as":1,"w.sort":"TotalPupils~desc"}""")]
    public async Task ShouldReturnExpectedRouteValuesOnClear(string code, string query, string formPrefix, string otherFormPrefix, string? expectedRouteValues)
    {
        // arrange
        const int maxRows = 123;
        const string tabId = nameof(tabId);
        _httpContext.Request.QueryString = QueryString.FromUriComponent(query);

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedRouteValues, model.RouteValuesOnClear.ToJson(Formatting.None));
    }

    [Theory]
    [InlineData("?f.as=0", "f.", "EHC plan and SEN support data are shown as percentages of total pupils.")]
    [InlineData("?f.as=1", "f.", "")]
    public async Task ShouldReturnExpectedDimensionCommentary(string query, string formPrefix, string expected)
    {
        // arrange
        const string code = nameof(code);
        const int maxRows = 123;
        const string otherFormPrefix = nameof(otherFormPrefix);
        const string tabId = nameof(tabId);
        _httpContext.Request.QueryString = QueryString.FromUriComponent(query);

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(expected, model.DimensionCommentary);
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
        const string otherFormPrefix = nameof(otherFormPrefix);
        const string tabId = nameof(tabId);
        _httpContext.Request.QueryString = new QueryString(query);
        var (expectedSelectedOverallPhases,
            expectedSelectedNurseryProvisions,
            expectedSelectedSpecialProvisions,
            expectedSelectedSixthFormProvisions) = expectedFilters;

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
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
        const string otherFormPrefix = nameof(otherFormPrefix);
        const string tabId = nameof(tabId);
        _httpContext.Request.QueryString = new QueryString(query);

        var rows = _fixture
            .Build<LocalAuthoritySchoolWorkforce>()
            .CreateMany(maxRows)
            .ToArray();
        ApiQuery? actualQuery = null;
        _localAuthorityApi
            .Setup(l => l.GetSchoolsWorkforce(code, It.IsAny<ApiQuery?>(), It.IsAny<CancellationToken>()))
            .Callback<string, ApiQuery?, CancellationToken>((_, apiQuery, _) =>
            {
                actualQuery = apiQuery;
            })
            .ReturnsAsync(ApiResult.Ok(rows))
            .Verifiable();

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, otherFormPrefix, tabId) as ViewViewComponentResult;

        // assert
        _localAuthorityApi.Verify();
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
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