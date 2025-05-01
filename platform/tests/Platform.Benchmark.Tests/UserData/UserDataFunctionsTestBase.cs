using AutoFixture;
using FluentValidation;
using Moq;
using Platform.Api.Benchmark.Features.UserData;
using Platform.Api.Benchmark.Features.UserData.Parameters;
using Platform.Api.Benchmark.Features.UserData.Services;
using Platform.Test;

namespace Platform.Benchmark.Tests.UserData;

public class UserDataFunctionsTestBase : FunctionsTestBase
{
    protected readonly Fixture Fixture = new();
    protected readonly UserDataFunctions Functions;
    protected readonly Mock<IUserDataService> Service;
    protected readonly Mock<IValidator<UserDataParameters>> Validator;

    protected UserDataFunctionsTestBase()
    {
        Service = new Mock<IUserDataService>();
        Validator = new Mock<IValidator<UserDataParameters>>();
        Functions = new UserDataFunctions(Service.Object, Validator.Object);
    }
}