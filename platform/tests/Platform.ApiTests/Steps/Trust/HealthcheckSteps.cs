using Platform.ApiTests.Drivers;
using Platform.ApiTests.Steps.Shared;

namespace Platform.ApiTests.Steps.Trust;

[Binding]
[Scope(Feature = "Trust Healthcheck")]
public class HealthcheckSteps(TrustApiDriver api) : BaseHealthcheckSteps(api);