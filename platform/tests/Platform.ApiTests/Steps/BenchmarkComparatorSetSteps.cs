using System.Net;
using System.Text;
using AutoFixture;
using FluentAssertions;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using TechTalk.SpecFlow.Assist;

namespace Platform.ApiTests.Steps;

[Binding]
public class BenchmarkComparatorSetSteps(BenchmarkApiDriver api)
{
    private const string DefaultComparatorSetKey = "default-comparator-set";
    private const string UserDefinedComparatorSetKey = "user-defined-comparator-set";
    private const string UserDefinedTrustComparatorSetKey = "user-defined-trust-comparator-set";
    private const string UserId = "api.test@example.com";
    private readonly Fixture _fixture = new();

    [Then("the comparator result should be accepted")]
    public void ThenTheComparatorResultShouldBeAccepted()
    {
        var response = api[UserDefinedComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    [Given("I have a valid user defined comparator set request for school id '(.*)'")]
    public void GivenIHaveAValidUserDefinedComparatorSetRequestForSchoolId(string urn)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .Concat(new[]
            {
                urn
            })
            .ToArray();
        PutUserDefinedComparatorRequest(urn, set);
    }

    [Given("I have an invalid user defined comparator set request for school id '(.*)'")]
    public void GivenIHaveAnInvalidUserDefinedComparatorSetRequestForSchoolId(string urn)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .ToArray();
        PutUserDefinedComparatorRequest(urn, set);
    }

    [Given("I have an invalid delete user defined comparator set request for school id '(.*)'")]
    public void GivenIHaveAnInvalidDeleteUserDefinedComparatorSetRequestForSchoolId(string urn)
    {
        DeleteUserDefinedComparatorRequest(urn, Guid.NewGuid());
    }

