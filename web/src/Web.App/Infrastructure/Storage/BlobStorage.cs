using System.Diagnostics.CodeAnalysis;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace Web.App.Infrastructure.Storage;

[ExcludeFromCodeCoverage]
public abstract class BlobStorage(string? connectionString)
{
    private BlobServiceClient StorageAcct { get; } = new(connectionString);

    protected SharedAccessTokenModel GetAccessToken(string? containerName)
    {
        var expiry = DateTimeOffset.UtcNow.AddMinutes(60);
        var containerClient = StorageAcct.GetBlobContainerClient(containerName);

        if (!containerClient.CanGenerateSasUri)
        {
            throw new InvalidOperationException("Unable to generate container SAS URI");
        }

        var sasBuilder = new BlobSasBuilder
        {
            BlobContainerName = containerClient.Name,
            Resource = "c",
            ExpiresOn = expiry
        };

        sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);

        var sasUri = containerClient.GenerateSasUri(sasBuilder);
        var sasToken = sasUri.Query;
        return new SharedAccessTokenModel
        { SasToken = sasToken, Expiry = expiry.DateTime, ContainerUri = containerClient.Uri };
    }
}

[ExcludeFromCodeCoverage]
public record SharedAccessTokenModel
{
    public string? SasToken { get; set; }
    public Uri? ContainerUri { get; set; }
    public DateTime Expiry { get; set; }
}