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
    public void ShouldReturnLabelSortFieldAndClassName()
    {
        // arrange
        const string label = nameof(label);
        const string sortField = nameof(sortField);
        const string className = nameof(className);

        // act
        var result = _component.Invoke(label, sortField, className: className) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal(label, model.Label);
        Assert.Equal(sortField, model.SortField);
        Assert.Equal(className, model.ClassName);
    }

    [Theory]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", "testField", "asc", "other=value", "?other=value&sortFieldKey=testField&sortOrderKey=desc")]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", "testField", "desc", "other=value", "?other=value&sortFieldKey=testField&sortOrderKey=asc")]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", null, null, "other=value", "?other=value&sortFieldKey=testField&sortOrderKey=asc")]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", null, null, null, "?sortFieldKey=testField&sortOrderKey=asc")]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", "otherField", "asc", null, "?sortFieldKey=testField&sortOrderKey=asc")]
    public void ShouldGenerateHrefWithSortFieldAndSortOrder(string sortField, string sortFieldKey, string sortOrderKey, string? currentSortField, string? currentSortOrder, string? baseQuery, string expectedQueryString)
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

        if (!string.IsNullOrWhiteSpace(currentSortField))
        {
            queryValues.Add(sortFieldKey, currentSortField);
        }

        if (!string.IsNullOrWhiteSpace(currentSortOrder))
        {
            queryValues.Add(sortOrderKey, currentSortOrder);
        }

        _httpContext.Request.QueryString = queryValues.Count > 0 ? QueryString.Create(queryValues) : QueryString.Empty;

        // act
        var result = _component.Invoke(label, sortField, sortFieldKey, sortOrderKey) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal($"{_path}{expectedQueryString}", model.Href);
    }

    [Theory]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", "testField", "asc", "asc")]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", "testField", "desc", "desc")]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", null, null, null)]
    [InlineData("testField", "sortFieldKey", "sortOrderKey", "otherField", "asc", null)]
    public void ShouldReturnCurrentSortForField(string sortField, string sortFieldKey, string sortOrderKey, string? currentSortField, string? currentSortOrder, string? expectedSort)
    {
        // arrange
        const string label = nameof(label);
        var queryValues = new Dictionary<string, StringValues>();
        if (!string.IsNullOrWhiteSpace(currentSortField))
        {
            queryValues.Add(sortFieldKey, currentSortField);
        }

        if (!string.IsNullOrWhiteSpace(currentSortOrder))
        {
            queryValues.Add(sortOrderKey, currentSortOrder);
        }

        _httpContext.Request.QueryString = queryValues.Count > 0 ? QueryString.Create(queryValues) : QueryString.Empty;

        // act
        var result = _component.Invoke(label, sortField, sortFieldKey, sortOrderKey) as ViewViewComponentResult;

        // assert
        Assert.NotNull(result);
        var model = result.ViewData?.Model as SortableTableHeaderCellViewModel;
        Assert.NotNull(model);
        Assert.Equal(expectedSort, model.Sort);
    }
}