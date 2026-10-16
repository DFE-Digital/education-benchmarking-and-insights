using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Moq;
using Platform.Orchestrator.Storage;
using Xunit;

namespace Platform.Orchestrator.Tests.Storage;

public class WhenBlobStorageServiceMovesBlob
{
    private readonly Mock<IBlobServiceClientFactory> _factoryMock = new();
    private readonly Mock<BlobServiceClient> _sourceServiceClientMock = new();
    private readonly Mock<BlobServiceClient> _destServiceClientMock = new();
    private readonly Mock<BlobContainerClient> _sourceContainerClientMock = new();
    private readonly Mock<BlobContainerClient> _destContainerClientMock = new();
    private readonly Mock<BlobClient> _sourceBlobClientMock = new();
    private readonly Mock<BlobClient> _destBlobClientMock = new();

    private readonly BlobStorageService _sut;

    public WhenBlobStorageServiceMovesBlob()
    {
        _factoryMock
            .Setup(f => f.CreateClient("src"))
            .Returns(_sourceServiceClientMock.Object);

        _factoryMock
            .Setup(f => f.CreateClient("dest"))
            .Returns(_destServiceClientMock.Object);

        _sourceServiceClientMock
            .Setup(c => c.GetBlobContainerClient(It.IsAny<string>()))
            .Returns(_sourceContainerClientMock.Object);

        _sourceContainerClientMock
            .Setup(c => c.GetBlobClient(It.IsAny<string>()))
            .Returns(_sourceBlobClientMock.Object);

        _destServiceClientMock
            .Setup(c => c.GetBlobContainerClient(It.IsAny<string>()))
            .Returns(_destContainerClientMock.Object);

        _destContainerClientMock
            .Setup(c => c.GetBlobClient(It.IsAny<string>()))
            .Returns(_destBlobClientMock.Object);

        _sut = new BlobStorageService(_factoryMock.Object);
    }

    [Fact]
    public async Task ShouldMoveBlobAndCalculateFileSizeInMbOnSuccess()
    {
        const string sourceEndpoint = "src";
        const string destEndpoint = "dest";
        const string sourceContainer = "src-container";
        const string sourceBlobName = "default/2024/file.csv";
        const string destContainer = "dest-container";
        const string destBlobName = "file.csv_2023_2024";
        const long filesizeBytes = 2_097_152; // 2 MB

        var stream = new MemoryStream("test content"u8.ToArray());

        _sourceBlobClientMock
            .Setup(b => b.OpenReadAsync(It.IsAny<BlobOpenReadOptions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(stream);

        _destBlobClientMock
            .Setup(b => b.UploadAsync(It.IsAny<Stream>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Mock.Of<Response<BlobContentInfo>>());

        var properties = BlobsModelFactory.BlobProperties(contentLength: filesizeBytes);
        var response = Response.FromValue(properties, Mock.Of<Response>());

        _destBlobClientMock
            .Setup(b => b.GetPropertiesAsync(It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(response);

        var result = await _sut.MoveBlobAsync(
            sourceEndpoint,
            sourceContainer,
            sourceBlobName,
            destEndpoint,
            destContainer,
            destBlobName);

        Assert.True(result.Success);
        Assert.Equal(sourceContainer, result.SourceContainer);
        Assert.Equal(destContainer, result.DestContainer);
        Assert.Equal(destBlobName, result.FileName);
        Assert.Equal(2.00m, result.FilesizeMb);
        Assert.Null(result.ErrorMessage);

        _destBlobClientMock.Verify(b => b.UploadAsync(It.IsAny<Stream>(), true, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnFailureBlobMoveResultWhenExceptionIsThrown()
    {
        const string expectedError = "mock-error";

        _sourceBlobClientMock
            .Setup(b => b.OpenReadAsync(It.IsAny<long>(), It.IsAny<int?>(), It.IsAny<BlobRequestConditions>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException(expectedError));

        var result = await _sut.MoveBlobAsync(
            "src",
            "src-container",
            "source-blob",
            "dest",
            "dest-container",
            "dest-blob");

        Assert.False(result.Success);
        Assert.Equal("src-container", result.SourceContainer);
        Assert.Equal("dest-container", result.DestContainer);
        Assert.Equal(expectedError, result.ErrorMessage);
        Assert.Null(result.FilesizeMb);
    }
}
