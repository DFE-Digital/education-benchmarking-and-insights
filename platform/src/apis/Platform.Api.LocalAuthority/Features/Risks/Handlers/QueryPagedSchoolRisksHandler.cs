using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Risks.Parameters;
using Platform.Api.LocalAuthority.Features.Risks.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Risks.Handlers;

public interface IQueryPagedSchoolRisksHandler : IVersionedHandler<BasicContext>;

public class QueryPagedSchoolRisksHandlerV1(ISchoolRisksService service, IValidator<PagedSchoolRisksParameters> validator) : IQueryPagedSchoolRisksHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<PagedSchoolRisksParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.QueryPagedAsync(
            queryParams.Codes,
            queryParams.Page,
            queryParams.PageSize,
            queryParams.SortField,
            queryParams.SortOrder,
            queryParams.Phase,
            context.Token);

        return await context.Request.CreatePagedJsonResponseAsync(result, cancellationToken: context.Token);
    }
}
