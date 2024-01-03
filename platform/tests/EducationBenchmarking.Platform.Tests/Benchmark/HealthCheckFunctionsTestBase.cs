using AutoFixture;
using EducationBenchmarking.Platform.Api.Benchmark;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class HealthCheckFunctionsTestBase : FunctionsTestBase
{
    protected HealthCheckFunctions Functions;
    protected Mock<HealthCheckService> Service;
    protected Fixture Fixture;

    public HealthCheckFunctionsTestBase()
    {
        Service = new Mock<HealthCheckService>();
        Functions = new HealthCheckFunctions(Service.Object);
        Fixture = new Fixture();
    }
}