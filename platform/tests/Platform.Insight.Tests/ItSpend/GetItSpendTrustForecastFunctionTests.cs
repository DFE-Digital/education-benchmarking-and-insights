using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.ItSpend;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Api.Insight.Features.ItSpend.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.ItSpend;

public class GetItSpendTrustForecastFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetItSpendTrustForecastFunction _function;
    private readonly Mock<IItSpendService> _service;

    public GetItSpendTrustForecastFunctionTests()
    {
        _service = new Mock<IItSpendService>();
        _fixture = new Fixture();
        _function = new GetItSpendTrustForecastFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Build<ItSpendTrustForecastResponse>().CreateMany();

        _service
            .Setup(s => s.GetTrustForecastAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var result = await _function.RunAsync(CreateHttpRequestData(), "12345678");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<ItSpendTrustForecastResponse[]>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnNotFound()
    {
        _service
            .Setup(s => s.GetTrustForecastAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync([]);

        var result = await _function.RunAsync(CreateHttpRequestData(), "12345678");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}