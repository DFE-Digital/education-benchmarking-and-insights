using System.Collections.Concurrent;
using System.Text;
using AngleSharp.Html.Dom;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Web.App.Extensions;
using Xunit;
namespace Web.Integration.Tests.Pages.Schools.Comparators;

public class WhenViewingComparatorsCreateSubmit(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task CanDisplay(bool isEdit)
    {
        const string urn = "12345";
        var page = await SetupNavigateInitPage(urn, isEdit);
        AssertPageLayout(page, isEdit);
        DocumentAssert.AssertPageUrl(page, Paths.SchoolComparatorsCreateSubmitted(urn, isEdit).ToAbsolute());
    }

    private async Task<IHtmlDocument> SetupNavigateInitPage(string urn, bool isEdit)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, urn)
            .Create();

        var key = SessionKeys.ComparatorSetUserDefined(school.URN!);
        var set = new UserDefinedSchoolComparatorSet
        {
            Set = ["1", "2", "3"],
            TotalSchools = 123,
            RunId = isEdit ? Guid.NewGuid().ToString() : null
        };
        var sessionState = new ConcurrentDictionary<string, byte[]>
        {
            [key] = Encoding.ASCII.GetBytes(set.ToJson())
        };

        var page = await Client.SetupEstablishment(school)
            .SetupSchoolInsight([
                new SchoolCharacteristic
                {
                    URN = "114504",
                    SchoolName = "Forest Row Church of England Primary School",
                    OverallPhase = "Primary",
                    Address = "Forest Row, RH18 5EB"
                }
            ])
            .SetupComparatorSetApi()
            .SetupHttpContextAccessor(sessionState)
            .Navigate(Paths.SchoolComparatorsCreateSubmit(school.URN));

        return page;
    }

    private static void AssertPageLayout(IHtmlDocument page, bool isEdit)
    {
        var expectedTitle = isEdit
            ? "Updating benchmarking data - Financial Benchmarking and Insights Tool - GOV.UK"
            : "Generating benchmarking data - Financial Benchmarking and Insights Tool - GOV.UK";

        var expectedHeading = isEdit
            ? "Updating benchmarking data"
            : "Generating benchmarking data";

        DocumentAssert.TitleAndH1(page, expectedTitle, expectedHeading);
    }
}