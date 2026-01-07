using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Models;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetExpenditureNationalAverageHistoryHandler : IVersionedHandler<BasicContext>;

public class GetExpenditureNationalAverageHistoryV1Handler(IExpenditureService service, IValidator<ExpenditureNationalAvgParameters> validator) : IGetExpenditureNationalAverageHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<ExpenditureNationalAvgParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var (years, rows) = await service.GetNationalAvgHistoryAsync(queryParams.OverallPhase, queryParams.FinanceType,
            queryParams.Dimension, context.Token);
        return years == null
            ? await context.Request.CreateJsonResponseAsync(new ExpenditureHistoryResponse(), context.Token)
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(rows), context.Token);
    }
}