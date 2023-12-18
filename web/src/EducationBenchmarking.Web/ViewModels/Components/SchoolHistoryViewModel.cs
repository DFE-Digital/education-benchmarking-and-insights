namespace EducationBenchmarking.Web.ViewModels.Components;

public class SchoolHistoryViewModel
{
    public SchoolHistoryViewModel(string identifier)
    {
        Identifier = identifier;
    }

    public string Identifier { get; set; }
}