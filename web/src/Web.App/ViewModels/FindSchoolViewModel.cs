using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels;

public class FindSchoolViewModel
{
    [Required(ErrorMessage = "Enter a school name or URN to start a search")]
    [MinLength(3, ErrorMessage = "Enter a school name or URN (minimum 3 characters)")]
    public string? Term { get; set; }
}