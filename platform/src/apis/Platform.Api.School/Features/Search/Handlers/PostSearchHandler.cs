using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Platform.Api.School.Features.Search.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search;

namespace Platform.Api.School.Features.Search.Handlers;


public interface IPostSearchHandler : IVersionedHandler<BasicContext>;

public class PostSearchV1Handler(ISchoolSearchService service, [FromKeyedServices(Constants.Features.Search)] IValidator<SearchRequest> validator) : IPostSearchHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var body = await context.Request.ReadAsJsonAsync<SearchRequest>(context.Token);

        var validationResult = await validator.ValidateAsync(body, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var schools = await service.SearchAsync(body, context.Token);
        return await context.Request.CreateJsonResponseAsync(schools, context.Token);
    }
}