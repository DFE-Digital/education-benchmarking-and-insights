using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Api.Benchmark.OpenApi;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class GetSchoolUserDefinedComparatorSetFunction(IComparatorSetsService service, ILogger<GetSchoolUserDefinedComparatorSetFunction> logger)
{
    [Function(nameof(GetSchoolUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(GetSchoolUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetUserDefinedSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.SchoolUserDefinedComparatorSetItem)] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await service.UserDefinedSchoolAsync(urn, identifier);
                return comparatorSet == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(comparatorSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user defined school comparator set");
                return req.CreateErrorResponse();
            }
        }
    }
}