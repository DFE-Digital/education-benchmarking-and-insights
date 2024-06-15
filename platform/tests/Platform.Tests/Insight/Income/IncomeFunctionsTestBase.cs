using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Income;

namespace Platform.Tests.Insight.Income;

public class IncomeFunctionsTestBase : FunctionsTestBase
{
    protected readonly IncomeFunctions Functions;
    protected readonly Mock<IIncomeService> Service;

    protected IncomeFunctionsTestBase()
    {
        Service = new Mock<IIncomeService>();
        Functions = new IncomeFunctions(new NullLogger<IncomeFunctions>(), Service.Object);
    }
}