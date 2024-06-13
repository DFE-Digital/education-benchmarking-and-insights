using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.CustomData;

[ExcludeFromCodeCoverage]
[Table("UserData")]
public record CustomDataUserData
{
    [ExplicitKey] public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? Type { get; set; }

    public string? OrganisationType { get; set; }

    public string? OrganisationId { get; set; }
    public DateTimeOffset Expiry { get; set; }
    public string? Status { get; set; }


    public static CustomDataUserData School(string? id, string? userId, string? urn)
    {
        return new CustomDataUserData
        {
            Id = id,
            UserId = userId,
            Type = "custom-data",
            OrganisationType = "school",
            OrganisationId = urn,
            Expiry = DateTimeOffset.Now.AddDays(30),
            Status = "pending"
        };
    }
}