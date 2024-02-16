using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected readonly SchoolsFunctions Functions;
    protected readonly Mock<ISchoolDb> Db;
    protected readonly Mock<ISearchService<School>> Search;
    protected readonly Mock<IValidator<PostSuggestRequest>> Validator;

    protected SchoolsFunctionsTestBase()
    {
        Db = new Mock<ISchoolDb>();
        Search = new Mock<ISearchService<School>>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(), Db.Object, Search.Object, Validator.Object);
    }
}