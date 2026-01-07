using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Parameters;
using Platform.Api.Trust.Features.BudgetForecast.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Trust.Features.BudgetForecast.Handlers;

public interface IQueryItSpendingHandler : IVersionedHandler<BasicContext>;

public class QueryItSpendingV1Handler(IBudgetForecastService service, IValidator<ItSpendingParameters> validator) : IQueryItSpendingHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<ItSpendingParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var result = await service.GetItSpendingAsync(queryParams.CompanyNumbers, context.Token);

        return await context.Request.CreateJsonResponseAsync(result, context.Token);
    }
}