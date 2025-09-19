using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Platform.MaintenanceTasks.Features.MonitorCommercialResources.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.MaintenanceTasks.Features.MonitorCommercialResources.Services;

public interface IMonitorCommercialResourcesService
{
    Task<CommercialResource[]> GetResourcesAsync();
    Task<CommercialResourceResult> CheckResourceAsync(CommercialResource resource);
}

public class MonitorCommercialResourcesService(
    IDatabaseFactory dbFactory,
    IHttpClientFactory httpClientFactory) : IMonitorCommercialResourcesService
{
    private readonly HttpClient _client = httpClientFactory.CreateClient("NoRedirects");

    public async Task<CommercialResource[]> GetResourcesAsync()
    {
        var query = new MonitorCommercialResourcesQuery();
        using var conn = await dbFactory.GetConnection();

        var resources = await conn.QueryAsync<CommercialResource>(query);

        return resources.ToArray();
    }

    public async Task<CommercialResourceResult> CheckResourceAsync(CommercialResource resource)
    {
        try
        {
            var response = await _client.GetAsync(resource.Url);

            string? redirectLocation = null;
            if ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400)
            {
                redirectLocation = response.Headers.Location?.ToString();
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
}