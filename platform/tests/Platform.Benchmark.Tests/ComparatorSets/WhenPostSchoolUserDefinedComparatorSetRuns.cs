using System.Net;
using AutoFixture;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Requests;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenPostSchoolUserDefinedComparatorSetRuns : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly PostSchoolUserDefinedComparatorSetFunction _function;
    private readonly Mock<IValidator<ComparatorSetUserDefinedSchool>> _schoolValidator = new();
    private readonly Mock<IComparatorSetsService> _service = new();

    public WhenPostSchoolUserDefinedComparatorSetRuns()
    {
        _function = new PostSchoolUserDefinedComparatorSetFunction(_service.Object, _schoolValidator.Object);
    }

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfully()
    {

        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        _service
            .Setup(d => d.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        _service
            .Setup(d => d.CurrentYearAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("2024");

        _schoolValidator
            .Setup(d => d.ValidateAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var response =
            await _function.RunAsync(
                CreateHttpRequestDataWithBody(model),
                "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        _service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        _service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());

    }

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfullyWithSetGreaterThanTen()
    {
        string[] set = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"];
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, set)
            .Create();

        _service
            .Setup(d => d.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        const int year = 2024;
        _service
            .Setup(d => d.CurrentYearAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(year.ToString());

        _schoolValidator
            .Setup(d => d.ValidateAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        const string urn = "123321";
        var response =
            await _function.RunAsync(
                CreateHttpRequestDataWithBody(model),
                urn);

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);

        Assert.NotEmpty(response.Messages);
        var actualMessage = response.Messages.First().FromJson<PipelineStartCustom>();
        Assert.Equal(Pipeline.JobType.ComparatorSet, actualMessage.Type);
        Assert.Equal(Pipeline.RunType.Default, actualMessage.RunType);
        Assert.NotNull(actualMessage.RunId);
        Assert.Equal(year, actualMessage.Year);
        Assert.Equal(urn, actualMessage.URN);
        Assert.Equal("ComparatorSetPayload", actualMessage.Payload?.Kind); // `kind` expected by data pipeline
        Assert.Equal(set, (actualMessage.Payload as ComparatorSetPipelinePayload)?.Set);

        _service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        _service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBeBadRequestWhenInvalid()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        _service
            .Setup(d => d.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()));

        _service
            .Setup(d => d.CurrentYearAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("2024");

        _schoolValidator
            .Setup(d => d.ValidateAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult
            {
                Errors = [new ValidationFailure("TestName", "test error")]
            });

        var response =
            await _function.RunAsync(
                CreateHttpRequestDataWithBody(model),
                "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.BadRequest, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        _service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
        _service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }
}