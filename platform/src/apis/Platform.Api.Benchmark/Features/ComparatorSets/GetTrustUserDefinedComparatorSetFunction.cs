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

public class GetTrustUserDefinedComparatorSetFunction(IComparatorSetsService service, ILogger<GetTrustUserDefinedComparatorSetFunction> logger)
{
    [Function(nameof(GetTrustUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(GetTrustUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetUserDefinedTrust))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.TrustUserDefinedComparatorSetItem)] HttpRequestData req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "CompanyNumber", companyNumber },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await service.UserDefinedTrustAsync(companyNumber, identifier);
                return comparatorSet == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(comparatorSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user defined trust comparator set");
                return req.CreateErrorResponse();
            }
        }
    }
}