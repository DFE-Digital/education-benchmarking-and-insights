﻿using System.Text;
using Platform.Api.Benchmark.Features.UserData.Models;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
public class BenchmarkUserDataSteps(BenchmarkApiDriver api)
{
    private const string UserDataKey = "user-data";
    private readonly string _userGuid = Guid.NewGuid().ToString();

    [Given("I have a valid user data get request for school id '(.*)' containing custom data:")]
    public async Task GivenIHaveAValidUserDataGetRequestForSchoolIdContainingCustomData(string urn, DataTable table)
    {
        PostCustomDataRequest(urn, table);
        await WhenISubmitTheUserDataRequest();
        GetUserDataRequest(urn);
    }

    [When("I submit the user data request")]
    public async Task WhenISubmitTheUserDataRequest()
    {
        await api.Send();
    }

    [Then("the user data response should be ok")]
    private async Task ThenTheUserDataResponseShouldBeOk()
    {
        var response = api[UserDataKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<IEnumerable<UserData>>().ToArray();

        var row = result.MaxBy(r => r.Expiry);
        Assert.NotNull(row);
        Assert.Equal(_userGuid, row.UserId);
        Assert.Equal("custom-data", row.Type);
        var nextMonth = DateTimeOffset.Now.AddDays(30);
        Assert.InRange(row.Expiry, nextMonth.AddMinutes(-1), nextMonth.AddMinutes(1));
        Assert.Equal("pending", row.Status);
    }

    private void GetUserDataRequest(string urn)
    {
        api.CreateRequest(UserDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/user-data?userId={_userGuid}&organisationId={urn}&organisationType=school&type=custom-data", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    private void PostCustomDataRequest(string urn, DataTable table)
    {
        var json = GetJsonFromTable(table);

        api.CreateRequest(UserDataKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/custom-data/school/{urn}", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
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
}