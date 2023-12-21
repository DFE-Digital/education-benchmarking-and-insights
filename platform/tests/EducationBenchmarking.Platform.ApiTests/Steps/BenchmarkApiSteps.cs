
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
        }

        [When(@"I send a POST request to get comparators with the following payload:")]
        public async Task WhenISendAPostRequestToGetComparatorsWithTheFollowingPayload(Table table)
        {
            var requestData = table.Rows.ToDictionary(row => row["Type"], row => row["Body"]);
            var jsonContent = requestData["requestJson"];

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
            _response.EnsureSuccessStatusCode();
            var responseBody = await _response.Content.ReadAsStringAsync();
            var schoolComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody);
            schoolComparatorSet.Should().NotBeNull();
            schoolComparatorSet["results"].Should().NotBeNull();
            var schools = schoolComparatorSet["results"].ToObject<List<JObject>>();
            foreach (var school in schools)
            {
                school.Should().ContainKey("urn");
                school.Should().ContainKey("name");
                school.Should().ContainKey("financeType");
                school.Should().ContainKey("kind");
            }
        }

        [Then(@"the response size should be '(.*)'")]
        public async Task ThenTheResponseSizeShouldBe(string expectedSize)
        {
            var responseBody = await _response.Content.ReadAsStringAsync();
            var schoolComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody);
            schoolComparatorSet.Should().NotBeNull();
            schoolComparatorSet["results"].Should().NotBeNull();
            schoolComparatorSet["results"].Children().Should().HaveCount(int.Parse(expectedSize));
        }
        
        [When(@"I send a POST request to get trusts comparators with the following payload:")]
        public async Task WhenISendAPostRequestToGetTrustsComparatorsWithTheFollowingPayload(Table table)
        {
            var requestData = table.Rows.ToDictionary(row => row["Type"], row => row["Body"]);
            var jsonContent = requestData["requestJson"];

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync($"{Config.Apis.Benchmark.Host}/api/trusts/comparator-set", content);
        }
        [Then(@"the response body should contain a valid trusts Comparator Set")]
        public async Task ThenTheResponseBodyShouldContainAValidTrustsComparatorSet()
        {
            _response.EnsureSuccessStatusCode();
            var responseBody = await _response.Content.ReadAsStringAsync();
            var academyComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody);
            academyComparatorSet.Should().NotBeNull();
            academyComparatorSet["results"].Should().NotBeNull();
            var academies = academyComparatorSet["results"].ToObject<List<JObject>>();
            foreach (var academy in academies)
            {
                academy.Should().ContainKey("companyNumber");
                academy.Should().ContainKey("name");
            }
        }


    }
}
