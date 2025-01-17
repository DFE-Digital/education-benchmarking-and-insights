using FluentValidation;
using Moq;
using Platform.Api.Establishment.Features.Trusts;
using Platform.Search;
using Platform.Test;

namespace Platform.Establishment.Tests.Trusts;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustsFunctions Functions;
    protected readonly Mock<ITrustsService> Service;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected TrustsFunctionsTestBase()
    {
        Service = new Mock<ITrustsService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new TrustsFunctions(Service.Object, Validator.Object);
    }
}

public class TrustComparatorsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustComparatorsFunctions Functions;
    protected readonly Mock<ITrustComparatorsService> Service;

    protected TrustComparatorsFunctionsTestBase()
    {
        Service = new Mock<ITrustComparatorsService>();
        Functions = new TrustComparatorsFunctions(Service.Object);
    }
}