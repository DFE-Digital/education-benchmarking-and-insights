using Azure;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Infrastructure;
using Platform.Orchestrator.Search;
using Xunit;

namespace Platform.Orchestrator.Tests.Search;

public class WhenPipelineSearchRunsIndexerAll
{
    private readonly Mock<ISearchIndexerClient> _client;
    private readonly PipelineSearch _search;

    public WhenPipelineSearchRunsIndexerAll()
    {
        _client = new Mock<ISearchIndexerClient>();
        _search = new PipelineSearch(new NullLogger<PipelineSearch>(), _client.Object);
    }

    [Fact]
    public async Task ShouldRunIndexerAsyncForAllIndexers()
    {
        // arrange
        var actualIndexers = new List<string>();
        _client
            .Setup(c => c.RunIndexerAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Callback<string, CancellationToken>((indexerName, _) =>
            {
                actualIndexers.Add(indexerName);
            });

        // act
        await _search.RunIndexerAll();

        // assert
        Assert.Equal(ResourceNames.Search.Indexers.All, actualIndexers);
    }

    [Fact]
    public async Task ShouldReturnTrueWhenRunIndexerReturnsSuccess()
    {
        // arrange
        var response = new Mock<Response>();
        response.Setup(r => r.IsError).Returns(false);

        _client
            .Setup(c => c.RunIndexerAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response.Object);

        // act
        var success = await _search.RunIndexerAll();

        // assert
        Assert.True(success);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenAnyRunIndexerReturnsFailure()
    {
        // arrange
        var failureResponse = new Mock<Response>();
        failureResponse.Setup(r => r.IsError).Returns(true);
        var successResponse = new Mock<Response>();
        successResponse.Setup(r => r.IsError).Returns(false);

        _client
            .SetupSequence(c => c.RunIndexerAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(failureResponse.Object)
            .ReturnsAsync(successResponse.Object);

        // act
        var success = await _search.RunIndexerAll();

        // assert
        Assert.False(success);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenRunIndexerReturnsNothing()
    {
        // arrange
        _client
            .Setup(c => c.RunIndexerAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Response?)null);

        // act
        var success = await _search.RunIndexerAll();

        // assert
        Assert.False(success);
    }

    [Fact]
    public async Task ShouldReturnFalseWhenRunIndexerThrowsException()
    {
        // arrange
        _client
            .Setup(c => c.RunIndexerAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .Throws(new RequestFailedException("Test"));

        // act
        var success = await _search.RunIndexerAll();

        // assert
        Assert.False(success);
    }
}