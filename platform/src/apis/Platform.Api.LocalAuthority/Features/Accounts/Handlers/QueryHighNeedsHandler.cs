using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Accounts.Parameters;
using Platform.Api.LocalAuthority.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Accounts.Handlers;

public interface IQueryHighNeedsHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryHighNeedsV1Handler(IHighNeedsService service, IValidator<HighNeedsParameters> validator) : IQueryHighNeedsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<HighNeedsParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        var highNeeds = await service.QueryAsync(queryParams.Codes, queryParams.Dimension, cancellationToken);
        return await request.CreateJsonResponseAsync(highNeeds, cancellationToken);
    }
}