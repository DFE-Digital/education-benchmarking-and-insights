using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment.Features.Schools;
using Platform.Search;
using Platform.Test;

namespace Platform.Establishment.Tests.Schools;

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

public class SchoolComparatorsFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolComparatorsFunctions Functions;
    protected readonly Mock<ISchoolComparatorsService> Service;

    protected SchoolComparatorsFunctionsTestBase()
    {
        Service = new Mock<ISchoolComparatorsService>();
        Functions = new SchoolComparatorsFunctions(new NullLogger<SchoolComparatorsFunctions>(), Service.Object);
    }
}