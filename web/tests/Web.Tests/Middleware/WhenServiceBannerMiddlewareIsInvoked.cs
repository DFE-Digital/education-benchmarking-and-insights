using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using Web.App;
using Web.App.Attributes;
using Web.App.Middleware;
using Xunit;

namespace Web.Tests.Middleware;

public class WhenServiceBannerMiddlewareIsInvoked
{
    private readonly Mock<RequestDelegate> _next;
    private readonly DefaultHttpContext _context;
    private readonly ServiceBannerMiddleware _middleware;
    private readonly Mock<IEndpointFeature> _feature;

    public WhenServiceBannerMiddlewareIsInvoked()
    {
        _next = new Mock<RequestDelegate>();
        _middleware = new ServiceBannerMiddleware(_next.Object);
        _feature = new Mock<IEndpointFeature>();
        _context = new DefaultHttpContext();
        _context.Features.Set(_feature.Object);
    }

    [Fact]
    public async Task ShouldSetBannerTargetItemIfCustomAttributeValid()
    {
        const string target = nameof(target);

        var metadata = new EndpointMetadataCollection(new ServiceBannerAttribute(target));
        var endpoint = new Endpoint(Mock.Of<RequestDelegate>(), metadata, null);
        _feature.Setup(f => f.Endpoint).Returns(endpoint);

        await _middleware.InvokeAsync(_context);

        var hasValue = _context.Items.TryGetValue(BannerTargets.Key, out var actual);

        Assert.True(hasValue);
        Assert.Equal(target, actual);
    }

    [Fact]
    public async Task ShouldNotSetBannerTargetItemIfCustomAttributeMissing()
    {
        var metadata = new EndpointMetadataCollection();
        var endpoint = new Endpoint(Mock.Of<RequestDelegate>(), metadata, null);
        _feature.Setup(f => f.Endpoint).Returns(endpoint);

        await _middleware.InvokeAsync(_context);

        var hasValue = _context.Items.TryGetValue(BannerTargets.Key, out _);

        Assert.False(hasValue);
    }

    [Fact]
    public async Task ShouldCallNextRequestDelegate()
    {
        await _middleware.InvokeAsync(_context);

        _next.Verify(n => n.Invoke(_context));
    }
}