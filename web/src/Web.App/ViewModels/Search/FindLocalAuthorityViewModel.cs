using System.ComponentModel.DataAnnotations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels.Search;

public class FindLocalAuthorityViewModel : ISearchTermViewModel
{
    [Required(ErrorMessage = "Enter a local authority name or code to start a search")]
    [MinLength(3, ErrorMessage = "Enter a local authority name or code (minimum 3 characters)")]
    public string? Term { get; set; }

    public string? EstablishmentId { get; set; }

    public string Hint => "Search by name or code";
}