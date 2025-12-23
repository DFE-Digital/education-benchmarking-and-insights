using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;

namespace Platform.Api.Trust.Features.Accounts.Parameters;

public record ExpenditureParameters : QueryParameters
{
    public string? Category { get; private set; }
    public string Dimension { get; private set; } = Dimensions.Finance.Actuals;

    public override void SetValues(NameValueCollection query)
    {
        Dimension = query["dimension"] ?? Dimensions.Finance.Actuals;
        Category = query["category"];
    }
}

public record ExpenditureQueryParameters : ExpenditureParameters
{
    public bool ExcludeCentralServices { get; private set; }
    public string[] CompanyNumbers { get; private set; } = [];

    public override void SetValues(NameValueCollection query)
    {
        base.SetValues(query);

        CompanyNumbers = query["companyNumbers"]?.Split(',') ?? [];
        ExcludeCentralServices = bool.TryParse(query["excludeCentralServices"], out var result) && result;
    }
}