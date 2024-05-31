using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using Dapper;
using Newtonsoft.Json;
using Platform.Functions.Extensions;

namespace Platform.Api.Benchmark.ComparatorSets;

[ExcludeFromCodeCoverage]
public class ComparatorSetTypeHandler : SqlMapper.TypeHandler<string[]>
{
    public override void SetValue(IDbDataParameter parameter, string[] value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value.ToJson(Formatting.None);
    }

    public override string[] Parse(object? value)
    {
        return value == null ? Array.Empty<string>() : value.ToString().FromJson<string[]>();
    }
}