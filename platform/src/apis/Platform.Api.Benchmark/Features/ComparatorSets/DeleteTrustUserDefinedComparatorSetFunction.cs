using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class DeleteTrustUserDefinedComparatorSetFunction(IComparatorSetsService service, ILogger<DeleteTrustUserDefinedComparatorSetFunction> logger)
{
    [Function(nameof(DeleteTrustUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(DeleteTrustUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.TrustUserDefinedComparatorSetItem)] HttpRequestData req,
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
                if (comparatorSet == null)
                {
                    return req.CreateNotFoundResponse();
                }

                await service.DeleteTrustAsync(comparatorSet);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete user defined trust comparator set");
                return req.CreateErrorResponse();
            }
        }
    }
}