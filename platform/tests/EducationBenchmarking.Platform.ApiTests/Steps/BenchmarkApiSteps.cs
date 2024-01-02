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
        private StringContent _content;

        public BenchmarkApiSteps()
        {
            var httpClientHandler = new HttpClientHandler();
            _httpClient = new HttpClient(httpClientHandler);
            _response = null!;
            _content = null!;
        }

        [Then(@"the response status code api is (.*)")]
        public void ThenTheResponseStatusCodeApiIs(int expectedStatusCode)
        {
            _response.StatusCode.Should().Be((HttpStatusCode)expectedStatusCode);
        }

        [Then(@"a valid comparator set of size '(.*)' should be returned")]
        public async Task ThenAValidComparatorSetOfSizeShouldBeReturned(string expectedSize)
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

        
        [When(@"I submit the request")]
        public async Task WhenISubmitTheRequest()
        {
            _response = await _httpClient.PostAsync($"{Config.Apis.Benchmark.Host}/api/comparator-set", _content);
        }


        [Given(@"I have a valid comparator set request of size set to '(.*)'")]
        public void GivenIHaveAValidComparatorSetRequestOfSizeSetTo(string size)
        {
            var jsonContent = "{\"includeSet\": true, \"size\": " + size + "}";
            _content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        [Given(@"I have a invalid comparator set request of size set to '(.*)'")]
        public void GivenIHaveAInvalidComparatorSetRequestOfSizeSetTo(string size)
        {
            var jsonContent = "{\"includeSet\": true, \"size\": " + size + "}";
            _content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }
    }
}