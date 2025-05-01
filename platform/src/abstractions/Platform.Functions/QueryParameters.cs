using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Platform.Functions;

[ExcludeFromCodeCoverage]
public abstract record QueryParameters
{
    public abstract void SetValues(NameValueCollection query);
}