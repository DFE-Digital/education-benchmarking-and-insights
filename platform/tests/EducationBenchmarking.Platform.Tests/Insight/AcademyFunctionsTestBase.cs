using AutoFixture;
using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class AcademyFunctionsTestBase : FunctionsTestBase
{
    protected AcademyFunctions Functions;
    protected Mock<IAcademyDb> Db;
    protected Fixture Fixture;

    public AcademyFunctionsTestBase()
    {
        Db = new Mock<IAcademyDb>();
        Functions = new AcademyFunctions(new NullLogger<AcademyFunctions>(),Db.Object);
        Fixture = new Fixture();
    }
}