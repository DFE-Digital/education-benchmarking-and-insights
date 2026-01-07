using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IQueryMaintainedSchoolWorkforceHandler : IVersionedHandler<IdContext>;

public class QueryMaintainedSchoolWorkforceV1Handler(IMaintainedSchoolsService service, IValidator<WorkforceSummaryParameters> validator) : IQueryMaintainedSchoolWorkforceHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<WorkforceSummaryParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        int? parsedLimit = int.TryParse(queryParams.Limit, out var parsed) ? parsed : null;


        var result = await service.GetWorkforceSummaryAsync(
            context.Id,
            queryParams.Dimension,
            queryParams.NurseryProvision,
            queryParams.SixthFormProvision,
            queryParams.SpecialClassesProvision,
            parsedLimit,
            queryParams.SortField,
            queryParams.SortOrder,
            queryParams.OverallPhase,
            cancellationToken: context.Token);

        return result.IsEmpty()
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}