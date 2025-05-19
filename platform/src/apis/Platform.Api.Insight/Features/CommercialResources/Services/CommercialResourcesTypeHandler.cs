using System.Data;
using Dapper;
using Platform.Api.Insight.Features.CommercialResources.Models;

namespace Platform.Api.Insight.Features.CommercialResources.Services;

public class CommercialResourcesListTypeHandler : SqlMapper.TypeHandler<CommercialResourcesList>
{
    public override void SetValue(IDbDataParameter parameter, CommercialResourcesList? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value?.ToString();
    }

    public override CommercialResourcesList Parse(object? value)
    {
        return CommercialResourcesList.FromString(value?.ToString());
    }
}