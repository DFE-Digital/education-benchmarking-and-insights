using Web.App.Domain;

namespace Web.App.ViewModels;

public class NonLeadFederationSchoolViewModel(School school, Census census, string? giasSchoolUrl) : SchoolDetailsViewModel(school, giasSchoolUrl)
{
    public bool HasNursery => School.HasNursery;
    public bool HasSixthForm => School.HasSixthForm;
    public string? FederationLeadUrn => School.FederationLeadURN;
    public decimal? NumberOfPupils => census.TotalPupils;
    public string? OverallPhase => School.OverallPhase;
    public string? SchoolType => School.SchoolType;
}