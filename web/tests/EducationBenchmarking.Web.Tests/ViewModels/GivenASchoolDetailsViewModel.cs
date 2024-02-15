using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.ViewModels;
using Xunit;

namespace EducationBenchmarking.Web.Tests.ViewModels;

public class GivenASchoolDetailsViewModel
{
    [Theory]
    [InlineData("https://some-website", "https://some-website")]
    [InlineData("http://some-website", "http://some-website")]
    [InlineData("", "")]
    [InlineData("some-website", "http://some-website")]
    public void WhenSchoolWebsiteIs(string website, string expected)
    {
        var school = new School { Website = website };
        var vm = new SchoolDetailsViewModel(school);
        
        Assert.Equal(expected, vm.Website);
    }
}