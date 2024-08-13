using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Dapper.Contrib.Extensions;
using Platform.Functions.Extensions;
namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
public record ComparatorSetSchool
{
    public string? URN { get; set; }
    public string? SetType { get; set; }

    public ComparatorSetIds? Pupil { get; set; }
    public ComparatorSetIds? Building { get; set; }
}

[ExcludeFromCodeCoverage]
[Table("UserDefinedSchoolComparatorSet")]
public record ComparatorSetUserDefinedSchool
{
    [ExplicitKey] public string? RunType { get; set; }
    [ExplicitKey] public string? RunId { get; set; }
    [ExplicitKey] public string? URN { get; set; }
    public ComparatorSetIds? Set { get; set; }
}

[ExcludeFromCodeCoverage]
[Table("UserDefinedTrustComparatorSet")]
public record ComparatorSetUserDefinedTrust
{
    [ExplicitKey] public string? RunType { get; set; }
    [ExplicitKey] public string? RunId { get; set; }
    [ExplicitKey] public string? CompanyNumber { get; set; }
    public ComparatorSetIds? Set { get; set; }
}

[ExcludeFromCodeCoverage]
public class ComparatorSetIds : List<string>
{
    public override string ToString() => ToArray().ToJson();

    public static ComparatorSetIds FromString(string? value)
    {
        var ids = value?.FromJson<string[]>() ?? Array.Empty<string>();
        var result = new ComparatorSetIds();
        result.AddRange(ids);
        return result;
    }

    public static ComparatorSetIds FromCollection(string[] value)
    {
        var result = new ComparatorSetIds();
        result.AddRange(value);
        return result;
    }
}