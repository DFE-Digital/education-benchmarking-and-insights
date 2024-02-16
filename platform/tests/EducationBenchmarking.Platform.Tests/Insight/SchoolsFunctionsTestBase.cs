using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolsFunctions Functions;
    protected readonly Mock<ISchoolsDb> Db;

    protected SchoolsFunctionsTestBase()
    {
        Db = new Mock<ISchoolsDb>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(), Db.Object);
    }
}