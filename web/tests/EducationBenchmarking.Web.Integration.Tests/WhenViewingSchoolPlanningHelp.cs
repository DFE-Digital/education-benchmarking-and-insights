using AngleSharp.Html.Dom;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests
{
    public class WhenViewingSchoolPlanningHelp : BenchmarkingWebAppClient
    {
        public WhenViewingSchoolPlanningHelp(BenchmarkingWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task PageLayoutIsCorrect()
        {
            var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

            // TODO: do we need back if we have breadcrumbs ??

            // page url
            DocumentAssert.AssertPageUrl(page, Paths.SchoolPlanningHelp(school.Urn).ToAbsolute());

            // assert breadcrumbs atm only Home
            var expectedBreadcrumbs = new[]
            {
                ("Home", Paths.ServiceHome.ToAbsolute()),
            };
            DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
            
            // assert back link
            var backLink = page.QuerySelector(".govuk-back-link");
            Assert.NotNull(backLink);
            DocumentAssert.BackLink(backLink, "Back", Paths.SchoolPlanning(school.Urn).ToAbsolute());

            // Title and H1 == Data required for ICFP
            DocumentAssert.TitleAndH1(page, "Data required for ICFP", "Data required for ICFP");

            // assert required div
            var required = page.GetElementById("required");
            Assert.NotNull(required);
            // assert h2 == Required:
            DocumentAssert.Heading2(required, "Required:");

            // assert h3 1 == Length of timetable cycle
            var requiredOne = page.GetElementById("required-1");
            Assert.NotNull(requiredOne);
            DocumentAssert.Heading3(required, "Length of timetable cycle");

            // assert h3 2 == Pupil figures
            var requiredTwo = page.GetElementById("required-2");
            Assert.NotNull(requiredTwo);
            DocumentAssert.Heading3(requiredTwo, "Pupil figures");

            // assert h3 3 == Teacher period allocation
            var requiredThree = page.GetElementById("required-3");
            Assert.NotNull(requiredThree);
            DocumentAssert.Heading3(requiredThree, "Teacher period allocation");

            // assert h3 4 == Other teaching periods
            var requiredFour = page.GetElementById("required-4");
            Assert.NotNull(requiredFour);
            DocumentAssert.Heading3(requiredFour, "Other teaching periods");

            // assert h3 5 == Management roles with teaching responsibilities
            var requiredFive = page.GetElementById("required-5");
            Assert.NotNull(requiredFive);
            DocumentAssert.Heading3(requiredFive, "Management roles with teaching responsibilities");

            // assert h3 6 == Total educational support staff costs (primary schools only)
            var requiredSix = page.GetElementById("required-6");
            Assert.NotNull(requiredSix);
            DocumentAssert.Heading3(requiredSix, "Total educational support staff costs (primary schools only)");

            // assert optional div
            var optional = page.GetElementById("optional");
            Assert.NotNull(optional);
            // assert h2 == Optional:
            DocumentAssert.Heading2(optional, "Optional:");

            // assert h3 1 == Total income
            var optionalOne = page.GetElementById("optional-1");
            Assert.NotNull(optionalOne);
            DocumentAssert.Heading3(optionalOne, "Total income");

            // assert h3 2 == Total expenditure
            var optionalTwo = page.GetElementById("optional-2");
            Assert.NotNull(optionalTwo);
            DocumentAssert.Heading3(optionalTwo, "Total expenditure");

            // assert h3 3 == Total teacher costs
            var optionalThree = page.GetElementById("optional-3");
            Assert.NotNull(optionalThree);
            DocumentAssert.Heading3(optionalThree, "Total teacher costs");

            // assert h3 4 == Full time equivalent teachers
            var optionalFour = page.GetElementById("optional-4");
            Assert.NotNull(optionalFour);
            DocumentAssert.Heading3(optionalFour, "Full time equivalent teachers");

            // assert further help
            var furtherHelp = page.GetElementById("further-help");
            Assert.NotNull(furtherHelp);
            // assert h3 == Further help
            DocumentAssert.Heading3(furtherHelp, "Further help");

            // assert link submit-enquiry
            var helpLink = page.GetElementById("submit-enquiry");
            Assert.NotNull(helpLink);
            DocumentAssert.Link(helpLink, "submit an enquiry", "/submit-an-enquiry".ToAbsolute());
        }

        [Fact]
        public async Task CanNavigateBack()
        {
            var (page, school) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

            var anchor = page.QuerySelector(".govuk-back-link");
            Assert.NotNull(anchor);

            var newPage = await Follow(anchor);

            DocumentAssert.AssertPageUrl(newPage, Paths.SchoolPlanning(school.Urn).ToAbsolute());
        }

        [Fact]
        public async Task CanNavigateToSubmitEnquiry()
        {
            var (page, _) = await SetupNavigateInitPage(EstablishmentTypes.Maintained);

            var anchor = page.GetElementById("submit-enquiry");
            Assert.NotNull(anchor);

            var newPage = await Follow(anchor);

            DocumentAssert.AssertPageUrl(newPage, "/submit-an-enquiry".ToAbsolute());
        }

        private async Task<(IHtmlDocument page, School school)> SetupNavigateInitPage(string financeType)
        {
            var school = Fixture.Build<School>()
                .With(x => x.FinanceType, financeType)
                .Create();

            var page = await SetupEstablishment(school)
                .Navigate(Paths.SchoolPlanningHelp(school.Urn));

            return (page, school);
        }
    }
}
