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
        private const string ComparatorSetCharacteristicsKey = "comparator-set-characteristics";
        private const string GetComparatorSetKey = "get-comparator-set";
        private const string FsmBandingKey = "free-school-meal-banding";
        private const string SchoolSizeBandingKey = "school-size-banding";
        private readonly ApiDriver _api = new(Config.Apis.Benchmark ?? throw new NullException(Config.Apis.Benchmark));


        public BenchmarkApiSteps()
        {
            var httpClientHandler = new HttpClientHandler();
        }

        [Then(@"the comparator result should be ok")]
        public void ThenTheComparatorResultShouldBeOk()
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

        [Given(@"I have a invalid comparator set request")]
        public void GivenIHaveAInvalidComparatorSetRequest()
        {
            var content = new
            {
                includeSet = "true", size="7", sortMethod = new { sortBy = "bad request"}
            };
        
            _api.CreateRequest(GetComparatorSetKey, new HttpRequestMessage
            {
                RequestUri = new Uri("/api/comparator-set", UriKind.Relative),
                Method = HttpMethod.Post,
                Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
            });
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

        [Given(@"a valid fsm banding request")]
        public void GivenAValidFsmBandingRequest()
        {
            _api.CreateRequest(FsmBandingKey, new HttpRequestMessage
            {
                RequestUri = new Uri("/api/free-school-meal/bandings", UriKind.Relative),
                Method = HttpMethod.Get
            });
        }

        [When(@"I submit the banding request")]
        public async Task WhenISubmitTheBandingRequest()
        {
            await _api.Send();
        }

        [Then(@"the free school meal banding result should be ok")]
        public void ThenTheFreeSchoolMealBandingResultShouldBeOk()
        {
            var response = _api[FsmBandingKey].Response ?? throw new NullException(_api[FsmBandingKey].Response);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Given(@"a valid school size banding request")]
        public void GivenAValidSchoolSizeBandingRequest()
        {
            _api.CreateRequest(SchoolSizeBandingKey, new HttpRequestMessage
            {
                RequestUri = new Uri("/api/school-size/bandings", UriKind.Relative),
                Method = HttpMethod.Get
            });
        }

        [Then(@"the comparator result should be bad request")]
        public void ThenTheComparatorResultShouldBeBadRequest()
        {
            var response = _api[GetComparatorSetKey].Response ?? throw new NullException(_api[GetComparatorSetKey].Response);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Then(@"the school size banding result should be ok")]
        public void ThenTheSchoolSizeBandingResultShouldBeOk()
        {
            var response = _api[SchoolSizeBandingKey].Response ?? throw new NullException(_api[SchoolSizeBandingKey].Response);

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}