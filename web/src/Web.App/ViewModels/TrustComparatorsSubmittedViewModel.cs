using Web.App.Domain;
using Web.App.Infrastructure.Apis;

namespace Web.App.ViewModels;

public class TrustComparatorsSubmittedViewModel(Trust trust, PostComparatorSetUserDefinedRequest request, bool isEdit)
{
    public string? CompanyNumber => trust.CompanyNumber;
    public string? Name => trust.TrustName;
    public Guid? Identifier => request.Identifier; // todo: remove on subsequent pass for Trusts
    public bool IsEdit => isEdit;
}