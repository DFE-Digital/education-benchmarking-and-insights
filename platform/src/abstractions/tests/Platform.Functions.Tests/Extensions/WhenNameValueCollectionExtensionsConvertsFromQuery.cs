using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using Platform.Functions.Extensions;
using Xunit;

namespace Platform.Functions.Tests.Extensions;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class WhenNameValueCollectionExtensionsConvertsFromQuery
{
    [Theory]
    [InlineData(null, new string[] { })]
    [InlineData("", new string[] { })]
    [InlineData("value", new[] { "value" })]
    [InlineData("value1, value2", new[] { "value1", "value2" })]
    [InlineData("value1, , value2", new[] { "value1", "value2" })]
    public void ShouldConvertQueryParameterToArray(string? value, string[] expected)
    {
        const string name = nameof(name);

        var collection = new NameValueCollection
        {
            { name, value }
        };

        var actual = collection.ToStringArray(name);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("", false)]
    [InlineData("true", true)]
    [InlineData("false", false)]
    [InlineData("invalid", false)]
    public void ShouldConvertQueryParameterToBoolean(string? value, bool expected)
    {
        const string name = nameof(name);

        var collection = new NameValueCollection
        {
            { name, value }
        };

        var actual = collection.ToBool(name);
        Assert.Equal(expected, actual);
    }
}