using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Details.Parameters;
using Platform.Api.School.Features.Details.Services;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.School.Features.Details.Handlers;

public interface IQuerySchoolsHandler : IVersionedHandler<BasicContext>;

public class QuerySchoolsV1Handler(ISchoolDetailsService service) : IQuerySchoolsHandler
{
    public string Version => "1.0";

    public async Task<HttpResponseData> HandleAsync(BasicContext context)
    {
        var queryParams = context.Request.GetParameters<SchoolsParameters>();

        var schools = await service.QueryAsync(queryParams.Schools, context.Token);
        return await context.Request.CreateJsonResponseAsync(schools, context.Token);
    }
}