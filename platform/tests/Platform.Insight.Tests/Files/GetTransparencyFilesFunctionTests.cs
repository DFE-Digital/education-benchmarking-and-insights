using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Files;
using Platform.Api.Insight.Features.Files.Models;
using Platform.Api.Insight.Features.Files.Responses;
using Platform.Api.Insight.Features.Files.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.Files;

public class GetTransparencyFilesFunctionTests : FunctionsTestBase
{
    private readonly Fixture _fixture;
    private readonly GetTransparencyFilesFunction _function;
    private readonly Mock<IFilesService> _service;

    public GetTransparencyFilesFunctionTests()
    {
        _service = new Mock<IFilesService>();
        _fixture = new Fixture();
        _function = new GetTransparencyFilesFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var models = _fixture.CreateMany<FileModel>();

        _service
            .Setup(d => d.GetActiveFilesByType("transparency-aar", "transparency-cfr"))
            .ReturnsAsync(models);

        var result = await _function.RunAsync(CreateHttpRequestData());

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<FileResponse[]>();
        Assert.NotNull(body);
    }
}