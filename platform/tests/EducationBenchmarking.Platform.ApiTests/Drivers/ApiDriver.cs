using EducationBenchmarking.Platform.ApiTests.TestSupport;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public class ApiDriver : Dictionary<string, ApiDriver.ApiMessage>
{
    private readonly HttpClient _client;

    public ApiDriver(Config.Api.ApiEndpoint endpoint)
    {
        _client = new HttpClient { BaseAddress = new Uri(endpoint.Host ?? throw new NullException(endpoint.Host)) };
        if (!string.IsNullOrEmpty(endpoint.Key))
        {
            _client.DefaultRequestHeaders.Add("x-functions-key", endpoint.Key);
        }
    }

    public void CreateRequest(string key, HttpRequestMessage request)
    {
        this[key] = new ApiMessage(request);
    }
    
    
    public async Task Send()
    {
        foreach (var message in this.Where(m => m.Value.Response is null))
        {
            message.Value.Response = await _client.SendAsync(message.Value.Request);
        }
    }
    
    public class ApiMessage
    {
        public ApiMessage(HttpRequestMessage request)
        {
            Request = request;
        }

        public HttpRequestMessage Request { get; }
        public HttpResponseMessage? Response { get; set; }
    }
}