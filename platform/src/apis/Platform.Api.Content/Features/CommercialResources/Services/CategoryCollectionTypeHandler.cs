using System.Data;
using Dapper;
using Platform.Api.Content.Features.CommercialResources.Models;

namespace Platform.Api.Content.Features.CommercialResources.Services;

public class CategoryCollectionTypeHandler : SqlMapper.TypeHandler<CategoryCollection>
{
    public override void SetValue(IDbDataParameter parameter, CategoryCollection? value)
    {
        parameter.DbType = DbType.String;
        parameter.Value = value?.ToString();
    }

    public override CategoryCollection Parse(object? value) => CategoryCollection.FromString(value?.ToString());
}