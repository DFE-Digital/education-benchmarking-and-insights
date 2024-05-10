using System.ComponentModel.DataAnnotations;

namespace Web.App.Attributes;

public class PositiveNumericValueAttribute : RangeAttribute
{
    public PositiveNumericValueAttribute() : base(0, double.MaxValue)
    {
        ErrorMessage = "Please enter positive values only for {0}";
    }
}