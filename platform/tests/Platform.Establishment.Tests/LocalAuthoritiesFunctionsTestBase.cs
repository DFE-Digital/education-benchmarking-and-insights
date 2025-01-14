using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Search.Requests;
using Platform.Test;

namespace Platform.Establishment.Tests;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;
    protected readonly Mock<ILocalAuthoritiesService> LocalAuthoritiesService;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected LocalAuthoritiesFunctionsTestBase()
    {
        LocalAuthoritiesService = new Mock<ILocalAuthoritiesService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new LocalAuthoritiesFunctions(new NullLogger<LocalAuthoritiesFunctions>(), LocalAuthoritiesService.Object, Validator.Object);
    }
}