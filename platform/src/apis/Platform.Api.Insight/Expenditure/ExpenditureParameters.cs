using System;
using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight.Expenditure;

public record ExpenditureParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; private set; }
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = ExpenditureDimensions.Actuals;
    public string[] Schools { get; private set; } = Array.Empty<string>();
    public string[] Trusts { get; private set; } = Array.Empty<string>();

    public override void SetValues(IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!ExpenditureDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = ExpenditureDimensions.Actuals;
        }

        var category = query["category"].ToString();
        if (!ExpenditureCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
        {
            category = null;
        }

        ExcludeCentralServices = query.ToBool("excludeCentralServices");
        Category = category;
        Dimension = dimension;
        Schools = query.ToStringArray("urns");
        Trusts = query.ToStringArray("companyNumbers"); ;
    }
}