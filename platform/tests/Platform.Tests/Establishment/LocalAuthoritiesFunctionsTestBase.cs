using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;
    protected readonly Mock<ILocalAuthorityService> Service;
    protected readonly Mock<IValidator<PostSuggestRequest>> Validator;

    protected LocalAuthoritiesFunctionsTestBase()
    {
        Service = new Mock<ILocalAuthorityService>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new LocalAuthoritiesFunctions(new NullLogger<LocalAuthoritiesFunctions>(), Service.Object, Validator.Object);
    }
}