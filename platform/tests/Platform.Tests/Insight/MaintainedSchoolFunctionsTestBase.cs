using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight;

public class MaintainedSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly MaintainedSchoolFunctions Functions;
    protected readonly Mock<ISchoolFinancesDb<Maintained>> Db;

    protected MaintainedSchoolFunctionsTestBase()
    {
        Db = new Mock<ISchoolFinancesDb<Maintained>>();
        Functions = new MaintainedSchoolFunctions(new NullLogger<MaintainedSchoolFunctions>(), Db.Object);
    }
}