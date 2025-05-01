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
using Platform.Api.Benchmark.Responses;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Json;

namespace Platform.Api.Benchmark.Features.ComparatorSets;

public class PostSchoolUserDefinedComparatorSetFunction(IComparatorSetsService service, ILogger<PostSchoolUserDefinedComparatorSetFunction> logger,
    IValidator<ComparatorSetUserDefinedSchool> schoolValidator)
{
    [Function(nameof(PostSchoolUserDefinedComparatorSetFunction))]
    [OpenApiOperation(nameof(PostSchoolUserDefinedComparatorSetFunction), Constants.Features.ComparatorSets)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(ComparatorSetUserDefinedRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<MultiResponse> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = Routes.SchoolUserDefinedComparatorSet)] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var response = new MultiResponse();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<ComparatorSetUserDefinedRequest>();
                var identifier = Guid.NewGuid().ToString();
                var comparatorSet = new ComparatorSetUserDefinedSchool
                {
                    RunId = identifier,
                    RunType = Pipeline.RunType.Default,
                    Set = ComparatorSetIds.FromCollection(body.Set),
                    URN = urn
                };

                var validationResult = await schoolValidator.ValidateAsync(comparatorSet);
                if (!validationResult.IsValid)
                {
                    response.HttpResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                    return response;
                }

                await service.UpsertUserDefinedSchoolAsync(comparatorSet);

                if (comparatorSet.Set.Count >= 10)
                {
                    await service.InsertNewAndDeactivateExistingUserDataAsync(
                        ComparatorSetUserData.PendingSchool(identifier, body.UserId, urn));
                    var year = await service.CurrentYearAsync();

                    var message = new PipelineStartCustom
                    {
                        RunId = comparatorSet.RunId,
                        RunType = comparatorSet.RunType,
                        Type = Pipeline.JobType.ComparatorSet,
                        URN = comparatorSet.URN,
                        Year = int.Parse(year),
                        Payload = new ComparatorSetPipelinePayload
                        {
                            Set = comparatorSet.Set.ToArray()
                        }
                    };

                    response.Messages = [message.ToJson()];
                }
                else
                {
                    await service.InsertNewAndDeactivateExistingUserDataAsync(
                        ComparatorSetUserData.CompleteSchool(identifier, body.UserId, urn));
                }

                response.HttpResponse = req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to upsert user defined school comparator set");
                response.HttpResponse = req.CreateErrorResponse();
            }

            return response;
        }
    }
}