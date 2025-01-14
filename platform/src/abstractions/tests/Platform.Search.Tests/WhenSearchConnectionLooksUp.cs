using Azure;
using Azure.Search.Documents;
using Moq;
using Platform.Search.Telemetry;
using Xunit;

namespace Platform.Search.Tests;

public class WhenSearchConnectionLooksUp
{
    private readonly Mock<SearchClient> _searchClient = new();
    private readonly SearchConnection<TestType> _service;
    private readonly Mock<ITelemetryService> _telemetryService = new();
    private string _indexName = nameof(_indexName);
    private string _serviceName = nameof(_serviceName);

    public WhenSearchConnectionLooksUp()
    {
        _searchClient.SetupGet(c => c.ServiceName).Returns(_serviceName);
        _searchClient.SetupGet(c => c.IndexName).Returns(_indexName);

        _service = new SearchConnection<TestType>(_searchClient.Object, _telemetryService.Object);
    }

    [Fact]
    public async Task ShouldReturnExpectedResult()
    {
        // arrange
        const string key = nameof(key);

        var result = new TestType();

        _searchClient
            .Setup(c => c.GetDocumentAsync<TestType>(key, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Response.FromValue(result, Mock.Of<Response>()));

        // act
        var results = await _service.LookUpAsync(key);

        // assert
        Assert.Equal(result, results);
    }

    private record TestType;
}