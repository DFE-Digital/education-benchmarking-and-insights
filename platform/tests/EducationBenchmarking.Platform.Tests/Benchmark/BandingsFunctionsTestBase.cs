using AutoFixture;
using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class BandingsFunctionsTestBase : FunctionsTestBase
{
    protected BandingsFunctions Functions;
    protected Mock<IBandingDb> Db;
    protected Fixture Fixture;
    
    public BandingsFunctionsTestBase()
    {
        Db = new Mock<IBandingDb>();
        Functions = new BandingsFunctions(new NullLogger<BandingsFunctions>(), Db.Object);
        Fixture = new Fixture();
    }
}