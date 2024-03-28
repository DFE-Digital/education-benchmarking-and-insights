using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight;

public class AcademySchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly AcademySchoolFunctions Functions;
    protected readonly Mock<ISchoolFinancesDb<Academy>> Db;

    protected AcademySchoolFunctionsTestBase()
    {
        Db = new Mock<ISchoolFinancesDb<Academy>>();
        Functions = new AcademySchoolFunctions(new NullLogger<AcademySchoolFunctions>(), Db.Object);
    }
}