using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class AcademyFunctionsTestBase : FunctionsTestBase
{
    protected readonly AcademyFunctions Functions;
    protected readonly Mock<IAcademyDb> Db;

    protected AcademyFunctionsTestBase()
    {
        Db = new Mock<IAcademyDb>();
        Functions = new AcademyFunctions(new NullLogger<AcademyFunctions>(),Db.Object);
    }
}