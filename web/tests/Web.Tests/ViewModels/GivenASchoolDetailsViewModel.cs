using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenASchoolDetailsViewModel
{
    [Theory]
    [InlineData("https://some-website", "https://some-website")]
    [InlineData("http://some-website", "http://some-website")]
    [InlineData("", "")]
    [InlineData("some-website", "http://some-website")]
    [InlineData("ftp:some-website", "")]
    public void WhenSchoolWebsiteIs(string website, string expected)
    {
        var school = new School { Website = website };
        var vm = new SchoolDetailsViewModel(school);

        Assert.Equal(expected, vm.Website);
    }
}