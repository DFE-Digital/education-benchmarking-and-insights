using System.Net;
using System.Text;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.Domain.Responses.Characteristics;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow.Assist;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class BenchmarkComparatorSetSteps
{
    private const string ComparatorSetCharacteristicsKey = "comparator-set-characteristics";
    private const string ComparatorSetKey = "get-comparator-set";
    private readonly BenchmarkApiDriver _api;

    public BenchmarkComparatorSetSteps(BenchmarkApiDriver api)
    {
        _api = api;
    }

    [Then("the comparator result should be ok")]
    public void ThenTheComparatorResultShouldBeOk()
    {
        var response = _api[ComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("a valid comparator set of size '(.*)' should be returned")]
    public async Task ThenAValidComparatorSetOfSizeShouldBeReturned(string expectedSize)
    {
        var response = _api[ComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var responseBody = await response.Content.ReadAsStringAsync();
        var schoolComparatorSet = JsonConvert.DeserializeObject<JObject>(responseBody) ?? throw new ArgumentNullException();

        schoolComparatorSet["results"].Should().NotBeNull();

        var schools = schoolComparatorSet["results"]?.ToObject<List<JObject>>() ?? throw new ArgumentNullException();

        foreach (var school in schools)
        {
            school.Should().ContainKey("urn");
            school.Should().ContainKey("name");
            school.Should().ContainKey("financeType");
            school.Should().ContainKey("kind");
        }

        schoolComparatorSet["results"]?.Children().Should().HaveCount(int.Parse(expectedSize));
    }


    [Given("I have a valid comparator set request of size set to '(.*)'")]
    public void GivenIHaveAValidComparatorSetRequestOfSizeSetTo(string comparatorSize)
    {
        var content = new
        {
            includeSet = "true",
            size = comparatorSize
        };

        _api.CreateRequest(ComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/comparator-set", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("I have a invalid comparator set request")]
    public void GivenIHaveAInvalidComparatorSetRequest()
    {
        var content = new
        {
            includeSet = "true",
            size = "7",
            sortMethod = new { sortBy = "bad request" }
        };

        _api.CreateRequest(ComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/comparator-set", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Given("a valid comparator set characteristics request")]
    public void GivenAValidComparatorSetCharacteristicsRequest()
    {
        _api.CreateRequest(ComparatorSetCharacteristicsKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/comparator-set/characteristics", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the comparator set characteristics request")]
    [When("I submit the comparator set request")]
    public async Task WhenISubmitTheComparatorSetRequest()
    {
        await _api.Send();
    }

    [Then("the comparator set characteristics result should be:")]
    public async Task ThenTheComparatorSetCharacteristicsResultShouldBe(Table expectedTable)
    {
        var response = _api[ComparatorSetCharacteristicsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsStringAsync();
        var results = content.FromJson<List<Characteristic>>() ?? throw new ArgumentNullException(content);
        var set = new List<dynamic>();
        foreach (var result in results)
        {
            set.Add(new { result.Code, result.Description });
        }

        expectedTable.CompareToDynamicSet(set, false);
    }

    [Then("the comparator result should be bad request")]
    public void ThenTheComparatorResultShouldBeBadRequest()
    {
        var response = _api[ComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}