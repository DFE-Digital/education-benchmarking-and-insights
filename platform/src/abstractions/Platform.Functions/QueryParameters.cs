using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Platform.Functions;

[ExcludeFromCodeCoverage]
public abstract record QueryParameters
{
    [Obsolete("Use SetValues(NameValueCollection query) overload instead")]
    public virtual void SetValues(IQueryCollection query)
    {
    }

    public virtual void SetValues(NameValueCollection query)
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