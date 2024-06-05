using System.ComponentModel.DataAnnotations;
using Web.App.Attributes;
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
        OverallPhase = [Characteristic?.OverallPhase],
        LaSelection = characteristic == null ? null : "This"
    };
}

public record UserDefinedCharacteristicViewModel
{
    [Required(ErrorMessage = "Select a school type")]
    public string? FinanceType { get; init; }

    [Required(ErrorMessage = "Select at least one school category")]
    public string?[]? OverallPhase { get; init; }

    [Required(ErrorMessage = "Select a local authority")]
    public string? LaSelection { get; init; }

    [RequiredDepends(nameof(LaSelection), "Choose", ErrorMessage = "Select a local authority from the suggester")]
    public string? LaInput { get; init; }

    [RequiredDepends(nameof(LaSelection), "Choose", ErrorMessage = "Select a local authority from the suggester")]
    public string? Code { get; init; }
}