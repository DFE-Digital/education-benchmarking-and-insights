using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Web.A11yTests.Drivers;

public class BenchmarkApiDriver(IMessageSink messageSink) : ApiDriver(TestConfiguration.Benchmark, messageSink);

public abstract class ApiDriver : Dictionary<string, ApiDriver.ApiMessage>, IDisposable
{
    private readonly HttpClient _client;
    private readonly IMessageSink _messageSink;

    protected ApiDriver(TestConfiguration.ApiEndpoint endpoint, IMessageSink messageSink)
    {
        ArgumentNullException.ThrowIfNull(endpoint.Host);
        _messageSink = messageSink;
        
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
            
            _messageSink.OnMessage(response.ToDiagnosticMessage());
        }
    }
    
    public class ApiMessage(HttpRequestMessage request)
    {
        private HttpResponseMessage? _response;

        public HttpRequestMessage Request { get; } = request;
        public bool Pending { get; set; } = true;

        public HttpResponseMessage Response
        {
            get => _response ??  throw new InvalidOperationException($"{nameof(Response)} must be assigned a non-null value before being read");
            set => _response = value;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _messageSink.OnMessage("Disposing of http client".ToDiagnosticMessage());
            _client.Dispose();
        }
    }
}