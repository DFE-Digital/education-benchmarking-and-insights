using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Responses;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Census;

public class GetCensusHistoryComparatorSetAverageFunctions(ICensusService service, IValidator<CensusParameters> validator)
{
    [Function(nameof(GetCensusHistoryComparatorSetAverageFunctions))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCensusHistoryComparatorSetAverageFunctions), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionCensus))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/{urn}/history/comparator-set-average")]
        HttpRequestData req,
        string urn,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<CensusParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var (years, rows) = await service.GetComparatorAveHistoryAsync(urn, queryParams.Dimension, token);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows));
    }
}