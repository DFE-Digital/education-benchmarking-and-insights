using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Models;
using Platform.Api.Insight.Features.MetricRagRatings.Parameters;
using Platform.Api.Insight.Features.MetricRagRatings.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.MetricRagRatings;

public class GetUserDefinedMetricRagRatingsFunction(IMetricRagRatingsService service)
{
    [Function(nameof(GetUserDefinedMetricRagRatingsFunction))]
    [OpenApiOperation(nameof(GetUserDefinedMetricRagRatingsFunction), "Metric RAG Ratings")]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("useCustomData", In = ParameterLocation.Query, Description = "Sets whether or not to use custom data context", Type = typeof(bool))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(MetricRagRating[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.UserDefined)] HttpRequestData req,
        string identifier)
    {
        var queryParams = req.GetParameters<MetricRagRatingParameters>();

        var result = await service.UserDefinedAsync(identifier, queryParams.DataContext);
        return await req.CreateJsonResponseAsync(result);
    }
}