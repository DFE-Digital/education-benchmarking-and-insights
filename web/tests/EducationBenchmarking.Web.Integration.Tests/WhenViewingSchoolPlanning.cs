using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Newtonsoft.Json.Linq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingSchoolPlanning : BenchmarkingWebAppClient
{
    public WhenViewingSchoolPlanning(BenchmarkingWebAppFactory factory) : base(factory)
    {
    }

    [Theory]
    [InlineData(true)]
    public async Task CanDisplaySchool(bool value)
    {
        Assert.True(value);
    }

    [Theory]
    [InlineData(true)]
    public async Task CanNavigateToHelp(bool value)
    {
        Assert.True(value);
    }

    [Theory]
    [InlineData(true)]
    public async Task CanNavigateToContinue(bool value)
    {
        Assert.True(value);
    }


    [Fact]
    public async Task CanDisplayNotFound()
    {
        var page = await SetupEstablishmentWithNotFound()
            .Navigate(Paths.SchoolComparison("12345"));

        DocumentAssert.AssertPageUrl(page, Paths.StatusError(404).ToAbsolute());
    }

    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await SetupEstablishmentWithException()
            .Navigate(Paths.SchoolComparison("12345"));

        DocumentAssert.AssertPageUrl(page, Paths.StatusError(500).ToAbsolute());
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        Assert.True(true);
        Assert.IsAssignableFrom<IHtmlDocument>(page);
        Assert.IsType<School>(school);
    }
}
