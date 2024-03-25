namespace Web.App.Domain;

public interface IDecileFinder
{
    public int Find();
}

public class FindFromHighest : IDecileFinder
{
    private readonly decimal _value;
    private readonly IReadOnlyList<decimal> _values;

    public FindFromHighest(decimal value, IReadOnlyCollection<decimal> values)
    {
        if (values.Count < 10)
        {
            throw new ArgumentException("There must be at least 10 values to calculate deciles.");
        }

        _value = value;
        _values = values.Order().ToArray();
    }

    public int Find()
    {
        for (var decile = 10; decile > 1; decile--)
        {
            var nextValueIndex = CountInDecile(decile - 1) - 1;
            var valueIndex = CountInDecile(decile) - 1;
            if (_value <= _values[nextValueIndex] && _value != _values[valueIndex]) continue;

            return decile;
        }

        return 1;
    }

    private int CountInDecile(int decile)
    {
        var perDecile = _values.Count / 10.0;
        return (int)Math.Round(perDecile * decile, MidpointRounding.AwayFromZero);
    }
}

public class FindFromLowest : IDecileFinder
{
    private readonly decimal _value;
    private readonly IReadOnlyList<decimal> _values;

    public FindFromLowest(decimal value, IReadOnlyCollection<decimal> values)
    {
        if (values.Count < 10)
        {
            throw new ArgumentException("There must be at least 10 values to calculate deciles.");
        }

        _value = value;
        _values = values.Order().ToArray();
    }

    public int Find()
    {
        for (var decile = 1; decile < 10; decile++)
        {
            var valueIndex = CountInDecile(decile) - 1;
            if (_value > _values[valueIndex]) continue;

            return decile;
        }

        return 10;
    }

    private int CountInDecile(int decile)
    {
        var perDecile = _values.Count / 10.0;
        return (int)Math.Round(perDecile * decile, MidpointRounding.AwayFromZero);
    }
}
