using AngleSharp.Html.Parser;
using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests
{
    public class SchoolExpenditureIntegrationTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SchoolExpenditureIntegrationTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }


        [Fact]
        public async Task ShouldHaveCorrectHeading()
        {
            // Arrange + Act
            var response = await _client.GetAsync("/school-expenditure/5");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            var h1Element = document.QuerySelector("h1");
            var h1ElementText = h1Element.TextContent;
            
            // Assert
            Assert.Contains("Compare your costs for", h1ElementText);
        }


        [Fact]
        public async Task ShouldHaveAHeader()
        {
            // Arrange + Act
            var response = await _client.GetAsync("/school-expenditure/5");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            var headerElement = document.QuerySelector("header");
            
            // Assert
            Assert.NotNull(headerElement);
        }


        [Fact]
        public async Task ShouldHaveAFooter()
        {
            // Arrange + Act
            var response = await _client.GetAsync("/school-expenditure/5");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser(); 
            var document = parser.ParseDocument(content);
            var footerElement = document.QuerySelector("footer");
            // Assert
            Assert.NotNull(footerElement);
        }

        [Fact]
        public async Task ShouldHaveChangeLink()
        {
            // Arrange + Act
            var response = await _client.GetAsync("/school-expenditure/5");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser(); 
            var document = parser.ParseDocument(content);
            var changeLinkElement = document.QuerySelector("#change-link");
            var actual = changeLinkElement.TextContent;
            
            // Assert
            Assert.Contains("Change", actual);
        }


        [Fact]
        public async Task ShouldHaveViewYourComparatorLink()
        {
            // Arrange + Act
            var response = await _client.GetAsync("/school-expenditure/5");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser(); 
            var document = parser.ParseDocument(content);
            var viewYourComparatorLinkElement = document.QuerySelector("#view-comparator-set");
            var actual = viewYourComparatorLinkElement.TextContent;

            // Assert
            Assert.Contains("View your comparator set", actual);
        }


        [Fact]
        public async Task ShouldHaveBackLink()
        {
            // Arrange + Act
            var response = await _client.GetAsync("/school-expenditure/5");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var parser = new HtmlParser();
            var document = parser.ParseDocument(content);
            var backLinkElement = document.QuerySelector("#back-link");
            var actual = backLinkElement.TextContent;

            // Assert
            Assert.Contains("Back", actual);
        }
    }
}


