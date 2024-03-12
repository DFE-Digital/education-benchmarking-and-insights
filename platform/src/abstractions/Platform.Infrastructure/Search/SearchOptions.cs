using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Azure;

namespace Platform.Infrastructure.Search;

[ExcludeFromCodeCoverage]
public abstract record SearchServiceOptions
{
    [Required] public string? Name { get; set; }
    [Required] public string? Key { get; set; }

    public Uri Endpoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential Credential => new(Key ?? throw new ArgumentNullException(nameof(Key)));
}