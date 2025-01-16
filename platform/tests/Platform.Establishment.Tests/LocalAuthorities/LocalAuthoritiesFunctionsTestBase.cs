using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Search;
using Platform.Test;

namespace Platform.Establishment.Tests.LocalAuthorities;

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