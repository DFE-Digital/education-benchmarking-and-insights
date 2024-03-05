using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests.Pages.Schools;

public class WhenViewingHistory(BenchmarkingWebAppClient client) : PageBase(client)
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
            .Navigate(Paths.SchoolHistory(school.Urn));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
      
        DocumentAssert.AssertPageUrl(page, Paths.SchoolHistory(school.Urn).ToAbsolute());
        DocumentAssert.BackLink(page, "Back", Paths.SchoolHome(school.Urn).ToAbsolute());
        DocumentAssert.TitleAndH1(page, "Historic data - Education benchmarking and insights - GOV.UK", "Historic data");
    }
}