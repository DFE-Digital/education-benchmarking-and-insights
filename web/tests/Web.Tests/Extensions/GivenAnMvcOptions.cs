using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenAnMvcOptions
{
    [Theory]
    [InlineData("School workforce (full time equivalent)",
        "Enter school workforce (full time equivalent) in the correct format")]
    [InlineData("Learning resources (not ICT equipment)",
        "Enter learning resources (not ICT equipment) in the correct format")]
    [InlineData("Pupils eligible for free school meals (FSM)",
        "Enter pupils eligible for free school meals (FSM) in the correct format")]
    public void SetAttemptedValueIsInvalidAccessorIsCalled(string field, string expected)
    {
        var actual = MvcOptionsExtensions.SetAttemptedValueIsInvalidAccessor(string.Empty, field);
        Assert.Equal(expected, actual);
    }
}