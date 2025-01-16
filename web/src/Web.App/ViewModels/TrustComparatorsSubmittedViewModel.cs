using Web.App.Domain;

namespace Web.App.ViewModels;

public class TrustComparatorsSubmittedViewModel(Trust trust, bool isEdit)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public bool IsEdit => isEdit;
}