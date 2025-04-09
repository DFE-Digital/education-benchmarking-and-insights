using System.ComponentModel.DataAnnotations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Web.App.ViewModels;

public class LocalAuthorityComparatorViewModel
{
    [Required]
    public string? Action { get; set; }
    public string? LaInput { get; set; }
    public string[] Selected { get; set; } = [];
    public string? Referrer { get; set; }
}