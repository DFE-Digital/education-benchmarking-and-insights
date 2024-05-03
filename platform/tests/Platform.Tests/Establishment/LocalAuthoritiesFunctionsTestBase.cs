using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class LocalAuthoritiesFunctionsTestBase : FunctionsTestBase
{
    protected readonly LocalAuthoritiesFunctions Functions;
    protected readonly Mock<ISearchService<LocalAuthorityResponseModel>> Search;
    protected readonly Mock<IValidator<PostSuggestRequestModel>> Validator;
    protected LocalAuthoritiesFunctionsTestBase()
    {
        Search = new Mock<ISearchService<LocalAuthorityResponseModel>>();
        Validator = new Mock<IValidator<PostSuggestRequestModel>>();
        Functions = new LocalAuthoritiesFunctions(new NullLogger<LocalAuthoritiesFunctions>(), Search.Object, Validator.Object);
    }
}