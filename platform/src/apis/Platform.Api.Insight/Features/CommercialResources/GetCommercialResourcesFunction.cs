using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.CommercialResources.Parameters;
using Platform.Api.Insight.Features.CommercialResources.Responses;
using Platform.Api.Insight.Features.CommercialResources.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.CommercialResources;

public class GetCommercialResourcesFunction(ICommercialResourcesService service,
    IValidator<CommercialResourcesParameters> commercialResourcesParametersValidator)
{
    [Function(nameof(GetCommercialResourcesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCommercialResourcesFunction), Constants.Features.CommercialResources)]
    [OpenApiParameter("categories", In = ParameterLocation.Query, Description = "List of cost categories", Type = typeof(string[]), Required = false)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CommercialResourcesResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.CommercialResources)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<CommercialResourcesParameters>();

        var validationResult = await commercialResourcesParametersValidator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var result = await service.GetCommercialResourcesByCategory(cancellationToken, queryParams.Categories);
        return await req.CreateJsonResponseAsync(result, cancellationToken: cancellationToken);
    }
}