using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Api.Content.Features.Files.Models;
using Platform.Api.Content.Features.Files.Responses;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Content.Tests.Files.Handlers;

public class GetTransparencyFilesV1HandlerTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetTransparencyFilesV1Handler _handler;
    private readonly Mock<IFilesService> _service;

    public GetTransparencyFilesV1HandlerTests()
    {
        _service = new Mock<IFilesService>();
        _fixture = new Fixture();
        _handler = new GetTransparencyFilesV1Handler(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture.CreateMany<FileModel>();

        _service
            .Setup(d => d.GetActiveFilesByType(It.IsAny<CancellationToken>(), "transparency-aar", "transparency-cfr"))
            .ReturnsAsync(models);

        var result = await _handler.HandleAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<FileResponse[]>();
        Assert.NotNull(body);
    }
}