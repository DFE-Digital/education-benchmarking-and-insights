namespace Web.App.Domain;

public interface IDecileHandler
{
    public int FindDecile();
}

public class DecileHandler : IDecileHandler
{
    private readonly decimal _value;
    private readonly IReadOnlyList<decimal> _values;
    private readonly bool _searchTopDown;

    public DecileHandler(decimal value, IReadOnlyCollection<decimal> values, bool searchTopDown = false)
    {
        if (values.Count < 10)
        {
            throw new ArgumentException("There must be at least 10 values to calculate deciles.");
        }

        _value = value;
        _values = values.Order().ToArray();
        _searchTopDown = searchTopDown;
    }

    public int FindDecile()
    {
        return _searchTopDown ? TopDown() : BottomUp();
    }

    private int TopDown()
    {
        for (var decile = 10; decile > 1; decile--)
        {
            var valueIndex = CountInDecile(decile) - 1;
            if (_value < _values[valueIndex]) continue;

            return decile;
        }

        return 1;
    }

    private int BottomUp()
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

