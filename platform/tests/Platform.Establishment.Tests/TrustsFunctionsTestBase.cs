using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Trusts;
using Platform.Search.Requests;
using Platform.Test;

namespace Platform.Establishment.Tests;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustsFunctions Functions;
    protected readonly Mock<ITrustsService> TrustsService;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected TrustsFunctionsTestBase()
    {
        TrustsService = new Mock<ITrustsService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new TrustsFunctions(new NullLogger<TrustsFunctions>(), TrustsService.Object, Validator.Object);
    }
}