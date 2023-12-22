using System.Net;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using FluentAssertions;

namespace EducationBenchmarking.Platform.ApiTests.Steps
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
            _response = null!;
        }

       
        [Given(@"the insight Api is running")]
        public void GivenTheInsightApiIsRunning()
        {
        }

        [When(@"I send the expenditure get request to the API")]
        public async Task WhenISendTheExpenditureGetRequestToTheApi()
        {
            _response = await _httpClient.GetAsync($"{Config.Apis.Insight.Host}/api/schools/expenditure");
        }

        [Then(@"the response status code should be (.*)")]
        public void ThenTheResponseStatusCodeShouldBe(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }

        [Then(@"I should get a response body")]
        public async Task ThenIShouldGetAResponseBody()
        {
            var responseBody = await _response.Content.ReadAsStringAsync();
            responseBody.Should().NotBeNullOrEmpty();
        }
    }
}
