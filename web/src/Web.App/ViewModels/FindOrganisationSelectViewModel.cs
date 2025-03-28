using System.ComponentModel.DataAnnotations;
// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Web.App.ViewModels;

public class FindOrganisationSelectViewModel
{
    [Required(ErrorMessage = "Select the type of organisation to search for")]
    public string? FindMethod { get; set; }
}