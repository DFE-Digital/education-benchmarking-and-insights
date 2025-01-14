using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Balance;
using Platform.Test;

namespace Platform.Insight.Tests.Balance.Endpoints;

public class BalanceFunctionsTestBase : FunctionsTestBase
{
    protected readonly BalanceFunctions Functions;

    protected BalanceFunctionsTestBase()
    {
        Functions = new BalanceFunctions(new NullLogger<BalanceFunctions>());
    }
}

public class BalanceSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly BalanceSchoolFunctions Functions;
    protected readonly Mock<IBalanceService> Service;

    protected BalanceSchoolFunctionsTestBase()
    {
        Service = new Mock<IBalanceService>();
        Functions = new BalanceSchoolFunctions(new NullLogger<BalanceSchoolFunctions>(), Service.Object);
    }
}

public class BalanceTrustFunctionsTestBase : FunctionsTestBase
{
    protected readonly BalanceTrustFunctions Functions;
    protected readonly Mock<IBalanceService> Service;

    protected BalanceTrustFunctionsTestBase()
    {
        Service = new Mock<IBalanceService>();
        Functions = new BalanceTrustFunctions(new NullLogger<BalanceTrustFunctions>(), Service.Object);
    }
}