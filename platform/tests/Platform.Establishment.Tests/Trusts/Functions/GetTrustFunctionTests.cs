using System.Net;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker.Http;
using Moq;
using Platform.Api.Establishment.Features.Trusts.Functions;
using Platform.Api.Establishment.Features.Trusts.Handlers;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.Trusts.Functions;

public class GetTrustFunctionTests : FunctionsTestBase
{
    private readonly string _companyNumber;
    private readonly Mock<IVersionedHandlerDispatcher<IGetTrustHandler>> _dispatcher;
    private readonly GetTrustFunction _function;
    private readonly Mock<IGetTrustHandler> _handler;
    private readonly Trust _trust;

    public GetTrustFunctionTests()
    {
        var fixture = new Fixture();
        _companyNumber = fixture.Create<string>();
        _trust = fixture.Build<Trust>()
            .With(t => t.CompanyNumber, _companyNumber)
            .Create();

        _handler = new Mock<IGetTrustHandler>();
        _dispatcher = new Mock<IVersionedHandlerDispatcher<IGetTrustHandler>>();
        _function = new GetTrustFunction(_dispatcher.Object);
    }

    [Fact]
    public async Task ShouldReturn200_WhenHandlerReturnsTrust()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "1.0");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));
        _dispatcher
            .Setup(d => d.GetHandler("1.0"))
            .Returns(_handler.Object);
        _handler
            .Setup(h => h.HandleAsync(request, _companyNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(await request.CreateJsonResponseAsync(_trust));

        var response = await _function.RunAsync(request, _companyNumber);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.ReadAsJsonAsync<Trust>();
        Assert.NotNull(actual);
        Assert.Equal(_trust.CompanyNumber, actual.CompanyNumber);
    }

    [Fact]
    public async Task ShouldReturnBadRequest_WhenVersionIsUnsupported()
    {
        var kvp = new KeyValuePair<string, string>("x-api-version", "9.9");
        var request = CreateHttpRequestData(null, new HttpHeadersCollection([kvp]));
        _dispatcher
            .Setup(d => d.GetHandler("9.9"))
            .Returns((IGetTrustHandler?)null);

        var response = await _function.RunAsync(request, _companyNumber);

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
        _dispatcher
            .Setup(d => d.GetHandler(null))
            .Returns(_handler.Object);
        _handler
            .Setup(h => h.HandleAsync(request, _companyNumber, It.IsAny<CancellationToken>()))
            .ReturnsAsync(await request.CreateJsonResponseAsync(_trust));

        var response = await _function.RunAsync(request, _companyNumber);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}