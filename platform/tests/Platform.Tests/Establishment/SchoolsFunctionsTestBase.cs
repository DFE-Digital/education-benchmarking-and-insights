using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Schools;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolsFunctions Functions;
    protected readonly Mock<ISchoolService> SchoolService;
    protected readonly Mock<ISchoolComparatorsService> ComparatorsService;
    protected readonly Mock<IValidator<PostSuggestRequest>> Validator;

    protected SchoolsFunctionsTestBase()
    {
        SchoolService = new Mock<ISchoolService>();
        ComparatorsService = new Mock<ISchoolComparatorsService>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(), SchoolService.Object, ComparatorsService.Object, Validator.Object);
    }
}