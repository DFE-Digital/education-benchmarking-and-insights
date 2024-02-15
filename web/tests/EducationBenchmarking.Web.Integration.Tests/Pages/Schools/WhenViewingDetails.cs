using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools;

public class WhenViewingDetails(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Theory]
    [InlineData(EstablishmentTypes.Academies)]
    [InlineData(EstablishmentTypes.Maintained)]
    public async Task CanDisplay(string financeType)
    {
        var (page, school) = await SetupNavigateInitPage(financeType);
            
        AssertPageLayout(page, school);
    }
    
    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
    {
        var school = Fixture.Build<School>()
            .With(x => x.FinanceType, financeType)
            .Create();
        
        var page = await Client.SetupEstablishment(school)
            .Navigate(Paths.SchoolDetails(school.Urn));

        return (page, school);
    }
    
    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        var expectedBreadcrumbs = new[]
        {
            ("Home", Paths.ServiceHome.ToAbsolute()),
            ("Your school", Paths.SchoolHome(school.Urn).ToAbsolute()),
            ("School details", Paths.SchoolDetails(school.Urn).ToAbsolute())
        };

        DocumentAssert.AssertPageUrl(page, Paths.SchoolDetails(school.Urn).ToAbsolute());
        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "School details", "School details");
    }
}