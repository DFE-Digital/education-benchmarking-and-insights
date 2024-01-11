using Microsoft.AspNetCore.Http;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests;

public class WhenGetCorrelationIdIsCalled
{
    [Fact]
    public void WithCorrelationIdHeaderAsValidGuid()
    {
        var context = new DefaultHttpContext();
        var id = Guid.NewGuid();
        context.Request.Headers.Add(Constants.CorrelationIdHeader, id.ToString());

        var request = context.Request;
        var result = request.GetCorrelationId();
            
        Assert.Equal(id, result);
    }

    [Fact]
    public void WithCorrelationIdHeaderAsInvalidGuid()
    {
        var testHttpContext = new DefaultHttpContext();
        var correlationIdHeaderValue = "invalid";
        testHttpContext.Request.Headers.Add(Constants.CorrelationIdHeader, correlationIdHeaderValue);

        var id = testHttpContext.Request.GetCorrelationId();
            
        Assert.IsType<Guid>(id);
        Assert.NotEqual(Guid.Empty, id);    
    }
        
        
    [Fact]
    public void WithoutCorrelationIdHeader()
    {
        var testHttpContext = new DefaultHttpContext();
        var testHttpRequest = testHttpContext.Request;
            
        var result = testHttpRequest.GetCorrelationId();
            
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
    }
}