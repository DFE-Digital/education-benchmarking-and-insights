using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight.Workforce;

public class WorkforceFunctionsTestBase : FunctionsTestBase
{
    protected readonly WorkforceFunctions Functions;
    protected readonly Mock<IWorkforceDb> Db;

    protected WorkforceFunctionsTestBase()
    {
        Db = new Mock<IWorkforceDb>();
        Functions = new WorkforceFunctions(new NullLogger<WorkforceFunctions>(), Db.Object);
    }
}