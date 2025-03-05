using System.Text;
using Platform.Api.Benchmark.CustomData;
using Platform.Api.Benchmark.Features.UserData.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Benchmark Custom Data Endpoint Testing")]
public class BenchmarkCustomDataSteps(BenchmarkApiDriver api)
{
    private const string CustomDataKey = "custom-data";
    private const string UserDefinedDataKey = "user-defined-data";
    private readonly string _userGuid = Guid.NewGuid().ToString();

    [Given("I have a valid custom data get request for school id '(.*)' containing:")]
    public async Task GivenIHaveAValidCustomDataGetRequestForSchoolIdContaining(string urn, DataTable table)
    {
        PostCustomDataRequest(urn, table);
        await WhenISubmitTheCustomDataRequest();

        GetUserDefinedCustomDataRequest(urn);
    }

    [Given("I have a valid custom data post request for school id '(.*)' containing:")]
    public async Task GivenIHaveAValidCustomDataPostRequestForSchoolIdContaining(string urn, DataTable table)
    {
        PostCustomDataRequest(urn, table);
        await WhenISubmitTheCustomDataRequest();

        GetUserDefinedCustomDataRequest(urn);
    }

    [Given("I have a valid custom data delete request for school id '(.*)' containing:")]
    public async Task GivenIHaveAValidCustomDataDeleteRequestForSchoolIdContaining(string urn, DataTable table)
    {
        PostCustomDataRequest(urn, table);
        await WhenISubmitTheCustomDataRequest();

        GetUserDefinedCustomDataRequest(urn);
        await api.Send();

        var response = api[UserDefinedDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<UserData[]>();
        Assert.NotNull(result);
        Assert.Single(result);

        DeleteCustomDataRequest(urn, result.Select(r => new Guid(r.Id!)).FirstOrDefault());
    }

    [Given("I have an invalid custom data delete request for school id '(.*)'")]
    public void GivenIHaveAnInvalidCustomDataDeleteRequestForSchoolId(string urn)
    {
        DeleteCustomDataRequest(urn, Guid.NewGuid());
    }

    [When("I submit the custom data request")]
    public async Task WhenISubmitTheCustomDataRequest()
    {
        await api.Send();
    }

    [Then("new user data should be created for school id '(.*)'")]
    public async Task ThenNewUserDataShouldBeCreatedForSchoolId(string urn)
    {
        var response = api[UserDefinedDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<UserData[]>();

        GetCustomDataRequest(urn, new Guid(result.First().Id!));

        await api.Send();
    }

    [Then("the custom data response should contain:")]
    private async Task ThenTheCustomDataResponseShouldContain(DataTable table)
    {
        var response = api[CustomDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CustomDataSchool>();

        Assert.Equal(GetJsonFromTable(table), result.Data);
    }

    [Then("the custom data response should return accepted")]
    private void ThenTheCustomDataResponseShouldReturnAccepted()
    {
        AssertHttpResponse.IsAccepted(api[CustomDataKey].Response);
    }

    [Then("the custom data response should return ok")]
    private void ThenTheCustomDataResponseShouldReturnOk()
    {
        AssertHttpResponse.IsOk(api[CustomDataKey].Response);
    }

    [Then("the custom data response should return not found")]
    private void ThenTheCustomDataResponseShouldReturnNotFound()
    {
        AssertHttpResponse.IsNotFound(api[CustomDataKey].Response);
    }

    private void GetCustomDataRequest(string urn, Guid identifier)
    {
        api.CreateRequest(CustomDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/custom-data/school/{urn}/{identifier}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    private void PostCustomDataRequest(string urn, DataTable table)
    {
        var json = GetJsonFromTable(table);

        api.CreateRequest(CustomDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/custom-data/school/{urn}", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        });
    }

    private void DeleteCustomDataRequest(string urn, Guid identifier)
    {
        api.CreateRequest(CustomDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/custom-data/school/{urn}/{identifier}", UriKind.Relative),
            Method = HttpMethod.Delete
        });
    }

    private string GetJsonFromTable(DataTable table)
    {
        var content = new Dictionary<string, object>
        {
            { "UserId", _userGuid }
        };
        foreach (var row in table.Rows)
        {
            var property = row.Values.ElementAt(0);
            var value = row.Values.ElementAt(1);
            content.Add(property, decimal.Parse(value));
        }

        return content.ToJson();
    }

    private void GetUserDefinedCustomDataRequest(string urn)
    {
        api.CreateRequest(UserDefinedDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/user-data?userId={_userGuid}&organisationId={urn}&organisationType=school&type=custom-data", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }
}