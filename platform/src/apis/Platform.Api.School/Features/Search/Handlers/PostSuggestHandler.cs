using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Search.Models;
using Platform.Api.School.Features.Search.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Search;

namespace Platform.Api.School.Features.Search.Handlers;

public interface IPostSuggestHandler : IVersionedHandler<BasicContext>;

public class PostSuggestV1Handler(ISchoolSearchService service, IValidator<SuggestRequest> validator) : IPostSuggestHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var body = await context.Request.ReadAsJsonAsync<SchoolSuggestRequest>(context.Token);

        var validationResult = await validator.ValidateAsync(body, context.Token);
        if (!validationResult.IsValid)
        {
            return await context.Request.CreateValidationErrorsResponseAsync(validationResult, context.Token);
        }

        var schools = await service.SuggestAsync(body, context.Token);
        return await context.Request.CreateJsonResponseAsync(schools, context.Token);
    }
}