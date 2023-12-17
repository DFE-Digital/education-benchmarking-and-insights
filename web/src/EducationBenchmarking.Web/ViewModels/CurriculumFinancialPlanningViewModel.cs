namespace EducationBenchmarking.Web.ViewModels;

public class CurriculumFinancialPlanningViewModel
{
    public string Identifier { get; }
    public CurriculumFinancialPlanningViewModel(string identifier)
    {
        Identifier = identifier;
    }
}