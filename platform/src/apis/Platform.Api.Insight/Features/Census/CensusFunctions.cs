using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Census;

public class CensusFunctions(
    ICensusService service,
   ICensusValidator validator)
{
    [Function(nameof(CensusAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(CensusAsync), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(ExampleCategoryCensus))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionCensus))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> CensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/{urn}")] HttpRequestData req,
        string urn)
    {
        var queryParams = req.GetParameters<CensusParameters>();

        var validationResult = await validator.ValidateAsync(queryParams);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.GetAsync(urn);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));

    }

    [Function(nameof(CustomCensusAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(CustomCensusAsync), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(ExampleCategoryCensus))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionCensus))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> CustomCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/{urn}/custom/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var queryParams = req.GetParameters<CensusParameters>();

        var validationResult = await validator.ValidateAsync(queryParams);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.GetCustomAsync(urn, identifier, queryParams.Dimension);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
    }

    [Function(nameof(CensusHistoryAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(CensusHistoryAsync), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionCensus))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> CensusHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/{urn}/history")] HttpRequestData req,
        string urn,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<CensusParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var (years, rows) = await service.GetSchoolHistoryAsync(urn, queryParams.Dimension, token);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows));
    }

    [Function(nameof(CensusHistoryAvgComparatorSetAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(CensusHistoryAvgComparatorSetAsync), Constants.Features.Census)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionCensus))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> CensusHistoryAvgComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/{urn}/history/comparator-set-average")] HttpRequestData req,
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

    //TODO: Consider separate end points for Trust and LA (i.e. census/trust/{id}/schools)
    [Function(nameof(QueryCensusAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryCensusAsync), Constants.Features.Census)]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = true, Example = typeof(ExampleCategoryCensus))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionCensus))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> QueryCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census")] HttpRequestData req)
    {
        var queryParams = req.GetParameters<CensusQuerySchoolsParameters>();

        var validationResult = await validator.ValidateAsync(queryParams);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var result = await service.QueryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode,
            queryParams.Phase, queryParams.Dimension);
        return await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));

    }

    [Function(nameof(CensusHistoryAvgNationalAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(CensusHistoryAvgNationalAsync), Constants.Features.Census)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionCensus))]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "Overall phase for response values", Type = typeof(string), Required = true, Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("financeType", In = ParameterLocation.Query, Description = "Finance type for response values", Type = typeof(string), Required = true, Example = typeof(ExampleFinanceTypes))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> CensusHistoryAvgNationalAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/history/national-average")] HttpRequestData req,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<CensusNationalAvgParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var (years, rows) = await service.GetNationalAvgHistoryAsync(queryParams.OverallPhase, queryParams.FinanceType,
            queryParams.Dimension, token);
        return years == null
            ? await req.CreateJsonResponseAsync(new CensusHistoryResponse())
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows));
    }
}