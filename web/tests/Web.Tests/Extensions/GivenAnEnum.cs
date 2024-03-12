using Web.App.Attributes;
using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenAnEnum
{
    [Theory]
    [InlineData(TestEnum.A, "A")]
    [InlineData(TestEnum.B, "")]
    public void WhenGetStringValueIsCalled(TestEnum actual, string expected)
    {
        var result = actual.GetStringValue();
        Assert.Equal(expected, result);
    }

    public enum TestEnum
    {
        [StringValue("A")]
        A,
        B
    }
}