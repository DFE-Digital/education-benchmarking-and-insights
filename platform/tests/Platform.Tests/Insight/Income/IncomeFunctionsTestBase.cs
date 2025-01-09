using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Income;

namespace Platform.Tests.Insight.Income;

public class IncomeFunctionsTestBase : FunctionsTestBase
{
    protected readonly IncomeFunctions Functions;

    protected IncomeFunctionsTestBase()
    {
        Functions = new IncomeFunctions(new NullLogger<IncomeFunctions>());
    }
}

public class IncomeSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly IncomeSchoolFunctions Functions;
    protected readonly Mock<IIncomeService> Service;
    protected readonly Mock<IValidator<IncomeParameters>> Validator;

    protected IncomeSchoolFunctionsTestBase()
    {
        Validator = new Mock<IValidator<IncomeParameters>>();
        Service = new Mock<IIncomeService>();

        Functions = new IncomeSchoolFunctions(new NullLogger<IncomeSchoolFunctions>(), Service.Object, Validator.Object);
    }
}


public class IncomeTrustFunctionsTestBase : FunctionsTestBase
{
    protected readonly IncomeTrustFunctions Functions;
    protected readonly Mock<IIncomeService> Service;
    protected readonly Mock<IValidator<IncomeParameters>> Validator;

    protected IncomeTrustFunctionsTestBase()
    {
        Validator = new Mock<IValidator<IncomeParameters>>();
        Service = new Mock<IIncomeService>();

        Functions = new IncomeTrustFunctions(new NullLogger<IncomeTrustFunctions>(), Service.Object, Validator.Object);
    }
}