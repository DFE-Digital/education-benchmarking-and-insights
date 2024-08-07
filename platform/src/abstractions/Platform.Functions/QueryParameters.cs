using System.Collections.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
namespace Platform.Functions;

public abstract record QueryParameters
{
    // ReSharper disable once MemberCanBeProtected.Global
    public abstract void SetValues(IQueryCollection query);

    public void SetValues(NameValueCollection query)
    {
        var queryParameters = new Dictionary<string, StringValues>();

        foreach (var key in query.AllKeys.Where(k => !string.IsNullOrWhiteSpace(k)).Cast<string>())
        {
            var values = query.GetValues(key);
            if (values != null)
            {
                queryParameters[key] = new StringValues(values);
            }
        }

        SetValues(new QueryCollection(queryParameters));
    }
}