namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class ApiQuery : Dictionary<string, string>
{
    public ApiQuery Page(int page = 1, int pageSize = 10)
    {
        this["page"] = page.ToString();
        this["pageSize"] = pageSize.ToString();

        return this;
    }

    public string ToQueryString()
    {
        return Count > 0 
            ? $"?{string.Join("&", this.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value.ToString())}"))}" 
            : string.Empty;
    }

    public ApiQuery AddIfNotNull(string key, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return this;

        this[key] = value;
        return this;
    }
}