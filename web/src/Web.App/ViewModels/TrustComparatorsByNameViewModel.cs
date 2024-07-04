using System.ComponentModel.DataAnnotations;
using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparatorsByNameViewModel(Trust trust, TrustCharacteristicUserDefined[]? trustCharacteristics)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public TrustCharacteristicUserDefined[]? Trusts => trustCharacteristics;
    public int ComparatorCount => trustCharacteristics?.Count(t => t.CompanyNumber != trust.CompanyNumber) ?? 0;
    public string[] ExcludeCompanyNumbers => (trustCharacteristics?.Select(t => t.CompanyNumber) ?? [])
        .Concat([trust.CompanyNumber])
        .OfType<string>()
        .ToArray();
}

public record TrustComparatorRemoveViewModel
{
    [Required(ErrorMessage = "Select a trust to remove")]
    public string? CompanyNumber { get; init; }
}

public record TrustComparatorAddViewModel : IValidatableObject
{
    public string? TrustInput { get; init; }
    public string? CompanyNumber { get; init; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(CompanyNumber) || string.IsNullOrEmpty(TrustInput))
        {
            var message = string.IsNullOrEmpty(TrustInput)
                ? "Enter a trust name or company number"
                : "Select a trust name or company number from the suggested list";
            yield return new ValidationResult(message, new[]
            {
                nameof(TrustInput)
            });
        }
    }
}