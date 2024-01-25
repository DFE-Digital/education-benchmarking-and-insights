using EducationBenchmarking.Platform.ApiTests.TestSupport;
using TechTalk.SpecFlow.Infrastructure;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Drivers;

public abstract class ApiDriver : Dictionary<string, ApiDriver.ApiMessage>
{
    private readonly HttpClient _client;
    private readonly ISpecFlowOutputHelper _output;

    protected ApiDriver(Config.Api.ApiEndpoint? endpoint, ISpecFlowOutputHelper output)
    {
        ArgumentNullException.ThrowIfNull(endpoint);

        _client = new HttpClient { BaseAddress = new Uri(endpoint.Host ?? throw new NullException(endpoint.Host)) };
        if (!string.IsNullOrEmpty(endpoint.Key))
        {
            _client.DefaultRequestHeaders.Add("x-functions-key", endpoint.Key);
        }

        _output = output;
    }

    public void CreateRequest(string key, HttpRequestMessage request)
    {
        this[key] = new ApiMessage(request);
    }


    public async Task Send()
    {
        foreach (var message in this.Where(m => m.Value.Response is null))
        {
            var response = await _client.SendAsync(message.Value.Request);
#if DEBUG
            _output.WriteLine(
                $"{response.RequestMessage?.Method} {response.RequestMessage?.RequestUri} [{(int)response.StatusCode}]");
#endif
            message.Value.Response = response;
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