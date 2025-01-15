using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.UserData;

public record UserDataParameters : QueryParameters
{
    public string[] UserIds { get; private set; } = [];
    public string? Type { get; private set; }
    public string? Status { get; private set; }
    public string? Id { get; private set; }
    public string? OrganisationType { get; private set; }
    public string? OrganisationId { get; private set; }

    public override void SetValues(IQueryCollection query)
    {
        UserIds = query.ToStringArray("userId");
        Type = query["type"];
        Status = query["status"];
        Id = query["id"];
        OrganisationType = query["organisationType"];
        OrganisationId = query["organisationId"];
    }
}