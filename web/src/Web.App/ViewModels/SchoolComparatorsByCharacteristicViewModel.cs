using System.ComponentModel.DataAnnotations;
using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsByCharacteristicViewModel(School school, SchoolCharacteristic? characteristic)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public SchoolCharacteristic? Characteristic => characteristic;
    public UserDefinedCharacteristicViewModel Data => new()
    {
        FinanceType = characteristic?.FinanceType,
        OverallPhase = [Characteristic?.OverallPhase]
    };
}

public record UserDefinedCharacteristicViewModel
{
    [Required(ErrorMessage = "Select a school type")]
    public string? FinanceType { get; init; }

    [Required(ErrorMessage = "Select at least one school category")]
    public string?[]? OverallPhase { get; init; }
}