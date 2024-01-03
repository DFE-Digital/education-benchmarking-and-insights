using AutoFixture;
using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Db;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class SchoolsFunctionsTestBase : FunctionsTestBase
{
    protected SchoolsFunctions Functions;
    protected Mock<ISchoolDb> Db;
    protected Mock<ISearchService<School>> Search;
    protected Mock<IValidator<PostSuggestRequest>> Validator;
    protected Fixture Fixture;

    public SchoolsFunctionsTestBase()
    {
        Db = new Mock<ISchoolDb>();
        Search = new Mock<ISearchService<School>>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new SchoolsFunctions(new NullLogger<SchoolsFunctions>(),Db.Object, Search.Object, Validator.Object);
        Fixture = new Fixture();
    }
}