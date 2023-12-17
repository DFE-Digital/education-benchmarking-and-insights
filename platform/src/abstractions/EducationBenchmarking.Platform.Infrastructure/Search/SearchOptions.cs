using System.ComponentModel.DataAnnotations;
using Azure;

namespace EducationBenchmarking.Platform.Infrastructure.Search;

public abstract class SearchServiceOptions
{
    [Required] public string Name { get; set; }
    [Required] public string Key { get; set; }
    
    public Uri Endpoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential Credential => new(Key);
}