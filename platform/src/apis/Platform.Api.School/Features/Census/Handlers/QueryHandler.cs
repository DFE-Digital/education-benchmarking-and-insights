using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IQueryHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryV1Handler(ICensusService service, IValidator<QueryCensusParameters> validator) : IQueryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<QueryCensusParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        var result = await service.QueryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase, queryParams.Dimension, cancellationToken);
        return await request.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category), cancellationToken);
    }
}