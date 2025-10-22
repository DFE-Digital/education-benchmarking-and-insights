using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Accounts.Handlers;

public interface IQueryExpenditureHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryExpenditureV1Handler(IAccountsService service, IValidator<ExpenditureQueryParameters> validator) : IQueryExpenditureHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<ExpenditureQueryParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken: cancellationToken);
        }

        var result = await service.QueryExpenditureAsync(queryParams.CompanyNumbers, queryParams.Dimension, cancellationToken);
        return await request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category,
            queryParams.ExcludeCentralServices), cancellationToken);
    }
}