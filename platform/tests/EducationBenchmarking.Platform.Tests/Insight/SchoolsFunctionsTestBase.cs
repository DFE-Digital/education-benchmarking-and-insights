using AutoFixture;
using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected SchoolsFunctions Functions;
    protected Mock<ISchoolsDb> Db;
    protected Fixture Fixture;

    public SchoolsFunctionsTestBase()
    {
        Db = new Mock<ISchoolsDb>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(),Db.Object);
        Fixture = new Fixture();
    }
}