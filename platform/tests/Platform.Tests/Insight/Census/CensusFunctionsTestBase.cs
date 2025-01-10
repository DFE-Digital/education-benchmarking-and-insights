using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Census;
using Platform.Cache;
namespace Platform.Tests.Insight.Census;

public class CensusFunctionsTestBase : FunctionsTestBase
{
    protected readonly CensusFunctions Functions;

    protected CensusFunctionsTestBase()
    {
        Functions = new CensusFunctions(new NullLogger<CensusFunctions>());
    }
}

public class CensusNationalAveFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<ICacheKeyFactory> CacheKeyFactory;
    protected readonly Mock<IValidator<CensusNationalAvgParameters>> CensusNationalAvgParametersValidator;
    protected readonly Mock<IDistributedCache> DistributedCache;
    protected readonly CensusNationalAveFunctions Functions;
    protected readonly Mock<ICensusService> Service;

    protected CensusNationalAveFunctionsTestBase()
    {
        CensusNationalAvgParametersValidator = new Mock<IValidator<CensusNationalAvgParameters>>();
        DistributedCache = new Mock<IDistributedCache>();
        CacheKeyFactory = new Mock<ICacheKeyFactory>();
        Service = new Mock<ICensusService>();

        Functions = new CensusNationalAveFunctions(
            new NullLogger<CensusNationalAveFunctions>(),
            Service.Object,
            CensusNationalAvgParametersValidator.Object,
            DistributedCache.Object,
            CacheKeyFactory.Object);
    }
}

public class CensusSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<IValidator<CensusParameters>> CensusParametersValidator;
    protected readonly CensusSchoolFunctions Functions;
    protected readonly Mock<IValidator<QuerySchoolCensusParameters>> QuerySchoolCensusParametersValidator;
    protected readonly Mock<ICensusService> Service;

    protected CensusSchoolFunctionsTestBase()
    {
        CensusParametersValidator = new Mock<IValidator<CensusParameters>>();
        QuerySchoolCensusParametersValidator = new Mock<IValidator<QuerySchoolCensusParameters>>();
        Service = new Mock<ICensusService>();

        Functions = new CensusSchoolFunctions(
            new NullLogger<CensusSchoolFunctions>(),
            Service.Object,
            CensusParametersValidator.Object,
            QuerySchoolCensusParametersValidator.Object);
    }
}