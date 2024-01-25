using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolPlanningViewModel
{
    private readonly School _school;

    public SchoolPlanningViewModel(School school)
    {
        _school = school;
    }
    public string Name => _school.Name;
    public string Urn => _school.Urn;

}