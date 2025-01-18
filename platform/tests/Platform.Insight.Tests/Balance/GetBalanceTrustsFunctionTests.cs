using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Api.Insight.Features.Balance.Models;
using Platform.Api.Insight.Features.Balance.Responses;
using Platform.Api.Insight.Features.Balance.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Balance;

public class GetBalanceTrustsFunctionTests : FunctionsTestBase
{
    private readonly GetBalanceTrustsFunction _function;
    private readonly Mock<IBalanceService> _service;
    private readonly Fixture _fixture;

    public GetBalanceTrustsFunctionTests()
    {
        _service = new Mock<IBalanceService>();
        _fixture = new Fixture();
        _function = new GetBalanceTrustsFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var trusts = _fixture.CreateMany<BalanceTrustModel>(5);

        _service
            .Setup(d => d.QueryTrustsAsync(It.IsAny<string[]>(), It.IsAny<string>()))
            .ReturnsAsync(trusts);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceTrustResponse[]>();
        Assert.NotNull(body);
    }
}