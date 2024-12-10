using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Census;
namespace Platform.Tests.Insight.Census;

public class CensusFunctionsTestBase : FunctionsTestBase
{
    protected readonly CensusFunctions Functions;
    protected readonly Mock<ICensusService> Service;
    protected readonly Mock<IValidator<CensusParameters>> CensusParametersValidator;
    protected readonly Mock<IValidator<CensusNationalAvgParameters>> CensusNationalAvgParametersValidator;

    protected CensusFunctionsTestBase()
    {
        CensusParametersValidator = new Mock<IValidator<CensusParameters>>();
        CensusNationalAvgParametersValidator = new Mock<IValidator<CensusNationalAvgParameters>>();
        InlineValidator<QuerySchoolCensusParameters> querySchoolCensusParametersValidator = new();
        Service = new Mock<ICensusService>();
        Functions = new CensusFunctions(
            new NullLogger<CensusFunctions>(),
            Service.Object,
            CensusParametersValidator.Object,
            CensusNationalAvgParametersValidator.Object,
            querySchoolCensusParametersValidator);
    }
}