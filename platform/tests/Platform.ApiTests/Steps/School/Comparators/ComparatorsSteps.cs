using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.ApiTests.TestDataHelpers;
using Xunit;
using System.IO;

namespace Platform.ApiTests.Steps.School.Comparators;

[Binding]
[Scope(Feature = "School Comparators")]
public class ComparatorsSteps(SchoolApiDriver api)
{
    private const string ComparatorsKey = "comparators";
    private const string RouteFolder = "School";
    private const string SubFolder = "Comparators";

    [Given("a valid comparators request with URN '(.*)' and a fully populated characteristics body")]
    public void GivenAValidComparatorsRequestWithUrnAndAFullyPopulatedCharacteristicsBody(string urn)
    {
        var body = @"
        {
            ""financeType"": { ""values"": [""Maintained""] },
            ""overallPhase"": { ""values"": [""Primary""] },
            ""laName"": { ""values"": [""Camden""] },
            ""schoolPosition"": { ""values"": [""Urban""] },
            ""isPFISchool"": { ""values"": false },
            ""londonWeighting"": { ""values"": [""Inner""] },
            ""ofstedDescription"": { ""values"": [""Good""] },
            ""totalPupils"": { ""from"": 100, ""to"": 500 },
            ""buildingAverageAge"": { ""from"": 10, ""to"": 50 },
            ""totalInternalFloorArea"": { ""from"": 1000, ""to"": 5000 },
            ""percentFreeSchoolMeals"": { ""from"": 10, ""to"": 30 },
            ""percentSpecialEducationNeeds"": { ""from"": 5, ""to"": 20 },
            ""totalPupilsSixthForm"": { ""from"": 0, ""to"": 100 },
            ""kS2Progress"": { ""from"": -2, ""to"": 2 },
            ""kS4Progress"": { ""from"": -0.5, ""to"": 0.5 },
            ""schoolsInTrust"": { ""from"": 1, ""to"": 5 },
            ""percentWithVI"": { ""from"": 0, ""to"": 5 },
            ""percentWithSPLD"": { ""from"": 0, ""to"": 5 },
            ""percentWithSLD"": { ""from"": 0, ""to"": 5 },
            ""percentWithSLCN"": { ""from"": 0, ""to"": 5 },
            ""percentWithSEMH"": { ""from"": 0, ""to"": 5 },
            ""percentWithPMLD"": { ""from"": 0, ""to"": 5 },
            ""percentWithPD"": { ""from"": 0, ""to"": 5 },
            ""percentWithOTH"": { ""from"": 0, ""to"": 5 },
            ""percentWithMSI"": { ""from"": 0, ""to"": 5 },
            ""percentWithMLD"": { ""from"": 0, ""to"": 5 },
            ""percentWithHI"": { ""from"": 0, ""to"": 5 },
            ""percentWithASD"": { ""from"": 0, ""to"": 5 }
        }";

        api.CreateRequest(ComparatorsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparators", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        });
    }

    [Given("a valid comparators request with URN '(.*)' and an empty characteristics body")]
    public void GivenAValidComparatorsRequestWithUrnAndAnEmptyCharacteristicsBody(string urn)
    {
        var body = "{}";

        api.CreateRequest(ComparatorsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparators", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        });
    }

    [Given("a valid comparators request with URN '(.*)' and a partially populated characteristics body")]
    public void GivenAValidComparatorsRequestWithUrnAndAPartiallyPopulatedCharacteristicsBody(string urn)
    {
        var body = @"
        {
            ""financeType"": { ""values"": [""Academy""] },
            ""totalPupils"": { ""from"": 200, ""to"": 800 }
        }";

        api.CreateRequest(ComparatorsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/schools/{urn}/comparators", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(body, Encoding.UTF8, "application/json")
        });
    }

    [Given("an invalid comparators request with (.*)")]
    public void GivenAnInvalidComparatorsRequestWith(string parameter)
    {
        var request = new HttpRequestMessage
        {
            RequestUri = new Uri("/api/schools/777042/comparators", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent("{}", Encoding.UTF8, "application/json")
        };

        switch (parameter)
        {
            case "invalid api version":
                request.Headers.Add("x-api-version", "invalid");
                break;
            default:
                throw new ArgumentException($"Unknown parameter: {parameter}");
        }

        api.CreateRequest(ComparatorsKey, request);
    }

    [When("I submit the comparators request")]
    public async Task WhenISubmitTheComparatorsRequest()
    {
        await api.Send();
    }

    [Then("the comparators result should be '(.*)' and match the expected output in '(.*)'")]
    public async Task ThenTheComparatorsResultShouldBeAndMatchTheExpectedOutputIn(string result, string testFile)
    {
        var response = api[ComparatorsKey].Response;

        switch (result)
        {
            case "ok":
                AssertHttpResponse.IsOk(response);
                break;
            case "bad request":
                AssertHttpResponse.IsBadRequest(response);
                break;
            case "not found":
                AssertHttpResponse.IsNotFound(response);
                break;
            default:
                Assert.Fail($"unexpected result: {result}");
                break;
        }

        var content = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(content))
        {
            Assert.Empty(content);
            return;
        }

        var actual = content.StartsWith("[") ? (JToken)JArray.Parse(content) : JObject.Parse(content);

        var expected = content.StartsWith("[")
            ? (JToken)TestDataProvider.GetJsonArrayData(testFile, RouteFolder, SubFolder)
            : TestDataProvider.GetJsonObjectData(testFile, RouteFolder, SubFolder);

        actual.AssertDeepEquals(expected);
    }
}