using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.Primitives;
using Web.App.ViewComponents;
using Web.App.ViewModels.Components;
using Xunit;

namespace Web.Tests.ViewComponents;

public class SortableTableHeaderCellViewComponentTests
{
    private readonly SortableTableHeaderCellViewComponent _component;
    private readonly HttpContext _httpContext;
    private readonly PathString _path = "/test/path";

    public SortableTableHeaderCellViewComponentTests()
    {
        _httpContext = new DefaultHttpContext();
        _httpContext.Request.Path = _path;
        var viewContext = new ViewContext
        {
            HttpContext = _httpContext
        };
        _component = new SortableTableHeaderCellViewComponent
        {
            ViewComponentContext = new ViewComponentContext
            {
                ViewContext = viewContext
            }
        };
    }

    [Fact]
    public void ShouldReturnPassThroughValues()
    {
        // arrange
        const string label = nameof(label);
        const string sortField = nameof(sortField);
        const string className = nameof(className);
        const string tableId = nameof(tableId);

        // act
        var result = _component.Invoke(label, sortField, className: className, tableId: tableId) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal(label, model.Label);
        Assert.Equal(sortField, model.SortField);
        Assert.Equal(className, model.ClassName);
        Assert.Equal(tableId, model.TableId);
    }

    [Theory]
    [InlineData("testField", "sortKey", "~", "testField", "asc", "other=value", "testField~desc")]
    [InlineData("testField", "sortKey", "~", "testField", "desc", "other=value", "testField~asc")]
    [InlineData("testField", "sortKey", "~", null, null, "other=value", "testField~asc")]
    [InlineData("testField", "sortKey", "~", null, null, null, "testField~asc")]
    [InlineData("testField", "sortKey", "~", "otherField", "asc", null, "testField~asc")]
    public void ShouldGenerateSortValueWithSortFieldAndSortOrderFor(string sortField, string sortKey, string sortDelimeter, string? currentSortField, string? currentSortOrder, string? baseQuery, string expectedSortValue)
    {
        // arrange
        const string label = nameof(label);
        var queryValues = new Dictionary<string, StringValues>();
        if (!string.IsNullOrWhiteSpace(baseQuery))
        {
            foreach (var kvp in baseQuery.Split("&"))
            {
                var parts = kvp.Split("=");
                queryValues.Add(parts.First(), parts.Last());
            }
        }

        if (!string.IsNullOrWhiteSpace(currentSortField) && !string.IsNullOrWhiteSpace(currentSortOrder))
        {
            queryValues.Add(sortKey, $"{currentSortField}{sortDelimeter}{currentSortOrder}");
        }

        _httpContext.Request.QueryString = queryValues.Count > 0 ? QueryString.Create(queryValues) : QueryString.Empty;

        // act
        var result = _component.Invoke(label, sortField, sortKey, sortDelimeter) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal(sortKey, model.SortKey);
        Assert.Equal(expectedSortValue, model.SortValue);
    }

    [Theory]
    [InlineData("testField", "sortKey", "~", "testField", "asc", "asc")]
    [InlineData("testField", "sortKey", "~", "testField", "desc", "desc")]
    [InlineData("testField", "sortKey", "~", null, null, null)]
    [InlineData("testField", "sortKey", "~", "otherField", "asc", null)]
    public void ShouldReturnCurrentSortForFieldFor(string sortField, string sortKey, string sortDelimeter, string? currentSortField, string? currentSortOrder, string? expectedSort)
    {
        // arrange
        const string label = nameof(label);
        var queryValues = new Dictionary<string, StringValues>();
        if (!string.IsNullOrWhiteSpace(currentSortField) && !string.IsNullOrWhiteSpace(currentSortOrder))
        {
            queryValues.Add(sortKey, $"{currentSortField}{sortDelimeter}{currentSortOrder}");
        }

        _httpContext.Request.QueryString = queryValues.Count > 0 ? QueryString.Create(queryValues) : QueryString.Empty;

        // act
        var result = _component.Invoke(label, sortField, sortKey, sortDelimeter) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedSort, model.Sort);
    }

    [Theory]
    [InlineData("testField", "sortKey", "~", "testField", "asc", "ascending", "true")]
    [InlineData("testField", "sortKey", "~", "testField", "desc", "descending", "true")]
    [InlineData("testField", "sortKey", "~", null, null, "none", "mixed")]
    [InlineData("testField", "sortKey", "~", "otherField", "asc", "none", "false")]
    public void ShouldReturnCurrentAriaValuesForFieldFor(string sortField, string sortKey, string sortDelimeter, string? currentSortField, string? currentSortOrder, string? expectedAriaSort, string? expectedAriaPressed)
    {
        // arrange
        const string label = nameof(label);
        var queryValues = new Dictionary<string, StringValues>();
        if (!string.IsNullOrWhiteSpace(currentSortField) && !string.IsNullOrWhiteSpace(currentSortOrder))
        {
            queryValues.Add(sortKey, $"{currentSortField}{sortDelimeter}{currentSortOrder}");
        }

        _httpContext.Request.QueryString = queryValues.Count > 0 ? QueryString.Create(queryValues) : QueryString.Empty;

        // act
        var result = _component.Invoke(label, sortField, sortKey, sortDelimeter) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedAriaSort, model.AriaSort);
        Assert.Equal(expectedAriaPressed, model.AriaPressed);
    }
}