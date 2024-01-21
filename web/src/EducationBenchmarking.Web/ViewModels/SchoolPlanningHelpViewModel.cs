using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanningHelpViewModel
{
    public SchoolPlanningHelpViewModel(string urn)
    {
        Urn = urn;
    }

    public string Urn { get; private set; }
}
