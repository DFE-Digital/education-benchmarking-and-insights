using System.ComponentModel.DataAnnotations;

namespace Web.App.ViewModels;

public class FindOrganisationSearchViewModel
{
    [Required(ErrorMessage = "Enter a search term")]
    public string? Term { get; set; }
}