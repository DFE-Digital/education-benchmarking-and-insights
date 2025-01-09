using System.Net;
using Moq;
using Platform.Api.Insight.Balance;
using Xunit;

namespace Platform.Tests.Insight.Balance;

public class WhenFunctionReceivesGetBalanceHistoryRequest : BalanceSchoolFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((new BalanceYearsModel(), Array.Empty<BalanceHistoryModel>()));

        var result = await Functions.SchoolBalanceHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var result = await Functions.SchoolBalanceHistoryAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.InternalServerError, result.StatusCode);
    }
}