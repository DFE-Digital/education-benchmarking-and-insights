using Web.App.Domain;

namespace Web.App.ViewModels
{
    public class SchoolHistoryViewModel(School school)
    {
        public string? Name => school.Name;
        public string? Urn => school.Urn;
    }
}