using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class WhenFunctionReceivesGetTrustBalanceRequest : BalanceTrustFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = Fixture.Build<BalanceTrustModel>().Create();

        Service
            .Setup(d => d.GetTrustAsync(It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await Functions.TrustBalanceAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceTrustResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        Service
            .Setup(d => d.GetTrustAsync(It.IsAny<string>()))
            .ReturnsAsync((BalanceTrustModel?)null);

        var result = await Functions.TrustBalanceAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}