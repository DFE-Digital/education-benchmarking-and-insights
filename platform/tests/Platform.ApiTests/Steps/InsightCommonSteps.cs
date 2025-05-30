﻿using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
public class InsightCommonSteps(InsightApiDriver api)
{
    private const string CommonKey = "common";

    [Given("a current return years request")]
    public void GivenACurrentReturnYearsRequest()
    {
        api.CreateRequest(CommonKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/current-return-years", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the request")]
    public async Task WhenISubmitTheRequest()
    {
        await api.Send();
    }

    [Then("the current return years result should be ok")]
    public async Task ThenTheCurrentReturnYearsResultShouldBeOk()
    {
        var response = api[CommonKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal(new { aar = "2022", cfr = "2022", s251 = "2024" }.ToJson(), content);
    }
}