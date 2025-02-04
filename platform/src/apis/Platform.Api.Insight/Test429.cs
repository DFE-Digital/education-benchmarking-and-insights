using System;
using System.Globalization;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Net.Http.Headers;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight;

public class Test429(ILogger<Test429> logger)
{
    [Function("Test429")]
    public HttpResponseData RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "429")] HttpRequestData req)
    {
        logger.LogInformation("C# HTTP trigger function processed a request.");
        var response = req.CreateErrorResponse((int)HttpStatusCode.TooManyRequests);
        response.Headers.Add(HeaderNames.RetryAfter, new TimeSpan(0, 0, 10).TotalSeconds.ToString(CultureInfo.InvariantCulture));
        return response;
    }
}