﻿using System.Net;
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

public class GetExpenditureTrustHistoryFunction(IExpenditureService service, IValidator<ExpenditureParameters> validator)
{
    [Function(nameof(GetExpenditureTrustHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetExpenditureTrustHistoryFunction), Constants.Features.Expenditure, Deprecated = true)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ExpenditureHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TrustHistory)] HttpRequestData req,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<ExpenditureParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors, cancellationToken: cancellationToken);
        }

        var (years, rows) = await service.GetTrustHistoryAsync(companyNumber, queryParams.Dimension, cancellationToken);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows), cancellationToken);
    }
}