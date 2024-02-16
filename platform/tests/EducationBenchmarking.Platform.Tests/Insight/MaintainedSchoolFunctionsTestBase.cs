using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class MaintainedSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly MaintainedSchoolFunctions Functions;
    protected readonly Mock<IMaintainSchoolDb> Db;

    protected MaintainedSchoolFunctionsTestBase()
    {
        Db = new Mock<IMaintainSchoolDb>();
        Functions = new MaintainedSchoolFunctions(new NullLogger<MaintainedSchoolFunctions>(), Db.Object);
    }
}