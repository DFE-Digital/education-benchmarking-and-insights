using System.Globalization;

namespace Web.App.Extensions;

public static class DecimalExtensions
{
    public static string ToCurrency(this decimal? value) => value.HasValue ? value.Value.ToCurrency() : string.Empty;

    public static string ToCurrency(this decimal value, int decimalDigits = 0)
    {
        var nfi = new CultureInfo("en-GB").NumberFormat;
        nfi.CurrencyDecimalDigits = decimalDigits;
        return value.ToString("C", nfi);
    }

    public static string ToHeadlineStatisticCurrency(this decimal? value) => value.HasValue ? value.Value.ToHeadlineStatisticCurrency() : string.Empty;

    public static string ToHeadlineStatisticCurrency(this decimal value)
    {
        if (Math.Abs(value) <= 999999) // only handle values that round to less than a million with straight currency formatting. 999999.99 rounds to 1000000 but should be displayed as "£1 million" 
        {
            return value.ToCurrency();
        }

        var sigFigs = (value).ToSignificantFigures(3); // get three significant figures
        var inMillions = sigFigs / 1000000; // the output unit is millions
        var currency = inMillions.ToCurrency(inMillions % 1 == 0 ? 0 : 2); // if the value has no decimal point, we want no decimal points in the result
        return (currency.Contains('.')
            ? currency.RemoveTrailingChar("0") // strip trailing zeroes if they follow a decimal
            : currency) + " million"; // add the word million
    }

    public static decimal ToSignificantFigures(this decimal num, int n)
    {
        if (num == 0)
        {
            return 0;
        }

        var d = (int)Math.Ceiling(Math.Log10((double)Math.Abs(num)));
        var power = n - d;
        var magnitude = (decimal)Math.Pow(10, power);
        var shifted = Math.Round(num * magnitude, 0, MidpointRounding.AwayFromZero);
        return shifted / magnitude;
    }

    public static string ToPercent(this decimal? value) => value.HasValue ? value.Value.ToPercent() : string.Empty;

    public static string ToPercent(this decimal value) => $"{value:0.#}%";

    public static string ToAge(this decimal? value) => value.HasValue ? value.Value.ToAge() : string.Empty;
    public static string ToAge(this decimal value) => $"{DateTime.UtcNow.Year - value:0} years";

    public static string ToSimpleDisplay(this decimal? value) => value.HasValue ? value.Value.ToSimpleDisplay() : string.Empty;
    public static string ToSimpleDisplay(this decimal value) => $"{value:0.##}";

    public static string ToNumberSeparator(this decimal value) => $"{value:N0}";

    public static decimal? SafeDivide(this decimal? numerator, decimal? denominator)
        => numerator.HasValue && denominator.HasValue
            ? numerator.Value.SafeDivide(denominator.Value)
            : null;
    public static decimal? SafeDivide(this decimal numerator, decimal denominator)
        => denominator == 0 ? null : numerator / denominator;

    public static decimal? SafePercentageOf(this decimal? value, decimal? total)
        => value.HasValue && total.HasValue
            ? value.Value.SafePercentageOf(total.Value)
            : null;
    public static decimal? SafePercentageOf(this decimal value, decimal total)
        => (value * 100m).SafeDivide(total);
}