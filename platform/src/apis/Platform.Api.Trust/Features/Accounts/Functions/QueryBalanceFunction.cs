using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.Accounts.Handlers;
using Platform.Api.Trust.Features.Accounts.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Trust.Features.Accounts.Functions;

public class QueryBalanceFunction(IEnumerable<IQueryBalanceHandler> handlers) : VersionedFunctionBase<IQueryBalanceHandler, BasicContext>(handlers)
{
    //TODO: Consider adding validation for parameters
    //TODO: Consider replacing with comparator-set endpoint balance/trust/{companyNumber}/comparator-set/{id}
    [Function(nameof(QueryBalanceFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryBalanceFunction), Constants.Features.Accounts)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.DimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.BalanceCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}