// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global

using System.ComponentModel.DataAnnotations;

namespace Web.App.Builders;

public record UriOptions
{
    [Required]
    public required string GiasBaseUrl { get; set; }

    [Required]
    public required string CompareSchoolPerformanceBaseUrl { get; set; }
}