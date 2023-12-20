
using System.Net;
using System.Text;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EducationBenchmarking.Platform.ApiTests.Steps
{
    [Binding]
    public class BenchmarkApiSteps
    {
        private readonly HttpClient _httpClient;
        private HttpResponseMessage _response;

        public BenchmarkApiSteps()
        {
            var httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(httpClientHandler);
        }

        [Given(@"the School Comparator Set API is running")]
        public void GivenTheSchoolComparatorSetApiIsRunning()
        {
            // Additional setup code for initializing or checking the API's availability
        }

        [When(@"I send a POST request to get comparators with the following data:")]
        public async Task WhenISendAPostRequestToGetComparatorsWithTheFollowingData(Table table)
        {
            var requestData = table.Rows.ToDictionary(row => row["requestJson"], row => row["requestJson"]);
            var jsonContent = JsonConvert.SerializeObject(requestData);

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync($"{Config.Apis.Benchmark.Host}/api/schools/comparator-set", content);
        }

        [Then(@"the response status code for benchmark api should be (.*)")]
        public void ThenTheResponseStatusCodeForBenchmarkApiShouldBe(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }

        [Then(@"the response body should contain a valid School Comparator Set")]
        public async Task ThenTheResponseBodyShouldContainAValidSchoolComparatorSet()
        {
            // Implement assertions for the structure and validity of the School Comparator Set in the response body
            _response.EnsureSuccessStatusCode(); // Ensure the response is successful

            var responseBody = await _response.Content.ReadAsStringAsync();
            var schoolComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody);

            schoolComparatorSet.Should().NotBeNull();
            schoolComparatorSet["results"].Should().NotBeNull();
            schoolComparatorSet["results"].Children().Should().HaveCountGreaterThan(0);
            // Add more specific assertions based on the structure of your School Comparator Set
        }

        [Then(@"the response size should be '(.*)'")]
        public async Task ThenTheResponseSizeShouldBe(string expectedSize)
        {
            // Implement assertions to check the size of the School Comparator Set in the response
            var responseBody = await _response.Content.ReadAsStringAsync();
            var schoolComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody);

            schoolComparatorSet.Should().NotBeNull();
            schoolComparatorSet["results"].Should().NotBeNull();
            schoolComparatorSet["results"].Children().Should().HaveCount(int.Parse(expectedSize));
        }
    }
}
