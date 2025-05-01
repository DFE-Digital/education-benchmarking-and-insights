using System.Net;
using AutoFixture;
using FluentValidation.Results;
using Moq;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Requests;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Xunit;

namespace Platform.Benchmark.Tests.ComparatorSets;

public class WhenFunctionReceivesCreateComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfully()
    {

        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        Service
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync("2024");

        SchoolValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var response =
            await Functions.CreateUserDefinedSchoolComparatorSetAsync(
                CreateHttpRequestDataWithBody(model),
                "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        Service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        Service.Verify(
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

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        const int year = 2024;
        Service
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync(year.ToString());

        SchoolValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        const string urn = "123321";
        var response =
            await Functions.CreateUserDefinedSchoolComparatorSetAsync(
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

        Service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        Service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBeBadRequestWhenInvalid()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        Service
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync("2024");

        SchoolValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult
            {
                Errors = [new ValidationFailure("TestName", "test error")]
            });

        var response =
            await Functions.CreateUserDefinedSchoolComparatorSetAsync(
                CreateHttpRequestDataWithBody(model),
                "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.BadRequest, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        Service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
        Service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBe500OnError()
    {
        var set = new[] { "1", "2" };

        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, set)
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>())).Throws(new Exception());

        var response =
            await Functions.CreateUserDefinedSchoolComparatorSetAsync(
                CreateHttpRequestDataWithBody(model),
                "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.InternalServerError, response.HttpResponse.StatusCode);
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldCreateSuccessfully()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        TrustValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var response =
            await Functions.CreateUserDefinedTrustComparatorSetAsync(CreateHttpRequestDataWithBody(model), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        Service.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once());
        Service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBeBadRequestOnValidationFailure()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        TrustValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult
            {
                Errors = [new ValidationFailure("TestName", "test error")]
            });

        var response =
            await Functions.CreateUserDefinedTrustComparatorSetAsync(CreateHttpRequestDataWithBody(model), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Service.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never());
        Service.Verify(
            x => x.InsertNewAndDeactivateExistingUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBe500OnError()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()))
            .Throws(new Exception());

        TrustValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var response =
            await Functions.CreateUserDefinedTrustComparatorSetAsync(CreateHttpRequestDataWithBody(model), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}