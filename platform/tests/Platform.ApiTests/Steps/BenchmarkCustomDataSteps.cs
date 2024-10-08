﻿using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Benchmark.CustomData;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using Xunit;
namespace Platform.ApiTests.Steps;

[Binding]
public class BenchmarkCustomDataSteps(BenchmarkApiDriver api)
{
    private const string CustomDataKey = "custom-data";
    private readonly string _userGuid = Guid.NewGuid().ToString();

    [Given("I have a valid custom data get request for school id '(.*)' containing:")]
    public async Task GivenIHaveAValidCustomDataGetRequestForSchoolIdContaining(string urn, DataTable table)
    {
        var identifier = PutCustomDataRequest(urn, table);
        await WhenISubmitTheCustomDataRequest();
        GetCustomDataRequest(urn, identifier);
    }

    [Given("I have a valid custom data put request for school id '(.*)' containing:")]
    public void GivenIHaveAValidCustomDataPutRequestForSchoolIdContaining(string urn, DataTable table)
    {
        PutCustomDataRequest(urn, table);
    }

    [Given("I have a valid custom data delete request for school id '(.*)' containing:")]
    public async Task GivenIHaveAValidCustomDataDeleteRequestForSchoolIdContaining(string urn, DataTable table)
    {
        var identifier = PutCustomDataRequest(urn, table);
        await WhenISubmitTheCustomDataRequest();
        DeleteCustomDataRequest(urn, identifier);
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

    [Then("the custom data response should contain:")]
    private async Task ThenTheCustomDataResponseShouldContain(DataTable table)
    {
        var response = api[CustomDataKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<CustomDataSchool>();

        Assert.Equal(GetJsonFromTable(table), result.Data);
    }

    [Then("the custom data response should return accepted")]
    private void ThenTheCustomDataResponseShouldReturnAccepted()
    {
        var response = api[CustomDataKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.Accepted);
    }

    [Then("the custom data response should return ok")]
    private void ThenTheCustomDataResponseShouldReturnOk()
    {
        var response = api[CustomDataKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the custom data response should return not found")]
    private void ThenTheCustomDataResponseShouldReturnNotFound()
    {
        var response = api[CustomDataKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    private void GetCustomDataRequest(string urn, Guid identifier)
    {
        api.CreateRequest(CustomDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/custom-data/school/{urn}/{identifier}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    private Guid PutCustomDataRequest(string urn, DataTable table)
    {
        var identifier = Guid.NewGuid();
        var json = GetJsonFromTable(table);

        api.CreateRequest(CustomDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/custom-data/school/{urn}/{identifier}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        });

        return identifier;
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
            {
                "UserId", _userGuid
            }
        };
        foreach (var row in table.Rows)
        {
            var property = row.Values.ElementAt(0);
            var value = row.Values.ElementAt(1);
            content.Add(property, decimal.Parse(value));
        }

        return content.ToJson();
    }
}