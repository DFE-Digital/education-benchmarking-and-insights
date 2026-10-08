using System;
using System.Threading.Tasks;
using Azure.Identity;
// TODO: issue with Azure.Core here latest version of Azure.Storage.Blobs
// causes ambiguity with DefaultAzureCredential 12.23.0 is fine
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
        Uri sourceAccountUri,
        string sourceContainer,
        string sourceBlobName,
        Uri destAccountUri,
        string destContainer,
        string fileName);
}

public class BlobStorageService : IBlobStorageService
{
    public async Task<BlobMoveResult> MoveBlobAsync(
        Uri sourceAccountUri,
        string sourceContainer,
        string sourceBlobName,
        Uri destAccountUri,
        string destContainer,
        string fileName)
    {
        try
        {
            BlobServiceClient sourceServiceClient;
            BlobServiceClient destServiceClient;
            // Fall back to Azurite local connection string if target URI is local
            if (sourceAccountUri.Host is "127.0.0.1" or "localhost")
            {
                sourceServiceClient = new BlobServiceClient("UseDevelopmentStorage=true");
                destServiceClient = new BlobServiceClient("UseDevelopmentStorage=true");
            }
            else
            {
                var credential = new DefaultAzureCredential();
                sourceServiceClient = new BlobServiceClient(sourceAccountUri, credential);
                destServiceClient = new BlobServiceClient(destAccountUri, credential);
            }

            var sourceContainerClient = sourceServiceClient.GetBlobContainerClient(sourceContainer);
            var sourceBlobClient = sourceContainerClient.GetBlobClient(sourceBlobName);

            var destContainerClient = destServiceClient.GetBlobContainerClient(destContainer);
            await destContainerClient.CreateIfNotExistsAsync();
            var destBlobClient = destContainerClient.GetBlobClient(fileName);

            var copyOperation = await destBlobClient.StartCopyFromUriAsync(sourceBlobClient.Uri);
            var copyResponse = await copyOperation.WaitForCompletionAsync();

            // TODO: this could be cleaner
            var rawResponse = copyResponse.GetRawResponse();
            if (rawResponse.Status is < 200 or > 299)
            {
                return new BlobMoveResult
                {
                    SourceContainer = sourceContainer,
                    DestContainer = destContainer,
                    Success = false,
                    ErrorMessage = $"Copy operation failed with HTTP status {rawResponse.Status}: {rawResponse.ReasonPhrase}"
                };
            }

            var properties = await sourceBlobClient.GetPropertiesAsync();
            var filesizeBytes = properties.Value.ContentLength;
            var filesizeMb = Math.Round((decimal)filesizeBytes / (1024 * 1024), 2);

            // TODO: delete from source?

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
}
