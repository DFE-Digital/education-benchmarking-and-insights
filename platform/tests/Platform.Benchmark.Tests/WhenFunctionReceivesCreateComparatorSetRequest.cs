using System.Net;
using AutoFixture;
using FluentValidation.Results;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Domain.Messages;
using Platform.Json;
using Xunit;
namespace Platform.Benchmark.Tests;

public class WhenFunctionReceivesCreateComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfully()
    {

        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        ComparatorSetsService
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        ComparatorSetsService
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        ComparatorSetsService
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync("2024");

        SchoolValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>(), default))
            .ReturnsAsync(new ValidationResult());

        var response =
            await Functions.CreateUserDefinedSchoolComparatorSetAsync(
                CreateHttpRequestDataWithBody(model),
                "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        ComparatorSetsService.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        ComparatorSetsService.Verify(
            x => x.UpsertUserDataActiveAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());

    }

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfullyWithSetGreaterThanTen()
    {
        string[] set = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"];
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, set)
            .Create();

        ComparatorSetsService
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        const string identifier = "testIdentifier";
        ComparatorSetsService
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        const int year = 2024;
        ComparatorSetsService
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync(year.ToString());

        SchoolValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>(), default))
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
        Assert.Equal("comparator-set", actualMessage.Type);
        Assert.Equal("default", actualMessage.RunType);
        Assert.NotNull(actualMessage.RunId);
        Assert.Equal(year, actualMessage.Year);
        Assert.Equal(urn, actualMessage.URN);
        Assert.Equal("ComparatorSetPipelinePayload", actualMessage.Payload?.Kind);
        Assert.Equal(set, (actualMessage.Payload as ComparatorSetPipelinePayload)?.Set);

        ComparatorSetsService.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        ComparatorSetsService.Verify(
            x => x.UpsertUserDataActiveAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBeBadRequestWhenInvalid()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        ComparatorSetsService
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        ComparatorSetsService
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        ComparatorSetsService
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync("2024");

        SchoolValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>(), default))
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
        ComparatorSetsService.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
        ComparatorSetsService.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBe500OnError()
    {
        var set = new[]
        {
            "1",
            "2"
        };

        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, set)
            .Create();

        ComparatorSetsService
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

        ComparatorSetsService
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        ComparatorSetsService
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        TrustValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>(), default))
            .ReturnsAsync(new ValidationResult());

        var response =
            await Functions.CreateUserDefinedTrustComparatorSetAsync(CreateHttpRequestDataWithBody(model), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.Accepted, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once());
        ComparatorSetsService.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBeBadRequestOnValidationFailure()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        ComparatorSetsService
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        ComparatorSetsService
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        TrustValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>(), default))
            .ReturnsAsync(new ValidationResult
            {
                Errors = [new ValidationFailure("TestName", "test error")]
            });

        var response =
            await Functions.CreateUserDefinedTrustComparatorSetAsync(CreateHttpRequestDataWithBody(model), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        ComparatorSetsService.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never());
        ComparatorSetsService.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBe500OnError()
    {
        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .Create();

        ComparatorSetsService
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()))
            .Throws(new Exception());

        TrustValidator
            .Setup(
                d => d.ValidateAsync(
                    It.IsAny<ComparatorSetUserDefinedTrust>(), default))
            .ReturnsAsync(new ValidationResult());

        var response =
            await Functions.CreateUserDefinedTrustComparatorSetAsync(CreateHttpRequestDataWithBody(model), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}