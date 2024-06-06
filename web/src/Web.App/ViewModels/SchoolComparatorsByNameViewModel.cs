using System.ComponentModel.DataAnnotations;
using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByNameViewModel(School school, SchoolCharacteristicUserDefined[]? schoolCharacteristics)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolCharacteristicUserDefined[]? Schools => schoolCharacteristics;
    public int ComparatorCount => schoolCharacteristics?.Count(s => s.URN != school.URN) ?? 0;
    public string[] ExcludeUrns => (schoolCharacteristics?.Select(s => s.URN) ?? [])
        .Concat([school.URN])
        .OfType<string>()
        .ToArray();
}

public record SchoolComparatorsUrnViewModel
{
    [Required(ErrorMessage = "Select a school from the suggester")]
    public string? Urn { get; init; }
}