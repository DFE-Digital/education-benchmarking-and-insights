using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Content.Features.CommercialResources.Handlers;
using Platform.Api.Content.Features.CommercialResources.Models;
using Platform.Api.Content.Features.CommercialResources.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Content.Tests.CommercialResources.Handlers;

public class GetCommercialResourcesV1HandlerTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetCommercialResourcesV1Handler _function;
    private readonly Mock<ICommercialResourcesService> _service;

    public GetCommercialResourcesV1HandlerTests()
    {
        _service = new Mock<ICommercialResourcesService>();
        _function = new GetCommercialResourcesV1Handler(_service.Object);
        _fixture = new Fixture();
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture
            .Build<CommercialResource>()
            .CreateMany()
            .ToArray();

        _service
            .Setup(x => x.GetCommercialResources(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(models);

        var result = await _function.HandleAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<CommercialResource[]>();
        Assert.NotNull(body);
        Assert.Equivalent(models, body);
    }
}