namespace Web.App.Infrastructure.Apis;

public record CharacteristicList(params string[] Values);

public record CharacteristicValueBool(bool Values);

public record CharacteristicRange
{
    public CharacteristicRange(decimal? from, decimal? to)
    {
        From = from.GetValueOrDefault();
        To = to.GetValueOrDefault();
    }

    public CharacteristicRange(int? from, int? to)
    {
        From = Convert.ToDecimal(from);
        To = Convert.ToDecimal(to);
    }

    public decimal From { get; }
    public decimal To { get; }
}

public record CharacteristicDateRange(DateTime? From, DateTime? To);