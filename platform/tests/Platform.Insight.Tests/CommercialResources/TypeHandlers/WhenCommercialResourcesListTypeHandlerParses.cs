using Xunit;
using Platform.Api.Insight.Features.CommercialResources.Models;
using Platform.Api.Insight.Features.CommercialResources.Services;

namespace Platform.Insight.Tests.CommercialResources.TypeHandlers;

public class WhenCommercialResourcesListTypeHandlerParses
{
    private readonly CommercialResourcesListTypeHandler _handler = new();

    [Fact]
    public void ShouldReturnListWhenValidSingleElementJsonProvided()
    {
        const string jsonInput = "[\"Item1\"]";

        var result = _handler.Parse(jsonInput);

        Assert.NotNull(result);
        Assert.IsType<CommercialResourcesList>(result);
        Assert.Single(result);
        Assert.Equal("Item1", result[0]);
    }

    [Fact]
    public void ShouldReturnListWhenValidMultipleElementsJsonProvided()
    {
        const string jsonInput = "[\"Item1\", \"Item2\", \"Item3\"]";

        var result = _handler.Parse(jsonInput);

        Assert.NotNull(result);
        Assert.IsType<CommercialResourcesList>(result);
        Assert.Equal(3, result.Count);
        Assert.Contains("Item1", result);
        Assert.Contains("Item2", result);
        Assert.Contains("Item3", result);
    }

    [Fact]
    public void ShouldReturnEmptyListWhenNullInput()
    {
        const string? jsonInput = null;

        var result = _handler.Parse(jsonInput);

        Assert.NotNull(result);
        Assert.Empty(result);
    }
}

