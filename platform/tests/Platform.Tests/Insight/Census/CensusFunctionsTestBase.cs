using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Census;
namespace Platform.Tests.Insight.Census;

public class CensusFunctionsTestBase : FunctionsTestBase
{
    protected readonly CensusFunctions Functions;
    protected readonly Mock<ICensusService> Service;

    protected CensusFunctionsTestBase()
    {
        InlineValidator<CensusParameters> censusParametersValidator = new();
        InlineValidator<QuerySchoolCensusParameters> querySchoolCensusParametersValidator = new();
        Service = new Mock<ICensusService>();
        Functions = new CensusFunctions(new NullLogger<CensusFunctions>(), Service.Object, censusParametersValidator, querySchoolCensusParametersValidator);
    }
}