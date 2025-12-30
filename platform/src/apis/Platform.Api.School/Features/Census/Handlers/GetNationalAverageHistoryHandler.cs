using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Models;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IGetNationalAverageHistoryHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class GetNationalAverageHistoryV1Handler(ICensusService service, IValidator<NationalAvgParameters> validator) : IGetNationalAverageHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<NationalAvgParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        var (years, rows) = await service.GetNationalAvgHistoryAsync(queryParams.OverallPhase, queryParams.FinanceType,
            queryParams.Dimension, cancellationToken);
        return years == null
            ? await request.CreateJsonResponseAsync(new CensusHistoryResponse(), cancellationToken)
            : await request.CreateJsonResponseAsync(years.MapToApiResponse(rows), cancellationToken);
    }
}