using FluentValidation;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Establishment;
using Platform.Api.Establishment.Db;
using Platform.Domain;
using Platform.Infrastructure.Search;

namespace Platform.Tests.Establishment;

public class TrustsFunctionsTestBase : FunctionsTestBase
{
    protected readonly TrustsFunctions Functions;
    protected readonly Mock<ISearchService<TrustResponseModel>> Search;
    protected readonly Mock<IValidator<PostSuggestRequestModel>> Validator;
    protected readonly Mock<ITrustDb> Db;

    protected TrustsFunctionsTestBase()
    {
        Search = new Mock<ISearchService<TrustResponseModel>>();
        Validator = new Mock<IValidator<PostSuggestRequestModel>>();
        Db = new Mock<ITrustDb>();
        Functions = new TrustsFunctions(new NullLogger<TrustsFunctions>(), Search.Object, Validator.Object, Db.Object);
    }
}