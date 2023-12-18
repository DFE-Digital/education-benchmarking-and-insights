namespace EducationBenchmarking.Web.ViewModels.Components;

public class SchoolDetailsViewModel
{
    public SchoolDetailsViewModel(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; set; }
}