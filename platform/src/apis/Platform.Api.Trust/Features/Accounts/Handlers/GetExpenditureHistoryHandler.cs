using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Accounts.Handlers;

public interface IGetExpenditureHistoryHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetExpenditureHistoryV1Handler(IAccountsService service, IValidator<ExpenditureParameters> validator) : IGetExpenditureHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<ExpenditureParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken: cancellationToken);
        }

        var (years, rows) = await service.GetExpenditureHistoryAsync(identifier, queryParams.Dimension, cancellationToken);
        return years == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(years.MapToApiResponse(rows), cancellationToken);
    }
}