using Platform.Orchestrator.Search;
using Xunit;

namespace Platform.Orchestrator.Tests.Search;

public class WhenPipelineSearchOptionsAreCreated
{
    [Fact]
    public void ShouldReturnEndpointAndCredentials()
    {
        const string name = nameof(name);
        const string key = nameof(key);
        var options = new PipelineSearchOptions
        {
            Name = name,
            Key = key
        };

        Assert.Equal($"https://{name}.search.windows.net/", options.SearchEndPoint.ToString());
        Assert.Equal(key, options.SearchCredentials.Key);
    }
}