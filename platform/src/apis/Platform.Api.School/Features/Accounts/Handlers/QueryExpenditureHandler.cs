using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IQueryExpenditureHandler : IVersionedHandler<BasicContext>;

public class QueryExpenditureV1Handler(IExpenditureService service, IValidator<ExpenditureQueryParameters> validator) : IQueryExpenditureHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<ExpenditureQueryParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.QueryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase, queryParams.Dimension, context.Token);
        return await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), context.Token);
    }
}