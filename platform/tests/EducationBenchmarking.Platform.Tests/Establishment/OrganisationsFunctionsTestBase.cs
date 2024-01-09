using EducationBenchmarking.Platform.Api.Establishment;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace EducationBenchmarking.Platform.Tests.Establishment;

public class OrganisationsFunctionsTestBase : FunctionsTestBase
{
    protected readonly OrganisationsFunctions Functions;
    protected readonly Mock<ISearchService<Organisation>> Search;
    protected readonly Mock<IValidator<PostSuggestRequest>> Validator;

    protected OrganisationsFunctionsTestBase()
    {
        Search = new Mock<ISearchService<Organisation>>();
        Validator = new Mock<IValidator<PostSuggestRequest>>();
        Functions = new OrganisationsFunctions(new NullLogger<OrganisationsFunctions>(),Search.Object, Validator.Object);
    }
}