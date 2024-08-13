using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Balance;
namespace Platform.Tests.Insight.Balance;

public class BalanceFunctionsTestBase : FunctionsTestBase
{
    protected readonly BalanceFunctions Functions;
    protected readonly Mock<IBalanceService> Service;

    protected BalanceFunctionsTestBase()
    {
        Service = new Mock<IBalanceService>();
        Functions = new BalanceFunctions(new NullLogger<BalanceFunctions>(), Service.Object);
    }
}