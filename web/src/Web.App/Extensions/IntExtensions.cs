namespace Web.App.Extensions;

public static class IntExtensions
{
    public static string ToFinanceYear(this int value)
    {
        return $"{value - 1} - {value}";
    }
}