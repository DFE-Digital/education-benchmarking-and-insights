using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Expenditure;
namespace Platform.Tests.Insight.Expenditure;

public class ExpenditureFunctionsTestBase : FunctionsTestBase
{
    protected readonly ExpenditureFunctions Functions;
    protected readonly Mock<IExpenditureService> Service;

    protected ExpenditureFunctionsTestBase()
    {
        InlineValidator<ExpenditureParameters> expenditureParametersValidator = new();
        InlineValidator<ExpenditureNationalAvgParameters> expenditureNationalAvgParametersValidator = new();
        InlineValidator<QuerySchoolExpenditureParameters> querySchoolExpenditureParametersValidator = new();
        InlineValidator<QueryTrustExpenditureParameters> queryTrustExpenditureParametersValidator = new();
        Service = new Mock<IExpenditureService>();
        Functions = new ExpenditureFunctions(
            new NullLogger<ExpenditureFunctions>(),
            Service.Object,
            expenditureParametersValidator,
            expenditureNationalAvgParametersValidator,
            querySchoolExpenditureParametersValidator,
            queryTrustExpenditureParametersValidator);
    }
}