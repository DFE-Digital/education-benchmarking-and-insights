namespace EducationBenchmarking.Web.A11yTests;

public class BenchmarkApiDriver() : ApiDriver(TestConfiguration.Benchmark);

public abstract class ApiDriver : Dictionary<string, ApiDriver.ApiMessage>
{
    private readonly HttpClient _client;
    protected ApiDriver(TestConfiguration.ApiEndpoint endpoint)
    {
        ArgumentNullException.ThrowIfNull(endpoint.Host);
        
        _client = new HttpClient { BaseAddress = new Uri(endpoint.Host) };
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
        foreach (var message in this.Where(m => m.Value.Pending))
        {
            var response = await _client.SendAsync(message.Value.Request);
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
            get => _response ??  throw new InvalidOperationException($"{nameof(Response)} must be assigned a non-null value before being read");
            set => _response = value;
        }
    }
}