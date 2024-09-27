using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Api.Establishment.Schools;
using Platform.Search;
namespace Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;
    protected readonly Mock<ILocalAuthoritiesService> LocalAuthoritiesService;
    protected readonly Mock<ISchoolsService> SchoolsService;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected LocalAuthoritiesFunctionsTestBase()
    {
        LocalAuthoritiesService = new Mock<ILocalAuthoritiesService>();
        SchoolsService = new Mock<ISchoolsService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new LocalAuthoritiesFunctions(new NullLogger<LocalAuthoritiesFunctions>(), LocalAuthoritiesService.Object, SchoolsService.Object, Validator.Object);
    }
}