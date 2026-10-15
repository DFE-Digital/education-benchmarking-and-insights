using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace Platform.Orchestrator.Storage;

public record BlobMoveResult
{
    public string SourceContainer { get; init; } = string.Empty;
    public string DestContainer { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public decimal? FilesizeMb { get; init; }
    public bool Success { get; init; }
    public string? ErrorMessage { get; init; }
}

public interface IBlobStorageService
{
    Task<BlobMoveResult> MoveBlobAsync(
        string sourceEndpoint,
        string sourceContainer,
        string sourceBlobName,
        string destEndpoint,
        string destContainer,
        string fileName,
        CancellationToken cancellationToken = default);
}

public class BlobStorageService(IBlobServiceClientFactory clientFactory) : IBlobStorageService
{
    public async Task<BlobMoveResult> MoveBlobAsync(
        string sourceEndpoint,
        string sourceContainer,
        string sourceBlobName,
        string destEndpoint,
        string destContainer,
        string destBlobName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var sourceServiceClient = clientFactory.CreateClient(sourceEndpoint);
            var destServiceClient = clientFactory.CreateClient(destEndpoint);

            var sourceContainerClient = sourceServiceClient.GetBlobContainerClient(sourceContainer);
            var sourceBlobClient = sourceContainerClient.GetBlobClient(sourceBlobName);

            var destContainerClient = destServiceClient.GetBlobContainerClient(destContainer);
            var destBlobClient = destContainerClient.GetBlobClient(destBlobName);

            await using (var stream = await sourceBlobClient.OpenReadAsync(cancellationToken: cancellationToken))
            {
                await destBlobClient.UploadAsync(stream, overwrite: true, cancellationToken: cancellationToken);
            }

            var properties = await destBlobClient.GetPropertiesAsync(cancellationToken: cancellationToken);
            var filesizeBytes = properties.Value.ContentLength;
            var filesizeMb = Math.Round(filesizeBytes / (1024m * 1024m), 2);

            return new BlobMoveResult
            {
                SourceContainer = sourceContainer,
                DestContainer = destContainer,
                FileName = destBlobName,
                FilesizeMb = filesizeMb,
                Success = true
            };
        }
        catch (Exception e)
        {
            return new BlobMoveResult
            {
                SourceContainer = sourceContainer,
                DestContainer = destContainer,
                Success = false,
                ErrorMessage = e.Message
            };
        }
    }
}

public interface IBlobServiceClientFactory
{
    BlobServiceClient CreateClient(string endPoint);
}

public class BlobServiceClientFactory : IBlobServiceClientFactory
{
    public BlobServiceClient CreateClient(string endPoint)
    {
        if (string.IsNullOrWhiteSpace(endPoint))
        {
            throw new ArgumentNullException(nameof(endPoint), "Storage endpoint cannot be empty.");
        }

        return endPoint.StartsWith("UseDevelopmentStorage=true", StringComparison.OrdinalIgnoreCase)
            ? new BlobServiceClient(endPoint)
            : new BlobServiceClient(new Uri(endPoint), new DefaultAzureCredential());
    }
}
