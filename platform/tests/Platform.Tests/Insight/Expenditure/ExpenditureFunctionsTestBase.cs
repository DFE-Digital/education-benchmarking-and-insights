using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Expenditure;
namespace Platform.Tests.Insight.Expenditure;

public class ExpenditureFunctionsTestBase : FunctionsTestBase
{
    protected readonly ExpenditureFunctions Functions;
    protected readonly Mock<IExpenditureService> Service;
    protected readonly Mock<IValidator<ExpenditureParameters>> ExpenditureParametersValidator;
    protected readonly Mock<IValidator<ExpenditureNationalAvgParameters>> ExpenditureNationalAvgParametersValidator;

    protected ExpenditureFunctionsTestBase()
    {
        InlineValidator<QuerySchoolExpenditureParameters> querySchoolExpenditureParametersValidator = new();
        InlineValidator<QueryTrustExpenditureParameters> queryTrustExpenditureParametersValidator = new();
        ExpenditureParametersValidator = new Mock<IValidator<ExpenditureParameters>>();
        ExpenditureNationalAvgParametersValidator = new Mock<IValidator<ExpenditureNationalAvgParameters>>();
        Service = new Mock<IExpenditureService>();
        Functions = new ExpenditureFunctions(
            new NullLogger<ExpenditureFunctions>(),
            Service.Object,
            ExpenditureParametersValidator.Object,
            ExpenditureNationalAvgParametersValidator.Object,
            querySchoolExpenditureParametersValidator,
            queryTrustExpenditureParametersValidator);
    }
}