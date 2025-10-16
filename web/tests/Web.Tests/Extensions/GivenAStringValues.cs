using Microsoft.Extensions.Primitives;
using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenAStringValues
{
    private enum TestEnum
    {
        A = 1,
        B = 2
    }

    [Fact]
    public void ShouldReturnParsedValidValues()
    {
        var query = new StringValues([((int)TestEnum.A).ToString(), ((int)TestEnum.B).ToString()]);

        var result = query.CastQueryToEnum<TestEnum>();

        Assert.Equal([TestEnum.A, TestEnum.B], result);
    }

    [Fact]
    public void ShouldNotReturnMalformedValues()
    {
        var query = new StringValues([string.Empty, " ", null, "not an int"]);

        var result = query.CastQueryToEnum<TestEnum>();

        Assert.Empty(result);
    }

    [Fact]
    public void ShouldNotReturnOutOfRangeValues()
    {
        var query = new StringValues(["3", "4", "5"]);

        var result = query.CastQueryToEnum<TestEnum>();

        Assert.Empty(result);
    }
}