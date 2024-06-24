using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ApiExplorerSettings(GroupName = "Comparator Sets")]
public class ComparatorSetsFunctions
{
    private readonly ILogger<ComparatorSetsFunctions> _logger;
    private readonly IComparatorSetsService _service;
    private readonly IValidator<ComparatorSetUserDefinedSchool> _schoolValidator;
    private readonly IValidator<ComparatorSetUserDefinedTrust> _trustValidator;

    public ComparatorSetsFunctions(IComparatorSetsService service, ILogger<ComparatorSetsFunctions> logger,
        IValidator<ComparatorSetUserDefinedSchool> schoolValidator,
        IValidator<ComparatorSetUserDefinedTrust> trustValidator)
    {
        _service = service;
        _logger = logger;
        _schoolValidator = schoolValidator;
        _trustValidator = trustValidator;
    }

    [FunctionName(nameof(DefaultSchoolComparatorSetAsync))]
    [ProducesResponseType(typeof(ComparatorSetSchool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> DefaultSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/school/{urn}/default")]
        HttpRequest req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn }
               }))
        {
            try
            {
                var comparatorSet = await _service.DefaultSchoolAsync(urn);
                return comparatorSet == null
                    ? new NotFoundResult()
                    : new JsonContentResult(comparatorSet);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get default school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(CustomSchoolComparatorSetAsync))]
    [ProducesResponseType(typeof(ComparatorSetSchool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CustomSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/school/{urn}/custom/{identifier}")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await _service.CustomSchoolAsync(identifier, urn);
                return comparatorSet == null
                    ? new NotFoundResult()
                    : new JsonContentResult(comparatorSet);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get custom school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(UserDefinedSchoolComparatorSetAsync))]
    [ProducesResponseType(typeof(ComparatorSetUserDefinedSchool), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UserDefinedSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/school/{urn}/user-defined/{identifier}")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await _service.UserDefinedSchoolAsync(urn, identifier);
                return comparatorSet == null
                    ? new NotFoundResult()
                    : new JsonContentResult(comparatorSet);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get user defined school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(CreateUserDefinedSchoolComparatorSetAsync))]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateUserDefinedSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "comparator-set/school/{urn}/user-defined/{identifier}")]
        [RequestBodyType(typeof(ComparatorSetUserDefinedRequest), "The user defined set of schools object")]
        HttpRequest req,
        [Queue("%PipelineMessageHub:JobPendingQueue%", Connection = "PipelineMessageHub:ConnectionString")]
        IAsyncCollector<string> queue,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var body = req.ReadAsJson<ComparatorSetUserDefinedRequest>();
                var comparatorSet = new ComparatorSetUserDefinedSchool
                {
                    RunId = identifier,
                    RunType = "default",
                    Set = ComparatorSetIds.FromCollection(body.Set),
                    URN = urn
                };

                var validationResult = await _schoolValidator.ValidateAsync(comparatorSet);
                if (!validationResult.IsValid)
                {
                    return new BadRequestResult();
                }

                await _service.UpsertUserDefinedSchoolAsync(comparatorSet);

                if (comparatorSet.Set.Count >= 10)
                {
                    await _service.UpsertUserDataAsync(
                        ComparatorSetUserData.PendingSchool(identifier, body.UserId, urn));
                    var year = await _service.CurrentYearAsync();

                    var message = new PipelineStartMessage
                    {
                        RunId = comparatorSet.RunId,
                        RunType = comparatorSet.RunType,
                        Type = "comparator-set",
                        URN = comparatorSet.URN,
                        Year = int.Parse(year),
                        Payload = new ComparatorSetPayload { Set = comparatorSet.Set.ToArray() }
                    };

                    await queue.AddAsync(message.ToJson());
                }
                else
                {
                    await _service.UpsertUserDataAsync(
                        ComparatorSetUserData.CompleteSchool(identifier, body.UserId, urn));
                }

                return new AcceptedResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upsert user defined school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(RemoveUserDefinedSchoolComparatorSetAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RemoveUserDefinedSchoolComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete",
            Route = "comparator-set/school/{urn}/user-defined/{identifier}")]
        HttpRequest req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await _service.UserDefinedSchoolAsync(urn, identifier);
                if (comparatorSet == null)
                {
                    return new NotFoundResult();
                }

                await _service.DeleteSchoolAsync(comparatorSet);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete user defined school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }


    [FunctionName(nameof(UserDefinedTrustComparatorSetAsync))]
    [ProducesResponseType(typeof(ComparatorSetUserDefinedTrust), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> UserDefinedTrustComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get",
            Route = "comparator-set/trust/{companyNumber}/user-defined/{identifier}")]
        HttpRequest req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "CompanyNumber", companyNumber },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await _service.UserDefinedTrustAsync(companyNumber, identifier);
                return comparatorSet == null
                    ? new NotFoundResult()
                    : new JsonContentResult(comparatorSet);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get user defined trust comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(CreateUserDefinedTrustComparatorSetAsync))]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateUserDefinedTrustComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put",
            Route = "comparator-set/trust/{companyNumber}/user-defined/{identifier}")]
        [RequestBodyType(typeof(ComparatorSetUserDefinedRequest), "The user defined set of schools object")]
        HttpRequest req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "CompanyNumber", companyNumber },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var body = req.ReadAsJson<ComparatorSetUserDefinedRequest>();
                var comparatorSet = new ComparatorSetUserDefinedTrust
                {
                    RunId = identifier,
                    RunType = "default",
                    Set = ComparatorSetIds.FromCollection(body.Set),
                    CompanyNumber = companyNumber
                };

                var validationResult = await _trustValidator.ValidateAsync(comparatorSet);
                if (!validationResult.IsValid)
                {
                    return new BadRequestResult();
                }

                await _service.UpsertUserDefinedTrustAsync(comparatorSet);

                await _service.UpsertUserDataAsync(
                    ComparatorSetUserData.CompleteTrust(identifier, body.UserId, companyNumber));

                return new AcceptedResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upsert user defined trust comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(RemoveUserDefinedTrustComparatorSetAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> RemoveUserDefinedTrustComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete",
            Route = "comparator-set/trust/{companyNumber}/user-defined/{identifier}")]
        HttpRequest req,
        string companyNumber,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "CompanyNumber", companyNumber },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var comparatorSet = await _service.UserDefinedTrustAsync(companyNumber, identifier);
                if (comparatorSet == null)
                {
                    return new NotFoundResult();
                }

                await _service.DeleteTrustAsync(comparatorSet);
                return new OkResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to delete user defined trust comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}