using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;

namespace Web.App.Infrastructure.Storage;

public interface IDataSourceStorage
{
    SharedAccessTokenModel GetAccessToken();
}

public class DataSourceStorage(IOptions<DataSourceStorageOptions> options)
    : BlobStorage(options.Value.ConnectionString), IDataSourceStorage
{
    private readonly DataSourceStorageOptions _options = options.Value;

    public SharedAccessTokenModel GetAccessToken()
    {
        return GetAccessToken(_options.ReturnsContainer);
    }
}


[ExcludeFromCodeCoverage]
public record DataSourceStorageOptions
{
    public string? ConnectionString { get; set; }
    public string? ReturnsContainer { get; set; }
}