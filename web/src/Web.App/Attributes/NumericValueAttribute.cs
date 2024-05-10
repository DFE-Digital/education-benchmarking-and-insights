using System.ComponentModel.DataAnnotations;

namespace Web.App.Attributes;

public class NumericValueAttribute : RegularExpressionAttribute
{
    public NumericValueAttribute() : base(@"^\s*-{0,1}((\d{1,}\.\d{1,})|(\d*))\s*$")
    {
        ErrorMessage = "Please enter numerical values only for {0}";
    }
}