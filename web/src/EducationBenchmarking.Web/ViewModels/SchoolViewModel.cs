using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolViewModel
{
    private readonly School _school;

    public SchoolViewModel(School school)
    {
        _school = school;
    }
    
    public string Name => _school.Name;
    public string Urn => _school.Urn;
}