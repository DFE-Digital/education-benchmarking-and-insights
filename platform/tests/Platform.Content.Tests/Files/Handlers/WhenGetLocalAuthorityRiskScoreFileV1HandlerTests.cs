using System.Net;
using Moq;
using Platform.Api.Content.Features.Files.Handlers;
using Platform.Api.Content.Features.Files.Models;
using Platform.Api.Content.Features.Files.Responses;
using Platform.Api.Content.Features.Files.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Platform.Test.Mocks;
using Xunit;

namespace Platform.Content.Tests.Files.Handlers;

public class WhenGetLocalAuthorityRiskScoreFileV1HandlerHandles : HandlerTestBase
{
    private readonly Mock<IFilesService> _service = new();
    private readonly GetLocalAuthorityRiskScoreFileV1Handler _handler;

    public WhenGetLocalAuthorityRiskScoreFileV1HandlerHandles()
    {
        _handler = new GetLocalAuthorityRiskScoreFileV1Handler(_service.Object);
    }

    [Fact]
    public void ShouldReturnCorrectVersion()
    {
        Assert.Equal("1.0", _handler.Version);
    }

    [Fact]
    public async Task ShouldReturn200WhenExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "2024");

        var fileResult = new FileModel
        {
            Type = "testType",
            Label = "testLabel",
            FileName = "test_2024.csv",
            RunId = "1234",
            FileSize = 1m
        };

        _service
            .Setup(s => s.GetLocalAuthorityRiskScoreFile("2024", token))
            .ReturnsAsync(fileResult);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);

        var body = await result.ReadAsJsonAsync<FileResponse>();
        Assert.NotNull(body);

        Assert.Equal(fileResult.MapToApiResponse(), body);
    }

    [Fact]
    public async Task ShouldReturn404WhenNoFileExists()
    {
        var token = CancellationToken.None;

        var request = MockHttpRequestData.Create();
        var context = new IdContext(request, token, "notFound");

        FileModel? nullResponse = null;
        _service
            .Setup(s => s.GetLocalAuthorityRiskScoreFile("notFound", token))
            .ReturnsAsync(nullResponse);

        var result = await _handler.HandleAsync(context);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}
