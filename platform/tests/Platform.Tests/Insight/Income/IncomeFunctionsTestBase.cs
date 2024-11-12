using FluentValidation;
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
        InlineValidator<IncomeParameters> incomeParametersValidator = new();
        InlineValidator<QuerySchoolIncomeParameters> querySchoolIncomeParametersValidator = new();
        InlineValidator<QueryTrustIncomeParameters> queryTrustIncomeParametersValidator = new();
        Service = new Mock<IIncomeService>();
        Functions = new IncomeFunctions(new NullLogger<IncomeFunctions>(), Service.Object, incomeParametersValidator, querySchoolIncomeParametersValidator, queryTrustIncomeParametersValidator);
    }
}