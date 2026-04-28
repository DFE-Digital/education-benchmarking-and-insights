using System.Collections.Specialized;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.LocalAuthority.Features.Accounts.Parameters;

public record HighNeedsByTransactionTypeParameters : QueryParameters
{
    public string[] Codes { get; private set; } = [];
    public string Dimension { get; private set; } = Dimensions.HighNeeds.PerPupil;
    public string Type { get; private set; } = TransactionType.Budget;

    public override void SetValues(NameValueCollection query)
    {
        Codes = query.ToStringArray("code");

        if (query.TryGetValue("dimension", out var dimension))
        {
            Dimension = dimension;
        }

        if (query.TryGetValue("type", out var type))
        {
            Type = type;
        }
    }
}