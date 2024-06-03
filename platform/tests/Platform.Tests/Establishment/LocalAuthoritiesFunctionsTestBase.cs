using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;
    protected readonly Mock<ILocalAuthoritiesService> Service;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected LocalAuthoritiesFunctionsTestBase()
    {
        Service = new Mock<ILocalAuthoritiesService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new LocalAuthoritiesFunctions(new NullLogger<LocalAuthoritiesFunctions>(), Service.Object, Validator.Object);
    }
}