namespace Web.App.ViewModels;

public class FindOrganisationViewModel
{
    public string? Code { get; set; }
    public string? CompanyNumber { get; set; }
    public string FindMethod { get; set; } = OrganisationTypes.School;
    public string? LaInput { get; set; }
    public string? SchoolInput { get; set; }
    public string? TrustInput { get; set; }
    public string? Urn { get; set; }
}