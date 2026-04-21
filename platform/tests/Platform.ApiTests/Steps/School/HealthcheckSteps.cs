using Platform.ApiTests.Drivers;
using Platform.ApiTests.Steps.Shared;

namespace Platform.ApiTests.Steps.School;

[Binding]
[Scope(Feature = "School Healthcheck")]
public class HealthcheckSteps(SchoolApiDriver api) : BaseHealthcheckSteps(api);