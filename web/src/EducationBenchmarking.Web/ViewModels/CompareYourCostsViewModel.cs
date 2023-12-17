namespace EducationBenchmarking.Web.ViewModels;

public class CompareYourCostsViewModel
{
    public string Identifier { get; }
    public CompareYourCostsViewModel(string identifier)
    {
        Identifier = identifier;
    }
}