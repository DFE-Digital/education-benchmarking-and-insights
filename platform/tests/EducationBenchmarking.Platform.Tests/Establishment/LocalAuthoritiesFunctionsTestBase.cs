using AutoFixture;
using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected LocalAuthoritiesFunctions Functions;
    protected Mock<ISearchService<LocalAuthority>> Search;
    protected Fixture Fixture;

    public LocalAuthoritiesFunctionsTestBase()
    {
        Search = new Mock<ISearchService<LocalAuthority>>();
        Functions = new LocalAuthoritiesFunctions(new NullLogger<LocalAuthoritiesFunctions>(),Search.Object);
        Fixture = new Fixture();
    }
}