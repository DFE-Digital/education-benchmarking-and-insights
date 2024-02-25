using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustsFunctions Functions;
    protected readonly Mock<ISearchService<Trust>> Search;
    protected readonly Mock<IValidator<PostSuggestRequest>> Validator;
    protected readonly Mock<ITrustDb> Db;

    protected TrustsFunctionsTestBase()
    {
        Search = new Mock<ISearchService<Trust>>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Db = new Mock<ITrustDb>();
        Functions = new TrustsFunctions(new NullLogger<TrustsFunctions>(), Search.Object, Validator.Object, Db.Object);
    }
}