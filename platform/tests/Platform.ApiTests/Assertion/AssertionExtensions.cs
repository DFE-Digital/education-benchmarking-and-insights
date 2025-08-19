using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Platform.ApiTests.Assertion;

public static class AssertionExtensions
{
    public static void AssertDeepEquals(this JToken actual, JToken expected)
    {
        Assert.True(JToken.DeepEquals(expected, actual), $"Expected: {expected}{Environment.NewLine}Actual: {actual}");
    }

    public static void AssertDeepEquals(this XDocument actual, XDocument expected)
    {
        Assert.True(XNode.DeepEquals(expected, actual), $"Expected: {expected}{Environment.NewLine}Actual: {actual}");
    }
}