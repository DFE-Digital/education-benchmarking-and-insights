using System;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;
using Platform.Domain;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Platform.Api.Benchmark.Features.ComparatorSets.Models;

[ExcludeFromCodeCoverage]
[Table("UserData")]
public record ComparatorSetUserData
{
    [ExplicitKey] public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string? Type { get; set; }

    public string? OrganisationType { get; set; }

    public string? OrganisationId { get; set; }
    public DateTimeOffset Expiry { get; set; }
    public string? Status { get; set; }
    public bool Active { get; set; }

    public static ComparatorSetUserData PendingSchool(string id, string? userId, string? urn) => new()
    {
        Id = id,
        UserId = userId,
        Type = Pipeline.JobType.ComparatorSet,
        OrganisationType = "school",
        OrganisationId = urn,
        Expiry = DateTimeOffset.Now.AddDays(30),
        Status = Pipeline.JobStatus.Pending,
        Active = true
    };

    public static ComparatorSetUserData CompleteSchool(string id, string? userId, string? urn) => new()
    {
        Id = id,
        UserId = userId,
        Type = Pipeline.JobType.ComparatorSet,
        OrganisationType = "school",
        OrganisationId = urn,
        Expiry = DateTimeOffset.Now.AddDays(30),
        Status = Pipeline.JobStatus.Complete,
        Active = true
    };

    public static ComparatorSetUserData CompleteTrust(string id, string? userId, string companyNumber) => new()
    {
        Id = id,
        UserId = userId,
        Type = Pipeline.JobType.ComparatorSet,
        OrganisationType = "trust",
        OrganisationId = companyNumber,
        Expiry = DateTimeOffset.Now.AddDays(30),
        Status = Pipeline.JobStatus.Complete,
        Active = true
    };
}