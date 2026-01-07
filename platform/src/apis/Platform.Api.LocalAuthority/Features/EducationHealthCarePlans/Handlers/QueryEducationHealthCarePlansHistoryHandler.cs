using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;

public interface IQueryEducationHealthCarePlansHistoryHandler : IVersionedHandler<BasicContext>;

public class QueryEducationHealthCarePlansHistoryV1Handler(IEducationHealthCarePlansService service, IValidator<EducationHealthCarePlansParameters> validator) : IQueryEducationHealthCarePlansHistoryHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<EducationHealthCarePlansParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var (years, data) = await service.QueryHistoryAsync(queryParams.Codes, queryParams.Dimension, context.Token);
        return years == null
            ? context.Request.CreateNotFoundResponse()
            : await context.Request.CreateJsonResponseAsync(years.MapToApiResponse(data), context.Token);
    }
}