using System.Net;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Xunit;

namespace Platform.Tests.Benchmark;

public class WhenFunctionReceivesGetComparatorSetRequest : ComparatorSetsFunctionsTestBase
{
    [Fact]
    public async Task DefaultShouldBeOkOnValidRequest()
    {
        Service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetSchool());

        var response =
            await Functions.DefaultSchoolComparatorSetAsync(CreateHttpRequestData(), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task DefaultShouldBe500OnError()
    {
        Service
            .Setup(d => d.DefaultSchoolAsync(It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var response = await Functions
            .DefaultSchoolComparatorSetAsync(CreateHttpRequestData(), "12313");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task CustomShouldBeOkOnValidRequest()
    {
        Service
            .Setup(d => d.CustomSchoolAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetSchool());

        var response =
            await Functions.CustomSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task CustomShouldBe500OnError()
    {
        Service
            .Setup(d => d.CustomSchoolAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var response = await Functions
            .CustomSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedShouldBeOkOnValidRequest()
    {
        Service
            .Setup(d => d.UserDefinedSchoolAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        var response =
            await Functions.UserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedShouldBe500OnError()
    {
        Service
            .Setup(d => d.UserDefinedSchoolAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var response = await Functions
            .UserDefinedSchoolComparatorSetAsync(CreateHttpRequestData(), "12313", "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedTrustShouldBeOkOnValidRequest()
    {
        Service
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(new ComparatorSetUserDefinedTrust());

        var response =
            await Functions.UserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedTrustShouldBeNotFoundOnInvalidRequest()
    {
        Service
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((ComparatorSetUserDefinedTrust?)null);

        var response =
            await Functions.UserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedTrustShould500OnError()
    {
        Service
            .Setup(
                d => d.UserDefinedTrustAsync(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Throws(new Exception());

        var response =
            await Functions.UserDefinedTrustComparatorSetAsync(CreateHttpRequestData(), "12313",
                "testIdentifier");

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}