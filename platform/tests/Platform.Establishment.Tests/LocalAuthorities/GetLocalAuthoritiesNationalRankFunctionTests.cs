using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Establishment.Features.LocalAuthorities;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Parameters;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Establishment.Tests.LocalAuthorities;

public class GetLocalAuthoritiesNationalRankFunctionTests : FunctionsTestBase
{
    private const string Ranking = Domain.Ranking.LocalAuthorityNationalRanking.SpendAsPercentageOfBudget;
    private const string Sort = Domain.Ranking.Sort.Asc;
    private readonly Fixture _fixture;
    private readonly GetLocalAuthoritiesNationalRankFunction _function;
    private readonly Mock<ILocalAuthorityRankingService> _service;
    private readonly Mock<IValidator<LocalAuthoritiesNationalRankParameters>> _validator;

    public GetLocalAuthoritiesNationalRankFunctionTests()
    {
        _service = new Mock<ILocalAuthorityRankingService>();
        _validator = new Mock<IValidator<LocalAuthoritiesNationalRankParameters>>();
        _fixture = new Fixture();
        _function = new GetLocalAuthoritiesNationalRankFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        var model = _fixture.Create<LocalAuthorityRanking>();
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<LocalAuthoritiesNationalRankParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetRanking(Ranking, Sort, It.IsAny<CancellationToken>()))
            .ReturnsAsync(model);

        var query = new Dictionary<string, StringValues>
        {
            { "ranking", Ranking },
            { "sort", Sort },
        };

        var result = await _function.RunAsync(CreateHttpRequestData(query), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<LocalAuthorityRanking>();
        Assert.NotNull(body);
        Assert.Equivalent(model, body);
    }

    [Fact]
    public async Task ShouldReturn400OnValidationError()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<LocalAuthoritiesNationalRankParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult([
                new ValidationFailure(nameof(LocalAuthoritiesNationalRankParameters.Ranking), "error message")
            ]));

        var result = await _function.RunAsync(CreateHttpRequestData(), CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var values = await result.ReadAsJsonAsync<IEnumerable<ValidationError>>();
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(LocalAuthoritiesNationalRankParameters.Ranking));

        _service
            .Verify(d => d.GetRanking(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}