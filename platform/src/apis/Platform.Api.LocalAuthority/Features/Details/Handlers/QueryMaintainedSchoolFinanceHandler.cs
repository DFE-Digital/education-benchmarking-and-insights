using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Platform.Api.LocalAuthority.Features.Details.Parameters;
using Platform.Api.LocalAuthority.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Details.Handlers;

public interface IQueryMaintainedSchoolFinanceHandler : IVersionedHandler<IdContext>;

public class QueryMaintainedSchoolFinanceV1Handler(IMaintainedSchoolsService service, IValidator<FinanceSummaryParameters> validator) : IQueryMaintainedSchoolFinanceHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<FinanceSummaryParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        int? parsedLimit = int.TryParse(queryParams.Limit, out var parsed) ? parsed : null;

        var result = await service.GetFinanceSummaryAsync(
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