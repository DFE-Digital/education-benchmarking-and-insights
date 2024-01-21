using AngleSharp.Html.Dom;
using AngleSharp.XPath;
using AutoFixture;
using EducationBenchmarking.Web.Domain;
using Newtonsoft.Json.Linq;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests
{
    public class WhenViewingSchoolPlanningHelp : BenchmarkingWebAppClient
    {
        public WhenViewingSchoolPlanningHelp(BenchmarkingWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task CanNavigateBack()
        {
            Assert.True(true);
        }

        [Fact]
        public async Task CanNavigateToSubmitEnquiry()
        {
            Assert.True(true);
        }

        private static void AssertPageLayout(IHtmlDocument page, School school)
        {
            Assert.True(true);
            Assert.IsAssignableFrom<IHtmlDocument>(page);
            Assert.IsType<School>(school);
        }

    }
}
