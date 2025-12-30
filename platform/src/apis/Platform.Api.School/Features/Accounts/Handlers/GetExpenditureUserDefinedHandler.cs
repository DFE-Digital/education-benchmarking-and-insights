using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetExpenditureUserDefinedHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string urn, string identifier, CancellationToken cancellationToken);
}

public class GetExpenditureUserDefinedV1Handler(IExpenditureService service, IValidator<ExpenditureParameters> validator) : IGetExpenditureUserDefinedHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string urn, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<ExpenditureParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        var result = await service.GetUserDefinedAsync(urn, identifier, queryParams.Dimension, cancellationToken);
        return result == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), cancellationToken);
    }
}