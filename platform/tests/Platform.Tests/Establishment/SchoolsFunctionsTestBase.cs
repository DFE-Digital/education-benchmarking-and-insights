using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Schools;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolsFunctions Functions;
    protected readonly Mock<ISchoolsService> Service;
    protected readonly Mock<IValidator<SuggestRequest>> Validator;

    protected SchoolsFunctionsTestBase()
    {
        Service = new Mock<ISchoolsService>();
        Validator = new Mock<IValidator<SuggestRequest>>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(), Service.Object, Validator.Object);
    }
}