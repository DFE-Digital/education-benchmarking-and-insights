namespace Web.App.Infrastructure.Apis;

public class SchoolApi(HttpClient httpClient, string? key = default) : ApiBase(httpClient, key), ISchoolApi
{
    
}

public interface ISchoolApi
{
    
}