using Web.App.Extensions;
using Xunit;
namespace Web.Tests.Extensions;

public class GivenAString
{
    [Theory]
    [InlineData("Hello, world", "hello-world")]
    [InlineData(" Test  value: 1, 2, 3 ", "test-value-1-2-3")]
    public void ReturnsSlugWhenToSlugIsCalled(string source, string expected)
    {
        var result = source.ToSlug();
        Assert.Equal(expected, result);
    }
}