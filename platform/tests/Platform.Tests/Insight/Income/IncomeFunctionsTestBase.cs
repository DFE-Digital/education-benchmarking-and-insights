using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight;
using Platform.Api.Insight.Db;

namespace Platform.Tests.Insight.Income;

public class IncomeFunctionsTestBase : FunctionsTestBase
{
    protected readonly IncomeFunctions Functions;
    protected readonly Mock<IIncomeDb> Db;

    protected IncomeFunctionsTestBase()
    {
        Db = new Mock<IIncomeDb>();
        Functions = new IncomeFunctions(new NullLogger<IncomeFunctions>(), Db.Object);
    }
}