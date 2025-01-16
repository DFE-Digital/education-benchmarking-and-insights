using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;
using Platform.Domain;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Benchmark.CustomData;

[ExcludeFromCodeCoverage]
[Table("UserData")]
public record CustomDataUserData
{
    [ExplicitKey] public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string? Type { get; set; }

    public string? OrganisationType { get; set; }

    public string? OrganisationId { get; set; }
    public DateTimeOffset Expiry { get; set; }
    public string? Status { get; set; }
    public bool Active { get; set; }

    public static CustomDataUserData School(string? id, string? userId, string? urn) => new()
    {
        Id = id,
        UserId = userId,
        Type = Pipeline.JobType.CustomData,
        OrganisationType = "school",
        OrganisationId = urn,
        Expiry = DateTimeOffset.Now.AddDays(30),
        Status = Pipeline.JobStatus.Pending,
        Active = true
    };
}