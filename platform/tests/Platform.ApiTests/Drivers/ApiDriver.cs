namespace Platform.ApiTests.Drivers;

public abstract class ApiDriver : Dictionary<string, ApiDriver.ApiMessage>
{
    private readonly HttpClient _client;
    private readonly IReqnrollOutputHelper _output;

    protected ApiDriver(TestConfiguration.ApiEndpoint endpoint, IReqnrollOutputHelper output)
    {
        _client = new HttpClient
        {
            BaseAddress = new Uri(endpoint.Host)
        };
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
            var content = message.Value.Request.Content;
            string? body = null;
            if (content != null)
            {
                body = await content.ReadAsStringAsync();
            }

            _output.WriteLine(
                $"{response.RequestMessage?.Method}{(body == null ? string.Empty : $" {body}")} {response.RequestMessage?.RequestUri} [{(int)response.StatusCode}]");
#endif
            message.Value.Response = response;
            message.Value.Pending = false;
        }
    }

    public class ApiMessage(HttpRequestMessage request)
    {
        private HttpResponseMessage? _response;

        public HttpRequestMessage Request { get; } = request;
        public bool Pending { get; set; } = true;

        public HttpResponseMessage Response
        {
            get => _response ?? throw new InvalidOperationException($"{nameof(Response)} must be assigned a non-null value before being read");
            set => _response = value;
        }
    }
}