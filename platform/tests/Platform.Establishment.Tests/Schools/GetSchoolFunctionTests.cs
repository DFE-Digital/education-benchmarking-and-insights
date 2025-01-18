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

public class GetSchoolFunctionTests : FunctionsTestBase
{
    private readonly GetSchoolFunction _function;
    private readonly Mock<ISchoolsService> _service;

    public GetSchoolFunctionTests()
    {
        _service = new Mock<ISchoolsService>();
        _function = new GetSchoolFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync(new School());

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<School>();
        Assert.NotNull(body);
    }

    [Fact]
    public async Task ShouldReturn404OnInvalidRequest()
    {

        _service
            .Setup(d => d.GetAsync(It.IsAny<string>()))
            .ReturnsAsync((School?)null);

        var result = await _function.RunAsync(CreateHttpRequestData(), "1");

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
    }
}