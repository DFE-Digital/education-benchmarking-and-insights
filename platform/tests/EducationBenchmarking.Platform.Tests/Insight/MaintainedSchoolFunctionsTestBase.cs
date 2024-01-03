using AutoFixture;
using EducationBenchmarking.Platform.Api.Insight;
using EducationBenchmarking.Platform.Api.Insight.Db;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Insight;

public class MaintainedSchoolFunctionsTestBase : FunctionsTestBase
{
    protected MaintainedSchoolFunctions Functions;
    protected Mock<IMaintainSchoolDb> Db;
    protected Fixture Fixture;

    public MaintainedSchoolFunctionsTestBase()
    {
        Db = new Mock<IMaintainSchoolDb>();
        Functions = new MaintainedSchoolFunctions(new NullLogger<MaintainedSchoolFunctions>(),Db.Object);
        Fixture = new Fixture();
    }
}