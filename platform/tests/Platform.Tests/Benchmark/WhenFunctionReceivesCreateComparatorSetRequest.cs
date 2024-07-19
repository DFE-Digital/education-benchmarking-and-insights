using AutoFixture;
using FluentValidation.Results;
using System.Net;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesCreateComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfully()
    {

        var model = _fixture.
            Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        Service
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        Service
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
                "123321",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        Service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        Service.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());

    }

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfullyWithSetGreaterThanTen()
    {
        var model = _fixture.
            Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"])
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        Service
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        Service
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
                "123321",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);
        Assert.NotEmpty(response.Messages);
        Service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Once());
        Service.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBeBadRequestWhenInvalid()
    {
        var model = _fixture.
            Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>()));

        Service
            .Setup(d => d.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()));

        Service
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
                "123321",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.BadRequest, response.HttpResponse.StatusCode);
        Assert.Empty(response.Messages);
        Service.Verify(
            x => x.UpsertUserDefinedSchoolAsync(
                It.IsAny<ComparatorSetUserDefinedSchool>()), Times.Never());
        Service.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBe500OnError()
    {
        var set = new[] { "1", "2" };

        var model = _fixture.
            Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, set)
            .Create();

        Service
            .Setup(
                d => d.UpsertUserDefinedSchoolAsync(
                    It.IsAny<ComparatorSetUserDefinedSchool>())).Throws(new Exception());

        var response =
            await Functions.CreateUserDefinedSchoolComparatorSetAsync(
                CreateHttpRequestDataWithBody(model),
                "123321",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.InternalServerError, response.HttpResponse.StatusCode);
    }
    
    [Fact]
    public async Task CreateUserDefinedTrustShouldCreateSuccessfully()
    {
        var model = _fixture.
            Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        Service
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
        Service.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Once());
        Service.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBeBadRequestOnValidationFailure()
    {
        var model = _fixture.
                   Build<ComparatorSetUserDefinedRequest>()
                   .Create();

        Service
            .Setup(d => d.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()));

        Service
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
        Service.Verify(
            x => x.UpsertUserDefinedTrustAsync(
                It.IsAny<ComparatorSetUserDefinedTrust>()), Times.Never());
        Service.Verify(
            x => x.UpsertUserDataAsync(
                It.IsAny<ComparatorSetUserData>()), Times.Never());
    }

    [Fact]
    public async Task CreateUserDefinedTrustShouldBe500OnError()
    {
        var model = _fixture.
            Build<ComparatorSetUserDefinedRequest>()
            .Create();

        Service
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