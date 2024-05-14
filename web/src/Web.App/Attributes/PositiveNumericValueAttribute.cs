using System.ComponentModel.DataAnnotations;

namespace Web.App.Attributes;

public class PositiveNumericValueAttribute : RangeAttribute
{
    public PositiveNumericValueAttribute() : base(0, double.MaxValue)
    {
        ErrorMessage = "{0} must be greater than or equal to zero";
    }
}