using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
[Table("UserData")]
public record ComparatorSetUserData
{
    [ExplicitKey] public string? Id { get; set; }
    public string? UserId { get; set; }
    public string? Type { get; set; }

    public string? OrganisationType { get; set; }

    public string? OrganisationId { get; set; }
    public DateTimeOffset Expiry { get; set; }
    public string? Status { get; set; }

    public static ComparatorSetUserData PendingSchool(string? id, string? userId, string? urn)
    {
        return new ComparatorSetUserData
        {
            Id = id,
            UserId = userId,
            Type = "comparator-set",
            OrganisationType = "school",
            OrganisationId = urn,
            Expiry = DateTimeOffset.Now.AddDays(30),
            Status = "pending"
        };
    }

    public static ComparatorSetUserData CompleteSchool(string? id, string? userId, string? urn)
    {
        return new ComparatorSetUserData
        {
            Id = id,
            UserId = userId,
            Type = "comparator-set",
            OrganisationType = "school",
            OrganisationId = urn,
            Expiry = DateTimeOffset.Now.AddDays(30),
            Status = "complete"
        };
    }
}