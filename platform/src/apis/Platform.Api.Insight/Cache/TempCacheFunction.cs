using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Cache;

public class TempCacheFunction(IDistributedCache distributedCache)
{
    [Function("TestCacheString")]
    public async Task<HttpResponseData> Test(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "cache/string/{key}")] HttpRequestData req, string key)
    {
        var was = await distributedCache.GetStringAsync(key) ?? string.Empty;
        var now = DateTime.UtcNow.ToString("s");
        await distributedCache.SetStringAsync(key, now);

        return await req.CreateJsonResponseAsync(new
        {
            was,
            now
        });
    }
}