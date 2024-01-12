using EducationBenchmarking.Platform.Functions.Extensions;
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

        var result = context.Request.GetCorrelationId();
            
        Assert.Equal(id, result);
    }

    [Fact]
    public void WithCorrelationIdHeaderAsInvalidGuid()
    {
        var context = new DefaultHttpContext();
        var id = "invalid";
        context.Request.Headers.Add(Constants.CorrelationIdHeader, id);

        var result = context.Request.GetCorrelationId();
            
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);    
    }
        
        
    [Fact]
    public void WithoutCorrelationIdHeader()
    {
        var context = new DefaultHttpContext();
            
        var result = context.Request.GetCorrelationId();
            
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
    }
}