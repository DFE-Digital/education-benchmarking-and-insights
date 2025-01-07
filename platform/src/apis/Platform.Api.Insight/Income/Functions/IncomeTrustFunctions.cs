using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Income;

public class IncomeTrustFunctions(
    ILogger<IncomeTrustFunctions> logger,
    IIncomeService service,
    IValidator<IncomeParameters> validator)
{
    //TODO: Add parameters to logger scope
    [Function(nameof(TrustIncomeHistoryAsync))]
    [OpenApiOperation(nameof(TrustIncomeHistoryAsync), "Income")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IncomeHistoryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trust/{companyNumber}/history")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                var validationResult = await validator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }
                
                var (years, rows) = await service.GetTrustHistoryAsync(companyNumber, queryParams.Dimension);
                return years == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust income history");
                return req.CreateErrorResponse();
            }
        }
    }
}