using System.Net;
using AutoFixture;
using Microsoft.Extensions.Primitives;
using Moq;
using Platform.Api.Insight.Features.BudgetForecast;
using Platform.Api.Insight.Features.BudgetForecast.Models;
using Platform.Api.Insight.Features.BudgetForecast.Parameters;
using Platform.Api.Insight.Features.BudgetForecast.Responses;
using Platform.Api.Insight.Features.BudgetForecast.Services;
using Platform.Functions;
using Platform.Test;
using Platform.Test.Extensions;
using Xunit;

namespace Platform.Insight.Tests.BudgetForecast;

public class WhenGetBudgetForecastReturnFunctionRuns : FunctionsTestBase
{
    private static readonly Fixture Fixture = new();
    private static readonly BudgetForecastReturnParameters QueryParams = Fixture.Create<BudgetForecastReturnParameters>();
    private readonly GetBudgetForecastReturnFunction _function;

    private readonly Dictionary<string, StringValues> _query = new()
    {
        { nameof(QueryParams.RunType), QueryParams.RunType },
        { nameof(QueryParams.Category), QueryParams.Category },
        { nameof(QueryParams.RunId), QueryParams.RunId }
    };

    private readonly Mock<IBudgetForecastService> _service = new();

    public WhenGetBudgetForecastReturnFunctionRuns()
    {
        _function = new GetBudgetForecastReturnFunction(_service.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        const string companyNumber = nameof(companyNumber);
        var bfr = Fixture.Build<BudgetForecastReturnModel>().CreateMany().ToArray();
        var ar = Fixture.Build<ActualReturnModel>().CreateMany().ToArray();

        _service
            .Setup(d => d.GetBudgetForecastReturnsAsync(companyNumber, QueryParams.RunType, QueryParams.Category, QueryParams.RunId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(bfr);

        _service
            .Setup(d => d.GetActualReturnsAsync(companyNumber, QueryParams.Category, QueryParams.RunId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ar);

        var result = await _function.RunAsync(CreateHttpRequestData(_query), companyNumber);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.Equal(ContentType.ApplicationJson, result.ContentType());

        var body = await result.ReadAsJsonAsync<BudgetForecastReturnResponse[]>();
        Assert.NotNull(body);
    }
}