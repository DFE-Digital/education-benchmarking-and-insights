using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolDetailsViewModel(School school) : SchoolViewModel(school)
{
    private readonly School _school = school
        ;
    public string? Address => _school.Address;
    public string? Telephone => _school.Telephone;
    public string? LocalAuthorityName => _school.LocalAuthorityName;
    public string? Website => _school.Website;
}