using System.Globalization;
namespace Web.App.Extensions;

public static class DecimalExtensions
{
    public static string ToCurrency(this decimal? value, int decimalDigits = 2) => value.HasValue ? value.Value.ToCurrency(decimalDigits) : string.Empty;

    public static string ToCurrency(this decimal value, int decimalDigits = 2)
    {
        var nfi = new CultureInfo("en-GB").NumberFormat;
        nfi.CurrencyDecimalDigits = decimalDigits;
        return value.ToString("C", nfi);
    }

    public static string ToPercent(this decimal value) => $"{value:0.##}%";

    public static string ToSimpleDisplay(this decimal value) => $"{value:0.##}";

    public static string ToNumberSeparator(this decimal value) => $"{value:N0}";
}