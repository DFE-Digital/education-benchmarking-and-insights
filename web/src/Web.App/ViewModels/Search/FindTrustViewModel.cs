using System.ComponentModel.DataAnnotations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class FindTrustViewModel : ISearchTermViewModel
{
    [Required(ErrorMessage = "Enter a trust name or company number to start a search")]
    [MinLength(3, ErrorMessage = "Enter a trust name or company number (minimum 3 characters)")]
    public string? Term { get; set; }

    public string? EstablishmentId { get; set; }

    public string Hint => "Search by name or company number";
}