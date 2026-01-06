using System.Net;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Platform.Api.LocalAuthority.Features.Accounts.Functions;
using Platform.Api.LocalAuthority.Features.Accounts.Handlers;
using Platform.Functions.Extensions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Accounts.Functions;

public class QueryHighNeedsFunctionTests : VersionedFunctionTestBase<IQueryHighNeedsHandler>
{
    private readonly QueryHighNeedsFunction _function;
    private readonly MockResponse _response;

    public QueryHighNeedsFunctionTests()
    {
        var identifier = Fixture.Create<string>();
        _response = new MockResponse(identifier);
        _function = new QueryHighNeedsFunction(Dispatcher.Object);
    }

    [Fact]
    public async Task ShouldReturnOk_WhenHVersionIsValid()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "1.0");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));

        Handler
            .Setup(h => h.HandleAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(await request.CreateJsonResponseAsync(_response));

        var response = await _function.RunAsync(request);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.ReadAsJsonAsync<MockResponse>();
        Assert.NotNull(actual);
        Assert.Equal(_response, actual);
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenVersionIsUnsupported()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "9.9");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));

        var response = await _function.RunAsync(request);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var problem = await response.ReadAsJsonAsync<ProblemDetails>();
        Assert.NotNull(problem);
        Assert.Equal("Unsupported API version", problem.Title);
    }

    [Fact]
    public async Task ShouldUseLatestHandler_WhenNoVersionHeaderPresent()
    {
        var request = CreateHttpRequestData();

        Handler
            .Setup(h => h.HandleAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(await request.CreateJsonResponseAsync(_response));

        var response = await _function.RunAsync(request);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.ReadAsJsonAsync<MockResponse>();
        Assert.NotNull(actual);
        Assert.Equal(_response, actual);
    }

    // ReSharper disable once NotAccessedPositionalProperty.Local
    private record MockResponse(string Identifier);
}