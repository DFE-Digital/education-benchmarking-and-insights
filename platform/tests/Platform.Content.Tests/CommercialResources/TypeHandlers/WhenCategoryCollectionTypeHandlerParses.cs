using Platform.Api.Content.Features.CommercialResources.Models;
using Platform.Api.Content.Features.CommercialResources.Services;
using Xunit;

namespace Platform.Content.Tests.CommercialResources.TypeHandlers;

public class WhenCategoryCollectionTypeHandlerParses
{
    private readonly CategoryCollectionTypeHandler _handler = new();

    [Fact]
    public void ShouldReturnListWhenValidSingleElementJsonProvided()
    {
        const string jsonInput = "[\"Item1\"]";

        var result = _handler.Parse(jsonInput);

        Assert.NotNull(result);
        Assert.IsType<CategoryCollection>(result);
        Assert.Single(result.Items);
        Assert.Equal("Item1", result.Items[0]);
    }

    [Fact]
    public void ShouldReturnListWhenValidMultipleElementsJsonProvided()
    {
        const string jsonInput = "[\"Item1\", \"Item2\", \"Item3\"]";

        var result = _handler.Parse(jsonInput);

        Assert.NotNull(result);
        Assert.IsType<CategoryCollection>(result);
        Assert.Equal(3, result.Items.Length);
        Assert.Contains("Item1", result.Items);
        Assert.Contains("Item2", result.Items);
        Assert.Contains("Item3", result.Items);
    }

    [Fact]
    public void ShouldReturnEmptyListWhenNullInput()
    {
        const string? jsonInput = null;

        var result = _handler.Parse(jsonInput);

        Assert.NotNull(result);
        Assert.Empty(result.Items);
    }
}

