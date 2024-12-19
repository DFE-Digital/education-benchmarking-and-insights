using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Cache;

public class TempCacheFunction(IDistributedCache distributedCache)
{
    [Function(nameof(TestCacheString))]
    public async Task<HttpResponseData> TestCacheString(
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

    [Function(nameof(TestCacheObject))]
    public async Task<HttpResponseData> TestCacheObject(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "cache/object/{key}")] HttpRequestData req, string key)
    {
        var was = await distributedCache.GetAsync<TestObject>(key) ?? null;
        var now = new TestObject(DateTime.UtcNow);
        await distributedCache.SetAsync(key, now);

        return await req.CreateJsonResponseAsync(new
        {
            was,
            now
        });
    }

    // ReSharper disable once NotAccessedPositionalProperty.Local
    private record TestObject(DateTime Timestamp);
}