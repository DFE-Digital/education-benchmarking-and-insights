using TechTalk.SpecFlow.Infrastructure;

namespace Platform.ApiTests.Drivers;

public abstract class ApiDriver : Dictionary<string, ApiDriver.ApiMessage>
{
    private readonly HttpClient _client;
    private readonly ISpecFlowOutputHelper _output;

    protected ApiDriver(TestConfiguration.ApiEndpoint endpoint, ISpecFlowOutputHelper output)
    {
        _client = new HttpClient { BaseAddress = new Uri(endpoint.Host) };
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
        foreach (var message in this.Where(m => m.Value.Pending))
        {
            var response = await _client.SendAsync(message.Value.Request);
#if DEBUG
            _output.WriteLine(
                $"{response.RequestMessage?.Method} {response.RequestMessage?.RequestUri} [{(int)response.StatusCode}]");
#endif
            message.Value.Response = response;
            message.Value.Pending = false;
        }
    }

    public class ApiMessage
    {
        private HttpResponseMessage? _response;

        public ApiMessage(HttpRequestMessage request)
        {
            Request = request;
            Pending = true;
        }

        public HttpRequestMessage Request { get; }
        public bool Pending { get; set; }

        public HttpResponseMessage Response
        {
            get => _response ?? throw new InvalidOperationException($"{nameof(Response)} must be assigned a non-null value before being read");
            set => _response = value;
        }
    }
}