    [Given("I have a valid default comparator set get request for school id '(.*)'")]
    public void GivenIHaveAValidDefaultComparatorSetGetRequestForSchoolId(string urn)
    {
        api.CreateRequest(DefaultComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/default", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a valid user defined comparator set get request for school id '(.*)' containing:")]
    public async Task GivenIHaveAValidUserDefinedComparatorSetGetRequestForSchoolIdContaining(string urn, Table table)
    {
        var set = GetFirstColumnsFromTableRowsAsString(table)
            .Concat(new[] { urn })
            .ToArray();
        var identifier = PutUserDefinedComparatorRequest(urn, set);
        await WhenISubmitTheUserDefinedComparatorSetRequest();

        api.CreateRequest(UserDefinedComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then("the trust comparator result should be accepted")]
    public void ThenTheTrustComparatorResultShouldBeAccepted()
    {
        var response = api[UserDefinedTrustComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    [Then("the trust comparator result should be bad request")]
    public void ThenTheTrustComparatorResultShouldBeBadRequest()
    {
        var response = api[UserDefinedTrustComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Then("the trust comparator result should be not found")]
    public void ThenTheTrustComparatorResultShouldBeNotFound()
    {
        var response = api[UserDefinedTrustComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the trust comparator result should be ok")]
    public void ThenTheTrustComparatorResultShouldBeOk()
    {
        var response = api[UserDefinedTrustComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Given("I have a valid user defined comparator set request for trust id '(.*)'")]
    public void GivenIHaveAValidUserDefinedComparatorSetRequestForTrustId(string companyNumber)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .Concat(new[]
            {
                companyNumber
            })
            .ToArray();
        PutUserDefinedTrustComparatorRequest(companyNumber, set);
    }

    [Given("I have an invalid user defined comparator set request for trust id '(.*)'")]
    public void GivenIHaveAnInvalidUserDefinedComparatorSetRequestForTrustId(string companyNumber)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .ToArray();
        PutUserDefinedTrustComparatorRequest(companyNumber, set);
    }

    [Given("I have an invalid delete user defined comparator set request for trust id '(.*)'")]
    public void GivenIHaveAnInvalidDeleteUserDefinedComparatorSetRequestForTrustId(string companyNumber)
    {
        DeleteUserDefinedTrustComparatorRequest(companyNumber, Guid.NewGuid());
    }

    [Given("I have a valid user defined comparator set get request for trust id '(.*)' containing:")]
    public async Task GivenIHaveAValidUserDefinedComparatorSetGetRequestForTrustIdContaining(string companyNumber, Table table)
    {
        var set = GetFirstColumnsFromTableRowsAsString(table)
            .Concat(new[] { companyNumber })
            .ToArray();
        var identifier = PutUserDefinedTrustComparatorRequest(companyNumber, set);
        await WhenISubmitTheUserDefinedComparatorSetRequest();

        api.CreateRequest(UserDefinedTrustComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/trust/{companyNumber}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a valid delete user defined comparator set get request for trust id '(.*)' containing:")]
    public async Task GivenIHaveAValidDeleteUserDefinedComparatorSetGetRequestForTrustIdContaining(string companyNumber, Table table)
    {
        var set = GetFirstColumnsFromTableRowsAsString(table)
            .Concat(new[] { companyNumber })
            .ToArray();
        var identifier = PutUserDefinedTrustComparatorRequest(companyNumber, set);
        await WhenISubmitTheUserDefinedComparatorSetRequest();

        DeleteUserDefinedTrustComparatorRequest(companyNumber, identifier);
    }

    [When("I submit the default comparator set request")]
    [When("I submit the user defined comparator set request")]
    [When("I submit the user defined trust comparator set request")]
    public async Task WhenISubmitTheUserDefinedComparatorSetRequest()
    {
        await api.Send();
    }

    [Then("the comparator result should be bad request")]
    public void ThenTheComparatorResultShouldBeBadRequest()
    {
        var response = api[UserDefinedComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Then("the comparator result should be not found")]
    public void ThenTheComparatorResultShouldBeNotFound()
    {
        var response = api[UserDefinedComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the comparator result should be ok")]
    public void ThenTheComparatorResultShouldBeOk()
    {
        var response = api[UserDefinedComparatorSetKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the default comparator set result should contain comparator buildings:")]
    private async Task ThenTheDefaultComparatorSetResultShouldContainComparatorBuildings(Table table)
    {
        var response = api[DefaultComparatorSetKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetSchool>();

        var set = new List<dynamic>();
        foreach (var urn in result.Building ?? [])
        {
            set.Add(new { Urn = urn });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the default comparator set result should contain comparator pupils:")]
    private async Task ThenTheDefaultComparatorSetResultShouldContainComparatorPupils(Table table)
    {
        var response = api[DefaultComparatorSetKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetSchool>();

        var set = new List<dynamic>();
        foreach (var urn in result.Pupil ?? [])
        {
            set.Add(new { Urn = urn });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the user defined comparator set result should contain comparator buildings:")]
    private async Task ThenTheUserDefinedComparatorSetResultShouldContainComparatorBuildings(Table table)
    {
        var response = api[UserDefinedComparatorSetKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetSchool>();

        var set = new List<dynamic>();
        foreach (var urn in result.Building ?? [])
        {
            set.Add(new { Urn = urn });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the user defined trust comparator set result should contain comparators:")]
    private async Task ThenTheUserDefinedTrustComparatorSetResultShouldContainComparators(Table table)
    {
        var response = api[UserDefinedTrustComparatorSetKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetUserDefinedTrust>();

        var set = new List<dynamic>();
        foreach (var companyNumber in result.Set ?? [])
        {
            set.Add(new { CompanyNumber = companyNumber });
        }

        table.CompareToDynamicSet(set, false);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(Table table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }

    private Guid PutUserDefinedComparatorRequest(string urn, string[] set)
    {
        var identifier = Guid.NewGuid();
        var content = new ComparatorSetUserDefinedRequest
        {
            UserId = UserId,
            Set = set
        };

        api.CreateRequest(UserDefinedComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });

        return identifier;
    }

    private void DeleteUserDefinedComparatorRequest(string urn, Guid identifier)
    {
        api.CreateRequest(UserDefinedComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Delete
        });
    }

    private Guid PutUserDefinedTrustComparatorRequest(string companyNumber, string[] set)
    {
        var identifier = Guid.NewGuid();
        var content = new ComparatorSetUserDefinedRequest
        {
            UserId = UserId,
            Set = set
        };

        api.CreateRequest(UserDefinedTrustComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/trust/{companyNumber}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });

        return identifier;
    }

    private void DeleteUserDefinedTrustComparatorRequest(string companyNumber, Guid identifier)
    {
        api.CreateRequest(UserDefinedTrustComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/trust/{companyNumber}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Delete
        });
    }
}