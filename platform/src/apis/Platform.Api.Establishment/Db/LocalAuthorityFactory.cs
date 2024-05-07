using System.Diagnostics.CodeAnalysis;
using Platform.Domain;

namespace Platform.Api.Establishment.Db;

[ExcludeFromCodeCoverage]
public static class LocalAuthorityFactory
{
    public static LocalAuthorityResponseModel CreateResponse(LocalAuthorityDataObject dataObject)
    {
        return new LocalAuthorityResponseModel
        {
            Code = dataObject.Code,
            Name = dataObject.Name
        };
    }
}