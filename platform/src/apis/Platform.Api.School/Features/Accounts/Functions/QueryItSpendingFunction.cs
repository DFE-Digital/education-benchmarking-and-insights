using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Attributes;

namespace Platform.Api.School.Features.Accounts.Functions;

public class QueryItSpendingFunction(IEnumerable<IQueryItSpendingHandler> handlers) : VersionedFunctionBase<IQueryItSpendingHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryItSpendingFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryItSpendingFunction), Constants.Features.Accounts, Summary = "Query IT spending across schools", Description = "Returns Information and Communication Technology (ICT) expenditure for schools matching the query criteria.")]
    [OpenApiUrnsParameter]
    [OpenApiDimensionParameter(Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ItSpendingResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ItSpending)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}