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
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.MetricRagRatings;

public class GetDefaultMetricRagRatingsSummaryFunction(
    IMetricRagRatingsService service,
    IValidator<MetricRagRatingSummaryParameters> metricRagRatingSummaryParametersValidator)
{
    [Function(nameof(GetDefaultMetricRagRatingsSummaryFunction))]
    [OpenApiOperation(nameof(GetDefaultMetricRagRatingsSummaryFunction), "Metric RAG Ratings")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Three digit Local Authority code", Type = typeof(string))]
    [OpenApiParameter("overallPhase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Required = false, Example = typeof(ExampleOverallPhase))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MetricRagRatingSummary[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.Summary)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<MetricRagRatingSummaryParameters>();

        var validationResult = await metricRagRatingSummaryParametersValidator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var result = await service.QuerySummaryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.OverallPhase, cancellationToken: cancellationToken);
        return await req.CreateJsonResponseAsync(result, cancellationToken);
    }
}