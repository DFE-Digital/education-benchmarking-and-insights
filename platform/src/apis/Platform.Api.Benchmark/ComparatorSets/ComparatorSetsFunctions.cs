using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Extensions;
using Platform.Api.Benchmark.OpenApi;
using Platform.Api.Benchmark.Responses;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Benchmark.ComparatorSets;

public class ComparatorSetsFunctions(IComparatorSetsService service, ILogger<ComparatorSetsFunctions> logger,
    IValidator<ComparatorSetUserDefinedSchool> schoolValidator,
    IValidator<ComparatorSetUserDefinedTrust> trustValidator)
{
    [Function(nameof(DefaultSchoolComparatorSetAsync))]
    [OpenApiOperation(nameof(DefaultSchoolComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> DefaultSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/school/{urn}/default")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   }
               }))
        {
            try
            {
                var comparatorSet = await service.DefaultSchoolAsync(urn);
                return comparatorSet == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(comparatorSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get default school comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }


    [Function(nameof(CustomSchoolComparatorSetAsync))]
    [OpenApiOperation(nameof(CustomSchoolComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CustomSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/school/{urn}/custom/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var comparatorSet = await service.CustomSchoolAsync(identifier, urn);
                return comparatorSet == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(comparatorSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get custom school comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(UserDefinedSchoolComparatorSetAsync))]
    [OpenApiOperation(nameof(UserDefinedSchoolComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetUserDefinedSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> UserDefinedSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/school/{urn}/user-defined/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var comparatorSet = await service.UserDefinedSchoolAsync(urn, identifier);
                return comparatorSet == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(comparatorSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user defined school comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(CreateUserDefinedSchoolComparatorSetAsync))]
    [OpenApiOperation(nameof(CreateUserDefinedSchoolComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(ComparatorSetUserDefinedRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<MultiResponse> CreateUserDefinedSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "comparator-set/school/{urn}/user-defined/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var response = new MultiResponse();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<ComparatorSetUserDefinedRequest>();
                var comparatorSet = new ComparatorSetUserDefinedSchool
                {
                    RunId = identifier,
                    RunType = "default",
                    Set = ComparatorSetIds.FromCollection(body.Set),
                    URN = urn
                };

                var validationResult = await schoolValidator.ValidateAsync(comparatorSet);
                if (!validationResult.IsValid)
                {
                    response.HttpResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                }

                await service.UpsertUserDefinedSchoolAsync(comparatorSet);

                if (comparatorSet.Set.Count >= 10)
                {
                    await service.UpsertUserDataAsync(
                        ComparatorSetUserData.PendingSchool(identifier, body.UserId, urn));
                    var year = await service.CurrentYearAsync();

                    var message = new PipelineStartMessage
                    {
                        RunId = comparatorSet.RunId,
                        RunType = comparatorSet.RunType,
                        Type = "comparator-set",
                        URN = comparatorSet.URN,
                        Year = int.Parse(year),
                        Payload = new ComparatorSetPayload
                        {
                            Set = comparatorSet.Set.ToArray()
                        }
                    };

                    response.Messages = [message.ToJson()];
                }
                else
                {
                    await service.UpsertUserDataAsync(
                        ComparatorSetUserData.CompleteSchool(identifier, body.UserId, urn));
                }

                response.HttpResponse = req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to upsert user defined school comparator set");
                response.HttpResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return response;
        }
    }

    [Function(nameof(RemoveUserDefinedSchoolComparatorSetAsync))]
    [OpenApiOperation(nameof(RemoveUserDefinedSchoolComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RemoveUserDefinedSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete",
            Route = "comparator-set/school/{urn}/user-defined/{identifier}")]
        HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var comparatorSet = await service.UserDefinedSchoolAsync(urn, identifier);
                if (comparatorSet == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                await service.DeleteSchoolAsync(comparatorSet);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete user defined school comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }


    [Function(nameof(UserDefinedTrustComparatorSetAsync))]
    [OpenApiOperation(nameof(UserDefinedTrustComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IComparatorSetUserDefinedTrust))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> UserDefinedTrustComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get",
            Route = "comparator-set/trust/{companyNumber}/user-defined/{identifier}")]
        HttpRequestData req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "CompanyNumber", companyNumber
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var comparatorSet = await service.UserDefinedTrustAsync(companyNumber, identifier);
                return comparatorSet == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(comparatorSet);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user defined trust comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(CreateUserDefinedTrustComparatorSetAsync))]
    [OpenApiOperation(nameof(CreateUserDefinedTrustComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(ComparatorSetUserDefinedRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CreateUserDefinedTrustComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put",
            Route = "comparator-set/trust/{companyNumber}/user-defined/{identifier}")]
        [RequestBodyType(typeof(ComparatorSetUserDefinedRequest), "The user defined set of schools object")]
        HttpRequestData req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "CompanyNumber", companyNumber
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<ComparatorSetUserDefinedRequest>();
                var comparatorSet = new ComparatorSetUserDefinedTrust
                {
                    RunId = identifier,
                    RunType = "default",
                    Set = ComparatorSetIds.FromCollection(body.Set),
                    CompanyNumber = companyNumber
                };

                var validationResult = await trustValidator.ValidateAsync(comparatorSet);
                if (!validationResult.IsValid)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest);
                }

                await service.UpsertUserDefinedTrustAsync(comparatorSet);

                await service.UpsertUserDataAsync(
                    ComparatorSetUserData.CompleteTrust(identifier, body.UserId, companyNumber));

                return req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to upsert user defined trust comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(RemoveUserDefinedTrustComparatorSetAsync))]
    [OpenApiOperation(nameof(RemoveUserDefinedTrustComparatorSetAsync), "Comparator Sets")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RemoveUserDefinedTrustComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete",
            Route = "comparator-set/trust/{companyNumber}/user-defined/{identifier}")]
        HttpRequestData req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "CompanyNumber", companyNumber
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var comparatorSet = await service.UserDefinedTrustAsync(companyNumber, identifier);
                if (comparatorSet == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                await service.DeleteTrustAsync(comparatorSet);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete user defined trust comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}