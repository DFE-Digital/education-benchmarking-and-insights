using Web.App.Domain;
namespace Web.App.ViewModels;

public class NonLeadFederationSchoolViewModel(School school, Census census) : SchoolViewModel(school)
{
    public decimal? NumberOfPupils => census.TotalPupils;
}