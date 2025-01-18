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

public class GetBalanceTrustFunctionTests : FunctionsTestBase
{
    private readonly GetBalanceTrustFunction _function;
    private readonly Mock<IBalanceService> _service;
    private readonly Fixture _fixture;

    public GetBalanceTrustFunctionTests()
    {
        _service = new Mock<IBalanceService>();
        _fixture = new Fixture();
        _function = new GetBalanceTrustFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<BalanceTrustModel>().Create();

        _service
            .Setup(d => d.GetTrustAsync(It.IsAny<string>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceTrustResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _service
            .Setup(d => d.GetTrustAsync(It.IsAny<string>()))
            .ReturnsAsync((BalanceTrustModel?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}