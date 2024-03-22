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
        var bounds = GetUpperBoundsForDeciles();

        return _searchTopDown
            ? TopDown(bounds)
            : BottomUp(bounds);
    }

    
    private int TopDown(IReadOnlyList<decimal> bounds)
    {
        for (var i = bounds.Count; i > 0; i--)
        {
            var bound = bounds[i];
            if (_value > bound) continue;
            
            return i;
        }

        return 1;
    }
    
    private int BottomUp(IReadOnlyList<decimal> bounds)
    {
        for (var i = 0; i < bounds.Count; i++)
        {
            var bound = bounds[i];
            if (_value > bound) continue;
            
            return i + 1;
        }

        return 10;
    }

    private decimal[] GetUpperBoundsForDeciles()
    {
        var values = _values.ToArray();
        var count = _values.Count;
        
        var bounds = new decimal[9];
        for (var i = 0; i <= 8; i++)
        {
            var decile = i + 1;
            var valueIndex = CountInDecile(decile, count) - 1;
            
            bounds[i] = values[valueIndex];
        }
        
        return bounds;
    }

    private static int CountInDecile(int decile, int count)
    {
        var perDecile = count / 10.0;
        return (int)Math.Round(perDecile * decile);
    }
}

