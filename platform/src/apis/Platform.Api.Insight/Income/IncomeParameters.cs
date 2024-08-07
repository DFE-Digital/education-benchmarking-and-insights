using Microsoft.AspNetCore.Http;
using Platform.Functions;
using Platform.Functions.Extensions;
namespace Platform.Api.Insight.Income;

public record IncomeParameters : QueryParameters
{
    public bool ExcludeCentralServices { get; internal set; }
    public string? Category { get; internal set; }
    public string Dimension { get; internal set; } = IncomeDimensions.Actuals;
    public string[] Schools { get; private set; } = [];
    public string[] Trusts { get; private set; } = [];

    public override void SetValues(IQueryCollection query)
    {
        var dimension = query["dimension"].ToString();
        if (!IncomeDimensions.IsValid(dimension) || string.IsNullOrWhiteSpace(dimension))
        {
            dimension = IncomeDimensions.Actuals;
        }

        var category = query["category"].ToString();
        if (!IncomeCategories.IsValid(category) || string.IsNullOrWhiteSpace(category))
        {
            category = null;
        }

        ExcludeCentralServices = query.ToBool("excludeCentralServices");
        Category = category;
        Dimension = dimension;
        Schools = query.ToStringArray("urns");
        Trusts = query.ToStringArray("companyNumbers");
    }
}