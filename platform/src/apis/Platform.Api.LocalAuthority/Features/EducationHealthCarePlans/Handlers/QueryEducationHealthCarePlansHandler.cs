using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Parameters;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;

public interface IQueryEducationHealthCarePlansHandler : IVersionedHandler<BasicContext>;

public class QueryEducationHealthCarePlansV1Handler(IEducationHealthCarePlansService service, IValidator<EducationHealthCarePlansParameters> validator) : IQueryEducationHealthCarePlansHandler
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

        var result = await service.QueryAsync(queryParams.Codes, queryParams.Dimension, context.Token);
        return await context.Request.CreateJsonResponseAsync(result.MapToApiResponse(), context.Token);
    }
}