using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Content.Features.Years.Handlers;
using Platform.Api.Content.Features.Years.Models;
using Platform.Api.Content.Features.Years.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Content.Tests.Years.Handlers;

public class GetCurrentReturnYearsV1HandlerTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetCurrentReturnYearsV1Handler _handler;
    private readonly Mock<IYearsService> _service;

    public GetCurrentReturnYearsV1HandlerTests()
    {
        _service = new Mock<IYearsService>();
        _handler = new GetCurrentReturnYearsV1Handler(_service.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var years = _fixture.Create<FinanceYears>();

        _service
            .Setup(x => x.GetCurrentReturnYears(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(years);

        var result = await _handler.HandleAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<FinanceYears>();
        Assert.NotNull(body);
        Assert.Equivalent(years, body);
    }
}