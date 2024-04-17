using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight;

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