﻿using Platform.Functions.Extensions;
using Platform.Functions.Tests.Mocks;
using Xunit;

namespace Platform.Functions.Tests.Extensions;

public class WhenHttpRequestDataGetsCorrelationId
{
    [Fact]
    public void WithCorrelationIdHeaderAsValidGuid()
    {
        var request = MockHttpRequestData.Create();
        var id = Guid.NewGuid();
        request.Headers.Add(Constants.CorrelationIdHeader, id.ToString());

        var result = request.GetCorrelationId();

        Assert.Equal(id, result);
    }

    [Fact]
    public void WithCorrelationIdHeaderAsInvalidGuid()
    {
        var request = MockHttpRequestData.Create();
        const string id = "invalid";
        request.Headers.Add(Constants.CorrelationIdHeader, id);

        var result = request.GetCorrelationId();

        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public void WithoutCorrelationIdHeader()
    {
        var request = MockHttpRequestData.Create();

        var result = request.GetCorrelationId();

        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
    }
}