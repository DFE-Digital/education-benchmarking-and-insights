using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparatorsChosenTrustsViewModel(string? companyNumber, TrustCharacteristicUserDefined[]? trustCharacteristics, bool isEdit)
{
    public string? CompanyNumber => companyNumber;
    public IOrderedEnumerable<TrustCharacteristicUserDefined>? Trusts => trustCharacteristics?.OrderBy(c => c.TrustName);
    public bool IsEdit => isEdit;
}