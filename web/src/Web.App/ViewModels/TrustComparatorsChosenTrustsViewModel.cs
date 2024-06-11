using Web.App.Domain;
namespace Web.App.ViewModels;

public class TrustComparatorsChosenTrustsViewModel(string? companyNumber, TrustCharacteristicUserDefined[]? trustCharacteristics)
{
    public string? CompanyNumber => companyNumber;
    public IOrderedEnumerable<TrustCharacteristicUserDefined>? Trusts => trustCharacteristics?.OrderBy(c => c.TrustName);
}