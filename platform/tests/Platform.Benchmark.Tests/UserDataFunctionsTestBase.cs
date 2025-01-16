using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.UserData;
using Platform.Test;

namespace Platform.Benchmark.Tests;

public class UserDataFunctionsTestBase : FunctionsTestBase
{
    protected readonly Fixture Fixture = new();
    protected readonly UserDataFunctions Functions;
    protected readonly Mock<IUserDataService> Service;

    protected UserDataFunctionsTestBase()
    {
        Service = new Mock<IUserDataService>();
        Functions = new UserDataFunctions(new NullLogger<UserDataFunctions>(), Service.Object);
    }
}