using Platform.ApiTests.Drivers;
using Platform.ApiTests.Steps.Shared;
using Reqnroll;

namespace Platform.ApiTests.Steps.School.HealthCheck;

[Binding]
[Scope(Feature = "School HealthCheck")]
public class HealthCheckSteps(SchoolApiDriver api) : BaseHealthcheckSteps(api);