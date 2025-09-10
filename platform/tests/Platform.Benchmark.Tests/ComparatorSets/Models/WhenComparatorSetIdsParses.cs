using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets.Models;

public class WhenComparatorSetIdsParses
{
    [Theory]
    [InlineData(new[]
    {
        "1",
        "2",
        "3"
    }, "[\"1\",\"2\",\"3\"]")]
    [InlineData(new string[0], "[]")]
    public void ShouldReturnStringRepresentingCollection(string[] items, string expected)
    {
        var ids = new ComparatorSetIds();
        ids.AddRange(items);

        Assert.Equal(expected, ids.ToString());
    }

    [Theory]
    [InlineData("[\"1\",\"2\",\"3\"]", new[]
    {
        "1",
        "2",
        "3"
    })]
    [InlineData("[]", new string[0])]
    public void ShouldGenerateInstanceFromString(string value, string[] expected)
    {
        var ids = ComparatorSetIds.FromString(value);

        Assert.Equal(expected, ids);
    }

    [Theory]
    [InlineData(new[]
    {
        "1",
        "2",
        "3"
    }, new[]
    {
        "1",
        "2",
        "3"
    })]
    [InlineData(new string[0], new string[0])]
    public void ShouldGenerateInstanceFromCollection(string[] value, string[] expected)
    {
        var ids = ComparatorSetIds.FromCollection(value);

        Assert.Equal(expected, ids);
    }
}