using System.Collections.Generic;

// any types that inherit from a base collection type fail to generate OpenApi spec,
// so until the below is fixed redeclare using base collection type explicitly:
// https://github.com/Azure/azure-functions-openapi-extension/issues/313
namespace Platform.Api.Benchmark.OpenApi;

/// <summary>
/// <see cref="ComparatorSets.ComparatorSetSchool" />
/// </summary>
internal interface IComparatorSetSchool
{
    string? URN { get; }
    string? SetType { get; }
    List<string>? Pupil { get; }
    List<string>? Building { get; }
}

/// <summary>
/// <see cref="ComparatorSets.ComparatorSetUserDefinedSchool" />
/// </summary>
internal interface IComparatorSetUserDefinedSchool
{
    string? RunType { get; }
    string? RunId { get; }
    string? URN { get; }
    public List<string>? Set { get; }
}

/// <summary>
/// <see cref="ComparatorSets.ComparatorSetUserDefinedTrust" />
/// </summary>
internal interface IComparatorSetUserDefinedTrust
{
    string? RunType { get; }
    string? RunId { get; }
    string? CompanyNumber { get; }
    public List<string>? Set { get; }
}