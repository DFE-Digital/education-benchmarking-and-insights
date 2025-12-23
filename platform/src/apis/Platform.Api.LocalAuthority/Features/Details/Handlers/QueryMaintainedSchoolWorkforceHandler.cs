using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IQueryMaintainedSchoolWorkforceHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class QueryMaintainedSchoolWorkforceV1Handler(IMaintainedSchoolsService service, IValidator<WorkforceSummaryParameters> validator) : IQueryMaintainedSchoolWorkforceHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<WorkforceSummaryParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        int? parsedLimit = int.TryParse(queryParams.Limit, out var parsed) ? parsed : null;


        var result = await service.GetWorkforceSummaryAsync(
            identifier,
            queryParams.Dimension,
            queryParams.NurseryProvision,
            queryParams.SixthFormProvision,
            queryParams.SpecialClassesProvision,
            parsedLimit,
            queryParams.SortField,
            queryParams.SortOrder,
            queryParams.OverallPhase,
            cancellationToken: cancellationToken);

        return result.IsEmpty()
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(result, cancellationToken);
    }
}