using AutoFixture;
using Moq;
using Platform.Api.Insight.Features.Balance;
using Platform.Test;

namespace Platform.Insight.Tests.Balance;

public class BalanceSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly BalanceSchoolFunctions Functions;
    protected readonly Mock<IBalanceService> Service;
    protected readonly Fixture Fixture = new();

    protected BalanceSchoolFunctionsTestBase()
    {
        Service = new Mock<IBalanceService>();
        Functions = new BalanceSchoolFunctions(Service.Object);
    }
}

public class BalanceTrustFunctionsTestBase : FunctionsTestBase
{
    protected readonly BalanceTrustFunctions Functions;
    protected readonly Mock<IBalanceService> Service;
    protected readonly Fixture Fixture = new();

    protected BalanceTrustFunctionsTestBase()
    {
        Service = new Mock<IBalanceService>();
        Functions = new BalanceTrustFunctions(Service.Object);
    }
}