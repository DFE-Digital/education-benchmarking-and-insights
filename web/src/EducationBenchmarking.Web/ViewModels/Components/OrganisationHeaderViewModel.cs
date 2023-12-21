namespace EducationBenchmarking.Web.ViewModels.Components;

public class OrganisationHeaderViewModel
{
    public OrganisationHeaderViewModel(string name, string organisationType)
    {
        Name = name;
        OrganisationType = organisationType;
    }

    public string Name { get; set; }
    public string OrganisationType { get; set; }
}