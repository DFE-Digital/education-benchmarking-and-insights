using System.Globalization;

namespace Web.App.Extensions;

public static class DecimalExtensions
{
    public static string ToCurrency(this decimal? value)
    {
        return value is { } valDecimal
            ? valDecimal.ToCurrency()
            : "";
    }

    public static string ToCurrency(this decimal value, int decimalDigits = 2)
    {
        var nfi = new CultureInfo("en-GB").NumberFormat;
        nfi.CurrencyDecimalDigits = decimalDigits;
        return value.ToString("C", nfi);
    }

    public static string ToPercent(this decimal value)
    {
        return $"{value:0.##}%";
    }

    public static string ToSimpleDisplay(this decimal value)
    {
        return $"{value:0.##}";
    }
    
    public static string ToNumberSeparator(this decimal value)
    {
        return $"{value:N0}";
    }
}