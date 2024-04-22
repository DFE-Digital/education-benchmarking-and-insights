using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Api.Establishment;

[ExcludeFromCodeCoverage]
public record DbOptions
{
    [Required(ErrorMessage = "Missing establishment collection name")] 
    public string? EstablishmentCollectionName { get; set; }
}