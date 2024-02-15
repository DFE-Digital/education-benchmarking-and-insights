namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class ApiQuery : List<QueryParameter>
{
    public ApiQuery Page(int page = 1, int pageSize = 10)
    {
        Add("page",page.ToString());
        Add("pageSize",pageSize.ToString());

        return this;
    }

    public string ToQueryString()
    {
        return Count > 0 
            ? $"?{string.Join("&", this.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value.ToString())}"))}" 
            : string.Empty;
    }

    private void Add(string key, string value)
    {
        Add(new QueryParameter(key, value));
    }
    public ApiQuery AddIfNotNull(string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return this;

        Add(key,value);
        return this;
    }
}

public class QueryParameter(string key, string value)
{
    public string Key => key;
    public string Value => value;
}