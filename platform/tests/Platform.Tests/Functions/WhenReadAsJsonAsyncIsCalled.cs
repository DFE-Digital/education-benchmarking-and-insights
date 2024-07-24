using Platform.Functions.Extensions;
using Platform.Tests.Mocks;
using Xunit;

namespace Platform.Tests.Functions;

public class WhenReadAsJsonAsyncIsCalled
{
    [Fact]
    public async Task ReturnsObjectMatchingType()
    {
        var request = MockHttpRequestData.Create(new TestObjectType("testValue"));
        var result = await request.ReadAsJsonAsync<TestObjectType>();

        Assert.NotNull(result);
        Assert.IsType<TestObjectType>(result);
        Assert.Equal("testValue", result.TestProp);
    }

    [Fact]
    public async Task IfResultIsNullThrowsArgumentNullException()
    {
        var request = MockHttpRequestData.Create("");
        await Assert.ThrowsAsync<ArgumentNullException>(() => request.ReadAsJsonAsync<TestObjectType>());
    }
}

public class TestObjectType(string testProp)
{
    public string TestProp { get; } = testProp;
}