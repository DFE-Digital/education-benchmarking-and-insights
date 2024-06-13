using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
public class ComparatorSetIdsTypeHandler : SqlMapper.TypeHandler<ComparatorSetIds>
{
    public override void SetValue(IDbDataParameter parameter, ComparatorSetIds value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.ToString();
    }

    public override ComparatorSetIds Parse(object? value)
    {
        return ComparatorSetIds.FromString(value?.ToString());
    }
}