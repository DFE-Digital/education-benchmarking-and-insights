using System.Net;
using System.Text;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using EducationBenchmarking.Platform.Domain.Responses.Characteristics;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow.Assist;
using Xunit;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps
{
    [Binding]
    public class BenchmarkApiSteps
    {
        private readonly HttpClient _httpClient;
        private HttpResponseMessage _response;
        private StringContent _content;
        private const string ComparatorSetCharacteristicsKey = "comparator-set-characteristics";
        private const string GetComparatorSetKey = "get-comparator-set";
        private readonly ApiDriver _api = new(Config.Apis.Benchmark ?? throw new NullException(Config.Apis.Benchmark));


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
            var response = _api[GetComparatorSetKey].Response ??
                           throw new NullException(_api[GetComparatorSetKey].Response);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"a valid comparator set of size '(.*)' should be returned")]
        public async Task ThenAValidComparatorSetOfSizeShouldBeReturned(string expectedSize)
        { 
            var response = _api[GetComparatorSetKey].Response ?? throw new NullException(_api[GetComparatorSetKey].Response);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            
            var responseBody = await response.Content.ReadAsStringAsync();
            var schoolComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody);
            schoolComparatorSet.Should().NotBeNull();
            schoolComparatorSet!["results"].Should().NotBeNull();
            var schools = schoolComparatorSet["results"]!.ToObject<List<JObject>>();
            foreach (var school in schools!)
            {
                school.Should().ContainKey("urn");
                school.Should().ContainKey("name");
                school.Should().ContainKey("financeType");
                school.Should().ContainKey("kind");
            }

            schoolComparatorSet["results"]!.Children().Should().HaveCount(int.Parse(expectedSize));
        }


        [When(@"I submit the request")]
        public async Task WhenISubmitTheRequest()
        {
            _response = await _httpClient.PostAsync($"{Config.Apis.Benchmark.Host}/api/comparator-set", _content);
        }


        [Given(@"I have a valid comparator set request of size set to '(.*)'")]
        public void GivenIHaveAValidComparatorSetRequestOfSizeSetTo(string size)
        {
            var content = new
            {
                includeSet = "true", size=size
            };
        
            _api.CreateRequest(GetComparatorSetKey, new HttpRequestMessage
            {
                RequestUri = new Uri("/api/comparator-set", UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
            });
        }

        [Given(@"I have a invalid comparator set request of size set to '(.*)'")]
        public void GivenIHaveAInvalidComparatorSetRequestOfSizeSetTo(string size)
        {
            var jsonContent = "{\"includeSet\": true, \"size\": " + size + "}";
            _content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        }

        [Given(@"a valid comparator set characteristics request")]
        public void GivenAValidComparatorSetCharacteristicsRequest()
        {
            _api.CreateRequest(ComparatorSetCharacteristicsKey, new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/comparator-set/characteristics", UriKind.Relative),
                Method = HttpMethod.Get
            });
        }

        [When(@"I submit the comparator set request")]
        public async Task WhenISubmitTheComparatorSetRequest()
        {
            await _api.Send();
        }

        [Then(@"the comparator set result should be:")]
        public async Task ThenTheComparatorSetResultShouldBe(Table expectedTable)
        {
            var response = _api[ComparatorSetCharacteristicsKey].Response ??
                           throw new NullException(_api[ComparatorSetCharacteristicsKey].Response);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();
            var results = content.FromJson<List<Characteristic>>() ?? throw new NullException(content);
            var set = new List<dynamic>();
            foreach (var result in results)
            {
                set.Add(new { result.Code, result.Description });
            }
        
            expectedTable.CompareToDynamicSet(set,false);
            

        }
    }
}