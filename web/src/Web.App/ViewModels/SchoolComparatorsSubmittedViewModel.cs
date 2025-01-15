using Web.App.Domain;
namespace Web.App.ViewModels;

public class SchoolComparatorsSubmittedViewModel(School school, bool isEdit)
{
    public string? Urn => school.URN;
    public string? Name => school.SchoolName;
    public bool IsEdit => isEdit;
}