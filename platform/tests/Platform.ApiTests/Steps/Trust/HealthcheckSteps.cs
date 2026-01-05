using Platform.ApiTests.Drivers;
using Platform.ApiTests.Steps.Shared;

namespace Platform.ApiTests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust Healthcheck")]
public class HealthcheckSteps(TrustApiDriver api) : BaseHealthcheckSteps(api)
{
    [Given("a valid request")]
    private void GivenAValidRequest()
    {
        CreateRequest();
    }

    [When("I submit the request")]
    private async Task WhenISubmitTheRequest()
    {
        await SubmitRequest();
    }

    [Then("the result should be healthy")]
    private async Task ThenTheResultShouldBeHealthy()
    {
        await ValidateResponse();
    }
}