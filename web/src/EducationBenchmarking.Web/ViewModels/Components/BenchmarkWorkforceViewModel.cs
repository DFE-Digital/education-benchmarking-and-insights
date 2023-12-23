namespace EducationBenchmarking.Web.ViewModels.Components;

public class BenchmarkWorkforceViewModel
{
    public string Identifier { get; }
    public BenchmarkWorkforceViewModel(string identifier)
    {
        Identifier = identifier;
    }
}