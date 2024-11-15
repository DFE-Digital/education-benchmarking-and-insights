using System;
using System.ComponentModel.DataAnnotations;
using Azure;
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Platform.Orchestrator.Search;

public class PipelineSearchOptions
{
    [Required]
    public string? Name { get; set; }

    [Required]
    public string? Key { get; set; }

    public Uri SearchEndPoint => new($"https://{Name}.search.windows.net/");
    public AzureKeyCredential SearchCredentials => new(Key ?? throw new ArgumentNullException());
}