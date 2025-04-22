using System.ComponentModel.DataAnnotations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class FindSchoolViewModel : ISearchTermViewModel
{
    [Required(ErrorMessage = "Enter a school name or URN to start a search")]
    [MinLength(3, ErrorMessage = "Enter a school name or URN (minimum 3 characters)")]
    public string? Term { get; set; }

    public string Hint => "Search by name, address, postcode or unique reference number (URN)";
}