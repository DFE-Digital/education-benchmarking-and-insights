using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Functions;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class WhenFunctionReceivesQueryTrustsBalanceRequest : BalanceTrustFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var trusts = Fixture.CreateMany<BalanceTrustModel>(5);

        Service
            .Setup(d => d.QueryTrustsAsync(It.IsAny<string[]>(), It.IsAny<string>()))
            .ReturnsAsync(trusts);

        var result = await Functions.QueryTrustsBalanceAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceTrustResponse[]>();
        Assert.NotNull(body);
    }
}