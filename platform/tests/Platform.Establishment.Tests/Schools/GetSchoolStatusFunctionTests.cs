using System.Net;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Schools;

public class GetSchoolStatusFunctionTests : FunctionsTestBase
{
    private readonly GetSchoolStatusFunction _function;
    private readonly Mock<ISchoolsService> _service;

    public GetSchoolStatusFunctionTests()
    {
        _service = new Mock<ISchoolsService>();
        _function = new GetSchoolStatusFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.GetSchoolStatusAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new SchoolStatus());

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<SchoolStatus>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {
        _service
            .Setup(d => d.GetSchoolStatusAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((SchoolStatus?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}