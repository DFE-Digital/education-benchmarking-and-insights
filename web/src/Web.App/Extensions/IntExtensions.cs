using System.Globalization;

namespace Web.App.Extensions;

public static class IntExtensions
{
    public static string ToFinanceYear(this int value)
    {
        return $"{value - 1} - {value}";
    }

    public static string ToCurrency(this int? value)
    {
        return value is { } valDecimal
            ? valDecimal.ToCurrency()
            : "";
    }

    public static string ToCurrency(this int value, int decimalDigits = 2)
    {
        var nfi = new CultureInfo("en-GB").NumberFormat;
        nfi.CurrencyDecimalDigits = decimalDigits;
        return value.ToString("C", nfi);
    }
}