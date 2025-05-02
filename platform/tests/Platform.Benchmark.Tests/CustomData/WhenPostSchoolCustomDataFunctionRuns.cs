using System.Net;
using AutoFixture;
using Moq;
using Platform.Api.Benchmark.Features.CustomData;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Api.Benchmark.Features.CustomData.Requests;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Json;
using Platform.Test;
using Xunit;

namespace Platform.Benchmark.Tests.CustomData;

public class WhenPostSchoolCustomDataFunctionRuns : FunctionsTestBase
{
    private readonly Fixture _fixture = new();
    private readonly PostSchoolCustomDataFunction _function;
    private readonly Mock<ICustomDataService> _service = new();

    public WhenPostSchoolCustomDataFunctionRuns()
    {
        _function = new PostSchoolCustomDataFunction(_service.Object);
    }

    [Fact]
    public async Task CreateUserDefinedShouldCreateSuccessfully()
    {
        var model = _fixture.Create<CustomDataRequest>();
        _service.Setup(d => d.UpsertCustomDataAsync(It.IsAny<CustomDataSchool>()));

        const int year = 2024;
        _service
            .Setup(d => d.CurrentYearAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(year.ToString());

        const string urn = "123321";
        var response = await _function.RunAsync(CreateHttpRequestDataWithBody(model), urn);

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
        Assert.Equal("CustomDataPayload", actualMessage.Payload?.Kind); // `kind` expected by data pipeline
        Assert.Equal(
            model.AdministrativeSuppliesNonEducationalCosts,
            (actualMessage.Payload as CustomDataPipelinePayload)?.AdministrativeSuppliesNonEducationalCosts);

        _service.Verify(x => x.UpsertCustomDataAsync(It.IsAny<CustomDataSchool>()), Times.Once());
        _service.Verify(x => x.InsertNewAndDeactivateExistingUserDataAsync(It.IsAny<CustomDataUserData>()), Times.Once());
    }
}