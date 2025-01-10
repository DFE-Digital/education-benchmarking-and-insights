using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Insight.Expenditure;
using Platform.Cache;
namespace Platform.Tests.Insight.Expenditure;

public class ExpenditureFunctionsTestBase : FunctionsTestBase
{
    protected readonly ExpenditureFunctions Functions;

    protected ExpenditureFunctionsTestBase()
    {
        Functions = new ExpenditureFunctions(new NullLogger<ExpenditureFunctions>());
    }
}

public class ExpenditureNationalAveFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<ICacheKeyFactory> CacheKeyFactory;
    protected readonly Mock<IDistributedCache> DistributedCache;
    protected readonly Mock<IValidator<ExpenditureNationalAvgParameters>> ExpenditureNationalAvgParametersValidator;
    protected readonly ExpenditureNationalAveFunctions Functions;
    protected readonly Mock<IExpenditureService> Service;

    protected ExpenditureNationalAveFunctionsTestBase()
    {
        ExpenditureNationalAvgParametersValidator = new Mock<IValidator<ExpenditureNationalAvgParameters>>();
        DistributedCache = new Mock<IDistributedCache>();
        CacheKeyFactory = new Mock<ICacheKeyFactory>();
        Service = new Mock<IExpenditureService>();

        Functions = new ExpenditureNationalAveFunctions(
            new NullLogger<ExpenditureNationalAveFunctions>(),
            Service.Object,
            ExpenditureNationalAvgParametersValidator.Object,
            DistributedCache.Object,
            CacheKeyFactory.Object);
    }
}

public class ExpenditureSchoolFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<IValidator<ExpenditureParameters>> ExpenditureParametersValidator;
    protected readonly ExpenditureSchoolFunctions Functions;
    protected readonly Mock<IValidator<QuerySchoolExpenditureParameters>> QuerySchoolExpenditureParametersValidator;
    protected readonly Mock<IExpenditureService> Service;

    protected ExpenditureSchoolFunctionsTestBase()
    {
        ExpenditureParametersValidator = new Mock<IValidator<ExpenditureParameters>>();
        QuerySchoolExpenditureParametersValidator = new Mock<IValidator<QuerySchoolExpenditureParameters>>();
        Service = new Mock<IExpenditureService>();

        Functions = new ExpenditureSchoolFunctions(
            new NullLogger<ExpenditureSchoolFunctions>(),
            Service.Object,
            ExpenditureParametersValidator.Object,
            QuerySchoolExpenditureParametersValidator.Object);
    }
}

public class ExpenditureTrustFunctionsTestBase : FunctionsTestBase
{
    protected readonly Mock<IValidator<ExpenditureParameters>> ExpenditureParametersValidator;
    protected readonly ExpenditureTrustFunctions Functions;
    protected readonly Mock<IValidator<QueryTrustExpenditureParameters>> QueryTrustExpenditureParametersValidator;
    protected readonly Mock<IExpenditureService> Service;

    protected ExpenditureTrustFunctionsTestBase()
    {
        QueryTrustExpenditureParametersValidator = new Mock<IValidator<QueryTrustExpenditureParameters>>();
        ExpenditureParametersValidator = new Mock<IValidator<ExpenditureParameters>>();
        Service = new Mock<IExpenditureService>();

        Functions = new ExpenditureTrustFunctions(
            new NullLogger<ExpenditureTrustFunctions>(),
            Service.Object,
            ExpenditureParametersValidator.Object,
            QueryTrustExpenditureParametersValidator.Object);
    }
}