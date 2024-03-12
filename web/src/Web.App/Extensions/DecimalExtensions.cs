namespace Web.App.Extensions
{
    public static class DecimalExtensions
    {
        public static string ToCurrency(this decimal? value)
        {
            return value is { } valDecimal
                ? valDecimal.ToCurrency()
                : "";
        }

        public static string ToCurrency(this decimal value)
        {
            return $"{value:C}";
        }

        public static string ToPercent(this decimal value)
        {
            return $"{value:0.##}%";
        }

        public static string ToSimpleDisplay(this decimal value)
        {
            return $"{value:0.##}";
        }
    }
}