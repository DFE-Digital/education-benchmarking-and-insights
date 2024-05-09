using System.ComponentModel.DataAnnotations;

namespace Web.App.Attributes;

public class CustomDataPositiveValueAttribute : RangeAttribute
{
    public CustomDataPositiveValueAttribute() : base(0, double.PositiveInfinity)
    {
        ErrorMessage = "Please enter positive values only for {0}";
    }
}