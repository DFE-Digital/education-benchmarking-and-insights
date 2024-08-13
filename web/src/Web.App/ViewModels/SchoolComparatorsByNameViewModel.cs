using System.ComponentModel.DataAnnotations;
using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByNameViewModel(School school, SchoolCharacteristicUserDefined[]? schoolCharacteristics, bool isEdit)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolCharacteristicUserDefined[]? Schools => schoolCharacteristics;
    public int ComparatorCount => schoolCharacteristics?.Count(s => s.URN != school.URN) ?? 0;
    public string[] ExcludeUrns => (schoolCharacteristics?.Select(s => s.URN) ?? [])
        .Concat([school.URN])
        .OfType<string>()
        .ToArray();
    public bool IsEdit => isEdit;
}

public record SchoolComparatorRemoveViewModel
{
    [Required(ErrorMessage = "Select a school to remove")]
    public string? Urn { get; init; }
}

public record SchoolComparatorAddViewModel : IValidatableObject
{
    public string? SchoolInput { get; init; }
    public string? Urn { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(Urn) || string.IsNullOrEmpty(SchoolInput))
        {
            var message = string.IsNullOrEmpty(SchoolInput)
                ? "Enter a school or academy name, postcode or URN"
                : "Select a school or academy from the suggested list";
            yield return new ValidationResult(message, new[]
            {
                nameof(SchoolInput)
            });
        }
    }
}