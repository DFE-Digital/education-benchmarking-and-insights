using System.Diagnostics.CodeAnalysis;
// ReSharper disable ClassNeverInstantiated.Global
namespace Web.App.Domain;

[ExcludeFromCodeCoverage]
public record CustomDataSchool
{
    public string? Id { get; set; }
    public string? URN { get; set; }
    public string? Data { get; set; }
}