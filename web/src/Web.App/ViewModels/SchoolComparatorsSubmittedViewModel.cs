using Web.App.Domain;
using Web.App.Infrastructure.Apis;
namespace Web.App.ViewModels;

public class SchoolComparatorsSubmittedViewModel(School school, PutComparatorSetUserDefinedRequest request, bool isEdit)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public Guid Identifier => request.Identifier;
    public bool IsEdit => isEdit;
}