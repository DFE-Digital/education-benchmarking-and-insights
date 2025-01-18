using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Responses;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Expenditure;

public class GetExpenditureSchoolHistoryComparatorSetAverageFunction(IExpenditureService service, IValidator<ExpenditureParameters> validator)
{
    [Function(nameof(GetExpenditureSchoolHistoryComparatorSetAverageFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureSchoolHistoryComparatorSetAverageFunction), Constants.Features.Expenditure)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SchoolHistoryComparatorSetAverage)]
        HttpRequestData req,
        string urn,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<ExpenditureParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var (years, rows) = await service.GetComparatorAveHistoryAsync(urn, queryParams.Dimension, token);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows));
    }
}