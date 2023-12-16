using Microsoft.Azure.Cosmos;

namespace EducationBenchmarking.Platform.Infrastructure.Cosmos;

public static class CosmosClientFactory
{
    public static CosmosClient Create(string connectionString)
    {
        return new CosmosClient(connectionString, new CosmosClientOptions
        {
#if DEBUG
            //Disabling SSL validation for local emulator
            HttpClientFactory = () =>
            {
                HttpMessageHandler httpMessageHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return new HttpClient(httpMessageHandler);
            }
#endif
        });
    }
}