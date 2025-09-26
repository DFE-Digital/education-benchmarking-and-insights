using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.MaintenanceTasks.Features.MonitorCommercialResources;

public class MonitorCommercialResourcesFunction(IDatabaseFactory dbFactory,
    IHttpClientFactory httpClientFactory,
    ILogger<MonitorCommercialResourcesFunction> logger,
    TelemetryClient telemetry)
{
    private readonly HttpClient _client = httpClientFactory.CreateClient("NoRedirects");

    [Function("MonitorCommercialResourcesFunction")]
    public async Task RunAsync([TimerTrigger("0 0 2 * * *")] TimerInfo timer)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.MonitorCommercialResources }
               }))
        {

            var resources = await GetResourcesAsync();

            foreach (var resource in resources)
            {
                var result = await CheckResourceAsync(resource);

                LogResult(result);
            }
        }
    }

    public void LogResult(CommercialResourceResult result)
    {
        telemetry.TrackEvent("commercial-resource-check", new Dictionary<string, string>
        {
            { "Title", result.Title },
            { "Url", result.Url },
            { "StatusCode", result.StatusCode?.ToString() ?? "none" },
            { "Success", result.Success.ToString() },
            { "RedirectLocation", result.RedirectLocation ?? "none" },
            { "Exception", result.Exception?.ToString() ?? "none" }
        });
    }

    public async Task<CommercialResourceResult> CheckResourceAsync(CommercialResource resource)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Head, resource.Url);
            var response = await _client.SendAsync(request);

            logger.LogInformation("Resource: {Title} status code: {StatusCode} success status: {IsSuccessStatusCode}",
                resource.Title,
                response.StatusCode,
                response.IsSuccessStatusCode);

            string? redirectLocation = null;

            if (IsRedirect(response))
            {
                redirectLocation = response.Headers.Location?.ToString();

                logger.LogInformation("Resource {Title} redirect detected. Redirect location: {redirectLocation}", resource.Title, redirectLocation);
            }

            return new CommercialResourceResult
            {
                Title = resource.Title,
                Url = resource.Url,
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                RedirectLocation = redirectLocation,
                Exception = null
            };

        }
        catch (HttpRequestException ex)
        {
            logger.LogError("Resource {Title} failed to fetch", resource.Title);

            return new CommercialResourceResult
            {
                Title = resource.Title,
                Url = resource.Url,
                Success = false,
                StatusCode = null,
                RedirectLocation = null,
                Exception = ex
            };
        }
    }

    private async Task<CommercialResource[]> GetResourcesAsync()
    {
        var query = new MonitorCommercialResourcesQuery();
        using var conn = await dbFactory.GetConnection();

        var resources = await conn.QueryAsync<CommercialResource>(query);

        return resources.ToArray();
    }

    private static bool IsRedirect(HttpResponseMessage response) => (int)response.StatusCode >= 300 && (int)response.StatusCode < 400;
}