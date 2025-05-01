using System.Text;
using AutoFixture;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Requests;
using Platform.Api.Benchmark.Features.UserData.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Assist;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Benchmark Comparator set Endpoint Testing")]
public class BenchmarkComparatorSetSteps(BenchmarkApiDriver api)
{
    private const string DefaultComparatorSetKey = "default-comparator-set";
    private const string UserDefinedComparatorSetKey = "user-defined-comparator-set";
    private const string UserDefinedTrustComparatorSetKey = "user-defined-trust-comparator-set";
    private const string UserDefinedDataKey = "user-defined-data";
    private readonly Fixture _fixture = new();
    private readonly string _userGuid = Guid.NewGuid().ToString();

    [Then("the comparator result should be accepted")]
    public void ThenTheComparatorResultShouldBeAccepted()
    {
        AssertHttpResponse.IsAccepted(api[UserDefinedComparatorSetKey].Response);
    }

    [Given("I have a valid user defined comparator set request for school id '(.*)'")]
    public void GivenIHaveAValidUserDefinedComparatorSetRequestForSchoolId(string urn)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .Concat([urn])
            .ToArray();
        PostUserDefinedComparatorRequest(urn, set);
    }

    [Given("I have an invalid user defined comparator set request for school id '(.*)'")]
    public void GivenIHaveAnInvalidUserDefinedComparatorSetRequestForSchoolId(string urn)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .ToArray();
        PostUserDefinedComparatorRequest(urn, set);
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
    public async Task GivenIHaveAValidUserDefinedComparatorSetGetRequestForSchoolIdContaining(string urn, DataTable table)
    {
        var set = GetFirstColumnsFromTableRowsAsString(table)
            .Concat([urn])
            .ToArray();
        PostUserDefinedComparatorRequest(urn, set);
        await WhenISubmitTheUserDefinedComparatorSetRequest();

        GetUserDefinedComparatorRequest("school", urn);
    }

    [Then("the trust comparator result should be accepted")]
    public void ThenTheTrustComparatorResultShouldBeAccepted()
    {
        AssertHttpResponse.IsAccepted(api[UserDefinedTrustComparatorSetKey].Response);
    }

    [Then("the trust comparator result should be bad request")]
    public void ThenTheTrustComparatorResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[UserDefinedTrustComparatorSetKey].Response);
    }

    [Then("the trust comparator result should be not found")]
    public void ThenTheTrustComparatorResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[UserDefinedTrustComparatorSetKey].Response);
    }

    [Then("the trust comparator result should be ok")]
    public void ThenTheTrustComparatorResultShouldBeOk()
    {
        AssertHttpResponse.IsOk(api[UserDefinedTrustComparatorSetKey].Response);
    }

    [Given("I have a valid user defined comparator set request for company number '(.*)'")]
    public void GivenIHaveAValidUserDefinedComparatorSetRequestForCompanyNumber(string companyNumber)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .Concat([companyNumber])
            .ToArray();
        PostUserDefinedTrustComparatorRequest(companyNumber, set);
    }

    [Given("I have an invalid user defined comparator set request for company number '(.*)'")]
    public void GivenIHaveAnInvalidUserDefinedComparatorSetRequestForCompanyNumber(string companyNumber)
    {
        var set = _fixture
            .CreateMany<int>(5)
            .Select(i => i.ToString())
            .ToArray();
        PostUserDefinedTrustComparatorRequest(companyNumber, set);
    }

    [Given("I have an invalid delete user defined comparator set request for company number '(.*)'")]
    public void GivenIHaveAnInvalidDeleteUserDefinedComparatorSetRequestForCompanyNumber(string companyNumber)
    {
        DeleteUserDefinedTrustComparatorRequest(companyNumber, Guid.NewGuid());
    }

    [Given("I have a valid user defined comparator set get request for company number '(.*)' containing:")]
    public async Task GivenIHaveAValidUserDefinedComparatorSetGetRequestForCompanyNumberContaining(string companyNumber, DataTable table)
    {
        var set = GetFirstColumnsFromTableRowsAsString(table)
            .Concat([companyNumber])
            .ToArray();
        PostUserDefinedTrustComparatorRequest(companyNumber, set);
        await WhenISubmitTheUserDefinedComparatorSetRequest();

        GetUserDefinedComparatorRequest("trust", companyNumber);
    }

    [Given("I have a valid delete user defined comparator set get request for company number '(.*)' containing:")]
    public async Task GivenIHaveAValidDeleteUserDefinedComparatorSetGetRequestForCompanyNumberContaining(string companyNumber, DataTable table)
    {
        var set = GetFirstColumnsFromTableRowsAsString(table)
            .Concat([companyNumber])
            .ToArray();
        PostUserDefinedTrustComparatorRequest(companyNumber, set);
        await WhenISubmitTheUserDefinedComparatorSetRequest();

        GetUserDefinedComparatorRequest("trust", companyNumber);
        await api.Send();

        var response = api[UserDefinedDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<UserData[]>();
        Assert.NotNull(result);
        Assert.Single(result);

        DeleteUserDefinedTrustComparatorRequest(companyNumber, result.Select(r => new Guid(r.Id!)).FirstOrDefault());
    }

    [When("I submit the default comparator set request")]
    [When("I submit the user defined school comparator set request")]
    [When("I submit the user defined trust comparator set request")]
    public async Task WhenISubmitTheUserDefinedComparatorSetRequest()
    {
        await api.Send();
    }

    [Then("the comparator result should be bad request")]
    public void ThenTheComparatorResultShouldBeBadRequest()
    {
        AssertHttpResponse.IsBadRequest(api[UserDefinedComparatorSetKey].Response);
    }

    [Then("the comparator result should be not found")]
    public void ThenTheComparatorResultShouldBeNotFound()
    {
        AssertHttpResponse.IsNotFound(api[UserDefinedComparatorSetKey].Response);
    }

    [Then("the comparator result should be ok")]
    public void ThenTheComparatorResultShouldBeOk()
    {
        AssertHttpResponse.IsOk(api[UserDefinedComparatorSetKey].Response);
    }

    [Then("the default comparator set result should contain comparator buildings:")]
    private async Task ThenTheDefaultComparatorSetResultShouldContainComparatorBuildings(DataTable table)
    {
        var response = api[DefaultComparatorSetKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetSchool>();

        var set = new List<dynamic>();
        foreach (var urn in result.Building ?? [])
        {
            set.Add(new
            {
                Urn = urn
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the default comparator set result should contain comparator pupils:")]
    private async Task ThenTheDefaultComparatorSetResultShouldContainComparatorPupils(DataTable table)
    {
        var response = api[DefaultComparatorSetKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetSchool>();

        var set = new List<dynamic>();
        foreach (var urn in result.Pupil ?? [])
        {
            set.Add(new
            {
                Urn = urn
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("new user data should be created for school id '(.*)'")]
    public async Task ThenNewUserDataShouldBeCreatedForSchoolId(string urn)
    {
        var response = api[UserDefinedDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<UserData[]>();

        api.CreateRequest(UserDefinedComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/user-defined/{result.First().Id}", UriKind.Relative),
            Method = HttpMethod.Get
        });

        await api.Send();
    }

    [Then("new user data should be created for company number '(.*)'")]
    public async Task ThenNewUserDataShouldBeCreatedForCompanyNumber(string companyNumber)
    {
        var response = api[UserDefinedDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<UserData[]>();

        api.CreateRequest(UserDefinedTrustComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/trust/{companyNumber}/user-defined/{result.First().Id}", UriKind.Relative),
            Method = HttpMethod.Get
        });

        await api.Send();
    }

    [Then("the user defined comparator set result should contain comparators:")]
    private async Task ThenTheUserDefinedComparatorSetResultShouldContainComparators(DataTable table)
    {
        var response = api[UserDefinedComparatorSetKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetUserDefinedSchool>();

        var set = new List<dynamic>();
        foreach (var urn in result.Set ?? [])
        {
            set.Add(new
            {
                Urn = urn
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Then("the user defined trust comparator set result should contain comparators:")]
    private async Task ThenTheUserDefinedTrustComparatorSetResultShouldContainComparators(DataTable table)
    {
        var response = api[UserDefinedTrustComparatorSetKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<ComparatorSetUserDefinedTrust>();

        var set = new List<dynamic>();
        foreach (var companyNumber in result.Set ?? [])
        {
            set.Add(new
            {
                CompanyNumber = companyNumber
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }

    private void PostUserDefinedComparatorRequest(string urn, string[] set)
    {
        var content = new ComparatorSetUserDefinedRequest
        {
            UserId = _userGuid,
            Set = set
        };

        api.CreateRequest(UserDefinedComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/user-defined", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    private void DeleteUserDefinedComparatorRequest(string urn, Guid identifier)
    {
        api.CreateRequest(UserDefinedComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/school/{urn}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Delete
        });
    }

    private void PostUserDefinedTrustComparatorRequest(string companyNumber, string[] set)
    {
        var content = new ComparatorSetUserDefinedRequest
        {
            UserId = _userGuid,
            Set = set
        };

        api.CreateRequest(UserDefinedTrustComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/trust/{companyNumber}/user-defined", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    private void DeleteUserDefinedTrustComparatorRequest(string companyNumber, Guid identifier)
    {
        api.CreateRequest(UserDefinedTrustComparatorSetKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/comparator-set/trust/{companyNumber}/user-defined/{identifier}", UriKind.Relative),
            Method = HttpMethod.Delete
        });
    }

    private void GetUserDefinedComparatorRequest(string type, string identifier)
    {
        api.CreateRequest(UserDefinedDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/user-data?userId={_userGuid}&organisationId={identifier}&organisationType={type}&status=complete&type=comparator-set", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}