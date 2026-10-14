using System;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;

namespace Platform.Orchestrator.Storage;

public record BlobMoveResult
{
    public string SourceContainer { get; init; } = string.Empty;
    public string DestContainer { get; init; } = string.Empty;
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
        string fileName);
}

public class BlobStorageService : IBlobStorageService
{
    public async Task<BlobMoveResult> MoveBlobAsync(
        string sourceEndpoint,
        string sourceContainer,
        string sourceBlobName,
        string destEndpoint,
        string destContainer,
        string fileName)
    {
        try
        {
            var sourceServiceClient = CreateBlobServiceClient(sourceEndpoint);
            var destServiceClient = CreateBlobServiceClient(destEndpoint);

            var sourceContainerClient = sourceServiceClient.GetBlobContainerClient(sourceContainer);
            var sourceBlobClient = sourceContainerClient.GetBlobClient(sourceBlobName);

            var destContainerClient = destServiceClient.GetBlobContainerClient(destContainer);
            if (!await destContainerClient.ExistsAsync())
            {
                await destContainerClient.CreateIfNotExistsAsync();
            }
            var destBlobClient = destContainerClient.GetBlobClient(fileName);

            await using (var stream = await sourceBlobClient.OpenReadAsync())
            {
                await destBlobClient.UploadAsync(stream, overwrite: true);
            }

            var properties = await sourceBlobClient.GetPropertiesAsync();
            var filesizeBytes = properties.Value.ContentLength;
            var filesizeMb = Math.Round(filesizeBytes / (1024m * 1024m), 2);

            return new BlobMoveResult
            {
                SourceContainer = sourceContainer,
                DestContainer = destContainer,
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

    private static BlobServiceClient CreateBlobServiceClient(string endPoint)
    {
        if (string.IsNullOrWhiteSpace(endPoint))
        {
            throw new ArgumentNullException(nameof(endPoint), "Storage endpoint cannot be empty.");
        }

        return endPoint.StartsWith("UseDevelopmentStorage=true", StringComparison.OrdinalIgnoreCase) ? new BlobServiceClient(endPoint) : new BlobServiceClient(new Uri(endPoint), new DefaultAzureCredential());
    }
}
