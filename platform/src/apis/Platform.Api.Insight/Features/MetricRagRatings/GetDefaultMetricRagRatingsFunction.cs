using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.MetricRagRatings;

public class GetDefaultMetricRagRatingsFunction(
    IMetricRagRatingsService service,
    IValidator<MetricRagRatingsParameters> metricRagRatingsParametersValidator)
{
    [Function(nameof(GetDefaultMetricRagRatingsFunction))]
    [OpenApiOperation(nameof(GetDefaultMetricRagRatingsFunction), "Metric RAG Ratings")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("categories", In = ParameterLocation.Query, Description = "List of cost category", Type = typeof(string[]), Example = typeof(ExampleCategoryCost))]
    [OpenApiParameter("statuses", In = ParameterLocation.Query, Description = "List of RAG statuses", Type = typeof(string[]), Example = typeof(ExampleRagStatuses))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MetricRagRating[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Default)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<MetricRagRatingsParameters>();

        var validationResult = await metricRagRatingsParametersValidator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var result = await service.QueryAsync(queryParams.Urns, queryParams.Categories, queryParams.Statuses, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase, cancellationToken: cancellationToken);
        return await req.CreateJsonResponseAsync(result, cancellationToken: cancellationToken);
    }
}