using System.Diagnostics.CodeAnalysis;

// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.Infrastructure.Storage;

[ExcludeFromCodeCoverage]
public record StorageOptions
{
    public string? ReturnsContainer { get; set; }
}