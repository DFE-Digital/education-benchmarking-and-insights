using System.Collections.Specialized;
using System.Text;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Primitives;
using Moq;
namespace Platform.Tests.Mocks;

public abstract class MockHttpRequestData
{
    public static HttpRequestData Create(Dictionary<string, StringValues>? query = null) => Create<string>("", query);

    public static HttpRequestData Create<T>(T requestData, Dictionary<string, StringValues>? query = null) where T : class
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddFunctionsWorkerDefaults();

        var serializedData = JsonSerializer.Serialize(requestData);
        var bodyDataStream = new MemoryStream(Encoding.UTF8.GetBytes(serializedData));

        var context = new Mock<FunctionContext>();
        context.SetupProperty(functionContext => functionContext.InstanceServices, serviceCollection.BuildServiceProvider());

        var queryCollection = new NameValueCollection();
        if (query != null)
        {
            foreach (var key in query.Keys)
            {
                foreach (var value in query[key])
                {
                    queryCollection.Add(key, value);
                }
            }
        }

        var request = new Mock<HttpRequestData>(context.Object);
        request.Setup(r => r.Body).Returns(bodyDataStream);
        request.Setup(r => r.Headers).Returns([]);
        request.Setup(r => r.Query).Returns(queryCollection);
        request.Setup(r => r.CreateResponse()).Returns(new MockHttpResponseData(context.Object));

        return request.Object;
    }
}