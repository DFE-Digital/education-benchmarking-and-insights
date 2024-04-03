using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment;
using Platform.Api.Establishment.Db;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolsFunctions Functions;
    protected readonly Mock<ISchoolDb> Db;
    protected readonly Mock<ISearchService<SchoolResponseModel>> Search;
    protected readonly Mock<IValidator<PostSuggestRequestModel>> Validator;

    protected SchoolsFunctionsTestBase()
    {
        Db = new Mock<ISchoolDb>();
        Search = new Mock<ISearchService<SchoolResponseModel>>();
        Validator = new Mock<IValidator<PostSuggestRequestModel>>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(), Db.Object, Search.Object, Validator.Object);
    }
}