using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Sql;

namespace Platform.Api.Insight.Features.Years;

[ExcludeFromCodeCoverage]
public class GetCurrentReturnYearsFunction(IDatabaseFactory dbFactory)
{
    [Function(nameof(GetCurrentReturnYearsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCurrentReturnYearsFunction), Constants.Features.Years)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(object))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.CurrentReturn)]
        HttpRequestData req)
    {
        using var conn = await dbFactory.GetConnection();
        const string sql = "SELECT Value FROM Parameters WHERE Name = @Name";

        var aar = await conn.QueryFirstOrDefaultAsync<string>(sql, new { Name = "LatestAARYear" });
        var cfr = await conn.QueryFirstOrDefaultAsync<string>(sql, new { Name = "LatestCFRYear" });

        return await req.CreateJsonResponseAsync(new { aar, cfr });
    }
}