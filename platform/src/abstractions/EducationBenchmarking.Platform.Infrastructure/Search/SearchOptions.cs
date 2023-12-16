using Azure;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public abstract class SearchServiceOptions
{
    public string Name { get; set; }
    public string Key { get; set; }
    
    public Uri Endpoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential Credential => new(Key);
}