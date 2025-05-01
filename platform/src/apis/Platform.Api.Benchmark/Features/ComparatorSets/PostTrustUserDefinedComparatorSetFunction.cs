using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;
using Platform.Api.Benchmark.Features.ComparatorSets.Requests;
using Platform.Api.Benchmark.Features.ComparatorSets.Services;
using Platform.Domain;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class PostTrustUserDefinedComparatorSetFunction(IComparatorSetsService service, ILogger<PostTrustUserDefinedComparatorSetFunction> logger,
    IValidator<ComparatorSetUserDefinedTrust> trustValidator)
{
    [Function(nameof(PostTrustUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(PostTrustUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(ComparatorSetUserDefinedRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = Routes.TrustUserDefinedComparatorSet)] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "CompanyNumber", companyNumber }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<ComparatorSetUserDefinedRequest>();
                var identifier = Guid.NewGuid().ToString();
                var comparatorSet = new ComparatorSetUserDefinedTrust
                {
                    RunId = identifier,
                    RunType = Pipeline.RunType.Default,
                    Set = ComparatorSetIds.FromCollection(body.Set),
                    CompanyNumber = companyNumber
                };

                var validationResult = await trustValidator.ValidateAsync(comparatorSet);
                if (!validationResult.IsValid)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest);
                }

                await service.UpsertUserDefinedTrustAsync(comparatorSet);

                await service.InsertNewAndDeactivateExistingUserDataAsync(
                    ComparatorSetUserData.CompleteTrust(identifier, body.UserId, companyNumber));

                return req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to upsert user defined trust comparator set");
                return req.CreateErrorResponse();
            }
        }
    }
}