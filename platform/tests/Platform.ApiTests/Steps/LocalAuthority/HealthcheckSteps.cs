using Platform.ApiTests.Drivers;
using Platform.ApiTests.Steps.Shared;

namespace Platform.ApiTests.Steps.LocalAuthority;

[Binding]
[Scope(Feature = "Local Authority Health Check")]
public class HealthcheckSteps(LocalAuthorityApiDriver api) : BaseHealthcheckSteps(api);

