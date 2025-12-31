namespace Web.App.Infrastructure.Apis;

public class LocalAuthorityApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ILocalAuthorityApi
{
    
}

public interface ILocalAuthorityApi
{
    
}