using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;

public interface IQueryEducationHealthCarePlansHandler : IVersionedHandler
{
    Task<HttpResponseData> HandleAsync(HttpRequestData request, CancellationToken cancellationToken);
}

public class QueryEducationHealthCarePlansV1Handler(IEducationHealthCarePlansService service, IValidator<EducationHealthCarePlansParameters> validator) : IQueryEducationHealthCarePlansHandler
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

        var result = await service.QueryAsync(queryParams.Codes, queryParams.Dimension, cancellationToken);
        return await request.CreateJsonResponseAsync(result.MapToApiResponse(), cancellationToken);
    }
}