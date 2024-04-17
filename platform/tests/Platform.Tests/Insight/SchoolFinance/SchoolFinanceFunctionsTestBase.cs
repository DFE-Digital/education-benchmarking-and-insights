using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight.SchoolFinance;

public class SchoolFinanceFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolFinanceFunctions Functions;
    protected readonly Mock<ISchoolFinancesDb> Db;

    protected SchoolFinanceFunctionsTestBase()
    {
        Db = new Mock<ISchoolFinancesDb>();
        Functions = new SchoolFinanceFunctions(new NullLogger<SchoolFinanceFunctions>(), Db.Object);
    }
}