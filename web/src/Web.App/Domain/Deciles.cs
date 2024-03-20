namespace Web.App.Domain;

public interface IDecileHandler
{
    public int FindDecile();
}

public class DecileHandler : IDecileHandler
{
    private readonly decimal _value;
    private readonly decimal[] _values;
    private readonly bool _searchTopDown;

    public DecileHandler(decimal value, decimal[] values, bool searchTopDown = false)
    {
        _value = value;
        _values = values.Length >= 10
            ? values
            : throw new ArgumentException("There must be at least 10 values to calculate deciles.");
        _searchTopDown = searchTopDown;
    }

    public int FindDecile()
    {
        var upperBounds = GetUpperBoundsForDeciles();

        var matchingDeciles = upperBounds.Select((upperBound, index) =>
        {
            if (index == 0 && _value <= upperBound)
                return index + 1;
            if (index > 0 && _value > upperBounds[index - 1] && _value <= upperBound)
                return index + 1;
            if (index > 0 && _value == upperBounds[index])
                return index + 1;
            return -1;
        })
        .Where(decile => decile != -1);

        return matchingDeciles.Any()
            ? _searchTopDown
                ? matchingDeciles.Max()
                : matchingDeciles.Min()
            : 10;
    }

    public decimal[] GetUpperBoundsForDeciles()
    {
        Array.Sort(_values);

        var upperBounds = new decimal[9];

        decimal decileSize = _values.Length / 10;

        int sum = 0;
        for (int i = 1; i <= 9; i++)
        {
            var currentDecileSize = (int)Math.Round((decileSize * i) / i);
            
            upperBounds[i - 1] = _values.Skip(sum).Take(currentDecileSize).Max();

            sum += currentDecileSize;
        }

        return upperBounds;
    }
}

