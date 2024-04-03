using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class OrganisationsFunctionsTestBase : FunctionsTestBase
{
    protected readonly OrganisationsFunctions Functions;
    protected readonly Mock<ISearchService<OrganisationResponseModel>> Search;
    protected readonly Mock<IValidator<PostSuggestRequestModel>> Validator;

    protected OrganisationsFunctionsTestBase()
    {
        Search = new Mock<ISearchService<OrganisationResponseModel>>();
        Validator = new Mock<IValidator<PostSuggestRequestModel>>();
        Functions = new OrganisationsFunctions(new NullLogger<OrganisationsFunctions>(), Search.Object, Validator.Object);
    }
}