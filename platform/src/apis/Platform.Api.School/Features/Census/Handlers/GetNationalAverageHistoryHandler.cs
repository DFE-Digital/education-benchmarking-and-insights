using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Models;
using Platform.Api.School.Features.Census.Parameters;
using Platform.Api.School.Features.Census.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Census.Handlers;

public interface IGetNationalAverageHistoryHandler : IVersionedHandler<BasicContext>;

public class GetNationalAverageHistoryV1Handler(ICensusService service, IValidator<NationalAvgParameters> validator) : IGetNationalAverageHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<NationalAvgParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var (years, rows) = await service.GetNationalAvgHistoryAsync(queryParams.OverallPhase, queryParams.FinanceType,
            queryParams.Dimension, context.Token);
        return years == null
            ? await context.Request.CreateJsonResponseAsync(new CensusHistoryResponse(), context.Token)
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(rows), context.Token);
    }
}