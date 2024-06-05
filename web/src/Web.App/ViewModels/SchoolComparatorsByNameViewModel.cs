using System.ComponentModel.DataAnnotations;
using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByNameViewModel(School school, SchoolCharacteristicUserDefined[]? schoolCharacteristics)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolCharacteristicUserDefined[]? Schools => schoolCharacteristics;
}

public record SchoolComparatorsUrnViewModel
{
    [Required(ErrorMessage = "Select a school from the suggester")]
    public string? Urn { get; init; }
}