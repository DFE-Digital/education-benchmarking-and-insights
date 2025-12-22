using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;

public interface IQueryEducationHealthCarePlansHistoryHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryEducationHealthCarePlansHistoryV1Handler(IEducationHealthCarePlansService service, IValidator<EducationHealthCarePlansParameters> validator) : IQueryEducationHealthCarePlansHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken)
    {
        var queryParams = request.GetParameters<EducationHealthCarePlansParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, cancellationToken);
        if (!validationResult.IsValid)
        {
            return await request.CreateValidationErrorsResponseAsync(validationResult, cancellationToken);
        }

        var (years, data) = await service.QueryHistoryAsync(queryParams.Codes, queryParams.Dimension, cancellationToken);
        return years == null
            ? request.CreateNotFoundResponse()
            : await request.CreateJsonResponseAsync(years.MapToApiResponse(data), cancellationToken);
    }
}