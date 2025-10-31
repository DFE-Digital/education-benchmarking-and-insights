using System.Diagnostics.CodeAnalysis;
using AutoFixture;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Primitives;
using Moq;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
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
    private readonly Fixture _fixture = new();
    private readonly HttpContext _httpContext;
    private readonly Mock<ILocalAuthoritiesApi> _localAuthorityApi = new();
    private readonly PathString _path = "/test/path";

    public LocalAuthoritySchoolWorkforceFormViewComponentTests()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Path = _path;
        var viewContext = new ViewContext
        {
            HttpContext = _httpContext
        };
        _component = new LocalAuthoritySchoolWorkforceFormViewComponent(_localAuthorityApi.Object)
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
            "w.", 5, "?w.as=1",
            [
                new QueryParameter("dimension", SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()),
                new QueryParameter("limit", "5")
            ]
        },
        { "w.", 5, "?w.sort=SchoolName~asc", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", "SchoolName"), new QueryParameter("sortOrder", "asc"), new QueryParameter("limit", "5")] },
        { "w.", 5, "?w.rows=all", [new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last())] },
        {
            "w.", 5, "?w.phase=0&w.phase=1",
            [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("overallPhase", "Primary"), new QueryParameter("overallPhase", "Secondary")
            ]
        },
        {
            "w.", 5, "?w.nursery=1",
            [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("nurseryProvision", "No Nursery Classes")
            ]
        },
        {
            "w.", 5, "?w.special=2",
            [
                new QueryParameter("dimension", DefaultDimension.GetQueryParam()), new QueryParameter("sortField", DefaultSort.Split("~").First()), new QueryParameter("sortOrder", DefaultSort.Split("~").Last()), new QueryParameter("limit", "5"),
                new QueryParameter("specialClassesProvision", "Not applicable")
            ]
        },
        {
            "w.", 5, "?w.sixth=3",
            [
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
        const string resetFieldName = nameof(resetFieldName);
        const string otherFormFieldName = nameof(otherFormFieldName);
        var otherFormValues = new Dictionary<string, StringValues>();

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, resetFieldName, otherFormFieldName, otherFormValues) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as LocalAuthoritySchoolWorkforceFormViewModel;
        Assert.NotNull(model);
        Assert.Equal(code, model.Code);
        Assert.Equal(formPrefix, model.FormPrefix);
        Assert.Equal(maxRows, model.MaxRows);
        Assert.Equal(resetFieldName, model.ResetFieldName);
        Assert.Equal(otherFormFieldName, model.OtherFormFieldName);
        Assert.Equal(otherFormValues, model.OtherFormValues);
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
        const string resetFieldName = nameof(resetFieldName);
        const string otherFormFieldName = nameof(otherFormFieldName);
        var otherFormValues = new Dictionary<string, StringValues>();
        _httpContext.Request.QueryString = new QueryString(query);
        var (expectedSelectedOverallPhases,
            expectedSelectedNurseryProvisions,
            expectedSelectedSpecialProvisions,
            expectedSelectedSixthFormProvisions) = expectedFilters;

        // act
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, resetFieldName, otherFormFieldName, otherFormValues) as ViewViewComponentResult;

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
        const string resetFieldName = nameof(resetFieldName);
        const string otherFormFieldName = nameof(otherFormFieldName);
        var otherFormValues = new Dictionary<string, StringValues>();
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
        var result = await _component.InvokeAsync(code, formPrefix, maxRows, DefaultSort, resetFieldName, otherFormFieldName, otherFormValues) as ViewViewComponentResult;

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