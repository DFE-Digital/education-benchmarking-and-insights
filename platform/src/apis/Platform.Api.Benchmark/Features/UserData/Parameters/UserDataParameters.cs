using System.Collections.Specialized;
using Platform.Functions;

namespace Platform.Api.Benchmark.Features.UserData.Parameters;

public record UserDataParameters : QueryParameters
{
    public string? UserId { get; private set; }
    public string? Type { get; private set; }
    public string? Status { get; private set; }
    public string? Id { get; private set; }
    public string? OrganisationType { get; private set; }
    public string? OrganisationId { get; private set; }

    public override void SetValues(NameValueCollection query)
    {
        UserId = query["userId"];
        Type = query["type"];
        Status = query["status"];
        Id = query["id"];
        OrganisationType = query["organisationType"];
        OrganisationId = query["organisationId"];
    }
}