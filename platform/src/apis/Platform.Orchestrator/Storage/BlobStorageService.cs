using System;
using System.Threading.Tasks;
// TODO: issue with Azure.Core here latest version of Azure.Storage.Blobs
// causes ambiguity with DefaultAzureCredential 12.23.0 is fine
// if the latest iteration works we won't use DefaultAzureCredential
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
        string sourceConnectionString,
        string sourceContainer,
        string sourceBlobName,
        string destConnectionString,
        string destContainer,
        string fileName);
}

public class BlobStorageService : IBlobStorageService
{
    public async Task<BlobMoveResult> MoveBlobAsync(
        string sourceConnectionString,
        string sourceContainer,
        string sourceBlobName,
        string destConnectionString,
        string destContainer,
        string fileName)
    {
        try
        {
            var sourceServiceClient = new BlobServiceClient(sourceConnectionString);
            var destServiceClient = new BlobServiceClient(destConnectionString);

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
            var filesizeMb = Math.Round(filesizeBytes / (1024m * 1024m), 2);

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
