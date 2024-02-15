using EducationBenchmarking.Web.Domain;

namespace EducationBenchmarking.Web.ViewModels;

public class SchoolDetailsViewModel(School school) : SchoolViewModel(school)
{
    private readonly School _school = school;
    
    public string? Address => _school.Address;
    public string? Telephone => _school.Telephone;
    public string? LocalAuthorityName => _school.LocalAuthorityName;
    public string? Website
    {
        get {
            var url = _school.Website;
            if (!string.IsNullOrEmpty(url) && !url.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
            {
                url = "http://" + url;
            }

            return url;
        }
    }
}