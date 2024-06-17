using Microsoft.AspNetCore.Http;

namespace Platform.Functions;

public abstract record QueryParameters
{
    public abstract void SetValues(IQueryCollection query);
}