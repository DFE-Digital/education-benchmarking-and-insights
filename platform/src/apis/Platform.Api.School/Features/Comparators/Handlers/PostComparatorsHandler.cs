using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Comparators.Models;
using Platform.Api.School.Features.Comparators.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Comparators.Handlers;

public interface IPostComparatorsHandler : IVersionedHandler<IdContext>;

public class PostComparatorsV1Handler(IComparatorsService service) : IPostComparatorsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(IdContext context)
    {
        var body = await context.Request.ReadAsJsonAsync<ComparatorsRequest>(context.Token);
        var comparators = await service.ComparatorsAsync(context.Id, body, context.Token);
        return await context.Request.CreateJsonResponseAsync(comparators, context.Token);
    }
}