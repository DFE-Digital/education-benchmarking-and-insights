using Azure;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public abstract class SearchServiceOptions
{
    public string ServiceName { get; set; }
    public string ApiKey { get; set; }
    
    public Uri Endpoint => new($"https://{ServiceName}.search.windows.net/");
    public AzureKeyCredential Credential => new(ApiKey);
}