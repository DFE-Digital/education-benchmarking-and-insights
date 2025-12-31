namespace Web.App.Infrastructure.Apis;

public class TrustApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ITrustApi
{
    
}

public interface ITrustApi
{
    
}