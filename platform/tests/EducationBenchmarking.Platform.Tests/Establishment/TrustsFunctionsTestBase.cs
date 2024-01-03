using AutoFixture;
using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected TrustsFunctions Functions;
    protected Mock<ISearchService<Trust>> Search;
    protected Mock<IValidator<PostSuggestRequest>> Validator;
    protected Fixture Fixture;

    public TrustsFunctionsTestBase()
    {
        Search = new Mock<ISearchService<Trust>>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new TrustsFunctions(new NullLogger<TrustsFunctions>(),Search.Object, Validator.Object);
        Fixture = new Fixture();
    }
}