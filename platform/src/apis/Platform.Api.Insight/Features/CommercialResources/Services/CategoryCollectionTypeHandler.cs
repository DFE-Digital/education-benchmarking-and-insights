using System.Data;
using Dapper;
using Platform.Api.Insight.Features.CommercialResources.Models;

namespace Platform.Api.Insight.Features.CommercialResources.Services;

public class CategoryCollectionTypeHandler : SqlMapper.TypeHandler<CategoryCollection>
{
    public override void SetValue(IDbDataParameter parameter, CategoryCollection? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value?.ToString();
    }

    public override CategoryCollection Parse(object? value)
    {
        return CategoryCollection.FromString(value?.ToString());
    }
}