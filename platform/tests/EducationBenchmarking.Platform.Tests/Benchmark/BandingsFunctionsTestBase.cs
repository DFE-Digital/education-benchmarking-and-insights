using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class BandingsFunctionsTestBase : FunctionsTestBase
{
    protected readonly BandingsFunctions Functions;
    protected readonly Mock<IBandingDb> Db;

    protected BandingsFunctionsTestBase()
    {
        Db = new Mock<IBandingDb>();
        Functions = new BandingsFunctions(new NullLogger<BandingsFunctions>(), Db.Object);
    }
}