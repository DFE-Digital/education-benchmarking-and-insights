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

public record TrustComparatorsCompanyNumberViewModel
{
    [Required(ErrorMessage = "Select a trust from the suggester")]
    public string? CompanyNumber { get; init; }
}