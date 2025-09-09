using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Platform.Api.Benchmark.Features.ComparatorSets.Models;

namespace Platform.Api.Benchmark.Features.ComparatorSets.Services;

[ExcludeFromCodeCoverage]
public class ComparatorSetIdsTypeHandler : SqlMapper.TypeHandler<ComparatorSetIds>
{
    public override void SetValue(IDbDataParameter parameter, ComparatorSetIds? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value?.ToString();
    }

    public override ComparatorSetIds Parse(object? value) => ComparatorSetIds.FromString(value?.ToString());
}