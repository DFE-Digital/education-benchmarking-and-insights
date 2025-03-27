using System.ComponentModel.DataAnnotations;

namespace Web.App.ViewModels;

public class FindOrganisationSelectViewModel
{
    [Required(ErrorMessage = "Select the type of organisation to search for")]
    public string? FindMethod { get; set; }
}