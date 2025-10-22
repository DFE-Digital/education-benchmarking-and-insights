using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Parameters;
using Platform.Api.Trust.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.Accounts.Handlers;

public interface IGetExpenditureHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetExpenditureV1Handler(IAccountsService service, IValidator<ExpenditureParameters> validator) : IGetExpenditureHandler
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

        var result = await service.GetExpenditureAsync(identifier, queryParams.Dimension, cancellationToken);
        return result == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), cancellationToken);
    }
}