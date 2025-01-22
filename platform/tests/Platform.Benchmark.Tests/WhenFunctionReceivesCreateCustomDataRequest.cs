using System.Net;
using AutoFixture;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.CustomData;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests;

public class WhenFunctionReceivesCreateCustomDataRequest : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly CustomDataFunctions _functions;
    private readonly Mock<ICustomDataService> _service = new();

    public WhenFunctionReceivesCreateCustomDataRequest()
    {
        _functions = new CustomDataFunctions(new NullLogger<CustomDataFunctions>(), _service.Object);
    }

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfully()
    {
        var model = _fixture.Create<CustomDataRequest>();
        _service.Setup(d => d.UpsertCustomDataAsync(It.IsAny<CustomDataSchool>()));

        const int year = 2024;
        _service
            .Setup(d => d.CurrentYearAsync())
            .ReturnsAsync(year.ToString());

        const string urn = "123321";
        var response = await _functions.CreateSchoolCustomDataAsync(CreateHttpRequestDataWithBody(model), urn);

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.Accepted, response.HttpResponse.StatusCode);

        Assert.NotEmpty(response.Messages);
        var actualMessage = response.Messages.First().FromJson<PipelineStartCustom>();
        Assert.Equal(Pipeline.JobType.CustomData, actualMessage.Type);
        Assert.Equal(Pipeline.RunType.Custom, actualMessage.RunType);
        Assert.NotNull(actualMessage.RunId);
        Assert.Equal(year, actualMessage.Year);
        Assert.Equal(urn, actualMessage.URN);
        Assert.Equal("CustomDataPayload", actualMessage.Payload?.Kind);
        Assert.Equal(
            model.AdministrativeSuppliesNonEducationalCosts,
            (actualMessage.Payload as CustomDataPayload)?.AdministrativeSuppliesNonEducationalCosts);

        _service.Verify(x => x.UpsertCustomDataAsync(It.IsAny<CustomDataSchool>()), Times.Once());
        _service.Verify(x => x.InsertNewAndDeactivateExistingUserDataAsync(It.IsAny<CustomDataUserData>()), Times.Once());
    }

    [Fact]
    public async Task CreateUserDefinedShouldBe500OnError()
    {
        var set = new[] { "1", "2" };

        var model = _fixture.Build<ComparatorSetUserDefinedRequest>()
            .With(x => x.Set, set)
            .Create();

        _service
            .Setup(d => d.UpsertCustomDataAsync(It.IsAny<CustomDataSchool>()))
            .Throws(new Exception());

        var response = await _functions.CreateSchoolCustomDataAsync(CreateHttpRequestDataWithBody(model), "123321");

        Assert.NotNull(response);
        Assert.NotNull(response.HttpResponse);
        Assert.Equal(HttpStatusCode.InternalServerError, response.HttpResponse.StatusCode);
    }
}