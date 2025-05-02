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

public class GetBalanceSchoolFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetBalanceSchoolFunction _function;
    private readonly Mock<IBalanceService> _service;

    public GetBalanceSchoolFunctionTests()
    {
        _service = new Mock<IBalanceService>();
        _fixture = new Fixture();
        _function = new GetBalanceSchoolFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<BalanceSchoolModel>().Create();

        _service
            .Setup(d => d.GetSchoolAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BalanceSchoolResponse>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _service
            .Setup(d => d.GetSchoolAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((BalanceSchoolModel?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}