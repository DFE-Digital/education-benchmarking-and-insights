using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Api.Insight.Features.ItSpend.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.ItSpend;

public class GetItSpendTrustForecastFunction(IItSpendService service, IValidator<ItSpendTrustForecastParameters> validator)
{
    [Function(nameof(GetItSpendTrustForecastFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetItSpendTrustForecastFunction), Constants.Features.ItSpend)]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Company number", Type = typeof(string), Required = true)]
    [OpenApiParameter("year", In = ParameterLocation.Query, Description = "Current BFR year", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ItSpendTrustForecastResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TrustItSpendForecast)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<ItSpendTrustForecastParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var result = await service.GetTrustForecastAsync(queryParams.CompanyNumber, queryParams.Year, cancellationToken);

        return await req.CreateJsonResponseAsync(result, cancellationToken);
    }
}