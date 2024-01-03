using AutoFixture;
using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class OrganisationsFunctionsTestBase : FunctionsTestBase
{
    protected OrganisationsFunctions Functions;
    protected Mock<ISearchService<Organisation>> Search;
    protected Mock<IValidator<PostSuggestRequest>> Validator;
    protected Fixture Fixture;

    public OrganisationsFunctionsTestBase()
    {
        Search = new Mock<ISearchService<Organisation>>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new OrganisationsFunctions(new NullLogger<OrganisationsFunctions>(),Search.Object, Validator.Object);
        Fixture = new Fixture();
    }
}