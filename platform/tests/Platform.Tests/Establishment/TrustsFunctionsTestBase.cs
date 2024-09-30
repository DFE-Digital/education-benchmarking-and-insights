using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Schools;
using Platform.Api.Establishment.Trusts;
using Platform.Search;
namespace Platform.Tests.Establishment;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustsFunctions Functions;
    protected readonly Mock<ISchoolsService> SchoolsService;
    protected readonly Mock<ITrustsService> TrustsService;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected TrustsFunctionsTestBase()
    {
        TrustsService = new Mock<ITrustsService>();
        SchoolsService = new Mock<ISchoolsService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new TrustsFunctions(new NullLogger<TrustsFunctions>(), TrustsService.Object, SchoolsService.Object, Validator.Object);
    }
}