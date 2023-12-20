using System.Net;
using EducationBenchmarking.platform.Api.Tests.TestSupport;
using FluentAssertions;

namespace EducationBenchmarking.platform.Api.Tests.Steps
{
    [Binding]
    public class InsightsApiSteps
    {
        private readonly HttpClient _httpClient;
        private HttpResponseMessage _response;

        public InsightsApiSteps()
        {
            var httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(httpClientHandler);
        }

       
        [Given(@"the Api is running")]
        public void GivenTheApiIsRunning()
        {
        }

        [When(@"I send a GET request to the ""(.*)"" endpoint")]
        public async Task WhenISendAGETRequestToTheEndpoint(string endpoint)
        {
            _response = await _httpClient.GetAsync($"{Config.Apis.Insight.Host}/{endpoint}");
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }
        
    }
}
