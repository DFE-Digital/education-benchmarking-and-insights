using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Requests;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenPostTrustUserDefinedComparatorSetRuns : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly PostTrustUserDefinedComparatorSetFunction _function;
    private readonly Mock<IComparatorSetsService> _service = new();
    private readonly Mock<IValidator<ComparatorSetUserDefinedTrust>> _trustValidator = new();

    public WhenPostTrustUserDefinedComparatorSetRuns()
    {
        _function = new PostTrustUserDefinedComparatorSetFunction(_service.Object, _trustValidator.Object);
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldCreateSuccessfully()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        _service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        _trustValidator
            .Setup(d => d.ValidateAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var response =
            await _function.RunAsync(CreateHttpRequestDataWithBody(model), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        _service.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once());
        _service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBeBadRequestOnValidationFailure()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        _service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        _trustValidator
            .Setup(d => d.ValidateAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult
            {
                Errors = [new ValidationFailure("TestName", "test error")]
            });

        var response =
            await _function.RunAsync(CreateHttpRequestDataWithBody(model), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        _service.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never());
        _service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }
}