using System.Collections.Generic;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;

// any types that inherit from a base collection type fail to generate OpenApi spec,
// so until the below is fixed redeclare using base collection type explicitly:
// https://github.com/Azure/azure-functions-openapi-extension/issues/313
namespace Platform.Api.Benchmark.OpenApi;

/// <summary>
///     <see cref="ComparatorSetSchool" />
/// </summary>
internal interface IComparatorSetSchool
{
    string? URN { get; }
    List<string>? Pupil { get; }
    List<string>? Building { get; }
}

/// <summary>
///     <see cref="ComparatorSetUserDefinedSchool" />
/// </summary>
internal interface IComparatorSetUserDefinedSchool
{
    string? RunType { get; }
    string? RunId { get; }
    string? URN { get; }
    List<string>? Set { get; }
}

/// <summary>
///     <see cref="ComparatorSetUserDefinedTrust" />
/// </summary>
internal interface IComparatorSetUserDefinedTrust
{
    string? RunType { get; }
    string? RunId { get; }
    string? CompanyNumber { get; }
    List<string>? Set { get; }
}