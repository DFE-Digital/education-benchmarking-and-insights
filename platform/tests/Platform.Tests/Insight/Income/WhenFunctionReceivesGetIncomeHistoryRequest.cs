using System.Net;
using Moq;
using Platform.Api.Insight.Income;
using Xunit;
namespace Platform.Tests.Insight.Income;

public class WhenFunctionReceivesGetIncomeHistoryRequest : IncomeFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>()))
            .ReturnsAsync(Array.Empty<SchoolIncomeHistoryModel>());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SchoolIncomeHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}