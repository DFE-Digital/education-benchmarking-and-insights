using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Parameters;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;
using Platform.Api.LocalAuthorityFinances.OpenApi.Examples;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds;

public class GetHighNeedsHistoryFunction(IHighNeedsService service, IValidator<HighNeedsDimensionedParameters> validator)
{
    [Function(nameof(GetHighNeedsHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetHighNeedsHistoryFunction), Constants.Features.HighNeeds)]
    [OpenApiParameter("code", In = ParameterLocation.Query, Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for resultant values", Type = typeof(string), Required = true, Example = typeof(ExampleHighNeedsDimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(History<HighNeedsYear>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.HighNeedsHistory)] HttpRequestData req,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<HighNeedsDimensionedParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var history = await service.GetHistory(queryParams.Codes, queryParams.Dimension, token);
        return history == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(history);
    }
}