using System.Net;
using Moq;
using Moq.Protected;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Services;
using Platform.Sql;
using Xunit;

namespace Platform.MaintenanceTasks.Tests.MonitorCommercialResources.Services;

public class MonitorCommercialResourcesServiceTests
{
    [Fact]
    public async Task CheckResourceAsyncReturnsCorrectResultForSuccess()
    {
        var response = new HttpResponseMessage(HttpStatusCode.OK);
        var service = CreateService(response);
        var resource = new CommercialResource
        {
            Title = "TestOk",
            Url = "https://foo.com"
        };

        var result = await service.CheckResourceAsync(resource);

        Assert.True(result.Success);
        Assert.Equal("https://foo.com", result.Url);
        Assert.Equal("TestOk", result.Title);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Null(result.RedirectLocation);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task CheckResourceAsyncReturnsCorrectResultForRedirect()
    {
        var response = new HttpResponseMessage(HttpStatusCode.MovedPermanently);
        response.Headers.Location = new Uri("https://foo-new.com/");
        var service = CreateService(response);
        var resource = new CommercialResource
        {
            Title = "TestRedirect",
            Url = "https://foo.com"
        };

        var result = await service.CheckResourceAsync(resource);

        Assert.False(result.Success);
        Assert.Equal("https://foo.com", result.Url);
        Assert.Equal("TestRedirect", result.Title);
        Assert.Equal(HttpStatusCode.MovedPermanently, result.StatusCode);
        Assert.Equal("https://foo-new.com/", result.RedirectLocation);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task CheckResourceAsyncReturnsCorrectResultForNotFound()
    {
        var response = new HttpResponseMessage(HttpStatusCode.NotFound);
        var service = CreateService(response);
        var resource = new CommercialResource
        {
            Title = "TestNotFound",
            Url = "https://foo.com"
        };

        var result = await service.CheckResourceAsync(resource);

        Assert.False(result.Success);
        Assert.Equal("https://foo.com", result.Url);
        Assert.Equal("TestNotFound", result.Title);
        Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        Assert.Null(result.RedirectLocation);
        Assert.Null(result.Exception);
    }

    [Fact]
    public async Task CheckResourceAsyncReturnsCorrectResultForException()
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException("TestException"));

        var httpClient = new HttpClient(handlerMock.Object);
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(f => f.CreateClient("NoRedirects")).Returns(httpClient);

        var dbFactoryMock = new Mock<IDatabaseFactory>();
        var service = new MonitorCommercialResourcesService(dbFactoryMock.Object, httpClientFactoryMock.Object);

        var resource = new CommercialResource
        {
            Title = "TestException",
            Url = "https://foo.com"
        };

        var result = await service.CheckResourceAsync(resource);

        Assert.False(result.Success);
        Assert.Equal("https://foo.com", result.Url);
        Assert.Equal("TestException", result.Title);
        Assert.Null(result.StatusCode);
        Assert.Null(result.RedirectLocation);
        Assert.NotNull(result.Exception);
        Assert.Contains("TestException", result.Exception.Message);
    }

    private static MonitorCommercialResourcesService CreateService(HttpResponseMessage response)
    {
        var handlerMock = new Mock<HttpMessageHandler>();
        handlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);

        var httpClient = new HttpClient(handlerMock.Object);
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(f => f.CreateClient("NoRedirects")).Returns(httpClient);

        var dbFactoryMock = new Mock<IDatabaseFactory>();

        return new MonitorCommercialResourcesService(dbFactoryMock.Object, httpClientFactoryMock.Object);
    }
}