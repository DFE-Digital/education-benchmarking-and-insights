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

    [Theory]
    [InlineData("Educational ICT", true, "educational ICT")]
    [InlineData("Educational ICT", false, "educational ict")]
    [InlineData("ICT Learning Resources", true, "ICT learning resources")]
    [InlineData("ICT Learning Resources", false, "ict learning resources")]
    [InlineData("Learning Resources (not ICT equipment)", true, "learning resources (not ICT equipment)")]
    [InlineData("Learning Resources (not ICT equipment)", false, "learning resources (not ict equipment)")]
    [InlineData("Cleaning and Caretaking", true, "cleaning and caretaking")]
    [InlineData("Cleaning and Caretaking", false, "cleaning and caretaking")]
    public void ReturnsLoweredCasedWhenToLowerIsCalled(string source, bool persistInitials, string expected)
    {
        var result = source.ToLower(persistInitials);
        Assert.Equal(expected, result);
    }
}