using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Trusts;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustsFunctions Functions;
    protected readonly Mock<ITrustService> Service;
    protected readonly Mock<IValidator<PostSuggestRequest>> Validator;

    protected TrustsFunctionsTestBase()
    {
        Service = new Mock<ITrustService>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new TrustsFunctions(new NullLogger<TrustsFunctions>(), Service.Object, Validator.Object);
    }
}