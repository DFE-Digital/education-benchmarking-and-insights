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

        [Then(@"the response status code api is (.*)")]
        public void ThenTheResponseStatusCodeApiIs(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }

        [Given(@"I want to create a comparator set")]
        public void GivenIWantToCreateAComparatorSet()
        {
        }

        [When(@"I send a request to get school comparators with includeset set to true and size set to '(.*)'")]
        public async Task WhenISendARequestToGetSchoolComparatorsWithIncludesetSetToTrueAndSizeSetTo(string size)
        {
            var jsonContent = "{\"includeSet\": true, \"size\": " + size + "}";
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync($"{Config.Apis.Benchmark.Host}/api/schools/comparator-set",
                content);
        }

        [Then(@"a valid school comparator set of size '(.*)' should be returned")]
        public async Task ThenAValidSchoolComparatorSetOfSizeShouldBeReturned(string expectedSize)
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

            schoolComparatorSet["results"].Children().Should().HaveCount(int.Parse(expectedSize));
        }

        [When(@"I send a request to get trust comparators with includeset set to true and size set to '(.*)'")]
        public async Task WhenISendARequestToGetTrustComparatorsWithIncludesetSetToTrueAndSizeSetTo(string size)
        {
            var jsonContent = "{\"includeSet\": true, \"size\": " + size + "}";
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            _response = await _httpClient.PostAsync($"{Config.Apis.Benchmark.Host}/api/trusts/comparator-set", content);
        }

        [Then(@"a valid trust comparator set of size '(.*)' should be returned")]
        public async Task ThenAValidTrustComparatorSetOfSizeShouldBeReturned(string expectedSize)
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

            academyComparatorSet["results"].Children().Should().HaveCount(int.Parse(expectedSize));
        }
    }
}