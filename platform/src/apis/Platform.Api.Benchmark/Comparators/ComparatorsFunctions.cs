using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Extensions;
using Platform.Api.Benchmark.OpenApi;
namespace Platform.Api.Benchmark.Comparators;

public class ComparatorsFunctions(ILogger<ComparatorsFunctions> logger, IComparatorSchoolsService schoolsService, IComparatorTrustsService trustsService)
{
    [Function(nameof(SchoolComparatorsAsync))]
    [OpenApiOperation(nameof(SchoolComparatorsAsync), "Comparators")]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(ComparatorSchoolsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ComparatorSchools))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparators/schools")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<ComparatorSchoolsRequest>();
                //TODO : Add request validation
                var comparators = await schoolsService.ComparatorsAsync(body);
                return await req.CreateJsonResponseAsync(comparators);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to create school comparators");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(TrustComparatorsAsync))]
    [OpenApiOperation(nameof(TrustComparatorsAsync), "Comparators")]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(ComparatorTrustsRequest), Description = "The comparator characteristics object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ComparatorSchools))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparators/trusts")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<ComparatorTrustsRequest>();
                //TODO : Add request validation
                var comparators = await trustsService.ComparatorsAsync(body);
                return await req.CreateJsonResponseAsync(comparators);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to create trust comparators");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}