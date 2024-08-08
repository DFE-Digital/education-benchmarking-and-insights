using Platform.Functions.Extensions;
using Platform.Tests.Mocks;
using Xunit;
namespace Platform.Tests.Extensions;

public class WhenHttpRequestDataReadsAsJsonAsync
{
    [Fact]
    public async Task ShouldReturnObjectMatchingType()
    {
        var request = MockHttpRequestData.Create(new TestObjectType("testValue"));
        var result = await request.ReadAsJsonAsync<TestObjectType>();

        Assert.NotNull(result);
        Assert.IsType<TestObjectType>(result);
        Assert.Equal("testValue", result.TestProp);
    }

    [Fact]
    public async Task ShouldThrowArgumentNullExceptionIfResultIsNull()
    {
        var request = MockHttpRequestData.Create("");
        await Assert.ThrowsAsync<ArgumentNullException>(() => request.ReadAsJsonAsync<TestObjectType>());
    }
}