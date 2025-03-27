namespace Web.App.ViewModels;

public class FindOrganisationViewModel : FindOrganisationSelectViewModel
{
    public string? Code { get; set; }
    public string? CompanyNumber { get; set; }
    public string? LaInput { get; set; }
    public string? SchoolInput { get; set; }
    public string? TrustInput { get; set; }
    public string? Urn { get; set; }
}