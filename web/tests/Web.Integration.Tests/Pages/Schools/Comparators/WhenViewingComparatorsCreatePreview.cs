using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsCreatePreview(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Fact]
    public async Task CanDisplay()
    {
        var (page, school) = await SetupNavigateInitPage();
        AssertPageLayout(page, school);
    }

    private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var key = SessionKeys.ComparatorSetUserDefined(school.URN!);
        var set = new UserDefinedSchoolComparatorSet
        {
            Set = ["1", "2", "3"],
            TotalSchools = 123
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(set.ToJson())
        };

        var page = await Client.SetupEstablishment(school)
            .SetupSchoolInsight(new[]
            {
                new SchoolCharacteristic
                {
                    URN = "114504",
                    SchoolName = "Forest Row Church of England Primary School",
                    OverallPhase = "Primary",
                    Address = "Forest Row, RH18 5EB"
                }
            })
            .SetupHttpContextAccessor(sessionState)
            .Navigate(Paths.SchoolComparatorsCreatePreview(school.URN));

        return (page, school);
    }

    private static void AssertPageLayout(IHtmlDocument page, School school)
    {
        DocumentAssert.BackLink(page, "Back", Paths.SchoolComparatorsCreateBy(school.URN).ToAbsolute());
        DocumentAssert.TitleAndH1(page,
            "Schools successfully matched - Financial Benchmarking and Insights Tool - GOV.UK",
            "Schools successfully matched");
        var cta = page.QuerySelector(".govuk-button");
        DocumentAssert.PrimaryCta(cta, "Create a set using these schools", Paths.SchoolComparatorsCreateSubmit(school.URN));
        var change = page.QuerySelector("#change-set");
        DocumentAssert.Link(change, "Change characteristics and create a new set", Paths.SchoolComparatorsCreateByCharacteristic(school.URN).ToAbsolute());
    }
}