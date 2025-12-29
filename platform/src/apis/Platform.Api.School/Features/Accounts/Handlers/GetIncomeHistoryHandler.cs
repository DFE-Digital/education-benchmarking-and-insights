using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Parameters;
using Platform.Api.School.Features.Accounts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Accounts.Handlers;

public interface IGetIncomeHistoryHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken);
}

public class GetIncomeHistoryV1Handler(IIncomeService service, IValidator<IncomeParameters> validator) : IGetIncomeHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, string identifier, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<IncomeParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        var (years, rows) = await service.GetHistoryAsync(identifier, queryParams.Dimension, cancellationToken);
        return years == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(years.MapToApiResponse(rows), cancellationToken);
    }
}