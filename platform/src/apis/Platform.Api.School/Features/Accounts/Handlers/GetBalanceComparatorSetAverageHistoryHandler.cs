using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetBalanceComparatorSetAverageHistoryHandler : IVersionedHandler<IdContext>;

public class GetBalanceComparatorSetAverageHistoryV1Handler(IBalanceService service, IValidator<BalanceParameters> validator) : IGetBalanceComparatorSetAverageHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var queryParams = context.Request.GetParameters<BalanceParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var (years, rows) = await service.GetComparatorAveHistoryAsync(context.Id, queryParams.Dimension, context.Token);
        return years == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(rows), context.Token);
    }
}