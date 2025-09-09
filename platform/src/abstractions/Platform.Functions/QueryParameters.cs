using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

namespace Platform.Functions;

[ExcludeFromCodeCoverage]
public abstract record QueryParameters
{
    public abstract void SetValues(NameValueCollection query);
}