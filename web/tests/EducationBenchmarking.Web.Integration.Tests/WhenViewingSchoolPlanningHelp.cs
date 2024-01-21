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

            DocumentAssert.AssertPageUrl(page, Paths.SchoolPlanningHelp(school.Urn).ToAbsolute());

            var expectedBreadcrumbs = new[]
            {
                ("Home", Paths.ServiceHome.ToAbsolute()),
            };
            DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
            
            var backLink = page.QuerySelector(".govuk-back-link");
            Assert.NotNull(backLink);
            DocumentAssert.BackLink(backLink, "Back", Paths.SchoolPlanning(school.Urn).ToAbsolute());

            DocumentAssert.TitleAndH1(page, "Data required for ICFP", "Data required for ICFP");

            var required = page.GetElementById("required");
            Assert.NotNull(required);
            DocumentAssert.Heading2(required, "Required:");

            var requiredOne = page.GetElementById("required-1");
            Assert.NotNull(requiredOne);
            DocumentAssert.Heading3(required, "Length of timetable cycle");

            var requiredTwo = page.GetElementById("required-2");
            Assert.NotNull(requiredTwo);
            DocumentAssert.Heading3(requiredTwo, "Pupil figures");

            var requiredThree = page.GetElementById("required-3");
            Assert.NotNull(requiredThree);
            DocumentAssert.Heading3(requiredThree, "Teacher period allocation");

            var requiredFour = page.GetElementById("required-4");
            Assert.NotNull(requiredFour);
            DocumentAssert.Heading3(requiredFour, "Other teaching periods");

            var requiredFive = page.GetElementById("required-5");
            Assert.NotNull(requiredFive);
            DocumentAssert.Heading3(requiredFive, "Management roles with teaching responsibilities");

            var requiredSix = page.GetElementById("required-6");
            Assert.NotNull(requiredSix);
            DocumentAssert.Heading3(requiredSix, "Total educational support staff costs (primary schools only)");

            var optional = page.GetElementById("optional");
            Assert.NotNull(optional);
            DocumentAssert.Heading2(optional, "Optional:");

            var optionalOne = page.GetElementById("optional-1");
            Assert.NotNull(optionalOne);
            DocumentAssert.Heading3(optionalOne, "Total income");

            var optionalTwo = page.GetElementById("optional-2");
            Assert.NotNull(optionalTwo);
            DocumentAssert.Heading3(optionalTwo, "Total expenditure");

            var optionalThree = page.GetElementById("optional-3");
            Assert.NotNull(optionalThree);
            DocumentAssert.Heading3(optionalThree, "Total teacher costs");

            var optionalFour = page.GetElementById("optional-4");
            Assert.NotNull(optionalFour);
            DocumentAssert.Heading3(optionalFour, "Full time equivalent teachers");

            var furtherHelp = page.GetElementById("further-help");
            Assert.NotNull(furtherHelp);
            DocumentAssert.Heading3(furtherHelp, "Further help");

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
